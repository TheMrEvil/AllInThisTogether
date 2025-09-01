using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002F3 RID: 755
	public class SelectMany : ARangeVariableQueryClause
	{
		// Token: 0x06002428 RID: 9256 RVA: 0x000AD8B7 File Offset: 0x000ABAB7
		public SelectMany(QueryBlock block, RangeVariable identifier, Expression expr, Location loc) : base(block, identifier, expr, loc)
		{
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000AD93C File Offset: 0x000ABB3C
		protected override void CreateArguments(ResolveContext ec, Parameter parameter, ref Arguments args)
		{
			if (args == null)
			{
				if (base.IdentifierType != null)
				{
					this.expr = base.CreateCastExpression(this.expr);
				}
				base.CreateArguments(ec, parameter.Clone(), ref args);
			}
			RangeVariable intoVariable = this.GetIntoVariable();
			ImplicitLambdaParameter second = new ImplicitLambdaParameter(intoVariable.Name, intoVariable.Location);
			Expression expr;
			QueryBlock queryBlock;
			if (this.next is Select)
			{
				expr = this.next.Expr;
				queryBlock = this.next.block;
				queryBlock.SetParameters(parameter, second);
				this.next = this.next.next;
			}
			else
			{
				RangeVariable rangeVariable = intoVariable;
				expr = ARangeVariableQueryClause.CreateRangeVariableType(ec, parameter, rangeVariable, new SimpleName(rangeVariable.Name, intoVariable.Location));
				queryBlock = new QueryBlock(this.block.Parent, this.block.StartLocation);
				queryBlock.SetParameters(parameter, second);
			}
			LambdaExpression lambdaExpression = new LambdaExpression(base.Location);
			lambdaExpression.Block = queryBlock;
			lambdaExpression.Block.AddStatement(new ContextualReturn(expr));
			args.Add(new Argument(lambdaExpression));
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x000ADA41 File Offset: 0x000ABC41
		protected override string MethodName
		{
			get
			{
				return "SelectMany";
			}
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000ADA48 File Offset: 0x000ABC48
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}
	}
}
