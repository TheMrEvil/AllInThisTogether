using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000FB RID: 251
	internal class UnsignedIntDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E05 RID: 3589 RVA: 0x00037131 File Offset: 0x00035331
		internal UnsignedIntDataContract() : base(typeof(uint), DictionaryGlobals.UnsignedIntLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0003714D File Offset: 0x0003534D
		internal override string WriteMethodName
		{
			get
			{
				return "WriteUnsignedInt";
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00037154 File Offset: 0x00035354
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsUnsignedInt";
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003715B File Offset: 0x0003535B
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteUnsignedInt((uint)obj);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00037169 File Offset: 0x00035369
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsUnsignedInt(), context);
			}
			return reader.ReadElementContentAsUnsignedInt();
		}
	}
}
