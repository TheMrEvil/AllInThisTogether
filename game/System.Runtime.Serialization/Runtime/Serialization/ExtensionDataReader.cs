using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x020000DD RID: 221
	internal class ExtensionDataReader : XmlReader
	{
		// Token: 0x06000C8F RID: 3215 RVA: 0x00033058 File Offset: 0x00031258
		[SecuritySafeCritical]
		static ExtensionDataReader()
		{
			ExtensionDataReader.AddPrefix("i", "http://www.w3.org/2001/XMLSchema-instance");
			ExtensionDataReader.AddPrefix("z", "http://schemas.microsoft.com/2003/10/Serialization/");
			ExtensionDataReader.AddPrefix(string.Empty, string.Empty);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000330A6 File Offset: 0x000312A6
		internal ExtensionDataReader(XmlObjectSerializerReadContext context)
		{
			this.attributeIndex = -1;
			this.context = context;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000330C8 File Offset: 0x000312C8
		internal void SetDeserializedValue(object obj)
		{
			IDataNode dataNode = (this.deserializedDataNodes == null || this.deserializedDataNodes.Count == 0) ? null : this.deserializedDataNodes.Dequeue();
			if (dataNode != null && !(obj is IDataNode))
			{
				dataNode.Value = obj;
				dataNode.IsFinalValue = true;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00033112 File Offset: 0x00031312
		internal IDataNode GetCurrentNode()
		{
			IDataNode dataNode = this.element.dataNode;
			this.Skip();
			return dataNode;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00033125 File Offset: 0x00031325
		internal void SetDataNode(IDataNode dataNode, string name, string ns)
		{
			this.SetNextElement(dataNode, name, ns, null);
			this.element = this.nextElement;
			this.nextElement = null;
			this.SetElement();
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003314C File Offset: 0x0003134C
		internal void Reset()
		{
			this.localName = null;
			this.ns = null;
			this.prefix = null;
			this.value = null;
			this.attributeCount = 0;
			this.attributeIndex = -1;
			this.depth = 0;
			this.element = null;
			this.nextElement = null;
			this.elements = null;
			this.deserializedDataNodes = null;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x000331A6 File Offset: 0x000313A6
		private bool IsXmlDataNode
		{
			get
			{
				return this.internalNodeType == ExtensionDataReader.ExtensionDataNodeType.Xml;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000331B1 File Offset: 0x000313B1
		public override XmlNodeType NodeType
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.nodeType;
				}
				return this.xmlNodeReader.NodeType;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x000331CD File Offset: 0x000313CD
		public override string LocalName
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.localName;
				}
				return this.xmlNodeReader.LocalName;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x000331E9 File Offset: 0x000313E9
		public override string NamespaceURI
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.ns;
				}
				return this.xmlNodeReader.NamespaceURI;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00033205 File Offset: 0x00031405
		public override string Prefix
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.prefix;
				}
				return this.xmlNodeReader.Prefix;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00033221 File Offset: 0x00031421
		public override string Value
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.value;
				}
				return this.xmlNodeReader.Value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0003323D File Offset: 0x0003143D
		public override int Depth
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.depth;
				}
				return this.xmlNodeReader.Depth;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00033259 File Offset: 0x00031459
		public override int AttributeCount
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.attributeCount;
				}
				return this.xmlNodeReader.AttributeCount;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00033275 File Offset: 0x00031475
		public override bool EOF
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.readState == ReadState.EndOfFile;
				}
				return this.xmlNodeReader.EOF;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00033294 File Offset: 0x00031494
		public override ReadState ReadState
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.readState;
				}
				return this.xmlNodeReader.ReadState;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x000332B0 File Offset: 0x000314B0
		public override bool IsEmptyElement
		{
			get
			{
				return this.IsXmlDataNode && this.xmlNodeReader.IsEmptyElement;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x000332C7 File Offset: 0x000314C7
		public override bool IsDefault
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return base.IsDefault;
				}
				return this.xmlNodeReader.IsDefault;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000332E3 File Offset: 0x000314E3
		public override char QuoteChar
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return base.QuoteChar;
				}
				return this.xmlNodeReader.QuoteChar;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000332FF File Offset: 0x000314FF
		public override XmlSpace XmlSpace
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return base.XmlSpace;
				}
				return this.xmlNodeReader.XmlSpace;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0003331B File Offset: 0x0003151B
		public override string XmlLang
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return base.XmlLang;
				}
				return this.xmlNodeReader.XmlLang;
			}
		}

		// Token: 0x17000260 RID: 608
		public override string this[int i]
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.GetAttribute(i);
				}
				return this.xmlNodeReader[i];
			}
		}

		// Token: 0x17000261 RID: 609
		public override string this[string name]
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.GetAttribute(name);
				}
				return this.xmlNodeReader[name];
			}
		}

		// Token: 0x17000262 RID: 610
		public override string this[string name, string namespaceURI]
		{
			get
			{
				if (!this.IsXmlDataNode)
				{
					return this.GetAttribute(name, namespaceURI);
				}
				return this.xmlNodeReader[name, namespaceURI];
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00033393 File Offset: 0x00031593
		public override bool MoveToFirstAttribute()
		{
			if (this.IsXmlDataNode)
			{
				return this.xmlNodeReader.MoveToFirstAttribute();
			}
			if (this.attributeCount == 0)
			{
				return false;
			}
			this.MoveToAttribute(0);
			return true;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000333BB File Offset: 0x000315BB
		public override bool MoveToNextAttribute()
		{
			if (this.IsXmlDataNode)
			{
				return this.xmlNodeReader.MoveToNextAttribute();
			}
			if (this.attributeIndex + 1 >= this.attributeCount)
			{
				return false;
			}
			this.MoveToAttribute(this.attributeIndex + 1);
			return true;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000333F4 File Offset: 0x000315F4
		public override void MoveToAttribute(int index)
		{
			if (this.IsXmlDataNode)
			{
				this.xmlNodeReader.MoveToAttribute(index);
				return;
			}
			if (index < 0 || index >= this.attributeCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid XML while deserializing extension data.")));
			}
			this.nodeType = XmlNodeType.Attribute;
			AttributeData attributeData = this.element.attributes[index];
			this.localName = attributeData.localName;
			this.ns = attributeData.ns;
			this.prefix = attributeData.prefix;
			this.value = attributeData.value;
			this.attributeIndex = index;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00033484 File Offset: 0x00031684
		public override string GetAttribute(string name, string namespaceURI)
		{
			if (this.IsXmlDataNode)
			{
				return this.xmlNodeReader.GetAttribute(name, namespaceURI);
			}
			for (int i = 0; i < this.element.attributeCount; i++)
			{
				AttributeData attributeData = this.element.attributes[i];
				if (attributeData.localName == name && attributeData.ns == namespaceURI)
				{
					return attributeData.value;
				}
			}
			return null;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000334F0 File Offset: 0x000316F0
		public override bool MoveToAttribute(string name, string namespaceURI)
		{
			if (this.IsXmlDataNode)
			{
				return this.xmlNodeReader.MoveToAttribute(name, this.ns);
			}
			for (int i = 0; i < this.element.attributeCount; i++)
			{
				AttributeData attributeData = this.element.attributes[i];
				if (attributeData.localName == name && attributeData.ns == namespaceURI)
				{
					this.MoveToAttribute(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00033562 File Offset: 0x00031762
		public override bool MoveToElement()
		{
			if (this.IsXmlDataNode)
			{
				return this.xmlNodeReader.MoveToElement();
			}
			if (this.nodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			this.SetElement();
			return true;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003358C File Offset: 0x0003178C
		private void SetElement()
		{
			this.nodeType = XmlNodeType.Element;
			this.localName = this.element.localName;
			this.ns = this.element.ns;
			this.prefix = this.element.prefix;
			this.value = string.Empty;
			this.attributeCount = this.element.attributeCount;
			this.attributeIndex = -1;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000335F8 File Offset: 0x000317F8
		[SecuritySafeCritical]
		public override string LookupNamespace(string prefix)
		{
			if (this.IsXmlDataNode)
			{
				return this.xmlNodeReader.LookupNamespace(prefix);
			}
			string result;
			if (!ExtensionDataReader.prefixToNsTable.TryGetValue(prefix, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0003362C File Offset: 0x0003182C
		public override void Skip()
		{
			if (this.IsXmlDataNode)
			{
				this.xmlNodeReader.Skip();
				return;
			}
			if (this.ReadState != ReadState.Interactive)
			{
				return;
			}
			this.MoveToElement();
			if (this.IsElementNode(this.internalNodeType))
			{
				int num = 1;
				while (num != 0)
				{
					if (!this.Read())
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid XML while deserializing extension data.")));
					}
					if (this.IsElementNode(this.internalNodeType))
					{
						num++;
					}
					else if (this.internalNodeType == ExtensionDataReader.ExtensionDataNodeType.EndElement)
					{
						this.ReadEndElement();
						num--;
					}
				}
				return;
			}
			this.Read();
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000336BF File Offset: 0x000318BF
		private bool IsElementNode(ExtensionDataReader.ExtensionDataNodeType nodeType)
		{
			return nodeType == ExtensionDataReader.ExtensionDataNodeType.Element || nodeType == ExtensionDataReader.ExtensionDataNodeType.ReferencedElement || nodeType == ExtensionDataReader.ExtensionDataNodeType.NullElement;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000336CF File Offset: 0x000318CF
		public override void Close()
		{
			if (this.IsXmlDataNode)
			{
				this.xmlNodeReader.Close();
				return;
			}
			this.Reset();
			this.readState = ReadState.Closed;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x000336F4 File Offset: 0x000318F4
		public override bool Read()
		{
			if (this.nodeType == XmlNodeType.Attribute && this.MoveToNextAttribute())
			{
				return true;
			}
			this.MoveNext(this.element.dataNode);
			switch (this.internalNodeType)
			{
			case ExtensionDataReader.ExtensionDataNodeType.None:
				if (this.depth != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid XML while deserializing extension data.")));
				}
				this.nodeType = XmlNodeType.None;
				this.prefix = string.Empty;
				this.ns = string.Empty;
				this.localName = string.Empty;
				this.value = string.Empty;
				this.attributeCount = 0;
				this.readState = ReadState.EndOfFile;
				return false;
			case ExtensionDataReader.ExtensionDataNodeType.Element:
			case ExtensionDataReader.ExtensionDataNodeType.ReferencedElement:
			case ExtensionDataReader.ExtensionDataNodeType.NullElement:
				this.PushElement();
				this.SetElement();
				break;
			case ExtensionDataReader.ExtensionDataNodeType.EndElement:
				this.nodeType = XmlNodeType.EndElement;
				this.prefix = string.Empty;
				this.ns = string.Empty;
				this.localName = string.Empty;
				this.value = string.Empty;
				this.attributeCount = 0;
				this.attributeIndex = -1;
				this.PopElement();
				break;
			case ExtensionDataReader.ExtensionDataNodeType.Text:
				this.nodeType = XmlNodeType.Text;
				this.prefix = string.Empty;
				this.ns = string.Empty;
				this.localName = string.Empty;
				this.attributeCount = 0;
				this.attributeIndex = -1;
				break;
			case ExtensionDataReader.ExtensionDataNodeType.Xml:
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("Invalid state in extension data reader.")));
			}
			this.readState = ReadState.Interactive;
			return true;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00033866 File Offset: 0x00031A66
		public override string Name
		{
			get
			{
				if (this.IsXmlDataNode)
				{
					return this.xmlNodeReader.Name;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00033881 File Offset: 0x00031A81
		public override bool HasValue
		{
			get
			{
				return this.IsXmlDataNode && this.xmlNodeReader.HasValue;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00033898 File Offset: 0x00031A98
		public override string BaseURI
		{
			get
			{
				if (this.IsXmlDataNode)
				{
					return this.xmlNodeReader.BaseURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x000338B3 File Offset: 0x00031AB3
		public override XmlNameTable NameTable
		{
			get
			{
				if (this.IsXmlDataNode)
				{
					return this.xmlNodeReader.NameTable;
				}
				return null;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000338CA File Offset: 0x00031ACA
		public override string GetAttribute(string name)
		{
			if (this.IsXmlDataNode)
			{
				return this.xmlNodeReader.GetAttribute(name);
			}
			return null;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000338E2 File Offset: 0x00031AE2
		public override string GetAttribute(int i)
		{
			if (this.IsXmlDataNode)
			{
				return this.xmlNodeReader.GetAttribute(i);
			}
			return null;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000338FA File Offset: 0x00031AFA
		public override bool MoveToAttribute(string name)
		{
			return this.IsXmlDataNode && this.xmlNodeReader.MoveToAttribute(name);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00033912 File Offset: 0x00031B12
		public override void ResolveEntity()
		{
			if (this.IsXmlDataNode)
			{
				this.xmlNodeReader.ResolveEntity();
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00033927 File Offset: 0x00031B27
		public override bool ReadAttributeValue()
		{
			return this.IsXmlDataNode && this.xmlNodeReader.ReadAttributeValue();
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00033940 File Offset: 0x00031B40
		private void MoveNext(IDataNode dataNode)
		{
			ExtensionDataReader.ExtensionDataNodeType extensionDataNodeType = this.internalNodeType;
			if (extensionDataNodeType == ExtensionDataReader.ExtensionDataNodeType.Text || extensionDataNodeType - ExtensionDataReader.ExtensionDataNodeType.ReferencedElement <= 1)
			{
				this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.EndElement;
				return;
			}
			Type dataType = dataNode.DataType;
			if (dataType == Globals.TypeOfClassDataNode)
			{
				this.MoveNextInClass((ClassDataNode)dataNode);
				return;
			}
			if (dataType == Globals.TypeOfCollectionDataNode)
			{
				this.MoveNextInCollection((CollectionDataNode)dataNode);
				return;
			}
			if (dataType == Globals.TypeOfISerializableDataNode)
			{
				this.MoveNextInISerializable((ISerializableDataNode)dataNode);
				return;
			}
			if (dataType == Globals.TypeOfXmlDataNode)
			{
				this.MoveNextInXml((XmlDataNode)dataNode);
				return;
			}
			if (dataNode.Value != null)
			{
				this.MoveToDeserializedObject(dataNode);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.GetString("Invalid state in extension data reader.")));
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000339FC File Offset: 0x00031BFC
		private void SetNextElement(IDataNode node, string name, string ns, string prefix)
		{
			this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.Element;
			this.nextElement = this.GetNextElement();
			this.nextElement.localName = name;
			this.nextElement.ns = ns;
			this.nextElement.prefix = prefix;
			if (node == null)
			{
				this.nextElement.attributeCount = 0;
				this.nextElement.AddAttribute("i", "http://www.w3.org/2001/XMLSchema-instance", "nil", "true");
				this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.NullElement;
				return;
			}
			if (!this.CheckIfNodeHandled(node))
			{
				this.AddDeserializedDataNode(node);
				node.GetData(this.nextElement);
				if (node is XmlDataNode)
				{
					this.MoveNextInXml((XmlDataNode)node);
				}
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00033AA8 File Offset: 0x00031CA8
		private void AddDeserializedDataNode(IDataNode node)
		{
			if (node.Id != Globals.NewObjectId && (node.Value == null || !node.IsFinalValue))
			{
				if (this.deserializedDataNodes == null)
				{
					this.deserializedDataNodes = new Queue<IDataNode>();
				}
				this.deserializedDataNodes.Enqueue(node);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00033AF8 File Offset: 0x00031CF8
		private bool CheckIfNodeHandled(IDataNode node)
		{
			bool flag = false;
			if (node.Id != Globals.NewObjectId)
			{
				flag = (this.cache[node] != null);
				if (flag)
				{
					if (this.nextElement == null)
					{
						this.nextElement = this.GetNextElement();
					}
					this.nextElement.attributeCount = 0;
					this.nextElement.AddAttribute("z", "http://schemas.microsoft.com/2003/10/Serialization/", "Ref", node.Id.ToString(NumberFormatInfo.InvariantInfo));
					this.nextElement.AddAttribute("i", "http://www.w3.org/2001/XMLSchema-instance", "nil", "true");
					this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.ReferencedElement;
				}
				else
				{
					this.cache.Add(node, node);
				}
			}
			return flag;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00033BB0 File Offset: 0x00031DB0
		private void MoveNextInClass(ClassDataNode dataNode)
		{
			if (dataNode.Members != null && this.element.childElementIndex < dataNode.Members.Count)
			{
				if (this.element.childElementIndex == 0)
				{
					this.context.IncrementItemCount(-dataNode.Members.Count);
				}
				IList<ExtensionDataMember> members = dataNode.Members;
				ElementData elementData = this.element;
				int childElementIndex = elementData.childElementIndex;
				elementData.childElementIndex = childElementIndex + 1;
				ExtensionDataMember extensionDataMember = members[childElementIndex];
				this.SetNextElement(extensionDataMember.Value, extensionDataMember.Name, extensionDataMember.Namespace, ExtensionDataReader.GetPrefix(extensionDataMember.Namespace));
				return;
			}
			this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.EndElement;
			this.element.childElementIndex = 0;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00033C60 File Offset: 0x00031E60
		private void MoveNextInCollection(CollectionDataNode dataNode)
		{
			if (dataNode.Items != null && this.element.childElementIndex < dataNode.Items.Count)
			{
				if (this.element.childElementIndex == 0)
				{
					this.context.IncrementItemCount(-dataNode.Items.Count);
				}
				IList<IDataNode> items = dataNode.Items;
				ElementData elementData = this.element;
				int childElementIndex = elementData.childElementIndex;
				elementData.childElementIndex = childElementIndex + 1;
				IDataNode node = items[childElementIndex];
				this.SetNextElement(node, dataNode.ItemName, dataNode.ItemNamespace, ExtensionDataReader.GetPrefix(dataNode.ItemNamespace));
				return;
			}
			this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.EndElement;
			this.element.childElementIndex = 0;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00033D08 File Offset: 0x00031F08
		private void MoveNextInISerializable(ISerializableDataNode dataNode)
		{
			if (dataNode.Members != null && this.element.childElementIndex < dataNode.Members.Count)
			{
				if (this.element.childElementIndex == 0)
				{
					this.context.IncrementItemCount(-dataNode.Members.Count);
				}
				IList<ISerializableDataMember> members = dataNode.Members;
				ElementData elementData = this.element;
				int childElementIndex = elementData.childElementIndex;
				elementData.childElementIndex = childElementIndex + 1;
				ISerializableDataMember serializableDataMember = members[childElementIndex];
				this.SetNextElement(serializableDataMember.Value, serializableDataMember.Name, string.Empty, string.Empty);
				return;
			}
			this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.EndElement;
			this.element.childElementIndex = 0;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00033DAC File Offset: 0x00031FAC
		private void MoveNextInXml(XmlDataNode dataNode)
		{
			if (this.IsXmlDataNode)
			{
				this.xmlNodeReader.Read();
				if (this.xmlNodeReader.Depth == 0)
				{
					this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.EndElement;
					this.xmlNodeReader = null;
					return;
				}
			}
			else
			{
				this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.Xml;
				if (this.element == null)
				{
					this.element = this.nextElement;
				}
				else
				{
					this.PushElement();
				}
				XmlNode xmlNode = XmlObjectSerializerReadContext.CreateWrapperXmlElement(dataNode.OwnerDocument, dataNode.XmlAttributes, dataNode.XmlChildNodes, this.element.prefix, this.element.localName, this.element.ns);
				for (int i = 0; i < this.element.attributeCount; i++)
				{
					AttributeData attributeData = this.element.attributes[i];
					XmlAttribute xmlAttribute = dataNode.OwnerDocument.CreateAttribute(attributeData.prefix, attributeData.localName, attributeData.ns);
					xmlAttribute.Value = attributeData.value;
					xmlNode.Attributes.Append(xmlAttribute);
				}
				this.xmlNodeReader = new XmlNodeReader(xmlNode);
				this.xmlNodeReader.Read();
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00033EBC File Offset: 0x000320BC
		private void MoveToDeserializedObject(IDataNode dataNode)
		{
			Type type = dataNode.DataType;
			bool isTypedNode = true;
			if (type == Globals.TypeOfObject)
			{
				type = dataNode.Value.GetType();
				if (type == Globals.TypeOfObject)
				{
					this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.EndElement;
					return;
				}
				isTypedNode = false;
			}
			if (this.MoveToText(type, dataNode, isTypedNode))
			{
				return;
			}
			if (dataNode.IsFinalValue)
			{
				this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.EndElement;
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid data node for '{0}' type.", new object[]
			{
				DataContract.GetClrTypeFullName(type)
			})));
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00033F44 File Offset: 0x00032144
		private bool MoveToText(Type type, IDataNode dataNode, bool isTypedNode)
		{
			bool flag = true;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<bool>)dataNode).GetValue() : ((bool)dataNode.Value));
				goto IL_3E7;
			case TypeCode.Char:
				this.value = XmlConvert.ToString((int)(isTypedNode ? ((DataNode<char>)dataNode).GetValue() : ((char)dataNode.Value)));
				goto IL_3E7;
			case TypeCode.SByte:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<sbyte>)dataNode).GetValue() : ((sbyte)dataNode.Value));
				goto IL_3E7;
			case TypeCode.Byte:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<byte>)dataNode).GetValue() : ((byte)dataNode.Value));
				goto IL_3E7;
			case TypeCode.Int16:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<short>)dataNode).GetValue() : ((short)dataNode.Value));
				goto IL_3E7;
			case TypeCode.UInt16:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<ushort>)dataNode).GetValue() : ((ushort)dataNode.Value));
				goto IL_3E7;
			case TypeCode.Int32:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<int>)dataNode).GetValue() : ((int)dataNode.Value));
				goto IL_3E7;
			case TypeCode.UInt32:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<uint>)dataNode).GetValue() : ((uint)dataNode.Value));
				goto IL_3E7;
			case TypeCode.Int64:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<long>)dataNode).GetValue() : ((long)dataNode.Value));
				goto IL_3E7;
			case TypeCode.UInt64:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<ulong>)dataNode).GetValue() : ((ulong)dataNode.Value));
				goto IL_3E7;
			case TypeCode.Single:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<float>)dataNode).GetValue() : ((float)dataNode.Value));
				goto IL_3E7;
			case TypeCode.Double:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<double>)dataNode).GetValue() : ((double)dataNode.Value));
				goto IL_3E7;
			case TypeCode.Decimal:
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<decimal>)dataNode).GetValue() : ((decimal)dataNode.Value));
				goto IL_3E7;
			case TypeCode.DateTime:
				this.value = (isTypedNode ? ((DataNode<DateTime>)dataNode).GetValue() : ((DateTime)dataNode.Value)).ToString("yyyy-MM-ddTHH:mm:ss.fffffffK", DateTimeFormatInfo.InvariantInfo);
				goto IL_3E7;
			case TypeCode.String:
				this.value = (isTypedNode ? ((DataNode<string>)dataNode).GetValue() : ((string)dataNode.Value));
				goto IL_3E7;
			}
			if (type == Globals.TypeOfByteArray)
			{
				byte[] array = isTypedNode ? ((DataNode<byte[]>)dataNode).GetValue() : ((byte[])dataNode.Value);
				this.value = ((array == null) ? string.Empty : Convert.ToBase64String(array));
			}
			else if (type == Globals.TypeOfTimeSpan)
			{
				this.value = XmlConvert.ToString(isTypedNode ? ((DataNode<TimeSpan>)dataNode).GetValue() : ((TimeSpan)dataNode.Value));
			}
			else if (type == Globals.TypeOfGuid)
			{
				this.value = (isTypedNode ? ((DataNode<Guid>)dataNode).GetValue() : ((Guid)dataNode.Value)).ToString();
			}
			else if (type == Globals.TypeOfUri)
			{
				Uri uri = isTypedNode ? ((DataNode<Uri>)dataNode).GetValue() : ((Uri)dataNode.Value);
				this.value = uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);
			}
			else
			{
				flag = false;
			}
			IL_3E7:
			if (flag)
			{
				this.internalNodeType = ExtensionDataReader.ExtensionDataNodeType.Text;
			}
			return flag;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00034344 File Offset: 0x00032544
		private void PushElement()
		{
			this.GrowElementsIfNeeded();
			ElementData[] array = this.elements;
			int num = this.depth;
			this.depth = num + 1;
			array[num] = this.element;
			if (this.nextElement == null)
			{
				this.element = this.GetNextElement();
				return;
			}
			this.element = this.nextElement;
			this.nextElement = null;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000343A0 File Offset: 0x000325A0
		private void PopElement()
		{
			this.prefix = this.element.prefix;
			this.localName = this.element.localName;
			this.ns = this.element.ns;
			if (this.depth == 0)
			{
				return;
			}
			this.depth--;
			if (this.elements != null)
			{
				this.element = this.elements[this.depth];
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00034414 File Offset: 0x00032614
		private void GrowElementsIfNeeded()
		{
			if (this.elements == null)
			{
				this.elements = new ElementData[8];
				return;
			}
			if (this.elements.Length == this.depth)
			{
				ElementData[] destinationArray = new ElementData[this.elements.Length * 2];
				Array.Copy(this.elements, 0, destinationArray, 0, this.elements.Length);
				this.elements = destinationArray;
			}
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00034474 File Offset: 0x00032674
		private ElementData GetNextElement()
		{
			int num = this.depth + 1;
			if (this.elements != null && this.elements.Length > num && this.elements[num] != null)
			{
				return this.elements[num];
			}
			return new ElementData();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000344B8 File Offset: 0x000326B8
		[SecuritySafeCritical]
		internal static string GetPrefix(string ns)
		{
			ns = (ns ?? string.Empty);
			string result;
			if (!ExtensionDataReader.nsToPrefixTable.TryGetValue(ns, out result))
			{
				Dictionary<string, string> obj = ExtensionDataReader.nsToPrefixTable;
				lock (obj)
				{
					if (!ExtensionDataReader.nsToPrefixTable.TryGetValue(ns, out result))
					{
						result = ((ns == null || ns.Length == 0) ? string.Empty : ("p" + ExtensionDataReader.nsToPrefixTable.Count.ToString()));
						ExtensionDataReader.AddPrefix(result, ns);
					}
				}
			}
			return result;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00034554 File Offset: 0x00032754
		[SecuritySafeCritical]
		private static void AddPrefix(string prefix, string ns)
		{
			ExtensionDataReader.nsToPrefixTable.Add(ns, prefix);
			ExtensionDataReader.prefixToNsTable.Add(prefix, ns);
		}

		// Token: 0x04000533 RID: 1331
		private Hashtable cache = new Hashtable();

		// Token: 0x04000534 RID: 1332
		private ElementData[] elements;

		// Token: 0x04000535 RID: 1333
		private ElementData element;

		// Token: 0x04000536 RID: 1334
		private ElementData nextElement;

		// Token: 0x04000537 RID: 1335
		private ReadState readState;

		// Token: 0x04000538 RID: 1336
		private ExtensionDataReader.ExtensionDataNodeType internalNodeType;

		// Token: 0x04000539 RID: 1337
		private XmlNodeType nodeType;

		// Token: 0x0400053A RID: 1338
		private int depth;

		// Token: 0x0400053B RID: 1339
		private string localName;

		// Token: 0x0400053C RID: 1340
		private string ns;

		// Token: 0x0400053D RID: 1341
		private string prefix;

		// Token: 0x0400053E RID: 1342
		private string value;

		// Token: 0x0400053F RID: 1343
		private int attributeCount;

		// Token: 0x04000540 RID: 1344
		private int attributeIndex;

		// Token: 0x04000541 RID: 1345
		private XmlNodeReader xmlNodeReader;

		// Token: 0x04000542 RID: 1346
		private Queue<IDataNode> deserializedDataNodes;

		// Token: 0x04000543 RID: 1347
		private XmlObjectSerializerReadContext context;

		// Token: 0x04000544 RID: 1348
		[SecurityCritical]
		private static Dictionary<string, string> nsToPrefixTable = new Dictionary<string, string>();

		// Token: 0x04000545 RID: 1349
		[SecurityCritical]
		private static Dictionary<string, string> prefixToNsTable = new Dictionary<string, string>();

		// Token: 0x020000DE RID: 222
		private enum ExtensionDataNodeType
		{
			// Token: 0x04000547 RID: 1351
			None,
			// Token: 0x04000548 RID: 1352
			Element,
			// Token: 0x04000549 RID: 1353
			EndElement,
			// Token: 0x0400054A RID: 1354
			Text,
			// Token: 0x0400054B RID: 1355
			Xml,
			// Token: 0x0400054C RID: 1356
			ReferencedElement,
			// Token: 0x0400054D RID: 1357
			NullElement
		}
	}
}
