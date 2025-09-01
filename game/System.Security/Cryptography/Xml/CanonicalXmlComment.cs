using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000020 RID: 32
	internal class CanonicalXmlComment : XmlComment, ICanonicalizableNode
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003986 File Offset: 0x00001B86
		public CanonicalXmlComment(string comment, XmlDocument doc, bool defaultNodeSetInclusionState, bool includeComments) : base(comment, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
			this._includeComments = includeComments;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000399F File Offset: 0x00001B9F
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000039A7 File Offset: 0x00001BA7
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000039B0 File Offset: 0x00001BB0
		public bool IncludeComments
		{
			get
			{
				return this._includeComments;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000039B8 File Offset: 0x00001BB8
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet || !this.IncludeComments)
			{
				return;
			}
			if (docPos == DocPosition.AfterRootElement)
			{
				strBuilder.Append('\n');
			}
			strBuilder.Append("<!--");
			strBuilder.Append(this.Value);
			strBuilder.Append("-->");
			if (docPos == DocPosition.BeforeRootElement)
			{
				strBuilder.Append('\n');
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003A14 File Offset: 0x00001C14
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet || !this.IncludeComments)
			{
				return;
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] bytes = utf8Encoding.GetBytes("(char) 10");
			if (docPos == DocPosition.AfterRootElement)
			{
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			bytes = utf8Encoding.GetBytes("<!--");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes(this.Value);
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes("-->");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			if (docPos == DocPosition.BeforeRootElement)
			{
				bytes = utf8Encoding.GetBytes("(char) 10");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x04000145 RID: 325
		private bool _isInNodeSet;

		// Token: 0x04000146 RID: 326
		private bool _includeComments;
	}
}
