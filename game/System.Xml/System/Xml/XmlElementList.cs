using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020001C1 RID: 449
	internal class XmlElementList : XmlNodeList
	{
		// Token: 0x0600113F RID: 4415 RVA: 0x0006A340 File Offset: 0x00068540
		private XmlElementList(XmlNode parent)
		{
			this.rootNode = parent;
			this.curInd = -1;
			this.curElem = this.rootNode;
			this.changeCount = 0;
			this.empty = false;
			this.atomized = true;
			this.matchCount = -1;
			this.listener = new WeakReference(new XmlElementListListener(parent.Document, this));
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0006A3A0 File Offset: 0x000685A0
		~XmlElementList()
		{
			this.Dispose(false);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0006A3D0 File Offset: 0x000685D0
		internal void ConcurrencyCheck(XmlNodeChangedEventArgs args)
		{
			if (!this.atomized)
			{
				XmlNameTable nameTable = this.rootNode.Document.NameTable;
				this.localName = nameTable.Add(this.localName);
				this.namespaceURI = nameTable.Add(this.namespaceURI);
				this.atomized = true;
			}
			if (this.IsMatch(args.Node))
			{
				this.changeCount++;
				this.curInd = -1;
				this.curElem = this.rootNode;
				if (args.Action == XmlNodeChangedAction.Insert)
				{
					this.empty = false;
				}
			}
			this.matchCount = -1;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0006A468 File Offset: 0x00068668
		internal XmlElementList(XmlNode parent, string name) : this(parent)
		{
			XmlNameTable nameTable = parent.Document.NameTable;
			this.asterisk = nameTable.Add("*");
			this.name = nameTable.Add(name);
			this.localName = null;
			this.namespaceURI = null;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0006A4B4 File Offset: 0x000686B4
		internal XmlElementList(XmlNode parent, string localName, string namespaceURI) : this(parent)
		{
			XmlNameTable nameTable = parent.Document.NameTable;
			this.asterisk = nameTable.Add("*");
			this.localName = nameTable.Get(localName);
			this.namespaceURI = nameTable.Get(namespaceURI);
			if (this.localName == null || this.namespaceURI == null)
			{
				this.empty = true;
				this.atomized = false;
				this.localName = localName;
				this.namespaceURI = namespaceURI;
			}
			this.name = null;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x0006A532 File Offset: 0x00068732
		internal int ChangeCount
		{
			get
			{
				return this.changeCount;
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0006A53C File Offset: 0x0006873C
		private XmlNode NextElemInPreOrder(XmlNode curNode)
		{
			XmlNode xmlNode = curNode.FirstChild;
			if (xmlNode == null)
			{
				xmlNode = curNode;
				while (xmlNode != null && xmlNode != this.rootNode && xmlNode.NextSibling == null)
				{
					xmlNode = xmlNode.ParentNode;
				}
				if (xmlNode != null && xmlNode != this.rootNode)
				{
					xmlNode = xmlNode.NextSibling;
				}
			}
			if (xmlNode == this.rootNode)
			{
				xmlNode = null;
			}
			return xmlNode;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0006A594 File Offset: 0x00068794
		private XmlNode PrevElemInPreOrder(XmlNode curNode)
		{
			XmlNode xmlNode = curNode.PreviousSibling;
			while (xmlNode != null && xmlNode.LastChild != null)
			{
				xmlNode = xmlNode.LastChild;
			}
			if (xmlNode == null)
			{
				xmlNode = curNode.ParentNode;
			}
			if (xmlNode == this.rootNode)
			{
				xmlNode = null;
			}
			return xmlNode;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0006A5D4 File Offset: 0x000687D4
		private bool IsMatch(XmlNode curNode)
		{
			if (curNode.NodeType == XmlNodeType.Element)
			{
				if (this.name != null)
				{
					if (Ref.Equal(this.name, this.asterisk) || Ref.Equal(curNode.Name, this.name))
					{
						return true;
					}
				}
				else if ((Ref.Equal(this.localName, this.asterisk) || Ref.Equal(curNode.LocalName, this.localName)) && (Ref.Equal(this.namespaceURI, this.asterisk) || curNode.NamespaceURI == this.namespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0006A66C File Offset: 0x0006886C
		private XmlNode GetMatchingNode(XmlNode n, bool bNext)
		{
			XmlNode xmlNode = n;
			do
			{
				if (bNext)
				{
					xmlNode = this.NextElemInPreOrder(xmlNode);
				}
				else
				{
					xmlNode = this.PrevElemInPreOrder(xmlNode);
				}
			}
			while (xmlNode != null && !this.IsMatch(xmlNode));
			return xmlNode;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0006A6A0 File Offset: 0x000688A0
		private XmlNode GetNthMatchingNode(XmlNode n, bool bNext, int nCount)
		{
			XmlNode xmlNode = n;
			for (int i = 0; i < nCount; i++)
			{
				xmlNode = this.GetMatchingNode(xmlNode, bNext);
				if (xmlNode == null)
				{
					return null;
				}
			}
			return xmlNode;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0006A6CC File Offset: 0x000688CC
		public XmlNode GetNextNode(XmlNode n)
		{
			if (this.empty)
			{
				return null;
			}
			XmlNode n2 = (n == null) ? this.rootNode : n;
			return this.GetMatchingNode(n2, true);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0006A6F8 File Offset: 0x000688F8
		public override XmlNode Item(int index)
		{
			if (this.rootNode == null || index < 0)
			{
				return null;
			}
			if (this.empty)
			{
				return null;
			}
			if (this.curInd == index)
			{
				return this.curElem;
			}
			int num = index - this.curInd;
			bool bNext = num > 0;
			if (num < 0)
			{
				num = -num;
			}
			XmlNode nthMatchingNode;
			if ((nthMatchingNode = this.GetNthMatchingNode(this.curElem, bNext, num)) != null)
			{
				this.curInd = index;
				this.curElem = nthMatchingNode;
				return this.curElem;
			}
			return null;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x0006A76C File Offset: 0x0006896C
		public override int Count
		{
			get
			{
				if (this.empty)
				{
					return 0;
				}
				if (this.matchCount < 0)
				{
					int num = 0;
					int num2 = this.changeCount;
					XmlNode matchingNode = this.rootNode;
					while ((matchingNode = this.GetMatchingNode(matchingNode, true)) != null)
					{
						num++;
					}
					if (num2 != this.changeCount)
					{
						return num;
					}
					this.matchCount = num;
				}
				return this.matchCount;
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0006A7C6 File Offset: 0x000689C6
		public override IEnumerator GetEnumerator()
		{
			if (this.empty)
			{
				return new XmlEmptyElementListEnumerator(this);
			}
			return new XmlElementListEnumerator(this);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0006A7DD File Offset: 0x000689DD
		protected override void PrivateDisposeNodeList()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0006A7EC File Offset: 0x000689EC
		protected virtual void Dispose(bool disposing)
		{
			if (this.listener != null)
			{
				XmlElementListListener xmlElementListListener = (XmlElementListListener)this.listener.Target;
				if (xmlElementListListener != null)
				{
					xmlElementListListener.Unregister();
				}
				this.listener = null;
			}
		}

		// Token: 0x0400107F RID: 4223
		private string asterisk;

		// Token: 0x04001080 RID: 4224
		private int changeCount;

		// Token: 0x04001081 RID: 4225
		private string name;

		// Token: 0x04001082 RID: 4226
		private string localName;

		// Token: 0x04001083 RID: 4227
		private string namespaceURI;

		// Token: 0x04001084 RID: 4228
		private XmlNode rootNode;

		// Token: 0x04001085 RID: 4229
		private int curInd;

		// Token: 0x04001086 RID: 4230
		private XmlNode curElem;

		// Token: 0x04001087 RID: 4231
		private bool empty;

		// Token: 0x04001088 RID: 4232
		private bool atomized;

		// Token: 0x04001089 RID: 4233
		private int matchCount;

		// Token: 0x0400108A RID: 4234
		private WeakReference listener;
	}
}
