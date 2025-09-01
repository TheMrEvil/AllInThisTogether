using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000FA RID: 250
	internal class IntDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E00 RID: 3584 RVA: 0x000370D6 File Offset: 0x000352D6
		internal IntDataContract() : base(typeof(int), DictionaryGlobals.IntLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x000370F2 File Offset: 0x000352F2
		internal override string WriteMethodName
		{
			get
			{
				return "WriteInt";
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x000370F9 File Offset: 0x000352F9
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsInt";
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00037100 File Offset: 0x00035300
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteInt((int)obj);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0003710E File Offset: 0x0003530E
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsInt(), context);
			}
			return reader.ReadElementContentAsInt();
		}
	}
}
