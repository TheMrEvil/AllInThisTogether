using System;

namespace System.Xml
{
	// Token: 0x02000013 RID: 19
	internal class DecimalArrayHelperWithDictionaryString : ArrayHelper<XmlDictionaryString, decimal>
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000024F5 File Offset: 0x000006F5
		protected override int ReadArray(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002505 File Offset: 0x00000705
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002517 File Offset: 0x00000717
		public DecimalArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000251F File Offset: 0x0000071F
		// Note: this type is marked as 'beforefieldinit'.
		static DecimalArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x04000010 RID: 16
		public static readonly DecimalArrayHelperWithDictionaryString Instance = new DecimalArrayHelperWithDictionaryString();
	}
}
