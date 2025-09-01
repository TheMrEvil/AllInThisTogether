using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000163 RID: 355
public class NookPanelItem : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06000F83 RID: 3971 RVA: 0x000623E0 File Offset: 0x000605E0
	public void Setup(NookDB.NookObject obj)
	{
		this.ObjRef = obj;
		this.Icon.sprite = obj.Icon;
		this.UpdateLockDisplay();
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.Click));
	}

	// Token: 0x06000F84 RID: 3972 RVA: 0x0006241C File Offset: 0x0006061C
	public void UpdateLockDisplay()
	{
		this.isUnlocked = UnlockManager.IsNookItemUnlocked(this.ObjRef);
		if (!this.isUnlocked)
		{
			if (this.ObjRef.UnlockedBy == Unlockable.UnlockType.Purchase)
			{
				this.CostDisplay.SetActive(true);
				this.CostText.text = this.ObjRef.Cost.ToString();
				base.StartCoroutine("UpdateLayoutDelayed");
				return;
			}
			if (this.ObjRef.UnlockedBy == Unlockable.UnlockType.Achievement)
			{
				this.AchievementDisplay.SetActive(true);
				return;
			}
			if (this.ObjRef.UnlockedBy == Unlockable.UnlockType.Drop)
			{
				this.DropDisplay.SetActive(true);
				return;
			}
			if (this.ObjRef.UnlockedBy == Unlockable.UnlockType.Raid)
			{
				this.RaidDisplay.SetActive(!this.ObjRef.HardMode);
				this.RaidHardDisplay.SetActive(this.ObjRef.HardMode);
				return;
			}
		}
		else
		{
			this.CostDisplay.SetActive(false);
			this.AchievementDisplay.SetActive(false);
			this.DropDisplay.SetActive(false);
			this.RaidDisplay.SetActive(false);
			this.RaidHardDisplay.SetActive(false);
		}
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x00062537 File Offset: 0x00060737
	public void OnSelect(BaseEventData ev)
	{
		if (this.ObjRef != null)
		{
			NookPanel.instance.Hover(this);
		}
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x0006254C File Offset: 0x0006074C
	public void OnDeselect(BaseEventData ev)
	{
		if (InputManager.IsUsingController)
		{
			return;
		}
		NookPanel.instance.Hover(null);
	}

	// Token: 0x06000F87 RID: 3975 RVA: 0x00062561 File Offset: 0x00060761
	public void Click()
	{
		if (this.ObjRef != null)
		{
			NookPanel.instance.Select(this.ObjRef);
		}
	}

	// Token: 0x06000F88 RID: 3976 RVA: 0x0006257B File Offset: 0x0006077B
	public void PlayPurchaseFX()
	{
		this.PurchaseVFX.gameObject.SetActive(true);
	}

	// Token: 0x06000F89 RID: 3977 RVA: 0x0006258E File Offset: 0x0006078E
	private IEnumerator UpdateLayoutDelayed()
	{
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.CostText.transform.parent.GetComponent<RectTransform>());
		yield break;
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x0006259D File Offset: 0x0006079D
	public NookPanelItem()
	{
	}

	// Token: 0x04000D75 RID: 3445
	public Image Icon;

	// Token: 0x04000D76 RID: 3446
	public GameObject CostDisplay;

	// Token: 0x04000D77 RID: 3447
	public TextMeshProUGUI CostText;

	// Token: 0x04000D78 RID: 3448
	public GameObject AchievementDisplay;

	// Token: 0x04000D79 RID: 3449
	public GameObject DropDisplay;

	// Token: 0x04000D7A RID: 3450
	public GameObject RaidDisplay;

	// Token: 0x04000D7B RID: 3451
	public GameObject RaidHardDisplay;

	// Token: 0x04000D7C RID: 3452
	[NonSerialized]
	public NookDB.NookObject ObjRef;

	// Token: 0x04000D7D RID: 3453
	[NonSerialized]
	public bool isUnlocked;

	// Token: 0x04000D7E RID: 3454
	public ParticleSystem PurchaseVFX;

	// Token: 0x02000556 RID: 1366
	[CompilerGenerated]
	private sealed class <UpdateLayoutDelayed>d__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002481 RID: 9345 RVA: 0x000CE637 File Offset: 0x000CC837
		[DebuggerHidden]
		public <UpdateLayoutDelayed>d__16(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000CE646 File Offset: 0x000CC846
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000CE648 File Offset: 0x000CC848
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			NookPanelItem nookPanelItem = this;
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
			LayoutRebuilder.ForceRebuildLayoutImmediate(nookPanelItem.CostText.transform.parent.GetComponent<RectTransform>());
			return false;
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x000CE6AA File Offset: 0x000CC8AA
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000CE6B2 File Offset: 0x000CC8B2
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x000CE6B9 File Offset: 0x000CC8B9
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040026C6 RID: 9926
		private int <>1__state;

		// Token: 0x040026C7 RID: 9927
		private object <>2__current;

		// Token: 0x040026C8 RID: 9928
		public NookPanelItem <>4__this;
	}
}
