using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020001CD RID: 461
	public class Unary : Expression
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x0007433B File Offset: 0x0007253B
		public Unary(Unary.Operator op, Expression expr, Location loc)
		{
			this.Oper = op;
			this.Expr = expr;
			this.loc = loc;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00074358 File Offset: 0x00072558
		private Constant TryReduceConstant(ResolveContext ec, Constant constant)
		{
			Constant constant2 = constant;
			while (constant2 is EmptyConstantCast)
			{
				constant2 = ((EmptyConstantCast)constant2).child;
			}
			if (constant2 is SideEffectConstant)
			{
				Constant constant3 = this.TryReduceConstant(ec, ((SideEffectConstant)constant2).value);
				if (constant3 != null)
				{
					return new SideEffectConstant(constant3, constant2, constant3.Location);
				}
				return null;
			}
			else
			{
				TypeSpec type = constant2.Type;
				switch (this.Oper)
				{
				case Unary.Operator.UnaryPlus:
					switch (type.BuiltinType)
					{
					case BuiltinTypeSpec.Type.Byte:
						return new IntConstant(ec.BuiltinTypes, (int)((ByteConstant)constant2).Value, constant2.Location);
					case BuiltinTypeSpec.Type.SByte:
						return new IntConstant(ec.BuiltinTypes, (int)((SByteConstant)constant2).Value, constant2.Location);
					case BuiltinTypeSpec.Type.Char:
						return new IntConstant(ec.BuiltinTypes, (int)((CharConstant)constant2).Value, constant2.Location);
					case BuiltinTypeSpec.Type.Short:
						return new IntConstant(ec.BuiltinTypes, (int)((ShortConstant)constant2).Value, constant2.Location);
					case BuiltinTypeSpec.Type.UShort:
						return new IntConstant(ec.BuiltinTypes, (int)((UShortConstant)constant2).Value, constant2.Location);
					case BuiltinTypeSpec.Type.Int:
					case BuiltinTypeSpec.Type.UInt:
					case BuiltinTypeSpec.Type.Long:
					case BuiltinTypeSpec.Type.ULong:
					case BuiltinTypeSpec.Type.Float:
					case BuiltinTypeSpec.Type.Double:
					case BuiltinTypeSpec.Type.Decimal:
						return constant2;
					default:
						return null;
					}
					break;
				case Unary.Operator.UnaryNegation:
					switch (type.BuiltinType)
					{
					case BuiltinTypeSpec.Type.Byte:
						return new IntConstant(ec.BuiltinTypes, (int)(-(int)((ByteConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.SByte:
						return new IntConstant(ec.BuiltinTypes, (int)(-(int)((SByteConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.Char:
						return new IntConstant(ec.BuiltinTypes, (int)(-(int)((CharConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.Short:
						return new IntConstant(ec.BuiltinTypes, (int)(-(int)((ShortConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.UShort:
						return new IntConstant(ec.BuiltinTypes, (int)(-(int)((UShortConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.Int:
					{
						int value = ((IntConstant)constant2).Value;
						if (value != -2147483648)
						{
							return new IntConstant(ec.BuiltinTypes, -value, constant2.Location);
						}
						if (ec.ConstantCheckState)
						{
							ConstantFold.Error_CompileTimeOverflow(ec, this.loc);
							return null;
						}
						return constant2;
					}
					case BuiltinTypeSpec.Type.UInt:
					{
						UIntLiteral uintLiteral = constant as UIntLiteral;
						if (uintLiteral == null)
						{
							return new LongConstant(ec.BuiltinTypes, (long)(-(long)((ulong)((UIntConstant)constant2).Value)), constant2.Location);
						}
						if (uintLiteral.Value == 2147483648U)
						{
							return new IntLiteral(ec.BuiltinTypes, int.MinValue, constant2.Location);
						}
						return new LongLiteral(ec.BuiltinTypes, (long)(-(long)((ulong)uintLiteral.Value)), constant2.Location);
					}
					case BuiltinTypeSpec.Type.Long:
					{
						long value2 = ((LongConstant)constant2).Value;
						if (value2 != -9223372036854775808L)
						{
							return new LongConstant(ec.BuiltinTypes, -value2, constant2.Location);
						}
						if (ec.ConstantCheckState)
						{
							ConstantFold.Error_CompileTimeOverflow(ec, this.loc);
							return null;
						}
						return constant2;
					}
					case BuiltinTypeSpec.Type.ULong:
					{
						ULongLiteral ulongLiteral = constant as ULongLiteral;
						if (ulongLiteral != null && ulongLiteral.Value == 9223372036854775808UL)
						{
							return new LongLiteral(ec.BuiltinTypes, long.MinValue, constant2.Location);
						}
						return null;
					}
					case BuiltinTypeSpec.Type.Float:
					{
						FloatLiteral floatLiteral = constant as FloatLiteral;
						if (floatLiteral != null)
						{
							return new FloatLiteral(ec.BuiltinTypes, -floatLiteral.Value, constant2.Location);
						}
						return new FloatConstant(ec.BuiltinTypes, (double)(-(double)((FloatConstant)constant2).Value), constant2.Location);
					}
					case BuiltinTypeSpec.Type.Double:
					{
						DoubleLiteral doubleLiteral = constant as DoubleLiteral;
						if (doubleLiteral != null)
						{
							return new DoubleLiteral(ec.BuiltinTypes, -doubleLiteral.Value, constant2.Location);
						}
						return new DoubleConstant(ec.BuiltinTypes, -((DoubleConstant)constant2).Value, constant2.Location);
					}
					case BuiltinTypeSpec.Type.Decimal:
						return new DecimalConstant(ec.BuiltinTypes, -((DecimalConstant)constant2).Value, constant2.Location);
					default:
						return null;
					}
					break;
				case Unary.Operator.LogicalNot:
				{
					if (type.BuiltinType != BuiltinTypeSpec.Type.FirstPrimitive)
					{
						return null;
					}
					bool flag = (bool)constant2.GetValue();
					return new BoolConstant(ec.BuiltinTypes, !flag, constant2.Location);
				}
				case Unary.Operator.OnesComplement:
					switch (type.BuiltinType)
					{
					case BuiltinTypeSpec.Type.Byte:
						return new IntConstant(ec.BuiltinTypes, (int)(~(int)((ByteConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.SByte:
						return new IntConstant(ec.BuiltinTypes, (int)(~(int)((SByteConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.Char:
						return new IntConstant(ec.BuiltinTypes, (int)(~(int)((CharConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.Short:
						return new IntConstant(ec.BuiltinTypes, (int)(~(int)((ShortConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.UShort:
						return new IntConstant(ec.BuiltinTypes, (int)(~(int)((UShortConstant)constant2).Value), constant2.Location);
					case BuiltinTypeSpec.Type.Int:
						return new IntConstant(ec.BuiltinTypes, ~((IntConstant)constant2).Value, constant2.Location);
					case BuiltinTypeSpec.Type.UInt:
						return new UIntConstant(ec.BuiltinTypes, ~((UIntConstant)constant2).Value, constant2.Location);
					case BuiltinTypeSpec.Type.Long:
						return new LongConstant(ec.BuiltinTypes, ~((LongConstant)constant2).Value, constant2.Location);
					case BuiltinTypeSpec.Type.ULong:
						return new ULongConstant(ec.BuiltinTypes, ~((ULongConstant)constant2).Value, constant2.Location);
					default:
						if (constant2 is EnumConstant)
						{
							Constant constant4 = this.TryReduceConstant(ec, ((EnumConstant)constant2).Child);
							if (constant4 != null)
							{
								if (constant4.Type.BuiltinType == BuiltinTypeSpec.Type.Int)
								{
									int value3 = ((IntConstant)constant4).Value;
									switch (((EnumConstant)constant2).Child.Type.BuiltinType)
									{
									case BuiltinTypeSpec.Type.Byte:
										constant4 = new ByteConstant(ec.BuiltinTypes, (byte)value3, constant2.Location);
										break;
									case BuiltinTypeSpec.Type.SByte:
										constant4 = new SByteConstant(ec.BuiltinTypes, (sbyte)value3, constant2.Location);
										break;
									case BuiltinTypeSpec.Type.Short:
										constant4 = new ShortConstant(ec.BuiltinTypes, (short)value3, constant2.Location);
										break;
									case BuiltinTypeSpec.Type.UShort:
										constant4 = new UShortConstant(ec.BuiltinTypes, (ushort)value3, constant2.Location);
										break;
									}
								}
								constant4 = new EnumConstant(constant4, type);
							}
							return constant4;
						}
						return null;
					}
					break;
				default:
					throw new Exception("Can not constant fold: " + this.Oper.ToString());
				}
			}
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000749EC File Offset: 0x00072BEC
		protected virtual Expression ResolveOperator(ResolveContext ec, Expression expr)
		{
			this.eclass = ExprClass.Value;
			TypeSpec type = expr.Type;
			TypeSpec[] predefined = ec.BuiltinTypes.OperatorsUnary[(int)this.Oper];
			if (BuiltinTypeSpec.IsPrimitiveType(type))
			{
				Expression expression = this.ResolvePrimitivePredefinedType(ec, expr, predefined);
				if (expression == null)
				{
					return null;
				}
				this.type = expression.Type;
				this.Expr = expression;
				return this;
			}
			else
			{
				if (this.Oper == Unary.Operator.OnesComplement && type.IsEnum)
				{
					return this.ResolveEnumOperator(ec, expr, predefined);
				}
				return this.ResolveUserType(ec, expr, predefined);
			}
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00074A6C File Offset: 0x00072C6C
		protected virtual Expression ResolveEnumOperator(ResolveContext ec, Expression expr, TypeSpec[] predefined)
		{
			TypeSpec underlyingType = EnumSpec.GetUnderlyingType(expr.Type);
			Expression expression = this.ResolvePrimitivePredefinedType(ec, EmptyCast.Create(expr, underlyingType), predefined);
			if (expression == null)
			{
				return null;
			}
			this.Expr = expression;
			this.enum_conversion = Binary.GetEnumResultCast(underlyingType);
			this.type = expr.Type;
			return EmptyCast.Create(this, this.type);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00074AC5 File Offset: 0x00072CC5
		public override bool ContainsEmitWithAwait()
		{
			return this.Expr.ContainsEmitWithAwait();
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00074AD2 File Offset: 0x00072CD2
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return this.CreateExpressionTree(ec, null);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00074ADC File Offset: 0x00072CDC
		private Expression CreateExpressionTree(ResolveContext ec, Expression user_op)
		{
			string name;
			switch (this.Oper)
			{
			case Unary.Operator.UnaryPlus:
				name = "UnaryPlus";
				break;
			case Unary.Operator.UnaryNegation:
				if (ec.HasSet(ResolveContext.Options.CheckedScope) && user_op == null && !Unary.IsFloat(this.type))
				{
					name = "NegateChecked";
				}
				else
				{
					name = "Negate";
				}
				break;
			case Unary.Operator.LogicalNot:
			case Unary.Operator.OnesComplement:
				name = "Not";
				break;
			case Unary.Operator.AddressOf:
				base.Error_PointerInsideExpressionTree(ec);
				return null;
			default:
				throw new InternalErrorException("Unknown unary operator " + this.Oper.ToString());
			}
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this.Expr.CreateExpressionTree(ec)));
			if (user_op != null)
			{
				arguments.Add(new Argument(user_op));
			}
			return base.CreateExpressionFactoryCall(ec, name, arguments);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00074BA8 File Offset: 0x00072DA8
		public static TypeSpec[][] CreatePredefinedOperatorsTable(BuiltinTypes types)
		{
			TypeSpec[][] array = new TypeSpec[5][];
			array[0] = new TypeSpec[]
			{
				types.Int,
				types.UInt,
				types.Long,
				types.ULong,
				types.Float,
				types.Double,
				types.Decimal
			};
			array[1] = new TypeSpec[]
			{
				types.Int,
				types.Long,
				types.Float,
				types.Double,
				types.Decimal
			};
			array[2] = new TypeSpec[]
			{
				types.Bool
			};
			array[3] = new TypeSpec[]
			{
				types.Int,
				types.UInt,
				types.Long,
				types.ULong
			};
			return array;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00074C7C File Offset: 0x00072E7C
		private static Expression DoNumericPromotion(ResolveContext rc, Unary.Operator op, Expression expr)
		{
			TypeSpec type = expr.Type;
			if (op == Unary.Operator.UnaryPlus || op == Unary.Operator.UnaryNegation || op == Unary.Operator.OnesComplement)
			{
				switch (type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
				case BuiltinTypeSpec.Type.SByte:
				case BuiltinTypeSpec.Type.Char:
				case BuiltinTypeSpec.Type.Short:
				case BuiltinTypeSpec.Type.UShort:
					return Convert.ImplicitNumericConversion(expr, rc.BuiltinTypes.Int);
				}
			}
			if (op == Unary.Operator.UnaryNegation && type.BuiltinType == BuiltinTypeSpec.Type.UInt)
			{
				return Convert.ImplicitNumericConversion(expr, rc.BuiltinTypes.Long);
			}
			return expr;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00074CF4 File Offset: 0x00072EF4
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.Oper == Unary.Operator.AddressOf)
			{
				return this.ResolveAddressOf(ec);
			}
			this.Expr = this.Expr.Resolve(ec);
			if (this.Expr == null)
			{
				return null;
			}
			if (this.Expr.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				Arguments arguments = new Arguments(1);
				arguments.Add(new Argument(this.Expr));
				return new DynamicUnaryConversion(this.GetOperatorExpressionTypeName(), arguments, this.loc).Resolve(ec);
			}
			if (this.Expr.Type.IsNullableType)
			{
				return new LiftedUnaryOperator(this.Oper, this.Expr, this.loc).Resolve(ec);
			}
			Constant constant = this.Expr as Constant;
			if (constant != null)
			{
				constant = this.TryReduceConstant(ec, constant);
				if (constant != null)
				{
					return constant;
				}
			}
			Expression expression = this.ResolveOperator(ec, this.Expr);
			if (expression == null)
			{
				this.Error_OperatorCannotBeApplied(ec, this.loc, Unary.OperName(this.Oper), this.Expr.Type);
			}
			if (expression == this && this.Oper == Unary.Operator.UnaryPlus)
			{
				return this.Expr;
			}
			return expression;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x000055E7 File Offset: 0x000037E7
		public override Expression DoResolveLValue(ResolveContext ec, Expression right)
		{
			return null;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00074E08 File Offset: 0x00073008
		public override void Emit(EmitContext ec)
		{
			this.EmitOperator(ec, this.type);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00074E18 File Offset: 0x00073018
		protected void EmitOperator(EmitContext ec, TypeSpec type)
		{
			switch (this.Oper)
			{
			case Unary.Operator.UnaryPlus:
				this.Expr.Emit(ec);
				break;
			case Unary.Operator.UnaryNegation:
				if (ec.HasSet(BuilderContext.Options.CheckedScope) && !Unary.IsFloat(type))
				{
					if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.Expr.ContainsEmitWithAwait())
					{
						this.Expr = this.Expr.EmitToField(ec);
					}
					ec.EmitInt(0);
					if (type.BuiltinType == BuiltinTypeSpec.Type.Long)
					{
						ec.Emit(OpCodes.Conv_U8);
					}
					this.Expr.Emit(ec);
					ec.Emit(OpCodes.Sub_Ovf);
				}
				else
				{
					this.Expr.Emit(ec);
					ec.Emit(OpCodes.Neg);
				}
				break;
			case Unary.Operator.LogicalNot:
				this.Expr.Emit(ec);
				ec.EmitInt(0);
				ec.Emit(OpCodes.Ceq);
				break;
			case Unary.Operator.OnesComplement:
				this.Expr.Emit(ec);
				ec.Emit(OpCodes.Not);
				break;
			case Unary.Operator.AddressOf:
				((IMemoryLocation)this.Expr).AddressOf(ec, AddressOp.LoadStore);
				break;
			default:
				throw new Exception("This should not happen: Operator = " + this.Oper.ToString());
			}
			if (this.enum_conversion != ConvCast.Mode.I1_U1)
			{
				using (ec.With(BuilderContext.Options.CheckedScope, false))
				{
					ConvCast.Emit(ec, this.enum_conversion);
				}
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00074F98 File Offset: 0x00073198
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			if (this.Oper == Unary.Operator.LogicalNot)
			{
				this.Expr.EmitBranchable(ec, target, !on_true);
				return;
			}
			base.EmitBranchable(ec, target, on_true);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00074FBE File Offset: 0x000731BE
		public override void EmitSideEffect(EmitContext ec)
		{
			this.Expr.EmitSideEffect(ec);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00074FCC File Offset: 0x000731CC
		public static void Error_Ambiguous(ResolveContext rc, string oper, TypeSpec type, Location loc)
		{
			rc.Report.Error(35, loc, "Operator `{0}' is ambiguous on an operand of type `{1}'", oper, type.GetSignatureForError());
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00074FE8 File Offset: 0x000731E8
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			this.FlowAnalysis(fc, false);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00074FF2 File Offset: 0x000731F2
		public override void FlowAnalysisConditional(FlowAnalysisContext fc)
		{
			this.FlowAnalysis(fc, true);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00074FFC File Offset: 0x000731FC
		private void FlowAnalysis(FlowAnalysisContext fc, bool conditional)
		{
			if (this.Oper == Unary.Operator.AddressOf)
			{
				VariableReference variableReference = this.Expr as VariableReference;
				if (variableReference != null && variableReference.VariableInfo != null)
				{
					fc.SetVariableAssigned(variableReference.VariableInfo, false);
				}
				return;
			}
			if (this.Oper == Unary.Operator.LogicalNot && conditional)
			{
				this.Expr.FlowAnalysisConditional(fc);
				DefiniteAssignmentBitSet definiteAssignmentOnTrue = fc.DefiniteAssignmentOnTrue;
				fc.DefiniteAssignmentOnTrue = fc.DefiniteAssignmentOnFalse;
				fc.DefiniteAssignmentOnFalse = definiteAssignmentOnTrue;
				return;
			}
			this.Expr.FlowAnalysis(fc);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00075078 File Offset: 0x00073278
		private string GetOperatorExpressionTypeName()
		{
			switch (this.Oper)
			{
			case Unary.Operator.UnaryPlus:
				return "UnaryPlus";
			case Unary.Operator.UnaryNegation:
				return "Negate";
			case Unary.Operator.LogicalNot:
				return "Not";
			case Unary.Operator.OnesComplement:
				return "OnesComplement";
			default:
				throw new NotImplementedException("Unknown express type operator " + this.Oper.ToString());
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000750DF File Offset: 0x000732DF
		private static bool IsFloat(TypeSpec t)
		{
			return t.BuiltinType == BuiltinTypeSpec.Type.Double || t.BuiltinType == BuiltinTypeSpec.Type.Float;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000750F8 File Offset: 0x000732F8
		public static string OperName(Unary.Operator oper)
		{
			switch (oper)
			{
			case Unary.Operator.UnaryPlus:
				return "+";
			case Unary.Operator.UnaryNegation:
				return "-";
			case Unary.Operator.LogicalNot:
				return "!";
			case Unary.Operator.OnesComplement:
				return "~";
			case Unary.Operator.AddressOf:
				return "&";
			default:
				throw new NotImplementedException(oper.ToString());
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00075154 File Offset: 0x00073354
		public override Expression MakeExpression(BuilderContext ctx)
		{
			Expression expression = this.Expr.MakeExpression(ctx);
			bool flag = ctx.HasSet(BuilderContext.Options.CheckedScope);
			Unary.Operator oper = this.Oper;
			if (oper != Unary.Operator.UnaryNegation)
			{
				if (oper != Unary.Operator.LogicalNot)
				{
					throw new NotImplementedException(this.Oper.ToString());
				}
				return Expression.Not(expression);
			}
			else
			{
				if (!flag)
				{
					return Expression.Negate(expression);
				}
				return Expression.NegateChecked(expression);
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000751B8 File Offset: 0x000733B8
		private Expression ResolveAddressOf(ResolveContext ec)
		{
			if (!ec.IsUnsafe)
			{
				Expression.UnsafeError(ec, this.loc);
			}
			this.Expr = this.Expr.DoResolveLValue(ec, EmptyExpression.UnaryAddress);
			if (this.Expr == null || this.Expr.eclass != ExprClass.Variable)
			{
				ec.Report.Error(211, this.loc, "Cannot take the address of the given expression");
				return null;
			}
			if (!TypeManager.VerifyUnmanaged(ec.Module, this.Expr.Type, this.loc))
			{
				return null;
			}
			IVariableReference variableReference = this.Expr as IVariableReference;
			bool flag;
			if (variableReference != null)
			{
				flag = variableReference.IsFixed;
				variableReference.SetHasAddressTaken();
				if (variableReference.IsHoisted)
				{
					AnonymousMethodExpression.Error_AddressOfCapturedVar(ec, variableReference, this.loc);
				}
			}
			else
			{
				IFixedExpression fixedExpression = this.Expr as IFixedExpression;
				flag = (fixedExpression != null && fixedExpression.IsFixed);
			}
			if (!flag && !ec.HasSet(ResolveContext.Options.FixedInitializerScope))
			{
				ec.Report.Error(212, this.loc, "You can only take the address of unfixed expression inside of a fixed statement initializer");
			}
			this.type = PointerContainer.MakeType(ec.Module, this.Expr.Type);
			this.eclass = ExprClass.Value;
			return this;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000752E0 File Offset: 0x000734E0
		private Expression ResolvePrimitivePredefinedType(ResolveContext rc, Expression expr, TypeSpec[] predefined)
		{
			expr = Unary.DoNumericPromotion(rc, this.Oper, expr);
			TypeSpec type = expr.Type;
			for (int i = 0; i < predefined.Length; i++)
			{
				if (predefined[i] == type)
				{
					return expr;
				}
			}
			return null;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0007531C File Offset: 0x0007351C
		protected virtual Expression ResolveUserOperator(ResolveContext ec, Expression expr)
		{
			Mono.CSharp.Operator.OpType op;
			switch (this.Oper)
			{
			case Unary.Operator.UnaryPlus:
				op = Mono.CSharp.Operator.OpType.UnaryPlus;
				break;
			case Unary.Operator.UnaryNegation:
				op = Mono.CSharp.Operator.OpType.UnaryNegation;
				break;
			case Unary.Operator.LogicalNot:
				op = Mono.CSharp.Operator.OpType.LogicalNot;
				break;
			case Unary.Operator.OnesComplement:
				op = Mono.CSharp.Operator.OpType.OnesComplement;
				break;
			default:
				throw new InternalErrorException(this.Oper.ToString());
			}
			IList<MemberSpec> userOperator = MemberCache.GetUserOperator(expr.Type, op, false);
			if (userOperator == null)
			{
				return null;
			}
			Arguments arguments = new Arguments(1);
			arguments.Add(new Argument(expr));
			OverloadResolver overloadResolver = new OverloadResolver(userOperator, OverloadResolver.Restrictions.NoBaseMembers | OverloadResolver.Restrictions.BaseMembersIncluded, this.loc);
			MethodSpec methodSpec = overloadResolver.ResolveOperator(ec, ref arguments);
			if (methodSpec == null)
			{
				return null;
			}
			this.Expr = arguments[0].Expr;
			return new UserOperatorCall(methodSpec, arguments, new Func<ResolveContext, Expression, Expression>(this.CreateExpressionTree), expr.Location);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000753EC File Offset: 0x000735EC
		private Expression ResolveUserType(ResolveContext ec, Expression expr, TypeSpec[] predefined)
		{
			Expression expression = this.ResolveUserOperator(ec, expr);
			if (expression != null)
			{
				return expression;
			}
			foreach (TypeSpec typeSpec in predefined)
			{
				Expression expression2 = Convert.ImplicitUserConversion(ec, expr, typeSpec, expr.Location);
				if (expression2 != null)
				{
					if (expression2 == ErrorExpression.Instance)
					{
						return expression2;
					}
					if (expression2.Type.BuiltinType == BuiltinTypeSpec.Type.Decimal)
					{
						expression2 = this.ResolveUserType(ec, expression2, predefined);
					}
					else
					{
						expression2 = this.ResolvePrimitivePredefinedType(ec, expression2, predefined);
					}
					if (expression2 != null)
					{
						if (expression == null)
						{
							expression = expression2;
						}
						else
						{
							int num = OverloadResolver.BetterTypeConversion(ec, expression.Type, typeSpec);
							if (num == 0)
							{
								if ((expression2 is UserOperatorCall || expression2 is UserCast) && (expression is UserOperatorCall || expression is UserCast))
								{
									Unary.Error_Ambiguous(ec, Unary.OperName(this.Oper), expr.Type, this.loc);
									break;
								}
								this.Error_OperatorCannotBeApplied(ec, this.loc, Unary.OperName(this.Oper), expr.Type);
								break;
							}
							else if (num == 2)
							{
								expression = expression2;
							}
						}
					}
				}
			}
			if (expression == null)
			{
				return null;
			}
			if (expression.Type.BuiltinType == BuiltinTypeSpec.Type.Decimal)
			{
				return expression;
			}
			this.Expr = expression;
			this.type = expression.Type;
			return this;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00075523 File Offset: 0x00073723
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			((Unary)t).Expr = this.Expr.Clone(clonectx);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x0007553C File Offset: 0x0007373C
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000995 RID: 2453
		public readonly Unary.Operator Oper;

		// Token: 0x04000996 RID: 2454
		public Expression Expr;

		// Token: 0x04000997 RID: 2455
		private ConvCast.Mode enum_conversion;

		// Token: 0x020003B0 RID: 944
		public enum Operator : byte
		{
			// Token: 0x04001066 RID: 4198
			UnaryPlus,
			// Token: 0x04001067 RID: 4199
			UnaryNegation,
			// Token: 0x04001068 RID: 4200
			LogicalNot,
			// Token: 0x04001069 RID: 4201
			OnesComplement,
			// Token: 0x0400106A RID: 4202
			AddressOf,
			// Token: 0x0400106B RID: 4203
			TOP
		}
	}
}
