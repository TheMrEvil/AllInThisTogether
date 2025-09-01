using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x020001FD RID: 509
	public class ElementAccess : Expression
	{
		// Token: 0x06001A62 RID: 6754 RVA: 0x00081380 File Offset: 0x0007F580
		public ElementAccess(Expression e, Arguments args, Location loc)
		{
			this.Expr = e;
			this.loc = loc;
			this.Arguments = args;
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0008139D File Offset: 0x0007F59D
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x000813A5 File Offset: 0x0007F5A5
		public bool ConditionalAccess
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionalAccess>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConditionalAccess>k__BackingField = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x000813AE File Offset: 0x0007F5AE
		public override Location StartLocation
		{
			get
			{
				return this.Expr.StartLocation;
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000813BB File Offset: 0x0007F5BB
		public override bool ContainsEmitWithAwait()
		{
			return this.Expr.ContainsEmitWithAwait() || this.Arguments.ContainsEmitWithAwait();
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000813D8 File Offset: 0x0007F5D8
		private Expression CreateAccessExpression(ResolveContext ec, bool conditionalAccessReceiver)
		{
			this.Expr = this.Expr.Resolve(ec);
			if (this.Expr == null)
			{
				return null;
			}
			this.type = this.Expr.Type;
			if (this.ConditionalAccess && !Expression.IsNullPropagatingValid(this.type))
			{
				this.Error_OperatorCannotBeApplied(ec, this.loc, "?", this.type);
				return null;
			}
			if (this.type.IsArray)
			{
				return new ArrayAccess(this, this.loc)
				{
					ConditionalAccess = this.ConditionalAccess,
					ConditionalAccessReceiver = conditionalAccessReceiver
				};
			}
			if (this.type.IsPointer)
			{
				return this.Expr.MakePointerAccess(ec, this.type, this.Arguments);
			}
			FieldExpr fieldExpr = this.Expr as FieldExpr;
			if (fieldExpr != null)
			{
				FixedFieldSpec fixedFieldSpec = fieldExpr.Spec as FixedFieldSpec;
				if (fixedFieldSpec != null)
				{
					return this.Expr.MakePointerAccess(ec, fixedFieldSpec.ElementType, this.Arguments);
				}
			}
			IList<MemberSpec> list = MemberCache.FindMembers(this.type, MemberCache.IndexerNameAlias, false);
			if (list != null || this.type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				IndexerExpr indexerExpr = new IndexerExpr(list, this.type, this)
				{
					ConditionalAccess = this.ConditionalAccess
				};
				if (conditionalAccessReceiver)
				{
					indexerExpr.SetConditionalAccessReceiver();
				}
				return indexerExpr;
			}
			ElementAccess.Error_CannotApplyIndexing(ec, this.type, this.loc);
			return null;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00081528 File Offset: 0x0007F728
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			Arguments args = Arguments.CreateForExpressionTree(ec, this.Arguments, new Expression[]
			{
				this.Expr.CreateExpressionTree(ec)
			});
			return base.CreateExpressionFactoryCall(ec, "ArrayIndex", args);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00081564 File Offset: 0x0007F764
		public static void Error_CannotApplyIndexing(ResolveContext rc, TypeSpec type, Location loc)
		{
			if (type != InternalType.ErrorType)
			{
				rc.Report.Error(21, loc, "Cannot apply indexing with [] to an expression of type `{0}'", type.GetSignatureForError());
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00081587 File Offset: 0x0007F787
		public override bool HasConditionalAccess()
		{
			return this.ConditionalAccess || this.Expr.HasConditionalAccess();
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000815A0 File Offset: 0x0007F7A0
		protected override Expression DoResolve(ResolveContext rc)
		{
			Expression expression;
			if (!rc.HasSet(ResolveContext.Options.ConditionalAccessReceiver) && this.HasConditionalAccess())
			{
				using (rc.Set(ResolveContext.Options.ConditionalAccessReceiver))
				{
					expression = this.CreateAccessExpression(rc, true);
					if (expression == null)
					{
						return null;
					}
					return expression.Resolve(rc);
				}
			}
			expression = this.CreateAccessExpression(rc, false);
			if (expression == null)
			{
				return null;
			}
			return expression.Resolve(rc);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00081620 File Offset: 0x0007F820
		public override Expression DoResolveLValue(ResolveContext ec, Expression rhs)
		{
			Expression expression = this.CreateAccessExpression(ec, false);
			if (expression == null)
			{
				return null;
			}
			return expression.ResolveLValue(ec, rhs);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00081643 File Offset: 0x0007F843
		public override void Emit(EmitContext ec)
		{
			throw new Exception("Should never be reached");
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0008164F File Offset: 0x0007F84F
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.Expr.FlowAnalysis(fc);
			if (this.ConditionalAccess)
			{
				fc.BranchConditionalAccessDefiniteAssignment();
			}
			this.Arguments.FlowAnalysis(fc, null);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00081678 File Offset: 0x0007F878
		public override string GetSignatureForError()
		{
			return this.Expr.GetSignatureForError();
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00081688 File Offset: 0x0007F888
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			ElementAccess elementAccess = (ElementAccess)t;
			elementAccess.Expr = this.Expr.Clone(clonectx);
			if (this.Arguments != null)
			{
				elementAccess.Arguments = this.Arguments.Clone(clonectx);
			}
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000816C8 File Offset: 0x0007F8C8
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009EB RID: 2539
		public Arguments Arguments;

		// Token: 0x040009EC RID: 2540
		public Expression Expr;

		// Token: 0x040009ED RID: 2541
		[CompilerGenerated]
		private bool <ConditionalAccess>k__BackingField;
	}
}
