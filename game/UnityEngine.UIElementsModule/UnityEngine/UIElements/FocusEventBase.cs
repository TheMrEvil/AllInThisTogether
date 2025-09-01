using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E2 RID: 482
	public abstract class FocusEventBase<T> : EventBase<T>, IFocusEvent where T : FocusEventBase<T>, new()
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003F394 File Offset: 0x0003D594
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x0003F39C File Offset: 0x0003D59C
		public Focusable relatedTarget
		{
			[CompilerGenerated]
			get
			{
				return this.<relatedTarget>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<relatedTarget>k__BackingField = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0003F3A5 File Offset: 0x0003D5A5
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0003F3AD File Offset: 0x0003D5AD
		public FocusChangeDirection direction
		{
			[CompilerGenerated]
			get
			{
				return this.<direction>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<direction>k__BackingField = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003F3B6 File Offset: 0x0003D5B6
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0003F3BE File Offset: 0x0003D5BE
		private protected FocusController focusController
		{
			[CompilerGenerated]
			protected get
			{
				return this.<focusController>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<focusController>k__BackingField = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0003F3C7 File Offset: 0x0003D5C7
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0003F3CF File Offset: 0x0003D5CF
		internal bool IsFocusDelegated
		{
			[CompilerGenerated]
			get
			{
				return this.<IsFocusDelegated>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsFocusDelegated>k__BackingField = value;
			}
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003F3D8 File Offset: 0x0003D5D8
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003F3E9 File Offset: 0x0003D5E9
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
			this.relatedTarget = null;
			this.direction = FocusChangeDirection.unspecified;
			this.focusController = null;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003F410 File Offset: 0x0003D610
		public static T GetPooled(IEventHandler target, Focusable relatedTarget, FocusChangeDirection direction, FocusController focusController, bool bIsFocusDelegated = false)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.target = target;
			pooled.relatedTarget = relatedTarget;
			pooled.direction = direction;
			pooled.focusController = focusController;
			pooled.IsFocusDelegated = bIsFocusDelegated;
			return pooled;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003F46B File Offset: 0x0003D66B
		protected FocusEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x04000709 RID: 1801
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Focusable <relatedTarget>k__BackingField;

		// Token: 0x0400070A RID: 1802
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private FocusChangeDirection <direction>k__BackingField;

		// Token: 0x0400070B RID: 1803
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private FocusController <focusController>k__BackingField;

		// Token: 0x0400070C RID: 1804
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <IsFocusDelegated>k__BackingField;
	}
}
