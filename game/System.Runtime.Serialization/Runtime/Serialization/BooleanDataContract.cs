using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000F5 RID: 245
	internal class BooleanDataContract : PrimitiveDataContract
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x00036F0F File Offset: 0x0003510F
		internal BooleanDataContract() : base(typeof(bool), DictionaryGlobals.BooleanLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00036F2B File Offset: 0x0003512B
		internal override string WriteMethodName
		{
			get
			{
				return "WriteBoolean";
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00036F32 File Offset: 0x00035132
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsBoolean";
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00036F39 File Offset: 0x00035139
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteBoolean((bool)obj);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00036F47 File Offset: 0x00035147
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsBoolean(), context);
			}
			return reader.ReadElementContentAsBoolean();
		}
	}
}
