using System;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000088 RID: 136
	internal sealed class IMAGE_FILE_HEADER
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x00016594 File Offset: 0x00014794
		public IMAGE_FILE_HEADER()
		{
		}

		// Token: 0x04000297 RID: 663
		public const ushort IMAGE_FILE_MACHINE_I386 = 332;

		// Token: 0x04000298 RID: 664
		public const ushort IMAGE_FILE_MACHINE_ARM = 452;

		// Token: 0x04000299 RID: 665
		public const ushort IMAGE_FILE_MACHINE_IA64 = 512;

		// Token: 0x0400029A RID: 666
		public const ushort IMAGE_FILE_MACHINE_AMD64 = 34404;

		// Token: 0x0400029B RID: 667
		public const ushort IMAGE_FILE_32BIT_MACHINE = 256;

		// Token: 0x0400029C RID: 668
		public const ushort IMAGE_FILE_EXECUTABLE_IMAGE = 2;

		// Token: 0x0400029D RID: 669
		public const ushort IMAGE_FILE_LARGE_ADDRESS_AWARE = 32;

		// Token: 0x0400029E RID: 670
		public const ushort IMAGE_FILE_DLL = 8192;

		// Token: 0x0400029F RID: 671
		public ushort Machine;

		// Token: 0x040002A0 RID: 672
		public ushort NumberOfSections;

		// Token: 0x040002A1 RID: 673
		public uint TimeDateStamp;

		// Token: 0x040002A2 RID: 674
		public uint PointerToSymbolTable;

		// Token: 0x040002A3 RID: 675
		public uint NumberOfSymbols;

		// Token: 0x040002A4 RID: 676
		public ushort SizeOfOptionalHeader = 224;

		// Token: 0x040002A5 RID: 677
		public ushort Characteristics = 2;
	}
}
