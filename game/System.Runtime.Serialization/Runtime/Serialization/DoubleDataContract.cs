using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000104 RID: 260
	internal class DoubleDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x00037301 File Offset: 0x00035501
		internal DoubleDataContract() : base(typeof(double), DictionaryGlobals.DoubleLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0003731D File Offset: 0x0003551D
		internal override string WriteMethodName
		{
			get
			{
				return "WriteDouble";
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00037324 File Offset: 0x00035524
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsDouble";
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003732B File Offset: 0x0003552B
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteDouble((double)obj);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00037339 File Offset: 0x00035539
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsDouble(), context);
			}
			return reader.ReadElementContentAsDouble();
		}
	}
}
