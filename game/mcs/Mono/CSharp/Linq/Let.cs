using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F1 RID: 753
	public class Let : ARangeVariableQueryClause
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x000AD8B7 File Offset: 0x000ABAB7
		public Let(QueryBlock block, RangeVariable identifier, Expression expr, Location loc) : base(block, identifier, expr, loc)
		{
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000AD8C4 File Offset: 0x000ABAC4
		protected override void CreateArguments(ResolveContext ec, Parameter parameter, ref Arguments args)
		{
			this.expr = ARangeVariableQueryClause.CreateRangeVariableType(ec, parameter, this.identifier, this.expr);
			base.CreateArguments(ec, parameter, ref args);
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000AD8E8 File Offset: 0x000ABAE8
		protected override string MethodName
		{
			get
			{
				return "Select";
			}
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000AD8EF File Offset: 0x000ABAEF
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
