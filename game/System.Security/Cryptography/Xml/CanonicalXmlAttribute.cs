using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200001E RID: 30
	internal class CanonicalXmlAttribute : XmlAttribute, ICanonicalizableNode
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003834 File Offset: 0x00001A34
		public CanonicalXmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState) : base(prefix, localName, namespaceURI, doc)
		{
			this.IsInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003849 File Offset: 0x00001A49
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003851 File Offset: 0x00001A51
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

		// Token: 0x06000085 RID: 133 RVA: 0x0000385A File Offset: 0x00001A5A
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			strBuilder.Append(" " + this.Name + "=\"");
			strBuilder.Append(Utils.EscapeAttributeValue(this.Value));
			strBuilder.Append("\"");
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003898 File Offset: 0x00001A98
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] bytes = utf8Encoding.GetBytes(" " + this.Name + "=\"");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes(Utils.EscapeAttributeValue(this.Value));
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes("\"");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
		}

		// Token: 0x04000143 RID: 323
		private bool _isInNodeSet;
	}
}
