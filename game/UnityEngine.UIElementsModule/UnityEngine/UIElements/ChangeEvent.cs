using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C7 RID: 455
	public class ChangeEvent<T> : EventBase<ChangeEvent<T>>, IChangeEvent
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0003D0ED File Offset: 0x0003B2ED
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0003D0F5 File Offset: 0x0003B2F5
		public T previousValue
		{
			[CompilerGenerated]
			get
			{
				return this.<previousValue>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<previousValue>k__BackingField = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0003D0FE File Offset: 0x0003B2FE
		// (set) Token: 0x06000E8F RID: 3727 RVA: 0x0003D106 File Offset: 0x0003B306
		public T newValue
		{
			[CompilerGenerated]
			get
			{
				return this.<newValue>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<newValue>k__BackingField = value;
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003D10F File Offset: 0x0003B30F
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003D120 File Offset: 0x0003B320
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
			this.previousValue = default(T);
			this.newValue = default(T);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003D158 File Offset: 0x0003B358
		public static ChangeEvent<T> GetPooled(T previousValue, T newValue)
		{
			ChangeEvent<T> pooled = EventBase<ChangeEvent<T>>.GetPooled();
			pooled.previousValue = previousValue;
			pooled.newValue = newValue;
			return pooled;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003D181 File Offset: 0x0003B381
		public ChangeEvent()
		{
			this.LocalInit();
		}

		// Token: 0x040006C1 RID: 1729
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private T <previousValue>k__BackingField;

		// Token: 0x040006C2 RID: 1730
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private T <newValue>k__BackingField;
	}
}
