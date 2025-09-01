using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020000F8 RID: 248
	internal class ShortDataContract : PrimitiveDataContract
	{
		// Token: 0x06000DF6 RID: 3574 RVA: 0x00037020 File Offset: 0x00035220
		internal ShortDataContract() : base(typeof(short), DictionaryGlobals.ShortLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0003703C File Offset: 0x0003523C
		internal override string WriteMethodName
		{
			get
			{
				return "WriteShort";
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x00037043 File Offset: 0x00035243
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsShort";
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003704A File Offset: 0x0003524A
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteShort((short)obj);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00037058 File Offset: 0x00035258
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsShort(), context);
			}
			return reader.ReadElementContentAsShort();
		}
	}
}
