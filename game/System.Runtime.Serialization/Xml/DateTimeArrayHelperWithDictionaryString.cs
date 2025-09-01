using System;

namespace System.Xml
{
	// Token: 0x02000015 RID: 21
	internal class DateTimeArrayHelperWithDictionaryString : ArrayHelper<XmlDictionaryString, DateTime>
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002561 File Offset: 0x00000761
		protected override int ReadArray(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002571 File Offset: 0x00000771
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002583 File Offset: 0x00000783
		public DateTimeArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000258B File Offset: 0x0000078B
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x04000012 RID: 18
		public static readonly DateTimeArrayHelperWithDictionaryString Instance = new DateTimeArrayHelperWithDictionaryString();
	}
}
