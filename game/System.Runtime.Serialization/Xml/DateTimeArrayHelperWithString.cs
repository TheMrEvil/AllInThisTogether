using System;

namespace System.Xml
{
	// Token: 0x02000014 RID: 20
	internal class DateTimeArrayHelperWithString : ArrayHelper<string, DateTime>
	{
		// Token: 0x0600004A RID: 74 RVA: 0x0000252B File Offset: 0x0000072B
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000253B File Offset: 0x0000073B
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000254D File Offset: 0x0000074D
		public DateTimeArrayHelperWithString()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002555 File Offset: 0x00000755
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimeArrayHelperWithString()
		{
		}

		// Token: 0x04000011 RID: 17
		public static readonly DateTimeArrayHelperWithString Instance = new DateTimeArrayHelperWithString();
	}
}
