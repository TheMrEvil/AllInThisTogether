using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200011C RID: 284
	internal class ByteArrayDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x000375E0 File Offset: 0x000357E0
		internal ByteArrayDataContract() : base(typeof(byte[]), DictionaryGlobals.ByteArrayLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x000375FC File Offset: 0x000357FC
		internal override string WriteMethodName
		{
			get
			{
				return "WriteBase64";
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00037603 File Offset: 0x00035803
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsBase64";
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003760A File Offset: 0x0003580A
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteBase64((byte[])obj);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00037618 File Offset: 0x00035818
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsBase64(), context);
			}
			if (!base.TryReadNullAtTopLevel(reader))
			{
				return reader.ReadElementContentAsBase64();
			}
			return null;
		}
	}
}
