using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000123 RID: 291
	internal class QNameDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E67 RID: 3687 RVA: 0x0003783A File Offset: 0x00035A3A
		internal QNameDataContract() : base(typeof(XmlQualifiedName), DictionaryGlobals.QNameLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00037856 File Offset: 0x00035A56
		internal override string WriteMethodName
		{
			get
			{
				return "WriteQName";
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0003785D File Offset: 0x00035A5D
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsQName";
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00003127 File Offset: 0x00001327
		internal override bool IsPrimitive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x00037864 File Offset: 0x00035A64
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
			writer.WriteQName((XmlQualifiedName)obj);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00037872 File Offset: 0x00035A72
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			if (context != null)
			{
				return base.HandleReadValue(reader.ReadElementContentAsQName(), context);
			}
			if (!base.TryReadNullAtTopLevel(reader))
			{
				return reader.ReadElementContentAsQName();
			}
			return null;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00037898 File Offset: 0x00035A98
		internal override void WriteRootElement(XmlWriterDelegator writer, XmlDictionaryString name, XmlDictionaryString ns)
		{
			if (ns == DictionaryGlobals.SerializationNamespace)
			{
				writer.WriteStartElement("z", name, ns);
				return;
			}
			if (ns != null && ns.Value != null && ns.Value.Length > 0)
			{
				writer.WriteStartElement("q", name, ns);
				return;
			}
			writer.WriteStartElement(name, ns);
		}
	}
}
