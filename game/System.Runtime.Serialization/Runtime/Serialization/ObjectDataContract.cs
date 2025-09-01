using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x0200011D RID: 285
	internal class ObjectDataContract : PrimitiveDataContract
	{
		// Token: 0x06000E4D RID: 3661 RVA: 0x0003763C File Offset: 0x0003583C
		internal ObjectDataContract() : base(typeof(object), DictionaryGlobals.ObjectLocalName, DictionaryGlobals.SchemaNamespace)
		{
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x00037658 File Offset: 0x00035858
		internal override string WriteMethodName
		{
			get
			{
				return "WriteAnyType";
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0003765F File Offset: 0x0003585F
		internal override string ReadMethodName
		{
			get
			{
				return "ReadElementContentAsAnyType";
			}
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0000A8EE File Offset: 0x00008AEE
		public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
		{
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00037668 File Offset: 0x00035868
		public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
		{
			object obj;
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				obj = new object();
			}
			else
			{
				string localName = reader.LocalName;
				string namespaceURI = reader.NamespaceURI;
				reader.Read();
				try
				{
					reader.ReadEndElement();
					obj = new object();
				}
				catch (XmlException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Element {0} from namespace {1} cannot have child contents to be deserialized as an object. Please use XElement to deserialize this pattern of XML.", new object[]
					{
						localName,
						namespaceURI
					}), innerException));
				}
			}
			if (context != null)
			{
				return base.HandleReadValue(obj, context);
			}
			return obj;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x000066E8 File Offset: 0x000048E8
		internal override bool CanContainReferences
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x00003127 File Offset: 0x00001327
		internal override bool IsPrimitive
		{
			get
			{
				return false;
			}
		}
	}
}
