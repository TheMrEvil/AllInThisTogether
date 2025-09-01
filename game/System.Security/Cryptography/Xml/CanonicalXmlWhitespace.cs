using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000028 RID: 40
	internal class CanonicalXmlWhitespace : XmlWhitespace, ICanonicalizableNode
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00004646 File Offset: 0x00002846
		public CanonicalXmlWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState) : base(strData, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004657 File Offset: 0x00002857
		// (set) Token: 0x060000CE RID: 206 RVA: 0x0000465F File Offset: 0x0000285F
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

		// Token: 0x060000CF RID: 207 RVA: 0x00004668 File Offset: 0x00002868
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(this.Value));
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004688 File Offset: 0x00002888
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				byte[] bytes = new UTF8Encoding(false).GetBytes(Utils.EscapeWhitespaceData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x04000150 RID: 336
		private bool _isInNodeSet;
	}
}
