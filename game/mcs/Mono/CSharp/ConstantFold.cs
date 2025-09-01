using System;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x0200012B RID: 299
	public static class ConstantFold
	{
		// Token: 0x06000E92 RID: 3730 RVA: 0x000378E3 File Offset: 0x00035AE3
		public static TypeSpec[] CreateBinaryPromotionsTypes(BuiltinTypes types)
		{
			return new TypeSpec[]
			{
				types.Decimal,
				types.Double,
				types.Float,
				types.ULong,
				types.Long,
				types.UInt
			};
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00037924 File Offset: 0x00035B24
		private static bool DoBinaryNumericPromotions(ResolveContext rc, ref Constant left, ref Constant right)
		{
			TypeSpec type = left.Type;
			TypeSpec type2 = right.Type;
			foreach (TypeSpec typeSpec in rc.BuiltinTypes.BinaryPromotionsTypes)
			{
				if (typeSpec == type)
				{
					return typeSpec == type2 || ConstantFold.ConvertPromotion(rc, ref right, ref left, typeSpec);
				}
				if (typeSpec == type2)
				{
					return typeSpec == type || ConstantFold.ConvertPromotion(rc, ref left, ref right, typeSpec);
				}
			}
			left = left.ConvertImplicitly(rc.BuiltinTypes.Int);
			right = right.ConvertImplicitly(rc.BuiltinTypes.Int);
			return left != null && right != null;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000379C4 File Offset: 0x00035BC4
		private static bool ConvertPromotion(ResolveContext rc, ref Constant prim, ref Constant second, TypeSpec type)
		{
			Constant constant = prim.ConvertImplicitly(type);
			if (constant != null)
			{
				prim = constant;
				return true;
			}
			if (type.BuiltinType == BuiltinTypeSpec.Type.UInt)
			{
				type = rc.BuiltinTypes.Long;
				prim = prim.ConvertImplicitly(type);
				second = second.ConvertImplicitly(type);
				return prim != null && second != null;
			}
			return false;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00037A19 File Offset: 0x00035C19
		public static void Error_CompileTimeOverflow(ResolveContext rc, Location loc)
		{
			rc.Report.Error(220, loc, "The operation overflows at compile time in checked mode");
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00037A34 File Offset: 0x00035C34
		public static Constant BinaryFold(ResolveContext ec, Binary.Operator oper, Constant left, Constant right, Location loc)
		{
			Constant constant = null;
			if (left is EmptyConstantCast)
			{
				return ConstantFold.BinaryFold(ec, oper, ((EmptyConstantCast)left).child, right, loc);
			}
			if (left is SideEffectConstant)
			{
				constant = ConstantFold.BinaryFold(ec, oper, ((SideEffectConstant)left).value, right, loc);
				if (constant == null)
				{
					return null;
				}
				return new SideEffectConstant(constant, left, loc);
			}
			else
			{
				if (right is EmptyConstantCast)
				{
					return ConstantFold.BinaryFold(ec, oper, left, ((EmptyConstantCast)right).child, loc);
				}
				if (right is SideEffectConstant)
				{
					constant = ConstantFold.BinaryFold(ec, oper, left, ((SideEffectConstant)right).value, loc);
					if (constant == null)
					{
						return null;
					}
					return new SideEffectConstant(constant, right, loc);
				}
				else
				{
					TypeSpec type = left.Type;
					TypeSpec type2 = right.Type;
					if (type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && type == type2)
					{
						bool flag = (bool)left.GetValue();
						bool flag2 = (bool)right.GetValue();
						if (oper > Binary.Operator.Inequality)
						{
							switch (oper)
							{
							case Binary.Operator.BitwiseAnd:
								break;
							case Binary.Operator.ExclusiveOr:
								return new BoolConstant(ec.BuiltinTypes, flag ^ flag2, left.Location);
							case Binary.Operator.BitwiseOr:
								goto IL_134;
							default:
								if (oper != Binary.Operator.LogicalAnd)
								{
									if (oper != Binary.Operator.LogicalOr)
									{
										goto IL_195;
									}
									goto IL_134;
								}
								break;
							}
							return new BoolConstant(ec.BuiltinTypes, flag && flag2, left.Location);
							IL_134:
							return new BoolConstant(ec.BuiltinTypes, flag || flag2, left.Location);
						}
						if (oper == Binary.Operator.Equality)
						{
							return new BoolConstant(ec.BuiltinTypes, flag == flag2, left.Location);
						}
						if (oper == Binary.Operator.Inequality)
						{
							return new BoolConstant(ec.BuiltinTypes, flag != flag2, left.Location);
						}
						IL_195:
						return null;
					}
					if (ec.HasSet(ResolveContext.Options.EnumScope))
					{
						if (left is EnumConstant)
						{
							left = ((EnumConstant)left).Child;
						}
						if (right is EnumConstant)
						{
							right = ((EnumConstant)right).Child;
						}
					}
					else if (left is EnumConstant && type2 == type)
					{
						if (oper <= Binary.Operator.Inequality)
						{
							if (oper != Binary.Operator.Equality && oper != Binary.Operator.Inequality)
							{
								goto IL_2CD;
							}
						}
						else
						{
							switch (oper)
							{
							case Binary.Operator.BitwiseAnd:
							case Binary.Operator.ExclusiveOr:
							case Binary.Operator.BitwiseOr:
								constant = ConstantFold.BinaryFold(ec, oper, ((EnumConstant)left).Child, ((EnumConstant)right).Child, loc);
								if (constant != null)
								{
									constant = constant.Reduce(ec, type);
								}
								return constant;
							default:
								if (oper == Binary.Operator.Subtraction)
								{
									constant = ConstantFold.BinaryFold(ec, oper, ((EnumConstant)left).Child, ((EnumConstant)right).Child, loc);
									if (constant != null)
									{
										constant = constant.Reduce(ec, EnumSpec.GetUnderlyingType(type));
									}
									return constant;
								}
								switch (oper)
								{
								case Binary.Operator.LessThan:
								case Binary.Operator.GreaterThan:
								case Binary.Operator.LessThanOrEqual:
								case Binary.Operator.GreaterThanOrEqual:
									break;
								default:
									goto IL_2CD;
								}
								break;
							}
						}
						return ConstantFold.BinaryFold(ec, oper, ((EnumConstant)left).Child, ((EnumConstant)right).Child, loc);
						IL_2CD:
						return null;
					}
					if (oper <= Binary.Operator.Equality)
					{
						if (oper <= Binary.Operator.LeftShift)
						{
							switch (oper)
							{
							case Binary.Operator.Multiply:
								if (left is NullLiteral && right is NullLiteral)
								{
									NullableType nullableType = new NullableType(ec.BuiltinTypes.Int, loc);
									nullableType.ResolveAsType(ec, false);
									return (Constant)new Binary(oper, nullableType, right).ResolveOperator(ec);
								}
								if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
								{
									return null;
								}
								try
								{
									if (left is DoubleConstant)
									{
										double v;
										if (ec.ConstantCheckState)
										{
											v = ((DoubleConstant)left).Value * ((DoubleConstant)right).Value;
										}
										else
										{
											v = ((DoubleConstant)left).Value * ((DoubleConstant)right).Value;
										}
										return new DoubleConstant(ec.BuiltinTypes, v, left.Location);
									}
									if (left is FloatConstant)
									{
										double doubleValue = ((FloatConstant)left).DoubleValue;
										double doubleValue2 = ((FloatConstant)right).DoubleValue;
										double v2;
										if (ec.ConstantCheckState)
										{
											v2 = doubleValue * doubleValue2;
										}
										else
										{
											v2 = doubleValue * doubleValue2;
										}
										return new FloatConstant(ec.BuiltinTypes, v2, left.Location);
									}
									if (left is ULongConstant)
									{
										ulong v3;
										if (ec.ConstantCheckState)
										{
											v3 = checked(((ULongConstant)left).Value * ((ULongConstant)right).Value);
										}
										else
										{
											v3 = ((ULongConstant)left).Value * ((ULongConstant)right).Value;
										}
										return new ULongConstant(ec.BuiltinTypes, v3, left.Location);
									}
									if (left is LongConstant)
									{
										long v4;
										if (ec.ConstantCheckState)
										{
											v4 = checked(((LongConstant)left).Value * ((LongConstant)right).Value);
										}
										else
										{
											v4 = ((LongConstant)left).Value * ((LongConstant)right).Value;
										}
										return new LongConstant(ec.BuiltinTypes, v4, left.Location);
									}
									if (left is UIntConstant)
									{
										uint v5;
										if (ec.ConstantCheckState)
										{
											v5 = checked(((UIntConstant)left).Value * ((UIntConstant)right).Value);
										}
										else
										{
											v5 = ((UIntConstant)left).Value * ((UIntConstant)right).Value;
										}
										return new UIntConstant(ec.BuiltinTypes, v5, left.Location);
									}
									if (left is IntConstant)
									{
										int v6;
										if (ec.ConstantCheckState)
										{
											v6 = checked(((IntConstant)left).Value * ((IntConstant)right).Value);
										}
										else
										{
											v6 = ((IntConstant)left).Value * ((IntConstant)right).Value;
										}
										return new IntConstant(ec.BuiltinTypes, v6, left.Location);
									}
									if (left is DecimalConstant)
									{
										decimal d;
										if (ec.ConstantCheckState)
										{
											d = ((DecimalConstant)left).Value * ((DecimalConstant)right).Value;
										}
										else
										{
											d = ((DecimalConstant)left).Value * ((DecimalConstant)right).Value;
										}
										return new DecimalConstant(ec.BuiltinTypes, d, left.Location);
									}
									throw new Exception("Unexepected multiply input: " + left);
								}
								catch (OverflowException)
								{
									ConstantFold.Error_CompileTimeOverflow(ec, loc);
									goto IL_234B;
								}
								break;
							case Binary.Operator.Division:
								break;
							case Binary.Operator.Modulus:
								goto IL_14ED;
							default:
								if (oper != Binary.Operator.LeftShift)
								{
									goto IL_234B;
								}
								goto IL_1808;
							}
							if (left is NullLiteral && right is NullLiteral)
							{
								NullableType nullableType2 = new NullableType(ec.BuiltinTypes.Int, loc);
								nullableType2.ResolveAsType(ec, false);
								return (Constant)new Binary(oper, nullableType2, right).ResolveOperator(ec);
							}
							if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
							{
								return null;
							}
							try
							{
								if (left is DoubleConstant)
								{
									double v7;
									if (ec.ConstantCheckState)
									{
										v7 = ((DoubleConstant)left).Value / ((DoubleConstant)right).Value;
									}
									else
									{
										v7 = ((DoubleConstant)left).Value / ((DoubleConstant)right).Value;
									}
									return new DoubleConstant(ec.BuiltinTypes, v7, left.Location);
								}
								if (left is FloatConstant)
								{
									double doubleValue3 = ((FloatConstant)left).DoubleValue;
									double doubleValue4 = ((FloatConstant)right).DoubleValue;
									double v8;
									if (ec.ConstantCheckState)
									{
										v8 = doubleValue3 / doubleValue4;
									}
									else
									{
										v8 = doubleValue3 / doubleValue4;
									}
									return new FloatConstant(ec.BuiltinTypes, v8, left.Location);
								}
								if (left is ULongConstant)
								{
									ulong v9;
									if (ec.ConstantCheckState)
									{
										v9 = ((ULongConstant)left).Value / ((ULongConstant)right).Value;
									}
									else
									{
										v9 = ((ULongConstant)left).Value / ((ULongConstant)right).Value;
									}
									return new ULongConstant(ec.BuiltinTypes, v9, left.Location);
								}
								if (left is LongConstant)
								{
									long v10;
									if (ec.ConstantCheckState)
									{
										v10 = ((LongConstant)left).Value / ((LongConstant)right).Value;
									}
									else
									{
										v10 = ((LongConstant)left).Value / ((LongConstant)right).Value;
									}
									return new LongConstant(ec.BuiltinTypes, v10, left.Location);
								}
								if (left is UIntConstant)
								{
									uint v11;
									if (ec.ConstantCheckState)
									{
										v11 = ((UIntConstant)left).Value / ((UIntConstant)right).Value;
									}
									else
									{
										v11 = ((UIntConstant)left).Value / ((UIntConstant)right).Value;
									}
									return new UIntConstant(ec.BuiltinTypes, v11, left.Location);
								}
								if (left is IntConstant)
								{
									int v12;
									if (ec.ConstantCheckState)
									{
										v12 = ((IntConstant)left).Value / ((IntConstant)right).Value;
									}
									else
									{
										v12 = ((IntConstant)left).Value / ((IntConstant)right).Value;
									}
									return new IntConstant(ec.BuiltinTypes, v12, left.Location);
								}
								if (left is DecimalConstant)
								{
									decimal d2;
									if (ec.ConstantCheckState)
									{
										d2 = ((DecimalConstant)left).Value / ((DecimalConstant)right).Value;
									}
									else
									{
										d2 = ((DecimalConstant)left).Value / ((DecimalConstant)right).Value;
									}
									return new DecimalConstant(ec.BuiltinTypes, d2, left.Location);
								}
								throw new Exception("Unexepected division input: " + left);
							}
							catch (OverflowException)
							{
								ConstantFold.Error_CompileTimeOverflow(ec, loc);
								goto IL_234B;
							}
							catch (DivideByZeroException)
							{
								ec.Report.Error(20, loc, "Division by constant zero");
								goto IL_234B;
							}
							IL_14ED:
							if (left is NullLiteral && right is NullLiteral)
							{
								NullableType nullableType3 = new NullableType(ec.BuiltinTypes.Int, loc);
								nullableType3.ResolveAsType(ec, false);
								return (Constant)new Binary(oper, nullableType3, right).ResolveOperator(ec);
							}
							if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
							{
								return null;
							}
							try
							{
								if (left is DoubleConstant)
								{
									double v13;
									if (ec.ConstantCheckState)
									{
										v13 = ((DoubleConstant)left).Value % ((DoubleConstant)right).Value;
									}
									else
									{
										v13 = ((DoubleConstant)left).Value % ((DoubleConstant)right).Value;
									}
									return new DoubleConstant(ec.BuiltinTypes, v13, left.Location);
								}
								if (left is FloatConstant)
								{
									double doubleValue5 = ((FloatConstant)left).DoubleValue;
									double doubleValue6 = ((FloatConstant)right).DoubleValue;
									double v14;
									if (ec.ConstantCheckState)
									{
										v14 = doubleValue5 % doubleValue6;
									}
									else
									{
										v14 = doubleValue5 % doubleValue6;
									}
									return new FloatConstant(ec.BuiltinTypes, v14, left.Location);
								}
								if (left is ULongConstant)
								{
									ulong v15;
									if (ec.ConstantCheckState)
									{
										v15 = ((ULongConstant)left).Value % ((ULongConstant)right).Value;
									}
									else
									{
										v15 = ((ULongConstant)left).Value % ((ULongConstant)right).Value;
									}
									return new ULongConstant(ec.BuiltinTypes, v15, left.Location);
								}
								if (left is LongConstant)
								{
									long v16;
									if (ec.ConstantCheckState)
									{
										v16 = ((LongConstant)left).Value % ((LongConstant)right).Value;
									}
									else
									{
										v16 = ((LongConstant)left).Value % ((LongConstant)right).Value;
									}
									return new LongConstant(ec.BuiltinTypes, v16, left.Location);
								}
								if (left is UIntConstant)
								{
									uint v17;
									if (ec.ConstantCheckState)
									{
										v17 = ((UIntConstant)left).Value % ((UIntConstant)right).Value;
									}
									else
									{
										v17 = ((UIntConstant)left).Value % ((UIntConstant)right).Value;
									}
									return new UIntConstant(ec.BuiltinTypes, v17, left.Location);
								}
								if (left is IntConstant)
								{
									int v18;
									if (ec.ConstantCheckState)
									{
										v18 = ((IntConstant)left).Value % ((IntConstant)right).Value;
									}
									else
									{
										v18 = ((IntConstant)left).Value % ((IntConstant)right).Value;
									}
									return new IntConstant(ec.BuiltinTypes, v18, left.Location);
								}
								if (left is DecimalConstant)
								{
									decimal d3;
									if (ec.ConstantCheckState)
									{
										d3 = ((DecimalConstant)left).Value % ((DecimalConstant)right).Value;
									}
									else
									{
										d3 = ((DecimalConstant)left).Value % ((DecimalConstant)right).Value;
									}
									return new DecimalConstant(ec.BuiltinTypes, d3, left.Location);
								}
								throw new Exception("Unexepected modulus input: " + left);
							}
							catch (DivideByZeroException)
							{
								ec.Report.Error(20, loc, "Division by constant zero");
								goto IL_234B;
							}
							catch (OverflowException)
							{
								ConstantFold.Error_CompileTimeOverflow(ec, loc);
								goto IL_234B;
							}
							IL_1808:
							if (left is NullLiteral && right is NullLiteral)
							{
								NullableType nullableType4 = new NullableType(ec.BuiltinTypes.Int, loc);
								nullableType4.ResolveAsType(ec, false);
								return (Constant)new Binary(oper, nullableType4, right).ResolveOperator(ec);
							}
							IntConstant intConstant = right.ConvertImplicitly(ec.BuiltinTypes.Int) as IntConstant;
							if (intConstant == null)
							{
								return null;
							}
							int value = intConstant.Value;
							switch (left.Type.BuiltinType)
							{
							case BuiltinTypeSpec.Type.UInt:
								return new UIntConstant(ec.BuiltinTypes, ((UIntConstant)left).Value << value, left.Location);
							case BuiltinTypeSpec.Type.Long:
								return new LongConstant(ec.BuiltinTypes, ((LongConstant)left).Value << value, left.Location);
							case BuiltinTypeSpec.Type.ULong:
								return new ULongConstant(ec.BuiltinTypes, ((ULongConstant)left).Value << value, left.Location);
							default:
								if (left is NullLiteral)
								{
									return (Constant)new Binary(oper, left, right).ResolveOperator(ec);
								}
								left = left.ConvertImplicitly(ec.BuiltinTypes.Int);
								if (left.Type.BuiltinType == BuiltinTypeSpec.Type.Int)
								{
									return new IntConstant(ec.BuiltinTypes, ((IntConstant)left).Value << value, left.Location);
								}
								return null;
							}
						}
						else if (oper != Binary.Operator.RightShift)
						{
							if (oper == Binary.Operator.Equality)
							{
								if ((TypeSpec.IsReferenceType(type) && TypeSpec.IsReferenceType(type2)) || (left is LiftedNull && right.IsNull) || (right is LiftedNull && left.IsNull))
								{
									if (left.IsNull || right.IsNull)
									{
										return ReducedExpression.Create(new BoolConstant(ec.BuiltinTypes, left.IsNull == right.IsNull, left.Location), new Binary(oper, left, right));
									}
									if (left is StringConstant && right is StringConstant)
									{
										return new BoolConstant(ec.BuiltinTypes, ((StringConstant)left).Value == ((StringConstant)right).Value, left.Location);
									}
									return null;
								}
								else
								{
									if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
									{
										return null;
									}
									bool val;
									if (left is DoubleConstant)
									{
										val = (((DoubleConstant)left).Value == ((DoubleConstant)right).Value);
									}
									else if (left is FloatConstant)
									{
										val = (((FloatConstant)left).DoubleValue == ((FloatConstant)right).DoubleValue);
									}
									else if (left is ULongConstant)
									{
										val = (((ULongConstant)left).Value == ((ULongConstant)right).Value);
									}
									else if (left is LongConstant)
									{
										val = (((LongConstant)left).Value == ((LongConstant)right).Value);
									}
									else if (left is UIntConstant)
									{
										val = (((UIntConstant)left).Value == ((UIntConstant)right).Value);
									}
									else
									{
										if (!(left is IntConstant))
										{
											return null;
										}
										val = (((IntConstant)left).Value == ((IntConstant)right).Value);
									}
									return new BoolConstant(ec.BuiltinTypes, val, left.Location);
								}
							}
						}
						else
						{
							if (left is NullLiteral && right is NullLiteral)
							{
								NullableType nullableType5 = new NullableType(ec.BuiltinTypes.Int, loc);
								nullableType5.ResolveAsType(ec, false);
								return (Constant)new Binary(oper, nullableType5, right).ResolveOperator(ec);
							}
							IntConstant intConstant2 = right.ConvertImplicitly(ec.BuiltinTypes.Int) as IntConstant;
							if (intConstant2 == null)
							{
								return null;
							}
							int value2 = intConstant2.Value;
							switch (left.Type.BuiltinType)
							{
							case BuiltinTypeSpec.Type.UInt:
								return new UIntConstant(ec.BuiltinTypes, ((UIntConstant)left).Value >> value2, left.Location);
							case BuiltinTypeSpec.Type.Long:
								return new LongConstant(ec.BuiltinTypes, ((LongConstant)left).Value >> value2, left.Location);
							case BuiltinTypeSpec.Type.ULong:
								return new ULongConstant(ec.BuiltinTypes, ((ULongConstant)left).Value >> value2, left.Location);
							default:
								if (left is NullLiteral)
								{
									return (Constant)new Binary(oper, left, right).ResolveOperator(ec);
								}
								left = left.ConvertImplicitly(ec.BuiltinTypes.Int);
								if (left.Type.BuiltinType == BuiltinTypeSpec.Type.Int)
								{
									return new IntConstant(ec.BuiltinTypes, ((IntConstant)left).Value >> value2, left.Location);
								}
								return null;
							}
						}
					}
					else if (oper <= Binary.Operator.BitwiseOr)
					{
						if (oper != Binary.Operator.Inequality)
						{
							switch (oper)
							{
							case Binary.Operator.BitwiseAnd:
								if ((type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && right is NullLiteral) || (type2.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && left is NullLiteral))
								{
									Expression expression = new Binary(oper, left, right).ResolveOperator(ec);
									if ((right is NullLiteral && left.IsDefaultValue) || (left is NullLiteral && right.IsDefaultValue))
									{
										return ReducedExpression.Create(new BoolConstant(ec.BuiltinTypes, false, loc), expression);
									}
									return LiftedNull.CreateFromExpression(ec, expression);
								}
								else
								{
									if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
									{
										return null;
									}
									if (left is IntConstant)
									{
										int v19 = ((IntConstant)left).Value & ((IntConstant)right).Value;
										return new IntConstant(ec.BuiltinTypes, v19, left.Location);
									}
									if (left is UIntConstant)
									{
										uint v20 = ((UIntConstant)left).Value & ((UIntConstant)right).Value;
										return new UIntConstant(ec.BuiltinTypes, v20, left.Location);
									}
									if (left is LongConstant)
									{
										long v21 = ((LongConstant)left).Value & ((LongConstant)right).Value;
										return new LongConstant(ec.BuiltinTypes, v21, left.Location);
									}
									if (left is ULongConstant)
									{
										ulong v22 = ((ULongConstant)left).Value & ((ULongConstant)right).Value;
										return new ULongConstant(ec.BuiltinTypes, v22, left.Location);
									}
								}
								break;
							case Binary.Operator.ExclusiveOr:
								if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
								{
									return null;
								}
								if (left is IntConstant)
								{
									int v23 = ((IntConstant)left).Value ^ ((IntConstant)right).Value;
									return new IntConstant(ec.BuiltinTypes, v23, left.Location);
								}
								if (left is UIntConstant)
								{
									uint v24 = ((UIntConstant)left).Value ^ ((UIntConstant)right).Value;
									return new UIntConstant(ec.BuiltinTypes, v24, left.Location);
								}
								if (left is LongConstant)
								{
									long v25 = ((LongConstant)left).Value ^ ((LongConstant)right).Value;
									return new LongConstant(ec.BuiltinTypes, v25, left.Location);
								}
								if (left is ULongConstant)
								{
									ulong v26 = ((ULongConstant)left).Value ^ ((ULongConstant)right).Value;
									return new ULongConstant(ec.BuiltinTypes, v26, left.Location);
								}
								break;
							case Binary.Operator.BitwiseOr:
								if ((type.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && right is NullLiteral) || (type2.BuiltinType == BuiltinTypeSpec.Type.FirstPrimitive && left is NullLiteral))
								{
									Expression expression2 = new Binary(oper, left, right).ResolveOperator(ec);
									if ((right is NullLiteral && left.IsDefaultValue) || (left is NullLiteral && right.IsDefaultValue))
									{
										return LiftedNull.CreateFromExpression(ec, expression2);
									}
									return ReducedExpression.Create(new BoolConstant(ec.BuiltinTypes, true, loc), expression2);
								}
								else
								{
									if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
									{
										return null;
									}
									if (left is IntConstant)
									{
										int v27 = ((IntConstant)left).Value | ((IntConstant)right).Value;
										return new IntConstant(ec.BuiltinTypes, v27, left.Location);
									}
									if (left is UIntConstant)
									{
										uint v28 = ((UIntConstant)left).Value | ((UIntConstant)right).Value;
										return new UIntConstant(ec.BuiltinTypes, v28, left.Location);
									}
									if (left is LongConstant)
									{
										long v29 = ((LongConstant)left).Value | ((LongConstant)right).Value;
										return new LongConstant(ec.BuiltinTypes, v29, left.Location);
									}
									if (left is ULongConstant)
									{
										ulong v30 = ((ULongConstant)left).Value | ((ULongConstant)right).Value;
										return new ULongConstant(ec.BuiltinTypes, v30, left.Location);
									}
								}
								break;
							}
						}
						else if ((TypeSpec.IsReferenceType(type) && TypeSpec.IsReferenceType(type2)) || (left is LiftedNull && right.IsNull) || (right is LiftedNull && left.IsNull))
						{
							if (left.IsNull || right.IsNull)
							{
								return ReducedExpression.Create(new BoolConstant(ec.BuiltinTypes, left.IsNull != right.IsNull, left.Location), new Binary(oper, left, right));
							}
							if (left is StringConstant && right is StringConstant)
							{
								return new BoolConstant(ec.BuiltinTypes, ((StringConstant)left).Value != ((StringConstant)right).Value, left.Location);
							}
							return null;
						}
						else
						{
							if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
							{
								return null;
							}
							bool val;
							if (left is DoubleConstant)
							{
								val = (((DoubleConstant)left).Value != ((DoubleConstant)right).Value);
							}
							else if (left is FloatConstant)
							{
								val = (((FloatConstant)left).DoubleValue != ((FloatConstant)right).DoubleValue);
							}
							else if (left is ULongConstant)
							{
								val = (((ULongConstant)left).Value != ((ULongConstant)right).Value);
							}
							else if (left is LongConstant)
							{
								val = (((LongConstant)left).Value != ((LongConstant)right).Value);
							}
							else if (left is UIntConstant)
							{
								val = (((UIntConstant)left).Value != ((UIntConstant)right).Value);
							}
							else
							{
								if (!(left is IntConstant))
								{
									return null;
								}
								val = (((IntConstant)left).Value != ((IntConstant)right).Value);
							}
							return new BoolConstant(ec.BuiltinTypes, val, left.Location);
						}
					}
					else if (oper != Binary.Operator.Addition)
					{
						if (oper != Binary.Operator.Subtraction)
						{
							switch (oper)
							{
							case Binary.Operator.LessThan:
							{
								if (right is NullLiteral && left is NullLiteral)
								{
									NullableType nullableType6 = new NullableType(ec.BuiltinTypes.Int, loc);
									nullableType6.ResolveAsType(ec, false);
									return (Constant)new Binary(oper, nullableType6, right).ResolveOperator(ec);
								}
								if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
								{
									return null;
								}
								bool val;
								if (left is DoubleConstant)
								{
									val = (((DoubleConstant)left).Value < ((DoubleConstant)right).Value);
								}
								else if (left is FloatConstant)
								{
									val = (((FloatConstant)left).DoubleValue < ((FloatConstant)right).DoubleValue);
								}
								else if (left is ULongConstant)
								{
									val = (((ULongConstant)left).Value < ((ULongConstant)right).Value);
								}
								else if (left is LongConstant)
								{
									val = (((LongConstant)left).Value < ((LongConstant)right).Value);
								}
								else if (left is UIntConstant)
								{
									val = (((UIntConstant)left).Value < ((UIntConstant)right).Value);
								}
								else
								{
									if (!(left is IntConstant))
									{
										return null;
									}
									val = (((IntConstant)left).Value < ((IntConstant)right).Value);
								}
								return new BoolConstant(ec.BuiltinTypes, val, left.Location);
							}
							case Binary.Operator.GreaterThan:
							{
								if (right is NullLiteral && left is NullLiteral)
								{
									NullableType nullableType7 = new NullableType(ec.BuiltinTypes.Int, loc);
									nullableType7.ResolveAsType(ec, false);
									return (Constant)new Binary(oper, nullableType7, right).ResolveOperator(ec);
								}
								if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
								{
									return null;
								}
								bool val;
								if (left is DoubleConstant)
								{
									val = (((DoubleConstant)left).Value > ((DoubleConstant)right).Value);
								}
								else if (left is FloatConstant)
								{
									val = (((FloatConstant)left).DoubleValue > ((FloatConstant)right).DoubleValue);
								}
								else if (left is ULongConstant)
								{
									val = (((ULongConstant)left).Value > ((ULongConstant)right).Value);
								}
								else if (left is LongConstant)
								{
									val = (((LongConstant)left).Value > ((LongConstant)right).Value);
								}
								else if (left is UIntConstant)
								{
									val = (((UIntConstant)left).Value > ((UIntConstant)right).Value);
								}
								else
								{
									if (!(left is IntConstant))
									{
										return null;
									}
									val = (((IntConstant)left).Value > ((IntConstant)right).Value);
								}
								return new BoolConstant(ec.BuiltinTypes, val, left.Location);
							}
							case Binary.Operator.LessThanOrEqual:
							{
								if (right is NullLiteral && left is NullLiteral)
								{
									NullableType nullableType8 = new NullableType(ec.BuiltinTypes.Int, loc);
									nullableType8.ResolveAsType(ec, false);
									return (Constant)new Binary(oper, nullableType8, right).ResolveOperator(ec);
								}
								if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
								{
									return null;
								}
								bool val;
								if (left is DoubleConstant)
								{
									val = (((DoubleConstant)left).Value <= ((DoubleConstant)right).Value);
								}
								else if (left is FloatConstant)
								{
									val = (((FloatConstant)left).DoubleValue <= ((FloatConstant)right).DoubleValue);
								}
								else if (left is ULongConstant)
								{
									val = (((ULongConstant)left).Value <= ((ULongConstant)right).Value);
								}
								else if (left is LongConstant)
								{
									val = (((LongConstant)left).Value <= ((LongConstant)right).Value);
								}
								else if (left is UIntConstant)
								{
									val = (((UIntConstant)left).Value <= ((UIntConstant)right).Value);
								}
								else
								{
									if (!(left is IntConstant))
									{
										return null;
									}
									val = (((IntConstant)left).Value <= ((IntConstant)right).Value);
								}
								return new BoolConstant(ec.BuiltinTypes, val, left.Location);
							}
							case Binary.Operator.GreaterThanOrEqual:
							{
								if (right is NullLiteral && left is NullLiteral)
								{
									NullableType nullableType9 = new NullableType(ec.BuiltinTypes.Int, loc);
									nullableType9.ResolveAsType(ec, false);
									return (Constant)new Binary(oper, nullableType9, right).ResolveOperator(ec);
								}
								if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
								{
									return null;
								}
								bool val;
								if (left is DoubleConstant)
								{
									val = (((DoubleConstant)left).Value >= ((DoubleConstant)right).Value);
								}
								else if (left is FloatConstant)
								{
									val = (((FloatConstant)left).DoubleValue >= ((FloatConstant)right).DoubleValue);
								}
								else if (left is ULongConstant)
								{
									val = (((ULongConstant)left).Value >= ((ULongConstant)right).Value);
								}
								else if (left is LongConstant)
								{
									val = (((LongConstant)left).Value >= ((LongConstant)right).Value);
								}
								else if (left is UIntConstant)
								{
									val = (((UIntConstant)left).Value >= ((UIntConstant)right).Value);
								}
								else
								{
									if (!(left is IntConstant))
									{
										return null;
									}
									val = (((IntConstant)left).Value >= ((IntConstant)right).Value);
								}
								return new BoolConstant(ec.BuiltinTypes, val, left.Location);
							}
							}
						}
						else
						{
							EnumConstant enumConstant = left as EnumConstant;
							EnumConstant enumConstant2 = right as EnumConstant;
							if (enumConstant != null || enumConstant2 != null)
							{
								if (enumConstant == null)
								{
									enumConstant = enumConstant2;
									type = enumConstant.Type;
									right = left;
								}
								right = right.ConvertImplicitly(enumConstant.Child.Type);
								if (right == null)
								{
									return null;
								}
								constant = ConstantFold.BinaryFold(ec, oper, enumConstant.Child, right, loc);
								if (constant == null)
								{
									return null;
								}
								constant = constant.Reduce(ec, type);
								if (constant == null)
								{
									return null;
								}
								return new EnumConstant(constant, type);
							}
							else
							{
								if (left is NullLiteral && right is NullLiteral)
								{
									NullableType nullableType10 = new NullableType(ec.BuiltinTypes.Int, loc);
									nullableType10.ResolveAsType(ec, false);
									return (Constant)new Binary(oper, nullableType10, right).ResolveOperator(ec);
								}
								if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
								{
									return null;
								}
								try
								{
									if (left is DoubleConstant)
									{
										double v31;
										if (ec.ConstantCheckState)
										{
											v31 = ((DoubleConstant)left).Value - ((DoubleConstant)right).Value;
										}
										else
										{
											v31 = ((DoubleConstant)left).Value - ((DoubleConstant)right).Value;
										}
										constant = new DoubleConstant(ec.BuiltinTypes, v31, left.Location);
									}
									else if (left is FloatConstant)
									{
										double doubleValue7 = ((FloatConstant)left).DoubleValue;
										double doubleValue8 = ((FloatConstant)right).DoubleValue;
										double v32;
										if (ec.ConstantCheckState)
										{
											v32 = doubleValue7 - doubleValue8;
										}
										else
										{
											v32 = doubleValue7 - doubleValue8;
										}
										constant = new FloatConstant(ec.BuiltinTypes, v32, left.Location);
									}
									else if (left is ULongConstant)
									{
										ulong v33;
										if (ec.ConstantCheckState)
										{
											v33 = checked(((ULongConstant)left).Value - ((ULongConstant)right).Value);
										}
										else
										{
											v33 = ((ULongConstant)left).Value - ((ULongConstant)right).Value;
										}
										constant = new ULongConstant(ec.BuiltinTypes, v33, left.Location);
									}
									else if (left is LongConstant)
									{
										long v34;
										if (ec.ConstantCheckState)
										{
											v34 = checked(((LongConstant)left).Value - ((LongConstant)right).Value);
										}
										else
										{
											v34 = ((LongConstant)left).Value - ((LongConstant)right).Value;
										}
										constant = new LongConstant(ec.BuiltinTypes, v34, left.Location);
									}
									else if (left is UIntConstant)
									{
										uint v35;
										if (ec.ConstantCheckState)
										{
											v35 = checked(((UIntConstant)left).Value - ((UIntConstant)right).Value);
										}
										else
										{
											v35 = ((UIntConstant)left).Value - ((UIntConstant)right).Value;
										}
										constant = new UIntConstant(ec.BuiltinTypes, v35, left.Location);
									}
									else if (left is IntConstant)
									{
										int v36;
										if (ec.ConstantCheckState)
										{
											v36 = checked(((IntConstant)left).Value - ((IntConstant)right).Value);
										}
										else
										{
											v36 = ((IntConstant)left).Value - ((IntConstant)right).Value;
										}
										constant = new IntConstant(ec.BuiltinTypes, v36, left.Location);
									}
									else
									{
										if (left is DecimalConstant)
										{
											decimal d4;
											if (ec.ConstantCheckState)
											{
												d4 = ((DecimalConstant)left).Value - ((DecimalConstant)right).Value;
											}
											else
											{
												d4 = ((DecimalConstant)left).Value - ((DecimalConstant)right).Value;
											}
											return new DecimalConstant(ec.BuiltinTypes, d4, left.Location);
										}
										throw new Exception("Unexepected subtraction input: " + left);
									}
								}
								catch (OverflowException)
								{
									ConstantFold.Error_CompileTimeOverflow(ec, loc);
								}
								return constant;
							}
						}
					}
					else if (type.BuiltinType == BuiltinTypeSpec.Type.String || type2.BuiltinType == BuiltinTypeSpec.Type.String)
					{
						if (type == type2)
						{
							return new StringConstant(ec.BuiltinTypes, (string)left.GetValue() + (string)right.GetValue(), left.Location);
						}
						if (type == InternalType.NullLiteral || left.IsNull)
						{
							return new StringConstant(ec.BuiltinTypes, string.Concat(right.GetValue()), left.Location);
						}
						if (type2 == InternalType.NullLiteral || right.IsNull)
						{
							return new StringConstant(ec.BuiltinTypes, string.Concat(left.GetValue()), left.Location);
						}
						return null;
					}
					else if (type == InternalType.NullLiteral)
					{
						if (type2.BuiltinType == BuiltinTypeSpec.Type.Object)
						{
							return new StringConstant(ec.BuiltinTypes, string.Concat(right.GetValue()), left.Location);
						}
						if (type == type2)
						{
							ec.Report.Error(34, loc, "Operator `{0}' is ambiguous on operands of type `{1}' and `{2}'", new string[]
							{
								"+",
								type.GetSignatureForError(),
								type2.GetSignatureForError()
							});
							return null;
						}
						return right;
					}
					else if (type2 == InternalType.NullLiteral)
					{
						if (type.BuiltinType == BuiltinTypeSpec.Type.Object)
						{
							return new StringConstant(ec.BuiltinTypes, string.Concat(right.GetValue()), left.Location);
						}
						return left;
					}
					else
					{
						EnumConstant enumConstant = left as EnumConstant;
						EnumConstant enumConstant2 = right as EnumConstant;
						if (enumConstant != null || enumConstant2 != null)
						{
							if (enumConstant == null)
							{
								enumConstant = enumConstant2;
								type = enumConstant.Type;
								right = left;
							}
							right = right.ConvertImplicitly(enumConstant.Child.Type);
							if (right == null)
							{
								return null;
							}
							constant = ConstantFold.BinaryFold(ec, oper, enumConstant.Child, right, loc);
							if (constant == null)
							{
								return null;
							}
							constant = constant.Reduce(ec, type);
							if (constant == null || type.IsEnum)
							{
								return constant;
							}
							return new EnumConstant(constant, type);
						}
						else
						{
							if (!ConstantFold.DoBinaryNumericPromotions(ec, ref left, ref right))
							{
								return null;
							}
							try
							{
								if (left is DoubleConstant)
								{
									double v37;
									if (ec.ConstantCheckState)
									{
										v37 = ((DoubleConstant)left).Value + ((DoubleConstant)right).Value;
									}
									else
									{
										v37 = ((DoubleConstant)left).Value + ((DoubleConstant)right).Value;
									}
									return new DoubleConstant(ec.BuiltinTypes, v37, left.Location);
								}
								if (left is FloatConstant)
								{
									double doubleValue9 = ((FloatConstant)left).DoubleValue;
									double doubleValue10 = ((FloatConstant)right).DoubleValue;
									double v38;
									if (ec.ConstantCheckState)
									{
										v38 = doubleValue9 + doubleValue10;
									}
									else
									{
										v38 = doubleValue9 + doubleValue10;
									}
									constant = new FloatConstant(ec.BuiltinTypes, v38, left.Location);
								}
								else if (left is ULongConstant)
								{
									ulong v39;
									if (ec.ConstantCheckState)
									{
										v39 = checked(((ULongConstant)left).Value + ((ULongConstant)right).Value);
									}
									else
									{
										v39 = ((ULongConstant)left).Value + ((ULongConstant)right).Value;
									}
									constant = new ULongConstant(ec.BuiltinTypes, v39, left.Location);
								}
								else if (left is LongConstant)
								{
									long v40;
									if (ec.ConstantCheckState)
									{
										v40 = checked(((LongConstant)left).Value + ((LongConstant)right).Value);
									}
									else
									{
										v40 = ((LongConstant)left).Value + ((LongConstant)right).Value;
									}
									constant = new LongConstant(ec.BuiltinTypes, v40, left.Location);
								}
								else if (left is UIntConstant)
								{
									uint v41;
									if (ec.ConstantCheckState)
									{
										v41 = checked(((UIntConstant)left).Value + ((UIntConstant)right).Value);
									}
									else
									{
										v41 = ((UIntConstant)left).Value + ((UIntConstant)right).Value;
									}
									constant = new UIntConstant(ec.BuiltinTypes, v41, left.Location);
								}
								else if (left is IntConstant)
								{
									int v42;
									if (ec.ConstantCheckState)
									{
										v42 = checked(((IntConstant)left).Value + ((IntConstant)right).Value);
									}
									else
									{
										v42 = ((IntConstant)left).Value + ((IntConstant)right).Value;
									}
									constant = new IntConstant(ec.BuiltinTypes, v42, left.Location);
								}
								else if (left is DecimalConstant)
								{
									decimal d5;
									if (ec.ConstantCheckState)
									{
										d5 = ((DecimalConstant)left).Value + ((DecimalConstant)right).Value;
									}
									else
									{
										d5 = ((DecimalConstant)left).Value + ((DecimalConstant)right).Value;
									}
									constant = new DecimalConstant(ec.BuiltinTypes, d5, left.Location);
								}
							}
							catch (OverflowException)
							{
								ConstantFold.Error_CompileTimeOverflow(ec, loc);
							}
							return constant;
						}
					}
					IL_234B:
					return null;
				}
			}
		}
	}
}
