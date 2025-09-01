using System;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000118 RID: 280
	public class CompoundAssign : Assign
	{
		// Token: 0x06000DBF RID: 3519 RVA: 0x00032B90 File Offset: 0x00030D90
		public CompoundAssign(Binary.Operator op, Expression target, Expression source) : base(target, source, target.Location)
		{
			this.right = source;
			this.op = op;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00032BAE File Offset: 0x00030DAE
		public CompoundAssign(Binary.Operator op, Expression target, Expression source, Expression left) : this(op, target, source)
		{
			this.left = left;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00032BC1 File Offset: 0x00030DC1
		public Binary.Operator Operator
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00032BCC File Offset: 0x00030DCC
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.right = this.right.Resolve(ec);
			if (this.right == null)
			{
				return null;
			}
			MemberAccess memberAccess = this.target as MemberAccess;
			using (ec.Set(ResolveContext.Options.CompoundAssignmentScope))
			{
				this.target = this.target.Resolve(ec);
			}
			if (this.target == null)
			{
				return null;
			}
			if (this.target is MethodGroupExpr)
			{
				ec.Report.Error(1656, this.loc, "Cannot assign to `{0}' because it is a `{1}'", ((MethodGroupExpr)this.target).Name, this.target.ExprClassName);
				return null;
			}
			EventExpr eventExpr = this.target as EventExpr;
			if (eventExpr != null)
			{
				this.source = Convert.ImplicitConversionRequired(ec, this.right, this.target.Type, this.loc);
				if (this.source == null)
				{
					return null;
				}
				Expression right_side;
				if (this.op == Binary.Operator.Addition)
				{
					right_side = EmptyExpression.EventAddition;
				}
				else if (this.op == Binary.Operator.Subtraction)
				{
					right_side = EmptyExpression.EventSubtraction;
				}
				else
				{
					right_side = null;
				}
				this.target = this.target.ResolveLValue(ec, right_side);
				if (this.target == null)
				{
					return null;
				}
				this.eclass = ExprClass.Value;
				this.type = eventExpr.Operator.ReturnType;
				return this;
			}
			else
			{
				if (this.left == null)
				{
					this.left = new CompoundAssign.TargetExpression(this.target);
				}
				this.source = new Binary(this.op, this.left, this.right, true);
				if (this.target is DynamicMemberAssignable)
				{
					Arguments arguments = ((DynamicMemberAssignable)this.target).Arguments;
					this.source = this.source.Resolve(ec);
					Arguments arguments2 = new Arguments(arguments.Count + 1);
					arguments2.AddRange(arguments);
					arguments2.Add(new Argument(this.source));
					CSharpBinderFlags csharpBinderFlags = CSharpBinderFlags.ValueFromCompoundAssignment;
					if (ec.HasSet(ResolveContext.Options.CheckedScope))
					{
						csharpBinderFlags |= CSharpBinderFlags.CheckedContext;
					}
					if (this.target is DynamicMemberBinder)
					{
						this.source = new DynamicMemberBinder(memberAccess.Name, csharpBinderFlags, arguments2, this.loc).Resolve(ec);
						if (this.op == Binary.Operator.Addition || this.op == Binary.Operator.Subtraction)
						{
							arguments2 = new Arguments(arguments.Count + 1);
							arguments2.AddRange(arguments);
							arguments2.Add(new Argument(this.right));
							string str = (this.op == Binary.Operator.Addition) ? "add_" : "remove_";
							Expression expression = DynamicInvocation.CreateSpecialNameInvoke(new MemberAccess(this.right, str + memberAccess.Name, this.loc), arguments2, this.loc).Resolve(ec);
							arguments2 = new Arguments(arguments.Count);
							arguments2.AddRange(arguments);
							this.source = new DynamicEventCompoundAssign(memberAccess.Name, arguments2, (ExpressionStatement)this.source, (ExpressionStatement)expression, this.loc).Resolve(ec);
						}
					}
					else
					{
						this.source = new DynamicIndexBinder(csharpBinderFlags, arguments2, this.loc).Resolve(ec);
					}
					return this.source;
				}
				return base.DoResolve(ec);
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00032F14 File Offset: 0x00031114
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.target.FlowAnalysis(fc);
			this.source.FlowAnalysis(fc);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00032F30 File Offset: 0x00031130
		protected override Expression ResolveConversions(ResolveContext ec)
		{
			if (this.target is RuntimeValueExpression)
			{
				return this;
			}
			TypeSpec type = this.target.Type;
			if (Convert.ImplicitConversionExists(ec, this.source, type))
			{
				this.source = Convert.ImplicitConversion(ec, this.source, type, this.loc);
				return this;
			}
			Binary binary = this.source as Binary;
			if (binary == null)
			{
				if (this.source is ReducedExpression)
				{
					binary = (((ReducedExpression)this.source).OriginalExpression as Binary);
				}
				else if (this.source is ReducedExpression.ReducedConstantExpression)
				{
					binary = (((ReducedExpression.ReducedConstantExpression)this.source).OriginalExpression as Binary);
				}
				else if (this.source is LiftedBinaryOperator)
				{
					LiftedBinaryOperator liftedBinaryOperator = (LiftedBinaryOperator)this.source;
					if (liftedBinaryOperator.UserOperator == null)
					{
						binary = liftedBinaryOperator.Binary;
					}
				}
				else if (this.source is TypeCast)
				{
					binary = (((TypeCast)this.source).Child as Binary);
				}
			}
			if (binary != null && ((binary.Oper & Binary.Operator.ShiftMask) != (Binary.Operator)0 || Convert.ImplicitConversionExists(ec, this.right, type)))
			{
				this.source = Convert.ExplicitConversion(ec, this.source, type, this.loc);
				return this;
			}
			if (this.source.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(this.source));
				return new SimpleAssign(this.target, new DynamicConversion(type, CSharpBinderFlags.ConvertExplicit, arguments, this.loc), this.loc).Resolve(ec);
			}
			this.right.Error_ValueCannotBeConverted(ec, type, false);
			return null;
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000330C8 File Offset: 0x000312C8
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			CompoundAssign compoundAssign = (CompoundAssign)t;
			compoundAssign.right = (compoundAssign.source = this.source.Clone(clonectx));
			compoundAssign.target = this.target.Clone(clonectx);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00033107 File Offset: 0x00031307
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000669 RID: 1641
		private readonly Binary.Operator op;

		// Token: 0x0400066A RID: 1642
		private Expression right;

		// Token: 0x0400066B RID: 1643
		private Expression left;

		// Token: 0x02000381 RID: 897
		public sealed class TargetExpression : Expression
		{
			// Token: 0x0600269A RID: 9882 RVA: 0x000B6C4B File Offset: 0x000B4E4B
			public TargetExpression(Expression child)
			{
				this.child = child;
				this.loc = child.Location;
			}

			// Token: 0x0600269B RID: 9883 RVA: 0x000B6C66 File Offset: 0x000B4E66
			public override bool ContainsEmitWithAwait()
			{
				return this.child.ContainsEmitWithAwait();
			}

			// Token: 0x0600269C RID: 9884 RVA: 0x0006D4AD File Offset: 0x0006B6AD
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				throw new NotSupportedException("ET");
			}

			// Token: 0x0600269D RID: 9885 RVA: 0x000B6C73 File Offset: 0x000B4E73
			protected override Expression DoResolve(ResolveContext ec)
			{
				this.type = this.child.Type;
				this.eclass = ExprClass.Value;
				return this;
			}

			// Token: 0x0600269E RID: 9886 RVA: 0x000B6C8E File Offset: 0x000B4E8E
			public override void Emit(EmitContext ec)
			{
				this.child.Emit(ec);
			}

			// Token: 0x0600269F RID: 9887 RVA: 0x000B6C9C File Offset: 0x000B4E9C
			public override Expression EmitToField(EmitContext ec)
			{
				return this.child.EmitToField(ec);
			}

			// Token: 0x04000F4C RID: 3916
			private readonly Expression child;
		}
	}
}
