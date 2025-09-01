using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000154 RID: 340
	internal static class Convert
	{
		// Token: 0x060010E5 RID: 4325 RVA: 0x00044C08 File Offset: 0x00042E08
		private static bool ArrayToIList(ArrayContainer array, TypeSpec list, bool isExplicit)
		{
			if (array.Rank != 1 || !list.IsArrayGenericInterface)
			{
				return false;
			}
			TypeSpec typeSpec = list.TypeArguments[0];
			if (array.Element == typeSpec)
			{
				return true;
			}
			if (typeSpec.IsGenericParameter)
			{
				return false;
			}
			if (isExplicit)
			{
				return Convert.ExplicitReferenceConversionExists(array.Element, typeSpec);
			}
			return Convert.ImplicitReferenceConversionExists(array.Element, typeSpec);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00044C64 File Offset: 0x00042E64
		private static bool IList_To_Array(TypeSpec list, ArrayContainer array)
		{
			if (array.Rank != 1 || !list.IsArrayGenericInterface)
			{
				return false;
			}
			TypeSpec typeSpec = list.TypeArguments[0];
			return array.Element == typeSpec || Convert.ImplicitReferenceConversionExists(array.Element, typeSpec) || Convert.ExplicitReferenceConversionExists(array.Element, typeSpec);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00044CB4 File Offset: 0x00042EB4
		public static Expression ImplicitTypeParameterConversion(Expression expr, TypeParameterSpec expr_type, TypeSpec target_type)
		{
			if (target_type.IsGenericParameter)
			{
				if (expr_type.TypeArguments == null || !expr_type.HasDependencyOn(target_type))
				{
					return null;
				}
				if (expr == null)
				{
					return EmptyExpression.Null;
				}
				if (expr_type.IsReferenceType && !((TypeParameterSpec)target_type).IsReferenceType)
				{
					return new BoxedCast(expr, target_type);
				}
				return new ClassCast(expr, target_type);
			}
			else if (target_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				if (expr == null)
				{
					return EmptyExpression.Null;
				}
				if (expr_type.IsReferenceType)
				{
					return new ClassCast(expr, target_type);
				}
				return new BoxedCast(expr, target_type);
			}
			else
			{
				TypeSpec effectiveBase = expr_type.GetEffectiveBase();
				if (effectiveBase == target_type || TypeSpec.IsBaseClass(effectiveBase, target_type, false) || effectiveBase.ImplementsInterface(target_type, true))
				{
					if (expr == null)
					{
						return EmptyExpression.Null;
					}
					if (expr_type.IsReferenceType)
					{
						return new ClassCast(expr, target_type);
					}
					return new BoxedCast(expr, target_type);
				}
				else
				{
					if (!target_type.IsInterface || !expr_type.IsConvertibleToInterface(target_type))
					{
						return null;
					}
					if (expr == null)
					{
						return EmptyExpression.Null;
					}
					if (expr_type.IsReferenceType)
					{
						return new ClassCast(expr, target_type);
					}
					return new BoxedCast(expr, target_type);
				}
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00044DA8 File Offset: 0x00042FA8
		private static Expression ExplicitTypeParameterConversionFromT(Expression source, TypeSpec source_type, TypeSpec target_type)
		{
			TypeParameterSpec typeParameterSpec = target_type as TypeParameterSpec;
			if (typeParameterSpec != null && typeParameterSpec.TypeArguments != null && typeParameterSpec.HasDependencyOn(source_type))
			{
				if (source != null)
				{
					return new ClassCast(source, target_type);
				}
				return EmptyExpression.Null;
			}
			else
			{
				if (!target_type.IsInterface)
				{
					return null;
				}
				if (source != null)
				{
					return new ClassCast(source, target_type, true);
				}
				return EmptyExpression.Null;
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00044DFC File Offset: 0x00042FFC
		private static Expression ExplicitTypeParameterConversionToT(Expression source, TypeSpec source_type, TypeParameterSpec target_type)
		{
			TypeSpec effectiveBase = target_type.GetEffectiveBase();
			if (!TypeSpecComparer.IsEqual(effectiveBase, source_type) && !TypeSpec.IsBaseClass(effectiveBase, source_type, false))
			{
				return null;
			}
			if (source != null)
			{
				return new ClassCast(source, target_type);
			}
			return EmptyExpression.Null;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00044E38 File Offset: 0x00043038
		public static Expression ImplicitReferenceConversion(Expression expr, TypeSpec target_type, bool explicit_cast)
		{
			TypeSpec type = expr.Type;
			if (type.Kind == MemberKind.TypeParameter)
			{
				return Convert.ImplicitTypeParameterConversion(expr, (TypeParameterSpec)expr.Type, target_type);
			}
			NullLiteral nullLiteral = expr as NullLiteral;
			if (nullLiteral != null)
			{
				return nullLiteral.ConvertImplicitly(target_type);
			}
			if (!Convert.ImplicitReferenceConversionExists(type, target_type))
			{
				return null;
			}
			if (!explicit_cast)
			{
				return expr;
			}
			return EmptyCast.Create(expr, target_type);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00044E95 File Offset: 0x00043095
		public static bool ImplicitReferenceConversionExists(TypeSpec expr_type, TypeSpec target_type)
		{
			return Convert.ImplicitReferenceConversionExists(expr_type, target_type, true);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00044EA0 File Offset: 0x000430A0
		public static bool ImplicitReferenceConversionExists(TypeSpec expr_type, TypeSpec target_type, bool refOnlyTypeParameter)
		{
			if (target_type.IsStruct)
			{
				return false;
			}
			MemberKind kind = expr_type.Kind;
			if (kind <= MemberKind.Interface)
			{
				if (kind != MemberKind.Class)
				{
					if (kind == MemberKind.Delegate)
					{
						BuiltinTypeSpec.Type builtinType = target_type.BuiltinType;
						if (builtinType <= BuiltinTypeSpec.Type.Dynamic)
						{
							if (builtinType != BuiltinTypeSpec.Type.Object && builtinType != BuiltinTypeSpec.Type.Dynamic)
							{
								goto IL_162;
							}
						}
						else if (builtinType != BuiltinTypeSpec.Type.Delegate && builtinType != BuiltinTypeSpec.Type.MulticastDelegate)
						{
							goto IL_162;
						}
						return true;
						IL_162:
						return TypeSpecComparer.IsEqual(expr_type, target_type) || expr_type.ImplementsInterface(target_type, false) || TypeSpecComparer.Variant.IsEqual(expr_type, target_type);
					}
					if (kind == MemberKind.Interface)
					{
						if (TypeSpecComparer.IsEqual(expr_type, target_type))
						{
							return true;
						}
						if (target_type.IsInterface)
						{
							return TypeSpecComparer.Variant.IsEqual(expr_type, target_type) || expr_type.ImplementsInterface(target_type, true);
						}
						return target_type.BuiltinType == BuiltinTypeSpec.Type.Object || target_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic;
					}
				}
				else
				{
					if (target_type.BuiltinType == BuiltinTypeSpec.Type.Object || target_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
					{
						return true;
					}
					if (target_type.IsClass)
					{
						return TypeSpecComparer.IsEqual(expr_type, target_type) || TypeSpec.IsBaseClass(expr_type, target_type, true);
					}
					return target_type.IsInterface && expr_type.ImplementsInterface(target_type, true);
				}
			}
			else
			{
				if (kind == MemberKind.TypeParameter)
				{
					return Convert.ImplicitTypeParameterConversion(null, (TypeParameterSpec)expr_type, target_type) != null && (!refOnlyTypeParameter || TypeSpec.IsReferenceType(expr_type));
				}
				if (kind != MemberKind.ArrayType)
				{
					if (kind == MemberKind.InternalCompilerType)
					{
						if (expr_type == InternalType.NullLiteral)
						{
							if (target_type.Kind == MemberKind.InternalCompilerType)
							{
								return target_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic;
							}
							return TypeSpec.IsReferenceType(target_type) || target_type.Kind == MemberKind.PointerType;
						}
						else if (expr_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
						{
							kind = target_type.Kind;
							if (kind <= MemberKind.Delegate)
							{
								if (kind != MemberKind.Class && kind != MemberKind.Delegate)
								{
									goto IL_23C;
								}
							}
							else if (kind != MemberKind.Interface && kind != MemberKind.TypeParameter && kind != MemberKind.ArrayType)
							{
								goto IL_23C;
							}
							return true;
							IL_23C:
							return target_type == InternalType.Arglist;
						}
					}
				}
				else
				{
					if (expr_type == target_type)
					{
						return true;
					}
					BuiltinTypeSpec.Type builtinType = target_type.BuiltinType;
					if (builtinType == BuiltinTypeSpec.Type.Object || builtinType == BuiltinTypeSpec.Type.Dynamic || builtinType == BuiltinTypeSpec.Type.Array)
					{
						return true;
					}
					ArrayContainer arrayContainer = (ArrayContainer)expr_type;
					ArrayContainer arrayContainer2 = target_type as ArrayContainer;
					if (arrayContainer2 != null && arrayContainer.Rank == arrayContainer2.Rank)
					{
						TypeSpec element = arrayContainer.Element;
						return TypeSpec.IsReferenceType(element) && Convert.ImplicitReferenceConversionExists(element, arrayContainer2.Element);
					}
					if (target_type.IsInterface)
					{
						if (expr_type.ImplementsInterface(target_type, false))
						{
							return true;
						}
						if (Convert.ArrayToIList(arrayContainer, target_type, false))
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000450F8 File Offset: 0x000432F8
		public static Expression ImplicitBoxingConversion(Expression expr, TypeSpec expr_type, TypeSpec target_type)
		{
			switch (target_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Object:
			case BuiltinTypeSpec.Type.Dynamic:
			case BuiltinTypeSpec.Type.ValueType:
				if (!TypeSpec.IsValueType(expr_type))
				{
					return null;
				}
				if (expr != null)
				{
					return new BoxedCast(expr, target_type);
				}
				return EmptyExpression.Null;
			case BuiltinTypeSpec.Type.Enum:
				if (expr_type.IsEnum)
				{
					if (expr != null)
					{
						return new BoxedCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			}
			if (expr_type.IsNullableType)
			{
				if (!TypeSpec.IsReferenceType(target_type))
				{
					return null;
				}
				Expression expression = Convert.ImplicitBoxingConversion(expr, NullableInfo.GetUnderlyingType(expr_type), target_type);
				if (expression != null && expr != null)
				{
					expression = new UnboxCast(expression, target_type);
				}
				return expression;
			}
			else
			{
				if (!target_type.IsInterface || !TypeSpec.IsValueType(expr_type) || !expr_type.ImplementsInterface(target_type, true))
				{
					return null;
				}
				if (expr != null)
				{
					return new BoxedCast(expr, target_type);
				}
				return EmptyExpression.Null;
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000451C0 File Offset: 0x000433C0
		public static Expression ImplicitNulableConversion(ResolveContext ec, Expression expr, TypeSpec target_type)
		{
			TypeSpec typeSpec = expr.Type;
			if (typeSpec == InternalType.NullLiteral)
			{
				if (ec != null)
				{
					return LiftedNull.Create(target_type, expr.Location);
				}
				return EmptyExpression.Null;
			}
			else
			{
				TypeSpec underlyingType = NullableInfo.GetUnderlyingType(target_type);
				if (typeSpec.IsNullableType)
				{
					typeSpec = NullableInfo.GetUnderlyingType(typeSpec);
				}
				if (ec == null)
				{
					if (TypeSpecComparer.IsEqual(typeSpec, underlyingType))
					{
						return EmptyExpression.Null;
					}
					if (expr is Constant)
					{
						return ((Constant)expr).ConvertImplicitly(underlyingType);
					}
					return Convert.ImplicitNumericConversion(null, typeSpec, underlyingType);
				}
				else
				{
					Expression expression;
					if (typeSpec != expr.Type)
					{
						expression = Unwrap.Create(expr);
					}
					else
					{
						expression = expr;
					}
					Expression expression2 = expression;
					if (!TypeSpecComparer.IsEqual(typeSpec, underlyingType))
					{
						if (expression2 is Constant)
						{
							expression2 = ((Constant)expression2).ConvertImplicitly(underlyingType);
						}
						else
						{
							expression2 = Convert.ImplicitNumericConversion(expression2, typeSpec, underlyingType);
						}
						if (expression2 == null)
						{
							return null;
						}
					}
					if (typeSpec != expr.Type)
					{
						return new LiftedConversion(expression2, expression, target_type).Resolve(ec);
					}
					return Wrap.Create(expression2, target_type);
				}
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0004529B File Offset: 0x0004349B
		public static Expression ImplicitNumericConversion(Expression expr, TypeSpec target_type)
		{
			return Convert.ImplicitNumericConversion(expr, expr.Type, target_type);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000452AA File Offset: 0x000434AA
		public static bool ImplicitNumericConversionExists(TypeSpec expr_type, TypeSpec target_type)
		{
			return Convert.ImplicitNumericConversion(null, expr_type, target_type) != null;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000452B8 File Offset: 0x000434B8
		private static Expression ImplicitNumericConversion(Expression expr, TypeSpec expr_type, TypeSpec target_type)
		{
			switch (expr_type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Short:
				case BuiltinTypeSpec.Type.UShort:
				case BuiltinTypeSpec.Type.Int:
				case BuiltinTypeSpec.Type.UInt:
					if (expr != null)
					{
						return EmptyCast.Create(expr, target_type);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Long:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_I8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.ULong:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_U8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.SByte:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Short:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_I2);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Int:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_I4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Long:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_I8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.Char:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.UShort:
				case BuiltinTypeSpec.Type.Int:
				case BuiltinTypeSpec.Type.UInt:
					if (expr != null)
					{
						return EmptyCast.Create(expr, target_type);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Long:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_I8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.ULong:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_U8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.Short:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Int:
					if (expr != null)
					{
						return EmptyCast.Create(expr, target_type);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Long:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_I8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.UShort:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Int:
				case BuiltinTypeSpec.Type.UInt:
					if (expr != null)
					{
						return EmptyCast.Create(expr, target_type);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Long:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_I8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.ULong:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_U8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.Int:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Long:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_I8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.UInt:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Long:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_U8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.ULong:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_U8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCastDuplex(expr, target_type, OpCodes.Conv_R_Un, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCastDuplex(expr, target_type, OpCodes.Conv_R_Un, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.Long:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.ULong:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Float:
					if (expr != null)
					{
						return new OpcodeCastDuplex(expr, target_type, OpCodes.Conv_R_Un, OpCodes.Conv_R4);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Double:
					if (expr != null)
					{
						return new OpcodeCastDuplex(expr, target_type, OpCodes.Conv_R_Un, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				case BuiltinTypeSpec.Type.Decimal:
					if (expr != null)
					{
						return new OperatorCast(expr, target_type);
					}
					return EmptyExpression.Null;
				}
				break;
			case BuiltinTypeSpec.Type.Float:
				if (target_type.BuiltinType == BuiltinTypeSpec.Type.Double)
				{
					if (expr != null)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
					return EmptyExpression.Null;
				}
				break;
			}
			return null;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00045858 File Offset: 0x00043A58
		public static bool ImplicitConversionExists(ResolveContext ec, Expression expr, TypeSpec target_type)
		{
			if (Convert.ImplicitStandardConversionExists(ec, expr, target_type))
			{
				return true;
			}
			if (expr.Type == InternalType.AnonymousMethod)
			{
				return (target_type.IsDelegate || target_type.IsExpressionTreeType) && ((AnonymousMethodExpression)expr).ImplicitStandardConversionExists(ec, target_type);
			}
			if (expr.Type == InternalType.Arglist)
			{
				return target_type == ec.Module.PredefinedTypes.ArgIterator.TypeSpec;
			}
			return Convert.UserDefinedConversion(ec, expr, target_type, Convert.UserConversionRestriction.ImplicitOnly | Convert.UserConversionRestriction.ProbingOnly, Location.Null) != null;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000458D8 File Offset: 0x00043AD8
		public static bool ImplicitStandardConversionExists(ResolveContext rc, Expression expr, TypeSpec target_type)
		{
			if (expr.eclass == ExprClass.MethodGroup)
			{
				if (target_type.IsDelegate && rc.Module.Compiler.Settings.Version != LanguageVersion.ISO_1)
				{
					MethodGroupExpr methodGroupExpr = expr as MethodGroupExpr;
					if (methodGroupExpr != null)
					{
						return DelegateCreation.ImplicitStandardConversionExists(rc, methodGroupExpr, target_type);
					}
				}
				return false;
			}
			return Convert.ImplicitStandardConversionExists(expr, target_type);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004592C File Offset: 0x00043B2C
		public static bool ImplicitStandardConversionExists(Expression expr, TypeSpec target_type)
		{
			TypeSpec type = expr.Type;
			if (type == target_type)
			{
				return true;
			}
			if (target_type.IsNullableType)
			{
				return Convert.ImplicitNulableConversion(null, expr, target_type) != null;
			}
			if (Convert.ImplicitNumericConversion(null, type, target_type) != null)
			{
				return true;
			}
			if (Convert.ImplicitReferenceConversionExists(type, target_type, false))
			{
				return true;
			}
			if (Convert.ImplicitBoxingConversion(null, type, target_type) != null)
			{
				return true;
			}
			if (expr is IntConstant)
			{
				int value = ((IntConstant)expr).Value;
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					if (value >= 0 && value <= 255)
					{
						return true;
					}
					break;
				case BuiltinTypeSpec.Type.SByte:
					if (value >= -128 && value <= 127)
					{
						return true;
					}
					break;
				case BuiltinTypeSpec.Type.Short:
					if (value >= -32768 && value <= 32767)
					{
						return true;
					}
					break;
				case BuiltinTypeSpec.Type.UShort:
					if (value >= 0 && value <= 65535)
					{
						return true;
					}
					break;
				case BuiltinTypeSpec.Type.UInt:
					if (value >= 0)
					{
						return true;
					}
					break;
				case BuiltinTypeSpec.Type.ULong:
					if (value >= 0)
					{
						return true;
					}
					break;
				}
			}
			if (expr is LongConstant && target_type.BuiltinType == BuiltinTypeSpec.Type.ULong && ((LongConstant)expr).Value >= 0L)
			{
				return true;
			}
			if (expr is IntegralConstant && target_type.IsEnum)
			{
				return ((IntegralConstant)expr).IsZeroInteger;
			}
			if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
			{
				MemberKind kind = target_type.Kind;
				return kind == MemberKind.Struct || kind == MemberKind.Enum;
			}
			return (target_type.IsPointer && expr.Type.IsPointer && ((PointerContainer)target_type).Element.Kind == MemberKind.Void) || (type.IsStruct && TypeSpecComparer.IsEqual(type, target_type));
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00045AB8 File Offset: 0x00043CB8
		public static TypeSpec FindMostEncompassedType(IList<TypeSpec> types)
		{
			TypeSpec typeSpec = null;
			EmptyExpression expr;
			foreach (TypeSpec typeSpec2 in types)
			{
				if (typeSpec == null)
				{
					typeSpec = typeSpec2;
				}
				else
				{
					expr = new EmptyExpression(typeSpec2);
					if (Convert.ImplicitStandardConversionExists(expr, typeSpec))
					{
						typeSpec = typeSpec2;
					}
				}
			}
			expr = new EmptyExpression(typeSpec);
			foreach (TypeSpec typeSpec3 in types)
			{
				if (typeSpec != typeSpec3 && !Convert.ImplicitStandardConversionExists(expr, typeSpec3))
				{
					typeSpec = null;
					break;
				}
			}
			return typeSpec;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00045B60 File Offset: 0x00043D60
		private static TypeSpec FindMostEncompassingType(IList<TypeSpec> types)
		{
			if (types.Count == 0)
			{
				return null;
			}
			if (types.Count == 1)
			{
				return types[0];
			}
			TypeSpec typeSpec = null;
			for (int i = 0; i < types.Count; i++)
			{
				int j;
				for (j = 0; j < types.Count; j++)
				{
					if (j != i && !Convert.ImplicitStandardConversionExists(new EmptyExpression(types[j]), types[i]))
					{
						j = 0;
						break;
					}
				}
				if (j != 0)
				{
					if (typeSpec != null)
					{
						return InternalType.FakeInternalType;
					}
					typeSpec = types[i];
				}
			}
			return typeSpec;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00045BE8 File Offset: 0x00043DE8
		private static TypeSpec FindMostSpecificSource(ResolveContext rc, List<MethodSpec> list, TypeSpec sourceType, Expression source, bool apply_explicit_conv_rules)
		{
			TypeSpec[] array = null;
			for (int i = 0; i < list.Count; i++)
			{
				TypeSpec typeSpec = list[i].Parameters.Types[0];
				if (typeSpec == sourceType)
				{
					return typeSpec;
				}
				if (array == null)
				{
					array = new TypeSpec[list.Count];
				}
				array[i] = typeSpec;
			}
			if (apply_explicit_conv_rules)
			{
				List<TypeSpec> list2 = new List<TypeSpec>();
				foreach (TypeSpec typeSpec2 in array)
				{
					if (Convert.ImplicitStandardConversionExists(rc, source, typeSpec2))
					{
						list2.Add(typeSpec2);
					}
				}
				if (list2.Count != 0)
				{
					if (source.eclass == ExprClass.MethodGroup)
					{
						return InternalType.FakeInternalType;
					}
					return Convert.FindMostEncompassedType(list2);
				}
			}
			if (apply_explicit_conv_rules)
			{
				return Convert.FindMostEncompassingType(array);
			}
			return Convert.FindMostEncompassedType(array);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00045CA0 File Offset: 0x00043EA0
		public static TypeSpec FindMostSpecificTarget(IList<MethodSpec> list, TypeSpec target, bool apply_explicit_conv_rules)
		{
			List<TypeSpec> list2 = null;
			foreach (MethodSpec methodSpec in list)
			{
				TypeSpec returnType = methodSpec.ReturnType;
				if (returnType == target)
				{
					return returnType;
				}
				if (list2 == null)
				{
					list2 = new List<TypeSpec>(list.Count);
				}
				else if (list2.Contains(returnType))
				{
					continue;
				}
				list2.Add(returnType);
			}
			if (apply_explicit_conv_rules)
			{
				List<TypeSpec> list3 = new List<TypeSpec>();
				foreach (TypeSpec typeSpec in list2)
				{
					if (Convert.ImplicitStandardConversionExists(new EmptyExpression(typeSpec), target))
					{
						list3.Add(typeSpec);
					}
				}
				if (list3.Count != 0)
				{
					return Convert.FindMostEncompassingType(list3);
				}
			}
			if (apply_explicit_conv_rules)
			{
				return Convert.FindMostEncompassedType(list2);
			}
			return Convert.FindMostEncompassingType(list2);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00045D94 File Offset: 0x00043F94
		public static Expression ImplicitUserConversion(ResolveContext ec, Expression source, TypeSpec target, Location loc)
		{
			return Convert.UserDefinedConversion(ec, source, target, Convert.UserConversionRestriction.ImplicitOnly, loc);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x00045DA0 File Offset: 0x00043FA0
		private static Expression ExplicitUserConversion(ResolveContext ec, Expression source, TypeSpec target, Location loc)
		{
			return Convert.UserDefinedConversion(ec, source, target, Convert.UserConversionRestriction.None, loc);
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00045DAC File Offset: 0x00043FAC
		private static void FindApplicableUserDefinedConversionOperators(ResolveContext rc, IList<MemberSpec> operators, Expression source, TypeSpec target, Convert.UserConversionRestriction restr, ref List<MethodSpec> candidates)
		{
			if (source.Type.IsInterface)
			{
				return;
			}
			Expression expression = null;
			foreach (MemberSpec memberSpec in operators)
			{
				MethodSpec methodSpec = (MethodSpec)memberSpec;
				if (methodSpec != null)
				{
					TypeSpec typeSpec = methodSpec.Parameters.Types[0];
					if ((source.Type == typeSpec || Convert.ImplicitStandardConversionExists(rc, source, typeSpec) || ((restr & Convert.UserConversionRestriction.ImplicitOnly) == Convert.UserConversionRestriction.None && Convert.ImplicitStandardConversionExists(new EmptyExpression(typeSpec), source.Type))) && ((restr & Convert.UserConversionRestriction.NullableSourceOnly) == Convert.UserConversionRestriction.None || typeSpec.IsNullableType))
					{
						typeSpec = methodSpec.ReturnType;
						if (!typeSpec.IsInterface)
						{
							if (target != typeSpec)
							{
								if (typeSpec.IsNullableType)
								{
									typeSpec = NullableInfo.GetUnderlyingType(typeSpec);
								}
								if (!Convert.ImplicitStandardConversionExists(new EmptyExpression(typeSpec), target))
								{
									if ((restr & Convert.UserConversionRestriction.ImplicitOnly) != Convert.UserConversionRestriction.None)
									{
										continue;
									}
									if (expression == null)
									{
										expression = new EmptyExpression(target);
									}
									if (!Convert.ImplicitStandardConversionExists(expression, typeSpec))
									{
										continue;
									}
								}
							}
							if (candidates == null)
							{
								candidates = new List<MethodSpec>();
							}
							candidates.Add(methodSpec);
						}
					}
				}
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00045EB8 File Offset: 0x000440B8
		public static Expression UserDefinedConversion(ResolveContext rc, Expression source, TypeSpec target, Convert.UserConversionRestriction restr, Location loc)
		{
			List<MethodSpec> list = null;
			TypeSpec type = source.Type;
			TypeSpec typeSpec = target;
			bool flag = false;
			bool flag2 = (restr & Convert.UserConversionRestriction.ImplicitOnly) > Convert.UserConversionRestriction.None;
			Expression expression;
			if (type.IsNullableType)
			{
				if (flag2 && !TypeSpec.IsReferenceType(typeSpec) && !typeSpec.IsNullableType)
				{
					expression = source;
				}
				else
				{
					expression = Unwrap.CreateUnwrapped(source);
					type = expression.Type;
					flag = true;
				}
			}
			else
			{
				expression = source;
			}
			if (typeSpec.IsNullableType)
			{
				typeSpec = NullableInfo.GetUnderlyingType(typeSpec);
			}
			if ((type.Kind & (MemberKind.Class | MemberKind.Struct | MemberKind.TypeParameter)) != (MemberKind)0 && type.BuiltinType != BuiltinTypeSpec.Type.Decimal)
			{
				bool isStruct = type.IsStruct;
				IList<MemberSpec> userOperator = MemberCache.GetUserOperator(type, Operator.OpType.Implicit, isStruct);
				if (userOperator != null)
				{
					Convert.FindApplicableUserDefinedConversionOperators(rc, userOperator, expression, typeSpec, restr, ref list);
				}
				if (!flag2)
				{
					userOperator = MemberCache.GetUserOperator(type, Operator.OpType.Explicit, isStruct);
					if (userOperator != null)
					{
						Convert.FindApplicableUserDefinedConversionOperators(rc, userOperator, expression, typeSpec, restr, ref list);
					}
				}
			}
			if ((target.Kind & (MemberKind.Class | MemberKind.Struct | MemberKind.TypeParameter)) != (MemberKind)0 && typeSpec.BuiltinType != BuiltinTypeSpec.Type.Decimal)
			{
				bool declaredOnly = target.IsStruct || flag2;
				IList<MemberSpec> userOperator2 = MemberCache.GetUserOperator(typeSpec, Operator.OpType.Implicit, declaredOnly);
				if (userOperator2 != null)
				{
					Convert.FindApplicableUserDefinedConversionOperators(rc, userOperator2, expression, typeSpec, restr, ref list);
				}
				if (!flag2)
				{
					userOperator2 = MemberCache.GetUserOperator(typeSpec, Operator.OpType.Explicit, declaredOnly);
					if (userOperator2 != null)
					{
						Convert.FindApplicableUserDefinedConversionOperators(rc, userOperator2, expression, typeSpec, restr, ref list);
					}
				}
			}
			if (list == null)
			{
				return null;
			}
			MethodSpec methodSpec;
			TypeSpec typeSpec2;
			TypeSpec typeSpec3;
			if (list.Count == 1)
			{
				methodSpec = list[0];
				typeSpec2 = methodSpec.Parameters.Types[0];
				typeSpec3 = methodSpec.ReturnType;
			}
			else
			{
				typeSpec2 = Convert.FindMostSpecificSource(rc, list, source.Type, expression, !flag2);
				if (typeSpec2 == null)
				{
					return null;
				}
				typeSpec3 = Convert.FindMostSpecificTarget(list, target, !flag2);
				if (typeSpec3 == null)
				{
					return null;
				}
				methodSpec = null;
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].ReturnType == typeSpec3 && list[i].Parameters.Types[0] == typeSpec2)
					{
						methodSpec = list[i];
						break;
					}
				}
				if (methodSpec == null)
				{
					if ((restr & Convert.UserConversionRestriction.ProbingOnly) == Convert.UserConversionRestriction.None)
					{
						MethodSpec methodSpec2 = list[0];
						methodSpec = list[1];
						rc.Report.Error(457, loc, "Ambiguous user defined operators `{0}' and `{1}' when converting from `{2}' to `{3}'", new string[]
						{
							methodSpec2.GetSignatureForError(),
							methodSpec.GetSignatureForError(),
							source.Type.GetSignatureForError(),
							target.GetSignatureForError()
						});
					}
					return ErrorExpression.Instance;
				}
			}
			if (typeSpec2 != type)
			{
				Constant constant = source as Constant;
				if (constant != null)
				{
					source = constant.Reduce(rc, typeSpec2);
					if (source == null)
					{
						constant = null;
					}
				}
				if (constant == null)
				{
					source = (flag2 ? Convert.ImplicitConversionStandard(rc, expression, typeSpec2, loc) : Convert.ExplicitConversionStandard(rc, expression, typeSpec2, loc));
				}
			}
			else
			{
				source = expression;
			}
			source = new UserCast(methodSpec, source, loc).Resolve(rc);
			if (typeSpec3 != typeSpec)
			{
				if (typeSpec3.IsNullableType && (target.IsNullableType || !flag2))
				{
					if (typeSpec3 != target)
					{
						Expression expression2 = Unwrap.CreateUnwrapped(source);
						source = (flag2 ? Convert.ImplicitConversionStandard(rc, expression2, typeSpec, loc) : Convert.ExplicitConversionStandard(rc, expression2, typeSpec, loc));
						if (source == null)
						{
							return null;
						}
						if (target.IsNullableType)
						{
							source = new LiftedConversion(source, expression2, target).Resolve(rc);
						}
					}
				}
				else
				{
					source = (flag2 ? Convert.ImplicitConversionStandard(rc, source, typeSpec, loc) : Convert.ExplicitConversionStandard(rc, source, typeSpec, loc));
					if (source == null)
					{
						return null;
					}
				}
			}
			if (flag && !typeSpec2.IsNullableType)
			{
				return new LiftedConversion(source, expression, target).Resolve(rc);
			}
			if (target.IsNullableType && !typeSpec3.IsNullableType)
			{
				source = Wrap.Create(source, target);
			}
			return source;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00046214 File Offset: 0x00044414
		public static Expression ImplicitConversion(ResolveContext ec, Expression expr, TypeSpec target_type, Location loc)
		{
			if (target_type == null)
			{
				throw new Exception("Target type is null");
			}
			Expression expression = Convert.ImplicitConversionStandard(ec, expr, target_type, loc);
			if (expression != null)
			{
				return expression;
			}
			expression = Convert.ImplicitUserConversion(ec, expr, target_type, loc);
			if (expression != null)
			{
				return expression;
			}
			return null;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0004624E File Offset: 0x0004444E
		public static Expression ImplicitConversionStandard(ResolveContext ec, Expression expr, TypeSpec target_type, Location loc)
		{
			return Convert.ImplicitConversionStandard(ec, expr, target_type, loc, false);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0004625C File Offset: 0x0004445C
		private static Expression ImplicitConversionStandard(ResolveContext ec, Expression expr, TypeSpec target_type, Location loc, bool explicit_cast)
		{
			if (expr.eclass == ExprClass.MethodGroup)
			{
				if (!target_type.IsDelegate)
				{
					return null;
				}
				if (ec.Module.Compiler.Settings.Version != LanguageVersion.ISO_1)
				{
					MethodGroupExpr methodGroupExpr = expr as MethodGroupExpr;
					if (methodGroupExpr != null)
					{
						return new ImplicitDelegateCreation(target_type, methodGroupExpr, loc).Resolve(ec);
					}
				}
			}
			TypeSpec type = expr.Type;
			if (type == target_type)
			{
				if (type != InternalType.NullLiteral && type != InternalType.AnonymousMethod)
				{
					return expr;
				}
				return null;
			}
			else
			{
				if (type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					MemberKind kind = target_type.Kind;
					if (kind <= MemberKind.Delegate)
					{
						if (kind != MemberKind.Class)
						{
							if (kind != MemberKind.Struct && kind != MemberKind.Delegate)
							{
								goto IL_110;
							}
							goto IL_E2;
						}
					}
					else if (kind <= MemberKind.Interface)
					{
						if (kind != MemberKind.Enum && kind != MemberKind.Interface)
						{
							goto IL_110;
						}
						goto IL_E2;
					}
					else
					{
						if (kind == MemberKind.TypeParameter)
						{
							goto IL_E2;
						}
						if (kind != MemberKind.ArrayType)
						{
							goto IL_110;
						}
					}
					if (target_type.BuiltinType == BuiltinTypeSpec.Type.Object)
					{
						return EmptyCast.Create(expr, target_type);
					}
					IL_E2:
					Arguments arguments = new Arguments(1);
					arguments.Add(new Argument(expr));
					return new DynamicConversion(target_type, explicit_cast ? CSharpBinderFlags.ConvertExplicit : CSharpBinderFlags.None, arguments, loc).Resolve(ec);
					IL_110:
					return null;
				}
				if (target_type.IsNullableType)
				{
					return Convert.ImplicitNulableConversion(ec, expr, target_type);
				}
				Constant constant = expr as Constant;
				if (constant != null)
				{
					try
					{
						constant = constant.ConvertImplicitly(target_type);
					}
					catch
					{
						throw new InternalErrorException("Conversion error", new object[]
						{
							loc
						});
					}
					if (constant != null)
					{
						return constant;
					}
				}
				Expression expression = Convert.ImplicitNumericConversion(expr, type, target_type);
				if (expression != null)
				{
					return expression;
				}
				expression = Convert.ImplicitReferenceConversion(expr, target_type, explicit_cast);
				if (expression != null)
				{
					return expression;
				}
				expression = Convert.ImplicitBoxingConversion(expr, type, target_type);
				if (expression != null)
				{
					return expression;
				}
				if (expr is IntegralConstant && target_type.IsEnum)
				{
					IntegralConstant integralConstant = (IntegralConstant)expr;
					if (integralConstant.IsZeroInteger)
					{
						return new EnumConstant(new IntLiteral(ec.BuiltinTypes, 0, integralConstant.Location), target_type);
					}
				}
				PointerContainer pointerContainer = target_type as PointerContainer;
				if (pointerContainer != null)
				{
					if (type.IsPointer)
					{
						if (type == pointerContainer)
						{
							return expr;
						}
						if (pointerContainer.Element.Kind == MemberKind.Void)
						{
							return EmptyCast.Create(expr, target_type);
						}
					}
					if (type == InternalType.NullLiteral)
					{
						return new NullPointer(target_type, loc);
					}
				}
				if (type == InternalType.AnonymousMethod)
				{
					Expression expression2 = ((AnonymousMethodExpression)expr).Compatible(ec, target_type);
					if (expression2 != null)
					{
						return expression2.Resolve(ec);
					}
					return ErrorExpression.Instance;
				}
				else
				{
					if (type == InternalType.Arglist && target_type == ec.Module.PredefinedTypes.ArgIterator.TypeSpec)
					{
						return expr;
					}
					if (type.IsStruct && TypeSpecComparer.IsEqual(type, target_type))
					{
						if (type != target_type)
						{
							return EmptyCast.Create(expr, target_type);
						}
						return expr;
					}
					else
					{
						InterpolatedString interpolatedString = expr as InterpolatedString;
						if (interpolatedString != null && (target_type == ec.Module.PredefinedTypes.IFormattable.TypeSpec || target_type == ec.Module.PredefinedTypes.FormattableString.TypeSpec))
						{
							return interpolatedString.ConvertTo(ec, target_type);
						}
						return null;
					}
				}
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0004652C File Offset: 0x0004472C
		public static Expression ImplicitConversionRequired(ResolveContext ec, Expression source, TypeSpec target_type, Location loc)
		{
			Expression expression = Convert.ImplicitConversion(ec, source, target_type, loc);
			if (expression != null)
			{
				return expression;
			}
			source.Error_ValueCannotBeConverted(ec, target_type, false);
			return null;
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00046554 File Offset: 0x00044754
		public static Expression ExplicitNumericConversion(ResolveContext rc, Expression expr, TypeSpec target_type)
		{
			switch (expr.Type.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
			{
				BuiltinTypeSpec.Type builtinType = target_type.BuiltinType;
				if (builtinType == BuiltinTypeSpec.Type.SByte)
				{
					return new ConvCast(expr, target_type, ConvCast.Mode.U1_I1);
				}
				if (builtinType == BuiltinTypeSpec.Type.Char)
				{
					return new ConvCast(expr, target_type, ConvCast.Mode.U1_CH);
				}
				break;
			}
			case BuiltinTypeSpec.Type.SByte:
			{
				BuiltinTypeSpec.Type builtinType = target_type.BuiltinType;
				switch (builtinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.I1_U1);
				case BuiltinTypeSpec.Type.SByte:
				case BuiltinTypeSpec.Type.Short:
				case BuiltinTypeSpec.Type.Int:
				case BuiltinTypeSpec.Type.Long:
					break;
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.I1_CH);
				case BuiltinTypeSpec.Type.UShort:
					return new ConvCast(expr, target_type, ConvCast.Mode.I1_U2);
				case BuiltinTypeSpec.Type.UInt:
					return new ConvCast(expr, target_type, ConvCast.Mode.I1_U4);
				case BuiltinTypeSpec.Type.ULong:
					return new ConvCast(expr, target_type, ConvCast.Mode.I1_U8);
				default:
					if (builtinType == BuiltinTypeSpec.Type.UIntPtr)
					{
						return new OperatorCast(new ConvCast(expr, rc.BuiltinTypes.ULong, ConvCast.Mode.I1_U8), target_type, target_type, true);
					}
					break;
				}
				break;
			}
			case BuiltinTypeSpec.Type.Char:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.CH_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.CH_I1);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(expr, target_type, ConvCast.Mode.CH_I2);
				}
				break;
			case BuiltinTypeSpec.Type.Short:
			{
				BuiltinTypeSpec.Type builtinType = target_type.BuiltinType;
				switch (builtinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.I2_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.I2_I1);
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.I2_CH);
				case BuiltinTypeSpec.Type.Short:
				case BuiltinTypeSpec.Type.Int:
				case BuiltinTypeSpec.Type.Long:
					break;
				case BuiltinTypeSpec.Type.UShort:
					return new ConvCast(expr, target_type, ConvCast.Mode.I2_U2);
				case BuiltinTypeSpec.Type.UInt:
					return new ConvCast(expr, target_type, ConvCast.Mode.I2_U4);
				case BuiltinTypeSpec.Type.ULong:
					return new ConvCast(expr, target_type, ConvCast.Mode.I2_U8);
				default:
					if (builtinType == BuiltinTypeSpec.Type.UIntPtr)
					{
						return new OperatorCast(new ConvCast(expr, rc.BuiltinTypes.ULong, ConvCast.Mode.I2_U8), target_type, target_type, true);
					}
					break;
				}
				break;
			}
			case BuiltinTypeSpec.Type.UShort:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.U2_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.U2_I1);
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.U2_CH);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(expr, target_type, ConvCast.Mode.U2_I2);
				}
				break;
			case BuiltinTypeSpec.Type.Int:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.I4_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.I4_I1);
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.I4_CH);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(expr, target_type, ConvCast.Mode.I4_I2);
				case BuiltinTypeSpec.Type.UShort:
					return new ConvCast(expr, target_type, ConvCast.Mode.I4_U2);
				case BuiltinTypeSpec.Type.UInt:
					return new ConvCast(expr, target_type, ConvCast.Mode.I4_U4);
				case BuiltinTypeSpec.Type.ULong:
					return new ConvCast(expr, target_type, ConvCast.Mode.I4_U8);
				case BuiltinTypeSpec.Type.UIntPtr:
					return new OperatorCast(new ConvCast(expr, rc.BuiltinTypes.ULong, ConvCast.Mode.I2_U8), target_type, target_type, true);
				}
				break;
			case BuiltinTypeSpec.Type.UInt:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.U4_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.U4_I1);
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.U4_CH);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(expr, target_type, ConvCast.Mode.U4_I2);
				case BuiltinTypeSpec.Type.UShort:
					return new ConvCast(expr, target_type, ConvCast.Mode.U4_U2);
				case BuiltinTypeSpec.Type.Int:
					return new ConvCast(expr, target_type, ConvCast.Mode.U4_I4);
				}
				break;
			case BuiltinTypeSpec.Type.Long:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_I1);
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_CH);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_I2);
				case BuiltinTypeSpec.Type.UShort:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_U2);
				case BuiltinTypeSpec.Type.Int:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_I4);
				case BuiltinTypeSpec.Type.UInt:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_U4);
				case BuiltinTypeSpec.Type.ULong:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_U8);
				}
				break;
			case BuiltinTypeSpec.Type.ULong:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_I1);
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_CH);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_I2);
				case BuiltinTypeSpec.Type.UShort:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_U2);
				case BuiltinTypeSpec.Type.Int:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_I4);
				case BuiltinTypeSpec.Type.UInt:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_U4);
				case BuiltinTypeSpec.Type.Long:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_I8);
				case BuiltinTypeSpec.Type.IntPtr:
					return new OperatorCast(EmptyCast.Create(expr, rc.BuiltinTypes.Long), target_type, true);
				}
				break;
			case BuiltinTypeSpec.Type.Float:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_I1);
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_CH);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_I2);
				case BuiltinTypeSpec.Type.UShort:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_U2);
				case BuiltinTypeSpec.Type.Int:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_I4);
				case BuiltinTypeSpec.Type.UInt:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_U4);
				case BuiltinTypeSpec.Type.Long:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_I8);
				case BuiltinTypeSpec.Type.ULong:
					return new ConvCast(expr, target_type, ConvCast.Mode.R4_U8);
				case BuiltinTypeSpec.Type.Decimal:
					return new OperatorCast(expr, target_type, true);
				}
				break;
			case BuiltinTypeSpec.Type.Double:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_I1);
				case BuiltinTypeSpec.Type.Char:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_CH);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_I2);
				case BuiltinTypeSpec.Type.UShort:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_U2);
				case BuiltinTypeSpec.Type.Int:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_I4);
				case BuiltinTypeSpec.Type.UInt:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_U4);
				case BuiltinTypeSpec.Type.Long:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_I8);
				case BuiltinTypeSpec.Type.ULong:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_U8);
				case BuiltinTypeSpec.Type.Float:
					return new ConvCast(expr, target_type, ConvCast.Mode.R8_R4);
				case BuiltinTypeSpec.Type.Decimal:
					return new OperatorCast(expr, target_type, true);
				}
				break;
			case BuiltinTypeSpec.Type.Decimal:
				switch (target_type.BuiltinType)
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
					return new OperatorCast(expr, expr.Type, target_type, true);
				}
				break;
			case BuiltinTypeSpec.Type.IntPtr:
				if (target_type.BuiltinType == BuiltinTypeSpec.Type.UInt)
				{
					return EmptyCast.Create(new OperatorCast(expr, expr.Type, rc.BuiltinTypes.Int, true), target_type);
				}
				if (target_type.BuiltinType == BuiltinTypeSpec.Type.ULong)
				{
					return EmptyCast.Create(new OperatorCast(expr, expr.Type, rc.BuiltinTypes.Long, true), target_type);
				}
				break;
			case BuiltinTypeSpec.Type.UIntPtr:
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.SByte:
					return new ConvCast(new OperatorCast(expr, expr.Type, rc.BuiltinTypes.UInt, true), target_type, ConvCast.Mode.U4_I1);
				case BuiltinTypeSpec.Type.Short:
					return new ConvCast(new OperatorCast(expr, expr.Type, rc.BuiltinTypes.UInt, true), target_type, ConvCast.Mode.U4_I2);
				case BuiltinTypeSpec.Type.Int:
					return EmptyCast.Create(new OperatorCast(expr, expr.Type, rc.BuiltinTypes.UInt, true), target_type);
				case BuiltinTypeSpec.Type.UInt:
					return new OperatorCast(expr, expr.Type, target_type, true);
				case BuiltinTypeSpec.Type.Long:
					return EmptyCast.Create(new OperatorCast(expr, expr.Type, rc.BuiltinTypes.ULong, true), target_type);
				}
				break;
			}
			return null;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00046C84 File Offset: 0x00044E84
		public static bool ExplicitReferenceConversionExists(TypeSpec source_type, TypeSpec target_type)
		{
			Expression expression = Convert.ExplicitReferenceConversion(null, source_type, target_type);
			if (expression == null)
			{
				return false;
			}
			if (expression == EmptyExpression.Null)
			{
				return true;
			}
			throw new InternalErrorException("Invalid probing conversion result");
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00046CB4 File Offset: 0x00044EB4
		private static Expression ExplicitReferenceConversion(Expression source, TypeSpec source_type, TypeSpec target_type)
		{
			if (source_type.BuiltinType == BuiltinTypeSpec.Type.Object && TypeManager.IsGenericParameter(target_type))
			{
				if (source != null)
				{
					return new UnboxCast(source, target_type);
				}
				return EmptyExpression.Null;
			}
			else
			{
				if (source_type.Kind == MemberKind.TypeParameter)
				{
					return Convert.ExplicitTypeParameterConversionFromT(source, source_type, target_type);
				}
				bool flag = target_type.Kind == MemberKind.Struct || target_type.Kind == MemberKind.Enum;
				if (source_type.BuiltinType == BuiltinTypeSpec.Type.ValueType && flag)
				{
					if (source != null)
					{
						return new UnboxCast(source, target_type);
					}
					return EmptyExpression.Null;
				}
				else if (source_type.BuiltinType == BuiltinTypeSpec.Type.Object || source_type.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					if (target_type.IsPointer)
					{
						return null;
					}
					if (source == null)
					{
						return EmptyExpression.Null;
					}
					if (flag)
					{
						return new UnboxCast(source, target_type);
					}
					if (!(source is Constant))
					{
						return new ClassCast(source, target_type);
					}
					return new EmptyConstantCast((Constant)source, target_type);
				}
				else if (source_type.Kind == MemberKind.Class && TypeSpec.IsBaseClass(target_type, source_type, true))
				{
					if (source != null)
					{
						return new ClassCast(source, target_type);
					}
					return EmptyExpression.Null;
				}
				else if (source_type.Kind == MemberKind.Interface)
				{
					if (!target_type.IsSealed || target_type.ImplementsInterface(source_type, true))
					{
						if (source == null)
						{
							return EmptyExpression.Null;
						}
						if (!flag)
						{
							return new ClassCast(source, target_type);
						}
						return new UnboxCast(source, target_type);
					}
					else
					{
						ArrayContainer arrayContainer = target_type as ArrayContainer;
						if (arrayContainer == null || !Convert.IList_To_Array(source_type, arrayContainer))
						{
							return null;
						}
						if (source != null)
						{
							return new ClassCast(source, target_type);
						}
						return EmptyExpression.Null;
					}
				}
				else
				{
					ArrayContainer arrayContainer2 = source_type as ArrayContainer;
					if (arrayContainer2 != null)
					{
						ArrayContainer arrayContainer3 = target_type as ArrayContainer;
						if (arrayContainer3 != null)
						{
							if (source_type.BuiltinType == BuiltinTypeSpec.Type.Array)
							{
								if (source != null)
								{
									return new ClassCast(source, target_type);
								}
								return EmptyExpression.Null;
							}
							else if (arrayContainer2.Rank == arrayContainer3.Rank)
							{
								source_type = arrayContainer2.Element;
								TypeSpec element = arrayContainer3.Element;
								if ((source_type.Kind & element.Kind & MemberKind.TypeParameter) == MemberKind.TypeParameter)
								{
									if (TypeSpec.IsValueType(source_type))
									{
										return null;
									}
								}
								else if (!TypeSpec.IsReferenceType(source_type))
								{
									return null;
								}
								if (!TypeSpec.IsReferenceType(element))
								{
									return null;
								}
								if (!Convert.ExplicitReferenceConversionExists(source_type, element))
								{
									return null;
								}
								if (source != null)
								{
									return new ClassCast(source, target_type);
								}
								return EmptyExpression.Null;
							}
						}
						if (!Convert.ArrayToIList(arrayContainer2, target_type, true))
						{
							return null;
						}
						if (source != null)
						{
							return new ClassCast(source, target_type);
						}
						return EmptyExpression.Null;
					}
					else if (target_type.IsInterface && !source_type.IsSealed && !source_type.ImplementsInterface(target_type, true))
					{
						if (source != null)
						{
							return new ClassCast(source, target_type);
						}
						return EmptyExpression.Null;
					}
					else if (source_type.BuiltinType == BuiltinTypeSpec.Type.Delegate && target_type.IsDelegate)
					{
						if (source != null)
						{
							return new ClassCast(source, target_type);
						}
						return EmptyExpression.Null;
					}
					else
					{
						if (source_type.IsDelegate && target_type.IsDelegate && source_type.MemberDefinition == target_type.MemberDefinition)
						{
							TypeParameterSpec[] typeParameters = source_type.MemberDefinition.TypeParameters;
							TypeSpec[] typeArguments = source_type.TypeArguments;
							TypeSpec[] typeArguments2 = target_type.TypeArguments;
							int i;
							for (i = 0; i < typeParameters.Length; i++)
							{
								if (!TypeSpecComparer.IsEqual(typeArguments[i], typeArguments2[i]))
								{
									if (typeParameters[i].Variance == Variance.Covariant)
									{
										if (!Convert.ImplicitReferenceConversionExists(typeArguments[i], typeArguments2[i]))
										{
											if (!Convert.ExplicitReferenceConversionExists(typeArguments[i], typeArguments2[i]))
											{
												break;
											}
										}
									}
									else if (typeParameters[i].Variance != Variance.Contravariant || !TypeSpec.IsReferenceType(typeArguments[i]) || !TypeSpec.IsReferenceType(typeArguments2[i]))
									{
										break;
									}
								}
							}
							if (i == typeParameters.Length)
							{
								if (source != null)
								{
									return new ClassCast(source, target_type);
								}
								return EmptyExpression.Null;
							}
						}
						TypeParameterSpec typeParameterSpec = target_type as TypeParameterSpec;
						if (typeParameterSpec != null)
						{
							return Convert.ExplicitTypeParameterConversionToT(source, source_type, typeParameterSpec);
						}
						return null;
					}
				}
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00047014 File Offset: 0x00045214
		public static Expression ExplicitConversionCore(ResolveContext ec, Expression expr, TypeSpec target_type, Location loc)
		{
			TypeSpec type = expr.Type;
			Expression expression = Convert.ImplicitConversionStandard(ec, expr, target_type, loc, true);
			if (expression != null)
			{
				return expression;
			}
			if (!type.IsEnum)
			{
				if (target_type.IsEnum)
				{
					if (type.BuiltinType == BuiltinTypeSpec.Type.Enum)
					{
						return new UnboxCast(expr, target_type);
					}
					TypeSpec typeSpec = target_type.IsEnum ? EnumSpec.GetUnderlyingType(target_type) : target_type;
					if (type == typeSpec)
					{
						return EmptyCast.Create(expr, target_type);
					}
					Constant constant = expr as Constant;
					if (constant != null)
					{
						constant = constant.TryReduce(ec, typeSpec);
						if (constant != null)
						{
							return constant;
						}
					}
					else
					{
						expression = Convert.ImplicitNumericConversion(expr, typeSpec);
						if (expression != null)
						{
							return EmptyCast.Create(expression, target_type);
						}
						expression = Convert.ExplicitNumericConversion(ec, expr, typeSpec);
						if (expression != null)
						{
							return EmptyCast.Create(expression, target_type);
						}
						if (type.BuiltinType == BuiltinTypeSpec.Type.IntPtr || type.BuiltinType == BuiltinTypeSpec.Type.UIntPtr)
						{
							expression = Convert.ExplicitUserConversion(ec, expr, typeSpec, loc);
							if (expression != null)
							{
								return Convert.ExplicitConversionCore(ec, expression, target_type, loc);
							}
						}
					}
				}
				else
				{
					expression = Convert.ExplicitNumericConversion(ec, expr, target_type);
					if (expression != null)
					{
						return expression;
					}
				}
				if (type != InternalType.NullLiteral)
				{
					expression = Convert.ExplicitReferenceConversion(expr, type, target_type);
					if (expression != null)
					{
						return expression;
					}
				}
				if (ec.IsUnsafe)
				{
					expression = Convert.ExplicitUnsafe(expr, target_type);
					if (expression != null)
					{
						return expression;
					}
				}
				return null;
			}
			TypeSpec typeSpec2 = target_type.IsEnum ? EnumSpec.GetUnderlyingType(target_type) : target_type;
			Expression expression2 = EmptyCast.Create(expr, EnumSpec.GetUnderlyingType(type));
			if (expression2.Type == typeSpec2)
			{
				expression = expression2;
			}
			if (expression == null)
			{
				expression = Convert.ImplicitNumericConversion(expression2, typeSpec2);
			}
			if (expression == null)
			{
				expression = Convert.ExplicitNumericConversion(ec, expression2, typeSpec2);
			}
			if (expression == null && (typeSpec2.BuiltinType == BuiltinTypeSpec.Type.IntPtr || typeSpec2.BuiltinType == BuiltinTypeSpec.Type.UIntPtr))
			{
				expression = Convert.ExplicitUserConversion(ec, expression2, typeSpec2, loc);
			}
			if (expression == null)
			{
				return null;
			}
			return EmptyCast.Create(expression, target_type);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0004719C File Offset: 0x0004539C
		public static Expression ExplicitUnsafe(Expression expr, TypeSpec target_type)
		{
			TypeSpec type = expr.Type;
			if (target_type.IsPointer)
			{
				if (type.IsPointer)
				{
					return EmptyCast.Create(expr, target_type);
				}
				switch (type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
				case BuiltinTypeSpec.Type.UShort:
				case BuiltinTypeSpec.Type.UInt:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_U);
				case BuiltinTypeSpec.Type.SByte:
				case BuiltinTypeSpec.Type.Short:
				case BuiltinTypeSpec.Type.Int:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_I);
				case BuiltinTypeSpec.Type.Long:
					return new ConvCast(expr, target_type, ConvCast.Mode.I8_I);
				case BuiltinTypeSpec.Type.ULong:
					return new ConvCast(expr, target_type, ConvCast.Mode.U8_I);
				}
			}
			if (type.IsPointer)
			{
				switch (target_type.BuiltinType)
				{
				case BuiltinTypeSpec.Type.Byte:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_U1);
				case BuiltinTypeSpec.Type.SByte:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_I1);
				case BuiltinTypeSpec.Type.Short:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_I2);
				case BuiltinTypeSpec.Type.UShort:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_U2);
				case BuiltinTypeSpec.Type.Int:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_I4);
				case BuiltinTypeSpec.Type.UInt:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_U4);
				case BuiltinTypeSpec.Type.Long:
					return new ConvCast(expr, target_type, ConvCast.Mode.I_I8);
				case BuiltinTypeSpec.Type.ULong:
					return new OpcodeCast(expr, target_type, OpCodes.Conv_U8);
				}
			}
			return null;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000472D4 File Offset: 0x000454D4
		public static Expression ExplicitConversionStandard(ResolveContext ec, Expression expr, TypeSpec target_type, Location l)
		{
			int errors = ec.Report.Errors;
			Expression expression = Convert.ImplicitConversionStandard(ec, expr, target_type, l);
			if (ec.Report.Errors > errors)
			{
				return null;
			}
			if (expression != null)
			{
				return expression;
			}
			expression = Convert.ExplicitNumericConversion(ec, expr, target_type);
			if (expression != null)
			{
				return expression;
			}
			expression = Convert.ExplicitReferenceConversion(expr, expr.Type, target_type);
			if (expression != null)
			{
				return expression;
			}
			if (ec.IsUnsafe && expr.Type.IsPointer && target_type.IsPointer && ((PointerContainer)expr.Type).Element.Kind == MemberKind.Void)
			{
				return EmptyCast.Create(expr, target_type);
			}
			expr.Error_ValueCannotBeConverted(ec, target_type, true);
			return null;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00047378 File Offset: 0x00045578
		public static Expression ExplicitConversion(ResolveContext ec, Expression expr, TypeSpec target_type, Location loc)
		{
			Expression expression = Convert.ExplicitConversionCore(ec, expr, target_type, loc);
			if (expression != null)
			{
				if (expression == expr)
				{
					if (target_type.BuiltinType == BuiltinTypeSpec.Type.Float)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R4);
					}
					if (target_type.BuiltinType == BuiltinTypeSpec.Type.Double)
					{
						return new OpcodeCast(expr, target_type, OpCodes.Conv_R8);
					}
				}
				return expression;
			}
			TypeSpec type = expr.Type;
			if (target_type.IsNullableType)
			{
				if (type.IsNullableType)
				{
					TypeSpec target_type2 = NullableInfo.GetUnderlyingType(target_type);
					Expression expression2 = Unwrap.Create(expr);
					expression = Convert.ExplicitConversion(ec, expression2, target_type2, expr.Location);
					if (expression == null)
					{
						return null;
					}
					return new LiftedConversion(expression, expression2, target_type).Resolve(ec);
				}
				else
				{
					if (type.BuiltinType == BuiltinTypeSpec.Type.Object)
					{
						return new UnboxCast(expr, target_type);
					}
					TypeSpec target_type2 = TypeManager.GetTypeArguments(target_type)[0];
					expression = Convert.ExplicitConversionCore(ec, expr, target_type2, loc);
					if (expression != null)
					{
						if (!TypeSpec.IsReferenceType(expr.Type))
						{
							return Wrap.Create(expression, target_type);
						}
						return new UnboxCast(expr, target_type);
					}
				}
			}
			else if (type.IsNullableType)
			{
				expression = Convert.ImplicitBoxingConversion(expr, NullableInfo.GetUnderlyingType(type), target_type);
				if (expression != null)
				{
					return expression;
				}
				expression = Unwrap.Create(expr, false);
				expression = Convert.ExplicitConversionCore(ec, expression, target_type, loc);
				if (expression != null)
				{
					return EmptyCast.Create(expression, target_type);
				}
			}
			expression = Convert.ExplicitUserConversion(ec, expr, target_type, loc);
			if (expression != null)
			{
				return expression;
			}
			expr.Error_ValueCannotBeConverted(ec, target_type, true);
			return null;
		}

		// Token: 0x0200038A RID: 906
		[Flags]
		public enum UserConversionRestriction
		{
			// Token: 0x04000F60 RID: 3936
			None = 0,
			// Token: 0x04000F61 RID: 3937
			ImplicitOnly = 1,
			// Token: 0x04000F62 RID: 3938
			ProbingOnly = 2,
			// Token: 0x04000F63 RID: 3939
			NullableSourceOnly = 4
		}
	}
}
