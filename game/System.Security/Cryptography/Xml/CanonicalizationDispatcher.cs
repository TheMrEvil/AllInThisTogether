using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000029 RID: 41
	internal class CanonicalizationDispatcher
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00002145 File Offset: 0x00000345
		private CanonicalizationDispatcher()
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000046C6 File Offset: 0x000028C6
		public static void Write(XmlNode node, StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).Write(strBuilder, docPos, anc);
				return;
			}
			CanonicalizationDispatcher.WriteGenericNode(node, strBuilder, docPos, anc);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000046E8 File Offset: 0x000028E8
		public static void WriteGenericNode(XmlNode node, StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			foreach (object obj in node.ChildNodes)
			{
				CanonicalizationDispatcher.Write((XmlNode)obj, strBuilder, docPos, anc);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004750 File Offset: 0x00002950
		public static void WriteHash(XmlNode node, HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).WriteHash(hash, docPos, anc);
				return;
			}
			CanonicalizationDispatcher.WriteHashGenericNode(node, hash, docPos, anc);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004774 File Offset: 0x00002974
		public static void WriteHashGenericNode(XmlNode node, HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			foreach (object obj in node.ChildNodes)
			{
				CanonicalizationDispatcher.WriteHash((XmlNode)obj, hash, docPos, anc);
			}
		}
	}
}
