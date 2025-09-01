using System;

namespace System.Xml
{
	// Token: 0x0200000E RID: 14
	internal class SingleArrayHelperWithString : ArrayHelper<string, float>
	{
		// Token: 0x06000032 RID: 50 RVA: 0x000023E7 File Offset: 0x000005E7
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, float[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000023F7 File Offset: 0x000005F7
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, float[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002409 File Offset: 0x00000609
		public SingleArrayHelperWithString()
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002411 File Offset: 0x00000611
		// Note: this type is marked as 'beforefieldinit'.
		static SingleArrayHelperWithString()
		{
		}

		// Token: 0x0400000B RID: 11
		public static readonly SingleArrayHelperWithString Instance = new SingleArrayHelperWithString();
	}
}
