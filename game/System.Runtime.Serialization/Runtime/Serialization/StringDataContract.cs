using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000107 RID: 263
	internal class StringDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x00037412 File Offset: 0x00035612
		internal StringDataContract() : this(DictionaryGlobals.StringLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00037424 File Offset: 0x00035624
		internal StringDataContract(XmlDictionaryString name, XmlDictionaryString ns) : base(typeof(string), name, ns)
		{
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00037438 File Offset: 0x00035638
		internal override string WriteMethodName
		{
			get
			{
				return "WriteString";
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0003743F File Offset: 0x0003563F
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsString";
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00037446 File Offset: 0x00035646
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteString((string)obj);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00037454 File Offset: 0x00035654
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsString(), context);
			}
			if (!base.TryReadNullAtTopLevel(reader))
			{
				return reader.ReadElementContentAsString();
			}
			return null;
		}
	}
}
