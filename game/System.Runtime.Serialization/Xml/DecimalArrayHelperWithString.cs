using System;

namespace System.Xml
{
	// Token: 0x02000012 RID: 18
	internal class DecimalArrayHelperWithString : ArrayHelper<string, decimal>
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000024BF File Offset: 0x000006BF
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000024CF File Offset: 0x000006CF
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000024E1 File Offset: 0x000006E1
		public DecimalArrayHelperWithString()
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000024E9 File Offset: 0x000006E9
		// Note: this type is marked as 'beforefieldinit'.
		static DecimalArrayHelperWithString()
		{
		}

		// Token: 0x0400000F RID: 15
		public static readonly DecimalArrayHelperWithString Instance = new DecimalArrayHelperWithString();
	}
}
