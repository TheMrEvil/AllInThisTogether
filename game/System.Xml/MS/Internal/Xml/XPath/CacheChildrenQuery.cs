using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200061E RID: 1566
	internal sealed class CacheChildrenQuery : ChildrenQuery
	{
		// Token: 0x0600402A RID: 16426 RVA: 0x00163D76 File Offset: 0x00161F76
		public CacheChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type) : base(qyInput, name, prefix, type)
		{
			this._elementStk = new ClonableStack<XPathNavigator>();
			this._positionStk = new ClonableStack<int>();
			this._needInput = true;
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x00163DA0 File Offset: 0x00161FA0
		private CacheChildrenQuery(CacheChildrenQuery other) : base(other)
		{
			this._nextInput = Query.Clone(other._nextInput);
			this._elementStk = other._elementStk.Clone();
			this._positionStk = other._positionStk.Clone();
			this._needInput = other._needInput;
		}

		// Token: 0x0600402C RID: 16428 RVA: 0x00163DF3 File Offset: 0x00161FF3
		public override void Reset()
		{
			this._nextInput = null;
			this._elementStk.Clear();
			this._positionStk.Clear();
			this._needInput = true;
			base.Reset();
		}

		// Token: 0x0600402D RID: 16429 RVA: 0x00163E20 File Offset: 0x00162020
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this._needInput)
				{
					if (this._elementStk.Count == 0)
					{
						this.currentNode = this.GetNextInput();
						if (this.currentNode == null)
						{
							break;
						}
						if (!this.currentNode.MoveToFirstChild())
						{
							continue;
						}
						this.position = 0;
					}
					else
					{
						this.currentNode = this._elementStk.Pop();
						this.position = this._positionStk.Pop();
						if (!this.DecideNextNode())
						{
							continue;
						}
					}
					this._needInput = false;
				}
				else if (!this.currentNode.MoveToNext() || !this.DecideNextNode())
				{
					this._needInput = true;
					continue;
				}
				if (this.matches(this.currentNode))
				{
					goto Block_5;
				}
			}
			return null;
			Block_5:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x0600402E RID: 16430 RVA: 0x00163EE8 File Offset: 0x001620E8
		private bool DecideNextNode()
		{
			this._nextInput = this.GetNextInput();
			if (this._nextInput != null && Query.CompareNodes(this.currentNode, this._nextInput) == XmlNodeOrder.After)
			{
				this._elementStk.Push(this.currentNode);
				this._positionStk.Push(this.position);
				this.currentNode = this._nextInput;
				this._nextInput = null;
				if (!this.currentNode.MoveToFirstChild())
				{
					return false;
				}
				this.position = 0;
			}
			return true;
		}

		// Token: 0x0600402F RID: 16431 RVA: 0x00163F6C File Offset: 0x0016216C
		private XPathNavigator GetNextInput()
		{
			XPathNavigator xpathNavigator;
			if (this._nextInput != null)
			{
				xpathNavigator = this._nextInput;
				this._nextInput = null;
			}
			else
			{
				xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator != null)
				{
					xpathNavigator = xpathNavigator.Clone();
				}
			}
			return xpathNavigator;
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x00163FA8 File Offset: 0x001621A8
		public override XPathNodeIterator Clone()
		{
			return new CacheChildrenQuery(this);
		}

		// Token: 0x04002DFC RID: 11772
		private XPathNavigator _nextInput;

		// Token: 0x04002DFD RID: 11773
		private ClonableStack<XPathNavigator> _elementStk;

		// Token: 0x04002DFE RID: 11774
		private ClonableStack<int> _positionStk;

		// Token: 0x04002DFF RID: 11775
		private bool _needInput;
	}
}
