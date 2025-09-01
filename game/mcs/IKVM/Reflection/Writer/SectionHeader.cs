using System;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200008A RID: 138
	internal class SectionHeader
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x00002CCC File Offset: 0x00000ECC
		public SectionHeader()
		{
		}

		// Token: 0x040002C9 RID: 713
		public const uint IMAGE_SCN_CNT_CODE = 32U;

		// Token: 0x040002CA RID: 714
		public const uint IMAGE_SCN_CNT_INITIALIZED_DATA = 64U;

		// Token: 0x040002CB RID: 715
		public const uint IMAGE_SCN_MEM_DISCARDABLE = 33554432U;

		// Token: 0x040002CC RID: 716
		public const uint IMAGE_SCN_MEM_EXECUTE = 536870912U;

		// Token: 0x040002CD RID: 717
		public const uint IMAGE_SCN_MEM_READ = 1073741824U;

		// Token: 0x040002CE RID: 718
		public const uint IMAGE_SCN_MEM_WRITE = 2147483648U;

		// Token: 0x040002CF RID: 719
		public string Name;

		// Token: 0x040002D0 RID: 720
		public uint VirtualSize;

		// Token: 0x040002D1 RID: 721
		public uint VirtualAddress;

		// Token: 0x040002D2 RID: 722
		public uint SizeOfRawData;

		// Token: 0x040002D3 RID: 723
		public uint PointerToRawData;

		// Token: 0x040002D4 RID: 724
		public uint PointerToRelocations;

		// Token: 0x040002D5 RID: 725
		public uint PointerToLinenumbers;

		// Token: 0x040002D6 RID: 726
		public ushort NumberOfRelocations;

		// Token: 0x040002D7 RID: 727
		public ushort NumberOfLinenumbers;

		// Token: 0x040002D8 RID: 728
		public uint Characteristics;
	}
}
