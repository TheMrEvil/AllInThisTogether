using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EB RID: 491
	public class InputEvent : EventBase<InputEvent>
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003F9B5 File Offset: 0x0003DBB5
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0003F9BD File Offset: 0x0003DBBD
		public string previousData
		{
			[CompilerGenerated]
			get
			{
				return this.<previousData>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<previousData>k__BackingField = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003F9C6 File Offset: 0x0003DBC6
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0003F9CE File Offset: 0x0003DBCE
		public string newData
		{
			[CompilerGenerated]
			get
			{
				return this.<newData>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<newData>k__BackingField = value;
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003F9D7 File Offset: 0x0003DBD7
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown);
			this.previousData = null;
			this.newData = null;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003FA04 File Offset: 0x0003DC04
		public static InputEvent GetPooled(string previousData, string newData)
		{
			InputEvent pooled = EventBase<InputEvent>.GetPooled();
			pooled.previousData = previousData;
			pooled.newData = newData;
			return pooled;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003FA2D File Offset: 0x0003DC2D
		public InputEvent()
		{
			this.LocalInit();
		}

		// Token: 0x04000714 RID: 1812
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <previousData>k__BackingField;

		// Token: 0x04000715 RID: 1813
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <newData>k__BackingField;
	}
}
