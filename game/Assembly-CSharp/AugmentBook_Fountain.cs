using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F3 RID: 499
public class AugmentBook_Fountain : MonoBehaviour
{
	// Token: 0x0600153E RID: 5438 RVA: 0x000853F0 File Offset: 0x000835F0
	public void Setup()
	{
		GenreTree gameGraph = GameplayManager.instance.GameGraph;
		bool flag = gameGraph != null && gameGraph.Root.HasTomePower && gameGraph.Root.TomePowerAugment != null;
		this.TomePowerBox.SetActive(flag);
		if (flag)
		{
			AugmentRootNode root = gameGraph.Root.TomePowerAugment.Root;
			this.TomePowerIcon.sprite = root.Icon;
			this.TomePowerTitle.text = root.Name;
			this.tomeDetailSrc = root.Detail;
			this.TomePowerDetail.text = TextParser.AugmentDetail(this.tomeDetailSrc, null, null, false);
		}
		this.ClearPurchased();
		this.NoPurchased.SetActive(InkManager.PurchasedMods.trees.Count == 0);
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in InkManager.PurchasedMods.trees)
		{
			AugmentBookBarItem component = UnityEngine.Object.Instantiate<GameObject>(this.BarRef, this.BarHolder).GetComponent<AugmentBookBarItem>();
			component.Setup(keyValuePair.Key, null);
			this.Purchased.Add(component);
		}
		UISelector.SetupVerticalListNav<AugmentBookBarItem>(this.Purchased, this.TomeButton, null, true);
		base.StartCoroutine("Rebuild");
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x00085554 File Offset: 0x00083754
	public void SelectDefaultUI()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.TomeButton.gameObject.activeInHierarchy)
		{
			UISelector.SelectSelectable(this.TomeButton);
			return;
		}
		if (this.Purchased.Count > 0)
		{
			UISelector.SelectSelectable(this.Purchased[0].GetComponent<Button>());
			return;
		}
		UISelector.ResetSelected();
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x000855B1 File Offset: 0x000837B1
	public void TickUpdate()
	{
		if (InputManager.IsUsingController && AugmentsPanel.instance.IsOnBookSelection)
		{
			this.Scroller.TickUpdate();
		}
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x000855D1 File Offset: 0x000837D1
	private IEnumerable Rebuild()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.TomePowerBox.GetComponent<RectTransform>());
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.PurchaseBox);
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(base.GetComponent<RectTransform>());
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.TomePowerBox.GetComponent<RectTransform>());
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.PurchaseBox);
		yield break;
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x000855E4 File Offset: 0x000837E4
	public void TomePowerSelect()
	{
		foreach (GameDB.Parsable parsable in TextParser.GetKeywords(this.tomeDetailSrc, PlayerControl.myInstance))
		{
			KeywordBoxUI.CreateBox(parsable, this.KeywordList, ref this.keywords, PlayerControl.myInstance);
		}
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x00085650 File Offset: 0x00083850
	public void TomePowerDeselect()
	{
		foreach (KeywordBoxUI keywordBoxUI in this.keywords)
		{
			if (keywordBoxUI != null)
			{
				UnityEngine.Object.Destroy(keywordBoxUI.gameObject);
			}
		}
		this.keywords.Clear();
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x000856BC File Offset: 0x000838BC
	private void ClearPurchased()
	{
		foreach (AugmentBookBarItem augmentBookBarItem in this.Purchased)
		{
			UnityEngine.Object.Destroy(augmentBookBarItem.gameObject);
		}
		this.Purchased.Clear();
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x0008571C File Offset: 0x0008391C
	public AugmentBook_Fountain()
	{
	}

	// Token: 0x040014B4 RID: 5300
	public CanvasGroup Group;

	// Token: 0x040014B5 RID: 5301
	public GameObject TomePowerBox;

	// Token: 0x040014B6 RID: 5302
	public Image TomePowerIcon;

	// Token: 0x040014B7 RID: 5303
	public TextMeshProUGUI TomePowerTitle;

	// Token: 0x040014B8 RID: 5304
	public TextMeshProUGUI TomePowerDetail;

	// Token: 0x040014B9 RID: 5305
	public Button TomeButton;

	// Token: 0x040014BA RID: 5306
	private string tomeDetailSrc;

	// Token: 0x040014BB RID: 5307
	public RectTransform KeywordList;

	// Token: 0x040014BC RID: 5308
	private List<KeywordBoxUI> keywords = new List<KeywordBoxUI>();

	// Token: 0x040014BD RID: 5309
	public GameObject BarRef;

	// Token: 0x040014BE RID: 5310
	public GameObject NoPurchased;

	// Token: 0x040014BF RID: 5311
	public RectTransform PurchaseBox;

	// Token: 0x040014C0 RID: 5312
	public Transform BarHolder;

	// Token: 0x040014C1 RID: 5313
	public AutoScrollRect Scroller;

	// Token: 0x040014C2 RID: 5314
	private List<AugmentBookBarItem> Purchased = new List<AugmentBookBarItem>();

	// Token: 0x020005C5 RID: 1477
	[CompilerGenerated]
	private sealed class <Rebuild>d__18 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002614 RID: 9748 RVA: 0x000D304F File Offset: 0x000D124F
		[DebuggerHidden]
		public <Rebuild>d__18(int <>1__state)
		{
			this.<>1__state = <>1__state;
			this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000D3069 File Offset: 0x000D1269
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000D306C File Offset: 0x000D126C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AugmentBook_Fountain augmentBook_Fountain = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				LayoutRebuilder.ForceRebuildLayoutImmediate(augmentBook_Fountain.GetComponent<RectTransform>());
				LayoutRebuilder.ForceRebuildLayoutImmediate(augmentBook_Fountain.TomePowerBox.GetComponent<RectTransform>());
				LayoutRebuilder.ForceRebuildLayoutImmediate(augmentBook_Fountain.PurchaseBox);
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LayoutRebuilder.ForceRebuildLayoutImmediate(augmentBook_Fountain.GetComponent<RectTransform>());
			LayoutRebuilder.ForceRebuildLayoutImmediate(augmentBook_Fountain.TomePowerBox.GetComponent<RectTransform>());
			LayoutRebuilder.ForceRebuildLayoutImmediate(augmentBook_Fountain.PurchaseBox);
			return false;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x000D3100 File Offset: 0x000D1300
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000D3108 File Offset: 0x000D1308
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x000D310F File Offset: 0x000D130F
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000D3118 File Offset: 0x000D1318
		[DebuggerHidden]
		IEnumerator<object> IEnumerable<object>.GetEnumerator()
		{
			AugmentBook_Fountain.<Rebuild>d__18 <Rebuild>d__;
			if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
			{
				this.<>1__state = 0;
				<Rebuild>d__ = this;
			}
			else
			{
				<Rebuild>d__ = new AugmentBook_Fountain.<Rebuild>d__18(0);
				<Rebuild>d__.<>4__this = this;
			}
			return <Rebuild>d__;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000D315B File Offset: 0x000D135B
		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
		}

		// Token: 0x0400289E RID: 10398
		private int <>1__state;

		// Token: 0x0400289F RID: 10399
		private object <>2__current;

		// Token: 0x040028A0 RID: 10400
		private int <>l__initialThreadId;

		// Token: 0x040028A1 RID: 10401
		public AugmentBook_Fountain <>4__this;
	}
}
