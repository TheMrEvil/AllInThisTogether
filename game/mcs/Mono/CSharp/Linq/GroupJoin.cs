using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F0 RID: 752
	public class GroupJoin : Join
	{
		// Token: 0x0600241C RID: 9244 RVA: 0x000AD886 File Offset: 0x000ABA86
		public GroupJoin(QueryBlock block, RangeVariable lt, Expression inner, QueryBlock outerSelector, QueryBlock innerSelector, RangeVariable into, Location loc) : base(block, lt, inner, outerSelector, innerSelector, loc)
		{
			this.into = into;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000AD89F File Offset: 0x000ABA9F
		protected override RangeVariable GetIntoVariable()
		{
			return this.into;
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x000AD8A7 File Offset: 0x000ABAA7
		protected override string MethodName
		{
			get
			{
				return "GroupJoin";
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000AD8AE File Offset: 0x000ABAAE
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000D8A RID: 3466
		private readonly RangeVariable into;
	}
}
