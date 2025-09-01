using System;

namespace System.Xml
{
	// Token: 0x02000011 RID: 17
	internal class DoubleArrayHelperWithDictionaryString : ArrayHelper<XmlDictionaryString, double>
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002489 File Offset: 0x00000689
		protected override int ReadArray(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002499 File Offset: 0x00000699
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000024AB File Offset: 0x000006AB
		public DoubleArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000024B3 File Offset: 0x000006B3
		// Note: this type is marked as 'beforefieldinit'.
		static DoubleArrayHelperWithDictionaryString()
		{
		}

		// Token: 0x0400000E RID: 14
		public static readonly DoubleArrayHelperWithDictionaryString Instance = new DoubleArrayHelperWithDictionaryString();
	}
}
