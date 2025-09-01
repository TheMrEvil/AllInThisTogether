using System;

namespace System.Xml
{
	// Token: 0x02000019 RID: 25
	internal class TimeSpanArrayHelperWithDictionaryString : ArrayHelper<XmlDictionaryString, TimeSpan>
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002639 File Offset: 0x00000839
		protected override int ReadArray(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002649 File Offset: 0x00000849
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000265B File Offset: 0x0000085B
		public TimeSpanArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002663 File Offset: 0x00000863
		// Note: this type is marked as 'beforefieldinit'.
		static TimeSpanArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x04000016 RID: 22
		public static readonly TimeSpanArrayHelperWithDictionaryString Instance = new TimeSpanArrayHelperWithDictionaryString();
	}
}
