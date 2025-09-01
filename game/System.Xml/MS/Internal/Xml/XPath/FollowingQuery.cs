using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200062F RID: 1583
	internal sealed class FollowingQuery : BaseAxisQuery
	{
		// Token: 0x060040A6 RID: 16550 RVA: 0x001635D7 File Offset: 0x001617D7
		public FollowingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x00165103 File Offset: 0x00163303
		private FollowingQuery(FollowingQuery other) : base(other)
		{
			this._input = Query.Clone(other._input);
			this._iterator = Query.Clone(other._iterator);
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0016512E File Offset: 0x0016332E
		public override void Reset()
		{
			this._iterator = null;
			base.Reset();
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x00165140 File Offset: 0x00163340
		public override XPathNavigator Advance()
		{
			if (this._iterator == null)
			{
				this._input = this.qyInput.Advance();
				if (this._input == null)
				{
					return null;
				}
				XPathNavigator xpathNavigator;
				do
				{
					xpathNavigator = this._input.Clone();
					this._input = this.qyInput.Advance();
				}
				while (xpathNavigator.IsDescendant(this._input));
				this._input = xpathNavigator;
				this._iterator = XPathEmptyIterator.Instance;
			}
			while (!this._iterator.MoveNext())
			{
				bool matchSelf;
				if (this._input.NodeType == XPathNodeType.Attribute || this._input.NodeType == XPathNodeType.Namespace)
				{
					this._input.MoveToParent();
					matchSelf = false;
				}
				else
				{
					while (!this._input.MoveToNext())
					{
						if (!this._input.MoveToParent())
						{
							return null;
						}
					}
					matchSelf = true;
				}
				if (base.NameTest)
				{
					this._iterator = this._input.SelectDescendants(base.Name, base.Namespace, matchSelf);
				}
				else
				{
					this._iterator = this._input.SelectDescendants(base.TypeTest, matchSelf);
				}
			}
			this.position++;
			this.currentNode = this._iterator.Current;
			return this.currentNode;
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x00165274 File Offset: 0x00163474
		public override XPathNodeIterator Clone()
		{
			return new FollowingQuery(this);
		}

		// Token: 0x04002E1B RID: 11803
		private XPathNavigator _input;

		// Token: 0x04002E1C RID: 11804
		private XPathNodeIterator _iterator;
	}
}
