using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200062E RID: 1582
	internal sealed class FollSiblingQuery : BaseAxisQuery
	{
		// Token: 0x0600409F RID: 16543 RVA: 0x00164EEA File Offset: 0x001630EA
		public FollSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
			this._elementStk = new ClonableStack<XPathNavigator>();
			this._parentStk = new List<XPathNavigator>();
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x00164F0D File Offset: 0x0016310D
		private FollSiblingQuery(FollSiblingQuery other) : base(other)
		{
			this._elementStk = other._elementStk.Clone();
			this._parentStk = new List<XPathNavigator>(other._parentStk);
			this._nextInput = Query.Clone(other._nextInput);
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x00164F49 File Offset: 0x00163149
		public override void Reset()
		{
			this._elementStk.Clear();
			this._parentStk.Clear();
			this._nextInput = null;
			base.Reset();
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x00164F70 File Offset: 0x00163170
		private bool Visited(XPathNavigator nav)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToParent();
			for (int i = 0; i < this._parentStk.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(this._parentStk[i]))
				{
					return true;
				}
			}
			this._parentStk.Add(xpathNavigator);
			return false;
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x00164FC4 File Offset: 0x001631C4
		private XPathNavigator FetchInput()
		{
			XPathNavigator xpathNavigator;
			for (;;)
			{
				xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					break;
				}
				if (!this.Visited(xpathNavigator))
				{
					goto Block_1;
				}
			}
			return null;
			Block_1:
			return xpathNavigator.Clone();
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x00164FF4 File Offset: 0x001631F4
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this.currentNode == null)
				{
					if (this._nextInput == null)
					{
						this._nextInput = this.FetchInput();
					}
					if (this._elementStk.Count == 0)
					{
						if (this._nextInput == null)
						{
							break;
						}
						this.currentNode = this._nextInput;
						this._nextInput = this.FetchInput();
					}
					else
					{
						this.currentNode = this._elementStk.Pop();
					}
				}
				while (this.currentNode.IsDescendant(this._nextInput))
				{
					this._elementStk.Push(this.currentNode);
					this.currentNode = this._nextInput;
					this._nextInput = this.qyInput.Advance();
					if (this._nextInput != null)
					{
						this._nextInput = this._nextInput.Clone();
					}
				}
				while (this.currentNode.MoveToNext())
				{
					if (this.matches(this.currentNode))
					{
						goto Block_6;
					}
				}
				this.currentNode = null;
			}
			return null;
			Block_6:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x001650FB File Offset: 0x001632FB
		public override XPathNodeIterator Clone()
		{
			return new FollSiblingQuery(this);
		}

		// Token: 0x04002E18 RID: 11800
		private ClonableStack<XPathNavigator> _elementStk;

		// Token: 0x04002E19 RID: 11801
		private List<XPathNavigator> _parentStk;

		// Token: 0x04002E1A RID: 11802
		private XPathNavigator _nextInput;
	}
}
