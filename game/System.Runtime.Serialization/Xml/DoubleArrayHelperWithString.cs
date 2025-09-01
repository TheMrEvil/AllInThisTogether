using System;

namespace System.Xml
{
	// Token: 0x02000010 RID: 16
	internal class DoubleArrayHelperWithString : ArrayHelper<string, double>
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002453 File Offset: 0x00000653
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, double[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002463 File Offset: 0x00000663
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, double[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002475 File Offset: 0x00000675
		public DoubleArrayHelperWithString()
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000247D File Offset: 0x0000067D
		// Note: this type is marked as 'beforefieldinit'.
		static DoubleArrayHelperWithString()
		{
		}

		// Token: 0x0400000D RID: 13
		public static readonly DoubleArrayHelperWithString Instance = new DoubleArrayHelperWithString();
	}
}
