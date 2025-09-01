using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000102 RID: 258
	internal class UnsignedLongDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x0003724B File Offset: 0x0003544B
		internal UnsignedLongDataContract() : base(typeof(ulong), DictionaryGlobals.UnsignedLongLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00037267 File Offset: 0x00035467
		internal override string WriteMethodName
		{
			get
			{
				return "WriteUnsignedLong";
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003726E File Offset: 0x0003546E
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsUnsignedLong";
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00037275 File Offset: 0x00035475
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteUnsignedLong((ulong)obj);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00037283 File Offset: 0x00035483
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsUnsignedLong(), context);
			}
			return reader.ReadElementContentAsUnsignedLong();
		}
	}
}
