using System;

namespace Mono.CSharp
{
	// Token: 0x02000112 RID: 274
	public abstract class Assign : ExpressionStatement
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x00032510 File Offset: 0x00030710
		protected Assign(Expression target, Expression source, Location loc)
		{
			this.target = target;
			this.source = source;
			this.loc = loc;
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0003252D File Offset: 0x0003072D
		public Expression Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x00032535 File Offset: 0x00030735
		public Expression Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0003253D File Offset: 0x0003073D
		public override Location StartLocation
		{
			get
			{
				return this.target.StartLocation;
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0003254A File Offset: 0x0003074A
		public override bool ContainsEmitWithAwait()
		{
			return this.target.ContainsEmitWithAwait() || this.source.ContainsEmitWithAwait();
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00032566 File Offset: 0x00030766
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			ec.Report.Error(832, this.loc, "An expression tree cannot contain an assignment operator");
			return null;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00032584 File Offset: 0x00030784
		protected override Expression DoResolve(ResolveContext ec)
		{
			bool flag = true;
			this.source = this.source.Resolve(ec);
			if (this.source == null)
			{
				flag = false;
				this.source = ErrorExpression.Instance;
			}
			this.target = this.target.ResolveLValue(ec, this.source);
			if (this.target == null || !flag)
			{
				return null;
			}
			TypeSpec type = this.target.Type;
			TypeSpec type2 = this.source.Type;
			this.eclass = ExprClass.Value;
			this.type = type;
			if (!(this.target is IAssignMethod))
			{
				this.target.Error_ValueAssignment(ec, this.source);
				return null;
			}
			if (type != type2)
			{
				Expression expression = this.ResolveConversions(ec);
				if (expression != this)
				{
					return expression;
				}
			}
			return this;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00032639 File Offset: 0x00030839
		protected virtual Expression ResolveConversions(ResolveContext ec)
		{
			this.source = Convert.ImplicitConversionRequired(ec, this.source, this.target.Type, this.source.Location);
			if (this.source == null)
			{
				return null;
			}
			return this;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003266E File Offset: 0x0003086E
		private void Emit(EmitContext ec, bool is_statement)
		{
			((IAssignMethod)this.target).EmitAssign(ec, this.source, !is_statement, this is CompoundAssign);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00032694 File Offset: 0x00030894
		public override void Emit(EmitContext ec)
		{
			this.Emit(ec, false);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003269E File Offset: 0x0003089E
		public override void EmitStatement(EmitContext ec)
		{
			this.Emit(ec, true);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000326A8 File Offset: 0x000308A8
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.source.FlowAnalysis(fc);
			if (this.target is ArrayAccess || this.target is IndexerExpr)
			{
				this.target.FlowAnalysis(fc);
				return;
			}
			PropertyExpr propertyExpr = this.target as PropertyExpr;
			if (propertyExpr != null && !propertyExpr.IsAutoPropertyAccess)
			{
				this.target.FlowAnalysis(fc);
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003270B File Offset: 0x0003090B
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			Assign assign = (Assign)t;
			assign.target = this.target.Clone(clonectx);
			assign.source = this.source.Clone(clonectx);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00032736 File Offset: 0x00030936
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000662 RID: 1634
		protected Expression target;

		// Token: 0x04000663 RID: 1635
		protected Expression source;
	}
}
