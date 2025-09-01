using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeTai.TrueShadow
{
	// Token: 0x02000006 RID: 6
	public class AnimatedBiStateButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000020 RID: 32 RVA: 0x000025EC File Offset: 0x000007EC
		// (remove) Token: 0x06000021 RID: 33 RVA: 0x00002624 File Offset: 0x00000824
		public event Action willPress
		{
			[CompilerGenerated]
			add
			{
				Action action = this.willPress;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.willPress, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.willPress;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.willPress, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000022 RID: 34 RVA: 0x0000265C File Offset: 0x0000085C
		// (remove) Token: 0x06000023 RID: 35 RVA: 0x00002694 File Offset: 0x00000894
		public event Action willRelease
		{
			[CompilerGenerated]
			add
			{
				Action action = this.willRelease;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.willRelease, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.willRelease;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.willRelease, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000026C9 File Offset: 0x000008C9
		protected bool IsAnimating
		{
			get
			{
				return this.state == AnimatedBiStateButton.State.AnimateDown || this.state == AnimatedBiStateButton.State.AnimateUp;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026DF File Offset: 0x000008DF
		private void Update()
		{
			this.PollPointerUp();
			this.DoAnimation();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026F0 File Offset: 0x000008F0
		private void DoAnimation()
		{
			if (!this.IsAnimating)
			{
				return;
			}
			if (this.state == AnimatedBiStateButton.State.AnimateDown)
			{
				this.pressAmount += Time.deltaTime / this.animationDuration;
			}
			else if (this.state == AnimatedBiStateButton.State.AnimateUp)
			{
				this.pressAmount -= Time.deltaTime / this.animationDuration;
			}
			this.pressAmount = Mathf.Clamp01(this.pressAmount);
			float num = this.pressAmount;
			if (this.state == AnimatedBiStateButton.State.AnimateUp)
			{
				num = 1f - num;
			}
			num = this.animationCurve.Evaluate(num);
			if (this.state == AnimatedBiStateButton.State.AnimateUp)
			{
				num = 1f - num;
			}
			this.Animate(num);
			if (this.state == AnimatedBiStateButton.State.AnimateDown && this.pressAmount == 1f)
			{
				this.state = AnimatedBiStateButton.State.Down;
			}
			if (this.state == AnimatedBiStateButton.State.AnimateUp && this.pressAmount == 0f)
			{
				this.state = AnimatedBiStateButton.State.Up;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027D4 File Offset: 0x000009D4
		protected void Press()
		{
			if (this.state != AnimatedBiStateButton.State.Down && this.state != AnimatedBiStateButton.State.AnimateDown)
			{
				this.OnWillPress();
				this.state = AnimatedBiStateButton.State.AnimateDown;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027F5 File Offset: 0x000009F5
		protected void Release()
		{
			if (this.state != AnimatedBiStateButton.State.Up && this.state != AnimatedBiStateButton.State.AnimateUp)
			{
				this.OnWillRelease();
				this.state = AnimatedBiStateButton.State.AnimateUp;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002815 File Offset: 0x00000A15
		private void PollPointerUp()
		{
			if (this.useEnterExitEvents && (this.state == AnimatedBiStateButton.State.Down || this.state == AnimatedBiStateButton.State.AnimateDown) && !Input.GetMouseButton(0))
			{
				this.Release();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000283F File Offset: 0x00000A3F
		protected virtual void Animate(float visualPressAmount)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002841 File Offset: 0x00000A41
		public void OnPointerDown(PointerEventData eventData)
		{
			this.Press();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002849 File Offset: 0x00000A49
		public void OnPointerUp(PointerEventData eventData)
		{
			this.Release();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002851 File Offset: 0x00000A51
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.useEnterExitEvents && Input.GetMouseButton(0))
			{
				this.Press();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002869 File Offset: 0x00000A69
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.useEnterExitEvents)
			{
				this.Release();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002879 File Offset: 0x00000A79
		protected virtual void OnWillPress()
		{
			Action action = this.willPress;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000288B File Offset: 0x00000A8B
		protected virtual void OnWillRelease()
		{
			Action action = this.willRelease;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000289D File Offset: 0x00000A9D
		public AnimatedBiStateButton()
		{
		}

		// Token: 0x04000010 RID: 16
		public float animationDuration = 0.1f;

		// Token: 0x04000011 RID: 17
		public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		// Token: 0x04000012 RID: 18
		public bool useEnterExitEvents = true;

		// Token: 0x04000013 RID: 19
		[CompilerGenerated]
		private Action willPress;

		// Token: 0x04000014 RID: 20
		[CompilerGenerated]
		private Action willRelease;

		// Token: 0x04000015 RID: 21
		protected AnimatedBiStateButton.State state;

		// Token: 0x04000016 RID: 22
		protected float pressAmount;

		// Token: 0x0200002D RID: 45
		public enum State
		{
			// Token: 0x040000C8 RID: 200
			Up,
			// Token: 0x040000C9 RID: 201
			AnimateDown,
			// Token: 0x040000CA RID: 202
			Down,
			// Token: 0x040000CB RID: 203
			AnimateUp
		}
	}
}
