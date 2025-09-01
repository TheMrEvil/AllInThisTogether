using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002EF RID: 751
	public class Join : SelectMany
	{
		// Token: 0x06002415 RID: 9237 RVA: 0x000AD725 File Offset: 0x000AB925
		public Join(QueryBlock block, RangeVariable lt, Expression inner, QueryBlock outerSelector, QueryBlock innerSelector, Location loc) : base(block, lt, inner, loc)
		{
			this.outer_selector = outerSelector;
			this.inner_selector = innerSelector;
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x000AD742 File Offset: 0x000AB942
		public QueryBlock InnerSelector
		{
			get
			{
				return this.inner_selector;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x000AD74A File Offset: 0x000AB94A
		public QueryBlock OuterSelector
		{
			get
			{
				return this.outer_selector;
			}
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000AD754 File Offset: 0x000AB954
		protected override void CreateArguments(ResolveContext ec, Parameter parameter, ref Arguments args)
		{
			args = new Arguments(4);
			if (base.IdentifierType != null)
			{
				this.expr = base.CreateCastExpression(this.expr);
			}
			args.Add(new Argument(this.expr));
			this.outer_selector.SetParameter(parameter.Clone());
			LambdaExpression lambdaExpression = new LambdaExpression(this.outer_selector.StartLocation);
			lambdaExpression.Block = this.outer_selector;
			args.Add(new Argument(lambdaExpression));
			this.inner_selector.SetParameter(new ImplicitLambdaParameter(this.identifier.Name, this.identifier.Location));
			lambdaExpression = new LambdaExpression(this.inner_selector.StartLocation);
			lambdaExpression.Block = this.inner_selector;
			args.Add(new Argument(lambdaExpression));
			base.CreateArguments(ec, parameter, ref args);
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000AD82C File Offset: 0x000ABA2C
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
			Join join = (Join)target;
			join.inner_selector = (QueryBlock)this.inner_selector.Clone(clonectx);
			join.outer_selector = (QueryBlock)this.outer_selector.Clone(clonectx);
			base.CloneTo(clonectx, join);
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x000AD876 File Offset: 0x000ABA76
		protected override string MethodName
		{
			get
			{
				return "Join";
			}
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000AD87D File Offset: 0x000ABA7D
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000D88 RID: 3464
		private QueryBlock inner_selector;

		// Token: 0x04000D89 RID: 3465
		private QueryBlock outer_selector;
	}
}
