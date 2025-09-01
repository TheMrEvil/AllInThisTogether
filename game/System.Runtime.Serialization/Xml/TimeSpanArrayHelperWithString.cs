using System;

namespace System.Xml
{
	// Token: 0x02000018 RID: 24
	internal class TimeSpanArrayHelperWithString : ArrayHelper<string, TimeSpan>
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002603 File Offset: 0x00000803
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002613 File Offset: 0x00000813
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002625 File Offset: 0x00000825
		public TimeSpanArrayHelperWithString()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000262D File Offset: 0x0000082D
		// Note: this type is marked as 'beforefieldinit'.
		static TimeSpanArrayHelperWithString()
		{
		}

		// Token: 0x04000015 RID: 21
		public static readonly TimeSpanArrayHelperWithString Instance = new TimeSpanArrayHelperWithString();
	}
}
