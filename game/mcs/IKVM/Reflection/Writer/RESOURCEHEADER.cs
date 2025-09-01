using System;
using System.Text;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200008E RID: 142
	internal struct RESOURCEHEADER
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00017148 File Offset: 0x00015348
		internal RESOURCEHEADER(ByteReader br)
		{
			this.DataSize = br.ReadInt32();
			this.HeaderSize = br.ReadInt32();
			this.TYPE = RESOURCEHEADER.ReadOrdinalOrName(br);
			this.NAME = RESOURCEHEADER.ReadOrdinalOrName(br);
			br.Align(4);
			this.DataVersion = br.ReadInt32();
			this.MemoryFlags = br.ReadUInt16();
			this.LanguageId = br.ReadUInt16();
			this.Version = br.ReadInt32();
			this.Characteristics = br.ReadInt32();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000171C8 File Offset: 0x000153C8
		private static OrdinalOrName ReadOrdinalOrName(ByteReader br)
		{
			char c = br.ReadChar();
			if (c == '￿')
			{
				return new OrdinalOrName(br.ReadUInt16());
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (c != '\0')
			{
				stringBuilder.Append(c);
				c = br.ReadChar();
			}
			return new OrdinalOrName(stringBuilder.ToString());
		}

		// Token: 0x040002E6 RID: 742
		internal int DataSize;

		// Token: 0x040002E7 RID: 743
		internal int HeaderSize;

		// Token: 0x040002E8 RID: 744
		internal OrdinalOrName TYPE;

		// Token: 0x040002E9 RID: 745
		internal OrdinalOrName NAME;

		// Token: 0x040002EA RID: 746
		internal int DataVersion;

		// Token: 0x040002EB RID: 747
		internal ushort MemoryFlags;

		// Token: 0x040002EC RID: 748
		internal ushort LanguageId;

		// Token: 0x040002ED RID: 749
		internal int Version;

		// Token: 0x040002EE RID: 750
		internal int Characteristics;
	}
}
