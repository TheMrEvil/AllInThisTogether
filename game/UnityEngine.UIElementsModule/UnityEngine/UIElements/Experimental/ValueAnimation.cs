using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200038C RID: 908
	public sealed class ValueAnimation<T> : IValueAnimationUpdate, IValueAnimation
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x0008A458 File Offset: 0x00088658
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x0008A470 File Offset: 0x00088670
		public int durationMs
		{
			get
			{
				return this.m_DurationMs;
			}
			set
			{
				bool flag = value < 1;
				if (flag)
				{
					value = 1;
				}
				this.m_DurationMs = value;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0008A492 File Offset: 0x00088692
		// (set) Token: 0x06001D4F RID: 7503 RVA: 0x0008A49A File Offset: 0x0008869A
		public Func<float, float> easingCurve
		{
			[CompilerGenerated]
			get
			{
				return this.<easingCurve>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<easingCurve>k__BackingField = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x0008A4A3 File Offset: 0x000886A3
		// (set) Token: 0x06001D51 RID: 7505 RVA: 0x0008A4AB File Offset: 0x000886AB
		public bool isRunning
		{
			[CompilerGenerated]
			get
			{
				return this.<isRunning>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isRunning>k__BackingField = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0008A4B4 File Offset: 0x000886B4
		// (set) Token: 0x06001D53 RID: 7507 RVA: 0x0008A4BC File Offset: 0x000886BC
		public Action onAnimationCompleted
		{
			[CompilerGenerated]
			get
			{
				return this.<onAnimationCompleted>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<onAnimationCompleted>k__BackingField = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x0008A4C5 File Offset: 0x000886C5
		// (set) Token: 0x06001D55 RID: 7509 RVA: 0x0008A4CD File Offset: 0x000886CD
		public bool autoRecycle
		{
			[CompilerGenerated]
			get
			{
				return this.<autoRecycle>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<autoRecycle>k__BackingField = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0008A4D6 File Offset: 0x000886D6
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0008A4DE File Offset: 0x000886DE
		private bool recycled
		{
			[CompilerGenerated]
			get
			{
				return this.<recycled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<recycled>k__BackingField = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0008A4E7 File Offset: 0x000886E7
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0008A4EF File Offset: 0x000886EF
		private VisualElement owner
		{
			[CompilerGenerated]
			get
			{
				return this.<owner>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<owner>k__BackingField = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0008A4F8 File Offset: 0x000886F8
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0008A500 File Offset: 0x00088700
		public Action<VisualElement, T> valueUpdated
		{
			[CompilerGenerated]
			get
			{
				return this.<valueUpdated>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<valueUpdated>k__BackingField = value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0008A509 File Offset: 0x00088709
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0008A511 File Offset: 0x00088711
		public Func<VisualElement, T> initialValue
		{
			[CompilerGenerated]
			get
			{
				return this.<initialValue>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<initialValue>k__BackingField = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0008A51A File Offset: 0x0008871A
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0008A522 File Offset: 0x00088722
		public Func<T, T, float, T> interpolator
		{
			[CompilerGenerated]
			get
			{
				return this.<interpolator>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<interpolator>k__BackingField = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0008A52C File Offset: 0x0008872C
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x0008A57A File Offset: 0x0008877A
		public T from
		{
			get
			{
				bool flag = !this.fromValueSet;
				if (flag)
				{
					bool flag2 = this.initialValue != null;
					if (flag2)
					{
						this.from = this.initialValue(this.owner);
					}
				}
				return this._from;
			}
			set
			{
				this.fromValueSet = true;
				this._from = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0008A58B File Offset: 0x0008878B
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0008A593 File Offset: 0x00088793
		public T to
		{
			[CompilerGenerated]
			get
			{
				return this.<to>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<to>k__BackingField = value;
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0008A59C File Offset: 0x0008879C
		public ValueAnimation()
		{
			this.SetDefaultValues();
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0008A5B4 File Offset: 0x000887B4
		public void Start()
		{
			this.CheckNotRecycled();
			bool flag = this.owner != null;
			if (flag)
			{
				this.m_StartTimeMs = Panel.TimeSinceStartupMs();
				this.Register();
				this.isRunning = true;
			}
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0008A5F4 File Offset: 0x000887F4
		public void Stop()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
				this.isRunning = false;
				Action onAnimationCompleted = this.onAnimationCompleted;
				if (onAnimationCompleted != null)
				{
					onAnimationCompleted();
				}
				bool autoRecycle = this.autoRecycle;
				if (autoRecycle)
				{
					bool flag = !this.recycled;
					if (flag)
					{
						this.Recycle();
					}
				}
			}
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0008A658 File Offset: 0x00088858
		public void Recycle()
		{
			this.CheckNotRecycled();
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				bool flag = !this.autoRecycle;
				if (!flag)
				{
					this.Stop();
					return;
				}
				this.Stop();
			}
			this.SetDefaultValues();
			this.recycled = true;
			ValueAnimation<T>.sObjectPool.Release(this);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0008A6B8 File Offset: 0x000888B8
		void IValueAnimationUpdate.Tick(long currentTimeMs)
		{
			this.CheckNotRecycled();
			long num = currentTimeMs - this.m_StartTimeMs;
			float num2 = (float)num / (float)this.durationMs;
			bool flag = false;
			bool flag2 = num2 >= 1f;
			if (flag2)
			{
				num2 = 1f;
				flag = true;
			}
			Func<float, float> easingCurve = this.easingCurve;
			num2 = ((easingCurve != null) ? easingCurve(num2) : num2);
			bool flag3 = this.interpolator != null;
			if (flag3)
			{
				T arg = this.interpolator(this.from, this.to, num2);
				Action<VisualElement, T> valueUpdated = this.valueUpdated;
				if (valueUpdated != null)
				{
					valueUpdated(this.owner, arg);
				}
			}
			bool flag4 = flag;
			if (flag4)
			{
				this.Stop();
			}
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0008A768 File Offset: 0x00088968
		private void SetDefaultValues()
		{
			this.m_DurationMs = 400;
			this.autoRecycle = true;
			this.owner = null;
			this.m_StartTimeMs = 0L;
			this.onAnimationCompleted = null;
			this.valueUpdated = null;
			this.initialValue = null;
			this.interpolator = null;
			this.to = default(T);
			this.from = default(T);
			this.fromValueSet = false;
			this.easingCurve = new Func<float, float>(Easing.OutQuad);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0008A7F4 File Offset: 0x000889F4
		private void Unregister()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.UnregisterAnimation(this);
			}
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0008A820 File Offset: 0x00088A20
		private void Register()
		{
			bool flag = this.owner != null;
			if (flag)
			{
				this.owner.RegisterAnimation(this);
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0008A84C File Offset: 0x00088A4C
		internal void SetOwner(VisualElement e)
		{
			bool isRunning = this.isRunning;
			if (isRunning)
			{
				this.Unregister();
			}
			this.owner = e;
			bool isRunning2 = this.isRunning;
			if (isRunning2)
			{
				this.Register();
			}
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0008A888 File Offset: 0x00088A88
		private void CheckNotRecycled()
		{
			bool recycled = this.recycled;
			if (recycled)
			{
				throw new InvalidOperationException("Animation object has been recycled. Use KeepAlive() to keep a reference to an animation after it has been stopped.");
			}
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0008A8AC File Offset: 0x00088AAC
		public static ValueAnimation<T> Create(VisualElement e, Func<T, T, float, T> interpolator)
		{
			ValueAnimation<T> valueAnimation = ValueAnimation<T>.sObjectPool.Get();
			valueAnimation.recycled = false;
			valueAnimation.SetOwner(e);
			valueAnimation.interpolator = interpolator;
			return valueAnimation;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0008A8E4 File Offset: 0x00088AE4
		public ValueAnimation<T> Ease(Func<float, float> easing)
		{
			this.easingCurve = easing;
			return this;
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0008A900 File Offset: 0x00088B00
		public ValueAnimation<T> OnCompleted(Action callback)
		{
			this.onAnimationCompleted = callback;
			return this;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0008A91C File Offset: 0x00088B1C
		public ValueAnimation<T> KeepAlive()
		{
			this.autoRecycle = false;
			return this;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0008A937 File Offset: 0x00088B37
		// Note: this type is marked as 'beforefieldinit'.
		static ValueAnimation()
		{
		}

		// Token: 0x04000EA2 RID: 3746
		private const int k_DefaultDurationMs = 400;

		// Token: 0x04000EA3 RID: 3747
		private const int k_DefaultMaxPoolSize = 100;

		// Token: 0x04000EA4 RID: 3748
		private long m_StartTimeMs;

		// Token: 0x04000EA5 RID: 3749
		private int m_DurationMs;

		// Token: 0x04000EA6 RID: 3750
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<float, float> <easingCurve>k__BackingField;

		// Token: 0x04000EA7 RID: 3751
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isRunning>k__BackingField;

		// Token: 0x04000EA8 RID: 3752
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action <onAnimationCompleted>k__BackingField;

		// Token: 0x04000EA9 RID: 3753
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <autoRecycle>k__BackingField;

		// Token: 0x04000EAA RID: 3754
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <recycled>k__BackingField;

		// Token: 0x04000EAB RID: 3755
		private static ObjectPool<ValueAnimation<T>> sObjectPool = new ObjectPool<ValueAnimation<T>>(100);

		// Token: 0x04000EAC RID: 3756
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualElement <owner>k__BackingField;

		// Token: 0x04000EAD RID: 3757
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<VisualElement, T> <valueUpdated>k__BackingField;

		// Token: 0x04000EAE RID: 3758
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Func<VisualElement, T> <initialValue>k__BackingField;

		// Token: 0x04000EAF RID: 3759
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Func<T, T, float, T> <interpolator>k__BackingField;

		// Token: 0x04000EB0 RID: 3760
		private T _from;

		// Token: 0x04000EB1 RID: 3761
		private bool fromValueSet = false;

		// Token: 0x04000EB2 RID: 3762
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private T <to>k__BackingField;
	}
}
