using System;
using System.IO;

namespace IKVM.Reflection.Reader
{
	// Token: 0x020000A0 RID: 160
	internal sealed class IMAGE_OPTIONAL_HEADER
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		internal void Read(BinaryReader br)
		{
			this.Magic = br.ReadUInt16();
			if (this.Magic != 267 && this.Magic != 523)
			{
				throw new BadImageFormatException();
			}
			this.MajorLinkerVersion = br.ReadByte();
			this.MinorLinkerVersion = br.ReadByte();
			this.SizeOfCode = br.ReadUInt32();
			this.SizeOfInitializedData = br.ReadUInt32();
			this.SizeOfUninitializedData = br.ReadUInt32();
			this.AddressOfEntryPoint = br.ReadUInt32();
			this.BaseOfCode = br.ReadUInt32();
			if (this.Magic == 267)
			{
				this.BaseOfData = br.ReadUInt32();
				this.ImageBase = (ulong)br.ReadUInt32();
			}
			else
			{
				this.ImageBase = br.ReadUInt64();
			}
			this.SectionAlignment = br.ReadUInt32();
			this.FileAlignment = br.ReadUInt32();
			this.MajorOperatingSystemVersion = br.ReadUInt16();
			this.MinorOperatingSystemVersion = br.ReadUInt16();
			this.MajorImageVersion = br.ReadUInt16();
			this.MinorImageVersion = br.ReadUInt16();
			this.MajorSubsystemVersion = br.ReadUInt16();
			this.MinorSubsystemVersion = br.ReadUInt16();
			this.Win32VersionValue = br.ReadUInt32();
			this.SizeOfImage = br.ReadUInt32();
			this.SizeOfHeaders = br.ReadUInt32();
			this.CheckSum = br.ReadUInt32();
			this.Subsystem = br.ReadUInt16();
			this.DllCharacteristics = br.ReadUInt16();
			if (this.Magic == 267)
			{
				this.SizeOfStackReserve = (ulong)br.ReadUInt32();
				this.SizeOfStackCommit = (ulong)br.ReadUInt32();
				this.SizeOfHeapReserve = (ulong)br.ReadUInt32();
				this.SizeOfHeapCommit = (ulong)br.ReadUInt32();
			}
			else
			{
				this.SizeOfStackReserve = br.ReadUInt64();
				this.SizeOfStackCommit = br.ReadUInt64();
				this.SizeOfHeapReserve = br.ReadUInt64();
				this.SizeOfHeapCommit = br.ReadUInt64();
			}
			this.LoaderFlags = br.ReadUInt32();
			this.NumberOfRvaAndSizes = br.ReadUInt32();
			this.DataDirectory = new IMAGE_DATA_DIRECTORY[this.NumberOfRvaAndSizes];
			for (uint num = 0U; num < this.NumberOfRvaAndSizes; num += 1U)
			{
				this.DataDirectory[(int)num] = default(IMAGE_DATA_DIRECTORY);
				this.DataDirectory[(int)num].Read(br);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00002CCC File Offset: 0x00000ECC
		public IMAGE_OPTIONAL_HEADER()
		{
		}

		// Token: 0x04000354 RID: 852
		public const ushort IMAGE_NT_OPTIONAL_HDR32_MAGIC = 267;

		// Token: 0x04000355 RID: 853
		public const ushort IMAGE_NT_OPTIONAL_HDR64_MAGIC = 523;

		// Token: 0x04000356 RID: 854
		public const ushort IMAGE_SUBSYSTEM_WINDOWS_GUI = 2;

		// Token: 0x04000357 RID: 855
		public const ushort IMAGE_SUBSYSTEM_WINDOWS_CUI = 3;

		// Token: 0x04000358 RID: 856
		public ushort Magic;

		// Token: 0x04000359 RID: 857
		public byte MajorLinkerVersion;

		// Token: 0x0400035A RID: 858
		public byte MinorLinkerVersion;

		// Token: 0x0400035B RID: 859
		public uint SizeOfCode;

		// Token: 0x0400035C RID: 860
		public uint SizeOfInitializedData;

		// Token: 0x0400035D RID: 861
		public uint SizeOfUninitializedData;

		// Token: 0x0400035E RID: 862
		public uint AddressOfEntryPoint;

		// Token: 0x0400035F RID: 863
		public uint BaseOfCode;

		// Token: 0x04000360 RID: 864
		public uint BaseOfData;

		// Token: 0x04000361 RID: 865
		public ulong ImageBase;

		// Token: 0x04000362 RID: 866
		public uint SectionAlignment;

		// Token: 0x04000363 RID: 867
		public uint FileAlignment;

		// Token: 0x04000364 RID: 868
		public ushort MajorOperatingSystemVersion;

		// Token: 0x04000365 RID: 869
		public ushort MinorOperatingSystemVersion;

		// Token: 0x04000366 RID: 870
		public ushort MajorImageVersion;

		// Token: 0x04000367 RID: 871
		public ushort MinorImageVersion;

		// Token: 0x04000368 RID: 872
		public ushort MajorSubsystemVersion;

		// Token: 0x04000369 RID: 873
		public ushort MinorSubsystemVersion;

		// Token: 0x0400036A RID: 874
		public uint Win32VersionValue;

		// Token: 0x0400036B RID: 875
		public uint SizeOfImage;

		// Token: 0x0400036C RID: 876
		public uint SizeOfHeaders;

		// Token: 0x0400036D RID: 877
		public uint CheckSum;

		// Token: 0x0400036E RID: 878
		public ushort Subsystem;

		// Token: 0x0400036F RID: 879
		public ushort DllCharacteristics;

		// Token: 0x04000370 RID: 880
		public ulong SizeOfStackReserve;

		// Token: 0x04000371 RID: 881
		public ulong SizeOfStackCommit;

		// Token: 0x04000372 RID: 882
		public ulong SizeOfHeapReserve;

		// Token: 0x04000373 RID: 883
		public ulong SizeOfHeapCommit;

		// Token: 0x04000374 RID: 884
		public uint LoaderFlags;

		// Token: 0x04000375 RID: 885
		public uint NumberOfRvaAndSizes;

		// Token: 0x04000376 RID: 886
		public IMAGE_DATA_DIRECTORY[] DataDirectory;
	}
}
