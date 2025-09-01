using System;

namespace System.Xml
{
	// Token: 0x02000007 RID: 7
	internal class BooleanArrayHelperWithDictionaryString : ArrayHelper<XmlDictionaryString, bool>
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000226D File Offset: 0x0000046D
		protected override int ReadArray(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000227D File Offset: 0x0000047D
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000228F File Offset: 0x0000048F
		public BooleanArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002297 File Offset: 0x00000497
		// Note: this type is marked as 'beforefieldinit'.
		static BooleanArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x04000004 RID: 4
		public static readonly BooleanArrayHelperWithDictionaryString Instance = new BooleanArrayHelperWithDictionaryString();
	}
}
