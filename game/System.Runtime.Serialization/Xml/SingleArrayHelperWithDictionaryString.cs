using System;

namespace System.Xml
{
	// Token: 0x0200000F RID: 15
	internal class SingleArrayHelperWithDictionaryString : ArrayHelper<XmlDictionaryString, float>
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000241D File Offset: 0x0000061D
		protected override int ReadArray(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000242D File Offset: 0x0000062D
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000243F File Offset: 0x0000063F
		public SingleArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002447 File Offset: 0x00000647
		// Note: this type is marked as 'beforefieldinit'.
		static SingleArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x0400000C RID: 12
		public static readonly SingleArrayHelperWithDictionaryString Instance = new SingleArrayHelperWithDictionaryString();
	}
}
