using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.Profiling.Experimental
{
	// Token: 0x0200027D RID: 637
	public struct DebugScreenCapture
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x0002CA3B File Offset: 0x0002AC3B
		// (set) Token: 0x06001BCA RID: 7114 RVA: 0x0002CA43 File Offset: 0x0002AC43
		public NativeArray<byte> rawImageDataReference
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<rawImageDataReference>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rawImageDataReference>k__BackingField = value;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x0002CA4C File Offset: 0x0002AC4C
		// (set) Token: 0x06001BCC RID: 7116 RVA: 0x0002CA54 File Offset: 0x0002AC54
		public TextureFormat imageFormat
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<imageFormat>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<imageFormat>k__BackingField = value;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001BCD RID: 7117 RVA: 0x0002CA5D File Offset: 0x0002AC5D
		// (set) Token: 0x06001BCE RID: 7118 RVA: 0x0002CA65 File Offset: 0x0002AC65
		public int width
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<width>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<width>k__BackingField = value;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x0002CA6E File Offset: 0x0002AC6E
		// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x0002CA76 File Offset: 0x0002AC76
		public int height
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<height>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<height>k__BackingField = value;
			}
		}

		// Token: 0x04000914 RID: 2324
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private NativeArray<byte> <rawImageDataReference>k__BackingField;

		// Token: 0x04000915 RID: 2325
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TextureFormat <imageFormat>k__BackingField;

		// Token: 0x04000916 RID: 2326
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <width>k__BackingField;

		// Token: 0x04000917 RID: 2327
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <height>k__BackingField;
	}
}
