using System;

namespace System.Xml
{
	// Token: 0x0200007B RID: 123
	internal sealed class TreeIterator : BaseTreeIterator
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x00012549 File Offset: 0x00010749
		internal TreeIterator(XmlNode nodeTop) : base(((XmlDataDocument)nodeTop.OwnerDocument).Mapper)
		{
			this._nodeTop = nodeTop;
			this._currentNode = nodeTop;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001256F File Offset: 0x0001076F
		internal override XmlNode CurrentNode
		{
			get
			{
				return this._currentNode;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00012578 File Offset: 0x00010778
		internal override bool Next()
		{
			XmlNode firstChild = this._currentNode.FirstChild;
			if (firstChild != null)
			{
				this._currentNode = firstChild;
				return true;
			}
			return this.NextRight();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000125A4 File Offset: 0x000107A4
		internal override bool NextRight()
		{
			if (this._currentNode == this._nodeTop)
			{
				this._currentNode = null;
				return false;
			}
			XmlNode xmlNode = this._currentNode.NextSibling;
			if (xmlNode != null)
			{
				this._currentNode = xmlNode;
				return true;
			}
			xmlNode = this._currentNode;
			while (xmlNode != this._nodeTop && xmlNode.NextSibling == null)
			{
				xmlNode = xmlNode.ParentNode;
			}
			if (xmlNode == this._nodeTop)
			{
				this._currentNode = null;
				return false;
			}
			this._currentNode = xmlNode.NextSibling;
			return true;
		}

		// Token: 0x0400063F RID: 1599
		private readonly XmlNode _nodeTop;

		// Token: 0x04000640 RID: 1600
		private XmlNode _currentNode;
	}
}
