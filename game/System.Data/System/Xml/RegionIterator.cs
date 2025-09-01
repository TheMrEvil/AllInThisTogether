using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200007A RID: 122
	internal sealed class RegionIterator : BaseRegionIterator
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0001231F File Offset: 0x0001051F
		internal RegionIterator(XmlBoundElement rowElement) : base(((XmlDataDocument)rowElement.OwnerDocument).Mapper)
		{
			this._rowElement = rowElement;
			this._currentNode = rowElement;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00012345 File Offset: 0x00010545
		internal override XmlNode CurrentNode
		{
			get
			{
				return this._currentNode;
			}
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00012350 File Offset: 0x00010550
		internal override bool Next()
		{
			ElementState elementState = this._rowElement.ElementState;
			XmlNode firstChild = this._currentNode.FirstChild;
			if (firstChild != null)
			{
				this._currentNode = firstChild;
				this._rowElement.ElementState = elementState;
				return true;
			}
			return this.NextRight();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00012394 File Offset: 0x00010594
		internal override bool NextRight()
		{
			if (this._currentNode == this._rowElement)
			{
				this._currentNode = null;
				return false;
			}
			ElementState elementState = this._rowElement.ElementState;
			XmlNode xmlNode = this._currentNode.NextSibling;
			if (xmlNode != null)
			{
				this._currentNode = xmlNode;
				this._rowElement.ElementState = elementState;
				return true;
			}
			xmlNode = this._currentNode;
			while (xmlNode != this._rowElement && xmlNode.NextSibling == null)
			{
				xmlNode = xmlNode.ParentNode;
			}
			if (xmlNode == this._rowElement)
			{
				this._currentNode = null;
				this._rowElement.ElementState = elementState;
				return false;
			}
			this._currentNode = xmlNode.NextSibling;
			this._rowElement.ElementState = elementState;
			return true;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00012440 File Offset: 0x00010640
		internal bool NextInitialTextLikeNodes(out string value)
		{
			ElementState elementState = this._rowElement.ElementState;
			XmlNode firstChild = this.CurrentNode.FirstChild;
			value = RegionIterator.GetInitialTextFromNodes(ref firstChild);
			if (firstChild == null)
			{
				this._rowElement.ElementState = elementState;
				return this.NextRight();
			}
			this._currentNode = firstChild;
			this._rowElement.ElementState = elementState;
			return true;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00012498 File Offset: 0x00010698
		private static string GetInitialTextFromNodes(ref XmlNode n)
		{
			string text = null;
			if (n != null)
			{
				while (n.NodeType == XmlNodeType.Whitespace)
				{
					n = n.NextSibling;
					if (n == null)
					{
						return string.Empty;
					}
				}
				if (XmlDataDocument.IsTextLikeNode(n) && (n.NextSibling == null || !XmlDataDocument.IsTextLikeNode(n.NextSibling)))
				{
					text = n.Value;
					n = n.NextSibling;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (n != null && XmlDataDocument.IsTextLikeNode(n))
					{
						if (n.NodeType != XmlNodeType.Whitespace)
						{
							stringBuilder.Append(n.Value);
						}
						n = n.NextSibling;
					}
					text = stringBuilder.ToString();
				}
			}
			return text ?? string.Empty;
		}

		// Token: 0x0400063D RID: 1597
		private XmlBoundElement _rowElement;

		// Token: 0x0400063E RID: 1598
		private XmlNode _currentNode;
	}
}
