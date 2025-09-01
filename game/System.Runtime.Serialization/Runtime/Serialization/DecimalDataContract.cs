using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000105 RID: 261
	internal class DecimalDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E24 RID: 3620 RVA: 0x0003735C File Offset: 0x0003555C
		internal DecimalDataContract() : base(typeof(decimal), DictionaryGlobals.DecimalLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00037378 File Offset: 0x00035578
		internal override string WriteMethodName
		{
			get
			{
				return "WriteDecimal";
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0003737F File Offset: 0x0003557F
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsDecimal";
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00037386 File Offset: 0x00035586
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteDecimal((decimal)obj);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00037394 File Offset: 0x00035594
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsDecimal(), context);
			}
			return reader.ReadElementContentAsDecimal();
		}
	}
}
