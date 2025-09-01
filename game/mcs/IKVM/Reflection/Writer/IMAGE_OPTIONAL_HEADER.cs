using System;
using System.IO;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000089 RID: 137
	internal sealed class IMAGE_OPTIONAL_HEADER
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x000165B0 File Offset: 0x000147B0
		internal void Write(BinaryWriter bw)
		{
			bw.Write(this.Magic);
			bw.Write(this.MajorLinkerVersion);
			bw.Write(this.MinorLinkerVersion);
			bw.Write(this.SizeOfCode);
			bw.Write(this.SizeOfInitializedData);
			bw.Write(this.SizeOfUninitializedData);
			bw.Write(this.AddressOfEntryPoint);
			bw.Write(this.BaseOfCode);
			if (this.Magic == 267)
			{
				bw.Write(this.BaseOfData);
				bw.Write((uint)this.ImageBase);
			}
			else
			{
				bw.Write(this.ImageBase);
			}
			bw.Write(this.SectionAlignment);
			bw.Write(this.FileAlignment);
			bw.Write(this.MajorOperatingSystemVersion);
			bw.Write(this.MinorOperatingSystemVersion);
			bw.Write(this.MajorImageVersion);
			bw.Write(this.MinorImageVersion);
			bw.Write(this.MajorSubsystemVersion);
			bw.Write(this.MinorSubsystemVersion);
			bw.Write(this.Win32VersionValue);
			bw.Write(this.SizeOfImage);
			bw.Write(this.SizeOfHeaders);
			bw.Write(this.CheckSum);
			bw.Write(this.Subsystem);
			bw.Write(this.DllCharacteristics);
			if (this.Magic == 267)
			{
				bw.Write((uint)this.SizeOfStackReserve);
				bw.Write((uint)this.SizeOfStackCommit);
				bw.Write((uint)this.SizeOfHeapReserve);
				bw.Write((uint)this.SizeOfHeapCommit);
			}
			else
			{
				bw.Write(this.SizeOfStackReserve);
				bw.Write(this.SizeOfStackCommit);
				bw.Write(this.SizeOfHeapReserve);
				bw.Write(this.SizeOfHeapCommit);
			}
			bw.Write(this.LoaderFlags);
			bw.Write(this.NumberOfRvaAndSizes);
			for (int i = 0; i < this.DataDirectory.Length; i++)
			{
				bw.Write(this.DataDirectory[i].VirtualAddress);
				bw.Write(this.DataDirectory[i].Size);
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000167C8 File Offset: 0x000149C8
		public IMAGE_OPTIONAL_HEADER()
		{
		}

		// Token: 0x040002A6 RID: 678
		public const ushort IMAGE_NT_OPTIONAL_HDR32_MAGIC = 267;

		// Token: 0x040002A7 RID: 679
		public const ushort IMAGE_NT_OPTIONAL_HDR64_MAGIC = 523;

		// Token: 0x040002A8 RID: 680
		public const ushort IMAGE_SUBSYSTEM_WINDOWS_GUI = 2;

		// Token: 0x040002A9 RID: 681
		public const ushort IMAGE_SUBSYSTEM_WINDOWS_CUI = 3;

		// Token: 0x040002AA RID: 682
		public ushort Magic = 267;

		// Token: 0x040002AB RID: 683
		public byte MajorLinkerVersion = 8;

		// Token: 0x040002AC RID: 684
		public byte MinorLinkerVersion;

		// Token: 0x040002AD RID: 685
		public uint SizeOfCode;

		// Token: 0x040002AE RID: 686
		public uint SizeOfInitializedData;

		// Token: 0x040002AF RID: 687
		public uint SizeOfUninitializedData;

		// Token: 0x040002B0 RID: 688
		public uint AddressOfEntryPoint;

		// Token: 0x040002B1 RID: 689
		public uint BaseOfCode;

		// Token: 0x040002B2 RID: 690
		public uint BaseOfData;

		// Token: 0x040002B3 RID: 691
		public ulong ImageBase;

		// Token: 0x040002B4 RID: 692
		public uint SectionAlignment = 8192U;

		// Token: 0x040002B5 RID: 693
		public uint FileAlignment;

		// Token: 0x040002B6 RID: 694
		public ushort MajorOperatingSystemVersion = 4;

		// Token: 0x040002B7 RID: 695
		public ushort MinorOperatingSystemVersion;

		// Token: 0x040002B8 RID: 696
		public ushort MajorImageVersion;

		// Token: 0x040002B9 RID: 697
		public ushort MinorImageVersion;

		// Token: 0x040002BA RID: 698
		public ushort MajorSubsystemVersion = 4;

		// Token: 0x040002BB RID: 699
		public ushort MinorSubsystemVersion;

		// Token: 0x040002BC RID: 700
		public uint Win32VersionValue;

		// Token: 0x040002BD RID: 701
		public uint SizeOfImage;

		// Token: 0x040002BE RID: 702
		public uint SizeOfHeaders;

		// Token: 0x040002BF RID: 703
		public uint CheckSum;

		// Token: 0x040002C0 RID: 704
		public ushort Subsystem;

		// Token: 0x040002C1 RID: 705
		public ushort DllCharacteristics;

		// Token: 0x040002C2 RID: 706
		public ulong SizeOfStackReserve;

		// Token: 0x040002C3 RID: 707
		public ulong SizeOfStackCommit = 4096UL;

		// Token: 0x040002C4 RID: 708
		public ulong SizeOfHeapReserve = 1048576UL;

		// Token: 0x040002C5 RID: 709
		public ulong SizeOfHeapCommit = 4096UL;

		// Token: 0x040002C6 RID: 710
		public uint LoaderFlags;

		// Token: 0x040002C7 RID: 711
		public uint NumberOfRvaAndSizes = 16U;

		// Token: 0x040002C8 RID: 712
		public IMAGE_DATA_DIRECTORY[] DataDirectory = new IMAGE_DATA_DIRECTORY[16];
	}
}
