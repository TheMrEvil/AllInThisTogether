using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200063C RID: 1596
	internal sealed class NamespaceQuery : BaseAxisQuery
	{
		// Token: 0x0600410C RID: 16652 RVA: 0x001635D7 File Offset: 0x001617D7
		public NamespaceQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type) : base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x001662CF File Offset: 0x001644CF
		private NamespaceQuery(NamespaceQuery other) : base(other)
		{
			this._onNamespace = other._onNamespace;
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x001662E4 File Offset: 0x001644E4
		public override void Reset()
		{
			this._onNamespace = false;
			base.Reset();
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x001662F4 File Offset: 0x001644F4
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (!this._onNamespace)
				{
					this.currentNode = this.qyInput.Advance();
					if (this.currentNode == null)
					{
						break;
					}
					this.position = 0;
					this.currentNode = this.currentNode.Clone();
					this._onNamespace = this.currentNode.MoveToFirstNamespace();
				}
				else
				{
					this._onNamespace = this.currentNode.MoveToNextNamespace();
				}
				if (this._onNamespace && this.matches(this.currentNode))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x0016638A File Offset: 0x0016458A
		public override bool matches(XPathNavigator e)
		{
			return e.Value.Length != 0 && (!base.NameTest || base.Name.Equals(e.LocalName));
		}

		// Token: 0x06004111 RID: 16657 RVA: 0x001663B6 File Offset: 0x001645B6
		public override XPathNodeIterator Clone()
		{
			return new NamespaceQuery(this);
		}

		// Token: 0x04002E4D RID: 11853
		private bool _onNamespace;
	}
}
