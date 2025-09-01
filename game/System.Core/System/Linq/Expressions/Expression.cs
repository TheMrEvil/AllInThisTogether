using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Globalization;
using System.IO;
using System.Linq.Expressions.Compiler;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace System.Linq.Expressions
{
	/// <summary>Provides the base class from which the classes that represent expression tree nodes are derived. It also contains <see langword="static" /> (<see langword="Shared" /> in Visual Basic) factory methods to create the various node types. This is an <see langword="abstract" /> class.</summary>
	// Token: 0x02000210 RID: 528
	public abstract class Expression
	{
		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Assign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000CEF RID: 3311 RVA: 0x0002D1BC File Offset: 0x0002B3BC
		public static BinaryExpression Assign(Expression left, Expression right)
		{
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			TypeUtils.ValidateType(left.Type, "left", true, true);
			TypeUtils.ValidateType(right.Type, "right", true, true);
			if (!TypeUtils.AreReferenceAssignable(left.Type, right.Type))
			{
				throw Error.ExpressionTypeDoesNotMatchAssignment(right.Type, left.Type);
			}
			return new AssignBinaryExpression(left, right);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002D230 File Offset: 0x0002B430
		private static BinaryExpression GetUserDefinedBinaryOperator(ExpressionType binaryType, string name, Expression left, Expression right, bool liftToNull)
		{
			MethodInfo userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, left.Type, right.Type, name);
			if (userDefinedBinaryOperator != null)
			{
				return new MethodBinaryExpression(binaryType, left, right, userDefinedBinaryOperator.ReturnType, userDefinedBinaryOperator);
			}
			if (left.Type.IsNullableType() && right.Type.IsNullableType())
			{
				Type nonNullableType = left.Type.GetNonNullableType();
				Type nonNullableType2 = right.Type.GetNonNullableType();
				userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, nonNullableType, nonNullableType2, name);
				if (userDefinedBinaryOperator != null && userDefinedBinaryOperator.ReturnType.IsValueType && !userDefinedBinaryOperator.ReturnType.IsNullableType())
				{
					if (userDefinedBinaryOperator.ReturnType != typeof(bool) || liftToNull)
					{
						return new MethodBinaryExpression(binaryType, left, right, userDefinedBinaryOperator.ReturnType.GetNullableType(), userDefinedBinaryOperator);
					}
					return new MethodBinaryExpression(binaryType, left, right, typeof(bool), userDefinedBinaryOperator);
				}
			}
			return null;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002D314 File Offset: 0x0002B514
		private static BinaryExpression GetMethodBasedBinaryOperator(ExpressionType binaryType, Expression left, Expression right, MethodInfo method, bool liftToNull)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 2)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], left.Type) && Expression.ParameterIsAssignable(parametersCached[1], right.Type))
			{
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, left.Type, binaryType, method.Name);
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[1].ParameterType, right.Type, binaryType, method.Name);
				return new MethodBinaryExpression(binaryType, left, right, method.ReturnType, method);
			}
			if (!left.Type.IsNullableType() || !right.Type.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.Type.GetNonNullableType()) || !Expression.ParameterIsAssignable(parametersCached[1], right.Type.GetNonNullableType()) || !method.ReturnType.IsValueType || method.ReturnType.IsNullableType())
			{
				throw Error.OperandTypesDoNotMatchParameters(binaryType, method.Name);
			}
			if (method.ReturnType != typeof(bool) || liftToNull)
			{
				return new MethodBinaryExpression(binaryType, left, right, method.ReturnType.GetNullableType(), method);
			}
			return new MethodBinaryExpression(binaryType, left, right, typeof(bool), method);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0002D45C File Offset: 0x0002B65C
		private static BinaryExpression GetMethodBasedAssignOperator(ExpressionType binaryType, Expression left, Expression right, MethodInfo method, LambdaExpression conversion, bool liftToNull)
		{
			BinaryExpression binaryExpression = Expression.GetMethodBasedBinaryOperator(binaryType, left, right, method, liftToNull);
			if (conversion == null)
			{
				if (!TypeUtils.AreReferenceAssignable(left.Type, binaryExpression.Type))
				{
					throw Error.UserDefinedOpMustHaveValidReturnType(binaryType, binaryExpression.Method.Name);
				}
			}
			else
			{
				Expression.ValidateOpAssignConversionLambda(conversion, binaryExpression.Left, binaryExpression.Method, binaryExpression.NodeType);
				binaryExpression = new OpAssignMethodConversionBinaryExpression(binaryExpression.NodeType, binaryExpression.Left, binaryExpression.Right, binaryExpression.Left.Type, binaryExpression.Method, conversion);
			}
			return binaryExpression;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		private static BinaryExpression GetUserDefinedBinaryOperatorOrThrow(ExpressionType binaryType, string name, Expression left, Expression right, bool liftToNull)
		{
			BinaryExpression userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, name, left, right, liftToNull);
			if (userDefinedBinaryOperator != null)
			{
				ParameterInfo[] parametersCached = userDefinedBinaryOperator.Method.GetParametersCached();
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, left.Type, binaryType, name);
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[1].ParameterType, right.Type, binaryType, name);
				return userDefinedBinaryOperator;
			}
			throw Error.BinaryOperatorNotDefined(binaryType, left.Type, right.Type);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0002D554 File Offset: 0x0002B754
		private static BinaryExpression GetUserDefinedAssignOperatorOrThrow(ExpressionType binaryType, string name, Expression left, Expression right, LambdaExpression conversion, bool liftToNull)
		{
			BinaryExpression binaryExpression = Expression.GetUserDefinedBinaryOperatorOrThrow(binaryType, name, left, right, liftToNull);
			if (conversion == null)
			{
				if (!TypeUtils.AreReferenceAssignable(left.Type, binaryExpression.Type))
				{
					throw Error.UserDefinedOpMustHaveValidReturnType(binaryType, binaryExpression.Method.Name);
				}
			}
			else
			{
				Expression.ValidateOpAssignConversionLambda(conversion, binaryExpression.Left, binaryExpression.Method, binaryExpression.NodeType);
				binaryExpression = new OpAssignMethodConversionBinaryExpression(binaryExpression.NodeType, binaryExpression.Left, binaryExpression.Right, binaryExpression.Left.Type, binaryExpression.Method, conversion);
			}
			return binaryExpression;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
		private static MethodInfo GetUserDefinedBinaryOperator(ExpressionType binaryType, Type leftType, Type rightType, string name)
		{
			Type[] types = new Type[]
			{
				leftType,
				rightType
			};
			Type nonNullableType = leftType.GetNonNullableType();
			Type nonNullableType2 = rightType.GetNonNullableType();
			MethodInfo methodInfo = nonNullableType.GetAnyStaticMethodValidated(name, types);
			if (methodInfo == null && !TypeUtils.AreEquivalent(leftType, rightType))
			{
				methodInfo = nonNullableType2.GetAnyStaticMethodValidated(name, types);
			}
			if (Expression.IsLiftingConditionalLogicalOperator(leftType, rightType, methodInfo, binaryType))
			{
				methodInfo = Expression.GetUserDefinedBinaryOperator(binaryType, nonNullableType, nonNullableType2, name);
			}
			return methodInfo;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002D644 File Offset: 0x0002B844
		private static bool IsLiftingConditionalLogicalOperator(Type left, Type right, MethodInfo method, ExpressionType binaryType)
		{
			return right.IsNullableType() && left.IsNullableType() && method == null && (binaryType == ExpressionType.AndAlso || binaryType == ExpressionType.OrElse);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002D66C File Offset: 0x0002B86C
		internal static bool ParameterIsAssignable(ParameterInfo pi, Type argType)
		{
			Type type = pi.ParameterType;
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			return TypeUtils.AreReferenceAssignable(type, argType);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002D696 File Offset: 0x0002B896
		private static void ValidateParamswithOperandsOrThrow(Type paramType, Type operandType, ExpressionType exprType, string name)
		{
			if (paramType.IsNullableType() && !operandType.IsNullableType())
			{
				throw Error.OperandTypesDoNotMatchParameters(exprType, name);
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002D6B8 File Offset: 0x0002B8B8
		private static void ValidateOperator(MethodInfo method)
		{
			Expression.ValidateMethodInfo(method, "method");
			if (!method.IsStatic)
			{
				throw Error.UserDefinedOperatorMustBeStatic(method, "method");
			}
			if (method.ReturnType == typeof(void))
			{
				throw Error.UserDefinedOperatorMustNotBeVoid(method, "method");
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002D707 File Offset: 0x0002B907
		private static void ValidateMethodInfo(MethodInfo method, string paramName)
		{
			if (method.ContainsGenericParameters)
			{
				throw method.IsGenericMethodDefinition ? Error.MethodIsGeneric(method, paramName) : Error.MethodContainsGenericParameters(method, paramName);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002D72A File Offset: 0x0002B92A
		private static bool IsNullComparison(Expression left, Expression right)
		{
			if (!Expression.IsNullConstant(left))
			{
				return Expression.IsNullConstant(right) && left.Type.IsNullableType();
			}
			return !Expression.IsNullConstant(right) && right.Type.IsNullableType();
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002D760 File Offset: 0x0002B960
		private static bool IsNullConstant(Expression e)
		{
			ConstantExpression constantExpression = e as ConstantExpression;
			return constantExpression != null && constantExpression.Value == null;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002D784 File Offset: 0x0002B984
		private static void ValidateUserDefinedConditionalLogicOperator(ExpressionType nodeType, Type left, Type right, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 2)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left) && (!left.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, method.Name);
			}
			if (!Expression.ParameterIsAssignable(parametersCached[1], right) && (!right.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[1], right.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, method.Name);
			}
			if (parametersCached[0].ParameterType != parametersCached[1].ParameterType)
			{
				throw Error.UserDefinedOpMustHaveConsistentTypes(nodeType, method.Name);
			}
			if (method.ReturnType != parametersCached[0].ParameterType)
			{
				throw Error.UserDefinedOpMustHaveConsistentTypes(nodeType, method.Name);
			}
			if (Expression.IsValidLiftedConditionalLogicalOperator(left, right, parametersCached))
			{
				left = left.GetNonNullableType();
			}
			Type declaringType = method.DeclaringType;
			if (declaringType == null)
			{
				throw Error.LogicalOperatorMustHaveBooleanOperators(nodeType, method.Name);
			}
			MethodInfo booleanOperator = TypeUtils.GetBooleanOperator(declaringType, "op_True");
			MethodInfo booleanOperator2 = TypeUtils.GetBooleanOperator(declaringType, "op_False");
			if (booleanOperator == null || booleanOperator.ReturnType != typeof(bool) || booleanOperator2 == null || booleanOperator2.ReturnType != typeof(bool))
			{
				throw Error.LogicalOperatorMustHaveBooleanOperators(nodeType, method.Name);
			}
			Expression.VerifyOpTrueFalse(nodeType, left, booleanOperator2, "method");
			Expression.VerifyOpTrueFalse(nodeType, left, booleanOperator, "method");
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002D920 File Offset: 0x0002BB20
		private static void VerifyOpTrueFalse(ExpressionType nodeType, Type left, MethodInfo opTrue, string paramName)
		{
			ParameterInfo[] parametersCached = opTrue.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(opTrue, paramName);
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left) && (!left.IsNullableType() || !Expression.ParameterIsAssignable(parametersCached[0], left.GetNonNullableType())))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, opTrue.Name);
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002D977 File Offset: 0x0002BB77
		private static bool IsValidLiftedConditionalLogicalOperator(Type left, Type right, ParameterInfo[] pms)
		{
			return TypeUtils.AreEquivalent(left, right) && right.IsNullableType() && TypeUtils.AreEquivalent(pms[1].ParameterType, right.GetNonNullableType());
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" />, given the left and right operands, by calling an appropriate factory method.</summary>
		/// <param name="binaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of binary operation.</param>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand.</param>
		/// <returns>The <see cref="T:System.Linq.Expressions.BinaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="binaryType" /> does not correspond to a binary expression node.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		// Token: 0x06000D00 RID: 3328 RVA: 0x0002D99F File Offset: 0x0002BB9F
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right)
		{
			return Expression.MakeBinary(binaryType, left, right, false, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" />, given the left operand, right operand and implementing method, by calling the appropriate factory method.</summary>
		/// <param name="binaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of binary operation.</param>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand.</param>
		/// <param name="liftToNull">
		///       <see langword="true" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="true" />; <see langword="false" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="false" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that specifies the implementing method.</param>
		/// <returns>The <see cref="T:System.Linq.Expressions.BinaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="binaryType" /> does not correspond to a binary expression node.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		// Token: 0x06000D01 RID: 3329 RVA: 0x0002D9AC File Offset: 0x0002BBAC
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			return Expression.MakeBinary(binaryType, left, right, liftToNull, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" />, given the left operand, right operand, implementing method and type conversion function, by calling the appropriate factory method.</summary>
		/// <param name="binaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of binary operation.</param>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand.</param>
		/// <param name="liftToNull">
		///       <see langword="true" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="true" />; <see langword="false" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="false" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that specifies the implementing method.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that represents a type conversion function. This parameter is used only if <paramref name="binaryType" /> is <see cref="F:System.Linq.Expressions.ExpressionType.Coalesce" /> or compound assignment..</param>
		/// <returns>The <see cref="T:System.Linq.Expressions.BinaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="binaryType" /> does not correspond to a binary expression node.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		// Token: 0x06000D02 RID: 3330 RVA: 0x0002D9BC File Offset: 0x0002BBBC
		public static BinaryExpression MakeBinary(ExpressionType binaryType, Expression left, Expression right, bool liftToNull, MethodInfo method, LambdaExpression conversion)
		{
			switch (binaryType)
			{
			case ExpressionType.Add:
				return Expression.Add(left, right, method);
			case ExpressionType.AddChecked:
				return Expression.AddChecked(left, right, method);
			case ExpressionType.And:
				return Expression.And(left, right, method);
			case ExpressionType.AndAlso:
				return Expression.AndAlso(left, right, method);
			case ExpressionType.ArrayIndex:
				return Expression.ArrayIndex(left, right);
			case ExpressionType.Coalesce:
				return Expression.Coalesce(left, right, conversion);
			case ExpressionType.Divide:
				return Expression.Divide(left, right, method);
			case ExpressionType.Equal:
				return Expression.Equal(left, right, liftToNull, method);
			case ExpressionType.ExclusiveOr:
				return Expression.ExclusiveOr(left, right, method);
			case ExpressionType.GreaterThan:
				return Expression.GreaterThan(left, right, liftToNull, method);
			case ExpressionType.GreaterThanOrEqual:
				return Expression.GreaterThanOrEqual(left, right, liftToNull, method);
			case ExpressionType.LeftShift:
				return Expression.LeftShift(left, right, method);
			case ExpressionType.LessThan:
				return Expression.LessThan(left, right, liftToNull, method);
			case ExpressionType.LessThanOrEqual:
				return Expression.LessThanOrEqual(left, right, liftToNull, method);
			case ExpressionType.Modulo:
				return Expression.Modulo(left, right, method);
			case ExpressionType.Multiply:
				return Expression.Multiply(left, right, method);
			case ExpressionType.MultiplyChecked:
				return Expression.MultiplyChecked(left, right, method);
			case ExpressionType.NotEqual:
				return Expression.NotEqual(left, right, liftToNull, method);
			case ExpressionType.Or:
				return Expression.Or(left, right, method);
			case ExpressionType.OrElse:
				return Expression.OrElse(left, right, method);
			case ExpressionType.Power:
				return Expression.Power(left, right, method);
			case ExpressionType.RightShift:
				return Expression.RightShift(left, right, method);
			case ExpressionType.Subtract:
				return Expression.Subtract(left, right, method);
			case ExpressionType.SubtractChecked:
				return Expression.SubtractChecked(left, right, method);
			case ExpressionType.Assign:
				return Expression.Assign(left, right);
			case ExpressionType.AddAssign:
				return Expression.AddAssign(left, right, method, conversion);
			case ExpressionType.AndAssign:
				return Expression.AndAssign(left, right, method, conversion);
			case ExpressionType.DivideAssign:
				return Expression.DivideAssign(left, right, method, conversion);
			case ExpressionType.ExclusiveOrAssign:
				return Expression.ExclusiveOrAssign(left, right, method, conversion);
			case ExpressionType.LeftShiftAssign:
				return Expression.LeftShiftAssign(left, right, method, conversion);
			case ExpressionType.ModuloAssign:
				return Expression.ModuloAssign(left, right, method, conversion);
			case ExpressionType.MultiplyAssign:
				return Expression.MultiplyAssign(left, right, method, conversion);
			case ExpressionType.OrAssign:
				return Expression.OrAssign(left, right, method, conversion);
			case ExpressionType.PowerAssign:
				return Expression.PowerAssign(left, right, method, conversion);
			case ExpressionType.RightShiftAssign:
				return Expression.RightShiftAssign(left, right, method, conversion);
			case ExpressionType.SubtractAssign:
				return Expression.SubtractAssign(left, right, method, conversion);
			case ExpressionType.AddAssignChecked:
				return Expression.AddAssignChecked(left, right, method, conversion);
			case ExpressionType.MultiplyAssignChecked:
				return Expression.MultiplyAssignChecked(left, right, method, conversion);
			case ExpressionType.SubtractAssignChecked:
				return Expression.SubtractAssignChecked(left, right, method, conversion);
			}
			throw Error.UnhandledBinary(binaryType, "binaryType");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an equality comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Equal" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The equality operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D03 RID: 3331 RVA: 0x0002DCBC File Offset: 0x0002BEBC
		public static BinaryExpression Equal(Expression left, Expression right)
		{
			return Expression.Equal(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an equality comparison. The implementing method can be specified.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">
		///       <see langword="true" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="true" />; <see langword="false" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="false" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Equal" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the equality operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D04 RID: 3332 RVA: 0x0002DCC7 File Offset: 0x0002BEC7
		public static BinaryExpression Equal(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetEqualityComparisonOperator(ExpressionType.Equal, "op_Equality", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.Equal, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a reference equality comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Equal" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D05 RID: 3333 RVA: 0x0002DD04 File Offset: 0x0002BF04
		public static BinaryExpression ReferenceEqual(Expression left, Expression right)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (TypeUtils.HasReferenceEquality(left.Type, right.Type))
			{
				return new LogicalBinaryExpression(ExpressionType.Equal, left, right);
			}
			throw Error.ReferenceEqualityNotDefined(left.Type, right.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an inequality comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NotEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The inequality operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D06 RID: 3334 RVA: 0x0002DD55 File Offset: 0x0002BF55
		public static BinaryExpression NotEqual(Expression left, Expression right)
		{
			return Expression.NotEqual(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an inequality comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">
		///       <see langword="true" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="true" />; <see langword="false" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="false" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NotEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the inequality operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D07 RID: 3335 RVA: 0x0002DD60 File Offset: 0x0002BF60
		public static BinaryExpression NotEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetEqualityComparisonOperator(ExpressionType.NotEqual, "op_Inequality", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.NotEqual, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a reference inequality comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NotEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D08 RID: 3336 RVA: 0x0002DD9C File Offset: 0x0002BF9C
		public static BinaryExpression ReferenceNotEqual(Expression left, Expression right)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (TypeUtils.HasReferenceEquality(left.Type, right.Type))
			{
				return new LogicalBinaryExpression(ExpressionType.NotEqual, left, right);
			}
			throw Error.ReferenceEqualityNotDefined(left.Type, right.Type);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002DDF0 File Offset: 0x0002BFF0
		private static BinaryExpression GetEqualityComparisonOperator(ExpressionType binaryType, string opName, Expression left, Expression right, bool liftToNull)
		{
			if (left.Type == right.Type && (left.Type.IsNumeric() || left.Type == typeof(object) || left.Type.IsBool() || left.Type.GetNonNullableType().IsEnum))
			{
				if (left.Type.IsNullableType() && liftToNull)
				{
					return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
				}
				return new LogicalBinaryExpression(binaryType, left, right);
			}
			else
			{
				BinaryExpression userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(binaryType, opName, left, right, liftToNull);
				if (userDefinedBinaryOperator != null)
				{
					return userDefinedBinaryOperator;
				}
				if (!TypeUtils.HasBuiltInEqualityOperator(left.Type, right.Type) && !Expression.IsNullComparison(left, right))
				{
					throw Error.BinaryOperatorNotDefined(binaryType, left.Type, right.Type);
				}
				if (left.Type.IsNullableType() && liftToNull)
				{
					return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
				}
				return new LogicalBinaryExpression(binaryType, left, right);
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "greater than" numeric comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.GreaterThan" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The "greater than" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D0A RID: 3338 RVA: 0x0002DEEF File Offset: 0x0002C0EF
		public static BinaryExpression GreaterThan(Expression left, Expression right)
		{
			return Expression.GreaterThan(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "greater than" numeric comparison. The implementing method can be specified.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">
		///       <see langword="true" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="true" />; <see langword="false" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="false" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.GreaterThan" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the "greater than" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D0B RID: 3339 RVA: 0x0002DEFA File Offset: 0x0002C0FA
		public static BinaryExpression GreaterThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.GreaterThan, "op_GreaterThan", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.GreaterThan, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "less than" numeric comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LessThan" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The "less than" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D0C RID: 3340 RVA: 0x0002DF36 File Offset: 0x0002C136
		public static BinaryExpression LessThan(Expression left, Expression right)
		{
			return Expression.LessThan(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "less than" numeric comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">
		///       <see langword="true" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="true" />; <see langword="false" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="false" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LessThan" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the "less than" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D0D RID: 3341 RVA: 0x0002DF41 File Offset: 0x0002C141
		public static BinaryExpression LessThan(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.LessThan, "op_LessThan", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.LessThan, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "greater than or equal" numeric comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.GreaterThanOrEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The "greater than or equal" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D0E RID: 3342 RVA: 0x0002DF7D File Offset: 0x0002C17D
		public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right)
		{
			return Expression.GreaterThanOrEqual(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "greater than or equal" numeric comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">
		///       <see langword="true" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="true" />; <see langword="false" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="false" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.GreaterThanOrEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the "greater than or equal" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D0F RID: 3343 RVA: 0x0002DF88 File Offset: 0x0002C188
		public static BinaryExpression GreaterThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.GreaterThanOrEqual, "op_GreaterThanOrEqual", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.GreaterThanOrEqual, left, right, method, liftToNull);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a " less than or equal" numeric comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LessThanOrEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The "less than or equal" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D10 RID: 3344 RVA: 0x0002DFC4 File Offset: 0x0002C1C4
		public static BinaryExpression LessThanOrEqual(Expression left, Expression right)
		{
			return Expression.LessThanOrEqual(left, right, false, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a "less than or equal" numeric comparison.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="liftToNull">
		///       <see langword="true" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="true" />; <see langword="false" /> to set <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" /> to <see langword="false" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LessThanOrEqual" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.IsLiftedToNull" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the "less than or equal" operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D11 RID: 3345 RVA: 0x0002DFCF File Offset: 0x0002C1CF
		public static BinaryExpression LessThanOrEqual(Expression left, Expression right, bool liftToNull, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				return Expression.GetComparisonOperator(ExpressionType.LessThanOrEqual, "op_LessThanOrEqual", left, right, liftToNull);
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.LessThanOrEqual, left, right, method, liftToNull);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002E00C File Offset: 0x0002C20C
		private static BinaryExpression GetComparisonOperator(ExpressionType binaryType, string opName, Expression left, Expression right, bool liftToNull)
		{
			if (!(left.Type == right.Type) || !left.Type.IsNumeric())
			{
				return Expression.GetUserDefinedBinaryOperatorOrThrow(binaryType, opName, left, right, liftToNull);
			}
			if (left.Type.IsNullableType() && liftToNull)
			{
				return new SimpleBinaryExpression(binaryType, left, right, typeof(bool?));
			}
			return new LogicalBinaryExpression(binaryType, left, right);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a conditional <see langword="AND" /> operation that evaluates the second operand only if the first operand evaluates to <see langword="true" />.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAlso" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The bitwise <see langword="AND" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-
		///         <paramref name="left" />.Type and <paramref name="right" />.Type are not the same Boolean type.</exception>
		// Token: 0x06000D13 RID: 3347 RVA: 0x0002E070 File Offset: 0x0002C270
		public static BinaryExpression AndAlso(Expression left, Expression right)
		{
			return Expression.AndAlso(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a conditional <see langword="AND" /> operation that evaluates the second operand only if the first operand is resolved to true. The implementing method can be specified.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAlso" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the bitwise <see langword="AND" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-
		///         <paramref name="method" /> is <see langword="null" /> and <paramref name="left" />.Type and <paramref name="right" />.Type are not the same Boolean type.</exception>
		// Token: 0x06000D14 RID: 3348 RVA: 0x0002E07C File Offset: 0x0002C27C
		public static BinaryExpression AndAlso(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.AndAlso, left.Type, right.Type, method);
				Type type = (left.Type.IsNullableType() && TypeUtils.AreEquivalent(method.ReturnType, left.Type.GetNonNullableType())) ? left.Type : method.ReturnType;
				return new MethodBinaryExpression(ExpressionType.AndAlso, left, right, type, method);
			}
			if (left.Type == right.Type)
			{
				if (left.Type == typeof(bool))
				{
					return new LogicalBinaryExpression(ExpressionType.AndAlso, left, right);
				}
				if (left.Type == typeof(bool?))
				{
					return new SimpleBinaryExpression(ExpressionType.AndAlso, left, right, left.Type);
				}
			}
			method = Expression.GetUserDefinedBinaryOperator(ExpressionType.AndAlso, left.Type, right.Type, "op_BitwiseAnd");
			if (method != null)
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.AndAlso, left.Type, right.Type, method);
				Type type = (left.Type.IsNullableType() && TypeUtils.AreEquivalent(method.ReturnType, left.Type.GetNonNullableType())) ? left.Type : method.ReturnType;
				return new MethodBinaryExpression(ExpressionType.AndAlso, left, right, type, method);
			}
			throw Error.BinaryOperatorNotDefined(ExpressionType.AndAlso, left.Type, right.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a conditional <see langword="OR" /> operation that evaluates the second operand only if the first operand evaluates to <see langword="false" />.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrElse" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The bitwise <see langword="OR" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-
		///         <paramref name="left" />.Type and <paramref name="right" />.Type are not the same Boolean type.</exception>
		// Token: 0x06000D15 RID: 3349 RVA: 0x0002E1E1 File Offset: 0x0002C3E1
		public static BinaryExpression OrElse(Expression left, Expression right)
		{
			return Expression.OrElse(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a conditional <see langword="OR" /> operation that evaluates the second operand only if the first operand evaluates to <see langword="false" />.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrElse" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the bitwise <see langword="OR" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-
		///         <paramref name="method" /> is <see langword="null" /> and <paramref name="left" />.Type and <paramref name="right" />.Type are not the same Boolean type.</exception>
		// Token: 0x06000D16 RID: 3350 RVA: 0x0002E1EC File Offset: 0x0002C3EC
		public static BinaryExpression OrElse(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.OrElse, left.Type, right.Type, method);
				Type type = (left.Type.IsNullableType() && method.ReturnType == left.Type.GetNonNullableType()) ? left.Type : method.ReturnType;
				return new MethodBinaryExpression(ExpressionType.OrElse, left, right, type, method);
			}
			if (left.Type == right.Type)
			{
				if (left.Type == typeof(bool))
				{
					return new LogicalBinaryExpression(ExpressionType.OrElse, left, right);
				}
				if (left.Type == typeof(bool?))
				{
					return new SimpleBinaryExpression(ExpressionType.OrElse, left, right, left.Type);
				}
			}
			method = Expression.GetUserDefinedBinaryOperator(ExpressionType.OrElse, left.Type, right.Type, "op_BitwiseOr");
			if (method != null)
			{
				Expression.ValidateUserDefinedConditionalLogicOperator(ExpressionType.OrElse, left.Type, right.Type, method);
				Type type = (left.Type.IsNullableType() && method.ReturnType == left.Type.GetNonNullableType()) ? left.Type : method.ReturnType;
				return new MethodBinaryExpression(ExpressionType.OrElse, left, right, type, method);
			}
			throw Error.BinaryOperatorNotDefined(ExpressionType.OrElse, left.Type, right.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a coalescing operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Coalesce" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of <paramref name="left" /> does not represent a reference type or a nullable value type.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="left" />.Type and <paramref name="right" />.Type are not convertible to each other.</exception>
		// Token: 0x06000D17 RID: 3351 RVA: 0x0002E359 File Offset: 0x0002C559
		public static BinaryExpression Coalesce(Expression left, Expression right)
		{
			return Expression.Coalesce(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a coalescing operation, given a conversion function.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Coalesce" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="left" />.Type and <paramref name="right" />.Type are not convertible to each other.-or-
		///         <paramref name="conversion" /> is not <see langword="null" /> and <paramref name="conversion" />.Type is a delegate type that does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of <paramref name="left" /> does not represent a reference type or a nullable value type.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of <paramref name="left" /> represents a type that is not assignable to the parameter type of the delegate type <paramref name="conversion" />.Type.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of <paramref name="right" /> is not equal to the return type of the delegate type <paramref name="conversion" />.Type.</exception>
		// Token: 0x06000D18 RID: 3352 RVA: 0x0002E364 File Offset: 0x0002C564
		public static BinaryExpression Coalesce(Expression left, Expression right, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (conversion == null)
			{
				Type type = Expression.ValidateCoalesceArgTypes(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.Coalesce, left, right, type);
			}
			if (left.Type.IsValueType && !left.Type.IsNullableType())
			{
				throw Error.CoalesceUsedOnNonNullType();
			}
			MethodInfo invokeMethod = conversion.Type.GetInvokeMethod();
			if (invokeMethod.ReturnType == typeof(void))
			{
				throw Error.UserDefinedOperatorMustNotBeVoid(conversion, "conversion");
			}
			ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(conversion, "conversion");
			}
			if (!TypeUtils.AreEquivalent(invokeMethod.ReturnType, right.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(ExpressionType.Coalesce, conversion.ToString());
			}
			if (!Expression.ParameterIsAssignable(parametersCached[0], left.Type.GetNonNullableType()) && !Expression.ParameterIsAssignable(parametersCached[0], left.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(ExpressionType.Coalesce, conversion.ToString());
			}
			return new CoalesceConversionBinaryExpression(left, right, conversion);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002E470 File Offset: 0x0002C670
		private static Type ValidateCoalesceArgTypes(Type left, Type right)
		{
			Type nonNullableType = left.GetNonNullableType();
			if (left.IsValueType && !left.IsNullableType())
			{
				throw Error.CoalesceUsedOnNonNullType();
			}
			if (left.IsNullableType() && right.IsImplicitlyConvertibleTo(nonNullableType))
			{
				return nonNullableType;
			}
			if (right.IsImplicitlyConvertibleTo(left))
			{
				return left;
			}
			if (nonNullableType.IsImplicitlyConvertibleTo(right))
			{
				return right;
			}
			throw Error.ArgumentTypesMustMatch();
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic addition operation that does not have overflow checking.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Add" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The addition operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D1A RID: 3354 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
		public static BinaryExpression Add(Expression left, Expression right)
		{
			return Expression.Add(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic addition operation that does not have overflow checking. The implementing method can be specified.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Add" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the addition operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D1B RID: 3355 RVA: 0x0002E4D4 File Offset: 0x0002C6D4
		public static BinaryExpression Add(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Add, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Add, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Add, "op_Addition", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D1C RID: 3356 RVA: 0x0002E548 File Offset: 0x0002C748
		public static BinaryExpression AddAssign(Expression left, Expression right)
		{
			return Expression.AddAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D1D RID: 3357 RVA: 0x0002E553 File Offset: 0x0002C753
		public static BinaryExpression AddAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AddAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D1E RID: 3358 RVA: 0x0002E560 File Offset: 0x0002C760
		public static BinaryExpression AddAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AddAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AddAssign, "op_Addition", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AddAssign, left, right, left.Type);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0002E5F0 File Offset: 0x0002C7F0
		private static void ValidateOpAssignConversionLambda(LambdaExpression conversion, Expression left, MethodInfo method, ExpressionType nodeType)
		{
			MethodInfo invokeMethod = conversion.Type.GetInvokeMethod();
			ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(conversion, "conversion");
			}
			if (!TypeUtils.AreEquivalent(invokeMethod.ReturnType, left.Type))
			{
				throw Error.OperandTypesDoNotMatchParameters(nodeType, conversion.ToString());
			}
			if (!TypeUtils.AreEquivalent(parametersCached[0].ParameterType, method.ReturnType))
			{
				throw Error.OverloadOperatorTypeDoesNotMatchConversionType(nodeType, conversion.ToString());
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D20 RID: 3360 RVA: 0x0002E66C File Offset: 0x0002C86C
		public static BinaryExpression AddAssignChecked(Expression left, Expression right)
		{
			return Expression.AddAssignChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D21 RID: 3361 RVA: 0x0002E676 File Offset: 0x0002C876
		public static BinaryExpression AddAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AddAssignChecked(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an addition assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D22 RID: 3362 RVA: 0x0002E684 File Offset: 0x0002C884
		public static BinaryExpression AddAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AddAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AddAssignChecked, "op_Addition", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AddAssignChecked, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic addition operation that has overflow checking.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The addition operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D23 RID: 3363 RVA: 0x0002E711 File Offset: 0x0002C911
		public static BinaryExpression AddChecked(Expression left, Expression right)
		{
			return Expression.AddChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic addition operation that has overflow checking. The implementing method can be specified.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AddChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the addition operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D24 RID: 3364 RVA: 0x0002E71C File Offset: 0x0002C91C
		public static BinaryExpression AddChecked(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.AddChecked, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.AddChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.AddChecked, "op_Addition", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic subtraction operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Subtract" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The subtraction operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D25 RID: 3365 RVA: 0x0002E790 File Offset: 0x0002C990
		public static BinaryExpression Subtract(Expression left, Expression right)
		{
			return Expression.Subtract(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic subtraction operation that does not have overflow checking.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Subtract" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the subtraction operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D26 RID: 3366 RVA: 0x0002E79C File Offset: 0x0002C99C
		public static BinaryExpression Subtract(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Subtract, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Subtract, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Subtract, "op_Subtraction", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D27 RID: 3367 RVA: 0x0002E813 File Offset: 0x0002CA13
		public static BinaryExpression SubtractAssign(Expression left, Expression right)
		{
			return Expression.SubtractAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D28 RID: 3368 RVA: 0x0002E81E File Offset: 0x0002CA1E
		public static BinaryExpression SubtractAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.SubtractAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D29 RID: 3369 RVA: 0x0002E82C File Offset: 0x0002CA2C
		public static BinaryExpression SubtractAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.SubtractAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.SubtractAssign, "op_Subtraction", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.SubtractAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D2A RID: 3370 RVA: 0x0002E8B9 File Offset: 0x0002CAB9
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right)
		{
			return Expression.SubtractAssignChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D2B RID: 3371 RVA: 0x0002E8C3 File Offset: 0x0002CAC3
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.SubtractAssignChecked(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a subtraction assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D2C RID: 3372 RVA: 0x0002E8D0 File Offset: 0x0002CAD0
		public static BinaryExpression SubtractAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.SubtractAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.SubtractAssignChecked, "op_Subtraction", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.SubtractAssignChecked, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic subtraction operation that has overflow checking.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The subtraction operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D2D RID: 3373 RVA: 0x0002E95D File Offset: 0x0002CB5D
		public static BinaryExpression SubtractChecked(Expression left, Expression right)
		{
			return Expression.SubtractChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic subtraction operation that has overflow checking.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.SubtractChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the subtraction operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D2E RID: 3374 RVA: 0x0002E968 File Offset: 0x0002CB68
		public static BinaryExpression SubtractChecked(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.SubtractChecked, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.SubtractChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.SubtractChecked, "op_Subtraction", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic division operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Divide" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The division operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D2F RID: 3375 RVA: 0x0002E9DF File Offset: 0x0002CBDF
		public static BinaryExpression Divide(Expression left, Expression right)
		{
			return Expression.Divide(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic division operation. The implementing method can be specified.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Divide" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the division operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D30 RID: 3376 RVA: 0x0002E9EC File Offset: 0x0002CBEC
		public static BinaryExpression Divide(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Divide, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Divide, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Divide, "op_Division", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a division assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.DivideAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D31 RID: 3377 RVA: 0x0002EA63 File Offset: 0x0002CC63
		public static BinaryExpression DivideAssign(Expression left, Expression right)
		{
			return Expression.DivideAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a division assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.DivideAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D32 RID: 3378 RVA: 0x0002EA6E File Offset: 0x0002CC6E
		public static BinaryExpression DivideAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.DivideAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a division assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.DivideAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D33 RID: 3379 RVA: 0x0002EA7C File Offset: 0x0002CC7C
		public static BinaryExpression DivideAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.DivideAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.DivideAssign, "op_Division", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.DivideAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic remainder operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Modulo" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The modulus operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D34 RID: 3380 RVA: 0x0002EB09 File Offset: 0x0002CD09
		public static BinaryExpression Modulo(Expression left, Expression right)
		{
			return Expression.Modulo(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic remainder operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Modulo" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the modulus operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D35 RID: 3381 RVA: 0x0002EB14 File Offset: 0x0002CD14
		public static BinaryExpression Modulo(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Modulo, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Modulo, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Modulo, "op_Modulus", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a remainder assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ModuloAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D36 RID: 3382 RVA: 0x0002EB8B File Offset: 0x0002CD8B
		public static BinaryExpression ModuloAssign(Expression left, Expression right)
		{
			return Expression.ModuloAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a remainder assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ModuloAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D37 RID: 3383 RVA: 0x0002EB96 File Offset: 0x0002CD96
		public static BinaryExpression ModuloAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.ModuloAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a remainder assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ModuloAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D38 RID: 3384 RVA: 0x0002EBA4 File Offset: 0x0002CDA4
		public static BinaryExpression ModuloAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.ModuloAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.ModuloAssign, "op_Modulus", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.ModuloAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic multiplication operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Multiply" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The multiplication operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D39 RID: 3385 RVA: 0x0002EC31 File Offset: 0x0002CE31
		public static BinaryExpression Multiply(Expression left, Expression right)
		{
			return Expression.Multiply(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic multiplication operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Multiply" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the multiplication operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D3A RID: 3386 RVA: 0x0002EC3C File Offset: 0x0002CE3C
		public static BinaryExpression Multiply(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Multiply, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.Multiply, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Multiply, "op_Multiply", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D3B RID: 3387 RVA: 0x0002ECB3 File Offset: 0x0002CEB3
		public static BinaryExpression MultiplyAssign(Expression left, Expression right)
		{
			return Expression.MultiplyAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D3C RID: 3388 RVA: 0x0002ECBE File Offset: 0x0002CEBE
		public static BinaryExpression MultiplyAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.MultiplyAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that does not have overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D3D RID: 3389 RVA: 0x0002ECCC File Offset: 0x0002CECC
		public static BinaryExpression MultiplyAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.MultiplyAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.MultiplyAssign, "op_Multiply", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.MultiplyAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D3E RID: 3390 RVA: 0x0002ED59 File Offset: 0x0002CF59
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right)
		{
			return Expression.MultiplyAssignChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D3F RID: 3391 RVA: 0x0002ED63 File Offset: 0x0002CF63
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right, MethodInfo method)
		{
			return Expression.MultiplyAssignChecked(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a multiplication assignment operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyAssignChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D40 RID: 3392 RVA: 0x0002ED70 File Offset: 0x0002CF70
		public static BinaryExpression MultiplyAssignChecked(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.MultiplyAssignChecked, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.MultiplyAssignChecked, "op_Multiply", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.MultiplyAssignChecked, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic multiplication operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The multiplication operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D41 RID: 3393 RVA: 0x0002EDFD File Offset: 0x0002CFFD
		public static BinaryExpression MultiplyChecked(Expression left, Expression right)
		{
			return Expression.MultiplyChecked(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents an arithmetic multiplication operation that has overflow checking.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MultiplyChecked" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the multiplication operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D42 RID: 3394 RVA: 0x0002EE08 File Offset: 0x0002D008
		public static BinaryExpression MultiplyChecked(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.MultiplyChecked, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsArithmetic())
			{
				return new SimpleBinaryExpression(ExpressionType.MultiplyChecked, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.MultiplyChecked, "op_Multiply", left, right, true);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002EE7F File Offset: 0x0002D07F
		private static bool IsSimpleShift(Type left, Type right)
		{
			return left.IsInteger() && right.GetNonNullableType() == typeof(int);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		private static Type GetResultTypeOfShift(Type left, Type right)
		{
			if (!left.IsNullableType() && right.IsNullableType())
			{
				return typeof(Nullable<>).MakeGenericType(new Type[]
				{
					left
				});
			}
			return left;
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShift" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The left-shift operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D45 RID: 3397 RVA: 0x0002EECD File Offset: 0x0002D0CD
		public static BinaryExpression LeftShift(Expression left, Expression right)
		{
			return Expression.LeftShift(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShift" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the left-shift operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D46 RID: 3398 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		public static BinaryExpression LeftShift(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.LeftShift, left, right, method, true);
			}
			if (Expression.IsSimpleShift(left.Type, right.Type))
			{
				Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.LeftShift, left, right, resultTypeOfShift);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.LeftShift, "op_LeftShift", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D47 RID: 3399 RVA: 0x0002EF4F File Offset: 0x0002D14F
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right)
		{
			return Expression.LeftShiftAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D48 RID: 3400 RVA: 0x0002EF5A File Offset: 0x0002D15A
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.LeftShiftAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise left-shift assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.LeftShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D49 RID: 3401 RVA: 0x0002EF68 File Offset: 0x0002D168
		public static BinaryExpression LeftShiftAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.LeftShiftAssign, left, right, method, conversion, true);
			}
			if (!Expression.IsSimpleShift(left.Type, right.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.LeftShiftAssign, "op_LeftShift", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
			return new SimpleBinaryExpression(ExpressionType.LeftShiftAssign, left, right, resultTypeOfShift);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShift" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The right-shift operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D4A RID: 3402 RVA: 0x0002EFF5 File Offset: 0x0002D1F5
		public static BinaryExpression RightShift(Expression left, Expression right)
		{
			return Expression.RightShift(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShift" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the right-shift operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D4B RID: 3403 RVA: 0x0002F000 File Offset: 0x0002D200
		public static BinaryExpression RightShift(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.RightShift, left, right, method, true);
			}
			if (Expression.IsSimpleShift(left.Type, right.Type))
			{
				Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
				return new SimpleBinaryExpression(ExpressionType.RightShift, left, right, resultTypeOfShift);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.RightShift, "op_RightShift", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D4C RID: 3404 RVA: 0x0002F077 File Offset: 0x0002D277
		public static BinaryExpression RightShiftAssign(Expression left, Expression right)
		{
			return Expression.RightShiftAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D4D RID: 3405 RVA: 0x0002F082 File Offset: 0x0002D282
		public static BinaryExpression RightShiftAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.RightShiftAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise right-shift assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RightShiftAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D4E RID: 3406 RVA: 0x0002F090 File Offset: 0x0002D290
		public static BinaryExpression RightShiftAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.RightShiftAssign, left, right, method, conversion, true);
			}
			if (!Expression.IsSimpleShift(left.Type, right.Type))
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.RightShiftAssign, "op_RightShift", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			Type resultTypeOfShift = Expression.GetResultTypeOfShift(left.Type, right.Type);
			return new SimpleBinaryExpression(ExpressionType.RightShiftAssign, left, right, resultTypeOfShift);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise <see langword="AND" /> operation.</summary>
		/// <param name="left">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.And" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The bitwise <see langword="AND" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D4F RID: 3407 RVA: 0x0002F11D File Offset: 0x0002D31D
		public static BinaryExpression And(Expression left, Expression right)
		{
			return Expression.And(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise <see langword="AND" /> operation. The implementing method can be specified.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.And" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the bitwise <see langword="AND" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D50 RID: 3408 RVA: 0x0002F128 File Offset: 0x0002D328
		public static BinaryExpression And(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.And, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsIntegerOrBool())
			{
				return new SimpleBinaryExpression(ExpressionType.And, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.And, "op_BitwiseAnd", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise AND assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D51 RID: 3409 RVA: 0x0002F19C File Offset: 0x0002D39C
		public static BinaryExpression AndAssign(Expression left, Expression right)
		{
			return Expression.AndAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise AND assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D52 RID: 3410 RVA: 0x0002F1A7 File Offset: 0x0002D3A7
		public static BinaryExpression AndAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.AndAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise AND assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.AndAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D53 RID: 3411 RVA: 0x0002F1B4 File Offset: 0x0002D3B4
		public static BinaryExpression AndAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.AndAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsIntegerOrBool())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.AndAssign, "op_BitwiseAnd", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.AndAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise <see langword="OR" /> operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Or" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The bitwise <see langword="OR" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D54 RID: 3412 RVA: 0x0002F241 File Offset: 0x0002D441
		public static BinaryExpression Or(Expression left, Expression right)
		{
			return Expression.Or(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise <see langword="OR" /> operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Or" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the bitwise <see langword="OR" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D55 RID: 3413 RVA: 0x0002F24C File Offset: 0x0002D44C
		public static BinaryExpression Or(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.Or, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsIntegerOrBool())
			{
				return new SimpleBinaryExpression(ExpressionType.Or, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.Or, "op_BitwiseOr", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise OR assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D56 RID: 3414 RVA: 0x0002F2C3 File Offset: 0x0002D4C3
		public static BinaryExpression OrAssign(Expression left, Expression right)
		{
			return Expression.OrAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise OR assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D57 RID: 3415 RVA: 0x0002F2CE File Offset: 0x0002D4CE
		public static BinaryExpression OrAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.OrAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise OR assignment operation.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.OrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D58 RID: 3416 RVA: 0x0002F2DC File Offset: 0x0002D4DC
		public static BinaryExpression OrAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.OrAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsIntegerOrBool())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.OrAssign, "op_BitwiseOr", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.OrAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise <see langword="XOR" /> operation, using op_ExclusiveOr for user-defined types.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOr" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see langword="XOR" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D59 RID: 3417 RVA: 0x0002F369 File Offset: 0x0002D569
		public static BinaryExpression ExclusiveOr(Expression left, Expression right)
		{
			return Expression.ExclusiveOr(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise <see langword="XOR" /> operation, using op_ExclusiveOr for user-defined types. The implementing method can be specified.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOr" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the <see langword="XOR" /> operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.</exception>
		// Token: 0x06000D5A RID: 3418 RVA: 0x0002F374 File Offset: 0x0002D574
		public static BinaryExpression ExclusiveOr(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedBinaryOperator(ExpressionType.ExclusiveOr, left, right, method, true);
			}
			if (left.Type == right.Type && left.Type.IsIntegerOrBool())
			{
				return new SimpleBinaryExpression(ExpressionType.ExclusiveOr, left, right, left.Type);
			}
			return Expression.GetUserDefinedBinaryOperatorOrThrow(ExpressionType.ExclusiveOr, "op_ExclusiveOr", left, right, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D5B RID: 3419 RVA: 0x0002F3EB File Offset: 0x0002D5EB
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right)
		{
			return Expression.ExclusiveOrAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D5C RID: 3420 RVA: 0x0002F3F6 File Offset: 0x0002D5F6
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.ExclusiveOrAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents a bitwise XOR assignment operation, using op_ExclusiveOr for user-defined types.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ExclusiveOrAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D5D RID: 3421 RVA: 0x0002F404 File Offset: 0x0002D604
		public static BinaryExpression ExclusiveOrAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (!(method == null))
			{
				return Expression.GetMethodBasedAssignOperator(ExpressionType.ExclusiveOrAssign, left, right, method, conversion, true);
			}
			if (!(left.Type == right.Type) || !left.Type.IsIntegerOrBool())
			{
				return Expression.GetUserDefinedAssignOperatorOrThrow(ExpressionType.ExclusiveOrAssign, "op_ExclusiveOr", left, right, conversion, true);
			}
			if (conversion != null)
			{
				throw Error.ConversionIsNotSupportedForArithmeticTypes();
			}
			return new SimpleBinaryExpression(ExpressionType.ExclusiveOrAssign, left, right, left.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising a number to a power.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Power" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The exponentiation operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-
		///         <paramref name="left" />.Type and/or <paramref name="right" />.Type are not <see cref="T:System.Double" />.</exception>
		// Token: 0x06000D5E RID: 3422 RVA: 0x0002F491 File Offset: 0x0002D691
		public static BinaryExpression Power(Expression left, Expression right)
		{
			return Expression.Power(left, right, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising a number to a power.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Power" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="left" /> or <paramref name="right" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly two arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the exponentiation operator is not defined for <paramref name="left" />.Type and <paramref name="right" />.Type.-or-
		///         <paramref name="method" /> is <see langword="null" /> and <paramref name="left" />.Type and/or <paramref name="right" />.Type are not <see cref="T:System.Double" />.</exception>
		// Token: 0x06000D5F RID: 3423 RVA: 0x0002F49C File Offset: 0x0002D69C
		public static BinaryExpression Power(Expression left, Expression right, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				if (!(left.Type == right.Type) || !left.Type.IsArithmetic())
				{
					string name = "op_Exponent";
					BinaryExpression userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(ExpressionType.Power, name, left, right, true);
					if (userDefinedBinaryOperator == null)
					{
						name = "op_Exponentiation";
						userDefinedBinaryOperator = Expression.GetUserDefinedBinaryOperator(ExpressionType.Power, name, left, right, true);
						if (userDefinedBinaryOperator == null)
						{
							throw Error.BinaryOperatorNotDefined(ExpressionType.Power, left.Type, right.Type);
						}
					}
					ParameterInfo[] parametersCached = userDefinedBinaryOperator.Method.GetParametersCached();
					Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, left.Type, ExpressionType.Power, name);
					Expression.ValidateParamswithOperandsOrThrow(parametersCached[1].ParameterType, right.Type, ExpressionType.Power, name);
					return userDefinedBinaryOperator;
				}
				method = CachedReflectionInfo.Math_Pow_Double_Double;
			}
			return Expression.GetMethodBasedBinaryOperator(ExpressionType.Power, left, right, method, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising an expression to a power and assigning the result back to the expression.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.PowerAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		// Token: 0x06000D60 RID: 3424 RVA: 0x0002F57A File Offset: 0x0002D77A
		public static BinaryExpression PowerAssign(Expression left, Expression right)
		{
			return Expression.PowerAssign(left, right, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising an expression to a power and assigning the result back to the expression.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.PowerAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000D61 RID: 3425 RVA: 0x0002F585 File Offset: 0x0002D785
		public static BinaryExpression PowerAssign(Expression left, Expression right, MethodInfo method)
		{
			return Expression.PowerAssign(left, right, method, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents raising an expression to a power and assigning the result back to the expression.</summary>
		/// <param name="left">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="right">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Method" /> property equal to.</param>
		/// <param name="conversion">A <see cref="T:System.Linq.Expressions.LambdaExpression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.PowerAssign" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Right" />, <see cref="P:System.Linq.Expressions.BinaryExpression.Method" />, and <see cref="P:System.Linq.Expressions.BinaryExpression.Conversion" /> properties set to the specified values.</returns>
		// Token: 0x06000D62 RID: 3426 RVA: 0x0002F590 File Offset: 0x0002D790
		public static BinaryExpression PowerAssign(Expression left, Expression right, MethodInfo method, LambdaExpression conversion)
		{
			ExpressionUtils.RequiresCanRead(left, "left");
			Expression.RequiresCanWrite(left, "left");
			ExpressionUtils.RequiresCanRead(right, "right");
			if (method == null)
			{
				method = CachedReflectionInfo.Math_Pow_Double_Double;
				if (method == null)
				{
					throw Error.BinaryOperatorNotDefined(ExpressionType.PowerAssign, left.Type, right.Type);
				}
			}
			return Expression.GetMethodBasedAssignOperator(ExpressionType.PowerAssign, left, right, method, conversion, true);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BinaryExpression" /> that represents applying an array index operator to an array of rank one.</summary>
		/// <param name="array">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> property equal to.</param>
		/// <param name="index">A <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.BinaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ArrayIndex" /> and the <see cref="P:System.Linq.Expressions.BinaryExpression.Left" /> and <see cref="P:System.Linq.Expressions.BinaryExpression.Right" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> or <paramref name="index" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="array" />.Type does not represent an array type.-or-
		///         <paramref name="array" />.Type represents an array type whose rank is not 1.-or-
		///         <paramref name="index" />.Type does not represent the <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x06000D63 RID: 3427 RVA: 0x0002F5FC File Offset: 0x0002D7FC
		public static BinaryExpression ArrayIndex(Expression array, Expression index)
		{
			ExpressionUtils.RequiresCanRead(array, "array");
			ExpressionUtils.RequiresCanRead(index, "index");
			if (index.Type != typeof(int))
			{
				throw Error.ArgumentMustBeArrayIndexType("index");
			}
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray("array");
			}
			if (type.GetArrayRank() != 1)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			return new SimpleBinaryExpression(ExpressionType.ArrayIndex, array, index, type.GetElementType());
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains two expressions and has no variables.</summary>
		/// <param name="arg0">The first expression in the block.</param>
		/// <param name="arg1">The second expression in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D64 RID: 3428 RVA: 0x0002F678 File Offset: 0x0002D878
		public static BlockExpression Block(Expression arg0, Expression arg1)
		{
			ExpressionUtils.RequiresCanRead(arg0, "arg0");
			ExpressionUtils.RequiresCanRead(arg1, "arg1");
			return new Block2(arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains three expressions and has no variables.</summary>
		/// <param name="arg0">The first expression in the block.</param>
		/// <param name="arg1">The second expression in the block.</param>
		/// <param name="arg2">The third expression in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D65 RID: 3429 RVA: 0x0002F697 File Offset: 0x0002D897
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2)
		{
			ExpressionUtils.RequiresCanRead(arg0, "arg0");
			ExpressionUtils.RequiresCanRead(arg1, "arg1");
			ExpressionUtils.RequiresCanRead(arg2, "arg2");
			return new Block3(arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains four expressions and has no variables.</summary>
		/// <param name="arg0">The first expression in the block.</param>
		/// <param name="arg1">The second expression in the block.</param>
		/// <param name="arg2">The third expression in the block.</param>
		/// <param name="arg3">The fourth expression in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D66 RID: 3430 RVA: 0x0002F6C2 File Offset: 0x0002D8C2
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ExpressionUtils.RequiresCanRead(arg0, "arg0");
			ExpressionUtils.RequiresCanRead(arg1, "arg1");
			ExpressionUtils.RequiresCanRead(arg2, "arg2");
			ExpressionUtils.RequiresCanRead(arg3, "arg3");
			return new Block4(arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains five expressions and has no variables.</summary>
		/// <param name="arg0">The first expression in the block.</param>
		/// <param name="arg1">The second expression in the block.</param>
		/// <param name="arg2">The third expression in the block.</param>
		/// <param name="arg3">The fourth expression in the block.</param>
		/// <param name="arg4">The fifth expression in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D67 RID: 3431 RVA: 0x0002F6FC File Offset: 0x0002D8FC
		public static BlockExpression Block(Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			ExpressionUtils.RequiresCanRead(arg0, "arg0");
			ExpressionUtils.RequiresCanRead(arg1, "arg1");
			ExpressionUtils.RequiresCanRead(arg2, "arg2");
			ExpressionUtils.RequiresCanRead(arg3, "arg3");
			ExpressionUtils.RequiresCanRead(arg4, "arg4");
			return new Block5(arg0, arg1, arg2, arg3, arg4);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given expressions and has no variables.</summary>
		/// <param name="expressions">The expressions in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D68 RID: 3432 RVA: 0x0002F74C File Offset: 0x0002D94C
		public static BlockExpression Block(params Expression[] expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			Expression.RequiresCanRead(expressions, "expressions");
			return Expression.GetOptimizedBlockExpression(expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given expressions and has no variables.</summary>
		/// <param name="expressions">The expressions in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D69 RID: 3433 RVA: 0x0002F76A File Offset: 0x0002D96A
		public static BlockExpression Block(IEnumerable<Expression> expressions)
		{
			return Expression.Block(EmptyReadOnlyCollection<ParameterExpression>.Instance, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given expressions, has no variables and has specific result type.</summary>
		/// <param name="type">The result type of the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D6A RID: 3434 RVA: 0x0002F777 File Offset: 0x0002D977
		public static BlockExpression Block(Type type, params Expression[] expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			return Expression.Block(type, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given expressions, has no variables and has specific result type.</summary>
		/// <param name="type">The result type of the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D6B RID: 3435 RVA: 0x0002F78B File Offset: 0x0002D98B
		public static BlockExpression Block(Type type, IEnumerable<Expression> expressions)
		{
			return Expression.Block(type, EmptyReadOnlyCollection<ParameterExpression>.Instance, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given variables and expressions.</summary>
		/// <param name="variables">The variables in the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D6C RID: 3436 RVA: 0x0002F799 File Offset: 0x0002D999
		public static BlockExpression Block(IEnumerable<ParameterExpression> variables, params Expression[] expressions)
		{
			return Expression.Block(variables, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given variables and expressions.</summary>
		/// <param name="type">The result type of the block.</param>
		/// <param name="variables">The variables in the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D6D RID: 3437 RVA: 0x0002F7A2 File Offset: 0x0002D9A2
		public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, params Expression[] expressions)
		{
			return Expression.Block(type, variables, expressions);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given variables and expressions.</summary>
		/// <param name="variables">The variables in the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D6E RID: 3438 RVA: 0x0002F7AC File Offset: 0x0002D9AC
		public static BlockExpression Block(IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(expressions, "expressions");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = variables.ToReadOnly<ParameterExpression>();
			if (readOnlyCollection.Count == 0)
			{
				IReadOnlyList<Expression> readOnlyList = (expressions as IReadOnlyList<Expression>) ?? expressions.ToReadOnly<Expression>();
				Expression.RequiresCanRead(readOnlyList, "expressions");
				return Expression.GetOptimizedBlockExpression(readOnlyList);
			}
			ReadOnlyCollection<Expression> readOnlyCollection2 = expressions.ToReadOnly<Expression>();
			Expression.RequiresCanRead(readOnlyCollection2, "expressions");
			return Expression.BlockCore(null, readOnlyCollection, readOnlyCollection2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.BlockExpression" /> that contains the given variables and expressions.</summary>
		/// <param name="type">The result type of the block.</param>
		/// <param name="variables">The variables in the block.</param>
		/// <param name="expressions">The expressions in the block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.BlockExpression" />.</returns>
		// Token: 0x06000D6F RID: 3439 RVA: 0x0002F810 File Offset: 0x0002DA10
		public static BlockExpression Block(Type type, IEnumerable<ParameterExpression> variables, IEnumerable<Expression> expressions)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(expressions, "expressions");
			ReadOnlyCollection<Expression> readOnlyCollection = expressions.ToReadOnly<Expression>();
			Expression.RequiresCanRead(readOnlyCollection, "expressions");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection2 = variables.ToReadOnly<ParameterExpression>();
			if (readOnlyCollection2.Count == 0 && readOnlyCollection.Count != 0)
			{
				int count = readOnlyCollection.Count;
				if (count != 0 && readOnlyCollection[count - 1].Type == type)
				{
					return Expression.GetOptimizedBlockExpression(readOnlyCollection);
				}
			}
			return Expression.BlockCore(type, readOnlyCollection2, readOnlyCollection);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002F88C File Offset: 0x0002DA8C
		private static BlockExpression BlockCore(Type type, ReadOnlyCollection<ParameterExpression> variables, ReadOnlyCollection<Expression> expressions)
		{
			Expression.ValidateVariables(variables, "variables");
			if (type != null)
			{
				if (expressions.Count == 0)
				{
					if (type != typeof(void))
					{
						throw Error.ArgumentTypesMustMatch();
					}
					return new ScopeWithType(variables, expressions, type);
				}
				else
				{
					Expression expression = expressions.Last<Expression>();
					if (type != typeof(void) && !TypeUtils.AreReferenceAssignable(type, expression.Type))
					{
						throw Error.ArgumentTypesMustMatch();
					}
					if (!TypeUtils.AreEquivalent(type, expression.Type))
					{
						return new ScopeWithType(variables, expressions, type);
					}
				}
			}
			int count = expressions.Count;
			if (count == 0)
			{
				return new ScopeWithType(variables, expressions, typeof(void));
			}
			if (count != 1)
			{
				return new ScopeN(variables, expressions);
			}
			return new Scope1(variables, expressions[0]);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002F954 File Offset: 0x0002DB54
		internal static void ValidateVariables(ReadOnlyCollection<ParameterExpression> varList, string collectionName)
		{
			int count = varList.Count;
			if (count != 0)
			{
				HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
				for (int i = 0; i < count; i++)
				{
					ParameterExpression parameterExpression = varList[i];
					ContractUtils.RequiresNotNull(parameterExpression, collectionName, i);
					if (parameterExpression.IsByRef)
					{
						throw Error.VariableMustNotBeByRef(parameterExpression, parameterExpression.Type, collectionName, i);
					}
					if (!hashSet.Add(parameterExpression))
					{
						throw Error.DuplicateVariable(parameterExpression, collectionName, i);
					}
				}
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002F9B8 File Offset: 0x0002DBB8
		private static BlockExpression GetOptimizedBlockExpression(IReadOnlyList<Expression> expressions)
		{
			switch (expressions.Count)
			{
			case 0:
				return Expression.BlockCore(typeof(void), EmptyReadOnlyCollection<ParameterExpression>.Instance, EmptyReadOnlyCollection<Expression>.Instance);
			case 2:
				return new Block2(expressions[0], expressions[1]);
			case 3:
				return new Block3(expressions[0], expressions[1], expressions[2]);
			case 4:
				return new Block4(expressions[0], expressions[1], expressions[2], expressions[3]);
			case 5:
				return new Block5(expressions[0], expressions[1], expressions[2], expressions[3], expressions[4]);
			}
			IReadOnlyList<Expression> readOnlyList = expressions as ReadOnlyCollection<Expression>;
			return new BlockN(readOnlyList ?? expressions.ToArray<Expression>());
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement.</summary>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> of <see cref="T:System.Exception" /> this <see cref="T:System.Linq.Expressions.CatchBlock" /> will handle.</param>
		/// <param name="body">The body of the catch statement.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		// Token: 0x06000D73 RID: 3443 RVA: 0x0002FA9A File Offset: 0x0002DC9A
		public static CatchBlock Catch(Type type, Expression body)
		{
			return Expression.MakeCatchBlock(type, null, body, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement with a reference to the caught <see cref="T:System.Exception" /> object for use in the handler body.</summary>
		/// <param name="variable">A <see cref="T:System.Linq.Expressions.ParameterExpression" /> representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</param>
		/// <param name="body">The body of the catch statement.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		// Token: 0x06000D74 RID: 3444 RVA: 0x0002FAA5 File Offset: 0x0002DCA5
		public static CatchBlock Catch(ParameterExpression variable, Expression body)
		{
			ContractUtils.RequiresNotNull(variable, "variable");
			return Expression.MakeCatchBlock(variable.Type, variable, body, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement with an <see cref="T:System.Exception" /> filter but no reference to the caught <see cref="T:System.Exception" /> object.</summary>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> of <see cref="T:System.Exception" /> this <see cref="T:System.Linq.Expressions.CatchBlock" /> will handle.</param>
		/// <param name="body">The body of the catch statement.</param>
		/// <param name="filter">The body of the <see cref="T:System.Exception" /> filter.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		// Token: 0x06000D75 RID: 3445 RVA: 0x0002FAC0 File Offset: 0x0002DCC0
		public static CatchBlock Catch(Type type, Expression body, Expression filter)
		{
			return Expression.MakeCatchBlock(type, null, body, filter);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement with an <see cref="T:System.Exception" /> filter and a reference to the caught <see cref="T:System.Exception" /> object.</summary>
		/// <param name="variable">A <see cref="T:System.Linq.Expressions.ParameterExpression" /> representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</param>
		/// <param name="body">The body of the catch statement.</param>
		/// <param name="filter">The body of the <see cref="T:System.Exception" /> filter.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		// Token: 0x06000D76 RID: 3446 RVA: 0x0002FACB File Offset: 0x0002DCCB
		public static CatchBlock Catch(ParameterExpression variable, Expression body, Expression filter)
		{
			ContractUtils.RequiresNotNull(variable, "variable");
			return Expression.MakeCatchBlock(variable.Type, variable, body, filter);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.CatchBlock" /> representing a catch statement with the specified elements.</summary>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> of <see cref="T:System.Exception" /> this <see cref="T:System.Linq.Expressions.CatchBlock" /> will handle.</param>
		/// <param name="variable">A <see cref="T:System.Linq.Expressions.ParameterExpression" /> representing a reference to the <see cref="T:System.Exception" /> object caught by this handler.</param>
		/// <param name="body">The body of the catch statement.</param>
		/// <param name="filter">The body of the <see cref="T:System.Exception" /> filter.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.CatchBlock" />.</returns>
		// Token: 0x06000D77 RID: 3447 RVA: 0x0002FAE8 File Offset: 0x0002DCE8
		public static CatchBlock MakeCatchBlock(Type type, ParameterExpression variable, Expression body, Expression filter)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.Requires(variable == null || TypeUtils.AreEquivalent(variable.Type, type), "variable");
			if (variable == null)
			{
				TypeUtils.ValidateType(type, "type");
			}
			else if (variable.IsByRef)
			{
				throw Error.VariableMustNotBeByRef(variable, variable.Type, "variable");
			}
			ExpressionUtils.RequiresCanRead(body, "body");
			if (filter != null)
			{
				ExpressionUtils.RequiresCanRead(filter, "filter");
				if (filter.Type != typeof(bool))
				{
					throw Error.ArgumentMustBeBoolean("filter");
				}
			}
			return new CatchBlock(type, variable, body, filter);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that represents a conditional statement.</summary>
		/// <param name="test">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property equal to.</param>
		/// <param name="ifTrue">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property equal to.</param>
		/// <param name="ifFalse">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Conditional" /> and the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" />, <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" />, and <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="test" /> or <paramref name="ifTrue" /> or <paramref name="ifFalse" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="test" />.Type is not <see cref="T:System.Boolean" />.-or-
		///         <paramref name="ifTrue" />.Type is not equal to <paramref name="ifFalse" />.Type.</exception>
		// Token: 0x06000D78 RID: 3448 RVA: 0x0002FB8C File Offset: 0x0002DD8C
		public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse)
		{
			ExpressionUtils.RequiresCanRead(test, "test");
			ExpressionUtils.RequiresCanRead(ifTrue, "ifTrue");
			ExpressionUtils.RequiresCanRead(ifFalse, "ifFalse");
			if (test.Type != typeof(bool))
			{
				throw Error.ArgumentMustBeBoolean("test");
			}
			if (!TypeUtils.AreEquivalent(ifTrue.Type, ifFalse.Type))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return ConditionalExpression.Make(test, ifTrue, ifFalse, ifTrue.Type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that represents a conditional statement.</summary>
		/// <param name="test">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property equal to.</param>
		/// <param name="ifTrue">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property equal to.</param>
		/// <param name="ifFalse">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property equal to.</param>
		/// <param name="type">A <see cref="P:System.Linq.Expressions.Expression.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Conditional" /> and the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" />, <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" />, and <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> properties set to the specified values.</returns>
		// Token: 0x06000D79 RID: 3449 RVA: 0x0002FC04 File Offset: 0x0002DE04
		public static ConditionalExpression Condition(Expression test, Expression ifTrue, Expression ifFalse, Type type)
		{
			ExpressionUtils.RequiresCanRead(test, "test");
			ExpressionUtils.RequiresCanRead(ifTrue, "ifTrue");
			ExpressionUtils.RequiresCanRead(ifFalse, "ifFalse");
			ContractUtils.RequiresNotNull(type, "type");
			if (test.Type != typeof(bool))
			{
				throw Error.ArgumentMustBeBoolean("test");
			}
			if (type != typeof(void) && (!TypeUtils.AreReferenceAssignable(type, ifTrue.Type) || !TypeUtils.AreReferenceAssignable(type, ifFalse.Type)))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return ConditionalExpression.Make(test, ifTrue, ifFalse, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that represents a conditional block with an <see langword="if" /> statement.</summary>
		/// <param name="test">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property equal to.</param>
		/// <param name="ifTrue">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Conditional" /> and the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" />, <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" />, properties set to the specified values. The <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property is set to default expression and the type of the resulting <see cref="T:System.Linq.Expressions.ConditionalExpression" /> returned by this method is <see cref="T:System.Void" />.</returns>
		// Token: 0x06000D7A RID: 3450 RVA: 0x0002FC9C File Offset: 0x0002DE9C
		public static ConditionalExpression IfThen(Expression test, Expression ifTrue)
		{
			return Expression.Condition(test, ifTrue, Expression.Empty(), typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that represents a conditional block with <see langword="if" /> and <see langword="else" /> statements.</summary>
		/// <param name="test">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" /> property equal to.</param>
		/// <param name="ifTrue">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" /> property equal to.</param>
		/// <param name="ifFalse">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConditionalExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Conditional" /> and the <see cref="P:System.Linq.Expressions.ConditionalExpression.Test" />, <see cref="P:System.Linq.Expressions.ConditionalExpression.IfTrue" />, and <see cref="P:System.Linq.Expressions.ConditionalExpression.IfFalse" /> properties set to the specified values. The type of the resulting <see cref="T:System.Linq.Expressions.ConditionalExpression" /> returned by this method is <see cref="T:System.Void" />.</returns>
		// Token: 0x06000D7B RID: 3451 RVA: 0x0002FCB4 File Offset: 0x0002DEB4
		public static ConditionalExpression IfThenElse(Expression test, Expression ifTrue, Expression ifFalse)
		{
			return Expression.Condition(test, ifTrue, ifFalse, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConstantExpression" /> that has the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> property set to the specified value.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> to set the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConstantExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Constant" /> and the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> property set to the specified value.</returns>
		// Token: 0x06000D7C RID: 3452 RVA: 0x0002FCC8 File Offset: 0x0002DEC8
		public static ConstantExpression Constant(object value)
		{
			return new ConstantExpression(value);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ConstantExpression" /> that has the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> to set the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ConstantExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Constant" /> and the <see cref="P:System.Linq.Expressions.ConstantExpression.Value" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="value" /> is not <see langword="null" /> and <paramref name="type" /> is not assignable from the dynamic type of <paramref name="value" />.</exception>
		// Token: 0x06000D7D RID: 3453 RVA: 0x0002FCD0 File Offset: 0x0002DED0
		public static ConstantExpression Constant(object value, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (value == null)
			{
				if (type == typeof(object))
				{
					return new ConstantExpression(null);
				}
				if (!type.IsValueType || type.IsNullableType())
				{
					return new TypedConstantExpression(null, type);
				}
			}
			else
			{
				Type type2 = value.GetType();
				if (type == type2)
				{
					return new ConstantExpression(value);
				}
				if (type.IsAssignableFrom(type2))
				{
					return new TypedConstantExpression(value, type);
				}
			}
			throw Error.ArgumentTypesMustMatch();
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> with the specified span.</summary>
		/// <param name="document">The <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that represents the source file.</param>
		/// <param name="startLine">The start line of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />. Must be greater than 0.</param>
		/// <param name="startColumn">The start column of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />. Must be greater than 0.</param>
		/// <param name="endLine">The end line of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />. Must be greater or equal than the start line.</param>
		/// <param name="endColumn">The end column of this <see cref="T:System.Linq.Expressions.DebugInfoExpression" />. If the end line is the same as the start line, it must be greater or equal than the start column. In any case, must be greater than 0.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.DebugInfoExpression" />.</returns>
		// Token: 0x06000D7E RID: 3454 RVA: 0x0002FD54 File Offset: 0x0002DF54
		public static DebugInfoExpression DebugInfo(SymbolDocumentInfo document, int startLine, int startColumn, int endLine, int endColumn)
		{
			ContractUtils.RequiresNotNull(document, "document");
			if (startLine == 16707566 && startColumn == 0 && endLine == 16707566 && endColumn == 0)
			{
				return new ClearDebugInfoExpression(document);
			}
			Expression.ValidateSpan(startLine, startColumn, endLine, endColumn);
			return new SpanDebugInfoExpression(document, startLine, startColumn, endLine, endColumn);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> for clearing a sequence point.</summary>
		/// <param name="document">The <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that represents the source file.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.DebugInfoExpression" /> for clearning a sequence point.</returns>
		// Token: 0x06000D7F RID: 3455 RVA: 0x0002FD94 File Offset: 0x0002DF94
		public static DebugInfoExpression ClearDebugInfo(SymbolDocumentInfo document)
		{
			ContractUtils.RequiresNotNull(document, "document");
			return new ClearDebugInfoExpression(document);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002FDA8 File Offset: 0x0002DFA8
		private static void ValidateSpan(int startLine, int startColumn, int endLine, int endColumn)
		{
			if (startLine < 1)
			{
				throw Error.OutOfRange("startLine", 1);
			}
			if (startColumn < 1)
			{
				throw Error.OutOfRange("startColumn", 1);
			}
			if (endLine < 1)
			{
				throw Error.OutOfRange("endLine", 1);
			}
			if (endColumn < 1)
			{
				throw Error.OutOfRange("endColumn", 1);
			}
			if (startLine > endLine)
			{
				throw Error.StartEndMustBeOrdered();
			}
			if (startLine == endLine && startColumn > endColumn)
			{
				throw Error.StartEndMustBeOrdered();
			}
		}

		/// <summary>Creates an empty expression that has <see cref="T:System.Void" /> type.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.DefaultExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Default" /> and the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <see cref="T:System.Void" />.</returns>
		// Token: 0x06000D81 RID: 3457 RVA: 0x0002FE21 File Offset: 0x0002E021
		public static DefaultExpression Empty()
		{
			return new DefaultExpression(typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DefaultExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DefaultExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Default" /> and the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to the specified type.</returns>
		// Token: 0x06000D82 RID: 3458 RVA: 0x0002FE32 File Offset: 0x0002E032
		public static DefaultExpression Default(Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			return new DefaultExpression(type);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.ElementInit" />, given an array of values as the second argument.</summary>
		/// <param name="addMethod">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.ElementInit.AddMethod" /> property equal to.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to set the <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> property equal to.</param>
		/// <returns>An <see cref="T:System.Linq.Expressions.ElementInit" /> that has the <see cref="P:System.Linq.Expressions.ElementInit.AddMethod" /> and <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="addMethod" /> or <paramref name="arguments" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The method that addMethod represents is not named "Add" (case insensitive).-or-The method that addMethod represents is not an instance method.-or-arguments does not contain the same number of elements as the number of parameters for the method that addMethod represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the method that <paramref name="addMethod" /> represents.</exception>
		// Token: 0x06000D83 RID: 3459 RVA: 0x0002FE50 File Offset: 0x0002E050
		public static ElementInit ElementInit(MethodInfo addMethod, params Expression[] arguments)
		{
			return Expression.ElementInit(addMethod, arguments);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.ElementInit" />, given an <see cref="T:System.Collections.Generic.IEnumerable`1" /> as the second argument.</summary>
		/// <param name="addMethod">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.ElementInit.AddMethod" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to set the <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> property equal to.</param>
		/// <returns>An <see cref="T:System.Linq.Expressions.ElementInit" /> that has the <see cref="P:System.Linq.Expressions.ElementInit.AddMethod" /> and <see cref="P:System.Linq.Expressions.ElementInit.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="addMethod" /> or <paramref name="arguments" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The method that <paramref name="addMethod" /> represents is not named "Add" (case insensitive).-or-The method that <paramref name="addMethod" /> represents is not an instance method.-or-
		///         <paramref name="arguments" /> does not contain the same number of elements as the number of parameters for the method that <paramref name="addMethod" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the method that <paramref name="addMethod" /> represents.</exception>
		// Token: 0x06000D84 RID: 3460 RVA: 0x0002FE5C File Offset: 0x0002E05C
		public static ElementInit ElementInit(MethodInfo addMethod, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(addMethod, "addMethod");
			ContractUtils.RequiresNotNull(arguments, "arguments");
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnly<Expression>();
			Expression.RequiresCanRead(readOnlyCollection, "arguments");
			Expression.ValidateElementInitAddMethodInfo(addMethod, "addMethod");
			Expression.ValidateArgumentTypes(addMethod, ExpressionType.Call, ref readOnlyCollection, "addMethod");
			return new ElementInit(addMethod, readOnlyCollection);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002FEB4 File Offset: 0x0002E0B4
		private static void ValidateElementInitAddMethodInfo(MethodInfo addMethod, string paramName)
		{
			Expression.ValidateMethodInfo(addMethod, paramName);
			ParameterInfo[] parametersCached = addMethod.GetParametersCached();
			if (parametersCached.Length == 0)
			{
				throw Error.ElementInitializerMethodWithZeroArgs(paramName);
			}
			if (!addMethod.Name.Equals("Add", StringComparison.OrdinalIgnoreCase))
			{
				throw Error.ElementInitializerMethodNotAdd(paramName);
			}
			if (addMethod.IsStatic)
			{
				throw Error.ElementInitializerMethodStatic(paramName);
			}
			foreach (ParameterInfo parameterInfo in parametersCached)
			{
				if (parameterInfo.ParameterType.IsByRef)
				{
					throw Error.ElementInitializerMethodNoRefOutParam(parameterInfo.Name, addMethod.Name, paramName);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.Expressions.Expression" /> class.</summary>
		/// <param name="nodeType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> to set as the node type.</param>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> of this <see cref="T:System.Linq.Expressions.Expression" />.</param>
		// Token: 0x06000D86 RID: 3462 RVA: 0x0002FF35 File Offset: 0x0002E135
		[Obsolete("use a different constructor that does not take ExpressionType. Then override NodeType and Type properties to provide the values that would be specified to this constructor.")]
		protected Expression(ExpressionType nodeType, Type type)
		{
			if (Expression.s_legacyCtorSupportTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<Expression, Expression.ExtensionInfo>>(ref Expression.s_legacyCtorSupportTable, new ConditionalWeakTable<Expression, Expression.ExtensionInfo>(), null);
			}
			Expression.s_legacyCtorSupportTable.Add(this, new Expression.ExtensionInfo(nodeType, type));
		}

		/// <summary>Constructs a new instance of <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		// Token: 0x06000D87 RID: 3463 RVA: 0x00002162 File Offset: 0x00000362
		protected Expression()
		{
		}

		/// <summary>Gets the node type of this <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>One of the <see cref="T:System.Linq.Expressions.ExpressionType" /> values.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0002FF68 File Offset: 0x0002E168
		public virtual ExpressionType NodeType
		{
			get
			{
				Expression.ExtensionInfo extensionInfo;
				if (Expression.s_legacyCtorSupportTable != null && Expression.s_legacyCtorSupportTable.TryGetValue(this, out extensionInfo))
				{
					return extensionInfo.NodeType;
				}
				throw Error.ExtensionNodeMustOverrideProperty("Expression.NodeType");
			}
		}

		/// <summary>Gets the static type of the expression that this <see cref="T:System.Linq.Expressions.Expression" /> represents.</summary>
		/// <returns>The <see cref="T:System.Type" /> that represents the static type of the expression.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0002FF9C File Offset: 0x0002E19C
		public virtual Type Type
		{
			get
			{
				Expression.ExtensionInfo extensionInfo;
				if (Expression.s_legacyCtorSupportTable != null && Expression.s_legacyCtorSupportTable.TryGetValue(this, out extensionInfo))
				{
					return extensionInfo.Type;
				}
				throw Error.ExtensionNodeMustOverrideProperty("Expression.Type");
			}
		}

		/// <summary>Indicates that the node can be reduced to a simpler node. If this returns true, Reduce() can be called to produce the reduced form.</summary>
		/// <returns>True if the node can be reduced, otherwise false.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000023D1 File Offset: 0x000005D1
		public virtual bool CanReduce
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reduces this node to a simpler expression. If CanReduce returns true, this should return a valid expression. This method can return another node which itself must be reduced.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000D8B RID: 3467 RVA: 0x0002FFD0 File Offset: 0x0002E1D0
		public virtual Expression Reduce()
		{
			if (this.CanReduce)
			{
				throw Error.ReducibleMustOverrideReduce();
			}
			return this;
		}

		/// <summary>Reduces the node and then calls the visitor delegate on the reduced expression. The method throws an exception if the node is not reducible.</summary>
		/// <param name="visitor">An instance of <see cref="T:System.Func`2" />.</param>
		/// <returns>The expression being visited, or an expression which should replace it in the tree.</returns>
		// Token: 0x06000D8C RID: 3468 RVA: 0x0002FFE1 File Offset: 0x0002E1E1
		protected internal virtual Expression VisitChildren(ExpressionVisitor visitor)
		{
			if (!this.CanReduce)
			{
				throw Error.MustBeReducible();
			}
			return visitor.Visit(this.ReduceAndCheck());
		}

		/// <summary>Dispatches to the specific visit method for this node type. For example, <see cref="T:System.Linq.Expressions.MethodCallExpression" /> calls the <see cref="M:System.Linq.Expressions.ExpressionVisitor.VisitMethodCall(System.Linq.Expressions.MethodCallExpression)" />.</summary>
		/// <param name="visitor">The visitor to visit this node with.</param>
		/// <returns>The result of visiting this node.</returns>
		// Token: 0x06000D8D RID: 3469 RVA: 0x0002FFFD File Offset: 0x0002E1FD
		protected internal virtual Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitExtension(this);
		}

		/// <summary>Reduces this node to a simpler expression. If CanReduce returns true, this should return a valid expression. This method can return another node which itself must be reduced.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000D8E RID: 3470 RVA: 0x00030008 File Offset: 0x0002E208
		public Expression ReduceAndCheck()
		{
			if (!this.CanReduce)
			{
				throw Error.MustBeReducible();
			}
			Expression expression = this.Reduce();
			if (expression == null || expression == this)
			{
				throw Error.MustReduceToDifferent();
			}
			if (!TypeUtils.AreReferenceAssignable(this.Type, expression.Type))
			{
				throw Error.ReducedNotCompatible();
			}
			return expression;
		}

		/// <summary>Reduces the expression to a known node type (that is not an Extension node) or just returns the expression if it is already a known type.</summary>
		/// <returns>The reduced expression.</returns>
		// Token: 0x06000D8F RID: 3471 RVA: 0x00030054 File Offset: 0x0002E254
		public Expression ReduceExtensions()
		{
			Expression expression = this;
			while (expression.NodeType == ExpressionType.Extension)
			{
				expression = expression.ReduceAndCheck();
			}
			return expression;
		}

		/// <summary>Returns a textual representation of the <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.Expression" />.</returns>
		// Token: 0x06000D90 RID: 3472 RVA: 0x00030077 File Offset: 0x0002E277
		public override string ToString()
		{
			return ExpressionStringBuilder.ExpressionToString(this);
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00030080 File Offset: 0x0002E280
		private string DebugView
		{
			get
			{
				string result;
				using (StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture))
				{
					DebugViewWriter.WriteTo(this, stringWriter);
					result = stringWriter.ToString();
				}
				return result;
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000300C4 File Offset: 0x0002E2C4
		private static void RequiresCanRead(IReadOnlyList<Expression> items, string paramName)
		{
			int i = 0;
			int count = items.Count;
			while (i < count)
			{
				ExpressionUtils.RequiresCanRead(items[i], paramName, i);
				i++;
			}
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000300F4 File Offset: 0x0002E2F4
		private static void RequiresCanWrite(Expression expression, string paramName)
		{
			if (expression == null)
			{
				throw new ArgumentNullException(paramName);
			}
			ExpressionType nodeType = expression.NodeType;
			if (nodeType != ExpressionType.MemberAccess)
			{
				if (nodeType == ExpressionType.Parameter)
				{
					return;
				}
				if (nodeType == ExpressionType.Index)
				{
					PropertyInfo indexer = ((IndexExpression)expression).Indexer;
					if (indexer == null || indexer.CanWrite)
					{
						return;
					}
				}
			}
			else
			{
				MemberInfo member = ((MemberExpression)expression).Member;
				PropertyInfo propertyInfo = member as PropertyInfo;
				if (propertyInfo != null)
				{
					if (propertyInfo.CanWrite)
					{
						return;
					}
				}
				else
				{
					FieldInfo fieldInfo = (FieldInfo)member;
					if (!fieldInfo.IsInitOnly && !fieldInfo.IsLiteral)
					{
						return;
					}
				}
			}
			throw Error.ExpressionMustBeWriteable(paramName);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D94 RID: 3476 RVA: 0x00030186 File Offset: 0x0002E386
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, IEnumerable<Expression> arguments)
		{
			return DynamicExpression.Dynamic(binder, returnType, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D95 RID: 3477 RVA: 0x00030190 File Offset: 0x0002E390
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0)
		{
			return DynamicExpression.Dynamic(binder, returnType, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D96 RID: 3478 RVA: 0x0003019A File Offset: 0x0002E39A
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1)
		{
			return DynamicExpression.Dynamic(binder, returnType, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D97 RID: 3479 RVA: 0x000301A5 File Offset: 0x0002E3A5
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2)
		{
			return DynamicExpression.Dynamic(binder, returnType, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		/// <param name="arg3">The fourth argument to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D98 RID: 3480 RVA: 0x000301B2 File Offset: 0x0002E3B2
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			return DynamicExpression.Dynamic(binder, returnType, arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="returnType">The result type of the dynamic expression.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" /> and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D99 RID: 3481 RVA: 0x000301C1 File Offset: 0x0002E3C1
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, params Expression[] arguments)
		{
			return DynamicExpression.Dynamic(binder, returnType, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D9A RID: 3482 RVA: 0x000301CB File Offset: 0x0002E3CB
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, IEnumerable<Expression> arguments)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and one argument.</summary>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The argument to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D9B RID: 3483 RVA: 0x000301D5 File Offset: 0x0002E3D5
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and two arguments.</summary>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D9C RID: 3484 RVA: 0x000301DF File Offset: 0x0002E3DF
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and three arguments.</summary>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D9D RID: 3485 RVA: 0x000301EA File Offset: 0x0002E3EA
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> and four arguments.</summary>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arg0">The first argument to the dynamic operation.</param>
		/// <param name="arg1">The second argument to the dynamic operation.</param>
		/// <param name="arg2">The third argument to the dynamic operation.</param>
		/// <param name="arg3">The fourth argument to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D9E RID: 3486 RVA: 0x000301F7 File Offset: 0x0002E3F7
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			return DynamicExpression.MakeDynamic(delegateType, binder, arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.DynamicExpression" /> that represents a dynamic operation bound by the provided <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" />.</summary>
		/// <param name="delegateType">The type of the delegate used by the <see cref="T:System.Runtime.CompilerServices.CallSite" />.</param>
		/// <param name="binder">The runtime binder for the dynamic operation.</param>
		/// <param name="arguments">The arguments to the dynamic operation.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.DynamicExpression" /> that has <see cref="P:System.Linq.Expressions.Expression.NodeType" /> equal to <see cref="F:System.Linq.Expressions.ExpressionType.Dynamic" /> and has the <see cref="P:System.Linq.Expressions.DynamicExpression.DelegateType" />, <see cref="P:System.Linq.Expressions.DynamicExpression.Binder" />, and <see cref="P:System.Linq.Expressions.DynamicExpression.Arguments" /> set to the specified values.</returns>
		// Token: 0x06000D9F RID: 3487 RVA: 0x00030206 File Offset: 0x0002E406
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, params Expression[] arguments)
		{
			return Expression.MakeDynamic(delegateType, binder, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a break statement.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Break, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and a null value to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x00030210 File Offset: 0x0002E410
		public static GotoExpression Break(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, null, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a break statement. The value passed to the label upon jumping can be specified.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Break, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00030224 File Offset: 0x0002E424
		public static GotoExpression Break(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, value, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a break statement with the specified type.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Break, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />.</returns>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x00030238 File Offset: 0x0002E438
		public static GotoExpression Break(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a break statement with the specified type. The value passed to the label upon jumping can be specified.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Break, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x00030243 File Offset: 0x0002E443
		public static GotoExpression Break(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Break, target, value, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a continue statement.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Continue, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and a null value to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003024E File Offset: 0x0002E44E
		public static GotoExpression Continue(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Continue, target, null, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a continue statement with the specified type.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Continue, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and a null value to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA5 RID: 3493 RVA: 0x00030262 File Offset: 0x0002E462
		public static GotoExpression Continue(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Continue, target, null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a return statement.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Return, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and a null value to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003026D File Offset: 0x0002E46D
		public static GotoExpression Return(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, null, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a return statement with the specified type.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Return, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and a null value to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA7 RID: 3495 RVA: 0x00030281 File Offset: 0x0002E481
		public static GotoExpression Return(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a return statement. The value passed to the label upon jumping can be specified.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Continue, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA8 RID: 3496 RVA: 0x0003028C File Offset: 0x0002E48C
		public static GotoExpression Return(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, value, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a return statement with the specified type. The value passed to the label upon jumping can be specified.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Continue, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DA9 RID: 3497 RVA: 0x000302A0 File Offset: 0x0002E4A0
		public static GotoExpression Return(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Return, target, value, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a "go to" statement.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Goto, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to the specified value, and a null value to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DAA RID: 3498 RVA: 0x000302AB File Offset: 0x0002E4AB
		public static GotoExpression Goto(LabelTarget target)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, null, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a "go to" statement with the specified type.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Goto, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to the specified value, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and a null value to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DAB RID: 3499 RVA: 0x000302BF File Offset: 0x0002E4BF
		public static GotoExpression Goto(LabelTarget target, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a "go to" statement. The value passed to the label upon jumping can be specified.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Goto, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DAC RID: 3500 RVA: 0x000302CA File Offset: 0x0002E4CA
		public static GotoExpression Goto(LabelTarget target, Expression value)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, value, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a "go to" statement with the specified type. The value passed to the label upon jumping can be specified.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to Goto, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DAD RID: 3501 RVA: 0x000302DE File Offset: 0x0002E4DE
		public static GotoExpression Goto(LabelTarget target, Expression value, Type type)
		{
			return Expression.MakeGoto(GotoExpressionKind.Goto, target, value, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.GotoExpression" /> representing a jump of the specified <see cref="T:System.Linq.Expressions.GotoExpressionKind" />. The value passed to the label upon jumping can also be specified.</summary>
		/// <param name="kind">The <see cref="T:System.Linq.Expressions.GotoExpressionKind" /> of the <see cref="T:System.Linq.Expressions.GotoExpression" />.</param>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> that the <see cref="T:System.Linq.Expressions.GotoExpression" /> will jump to.</param>
		/// <param name="value">The value that will be passed to the associated label upon jumping.</param>
		/// <param name="type">An <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.GotoExpression" /> with <see cref="P:System.Linq.Expressions.GotoExpression.Kind" /> equal to <paramref name="kind" />, the <see cref="P:System.Linq.Expressions.GotoExpression.Target" /> property set to <paramref name="target" />, the <see cref="P:System.Linq.Expressions.Expression.Type" /> property set to <paramref name="type" />, and <paramref name="value" /> to be passed to the target label upon jumping.</returns>
		// Token: 0x06000DAE RID: 3502 RVA: 0x000302E9 File Offset: 0x0002E4E9
		public static GotoExpression MakeGoto(GotoExpressionKind kind, LabelTarget target, Expression value, Type type)
		{
			Expression.ValidateGoto(target, ref value, "target", "value", type);
			return new GotoExpression(kind, target, value, type);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00030308 File Offset: 0x0002E508
		private static void ValidateGoto(LabelTarget target, ref Expression value, string targetParameter, string valueParameter, Type type)
		{
			ContractUtils.RequiresNotNull(target, targetParameter);
			if (value == null)
			{
				if (target.Type != typeof(void))
				{
					throw Error.LabelMustBeVoidOrHaveExpression("target");
				}
				if (type != null)
				{
					TypeUtils.ValidateType(type, "type");
					return;
				}
			}
			else
			{
				Expression.ValidateGotoType(target.Type, ref value, valueParameter);
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00030368 File Offset: 0x0002E568
		private static void ValidateGotoType(Type expectedType, ref Expression value, string paramName)
		{
			ExpressionUtils.RequiresCanRead(value, paramName);
			if (expectedType != typeof(void) && !TypeUtils.AreReferenceAssignable(expectedType, value.Type) && !Expression.TryQuote(expectedType, ref value))
			{
				throw Error.ExpressionTypeDoesNotMatchLabel(value.Type, expectedType);
			}
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> that represents accessing an indexed property in an object.</summary>
		/// <param name="instance">The object to which the property belongs. It should be null if the property is <see langword="static" /> (<see langword="shared" /> in Visual Basic).</param>
		/// <param name="indexer">An <see cref="T:System.Linq.Expressions.Expression" /> representing the property to index.</param>
		/// <param name="arguments">An IEnumerable&lt;Expression&gt; (IEnumerable (Of Expression) in Visual Basic) that contains the arguments that will be used to index the property.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		// Token: 0x06000DB1 RID: 3505 RVA: 0x000303B5 File Offset: 0x0002E5B5
		public static IndexExpression MakeIndex(Expression instance, PropertyInfo indexer, IEnumerable<Expression> arguments)
		{
			if (indexer != null)
			{
				return Expression.Property(instance, indexer, arguments);
			}
			return Expression.ArrayAccess(instance, arguments);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> to access an array.</summary>
		/// <param name="array">An expression representing the array to index.</param>
		/// <param name="indexes">An array that contains expressions used to index the array.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		// Token: 0x06000DB2 RID: 3506 RVA: 0x000303D0 File Offset: 0x0002E5D0
		public static IndexExpression ArrayAccess(Expression array, params Expression[] indexes)
		{
			return Expression.ArrayAccess(array, indexes);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> to access a multidimensional array.</summary>
		/// <param name="array">An expression that represents the multidimensional array.</param>
		/// <param name="indexes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> containing expressions used to index the array.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		// Token: 0x06000DB3 RID: 3507 RVA: 0x000303DC File Offset: 0x0002E5DC
		public static IndexExpression ArrayAccess(Expression array, IEnumerable<Expression> indexes)
		{
			ExpressionUtils.RequiresCanRead(array, "array");
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray("array");
			}
			ReadOnlyCollection<Expression> readOnlyCollection = indexes.ToReadOnly<Expression>();
			if (type.GetArrayRank() != readOnlyCollection.Count)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			foreach (Expression expression in readOnlyCollection)
			{
				ExpressionUtils.RequiresCanRead(expression, "indexes");
				if (expression.Type != typeof(int))
				{
					throw Error.ArgumentMustBeArrayIndexType("indexes");
				}
			}
			return new IndexExpression(array, null, readOnlyCollection);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> representing the access to an indexed property.</summary>
		/// <param name="instance">The object to which the property belongs. If the property is static/shared, it must be null.</param>
		/// <param name="propertyName">The name of the indexer.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that are used to index the property.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		// Token: 0x06000DB4 RID: 3508 RVA: 0x00030490 File Offset: 0x0002E690
		public static IndexExpression Property(Expression instance, string propertyName, params Expression[] arguments)
		{
			ExpressionUtils.RequiresCanRead(instance, "instance");
			ContractUtils.RequiresNotNull(propertyName, "propertyName");
			PropertyInfo indexer = Expression.FindInstanceProperty(instance.Type, propertyName, arguments);
			return Expression.MakeIndexProperty(instance, indexer, "propertyName", arguments.ToReadOnly<Expression>());
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x000304D4 File Offset: 0x0002E6D4
		private static PropertyInfo FindInstanceProperty(Type type, string propertyName, Expression[] arguments)
		{
			BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy;
			PropertyInfo propertyInfo = Expression.FindProperty(type, propertyName, arguments, flags);
			if (propertyInfo == null)
			{
				flags = (BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
				propertyInfo = Expression.FindProperty(type, propertyName, arguments, flags);
			}
			if (!(propertyInfo == null))
			{
				return propertyInfo;
			}
			if (arguments == null || arguments.Length == 0)
			{
				throw Error.InstancePropertyWithoutParameterNotDefinedForType(propertyName, type);
			}
			throw Error.InstancePropertyWithSpecifiedParametersNotDefinedForType(propertyName, Expression.GetArgTypesString(arguments), type, "propertyName");
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00030530 File Offset: 0x0002E730
		private static string GetArgTypesString(Expression[] arguments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			for (int i = 0; i < arguments.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				StringBuilder stringBuilder2 = stringBuilder;
				Expression expression = arguments[i];
				stringBuilder2.Append((expression != null) ? expression.Type.Name : null);
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00030594 File Offset: 0x0002E794
		private static PropertyInfo FindProperty(Type type, string propertyName, Expression[] arguments, BindingFlags flags)
		{
			PropertyInfo propertyInfo = null;
			foreach (PropertyInfo propertyInfo2 in type.GetProperties(flags))
			{
				if (propertyInfo2.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase) && Expression.IsCompatible(propertyInfo2, arguments))
				{
					if (!(propertyInfo == null))
					{
						throw Error.PropertyWithMoreThanOneMatch(propertyName, type);
					}
					propertyInfo = propertyInfo2;
				}
			}
			return propertyInfo;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000305EC File Offset: 0x0002E7EC
		private static bool IsCompatible(PropertyInfo pi, Expression[] args)
		{
			MethodInfo methodInfo = pi.GetGetMethod(true);
			ParameterInfo[] array;
			if (methodInfo != null)
			{
				array = methodInfo.GetParametersCached();
			}
			else
			{
				methodInfo = pi.GetSetMethod(true);
				if (methodInfo == null)
				{
					return false;
				}
				array = methodInfo.GetParametersCached();
				if (array.Length == 0)
				{
					return false;
				}
				array = array.RemoveLast<ParameterInfo>();
			}
			if (args == null)
			{
				return array.Length == 0;
			}
			if (array.Length != args.Length)
			{
				return false;
			}
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == null)
				{
					return false;
				}
				if (!TypeUtils.AreReferenceAssignable(array[i].ParameterType, args[i].Type))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> representing the access to an indexed property.</summary>
		/// <param name="instance">The object to which the property belongs. If the property is static/shared, it must be null.</param>
		/// <param name="indexer">The <see cref="T:System.Reflection.PropertyInfo" /> that represents the property to index.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that are used to index the property.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0003067C File Offset: 0x0002E87C
		public static IndexExpression Property(Expression instance, PropertyInfo indexer, params Expression[] arguments)
		{
			return Expression.Property(instance, indexer, arguments);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.IndexExpression" /> representing the access to an indexed property.</summary>
		/// <param name="instance">The object to which the property belongs. If the property is static/shared, it must be null.</param>
		/// <param name="indexer">The <see cref="T:System.Reflection.PropertyInfo" /> that represents the property to index.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects that are used to index the property.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.IndexExpression" />.</returns>
		// Token: 0x06000DBA RID: 3514 RVA: 0x00030686 File Offset: 0x0002E886
		public static IndexExpression Property(Expression instance, PropertyInfo indexer, IEnumerable<Expression> arguments)
		{
			return Expression.MakeIndexProperty(instance, indexer, "indexer", arguments.ToReadOnly<Expression>());
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0003069A File Offset: 0x0002E89A
		private static IndexExpression MakeIndexProperty(Expression instance, PropertyInfo indexer, string paramName, ReadOnlyCollection<Expression> argList)
		{
			Expression.ValidateIndexedProperty(instance, indexer, paramName, ref argList);
			return new IndexExpression(instance, indexer, argList);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000306B0 File Offset: 0x0002E8B0
		private static void ValidateIndexedProperty(Expression instance, PropertyInfo indexer, string paramName, ref ReadOnlyCollection<Expression> argList)
		{
			ContractUtils.RequiresNotNull(indexer, paramName);
			if (indexer.PropertyType.IsByRef)
			{
				throw Error.PropertyCannotHaveRefType(paramName);
			}
			if (indexer.PropertyType == typeof(void))
			{
				throw Error.PropertyTypeCannotBeVoid(paramName);
			}
			ParameterInfo[] array = null;
			MethodInfo getMethod = indexer.GetGetMethod(true);
			if (getMethod != null)
			{
				if (getMethod.ReturnType != indexer.PropertyType)
				{
					throw Error.PropertyTypeMustMatchGetter(paramName);
				}
				array = getMethod.GetParametersCached();
				Expression.ValidateAccessor(instance, getMethod, array, ref argList, paramName);
			}
			MethodInfo setMethod = indexer.GetSetMethod(true);
			if (setMethod != null)
			{
				ParameterInfo[] parametersCached = setMethod.GetParametersCached();
				if (parametersCached.Length == 0)
				{
					throw Error.SetterHasNoParams(paramName);
				}
				Type parameterType = parametersCached[parametersCached.Length - 1].ParameterType;
				if (parameterType.IsByRef)
				{
					throw Error.PropertyCannotHaveRefType(paramName);
				}
				if (setMethod.ReturnType != typeof(void))
				{
					throw Error.SetterMustBeVoid(paramName);
				}
				if (indexer.PropertyType != parameterType)
				{
					throw Error.PropertyTypeMustMatchSetter(paramName);
				}
				if (!(getMethod != null))
				{
					Expression.ValidateAccessor(instance, setMethod, parametersCached.RemoveLast<ParameterInfo>(), ref argList, paramName);
					return;
				}
				if (getMethod.IsStatic ^ setMethod.IsStatic)
				{
					throw Error.BothAccessorsMustBeStatic(paramName);
				}
				if (array.Length != parametersCached.Length - 1)
				{
					throw Error.IndexesOfSetGetMustMatch(paramName);
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].ParameterType != parametersCached[i].ParameterType)
					{
						throw Error.IndexesOfSetGetMustMatch(paramName);
					}
				}
				return;
			}
			else
			{
				if (getMethod == null)
				{
					throw Error.PropertyDoesNotHaveAccessor(indexer, paramName);
				}
				return;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00030830 File Offset: 0x0002EA30
		private static void ValidateAccessor(Expression instance, MethodInfo method, ParameterInfo[] indexes, ref ReadOnlyCollection<Expression> arguments, string paramName)
		{
			ContractUtils.RequiresNotNull(arguments, "arguments");
			Expression.ValidateMethodInfo(method, "method");
			if ((method.CallingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				throw Error.AccessorsCannotHaveVarArgs(paramName);
			}
			if (method.IsStatic)
			{
				if (instance != null)
				{
					throw Error.OnlyStaticPropertiesHaveNullInstance("instance");
				}
			}
			else
			{
				if (instance == null)
				{
					throw Error.OnlyStaticPropertiesHaveNullInstance("instance");
				}
				ExpressionUtils.RequiresCanRead(instance, "instance");
				Expression.ValidateCallInstanceType(instance.Type, method);
			}
			Expression.ValidateAccessorArgumentTypes(method, indexes, ref arguments, paramName);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000308AC File Offset: 0x0002EAAC
		private static void ValidateAccessorArgumentTypes(MethodInfo method, ParameterInfo[] indexes, ref ReadOnlyCollection<Expression> arguments, string paramName)
		{
			if (indexes.Length != 0)
			{
				if (indexes.Length != arguments.Count)
				{
					throw Error.IncorrectNumberOfMethodCallArguments(method, paramName);
				}
				Expression[] array = null;
				int i = 0;
				int num = indexes.Length;
				while (i < num)
				{
					Expression expression = arguments[i];
					ParameterInfo parameterInfo = indexes[i];
					ExpressionUtils.RequiresCanRead(expression, "arguments", i);
					Type parameterType = parameterInfo.ParameterType;
					if (parameterType.IsByRef)
					{
						throw Error.AccessorsCannotHaveByRefArgs("indexes", i);
					}
					TypeUtils.ValidateType(parameterType, "indexes", i);
					if (!TypeUtils.AreReferenceAssignable(parameterType, expression.Type) && !Expression.TryQuote(parameterType, ref expression))
					{
						throw Error.ExpressionTypeDoesNotMatchMethodParameter(expression.Type, parameterType, method, "arguments", i);
					}
					if (array == null && expression != arguments[i])
					{
						array = new Expression[arguments.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = arguments[j];
						}
					}
					if (array != null)
					{
						array[i] = expression;
					}
					i++;
				}
				if (array != null)
				{
					arguments = new TrueReadOnlyCollection<Expression>(array);
					return;
				}
			}
			else if (arguments.Count > 0)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, paramName);
			}
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x000309BC File Offset: 0x0002EBBC
		internal static InvocationExpression Invoke(Expression expression)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 0, parametersForValidation);
			return new InvocationExpression0(expression, invokeMethod.ReturnType);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000309FC File Offset: 0x0002EBFC
		internal static InvocationExpression Invoke(Expression expression, Expression arg0)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 1, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			return new InvocationExpression1(expression, invokeMethod.ReturnType, arg0);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x00030A54 File Offset: 0x0002EC54
		internal static InvocationExpression Invoke(Expression expression, Expression arg0, Expression arg1)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 2, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			arg1 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg1, parametersForValidation[1], "expression", "arg1");
			return new InvocationExpression2(expression, invokeMethod.ReturnType, arg0, arg1);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00030AC4 File Offset: 0x0002ECC4
		internal static InvocationExpression Invoke(Expression expression, Expression arg0, Expression arg1, Expression arg2)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 3, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			arg1 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg1, parametersForValidation[1], "expression", "arg1");
			arg2 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg2, parametersForValidation[2], "expression", "arg2");
			return new InvocationExpression3(expression, invokeMethod.ReturnType, arg0, arg1, arg2);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x00030B50 File Offset: 0x0002ED50
		internal static InvocationExpression Invoke(Expression expression, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 4, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			arg1 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg1, parametersForValidation[1], "expression", "arg1");
			arg2 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg2, parametersForValidation[2], "expression", "arg2");
			arg3 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg3, parametersForValidation[3], "expression", "arg3");
			return new InvocationExpression4(expression, invokeMethod.ReturnType, arg0, arg1, arg2, arg3);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00030BF4 File Offset: 0x0002EDF4
		internal static InvocationExpression Invoke(Expression expression, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
			ParameterInfo[] parametersForValidation = Expression.GetParametersForValidation(invokeMethod, ExpressionType.Invoke);
			Expression.ValidateArgumentCount(invokeMethod, ExpressionType.Invoke, 5, parametersForValidation);
			arg0 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg0, parametersForValidation[0], "expression", "arg0");
			arg1 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg1, parametersForValidation[1], "expression", "arg1");
			arg2 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg2, parametersForValidation[2], "expression", "arg2");
			arg3 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg3, parametersForValidation[3], "expression", "arg3");
			arg4 = Expression.ValidateOneArgument(invokeMethod, ExpressionType.Invoke, arg4, parametersForValidation[4], "expression", "arg4");
			return new InvocationExpression5(expression, invokeMethod.ReturnType, arg0, arg1, arg2, arg3, arg4);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.InvocationExpression" /> that applies a delegate or lambda expression to a list of argument expressions.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the delegate or lambda expression to be applied.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments that the delegate or lambda expression is applied to.</param>
		/// <returns>An <see cref="T:System.Linq.Expressions.InvocationExpression" /> that applies the specified delegate or lambda expression to the provided arguments.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="expression" />.Type does not represent a delegate type or an <see cref="T:System.Linq.Expressions.Expression`1" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the delegate represented by <paramref name="expression" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="arguments" /> does not contain the same number of elements as the list of parameters for the delegate represented by <paramref name="expression" />.</exception>
		// Token: 0x06000DC5 RID: 3525 RVA: 0x00030CB3 File Offset: 0x0002EEB3
		public static InvocationExpression Invoke(Expression expression, params Expression[] arguments)
		{
			return Expression.Invoke(expression, arguments);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.InvocationExpression" /> that applies a delegate or lambda expression to a list of argument expressions.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the delegate or lambda expression to be applied to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments that the delegate or lambda expression is applied to.</param>
		/// <returns>An <see cref="T:System.Linq.Expressions.InvocationExpression" /> that applies the specified delegate or lambda expression to the provided arguments.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="expression" />.Type does not represent a delegate type or an <see cref="T:System.Linq.Expressions.Expression`1" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the delegate represented by <paramref name="expression" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="arguments" /> does not contain the same number of elements as the list of parameters for the delegate represented by <paramref name="expression" />.</exception>
		// Token: 0x06000DC6 RID: 3526 RVA: 0x00030CBC File Offset: 0x0002EEBC
		public static InvocationExpression Invoke(Expression expression, IEnumerable<Expression> arguments)
		{
			IReadOnlyList<Expression> readOnlyList = (arguments as IReadOnlyList<Expression>) ?? arguments.ToReadOnly<Expression>();
			switch (readOnlyList.Count)
			{
			case 0:
				return Expression.Invoke(expression);
			case 1:
				return Expression.Invoke(expression, readOnlyList[0]);
			case 2:
				return Expression.Invoke(expression, readOnlyList[0], readOnlyList[1]);
			case 3:
				return Expression.Invoke(expression, readOnlyList[0], readOnlyList[1], readOnlyList[2]);
			case 4:
				return Expression.Invoke(expression, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3]);
			case 5:
				return Expression.Invoke(expression, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3], readOnlyList[4]);
			default:
			{
				ExpressionUtils.RequiresCanRead(expression, "expression");
				ReadOnlyCollection<Expression> arguments2 = readOnlyList.ToReadOnly<Expression>();
				MethodInfo invokeMethod = Expression.GetInvokeMethod(expression);
				Expression.ValidateArgumentTypes(invokeMethod, ExpressionType.Invoke, ref arguments2, "expression");
				return new InvocationExpressionN(expression, arguments2, invokeMethod.ReturnType);
			}
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00030DCC File Offset: 0x0002EFCC
		internal static MethodInfo GetInvokeMethod(Expression expression)
		{
			Type delegateType = expression.Type;
			if (!expression.Type.IsSubclassOf(typeof(MulticastDelegate)))
			{
				Type type = TypeUtils.FindGenericType(typeof(Expression<>), expression.Type);
				if (type == null)
				{
					throw Error.ExpressionTypeNotInvocable(expression.Type, "expression");
				}
				delegateType = type.GetGenericArguments()[0];
			}
			return delegateType.GetInvokeMethod();
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelExpression" /> representing a label without a default value.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this <see cref="T:System.Linq.Expressions.LabelExpression" /> will be associated with.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LabelExpression" /> without a default value.</returns>
		// Token: 0x06000DC8 RID: 3528 RVA: 0x00030E34 File Offset: 0x0002F034
		public static LabelExpression Label(LabelTarget target)
		{
			return Expression.Label(target, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelExpression" /> representing a label with the given default value.</summary>
		/// <param name="target">The <see cref="T:System.Linq.Expressions.LabelTarget" /> which this <see cref="T:System.Linq.Expressions.LabelExpression" /> will be associated with.</param>
		/// <param name="defaultValue">The value of this <see cref="T:System.Linq.Expressions.LabelExpression" /> when the label is reached through regular control flow.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LabelExpression" /> with the given default value.</returns>
		// Token: 0x06000DC9 RID: 3529 RVA: 0x00030E3D File Offset: 0x0002F03D
		public static LabelExpression Label(LabelTarget target, Expression defaultValue)
		{
			Expression.ValidateGoto(target, ref defaultValue, "target", "defaultValue", null);
			return new LabelExpression(target, defaultValue);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelTarget" /> representing a label with void type and no name.</summary>
		/// <returns>The new <see cref="T:System.Linq.Expressions.LabelTarget" />.</returns>
		// Token: 0x06000DCA RID: 3530 RVA: 0x00030E59 File Offset: 0x0002F059
		public static LabelTarget Label()
		{
			return Expression.Label(typeof(void), null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelTarget" /> representing a label with void type and the given name.</summary>
		/// <param name="name">The name of the label.</param>
		/// <returns>The new <see cref="T:System.Linq.Expressions.LabelTarget" />.</returns>
		// Token: 0x06000DCB RID: 3531 RVA: 0x00030E6B File Offset: 0x0002F06B
		public static LabelTarget Label(string name)
		{
			return Expression.Label(typeof(void), name);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelTarget" /> representing a label with the given type.</summary>
		/// <param name="type">The type of value that is passed when jumping to the label.</param>
		/// <returns>The new <see cref="T:System.Linq.Expressions.LabelTarget" />.</returns>
		// Token: 0x06000DCC RID: 3532 RVA: 0x00030E7D File Offset: 0x0002F07D
		public static LabelTarget Label(Type type)
		{
			return Expression.Label(type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LabelTarget" /> representing a label with the given type and name.</summary>
		/// <param name="type">The type of value that is passed when jumping to the label.</param>
		/// <param name="name">The name of the label.</param>
		/// <returns>The new <see cref="T:System.Linq.Expressions.LabelTarget" />.</returns>
		// Token: 0x06000DCD RID: 3533 RVA: 0x00030E86 File Offset: 0x0002F086
		public static LabelTarget Label(Type type, string name)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			return new LabelTarget(type, name);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00030EA8 File Offset: 0x0002F0A8
		internal static LambdaExpression CreateLambda(Type delegateType, Expression body, string name, bool tailCall, ReadOnlyCollection<ParameterExpression> parameters)
		{
			CacheDict<Type, Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>> cacheDict = Expression.s_lambdaFactories;
			if (cacheDict == null)
			{
				cacheDict = (Expression.s_lambdaFactories = new CacheDict<Type, Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>>(50));
			}
			Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression> func;
			if (!cacheDict.TryGetValue(delegateType, out func))
			{
				MethodInfo method = typeof(Expression<>).MakeGenericType(new Type[]
				{
					delegateType
				}).GetMethod("Create", BindingFlags.Static | BindingFlags.NonPublic);
				if (delegateType.IsCollectible)
				{
					return (LambdaExpression)method.Invoke(null, new object[]
					{
						body,
						name,
						tailCall,
						parameters
					});
				}
				func = (cacheDict[delegateType] = (Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>)method.CreateDelegate(typeof(Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>)));
			}
			return func(body, name, tailCall, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">A delegate type.</typeparam>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="body" /> is <see langword="null" />.-or-One or more elements in <paramref name="parameters" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="TDelegate" /> is not a delegate type.-or-
		///         <paramref name="body" />.Type represents a type that is not assignable to the return type of <paramref name="TDelegate" />.-or-
		///         <paramref name="parameters" /> does not contain the same number of elements as the list of parameters for <paramref name="TDelegate" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="parameters" /> is not assignable from the type of the corresponding parameter type of <paramref name="TDelegate" />.</exception>
		// Token: 0x06000DCF RID: 3535 RVA: 0x00030F5B File Offset: 0x0002F15B
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, false, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An array that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">The delegate type. </typeparam>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DD0 RID: 3536 RVA: 0x00030F65 File Offset: 0x0002F165
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda<TDelegate>(body, tailCall, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">A delegate type.</typeparam>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="body" /> is <see langword="null" />.-or-One or more elements in <paramref name="parameters" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="TDelegate" /> is not a delegate type.-or-
		///         <paramref name="body" />.Type represents a type that is not assignable to the return type of <paramref name="TDelegate" />.-or-
		///         <paramref name="parameters" /> does not contain the same number of elements as the list of parameters for <paramref name="TDelegate" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="parameters" /> is not assignable from the type of the corresponding parameter type of <paramref name="TDelegate" />.</exception>
		// Token: 0x06000DD1 RID: 3537 RVA: 0x00030F6F File Offset: 0x0002F16F
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, null, false, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">The delegate type. </typeparam>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DD2 RID: 3538 RVA: 0x00030F7A File Offset: 0x0002F17A
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, null, tailCall, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name of the lambda. Used for generating debugging information.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">The delegate type. </typeparam>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DD3 RID: 3539 RVA: 0x00030F85 File Offset: 0x0002F185
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda<TDelegate>(body, name, false, parameters);
		}

		/// <summary>Creates an <see cref="T:System.Linq.Expressions.Expression`1" /> where the delegate type is known at compile time.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name of the lambda. Used for generating debugging info.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <typeparam name="TDelegate">The delegate type. </typeparam>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression`1" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00030F90 File Offset: 0x0002F190
		public static Expression<TDelegate> Lambda<TDelegate>(Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> parameters2 = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(typeof(TDelegate), ref body, parameters2, "TDelegate");
			return (Expression<TDelegate>)Expression.CreateLambda(typeof(TDelegate), body, name, tailCall, parameters2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LambdaExpression" /> by first constructing a delegate type.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="body" /> is <see langword="null" />.-or-One or more elements of <paramref name="parameters" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="parameters" /> contains more than sixteen elements.</exception>
		// Token: 0x06000DD5 RID: 3541 RVA: 0x00030FD3 File Offset: 0x0002F1D3
		public static LambdaExpression Lambda(Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(body, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An array that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DD6 RID: 3542 RVA: 0x00030FDD File Offset: 0x0002F1DD
		public static LambdaExpression Lambda(Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(body, tailCall, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DD7 RID: 3543 RVA: 0x00030FE7 File Offset: 0x0002F1E7
		public static LambdaExpression Lambda(Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, null, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DD8 RID: 3544 RVA: 0x00030FF2 File Offset: 0x0002F1F2
		public static LambdaExpression Lambda(Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, null, tailCall, parameters);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LambdaExpression" /> by first constructing a delegate type. It can be used when the delegate type is not known at compile time.</summary>
		/// <param name="delegateType">A <see cref="T:System.Type" /> that represents a delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>An object that represents a lambda expression which has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="delegateType" /> or <paramref name="body" /> is <see langword="null" />.-or-One or more elements in <paramref name="parameters" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="delegateType" /> does not represent a delegate type.-or-
		///         <paramref name="body" />.Type represents a type that is not assignable to the return type of the delegate type represented by <paramref name="delegateType" />.-or-
		///         <paramref name="parameters" /> does not contain the same number of elements as the list of parameters for the delegate type represented by <paramref name="delegateType" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="parameters" /> is not assignable from the type of the corresponding parameter type of the delegate type represented by <paramref name="delegateType" />.</exception>
		// Token: 0x06000DD9 RID: 3545 RVA: 0x00030FFD File Offset: 0x0002F1FD
		public static LambdaExpression Lambda(Type delegateType, Expression body, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(delegateType, body, null, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="delegateType">A <see cref="P:System.Linq.Expressions.Expression.Type" /> representing the delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An array that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DDA RID: 3546 RVA: 0x00031009 File Offset: 0x0002F209
		public static LambdaExpression Lambda(Type delegateType, Expression body, bool tailCall, params ParameterExpression[] parameters)
		{
			return Expression.Lambda(delegateType, body, null, tailCall, parameters);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LambdaExpression" /> by first constructing a delegate type. It can be used when the delegate type is not known at compile time.</summary>
		/// <param name="delegateType">A <see cref="T:System.Type" /> that represents a delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>An object that represents a lambda expression which has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Lambda" /> and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="delegateType" /> or <paramref name="body" /> is <see langword="null" />.-or-One or more elements in <paramref name="parameters" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="delegateType" /> does not represent a delegate type.-or-
		///         <paramref name="body" />.Type represents a type that is not assignable to the return type of the delegate type represented by <paramref name="delegateType" />.-or-
		///         <paramref name="parameters" /> does not contain the same number of elements as the list of parameters for the delegate type represented by <paramref name="delegateType" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="parameters" /> is not assignable from the type of the corresponding parameter type of the delegate type represented by <paramref name="delegateType" />.</exception>
		// Token: 0x06000DDB RID: 3547 RVA: 0x00030FFD File Offset: 0x0002F1FD
		public static LambdaExpression Lambda(Type delegateType, Expression body, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(delegateType, body, null, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="delegateType">A <see cref="P:System.Linq.Expressions.Expression.Type" /> representing the delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DDC RID: 3548 RVA: 0x00031009 File Offset: 0x0002F209
		public static LambdaExpression Lambda(Type delegateType, Expression body, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(delegateType, body, null, tailCall, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name for the lambda. Used for emitting debug information.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DDD RID: 3549 RVA: 0x00031015 File Offset: 0x0002F215
		public static LambdaExpression Lambda(Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			return Expression.Lambda(body, name, false, parameters);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name for the lambda. Used for emitting debug information.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DDE RID: 3550 RVA: 0x00031020 File Offset: 0x0002F220
		public static LambdaExpression Lambda(Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ContractUtils.RequiresNotNull(body, "body");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = parameters.ToReadOnly<ParameterExpression>();
			int count = readOnlyCollection.Count;
			Type[] array = new Type[count + 1];
			if (count > 0)
			{
				HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
				for (int i = 0; i < count; i++)
				{
					ParameterExpression parameterExpression = readOnlyCollection[i];
					ContractUtils.RequiresNotNull(parameterExpression, "parameter");
					array[i] = (parameterExpression.IsByRef ? parameterExpression.Type.MakeByRefType() : parameterExpression.Type);
					if (!hashSet.Add(parameterExpression))
					{
						throw Error.DuplicateVariable(parameterExpression, "parameters", i);
					}
				}
			}
			array[count] = body.Type;
			return Expression.CreateLambda(DelegateHelpers.MakeDelegateType(array), body, name, tailCall, readOnlyCollection);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="delegateType">A <see cref="P:System.Linq.Expressions.Expression.Type" /> representing the delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to.</param>
		/// <param name="name">The name for the lambda. Used for emitting debug information.</param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DDF RID: 3551 RVA: 0x000310D4 File Offset: 0x0002F2D4
		public static LambdaExpression Lambda(Type delegateType, Expression body, string name, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> parameters2 = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(delegateType, ref body, parameters2, "delegateType");
			return Expression.CreateLambda(delegateType, body, name, false, parameters2);
		}

		/// <summary>Creates a LambdaExpression by first constructing a delegate type.</summary>
		/// <param name="delegateType">A <see cref="P:System.Linq.Expressions.Expression.Type" /> representing the delegate signature for the lambda.</param>
		/// <param name="body">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> property equal to. </param>
		/// <param name="name">The name for the lambda. Used for emitting debug information.</param>
		/// <param name="tailCall">A <see cref="T:System.Boolean" /> that indicates if tail call optimization will be applied when compiling the created expression. </param>
		/// <param name="parameters">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> collection. </param>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that has the <see cref="P:System.Linq.Expressions.LambdaExpression.NodeType" /> property equal to Lambda and the <see cref="P:System.Linq.Expressions.LambdaExpression.Body" /> and <see cref="P:System.Linq.Expressions.LambdaExpression.Parameters" /> properties set to the specified values.</returns>
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00031100 File Offset: 0x0002F300
		public static LambdaExpression Lambda(Type delegateType, Expression body, string name, bool tailCall, IEnumerable<ParameterExpression> parameters)
		{
			ReadOnlyCollection<ParameterExpression> parameters2 = parameters.ToReadOnly<ParameterExpression>();
			Expression.ValidateLambdaArgs(delegateType, ref body, parameters2, "delegateType");
			return Expression.CreateLambda(delegateType, body, name, tailCall, parameters2);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00031130 File Offset: 0x0002F330
		private static void ValidateLambdaArgs(Type delegateType, ref Expression body, ReadOnlyCollection<ParameterExpression> parameters, string paramName)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ExpressionUtils.RequiresCanRead(body, "body");
			if (!typeof(MulticastDelegate).IsAssignableFrom(delegateType) || delegateType == typeof(MulticastDelegate))
			{
				throw Error.LambdaTypeMustBeDerivedFromSystemDelegate(paramName);
			}
			TypeUtils.ValidateType(delegateType, "delegateType", true, true);
			CacheDict<Type, MethodInfo> cacheDict = Expression.s_lambdaDelegateCache;
			MethodInfo invokeMethod;
			if (!cacheDict.TryGetValue(delegateType, out invokeMethod))
			{
				invokeMethod = delegateType.GetInvokeMethod();
				if (!delegateType.IsCollectible)
				{
					cacheDict[delegateType] = invokeMethod;
				}
			}
			ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
			if (parametersCached.Length != 0)
			{
				if (parametersCached.Length != parameters.Count)
				{
					throw Error.IncorrectNumberOfLambdaDeclarationParameters();
				}
				HashSet<ParameterExpression> hashSet = new HashSet<ParameterExpression>();
				int i = 0;
				int num = parametersCached.Length;
				while (i < num)
				{
					ParameterExpression parameterExpression = parameters[i];
					ParameterInfo parameterInfo = parametersCached[i];
					ExpressionUtils.RequiresCanRead(parameterExpression, "parameters", i);
					Type type = parameterInfo.ParameterType;
					if (parameterExpression.IsByRef)
					{
						if (!type.IsByRef)
						{
							throw Error.ParameterExpressionNotValidAsDelegate(parameterExpression.Type.MakeByRefType(), type);
						}
						type = type.GetElementType();
					}
					if (!TypeUtils.AreReferenceAssignable(parameterExpression.Type, type))
					{
						throw Error.ParameterExpressionNotValidAsDelegate(parameterExpression.Type, type);
					}
					if (!hashSet.Add(parameterExpression))
					{
						throw Error.DuplicateVariable(parameterExpression, "parameters", i);
					}
					i++;
				}
			}
			else if (parameters.Count > 0)
			{
				throw Error.IncorrectNumberOfLambdaDeclarationParameters();
			}
			if (invokeMethod.ReturnType != typeof(void) && !TypeUtils.AreReferenceAssignable(invokeMethod.ReturnType, body.Type) && !Expression.TryQuote(invokeMethod.ReturnType, ref body))
			{
				throw Error.ExpressionTypeDoesNotMatchReturn(body.Type, invokeMethod.ReturnType);
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000312DC File Offset: 0x0002F4DC
		private static Expression.TryGetFuncActionArgsResult ValidateTryGetFuncActionArgs(Type[] typeArgs)
		{
			if (typeArgs == null)
			{
				return Expression.TryGetFuncActionArgsResult.ArgumentNull;
			}
			foreach (Type type in typeArgs)
			{
				if (type == null)
				{
					return Expression.TryGetFuncActionArgsResult.ArgumentNull;
				}
				if (type.IsByRef)
				{
					return Expression.TryGetFuncActionArgsResult.ByRef;
				}
				if (type == typeof(void) || type.IsPointer)
				{
					return Expression.TryGetFuncActionArgsResult.PointerOrVoid;
				}
			}
			return Expression.TryGetFuncActionArgsResult.Valid;
		}

		/// <summary>Creates a <see cref="P:System.Linq.Expressions.Expression.Type" /> object that represents a generic System.Func delegate type that has specific type arguments. The last type argument specifies the return type of the created delegate.</summary>
		/// <param name="typeArgs">An array of one to seventeen <see cref="T:System.Type" /> objects that specify the type arguments for the <see langword="System.Func" /> delegate type.</param>
		/// <returns>The type of a System.Func delegate that has the specified type arguments.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="typeArgs" /> contains fewer than one or more than seventeen elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="typeArgs" /> is <see langword="null" />.</exception>
		// Token: 0x06000DE3 RID: 3555 RVA: 0x00031334 File Offset: 0x0002F534
		public static Type GetFuncType(params Type[] typeArgs)
		{
			Expression.TryGetFuncActionArgsResult tryGetFuncActionArgsResult = Expression.ValidateTryGetFuncActionArgs(typeArgs);
			if (tryGetFuncActionArgsResult == Expression.TryGetFuncActionArgsResult.ArgumentNull)
			{
				throw new ArgumentNullException("typeArgs");
			}
			if (tryGetFuncActionArgsResult == Expression.TryGetFuncActionArgsResult.ByRef)
			{
				throw Error.TypeMustNotBeByRef("typeArgs");
			}
			Type funcType = DelegateHelpers.GetFuncType(typeArgs);
			if (funcType == null)
			{
				throw Error.IncorrectNumberOfTypeArgsForFunc("typeArgs");
			}
			return funcType;
		}

		/// <summary>Creates a <see cref="P:System.Linq.Expressions.Expression.Type" /> object that represents a generic System.Func delegate type that has specific type arguments. The last type argument specifies the return type of the created delegate.</summary>
		/// <param name="typeArgs">An array of Type objects that specify the type arguments for the System.Func delegate type.</param>
		/// <param name="funcType">When this method returns, contains the generic System.Func delegate type that has specific type arguments. Contains null if there is no generic System.Func delegate that matches the <paramref name="typeArgs" />.This parameter is passed uninitialized.</param>
		/// <returns>true if generic System.Func delegate type was created for specific <paramref name="typeArgs" />; false otherwise.</returns>
		// Token: 0x06000DE4 RID: 3556 RVA: 0x00031384 File Offset: 0x0002F584
		public static bool TryGetFuncType(Type[] typeArgs, out Type funcType)
		{
			if (Expression.ValidateTryGetFuncActionArgs(typeArgs) == Expression.TryGetFuncActionArgsResult.Valid)
			{
				Type funcType2;
				funcType = (funcType2 = DelegateHelpers.GetFuncType(typeArgs));
				return funcType2 != null;
			}
			funcType = null;
			return false;
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object that represents a generic System.Action delegate type that has specific type arguments.</summary>
		/// <param name="typeArgs">An array of up to sixteen <see cref="T:System.Type" /> objects that specify the type arguments for the <see langword="System.Action" /> delegate type.</param>
		/// <returns>The type of a System.Action delegate that has the specified type arguments.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="typeArgs" /> contains more than sixteen elements.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="typeArgs" /> is <see langword="null" />.</exception>
		// Token: 0x06000DE5 RID: 3557 RVA: 0x000313B0 File Offset: 0x0002F5B0
		public static Type GetActionType(params Type[] typeArgs)
		{
			Expression.TryGetFuncActionArgsResult tryGetFuncActionArgsResult = Expression.ValidateTryGetFuncActionArgs(typeArgs);
			if (tryGetFuncActionArgsResult == Expression.TryGetFuncActionArgsResult.ArgumentNull)
			{
				throw new ArgumentNullException("typeArgs");
			}
			if (tryGetFuncActionArgsResult == Expression.TryGetFuncActionArgsResult.ByRef)
			{
				throw Error.TypeMustNotBeByRef("typeArgs");
			}
			Type actionType = DelegateHelpers.GetActionType(typeArgs);
			if (actionType == null)
			{
				throw Error.IncorrectNumberOfTypeArgsForAction("typeArgs");
			}
			return actionType;
		}

		/// <summary>Creates a <see cref="P:System.Linq.Expressions.Expression.Type" /> object that represents a generic System.Action delegate type that has specific type arguments.</summary>
		/// <param name="typeArgs">An array of Type objects that specify the type arguments for the System.Action delegate type.</param>
		/// <param name="actionType">When this method returns, contains the generic System.Action delegate type that has specific type arguments. Contains null if there is no generic System.Action delegate that matches the <paramref name="typeArgs" />.This parameter is passed uninitialized.</param>
		/// <returns>true if generic System.Action delegate type was created for specific <paramref name="typeArgs" />; false otherwise.</returns>
		// Token: 0x06000DE6 RID: 3558 RVA: 0x00031400 File Offset: 0x0002F600
		public static bool TryGetActionType(Type[] typeArgs, out Type actionType)
		{
			if (Expression.ValidateTryGetFuncActionArgs(typeArgs) == Expression.TryGetFuncActionArgsResult.Valid)
			{
				Type actionType2;
				actionType = (actionType2 = DelegateHelpers.GetActionType(typeArgs));
				return actionType2 != null;
			}
			actionType = null;
			return false;
		}

		/// <summary>Gets a <see cref="P:System.Linq.Expressions.Expression.Type" /> object that represents a generic System.Func or System.Action delegate type that has specific type arguments.</summary>
		/// <param name="typeArgs">The type arguments of the delegate.</param>
		/// <returns>The delegate type.</returns>
		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003142B File Offset: 0x0002F62B
		public static Type GetDelegateType(params Type[] typeArgs)
		{
			ContractUtils.RequiresNotEmpty<Type>(typeArgs, "typeArgs");
			ContractUtils.RequiresNotNullItems<Type>(typeArgs, "typeArgs");
			return DelegateHelpers.MakeDelegateType(typeArgs);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses a method named "Add" to add elements to a collection.</summary>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="newExpression" /> or <paramref name="initializers" /> is <see langword="null" />.-or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no instance method named "Add" (case insensitive) declared in <paramref name="newExpression" />.Type or its base type.-or-The add method on <paramref name="newExpression" />.Type or its base type does not take exactly one argument.-or-The type represented by the <see cref="P:System.Linq.Expressions.Expression.Type" /> property of the first element of <paramref name="initializers" /> is not assignable to the argument type of the add method on <paramref name="newExpression" />.Type or its base type.-or-More than one argument-compatible method named "Add" (case-insensitive) exists on <paramref name="newExpression" />.Type and/or its base type.</exception>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x00031449 File Offset: 0x0002F649
		public static ListInitExpression ListInit(NewExpression newExpression, params Expression[] initializers)
		{
			return Expression.ListInit(newExpression, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses a method named "Add" to add elements to a collection.</summary>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="newExpression" /> or <paramref name="initializers" /> is <see langword="null" />.-or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">There is no instance method named "Add" (case insensitive) declared in <paramref name="newExpression" />.Type or its base type.-or-The add method on <paramref name="newExpression" />.Type or its base type does not take exactly one argument.-or-The type represented by the <see cref="P:System.Linq.Expressions.Expression.Type" /> property of the first element of <paramref name="initializers" /> is not assignable to the argument type of the add method on <paramref name="newExpression" />.Type or its base type.-or-More than one argument-compatible method named "Add" (case-insensitive) exists on <paramref name="newExpression" />.Type and/or its base type.</exception>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x00031454 File Offset: 0x0002F654
		public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<Expression> initializers)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			if (readOnlyCollection.Count == 0)
			{
				return new ListInitExpression(newExpression, EmptyReadOnlyCollection<System.Linq.Expressions.ElementInit>.Instance);
			}
			MethodInfo addMethod = Expression.FindMethod(newExpression.Type, "Add", null, new Expression[]
			{
				readOnlyCollection[0]
			}, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			return Expression.ListInit(newExpression, addMethod, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses a specified method to add elements to a collection.</summary>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="addMethod">A <see cref="T:System.Reflection.MethodInfo" /> that represents an instance method that takes one argument, that adds an element to a collection.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="newExpression" /> or <paramref name="initializers" /> is <see langword="null" />.-or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.-or-
		///         <paramref name="addMethod" /> is not <see langword="null" /> and it does not represent an instance method named "Add" (case insensitive) that takes exactly one argument.-or-
		///         <paramref name="addMethod" /> is not <see langword="null" /> and the type represented by the <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="initializers" /> is not assignable to the argument type of the method that <paramref name="addMethod" /> represents.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="addMethod" /> is <see langword="null" /> and no instance method named "Add" that takes one type-compatible argument exists on <paramref name="newExpression" />.Type or its base type.</exception>
		// Token: 0x06000DEA RID: 3562 RVA: 0x000314BE File Offset: 0x0002F6BE
		public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, params Expression[] initializers)
		{
			return Expression.ListInit(newExpression, addMethod, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses a specified method to add elements to a collection.</summary>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="addMethod">A <see cref="T:System.Reflection.MethodInfo" /> that represents an instance method named "Add" (case insensitive), that adds an element to a collection.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="newExpression" /> or <paramref name="initializers" /> is <see langword="null" />.-or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.-or-
		///         <paramref name="addMethod" /> is not <see langword="null" /> and it does not represent an instance method named "Add" (case insensitive) that takes exactly one argument.-or-
		///         <paramref name="addMethod" /> is not <see langword="null" /> and the type represented by the <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="initializers" /> is not assignable to the argument type of the method that <paramref name="addMethod" /> represents.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="addMethod" /> is <see langword="null" /> and no instance method named "Add" that takes one type-compatible argument exists on <paramref name="newExpression" />.Type or its base type.</exception>
		// Token: 0x06000DEB RID: 3563 RVA: 0x000314C8 File Offset: 0x0002F6C8
		public static ListInitExpression ListInit(NewExpression newExpression, MethodInfo addMethod, IEnumerable<Expression> initializers)
		{
			if (addMethod == null)
			{
				return Expression.ListInit(newExpression, initializers);
			}
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			ElementInit[] array = new ElementInit[readOnlyCollection.Count];
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				array[i] = Expression.ElementInit(addMethod, new Expression[]
				{
					readOnlyCollection[i]
				});
			}
			return Expression.ListInit(newExpression, new TrueReadOnlyCollection<ElementInit>(array));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses specified <see cref="T:System.Linq.Expressions.ElementInit" /> objects to initialize a collection.</summary>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> and <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="newExpression" /> or <paramref name="initializers" /> is <see langword="null" />.-or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000DEC RID: 3564 RVA: 0x00031545 File Offset: 0x0002F745
		public static ListInitExpression ListInit(NewExpression newExpression, params ElementInit[] initializers)
		{
			return Expression.ListInit(newExpression, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ListInitExpression" /> that uses specified <see cref="T:System.Linq.Expressions.ElementInit" /> objects to initialize a collection.</summary>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ListInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ListInit" /> and the <see cref="P:System.Linq.Expressions.ListInitExpression.NewExpression" /> and <see cref="P:System.Linq.Expressions.ListInitExpression.Initializers" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="newExpression" /> or <paramref name="initializers" /> is <see langword="null" />.-or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="newExpression" />.Type does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000DED RID: 3565 RVA: 0x00031550 File Offset: 0x0002F750
		public static ListInitExpression ListInit(NewExpression newExpression, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			ReadOnlyCollection<ElementInit> initializers2 = initializers.ToReadOnly<ElementInit>();
			Expression.ValidateListInitArgs(newExpression.Type, initializers2, "newExpression");
			return new ListInitExpression(newExpression, initializers2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LoopExpression" /> with the given body.</summary>
		/// <param name="body">The body of the loop.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.LoopExpression" />.</returns>
		// Token: 0x06000DEE RID: 3566 RVA: 0x00031592 File Offset: 0x0002F792
		public static LoopExpression Loop(Expression body)
		{
			return Expression.Loop(body, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LoopExpression" /> with the given body and break target.</summary>
		/// <param name="body">The body of the loop.</param>
		/// <param name="break">The break target used by the loop body.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.LoopExpression" />.</returns>
		// Token: 0x06000DEF RID: 3567 RVA: 0x0003159B File Offset: 0x0002F79B
		public static LoopExpression Loop(Expression body, LabelTarget @break)
		{
			return Expression.Loop(body, @break, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.LoopExpression" /> with the given body.</summary>
		/// <param name="body">The body of the loop.</param>
		/// <param name="break">The break target used by the loop body.</param>
		/// <param name="continue">The continue target used by the loop body.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.LoopExpression" />.</returns>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x000315A5 File Offset: 0x0002F7A5
		public static LoopExpression Loop(Expression body, LabelTarget @break, LabelTarget @continue)
		{
			ExpressionUtils.RequiresCanRead(body, "body");
			if (@continue != null && @continue.Type != typeof(void))
			{
				throw Error.LabelTypeMustBeVoid("continue");
			}
			return new LoopExpression(body, @break, @continue);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberAssignment" /> that represents the initialization of a field or property.</summary>
		/// <param name="member">A <see cref="T:System.Reflection.MemberInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberAssignment" /> that has <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> equal to <see cref="F:System.Linq.Expressions.MemberBindingType.Assignment" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="member" /> or <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="member" /> does not represent a field or property.-or-The property represented by <paramref name="member" /> does not have a <see langword="set" /> accessor.-or-
		///         <paramref name="expression" />.Type is not assignable to the type of the field or property that <paramref name="member" /> represents.</exception>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x000315E0 File Offset: 0x0002F7E0
		public static MemberAssignment Bind(MemberInfo member, Expression expression)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ExpressionUtils.RequiresCanRead(expression, "expression");
			Type type;
			Expression.ValidateSettableFieldOrPropertyMember(member, out type);
			if (!type.IsAssignableFrom(expression.Type))
			{
				throw Error.ArgumentTypesMustMatch();
			}
			return new MemberAssignment(member, expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberAssignment" /> that represents the initialization of a member by using a property accessor method.</summary>
		/// <param name="propertyAccessor">A <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberAssignment" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.Assignment" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and the <see cref="P:System.Linq.Expressions.MemberAssignment.Expression" /> property set to <paramref name="expression" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="propertyAccessor" /> or <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The property accessed by <paramref name="propertyAccessor" /> does not have a <see langword="set" /> accessor.-or-
		///         <paramref name="expression" />.Type is not assignable to the type of the field or property that <paramref name="member" /> represents.</exception>
		// Token: 0x06000DF2 RID: 3570 RVA: 0x00031626 File Offset: 0x0002F826
		public static MemberAssignment Bind(MethodInfo propertyAccessor, Expression expression)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			ContractUtils.RequiresNotNull(expression, "expression");
			Expression.ValidateMethodInfo(propertyAccessor, "propertyAccessor");
			return Expression.Bind(Expression.GetProperty(propertyAccessor, "propertyAccessor", -1), expression);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003165C File Offset: 0x0002F85C
		private static void ValidateSettableFieldOrPropertyMember(MemberInfo member, out Type memberType)
		{
			Type declaringType = member.DeclaringType;
			if (declaringType == null)
			{
				throw Error.NotAMemberOfAnyType(member, "member");
			}
			TypeUtils.ValidateType(declaringType, null);
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo == null)
			{
				FieldInfo fieldInfo = member as FieldInfo;
				if (fieldInfo == null)
				{
					throw Error.ArgumentMustBeFieldInfoOrPropertyInfo("member");
				}
				memberType = fieldInfo.FieldType;
				return;
			}
			else
			{
				if (!propertyInfo.CanWrite)
				{
					throw Error.PropertyDoesNotHaveSetter(propertyInfo, "member");
				}
				memberType = propertyInfo.PropertyType;
				return;
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a field.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property equal to. For <see langword="static" /> (<see langword="Shared" /> in Visual Basic), <paramref name="expression" /> must be <see langword="null" />.</param>
		/// <param name="field">The <see cref="T:System.Reflection.FieldInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" /> and the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> and <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="field" /> is <see langword="null" />.-or-The field represented by <paramref name="field" /> is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic) and <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="expression" />.Type is not assignable to the declaring type of the field represented by <paramref name="field" />.</exception>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x000316D0 File Offset: 0x0002F8D0
		public static MemberExpression Field(Expression expression, FieldInfo field)
		{
			ContractUtils.RequiresNotNull(field, "field");
			if (field.IsStatic)
			{
				if (expression != null)
				{
					throw Error.OnlyStaticFieldsHaveNullInstance("expression");
				}
			}
			else
			{
				if (expression == null)
				{
					throw Error.OnlyStaticFieldsHaveNullInstance("field");
				}
				ExpressionUtils.RequiresCanRead(expression, "expression");
				if (!TypeUtils.AreReferenceAssignable(field.DeclaringType, expression.Type))
				{
					throw Error.FieldInfoNotDefinedForType(field.DeclaringType, field.Name, expression.Type);
				}
			}
			return MemberExpression.Make(expression, field);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a field given the name of the field.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> whose <see cref="P:System.Linq.Expressions.Expression.Type" /> contains a field named <paramref name="fieldName" />. This can be null for static fields.</param>
		/// <param name="fieldName">The name of a field to be accessed.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" />, the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property set to <paramref name="expression" />, and the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property set to the <see cref="T:System.Reflection.FieldInfo" /> that represents the field denoted by <paramref name="fieldName" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="fieldName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">No field named <paramref name="fieldName" /> is defined in <paramref name="expression" />.Type or its base types.</exception>
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003174C File Offset: 0x0002F94C
		public static MemberExpression Field(Expression expression, string fieldName)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(fieldName, "fieldName");
			FieldInfo fieldInfo = expression.Type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? expression.Type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (fieldInfo == null)
			{
				throw Error.InstanceFieldNotDefinedForType(fieldName, expression.Type);
			}
			return Expression.Field(expression, fieldInfo);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a field.</summary>
		/// <param name="expression">The containing object of the field. This can be null for static fields.</param>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> that contains the field.</param>
		/// <param name="fieldName">The field to be accessed.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.MemberExpression" />.</returns>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x000317B0 File Offset: 0x0002F9B0
		public static MemberExpression Field(Expression expression, Type type, string fieldName)
		{
			ContractUtils.RequiresNotNull(type, "type");
			FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? type.GetField(fieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (fieldInfo == null)
			{
				throw Error.FieldNotDefinedForType(fieldName, type);
			}
			return Expression.Field(expression, fieldInfo);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a property.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> whose <see cref="P:System.Linq.Expressions.Expression.Type" /> contains a property named <paramref name="propertyName" />. This can be <see langword="null" /> for static properties.</param>
		/// <param name="propertyName">The name of a property to be accessed.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" />, the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property set to <paramref name="expression" />, and the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property denoted by <paramref name="propertyName" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="propertyName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">No property named <paramref name="propertyName" /> is defined in <paramref name="expression" />.Type or its base types.</exception>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x000317F8 File Offset: 0x0002F9F8
		public static MemberExpression Property(Expression expression, string propertyName)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(propertyName, "propertyName");
			PropertyInfo propertyInfo = expression.Type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? expression.Type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (propertyInfo == null)
			{
				throw Error.InstancePropertyNotDefinedForType(propertyName, expression.Type, "propertyName");
			}
			return Expression.Property(expression, propertyInfo);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> accessing a property.</summary>
		/// <param name="expression">The containing object of the property. This can be null for static properties.</param>
		/// <param name="type">The <see cref="P:System.Linq.Expressions.Expression.Type" /> that contains the property.</param>
		/// <param name="propertyName">The property to be accessed.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.MemberExpression" />.</returns>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x00031860 File Offset: 0x0002FA60
		public static MemberExpression Property(Expression expression, Type type, string propertyName)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(propertyName, "propertyName");
			PropertyInfo propertyInfo = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy) ?? type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (propertyInfo == null)
			{
				throw Error.PropertyNotDefinedForType(propertyName, type, "propertyName");
			}
			return Expression.Property(expression, propertyInfo);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a property.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property equal to. This can be null for static properties.</param>
		/// <param name="property">The <see cref="T:System.Reflection.PropertyInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" /> and the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> and <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="property" /> is <see langword="null" />.-or-The property that <paramref name="property" /> represents is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic) and <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="expression" />.Type is not assignable to the declaring type of the property that <paramref name="property" /> represents.</exception>
		// Token: 0x06000DF9 RID: 3577 RVA: 0x000318B8 File Offset: 0x0002FAB8
		public static MemberExpression Property(Expression expression, PropertyInfo property)
		{
			ContractUtils.RequiresNotNull(property, "property");
			MethodInfo methodInfo = property.GetGetMethod(true);
			if (methodInfo == null)
			{
				methodInfo = property.GetSetMethod(true);
				if (methodInfo == null)
				{
					throw Error.PropertyDoesNotHaveAccessor(property, "property");
				}
				if (methodInfo.GetParametersCached().Length != 1)
				{
					throw Error.IncorrectNumberOfMethodCallArguments(methodInfo, "property");
				}
			}
			else if (methodInfo.GetParametersCached().Length != 0)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(methodInfo, "property");
			}
			if (methodInfo.IsStatic)
			{
				if (expression != null)
				{
					throw Error.OnlyStaticPropertiesHaveNullInstance("expression");
				}
			}
			else
			{
				if (expression == null)
				{
					throw Error.OnlyStaticPropertiesHaveNullInstance("property");
				}
				ExpressionUtils.RequiresCanRead(expression, "expression");
				if (!TypeUtils.IsValidInstanceType(property, expression.Type))
				{
					throw Error.PropertyNotDefinedForType(property, expression.Type, "property");
				}
			}
			Expression.ValidateMethodInfo(methodInfo, "property");
			return MemberExpression.Make(expression, property);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a property by using a property accessor method.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property equal to. This can be null for static properties.</param>
		/// <param name="propertyAccessor">The <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" />, the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property set to <paramref name="expression" /> and the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="propertyAccessor" /> is <see langword="null" />.-or-The method that <paramref name="propertyAccessor" /> represents is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic) and <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="expression" />.Type is not assignable to the declaring type of the method represented by <paramref name="propertyAccessor" />.-or-The method that <paramref name="propertyAccessor" /> represents is not a property accessor method.</exception>
		// Token: 0x06000DFA RID: 3578 RVA: 0x0003198B File Offset: 0x0002FB8B
		public static MemberExpression Property(Expression expression, MethodInfo propertyAccessor)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			Expression.ValidateMethodInfo(propertyAccessor, "propertyAccessor");
			return Expression.Property(expression, Expression.GetProperty(propertyAccessor, "propertyAccessor", -1));
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x000319B8 File Offset: 0x0002FBB8
		private static PropertyInfo GetProperty(MethodInfo mi, string paramName, int index = -1)
		{
			Type declaringType = mi.DeclaringType;
			if (declaringType != null)
			{
				BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
				bindingFlags |= (mi.IsStatic ? BindingFlags.Static : BindingFlags.Instance);
				foreach (PropertyInfo propertyInfo in declaringType.GetProperties(bindingFlags))
				{
					if (propertyInfo.CanRead && Expression.CheckMethod(mi, propertyInfo.GetGetMethod(true)))
					{
						return propertyInfo;
					}
					if (propertyInfo.CanWrite && Expression.CheckMethod(mi, propertyInfo.GetSetMethod(true)))
					{
						return propertyInfo;
					}
				}
			}
			throw Error.MethodNotPropertyAccessor(mi.DeclaringType, mi.Name, paramName, index);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00031A50 File Offset: 0x0002FC50
		private static bool CheckMethod(MethodInfo method, MethodInfo propertyMethod)
		{
			if (method.Equals(propertyMethod))
			{
				return true;
			}
			Type declaringType = method.DeclaringType;
			return declaringType.IsInterface && method.Name == propertyMethod.Name && declaringType.GetMethod(method.Name) == propertyMethod;
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing a property or field.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> whose <see cref="P:System.Linq.Expressions.Expression.Type" /> contains a property or field named <paramref name="propertyOrFieldName" />. This can be null for static members.</param>
		/// <param name="propertyOrFieldName">The name of a property or field to be accessed.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberAccess" />, the <see cref="P:System.Linq.Expressions.MemberExpression.Expression" /> property set to <paramref name="expression" />, and the <see cref="P:System.Linq.Expressions.MemberExpression.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> or <see cref="T:System.Reflection.FieldInfo" /> that represents the property or field denoted by <paramref name="propertyOrFieldName" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="propertyOrFieldName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">No property or field named <paramref name="propertyOrFieldName" /> is defined in <paramref name="expression" />.Type or its base types.</exception>
		// Token: 0x06000DFD RID: 3581 RVA: 0x00031AA4 File Offset: 0x0002FCA4
		public static MemberExpression PropertyOrField(Expression expression, string propertyOrFieldName)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			PropertyInfo property = expression.Type.GetProperty(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (property != null)
			{
				return Expression.Property(expression, property);
			}
			FieldInfo field = expression.Type.GetField(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (field != null)
			{
				return Expression.Field(expression, field);
			}
			property = expression.Type.GetProperty(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (property != null)
			{
				return Expression.Property(expression, property);
			}
			field = expression.Type.GetField(propertyOrFieldName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
			if (field != null)
			{
				return Expression.Field(expression, field);
			}
			throw Error.NotAMemberOfType(propertyOrFieldName, expression.Type, "propertyOrFieldName");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberExpression" /> that represents accessing either a field or a property.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the object that the member belongs to. This can be null for static members.</param>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> that describes the field or property to be accessed.</param>
		/// <returns>The <see cref="T:System.Linq.Expressions.MemberExpression" /> that results from calling the appropriate factory method.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="member" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="member" /> does not represent a field or property.</exception>
		// Token: 0x06000DFE RID: 3582 RVA: 0x00031B50 File Offset: 0x0002FD50
		public static MemberExpression MakeMemberAccess(Expression expression, MemberInfo member)
		{
			ContractUtils.RequiresNotNull(member, "member");
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return Expression.Field(expression, fieldInfo);
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return Expression.Property(expression, propertyInfo);
			}
			throw Error.MemberNotFieldOrProperty(member, "member");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberInitExpression" />.</summary>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="bindings">An array of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberInit" /> and the <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> and <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="newExpression" /> or <paramref name="bindings" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type that <paramref name="newExpression" />.Type represents.</exception>
		// Token: 0x06000DFF RID: 3583 RVA: 0x00031BA3 File Offset: 0x0002FDA3
		public static MemberInitExpression MemberInit(NewExpression newExpression, params MemberBinding[] bindings)
		{
			return Expression.MemberInit(newExpression, bindings);
		}

		/// <summary>Represents an expression that creates a new object and initializes a property of the object.</summary>
		/// <param name="newExpression">A <see cref="T:System.Linq.Expressions.NewExpression" /> to set the <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> property equal to.</param>
		/// <param name="bindings">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberInitExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.MemberInit" /> and the <see cref="P:System.Linq.Expressions.MemberInitExpression.NewExpression" /> and <see cref="P:System.Linq.Expressions.MemberInitExpression.Bindings" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="newExpression" /> or <paramref name="bindings" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type that <paramref name="newExpression" />.Type represents.</exception>
		// Token: 0x06000E00 RID: 3584 RVA: 0x00031BAC File Offset: 0x0002FDAC
		public static MemberInitExpression MemberInit(NewExpression newExpression, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(newExpression, "newExpression");
			ContractUtils.RequiresNotNull(bindings, "bindings");
			ReadOnlyCollection<MemberBinding> bindings2 = bindings.ToReadOnly<MemberBinding>();
			Expression.ValidateMemberInitArgs(newExpression.Type, bindings2);
			return new MemberInitExpression(newExpression, bindings2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberListBinding" /> where the member is a field or property.</summary>
		/// <param name="member">A <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.ListBinding" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="member" /> is <see langword="null" />. -or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Reflection.FieldInfo.FieldType" /> or <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the field or property that <paramref name="member" /> represents does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000E01 RID: 3585 RVA: 0x00031BE9 File Offset: 0x0002FDE9
		public static MemberListBinding ListBind(MemberInfo member, params ElementInit[] initializers)
		{
			return Expression.ListBind(member, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberListBinding" /> where the member is a field or property.</summary>
		/// <param name="member">A <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.ListBinding" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="member" /> is <see langword="null" />. -or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Reflection.FieldInfo.FieldType" /> or <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the field or property that <paramref name="member" /> represents does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000E02 RID: 3586 RVA: 0x00031BF4 File Offset: 0x0002FDF4
		public static MemberListBinding ListBind(MemberInfo member, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			Type listType;
			Expression.ValidateGettableFieldOrPropertyMember(member, out listType);
			ReadOnlyCollection<ElementInit> initializers2 = initializers.ToReadOnly<ElementInit>();
			Expression.ValidateListInitArgs(listType, initializers2, "member");
			return new MemberListBinding(member, initializers2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberListBinding" /> object based on a specified property accessor method.</summary>
		/// <param name="propertyAccessor">A <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.ListBinding" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.MemberInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> populated with the elements of <paramref name="initializers" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="propertyAccessor" /> is <see langword="null" />. -or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the property that the method represented by <paramref name="propertyAccessor" /> accesses does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000E03 RID: 3587 RVA: 0x00031C39 File Offset: 0x0002FE39
		public static MemberListBinding ListBind(MethodInfo propertyAccessor, params ElementInit[] initializers)
		{
			return Expression.ListBind(propertyAccessor, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberListBinding" /> based on a specified property accessor method.</summary>
		/// <param name="propertyAccessor">A <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.ElementInit" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberListBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.ListBinding" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.MemberInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:System.Linq.Expressions.MemberListBinding.Initializers" /> populated with the elements of <paramref name="initializers" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="propertyAccessor" /> is <see langword="null" />. -or-One or more elements of <paramref name="initializers" /> are <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Reflection.PropertyInfo.PropertyType" /> of the property that the method represented by <paramref name="propertyAccessor" /> accesses does not implement <see cref="T:System.Collections.IEnumerable" />.</exception>
		// Token: 0x06000E04 RID: 3588 RVA: 0x00031C42 File Offset: 0x0002FE42
		public static MemberListBinding ListBind(MethodInfo propertyAccessor, IEnumerable<ElementInit> initializers)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			return Expression.ListBind(Expression.GetProperty(propertyAccessor, "propertyAccessor", -1), initializers);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00031C6C File Offset: 0x0002FE6C
		private static void ValidateListInitArgs(Type listType, ReadOnlyCollection<ElementInit> initializers, string listTypeParamName)
		{
			if (!typeof(IEnumerable).IsAssignableFrom(listType))
			{
				throw Error.TypeNotIEnumerable(listType, listTypeParamName);
			}
			int i = 0;
			int count = initializers.Count;
			while (i < count)
			{
				ElementInit elementInit = initializers[i];
				ContractUtils.RequiresNotNull(elementInit, "initializers", i);
				Expression.ValidateCallInstanceType(listType, elementInit.AddMethod);
				i++;
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that represents the recursive initialization of members of a field or property.</summary>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="bindings">An array of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.MemberBinding" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="member" /> or <paramref name="bindings" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type of the field or property that <paramref name="member" /> represents.</exception>
		// Token: 0x06000E06 RID: 3590 RVA: 0x00031CC6 File Offset: 0x0002FEC6
		public static MemberMemberBinding MemberBind(MemberInfo member, params MemberBinding[] bindings)
		{
			return Expression.MemberBind(member, bindings);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that represents the recursive initialization of members of a field or property.</summary>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> to set the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property equal to.</param>
		/// <param name="bindings">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.MemberBinding" /> and the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> and <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="member" /> or <paramref name="bindings" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="member" /> does not represent a field or property.-or-The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type of the field or property that <paramref name="member" /> represents.</exception>
		// Token: 0x06000E07 RID: 3591 RVA: 0x00031CD0 File Offset: 0x0002FED0
		public static MemberMemberBinding MemberBind(MemberInfo member, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(member, "member");
			ContractUtils.RequiresNotNull(bindings, "bindings");
			ReadOnlyCollection<MemberBinding> bindings2 = bindings.ToReadOnly<MemberBinding>();
			Type type;
			Expression.ValidateGettableFieldOrPropertyMember(member, out type);
			Expression.ValidateMemberInitArgs(type, bindings2);
			return new MemberMemberBinding(member, bindings2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that represents the recursive initialization of members of a member that is accessed by using a property accessor method.</summary>
		/// <param name="propertyAccessor">The <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="bindings">An array of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.MemberBinding" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="propertyAccessor" /> or <paramref name="bindings" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type of the property accessed by the method that <paramref name="propertyAccessor" /> represents.</exception>
		// Token: 0x06000E08 RID: 3592 RVA: 0x00031D10 File Offset: 0x0002FF10
		public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, params MemberBinding[] bindings)
		{
			return Expression.MemberBind(propertyAccessor, bindings);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that represents the recursive initialization of members of a member that is accessed by using a property accessor method.</summary>
		/// <param name="propertyAccessor">The <see cref="T:System.Reflection.MethodInfo" /> that represents a property accessor method.</param>
		/// <param name="bindings">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.MemberBinding" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MemberMemberBinding" /> that has the <see cref="P:System.Linq.Expressions.MemberBinding.BindingType" /> property equal to <see cref="F:System.Linq.Expressions.MemberBindingType.MemberBinding" />, the <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property set to the <see cref="T:System.Reflection.PropertyInfo" /> that represents the property accessed in <paramref name="propertyAccessor" />, and <see cref="P:System.Linq.Expressions.MemberMemberBinding.Bindings" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="propertyAccessor" /> or <paramref name="bindings" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="propertyAccessor" /> does not represent a property accessor method.-or-The <see cref="P:System.Linq.Expressions.MemberBinding.Member" /> property of an element of <paramref name="bindings" /> does not represent a member of the type of the property accessed by the method that <paramref name="propertyAccessor" /> represents.</exception>
		// Token: 0x06000E09 RID: 3593 RVA: 0x00031D19 File Offset: 0x0002FF19
		public static MemberMemberBinding MemberBind(MethodInfo propertyAccessor, IEnumerable<MemberBinding> bindings)
		{
			ContractUtils.RequiresNotNull(propertyAccessor, "propertyAccessor");
			return Expression.MemberBind(Expression.GetProperty(propertyAccessor, "propertyAccessor", -1), bindings);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00031D38 File Offset: 0x0002FF38
		private static void ValidateGettableFieldOrPropertyMember(MemberInfo member, out Type memberType)
		{
			Type declaringType = member.DeclaringType;
			if (declaringType == null)
			{
				throw Error.NotAMemberOfAnyType(member, "member");
			}
			TypeUtils.ValidateType(declaringType, null, true, true);
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo == null)
			{
				FieldInfo fieldInfo = member as FieldInfo;
				if (fieldInfo == null)
				{
					throw Error.ArgumentMustBeFieldInfoOrPropertyInfo("member");
				}
				memberType = fieldInfo.FieldType;
				return;
			}
			else
			{
				if (!propertyInfo.CanRead)
				{
					throw Error.PropertyDoesNotHaveGetter(propertyInfo, "member");
				}
				memberType = propertyInfo.PropertyType;
				return;
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		private static void ValidateMemberInitArgs(Type type, ReadOnlyCollection<MemberBinding> bindings)
		{
			int i = 0;
			int count = bindings.Count;
			while (i < count)
			{
				MemberBinding memberBinding = bindings[i];
				ContractUtils.RequiresNotNull(memberBinding, "bindings");
				memberBinding.ValidateAsDefinedHere(i);
				if (!memberBinding.Member.DeclaringType.IsAssignableFrom(type))
				{
					throw Error.NotAMemberOfType(memberBinding.Member.Name, type, "bindings", i);
				}
				i++;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00031E18 File Offset: 0x00030018
		internal static MethodCallExpression Call(MethodInfo method)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ParameterInfo[] pis = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 0, pis);
			return new MethodCallExpression0(method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method that takes one argument.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is null.</exception>
		// Token: 0x06000E0D RID: 3597 RVA: 0x00031E48 File Offset: 0x00030048
		public static MethodCallExpression Call(MethodInfo method, Expression arg0)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 1, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			return new MethodCallExpression1(method, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static method that takes two arguments.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is null.</exception>
		// Token: 0x06000E0E RID: 3598 RVA: 0x00031E9C File Offset: 0x0003009C
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 2, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			return new MethodCallExpression2(method, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static method that takes three arguments.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <param name="arg2">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the third argument.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is null.</exception>
		// Token: 0x06000E0F RID: 3599 RVA: 0x00031F14 File Offset: 0x00030114
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 3, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2], "method", "arg2");
			return new MethodCallExpression3(method, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static method that takes four arguments.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <param name="arg2">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the third argument.</param>
		/// <param name="arg3">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the fourth argument.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is null.</exception>
		// Token: 0x06000E10 RID: 3600 RVA: 0x00031FAC File Offset: 0x000301AC
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ContractUtils.RequiresNotNull(arg3, "arg3");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 4, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2], "method", "arg2");
			arg3 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg3, array[3], "method", "arg3");
			return new MethodCallExpression4(method, arg0, arg1, arg2, arg3);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static method that takes five arguments.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <param name="arg2">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the third argument.</param>
		/// <param name="arg3">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the fourth argument.</param>
		/// <param name="arg4">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the fifth argument.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is null.</exception>
		// Token: 0x06000E11 RID: 3601 RVA: 0x0003206C File Offset: 0x0003026C
		public static MethodCallExpression Call(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ContractUtils.RequiresNotNull(arg3, "arg3");
			ContractUtils.RequiresNotNull(arg4, "arg4");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(null, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 5, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2], "method", "arg2");
			arg3 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg3, array[3], "method", "arg3");
			arg4 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg4, array[4], "method", "arg4");
			return new MethodCallExpression5(method, arg0, arg1, arg2, arg3, arg4);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method that has arguments.</summary>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents a <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in <paramref name="arguments" /> does not equal the number of parameters for the method represented by <paramref name="method" />.-or-One or more of the elements of <paramref name="arguments" /> is not assignable to the corresponding parameter for the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000E12 RID: 3602 RVA: 0x00032150 File Offset: 0x00030350
		public static MethodCallExpression Call(MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(null, method, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a static (Shared in Visual Basic) method.</summary>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> that represents the target method.</param>
		/// <param name="arguments">A collection of <see cref="T:System.Linq.Expressions.Expression" /> that represents the call arguments.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000E13 RID: 3603 RVA: 0x0003215A File Offset: 0x0003035A
		public static MethodCallExpression Call(MethodInfo method, IEnumerable<Expression> arguments)
		{
			return Expression.Call(null, method, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes no arguments.</summary>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> that specifies the instance for an instance method call (pass <see langword="null" /> for a <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method).</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is <see langword="null" />.-or-
		///         <paramref name="instance" /> is <see langword="null" /> and <paramref name="method" /> represents an instance method.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="instance" />.Type is not assignable to the declaring type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000E14 RID: 3604 RVA: 0x00032164 File Offset: 0x00030364
		public static MethodCallExpression Call(Expression instance, MethodInfo method)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ParameterInfo[] pis = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 0, pis);
			if (instance != null)
			{
				return new InstanceMethodCallExpression0(method, instance);
			}
			return new MethodCallExpression0(method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes arguments.</summary>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> that specifies the instance for an instance method call (pass <see langword="null" /> for a <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method).</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" />, <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" />, and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is <see langword="null" />.-or-
		///         <paramref name="instance" /> is <see langword="null" /> and <paramref name="method" /> represents an instance method.-or-
		///         <paramref name="arguments" /> is not <see langword="null" /> and one or more of its elements is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="instance" />.Type is not assignable to the declaring type of the method represented by <paramref name="method" />.-or-The number of elements in <paramref name="arguments" /> does not equal the number of parameters for the method represented by <paramref name="method" />.-or-One or more of the elements of <paramref name="arguments" /> is not assignable to the corresponding parameter for the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000E15 RID: 3605 RVA: 0x0003219E File Offset: 0x0003039E
		public static MethodCallExpression Call(Expression instance, MethodInfo method, params Expression[] arguments)
		{
			return Expression.Call(instance, method, arguments);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000321A8 File Offset: 0x000303A8
		internal static MethodCallExpression Call(Expression instance, MethodInfo method, Expression arg0)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 1, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			if (instance != null)
			{
				return new InstanceMethodCallExpression1(method, instance, arg0);
			}
			return new MethodCallExpression1(method, arg0);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes two arguments.</summary>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> that specifies the instance for an instance call. (pass null for a static (Shared in Visual Basic) method).</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> that represents the target method.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000E17 RID: 3607 RVA: 0x00032208 File Offset: 0x00030408
		public static MethodCallExpression Call(Expression instance, MethodInfo method, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 2, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			if (instance != null)
			{
				return new InstanceMethodCallExpression2(method, instance, arg0, arg1);
			}
			return new MethodCallExpression2(method, arg0, arg1);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes three arguments.</summary>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> that specifies the instance for an instance call. (pass null for a static (Shared in Visual Basic) method).</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> that represents the target method.</param>
		/// <param name="arg0">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the first argument.</param>
		/// <param name="arg1">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the second argument.</param>
		/// <param name="arg2">The <see cref="T:System.Linq.Expressions.Expression" /> that represents the third argument.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> properties set to the specified values.</returns>
		// Token: 0x06000E18 RID: 3608 RVA: 0x0003228C File Offset: 0x0003048C
		public static MethodCallExpression Call(Expression instance, MethodInfo method, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(method, "method");
			ContractUtils.RequiresNotNull(arg0, "arg0");
			ContractUtils.RequiresNotNull(arg1, "arg1");
			ContractUtils.RequiresNotNull(arg2, "arg2");
			ParameterInfo[] array = Expression.ValidateMethodAndGetParameters(instance, method);
			Expression.ValidateArgumentCount(method, ExpressionType.Call, 3, array);
			arg0 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg0, array[0], "method", "arg0");
			arg1 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg1, array[1], "method", "arg1");
			arg2 = Expression.ValidateOneArgument(method, ExpressionType.Call, arg2, array[2], "method", "arg2");
			if (instance != null)
			{
				return new InstanceMethodCallExpression3(method, instance, arg0, arg1, arg2);
			}
			return new MethodCallExpression3(method, arg0, arg1, arg2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method by calling the appropriate factory method.</summary>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> whose <see cref="P:System.Linq.Expressions.Expression.Type" /> property value will be searched for a specific method.</param>
		/// <param name="methodName">The name of the method.</param>
		/// <param name="typeArguments">An array of <see cref="T:System.Type" /> objects that specify the type parameters of the generic method. This argument should be null when methodName specifies a non-generic method.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that represents the arguments to the method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" />, the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> property equal to <paramref name="instance" />, <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> set to the <see cref="T:System.Reflection.MethodInfo" /> that represents the specified instance method, and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> set to the specified arguments.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="instance" /> or <paramref name="methodName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">No method whose name is <paramref name="methodName" />, whose type parameters match <paramref name="typeArguments" />, and whose parameter types match <paramref name="arguments" /> is found in <paramref name="instance" />.Type or its base types.-or-More than one method whose name is <paramref name="methodName" />, whose type parameters match <paramref name="typeArguments" />, and whose parameter types match <paramref name="arguments" /> is found in <paramref name="instance" />.Type or its base types.</exception>
		// Token: 0x06000E19 RID: 3609 RVA: 0x00032338 File Offset: 0x00030538
		public static MethodCallExpression Call(Expression instance, string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			ContractUtils.RequiresNotNull(instance, "instance");
			ContractUtils.RequiresNotNull(methodName, "methodName");
			if (arguments == null)
			{
				arguments = Array.Empty<Expression>();
			}
			BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return Expression.Call(instance, Expression.FindMethod(instance.Type, methodName, typeArguments, arguments, flags), arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method by calling the appropriate factory method.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> that specifies the type that contains the specified <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method.</param>
		/// <param name="methodName">The name of the method.</param>
		/// <param name="typeArguments">An array of <see cref="T:System.Type" /> objects that specify the type parameters of the generic method. This argument should be null when methodName specifies a non-generic method.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments to the method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" />, the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property set to the <see cref="T:System.Reflection.MethodInfo" /> that represents the specified <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method, and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> property set to the specified arguments.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="type" /> or <paramref name="methodName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">No method whose name is <paramref name="methodName" />, whose type parameters match <paramref name="typeArguments" />, and whose parameter types match <paramref name="arguments" /> is found in <paramref name="type" /> or its base types.-or-More than one method whose name is <paramref name="methodName" />, whose type parameters match <paramref name="typeArguments" />, and whose parameter types match <paramref name="arguments" /> is found in <paramref name="type" /> or its base types.</exception>
		// Token: 0x06000E1A RID: 3610 RVA: 0x00032380 File Offset: 0x00030580
		public static MethodCallExpression Call(Type type, string methodName, Type[] typeArguments, params Expression[] arguments)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(methodName, "methodName");
			if (arguments == null)
			{
				arguments = Array.Empty<Expression>();
			}
			BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
			return Expression.Call(null, Expression.FindMethod(type, methodName, typeArguments, arguments, flags), arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents a call to a method that takes arguments.</summary>
		/// <param name="instance">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> property equal to (pass <see langword="null" /> for a <see langword="static" /> (<see langword="Shared" /> in Visual Basic) method).</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" />, <see cref="P:System.Linq.Expressions.MethodCallExpression.Method" />, and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="method" /> is <see langword="null" />.-or-
		///         <paramref name="instance" /> is <see langword="null" /> and <paramref name="method" /> represents an instance method.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="instance" />.Type is not assignable to the declaring type of the method represented by <paramref name="method" />.-or-The number of elements in <paramref name="arguments" /> does not equal the number of parameters for the method represented by <paramref name="method" />.-or-One or more of the elements of <paramref name="arguments" /> is not assignable to the corresponding parameter for the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000E1B RID: 3611 RVA: 0x000323C4 File Offset: 0x000305C4
		public static MethodCallExpression Call(Expression instance, MethodInfo method, IEnumerable<Expression> arguments)
		{
			IReadOnlyList<Expression> readOnlyList = (arguments as IReadOnlyList<Expression>) ?? arguments.ToReadOnly<Expression>();
			int count = readOnlyList.Count;
			switch (count)
			{
			case 0:
				return Expression.Call(instance, method);
			case 1:
				return Expression.Call(instance, method, readOnlyList[0]);
			case 2:
				return Expression.Call(instance, method, readOnlyList[0], readOnlyList[1]);
			case 3:
				return Expression.Call(instance, method, readOnlyList[0], readOnlyList[1], readOnlyList[2]);
			default:
			{
				if (instance == null)
				{
					if (count == 4)
					{
						return Expression.Call(method, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3]);
					}
					if (count == 5)
					{
						return Expression.Call(method, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3], readOnlyList[4]);
					}
				}
				ContractUtils.RequiresNotNull(method, "method");
				ReadOnlyCollection<Expression> args = readOnlyList.ToReadOnly<Expression>();
				Expression.ValidateMethodInfo(method, "method");
				Expression.ValidateStaticOrInstanceMethod(instance, method);
				Expression.ValidateArgumentTypes(method, ExpressionType.Call, ref args, "method");
				if (instance == null)
				{
					return new MethodCallExpressionN(method, args);
				}
				return new InstanceMethodCallExpressionN(method, instance, args);
			}
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000324EA File Offset: 0x000306EA
		private static ParameterInfo[] ValidateMethodAndGetParameters(Expression instance, MethodInfo method)
		{
			Expression.ValidateMethodInfo(method, "method");
			Expression.ValidateStaticOrInstanceMethod(instance, method);
			return Expression.GetParametersForValidation(method, ExpressionType.Call);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00032505 File Offset: 0x00030705
		private static void ValidateStaticOrInstanceMethod(Expression instance, MethodInfo method)
		{
			if (method.IsStatic)
			{
				if (instance != null)
				{
					throw Error.OnlyStaticMethodsHaveNullInstance();
				}
			}
			else
			{
				if (instance == null)
				{
					throw Error.OnlyStaticMethodsHaveNullInstance();
				}
				ExpressionUtils.RequiresCanRead(instance, "instance");
				Expression.ValidateCallInstanceType(instance.Type, method);
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00032538 File Offset: 0x00030738
		private static void ValidateCallInstanceType(Type instanceType, MethodInfo method)
		{
			if (!TypeUtils.IsValidInstanceType(method, instanceType))
			{
				throw Error.InstanceAndMethodTypeMismatch(method, method.DeclaringType, instanceType);
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00032551 File Offset: 0x00030751
		private static void ValidateArgumentTypes(MethodBase method, ExpressionType nodeKind, ref ReadOnlyCollection<Expression> arguments, string methodParamName)
		{
			ExpressionUtils.ValidateArgumentTypes(method, nodeKind, ref arguments, methodParamName);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003255C File Offset: 0x0003075C
		private static ParameterInfo[] GetParametersForValidation(MethodBase method, ExpressionType nodeKind)
		{
			return ExpressionUtils.GetParametersForValidation(method, nodeKind);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00032565 File Offset: 0x00030765
		private static void ValidateArgumentCount(MethodBase method, ExpressionType nodeKind, int count, ParameterInfo[] pis)
		{
			ExpressionUtils.ValidateArgumentCount(method, nodeKind, count, pis);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00032570 File Offset: 0x00030770
		private static Expression ValidateOneArgument(MethodBase method, ExpressionType nodeKind, Expression arg, ParameterInfo pi, string methodParamName, string argumentParamName)
		{
			return ExpressionUtils.ValidateOneArgument(method, nodeKind, arg, pi, methodParamName, argumentParamName, -1);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00032580 File Offset: 0x00030780
		private static bool TryQuote(Type parameterType, ref Expression argument)
		{
			return ExpressionUtils.TryQuote(parameterType, ref argument);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003258C File Offset: 0x0003078C
		private static MethodInfo FindMethod(Type type, string methodName, Type[] typeArgs, Expression[] args, BindingFlags flags)
		{
			int num = 0;
			MethodInfo methodInfo = null;
			foreach (MethodInfo methodInfo2 in type.GetMethods(flags))
			{
				if (methodInfo2.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase))
				{
					MethodInfo methodInfo3 = Expression.ApplyTypeArgs(methodInfo2, typeArgs);
					if (methodInfo3 != null && Expression.IsCompatible(methodInfo3, args))
					{
						if (methodInfo == null || (!methodInfo.IsPublic && methodInfo3.IsPublic))
						{
							methodInfo = methodInfo3;
							num = 1;
						}
						else if (methodInfo.IsPublic == methodInfo3.IsPublic)
						{
							num++;
						}
					}
				}
			}
			if (num == 0)
			{
				if (typeArgs != null && typeArgs.Length != 0)
				{
					throw Error.GenericMethodWithArgsDoesNotExistOnType(methodName, type);
				}
				throw Error.MethodWithArgsDoesNotExistOnType(methodName, type);
			}
			else
			{
				if (num > 1)
				{
					throw Error.MethodWithMoreThanOneMatch(methodName, type);
				}
				return methodInfo;
			}
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00032644 File Offset: 0x00030844
		private static bool IsCompatible(MethodBase m, Expression[] arguments)
		{
			ParameterInfo[] parametersCached = m.GetParametersCached();
			if (parametersCached.Length != arguments.Length)
			{
				return false;
			}
			for (int i = 0; i < arguments.Length; i++)
			{
				Expression expression = arguments[i];
				ContractUtils.RequiresNotNull(expression, "arguments");
				Type type = expression.Type;
				Type type2 = parametersCached[i].ParameterType;
				if (type2.IsByRef)
				{
					type2 = type2.GetElementType();
				}
				if (!TypeUtils.AreReferenceAssignable(type2, type) && (!TypeUtils.IsSameOrSubclass(typeof(LambdaExpression), type2) || !type2.IsAssignableFrom(expression.GetType())))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000326D1 File Offset: 0x000308D1
		private static MethodInfo ApplyTypeArgs(MethodInfo m, Type[] typeArgs)
		{
			if (typeArgs == null || typeArgs.Length == 0)
			{
				if (!m.IsGenericMethodDefinition)
				{
					return m;
				}
			}
			else if (m.IsGenericMethodDefinition && m.GetGenericArguments().Length == typeArgs.Length)
			{
				return m.MakeGenericMethod(typeArgs);
			}
			return null;
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents applying an array index operator to a multidimensional array.</summary>
		/// <param name="array">An array of <see cref="T:System.Linq.Expressions.Expression" /> instances - indexes for the array index operation.</param>
		/// <param name="indexes">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> or <paramref name="indexes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="array" />.Type does not represent an array type.-or-The rank of <paramref name="array" />.Type does not match the number of elements in <paramref name="indexes" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="indexes" /> does not represent the <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x06000E27 RID: 3623 RVA: 0x00032702 File Offset: 0x00030902
		public static MethodCallExpression ArrayIndex(Expression array, params Expression[] indexes)
		{
			return Expression.ArrayIndex(array, indexes);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that represents applying an array index operator to an array of rank more than one.</summary>
		/// <param name="array">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> property equal to.</param>
		/// <param name="indexes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.MethodCallExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Call" /> and the <see cref="P:System.Linq.Expressions.MethodCallExpression.Object" /> and <see cref="P:System.Linq.Expressions.MethodCallExpression.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> or <paramref name="indexes" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="array" />.Type does not represent an array type.-or-The rank of <paramref name="array" />.Type does not match the number of elements in <paramref name="indexes" />.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of one or more elements of <paramref name="indexes" /> does not represent the <see cref="T:System.Int32" /> type.</exception>
		// Token: 0x06000E28 RID: 3624 RVA: 0x0003270C File Offset: 0x0003090C
		public static MethodCallExpression ArrayIndex(Expression array, IEnumerable<Expression> indexes)
		{
			ExpressionUtils.RequiresCanRead(array, "array", -1);
			ContractUtils.RequiresNotNull(indexes, "indexes");
			Type type = array.Type;
			if (!type.IsArray)
			{
				throw Error.ArgumentMustBeArray("array");
			}
			ReadOnlyCollection<Expression> readOnlyCollection = indexes.ToReadOnly<Expression>();
			if (type.GetArrayRank() != readOnlyCollection.Count)
			{
				throw Error.IncorrectNumberOfIndexes();
			}
			int i = 0;
			int count = readOnlyCollection.Count;
			while (i < count)
			{
				Expression expression = readOnlyCollection[i];
				ExpressionUtils.RequiresCanRead(expression, "indexes", i);
				if (expression.Type != typeof(int))
				{
					throw Error.ArgumentMustBeArrayIndexType("indexes", i);
				}
				i++;
			}
			MethodInfo method = array.Type.GetMethod("Get", BindingFlags.Instance | BindingFlags.Public);
			return Expression.Call(array, method, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that represents creating a one-dimensional array and initializing it from a list of elements.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the element type of the array.</param>
		/// <param name="initializers">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayInit" /> and the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="type" /> or <paramref name="initializers" /> is <see langword="null" />.-or-An element of <paramref name="initializers" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="initializers" /> represents a type that is not assignable to the type <paramref name="type" />.</exception>
		// Token: 0x06000E29 RID: 3625 RVA: 0x000327C6 File Offset: 0x000309C6
		public static NewArrayExpression NewArrayInit(Type type, params Expression[] initializers)
		{
			return Expression.NewArrayInit(type, initializers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that represents creating a one-dimensional array and initializing it from a list of elements.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the element type of the array.</param>
		/// <param name="initializers">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayInit" /> and the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="type" /> or <paramref name="initializers" /> is <see langword="null" />.-or-An element of <paramref name="initializers" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="initializers" /> represents a type that is not assignable to the type that <paramref name="type" /> represents.</exception>
		// Token: 0x06000E2A RID: 3626 RVA: 0x000327D0 File Offset: 0x000309D0
		public static NewArrayExpression NewArrayInit(Type type, IEnumerable<Expression> initializers)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(initializers, "initializers");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("type");
			}
			TypeUtils.ValidateType(type, "type");
			ReadOnlyCollection<Expression> readOnlyCollection = initializers.ToReadOnly<Expression>();
			Expression[] array = null;
			int i = 0;
			int count = readOnlyCollection.Count;
			while (i < count)
			{
				Expression expression = readOnlyCollection[i];
				ExpressionUtils.RequiresCanRead(expression, "initializers", i);
				if (!TypeUtils.AreReferenceAssignable(type, expression.Type))
				{
					if (!Expression.TryQuote(type, ref expression))
					{
						throw Error.ExpressionTypeCannotInitializeArrayType(expression.Type, type);
					}
					if (array == null)
					{
						array = new Expression[readOnlyCollection.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = readOnlyCollection[j];
						}
					}
				}
				if (array != null)
				{
					array[i] = expression;
				}
				i++;
			}
			if (array != null)
			{
				readOnlyCollection = new TrueReadOnlyCollection<Expression>(array);
			}
			return NewArrayExpression.Make(ExpressionType.NewArrayInit, type.MakeArrayType(), readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that represents creating an array that has a specified rank.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the element type of the array.</param>
		/// <param name="bounds">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayBounds" /> and the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="type" /> or <paramref name="bounds" /> is <see langword="null" />.-or-An element of <paramref name="bounds" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="bounds" /> does not represent an integral type.</exception>
		// Token: 0x06000E2B RID: 3627 RVA: 0x000328BF File Offset: 0x00030ABF
		public static NewArrayExpression NewArrayBounds(Type type, params Expression[] bounds)
		{
			return Expression.NewArrayBounds(type, bounds);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that represents creating an array that has a specified rank.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the element type of the array.</param>
		/// <param name="bounds">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewArrayExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NewArrayBounds" /> and the <see cref="P:System.Linq.Expressions.NewArrayExpression.Expressions" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="type" /> or <paramref name="bounds" /> is <see langword="null" />.-or-An element of <paramref name="bounds" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="bounds" /> does not represent an integral type.</exception>
		// Token: 0x06000E2C RID: 3628 RVA: 0x000328C8 File Offset: 0x00030AC8
		public static NewArrayExpression NewArrayBounds(Type type, IEnumerable<Expression> bounds)
		{
			ContractUtils.RequiresNotNull(type, "type");
			ContractUtils.RequiresNotNull(bounds, "bounds");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("type");
			}
			TypeUtils.ValidateType(type, "type");
			ReadOnlyCollection<Expression> readOnlyCollection = bounds.ToReadOnly<Expression>();
			int count = readOnlyCollection.Count;
			if (count <= 0)
			{
				throw Error.BoundsCannotBeLessThanOne("bounds");
			}
			for (int i = 0; i < count; i++)
			{
				Expression expression = readOnlyCollection[i];
				ExpressionUtils.RequiresCanRead(expression, "bounds", i);
				if (!expression.Type.IsInteger())
				{
					throw Error.ArgumentMustBeInteger("bounds", i);
				}
			}
			Type type2;
			if (count == 1)
			{
				type2 = type.MakeArrayType();
			}
			else
			{
				type2 = type.MakeArrayType(count);
			}
			return NewArrayExpression.Make(ExpressionType.NewArrayBounds, type2, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor that takes no arguments.</summary>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="constructor" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The constructor that <paramref name="constructor" /> represents has at least one parameter.</exception>
		// Token: 0x06000E2D RID: 3629 RVA: 0x00032985 File Offset: 0x00030B85
		public static NewExpression New(ConstructorInfo constructor)
		{
			return Expression.New(constructor, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor with the specified arguments.</summary>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <param name="arguments">An array of <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> and <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="constructor" /> is <see langword="null" />.-or-An element of <paramref name="arguments" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="arguments" /> does match the number of parameters for the constructor that <paramref name="constructor" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the constructor that <paramref name="constructor" /> represents.</exception>
		// Token: 0x06000E2E RID: 3630 RVA: 0x0003298E File Offset: 0x00030B8E
		public static NewExpression New(ConstructorInfo constructor, params Expression[] arguments)
		{
			return Expression.New(constructor, arguments);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor with the specified arguments.</summary>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> and <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="constructor" /> is <see langword="null" />.-or-An element of <paramref name="arguments" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="arguments" /> parameter does not contain the same number of elements as the number of parameters for the constructor that <paramref name="constructor" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the constructor that <paramref name="constructor" /> represents.</exception>
		// Token: 0x06000E2F RID: 3631 RVA: 0x00032998 File Offset: 0x00030B98
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(constructor, "constructor");
			ContractUtils.RequiresNotNull(constructor.DeclaringType, "constructor.DeclaringType");
			TypeUtils.ValidateType(constructor.DeclaringType, "constructor", true, true);
			Expression.ValidateConstructor(constructor, "constructor");
			ReadOnlyCollection<Expression> arguments2 = arguments.ToReadOnly<Expression>();
			Expression.ValidateArgumentTypes(constructor, ExpressionType.New, ref arguments2, "constructor");
			return new NewExpression(constructor, arguments2, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor with the specified arguments. The members that access the constructor initialized fields are specified.</summary>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> collection.</param>
		/// <param name="members">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Reflection.MemberInfo" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Members" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" />, <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> and <see cref="P:System.Linq.Expressions.NewExpression.Members" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="constructor" /> is <see langword="null" />.-or-An element of <paramref name="arguments" /> is <see langword="null" />.-or-An element of <paramref name="members" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="arguments" /> parameter does not contain the same number of elements as the number of parameters for the constructor that <paramref name="constructor" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the constructor that <paramref name="constructor" /> represents.-or-The <paramref name="members" /> parameter does not have the same number of elements as <paramref name="arguments" />.-or-An element of <paramref name="arguments" /> has a <see cref="P:System.Linq.Expressions.Expression.Type" /> property that represents a type that is not assignable to the type of the member that is represented by the corresponding element of <paramref name="members" />.</exception>
		// Token: 0x06000E30 RID: 3632 RVA: 0x000329FC File Offset: 0x00030BFC
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, IEnumerable<MemberInfo> members)
		{
			ContractUtils.RequiresNotNull(constructor, "constructor");
			ContractUtils.RequiresNotNull(constructor.DeclaringType, "constructor.DeclaringType");
			TypeUtils.ValidateType(constructor.DeclaringType, "constructor", true, true);
			Expression.ValidateConstructor(constructor, "constructor");
			ReadOnlyCollection<MemberInfo> members2 = members.ToReadOnly<MemberInfo>();
			ReadOnlyCollection<Expression> arguments2 = arguments.ToReadOnly<Expression>();
			Expression.ValidateNewArgs(constructor, ref arguments2, ref members2);
			return new NewExpression(constructor, arguments2, members2);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the specified constructor with the specified arguments. The members that access the constructor initialized fields are specified as an array.</summary>
		/// <param name="constructor">The <see cref="T:System.Reflection.ConstructorInfo" /> to set the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property equal to.</param>
		/// <param name="arguments">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <see cref="T:System.Linq.Expressions.Expression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> collection.</param>
		/// <param name="members">An array of <see cref="T:System.Reflection.MemberInfo" /> objects to use to populate the <see cref="P:System.Linq.Expressions.NewExpression.Members" /> collection.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" />, <see cref="P:System.Linq.Expressions.NewExpression.Arguments" /> and <see cref="P:System.Linq.Expressions.NewExpression.Members" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="constructor" /> is <see langword="null" />.-or-An element of <paramref name="arguments" /> is <see langword="null" />.-or-An element of <paramref name="members" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="arguments" /> parameter does not contain the same number of elements as the number of parameters for the constructor that <paramref name="constructor" /> represents.-or-The <see cref="P:System.Linq.Expressions.Expression.Type" /> property of an element of <paramref name="arguments" /> is not assignable to the type of the corresponding parameter of the constructor that <paramref name="constructor" /> represents.-or-The <paramref name="members" /> parameter does not have the same number of elements as <paramref name="arguments" />.-or-An element of <paramref name="arguments" /> has a <see cref="P:System.Linq.Expressions.Expression.Type" /> property that represents a type that is not assignable to the type of the member that is represented by the corresponding element of <paramref name="members" />.</exception>
		// Token: 0x06000E31 RID: 3633 RVA: 0x00032A61 File Offset: 0x00030C61
		public static NewExpression New(ConstructorInfo constructor, IEnumerable<Expression> arguments, params MemberInfo[] members)
		{
			return Expression.New(constructor, arguments, members);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.NewExpression" /> that represents calling the parameterless constructor of the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that has a constructor that takes no arguments.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.NewExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.New" /> and the <see cref="P:System.Linq.Expressions.NewExpression.Constructor" /> property set to the <see cref="T:System.Reflection.ConstructorInfo" /> that represents the constructor without parameters for the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The type that <paramref name="type" /> represents does not have a constructor without parameters.</exception>
		// Token: 0x06000E32 RID: 3634 RVA: 0x00032A6C File Offset: 0x00030C6C
		public static NewExpression New(Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("type");
			}
			TypeUtils.ValidateType(type, "type");
			if (type.IsValueType)
			{
				return new NewValueTypeExpression(type, EmptyReadOnlyCollection<Expression>.Instance, null);
			}
			ConstructorInfo constructorInfo = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).SingleOrDefault((ConstructorInfo c) => c.GetParametersCached().Length == 0);
			if (constructorInfo == null)
			{
				throw Error.TypeMissingDefaultConstructor(type, "type");
			}
			return Expression.New(constructorInfo);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00032B08 File Offset: 0x00030D08
		private static void ValidateNewArgs(ConstructorInfo constructor, ref ReadOnlyCollection<Expression> arguments, ref ReadOnlyCollection<MemberInfo> members)
		{
			ParameterInfo[] parametersCached;
			if ((parametersCached = constructor.GetParametersCached()).Length != 0)
			{
				if (arguments.Count != parametersCached.Length)
				{
					throw Error.IncorrectNumberOfConstructorArguments();
				}
				if (arguments.Count != members.Count)
				{
					throw Error.IncorrectNumberOfArgumentsForMembers();
				}
				Expression[] array = null;
				MemberInfo[] array2 = null;
				int i = 0;
				int count = arguments.Count;
				while (i < count)
				{
					Expression expression = arguments[i];
					ExpressionUtils.RequiresCanRead(expression, "arguments", i);
					MemberInfo memberInfo = members[i];
					ContractUtils.RequiresNotNull(memberInfo, "members", i);
					if (!TypeUtils.AreEquivalent(memberInfo.DeclaringType, constructor.DeclaringType))
					{
						throw Error.ArgumentMemberNotDeclOnType(memberInfo.Name, constructor.DeclaringType.Name, "members", i);
					}
					Type type;
					Expression.ValidateAnonymousTypeMember(ref memberInfo, out type, "members", i);
					if (!TypeUtils.AreReferenceAssignable(type, expression.Type) && !Expression.TryQuote(type, ref expression))
					{
						throw Error.ArgumentTypeDoesNotMatchMember(expression.Type, type, "arguments", i);
					}
					Type type2 = parametersCached[i].ParameterType;
					if (type2.IsByRef)
					{
						type2 = type2.GetElementType();
					}
					if (!TypeUtils.AreReferenceAssignable(type2, expression.Type) && !Expression.TryQuote(type2, ref expression))
					{
						throw Error.ExpressionTypeDoesNotMatchConstructorParameter(expression.Type, type2, "arguments", i);
					}
					if (array == null && expression != arguments[i])
					{
						array = new Expression[arguments.Count];
						for (int j = 0; j < i; j++)
						{
							array[j] = arguments[j];
						}
					}
					if (array != null)
					{
						array[i] = expression;
					}
					if (array2 == null && memberInfo != members[i])
					{
						array2 = new MemberInfo[members.Count];
						for (int k = 0; k < i; k++)
						{
							array2[k] = members[k];
						}
					}
					if (array2 != null)
					{
						array2[i] = memberInfo;
					}
					i++;
				}
				if (array != null)
				{
					arguments = new TrueReadOnlyCollection<Expression>(array);
				}
				if (array2 != null)
				{
					members = new TrueReadOnlyCollection<MemberInfo>(array2);
					return;
				}
			}
			else
			{
				if (arguments != null && arguments.Count > 0)
				{
					throw Error.IncorrectNumberOfConstructorArguments();
				}
				if (members != null && members.Count > 0)
				{
					throw Error.IncorrectNumberOfMembersForGivenConstructor();
				}
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00032D1C File Offset: 0x00030F1C
		private static void ValidateAnonymousTypeMember(ref MemberInfo member, out Type memberType, string paramName, int index)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				if (fieldInfo.IsStatic)
				{
					throw Error.ArgumentMustBeInstanceMember(paramName, index);
				}
				memberType = fieldInfo.FieldType;
				return;
			}
			else
			{
				PropertyInfo propertyInfo = member as PropertyInfo;
				if (propertyInfo != null)
				{
					if (!propertyInfo.CanRead)
					{
						throw Error.PropertyDoesNotHaveGetter(propertyInfo, paramName, index);
					}
					if (propertyInfo.GetGetMethod().IsStatic)
					{
						throw Error.ArgumentMustBeInstanceMember(paramName, index);
					}
					memberType = propertyInfo.PropertyType;
					return;
				}
				else
				{
					MethodInfo methodInfo = member as MethodInfo;
					if (!(methodInfo != null))
					{
						throw Error.ArgumentMustBeFieldInfoOrPropertyInfoOrMethod(paramName, index);
					}
					if (methodInfo.IsStatic)
					{
						throw Error.ArgumentMustBeInstanceMember(paramName, index);
					}
					PropertyInfo property = Expression.GetProperty(methodInfo, paramName, index);
					member = property;
					memberType = property.PropertyType;
					return;
				}
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00032DD0 File Offset: 0x00030FD0
		private static void ValidateConstructor(ConstructorInfo constructor, string paramName)
		{
			if (constructor.IsStatic)
			{
				throw Error.NonStaticConstructorRequired(paramName);
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ParameterExpression" /> node that can be used to identify a parameter or a variable in an expression tree.</summary>
		/// <param name="type">The type of the parameter or variable.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ParameterExpression" /> node with the specified name and type.</returns>
		// Token: 0x06000E36 RID: 3638 RVA: 0x00032DE1 File Offset: 0x00030FE1
		public static ParameterExpression Parameter(Type type)
		{
			return Expression.Parameter(type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ParameterExpression" /> node that can be used to identify a parameter or a variable in an expression tree.</summary>
		/// <param name="type">The type of the parameter or variable.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ParameterExpression" /> node with the specified name and type</returns>
		// Token: 0x06000E37 RID: 3639 RVA: 0x00032DEA File Offset: 0x00030FEA
		public static ParameterExpression Variable(Type type)
		{
			return Expression.Variable(type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ParameterExpression" /> node that can be used to identify a parameter or a variable in an expression tree.</summary>
		/// <param name="type">The type of the parameter or variable.</param>
		/// <param name="name">The name of the parameter or variable, used for debugging or printing purpose only.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ParameterExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Parameter" /> and the <see cref="P:System.Linq.Expressions.Expression.Type" /> and <see cref="P:System.Linq.Expressions.ParameterExpression.Name" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06000E38 RID: 3640 RVA: 0x00032DF4 File Offset: 0x00030FF4
		public static ParameterExpression Parameter(Type type, string name)
		{
			Expression.Validate(type, true);
			bool isByRef = type.IsByRef;
			if (isByRef)
			{
				type = type.GetElementType();
			}
			return ParameterExpression.Make(type, name, isByRef);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.ParameterExpression" /> node that can be used to identify a parameter or a variable in an expression tree.</summary>
		/// <param name="type">The type of the parameter or variable.</param>
		/// <param name="name">The name of the parameter or variable. This name is used for debugging or printing purpose only.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.ParameterExpression" /> node with the specified name and type.</returns>
		// Token: 0x06000E39 RID: 3641 RVA: 0x00032E22 File Offset: 0x00031022
		public static ParameterExpression Variable(Type type, string name)
		{
			Expression.Validate(type, false);
			return ParameterExpression.Make(type, name, false);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00032E33 File Offset: 0x00031033
		private static void Validate(Type type, bool allowByRef)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type", allowByRef, false);
			if (type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("type");
			}
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" />.</summary>
		/// <param name="variables">An array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> collection.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RuntimeVariables" /> and the <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> property set to the specified value.</returns>
		// Token: 0x06000E3B RID: 3643 RVA: 0x00032E6A File Offset: 0x0003106A
		public static RuntimeVariablesExpression RuntimeVariables(params ParameterExpression[] variables)
		{
			return Expression.RuntimeVariables(variables);
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" />.</summary>
		/// <param name="variables">A collection of <see cref="T:System.Linq.Expressions.ParameterExpression" /> objects to use to populate the <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> collection.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.RuntimeVariablesExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.RuntimeVariables" /> and the <see cref="P:System.Linq.Expressions.RuntimeVariablesExpression.Variables" /> property set to the specified value.</returns>
		// Token: 0x06000E3C RID: 3644 RVA: 0x00032E74 File Offset: 0x00031074
		public static RuntimeVariablesExpression RuntimeVariables(IEnumerable<ParameterExpression> variables)
		{
			ContractUtils.RequiresNotNull(variables, "variables");
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = variables.ToReadOnly<ParameterExpression>();
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				ContractUtils.RequiresNotNull(readOnlyCollection[i], "variables", i);
			}
			return new RuntimeVariablesExpression(readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchCase" /> for use in a <see cref="T:System.Linq.Expressions.SwitchExpression" />.</summary>
		/// <param name="body">The body of the case.</param>
		/// <param name="testValues">The test values of the case.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchCase" />.</returns>
		// Token: 0x06000E3D RID: 3645 RVA: 0x00032EBC File Offset: 0x000310BC
		public static SwitchCase SwitchCase(Expression body, params Expression[] testValues)
		{
			return Expression.SwitchCase(body, testValues);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchCase" /> object to be used in a <see cref="T:System.Linq.Expressions.SwitchExpression" /> object.</summary>
		/// <param name="body">The body of the case.</param>
		/// <param name="testValues">The test values of the case.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchCase" />.</returns>
		// Token: 0x06000E3E RID: 3646 RVA: 0x00032EC8 File Offset: 0x000310C8
		public static SwitchCase SwitchCase(Expression body, IEnumerable<Expression> testValues)
		{
			ExpressionUtils.RequiresCanRead(body, "body");
			ReadOnlyCollection<Expression> readOnlyCollection = testValues.ToReadOnly<Expression>();
			ContractUtils.RequiresNotEmpty<Expression>(readOnlyCollection, "testValues");
			Expression.RequiresCanRead(readOnlyCollection, "testValues");
			return new SwitchCase(body, readOnlyCollection);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a <see langword="switch" /> statement without a default case.</summary>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		// Token: 0x06000E3F RID: 3647 RVA: 0x00032F04 File Offset: 0x00031104
		public static SwitchExpression Switch(Expression switchValue, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, null, null, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a <see langword="switch" /> statement that has a default case.</summary>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		// Token: 0x06000E40 RID: 3648 RVA: 0x00032F0F File Offset: 0x0003110F
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, defaultBody, null, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a <see langword="switch" /> statement that has a default case.</summary>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="comparison">The equality comparison method to use.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		// Token: 0x06000E41 RID: 3649 RVA: 0x00032F1A File Offset: 0x0003111A
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, MethodInfo comparison, params SwitchCase[] cases)
		{
			return Expression.Switch(switchValue, defaultBody, comparison, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a <see langword="switch" /> statement that has a default case..</summary>
		/// <param name="type">The result type of the switch.</param>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="comparison">The equality comparison method to use.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		// Token: 0x06000E42 RID: 3650 RVA: 0x00032F25 File Offset: 0x00031125
		public static SwitchExpression Switch(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, params SwitchCase[] cases)
		{
			return Expression.Switch(type, switchValue, defaultBody, comparison, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a <see langword="switch" /> statement that has a default case.</summary>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="comparison">The equality comparison method to use.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		// Token: 0x06000E43 RID: 3651 RVA: 0x00032F32 File Offset: 0x00031132
		public static SwitchExpression Switch(Expression switchValue, Expression defaultBody, MethodInfo comparison, IEnumerable<SwitchCase> cases)
		{
			return Expression.Switch(null, switchValue, defaultBody, comparison, cases);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.SwitchExpression" /> that represents a <see langword="switch" /> statement that has a default case.</summary>
		/// <param name="type">The result type of the switch.</param>
		/// <param name="switchValue">The value to be tested against each case.</param>
		/// <param name="defaultBody">The result of the switch if <paramref name="switchValue" /> does not match any of the cases.</param>
		/// <param name="comparison">The equality comparison method to use.</param>
		/// <param name="cases">The set of cases for this switch expression.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.SwitchExpression" />.</returns>
		// Token: 0x06000E44 RID: 3652 RVA: 0x00032F40 File Offset: 0x00031140
		public static SwitchExpression Switch(Type type, Expression switchValue, Expression defaultBody, MethodInfo comparison, IEnumerable<SwitchCase> cases)
		{
			ExpressionUtils.RequiresCanRead(switchValue, "switchValue");
			if (switchValue.Type == typeof(void))
			{
				throw Error.ArgumentCannotBeOfTypeVoid("switchValue");
			}
			ReadOnlyCollection<SwitchCase> readOnlyCollection = cases.ToReadOnly<SwitchCase>();
			ContractUtils.RequiresNotNullItems<SwitchCase>(readOnlyCollection, "cases");
			Type type2;
			if (type != null)
			{
				type2 = type;
			}
			else if (readOnlyCollection.Count != 0)
			{
				type2 = readOnlyCollection[0].Body.Type;
			}
			else if (defaultBody != null)
			{
				type2 = defaultBody.Type;
			}
			else
			{
				type2 = typeof(void);
			}
			bool customType = type != null;
			if (comparison != null)
			{
				Expression.ValidateMethodInfo(comparison, "comparison");
				ParameterInfo[] parametersCached = comparison.GetParametersCached();
				if (parametersCached.Length != 2)
				{
					throw Error.IncorrectNumberOfMethodCallArguments(comparison, "comparison");
				}
				ParameterInfo parameterInfo = parametersCached[0];
				bool flag = false;
				if (!Expression.ParameterIsAssignable(parameterInfo, switchValue.Type))
				{
					flag = Expression.ParameterIsAssignable(parameterInfo, switchValue.Type.GetNonNullableType());
					if (!flag)
					{
						throw Error.SwitchValueTypeDoesNotMatchComparisonMethodParameter(switchValue.Type, parameterInfo.ParameterType);
					}
				}
				ParameterInfo parameterInfo2 = parametersCached[1];
				foreach (SwitchCase switchCase in readOnlyCollection)
				{
					ContractUtils.RequiresNotNull(switchCase, "cases");
					Expression.ValidateSwitchCaseType(switchCase.Body, customType, type2, "cases");
					int i = 0;
					int count = switchCase.TestValues.Count;
					while (i < count)
					{
						Type type3 = switchCase.TestValues[i].Type;
						if (flag)
						{
							if (!type3.IsNullableType())
							{
								throw Error.TestValueTypeDoesNotMatchComparisonMethodParameter(type3, parameterInfo2.ParameterType);
							}
							type3 = type3.GetNonNullableType();
						}
						if (!Expression.ParameterIsAssignable(parameterInfo2, type3))
						{
							throw Error.TestValueTypeDoesNotMatchComparisonMethodParameter(type3, parameterInfo2.ParameterType);
						}
						i++;
					}
				}
				if (comparison.ReturnType != typeof(bool))
				{
					throw Error.EqualityMustReturnBoolean(comparison, "comparison");
				}
			}
			else if (readOnlyCollection.Count != 0)
			{
				Expression expression = readOnlyCollection[0].TestValues[0];
				foreach (SwitchCase switchCase2 in readOnlyCollection)
				{
					ContractUtils.RequiresNotNull(switchCase2, "cases");
					Expression.ValidateSwitchCaseType(switchCase2.Body, customType, type2, "cases");
					int j = 0;
					int count2 = switchCase2.TestValues.Count;
					while (j < count2)
					{
						if (!TypeUtils.AreEquivalent(expression.Type, switchCase2.TestValues[j].Type))
						{
							throw Error.AllTestValuesMustHaveSameType("cases");
						}
						j++;
					}
				}
				comparison = Expression.Equal(switchValue, expression, false, comparison).Method;
			}
			if (defaultBody == null)
			{
				if (type2 != typeof(void))
				{
					throw Error.DefaultBodyMustBeSupplied("defaultBody");
				}
			}
			else
			{
				Expression.ValidateSwitchCaseType(defaultBody, customType, type2, "defaultBody");
			}
			return new SwitchExpression(type2, switchValue, defaultBody, comparison, readOnlyCollection);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00033248 File Offset: 0x00031448
		private static void ValidateSwitchCaseType(Expression @case, bool customType, Type resultType, string parameterName)
		{
			if (customType)
			{
				if (resultType != typeof(void) && !TypeUtils.AreReferenceAssignable(resultType, @case.Type))
				{
					throw Error.ArgumentTypesMustMatch(parameterName);
				}
			}
			else if (!TypeUtils.AreEquivalent(resultType, @case.Type))
			{
				throw Error.AllCaseBodiesMustHaveSameType(parameterName);
			}
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that has the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> property set to the specified value.</returns>
		// Token: 0x06000E46 RID: 3654 RVA: 0x00033294 File Offset: 0x00031494
		public static SymbolDocumentInfo SymbolDocument(string fileName)
		{
			return new SymbolDocumentInfo(fileName);
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> equal to.</param>
		/// <param name="language">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that has the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> properties set to the specified value.</returns>
		// Token: 0x06000E47 RID: 3655 RVA: 0x0003329C File Offset: 0x0003149C
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language)
		{
			return new SymbolDocumentWithGuids(fileName, ref language);
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> equal to.</param>
		/// <param name="language">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> equal to.</param>
		/// <param name="languageVendor">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.LanguageVendor" /> equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that has the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.LanguageVendor" /> properties set to the specified value.</returns>
		// Token: 0x06000E48 RID: 3656 RVA: 0x000332A6 File Offset: 0x000314A6
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language, Guid languageVendor)
		{
			return new SymbolDocumentWithGuids(fileName, ref language, ref languageVendor);
		}

		/// <summary>Creates an instance of <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> equal to.</param>
		/// <param name="language">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> equal to.</param>
		/// <param name="languageVendor">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.LanguageVendor" /> equal to.</param>
		/// <param name="documentType">A <see cref="T:System.Guid" /> to set the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.DocumentType" /> equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.SymbolDocumentInfo" /> that has the <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.FileName" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.Language" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.LanguageVendor" /> and <see cref="P:System.Linq.Expressions.SymbolDocumentInfo.DocumentType" /> properties set to the specified value.</returns>
		// Token: 0x06000E49 RID: 3657 RVA: 0x000332B2 File Offset: 0x000314B2
		public static SymbolDocumentInfo SymbolDocument(string fileName, Guid language, Guid languageVendor, Guid documentType)
		{
			return new SymbolDocumentWithGuids(fileName, ref language, ref languageVendor, ref documentType);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with a fault block and no catch statements.</summary>
		/// <param name="body">The body of the try block.</param>
		/// <param name="fault">The body of the fault block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		// Token: 0x06000E4A RID: 3658 RVA: 0x000332C0 File Offset: 0x000314C0
		public static TryExpression TryFault(Expression body, Expression fault)
		{
			return Expression.MakeTry(null, body, null, fault, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with a finally block and no catch statements.</summary>
		/// <param name="body">The body of the try block.</param>
		/// <param name="finally">The body of the finally block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		// Token: 0x06000E4B RID: 3659 RVA: 0x000332CC File Offset: 0x000314CC
		public static TryExpression TryFinally(Expression body, Expression @finally)
		{
			return Expression.MakeTry(null, body, @finally, null, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with any number of catch statements and neither a fault nor finally block.</summary>
		/// <param name="body">The body of the try block.</param>
		/// <param name="handlers">The array of zero or more <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions representing the catch statements to be associated with the try block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		// Token: 0x06000E4C RID: 3660 RVA: 0x000332D8 File Offset: 0x000314D8
		public static TryExpression TryCatch(Expression body, params CatchBlock[] handlers)
		{
			return Expression.MakeTry(null, body, null, null, handlers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with any number of catch statements and a finally block.</summary>
		/// <param name="body">The body of the try block.</param>
		/// <param name="finally">The body of the finally block.</param>
		/// <param name="handlers">The array of zero or more <see cref="T:System.Linq.Expressions.CatchBlock" /> expressions representing the catch statements to be associated with the try block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		// Token: 0x06000E4D RID: 3661 RVA: 0x000332E4 File Offset: 0x000314E4
		public static TryExpression TryCatchFinally(Expression body, Expression @finally, params CatchBlock[] handlers)
		{
			return Expression.MakeTry(null, body, @finally, null, handlers);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TryExpression" /> representing a try block with the specified elements.</summary>
		/// <param name="type">The result type of the try expression. If null, bodh and all handlers must have identical type.</param>
		/// <param name="body">The body of the try block.</param>
		/// <param name="finally">The body of the finally block. Pass null if the try block has no finally block associated with it.</param>
		/// <param name="fault">The body of the fault block. Pass null if the try block has no fault block associated with it.</param>
		/// <param name="handlers">A collection of <see cref="T:System.Linq.Expressions.CatchBlock" />s representing the catch statements to be associated with the try block.</param>
		/// <returns>The created <see cref="T:System.Linq.Expressions.TryExpression" />.</returns>
		// Token: 0x06000E4E RID: 3662 RVA: 0x000332F0 File Offset: 0x000314F0
		public static TryExpression MakeTry(Type type, Expression body, Expression @finally, Expression fault, IEnumerable<CatchBlock> handlers)
		{
			ExpressionUtils.RequiresCanRead(body, "body");
			ReadOnlyCollection<CatchBlock> readOnlyCollection = handlers.ToReadOnly<CatchBlock>();
			ContractUtils.RequiresNotNullItems<CatchBlock>(readOnlyCollection, "handlers");
			Expression.ValidateTryAndCatchHaveSameType(type, body, readOnlyCollection);
			if (fault != null)
			{
				if (@finally != null || readOnlyCollection.Count > 0)
				{
					throw Error.FaultCannotHaveCatchOrFinally("fault");
				}
				ExpressionUtils.RequiresCanRead(fault, "fault");
			}
			else if (@finally != null)
			{
				ExpressionUtils.RequiresCanRead(@finally, "finally");
			}
			else if (readOnlyCollection.Count == 0)
			{
				throw Error.TryMustHaveCatchFinallyOrFault();
			}
			return new TryExpression(type ?? body.Type, body, @finally, fault, readOnlyCollection);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0003337C File Offset: 0x0003157C
		private static void ValidateTryAndCatchHaveSameType(Type type, Expression tryBody, ReadOnlyCollection<CatchBlock> handlers)
		{
			if (type != null)
			{
				if (!(type != typeof(void)))
				{
					return;
				}
				if (!TypeUtils.AreReferenceAssignable(type, tryBody.Type))
				{
					throw Error.ArgumentTypesMustMatch();
				}
				using (IEnumerator<CatchBlock> enumerator = handlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CatchBlock catchBlock = enumerator.Current;
						if (!TypeUtils.AreReferenceAssignable(type, catchBlock.Body.Type))
						{
							throw Error.ArgumentTypesMustMatch();
						}
					}
					return;
				}
			}
			if (tryBody.Type == typeof(void))
			{
				using (IEnumerator<CatchBlock> enumerator = handlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Body.Type != typeof(void))
						{
							throw Error.BodyOfCatchMustHaveSameTypeAsBodyOfTry();
						}
					}
					return;
				}
			}
			type = tryBody.Type;
			using (IEnumerator<CatchBlock> enumerator = handlers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!TypeUtils.AreEquivalent(enumerator.Current.Body.Type, type))
					{
						throw Error.BodyOfCatchMustHaveSameTypeAsBodyOfTry();
					}
				}
			}
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TypeBinaryExpression" />.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> property equal to.</param>
		/// <param name="type">A <see cref="P:System.Linq.Expressions.Expression.Type" /> to set the <see cref="P:System.Linq.Expressions.TypeBinaryExpression.TypeOperand" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.TypeBinaryExpression" /> for which the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is equal to <see cref="F:System.Linq.Expressions.ExpressionType.TypeIs" /> and for which the <see cref="P:System.Linq.Expressions.TypeBinaryExpression.Expression" /> and <see cref="P:System.Linq.Expressions.TypeBinaryExpression.TypeOperand" /> properties are set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06000E50 RID: 3664 RVA: 0x000334C4 File Offset: 0x000316C4
		public static TypeBinaryExpression TypeIs(Expression expression, Type type)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (type.IsByRef)
			{
				throw Error.TypeMustNotBeByRef("type");
			}
			return new TypeBinaryExpression(expression, type, ExpressionType.TypeIs);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.TypeBinaryExpression" /> that compares run-time type identity.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="T:System.Linq.Expressions.Expression" /> property equal to.</param>
		/// <param name="type">A <see cref="P:System.Linq.Expressions.Expression.Type" /> to set the <see cref="P:System.Linq.Expressions.TypeBinaryExpression.TypeOperand" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.TypeBinaryExpression" /> for which the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property is equal to <see cref="M:System.Linq.Expressions.Expression.TypeEqual(System.Linq.Expressions.Expression,System.Type)" /> and for which the <see cref="T:System.Linq.Expressions.Expression" /> and <see cref="P:System.Linq.Expressions.TypeBinaryExpression.TypeOperand" /> properties are set to the specified values.</returns>
		// Token: 0x06000E51 RID: 3665 RVA: 0x000334F8 File Offset: 0x000316F8
		public static TypeBinaryExpression TypeEqual(Expression expression, Type type)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (type.IsByRef)
			{
				throw Error.TypeMustNotBeByRef("type");
			}
			return new TypeBinaryExpression(expression, type, ExpressionType.TypeEqual);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" />, given an operand, by calling the appropriate factory method.</summary>
		/// <param name="unaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of unary operation.</param>
		/// <param name="operand">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the operand.</param>
		/// <param name="type">The <see cref="T:System.Type" /> that specifies the type to be converted to (pass <see langword="null" /> if not applicable).</param>
		/// <returns>The <see cref="T:System.Linq.Expressions.UnaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="operand" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="unaryType" /> does not correspond to a unary expression node.</exception>
		// Token: 0x06000E52 RID: 3666 RVA: 0x0003352C File Offset: 0x0003172C
		public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type)
		{
			return Expression.MakeUnary(unaryType, operand, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" />, given an operand and implementing method, by calling the appropriate factory method.</summary>
		/// <param name="unaryType">The <see cref="T:System.Linq.Expressions.ExpressionType" /> that specifies the type of unary operation.</param>
		/// <param name="operand">An <see cref="T:System.Linq.Expressions.Expression" /> that represents the operand.</param>
		/// <param name="type">The <see cref="T:System.Type" /> that specifies the type to be converted to (pass <see langword="null" /> if not applicable).</param>
		/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>The <see cref="T:System.Linq.Expressions.UnaryExpression" /> that results from calling the appropriate factory method.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="operand" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="unaryType" /> does not correspond to a unary expression node.</exception>
		// Token: 0x06000E53 RID: 3667 RVA: 0x00033538 File Offset: 0x00031738
		public static UnaryExpression MakeUnary(ExpressionType unaryType, Expression operand, Type type, MethodInfo method)
		{
			if (unaryType <= ExpressionType.Quote)
			{
				if (unaryType <= ExpressionType.Convert)
				{
					if (unaryType == ExpressionType.ArrayLength)
					{
						return Expression.ArrayLength(operand);
					}
					if (unaryType == ExpressionType.Convert)
					{
						return Expression.Convert(operand, type, method);
					}
				}
				else
				{
					if (unaryType == ExpressionType.ConvertChecked)
					{
						return Expression.ConvertChecked(operand, type, method);
					}
					switch (unaryType)
					{
					case ExpressionType.Negate:
						return Expression.Negate(operand, method);
					case ExpressionType.UnaryPlus:
						return Expression.UnaryPlus(operand, method);
					case ExpressionType.NegateChecked:
						return Expression.NegateChecked(operand, method);
					case ExpressionType.New:
					case ExpressionType.NewArrayInit:
					case ExpressionType.NewArrayBounds:
						break;
					case ExpressionType.Not:
						return Expression.Not(operand, method);
					default:
						if (unaryType == ExpressionType.Quote)
						{
							return Expression.Quote(operand);
						}
						break;
					}
				}
			}
			else if (unaryType <= ExpressionType.Increment)
			{
				if (unaryType == ExpressionType.TypeAs)
				{
					return Expression.TypeAs(operand, type);
				}
				if (unaryType == ExpressionType.Decrement)
				{
					return Expression.Decrement(operand, method);
				}
				if (unaryType == ExpressionType.Increment)
				{
					return Expression.Increment(operand, method);
				}
			}
			else
			{
				if (unaryType == ExpressionType.Throw)
				{
					return Expression.Throw(operand, type);
				}
				if (unaryType == ExpressionType.Unbox)
				{
					return Expression.Unbox(operand, type);
				}
				switch (unaryType)
				{
				case ExpressionType.PreIncrementAssign:
					return Expression.PreIncrementAssign(operand, method);
				case ExpressionType.PreDecrementAssign:
					return Expression.PreDecrementAssign(operand, method);
				case ExpressionType.PostIncrementAssign:
					return Expression.PostIncrementAssign(operand, method);
				case ExpressionType.PostDecrementAssign:
					return Expression.PostDecrementAssign(operand, method);
				case ExpressionType.OnesComplement:
					return Expression.OnesComplement(operand, method);
				case ExpressionType.IsTrue:
					return Expression.IsTrue(operand, method);
				case ExpressionType.IsFalse:
					return Expression.IsFalse(operand, method);
				}
			}
			throw Error.UnhandledUnary(unaryType, "unaryType");
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000336AC File Offset: 0x000318AC
		private static UnaryExpression GetUserDefinedUnaryOperatorOrThrow(ExpressionType unaryType, string name, Expression operand)
		{
			UnaryExpression userDefinedUnaryOperator = Expression.GetUserDefinedUnaryOperator(unaryType, name, operand);
			if (userDefinedUnaryOperator != null)
			{
				Expression.ValidateParamswithOperandsOrThrow(userDefinedUnaryOperator.Method.GetParametersCached()[0].ParameterType, operand.Type, unaryType, name);
				return userDefinedUnaryOperator;
			}
			throw Error.UnaryOperatorNotDefined(unaryType, operand.Type);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000336F8 File Offset: 0x000318F8
		private static UnaryExpression GetUserDefinedUnaryOperator(ExpressionType unaryType, string name, Expression operand)
		{
			Type type = operand.Type;
			Type[] array = new Type[]
			{
				type
			};
			Type nonNullableType = type.GetNonNullableType();
			MethodInfo anyStaticMethodValidated = nonNullableType.GetAnyStaticMethodValidated(name, array);
			if (anyStaticMethodValidated != null)
			{
				return new UnaryExpression(unaryType, operand, anyStaticMethodValidated.ReturnType, anyStaticMethodValidated);
			}
			if (type.IsNullableType())
			{
				array[0] = nonNullableType;
				anyStaticMethodValidated = nonNullableType.GetAnyStaticMethodValidated(name, array);
				if (anyStaticMethodValidated != null && anyStaticMethodValidated.ReturnType.IsValueType && !anyStaticMethodValidated.ReturnType.IsNullableType())
				{
					return new UnaryExpression(unaryType, operand, anyStaticMethodValidated.ReturnType.GetNullableType(), anyStaticMethodValidated);
				}
			}
			return null;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003378C File Offset: 0x0003198C
		private static UnaryExpression GetMethodBasedUnaryOperator(ExpressionType unaryType, Expression operand, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], operand.Type))
			{
				Expression.ValidateParamswithOperandsOrThrow(parametersCached[0].ParameterType, operand.Type, unaryType, method.Name);
				return new UnaryExpression(unaryType, operand, method.ReturnType, method);
			}
			if (operand.Type.IsNullableType() && Expression.ParameterIsAssignable(parametersCached[0], operand.Type.GetNonNullableType()) && method.ReturnType.IsValueType && !method.ReturnType.IsNullableType())
			{
				return new UnaryExpression(unaryType, operand, method.ReturnType.GetNullableType(), method);
			}
			throw Error.OperandTypesDoNotMatchParameters(unaryType, method.Name);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00033854 File Offset: 0x00031A54
		private static UnaryExpression GetUserDefinedCoercionOrThrow(ExpressionType coercionType, Expression expression, Type convertToType)
		{
			UnaryExpression userDefinedCoercion = Expression.GetUserDefinedCoercion(coercionType, expression, convertToType);
			if (userDefinedCoercion != null)
			{
				return userDefinedCoercion;
			}
			throw Error.CoercionOperatorNotDefined(expression.Type, convertToType);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003387C File Offset: 0x00031A7C
		private static UnaryExpression GetUserDefinedCoercion(ExpressionType coercionType, Expression expression, Type convertToType)
		{
			MethodInfo userDefinedCoercionMethod = TypeUtils.GetUserDefinedCoercionMethod(expression.Type, convertToType);
			if (userDefinedCoercionMethod != null)
			{
				return new UnaryExpression(coercionType, expression, convertToType, userDefinedCoercionMethod);
			}
			return null;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000338AC File Offset: 0x00031AAC
		private static UnaryExpression GetMethodBasedCoercionOperator(ExpressionType unaryType, Expression operand, Type convertToType, MethodInfo method)
		{
			Expression.ValidateOperator(method);
			ParameterInfo[] parametersCached = method.GetParametersCached();
			if (parametersCached.Length != 1)
			{
				throw Error.IncorrectNumberOfMethodCallArguments(method, "method");
			}
			if (Expression.ParameterIsAssignable(parametersCached[0], operand.Type) && TypeUtils.AreEquivalent(method.ReturnType, convertToType))
			{
				return new UnaryExpression(unaryType, operand, method.ReturnType, method);
			}
			if ((operand.Type.IsNullableType() || convertToType.IsNullableType()) && Expression.ParameterIsAssignable(parametersCached[0], operand.Type.GetNonNullableType()) && (TypeUtils.AreEquivalent(method.ReturnType, convertToType.GetNonNullableType()) || TypeUtils.AreEquivalent(method.ReturnType, convertToType)))
			{
				return new UnaryExpression(unaryType, operand, convertToType, method);
			}
			throw Error.OperandTypesDoNotMatchParameters(unaryType, method.Name);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an arithmetic negation operation.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Negate" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The unary minus operator is not defined for <paramref name="expression" />.Type.</exception>
		// Token: 0x06000E5A RID: 3674 RVA: 0x0003396B File Offset: 0x00031B6B
		public static UnaryExpression Negate(Expression expression)
		{
			return Expression.Negate(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an arithmetic negation operation.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Negate" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the unary minus operator is not defined for <paramref name="expression" />.Type.-or-
		///         <paramref name="expression" />.Type (or its corresponding non-nullable type if it is a nullable value type) is not assignable to the argument type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000E5B RID: 3675 RVA: 0x00033974 File Offset: 0x00031B74
		public static UnaryExpression Negate(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Negate, expression, method);
			}
			if (expression.Type.IsArithmetic() && !expression.Type.IsUnsignedInt())
			{
				return new UnaryExpression(ExpressionType.Negate, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Negate, "op_UnaryNegation", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a unary plus operation.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.UnaryPlus" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The unary plus operator is not defined for <paramref name="expression" />.Type.</exception>
		// Token: 0x06000E5C RID: 3676 RVA: 0x000339D6 File Offset: 0x00031BD6
		public static UnaryExpression UnaryPlus(Expression expression)
		{
			return Expression.UnaryPlus(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a unary plus operation.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.UnaryPlus" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the unary plus operator is not defined for <paramref name="expression" />.Type.-or-
		///         <paramref name="expression" />.Type (or its corresponding non-nullable type if it is a nullable value type) is not assignable to the argument type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000E5D RID: 3677 RVA: 0x000339E0 File Offset: 0x00031BE0
		public static UnaryExpression UnaryPlus(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.UnaryPlus, expression, method);
			}
			if (expression.Type.IsArithmetic())
			{
				return new UnaryExpression(ExpressionType.UnaryPlus, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.UnaryPlus, "op_UnaryPlus", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an arithmetic negation operation that has overflow checking.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NegateChecked" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The unary minus operator is not defined for <paramref name="expression" />.Type.</exception>
		// Token: 0x06000E5E RID: 3678 RVA: 0x00033A35 File Offset: 0x00031C35
		public static UnaryExpression NegateChecked(Expression expression)
		{
			return Expression.NegateChecked(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an arithmetic negation operation that has overflow checking. The implementing method can be specified.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.NegateChecked" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the unary minus operator is not defined for <paramref name="expression" />.Type.-or-
		///         <paramref name="expression" />.Type (or its corresponding non-nullable type if it is a nullable value type) is not assignable to the argument type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000E5F RID: 3679 RVA: 0x00033A40 File Offset: 0x00031C40
		public static UnaryExpression NegateChecked(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.NegateChecked, expression, method);
			}
			if (expression.Type.IsArithmetic() && !expression.Type.IsUnsignedInt())
			{
				return new UnaryExpression(ExpressionType.NegateChecked, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.NegateChecked, "op_UnaryNegation", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a bitwise complement operation.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Not" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The unary not operator is not defined for <paramref name="expression" />.Type.</exception>
		// Token: 0x06000E60 RID: 3680 RVA: 0x00033AA2 File Offset: 0x00031CA2
		public static UnaryExpression Not(Expression expression)
		{
			return Expression.Not(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a bitwise complement operation. The implementing method can be specified.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Not" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="method" /> is <see langword="null" /> and the unary not operator is not defined for <paramref name="expression" />.Type.-or-
		///         <paramref name="expression" />.Type (or its corresponding non-nullable type if it is a nullable value type) is not assignable to the argument type of the method represented by <paramref name="method" />.</exception>
		// Token: 0x06000E61 RID: 3681 RVA: 0x00033AAC File Offset: 0x00031CAC
		public static UnaryExpression Not(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Not, expression, method);
			}
			if (expression.Type.IsIntegerOrBool())
			{
				return new UnaryExpression(ExpressionType.Not, expression, expression.Type, null);
			}
			UnaryExpression userDefinedUnaryOperator = Expression.GetUserDefinedUnaryOperator(ExpressionType.Not, "op_LogicalNot", expression);
			if (userDefinedUnaryOperator != null)
			{
				return userDefinedUnaryOperator;
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Not, "op_OnesComplement", expression);
		}

		/// <summary>Returns whether the expression evaluates to false.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to evaluate.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		// Token: 0x06000E62 RID: 3682 RVA: 0x00033B14 File Offset: 0x00031D14
		public static UnaryExpression IsFalse(Expression expression)
		{
			return Expression.IsFalse(expression, null);
		}

		/// <summary>Returns whether the expression evaluates to false.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to evaluate.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		// Token: 0x06000E63 RID: 3683 RVA: 0x00033B20 File Offset: 0x00031D20
		public static UnaryExpression IsFalse(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.IsFalse, expression, method);
			}
			if (expression.Type.IsBool())
			{
				return new UnaryExpression(ExpressionType.IsFalse, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.IsFalse, "op_False", expression);
		}

		/// <summary>Returns whether the expression evaluates to true.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to evaluate.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		// Token: 0x06000E64 RID: 3684 RVA: 0x00033B75 File Offset: 0x00031D75
		public static UnaryExpression IsTrue(Expression expression)
		{
			return Expression.IsTrue(expression, null);
		}

		/// <summary>Returns whether the expression evaluates to true.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to evaluate.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		// Token: 0x06000E65 RID: 3685 RVA: 0x00033B80 File Offset: 0x00031D80
		public static UnaryExpression IsTrue(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.IsTrue, expression, method);
			}
			if (expression.Type.IsBool())
			{
				return new UnaryExpression(ExpressionType.IsTrue, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.IsTrue, "op_True", expression);
		}

		/// <summary>Returns the expression representing the ones complement.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" />.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		// Token: 0x06000E66 RID: 3686 RVA: 0x00033BD5 File Offset: 0x00031DD5
		public static UnaryExpression OnesComplement(Expression expression)
		{
			return Expression.OnesComplement(expression, null);
		}

		/// <summary>Returns the expression representing the ones complement.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" />.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		// Token: 0x06000E67 RID: 3687 RVA: 0x00033BE0 File Offset: 0x00031DE0
		public static UnaryExpression OnesComplement(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.OnesComplement, expression, method);
			}
			if (expression.Type.IsInteger())
			{
				return new UnaryExpression(ExpressionType.OnesComplement, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.OnesComplement, "op_OnesComplement", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an explicit reference or boxing conversion where <see langword="null" /> is supplied if the conversion fails.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.TypeAs" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06000E68 RID: 3688 RVA: 0x00033C38 File Offset: 0x00031E38
		public static UnaryExpression TypeAs(Expression expression, Type type)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (type.IsValueType && !type.IsNullableType())
			{
				throw Error.IncorrectTypeForTypeAs(type, "type");
			}
			return new UnaryExpression(ExpressionType.TypeAs, expression, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an explicit unboxing.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to unbox.</param>
		/// <param name="type">The new <see cref="T:System.Type" /> of the expression.</param>
		/// <returns>An instance of <see cref="T:System.Linq.Expressions.UnaryExpression" />.</returns>
		// Token: 0x06000E69 RID: 3689 RVA: 0x00033C8C File Offset: 0x00031E8C
		public static UnaryExpression Unbox(Expression expression, Type type)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			if (!expression.Type.IsInterface && expression.Type != typeof(object))
			{
				throw Error.InvalidUnboxType("expression");
			}
			if (!type.IsValueType)
			{
				throw Error.InvalidUnboxType("type");
			}
			TypeUtils.ValidateType(type, "type");
			return new UnaryExpression(ExpressionType.Unbox, expression, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a type conversion operation.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Convert" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">No conversion operator is defined between <paramref name="expression" />.Type and <paramref name="type" />.</exception>
		// Token: 0x06000E6A RID: 3690 RVA: 0x00033D06 File Offset: 0x00031F06
		public static UnaryExpression Convert(Expression expression, Type type)
		{
			return Expression.Convert(expression, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a conversion operation for which the implementing method is specified.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Convert" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" />, <see cref="P:System.Linq.Expressions.Expression.Type" />, and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">No conversion operator is defined between <paramref name="expression" />.Type and <paramref name="type" />.-or-
		///         <paramref name="expression" />.Type is not assignable to the argument type of the method represented by <paramref name="method" />.-or-The return type of the method represented by <paramref name="method" /> is not assignable to <paramref name="type" />.-or-
		///         <paramref name="expression" />.Type or <paramref name="type" /> is a nullable value type and the corresponding non-nullable value type does not equal the argument type or the return type, respectively, of the method represented by <paramref name="method" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method that matches the <paramref name="method" /> description was found.</exception>
		// Token: 0x06000E6B RID: 3691 RVA: 0x00033D10 File Offset: 0x00031F10
		public static UnaryExpression Convert(Expression expression, Type type, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (!(method == null))
			{
				return Expression.GetMethodBasedCoercionOperator(ExpressionType.Convert, expression, type, method);
			}
			if (expression.Type.HasIdentityPrimitiveOrNullableConversionTo(type) || expression.Type.HasReferenceConversionTo(type))
			{
				return new UnaryExpression(ExpressionType.Convert, expression, type, null);
			}
			return Expression.GetUserDefinedCoercionOrThrow(ExpressionType.Convert, expression, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a conversion operation that throws an exception if the target type is overflowed.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ConvertChecked" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> and <see cref="P:System.Linq.Expressions.Expression.Type" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">No conversion operator is defined between <paramref name="expression" />.Type and <paramref name="type" />.</exception>
		// Token: 0x06000E6C RID: 3692 RVA: 0x00033D82 File Offset: 0x00031F82
		public static UnaryExpression ConvertChecked(Expression expression, Type type)
		{
			return Expression.ConvertChecked(expression, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a conversion operation that throws an exception if the target type is overflowed and for which the implementing method is specified.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <param name="type">A <see cref="T:System.Type" /> to set the <see cref="P:System.Linq.Expressions.Expression.Type" /> property equal to.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ConvertChecked" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" />, <see cref="P:System.Linq.Expressions.Expression.Type" />, and <see cref="P:System.Linq.Expressions.UnaryExpression.Method" /> properties set to the specified values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> or <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="method" /> is not <see langword="null" /> and the method it represents returns <see langword="void" />, is not <see langword="static" /> (<see langword="Shared" /> in Visual Basic), or does not take exactly one argument.</exception>
		/// <exception cref="T:System.InvalidOperationException">No conversion operator is defined between <paramref name="expression" />.Type and <paramref name="type" />.-or-
		///         <paramref name="expression" />.Type is not assignable to the argument type of the method represented by <paramref name="method" />.-or-The return type of the method represented by <paramref name="method" /> is not assignable to <paramref name="type" />.-or-
		///         <paramref name="expression" />.Type or <paramref name="type" /> is a nullable value type and the corresponding non-nullable value type does not equal the argument type or the return type, respectively, of the method represented by <paramref name="method" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method that matches the <paramref name="method" /> description was found.</exception>
		// Token: 0x06000E6D RID: 3693 RVA: 0x00033D8C File Offset: 0x00031F8C
		public static UnaryExpression ConvertChecked(Expression expression, Type type, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (!(method == null))
			{
				return Expression.GetMethodBasedCoercionOperator(ExpressionType.ConvertChecked, expression, type, method);
			}
			if (expression.Type.HasIdentityPrimitiveOrNullableConversionTo(type))
			{
				return new UnaryExpression(ExpressionType.ConvertChecked, expression, type, null);
			}
			if (expression.Type.HasReferenceConversionTo(type))
			{
				return new UnaryExpression(ExpressionType.Convert, expression, type, null);
			}
			return Expression.GetUserDefinedCoercionOrThrow(ExpressionType.ConvertChecked, expression, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an expression for obtaining the length of a one-dimensional array.</summary>
		/// <param name="array">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.ArrayLength" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to <paramref name="array" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="array" />.Type does not represent an array type.</exception>
		// Token: 0x06000E6E RID: 3694 RVA: 0x00033E0C File Offset: 0x0003200C
		public static UnaryExpression ArrayLength(Expression array)
		{
			ExpressionUtils.RequiresCanRead(array, "array");
			if (array.Type.IsSZArray)
			{
				return new UnaryExpression(ExpressionType.ArrayLength, array, typeof(int), null);
			}
			if (!array.Type.IsArray || !typeof(Array).IsAssignableFrom(array.Type))
			{
				throw Error.ArgumentMustBeArray("array");
			}
			throw Error.ArgumentMustBeSingleDimensionalArrayType("array");
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents an expression that has a constant value of type <see cref="T:System.Linq.Expressions.Expression" />.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to set the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property equal to.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that has the <see cref="P:System.Linq.Expressions.Expression.NodeType" /> property equal to <see cref="F:System.Linq.Expressions.ExpressionType.Quote" /> and the <see cref="P:System.Linq.Expressions.UnaryExpression.Operand" /> property set to the specified value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="expression" /> is <see langword="null" />.</exception>
		// Token: 0x06000E6F RID: 3695 RVA: 0x00033E80 File Offset: 0x00032080
		public static UnaryExpression Quote(Expression expression)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			LambdaExpression lambdaExpression = expression as LambdaExpression;
			if (lambdaExpression == null)
			{
				throw Error.QuotedExpressionMustBeLambda("expression");
			}
			return new UnaryExpression(ExpressionType.Quote, lambdaExpression, lambdaExpression.PublicType, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a rethrowing of an exception.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a rethrowing of an exception.</returns>
		// Token: 0x06000E70 RID: 3696 RVA: 0x00033EBC File Offset: 0x000320BC
		public static UnaryExpression Rethrow()
		{
			return Expression.Throw(null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a rethrowing of an exception with a given type.</summary>
		/// <param name="type">The new <see cref="T:System.Type" /> of the expression.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a rethrowing of an exception.</returns>
		// Token: 0x06000E71 RID: 3697 RVA: 0x00033EC4 File Offset: 0x000320C4
		public static UnaryExpression Rethrow(Type type)
		{
			return Expression.Throw(null, type);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a throwing of an exception.</summary>
		/// <param name="value">An <see cref="T:System.Linq.Expressions.Expression" />.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the exception.</returns>
		// Token: 0x06000E72 RID: 3698 RVA: 0x00033ECD File Offset: 0x000320CD
		public static UnaryExpression Throw(Expression value)
		{
			return Expression.Throw(value, typeof(void));
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents a throwing of an exception with a given type.</summary>
		/// <param name="value">An <see cref="T:System.Linq.Expressions.Expression" />.</param>
		/// <param name="type">The new <see cref="T:System.Type" /> of the expression.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the exception.</returns>
		// Token: 0x06000E73 RID: 3699 RVA: 0x00033EE0 File Offset: 0x000320E0
		public static UnaryExpression Throw(Expression value, Type type)
		{
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type");
			if (value != null)
			{
				ExpressionUtils.RequiresCanRead(value, "value");
				if (value.Type.IsValueType)
				{
					throw Error.ArgumentMustNotHaveValueType("value");
				}
			}
			return new UnaryExpression(ExpressionType.Throw, value, type, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the incrementing of the expression value by 1.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to increment.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the incremented expression.</returns>
		// Token: 0x06000E74 RID: 3700 RVA: 0x00033F33 File Offset: 0x00032133
		public static UnaryExpression Increment(Expression expression)
		{
			return Expression.Increment(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the incrementing of the expression by 1.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to increment.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the incremented expression.</returns>
		// Token: 0x06000E75 RID: 3701 RVA: 0x00033F3C File Offset: 0x0003213C
		public static UnaryExpression Increment(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Increment, expression, method);
			}
			if (expression.Type.IsArithmetic())
			{
				return new UnaryExpression(ExpressionType.Increment, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Increment, "op_Increment", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the decrementing of the expression by 1.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to decrement.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the decremented expression.</returns>
		// Token: 0x06000E76 RID: 3702 RVA: 0x00033F91 File Offset: 0x00032191
		public static UnaryExpression Decrement(Expression expression)
		{
			return Expression.Decrement(expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the decrementing of the expression by 1.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to decrement.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the decremented expression.</returns>
		// Token: 0x06000E77 RID: 3703 RVA: 0x00033F9C File Offset: 0x0003219C
		public static UnaryExpression Decrement(Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			if (!(method == null))
			{
				return Expression.GetMethodBasedUnaryOperator(ExpressionType.Decrement, expression, method);
			}
			if (expression.Type.IsArithmetic())
			{
				return new UnaryExpression(ExpressionType.Decrement, expression, expression.Type, null);
			}
			return Expression.GetUserDefinedUnaryOperatorOrThrow(ExpressionType.Decrement, "op_Decrement", expression);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that increments the expression by 1 and assigns the result back to the expression.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		// Token: 0x06000E78 RID: 3704 RVA: 0x00033FF1 File Offset: 0x000321F1
		public static UnaryExpression PreIncrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreIncrementAssign, expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that increments the expression by 1 and assigns the result back to the expression.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		// Token: 0x06000E79 RID: 3705 RVA: 0x00033FFC File Offset: 0x000321FC
		public static UnaryExpression PreIncrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreIncrementAssign, expression, method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that decrements the expression by 1 and assigns the result back to the expression.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		// Token: 0x06000E7A RID: 3706 RVA: 0x00034007 File Offset: 0x00032207
		public static UnaryExpression PreDecrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreDecrementAssign, expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that decrements the expression by 1 and assigns the result back to the expression.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		// Token: 0x06000E7B RID: 3707 RVA: 0x00034012 File Offset: 0x00032212
		public static UnaryExpression PreDecrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PreDecrementAssign, expression, method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the assignment of the expression followed by a subsequent increment by 1 of the original expression.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		// Token: 0x06000E7C RID: 3708 RVA: 0x0003401D File Offset: 0x0003221D
		public static UnaryExpression PostIncrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostIncrementAssign, expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the assignment of the expression followed by a subsequent increment by 1 of the original expression.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		// Token: 0x06000E7D RID: 3709 RVA: 0x00034028 File Offset: 0x00032228
		public static UnaryExpression PostIncrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostIncrementAssign, expression, method);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the assignment of the expression followed by a subsequent decrement by 1 of the original expression.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		// Token: 0x06000E7E RID: 3710 RVA: 0x00034033 File Offset: 0x00032233
		public static UnaryExpression PostDecrementAssign(Expression expression)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostDecrementAssign, expression, null);
		}

		/// <summary>Creates a <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the assignment of the expression followed by a subsequent decrement by 1 of the original expression.</summary>
		/// <param name="expression">An <see cref="T:System.Linq.Expressions.Expression" /> to apply the operations on.</param>
		/// <param name="method">A <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</param>
		/// <returns>A <see cref="T:System.Linq.Expressions.UnaryExpression" /> that represents the resultant expression.</returns>
		// Token: 0x06000E7F RID: 3711 RVA: 0x0003403E File Offset: 0x0003223E
		public static UnaryExpression PostDecrementAssign(Expression expression, MethodInfo method)
		{
			return Expression.MakeOpAssignUnary(ExpressionType.PostDecrementAssign, expression, method);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003404C File Offset: 0x0003224C
		private static UnaryExpression MakeOpAssignUnary(ExpressionType kind, Expression expression, MethodInfo method)
		{
			ExpressionUtils.RequiresCanRead(expression, "expression");
			Expression.RequiresCanWrite(expression, "expression");
			UnaryExpression unaryExpression;
			if (method == null)
			{
				if (expression.Type.IsArithmetic())
				{
					return new UnaryExpression(kind, expression, expression.Type, null);
				}
				string name;
				if (kind == ExpressionType.PreIncrementAssign || kind == ExpressionType.PostIncrementAssign)
				{
					name = "op_Increment";
				}
				else
				{
					name = "op_Decrement";
				}
				unaryExpression = Expression.GetUserDefinedUnaryOperatorOrThrow(kind, name, expression);
			}
			else
			{
				unaryExpression = Expression.GetMethodBasedUnaryOperator(kind, expression, method);
			}
			if (!TypeUtils.AreReferenceAssignable(expression.Type, unaryExpression.Type))
			{
				throw Error.UserDefinedOpMustHaveValidReturnType(kind, method.Name);
			}
			return unaryExpression;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000340E6 File Offset: 0x000322E6
		// Note: this type is marked as 'beforefieldinit'.
		static Expression()
		{
		}

		// Token: 0x0400091B RID: 2331
		private static readonly CacheDict<Type, MethodInfo> s_lambdaDelegateCache = new CacheDict<Type, MethodInfo>(40);

		// Token: 0x0400091C RID: 2332
		private static volatile CacheDict<Type, Func<Expression, string, bool, ReadOnlyCollection<ParameterExpression>, LambdaExpression>> s_lambdaFactories;

		// Token: 0x0400091D RID: 2333
		private static ConditionalWeakTable<Expression, Expression.ExtensionInfo> s_legacyCtorSupportTable;

		// Token: 0x02000211 RID: 529
		internal class BinaryExpressionProxy
		{
			// Token: 0x06000E82 RID: 3714 RVA: 0x000340F4 File Offset: 0x000322F4
			public BinaryExpressionProxy(BinaryExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170001CE RID: 462
			// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0003410E File Offset: 0x0003230E
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0003411B File Offset: 0x0003231B
			public LambdaExpression Conversion
			{
				get
				{
					return this._node.Conversion;
				}
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00034128 File Offset: 0x00032328
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00034135 File Offset: 0x00032335
			public bool IsLifted
			{
				get
				{
					return this._node.IsLifted;
				}
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00034142 File Offset: 0x00032342
			public bool IsLiftedToNull
			{
				get
				{
					return this._node.IsLiftedToNull;
				}
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06000E88 RID: 3720 RVA: 0x0003414F File Offset: 0x0003234F
			public Expression Left
			{
				get
				{
					return this._node.Left;
				}
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0003415C File Offset: 0x0003235C
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00034169 File Offset: 0x00032369
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00034176 File Offset: 0x00032376
			public Expression Right
			{
				get
				{
					return this._node.Right;
				}
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00034183 File Offset: 0x00032383
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400091E RID: 2334
			private readonly BinaryExpression _node;
		}

		// Token: 0x02000212 RID: 530
		internal class BlockExpressionProxy
		{
			// Token: 0x06000E8D RID: 3725 RVA: 0x00034190 File Offset: 0x00032390
			public BlockExpressionProxy(BlockExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x06000E8E RID: 3726 RVA: 0x000341AA File Offset: 0x000323AA
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x06000E8F RID: 3727 RVA: 0x000341B7 File Offset: 0x000323B7
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000E90 RID: 3728 RVA: 0x000341C4 File Offset: 0x000323C4
			public ReadOnlyCollection<Expression> Expressions
			{
				get
				{
					return this._node.Expressions;
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000E91 RID: 3729 RVA: 0x000341D1 File Offset: 0x000323D1
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000E92 RID: 3730 RVA: 0x000341DE File Offset: 0x000323DE
			public Expression Result
			{
				get
				{
					return this._node.Result;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000E93 RID: 3731 RVA: 0x000341EB File Offset: 0x000323EB
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000E94 RID: 3732 RVA: 0x000341F8 File Offset: 0x000323F8
			public ReadOnlyCollection<ParameterExpression> Variables
			{
				get
				{
					return this._node.Variables;
				}
			}

			// Token: 0x0400091F RID: 2335
			private readonly BlockExpression _node;
		}

		// Token: 0x02000213 RID: 531
		internal class CatchBlockProxy
		{
			// Token: 0x06000E95 RID: 3733 RVA: 0x00034205 File Offset: 0x00032405
			public CatchBlockProxy(CatchBlock node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0003421F File Offset: 0x0003241F
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0003422C File Offset: 0x0003242C
			public Expression Filter
			{
				get
				{
					return this._node.Filter;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00034239 File Offset: 0x00032439
			public Type Test
			{
				get
				{
					return this._node.Test;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00034246 File Offset: 0x00032446
			public ParameterExpression Variable
			{
				get
				{
					return this._node.Variable;
				}
			}

			// Token: 0x04000920 RID: 2336
			private readonly CatchBlock _node;
		}

		// Token: 0x02000214 RID: 532
		internal class ConditionalExpressionProxy
		{
			// Token: 0x06000E9A RID: 3738 RVA: 0x00034253 File Offset: 0x00032453
			public ConditionalExpressionProxy(ConditionalExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0003426D File Offset: 0x0003246D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000E9C RID: 3740 RVA: 0x0003427A File Offset: 0x0003247A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00034287 File Offset: 0x00032487
			public Expression IfFalse
			{
				get
				{
					return this._node.IfFalse;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00034294 File Offset: 0x00032494
			public Expression IfTrue
			{
				get
				{
					return this._node.IfTrue;
				}
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000E9F RID: 3743 RVA: 0x000342A1 File Offset: 0x000324A1
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x000342AE File Offset: 0x000324AE
			public Expression Test
			{
				get
				{
					return this._node.Test;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x000342BB File Offset: 0x000324BB
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000921 RID: 2337
			private readonly ConditionalExpression _node;
		}

		// Token: 0x02000215 RID: 533
		internal class ConstantExpressionProxy
		{
			// Token: 0x06000EA2 RID: 3746 RVA: 0x000342C8 File Offset: 0x000324C8
			public ConstantExpressionProxy(ConstantExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x000342E2 File Offset: 0x000324E2
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x000342EF File Offset: 0x000324EF
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x000342FC File Offset: 0x000324FC
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00034309 File Offset: 0x00032509
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00034316 File Offset: 0x00032516
			public object Value
			{
				get
				{
					return this._node.Value;
				}
			}

			// Token: 0x04000922 RID: 2338
			private readonly ConstantExpression _node;
		}

		// Token: 0x02000216 RID: 534
		internal class DebugInfoExpressionProxy
		{
			// Token: 0x06000EA8 RID: 3752 RVA: 0x00034323 File Offset: 0x00032523
			public DebugInfoExpressionProxy(DebugInfoExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0003433D File Offset: 0x0003253D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0003434A File Offset: 0x0003254A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00034357 File Offset: 0x00032557
			public SymbolDocumentInfo Document
			{
				get
				{
					return this._node.Document;
				}
			}

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00034364 File Offset: 0x00032564
			public int EndColumn
			{
				get
				{
					return this._node.EndColumn;
				}
			}

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x06000EAD RID: 3757 RVA: 0x00034371 File Offset: 0x00032571
			public int EndLine
			{
				get
				{
					return this._node.EndLine;
				}
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0003437E File Offset: 0x0003257E
			public bool IsClear
			{
				get
				{
					return this._node.IsClear;
				}
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0003438B File Offset: 0x0003258B
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00034398 File Offset: 0x00032598
			public int StartColumn
			{
				get
				{
					return this._node.StartColumn;
				}
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x000343A5 File Offset: 0x000325A5
			public int StartLine
			{
				get
				{
					return this._node.StartLine;
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x000343B2 File Offset: 0x000325B2
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000923 RID: 2339
			private readonly DebugInfoExpression _node;
		}

		// Token: 0x02000217 RID: 535
		internal class DefaultExpressionProxy
		{
			// Token: 0x06000EB3 RID: 3763 RVA: 0x000343BF File Offset: 0x000325BF
			public DefaultExpressionProxy(DefaultExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x000343D9 File Offset: 0x000325D9
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x000343E6 File Offset: 0x000325E6
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x000343F3 File Offset: 0x000325F3
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00034400 File Offset: 0x00032600
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000924 RID: 2340
			private readonly DefaultExpression _node;
		}

		// Token: 0x02000218 RID: 536
		internal class GotoExpressionProxy
		{
			// Token: 0x06000EB8 RID: 3768 RVA: 0x0003440D File Offset: 0x0003260D
			public GotoExpressionProxy(GotoExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00034427 File Offset: 0x00032627
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00034434 File Offset: 0x00032634
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00034441 File Offset: 0x00032641
			public GotoExpressionKind Kind
			{
				get
				{
					return this._node.Kind;
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0003444E File Offset: 0x0003264E
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0003445B File Offset: 0x0003265B
			public LabelTarget Target
			{
				get
				{
					return this._node.Target;
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00034468 File Offset: 0x00032668
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x17000203 RID: 515
			// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00034475 File Offset: 0x00032675
			public Expression Value
			{
				get
				{
					return this._node.Value;
				}
			}

			// Token: 0x04000925 RID: 2341
			private readonly GotoExpression _node;
		}

		// Token: 0x02000219 RID: 537
		internal class IndexExpressionProxy
		{
			// Token: 0x06000EC0 RID: 3776 RVA: 0x00034482 File Offset: 0x00032682
			public IndexExpressionProxy(IndexExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000204 RID: 516
			// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0003449C File Offset: 0x0003269C
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x17000205 RID: 517
			// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000344A9 File Offset: 0x000326A9
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000206 RID: 518
			// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x000344B6 File Offset: 0x000326B6
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000207 RID: 519
			// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x000344C3 File Offset: 0x000326C3
			public PropertyInfo Indexer
			{
				get
				{
					return this._node.Indexer;
				}
			}

			// Token: 0x17000208 RID: 520
			// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000344D0 File Offset: 0x000326D0
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000209 RID: 521
			// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x000344DD File Offset: 0x000326DD
			public Expression Object
			{
				get
				{
					return this._node.Object;
				}
			}

			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x000344EA File Offset: 0x000326EA
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000926 RID: 2342
			private readonly IndexExpression _node;
		}

		// Token: 0x0200021A RID: 538
		internal class InvocationExpressionProxy
		{
			// Token: 0x06000EC8 RID: 3784 RVA: 0x000344F7 File Offset: 0x000326F7
			public InvocationExpressionProxy(InvocationExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00034511 File Offset: 0x00032711
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0003451E File Offset: 0x0003271E
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0003452B File Offset: 0x0003272B
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00034538 File Offset: 0x00032738
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00034545 File Offset: 0x00032745
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00034552 File Offset: 0x00032752
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000927 RID: 2343
			private readonly InvocationExpression _node;
		}

		// Token: 0x0200021B RID: 539
		internal class LabelExpressionProxy
		{
			// Token: 0x06000ECF RID: 3791 RVA: 0x0003455F File Offset: 0x0003275F
			public LabelExpressionProxy(LabelExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00034579 File Offset: 0x00032779
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00034586 File Offset: 0x00032786
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00034593 File Offset: 0x00032793
			public Expression DefaultValue
			{
				get
				{
					return this._node.DefaultValue;
				}
			}

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x000345A0 File Offset: 0x000327A0
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x000345AD File Offset: 0x000327AD
			public LabelTarget Target
			{
				get
				{
					return this._node.Target;
				}
			}

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x000345BA File Offset: 0x000327BA
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000928 RID: 2344
			private readonly LabelExpression _node;
		}

		// Token: 0x0200021C RID: 540
		internal class LambdaExpressionProxy
		{
			// Token: 0x06000ED6 RID: 3798 RVA: 0x000345C7 File Offset: 0x000327C7
			public LambdaExpressionProxy(LambdaExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x000345E1 File Offset: 0x000327E1
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000218 RID: 536
			// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x000345EE File Offset: 0x000327EE
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000219 RID: 537
			// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x000345FB File Offset: 0x000327FB
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00034608 File Offset: 0x00032808
			public string Name
			{
				get
				{
					return this._node.Name;
				}
			}

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00034615 File Offset: 0x00032815
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00034622 File Offset: 0x00032822
			public ReadOnlyCollection<ParameterExpression> Parameters
			{
				get
				{
					return this._node.Parameters;
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003462F File Offset: 0x0003282F
			public Type ReturnType
			{
				get
				{
					return this._node.ReturnType;
				}
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0003463C File Offset: 0x0003283C
			public bool TailCall
			{
				get
				{
					return this._node.TailCall;
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00034649 File Offset: 0x00032849
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000929 RID: 2345
			private readonly LambdaExpression _node;
		}

		// Token: 0x0200021D RID: 541
		internal class ListInitExpressionProxy
		{
			// Token: 0x06000EE0 RID: 3808 RVA: 0x00034656 File Offset: 0x00032856
			public ListInitExpressionProxy(ListInitExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00034670 File Offset: 0x00032870
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000221 RID: 545
			// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0003467D File Offset: 0x0003287D
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000222 RID: 546
			// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0003468A File Offset: 0x0003288A
			public ReadOnlyCollection<ElementInit> Initializers
			{
				get
				{
					return this._node.Initializers;
				}
			}

			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00034697 File Offset: 0x00032897
			public NewExpression NewExpression
			{
				get
				{
					return this._node.NewExpression;
				}
			}

			// Token: 0x17000224 RID: 548
			// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000346A4 File Offset: 0x000328A4
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000225 RID: 549
			// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x000346B1 File Offset: 0x000328B1
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400092A RID: 2346
			private readonly ListInitExpression _node;
		}

		// Token: 0x0200021E RID: 542
		internal class LoopExpressionProxy
		{
			// Token: 0x06000EE7 RID: 3815 RVA: 0x000346BE File Offset: 0x000328BE
			public LoopExpressionProxy(LoopExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x000346D8 File Offset: 0x000328D8
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x000346E5 File Offset: 0x000328E5
			public LabelTarget BreakLabel
			{
				get
				{
					return this._node.BreakLabel;
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06000EEA RID: 3818 RVA: 0x000346F2 File Offset: 0x000328F2
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x06000EEB RID: 3819 RVA: 0x000346FF File Offset: 0x000328FF
			public LabelTarget ContinueLabel
			{
				get
				{
					return this._node.ContinueLabel;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0003470C File Offset: 0x0003290C
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x06000EED RID: 3821 RVA: 0x00034719 File Offset: 0x00032919
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00034726 File Offset: 0x00032926
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400092B RID: 2347
			private readonly LoopExpression _node;
		}

		// Token: 0x0200021F RID: 543
		internal class MemberExpressionProxy
		{
			// Token: 0x06000EEF RID: 3823 RVA: 0x00034733 File Offset: 0x00032933
			public MemberExpressionProxy(MemberExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0003474D File Offset: 0x0003294D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0003475A File Offset: 0x0003295A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700022F RID: 559
			// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00034767 File Offset: 0x00032967
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x17000230 RID: 560
			// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00034774 File Offset: 0x00032974
			public MemberInfo Member
			{
				get
				{
					return this._node.Member;
				}
			}

			// Token: 0x17000231 RID: 561
			// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00034781 File Offset: 0x00032981
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000232 RID: 562
			// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003478E File Offset: 0x0003298E
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400092C RID: 2348
			private readonly MemberExpression _node;
		}

		// Token: 0x02000220 RID: 544
		internal class MemberInitExpressionProxy
		{
			// Token: 0x06000EF6 RID: 3830 RVA: 0x0003479B File Offset: 0x0003299B
			public MemberInitExpressionProxy(MemberInitExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000233 RID: 563
			// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x000347B5 File Offset: 0x000329B5
			public ReadOnlyCollection<MemberBinding> Bindings
			{
				get
				{
					return this._node.Bindings;
				}
			}

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x000347C2 File Offset: 0x000329C2
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000347CF File Offset: 0x000329CF
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000EFA RID: 3834 RVA: 0x000347DC File Offset: 0x000329DC
			public NewExpression NewExpression
			{
				get
				{
					return this._node.NewExpression;
				}
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000EFB RID: 3835 RVA: 0x000347E9 File Offset: 0x000329E9
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000347F6 File Offset: 0x000329F6
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400092D RID: 2349
			private readonly MemberInitExpression _node;
		}

		// Token: 0x02000221 RID: 545
		internal class MethodCallExpressionProxy
		{
			// Token: 0x06000EFD RID: 3837 RVA: 0x00034803 File Offset: 0x00032A03
			public MethodCallExpressionProxy(MethodCallExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0003481D File Offset: 0x00032A1D
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003482A File Offset: 0x00032A2A
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00034837 File Offset: 0x00032A37
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00034844 File Offset: 0x00032A44
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00034851 File Offset: 0x00032A51
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0003485E File Offset: 0x00032A5E
			public Expression Object
			{
				get
				{
					return this._node.Object;
				}
			}

			// Token: 0x1700023F RID: 575
			// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0003486B File Offset: 0x00032A6B
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400092E RID: 2350
			private readonly MethodCallExpression _node;
		}

		// Token: 0x02000222 RID: 546
		internal class NewArrayExpressionProxy
		{
			// Token: 0x06000F05 RID: 3845 RVA: 0x00034878 File Offset: 0x00032A78
			public NewArrayExpressionProxy(NewArrayExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000240 RID: 576
			// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00034892 File Offset: 0x00032A92
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000241 RID: 577
			// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0003489F File Offset: 0x00032A9F
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000242 RID: 578
			// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000348AC File Offset: 0x00032AAC
			public ReadOnlyCollection<Expression> Expressions
			{
				get
				{
					return this._node.Expressions;
				}
			}

			// Token: 0x17000243 RID: 579
			// (get) Token: 0x06000F09 RID: 3849 RVA: 0x000348B9 File Offset: 0x00032AB9
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000244 RID: 580
			// (get) Token: 0x06000F0A RID: 3850 RVA: 0x000348C6 File Offset: 0x00032AC6
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x0400092F RID: 2351
			private readonly NewArrayExpression _node;
		}

		// Token: 0x02000223 RID: 547
		internal class NewExpressionProxy
		{
			// Token: 0x06000F0B RID: 3851 RVA: 0x000348D3 File Offset: 0x00032AD3
			public NewExpressionProxy(NewExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000245 RID: 581
			// (get) Token: 0x06000F0C RID: 3852 RVA: 0x000348ED File Offset: 0x00032AED
			public ReadOnlyCollection<Expression> Arguments
			{
				get
				{
					return this._node.Arguments;
				}
			}

			// Token: 0x17000246 RID: 582
			// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000348FA File Offset: 0x00032AFA
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000247 RID: 583
			// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00034907 File Offset: 0x00032B07
			public ConstructorInfo Constructor
			{
				get
				{
					return this._node.Constructor;
				}
			}

			// Token: 0x17000248 RID: 584
			// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00034914 File Offset: 0x00032B14
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000249 RID: 585
			// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00034921 File Offset: 0x00032B21
			public ReadOnlyCollection<MemberInfo> Members
			{
				get
				{
					return this._node.Members;
				}
			}

			// Token: 0x1700024A RID: 586
			// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0003492E File Offset: 0x00032B2E
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0003493B File Offset: 0x00032B3B
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000930 RID: 2352
			private readonly NewExpression _node;
		}

		// Token: 0x02000224 RID: 548
		internal class ParameterExpressionProxy
		{
			// Token: 0x06000F13 RID: 3859 RVA: 0x00034948 File Offset: 0x00032B48
			public ParameterExpressionProxy(ParameterExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00034962 File Offset: 0x00032B62
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700024D RID: 589
			// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0003496F File Offset: 0x00032B6F
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700024E RID: 590
			// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0003497C File Offset: 0x00032B7C
			public bool IsByRef
			{
				get
				{
					return this._node.IsByRef;
				}
			}

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00034989 File Offset: 0x00032B89
			public string Name
			{
				get
				{
					return this._node.Name;
				}
			}

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00034996 File Offset: 0x00032B96
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x06000F19 RID: 3865 RVA: 0x000349A3 File Offset: 0x00032BA3
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000931 RID: 2353
			private readonly ParameterExpression _node;
		}

		// Token: 0x02000225 RID: 549
		internal class RuntimeVariablesExpressionProxy
		{
			// Token: 0x06000F1A RID: 3866 RVA: 0x000349B0 File Offset: 0x00032BB0
			public RuntimeVariablesExpressionProxy(RuntimeVariablesExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000252 RID: 594
			// (get) Token: 0x06000F1B RID: 3867 RVA: 0x000349CA File Offset: 0x00032BCA
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06000F1C RID: 3868 RVA: 0x000349D7 File Offset: 0x00032BD7
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000F1D RID: 3869 RVA: 0x000349E4 File Offset: 0x00032BE4
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000F1E RID: 3870 RVA: 0x000349F1 File Offset: 0x00032BF1
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000349FE File Offset: 0x00032BFE
			public ReadOnlyCollection<ParameterExpression> Variables
			{
				get
				{
					return this._node.Variables;
				}
			}

			// Token: 0x04000932 RID: 2354
			private readonly RuntimeVariablesExpression _node;
		}

		// Token: 0x02000226 RID: 550
		internal class SwitchCaseProxy
		{
			// Token: 0x06000F20 RID: 3872 RVA: 0x00034A0B File Offset: 0x00032C0B
			public SwitchCaseProxy(SwitchCase node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00034A25 File Offset: 0x00032C25
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00034A32 File Offset: 0x00032C32
			public ReadOnlyCollection<Expression> TestValues
			{
				get
				{
					return this._node.TestValues;
				}
			}

			// Token: 0x04000933 RID: 2355
			private readonly SwitchCase _node;
		}

		// Token: 0x02000227 RID: 551
		internal class SwitchExpressionProxy
		{
			// Token: 0x06000F23 RID: 3875 RVA: 0x00034A3F File Offset: 0x00032C3F
			public SwitchExpressionProxy(SwitchExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00034A59 File Offset: 0x00032C59
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00034A66 File Offset: 0x00032C66
			public ReadOnlyCollection<SwitchCase> Cases
			{
				get
				{
					return this._node.Cases;
				}
			}

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00034A73 File Offset: 0x00032C73
			public MethodInfo Comparison
			{
				get
				{
					return this._node.Comparison;
				}
			}

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00034A80 File Offset: 0x00032C80
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700025D RID: 605
			// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00034A8D File Offset: 0x00032C8D
			public Expression DefaultBody
			{
				get
				{
					return this._node.DefaultBody;
				}
			}

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00034A9A File Offset: 0x00032C9A
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00034AA7 File Offset: 0x00032CA7
			public Expression SwitchValue
			{
				get
				{
					return this._node.SwitchValue;
				}
			}

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00034AB4 File Offset: 0x00032CB4
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000934 RID: 2356
			private readonly SwitchExpression _node;
		}

		// Token: 0x02000228 RID: 552
		internal class TryExpressionProxy
		{
			// Token: 0x06000F2C RID: 3884 RVA: 0x00034AC1 File Offset: 0x00032CC1
			public TryExpressionProxy(TryExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00034ADB File Offset: 0x00032CDB
			public Expression Body
			{
				get
				{
					return this._node.Body;
				}
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00034AE8 File Offset: 0x00032CE8
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00034AF5 File Offset: 0x00032CF5
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00034B02 File Offset: 0x00032D02
			public Expression Fault
			{
				get
				{
					return this._node.Fault;
				}
			}

			// Token: 0x17000265 RID: 613
			// (get) Token: 0x06000F31 RID: 3889 RVA: 0x00034B0F File Offset: 0x00032D0F
			public Expression Finally
			{
				get
				{
					return this._node.Finally;
				}
			}

			// Token: 0x17000266 RID: 614
			// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00034B1C File Offset: 0x00032D1C
			public ReadOnlyCollection<CatchBlock> Handlers
			{
				get
				{
					return this._node.Handlers;
				}
			}

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00034B29 File Offset: 0x00032D29
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000268 RID: 616
			// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00034B36 File Offset: 0x00032D36
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000935 RID: 2357
			private readonly TryExpression _node;
		}

		// Token: 0x02000229 RID: 553
		internal class TypeBinaryExpressionProxy
		{
			// Token: 0x06000F35 RID: 3893 RVA: 0x00034B43 File Offset: 0x00032D43
			public TypeBinaryExpressionProxy(TypeBinaryExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00034B5D File Offset: 0x00032D5D
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00034B6A File Offset: 0x00032D6A
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00034B77 File Offset: 0x00032D77
			public Expression Expression
			{
				get
				{
					return this._node.Expression;
				}
			}

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00034B84 File Offset: 0x00032D84
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x1700026D RID: 621
			// (get) Token: 0x06000F3A RID: 3898 RVA: 0x00034B91 File Offset: 0x00032D91
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00034B9E File Offset: 0x00032D9E
			public Type TypeOperand
			{
				get
				{
					return this._node.TypeOperand;
				}
			}

			// Token: 0x04000936 RID: 2358
			private readonly TypeBinaryExpression _node;
		}

		// Token: 0x0200022A RID: 554
		internal class UnaryExpressionProxy
		{
			// Token: 0x06000F3C RID: 3900 RVA: 0x00034BAB File Offset: 0x00032DAB
			public UnaryExpressionProxy(UnaryExpression node)
			{
				ContractUtils.RequiresNotNull(node, "node");
				this._node = node;
			}

			// Token: 0x1700026F RID: 623
			// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00034BC5 File Offset: 0x00032DC5
			public bool CanReduce
			{
				get
				{
					return this._node.CanReduce;
				}
			}

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00034BD2 File Offset: 0x00032DD2
			public string DebugView
			{
				get
				{
					return this._node.DebugView;
				}
			}

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00034BDF File Offset: 0x00032DDF
			public bool IsLifted
			{
				get
				{
					return this._node.IsLifted;
				}
			}

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00034BEC File Offset: 0x00032DEC
			public bool IsLiftedToNull
			{
				get
				{
					return this._node.IsLiftedToNull;
				}
			}

			// Token: 0x17000273 RID: 627
			// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00034BF9 File Offset: 0x00032DF9
			public MethodInfo Method
			{
				get
				{
					return this._node.Method;
				}
			}

			// Token: 0x17000274 RID: 628
			// (get) Token: 0x06000F42 RID: 3906 RVA: 0x00034C06 File Offset: 0x00032E06
			public ExpressionType NodeType
			{
				get
				{
					return this._node.NodeType;
				}
			}

			// Token: 0x17000275 RID: 629
			// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00034C13 File Offset: 0x00032E13
			public Expression Operand
			{
				get
				{
					return this._node.Operand;
				}
			}

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00034C20 File Offset: 0x00032E20
			public Type Type
			{
				get
				{
					return this._node.Type;
				}
			}

			// Token: 0x04000937 RID: 2359
			private readonly UnaryExpression _node;
		}

		// Token: 0x0200022B RID: 555
		private class ExtensionInfo
		{
			// Token: 0x06000F45 RID: 3909 RVA: 0x00034C2D File Offset: 0x00032E2D
			public ExtensionInfo(ExpressionType nodeType, Type type)
			{
				this.NodeType = nodeType;
				this.Type = type;
			}

			// Token: 0x04000938 RID: 2360
			internal readonly ExpressionType NodeType;

			// Token: 0x04000939 RID: 2361
			internal readonly Type Type;
		}

		// Token: 0x0200022C RID: 556
		private enum TryGetFuncActionArgsResult
		{
			// Token: 0x0400093B RID: 2363
			Valid,
			// Token: 0x0400093C RID: 2364
			ArgumentNull,
			// Token: 0x0400093D RID: 2365
			ByRef,
			// Token: 0x0400093E RID: 2366
			PointerOrVoid
		}

		// Token: 0x0200022D RID: 557
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000F46 RID: 3910 RVA: 0x00034C43 File Offset: 0x00032E43
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000F47 RID: 3911 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x00034C4F File Offset: 0x00032E4F
			internal bool <New>b__358_0(ConstructorInfo c)
			{
				return c.GetParametersCached().Length == 0;
			}

			// Token: 0x0400093F RID: 2367
			public static readonly Expression.<>c <>9 = new Expression.<>c();

			// Token: 0x04000940 RID: 2368
			public static Func<ConstructorInfo, bool> <>9__358_0;
		}
	}
}
