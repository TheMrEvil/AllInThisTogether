using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000022 RID: 34
	internal class CanonicalXmlElement : XmlElement, ICanonicalizableNode
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003C8A File Offset: 0x00001E8A
		public CanonicalXmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState) : base(prefix, localName, namespaceURI, doc)
		{
			this._isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003C9F File Offset: 0x00001E9F
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003CA7 File Offset: 0x00001EA7
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

		// Token: 0x060000A4 RID: 164 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			Hashtable nsLocallyDeclared = new Hashtable();
			SortedList sortedList = new SortedList(new NamespaceSortOrder());
			SortedList sortedList2 = new SortedList(new AttributeSortOrder());
			XmlAttributeCollection attributes = this.Attributes;
			if (attributes != null)
			{
				foreach (object obj in attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (((CanonicalXmlAttribute)xmlAttribute).IsInNodeSet || Utils.IsNamespaceNode(xmlAttribute) || Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						if (Utils.IsNamespaceNode(xmlAttribute))
						{
							anc.TrackNamespaceNode(xmlAttribute, sortedList, nsLocallyDeclared);
						}
						else if (Utils.IsXmlNamespaceNode(xmlAttribute))
						{
							anc.TrackXmlNamespaceNode(xmlAttribute, sortedList, sortedList2, nsLocallyDeclared);
						}
						else if (this.IsInNodeSet)
						{
							sortedList2.Add(xmlAttribute, null);
						}
					}
				}
			}
			if (!Utils.IsCommittedNamespace(this, this.Prefix, this.NamespaceURI))
			{
				string name = (this.Prefix.Length > 0) ? ("xmlns:" + this.Prefix) : "xmlns";
				XmlAttribute xmlAttribute2 = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute2.Value = this.NamespaceURI;
				anc.TrackNamespaceNode(xmlAttribute2, sortedList, nsLocallyDeclared);
			}
			if (this.IsInNodeSet)
			{
				anc.GetNamespacesToRender(this, sortedList2, sortedList, nsLocallyDeclared);
				strBuilder.Append("<" + this.Name);
				foreach (object obj2 in sortedList.GetKeyList())
				{
					(obj2 as CanonicalXmlAttribute).Write(strBuilder, docPos, anc);
				}
				foreach (object obj3 in sortedList2.GetKeyList())
				{
					(obj3 as CanonicalXmlAttribute).Write(strBuilder, docPos, anc);
				}
				strBuilder.Append(">");
			}
			anc.EnterElementContext();
			anc.LoadUnrenderedNamespaces(nsLocallyDeclared);
			anc.LoadRenderedNamespaces(sortedList);
			foreach (object obj4 in this.ChildNodes)
			{
				CanonicalizationDispatcher.Write((XmlNode)obj4, strBuilder, docPos, anc);
			}
			anc.ExitElementContext();
			if (this.IsInNodeSet)
			{
				strBuilder.Append("</" + this.Name + ">");
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003F50 File Offset: 0x00002150
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			Hashtable nsLocallyDeclared = new Hashtable();
			SortedList sortedList = new SortedList(new NamespaceSortOrder());
			SortedList sortedList2 = new SortedList(new AttributeSortOrder());
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			XmlAttributeCollection attributes = this.Attributes;
			if (attributes != null)
			{
				foreach (object obj in attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (((CanonicalXmlAttribute)xmlAttribute).IsInNodeSet || Utils.IsNamespaceNode(xmlAttribute) || Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						if (Utils.IsNamespaceNode(xmlAttribute))
						{
							anc.TrackNamespaceNode(xmlAttribute, sortedList, nsLocallyDeclared);
						}
						else if (Utils.IsXmlNamespaceNode(xmlAttribute))
						{
							anc.TrackXmlNamespaceNode(xmlAttribute, sortedList, sortedList2, nsLocallyDeclared);
						}
						else if (this.IsInNodeSet)
						{
							sortedList2.Add(xmlAttribute, null);
						}
					}
				}
			}
			if (!Utils.IsCommittedNamespace(this, this.Prefix, this.NamespaceURI))
			{
				string name = (this.Prefix.Length > 0) ? ("xmlns:" + this.Prefix) : "xmlns";
				XmlAttribute xmlAttribute2 = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute2.Value = this.NamespaceURI;
				anc.TrackNamespaceNode(xmlAttribute2, sortedList, nsLocallyDeclared);
			}
			if (this.IsInNodeSet)
			{
				anc.GetNamespacesToRender(this, sortedList2, sortedList, nsLocallyDeclared);
				byte[] bytes = utf8Encoding.GetBytes("<" + this.Name);
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				foreach (object obj2 in sortedList.GetKeyList())
				{
					(obj2 as CanonicalXmlAttribute).WriteHash(hash, docPos, anc);
				}
				foreach (object obj3 in sortedList2.GetKeyList())
				{
					(obj3 as CanonicalXmlAttribute).WriteHash(hash, docPos, anc);
				}
				bytes = utf8Encoding.GetBytes(">");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			anc.EnterElementContext();
			anc.LoadUnrenderedNamespaces(nsLocallyDeclared);
			anc.LoadRenderedNamespaces(sortedList);
			foreach (object obj4 in this.ChildNodes)
			{
				CanonicalizationDispatcher.WriteHash((XmlNode)obj4, hash, docPos, anc);
			}
			anc.ExitElementContext();
			if (this.IsInNodeSet)
			{
				byte[] bytes = utf8Encoding.GetBytes("</" + this.Name + ">");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x0400014A RID: 330
		private bool _isInNodeSet;
	}
}
