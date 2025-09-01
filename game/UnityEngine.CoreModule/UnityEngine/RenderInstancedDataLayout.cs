using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200013A RID: 314
	internal readonly struct RenderInstancedDataLayout
	{
		// Token: 0x06000A0D RID: 2573 RVA: 0x0000F160 File Offset: 0x0000D360
		public RenderInstancedDataLayout(Type t)
		{
			this.size = Marshal.SizeOf(t);
			this.offsetObjectToWorld = ((t == typeof(Matrix4x4)) ? 0 : Marshal.OffsetOf(t, "objectToWorld").ToInt32());
			try
			{
				this.offsetPrevObjectToWorld = Marshal.OffsetOf(t, "prevObjectToWorld").ToInt32();
			}
			catch (ArgumentException)
			{
				this.offsetPrevObjectToWorld = -1;
			}
			try
			{
				this.offsetRenderingLayerMask = Marshal.OffsetOf(t, "renderingLayerMask").ToInt32();
			}
			catch (ArgumentException)
			{
				this.offsetRenderingLayerMask = -1;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0000F214 File Offset: 0x0000D414
		public int size
		{
			[CompilerGenerated]
			get
			{
				return this.<size>k__BackingField;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0000F21C File Offset: 0x0000D41C
		public int offsetObjectToWorld
		{
			[CompilerGenerated]
			get
			{
				return this.<offsetObjectToWorld>k__BackingField;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x0000F224 File Offset: 0x0000D424
		public int offsetPrevObjectToWorld
		{
			[CompilerGenerated]
			get
			{
				return this.<offsetPrevObjectToWorld>k__BackingField;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0000F22C File Offset: 0x0000D42C
		public int offsetRenderingLayerMask
		{
			[CompilerGenerated]
			get
			{
				return this.<offsetRenderingLayerMask>k__BackingField;
			}
		}

		// Token: 0x040003FD RID: 1021
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <size>k__BackingField;

		// Token: 0x040003FE RID: 1022
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <offsetObjectToWorld>k__BackingField;

		// Token: 0x040003FF RID: 1023
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <offsetPrevObjectToWorld>k__BackingField;

		// Token: 0x04000400 RID: 1024
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <offsetRenderingLayerMask>k__BackingField;
	}
}
