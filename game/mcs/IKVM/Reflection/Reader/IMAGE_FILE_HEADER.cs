using System;
using System.IO;

namespace IKVM.Reflection.Reader
{
	// Token: 0x0200009F RID: 159
	internal sealed class IMAGE_FILE_HEADER
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		internal void Read(BinaryReader br)
		{
			this.Machine = br.ReadUInt16();
			this.NumberOfSections = br.ReadUInt16();
			this.TimeDateStamp = br.ReadUInt32();
			this.PointerToSymbolTable = br.ReadUInt32();
			this.NumberOfSymbols = br.ReadUInt32();
			this.SizeOfOptionalHeader = br.ReadUInt16();
			this.Characteristics = br.ReadUInt16();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00002CCC File Offset: 0x00000ECC
		public IMAGE_FILE_HEADER()
		{
		}

		// Token: 0x04000346 RID: 838
		public const ushort IMAGE_FILE_MACHINE_I386 = 332;

		// Token: 0x04000347 RID: 839
		public const ushort IMAGE_FILE_MACHINE_IA64 = 512;

		// Token: 0x04000348 RID: 840
		public const ushort IMAGE_FILE_MACHINE_AMD64 = 34404;

		// Token: 0x04000349 RID: 841
		public const ushort IMAGE_FILE_32BIT_MACHINE = 256;

		// Token: 0x0400034A RID: 842
		public const ushort IMAGE_FILE_EXECUTABLE_IMAGE = 2;

		// Token: 0x0400034B RID: 843
		public const ushort IMAGE_FILE_LARGE_ADDRESS_AWARE = 32;

		// Token: 0x0400034C RID: 844
		public const ushort IMAGE_FILE_DLL = 8192;

		// Token: 0x0400034D RID: 845
		public ushort Machine;

		// Token: 0x0400034E RID: 846
		public ushort NumberOfSections;

		// Token: 0x0400034F RID: 847
		public uint TimeDateStamp;

		// Token: 0x04000350 RID: 848
		public uint PointerToSymbolTable;

		// Token: 0x04000351 RID: 849
		public uint NumberOfSymbols;

		// Token: 0x04000352 RID: 850
		public ushort SizeOfOptionalHeader;

		// Token: 0x04000353 RID: 851
		public ushort Characteristics;
	}
}
