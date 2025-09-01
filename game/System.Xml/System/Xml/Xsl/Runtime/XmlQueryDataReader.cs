using System;
using System.IO;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000480 RID: 1152
	internal class XmlQueryDataReader : BinaryReader
	{
		// Token: 0x06002D22 RID: 11554 RVA: 0x0010920C File Offset: 0x0010740C
		public XmlQueryDataReader(Stream input) : base(input)
		{
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x00109215 File Offset: 0x00107415
		public int ReadInt32Encoded()
		{
			return base.Read7BitEncodedInt();
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x0010921D File Offset: 0x0010741D
		public string ReadStringQ()
		{
			if (!this.ReadBoolean())
			{
				return null;
			}
			return this.ReadString();
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x00109230 File Offset: 0x00107430
		public sbyte ReadSByte(sbyte minValue, sbyte maxValue)
		{
			sbyte b = this.ReadSByte();
			if (b < minValue)
			{
				throw new ArgumentOutOfRangeException("minValue");
			}
			if (maxValue < b)
			{
				throw new ArgumentOutOfRangeException("maxValue");
			}
			return b;
		}
	}
}
