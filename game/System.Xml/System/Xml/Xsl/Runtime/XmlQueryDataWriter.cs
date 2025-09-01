using System;
using System.IO;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000481 RID: 1153
	internal class XmlQueryDataWriter : BinaryWriter
	{
		// Token: 0x06002D26 RID: 11558 RVA: 0x00109263 File Offset: 0x00107463
		public XmlQueryDataWriter(Stream output) : base(output)
		{
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x0010926C File Offset: 0x0010746C
		public void WriteInt32Encoded(int value)
		{
			base.Write7BitEncodedInt(value);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x00109275 File Offset: 0x00107475
		public void WriteStringQ(string value)
		{
			this.Write(value != null);
			if (value != null)
			{
				this.Write(value);
			}
		}
	}
}
