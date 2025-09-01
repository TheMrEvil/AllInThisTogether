using System;

namespace System.Xml
{
	// Token: 0x02000008 RID: 8
	internal class Int16ArrayHelperWithString : ArrayHelper<string, short>
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022A3 File Offset: 0x000004A3
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, short[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022B3 File Offset: 0x000004B3
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, short[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022C5 File Offset: 0x000004C5
		public Int16ArrayHelperWithString()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022CD File Offset: 0x000004CD
		// Note: this type is marked as 'beforefieldinit'.
		static Int16ArrayHelperWithString()
		{
		}

		// Token: 0x04000005 RID: 5
		public static readonly Int16ArrayHelperWithString Instance = new Int16ArrayHelperWithString();
	}
}
