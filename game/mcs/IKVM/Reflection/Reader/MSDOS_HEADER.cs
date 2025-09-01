using System;

namespace IKVM.Reflection.Reader
{
	// Token: 0x0200009D RID: 157
	internal sealed class MSDOS_HEADER
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x00002CCC File Offset: 0x00000ECC
		public MSDOS_HEADER()
		{
		}

		// Token: 0x0400033F RID: 831
		internal const ushort MAGIC_MZ = 23117;

		// Token: 0x04000340 RID: 832
		internal ushort signature;

		// Token: 0x04000341 RID: 833
		internal uint peSignatureOffset;
	}
}
