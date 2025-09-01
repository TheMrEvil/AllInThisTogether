using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000003 RID: 3
	[AddComponentMenu("UI/Button", 30)]
	public class Button : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020E4 File Offset: 0x000002E4
		protected Button()
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020F7 File Offset: 0x000002F7
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020FF File Offset: 0x000002FF
		public Button.ButtonClickedEvent onClick
		{
			get
			{
				return this.m_OnClick;
			}
			set
			{
				this.m_OnClick = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002108 File Offset: 0x00000308
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			UISystemProfilerApi.AddMarker("Button.onClick", this);
			this.m_OnClick.Invoke();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002131 File Offset: 0x00000331
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002142 File Offset: 0x00000342
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.DoStateTransition(Selectable.SelectionState.Pressed, false);
			base.StartCoroutine(this.OnFinishSubmit());
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002170 File Offset: 0x00000370
		private IEnumerator OnFinishSubmit()
		{
			float fadeTime = base.colors.fadeDuration;
			float elapsedTime = 0f;
			while (elapsedTime < fadeTime)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			this.DoStateTransition(base.currentSelectionState, false);
			yield break;
		}

		// Token: 0x0400000B RID: 11
		[FormerlySerializedAs("onClick")]
		[SerializeField]
		private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();

		// Token: 0x02000076 RID: 118
		[Serializable]
		public class ButtonClickedEvent : UnityEvent
		{
			// Token: 0x0600068B RID: 1675 RVA: 0x0001BA86 File Offset: 0x00019C86
			public ButtonClickedEvent()
			{
			}
		}

		// Token: 0x02000077 RID: 119
		[CompilerGenerated]
		private sealed class <OnFinishSubmit>d__9 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600068C RID: 1676 RVA: 0x0001BA8E File Offset: 0x00019C8E
			[DebuggerHidden]
			public <OnFinishSubmit>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x0001BA9D File Offset: 0x00019C9D
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0001BAA0 File Offset: 0x00019CA0
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Button button = this;
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
					fadeTime = button.colors.fadeDuration;
					elapsedTime = 0f;
				}
				if (elapsedTime >= fadeTime)
				{
					button.DoStateTransition(button.currentSelectionState, false);
					return false;
				}
				elapsedTime += Time.unscaledDeltaTime;
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001BB31 File Offset: 0x00019D31
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x0001BB39 File Offset: 0x00019D39
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001BB40 File Offset: 0x00019D40
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400023F RID: 575
			private int <>1__state;

			// Token: 0x04000240 RID: 576
			private object <>2__current;

			// Token: 0x04000241 RID: 577
			public Button <>4__this;

			// Token: 0x04000242 RID: 578
			private float <fadeTime>5__2;

			// Token: 0x04000243 RID: 579
			private float <elapsedTime>5__3;
		}
	}
}
