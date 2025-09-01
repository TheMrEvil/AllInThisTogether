using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000018 RID: 24
	internal class XmlRawWriterBase64Encoder : Base64Encoder
	{
		// Token: 0x06000048 RID: 72 RVA: 0x0000344E File Offset: 0x0000164E
		internal XmlRawWriterBase64Encoder(XmlRawWriter rawWriter)
		{
			this.rawWriter = rawWriter;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000345D File Offset: 0x0000165D
		internal override void WriteChars(char[] chars, int index, int count)
		{
			this.rawWriter.WriteRaw(chars, index, count);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000346D File Offset: 0x0000166D
		internal override Task WriteCharsAsync(char[] chars, int index, int count)
		{
			return this.rawWriter.WriteRawAsync(chars, index, count);
		}

		// Token: 0x040004FD RID: 1277
		private XmlRawWriter rawWriter;
	}
}
