using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000027 RID: 39
	internal class CanonicalXmlText : XmlText, ICanonicalizableNode
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x000045CE File Offset: 0x000027CE
		public CanonicalXmlText(string strData, XmlDocument doc, bool defaultNodeSetInclusionState) : base(strData, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000045DF File Offset: 0x000027DF
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000045E7 File Offset: 0x000027E7
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

		// Token: 0x060000CA RID: 202 RVA: 0x000045F0 File Offset: 0x000027F0
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeTextData(this.Value));
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000460C File Offset: 0x0000280C
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				byte[] bytes = new UTF8Encoding(false).GetBytes(Utils.EscapeTextData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x0400014F RID: 335
		private bool _isInNodeSet;
	}
}
