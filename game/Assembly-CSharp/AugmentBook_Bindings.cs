using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F2 RID: 498
public class AugmentBook_Bindings : MonoBehaviour
{
	// Token: 0x06001538 RID: 5432 RVA: 0x0008525C File Offset: 0x0008345C
	public void Setup()
	{
		this.BindingLevel.text = GameplayManager.BindingLevel.ToString();
		this.ClearList();
		foreach (KeyValuePair<AugmentRootNode, int> keyValuePair in GameplayManager.instance.GenreBindings.trees)
		{
			AugmentBookBarItem component = UnityEngine.Object.Instantiate<GameObject>(this.BarRef, this.BarHolder).GetComponent<AugmentBookBarItem>();
			component.Setup(keyValuePair.Key, null);
			this.bindList.Add(component);
		}
		UISelector.SetupVerticalListNav<AugmentBookBarItem>(this.bindList, null, null, false);
		base.StartCoroutine("Rebuild");
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x0008531C File Offset: 0x0008351C
	public void SelectDefaultUI()
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		if (this.bindList.Count > 0)
		{
			UISelector.SelectSelectable(this.bindList[0].GetComponent<Button>());
		}
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x0008534A File Offset: 0x0008354A
	public void TickUpdate()
	{
		if (InputManager.IsUsingController && AugmentsPanel.instance.IsOnBookSelection)
		{
			this.Scroller.TickUpdate();
		}
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x0008536C File Offset: 0x0008356C
	private void ClearList()
	{
		foreach (AugmentBookBarItem augmentBookBarItem in this.bindList)
		{
			UnityEngine.Object.Destroy(augmentBookBarItem.gameObject);
		}
		this.bindList.Clear();
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x000853CC File Offset: 0x000835CC
	private IEnumerable Rebuild()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.BindBox);
		yield return true;
		LayoutRebuilder.ForceRebuildLayoutImmediate(this.BindBox);
		yield break;
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x000853DC File Offset: 0x000835DC
	public AugmentBook_Bindings()
	{
	}

	// Token: 0x040014AD RID: 5293
	public CanvasGroup Group;

	// Token: 0x040014AE RID: 5294
	public TextMeshProUGUI BindingLevel;

	// Token: 0x040014AF RID: 5295
	public GameObject BarRef;

	// Token: 0x040014B0 RID: 5296
	public RectTransform BindBox;

	// Token: 0x040014B1 RID: 5297
	public Transform BarHolder;

	// Token: 0x040014B2 RID: 5298
	public AutoScrollRect Scroller;

	// Token: 0x040014B3 RID: 5299
	private List<AugmentBookBarItem> bindList = new List<AugmentBookBarItem>();

	// Token: 0x020005C4 RID: 1476
	[CompilerGenerated]
	private sealed class <Rebuild>d__11 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x000D2F6E File Offset: 0x000D116E
		[DebuggerHidden]
		public <Rebuild>d__11(int <>1__state)
		{
			this.<>1__state = <>1__state;
			this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000D2F88 File Offset: 0x000D1188
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000D2F8C File Offset: 0x000D118C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			AugmentBook_Bindings augmentBook_Bindings = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				LayoutRebuilder.ForceRebuildLayoutImmediate(augmentBook_Bindings.BindBox);
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			LayoutRebuilder.ForceRebuildLayoutImmediate(augmentBook_Bindings.BindBox);
			return false;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000D2FEA File Offset: 0x000D11EA
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000D2FF2 File Offset: 0x000D11F2
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000D2FF9 File Offset: 0x000D11F9
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000D3004 File Offset: 0x000D1204
		[DebuggerHidden]
		IEnumerator<object> IEnumerable<object>.GetEnumerator()
		{
			AugmentBook_Bindings.<Rebuild>d__11 <Rebuild>d__;
			if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
			{
				this.<>1__state = 0;
				<Rebuild>d__ = this;
			}
			else
			{
				<Rebuild>d__ = new AugmentBook_Bindings.<Rebuild>d__11(0);
				<Rebuild>d__.<>4__this = this;
			}
			return <Rebuild>d__;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000D3047 File Offset: 0x000D1247
		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
		}

		// Token: 0x0400289A RID: 10394
		private int <>1__state;

		// Token: 0x0400289B RID: 10395
		private object <>2__current;

		// Token: 0x0400289C RID: 10396
		private int <>l__initialThreadId;

		// Token: 0x0400289D RID: 10397
		public AugmentBook_Bindings <>4__this;
	}
}
