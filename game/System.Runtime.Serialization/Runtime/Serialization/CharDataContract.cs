using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000F3 RID: 243
	internal class CharDataContract : PrimitiveDataContract
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00036E98 File Offset: 0x00035098
		internal CharDataContract() : this(DictionaryGlobals.CharLocalName, DictionaryGlobals.SerializationNamespace)
		{
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00036EAA File Offset: 0x000350AA
		internal CharDataContract(XmlDictionaryString name, XmlDictionaryString ns) : base(typeof(char), name, ns)
		{
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00036EBE File Offset: 0x000350BE
		internal override string WriteMethodName
		{
			get
			{
				return "WriteChar";
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00036EC5 File Offset: 0x000350C5
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsChar";
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00036ECC File Offset: 0x000350CC
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteChar((char)obj);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00036EDA File Offset: 0x000350DA
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsChar(), context);
			}
			return reader.ReadElementContentAsChar();
		}
	}
}
