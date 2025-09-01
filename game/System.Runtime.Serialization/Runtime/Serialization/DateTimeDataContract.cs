using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000106 RID: 262
	internal class DateTimeDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x000373B7 File Offset: 0x000355B7
		internal DateTimeDataContract() : base(typeof(DateTime), DictionaryGlobals.DateTimeLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x000373D3 File Offset: 0x000355D3
		internal override string WriteMethodName
		{
			get
			{
				return "WriteDateTime";
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x000373DA File Offset: 0x000355DA
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsDateTime";
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000373E1 File Offset: 0x000355E1
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteDateTime((DateTime)obj);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000373EF File Offset: 0x000355EF
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsDateTime(), context);
			}
			return reader.ReadElementContentAsDateTime();
		}
	}
}
