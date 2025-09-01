using System;

namespace System.Xml
{
	// Token: 0x02000016 RID: 22
	internal class GuidArrayHelperWithString : ArrayHelper<string, Guid>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002597 File Offset: 0x00000797
		protected override int ReadArray(XmlDictionaryReader reader, string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			return reader.ReadArray(localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000025A7 File Offset: 0x000007A7
		protected override void WriteArray(XmlDictionaryWriter writer, string prefix, string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			writer.WriteArray(prefix, localName, namespaceUri, array, offset, count);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000025B9 File Offset: 0x000007B9
		public GuidArrayHelperWithString()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000025C1 File Offset: 0x000007C1
		// Note: this type is marked as 'beforefieldinit'.
		static GuidArrayHelperWithString()
		{
		}

		// Token: 0x04000013 RID: 19
		public static readonly GuidArrayHelperWithString Instance = new GuidArrayHelperWithString();
	}
}
