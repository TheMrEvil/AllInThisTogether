using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000FC RID: 252
	internal class LongDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x0003718C File Offset: 0x0003538C
		internal LongDataContract() : this(DictionaryGlobals.LongLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003719E File Offset: 0x0003539E
		internal LongDataContract(XmlDictionaryString name, XmlDictionaryString ns) : base(typeof(long), name, ns)
		{
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x000371B2 File Offset: 0x000353B2
		internal override string WriteMethodName
		{
			get
			{
				return "WriteLong";
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000371B9 File Offset: 0x000353B9
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsLong";
			}
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000371C0 File Offset: 0x000353C0
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteLong((long)obj);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000371CE File Offset: 0x000353CE
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsLong(), context);
			}
			return reader.ReadElementContentAsLong();
		}
	}
}
