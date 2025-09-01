using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020001A6 RID: 422
	internal abstract class DocumentXPathNodeIterator_ElemDescendants : XPathNodeIterator
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x0006555D File Offset: 0x0006375D
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNavigator nav)
		{
			this.nav = (DocumentXPathNavigator)nav.Clone();
			this.level = 0;
			this.position = 0;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00065584 File Offset: 0x00063784
		internal DocumentXPathNodeIterator_ElemDescendants(DocumentXPathNodeIterator_ElemDescendants other)
		{
			this.nav = (DocumentXPathNavigator)other.nav.Clone();
			this.level = other.level;
			this.position = other.position;
		}

		// Token: 0x06000F77 RID: 3959
		protected abstract bool Match(XmlNode node);

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000655BA File Offset: 0x000637BA
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x000655C2 File Offset: 0x000637C2
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000655CA File Offset: 0x000637CA
		protected void SetPosition(int pos)
		{
			this.position = pos;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000655D4 File Offset: 0x000637D4
		public override bool MoveNext()
		{
			for (;;)
			{
				if (this.nav.MoveToFirstChild())
				{
					this.level++;
				}
				else
				{
					if (this.level == 0)
					{
						break;
					}
					while (!this.nav.MoveToNext())
					{
						this.level--;
						if (this.level == 0)
						{
							return false;
						}
						if (!this.nav.MoveToParent())
						{
							return false;
						}
					}
				}
				XmlNode xmlNode = (XmlNode)this.nav.UnderlyingObject;
				if (xmlNode.NodeType == XmlNodeType.Element && this.Match(xmlNode))
				{
					goto Block_5;
				}
			}
			return false;
			Block_5:
			this.position++;
			return true;
		}

		// Token: 0x04000FFC RID: 4092
		private DocumentXPathNavigator nav;

		// Token: 0x04000FFD RID: 4093
		private int level;

		// Token: 0x04000FFE RID: 4094
		private int position;
	}
}
