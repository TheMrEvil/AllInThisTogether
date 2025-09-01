using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000635 RID: 1589
	internal sealed class GroupQuery : BaseAxisQuery
	{
		// Token: 0x060040C5 RID: 16581 RVA: 0x00165636 File Offset: 0x00163836
		public GroupQuery(Query qy) : base(qy)
		{
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x0016563F File Offset: 0x0016383F
		private GroupQuery(GroupQuery other) : base(other)
		{
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x00165648 File Offset: 0x00163848
		public override XPathNavigator Advance()
		{
			this.currentNode = this.qyInput.Advance();
			if (this.currentNode != null)
			{
				this.position++;
			}
			return this.currentNode;
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x00165677 File Offset: 0x00163877
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.qyInput.Evaluate(nodeIterator);
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x00165685 File Offset: 0x00163885
		public override XPathNodeIterator Clone()
		{
			return new GroupQuery(this);
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x0016568D File Offset: 0x0016388D
		public override XPathResultType StaticType
		{
			get
			{
				return this.qyInput.StaticType;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x0001222F File Offset: 0x0001042F
		public override QueryProps Properties
		{
			get
			{
				return QueryProps.Position;
			}
		}
	}
}
