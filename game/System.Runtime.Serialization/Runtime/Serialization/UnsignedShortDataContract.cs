using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000F9 RID: 249
	internal class UnsignedShortDataContract : PrimitiveDataContract
	{
		// Token: 0x06000DFB RID: 3579 RVA: 0x0003707B File Offset: 0x0003527B
		internal UnsignedShortDataContract() : base(typeof(ushort), DictionaryGlobals.UnsignedShortLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x00037097 File Offset: 0x00035297
		internal override string WriteMethodName
		{
			get
			{
				return "WriteUnsignedShort";
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0003709E File Offset: 0x0003529E
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsUnsignedShort";
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000370A5 File Offset: 0x000352A5
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteUnsignedShort((ushort)obj);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000370B3 File Offset: 0x000352B3
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsUnsignedShort(), context);
			}
			return reader.ReadElementContentAsUnsignedShort();
		}
	}
}
