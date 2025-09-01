using System;

namespace System.Xml
{
	// Token: 0x0200000A RID: 10
	internal class Int32ArrayHelperWithString : ArrayHelper<string, int>
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000230F File Offset: 0x0000050F
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, int[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000231F File Offset: 0x0000051F
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, int[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002331 File Offset: 0x00000531
		public Int32ArrayHelperWithString()
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002339 File Offset: 0x00000539
		// Note: this type is marked as 'beforefieldinit'.
		static Int32ArrayHelperWithString()
		{
		}

		// Token: 0x04000007 RID: 7
		public static readonly Int32ArrayHelperWithString Instance = new Int32ArrayHelperWithString();
	}
}
