using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002ED RID: 749
	public class QueryStartClause : ARangeVariableQueryClause
	{
		// Token: 0x0600240B RID: 9227 RVA: 0x000AD5AB File Offset: 0x000AB7AB
		public QueryStartClause(QueryBlock block, Expression expr, RangeVariable identifier, Location loc) : base(block, identifier, expr, loc)
		{
			block.AddRangeVariable(identifier);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000AD5C0 File Offset: 0x000AB7C0
		public override Expression BuildQueryClause(ResolveContext ec, Expression lSide, Parameter parameter)
		{
			if (base.IdentifierType != null)
			{
				this.expr = base.CreateCastExpression(this.expr);
			}
			if (parameter == null)
			{
				lSide = this.expr;
			}
			return this.next.BuildQueryClause(ec, lSide, new ImplicitLambdaParameter(this.identifier.Name, this.identifier.Location));
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000AD61A File Offset: 0x000AB81A
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this.BuildQueryClause(ec, null, null).Resolve(ec);
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x0000225C File Offset: 0x0000045C
		protected override string MethodName
		{
			get
			{
				throw new NotSupportedException();
			}
		}
	}
}
