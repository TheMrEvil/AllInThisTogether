using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002E9 RID: 745
	public class QueryExpression : AQueryClause
	{
		// Token: 0x060023EB RID: 9195 RVA: 0x000AD192 File Offset: 0x000AB392
		public QueryExpression(AQueryClause start) : base(null, null, start.Location)
		{
			this.next = start;
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000AD1A9 File Offset: 0x000AB3A9
		public override Expression BuildQueryClause(ResolveContext ec, Expression lSide, Parameter parentParameter)
		{
			return this.next.BuildQueryClause(ec, lSide, parentParameter);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000AD1BC File Offset: 0x000AB3BC
		protected override Expression DoResolve(ResolveContext ec)
		{
			int counter = QueryBlock.TransparentParameter.Counter;
			Expression expression = this.BuildQueryClause(ec, null, null);
			if (expression != null)
			{
				expression = expression.Resolve(ec);
			}
			if (ec.IsInProbingMode)
			{
				QueryBlock.TransparentParameter.Counter = counter;
			}
			return expression;
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x0000225C File Offset: 0x0000045C
		protected override string MethodName
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
