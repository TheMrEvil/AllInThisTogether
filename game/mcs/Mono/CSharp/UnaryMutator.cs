using System;
using System.Collections.Generic;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020001CF RID: 463
	public class UnaryMutator : ExpressionStatement
	{
		// Token: 0x06001857 RID: 6231 RVA: 0x0007573E File Offset: 0x0007393E
		public UnaryMutator(UnaryMutator.Mode m, Expression e, Location loc)
		{
			this.mode = m;
			this.loc = loc;
			this.expr = e;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0007575B File Offset: 0x0007395B
		public UnaryMutator.Mode UnaryMutatorMode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x00075763 File Offset: 0x00073963
		public Expression Expr
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0007576B File Offset: 0x0007396B
		public override Location StartLocation
		{
			get
			{
				if ((this.mode & UnaryMutator.Mode.IsPost) == UnaryMutator.Mode.IsIncrement)
				{
					return this.loc;
				}
				return this.expr.Location;
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00075789 File Offset: 0x00073989
		public override bool ContainsEmitWithAwait()
		{
			return this.expr.ContainsEmitWithAwait();
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00075796 File Offset: 0x00073996
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return new SimpleAssign(this, this).CreateExpressionTree(ec);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000757A8 File Offset: 0x000739A8
		public static TypeSpec[] CreatePredefinedOperatorsTable(BuiltinTypes types)
		{
			return new TypeSpec[]
			{
				types.Int,
				types.Long,
				types.SByte,
				types.Byte,
				types.Short,
				types.UInt,
				types.ULong,
				types.Char,
				types.Float,
				types.Double,
				types.Decimal
			};
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00075824 File Offset: 0x00073A24
		protected override Expression DoResolve(ResolveContext ec)
		{
			this.expr = this.expr.Resolve(ec);
			if (this.expr == null || this.expr.Type == InternalType.ErrorType)
			{
				return null;
			}
			if (this.expr.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				if ((this.mode & UnaryMutator.Mode.IsPost) != UnaryMutator.Mode.IsIncrement)
				{
					this.expr = new UnaryMutator.DynamicPostMutator(this.expr);
				}
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(this.expr));
				return new SimpleAssign(this.expr, new DynamicUnaryConversion(this.GetOperatorExpressionTypeName(), arguments, this.loc)).Resolve(ec);
			}
			if (this.expr.Type.IsNullableType)
			{
				return new LiftedUnaryMutator(this.mode, this.expr, this.loc).Resolve(ec);
			}
			return this.DoResolveOperation(ec);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00075904 File Offset: 0x00073B04
		protected Expression DoResolveOperation(ResolveContext ec)
		{
			this.eclass = ExprClass.Value;
			this.type = this.expr.Type;
			if (this.expr is RuntimeValueExpression)
			{
				this.operation = this.expr;
			}
			else
			{
				this.operation = new EmptyExpression(this.type);
			}
			if (this.expr.eclass == ExprClass.Variable || this.expr.eclass == ExprClass.IndexerAccess || this.expr.eclass == ExprClass.PropertyAccess)
			{
				this.expr = this.expr.ResolveLValue(ec, this.expr);
			}
			else
			{
				ec.Report.Error(1059, this.loc, "The operand of an increment or decrement operator must be a variable, property or indexer");
			}
			Operator.OpType opType = this.IsDecrement ? Operator.OpType.Decrement : Operator.OpType.Increment;
			IList<MemberSpec> userOperator = MemberCache.GetUserOperator(this.type, opType, false);
			if (userOperator != null)
			{
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(this.expr));
				OverloadResolver overloadResolver = new OverloadResolver(userOperator, OverloadResolver.Restrictions.NoBaseMembers | OverloadResolver.Restrictions.BaseMembersIncluded, this.loc);
				MethodSpec methodSpec = overloadResolver.ResolveOperator(ec, ref arguments);
				if (methodSpec == null)
				{
					return null;
				}
				arguments[0].Expr = this.operation;
				this.operation = new UserOperatorCall(methodSpec, arguments, null, this.loc);
				this.operation = Convert.ImplicitConversionRequired(ec, this.operation, this.type, this.loc);
				return this;
			}
			else
			{
				Expression expression;
				bool flag;
				switch (this.type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
				case BuiltinTypeSpec.Type.SByte:
				case BuiltinTypeSpec.Type.Char:
				case BuiltinTypeSpec.Type.Short:
				case BuiltinTypeSpec.Type.UShort:
				case BuiltinTypeSpec.Type.Int:
				case BuiltinTypeSpec.Type.UInt:
				case BuiltinTypeSpec.Type.Long:
				case BuiltinTypeSpec.Type.ULong:
				case BuiltinTypeSpec.Type.Float:
				case BuiltinTypeSpec.Type.Double:
				case BuiltinTypeSpec.Type.Decimal:
					expression = this.operation;
					flag = true;
					break;
				default:
					flag = false;
					if (this.type.IsPointer)
					{
						if (((PointerContainer)this.type).Element.Kind == MemberKind.Void)
						{
							base.Error_VoidPointerOperation(ec);
							return null;
						}
						expression = this.operation;
					}
					else
					{
						Expression expression2 = null;
						foreach (TypeSpec target in ec.BuiltinTypes.OperatorsUnaryMutator)
						{
							expression = Convert.ImplicitUserConversion(ec, this.operation, target, this.loc);
							if (expression != null)
							{
								if (expression2 == null)
								{
									expression2 = expression;
								}
								else
								{
									int num = OverloadResolver.BetterTypeConversion(ec, expression2.Type, expression.Type);
									if (num != 1)
									{
										if (num != 2)
										{
											Unary.Error_Ambiguous(ec, UnaryMutator.OperName(this.mode), this.type, this.loc);
											break;
										}
										expression2 = expression;
									}
								}
							}
						}
						expression = expression2;
					}
					if (expression == null && this.type.IsEnum)
					{
						expression = this.operation;
					}
					if (expression == null)
					{
						this.expr.Error_OperatorCannotBeApplied(ec, this.loc, Operator.GetName(opType), this.type);
						return null;
					}
					break;
				}
				IntConstant right = new IntConstant(ec.BuiltinTypes, 1, this.loc);
				Binary.Operator oper = this.IsDecrement ? Binary.Operator.Subtraction : Binary.Operator.Addition;
				this.operation = new Binary(oper, expression, right);
				this.operation = this.operation.Resolve(ec);
				if (this.operation == null)
				{
					throw new NotImplementedException("should not be reached");
				}
				if (this.operation.Type != this.type)
				{
					if (flag)
					{
						this.operation = Convert.ExplicitNumericConversion(ec, this.operation, this.type);
					}
					else
					{
						this.operation = Convert.ImplicitConversionRequired(ec, this.operation, this.type, this.loc);
					}
				}
				return this;
			}
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x00075C6C File Offset: 0x00073E6C
		private void EmitCode(EmitContext ec, bool is_expr)
		{
			this.recurse = true;
			this.is_expr = is_expr;
			((IAssignMethod)this.expr).EmitAssign(ec, this, is_expr && (this.mode == UnaryMutator.Mode.IsIncrement || this.mode == UnaryMutator.Mode.IsDecrement), true);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x00075CAC File Offset: 0x00073EAC
		public override void Emit(EmitContext ec)
		{
			if (this.recurse)
			{
				((IAssignMethod)this.expr).Emit(ec, this.is_expr && (this.mode == UnaryMutator.Mode.IsPost || this.mode == UnaryMutator.Mode.PostDecrement));
				this.EmitOperation(ec);
				this.recurse = false;
				return;
			}
			this.EmitCode(ec, true);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x00075D09 File Offset: 0x00073F09
		protected virtual void EmitOperation(EmitContext ec)
		{
			this.operation.Emit(ec);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00075D17 File Offset: 0x00073F17
		public override void EmitStatement(EmitContext ec)
		{
			this.EmitCode(ec, false);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00075D21 File Offset: 0x00073F21
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.expr.FlowAnalysis(fc);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x00075D2F File Offset: 0x00073F2F
		private string GetOperatorExpressionTypeName()
		{
			if (!this.IsDecrement)
			{
				return "Increment";
			}
			return "Decrement";
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x00075D44 File Offset: 0x00073F44
		private bool IsDecrement
		{
			get
			{
				return (this.mode & UnaryMutator.Mode.IsDecrement) > UnaryMutator.Mode.IsIncrement;
			}
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00075D51 File Offset: 0x00073F51
		public static string OperName(UnaryMutator.Mode oper)
		{
			if ((oper & UnaryMutator.Mode.IsDecrement) == UnaryMutator.Mode.IsIncrement)
			{
				return "++";
			}
			return "--";
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00075D63 File Offset: 0x00073F63
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((UnaryMutator)t).expr = this.expr.Clone(clonectx);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00075D7C File Offset: 0x00073F7C
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x0400099B RID: 2459
		private UnaryMutator.Mode mode;

		// Token: 0x0400099C RID: 2460
		private bool is_expr;

		// Token: 0x0400099D RID: 2461
		private bool recurse;

		// Token: 0x0400099E RID: 2462
		protected Expression expr;

		// Token: 0x0400099F RID: 2463
		private Expression operation;

		// Token: 0x020003B1 RID: 945
		private class DynamicPostMutator : Expression, IAssignMethod
		{
			// Token: 0x0600270F RID: 9999 RVA: 0x000BAEB0 File Offset: 0x000B90B0
			public DynamicPostMutator(Expression expr)
			{
				this.expr = expr;
				this.type = expr.Type;
				this.loc = expr.Location;
			}

			// Token: 0x06002710 RID: 10000 RVA: 0x0003314F File Offset: 0x0003134F
			public override Expression CreateExpressionTree(ResolveContext ec)
			{
				throw new NotImplementedException("ET");
			}

			// Token: 0x06002711 RID: 10001 RVA: 0x000BAED7 File Offset: 0x000B90D7
			protected override Expression DoResolve(ResolveContext rc)
			{
				this.eclass = this.expr.eclass;
				return this;
			}

			// Token: 0x06002712 RID: 10002 RVA: 0x000BAEEB File Offset: 0x000B90EB
			public override Expression DoResolveLValue(ResolveContext ec, Expression right_side)
			{
				this.expr.DoResolveLValue(ec, right_side);
				return this.DoResolve(ec);
			}

			// Token: 0x06002713 RID: 10003 RVA: 0x000BAF02 File Offset: 0x000B9102
			public override void Emit(EmitContext ec)
			{
				this.temp.Emit(ec);
			}

			// Token: 0x06002714 RID: 10004 RVA: 0x00023DF4 File Offset: 0x00021FF4
			public void Emit(EmitContext ec, bool leave_copy)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002715 RID: 10005 RVA: 0x000BAF10 File Offset: 0x000B9110
			public void EmitAssign(EmitContext ec, Expression source, bool leave_copy, bool isCompound)
			{
				this.temp = new LocalTemporary(this.type);
				this.expr.Emit(ec);
				this.temp.Store(ec);
				((IAssignMethod)this.expr).EmitAssign(ec, source, false, isCompound);
				if (leave_copy)
				{
					this.Emit(ec);
				}
				this.temp.Release(ec);
				this.temp = null;
			}

			// Token: 0x0400106C RID: 4204
			private LocalTemporary temp;

			// Token: 0x0400106D RID: 4205
			private Expression expr;
		}

		// Token: 0x020003B2 RID: 946
		[Flags]
		public enum Mode : byte
		{
			// Token: 0x0400106F RID: 4207
			IsIncrement = 0,
			// Token: 0x04001070 RID: 4208
			IsDecrement = 1,
			// Token: 0x04001071 RID: 4209
			IsPre = 0,
			// Token: 0x04001072 RID: 4210
			IsPost = 2,
			// Token: 0x04001073 RID: 4211
			PreIncrement = 0,
			// Token: 0x04001074 RID: 4212
			PreDecrement = 1,
			// Token: 0x04001075 RID: 4213
			PostIncrement = 2,
			// Token: 0x04001076 RID: 4214
			PostDecrement = 3
		}
	}
}
