using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200065C RID: 1628
	internal class XPathDescendantIterator : XPathAxisIterator
	{
		// Token: 0x060041ED RID: 16877 RVA: 0x00168805 File Offset: 0x00166A05
		public XPathDescendantIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : base(nav, type, matchSelf)
		{
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x00168810 File Offset: 0x00166A10
		public XPathDescendantIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf) : base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x00168C6D File Offset: 0x00166E6D
		public XPathDescendantIterator(XPathDescendantIterator it) : base(it)
		{
			this._level = it._level;
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x00168C82 File Offset: 0x00166E82
		public override XPathNodeIterator Clone()
		{
			return new XPathDescendantIterator(this);
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x00168C8C File Offset: 0x00166E8C
		public override bool MoveNext()
		{
			if (this.first)
			{
				this.first = false;
				if (this.matchSelf && this.Matches)
				{
					this.position = 1;
					return true;
				}
			}
			for (;;)
			{
				if (!this.nav.MoveToFirstChild())
				{
					while (this._level != 0)
					{
						if (this.nav.MoveToNext())
						{
							goto IL_78;
						}
						this.nav.MoveToParent();
						this._level--;
					}
					break;
				}
				this._level++;
				IL_78:
				if (this.Matches)
				{
					goto Block_7;
				}
			}
			return false;
			Block_7:
			this.position++;
			return true;
		}

		// Token: 0x04002EAB RID: 11947
		private int _level;
	}
}
