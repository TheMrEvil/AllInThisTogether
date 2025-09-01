using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000628 RID: 1576
	internal sealed class DescendantOverDescendantQuery : DescendantBaseQuery
	{
		// Token: 0x06004070 RID: 16496 RVA: 0x00164744 File Offset: 0x00162944
		public DescendantOverDescendantQuery(Query qyParent, bool matchSelf, string name, string prefix, XPathNodeType typeTest, bool abbrAxis) : base(qyParent, name, prefix, typeTest, matchSelf, abbrAxis)
		{
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x00164755 File Offset: 0x00162955
		private DescendantOverDescendantQuery(DescendantOverDescendantQuery other) : base(other)
		{
			this._level = other._level;
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x0016476A File Offset: 0x0016296A
		public override void Reset()
		{
			this._level = 0;
			base.Reset();
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x0016477C File Offset: 0x0016297C
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				IL_00:
				if (this._level == 0)
				{
					this.currentNode = this.qyInput.Advance();
					this.position = 0;
					if (this.currentNode == null)
					{
						break;
					}
					if (this.matchSelf && this.matches(this.currentNode))
					{
						goto Block_3;
					}
					this.currentNode = this.currentNode.Clone();
					if (!this.MoveToFirstChild())
					{
						continue;
					}
				}
				else if (!this.MoveUpUntilNext())
				{
					continue;
				}
				while (!this.matches(this.currentNode))
				{
					if (!this.MoveToFirstChild())
					{
						goto IL_00;
					}
				}
				goto Block_5;
			}
			return null;
			Block_3:
			this.position = 1;
			return this.currentNode;
			Block_5:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x00164829 File Offset: 0x00162A29
		private bool MoveToFirstChild()
		{
			if (this.currentNode.MoveToFirstChild())
			{
				this._level++;
				return true;
			}
			return false;
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x00164849 File Offset: 0x00162A49
		private bool MoveUpUntilNext()
		{
			while (!this.currentNode.MoveToNext())
			{
				this._level--;
				if (this._level == 0)
				{
					return false;
				}
				this.currentNode.MoveToParent();
			}
			return true;
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x0016487F File Offset: 0x00162A7F
		public override XPathNodeIterator Clone()
		{
			return new DescendantOverDescendantQuery(this);
		}

		// Token: 0x04002E0F RID: 11791
		private int _level;
	}
}
