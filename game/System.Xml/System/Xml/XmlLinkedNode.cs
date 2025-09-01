using System;

namespace System.Xml
{
	/// <summary>Gets the node immediately preceding or following this node.</summary>
	// Token: 0x020001C9 RID: 457
	public abstract class XmlLinkedNode : XmlNode
	{
		// Token: 0x0600118A RID: 4490 RVA: 0x0006AD9F File Offset: 0x00068F9F
		internal XmlLinkedNode()
		{
			this.next = null;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0006ADAE File Offset: 0x00068FAE
		internal XmlLinkedNode(XmlDocument doc) : base(doc)
		{
			this.next = null;
		}

		/// <summary>Gets the node immediately preceding this node.</summary>
		/// <returns>The preceding <see cref="T:System.Xml.XmlNode" /> or <see langword="null" /> if one does not exist.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0006ADC0 File Offset: 0x00068FC0
		public override XmlNode PreviousSibling
		{
			get
			{
				XmlNode parentNode = this.ParentNode;
				if (parentNode != null)
				{
					XmlNode xmlNode;
					XmlNode nextSibling;
					for (xmlNode = parentNode.FirstChild; xmlNode != null; xmlNode = nextSibling)
					{
						nextSibling = xmlNode.NextSibling;
						if (nextSibling == this)
						{
							break;
						}
					}
					return xmlNode;
				}
				return null;
			}
		}

		/// <summary>Gets the node immediately following this node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> immediately following this node or <see langword="null" /> if one does not exist.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0006ADF4 File Offset: 0x00068FF4
		public override XmlNode NextSibling
		{
			get
			{
				XmlNode parentNode = this.ParentNode;
				if (parentNode != null && this.next != parentNode.FirstChild)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x040010A0 RID: 4256
		internal XmlLinkedNode next;
	}
}
