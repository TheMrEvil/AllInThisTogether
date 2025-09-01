using System;

namespace System
{
	// Token: 0x020001B9 RID: 441
	internal struct MutableDecimal
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004D7FB File Offset: 0x0004B9FB
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x0004D80C File Offset: 0x0004BA0C
		public bool IsNegative
		{
			get
			{
				return (this.Flags & 2147483648U) > 0U;
			}
			set
			{
				this.Flags = ((this.Flags & 2147483647U) | (value ? 2147483648U : 0U));
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x0004D82C File Offset: 0x0004BA2C
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x0004D838 File Offset: 0x0004BA38
		public int Scale
		{
			get
			{
				return (int)((byte)(this.Flags >> 16));
			}
			set
			{
				this.Flags = ((this.Flags & 4278255615U) | (uint)((uint)value << 16));
			}
		}

		// Token: 0x0400138E RID: 5006
		public uint Flags;

		// Token: 0x0400138F RID: 5007
		public uint High;

		// Token: 0x04001390 RID: 5008
		public uint Low;

		// Token: 0x04001391 RID: 5009
		public uint Mid;

		// Token: 0x04001392 RID: 5010
		private const uint SignMask = 2147483648U;

		// Token: 0x04001393 RID: 5011
		private const uint ScaleMask = 16711680U;

		// Token: 0x04001394 RID: 5012
		private const int ScaleShift = 16;
	}
}
