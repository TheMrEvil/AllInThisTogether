using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x02000330 RID: 816
	internal static class TypeUtils
	{
		// Token: 0x0600189C RID: 6300 RVA: 0x00052CBF File Offset: 0x00050EBF
		public static Type GetNonNullableType(this Type type)
		{
			if (!type.IsNullableType())
			{
				return type;
			}
			return type.GetGenericArguments()[0];
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00052CD3 File Offset: 0x00050ED3
		public static Type GetNullableType(this Type type)
		{
			if (type.IsValueType && !type.IsNullableType())
			{
				return typeof(Nullable<>).MakeGenericType(new Type[]
				{
					type
				});
			}
			return type;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00052D00 File Offset: 0x00050F00
		public static bool IsNullableType(this Type type)
		{
			return type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00052D21 File Offset: 0x00050F21
		public static bool IsNullableOrReferenceType(this Type type)
		{
			return !type.IsValueType || type.IsNullableType();
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00052D33 File Offset: 0x00050F33
		public static bool IsBool(this Type type)
		{
			return type.GetNonNullableType() == typeof(bool);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00052D4C File Offset: 0x00050F4C
		public static bool IsNumeric(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Char <= 10)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00052D7C File Offset: 0x00050F7C
		public static bool IsInteger(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.SByte <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00052DAC File Offset: 0x00050FAC
		public static bool IsInteger64(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Int64 <= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00052DDC File Offset: 0x00050FDC
		public static bool IsArithmetic(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode - TypeCode.Int16 <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00052E0C File Offset: 0x0005100C
		public static bool IsUnsignedInt(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				switch (type.GetTypeCode())
				{
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00052E54 File Offset: 0x00051054
		public static bool IsIntegerOrBool(this Type type)
		{
			type = type.GetNonNullableType();
			if (!type.IsEnum)
			{
				TypeCode typeCode = type.GetTypeCode();
				if (typeCode == TypeCode.Boolean || typeCode - TypeCode.SByte <= 7)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00052E85 File Offset: 0x00051085
		public static bool IsNumericOrBool(this Type type)
		{
			return type.IsNumeric() || type.IsBool();
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00052E98 File Offset: 0x00051098
		public static bool IsValidInstanceType(MemberInfo member, Type instanceType)
		{
			Type declaringType = member.DeclaringType;
			if (TypeUtils.AreReferenceAssignable(declaringType, instanceType))
			{
				return true;
			}
			if (declaringType == null)
			{
				return false;
			}
			if (instanceType.IsValueType)
			{
				if (TypeUtils.AreReferenceAssignable(declaringType, typeof(object)))
				{
					return true;
				}
				if (TypeUtils.AreReferenceAssignable(declaringType, typeof(ValueType)))
				{
					return true;
				}
				if (instanceType.IsEnum && TypeUtils.AreReferenceAssignable(declaringType, typeof(Enum)))
				{
					return true;
				}
				if (declaringType.IsInterface)
				{
					foreach (Type src in instanceType.GetTypeInfo().ImplementedInterfaces)
					{
						if (TypeUtils.AreReferenceAssignable(declaringType, src))
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00052F68 File Offset: 0x00051168
		public static bool HasIdentityPrimitiveOrNullableConversionTo(this Type source, Type dest)
		{
			return TypeUtils.AreEquivalent(source, dest) || (source.IsNullableType() && TypeUtils.AreEquivalent(dest, source.GetNonNullableType())) || (dest.IsNullableType() && TypeUtils.AreEquivalent(source, dest.GetNonNullableType())) || (source.IsConvertible() && dest.IsConvertible() && (dest.GetNonNullableType() != typeof(bool) || (source.IsEnum && source.GetEnumUnderlyingType() == typeof(bool))));
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00052FFC File Offset: 0x000511FC
		public static bool HasReferenceConversionTo(this Type source, Type dest)
		{
			if (source == typeof(void) || dest == typeof(void))
			{
				return false;
			}
			Type nonNullableType = source.GetNonNullableType();
			Type nonNullableType2 = dest.GetNonNullableType();
			return nonNullableType.IsAssignableFrom(nonNullableType2) || nonNullableType2.IsAssignableFrom(nonNullableType) || (source.IsInterface || dest.IsInterface) || TypeUtils.IsLegalExplicitVariantDelegateConversion(source, dest) || ((source.IsArray || dest.IsArray) && source.StrictHasReferenceConversionTo(dest, true));
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0005308C File Offset: 0x0005128C
		private static bool StrictHasReferenceConversionTo(this Type source, Type dest, bool skipNonArray)
		{
			for (;;)
			{
				if (!skipNonArray)
				{
					if (source.IsValueType | dest.IsValueType)
					{
						break;
					}
					if (source.IsAssignableFrom(dest) || dest.IsAssignableFrom(source))
					{
						return true;
					}
					if (source.IsInterface)
					{
						if (dest.IsInterface || (dest.IsClass && !dest.IsSealed))
						{
							return true;
						}
					}
					else if (dest.IsInterface && source.IsClass && !source.IsSealed)
					{
						return true;
					}
				}
				if (!source.IsArray)
				{
					goto IL_B2;
				}
				if (!dest.IsArray)
				{
					goto IL_AA;
				}
				if (source.GetArrayRank() != dest.GetArrayRank() || source.IsSZArray != dest.IsSZArray)
				{
					return false;
				}
				source = source.GetElementType();
				dest = dest.GetElementType();
				skipNonArray = false;
			}
			return false;
			IL_AA:
			return TypeUtils.HasArrayToInterfaceConversion(source, dest);
			IL_B2:
			if (dest.IsArray)
			{
				return TypeUtils.HasInterfaceToArrayConversion(source, dest) || TypeUtils.IsImplicitReferenceConversion(typeof(Array), source);
			}
			return TypeUtils.IsLegalExplicitVariantDelegateConversion(source, dest);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00053178 File Offset: 0x00051378
		private static bool HasArrayToInterfaceConversion(Type source, Type dest)
		{
			if (!source.IsSZArray || !dest.IsInterface || !dest.IsGenericType)
			{
				return false;
			}
			Type[] genericArguments = dest.GetGenericArguments();
			if (genericArguments.Length != 1)
			{
				return false;
			}
			Type genericTypeDefinition = dest.GetGenericTypeDefinition();
			foreach (Type t in TypeUtils.s_arrayAssignableInterfaces)
			{
				if (TypeUtils.AreEquivalent(genericTypeDefinition, t))
				{
					return source.GetElementType().StrictHasReferenceConversionTo(genericArguments[0], false);
				}
			}
			return false;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000531EC File Offset: 0x000513EC
		private static bool HasInterfaceToArrayConversion(Type source, Type dest)
		{
			if (!dest.IsSZArray || !source.IsInterface || !source.IsGenericType)
			{
				return false;
			}
			Type[] genericArguments = source.GetGenericArguments();
			if (genericArguments.Length != 1)
			{
				return false;
			}
			Type genericTypeDefinition = source.GetGenericTypeDefinition();
			foreach (Type t in TypeUtils.s_arrayAssignableInterfaces)
			{
				if (TypeUtils.AreEquivalent(genericTypeDefinition, t))
				{
					return genericArguments[0].StrictHasReferenceConversionTo(dest.GetElementType(), false);
				}
			}
			return false;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0005325D File Offset: 0x0005145D
		private static bool IsCovariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.Covariant) > GenericParameterAttributes.None;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0005326A File Offset: 0x0005146A
		private static bool IsContravariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.Contravariant) > GenericParameterAttributes.None;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00053277 File Offset: 0x00051477
		private static bool IsInvariant(Type t)
		{
			return (t.GenericParameterAttributes & GenericParameterAttributes.VarianceMask) == GenericParameterAttributes.None;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00053284 File Offset: 0x00051484
		private static bool IsDelegate(Type t)
		{
			return t.IsSubclassOf(typeof(MulticastDelegate));
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00053298 File Offset: 0x00051498
		public static bool IsLegalExplicitVariantDelegateConversion(Type source, Type dest)
		{
			if (!TypeUtils.IsDelegate(source) || !TypeUtils.IsDelegate(dest) || !source.IsGenericType || !dest.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = source.GetGenericTypeDefinition();
			if (dest.GetGenericTypeDefinition() != genericTypeDefinition)
			{
				return false;
			}
			Type[] genericArguments = genericTypeDefinition.GetGenericArguments();
			Type[] genericArguments2 = source.GetGenericArguments();
			Type[] genericArguments3 = dest.GetGenericArguments();
			for (int i = 0; i < genericArguments.Length; i++)
			{
				Type type = genericArguments2[i];
				Type type2 = genericArguments3[i];
				if (!TypeUtils.AreEquivalent(type, type2))
				{
					Type t = genericArguments[i];
					if (TypeUtils.IsInvariant(t))
					{
						return false;
					}
					if (TypeUtils.IsCovariant(t))
					{
						if (!type.HasReferenceConversionTo(type2))
						{
							return false;
						}
					}
					else if (TypeUtils.IsContravariant(t) && (type.IsValueType || type2.IsValueType))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00053364 File Offset: 0x00051564
		public static bool IsConvertible(this Type type)
		{
			type = type.GetNonNullableType();
			if (type.IsEnum)
			{
				return true;
			}
			TypeCode typeCode = type.GetTypeCode();
			return typeCode - TypeCode.Boolean <= 11;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00053394 File Offset: 0x00051594
		public static bool HasReferenceEquality(Type left, Type right)
		{
			return !left.IsValueType && !right.IsValueType && (left.IsInterface || right.IsInterface || TypeUtils.AreReferenceAssignable(left, right) || TypeUtils.AreReferenceAssignable(right, left));
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000533CC File Offset: 0x000515CC
		public static bool HasBuiltInEqualityOperator(Type left, Type right)
		{
			if (left.IsInterface && !right.IsValueType)
			{
				return true;
			}
			if (right.IsInterface && !left.IsValueType)
			{
				return true;
			}
			if (!left.IsValueType && !right.IsValueType && (TypeUtils.AreReferenceAssignable(left, right) || TypeUtils.AreReferenceAssignable(right, left)))
			{
				return true;
			}
			if (!TypeUtils.AreEquivalent(left, right))
			{
				return false;
			}
			Type nonNullableType = left.GetNonNullableType();
			return nonNullableType == typeof(bool) || nonNullableType.IsNumeric() || nonNullableType.IsEnum;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00053455 File Offset: 0x00051655
		public static bool IsImplicitlyConvertibleTo(this Type source, Type destination)
		{
			return TypeUtils.AreEquivalent(source, destination) || TypeUtils.IsImplicitNumericConversion(source, destination) || TypeUtils.IsImplicitReferenceConversion(source, destination) || TypeUtils.IsImplicitBoxingConversion(source, destination) || TypeUtils.IsImplicitNullableConversion(source, destination);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00053484 File Offset: 0x00051684
		public static MethodInfo GetUserDefinedCoercionMethod(Type convertFrom, Type convertToType)
		{
			Type nonNullableType = convertFrom.GetNonNullableType();
			Type nonNullableType2 = convertToType.GetNonNullableType();
			MethodInfo[] methods = nonNullableType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			MethodInfo methodInfo = TypeUtils.FindConversionOperator(methods, convertFrom, convertToType);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			MethodInfo[] methods2 = nonNullableType2.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			methodInfo = TypeUtils.FindConversionOperator(methods2, convertFrom, convertToType);
			if (methodInfo != null)
			{
				return methodInfo;
			}
			if (TypeUtils.AreEquivalent(nonNullableType, convertFrom) && TypeUtils.AreEquivalent(nonNullableType2, convertToType))
			{
				return null;
			}
			MethodInfo result;
			if ((result = TypeUtils.FindConversionOperator(methods, nonNullableType, nonNullableType2)) == null && (result = TypeUtils.FindConversionOperator(methods2, nonNullableType, nonNullableType2)) == null)
			{
				result = (TypeUtils.FindConversionOperator(methods, nonNullableType, convertToType) ?? TypeUtils.FindConversionOperator(methods2, nonNullableType, convertToType));
			}
			return result;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00053520 File Offset: 0x00051720
		private static MethodInfo FindConversionOperator(MethodInfo[] methods, Type typeFrom, Type typeTo)
		{
			foreach (MethodInfo methodInfo in methods)
			{
				if ((methodInfo.Name == "op_Implicit" || methodInfo.Name == "op_Explicit") && TypeUtils.AreEquivalent(methodInfo.ReturnType, typeTo))
				{
					ParameterInfo[] parametersCached = methodInfo.GetParametersCached();
					if (parametersCached.Length == 1 && TypeUtils.AreEquivalent(parametersCached[0].ParameterType, typeFrom))
					{
						return methodInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00053594 File Offset: 0x00051794
		private static bool IsImplicitNumericConversion(Type source, Type destination)
		{
			TypeCode typeCode = source.GetTypeCode();
			TypeCode typeCode2 = destination.GetTypeCode();
			switch (typeCode)
			{
			case TypeCode.Char:
				if (typeCode2 - TypeCode.UInt16 <= 7)
				{
					return true;
				}
				break;
			case TypeCode.SByte:
				switch (typeCode2)
				{
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.Byte:
				if (typeCode2 - TypeCode.Int16 <= 8)
				{
					return true;
				}
				break;
			case TypeCode.Int16:
				switch (typeCode2)
				{
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					return true;
				}
				break;
			case TypeCode.UInt16:
				if (typeCode2 - TypeCode.Int32 <= 6)
				{
					return true;
				}
				break;
			case TypeCode.Int32:
				if (typeCode2 == TypeCode.Int64 || typeCode2 - TypeCode.Single <= 2)
				{
					return true;
				}
				break;
			case TypeCode.UInt32:
				if (typeCode2 - TypeCode.Int64 <= 4)
				{
					return true;
				}
				break;
			case TypeCode.Int64:
			case TypeCode.UInt64:
				if (typeCode2 - TypeCode.Single <= 2)
				{
					return true;
				}
				break;
			case TypeCode.Single:
				return typeCode2 == TypeCode.Double;
			}
			return false;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0005367D File Offset: 0x0005187D
		private static bool IsImplicitReferenceConversion(Type source, Type destination)
		{
			return destination.IsAssignableFrom(source);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00053688 File Offset: 0x00051888
		private static bool IsImplicitBoxingConversion(Type source, Type destination)
		{
			return (source.IsValueType && (destination == typeof(object) || destination == typeof(ValueType))) || (source.IsEnum && destination == typeof(Enum));
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000536DD File Offset: 0x000518DD
		private static bool IsImplicitNullableConversion(Type source, Type destination)
		{
			return destination.IsNullableType() && source.GetNonNullableType().IsImplicitlyConvertibleTo(destination.GetNonNullableType());
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000536FC File Offset: 0x000518FC
		public static Type FindGenericType(Type definition, Type type)
		{
			while (type != null && type != typeof(object))
			{
				if (type.IsConstructedGenericType && TypeUtils.AreEquivalent(type.GetGenericTypeDefinition(), definition))
				{
					return type;
				}
				if (definition.IsInterface)
				{
					foreach (Type type2 in type.GetTypeInfo().ImplementedInterfaces)
					{
						Type type3 = TypeUtils.FindGenericType(definition, type2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000537A0 File Offset: 0x000519A0
		public static MethodInfo GetBooleanOperator(Type type, string name)
		{
			MethodInfo anyStaticMethodValidated;
			for (;;)
			{
				anyStaticMethodValidated = type.GetAnyStaticMethodValidated(name, new Type[]
				{
					type
				});
				if (anyStaticMethodValidated != null && anyStaticMethodValidated.IsSpecialName && !anyStaticMethodValidated.ContainsGenericParameters)
				{
					break;
				}
				type = type.BaseType;
				if (!(type != null))
				{
					goto Block_3;
				}
			}
			return anyStaticMethodValidated;
			Block_3:
			return null;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000537EC File Offset: 0x000519EC
		public static Type GetNonRefType(this Type type)
		{
			if (!type.IsByRef)
			{
				return type;
			}
			return type.GetElementType();
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000537FE File Offset: 0x000519FE
		public static bool AreEquivalent(Type t1, Type t2)
		{
			return t1 != null && t1.IsEquivalentTo(t2);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00053812 File Offset: 0x00051A12
		public static bool AreReferenceAssignable(Type dest, Type src)
		{
			return TypeUtils.AreEquivalent(dest, src) || (!dest.IsValueType && !src.IsValueType && dest.IsAssignableFrom(src));
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00053838 File Offset: 0x00051A38
		public static bool IsSameOrSubclass(Type type, Type subType)
		{
			return TypeUtils.AreEquivalent(type, subType) || subType.IsSubclassOf(type);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0005384C File Offset: 0x00051A4C
		public static void ValidateType(Type type, string paramName)
		{
			TypeUtils.ValidateType(type, paramName, false, false);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00053857 File Offset: 0x00051A57
		public static void ValidateType(Type type, string paramName, bool allowByRef, bool allowPointer)
		{
			if (TypeUtils.ValidateType(type, paramName, -1))
			{
				if (!allowByRef && type.IsByRef)
				{
					throw System.Linq.Expressions.Error.TypeMustNotBeByRef(paramName);
				}
				if (!allowPointer && type.IsPointer)
				{
					throw System.Linq.Expressions.Error.TypeMustNotBePointer(paramName);
				}
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00053887 File Offset: 0x00051A87
		public static bool ValidateType(Type type, string paramName, int index)
		{
			if (type == typeof(void))
			{
				return false;
			}
			if (type.ContainsGenericParameters)
			{
				throw type.IsGenericTypeDefinition ? System.Linq.Expressions.Error.TypeIsGeneric(type, paramName, index) : System.Linq.Expressions.Error.TypeContainsGenericParameters(type, paramName, index);
			}
			return true;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000538C1 File Offset: 0x00051AC1
		public static MethodInfo GetInvokeMethod(this Type delegateType)
		{
			return delegateType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000538D0 File Offset: 0x00051AD0
		internal static bool IsUnsigned(this Type type)
		{
			return type.GetNonNullableType().GetTypeCode().IsUnsigned();
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000538E2 File Offset: 0x00051AE2
		internal static bool IsUnsigned(this TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Char:
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00053915 File Offset: 0x00051B15
		internal static bool IsFloatingPoint(this Type type)
		{
			return type.GetNonNullableType().GetTypeCode().IsFloatingPoint();
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00053927 File Offset: 0x00051B27
		internal static bool IsFloatingPoint(this TypeCode typeCode)
		{
			return typeCode - TypeCode.Single <= 1;
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00053934 File Offset: 0x00051B34
		// Note: this type is marked as 'beforefieldinit'.
		static TypeUtils()
		{
		}

		// Token: 0x04000BE9 RID: 3049
		private static readonly Type[] s_arrayAssignableInterfaces = (from i in typeof(int[]).GetInterfaces()
		where i.IsGenericType
		select i.GetGenericTypeDefinition()).ToArray<Type>();

		// Token: 0x02000331 RID: 817
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060018CC RID: 6348 RVA: 0x00053984 File Offset: 0x00051B84
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060018CD RID: 6349 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x060018CE RID: 6350 RVA: 0x00053990 File Offset: 0x00051B90
			internal bool <.cctor>b__48_0(Type i)
			{
				return i.IsGenericType;
			}

			// Token: 0x060018CF RID: 6351 RVA: 0x00053998 File Offset: 0x00051B98
			internal Type <.cctor>b__48_1(Type i)
			{
				return i.GetGenericTypeDefinition();
			}

			// Token: 0x04000BEA RID: 3050
			public static readonly TypeUtils.<>c <>9 = new TypeUtils.<>c();
		}
	}
}
