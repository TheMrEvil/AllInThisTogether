using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.VFX
{
	// Token: 0x02000018 RID: 24
	public struct VFXOutputEventArgs
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002A2F File Offset: 0x00000C2F
		public readonly int nameId
		{
			[CompilerGenerated]
			get
			{
				return this.<nameId>k__BackingField;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002A37 File Offset: 0x00000C37
		public readonly VFXEventAttribute eventAttribute
		{
			[CompilerGenerated]
			get
			{
				return this.<eventAttribute>k__BackingField;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002A3F File Offset: 0x00000C3F
		public VFXOutputEventArgs(int nameId, VFXEventAttribute eventAttribute)
		{
			this.nameId = nameId;
			this.eventAttribute = eventAttribute;
		}

		// Token: 0x040000FA RID: 250
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <nameId>k__BackingField;

		// Token: 0x040000FB RID: 251
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly VFXEventAttribute <eventAttribute>k__BackingField;
	}
}
