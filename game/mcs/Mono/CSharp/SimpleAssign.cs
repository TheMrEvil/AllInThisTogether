using System;

namespace Mono.CSharp
{
	// Token: 0x02000113 RID: 275
	public class SimpleAssign : Assign
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003273F File Offset: 0x0003093F
		public SimpleAssign(Expression target, Expression source) : this(target, source, target.Location)
		{
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003274F File Offset: 0x0003094F
		public SimpleAssign(Expression target, Expression source, Location loc) : base(target, source, loc)
		{
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003275C File Offset: 0x0003095C
		private bool CheckEqualAssign(Expression t)
		{
			if (this.source is Assign)
			{
				Assign assign = (Assign)this.source;
				return t.Equals(assign.Target) || (assign is SimpleAssign && ((SimpleAssign)assign).CheckEqualAssign(t));
			}
			return t.Equals(this.source);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000327B8 File Offset: 0x000309B8
		protected override Expression DoResolve(ResolveContext ec)
		{
			Expression expression = base.DoResolve(ec);
			if (expression == null || expression != this)
			{
				return expression;
			}
			if (this.CheckEqualAssign(this.target))
			{
				ec.Report.Warning(1717, 3, this.loc, "Assignment made to same variable; did you mean to assign something else?");
			}
			return this;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00032804 File Offset: 0x00030A04
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			base.FlowAnalysis(fc);
			VariableReference variableReference = this.target as VariableReference;
			if (variableReference != null)
			{
				if (variableReference.VariableInfo != null)
				{
					fc.SetVariableAssigned(variableReference.VariableInfo, false);
				}
				return;
			}
			FieldExpr fieldExpr = this.target as FieldExpr;
			if (fieldExpr != null)
			{
				fieldExpr.SetFieldAssigned(fc);
				return;
			}
			PropertyExpr propertyExpr = this.target as PropertyExpr;
			if (propertyExpr != null)
			{
				propertyExpr.SetBackingFieldAssigned(fc);
				return;
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0003286C File Offset: 0x00030A6C
		public override void MarkReachable(Reachability rc)
		{
			ExpressionStatement expressionStatement = this.source as ExpressionStatement;
			if (expressionStatement != null)
			{
				expressionStatement.MarkReachable(rc);
			}
		}
	}
}
