using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000120 RID: 288
	internal class GuidDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E5B RID: 3675 RVA: 0x00037767 File Offset: 0x00035967
		internal GuidDataContract() : this(DictionaryGlobals.GuidLocalName, DictionaryGlobals.SerializationNamespace)
		{
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00037779 File Offset: 0x00035979
		internal GuidDataContract(XmlDictionaryString name, XmlDictionaryString ns) : base(typeof(Guid), name, ns)
		{
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0003778D File Offset: 0x0003598D
		internal override string WriteMethodName
		{
			get
			{
				return "WriteGuid";
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00037794 File Offset: 0x00035994
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsGuid";
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0003779B File Offset: 0x0003599B
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteGuid((Guid)obj);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000377A9 File Offset: 0x000359A9
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsGuid(), context);
			}
			return reader.ReadElementContentAsGuid();
		}
	}
}
