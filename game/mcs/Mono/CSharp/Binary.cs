using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x020001DD RID: 477
	public class Binary : Expression, IDynamicBinder
	{
		// Token: 0x060018C9 RID: 6345 RVA: 0x000779BA File Offset: 0x00075BBA
		public Binary(Binary.Operator oper, Expression left, Expression right, bool isCompound) : this(oper, left, right, Binary.State.Compound)
		{
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000779C6 File Offset: 0x00075BC6
		public Binary(Binary.Operator oper, Expression left, Expression right, Binary.State state) : this(oper, left, right)
		{
			this.state = state;
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000779D9 File Offset: 0x00075BD9
		public Binary(Binary.Operator oper, Expression left, Expression right) : this(oper, left, right, left.Location)
		{
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000779EA File Offset: 0x00075BEA
		public Binary(Binary.Operator oper, Expression left, Expression right, Location loc)
		{
			this.oper = oper;
			this.left = left;
			this.right = right;
			this.loc = loc;
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00077A0F File Offset: 0x00075C0F
		public bool IsCompound
		{
			get
			{
				return (this.state & Binary.State.Compound) > Binary.State.None;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00077A1C File Offset: 0x00075C1C
		public Binary.Operator Oper
		{
			get
			{
				return this.oper;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00077A24 File Offset: 0x00075C24
		public Expression Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00077A2C File Offset: 0x00075C2C
		public Expression Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00077A34 File Offset: 0x00075C34
		public override Location StartLocation
		{
			get
			{
				return this.left.StartLocation;
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00077A44 File Offset: 0x00075C44
		private string OperName(Binary.Operator oper)
		{
			string text;
			if (oper <= Binary.Operator.Inequality)
			{
				if (oper <= Binary.Operator.LeftShift)
				{
					switch (oper)
					{
					case Binary.Operator.Multiply:
						text = "*";
						goto IL_165;
					case Binary.Operator.Division:
						text = "/";
						goto IL_165;
					case Binary.Operator.Modulus:
						text = "%";
						goto IL_165;
					default:
						if (oper == Binary.Operator.LeftShift)
						{
							text = "<<";
							goto IL_165;
						}
						break;
					}
				}
				else
				{
					if (oper == Binary.Operator.RightShift)
					{
						text = ">>";
						goto IL_165;
					}
					if (oper == Binary.Operator.Equality)
					{
						text = "==";
						goto IL_165;
					}
					if (oper == Binary.Operator.Inequality)
					{
						text = "!=";
						goto IL_165;
					}
				}
			}
			else if (oper <= Binary.Operator.LogicalOr)
			{
				switch (oper)
				{
				case Binary.Operator.BitwiseAnd:
					text = "&";
					goto IL_165;
				case Binary.Operator.ExclusiveOr:
					text = "^";
					goto IL_165;
				case Binary.Operator.BitwiseOr:
					text = "|";
					goto IL_165;
				default:
					if (oper == Binary.Operator.LogicalAnd)
					{
						text = "&&";
						goto IL_165;
					}
					if (oper == Binary.Operator.LogicalOr)
					{
						text = "||";
						goto IL_165;
					}
					break;
				}
			}
			else
			{
				if (oper == Binary.Operator.Addition)
				{
					text = "+";
					goto IL_165;
				}
				if (oper == Binary.Operator.Subtraction)
				{
					text = "-";
					goto IL_165;
				}
				switch (oper)
				{
				case Binary.Operator.LessThan:
					text = "<";
					goto IL_165;
				case Binary.Operator.GreaterThan:
					text = ">";
					goto IL_165;
				case Binary.Operator.LessThanOrEqual:
					text = "<=";
					goto IL_165;
				case Binary.Operator.GreaterThanOrEqual:
					text = ">=";
					goto IL_165;
				}
			}
			text = oper.ToString();
			IL_165:
			if (this.IsCompound)
			{
				return text + "=";
			}
			return text;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00077BCB File Offset: 0x00075DCB
		public static void Error_OperatorCannotBeApplied(ResolveContext ec, Expression left, Expression right, Binary.Operator oper, Location loc)
		{
			new Binary(oper, left, right).Error_OperatorCannotBeApplied(ec, left, right);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00077BE0 File Offset: 0x00075DE0
		public static void Error_OperatorCannotBeApplied(ResolveContext ec, Expression left, Expression right, string oper, Location loc)
		{
			if (left.Type == InternalType.ErrorType || right.Type == InternalType.ErrorType)
			{
				return;
			}
			string signatureForError = left.Type.GetSignatureForError();
			string signatureForError2 = right.Type.GetSignatureForError();
			ec.Report.Error(19, loc, "Operator `{0}' cannot be applied to operands of type `{1}' and `{2}'", new string[]
			{
				oper,
				signatureForError,
				signatureForError2
			});
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00077C46 File Offset: 0x00075E46
		private void Error_OperatorCannotBeApplied(ResolveContext ec, Expression left, Expression right)
		{
			Binary.Error_OperatorCannotBeApplied(ec, left, right, this.OperName(this.oper), this.loc);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00077C64 File Offset: 0x00075E64
		public override void FlowAnalysis(FlowAnalysisContext fc)
		{
			if ((this.oper & Binary.Operator.LogicalMask) == (Binary.Operator)0)
			{
				this.left.FlowAnalysis(fc);
				this.right.FlowAnalysis(fc);
				return;
			}
			this.left.FlowAnalysisConditional(fc);
			DefiniteAssignmentBitSet definiteAssignmentOnTrue = fc.DefiniteAssignmentOnTrue;
			DefiniteAssignmentBitSet definiteAssignmentOnFalse = fc.DefiniteAssignmentOnFalse;
			fc.DefiniteAssignmentOnTrue = (fc.DefiniteAssignmentOnFalse = (fc.DefiniteAssignment = new DefiniteAssignmentBitSet((this.oper == Binary.Operator.LogicalOr) ? definiteAssignmentOnFalse : definiteAssignmentOnTrue)));
			this.right.FlowAnalysisConditional(fc);
			if (this.oper == Binary.Operator.LogicalOr)
			{
				fc.DefiniteAssignment = ((definiteAssignmentOnFalse | (fc.DefiniteAssignmentOnFalse & fc.DefiniteAssignmentOnTrue)) & definiteAssignmentOnTrue);
				return;
			}
			fc.DefiniteAssignment = ((definiteAssignmentOnTrue | (fc.DefiniteAssignmentOnFalse & fc.DefiniteAssignmentOnTrue)) & definiteAssignmentOnFalse);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00077D40 File Offset: 0x00075F40
		public override void FlowAnalysisConditional(FlowAnalysisContext fc)
		{
			if ((this.oper & Binary.Operator.LogicalMask) == (Binary.Operator)0)
			{
				base.FlowAnalysisConditional(fc);
				return;
			}
			this.left.FlowAnalysisConditional(fc);
			DefiniteAssignmentBitSet definiteAssignmentOnTrue = fc.DefiniteAssignmentOnTrue;
			DefiniteAssignmentBitSet definiteAssignmentOnFalse = fc.DefiniteAssignmentOnFalse;
			fc.DefiniteAssignmentOnTrue = (fc.DefiniteAssignmentOnFalse = (fc.DefiniteAssignment = new DefiniteAssignmentBitSet((this.oper == Binary.Operator.LogicalOr) ? definiteAssignmentOnFalse : definiteAssignmentOnTrue)));
			this.right.FlowAnalysisConditional(fc);
			Constant constant = this.left as Constant;
			if (this.oper == Binary.Operator.LogicalOr)
			{
				fc.DefiniteAssignmentOnFalse = (definiteAssignmentOnFalse | fc.DefiniteAssignmentOnFalse);
				if (constant != null && constant.IsDefaultValue)
				{
					fc.DefiniteAssignmentOnTrue = fc.DefiniteAssignmentOnFalse;
					return;
				}
				fc.DefiniteAssignmentOnTrue = new DefiniteAssignmentBitSet(definiteAssignmentOnTrue & (definiteAssignmentOnFalse | fc.DefiniteAssignmentOnTrue));
				return;
			}
			else
			{
				fc.DefiniteAssignmentOnTrue = (definiteAssignmentOnTrue | fc.DefiniteAssignmentOnTrue);
				if (constant != null && !constant.IsDefaultValue)
				{
					fc.DefiniteAssignmentOnFalse = fc.DefiniteAssignmentOnTrue;
					return;
				}
				fc.DefiniteAssignmentOnFalse = new DefiniteAssignmentBitSet((definiteAssignmentOnTrue | fc.DefiniteAssignmentOnFalse) & definiteAssignmentOnFalse);
				return;
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00077E64 File Offset: 0x00076064
		private string GetOperatorExpressionTypeName()
		{
			Binary.Operator @operator = this.oper;
			if (@operator <= Binary.Operator.Inequality)
			{
				if (@operator <= Binary.Operator.LeftShift)
				{
					switch (@operator)
					{
					case Binary.Operator.Multiply:
						if (!this.IsCompound)
						{
							return "Multiply";
						}
						return "MultiplyAssign";
					case Binary.Operator.Division:
						if (!this.IsCompound)
						{
							return "Divide";
						}
						return "DivideAssign";
					case Binary.Operator.Modulus:
						if (!this.IsCompound)
						{
							return "Modulo";
						}
						return "ModuloAssign";
					default:
						if (@operator == Binary.Operator.LeftShift)
						{
							if (!this.IsCompound)
							{
								return "LeftShift";
							}
							return "LeftShiftAssign";
						}
						break;
					}
				}
				else if (@operator != Binary.Operator.RightShift)
				{
					if (@operator == Binary.Operator.Equality)
					{
						return "Equal";
					}
					if (@operator == Binary.Operator.Inequality)
					{
						return "NotEqual";
					}
				}
				else
				{
					if (!this.IsCompound)
					{
						return "RightShift";
					}
					return "RightShiftAssign";
				}
			}
			else if (@operator <= Binary.Operator.LogicalOr)
			{
				switch (@operator)
				{
				case Binary.Operator.BitwiseAnd:
					if (!this.IsCompound)
					{
						return "And";
					}
					return "AndAssign";
				case Binary.Operator.ExclusiveOr:
					if (!this.IsCompound)
					{
						return "ExclusiveOr";
					}
					return "ExclusiveOrAssign";
				case Binary.Operator.BitwiseOr:
					if (!this.IsCompound)
					{
						return "Or";
					}
					return "OrAssign";
				default:
					if (@operator == Binary.Operator.LogicalAnd)
					{
						return "And";
					}
					if (@operator == Binary.Operator.LogicalOr)
					{
						return "Or";
					}
					break;
				}
			}
			else if (@operator != Binary.Operator.Addition)
			{
				if (@operator != Binary.Operator.Subtraction)
				{
					switch (@operator)
					{
					case Binary.Operator.LessThan:
						return "LessThan";
					case Binary.Operator.GreaterThan:
						return "GreaterThan";
					case Binary.Operator.LessThanOrEqual:
						return "LessThanOrEqual";
					case Binary.Operator.GreaterThanOrEqual:
						return "GreaterThanOrEqual";
					}
				}
				else
				{
					if (!this.IsCompound)
					{
						return "Subtract";
					}
					return "SubtractAssign";
				}
			}
			else
			{
				if (!this.IsCompound)
				{
					return "Add";
				}
				return "AddAssign";
			}
			throw new NotImplementedException("Unknown expression type operator " + this.oper.ToString());
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00078054 File Offset: 0x00076254
		private static Mono.CSharp.Operator.OpType ConvertBinaryToUserOperator(Binary.Operator op)
		{
			if (op <= Binary.Operator.Inequality)
			{
				if (op <= Binary.Operator.LeftShift)
				{
					switch (op)
					{
					case Binary.Operator.Multiply:
						return Mono.CSharp.Operator.OpType.Multiply;
					case Binary.Operator.Division:
						return Mono.CSharp.Operator.OpType.Division;
					case Binary.Operator.Modulus:
						return Mono.CSharp.Operator.OpType.Modulus;
					default:
						if (op == Binary.Operator.LeftShift)
						{
							return Mono.CSharp.Operator.OpType.LeftShift;
						}
						break;
					}
				}
				else
				{
					if (op == Binary.Operator.RightShift)
					{
						return Mono.CSharp.Operator.OpType.RightShift;
					}
					if (op == Binary.Operator.Equality)
					{
						return Mono.CSharp.Operator.OpType.Equality;
					}
					if (op == Binary.Operator.Inequality)
					{
						return Mono.CSharp.Operator.OpType.Inequality;
					}
				}
			}
			else
			{
				if (op <= Binary.Operator.LogicalOr)
				{
					switch (op)
					{
					case Binary.Operator.BitwiseAnd:
						break;
					case Binary.Operator.ExclusiveOr:
						return Mono.CSharp.Operator.OpType.ExclusiveOr;
					case Binary.Operator.BitwiseOr:
						return Mono.CSharp.Operator.OpType.BitwiseOr;
					default:
						if (op != Binary.Operator.LogicalAnd)
						{
							if (op != Binary.Operator.LogicalOr)
							{
								goto IL_DA;
							}
							return Mono.CSharp.Operator.OpType.BitwiseOr;
						}
						break;
					}
					return Mono.CSharp.Operator.OpType.BitwiseAnd;
				}
				if (op == Binary.Operator.Addition)
				{
					return Mono.CSharp.Operator.OpType.Addition;
				}
				if (op == Binary.Operator.Subtraction)
				{
					return Mono.CSharp.Operator.OpType.Subtraction;
				}
				switch (op)
				{
				case Binary.Operator.LessThan:
					return Mono.CSharp.Operator.OpType.LessThan;
				case Binary.Operator.GreaterThan:
					return Mono.CSharp.Operator.OpType.GreaterThan;
				case Binary.Operator.LessThanOrEqual:
					return Mono.CSharp.Operator.OpType.LessThanOrEqual;
				case Binary.Operator.GreaterThanOrEqual:
					return Mono.CSharp.Operator.OpType.GreaterThanOrEqual;
				}
			}
			IL_DA:
			throw new InternalErrorException(op.ToString());
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0007814D File Offset: 0x0007634D
		public override bool ContainsEmitWithAwait()
		{
			return this.left.ContainsEmitWithAwait() || this.right.ContainsEmitWithAwait();
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0007816C File Offset: 0x0007636C
		public static void EmitOperatorOpcode(EmitContext ec, Binary.Operator oper, TypeSpec l, Expression right)
		{
			OpCode opcode;
			if (oper <= Binary.Operator.Equality)
			{
				if (oper <= Binary.Operator.LeftShift)
				{
					switch (oper)
					{
					case Binary.Operator.Multiply:
						if (!ec.HasSet(BuilderContext.Options.CheckedScope))
						{
							opcode = OpCodes.Mul;
							goto IL_33E;
						}
						if (l.BuiltinType == BuiltinTypeSpec.Type.Int || l.BuiltinType == BuiltinTypeSpec.Type.Long)
						{
							opcode = OpCodes.Mul_Ovf;
							goto IL_33E;
						}
						if (!Binary.IsFloat(l))
						{
							opcode = OpCodes.Mul_Ovf_Un;
							goto IL_33E;
						}
						opcode = OpCodes.Mul;
						goto IL_33E;
					case Binary.Operator.Division:
						if (Binary.IsUnsigned(l))
						{
							opcode = OpCodes.Div_Un;
							goto IL_33E;
						}
						opcode = OpCodes.Div;
						goto IL_33E;
					case Binary.Operator.Modulus:
						if (Binary.IsUnsigned(l))
						{
							opcode = OpCodes.Rem_Un;
							goto IL_33E;
						}
						opcode = OpCodes.Rem;
						goto IL_33E;
					default:
						if (oper == Binary.Operator.LeftShift)
						{
							if (!(right is IntConstant))
							{
								ec.EmitInt(Binary.GetShiftMask(l));
								ec.Emit(OpCodes.And);
							}
							opcode = OpCodes.Shl;
							goto IL_33E;
						}
						break;
					}
				}
				else if (oper != Binary.Operator.RightShift)
				{
					if (oper == Binary.Operator.Equality)
					{
						opcode = OpCodes.Ceq;
						goto IL_33E;
					}
				}
				else
				{
					if (!(right is IntConstant))
					{
						ec.EmitInt(Binary.GetShiftMask(l));
						ec.Emit(OpCodes.And);
					}
					if (Binary.IsUnsigned(l))
					{
						opcode = OpCodes.Shr_Un;
						goto IL_33E;
					}
					opcode = OpCodes.Shr;
					goto IL_33E;
				}
			}
			else if (oper <= Binary.Operator.BitwiseOr)
			{
				if (oper == Binary.Operator.Inequality)
				{
					ec.Emit(OpCodes.Ceq);
					ec.EmitInt(0);
					opcode = OpCodes.Ceq;
					goto IL_33E;
				}
				switch (oper)
				{
				case Binary.Operator.BitwiseAnd:
					opcode = OpCodes.And;
					goto IL_33E;
				case Binary.Operator.ExclusiveOr:
					opcode = OpCodes.Xor;
					goto IL_33E;
				case Binary.Operator.BitwiseOr:
					opcode = OpCodes.Or;
					goto IL_33E;
				}
			}
			else if (oper != Binary.Operator.Addition)
			{
				if (oper != Binary.Operator.Subtraction)
				{
					switch (oper)
					{
					case Binary.Operator.LessThan:
						if (Binary.IsUnsigned(l))
						{
							opcode = OpCodes.Clt_Un;
							goto IL_33E;
						}
						opcode = OpCodes.Clt;
						goto IL_33E;
					case Binary.Operator.GreaterThan:
						if (Binary.IsUnsigned(l))
						{
							opcode = OpCodes.Cgt_Un;
							goto IL_33E;
						}
						opcode = OpCodes.Cgt;
						goto IL_33E;
					case Binary.Operator.LessThanOrEqual:
						if (Binary.IsUnsigned(l) || Binary.IsFloat(l))
						{
							ec.Emit(OpCodes.Cgt_Un);
						}
						else
						{
							ec.Emit(OpCodes.Cgt);
						}
						ec.EmitInt(0);
						opcode = OpCodes.Ceq;
						goto IL_33E;
					case Binary.Operator.GreaterThanOrEqual:
						if (Binary.IsUnsigned(l) || Binary.IsFloat(l))
						{
							ec.Emit(OpCodes.Clt_Un);
						}
						else
						{
							ec.Emit(OpCodes.Clt);
						}
						ec.EmitInt(0);
						opcode = OpCodes.Ceq;
						goto IL_33E;
					}
				}
				else
				{
					if (!ec.HasSet(BuilderContext.Options.CheckedScope))
					{
						opcode = OpCodes.Sub;
						goto IL_33E;
					}
					if (l.BuiltinType == BuiltinTypeSpec.Type.Int || l.BuiltinType == BuiltinTypeSpec.Type.Long)
					{
						opcode = OpCodes.Sub_Ovf;
						goto IL_33E;
					}
					if (!Binary.IsFloat(l))
					{
						opcode = OpCodes.Sub_Ovf_Un;
						goto IL_33E;
					}
					opcode = OpCodes.Sub;
					goto IL_33E;
				}
			}
			else
			{
				if (!ec.HasSet(BuilderContext.Options.CheckedScope))
				{
					opcode = OpCodes.Add;
					goto IL_33E;
				}
				if (l.BuiltinType == BuiltinTypeSpec.Type.Int || l.BuiltinType == BuiltinTypeSpec.Type.Long)
				{
					opcode = OpCodes.Add_Ovf;
					goto IL_33E;
				}
				if (!Binary.IsFloat(l))
				{
					opcode = OpCodes.Add_Ovf_Un;
					goto IL_33E;
				}
				opcode = OpCodes.Add;
				goto IL_33E;
			}
			throw new InternalErrorException(oper.ToString());
			IL_33E:
			ec.Emit(opcode);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000784BE File Offset: 0x000766BE
		private static int GetShiftMask(TypeSpec type)
		{
			if (type.BuiltinType != BuiltinTypeSpec.Type.Int && type.BuiltinType != BuiltinTypeSpec.Type.UInt)
			{
				return 63;
			}
			return 31;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000784D8 File Offset: 0x000766D8
		private static bool IsUnsigned(TypeSpec t)
		{
			switch (t.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.UInt:
			case BuiltinTypeSpec.Type.ULong:
				return true;
			}
			return t.IsPointer;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00078522 File Offset: 0x00076722
		private static bool IsFloat(TypeSpec t)
		{
			return t.BuiltinType == BuiltinTypeSpec.Type.Float || t.BuiltinType == BuiltinTypeSpec.Type.Double;
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0007853C File Offset: 0x0007673C
		public Expression ResolveOperator(ResolveContext rc)
		{
			this.eclass = ExprClass.Value;
			TypeSpec type = this.left.Type;
			TypeSpec type2 = this.right.Type;
			bool flag = false;
			Expression expression;
			if ((BuiltinTypeSpec.IsPrimitiveType(type) || (type.IsNullableType && BuiltinTypeSpec.IsPrimitiveType(NullableInfo.GetUnderlyingType(type)))) && (BuiltinTypeSpec.IsPrimitiveType(type2) || (type2.IsNullableType && BuiltinTypeSpec.IsPrimitiveType(NullableInfo.GetUnderlyingType(type2)))))
			{
				if ((this.oper & Binary.Operator.ShiftMask) == (Binary.Operator)0)
				{
					if (!this.DoBinaryOperatorPromotion(rc))
					{
						return null;
					}
					flag = (BuiltinTypeSpec.IsPrimitiveType(type) && BuiltinTypeSpec.IsPrimitiveType(type2));
				}
			}
			else
			{
				if (type.IsPointer || type2.IsPointer)
				{
					return this.ResolveOperatorPointer(rc, type, type2);
				}
				if ((this.state & Binary.State.UserOperatorsExcluded) == Binary.State.None)
				{
					expression = this.ResolveUserOperator(rc, this.left, this.right);
					if (expression != null)
					{
						return expression;
					}
				}
				bool isEnum = type.IsEnum;
				bool isEnum2 = type2.IsEnum;
				if ((this.oper & (Binary.Operator.ComparisonMask | Binary.Operator.BitwiseMask)) != (Binary.Operator)0)
				{
					if (Binary.IsEnumOrNullableEnum(type) || Binary.IsEnumOrNullableEnum(type2))
					{
						expression = this.ResolveSingleEnumOperators(rc, isEnum, isEnum2, type, type2);
						if (expression == null)
						{
							return null;
						}
						if ((this.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
						{
							expression = EmptyCast.Create(expression, this.type);
							this.enum_conversion = Binary.GetEnumResultCast(this.type);
							if (this.oper == Binary.Operator.BitwiseAnd && this.left.Type.IsEnum && this.right.Type.IsEnum)
							{
								expression = this.OptimizeAndOperation(expression);
							}
						}
						this.left = Binary.ConvertEnumOperandToUnderlyingType(rc, this.left, type2.IsNullableType);
						this.right = Binary.ConvertEnumOperandToUnderlyingType(rc, this.right, type.IsNullableType);
						return expression;
					}
				}
				else if (this.oper == Binary.Operator.Addition || this.oper == Binary.Operator.Subtraction)
				{
					if (Binary.IsEnumOrNullableEnum(type) || Binary.IsEnumOrNullableEnum(type2))
					{
						expression = this.ResolveEnumOperators(rc, isEnum, isEnum2, type, type2);
						if (expression != null)
						{
							this.left = Binary.ConvertEnumOperandToUnderlyingType(rc, this.left, false);
							this.right = Binary.ConvertEnumOperandToUnderlyingType(rc, this.right, false);
							return expression;
						}
					}
					else if (type.IsDelegate || type2.IsDelegate)
					{
						expression = this.ResolveOperatorDelegate(rc, type, type2);
						if (expression != null)
						{
							return expression;
						}
					}
				}
			}
			if ((this.oper & Binary.Operator.EqualityMask) != (Binary.Operator)0)
			{
				return this.ResolveEquality(rc, type, type2, flag);
			}
			expression = this.ResolveOperatorPredefined(rc, rc.BuiltinTypes.OperatorsBinaryStandard, flag);
			if (expression != null)
			{
				return expression;
			}
			if (flag)
			{
				return null;
			}
			return this.ResolveOperatorPredefined(rc, rc.Module.OperatorsBinaryLifted, false);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000787BB File Offset: 0x000769BB
		private static bool IsEnumOrNullableEnum(TypeSpec type)
		{
			return type.IsEnum || (type.IsNullableType && NullableInfo.GetUnderlyingType(type).IsEnum);
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000787DC File Offset: 0x000769DC
		private Constant EnumLiftUp(ResolveContext ec, Constant left, Constant right)
		{
			Binary.Operator @operator = this.oper;
			if (@operator <= Binary.Operator.Equality)
			{
				if (@operator <= Binary.Operator.LeftShift)
				{
					switch (@operator)
					{
					case Binary.Operator.Multiply:
					case Binary.Operator.Division:
					case Binary.Operator.Modulus:
						break;
					default:
						if (@operator != Binary.Operator.LeftShift)
						{
							goto IL_E6;
						}
						break;
					}
				}
				else if (@operator != Binary.Operator.RightShift)
				{
					if (@operator != Binary.Operator.Equality)
					{
						goto IL_E6;
					}
					goto IL_A3;
				}
				if (!right.Type.IsEnum && !left.Type.IsEnum)
				{
					return left;
				}
				goto IL_E6;
			}
			else if (@operator <= Binary.Operator.BitwiseOr)
			{
				if (@operator != Binary.Operator.Inequality)
				{
					switch (@operator)
					{
					case Binary.Operator.BitwiseAnd:
					case Binary.Operator.ExclusiveOr:
					case Binary.Operator.BitwiseOr:
						break;
					default:
						goto IL_E6;
					}
				}
			}
			else
			{
				if (@operator == Binary.Operator.Addition || @operator == Binary.Operator.Subtraction)
				{
					return left;
				}
				switch (@operator)
				{
				case Binary.Operator.LessThan:
				case Binary.Operator.GreaterThan:
				case Binary.Operator.LessThanOrEqual:
				case Binary.Operator.GreaterThanOrEqual:
					break;
				default:
					goto IL_E6;
				}
			}
			IL_A3:
			if (left.Type.IsEnum)
			{
				return left;
			}
			if (left.IsZeroInteger)
			{
				return left.Reduce(ec, right.Type);
			}
			IL_E6:
			return null;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000788D0 File Offset: 0x00076AD0
		private void CheckBitwiseOrOnSignExtended(ResolveContext ec)
		{
			OpcodeCast opcodeCast = this.left as OpcodeCast;
			if (opcodeCast != null && Binary.IsUnsigned(opcodeCast.UnderlyingType))
			{
				opcodeCast = null;
			}
			OpcodeCast opcodeCast2 = this.right as OpcodeCast;
			if (opcodeCast2 != null && Binary.IsUnsigned(opcodeCast2.UnderlyingType))
			{
				opcodeCast2 = null;
			}
			if (opcodeCast == null && opcodeCast2 == null)
			{
				return;
			}
			TypeSpec typeSpec = (opcodeCast != null) ? opcodeCast.UnderlyingType : opcodeCast2.UnderlyingType;
			ec.Report.Warning(675, 3, this.loc, "The operator `|' used on the sign-extended type `{0}'. Consider casting to a smaller unsigned type first", typeSpec.GetSignatureForError());
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00078954 File Offset: 0x00076B54
		public static Binary.PredefinedOperator[] CreatePointerOperatorsTable(BuiltinTypes types)
		{
			return new Binary.PredefinedOperator[]
			{
				new Binary.PredefinedPointerOperator(null, types.Int, Binary.Operator.AdditionMask | Binary.Operator.SubtractionMask),
				new Binary.PredefinedPointerOperator(null, types.UInt, Binary.Operator.AdditionMask | Binary.Operator.SubtractionMask),
				new Binary.PredefinedPointerOperator(null, types.Long, Binary.Operator.AdditionMask | Binary.Operator.SubtractionMask),
				new Binary.PredefinedPointerOperator(null, types.ULong, Binary.Operator.AdditionMask | Binary.Operator.SubtractionMask),
				new Binary.PredefinedPointerOperator(types.Int, null, Binary.Operator.AdditionMask, null),
				new Binary.PredefinedPointerOperator(types.UInt, null, Binary.Operator.AdditionMask, null),
				new Binary.PredefinedPointerOperator(types.Long, null, Binary.Operator.AdditionMask, null),
				new Binary.PredefinedPointerOperator(types.ULong, null, Binary.Operator.AdditionMask, null),
				new Binary.PredefinedPointerOperator(null, Binary.Operator.SubtractionMask, types.Long)
			};
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00078A20 File Offset: 0x00076C20
		public static Binary.PredefinedOperator[] CreateStandardOperatorsTable(BuiltinTypes types)
		{
			TypeSpec @bool = types.Bool;
			return new Binary.PredefinedOperator[]
			{
				new Binary.PredefinedOperator(types.Int, Binary.Operator.Multiply | Binary.Operator.ShiftMask | Binary.Operator.BitwiseMask),
				new Binary.PredefinedOperator(types.UInt, Binary.Operator.Multiply | Binary.Operator.BitwiseMask),
				new Binary.PredefinedOperator(types.Long, Binary.Operator.Multiply | Binary.Operator.BitwiseMask),
				new Binary.PredefinedOperator(types.ULong, Binary.Operator.Multiply | Binary.Operator.BitwiseMask),
				new Binary.PredefinedOperator(types.Float, Binary.Operator.Multiply),
				new Binary.PredefinedOperator(types.Double, Binary.Operator.Multiply),
				new Binary.PredefinedOperator(types.Decimal, Binary.Operator.Multiply),
				new Binary.PredefinedOperator(types.Int, Binary.Operator.ComparisonMask, @bool),
				new Binary.PredefinedOperator(types.UInt, Binary.Operator.ComparisonMask, @bool),
				new Binary.PredefinedOperator(types.Long, Binary.Operator.ComparisonMask, @bool),
				new Binary.PredefinedOperator(types.ULong, Binary.Operator.ComparisonMask, @bool),
				new Binary.PredefinedOperator(types.Float, Binary.Operator.ComparisonMask, @bool),
				new Binary.PredefinedOperator(types.Double, Binary.Operator.ComparisonMask, @bool),
				new Binary.PredefinedOperator(types.Decimal, Binary.Operator.ComparisonMask, @bool),
				new Binary.PredefinedStringOperator(types.String, Binary.Operator.AdditionMask, types.String),
				new Binary.PredefinedOperator(@bool, Binary.Operator.EqualityMask | Binary.Operator.BitwiseMask | Binary.Operator.LogicalMask, @bool),
				new Binary.PredefinedOperator(types.UInt, types.Int, Binary.Operator.ShiftMask),
				new Binary.PredefinedOperator(types.Long, types.Int, Binary.Operator.ShiftMask),
				new Binary.PredefinedOperator(types.ULong, types.Int, Binary.Operator.ShiftMask)
			};
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00078BB8 File Offset: 0x00076DB8
		public static Binary.PredefinedOperator[] CreateStandardLiftedOperatorsTable(ModuleContainer module)
		{
			BuiltinTypes builtinTypes = module.Compiler.BuiltinTypes;
			Binary.PredefinedStringOperator[] array = new Binary.PredefinedStringOperator[]
			{
				new Binary.PredefinedStringOperator(builtinTypes.String, builtinTypes.Object, Binary.Operator.AdditionMask, builtinTypes.String),
				new Binary.PredefinedStringOperator(builtinTypes.Object, builtinTypes.String, Binary.Operator.AdditionMask, builtinTypes.String)
			};
			TypeSpec typeSpec = module.PredefinedTypes.Nullable.TypeSpec;
			if (typeSpec == null)
			{
				return array;
			}
			BuiltinTypeSpec @bool = builtinTypes.Bool;
			InflatedTypeSpec inflatedTypeSpec = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				@bool
			});
			InflatedTypeSpec inflatedTypeSpec2 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Int
			});
			InflatedTypeSpec inflatedTypeSpec3 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.UInt
			});
			InflatedTypeSpec inflatedTypeSpec4 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Long
			});
			InflatedTypeSpec inflatedTypeSpec5 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.ULong
			});
			InflatedTypeSpec type = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Float
			});
			InflatedTypeSpec type2 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Double
			});
			InflatedTypeSpec type3 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Decimal
			});
			return new Binary.PredefinedOperator[]
			{
				new Binary.PredefinedOperator(inflatedTypeSpec2, Binary.Operator.Multiply | Binary.Operator.ShiftMask | Binary.Operator.BitwiseMask | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(inflatedTypeSpec3, Binary.Operator.Multiply | Binary.Operator.BitwiseMask | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(inflatedTypeSpec4, Binary.Operator.Multiply | Binary.Operator.BitwiseMask | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(inflatedTypeSpec5, Binary.Operator.Multiply | Binary.Operator.BitwiseMask | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(type, Binary.Operator.Multiply | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(type2, Binary.Operator.Multiply | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(type3, Binary.Operator.Multiply | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(inflatedTypeSpec2, Binary.Operator.ComparisonMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(inflatedTypeSpec3, Binary.Operator.ComparisonMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(inflatedTypeSpec4, Binary.Operator.ComparisonMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(inflatedTypeSpec5, Binary.Operator.ComparisonMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type, Binary.Operator.ComparisonMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type2, Binary.Operator.ComparisonMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type3, Binary.Operator.ComparisonMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(inflatedTypeSpec, Binary.Operator.BitwiseMask | Binary.Operator.NullableMask, inflatedTypeSpec),
				new Binary.PredefinedOperator(inflatedTypeSpec3, inflatedTypeSpec2, Binary.Operator.ShiftMask | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(inflatedTypeSpec4, inflatedTypeSpec2, Binary.Operator.ShiftMask | Binary.Operator.NullableMask),
				new Binary.PredefinedOperator(inflatedTypeSpec5, inflatedTypeSpec2, Binary.Operator.ShiftMask | Binary.Operator.NullableMask),
				array[0],
				array[1]
			};
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00078E2C File Offset: 0x0007702C
		public static Binary.PredefinedOperator[] CreateEqualityOperatorsTable(BuiltinTypes types)
		{
			TypeSpec @bool = types.Bool;
			return new Binary.PredefinedOperator[]
			{
				new Binary.PredefinedEqualityOperator(types.String, @bool),
				new Binary.PredefinedEqualityOperator(types.Delegate, @bool),
				new Binary.PredefinedOperator(@bool, Binary.Operator.EqualityMask, @bool),
				new Binary.PredefinedOperator(types.Int, Binary.Operator.EqualityMask, @bool),
				new Binary.PredefinedOperator(types.UInt, Binary.Operator.EqualityMask, @bool),
				new Binary.PredefinedOperator(types.Long, Binary.Operator.EqualityMask, @bool),
				new Binary.PredefinedOperator(types.ULong, Binary.Operator.EqualityMask, @bool),
				new Binary.PredefinedOperator(types.Float, Binary.Operator.EqualityMask, @bool),
				new Binary.PredefinedOperator(types.Double, Binary.Operator.EqualityMask, @bool),
				new Binary.PredefinedOperator(types.Decimal, Binary.Operator.EqualityMask, @bool)
			};
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x00078F04 File Offset: 0x00077104
		public static Binary.PredefinedOperator[] CreateEqualityLiftedOperatorsTable(ModuleContainer module)
		{
			TypeSpec typeSpec = module.PredefinedTypes.Nullable.TypeSpec;
			if (typeSpec == null)
			{
				return new Binary.PredefinedOperator[0];
			}
			BuiltinTypes builtinTypes = module.Compiler.BuiltinTypes;
			BuiltinTypeSpec @bool = builtinTypes.Bool;
			InflatedTypeSpec type = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				@bool
			});
			InflatedTypeSpec type2 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Int
			});
			InflatedTypeSpec type3 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.UInt
			});
			InflatedTypeSpec type4 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Long
			});
			InflatedTypeSpec type5 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.ULong
			});
			InflatedTypeSpec type6 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Float
			});
			InflatedTypeSpec type7 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Double
			});
			InflatedTypeSpec type8 = typeSpec.MakeGenericType(module, new BuiltinTypeSpec[]
			{
				builtinTypes.Decimal
			});
			return new Binary.PredefinedOperator[]
			{
				new Binary.PredefinedOperator(type, Binary.Operator.EqualityMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type2, Binary.Operator.EqualityMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type3, Binary.Operator.EqualityMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type4, Binary.Operator.EqualityMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type5, Binary.Operator.EqualityMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type6, Binary.Operator.EqualityMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type7, Binary.Operator.EqualityMask | Binary.Operator.NullableMask, @bool),
				new Binary.PredefinedOperator(type8, Binary.Operator.EqualityMask | Binary.Operator.NullableMask, @bool)
			};
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x00079080 File Offset: 0x00077280
		private bool DoBinaryOperatorPromotion(ResolveContext rc)
		{
			TypeSpec typeSpec = this.left.Type;
			if (typeSpec.IsNullableType)
			{
				typeSpec = NullableInfo.GetUnderlyingType(typeSpec);
			}
			if (typeSpec.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive)
			{
				return true;
			}
			TypeSpec typeSpec2 = this.right.Type;
			if (typeSpec2.IsNullableType)
			{
				typeSpec2 = NullableInfo.GetUnderlyingType(typeSpec2);
			}
			BuiltinTypeSpec.Type builtinType = typeSpec.BuiltinType;
			BuiltinTypeSpec.Type builtinType2 = typeSpec2.BuiltinType;
			TypeSpec typeSpec3;
			if (builtinType == BuiltinTypeSpec.Type.Decimal || builtinType2 == BuiltinTypeSpec.Type.Decimal)
			{
				typeSpec3 = rc.BuiltinTypes.Decimal;
			}
			else if (builtinType == BuiltinTypeSpec.Type.Double || builtinType2 == BuiltinTypeSpec.Type.Double)
			{
				typeSpec3 = rc.BuiltinTypes.Double;
			}
			else if (builtinType == BuiltinTypeSpec.Type.Float || builtinType2 == BuiltinTypeSpec.Type.Float)
			{
				typeSpec3 = rc.BuiltinTypes.Float;
			}
			else if (builtinType == BuiltinTypeSpec.Type.ULong || builtinType2 == BuiltinTypeSpec.Type.ULong)
			{
				typeSpec3 = rc.BuiltinTypes.ULong;
				if (Binary.IsSignedType(builtinType))
				{
					Expression expression = Binary.ConvertSignedConstant(this.left, typeSpec3);
					if (expression == null)
					{
						return false;
					}
					this.left = expression;
				}
				else if (Binary.IsSignedType(builtinType2))
				{
					Expression expression = Binary.ConvertSignedConstant(this.right, typeSpec3);
					if (expression == null)
					{
						return false;
					}
					this.right = expression;
				}
			}
			else if (builtinType == BuiltinTypeSpec.Type.Long || builtinType2 == BuiltinTypeSpec.Type.Long)
			{
				typeSpec3 = rc.BuiltinTypes.Long;
			}
			else if (builtinType == BuiltinTypeSpec.Type.UInt || builtinType2 == BuiltinTypeSpec.Type.UInt)
			{
				typeSpec3 = rc.BuiltinTypes.UInt;
				if (Binary.IsSignedType(builtinType))
				{
					if (Binary.ConvertSignedConstant(this.left, typeSpec3) == null)
					{
						typeSpec3 = rc.BuiltinTypes.Long;
					}
				}
				else if (Binary.IsSignedType(builtinType2) && Binary.ConvertSignedConstant(this.right, typeSpec3) == null)
				{
					typeSpec3 = rc.BuiltinTypes.Long;
				}
			}
			else
			{
				typeSpec3 = rc.BuiltinTypes.Int;
			}
			if (typeSpec != typeSpec3)
			{
				Expression expression = Binary.PromoteExpression(rc, this.left, typeSpec3);
				if (expression == null)
				{
					return false;
				}
				this.left = expression;
			}
			if (typeSpec2 != typeSpec3)
			{
				Expression expression = Binary.PromoteExpression(rc, this.right, typeSpec3);
				if (expression == null)
				{
					return false;
				}
				this.right = expression;
			}
			return true;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00079274 File Offset: 0x00077474
		private static bool IsSignedType(BuiltinTypeSpec.Type type)
		{
			switch (type)
			{
			case BuiltinTypeSpec.Type.SByte:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.Long:
				return true;
			}
			return false;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000792A0 File Offset: 0x000774A0
		private static Expression ConvertSignedConstant(Expression expr, TypeSpec type)
		{
			Constant constant = expr as Constant;
			if (constant == null)
			{
				return null;
			}
			return constant.ConvertImplicitly(type);
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000792C0 File Offset: 0x000774C0
		private static Expression PromoteExpression(ResolveContext rc, Expression expr, TypeSpec type)
		{
			if (expr.Type.IsNullableType)
			{
				return Convert.ImplicitConversionStandard(rc, expr, rc.Module.PredefinedTypes.Nullable.TypeSpec.MakeGenericType(rc, new TypeSpec[]
				{
					type
				}), expr.Location);
			}
			Constant constant = expr as Constant;
			if (constant != null)
			{
				return constant.ConvertImplicitly(type);
			}
			return Convert.ImplicitNumericConversion(expr, type);
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00079328 File Offset: 0x00077528
		protected override Expression DoResolve(ResolveContext ec)
		{
			if (this.left == null)
			{
				return null;
			}
			if (this.oper == Binary.Operator.Subtraction && this.left is ParenthesizedExpression)
			{
				this.left = ((ParenthesizedExpression)this.left).Expr;
				this.left = this.left.Resolve(ec, ResolveFlags.VariableOrValue | ResolveFlags.Type);
				if (this.left == null)
				{
					return null;
				}
				if (this.left.eclass == ExprClass.Type)
				{
					ec.Report.Error(75, this.loc, "To cast a negative value, you must enclose the value in parentheses");
					return null;
				}
			}
			else
			{
				this.left = this.left.Resolve(ec);
			}
			if (this.left == null)
			{
				return null;
			}
			this.right = this.right.Resolve(ec);
			if (this.right == null)
			{
				return null;
			}
			Constant constant = this.left as Constant;
			Constant constant2 = this.right as Constant;
			if (!ec.HasSet(ResolveContext.Options.EnumScope) && constant != null && constant2 != null && (this.left.Type.IsEnum || this.right.Type.IsEnum))
			{
				constant = this.EnumLiftUp(ec, constant, constant2);
				if (constant != null)
				{
					constant2 = this.EnumLiftUp(ec, constant2, constant);
				}
			}
			if (constant2 != null && constant != null)
			{
				int errors = ec.Report.Errors;
				Expression expression = ConstantFold.BinaryFold(ec, this.oper, constant, constant2, this.loc);
				if (expression != null || ec.Report.Errors != errors)
				{
					return expression;
				}
			}
			if ((this.oper & Binary.Operator.ComparisonMask) != (Binary.Operator)0)
			{
				if (this.left.Equals(this.right))
				{
					ec.Report.Warning(1718, 3, this.loc, "A comparison made to same variable. Did you mean to compare something else?");
				}
				this.CheckOutOfRangeComparison(ec, constant, this.right.Type);
				this.CheckOutOfRangeComparison(ec, constant2, this.left.Type);
			}
			if (this.left.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic || this.right.Type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				return this.DoResolveDynamic(ec);
			}
			return this.DoResolveCore(ec, this.left, this.right);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00079534 File Offset: 0x00077734
		private Expression DoResolveDynamic(ResolveContext rc)
		{
			TypeSpec type = this.left.Type;
			TypeSpec type2 = this.right.Type;
			if (type.Kind == MemberKind.Void || type == InternalType.MethodGroup || type == InternalType.AnonymousMethod || type2.Kind == MemberKind.Void || type2 == InternalType.MethodGroup || type2 == InternalType.AnonymousMethod)
			{
				this.Error_OperatorCannotBeApplied(rc, this.left, this.right);
				return null;
			}
			Arguments arguments;
			if ((this.oper & Binary.Operator.LogicalMask) != (Binary.Operator)0)
			{
				arguments = new Arguments(2);
				Expression expr;
				Expression true_expr;
				Expression false_expr;
				if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					LocalVariable localVariable = LocalVariable.CreateCompilerGenerated(type, rc.CurrentBlock, this.loc);
					Arguments arguments2 = new Arguments(1);
					arguments2.Add(new Argument(new SimpleAssign(localVariable.CreateReferenceExpression(rc, this.loc), this.left).Resolve(rc)));
					this.left = localVariable.CreateReferenceExpression(rc, this.loc);
					if (this.oper == Binary.Operator.LogicalAnd)
					{
						expr = DynamicUnaryConversion.CreateIsFalse(rc, arguments2, this.loc);
						true_expr = this.left;
					}
					else
					{
						expr = DynamicUnaryConversion.CreateIsTrue(rc, arguments2, this.loc);
						true_expr = this.left;
					}
					arguments.Add(new Argument(this.left));
					arguments.Add(new Argument(this.right));
					false_expr = new DynamicExpressionStatement(this, arguments, this.loc);
				}
				else
				{
					LocalVariable localVariable2 = LocalVariable.CreateCompilerGenerated(rc.BuiltinTypes.Bool, rc.CurrentBlock, this.loc);
					if (!Convert.ImplicitConversionExists(rc, this.left, localVariable2.Type) && ((this.oper == Binary.Operator.LogicalAnd) ? Expression.GetOperatorFalse(rc, this.left, this.loc) : Expression.GetOperatorTrue(rc, this.left, this.loc)) == null)
					{
						rc.Report.Error(7083, this.left.Location, "Expression must be implicitly convertible to Boolean or its type `{0}' must define operator `{1}'", type.GetSignatureForError(), (this.oper == Binary.Operator.LogicalAnd) ? "false" : "true");
						return null;
					}
					arguments.Add(new Argument(localVariable2.CreateReferenceExpression(rc, this.loc).Resolve(rc)));
					arguments.Add(new Argument(this.right));
					this.right = new DynamicExpressionStatement(this, arguments, this.loc);
					if (this.oper == Binary.Operator.LogicalAnd)
					{
						true_expr = this.right;
						false_expr = localVariable2.CreateReferenceExpression(rc, this.loc);
					}
					else
					{
						true_expr = localVariable2.CreateReferenceExpression(rc, this.loc);
						false_expr = this.right;
					}
					expr = new BooleanExpression(new SimpleAssign(localVariable2.CreateReferenceExpression(rc, this.loc), this.left));
				}
				return new Conditional(expr, true_expr, false_expr, this.loc).Resolve(rc);
			}
			arguments = new Arguments(2);
			arguments.Add(new Argument(this.left));
			arguments.Add(new Argument(this.right));
			return new DynamicExpressionStatement(this, arguments, this.loc).Resolve(rc);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00079838 File Offset: 0x00077A38
		private Expression DoResolveCore(ResolveContext ec, Expression left_orig, Expression right_orig)
		{
			Expression expression = this.ResolveOperator(ec);
			if (expression == null)
			{
				this.Error_OperatorCannotBeApplied(ec, left_orig, right_orig);
			}
			if (this.left == null || this.right == null)
			{
				throw new InternalErrorException("Invalid conversion");
			}
			if (this.oper == Binary.Operator.BitwiseOr)
			{
				this.CheckBitwiseOrOnSignExtended(ec);
			}
			return expression;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00079887 File Offset: 0x00077A87
		public override Expression MakeExpression(BuilderContext ctx)
		{
			return this.MakeExpression(ctx, this.left, this.right);
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0007989C File Offset: 0x00077A9C
		public Expression MakeExpression(BuilderContext ctx, Expression left, Expression right)
		{
			Expression expression = left.MakeExpression(ctx);
			Expression expression2 = right.MakeExpression(ctx);
			bool flag = ctx.HasSet(BuilderContext.Options.CheckedScope);
			Binary.Operator @operator = this.oper;
			if (@operator <= Binary.Operator.Inequality)
			{
				if (@operator <= Binary.Operator.LeftShift)
				{
					switch (@operator)
					{
					case Binary.Operator.Multiply:
						if (!flag)
						{
							return Expression.Multiply(expression, expression2);
						}
						return Expression.MultiplyChecked(expression, expression2);
					case Binary.Operator.Division:
						return Expression.Divide(expression, expression2);
					case Binary.Operator.Modulus:
						return Expression.Modulo(expression, expression2);
					default:
						if (@operator == Binary.Operator.LeftShift)
						{
							return Expression.LeftShift(expression, expression2);
						}
						break;
					}
				}
				else
				{
					if (@operator == Binary.Operator.RightShift)
					{
						return Expression.RightShift(expression, expression2);
					}
					if (@operator == Binary.Operator.Equality)
					{
						return Expression.Equal(expression, expression2);
					}
					if (@operator == Binary.Operator.Inequality)
					{
						return Expression.NotEqual(expression, expression2);
					}
				}
			}
			else if (@operator <= Binary.Operator.LogicalOr)
			{
				switch (@operator)
				{
				case Binary.Operator.BitwiseAnd:
					return Expression.And(expression, expression2);
				case Binary.Operator.ExclusiveOr:
					return Expression.ExclusiveOr(expression, expression2);
				case Binary.Operator.BitwiseOr:
					return Expression.Or(expression, expression2);
				default:
					if (@operator == Binary.Operator.LogicalAnd)
					{
						return Expression.AndAlso(expression, expression2);
					}
					if (@operator == Binary.Operator.LogicalOr)
					{
						return Expression.OrElse(expression, expression2);
					}
					break;
				}
			}
			else if (@operator != Binary.Operator.Addition)
			{
				if (@operator != Binary.Operator.Subtraction)
				{
					switch (@operator)
					{
					case Binary.Operator.LessThan:
						return Expression.LessThan(expression, expression2);
					case Binary.Operator.GreaterThan:
						return Expression.GreaterThan(expression, expression2);
					case Binary.Operator.LessThanOrEqual:
						return Expression.LessThanOrEqual(expression, expression2);
					case Binary.Operator.GreaterThanOrEqual:
						return Expression.GreaterThanOrEqual(expression, expression2);
					}
				}
				else
				{
					if (!flag)
					{
						return Expression.Subtract(expression, expression2);
					}
					return Expression.SubtractChecked(expression, expression2);
				}
			}
			else
			{
				if (!flag)
				{
					return Expression.Add(expression, expression2);
				}
				return Expression.AddChecked(expression, expression2);
			}
			throw new NotImplementedException(this.oper.ToString());
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00079A54 File Offset: 0x00077C54
		private Expression ResolveOperatorDelegate(ResolveContext ec, TypeSpec l, TypeSpec r)
		{
			if (l != r && !TypeSpecComparer.Variant.IsEqual(r, l))
			{
				if (this.right.eclass == ExprClass.MethodGroup || r == InternalType.AnonymousMethod || r == InternalType.NullLiteral)
				{
					Expression expression = Convert.ImplicitConversionRequired(ec, this.right, l, this.loc);
					if (expression == null)
					{
						return null;
					}
					this.right = expression;
					r = this.right.Type;
				}
				else
				{
					if (this.left.eclass != ExprClass.MethodGroup && l != InternalType.AnonymousMethod && l != InternalType.NullLiteral)
					{
						return null;
					}
					Expression expression = Convert.ImplicitConversionRequired(ec, this.left, r, this.loc);
					if (expression == null)
					{
						return null;
					}
					this.left = expression;
					l = this.left.Type;
				}
			}
			MethodSpec methodSpec = null;
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this.left));
			arguments.Add(new Argument(this.right));
			if (this.oper == Binary.Operator.Addition)
			{
				methodSpec = ec.Module.PredefinedMembers.DelegateCombine.Resolve(this.loc);
			}
			else if (this.oper == Binary.Operator.Subtraction)
			{
				methodSpec = ec.Module.PredefinedMembers.DelegateRemove.Resolve(this.loc);
			}
			if (methodSpec == null)
			{
				return new EmptyExpression(ec.BuiltinTypes.Decimal);
			}
			return new ClassCast(new UserOperatorCall(methodSpec, arguments, new Func<ResolveContext, Expression, Expression>(this.CreateExpressionTree), this.loc), l);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00079BC4 File Offset: 0x00077DC4
		private Expression ResolveSingleEnumOperators(ResolveContext rc, bool lenum, bool renum, TypeSpec ltype, TypeSpec rtype)
		{
			if ((this.oper & Binary.Operator.ComparisonMask) != (Binary.Operator)0)
			{
				this.type = rc.BuiltinTypes.Bool;
			}
			else if (lenum)
			{
				this.type = ltype;
			}
			else if (renum)
			{
				this.type = rtype;
			}
			else if (ltype.IsNullableType && NullableInfo.GetUnderlyingType(ltype).IsEnum)
			{
				this.type = ltype;
			}
			else
			{
				this.type = rtype;
			}
			if (ltype != rtype)
			{
				if (renum && !ltype.IsNullableType)
				{
					Expression expression = Convert.ImplicitConversion(rc, this.left, rtype, this.loc);
					if (expression != null)
					{
						this.left = expression;
						return this;
					}
				}
				else if (lenum && !rtype.IsNullableType)
				{
					Expression expression = Convert.ImplicitConversion(rc, this.right, ltype, this.loc);
					if (expression != null)
					{
						this.right = expression;
						return this;
					}
				}
				TypeSpec typeSpec = rc.Module.PredefinedTypes.Nullable.TypeSpec;
				if (typeSpec != null)
				{
					if (renum && !ltype.IsNullableType)
					{
						InflatedTypeSpec inflatedTypeSpec = typeSpec.MakeGenericType(rc.Module, new TypeSpec[]
						{
							rtype
						});
						Expression expression = Convert.ImplicitConversion(rc, this.left, inflatedTypeSpec, this.loc);
						if (expression != null)
						{
							this.left = expression;
							this.right = Convert.ImplicitConversion(rc, this.right, inflatedTypeSpec, this.loc);
						}
						if ((this.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
						{
							this.type = inflatedTypeSpec;
						}
						if (this.left.IsNull)
						{
							if ((this.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
							{
								return LiftedNull.CreateFromExpression(rc, this);
							}
							return this.CreateLiftedValueTypeResult(rc, rtype);
						}
						else if (expression != null)
						{
							return new LiftedBinaryOperator(this)
							{
								Left = expression,
								Right = this.right
							}.Resolve(rc);
						}
					}
					else if (lenum && !rtype.IsNullableType)
					{
						InflatedTypeSpec inflatedTypeSpec2 = typeSpec.MakeGenericType(rc.Module, new TypeSpec[]
						{
							ltype
						});
						Expression expression = Convert.ImplicitConversion(rc, this.right, inflatedTypeSpec2, this.loc);
						if (expression != null)
						{
							this.right = expression;
							this.left = Convert.ImplicitConversion(rc, this.left, inflatedTypeSpec2, this.loc);
						}
						if ((this.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
						{
							this.type = inflatedTypeSpec2;
						}
						if (this.right.IsNull)
						{
							if ((this.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
							{
								return LiftedNull.CreateFromExpression(rc, this);
							}
							return this.CreateLiftedValueTypeResult(rc, ltype);
						}
						else if (expression != null)
						{
							return new LiftedBinaryOperator(this)
							{
								Left = this.left,
								Right = expression
							}.Resolve(rc);
						}
					}
					else if (rtype.IsNullableType && NullableInfo.GetUnderlyingType(rtype).IsEnum)
					{
						Unwrap unwrapRight = null;
						Expression expression;
						if (this.left.IsNull || this.right.IsNull)
						{
							if (rc.HasSet(ResolveContext.Options.ExpressionTreeConversion))
							{
								this.left = Convert.ImplicitConversion(rc, this.left, rtype, this.left.Location);
							}
							if ((this.oper & Binary.Operator.RelationalMask) != (Binary.Operator)0)
							{
								return this.CreateLiftedValueTypeResult(rc, rtype);
							}
							if ((this.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
							{
								return LiftedNull.CreateFromExpression(rc, this);
							}
							if (this.right.IsNull)
							{
								return this.CreateLiftedValueTypeResult(rc, this.left.Type);
							}
							expression = this.left;
							unwrapRight = new Unwrap(this.right, true);
						}
						else
						{
							expression = Convert.ImplicitConversion(rc, this.left, NullableInfo.GetUnderlyingType(rtype), this.loc);
							if (expression == null)
							{
								return null;
							}
						}
						if (expression != null)
						{
							return new LiftedBinaryOperator(this)
							{
								Left = expression,
								Right = this.right,
								UnwrapRight = unwrapRight
							}.Resolve(rc);
						}
					}
					else if (ltype.IsNullableType && NullableInfo.GetUnderlyingType(ltype).IsEnum)
					{
						Unwrap unwrapLeft = null;
						Expression expression;
						if (this.right.IsNull || this.left.IsNull)
						{
							if (rc.HasSet(ResolveContext.Options.ExpressionTreeConversion))
							{
								this.right = Convert.ImplicitConversion(rc, this.right, ltype, this.right.Location);
							}
							if ((this.oper & Binary.Operator.RelationalMask) != (Binary.Operator)0)
							{
								return this.CreateLiftedValueTypeResult(rc, ltype);
							}
							if ((this.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
							{
								return LiftedNull.CreateFromExpression(rc, this);
							}
							if (this.left.IsNull)
							{
								return this.CreateLiftedValueTypeResult(rc, this.right.Type);
							}
							expression = this.right;
							unwrapLeft = new Unwrap(this.left, true);
						}
						else
						{
							expression = Convert.ImplicitConversion(rc, this.right, NullableInfo.GetUnderlyingType(ltype), this.loc);
							if (expression == null)
							{
								return null;
							}
						}
						if (expression != null)
						{
							return new LiftedBinaryOperator(this)
							{
								Left = this.left,
								UnwrapLeft = unwrapLeft,
								Right = expression
							}.Resolve(rc);
						}
					}
				}
				return null;
			}
			if (lenum || renum)
			{
				return this;
			}
			return new LiftedBinaryOperator(this)
			{
				Left = this.left,
				Right = this.right
			}.Resolve(rc);
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0007A0A4 File Offset: 0x000782A4
		private static Expression ConvertEnumOperandToUnderlyingType(ResolveContext rc, Expression expr, bool liftType)
		{
			TypeSpec typeSpec;
			if (expr.Type.IsNullableType)
			{
				TypeSpec underlyingType = NullableInfo.GetUnderlyingType(expr.Type);
				if (underlyingType.IsEnum)
				{
					typeSpec = EnumSpec.GetUnderlyingType(underlyingType);
				}
				else
				{
					typeSpec = underlyingType;
				}
			}
			else if (expr.Type.IsEnum)
			{
				typeSpec = EnumSpec.GetUnderlyingType(expr.Type);
			}
			else
			{
				typeSpec = expr.Type;
			}
			switch (typeSpec.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
				typeSpec = rc.BuiltinTypes.Int;
				break;
			}
			if (expr.Type.IsNullableType || liftType)
			{
				typeSpec = rc.Module.PredefinedTypes.Nullable.TypeSpec.MakeGenericType(rc.Module, new TypeSpec[]
				{
					typeSpec
				});
			}
			if (expr.Type == typeSpec)
			{
				return expr;
			}
			return EmptyCast.Create(expr, typeSpec);
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0007A180 File Offset: 0x00078380
		private Expression ResolveEnumOperators(ResolveContext rc, bool lenum, bool renum, TypeSpec ltype, TypeSpec rtype)
		{
			TypeSpec typeSpec;
			if (lenum)
			{
				typeSpec = ltype;
			}
			else if (renum)
			{
				typeSpec = rtype;
			}
			else if (ltype.IsNullableType && NullableInfo.GetUnderlyingType(ltype).IsEnum)
			{
				typeSpec = ltype;
			}
			else
			{
				typeSpec = rtype;
			}
			Expression expression;
			if (!typeSpec.IsNullableType)
			{
				expression = this.ResolveOperatorPredefined(rc, rc.Module.GetPredefinedEnumAritmeticOperators(typeSpec, false), false);
				if (expression != null)
				{
					if (this.oper == Binary.Operator.Subtraction)
					{
						expression = this.ConvertEnumSubtractionResult(rc, expression);
					}
					else
					{
						expression = Binary.ConvertEnumAdditionalResult(expression, typeSpec);
					}
					this.enum_conversion = Binary.GetEnumResultCast(expression.Type);
					return expression;
				}
				PredefinedType nullable = rc.Module.PredefinedTypes.Nullable;
				if (!nullable.IsDefined)
				{
					return null;
				}
				typeSpec = nullable.TypeSpec.MakeGenericType(rc.Module, new TypeSpec[]
				{
					typeSpec
				});
			}
			expression = this.ResolveOperatorPredefined(rc, rc.Module.GetPredefinedEnumAritmeticOperators(typeSpec, true), false);
			if (expression != null)
			{
				if (this.oper == Binary.Operator.Subtraction)
				{
					expression = this.ConvertEnumSubtractionResult(rc, expression);
				}
				else
				{
					expression = Binary.ConvertEnumAdditionalResult(expression, typeSpec);
				}
				this.enum_conversion = Binary.GetEnumResultCast(expression.Type);
			}
			return expression;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0007A295 File Offset: 0x00078495
		private static Expression ConvertEnumAdditionalResult(Expression expr, TypeSpec enumType)
		{
			return EmptyCast.Create(expr, enumType);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0007A2A0 File Offset: 0x000784A0
		private Expression ConvertEnumSubtractionResult(ResolveContext rc, Expression expr)
		{
			TypeSpec typeSpec;
			if (this.left.Type == this.right.Type)
			{
				EnumConstant enumConstant = this.right as EnumConstant;
				if (enumConstant != null && enumConstant.IsZeroInteger && !this.right.Type.IsEnum)
				{
					typeSpec = this.left.Type;
				}
				else
				{
					typeSpec = (this.left.Type.IsNullableType ? NullableInfo.GetEnumUnderlyingType(rc.Module, this.left.Type) : EnumSpec.GetUnderlyingType(this.left.Type));
				}
			}
			else
			{
				if (Binary.IsEnumOrNullableEnum(this.left.Type))
				{
					typeSpec = this.left.Type;
				}
				else
				{
					typeSpec = this.right.Type;
				}
				if (expr is LiftedBinaryOperator && !typeSpec.IsNullableType)
				{
					typeSpec = rc.Module.PredefinedTypes.Nullable.TypeSpec.MakeGenericType(rc.Module, new TypeSpec[]
					{
						typeSpec
					});
				}
			}
			return EmptyCast.Create(expr, typeSpec);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0007A3AC File Offset: 0x000785AC
		public static ConvCast.Mode GetEnumResultCast(TypeSpec type)
		{
			if (type.IsNullableType)
			{
				type = NullableInfo.GetUnderlyingType(type);
			}
			if (type.IsEnum)
			{
				type = EnumSpec.GetUnderlyingType(type);
			}
			switch (type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				return ConvCast.Mode.I4_U1;
			case BuiltinTypeSpec.Type.SByte:
				return ConvCast.Mode.I4_I1;
			case BuiltinTypeSpec.Type.Short:
				return ConvCast.Mode.I4_I2;
			case BuiltinTypeSpec.Type.UShort:
				return ConvCast.Mode.I4_U2;
			}
			return ConvCast.Mode.I1_U1;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0007A40C File Offset: 0x0007860C
		private Expression ResolveEquality(ResolveContext ec, TypeSpec l, TypeSpec r, bool primitives_only)
		{
			this.type = ec.BuiltinTypes.Bool;
			bool flag = false;
			if (!primitives_only)
			{
				TypeParameterSpec typeParameterSpec = l as TypeParameterSpec;
				TypeParameterSpec typeParameterSpec2 = r as TypeParameterSpec;
				if (typeParameterSpec != null)
				{
					if (this.right is NullLiteral)
					{
						if (typeParameterSpec.GetEffectiveBase().BuiltinType == BuiltinTypeSpec.Type.ValueType)
						{
							return null;
						}
						this.left = new BoxedCast(this.left, ec.BuiltinTypes.Object);
						return this;
					}
					else
					{
						if (!typeParameterSpec.IsReferenceType)
						{
							return null;
						}
						l = typeParameterSpec.GetEffectiveBase();
						this.left = new BoxedCast(this.left, l);
					}
				}
				else if (this.left is NullLiteral && typeParameterSpec2 == null)
				{
					if (TypeSpec.IsReferenceType(r))
					{
						return this;
					}
					if (r.Kind == MemberKind.InternalCompilerType)
					{
						return null;
					}
				}
				if (typeParameterSpec2 != null)
				{
					if (this.left is NullLiteral)
					{
						if (typeParameterSpec2.GetEffectiveBase().BuiltinType == BuiltinTypeSpec.Type.ValueType)
						{
							return null;
						}
						this.right = new BoxedCast(this.right, ec.BuiltinTypes.Object);
						return this;
					}
					else
					{
						if (!typeParameterSpec2.IsReferenceType)
						{
							return null;
						}
						r = typeParameterSpec2.GetEffectiveBase();
						this.right = new BoxedCast(this.right, r);
					}
				}
				else if (this.right is NullLiteral)
				{
					if (TypeSpec.IsReferenceType(l))
					{
						return this;
					}
					if (l.Kind == MemberKind.InternalCompilerType)
					{
						return null;
					}
				}
				if (l.IsDelegate)
				{
					if (this.right.eclass == ExprClass.MethodGroup)
					{
						Expression expression = Convert.ImplicitConversion(ec, this.right, l, this.loc);
						if (expression == null)
						{
							return null;
						}
						this.right = expression;
						r = l;
					}
					else if (r.IsDelegate && l != r)
					{
						return null;
					}
				}
				else if (this.left.eclass == ExprClass.MethodGroup && r.IsDelegate)
				{
					Expression expression = Convert.ImplicitConversionRequired(ec, this.left, r, this.loc);
					if (expression == null)
					{
						return null;
					}
					this.left = expression;
					l = r;
				}
				else
				{
					flag = (l == r && !l.IsStruct);
				}
			}
			if (r.BuiltinType != BuiltinTypeSpec.Type.Object && l.BuiltinType != BuiltinTypeSpec.Type.Object)
			{
				Expression expression = this.ResolveOperatorPredefined(ec, ec.BuiltinTypes.OperatorsBinaryEquality, flag);
				if (expression != null)
				{
					return expression;
				}
				if (!flag || l.IsNullableType)
				{
					expression = this.ResolveOperatorPredefined(ec, ec.Module.OperatorsBinaryEqualityLifted, flag);
					if (expression != null)
					{
						return expression;
					}
				}
				if ((l.IsNullableType && this.right.IsNull) || (r.IsNullableType && this.left.IsNull))
				{
					return new LiftedBinaryOperator(this)
					{
						Left = this.left,
						Right = this.right
					}.Resolve(ec);
				}
			}
			if (l == r)
			{
				if (l.Kind != MemberKind.InternalCompilerType && l.Kind != MemberKind.Struct)
				{
					return this;
				}
				return null;
			}
			else
			{
				if (!Convert.ExplicitReferenceConversionExists(l, r) && !Convert.ExplicitReferenceConversionExists(r, l))
				{
					return null;
				}
				if (!TypeSpec.IsReferenceType(l) || !TypeSpec.IsReferenceType(r))
				{
					return null;
				}
				if (l.BuiltinType == BuiltinTypeSpec.Type.String || l.BuiltinType == BuiltinTypeSpec.Type.Delegate || l.IsDelegate || MemberCache.GetUserOperator(l, Mono.CSharp.Operator.OpType.Equality, false) != null)
				{
					ec.Report.Warning(253, 2, this.loc, "Possible unintended reference comparison. Consider casting the right side expression to type `{0}' to get value comparison", l.GetSignatureForError());
				}
				if (r.BuiltinType == BuiltinTypeSpec.Type.String || r.BuiltinType == BuiltinTypeSpec.Type.Delegate || r.IsDelegate || MemberCache.GetUserOperator(r, Mono.CSharp.Operator.OpType.Equality, false) != null)
				{
					ec.Report.Warning(252, 2, this.loc, "Possible unintended reference comparison. Consider casting the left side expression to type `{0}' to get value comparison", r.GetSignatureForError());
				}
				return this;
			}
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0007A770 File Offset: 0x00078970
		private Expression ResolveOperatorPointer(ResolveContext ec, TypeSpec l, TypeSpec r)
		{
			if ((this.oper & Binary.Operator.ComparisonMask) != (Binary.Operator)0)
			{
				if (!l.IsPointer)
				{
					Expression expression = Convert.ImplicitConversion(ec, this.left, r, this.left.Location);
					if (expression == null)
					{
						return null;
					}
					this.left = expression;
				}
				if (!r.IsPointer)
				{
					Expression expression = Convert.ImplicitConversion(ec, this.right, l, this.right.Location);
					if (expression == null)
					{
						return null;
					}
					this.right = expression;
				}
				this.type = ec.BuiltinTypes.Bool;
				return this;
			}
			return this.ResolveOperatorPredefined(ec, ec.BuiltinTypes.OperatorsBinaryUnsafe, false);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0007A80C File Offset: 0x00078A0C
		private Expression ResolveOperatorPredefined(ResolveContext ec, Binary.PredefinedOperator[] operators, bool primitives_only)
		{
			Binary.PredefinedOperator predefinedOperator = null;
			TypeSpec type = this.left.Type;
			TypeSpec type2 = this.right.Type;
			Binary.Operator @operator = this.oper & ~Binary.Operator.ValuesOnlyMask;
			foreach (Binary.PredefinedOperator predefinedOperator2 in operators)
			{
				if ((predefinedOperator2.OperatorsMask & @operator) != (Binary.Operator)0)
				{
					if (primitives_only)
					{
						if (!predefinedOperator2.IsPrimitiveApplicable(type, type2))
						{
							goto IL_C6;
						}
					}
					else if (!predefinedOperator2.IsApplicable(ec, this.left, this.right))
					{
						goto IL_C6;
					}
					if (predefinedOperator == null)
					{
						predefinedOperator = predefinedOperator2;
						if (primitives_only)
						{
							break;
						}
					}
					else
					{
						predefinedOperator = predefinedOperator2.ResolveBetterOperator(ec, predefinedOperator);
						if (predefinedOperator == null)
						{
							ec.Report.Error(34, this.loc, "Operator `{0}' is ambiguous on operands of type `{1}' and `{2}'", new string[]
							{
								this.OperName(this.oper),
								type.GetSignatureForError(),
								type2.GetSignatureForError()
							});
							predefinedOperator = predefinedOperator2;
							break;
						}
					}
				}
				IL_C6:;
			}
			if (predefinedOperator == null)
			{
				return null;
			}
			return predefinedOperator.ConvertResult(ec, this);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x0007A900 File Offset: 0x00078B00
		private Expression OptimizeAndOperation(Expression expr)
		{
			Constant constant = this.right as Constant;
			Constant constant2 = this.left as Constant;
			if ((constant2 != null && constant2.IsDefaultValue) || (constant != null && constant.IsDefaultValue))
			{
				return ReducedExpression.Create((constant == null) ? new SideEffectConstant(constant2, this.right, this.loc) : new SideEffectConstant(constant, this.left, this.loc), expr);
			}
			return expr;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0007A96C File Offset: 0x00078B6C
		public Expression CreateLiftedValueTypeResult(ResolveContext rc, TypeSpec valueType)
		{
			if (rc.HasSet(ResolveContext.Options.ExpressionTreeConversion))
			{
				this.type = rc.BuiltinTypes.Bool;
				return this;
			}
			Constant constant = new BoolConstant(rc.BuiltinTypes, this.Oper == Binary.Operator.Inequality, this.loc);
			if ((this.Oper & Binary.Operator.EqualityMask) != (Binary.Operator)0)
			{
				rc.Report.Warning(472, 2, this.loc, "The result of comparing value type `{0}' with null is always `{1}'", valueType.GetSignatureForError(), constant.GetValueAsLiteral());
			}
			else
			{
				rc.Report.Warning(464, 2, this.loc, "The result of comparing type `{0}' with null is always `{1}'", valueType.GetSignatureForError(), constant.GetValueAsLiteral());
			}
			return constant;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0007AA1C File Offset: 0x00078C1C
		private Expression ResolveUserOperator(ResolveContext rc, Expression left, Expression right)
		{
			Mono.CSharp.Operator.OpType op = Binary.ConvertBinaryToUserOperator(this.oper);
			TypeSpec typeSpec = left.Type;
			if (typeSpec.IsNullableType)
			{
				typeSpec = NullableInfo.GetUnderlyingType(typeSpec);
			}
			TypeSpec typeSpec2 = right.Type;
			if (typeSpec2.IsNullableType)
			{
				typeSpec2 = NullableInfo.GetUnderlyingType(typeSpec2);
			}
			IList<MemberSpec> list = MemberCache.GetUserOperator(typeSpec, op, false);
			IList<MemberSpec> list2 = null;
			if (typeSpec != typeSpec2)
			{
				list2 = MemberCache.GetUserOperator(typeSpec2, op, false);
				if (list2 == null && list == null)
				{
					return null;
				}
			}
			else if (list == null)
			{
				return null;
			}
			Arguments arguments = new Arguments(2);
			Argument argument = new Argument(left);
			arguments.Add(argument);
			Argument argument2 = new Argument(right);
			arguments.Add(argument2);
			if (list != null && list2 != null)
			{
				list = Binary.CombineUserOperators(list, list2);
			}
			else if (list2 != null)
			{
				list = list2;
			}
			OverloadResolver overloadResolver = new OverloadResolver(list, OverloadResolver.Restrictions.ProbingOnly | OverloadResolver.Restrictions.NoBaseMembers | OverloadResolver.Restrictions.BaseMembersIncluded, this.loc);
			MethodSpec methodSpec = overloadResolver.ResolveOperator(rc, ref arguments);
			if (methodSpec != null)
			{
				Expression result;
				if ((this.oper & Binary.Operator.LogicalMask) != (Binary.Operator)0)
				{
					result = new ConditionalLogicalOperator(methodSpec, arguments, new Func<ResolveContext, Expression, Expression>(this.CreateExpressionTree), this.oper == Binary.Operator.LogicalAnd, this.loc).Resolve(rc);
				}
				else
				{
					result = new UserOperatorCall(methodSpec, arguments, new Func<ResolveContext, Expression, Expression>(this.CreateExpressionTree), this.loc);
				}
				this.left = argument.Expr;
				this.right = argument2.Expr;
				return result;
			}
			if ((this.oper & Binary.Operator.LogicalMask) != (Binary.Operator)0)
			{
				return null;
			}
			if (!this.IsLiftedOperatorApplicable())
			{
				return null;
			}
			List<MemberSpec> list3 = this.CreateLiftedOperators(rc, list);
			if (list3 == null)
			{
				return null;
			}
			overloadResolver = new OverloadResolver(list3, OverloadResolver.Restrictions.ProbingOnly | OverloadResolver.Restrictions.NoBaseMembers | OverloadResolver.Restrictions.BaseMembersIncluded, this.loc);
			methodSpec = overloadResolver.ResolveOperator(rc, ref arguments);
			if (methodSpec == null)
			{
				return null;
			}
			MethodSpec methodSpec2 = null;
			foreach (MemberSpec memberSpec in list)
			{
				MethodSpec methodSpec3 = (MethodSpec)memberSpec;
				if (methodSpec3.MemberDefinition == methodSpec.MemberDefinition)
				{
					methodSpec2 = methodSpec3;
					break;
				}
			}
			if (rc.HasSet(ResolveContext.Options.ExpressionTreeConversion))
			{
				this.left = Convert.ImplicitConversion(rc, left, methodSpec.Parameters.Types[0], left.Location);
				this.right = Convert.ImplicitConversion(rc, right, methodSpec.Parameters.Types[1], left.Location);
			}
			TypeSpec[] types = methodSpec2.Parameters.Types;
			if (left.IsNull || right.IsNull)
			{
				if ((this.oper & (Binary.Operator.Multiply | Binary.Operator.ShiftMask | Binary.Operator.BitwiseMask)) != (Binary.Operator)0)
				{
					this.type = methodSpec.ReturnType;
					return LiftedNull.CreateFromExpression(rc, this);
				}
				if ((this.oper & Binary.Operator.RelationalMask) != (Binary.Operator)0)
				{
					return this.CreateLiftedValueTypeResult(rc, left.IsNull ? types[1] : types[0]);
				}
				if ((this.oper & Binary.Operator.EqualityMask) != (Binary.Operator)0 && ((left.IsNull && !right.Type.IsNullableType) || !left.Type.IsNullableType))
				{
					return this.CreateLiftedValueTypeResult(rc, left.IsNull ? types[1] : types[0]);
				}
			}
			this.type = methodSpec.ReturnType;
			LiftedBinaryOperator liftedBinaryOperator = new LiftedBinaryOperator(this);
			liftedBinaryOperator.UserOperator = methodSpec2;
			if (left.Type.IsNullableType && !types[0].IsNullableType)
			{
				liftedBinaryOperator.UnwrapLeft = new Unwrap(left, true);
			}
			if (right.Type.IsNullableType && !types[1].IsNullableType)
			{
				liftedBinaryOperator.UnwrapRight = new Unwrap(right, true);
			}
			liftedBinaryOperator.Left = Convert.ImplicitConversion(rc, liftedBinaryOperator.UnwrapLeft ?? left, types[0], left.Location);
			liftedBinaryOperator.Right = Convert.ImplicitConversion(rc, liftedBinaryOperator.UnwrapRight ?? right, types[1], right.Location);
			return liftedBinaryOperator.Resolve(rc);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0007ADD4 File Offset: 0x00078FD4
		private bool IsLiftedOperatorApplicable()
		{
			if (this.left.Type.IsNullableType)
			{
				return (this.oper & Binary.Operator.EqualityMask) == (Binary.Operator)0 || !this.right.IsNull;
			}
			if (this.right.Type.IsNullableType)
			{
				return (this.oper & Binary.Operator.EqualityMask) == (Binary.Operator)0 || !this.left.IsNull;
			}
			if (TypeSpec.IsValueType(this.left.Type))
			{
				return this.right.IsNull;
			}
			return TypeSpec.IsValueType(this.right.Type) && this.left.IsNull;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0007AE80 File Offset: 0x00079080
		private List<MemberSpec> CreateLiftedOperators(ResolveContext rc, IList<MemberSpec> operators)
		{
			TypeSpec typeSpec = rc.Module.PredefinedTypes.Nullable.TypeSpec;
			if (typeSpec == null)
			{
				return null;
			}
			List<MemberSpec> list = null;
			foreach (MemberSpec memberSpec in operators)
			{
				MethodSpec methodSpec = (MethodSpec)memberSpec;
				TypeSpec typeSpec2;
				if ((this.Oper & Binary.Operator.ComparisonMask) != (Binary.Operator)0)
				{
					typeSpec2 = methodSpec.ReturnType;
					if (typeSpec2.BuiltinType != BuiltinTypeSpec.Type.FirstPrimitive)
					{
						continue;
					}
				}
				else
				{
					if (!TypeSpec.IsNonNullableValueType(methodSpec.ReturnType))
					{
						continue;
					}
					typeSpec2 = null;
				}
				TypeSpec[] types = methodSpec.Parameters.Types;
				if (TypeSpec.IsNonNullableValueType(types[0]) && TypeSpec.IsNonNullableValueType(types[1]) && ((this.Oper & Binary.Operator.EqualityMask) == (Binary.Operator)0 || types[0] == types[1]))
				{
					if (list == null)
					{
						list = new List<MemberSpec>();
					}
					if (typeSpec2 == null)
					{
						typeSpec2 = typeSpec.MakeGenericType(rc.Module, new TypeSpec[]
						{
							methodSpec.ReturnType
						});
					}
					AParametersCollection parameters = ParametersCompiled.CreateFullyResolved(new TypeSpec[]
					{
						typeSpec.MakeGenericType(rc.Module, new TypeSpec[]
						{
							types[0]
						}),
						typeSpec.MakeGenericType(rc.Module, new TypeSpec[]
						{
							types[1]
						})
					});
					MethodSpec item = new MethodSpec(methodSpec.Kind, methodSpec.DeclaringType, methodSpec.MemberDefinition, typeSpec2, parameters, methodSpec.Modifiers);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0007B00C File Offset: 0x0007920C
		private static IList<MemberSpec> CombineUserOperators(IList<MemberSpec> left, IList<MemberSpec> right)
		{
			List<MemberSpec> list = new List<MemberSpec>(left.Count + right.Count);
			list.AddRange(left);
			foreach (MemberSpec memberSpec in right)
			{
				bool flag = false;
				using (IEnumerator<MemberSpec> enumerator2 = left.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.DeclaringType == memberSpec.DeclaringType)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					list.Add(memberSpec);
				}
			}
			return list;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0007B0BC File Offset: 0x000792BC
		private void CheckOutOfRangeComparison(ResolveContext ec, Constant c, TypeSpec type)
		{
			if (c is IntegralConstant || c is CharConstant)
			{
				try
				{
					c.ConvertExplicitly(true, type);
				}
				catch (OverflowException)
				{
					ec.Report.Warning(652, 2, this.loc, "A comparison between a constant and a variable is useless. The constant is out of the range of the variable type `{0}'", type.GetSignatureForError());
				}
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0007B11C File Offset: 0x0007931C
		public override void EmitBranchable(EmitContext ec, Label target, bool on_true)
		{
			if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.right.ContainsEmitWithAwait())
			{
				this.left = this.left.EmitToField(ec);
				if ((this.oper & Binary.Operator.LogicalMask) == (Binary.Operator)0)
				{
					this.right = this.right.EmitToField(ec);
				}
			}
			if ((this.oper & Binary.Operator.EqualityMask) != (Binary.Operator)0 && (this.left is Constant || this.right is Constant))
			{
				bool flag = (this.oper == Binary.Operator.Inequality) ? on_true : (!on_true);
				if (this.left is Constant)
				{
					Expression expression = this.right;
					this.right = this.left;
					this.left = expression;
				}
				if (((Constant)this.right).IsZeroInteger && this.right.Type.BuiltinType != BuiltinTypeSpec.Type.Long && this.right.Type.BuiltinType != BuiltinTypeSpec.Type.ULong)
				{
					this.left.EmitBranchable(ec, target, flag);
					return;
				}
				if (this.right.Type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive)
				{
					this.left.EmitBranchable(ec, target, !flag);
					return;
				}
			}
			else if (this.oper == Binary.Operator.LogicalAnd)
			{
				if (on_true)
				{
					Label label = ec.DefineLabel();
					this.left.EmitBranchable(ec, label, false);
					this.right.EmitBranchable(ec, target, true);
					ec.MarkLabel(label);
					return;
				}
				if (!(this.left is Constant))
				{
					this.left.EmitBranchable(ec, target, false);
				}
				if (!(this.right is Constant))
				{
					this.right.EmitBranchable(ec, target, false);
				}
				return;
			}
			else if (this.oper == Binary.Operator.LogicalOr)
			{
				if (on_true)
				{
					this.left.EmitBranchable(ec, target, true);
					this.right.EmitBranchable(ec, target, true);
					return;
				}
				Label label2 = ec.DefineLabel();
				this.left.EmitBranchable(ec, label2, true);
				this.right.EmitBranchable(ec, target, false);
				ec.MarkLabel(label2);
				return;
			}
			else if ((this.oper & Binary.Operator.ComparisonMask) == (Binary.Operator)0)
			{
				base.EmitBranchable(ec, target, on_true);
				return;
			}
			this.left.Emit(ec);
			this.right.Emit(ec);
			TypeSpec type = this.left.Type;
			bool flag2 = Binary.IsFloat(type);
			bool flag3 = flag2 || Binary.IsUnsigned(type);
			Binary.Operator @operator = this.oper;
			if (@operator != Binary.Operator.Equality)
			{
				if (@operator != Binary.Operator.Inequality)
				{
					switch (@operator)
					{
					case Binary.Operator.LessThan:
						if (on_true)
						{
							if (flag3 && !flag2)
							{
								ec.Emit(OpCodes.Blt_Un, target);
								return;
							}
							ec.Emit(OpCodes.Blt, target);
							return;
						}
						else
						{
							if (flag3)
							{
								ec.Emit(OpCodes.Bge_Un, target);
								return;
							}
							ec.Emit(OpCodes.Bge, target);
							return;
						}
						break;
					case Binary.Operator.GreaterThan:
						if (on_true)
						{
							if (flag3 && !flag2)
							{
								ec.Emit(OpCodes.Bgt_Un, target);
								return;
							}
							ec.Emit(OpCodes.Bgt, target);
							return;
						}
						else
						{
							if (flag3)
							{
								ec.Emit(OpCodes.Ble_Un, target);
								return;
							}
							ec.Emit(OpCodes.Ble, target);
							return;
						}
						break;
					case Binary.Operator.LessThanOrEqual:
						if (on_true)
						{
							if (flag3 && !flag2)
							{
								ec.Emit(OpCodes.Ble_Un, target);
								return;
							}
							ec.Emit(OpCodes.Ble, target);
							return;
						}
						else
						{
							if (flag3)
							{
								ec.Emit(OpCodes.Bgt_Un, target);
								return;
							}
							ec.Emit(OpCodes.Bgt, target);
							return;
						}
						break;
					case Binary.Operator.GreaterThanOrEqual:
						if (on_true)
						{
							if (flag3 && !flag2)
							{
								ec.Emit(OpCodes.Bge_Un, target);
								return;
							}
							ec.Emit(OpCodes.Bge, target);
							return;
						}
						else
						{
							if (flag3)
							{
								ec.Emit(OpCodes.Blt_Un, target);
								return;
							}
							ec.Emit(OpCodes.Blt, target);
							return;
						}
						break;
					default:
						throw new InternalErrorException(this.oper.ToString());
					}
				}
				else
				{
					if (on_true)
					{
						ec.Emit(OpCodes.Bne_Un, target);
						return;
					}
					ec.Emit(OpCodes.Beq, target);
					return;
				}
			}
			else
			{
				if (on_true)
				{
					ec.Emit(OpCodes.Beq, target);
					return;
				}
				ec.Emit(OpCodes.Bne_Un, target);
				return;
			}
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0007B500 File Offset: 0x00079700
		public override void Emit(EmitContext ec)
		{
			if (ec.HasSet(BuilderContext.Options.AsyncBody) && this.right.ContainsEmitWithAwait())
			{
				this.left = this.left.EmitToField(ec);
				if ((this.oper & Binary.Operator.LogicalMask) == (Binary.Operator)0)
				{
					this.right = this.right.EmitToField(ec);
				}
			}
			if ((this.oper & Binary.Operator.LogicalMask) != (Binary.Operator)0)
			{
				Label label = ec.DefineLabel();
				Label label2 = ec.DefineLabel();
				bool flag = this.oper == Binary.Operator.LogicalOr;
				this.left.EmitBranchable(ec, label, flag);
				this.right.Emit(ec);
				ec.Emit(OpCodes.Br_S, label2);
				ec.MarkLabel(label);
				ec.EmitInt(flag ? 1 : 0);
				ec.MarkLabel(label2);
				return;
			}
			if (this.oper == Binary.Operator.Subtraction)
			{
				IntegralConstant integralConstant = this.left as IntegralConstant;
				if (integralConstant != null && integralConstant.IsDefaultValue)
				{
					this.right.Emit(ec);
					ec.Emit(OpCodes.Neg);
					return;
				}
			}
			this.EmitOperator(ec, this.left, this.right);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0007B611 File Offset: 0x00079811
		public void EmitOperator(EmitContext ec, Expression left, Expression right)
		{
			left.Emit(ec);
			right.Emit(ec);
			Binary.EmitOperatorOpcode(ec, this.oper, left.Type, right);
			if (this.enum_conversion != ConvCast.Mode.I1_U1)
			{
				ConvCast.Emit(ec, this.enum_conversion);
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0007B648 File Offset: 0x00079848
		public override void EmitSideEffect(EmitContext ec)
		{
			if ((this.oper & Binary.Operator.LogicalMask) != (Binary.Operator)0 || (ec.HasSet(BuilderContext.Options.CheckedScope) && (this.oper == Binary.Operator.Multiply || this.oper == Binary.Operator.Addition || this.oper == Binary.Operator.Subtraction)))
			{
				base.EmitSideEffect(ec);
				return;
			}
			this.left.EmitSideEffect(ec);
			this.right.EmitSideEffect(ec);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0007B6B0 File Offset: 0x000798B0
		public override Expression EmitToField(EmitContext ec)
		{
			if ((this.oper & Binary.Operator.LogicalMask) == (Binary.Operator)0)
			{
				Await await = this.left as Await;
				if (await != null && this.right.IsSideEffectFree)
				{
					await.Statement.EmitPrologue(ec);
					this.left = await.Statement.GetResultExpression(ec);
					return this;
				}
				await = (this.right as Await);
				if (await != null && this.left.IsSideEffectFree)
				{
					await.Statement.EmitPrologue(ec);
					this.right = await.Statement.GetResultExpression(ec);
					return this;
				}
			}
			return base.EmitToField(ec);
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0007B74A File Offset: 0x0007994A
		protected override void CloneTo(CloneContext clonectx, Expression t)
		{
			Binary binary = (Binary)t;
			binary.left = this.left.Clone(clonectx);
			binary.right = this.right.Clone(clonectx);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0007B778 File Offset: 0x00079978
		public Expression CreateCallSiteBinder(ResolveContext ec, Arguments args)
		{
			Arguments arguments = new Arguments(4);
			MemberAccess expr = new MemberAccess(new MemberAccess(new QualifiedAliasMember(QualifiedAliasMember.GlobalAlias, "System", this.loc), "Linq", this.loc), "Expressions", this.loc);
			CSharpBinderFlags csharpBinderFlags = CSharpBinderFlags.None;
			if (ec.HasSet(ResolveContext.Options.CheckedScope))
			{
				csharpBinderFlags = CSharpBinderFlags.CheckedContext;
			}
			if ((this.oper & Binary.Operator.LogicalMask) != (Binary.Operator)0)
			{
				csharpBinderFlags |= CSharpBinderFlags.BinaryOperationLogical;
			}
			arguments.Add(new Argument(new EnumConstant(new IntLiteral(ec.BuiltinTypes, (int)csharpBinderFlags, this.loc), ec.Module.PredefinedTypes.BinderFlags.Resolve())));
			arguments.Add(new Argument(new MemberAccess(new MemberAccess(expr, "ExpressionType", this.loc), this.GetOperatorExpressionTypeName(), this.loc)));
			arguments.Add(new Argument(new TypeOf(ec.CurrentType, this.loc)));
			arguments.Add(new Argument(new ImplicitlyTypedArrayCreation(args.CreateDynamicBinderArguments(ec), this.loc)));
			return new Invocation(new MemberAccess(new TypeExpression(ec.Module.PredefinedTypes.Binder.TypeSpec, this.loc), "BinaryOperation", this.loc), arguments);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0007B8B4 File Offset: 0x00079AB4
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			return this.CreateExpressionTree(ec, null);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0007B8C0 File Offset: 0x00079AC0
		public Expression CreateExpressionTree(ResolveContext ec, Expression method)
		{
			bool flag = false;
			Binary.Operator @operator = this.oper;
			string name;
			if (@operator <= Binary.Operator.Inequality)
			{
				if (@operator <= Binary.Operator.LeftShift)
				{
					switch (@operator)
					{
					case Binary.Operator.Multiply:
						if (method == null && ec.HasSet(ResolveContext.Options.CheckedScope) && !Binary.IsFloat(this.type))
						{
							name = "MultiplyChecked";
							goto IL_20E;
						}
						name = "Multiply";
						goto IL_20E;
					case Binary.Operator.Division:
						name = "Divide";
						goto IL_20E;
					case Binary.Operator.Modulus:
						name = "Modulo";
						goto IL_20E;
					default:
						if (@operator == Binary.Operator.LeftShift)
						{
							name = "LeftShift";
							goto IL_20E;
						}
						break;
					}
				}
				else
				{
					if (@operator == Binary.Operator.RightShift)
					{
						name = "RightShift";
						goto IL_20E;
					}
					if (@operator == Binary.Operator.Equality)
					{
						name = "Equal";
						flag = true;
						goto IL_20E;
					}
					if (@operator == Binary.Operator.Inequality)
					{
						name = "NotEqual";
						flag = true;
						goto IL_20E;
					}
				}
			}
			else if (@operator <= Binary.Operator.LogicalOr)
			{
				switch (@operator)
				{
				case Binary.Operator.BitwiseAnd:
					name = "And";
					goto IL_20E;
				case Binary.Operator.ExclusiveOr:
					name = "ExclusiveOr";
					goto IL_20E;
				case Binary.Operator.BitwiseOr:
					name = "Or";
					goto IL_20E;
				default:
					if (@operator == Binary.Operator.LogicalAnd)
					{
						name = "AndAlso";
						goto IL_20E;
					}
					if (@operator == Binary.Operator.LogicalOr)
					{
						name = "OrElse";
						goto IL_20E;
					}
					break;
				}
			}
			else if (@operator != Binary.Operator.Addition)
			{
				if (@operator != Binary.Operator.Subtraction)
				{
					switch (@operator)
					{
					case Binary.Operator.LessThan:
						name = "LessThan";
						flag = true;
						goto IL_20E;
					case Binary.Operator.GreaterThan:
						name = "GreaterThan";
						flag = true;
						goto IL_20E;
					case Binary.Operator.LessThanOrEqual:
						name = "LessThanOrEqual";
						flag = true;
						goto IL_20E;
					case Binary.Operator.GreaterThanOrEqual:
						name = "GreaterThanOrEqual";
						flag = true;
						goto IL_20E;
					}
				}
				else
				{
					if (method == null && ec.HasSet(ResolveContext.Options.CheckedScope) && !Binary.IsFloat(this.type))
					{
						name = "SubtractChecked";
						goto IL_20E;
					}
					name = "Subtract";
					goto IL_20E;
				}
			}
			else
			{
				if (method == null && ec.HasSet(ResolveContext.Options.CheckedScope) && !Binary.IsFloat(this.type))
				{
					name = "AddChecked";
					goto IL_20E;
				}
				name = "Add";
				goto IL_20E;
			}
			throw new InternalErrorException("Unknown expression tree binary operator " + this.oper);
			IL_20E:
			Arguments arguments = new Arguments(2);
			arguments.Add(new Argument(this.left.CreateExpressionTree(ec)));
			arguments.Add(new Argument(this.right.CreateExpressionTree(ec)));
			if (method != null)
			{
				if (flag)
				{
					arguments.Add(new Argument(new BoolLiteral(ec.BuiltinTypes, false, this.loc)));
				}
				arguments.Add(new Argument(method));
			}
			return base.CreateExpressionFactoryCall(ec, name, arguments);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0007BB48 File Offset: 0x00079D48
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x040009B8 RID: 2488
		private readonly Binary.Operator oper;

		// Token: 0x040009B9 RID: 2489
		private Expression left;

		// Token: 0x040009BA RID: 2490
		private Expression right;

		// Token: 0x040009BB RID: 2491
		private Binary.State state;

		// Token: 0x040009BC RID: 2492
		private ConvCast.Mode enum_conversion;

		// Token: 0x020003B3 RID: 947
		public class PredefinedOperator
		{
			// Token: 0x06002716 RID: 10006 RVA: 0x000BAF78 File Offset: 0x000B9178
			public PredefinedOperator(TypeSpec ltype, TypeSpec rtype, Binary.Operator op_mask) : this(ltype, rtype, op_mask, ltype)
			{
			}

			// Token: 0x06002717 RID: 10007 RVA: 0x000BAF84 File Offset: 0x000B9184
			public PredefinedOperator(TypeSpec type, Binary.Operator op_mask, TypeSpec return_type) : this(type, type, op_mask, return_type)
			{
			}

			// Token: 0x06002718 RID: 10008 RVA: 0x000BAF90 File Offset: 0x000B9190
			public PredefinedOperator(TypeSpec type, Binary.Operator op_mask) : this(type, type, op_mask, type)
			{
			}

			// Token: 0x06002719 RID: 10009 RVA: 0x000BAF9C File Offset: 0x000B919C
			public PredefinedOperator(TypeSpec ltype, TypeSpec rtype, Binary.Operator op_mask, TypeSpec return_type)
			{
				if ((op_mask & Binary.Operator.ValuesOnlyMask) != (Binary.Operator)0)
				{
					throw new InternalErrorException("Only masked values can be used");
				}
				if ((op_mask & Binary.Operator.NullableMask) != (Binary.Operator)0)
				{
					this.left_unwrap = NullableInfo.GetUnderlyingType(ltype);
					this.right_unwrap = NullableInfo.GetUnderlyingType(rtype);
				}
				else
				{
					this.left_unwrap = ltype;
					this.right_unwrap = rtype;
				}
				this.left = ltype;
				this.right = rtype;
				this.OperatorsMask = op_mask;
				this.ReturnType = return_type;
			}

			// Token: 0x170008DE RID: 2270
			// (get) Token: 0x0600271A RID: 10010 RVA: 0x000BB00E File Offset: 0x000B920E
			public bool IsLifted
			{
				get
				{
					return (this.OperatorsMask & Binary.Operator.NullableMask) > (Binary.Operator)0;
				}
			}

			// Token: 0x0600271B RID: 10011 RVA: 0x000BB020 File Offset: 0x000B9220
			public virtual Expression ConvertResult(ResolveContext rc, Binary b)
			{
				Expression expression = b.left;
				Expression expression2 = b.right;
				b.type = this.ReturnType;
				if (this.IsLifted)
				{
					if (rc.HasSet(ResolveContext.Options.ExpressionTreeConversion))
					{
						b.left = Convert.ImplicitConversion(rc, b.left, this.left, b.left.Location);
						b.right = Convert.ImplicitConversion(rc, b.right, this.right, b.right.Location);
					}
					if (expression2.IsNull)
					{
						if ((b.oper & Binary.Operator.EqualityMask) != (Binary.Operator)0)
						{
							if (!expression.Type.IsNullableType && BuiltinTypeSpec.IsPrimitiveType(expression.Type))
							{
								return b.CreateLiftedValueTypeResult(rc, expression.Type);
							}
						}
						else if ((b.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
						{
							if (this.left_unwrap.BuiltinType != BuiltinTypeSpec.Type.FirstPrimitive)
							{
								return LiftedNull.CreateFromExpression(rc, b);
							}
						}
						else
						{
							b.left = Convert.ImplicitConversion(rc, b.left, this.left, b.left.Location);
							b.right = Convert.ImplicitConversion(rc, b.right, this.right, b.right.Location);
							if ((b.Oper & (Binary.Operator.Multiply | Binary.Operator.ShiftMask)) != (Binary.Operator)0)
							{
								return LiftedNull.CreateFromExpression(rc, b);
							}
							return b.CreateLiftedValueTypeResult(rc, this.left);
						}
					}
					else if (expression.IsNull)
					{
						if ((b.oper & Binary.Operator.EqualityMask) != (Binary.Operator)0)
						{
							if (!expression2.Type.IsNullableType && BuiltinTypeSpec.IsPrimitiveType(expression2.Type))
							{
								return b.CreateLiftedValueTypeResult(rc, expression2.Type);
							}
						}
						else if ((b.oper & Binary.Operator.BitwiseMask) != (Binary.Operator)0)
						{
							if (this.right_unwrap.BuiltinType != BuiltinTypeSpec.Type.FirstPrimitive)
							{
								return LiftedNull.CreateFromExpression(rc, b);
							}
						}
						else
						{
							b.left = Convert.ImplicitConversion(rc, b.left, this.left, b.left.Location);
							b.right = Convert.ImplicitConversion(rc, b.right, this.right, b.right.Location);
							if ((b.Oper & (Binary.Operator.Multiply | Binary.Operator.ShiftMask)) != (Binary.Operator)0)
							{
								return LiftedNull.CreateFromExpression(rc, b);
							}
							return b.CreateLiftedValueTypeResult(rc, this.right);
						}
					}
				}
				if (this.left.BuiltinType == BuiltinTypeSpec.Type.Decimal)
				{
					b.left = Convert.ImplicitConversion(rc, b.left, this.left, b.left.Location);
					b.right = Convert.ImplicitConversion(rc, b.right, this.right, b.right.Location);
					return b.ResolveUserOperator(rc, b.left, b.right);
				}
				Constant constant = expression2 as Constant;
				if (constant != null)
				{
					if (constant.IsDefaultValue)
					{
						if (b.oper == Binary.Operator.Addition || b.oper == Binary.Operator.Subtraction || (b.oper == Binary.Operator.BitwiseOr && this.left_unwrap.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && constant is BoolConstant))
						{
							b.left = Convert.ImplicitConversion(rc, b.left, this.left, b.left.Location);
							return ReducedExpression.Create(b.left, b).Resolve(rc);
						}
						if ((b.oper == Binary.Operator.BitwiseAnd || b.oper == Binary.Operator.LogicalAnd) && !this.IsLifted)
						{
							return ReducedExpression.Create(new SideEffectConstant(constant, b.left, constant.Location), b);
						}
					}
					else if (this.IsLifted && this.left_unwrap.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && b.oper == Binary.Operator.BitwiseAnd)
					{
						return ReducedExpression.Create(b.left, b).Resolve(rc);
					}
					if ((b.oper == Binary.Operator.Multiply || b.oper == Binary.Operator.Division) && constant.IsOneInteger)
					{
						return ReducedExpression.Create(b.left, b).Resolve(rc);
					}
					if ((b.oper & Binary.Operator.ShiftMask) != (Binary.Operator)0 && constant is IntConstant)
					{
						b.right = new IntConstant(rc.BuiltinTypes, ((IntConstant)constant).Value & Binary.GetShiftMask(this.left_unwrap), b.right.Location);
					}
				}
				constant = (b.left as Constant);
				if (constant != null)
				{
					if (constant.IsDefaultValue)
					{
						if (b.oper == Binary.Operator.Addition || (b.oper == Binary.Operator.BitwiseOr && this.right_unwrap.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && constant is BoolConstant))
						{
							b.right = Convert.ImplicitConversion(rc, b.right, this.right, b.right.Location);
							return ReducedExpression.Create(b.right, b).Resolve(rc);
						}
						if (b.oper == Binary.Operator.LogicalAnd && constant.Type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive)
						{
							Expression.Warning_UnreachableExpression(rc, b.right.StartLocation);
							return ReducedExpression.Create(constant, b);
						}
						if (b.oper == Binary.Operator.BitwiseAnd && !this.IsLifted)
						{
							return ReducedExpression.Create(new SideEffectConstant(constant, b.right, constant.Location), b);
						}
					}
					else
					{
						if (this.IsLifted && this.left_unwrap.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && b.oper == Binary.Operator.BitwiseAnd)
						{
							return ReducedExpression.Create(b.right, b).Resolve(rc);
						}
						if (b.oper == Binary.Operator.LogicalOr && constant.Type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive)
						{
							Expression.Warning_UnreachableExpression(rc, b.right.StartLocation);
							return ReducedExpression.Create(constant, b);
						}
					}
					if (b.oper == Binary.Operator.Multiply && constant.IsOneInteger)
					{
						return ReducedExpression.Create(b.right, b).Resolve(rc);
					}
				}
				if (this.IsLifted)
				{
					LiftedBinaryOperator liftedBinaryOperator = new LiftedBinaryOperator(b);
					TypeSpec target_type;
					if (b.left.Type.IsNullableType)
					{
						liftedBinaryOperator.UnwrapLeft = new Unwrap(b.left, true);
						target_type = this.left_unwrap;
					}
					else
					{
						target_type = this.left;
					}
					TypeSpec target_type2;
					if (b.right.Type.IsNullableType)
					{
						liftedBinaryOperator.UnwrapRight = new Unwrap(b.right, true);
						target_type2 = this.right_unwrap;
					}
					else
					{
						target_type2 = this.right;
					}
					liftedBinaryOperator.Left = (b.left.IsNull ? b.left : Convert.ImplicitConversion(rc, liftedBinaryOperator.UnwrapLeft ?? b.left, target_type, b.left.Location));
					liftedBinaryOperator.Right = (b.right.IsNull ? b.right : Convert.ImplicitConversion(rc, liftedBinaryOperator.UnwrapRight ?? b.right, target_type2, b.right.Location));
					return liftedBinaryOperator.Resolve(rc);
				}
				b.left = Convert.ImplicitConversion(rc, b.left, this.left, b.left.Location);
				b.right = Convert.ImplicitConversion(rc, b.right, this.right, b.right.Location);
				return b;
			}

			// Token: 0x0600271C RID: 10012 RVA: 0x000BB6E9 File Offset: 0x000B98E9
			public bool IsPrimitiveApplicable(TypeSpec ltype, TypeSpec rtype)
			{
				return this.left == ltype && ltype == rtype;
			}

			// Token: 0x0600271D RID: 10013 RVA: 0x000BB6FA File Offset: 0x000B98FA
			public virtual bool IsApplicable(ResolveContext ec, Expression lexpr, Expression rexpr)
			{
				return (this.left == lexpr.Type && this.right == rexpr.Type) || (Convert.ImplicitConversionExists(ec, lexpr, this.left) && Convert.ImplicitConversionExists(ec, rexpr, this.right));
			}

			// Token: 0x0600271E RID: 10014 RVA: 0x000BB738 File Offset: 0x000B9938
			public Binary.PredefinedOperator ResolveBetterOperator(ResolveContext ec, Binary.PredefinedOperator best_operator)
			{
				if ((this.OperatorsMask & Binary.Operator.DecomposedMask) != (Binary.Operator)0)
				{
					return best_operator;
				}
				if ((best_operator.OperatorsMask & Binary.Operator.DecomposedMask) != (Binary.Operator)0)
				{
					return this;
				}
				int num = 0;
				if (this.left != null && best_operator.left != null)
				{
					num = OverloadResolver.BetterTypeConversion(ec, best_operator.left_unwrap, this.left_unwrap);
				}
				if (this.right != null && (this.left != this.right || best_operator.left != best_operator.right))
				{
					num |= OverloadResolver.BetterTypeConversion(ec, best_operator.right_unwrap, this.right_unwrap);
				}
				if (num == 0 || num > 2)
				{
					return null;
				}
				if (num != 1)
				{
					return this;
				}
				return best_operator;
			}

			// Token: 0x04001077 RID: 4215
			protected readonly TypeSpec left;

			// Token: 0x04001078 RID: 4216
			protected readonly TypeSpec right;

			// Token: 0x04001079 RID: 4217
			protected readonly TypeSpec left_unwrap;

			// Token: 0x0400107A RID: 4218
			protected readonly TypeSpec right_unwrap;

			// Token: 0x0400107B RID: 4219
			public readonly Binary.Operator OperatorsMask;

			// Token: 0x0400107C RID: 4220
			public TypeSpec ReturnType;
		}

		// Token: 0x020003B4 RID: 948
		private sealed class PredefinedStringOperator : Binary.PredefinedOperator
		{
			// Token: 0x0600271F RID: 10015 RVA: 0x000BAF84 File Offset: 0x000B9184
			public PredefinedStringOperator(TypeSpec type, Binary.Operator op_mask, TypeSpec retType) : base(type, type, op_mask, retType)
			{
			}

			// Token: 0x06002720 RID: 10016 RVA: 0x000BB7D3 File Offset: 0x000B99D3
			public PredefinedStringOperator(TypeSpec ltype, TypeSpec rtype, Binary.Operator op_mask, TypeSpec retType) : base(ltype, rtype, op_mask, retType)
			{
			}

			// Token: 0x06002721 RID: 10017 RVA: 0x000BB7E0 File Offset: 0x000B99E0
			public override Expression ConvertResult(ResolveContext ec, Binary b)
			{
				Unwrap unwrap = b.left as Unwrap;
				if (unwrap != null)
				{
					b.left = unwrap.Original;
				}
				unwrap = (b.right as Unwrap);
				if (unwrap != null)
				{
					b.right = unwrap.Original;
				}
				b.left = Convert.ImplicitConversion(ec, b.left, this.left, b.left.Location);
				b.right = Convert.ImplicitConversion(ec, b.right, this.right, b.right.Location);
				return StringConcat.Create(ec, b.left, b.right, b.loc);
			}
		}

		// Token: 0x020003B5 RID: 949
		private sealed class PredefinedEqualityOperator : Binary.PredefinedOperator
		{
			// Token: 0x06002722 RID: 10018 RVA: 0x000BB881 File Offset: 0x000B9A81
			public PredefinedEqualityOperator(TypeSpec arg, TypeSpec retType) : base(arg, arg, Binary.Operator.EqualityMask, retType)
			{
			}

			// Token: 0x06002723 RID: 10019 RVA: 0x000BB894 File Offset: 0x000B9A94
			public override Expression ConvertResult(ResolveContext ec, Binary b)
			{
				b.type = this.ReturnType;
				b.left = Convert.ImplicitConversion(ec, b.left, this.left, b.left.Location);
				b.right = Convert.ImplicitConversion(ec, b.right, this.right, b.right.Location);
				Arguments arguments = new Arguments(2);
				arguments.Add(new Argument(b.left));
				arguments.Add(new Argument(b.right));
				MethodSpec oper;
				if (b.oper == Binary.Operator.Equality)
				{
					if (this.equal_method == null)
					{
						if (this.left.BuiltinType == BuiltinTypeSpec.Type.String)
						{
							this.equal_method = ec.Module.PredefinedMembers.StringEqual.Resolve(b.loc);
						}
						else
						{
							if (this.left.BuiltinType != BuiltinTypeSpec.Type.Delegate)
							{
								throw new NotImplementedException(this.left.GetSignatureForError());
							}
							this.equal_method = ec.Module.PredefinedMembers.DelegateEqual.Resolve(b.loc);
						}
					}
					oper = this.equal_method;
				}
				else
				{
					if (this.inequal_method == null)
					{
						if (this.left.BuiltinType == BuiltinTypeSpec.Type.String)
						{
							this.inequal_method = ec.Module.PredefinedMembers.StringInequal.Resolve(b.loc);
						}
						else
						{
							if (this.left.BuiltinType != BuiltinTypeSpec.Type.Delegate)
							{
								throw new NotImplementedException(this.left.GetSignatureForError());
							}
							this.inequal_method = ec.Module.PredefinedMembers.DelegateInequal.Resolve(b.loc);
						}
					}
					oper = this.inequal_method;
				}
				return new UserOperatorCall(oper, arguments, new Func<ResolveContext, Expression, Expression>(b.CreateExpressionTree), b.loc);
			}

			// Token: 0x0400107D RID: 4221
			private MethodSpec equal_method;

			// Token: 0x0400107E RID: 4222
			private MethodSpec inequal_method;
		}

		// Token: 0x020003B6 RID: 950
		private class PredefinedPointerOperator : Binary.PredefinedOperator
		{
			// Token: 0x06002724 RID: 10020 RVA: 0x000BBA52 File Offset: 0x000B9C52
			public PredefinedPointerOperator(TypeSpec ltype, TypeSpec rtype, Binary.Operator op_mask) : base(ltype, rtype, op_mask)
			{
			}

			// Token: 0x06002725 RID: 10021 RVA: 0x000BB7D3 File Offset: 0x000B99D3
			public PredefinedPointerOperator(TypeSpec ltype, TypeSpec rtype, Binary.Operator op_mask, TypeSpec retType) : base(ltype, rtype, op_mask, retType)
			{
			}

			// Token: 0x06002726 RID: 10022 RVA: 0x000BBA5D File Offset: 0x000B9C5D
			public PredefinedPointerOperator(TypeSpec type, Binary.Operator op_mask, TypeSpec return_type) : base(type, op_mask, return_type)
			{
			}

			// Token: 0x06002727 RID: 10023 RVA: 0x000BBA68 File Offset: 0x000B9C68
			public override bool IsApplicable(ResolveContext ec, Expression lexpr, Expression rexpr)
			{
				if (this.left == null)
				{
					if (!lexpr.Type.IsPointer)
					{
						return false;
					}
				}
				else if (!Convert.ImplicitConversionExists(ec, lexpr, this.left))
				{
					return false;
				}
				if (this.right == null)
				{
					if (!rexpr.Type.IsPointer)
					{
						return false;
					}
				}
				else if (!Convert.ImplicitConversionExists(ec, rexpr, this.right))
				{
					return false;
				}
				return true;
			}

			// Token: 0x06002728 RID: 10024 RVA: 0x000BBAC8 File Offset: 0x000B9CC8
			public override Expression ConvertResult(ResolveContext ec, Binary b)
			{
				if (this.left != null)
				{
					b.left = EmptyCast.Create(b.left, this.left);
				}
				else if (this.right != null)
				{
					b.right = EmptyCast.Create(b.right, this.right);
				}
				TypeSpec typeSpec = this.ReturnType;
				Expression l;
				Expression r;
				if (typeSpec == null)
				{
					if (this.left == null)
					{
						l = b.left;
						r = b.right;
						typeSpec = b.left.Type;
					}
					else
					{
						l = b.right;
						r = b.left;
						typeSpec = b.right.Type;
					}
				}
				else
				{
					l = b.left;
					r = b.right;
				}
				return new PointerArithmetic(b.oper, l, r, typeSpec, b.loc).Resolve(ec);
			}
		}

		// Token: 0x020003B7 RID: 951
		[Flags]
		public enum Operator
		{
			// Token: 0x04001080 RID: 4224
			Multiply = 32,
			// Token: 0x04001081 RID: 4225
			Division = 33,
			// Token: 0x04001082 RID: 4226
			Modulus = 34,
			// Token: 0x04001083 RID: 4227
			Addition = 2083,
			// Token: 0x04001084 RID: 4228
			Subtraction = 4132,
			// Token: 0x04001085 RID: 4229
			LeftShift = 69,
			// Token: 0x04001086 RID: 4230
			RightShift = 70,
			// Token: 0x04001087 RID: 4231
			LessThan = 8327,
			// Token: 0x04001088 RID: 4232
			GreaterThan = 8328,
			// Token: 0x04001089 RID: 4233
			LessThanOrEqual = 8329,
			// Token: 0x0400108A RID: 4234
			GreaterThanOrEqual = 8330,
			// Token: 0x0400108B RID: 4235
			Equality = 395,
			// Token: 0x0400108C RID: 4236
			Inequality = 396,
			// Token: 0x0400108D RID: 4237
			BitwiseAnd = 525,
			// Token: 0x0400108E RID: 4238
			ExclusiveOr = 526,
			// Token: 0x0400108F RID: 4239
			BitwiseOr = 527,
			// Token: 0x04001090 RID: 4240
			LogicalAnd = 1040,
			// Token: 0x04001091 RID: 4241
			LogicalOr = 1041,
			// Token: 0x04001092 RID: 4242
			ValuesOnlyMask = 31,
			// Token: 0x04001093 RID: 4243
			ArithmeticMask = 32,
			// Token: 0x04001094 RID: 4244
			ShiftMask = 64,
			// Token: 0x04001095 RID: 4245
			ComparisonMask = 128,
			// Token: 0x04001096 RID: 4246
			EqualityMask = 256,
			// Token: 0x04001097 RID: 4247
			BitwiseMask = 512,
			// Token: 0x04001098 RID: 4248
			LogicalMask = 1024,
			// Token: 0x04001099 RID: 4249
			AdditionMask = 2048,
			// Token: 0x0400109A RID: 4250
			SubtractionMask = 4096,
			// Token: 0x0400109B RID: 4251
			RelationalMask = 8192,
			// Token: 0x0400109C RID: 4252
			DecomposedMask = 524288,
			// Token: 0x0400109D RID: 4253
			NullableMask = 1048576
		}

		// Token: 0x020003B8 RID: 952
		[Flags]
		public enum State : byte
		{
			// Token: 0x0400109F RID: 4255
			None = 0,
			// Token: 0x040010A0 RID: 4256
			Compound = 2,
			// Token: 0x040010A1 RID: 4257
			UserOperatorsExcluded = 4
		}
	}
}
