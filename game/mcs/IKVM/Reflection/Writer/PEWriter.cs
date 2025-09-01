using System;
using System.IO;
using System.Text;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000086 RID: 134
	internal sealed class PEWriter
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x00016294 File Offset: 0x00014494
		internal PEWriter(Stream stream)
		{
			this.bw = new BinaryWriter(stream);
			this.WriteMSDOSHeader();
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000162B9 File Offset: 0x000144B9
		public IMAGE_NT_HEADERS Headers
		{
			get
			{
				return this.hdr;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x000162C1 File Offset: 0x000144C1
		public uint HeaderSize
		{
			get
			{
				return (uint)(152 + this.hdr.FileHeader.SizeOfOptionalHeader + this.hdr.FileHeader.NumberOfSections * 40);
			}
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000162ED File Offset: 0x000144ED
		private void WriteMSDOSHeader()
		{
			this.bw.Write(new byte[]
			{
				77,
				90,
				144,
				0,
				3,
				0,
				0,
				0,
				4,
				0,
				0,
				0,
				byte.MaxValue,
				byte.MaxValue,
				0,
				0,
				184,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				64,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				128,
				0,
				0,
				0,
				14,
				31,
				186,
				14,
				0,
				180,
				9,
				205,
				33,
				184,
				1,
				76,
				205,
				33,
				84,
				104,
				105,
				115,
				32,
				112,
				114,
				111,
				103,
				114,
				97,
				109,
				32,
				99,
				97,
				110,
				110,
				111,
				116,
				32,
				98,
				101,
				32,
				114,
				117,
				110,
				32,
				105,
				110,
				32,
				68,
				79,
				83,
				32,
				109,
				111,
				100,
				101,
				46,
				13,
				13,
				10,
				36,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			});
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00016310 File Offset: 0x00014510
		internal void WritePEHeaders()
		{
			this.bw.Write(this.hdr.Signature);
			this.bw.Write(this.hdr.FileHeader.Machine);
			this.bw.Write(this.hdr.FileHeader.NumberOfSections);
			this.bw.Write(this.hdr.FileHeader.TimeDateStamp);
			this.bw.Write(this.hdr.FileHeader.PointerToSymbolTable);
			this.bw.Write(this.hdr.FileHeader.NumberOfSymbols);
			this.bw.Write(this.hdr.FileHeader.SizeOfOptionalHeader);
			this.bw.Write(this.hdr.FileHeader.Characteristics);
			this.hdr.OptionalHeader.Write(this.bw);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00016408 File Offset: 0x00014608
		internal void WriteSectionHeader(SectionHeader sectionHeader)
		{
			byte[] array = new byte[8];
			Encoding.UTF8.GetBytes(sectionHeader.Name, 0, sectionHeader.Name.Length, array, 0);
			this.bw.Write(array);
			this.bw.Write(sectionHeader.VirtualSize);
			this.bw.Write(sectionHeader.VirtualAddress);
			this.bw.Write(sectionHeader.SizeOfRawData);
			this.bw.Write(sectionHeader.PointerToRawData);
			this.bw.Write(sectionHeader.PointerToRelocations);
			this.bw.Write(sectionHeader.PointerToLinenumbers);
			this.bw.Write(sectionHeader.NumberOfRelocations);
			this.bw.Write(sectionHeader.NumberOfLinenumbers);
			this.bw.Write(sectionHeader.Characteristics);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000164E0 File Offset: 0x000146E0
		internal uint ToFileAlignment(uint p)
		{
			return p + (this.Headers.OptionalHeader.FileAlignment - 1U) & ~(this.Headers.OptionalHeader.FileAlignment - 1U);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001650A File Offset: 0x0001470A
		internal uint ToSectionAlignment(uint p)
		{
			return p + (this.Headers.OptionalHeader.SectionAlignment - 1U) & ~(this.Headers.OptionalHeader.SectionAlignment - 1U);
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00016534 File Offset: 0x00014734
		internal bool Is32Bit
		{
			get
			{
				return (this.Headers.FileHeader.Characteristics & 256) > 0;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001654F File Offset: 0x0001474F
		internal uint Thumb
		{
			get
			{
				if (this.Headers.FileHeader.Machine != 452)
				{
					return 0U;
				}
				return 1U;
			}
		}

		// Token: 0x04000292 RID: 658
		private readonly BinaryWriter bw;

		// Token: 0x04000293 RID: 659
		private readonly IMAGE_NT_HEADERS hdr = new IMAGE_NT_HEADERS();
	}
}
