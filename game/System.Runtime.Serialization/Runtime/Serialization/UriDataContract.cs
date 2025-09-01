using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000122 RID: 290
	internal class UriDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x000377DE File Offset: 0x000359DE
		internal UriDataContract() : base(typeof(Uri), DictionaryGlobals.UriLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x000377FA File Offset: 0x000359FA
		internal override string WriteMethodName
		{
			get
			{
				return "WriteUri";
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00037801 File Offset: 0x00035A01
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsUri";
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00037808 File Offset: 0x00035A08
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteUri((Uri)obj);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00037816 File Offset: 0x00035A16
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsUri(), context);
			}
			if (!base.TryReadNullAtTopLevel(reader))
			{
				return reader.ReadElementContentAsUri();
			}
			return null;
		}
	}
}
