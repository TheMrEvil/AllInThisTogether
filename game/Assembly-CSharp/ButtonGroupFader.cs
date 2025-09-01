using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000207 RID: 519
public class ButtonGroupFader : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x06001617 RID: 5655 RVA: 0x0008BDAE File Offset: 0x00089FAE
	private void Awake()
	{
		this.buttonRef = base.GetComponent<Button>();
		this.UpdateRoutine();
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x0008BDC4 File Offset: 0x00089FC4
	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		if (!this.buttonRef.interactable || this.curState == ButtonGroupFader.InteractState.Hover || this.groupRef == null)
		{
			return;
		}
		if (!this.groupRef.gameObject.activeSelf)
		{
			this.groupRef.gameObject.SetActive(true);
		}
		this.curState = ButtonGroupFader.InteractState.Hover;
		this.UpdateRoutine();
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x0008BE26 File Offset: 0x0008A026
	public void OnPointerExit(PointerEventData pointerEventData)
	{
		if (this.curState == ButtonGroupFader.InteractState.Default)
		{
			return;
		}
		this.curState = ButtonGroupFader.InteractState.Default;
		this.UpdateRoutine();
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x0008BE3E File Offset: 0x0008A03E
	public void OnSelect(BaseEventData ev)
	{
		if (!this.buttonRef.interactable || this.curState == ButtonGroupFader.InteractState.Hover)
		{
			return;
		}
		this.curState = ButtonGroupFader.InteractState.Hover;
		this.UpdateRoutine();
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x0008BE64 File Offset: 0x0008A064
	public void OnDeselect(BaseEventData ev)
	{
		if (this.curState == ButtonGroupFader.InteractState.Default)
		{
			return;
		}
		this.curState = ButtonGroupFader.InteractState.Default;
		this.UpdateRoutine();
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x0008BE7C File Offset: 0x0008A07C
	public void OnPointerDown(PointerEventData eventData)
	{
		if (!this.buttonRef.interactable)
		{
			return;
		}
		this.curState = ButtonGroupFader.InteractState.Down;
		this.UpdateRoutine();
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x0008BE99 File Offset: 0x0008A099
	public void OnPointerUp(PointerEventData eventData)
	{
		if (!this.buttonRef.interactable || this.curState == ButtonGroupFader.InteractState.Default)
		{
			return;
		}
		this.curState = ButtonGroupFader.InteractState.Hover;
		this.UpdateRoutine();
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x0008BEBE File Offset: 0x0008A0BE
	private void UpdateRoutine()
	{
		base.StopAllCoroutines();
		base.StartCoroutine("FadeRoutine");
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x0008BED2 File Offset: 0x0008A0D2
	private IEnumerator FadeRoutine()
	{
		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			this.UpdateOpacity();
			yield return true;
		}
		CanvasGroup canvasGroup = this.groupRef;
		ButtonGroupFader.InteractState interactState = this.curState;
		float alpha;
		switch (interactState)
		{
		case ButtonGroupFader.InteractState.Default:
			alpha = 0f;
			break;
		case ButtonGroupFader.InteractState.Hover:
			alpha = 1f;
			break;
		case ButtonGroupFader.InteractState.Down:
			alpha = 0.8f;
			break;
		default:
			throw new SwitchExpressionException(interactState);
		}
		canvasGroup.alpha = alpha;
		using (List<CanvasGroup>.Enumerator enumerator = this.extraGroups.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CanvasGroup canvasGroup2 = enumerator.Current;
				canvasGroup2.alpha = this.groupRef.alpha;
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x0008BEE4 File Offset: 0x0008A0E4
	private void UpdateOpacity()
	{
		float b = 0f;
		if (this.curState == ButtonGroupFader.InteractState.Hover)
		{
			b = 1f;
		}
		if (this.curState == ButtonGroupFader.InteractState.Down)
		{
			b = 0.8f;
		}
		if (this.buttonRef != null && !this.buttonRef.interactable)
		{
			b = 0f;
		}
		this.groupRef.alpha = Mathf.Lerp(this.groupRef.alpha, b, Time.deltaTime * 10f);
		foreach (CanvasGroup canvasGroup in this.extraGroups)
		{
			canvasGroup.alpha = this.groupRef.alpha;
		}
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x0008BFAC File Offset: 0x0008A1AC
	public void HideImmediate()
	{
		base.StopAllCoroutines();
		this.groupRef.alpha = 0f;
		foreach (CanvasGroup canvasGroup in this.extraGroups)
		{
			canvasGroup.alpha = 0f;
		}
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x0008C018 File Offset: 0x0008A218
	public ButtonGroupFader()
	{
	}

	// Token: 0x040015C3 RID: 5571
	private Button buttonRef;

	// Token: 0x040015C4 RID: 5572
	public CanvasGroup groupRef;

	// Token: 0x040015C5 RID: 5573
	public List<CanvasGroup> extraGroups;

	// Token: 0x040015C6 RID: 5574
	private ButtonGroupFader.InteractState curState;

	// Token: 0x020005F1 RID: 1521
	public enum InteractState
	{
		// Token: 0x04002944 RID: 10564
		Default,
		// Token: 0x04002945 RID: 10565
		Hover,
		// Token: 0x04002946 RID: 10566
		Down
	}

	// Token: 0x020005F2 RID: 1522
	[CompilerGenerated]
	private sealed class <FadeRoutine>d__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002697 RID: 9879 RVA: 0x000D3BE4 File Offset: 0x000D1DE4
		[DebuggerHidden]
		public <FadeRoutine>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000D3BF3 File Offset: 0x000D1DF3
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000D3BF8 File Offset: 0x000D1DF8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ButtonGroupFader buttonGroupFader = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				t = 1f;
			}
			if (t <= 0f)
			{
				CanvasGroup groupRef = buttonGroupFader.groupRef;
				ButtonGroupFader.InteractState curState = buttonGroupFader.curState;
				float alpha;
				switch (curState)
				{
				case ButtonGroupFader.InteractState.Default:
					alpha = 0f;
					break;
				case ButtonGroupFader.InteractState.Hover:
					alpha = 1f;
					break;
				case ButtonGroupFader.InteractState.Down:
					alpha = 0.8f;
					break;
				default:
					throw new SwitchExpressionException(curState);
				}
				groupRef.alpha = alpha;
				foreach (CanvasGroup canvasGroup in buttonGroupFader.extraGroups)
				{
					canvasGroup.alpha = buttonGroupFader.groupRef.alpha;
				}
				return false;
			}
			t -= Time.deltaTime;
			buttonGroupFader.UpdateOpacity();
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x000D3D14 File Offset: 0x000D1F14
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000D3D1C File Offset: 0x000D1F1C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000D3D23 File Offset: 0x000D1F23
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002947 RID: 10567
		private int <>1__state;

		// Token: 0x04002948 RID: 10568
		private object <>2__current;

		// Token: 0x04002949 RID: 10569
		public ButtonGroupFader <>4__this;

		// Token: 0x0400294A RID: 10570
		private float <t>5__2;
	}
}
