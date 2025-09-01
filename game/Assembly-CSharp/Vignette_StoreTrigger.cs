using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class Vignette_StoreTrigger : VignetteTrigger
{
	// Token: 0x0600088C RID: 2188 RVA: 0x0003AC08 File Offset: 0x00038E08
	public override void Start()
	{
		base.Start();
		this.currentCost = this.Cost;
		if (this.CanHaveSale && GameplayManager.HasGameOverride("SALE_AVAILABLE") && (float)UnityEngine.Random.Range(0, 100) < 25f)
		{
			this.currentCost = this.SaleCost;
			if (this.SaleDisplay != null)
			{
				this.SaleDisplay.SetActive(true);
			}
		}
		this.CostText.text = this.currentCost.ToString();
		if (this.ItemType == Vignette_StoreTrigger.StoreItemType.AugmentScroll)
		{
			base.StartCoroutine(this.CreateScroll());
		}
		if (this.ItemType == Vignette_StoreTrigger.StoreItemType.StatusAugment)
		{
			base.StartCoroutine(this.CreateStatusScroll());
		}
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0003ACB4 File Offset: 0x00038EB4
	public override void Update()
	{
		PlayerControl myInstance = PlayerControl.myInstance;
		int? num;
		if (myInstance == null)
		{
			num = null;
		}
		else
		{
			EntityControl.AppliedStatus statusInfoByID = myInstance.GetStatusInfoByID(this.CurrencyStatus.ID, -1);
			num = ((statusInfoByID != null) ? new int?(statusInfoByID.Stacks) : null);
		}
		int? num2 = num;
		this.currencyValue = num2.GetValueOrDefault();
		this.CostText.color = ((this.currencyValue >= this.currentCost) ? Color.white : Color.red);
		this.CostGroup.UpdateOpacity(base.IsInRange && this.CanBePurchased(), 3f, true);
		base.Update();
		if (!this.IsAvailable && this.CanActivate())
		{
			this.Activate();
		}
		if (this.scroll != null)
		{
			this.scroll.CanCollect = this.CanActivate();
		}
		if (this.status != null)
		{
			this.status.CanCollect = this.CanActivate();
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0003ADB0 File Offset: 0x00038FB0
	public override void Select()
	{
		PlayerControl.myInstance.RemoveStatusEffect(this.CurrencyStatus.HashCode, PlayerControl.myInstance.ViewID, this.currentCost, false, false);
		base.Select();
		this.HasUsed = true;
		if (this.ItemType == Vignette_StoreTrigger.StoreItemType.AugmentChoice)
		{
			InkManager.instance.AwardPlayerPageChoice(this.Filter, null);
			ParticleSystem choiceFXDisplay = this.ChoiceFXDisplay;
			if (choiceFXDisplay == null)
			{
				return;
			}
			choiceFXDisplay.Stop();
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0003AE1B File Offset: 0x0003901B
	public override bool CanActivate()
	{
		return this.currencyValue >= this.currentCost && this.CanBePurchased();
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0003AE33 File Offset: 0x00039033
	private bool CanBePurchased()
	{
		if (VignetteControl.instance == null)
		{
			return false;
		}
		VignetteControl instance = VignetteControl.instance;
		string actionID = this.ActionID;
		PlayerControl myInstance = PlayerControl.myInstance;
		return instance.CanActivate(actionID, (myInstance != null) ? myInstance.ViewID : -1);
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0003AE68 File Offset: 0x00039068
	public void Reroll()
	{
		if (!base.gameObject.activeInHierarchy || this.HasUsed)
		{
			return;
		}
		if (this.ItemType == Vignette_StoreTrigger.StoreItemType.AugmentScroll)
		{
			UnityEngine.Debug.Log("Rerolling Augment Scroll");
			if (this.scroll != null)
			{
				UnityEngine.Object.Destroy(this.scroll.gameObject);
			}
			base.StartCoroutine(this.CreateScroll());
		}
		if (this.ItemType == Vignette_StoreTrigger.StoreItemType.StatusAugment)
		{
			UnityEngine.Debug.Log("Rerolling Status Scroll");
			if (this.status != null)
			{
				UnityEngine.Object.Destroy(this.status.gameObject);
			}
			base.StartCoroutine(this.CreateStatusScroll());
		}
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0003AF07 File Offset: 0x00039107
	private IEnumerator CreateScroll()
	{
		while (GoalManager.instance == null)
		{
			yield return true;
		}
		yield return true;
		if (this.ScrollPrefab == null)
		{
			this.scroll = GoalManager.instance.GiveAugmentScroll(this.Filter, base.transform.position);
		}
		else
		{
			this.CreateCustomScroll();
		}
		if (this.scroll == null)
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			ScrollPickup scrollPickup = this.scroll;
			scrollPickup.OnCollect = (Action)Delegate.Combine(scrollPickup.OnCollect, new Action(this.Select));
		}
		yield break;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0003AF18 File Offset: 0x00039118
	private void CreateCustomScroll()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ScrollPrefab);
		gameObject.SetActive(true);
		List<AugmentTree> validMods = GraphDB.GetValidMods(ModType.Player);
		this.Filter.FilterList(validMods, PlayerControl.myInstance);
		AugmentTree augment = validMods[UnityEngine.Random.Range(0, validMods.Count)];
		this.scroll = gameObject.GetComponent<ScrollPickup>();
		this.scroll.Setup(augment);
		AudioManager.PlayLoudClipAtPoint(this.OnActivateSFX, gameObject.transform.position, 1f, 1f, 1f, 10f, 250f);
		AudioManager.ClipPlayed(this.OnActivateSFX, 0.075f);
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0003AFBB File Offset: 0x000391BB
	private IEnumerator CreateStatusScroll()
	{
		while (GoalManager.instance == null)
		{
			yield return true;
		}
		yield return true;
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ScrollPrefab, this.ScrollPrefab.transform.position, this.ScrollPrefab.transform.rotation);
		gameObject.SetActive(true);
		this.status = gameObject.GetComponent<StatusPickup>();
		if (this.status != null)
		{
			this.status.Setup(this.StatusOptions[UnityEngine.Random.Range(0, this.StatusOptions.Count)], this.Stacks, 0f);
			StatusPickup statusPickup = this.status;
			statusPickup.OnCollect = (Action)Delegate.Combine(statusPickup.OnCollect, new Action(this.Select));
		}
		else
		{
			UnityEngine.Object.Destroy(gameObject);
			base.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0003AFCA File Offset: 0x000391CA
	public Vignette_StoreTrigger()
	{
	}

	// Token: 0x04000734 RID: 1844
	public StatusTree CurrencyStatus;

	// Token: 0x04000735 RID: 1845
	public CanvasGroup CostGroup;

	// Token: 0x04000736 RID: 1846
	public TextMeshProUGUI CostText;

	// Token: 0x04000737 RID: 1847
	public Vignette_StoreTrigger.StoreItemType ItemType;

	// Token: 0x04000738 RID: 1848
	public int Cost;

	// Token: 0x04000739 RID: 1849
	public bool CanHaveSale;

	// Token: 0x0400073A RID: 1850
	public int SaleCost;

	// Token: 0x0400073B RID: 1851
	public GameObject SaleDisplay;

	// Token: 0x0400073C RID: 1852
	public AugmentFilter Filter;

	// Token: 0x0400073D RID: 1853
	public List<StatusTree> StatusOptions;

	// Token: 0x0400073E RID: 1854
	public int Stacks = 1;

	// Token: 0x0400073F RID: 1855
	public GameObject ScrollPrefab;

	// Token: 0x04000740 RID: 1856
	public AudioClip OnActivateSFX;

	// Token: 0x04000741 RID: 1857
	public ParticleSystem ChoiceFXDisplay;

	// Token: 0x04000742 RID: 1858
	private ScrollPickup scroll;

	// Token: 0x04000743 RID: 1859
	private StatusPickup status;

	// Token: 0x04000744 RID: 1860
	private int currencyValue;

	// Token: 0x04000745 RID: 1861
	private int currentCost;

	// Token: 0x04000746 RID: 1862
	public bool HasUsed;

	// Token: 0x020004B6 RID: 1206
	public enum StoreItemType
	{
		// Token: 0x0400241B RID: 9243
		VignetteAction,
		// Token: 0x0400241C RID: 9244
		AugmentScroll,
		// Token: 0x0400241D RID: 9245
		StatusAugment,
		// Token: 0x0400241E RID: 9246
		AugmentChoice
	}

	// Token: 0x020004B7 RID: 1207
	[CompilerGenerated]
	private sealed class <CreateScroll>d__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002275 RID: 8821 RVA: 0x000C6DFB File Offset: 0x000C4FFB
		[DebuggerHidden]
		public <CreateScroll>d__25(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000C6E0A File Offset: 0x000C500A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000C6E0C File Offset: 0x000C500C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Vignette_StoreTrigger vignette_StoreTrigger = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				if (vignette_StoreTrigger.ScrollPrefab == null)
				{
					vignette_StoreTrigger.scroll = GoalManager.instance.GiveAugmentScroll(vignette_StoreTrigger.Filter, vignette_StoreTrigger.transform.position);
				}
				else
				{
					vignette_StoreTrigger.CreateCustomScroll();
				}
				if (vignette_StoreTrigger.scroll == null)
				{
					vignette_StoreTrigger.gameObject.SetActive(false);
				}
				else
				{
					ScrollPickup scroll = vignette_StoreTrigger.scroll;
					scroll.OnCollect = (Action)Delegate.Combine(scroll.OnCollect, new Action(vignette_StoreTrigger.Select));
				}
				return false;
			default:
				return false;
			}
			if (!(GoalManager.instance == null))
			{
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x000C6F05 File Offset: 0x000C5105
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x000C6F0D File Offset: 0x000C510D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x000C6F14 File Offset: 0x000C5114
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400241F RID: 9247
		private int <>1__state;

		// Token: 0x04002420 RID: 9248
		private object <>2__current;

		// Token: 0x04002421 RID: 9249
		public Vignette_StoreTrigger <>4__this;
	}

	// Token: 0x020004B8 RID: 1208
	[CompilerGenerated]
	private sealed class <CreateStatusScroll>d__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600227B RID: 8827 RVA: 0x000C6F1C File Offset: 0x000C511C
		[DebuggerHidden]
		public <CreateStatusScroll>d__27(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x000C6F2B File Offset: 0x000C512B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x000C6F30 File Offset: 0x000C5130
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Vignette_StoreTrigger vignette_StoreTrigger = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
			{
				this.<>1__state = -1;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(vignette_StoreTrigger.ScrollPrefab, vignette_StoreTrigger.ScrollPrefab.transform.position, vignette_StoreTrigger.ScrollPrefab.transform.rotation);
				gameObject.SetActive(true);
				vignette_StoreTrigger.status = gameObject.GetComponent<StatusPickup>();
				if (vignette_StoreTrigger.status != null)
				{
					vignette_StoreTrigger.status.Setup(vignette_StoreTrigger.StatusOptions[UnityEngine.Random.Range(0, vignette_StoreTrigger.StatusOptions.Count)], vignette_StoreTrigger.Stacks, 0f);
					StatusPickup status = vignette_StoreTrigger.status;
					status.OnCollect = (Action)Delegate.Combine(status.OnCollect, new Action(vignette_StoreTrigger.Select));
				}
				else
				{
					UnityEngine.Object.Destroy(gameObject);
					vignette_StoreTrigger.gameObject.SetActive(false);
				}
				return false;
			}
			default:
				return false;
			}
			if (!(GoalManager.instance == null))
			{
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000C7069 File Offset: 0x000C5269
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000C7071 File Offset: 0x000C5271
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000C7078 File Offset: 0x000C5278
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002422 RID: 9250
		private int <>1__state;

		// Token: 0x04002423 RID: 9251
		private object <>2__current;

		// Token: 0x04002424 RID: 9252
		public Vignette_StoreTrigger <>4__this;
	}
}
