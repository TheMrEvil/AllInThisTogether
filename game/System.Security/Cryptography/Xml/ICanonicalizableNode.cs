using System;
using System.Text;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200003E RID: 62
	internal interface ICanonicalizableNode
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600019D RID: 413
		// (set) Token: 0x0600019E RID: 414
		bool IsInNodeSet { get; set; }

		// Token: 0x0600019F RID: 415
		void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc);

		// Token: 0x060001A0 RID: 416
		void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc);
	}
}
