using System;

namespace System.Xml
{
	// Token: 0x0200000D RID: 13
	internal class Int64ArrayHelperWithDictionaryString : ArrayHelper<XmlDictionaryString, long>
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000023B1 File Offset: 0x000005B1
		protected override int ReadArray(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000023C1 File Offset: 0x000005C1
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000023D3 File Offset: 0x000005D3
		public Int64ArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000023DB File Offset: 0x000005DB
		// Note: this type is marked as 'beforefieldinit'.
		static Int64ArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x0400000A RID: 10
		public static readonly Int64ArrayHelperWithDictionaryString Instance = new Int64ArrayHelperWithDictionaryString();
	}
}
