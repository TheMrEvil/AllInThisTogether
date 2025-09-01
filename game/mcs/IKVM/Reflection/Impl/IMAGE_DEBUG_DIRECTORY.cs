using System;

namespace IKVM.Reflection.Impl
{
	// Token: 0x02000078 RID: 120
	internal struct IMAGE_DEBUG_DIRECTORY
	{
		// Token: 0x04000279 RID: 633
		public uint Characteristics;

		// Token: 0x0400027A RID: 634
		public uint TimeDateStamp;

		// Token: 0x0400027B RID: 635
		public ushort MajorVersion;

		// Token: 0x0400027C RID: 636
		public ushort MinorVersion;

		// Token: 0x0400027D RID: 637
		public uint Type;

		// Token: 0x0400027E RID: 638
		public uint SizeOfData;

		// Token: 0x0400027F RID: 639
		public uint AddressOfRawData;

		// Token: 0x04000280 RID: 640
		public uint PointerToRawData;
	}
}
