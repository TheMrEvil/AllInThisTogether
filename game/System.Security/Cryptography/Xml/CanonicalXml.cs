using System;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200001D RID: 29
	internal class CanonicalXml
	{
		// Token: 0x0600007A RID: 122 RVA: 0x0000356C File Offset: 0x0000176C
		internal CanonicalXml(Stream inputStream, bool includeComments, XmlResolver resolver, string strBaseUri)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			this._c14nDoc = new CanonicalXmlDocument(true, includeComments);
			this._c14nDoc.XmlResolver = resolver;
			this._c14nDoc.Load(Utils.PreProcessStreamInput(inputStream, resolver, strBaseUri));
			this._ancMgr = new C14NAncestralNamespaceContextManager();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000035C5 File Offset: 0x000017C5
		internal CanonicalXml(XmlDocument document, XmlResolver resolver) : this(document, resolver, false)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000035D0 File Offset: 0x000017D0
		internal CanonicalXml(XmlDocument document, XmlResolver resolver, bool includeComments)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this._c14nDoc = new CanonicalXmlDocument(true, includeComments);
			this._c14nDoc.XmlResolver = resolver;
			this._c14nDoc.Load(new XmlNodeReader(document));
			this._ancMgr = new C14NAncestralNamespaceContextManager();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003628 File Offset: 0x00001828
		internal CanonicalXml(XmlNodeList nodeList, XmlResolver resolver, bool includeComments)
		{
			if (nodeList == null)
			{
				throw new ArgumentNullException("nodeList");
			}
			XmlDocument ownerDocument = Utils.GetOwnerDocument(nodeList);
			if (ownerDocument == null)
			{
				throw new ArgumentException("nodeList");
			}
			this._c14nDoc = new CanonicalXmlDocument(false, includeComments);
			this._c14nDoc.XmlResolver = resolver;
			this._c14nDoc.Load(new XmlNodeReader(ownerDocument));
			this._ancMgr = new C14NAncestralNamespaceContextManager();
			CanonicalXml.MarkInclusionStateForNodes(nodeList, ownerDocument, this._c14nDoc);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000036A0 File Offset: 0x000018A0
		private static void MarkNodeAsIncluded(XmlNode node)
		{
			if (node is ICanonicalizableNode)
			{
				((ICanonicalizableNode)node).IsInNodeSet = true;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000036B8 File Offset: 0x000018B8
		private static void MarkInclusionStateForNodes(XmlNodeList nodeList, XmlDocument inputRoot, XmlDocument root)
		{
			CanonicalXmlNodeList canonicalXmlNodeList = new CanonicalXmlNodeList();
			CanonicalXmlNodeList canonicalXmlNodeList2 = new CanonicalXmlNodeList();
			canonicalXmlNodeList.Add(inputRoot);
			canonicalXmlNodeList2.Add(root);
			int num = 0;
			do
			{
				XmlNode xmlNode = canonicalXmlNodeList[num];
				XmlNode xmlNode2 = canonicalXmlNodeList2[num];
				XmlNodeList childNodes = xmlNode.ChildNodes;
				XmlNodeList childNodes2 = xmlNode2.ChildNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					canonicalXmlNodeList.Add(childNodes[i]);
					canonicalXmlNodeList2.Add(childNodes2[i]);
					if (Utils.NodeInList(childNodes[i], nodeList))
					{
						CanonicalXml.MarkNodeAsIncluded(childNodes2[i]);
					}
					XmlAttributeCollection attributes = childNodes[i].Attributes;
					if (attributes != null)
					{
						for (int j = 0; j < attributes.Count; j++)
						{
							if (Utils.NodeInList(attributes[j], nodeList))
							{
								CanonicalXml.MarkNodeAsIncluded(childNodes2[i].Attributes.Item(j));
							}
						}
					}
				}
				num++;
			}
			while (num < canonicalXmlNodeList.Count);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000037C4 File Offset: 0x000019C4
		internal byte[] GetBytes()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this._c14nDoc.Write(stringBuilder, DocPosition.BeforeRootElement, this._ancMgr);
			return new UTF8Encoding(false).GetBytes(stringBuilder.ToString());
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000037FB File Offset: 0x000019FB
		internal byte[] GetDigestedBytes(HashAlgorithm hash)
		{
			this._c14nDoc.WriteHash(hash, DocPosition.BeforeRootElement, this._ancMgr);
			hash.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			byte[] result = (byte[])hash.Hash.Clone();
			hash.Initialize();
			return result;
		}

		// Token: 0x04000141 RID: 321
		private CanonicalXmlDocument _c14nDoc;

		// Token: 0x04000142 RID: 322
		private C14NAncestralNamespaceContextManager _ancMgr;
	}
}
