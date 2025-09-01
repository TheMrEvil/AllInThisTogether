using System;

namespace Mono.CSharp.Linq
{
	// Token: 0x020002EE RID: 750
	public class GroupBy : AQueryClause
	{
		// Token: 0x0600240F RID: 9231 RVA: 0x000AD62B File Offset: 0x000AB82B
		public GroupBy(QueryBlock block, Expression elementSelector, QueryBlock elementBlock, Expression keySelector, Location loc) : base(block, keySelector, loc)
		{
			if (!elementSelector.Equals(keySelector))
			{
				this.element_selector = elementSelector;
				this.element_block = elementBlock;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000AD650 File Offset: 0x000AB850
		public Expression SelectorExpression
		{
			get
			{
				return this.element_selector;
			}
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000AD658 File Offset: 0x000AB858
		protected override void CreateArguments(ResolveContext ec, Parameter parameter, ref Arguments args)
		{
			base.CreateArguments(ec, parameter, ref args);
			if (this.element_selector != null)
			{
				LambdaExpression lambdaExpression = new LambdaExpression(this.element_selector.Location);
				this.element_block.SetParameter(parameter.Clone());
				lambdaExpression.Block = this.element_block;
				lambdaExpression.Block.AddStatement(new ContextualReturn(this.element_selector));
				args.Add(new Argument(lambdaExpression));
			}
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000AD6C8 File Offset: 0x000AB8C8
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
			GroupBy groupBy = (GroupBy)target;
			if (this.element_selector != null)
			{
				groupBy.element_selector = this.element_selector.Clone(clonectx);
				groupBy.element_block = (QueryBlock)this.element_block.Clone(clonectx);
			}
			base.CloneTo(clonectx, groupBy);
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x000AD715 File Offset: 0x000AB915
		protected override string MethodName
		{
			get
			{
				return "GroupBy";
			}
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000AD71C File Offset: 0x000AB91C
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000D86 RID: 3462
		private Expression element_selector;

		// Token: 0x04000D87 RID: 3463
		private QueryBlock element_block;
	}
}
