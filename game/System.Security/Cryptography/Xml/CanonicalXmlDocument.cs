using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000021 RID: 33
	internal class CanonicalXmlDocument : XmlDocument, ICanonicalizableNode
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00003AC4 File Offset: 0x00001CC4
		public CanonicalXmlDocument(bool defaultNodeSetInclusionState, bool includeComments)
		{
			base.PreserveWhitespace = true;
			this._includeComments = includeComments;
			this._defaultNodeSetInclusionState = defaultNodeSetInclusionState;
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003AF5 File Offset: 0x00001CF5
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003AFD File Offset: 0x00001CFD
		public bool IsInNodeSet
		{
			get
			{
				return this._isInNodeSet;
			}
			set
			{
				this._isInNodeSet = value;
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B08 File Offset: 0x00001D08
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			docPos = DocPosition.BeforeRootElement;
			foreach (object obj in this.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					CanonicalizationDispatcher.Write(xmlNode, strBuilder, DocPosition.InRootElement, anc);
					docPos = DocPosition.AfterRootElement;
				}
				else
				{
					CanonicalizationDispatcher.Write(xmlNode, strBuilder, docPos, anc);
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B80 File Offset: 0x00001D80
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			docPos = DocPosition.BeforeRootElement;
			foreach (object obj in this.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					CanonicalizationDispatcher.WriteHash(xmlNode, hash, DocPosition.InRootElement, anc);
					docPos = DocPosition.AfterRootElement;
				}
				else
				{
					CanonicalizationDispatcher.WriteHash(xmlNode, hash, docPos, anc);
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlElement(prefix, localName, namespaceURI, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003C09 File Offset: 0x00001E09
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003C09 File Offset: 0x00001E09
		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003C1A File Offset: 0x00001E1A
		public override XmlText CreateTextNode(string text)
		{
			return new CanonicalXmlText(text, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003C29 File Offset: 0x00001E29
		public override XmlWhitespace CreateWhitespace(string prefix)
		{
			return new CanonicalXmlWhitespace(prefix, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003C38 File Offset: 0x00001E38
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new CanonicalXmlSignificantWhitespace(text, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003C47 File Offset: 0x00001E47
		public override XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new CanonicalXmlProcessingInstruction(target, data, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003C57 File Offset: 0x00001E57
		public override XmlComment CreateComment(string data)
		{
			return new CanonicalXmlComment(data, this, this._defaultNodeSetInclusionState, this._includeComments);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003C6C File Offset: 0x00001E6C
		public override XmlEntityReference CreateEntityReference(string name)
		{
			return new CanonicalXmlEntityReference(name, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003C7B File Offset: 0x00001E7B
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new CanonicalXmlCDataSection(data, this, this._defaultNodeSetInclusionState);
		}

		// Token: 0x04000147 RID: 327
		private bool _defaultNodeSetInclusionState;

		// Token: 0x04000148 RID: 328
		private bool _includeComments;

		// Token: 0x04000149 RID: 329
		private bool _isInNodeSet;
	}
}
