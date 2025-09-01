using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001E8 RID: 488
public class NookPanel : MonoBehaviour
{
	// Token: 0x06001497 RID: 5271 RVA: 0x00080590 File Offset: 0x0007E790
	private void Awake()
	{
		NookPanel.instance = this;
		this.NookElementRef.SetActive(false);
		UIPanel component = base.GetComponent<UIPanel>();
		component.OnEnteredPanel = (Action)Delegate.Combine(component.OnEnteredPanel, new Action(this.OnPanelEnter));
		component.OnNextTab = (Action)Delegate.Combine(component.OnNextTab, new Action(this.NextTab));
		component.OnPrevTab = (Action)Delegate.Combine(component.OnPrevTab, new Action(this.PrevTab));
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x0008061A File Offset: 0x0007E81A
	private void OnPanelEnter()
	{
		this.SelectTab(this.CurTab, true, false);
		this.UpdateCurrencyInfo();
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x00080630 File Offset: 0x0007E830
	private void Update()
	{
		if (PanelManager.CurPanel != PanelType.NookCustomize)
		{
			return;
		}
		if (PlayerControl.myInstance != null && PlayerControl.myInstance.actions.Input.actions.Ping.WasPressed)
		{
			NookPanel.Toggle();
		}
		if (this.hoveredItem != null && this.hoveredItem.ObjRef.UnlockedBy == Unlockable.UnlockType.Purchase && !this.hoveredItem.isUnlocked && Currency.Gildings >= this.hoveredItem.ObjRef.Cost)
		{
			if (InputManager.IsUsingController)
			{
				if (InputManager.UIAct.UISecondary.IsPressed)
				{
					this.holdT += Time.deltaTime;
				}
				else if (this.holdT > 0f)
				{
					this.holdT -= Time.deltaTime * 2f;
				}
			}
			else if (InputManager.UIAct.UIPrimary.IsPressed)
			{
				this.holdT += Time.deltaTime;
			}
			else if (this.holdT > 0f)
			{
				this.holdT -= Time.deltaTime * 2f;
			}
			if (this.holdT >= this.HoldTime)
			{
				this.TryPurchase(this.hoveredItem.ObjRef);
				this.justPurchased = true;
				this.holdT = 0f;
			}
		}
		else
		{
			this.holdT = 0f;
		}
		if (this.justPurchased && !InputManager.UIAct.UIPrimary.IsPressed && !InputManager.UIAct.UISecondary.IsPressed)
		{
			this.justPurchased = false;
		}
		foreach (Image image in this.PurchaseFills)
		{
			image.fillAmount = this.holdT / this.HoldTime;
		}
		if (InputManager.UIAct.UIBack.WasPressed)
		{
			base.StartCoroutine("GoBack");
		}
		if (InputManager.IsUsingController)
		{
			this.Scroller.TickUpdate();
		}
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x00080858 File Offset: 0x0007EA58
	private IEnumerator GoBack()
	{
		yield return new WaitForEndOfFrame();
		NookPanel.Toggle();
		yield break;
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x00080860 File Offset: 0x0007EA60
	public static void Toggle()
	{
		if (PanelManager.CurPanel == PanelType.NookCustomize)
		{
			PanelManager.instance.PopPanel();
			NookPanel.instance.CancelPreview();
			return;
		}
		if (PlayerNook.IsInEditMode)
		{
			PanelManager.instance.PushPanel(PanelType.NookCustomize);
			if (PlayerNook.MyNook.HeldItem != null)
			{
				PlayerNook.MyNook.HeldItem.CancelPlacement(PlayerNook.MyNook);
			}
		}
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x000808C4 File Offset: 0x0007EAC4
	private void NextTab()
	{
		int num = this.CurrentTabIndex();
		num++;
		if (num >= this.Tabs.Count)
		{
			num = 0;
		}
		this.SelectTab(this.Tabs[num].Tab, false, true);
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x00080908 File Offset: 0x0007EB08
	private void PrevTab()
	{
		int num = this.CurrentTabIndex();
		num--;
		if (num < 0)
		{
			num = this.Tabs.Count - 1;
		}
		this.SelectTab(this.Tabs[num].Tab, false, true);
	}

	// Token: 0x0600149E RID: 5278 RVA: 0x0008094C File Offset: 0x0007EB4C
	public void SelectTab(AugmentsPanel.BookTab tab, bool force = false, bool scrollToTop = false)
	{
		if (this.CurTab == tab && !force)
		{
			return;
		}
		this.CurTab = tab;
		this.holdT = 0f;
		foreach (AugmentsTabElement augmentsTabElement in this.Tabs)
		{
			if (augmentsTabElement.Tab == this.CurTab)
			{
				augmentsTabElement.Select();
			}
			else
			{
				augmentsTabElement.Release();
			}
		}
		this.GenerateList(scrollToTop);
	}

	// Token: 0x0600149F RID: 5279 RVA: 0x000809DC File Offset: 0x0007EBDC
	private int CurrentTabIndex()
	{
		for (int i = 0; i < this.Tabs.Count; i++)
		{
			if (this.Tabs[i].Tab == this.CurTab)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x060014A0 RID: 5280 RVA: 0x00080A1C File Offset: 0x0007EC1C
	public void GenerateList(bool scrollToTop = false)
	{
		foreach (NookPanelItem nookPanelItem in this.GridItems)
		{
			UnityEngine.Object.Destroy(nookPanelItem.gameObject);
		}
		this.GridItems.Clear();
		List<NookDB.NookObject> list;
		switch (this.CurTab)
		{
		case AugmentsPanel.BookTab.Nook_Furniture:
			list = NookDB.DB.Furniture;
			break;
		case AugmentsPanel.BookTab.Nook_Furnishings:
			list = NookDB.DB.Furnishings;
			break;
		case AugmentsPanel.BookTab.Nook_Decorations:
			list = NookDB.DB.SmallItems;
			break;
		case AugmentsPanel.BookTab.Nook_Utility:
			list = NookDB.DB.Utility;
			break;
		case AugmentsPanel.BookTab.Nook_Treasures:
			list = NookDB.DB.Treasures;
			break;
		case AugmentsPanel.BookTab.Nook_Knowledge:
			list = NookDB.DB.Knowledge;
			break;
		default:
			list = null;
			break;
		}
		List<NookDB.NookObject> list2 = list.Clone<NookDB.NookObject>();
		for (int i = list2.Count - 1; i >= 0; i--)
		{
			if (!UnlockManager.IsNookItemUnlocked(list2[i]))
			{
				NookDB.NookObject item = list2[i];
				list2.RemoveAt(i);
				list2.Add(item);
			}
		}
		foreach (NookDB.NookObject item2 in list2)
		{
			this.AddGridItem(item2);
		}
		float width = this.GridLayout.GetComponent<RectTransform>().rect.width;
		float num = this.GridLayout.cellSize.x + this.GridLayout.spacing.x;
		int perRow = Mathf.FloorToInt(width / num);
		UISelector.SetupGridListNav<NookPanelItem>(this.GridItems, perRow, null, null, true);
		if (InputManager.IsUsingController)
		{
			UISelector.SelectSelectable(this.GridItems[0].GetComponent<Button>());
		}
		if (scrollToTop)
		{
			this.Scroller.GetComponent<ScrollRect>().verticalNormalizedPosition = 1f;
		}
	}

	// Token: 0x060014A1 RID: 5281 RVA: 0x00080C10 File Offset: 0x0007EE10
	private void AddGridItem(NookDB.NookObject item)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.NookElementRef, this.NookElementRef.transform.parent);
		gameObject.SetActive(true);
		NookPanelItem component = gameObject.GetComponent<NookPanelItem>();
		component.Setup(item);
		this.GridItems.Add(component);
	}

	// Token: 0x060014A2 RID: 5282 RVA: 0x00080C58 File Offset: 0x0007EE58
	private void UpdateInfoDisplay(NookDB.NookObject obj)
	{
		this.ItemName.text = obj.Name;
		if (UnlockManager.IsNookItemUnlocked(obj))
		{
			this.AchievementDisplay.SetActive(false);
			this.DropDisplay.SetActive(false);
			this.RaidDisplay.SetActive(false);
			this.CostDisplay.SetActive(false);
			TextMeshProUGUI unlockedText = this.UnlockedText;
			string text;
			switch (obj.UnlockedBy)
			{
			case Unlockable.UnlockType.Default:
				text = "Available to all Scribes.";
				break;
			case Unlockable.UnlockType.Purchase:
				text = "Unlocked by Purchasing.";
				break;
			case Unlockable.UnlockType.Achievement:
				text = "Unlocked: " + TextParser.AugmentDetail(obj.GetUnlockReqText(), null, null, false);
				break;
			case Unlockable.UnlockType.Drop:
				text = "Found in a Tome.";
				break;
			case Unlockable.UnlockType.TomeReward:
				text = "Unlocked by mending " + obj.RewardingTome.Root.ShortName + ".";
				break;
			case Unlockable.UnlockType.Prestige:
				text = string.Format("Unlocked from Scribe Ascension {0}.", obj.PrestigeLevel);
				break;
			default:
				text = "Unknown Unlock Requirements";
				break;
			}
			unlockedText.text = text;
			bool flag = PlayerNook.MyNook.ItemCount >= NookDB.DB.NookLimit;
			this.PlacePrompt.SetActive(!flag);
			this.LimitPrompt.SetActive(flag);
			this.Purchase_Controller.SetActive(false);
			this.Purchase_KBM.SetActive(false);
			return;
		}
		this.LimitPrompt.SetActive(false);
		this.PlacePrompt.SetActive(false);
		this.UnlockedText.text = "";
		bool flag2 = obj.UnlockedBy == Unlockable.UnlockType.Purchase;
		this.AchievementDisplay.SetActive(obj.UnlockedBy == Unlockable.UnlockType.Achievement);
		this.DropDisplay.SetActive(obj.UnlockedBy == Unlockable.UnlockType.Drop);
		this.CostDisplay.SetActive(flag2);
		this.RaidDisplay.SetActive(obj.UnlockedBy == Unlockable.UnlockType.Raid);
		if (obj.UnlockedBy == Unlockable.UnlockType.Achievement)
		{
			this.AchievementText.text = TextParser.AugmentDetail(obj.GetUnlockReqText(), null, null, false);
		}
		else if (obj.UnlockedBy == Unlockable.UnlockType.Drop)
		{
			this.DropText.text = this.GetDropUnlockText();
		}
		else if (obj.UnlockedBy == Unlockable.UnlockType.Raid)
		{
			this.RaidText.text = obj.GetUnlockReqText();
		}
		else if (flag2)
		{
			this.CostText.text = obj.Cost.ToString();
			this.CostText.color = ((obj.Cost <= Currency.Gildings) ? this.CostBaseColor : this.CostHighColor);
		}
		this.Purchase_Controller.SetActive(flag2 && InputManager.IsUsingController);
		this.Purchase_KBM.SetActive(flag2 && !InputManager.IsUsingController);
	}

	// Token: 0x060014A3 RID: 5283 RVA: 0x00080EF0 File Offset: 0x0007F0F0
	private string GetDropUnlockText()
	{
		if (!NookDB.AllowedDrops())
		{
			return "Found in Tomes after mending\n<b>Pride and Profit</b>.";
		}
		return "Found after mending Tomes.";
	}

	// Token: 0x060014A4 RID: 5284 RVA: 0x00080F04 File Offset: 0x0007F104
	private void TryPurchase(NookDB.NookObject item)
	{
		if (item == null || item.UnlockedBy != Unlockable.UnlockType.Purchase)
		{
			return;
		}
		if (item.Cost > Currency.Gildings)
		{
			return;
		}
		if (!Currency.TrySpend(item.Cost))
		{
			return;
		}
		UnlockManager.UnlockNookItem(item);
		NookPanelItem nookPanelItem = this.hoveredItem;
		if (nookPanelItem != null)
		{
			nookPanelItem.UpdateLockDisplay();
		}
		AudioManager.PlayInterfaceSFX(this.PurchaseSFX, 1f, 0f);
		NookPanelItem nookPanelItem2 = this.hoveredItem;
		if (nookPanelItem2 != null)
		{
			nookPanelItem2.PlayPurchaseFX();
		}
		GameObject gameObject = this.preparedItem;
		if (gameObject != null)
		{
			gameObject.GetComponent<NookItem>().SetRimMaterial(NookItem.VisibilityType.Hilight);
		}
		this.UpdateInfoDisplay(item);
		this.UpdateCurrencyInfo();
	}

	// Token: 0x060014A5 RID: 5285 RVA: 0x00080F9B File Offset: 0x0007F19B
	public void Hover(NookPanelItem obj)
	{
		this.hoveredItem = obj;
		if (obj == null)
		{
			this.CancelPreview();
			return;
		}
		this.UpdateInfoDisplay(obj.ObjRef);
		this.PreviewItem(obj.ObjRef);
	}

	// Token: 0x060014A6 RID: 5286 RVA: 0x00080FCC File Offset: 0x0007F1CC
	private void PreviewItem(NookDB.NookObject item)
	{
		this.CancelPreview();
		this.preparedItem = UnityEngine.Object.Instantiate<GameObject>(item.Prefab, PlayerNook.MyNook.RootHolder);
		Transform transform = PlayerControl.MyCamera.transform;
		Vector3 vector = transform.position;
		NookItem component = this.preparedItem.GetComponent<NookItem>();
		float d = Mathf.Clamp(2f * component.Size, 1.5f, 10f);
		vector += transform.forward * d;
		vector += transform.up * 0.1f * d;
		if (component.AllowedSurfaces == NookSurface.SurfaceType.Flat)
		{
			vector -= transform.up * 0.15f * d;
		}
		this.preparedItem.transform.SetPositionAndRotation(vector, item.Prefab.transform.rotation);
		component.SetID(item.ID);
		component.EnterPreviewMode();
		if (!UnlockManager.IsNookItemUnlocked(item))
		{
			component.SetRimMaterial(NookItem.VisibilityType.Invalid);
		}
	}

	// Token: 0x060014A7 RID: 5287 RVA: 0x000810CC File Offset: 0x0007F2CC
	public void Select(NookDB.NookObject item)
	{
		if (this.hoveredItem == null || item != this.hoveredItem.ObjRef)
		{
			return;
		}
		if (!UnlockManager.IsNookItemUnlocked(item) || this.justPurchased)
		{
			return;
		}
		this.EnterPlacementMode();
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x00081104 File Offset: 0x0007F304
	public void EnterPlacementMode()
	{
		if (this.preparedItem == null || PlayerNook.MyNook.ItemCount >= NookDB.DB.NookLimit)
		{
			return;
		}
		PlayerNook.MyNook.CameFromUI = true;
		this.preparedItem.GetComponent<NookItem>().EnterPlacementMode(true);
		this.preparedItem = null;
		NookPanel.Toggle();
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x0008115E File Offset: 0x0007F35E
	private void CancelPreview()
	{
		if (this.preparedItem != null)
		{
			UnityEngine.Object.Destroy(this.preparedItem);
		}
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x0008117C File Offset: 0x0007F37C
	private void UpdateCurrencyInfo()
	{
		this.CurrentGildings.text = Currency.Gildings.ToString();
		base.StartCoroutine("UpdateLayoutDelayed");
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x000811AD File Offset: 0x0007F3AD
	private IEnumerator UpdateLayoutDelayed()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.CurrentGildings.transform.parent.GetComponent<RectTransform>());
		yield break;
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000811BC File Offset: 0x0007F3BC
	public NookPanel()
	{
	}

	// Token: 0x040013D2 RID: 5074
	public static NookPanel instance;

	// Token: 0x040013D3 RID: 5075
	public TextMeshProUGUI CurrentGildings;

	// Token: 0x040013D4 RID: 5076
	[Header("Tabs")]
	public List<AugmentsTabElement> Tabs;

	// Token: 0x040013D5 RID: 5077
	[HideInInspector]
	public AugmentsPanel.BookTab CurTab = AugmentsPanel.BookTab.Nook_Furniture;

	// Token: 0x040013D6 RID: 5078
	[Header("Grid List")]
	public AutoScrollRect Scroller;

	// Token: 0x040013D7 RID: 5079
	public GridLayoutGroup GridLayout;

	// Token: 0x040013D8 RID: 5080
	public GameObject NookElementRef;

	// Token: 0x040013D9 RID: 5081
	private List<NookPanelItem> GridItems = new List<NookPanelItem>();

	// Token: 0x040013DA RID: 5082
	public TextMeshProUGUI ItemName;

	// Token: 0x040013DB RID: 5083
	public TextMeshProUGUI UnlockedText;

	// Token: 0x040013DC RID: 5084
	public GameObject AchievementDisplay;

	// Token: 0x040013DD RID: 5085
	public TextMeshProUGUI AchievementText;

	// Token: 0x040013DE RID: 5086
	public GameObject DropDisplay;

	// Token: 0x040013DF RID: 5087
	public TextMeshProUGUI DropText;

	// Token: 0x040013E0 RID: 5088
	public GameObject RaidDisplay;

	// Token: 0x040013E1 RID: 5089
	public TextMeshProUGUI RaidText;

	// Token: 0x040013E2 RID: 5090
	public GameObject CostDisplay;

	// Token: 0x040013E3 RID: 5091
	public TextMeshProUGUI CostText;

	// Token: 0x040013E4 RID: 5092
	public Color CostBaseColor;

	// Token: 0x040013E5 RID: 5093
	public Color CostHighColor;

	// Token: 0x040013E6 RID: 5094
	public GameObject PlacePrompt;

	// Token: 0x040013E7 RID: 5095
	public GameObject LimitPrompt;

	// Token: 0x040013E8 RID: 5096
	public GameObject Purchase_Controller;

	// Token: 0x040013E9 RID: 5097
	public GameObject Purchase_KBM;

	// Token: 0x040013EA RID: 5098
	public List<Image> PurchaseFills;

	// Token: 0x040013EB RID: 5099
	private float HoldTime = 0.75f;

	// Token: 0x040013EC RID: 5100
	private float holdT;

	// Token: 0x040013ED RID: 5101
	private bool justPurchased;

	// Token: 0x040013EE RID: 5102
	public AudioClip PurchaseSFX;

	// Token: 0x040013EF RID: 5103
	private GameObject preparedItem;

	// Token: 0x040013F0 RID: 5104
	private NookPanelItem hoveredItem;

	// Token: 0x020005B6 RID: 1462
	[CompilerGenerated]
	private sealed class <GoBack>d__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025E0 RID: 9696 RVA: 0x000D26AC File Offset: 0x000D08AC
		[DebuggerHidden]
		public <GoBack>d__34(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000D26BB File Offset: 0x000D08BB
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000D26C0 File Offset: 0x000D08C0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			NookPanel.Toggle();
			return false;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000D2705 File Offset: 0x000D0905
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000D270D File Offset: 0x000D090D
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x000D2714 File Offset: 0x000D0914
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400284D RID: 10317
		private int <>1__state;

		// Token: 0x0400284E RID: 10318
		private object <>2__current;
	}

	// Token: 0x020005B7 RID: 1463
	[CompilerGenerated]
	private sealed class <UpdateLayoutDelayed>d__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060025E6 RID: 9702 RVA: 0x000D271C File Offset: 0x000D091C
		[DebuggerHidden]
		public <UpdateLayoutDelayed>d__51(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000D272B File Offset: 0x000D092B
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000D2730 File Offset: 0x000D0930
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			NookPanel nookPanel = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LayoutRebuilder.ForceRebuildLayoutImmediate(nookPanel.CurrentGildings.transform.parent.GetComponent<RectTransform>());
			return false;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000D2792 File Offset: 0x000D0992
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000D279A File Offset: 0x000D099A
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x000D27A1 File Offset: 0x000D09A1
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400284F RID: 10319
		private int <>1__state;

		// Token: 0x04002850 RID: 10320
		private object <>2__current;

		// Token: 0x04002851 RID: 10321
		public NookPanel <>4__this;
	}
}
