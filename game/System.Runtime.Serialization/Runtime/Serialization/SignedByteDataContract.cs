using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000F6 RID: 246
	internal class SignedByteDataContract : PrimitiveDataContract
	{
		// Token: 0x06000DEC RID: 3564 RVA: 0x00036F6A File Offset: 0x0003516A
		internal SignedByteDataContract() : base(typeof(sbyte), DictionaryGlobals.SignedByteLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00036F86 File Offset: 0x00035186
		internal override string WriteMethodName
		{
			get
			{
				return "WriteSignedByte";
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00036F8D File Offset: 0x0003518D
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsSignedByte";
			}
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00036F94 File Offset: 0x00035194
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteSignedByte((sbyte)obj);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00036FA2 File Offset: 0x000351A2
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsSignedByte(), context);
			}
			return reader.ReadElementContentAsSignedByte();
		}
	}
}
