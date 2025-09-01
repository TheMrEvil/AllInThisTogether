using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000019 RID: 25
	internal class XmlTextWriterBase64Encoder : Base64Encoder
	{
		// Token: 0x0600004B RID: 75 RVA: 0x0000347D File Offset: 0x0000167D
		internal XmlTextWriterBase64Encoder(XmlTextEncoder xmlTextEncoder)
		{
			this.xmlTextEncoder = xmlTextEncoder;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000348C File Offset: 0x0000168C
		internal override void WriteChars(char[] chars, int index, int count)
		{
			this.xmlTextEncoder.WriteRaw(chars, index, count);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000349C File Offset: 0x0000169C
		internal override Task WriteCharsAsync(char[] chars, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040004FE RID: 1278
		private XmlTextEncoder xmlTextEncoder;
	}
}
