using System;

namespace System.Xml
{
	// Token: 0x02000017 RID: 23
	internal class GuidArrayHelperWithDictionaryString : ArrayHelper<XmlDictionaryString, Guid>
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000025CD File Offset: 0x000007CD
		protected override int ReadArray(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000025DD File Offset: 0x000007DD
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000025EF File Offset: 0x000007EF
		public GuidArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000025F7 File Offset: 0x000007F7
		// Note: this type is marked as 'beforefieldinit'.
		static GuidArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x04000014 RID: 20
		public static readonly GuidArrayHelperWithDictionaryString Instance = new GuidArrayHelperWithDictionaryString();
	}
}
