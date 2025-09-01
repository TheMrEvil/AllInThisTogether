using System;

namespace System.Xml
{
	// Token: 0x02000006 RID: 6
	internal class BooleanArrayHelperWithString : ArrayHelper<string, bool>
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002237 File Offset: 0x00000437
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002247 File Offset: 0x00000447
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002259 File Offset: 0x00000459
		public BooleanArrayHelperWithString()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002261 File Offset: 0x00000461
		// Note: this type is marked as 'beforefieldinit'.
		static BooleanArrayHelperWithString()
		{
		}

		// Token: 0x04000003 RID: 3
		public static readonly BooleanArrayHelperWithString Instance = new BooleanArrayHelperWithString();
	}
}
