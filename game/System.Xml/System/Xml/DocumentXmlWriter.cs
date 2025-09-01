using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x020001AE RID: 430
	internal sealed class DocumentXmlWriter : XmlRawWriter, IXmlNamespaceResolver
	{
		// Token: 0x06000F94 RID: 3988 RVA: 0x00065874 File Offset: 0x00063A74
		public DocumentXmlWriter(DocumentXmlWriterType type, XmlNode start, XmlDocument document)
		{
			this.type = type;
			this.start = start;
			this.document = document;
			this.state = this.StartState();
			this.fragment = new List<XmlNode>();
			this.settings = new XmlWriterSettings();
			this.settings.ReadOnly = false;
			this.settings.CheckCharacters = false;
			this.settings.CloseOutput = false;
			this.settings.ConformanceLevel = ((this.state == DocumentXmlWriter.State.Prolog) ? ConformanceLevel.Document : ConformanceLevel.Fragment);
			this.settings.ReadOnly = true;
		}

		// Token: 0x1700026D RID: 621
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x00065906 File Offset: 0x00063B06
		public XmlNamespaceManager NamespaceManager
		{
			set
			{
				this.namespaceManager = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0006590F File Offset: 0x00063B0F
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00065917 File Offset: 0x00063B17
		internal void SetSettings(XmlWriterSettings value)
		{
			this.settings = value;
		}

		// Token: 0x1700026F RID: 623
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x00065920 File Offset: 0x00063B20
		public DocumentXPathNavigator Navigator
		{
			set
			{
				this.navigator = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x00065929 File Offset: 0x00063B29
		public XmlNode EndNode
		{
			set
			{
				this.end = value;
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00065934 File Offset: 0x00063B34
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteXmlDeclaration);
			if (standalone != XmlStandalone.Omit)
			{
				XmlNode node = this.document.CreateXmlDeclaration("1.0", string.Empty, (standalone == XmlStandalone.Yes) ? "yes" : "no");
				this.AddChild(node, this.write);
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00065980 File Offset: 0x00063B80
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteXmlDeclaration);
			string version;
			string encoding;
			string standalone;
			XmlLoader.ParseXmlDeclarationValue(xmldecl, out version, out encoding, out standalone);
			XmlNode node = this.document.CreateXmlDeclaration(version, encoding, standalone);
			this.AddChild(node, this.write);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000659BC File Offset: 0x00063BBC
		public override void WriteStartDocument()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartDocument);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000659BC File Offset: 0x00063BBC
		public override void WriteStartDocument(bool standalone)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartDocument);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000659C5 File Offset: 0x00063BC5
		public override void WriteEndDocument()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndDocument);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000659D0 File Offset: 0x00063BD0
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteDocType);
			XmlNode node = this.document.CreateDocumentType(name, pubid, sysid, subset);
			this.AddChild(node, this.write);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00065A04 File Offset: 0x00063C04
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartElement);
			XmlNode node = this.document.CreateElement(prefix, localName, ns);
			this.AddChild(node, this.write);
			this.write = node;
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00065A3B File Offset: 0x00063C3B
		public override void WriteEndElement()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndElement);
			if (this.write == null)
			{
				throw new InvalidOperationException();
			}
			this.write = this.write.ParentNode;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00065A63 File Offset: 0x00063C63
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.WriteEndElement();
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00065A6C File Offset: 0x00063C6C
		public override void WriteFullEndElement()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteFullEndElement);
			XmlElement xmlElement = this.write as XmlElement;
			if (xmlElement == null)
			{
				throw new InvalidOperationException();
			}
			xmlElement.IsEmpty = false;
			this.write = xmlElement.ParentNode;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00065AA8 File Offset: 0x00063CA8
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.WriteFullEndElement();
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void StartElementContent()
		{
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00065AB0 File Offset: 0x00063CB0
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartAttribute);
			XmlAttribute attr = this.document.CreateAttribute(prefix, localName, ns);
			this.AddAttribute(attr, this.write);
			this.write = attr;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00065AE8 File Offset: 0x00063CE8
		public override void WriteEndAttribute()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndAttribute);
			XmlAttribute xmlAttribute = this.write as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException();
			}
			if (!xmlAttribute.HasChildNodes)
			{
				XmlNode node = this.document.CreateTextNode(string.Empty);
				this.AddChild(node, xmlAttribute);
			}
			this.write = xmlAttribute.OwnerElement;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000153D1 File Offset: 0x000135D1
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(ns);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00065B40 File Offset: 0x00063D40
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteStartNamespaceDeclaration);
			XmlAttribute attr;
			if (prefix.Length == 0)
			{
				attr = this.document.CreateAttribute(prefix, this.document.strXmlns, this.document.strReservedXmlns);
			}
			else
			{
				attr = this.document.CreateAttribute(this.document.strXmlns, prefix, this.document.strReservedXmlns);
			}
			this.AddAttribute(attr, this.write);
			this.write = attr;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00065BBC File Offset: 0x00063DBC
		internal override void WriteEndNamespaceDeclaration()
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEndNamespaceDeclaration);
			XmlAttribute xmlAttribute = this.write as XmlAttribute;
			if (xmlAttribute == null)
			{
				throw new InvalidOperationException();
			}
			if (!xmlAttribute.HasChildNodes)
			{
				XmlNode node = this.document.CreateTextNode(string.Empty);
				this.AddChild(node, xmlAttribute);
			}
			this.write = xmlAttribute.OwnerElement;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x00065C14 File Offset: 0x00063E14
		public override void WriteCData(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteCData);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode node = this.document.CreateCDataSection(text);
			this.AddChild(node, this.write);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00065C4C File Offset: 0x00063E4C
		public override void WriteComment(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteComment);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode node = this.document.CreateComment(text);
			this.AddChild(node, this.write);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00065C84 File Offset: 0x00063E84
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteProcessingInstruction);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode node = this.document.CreateProcessingInstruction(name, text);
			this.AddChild(node, this.write);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00065CBC File Offset: 0x00063EBC
		public override void WriteEntityRef(string name)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteEntityRef);
			XmlNode node = this.document.CreateEntityReference(name);
			this.AddChild(node, this.write);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00065CEB File Offset: 0x00063EEB
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(ch, 1));
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00065CFC File Offset: 0x00063EFC
		public override void WriteWhitespace(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteWhitespace);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			if (this.document.PreserveWhitespace)
			{
				XmlNode node = this.document.CreateWhitespace(text);
				this.AddChild(node, this.write);
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x00065D40 File Offset: 0x00063F40
		public override void WriteString(string text)
		{
			this.VerifyState(DocumentXmlWriter.Method.WriteString);
			XmlConvert.VerifyCharData(text, ExceptionType.ArgumentException);
			XmlNode node = this.document.CreateTextNode(text);
			this.AddChild(node, this.write);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00065D76 File Offset: 0x00063F76
		public override void WriteSurrogateCharEntity(char lowCh, char highCh)
		{
			this.WriteString(new string(new char[]
			{
				highCh,
				lowCh
			}));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000118AB File Offset: 0x0000FAAB
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000118AB File Offset: 0x0000FAAB
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0001DB5A File Offset: 0x0001BD5A
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0000B528 File Offset: 0x00009728
		public override void Close()
		{
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00065D94 File Offset: 0x00063F94
		internal override void Close(WriteState currentState)
		{
			if (currentState == WriteState.Error)
			{
				return;
			}
			try
			{
				switch (this.type)
				{
				case DocumentXmlWriterType.InsertSiblingAfter:
				{
					XmlNode parentNode = this.start.ParentNode;
					if (parentNode == null)
					{
						throw new InvalidOperationException(Res.GetString("The current position of the navigator is missing a valid parent."));
					}
					for (int i = this.fragment.Count - 1; i >= 0; i--)
					{
						parentNode.InsertAfter(this.fragment[i], this.start);
					}
					break;
				}
				case DocumentXmlWriterType.InsertSiblingBefore:
				{
					XmlNode parentNode = this.start.ParentNode;
					if (parentNode == null)
					{
						throw new InvalidOperationException(Res.GetString("The current position of the navigator is missing a valid parent."));
					}
					for (int j = 0; j < this.fragment.Count; j++)
					{
						parentNode.InsertBefore(this.fragment[j], this.start);
					}
					break;
				}
				case DocumentXmlWriterType.PrependChild:
					for (int k = this.fragment.Count - 1; k >= 0; k--)
					{
						this.start.PrependChild(this.fragment[k]);
					}
					break;
				case DocumentXmlWriterType.AppendChild:
					for (int l = 0; l < this.fragment.Count; l++)
					{
						this.start.AppendChild(this.fragment[l]);
					}
					break;
				case DocumentXmlWriterType.AppendAttribute:
					this.CloseWithAppendAttribute();
					break;
				case DocumentXmlWriterType.ReplaceToFollowingSibling:
					if (this.fragment.Count == 0)
					{
						throw new InvalidOperationException(Res.GetString("No content generated as the result of the operation."));
					}
					this.CloseWithReplaceToFollowingSibling();
					break;
				}
			}
			finally
			{
				this.fragment.Clear();
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00065F3C File Offset: 0x0006413C
		private void CloseWithAppendAttribute()
		{
			XmlAttributeCollection attributes = (this.start as XmlElement).Attributes;
			for (int i = 0; i < this.fragment.Count; i++)
			{
				XmlAttribute xmlAttribute = this.fragment[i] as XmlAttribute;
				int num = attributes.FindNodeOffsetNS(xmlAttribute);
				if (num != -1 && ((XmlAttribute)attributes.nodes[num]).Specified)
				{
					throw new XmlException("'{0}' is a duplicate attribute name.", (xmlAttribute.Prefix.Length == 0) ? xmlAttribute.LocalName : (xmlAttribute.Prefix + ":" + xmlAttribute.LocalName));
				}
			}
			for (int j = 0; j < this.fragment.Count; j++)
			{
				XmlAttribute node = this.fragment[j] as XmlAttribute;
				attributes.Append(node);
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00066014 File Offset: 0x00064214
		private void CloseWithReplaceToFollowingSibling()
		{
			XmlNode parentNode = this.start.ParentNode;
			if (parentNode == null)
			{
				throw new InvalidOperationException(Res.GetString("The current position of the navigator is missing a valid parent."));
			}
			if (this.start != this.end)
			{
				if (!DocumentXPathNavigator.IsFollowingSibling(this.start, this.end))
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current position of the navigator."));
				}
				if (this.start.IsReadOnly)
				{
					throw new InvalidOperationException(Res.GetString("This node is read-only. It cannot be modified."));
				}
				DocumentXPathNavigator.DeleteToFollowingSibling(this.start.NextSibling, this.end);
			}
			XmlNode xmlNode = this.fragment[0];
			parentNode.ReplaceChild(xmlNode, this.start);
			for (int i = this.fragment.Count - 1; i >= 1; i--)
			{
				parentNode.InsertAfter(this.fragment[i], xmlNode);
			}
			this.navigator.ResetPosition(xmlNode);
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0000B528 File Offset: 0x00009728
		public override void Flush()
		{
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000660F7 File Offset: 0x000642F7
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.namespaceManager.GetNamespacesInScope(scope);
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00066105 File Offset: 0x00064305
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.namespaceManager.LookupNamespace(prefix);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00066113 File Offset: 0x00064313
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.namespaceManager.LookupPrefix(namespaceName);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00066121 File Offset: 0x00064321
		private void AddAttribute(XmlAttribute attr, XmlNode parent)
		{
			if (parent == null)
			{
				this.fragment.Add(attr);
				return;
			}
			XmlElement xmlElement = parent as XmlElement;
			if (xmlElement == null)
			{
				throw new InvalidOperationException();
			}
			xmlElement.Attributes.Append(attr);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0006614E File Offset: 0x0006434E
		private void AddChild(XmlNode node, XmlNode parent)
		{
			if (parent == null)
			{
				this.fragment.Add(node);
				return;
			}
			parent.AppendChild(node);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00066168 File Offset: 0x00064368
		private DocumentXmlWriter.State StartState()
		{
			XmlNodeType xmlNodeType = XmlNodeType.None;
			switch (this.type)
			{
			case DocumentXmlWriterType.InsertSiblingAfter:
			case DocumentXmlWriterType.InsertSiblingBefore:
			{
				XmlNode parentNode = this.start.ParentNode;
				if (parentNode != null)
				{
					xmlNodeType = parentNode.NodeType;
				}
				if (xmlNodeType == XmlNodeType.Document)
				{
					return DocumentXmlWriter.State.Prolog;
				}
				if (xmlNodeType == XmlNodeType.DocumentFragment)
				{
					return DocumentXmlWriter.State.Fragment;
				}
				break;
			}
			case DocumentXmlWriterType.PrependChild:
			case DocumentXmlWriterType.AppendChild:
				xmlNodeType = this.start.NodeType;
				if (xmlNodeType == XmlNodeType.Document)
				{
					return DocumentXmlWriter.State.Prolog;
				}
				if (xmlNodeType == XmlNodeType.DocumentFragment)
				{
					return DocumentXmlWriter.State.Fragment;
				}
				break;
			case DocumentXmlWriterType.AppendAttribute:
				return DocumentXmlWriter.State.Attribute;
			}
			return DocumentXmlWriter.State.Content;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000661DF File Offset: 0x000643DF
		private void VerifyState(DocumentXmlWriter.Method method)
		{
			this.state = DocumentXmlWriter.changeState[(int)(method * DocumentXmlWriter.Method.WriteEndElement + (int)this.state)];
			if (this.state == DocumentXmlWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed or in error state."));
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0006620F File Offset: 0x0006440F
		// Note: this type is marked as 'beforefieldinit'.
		static DocumentXmlWriter()
		{
		}

		// Token: 0x04001009 RID: 4105
		private DocumentXmlWriterType type;

		// Token: 0x0400100A RID: 4106
		private XmlNode start;

		// Token: 0x0400100B RID: 4107
		private XmlDocument document;

		// Token: 0x0400100C RID: 4108
		private XmlNamespaceManager namespaceManager;

		// Token: 0x0400100D RID: 4109
		private DocumentXmlWriter.State state;

		// Token: 0x0400100E RID: 4110
		private XmlNode write;

		// Token: 0x0400100F RID: 4111
		private List<XmlNode> fragment;

		// Token: 0x04001010 RID: 4112
		private XmlWriterSettings settings;

		// Token: 0x04001011 RID: 4113
		private DocumentXPathNavigator navigator;

		// Token: 0x04001012 RID: 4114
		private XmlNode end;

		// Token: 0x04001013 RID: 4115
		private static DocumentXmlWriter.State[] changeState = new DocumentXmlWriter.State[]
		{
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Prolog,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Error,
			DocumentXmlWriter.State.Content,
			DocumentXmlWriter.State.Content
		};

		// Token: 0x020001AF RID: 431
		private enum State
		{
			// Token: 0x04001015 RID: 4117
			Error,
			// Token: 0x04001016 RID: 4118
			Attribute,
			// Token: 0x04001017 RID: 4119
			Prolog,
			// Token: 0x04001018 RID: 4120
			Fragment,
			// Token: 0x04001019 RID: 4121
			Content,
			// Token: 0x0400101A RID: 4122
			Last
		}

		// Token: 0x020001B0 RID: 432
		private enum Method
		{
			// Token: 0x0400101C RID: 4124
			WriteXmlDeclaration,
			// Token: 0x0400101D RID: 4125
			WriteStartDocument,
			// Token: 0x0400101E RID: 4126
			WriteEndDocument,
			// Token: 0x0400101F RID: 4127
			WriteDocType,
			// Token: 0x04001020 RID: 4128
			WriteStartElement,
			// Token: 0x04001021 RID: 4129
			WriteEndElement,
			// Token: 0x04001022 RID: 4130
			WriteFullEndElement,
			// Token: 0x04001023 RID: 4131
			WriteStartAttribute,
			// Token: 0x04001024 RID: 4132
			WriteEndAttribute,
			// Token: 0x04001025 RID: 4133
			WriteStartNamespaceDeclaration,
			// Token: 0x04001026 RID: 4134
			WriteEndNamespaceDeclaration,
			// Token: 0x04001027 RID: 4135
			WriteCData,
			// Token: 0x04001028 RID: 4136
			WriteComment,
			// Token: 0x04001029 RID: 4137
			WriteProcessingInstruction,
			// Token: 0x0400102A RID: 4138
			WriteEntityRef,
			// Token: 0x0400102B RID: 4139
			WriteWhitespace,
			// Token: 0x0400102C RID: 4140
			WriteString
		}
	}
}
