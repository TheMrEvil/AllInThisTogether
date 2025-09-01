using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200001F RID: 31
	internal class CanonicalXmlCDataSection : XmlCDataSection, ICanonicalizableNode
	{
		// Token: 0x06000087 RID: 135 RVA: 0x0000390E File Offset: 0x00001B0E
		public CanonicalXmlCDataSection(string data, XmlDocument doc, bool defaultNodeSetInclusionState) : base(data, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000391F File Offset: 0x00001B1F
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003927 File Offset: 0x00001B27
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

		// Token: 0x0600008A RID: 138 RVA: 0x00003930 File Offset: 0x00001B30
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeCData(this.Data));
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000394C File Offset: 0x00001B4C
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				byte[] bytes = new UTF8Encoding(false).GetBytes(Utils.EscapeCData(this.Data));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x04000144 RID: 324
		private bool _isInNodeSet;
	}
}
