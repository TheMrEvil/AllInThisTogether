using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000023 RID: 35
	internal class CanonicalXmlEntityReference : XmlEntityReference, ICanonicalizableNode
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004230 File Offset: 0x00002430
		public CanonicalXmlEntityReference(string name, XmlDocument doc, bool defaultNodeSetInclusionState) : base(name, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004241 File Offset: 0x00002441
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004249 File Offset: 0x00002449
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

		// Token: 0x060000A9 RID: 169 RVA: 0x00004252 File Offset: 0x00002452
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteGenericNode(this, strBuilder, docPos, anc);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004265 File Offset: 0x00002465
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteHashGenericNode(this, hash, docPos, anc);
			}
		}

		// Token: 0x0400014B RID: 331
		private bool _isInNodeSet;
	}
}
