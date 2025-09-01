using System;
using System.IO;

namespace IKVM.Reflection.Reader
{
	// Token: 0x020000A3 RID: 163
	internal sealed class PEReader
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x0001D0F4 File Offset: 0x0001B2F4
		internal void Read(BinaryReader br, bool mapped)
		{
			this.mapped = mapped;
			this.msdos.signature = br.ReadUInt16();
			br.BaseStream.Seek(58L, SeekOrigin.Current);
			this.msdos.peSignatureOffset = br.ReadUInt32();
			if (this.msdos.signature != 23117)
			{
				throw new BadImageFormatException();
			}
			br.BaseStream.Seek((long)((ulong)this.msdos.peSignatureOffset), SeekOrigin.Begin);
			this.headers.Read(br);
			this.sections = new SectionHeader[(int)this.headers.FileHeader.NumberOfSections];
			for (int i = 0; i < this.sections.Length; i++)
			{
				this.sections[i] = new SectionHeader();
				this.sections[i].Read(br);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001D1C0 File Offset: 0x0001B3C0
		internal IMAGE_FILE_HEADER FileHeader
		{
			get
			{
				return this.headers.FileHeader;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0001D1CD File Offset: 0x0001B3CD
		internal IMAGE_OPTIONAL_HEADER OptionalHeader
		{
			get
			{
				return this.headers.OptionalHeader;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001D1DA File Offset: 0x0001B3DA
		internal uint GetComDescriptorVirtualAddress()
		{
			return this.headers.OptionalHeader.DataDirectory[14].VirtualAddress;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		internal void GetDataDirectoryEntry(int index, out int rva, out int length)
		{
			rva = (int)this.headers.OptionalHeader.DataDirectory[index].VirtualAddress;
			length = (int)this.headers.OptionalHeader.DataDirectory[index].Size;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001D234 File Offset: 0x0001B434
		internal long RvaToFileOffset(uint rva)
		{
			if (this.mapped)
			{
				return (long)((ulong)rva);
			}
			for (int i = 0; i < this.sections.Length; i++)
			{
				if (rva >= this.sections[i].VirtualAddress && rva < this.sections[i].VirtualAddress + this.sections[i].VirtualSize)
				{
					return (long)((ulong)(this.sections[i].PointerToRawData + rva - this.sections[i].VirtualAddress));
				}
			}
			throw new BadImageFormatException();
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001D2B4 File Offset: 0x0001B4B4
		internal bool GetSectionInfo(int rva, out string name, out int characteristics, out int virtualAddress, out int virtualSize, out int pointerToRawData, out int sizeOfRawData)
		{
			for (int i = 0; i < this.sections.Length; i++)
			{
				if ((long)rva >= (long)((ulong)this.sections[i].VirtualAddress) && (long)rva < (long)((ulong)(this.sections[i].VirtualAddress + this.sections[i].VirtualSize)))
				{
					name = this.sections[i].Name;
					characteristics = (int)this.sections[i].Characteristics;
					virtualAddress = (int)this.sections[i].VirtualAddress;
					virtualSize = (int)this.sections[i].VirtualSize;
					pointerToRawData = (int)this.sections[i].PointerToRawData;
					sizeOfRawData = (int)this.sections[i].SizeOfRawData;
					return true;
				}
			}
			name = null;
			characteristics = 0;
			virtualAddress = 0;
			virtualSize = 0;
			pointerToRawData = 0;
			sizeOfRawData = 0;
			return false;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001D386 File Offset: 0x0001B586
		public PEReader()
		{
		}

		// Token: 0x04000389 RID: 905
		private MSDOS_HEADER msdos = new MSDOS_HEADER();

		// Token: 0x0400038A RID: 906
		private IMAGE_NT_HEADERS headers = new IMAGE_NT_HEADERS();

		// Token: 0x0400038B RID: 907
		private SectionHeader[] sections;

		// Token: 0x0400038C RID: 908
		private bool mapped;
	}
}
