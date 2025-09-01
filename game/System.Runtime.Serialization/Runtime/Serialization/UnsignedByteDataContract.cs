using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000F7 RID: 247
	internal class UnsignedByteDataContract : PrimitiveDataContract
	{
		// Token: 0x06000DF1 RID: 3569 RVA: 0x00036FC5 File Offset: 0x000351C5
		internal UnsignedByteDataContract() : base(typeof(byte), DictionaryGlobals.UnsignedByteLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00036FE1 File Offset: 0x000351E1
		internal override string WriteMethodName
		{
			get
			{
				return "WriteUnsignedByte";
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00036FE8 File Offset: 0x000351E8
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsUnsignedByte";
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00036FEF File Offset: 0x000351EF
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteUnsignedByte((byte)obj);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00036FFD File Offset: 0x000351FD
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsUnsignedByte(), context);
			}
			return reader.ReadElementContentAsUnsignedByte();
		}
	}
}
