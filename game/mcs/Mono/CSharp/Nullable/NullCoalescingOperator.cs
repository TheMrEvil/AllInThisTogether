using System;
using System.Reflection.Emit;

namespace Mono.CSharp.Nullable
{
	// Token: 0x02000304 RID: 772
	public class NullCoalescingOperator : Expression
	{
		// Token: 0x06002497 RID: 9367 RVA: 0x000AF20F File Offset: 0x000AD40F
		public NullCoalescingOperator(Expression left, Expression right)
		{
			this.left = left;
			this.right = right;
			this.loc = left.Location;
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x000AF231 File Offset: 0x000AD431
		public Expression LeftExpression
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002499 RID: 9369 RVA: 0x000AF239 File Offset: 0x000AD439
		public Expression RightExpression
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000AF244 File Offset: 0x000AD444
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			if (this.left is NullLiteral)
			{
				ec.Report.Error(845, this.loc, "An expression tree cannot contain a coalescing operator with null left side");
			}
			UserCast userCast = this.left as UserCast;
			Expression expression = null;
			if (userCast != null)
			{
				this.left = userCast.Source;
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(userCast.CreateExpressionTree(ec)));
				arguments.Add(new Argument(this.left.CreateExpressionTree(ec)));
				expression = base.CreateExpressionFactoryCall(ec, "Lambda", arguments);
			}
			Arguments arguments2 = new Arguments(3);
			arguments2.Add(new Argument(this.left.CreateExpressionTree(ec)));
			arguments2.Add(new Argument(this.right.CreateExpressionTree(ec)));
			if (expression != null)
			{
				arguments2.Add(new Argument(expression));
			}
			return base.CreateExpressionFactoryCall(ec, "Coalesce", arguments2);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000AF328 File Offset: 0x000AD528
		private Expression ConvertExpression(ResolveContext ec)
		{
			if (this.left.eclass == ExprClass.MethodGroup)
			{
				return null;
			}
			TypeSpec type = this.left.Type;
			if (type.IsNullableType)
			{
				this.unwrap = Unwrap.Create(this.left, false);
				if (this.unwrap == null)
				{
					return null;
				}
				if (this.right.IsNull)
				{
					return ReducedExpression.Create(this.left, this);
				}
				if (this.right.Type.IsNullableType)
				{
					Expression expression = (this.right.Type == type) ? this.right : Convert.ImplicitNulableConversion(ec, this.right, type);
					if (expression != null)
					{
						this.right = expression;
						this.type = type;
						return this;
					}
				}
				else
				{
					Expression expression = Convert.ImplicitConversion(ec, this.right, this.unwrap.Type, this.loc);
					if (expression != null)
					{
						this.left = this.unwrap;
						type = this.left.Type;
						if (this.right.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
						{
							this.type = this.right.Type;
							this.left = Convert.ImplicitBoxingConversion(this.left, type, this.type);
							return this;
						}
						this.right = expression;
						this.type = type;
						return this;
					}
				}
			}
			else
			{
				if (!TypeSpec.IsReferenceType(type))
				{
					return null;
				}
				if (Convert.ImplicitConversionExists(ec, this.right, type))
				{
					if (this.right.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
					{
						this.type = this.right.Type;
						return this;
					}
					Constant constant = this.left as Constant;
					if (constant != null && !constant.IsDefaultValue)
					{
						return ReducedExpression.Create(constant, this, false);
					}
					if (!this.right.IsNull && constant == null)
					{
						this.right = Convert.ImplicitConversion(ec, this.right, type, this.loc);
						this.type = type;
						return this;
					}
					if (this.right.IsNull && type == this.right.Type)
					{
						return null;
					}
					return ReducedExpression.Create((constant != null) ? this.right : this.left, this, false);
				}
			}
			TypeSpec type2 = this.right.Type;
			if (!Convert.ImplicitConversionExists(ec, this.unwrap ?? this.left, type2) || this.right.eclass == ExprClass.MethodGroup)
			{
				return null;
			}
			if (this.left.IsNull)
			{
				return ReducedExpression.Create(this.right, this, false).Resolve(ec);
			}
			this.left = Convert.ImplicitConversion(ec, this.unwrap ?? this.left, type2, this.loc);
			this.type = type2;
			return this;
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000AF5B8 File Offset: 0x000AD7B8
		public override bool ContainsEmitWithAwait()
		{
			if (this.unwrap != null)
			{
				return this.unwrap.ContainsEmitWithAwait() || this.right.ContainsEmitWithAwait();
			}
			return this.left.ContainsEmitWithAwait() || this.right.ContainsEmitWithAwait();
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000AF5F8 File Offset: 0x000AD7F8
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.left = this.left.Resolve(ec);
			this.right = this.right.Resolve(ec);
			if (this.left == null || this.right == null)
			{
				return null;
			}
			this.eclass = ExprClass.Value;
			Expression expression = this.ConvertExpression(ec);
			if (expression == null)
			{
				Binary.Error_OperatorCannotBeApplied(ec, this.left, this.right, "??", this.loc);
				return null;
			}
			return expression;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000AF670 File Offset: 0x000AD870
		public override void Emit(EmitContext ec)
		{
			Label label = ec.DefineLabel();
			if (this.unwrap != null)
			{
				Label label2 = ec.DefineLabel();
				this.unwrap.EmitCheck(ec);
				ec.Emit(OpCodes.Brfalse, label2);
				if (this.type.IsNullableType && TypeSpecComparer.IsEqual(NullableInfo.GetUnderlyingType(this.type), this.unwrap.Type))
				{
					this.unwrap.Load(ec);
				}
				else
				{
					this.left.Emit(ec);
				}
				ec.Emit(OpCodes.Br, label);
				ec.MarkLabel(label2);
				this.right.Emit(ec);
				ec.MarkLabel(label);
				return;
			}
			UserCast userCast = this.left as UserCast;
			if (userCast != null)
			{
				userCast.Source.Emit(ec);
				LocalTemporary localTemporary;
				if (!(userCast.Source is VariableReference))
				{
					localTemporary = new LocalTemporary(userCast.Source.Type);
					localTemporary.Store(ec);
					localTemporary.Emit(ec);
					userCast.Source = localTemporary;
				}
				else
				{
					localTemporary = null;
				}
				Label label3 = ec.DefineLabel();
				ec.Emit(OpCodes.Brfalse_S, label3);
				this.left.Emit(ec);
				ec.Emit(OpCodes.Br, label);
				ec.MarkLabel(label3);
				if (localTemporary != null)
				{
					localTemporary.Release(ec);
				}
			}
			else
			{
				this.left.Emit(ec);
				ec.Emit(OpCodes.Dup);
				if (this.left.Type.IsGenericParameter)
				{
					ec.Emit(OpCodes.Box, this.left.Type);
				}
				ec.Emit(OpCodes.Brtrue, label);
				ec.Emit(OpCodes.Pop);
			}
			this.right.Emit(ec);
			ec.MarkLabel(label);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000AF818 File Offset: 0x000ADA18
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.left.FlowAnalysis(fc);
			DefiniteAssignmentBitSet definiteAssignment = fc.BranchDefiniteAssignment();
			this.right.FlowAnalysis(fc);
			fc.DefiniteAssignment = definiteAssignment;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000AF84B File Offset: 0x000ADA4B
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			NullCoalescingOperator nullCoalescingOperator = (NullCoalescingOperator)t;
			nullCoalescingOperator.left = this.left.Clone(clonectx);
			nullCoalescingOperator.right = this.right.Clone(clonectx);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000AF876 File Offset: 0x000ADA76
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000D9B RID: 3483
		private Expression left;

		// Token: 0x04000D9C RID: 3484
		private Expression right;

		// Token: 0x04000D9D RID: 3485
		private Unwrap unwrap;
	}
}
