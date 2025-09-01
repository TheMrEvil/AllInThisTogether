using System;
using System.IO;

namespace IKVM.Reflection.Reader
{
	// Token: 0x020000A2 RID: 162
	internal class SectionHeader
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x0001D044 File Offset: 0x0001B244
		internal void Read(BinaryReader br)
		{
			char[] array = new char[8];
			int num = 8;
			for (int i = 0; i < 8; i++)
			{
				byte b = br.ReadByte();
				array[i] = (char)b;
				if (b == 0 && num == 8)
				{
					num = i;
				}
			}
			this.Name = new string(array, 0, num);
			this.VirtualSize = br.ReadUInt32();
			this.VirtualAddress = br.ReadUInt32();
			this.SizeOfRawData = br.ReadUInt32();
			this.PointerToRawData = br.ReadUInt32();
			this.PointerToRelocations = br.ReadUInt32();
			this.PointerToLinenumbers = br.ReadUInt32();
			this.NumberOfRelocations = br.ReadUInt16();
			this.NumberOfLinenumbers = br.ReadUInt16();
			this.Characteristics = br.ReadUInt32();
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00002CCC File Offset: 0x00000ECC
		public SectionHeader()
		{
		}

		// Token: 0x04000379 RID: 889
		public const uint IMAGE_SCN_CNT_CODE = 32U;

		// Token: 0x0400037A RID: 890
		public const uint IMAGE_SCN_CNT_INITIALIZED_DATA = 64U;

		// Token: 0x0400037B RID: 891
		public const uint IMAGE_SCN_MEM_DISCARDABLE = 33554432U;

		// Token: 0x0400037C RID: 892
		public const uint IMAGE_SCN_MEM_EXECUTE = 536870912U;

		// Token: 0x0400037D RID: 893
		public const uint IMAGE_SCN_MEM_READ = 1073741824U;

		// Token: 0x0400037E RID: 894
		public const uint IMAGE_SCN_MEM_WRITE = 2147483648U;

		// Token: 0x0400037F RID: 895
		public string Name;

		// Token: 0x04000380 RID: 896
		public uint VirtualSize;

		// Token: 0x04000381 RID: 897
		public uint VirtualAddress;

		// Token: 0x04000382 RID: 898
		public uint SizeOfRawData;

		// Token: 0x04000383 RID: 899
		public uint PointerToRawData;

		// Token: 0x04000384 RID: 900
		public uint PointerToRelocations;

		// Token: 0x04000385 RID: 901
		public uint PointerToLinenumbers;

		// Token: 0x04000386 RID: 902
		public ushort NumberOfRelocations;

		// Token: 0x04000387 RID: 903
		public ushort NumberOfLinenumbers;

		// Token: 0x04000388 RID: 904
		public uint Characteristics;
	}
}
