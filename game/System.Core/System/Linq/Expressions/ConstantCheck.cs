using System;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200023D RID: 573
	internal static class ConstantCheck
	{
		// Token: 0x06000FAB RID: 4011 RVA: 0x000356B0 File Offset: 0x000338B0
		internal static bool IsNull(Expression e)
		{
			ExpressionType nodeType = e.NodeType;
			if (nodeType != ExpressionType.Constant)
			{
				return nodeType == ExpressionType.Default && e.Type.IsNullableOrReferenceType();
			}
			return ((ConstantExpression)e).Value == null;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000356EC File Offset: 0x000338EC
		internal static AnalyzeTypeIsResult AnalyzeTypeIs(TypeBinaryExpression typeIs)
		{
			return ConstantCheck.AnalyzeTypeIs(typeIs.Expression, typeIs.TypeOperand);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00035700 File Offset: 0x00033900
		private static AnalyzeTypeIsResult AnalyzeTypeIs(Expression operand, Type testType)
		{
			Type type = operand.Type;
			if (type == typeof(void))
			{
				if (!(testType == typeof(void)))
				{
					return AnalyzeTypeIsResult.KnownFalse;
				}
				return AnalyzeTypeIsResult.KnownTrue;
			}
			else
			{
				if (testType == typeof(void) || testType.IsPointer)
				{
					return AnalyzeTypeIsResult.KnownFalse;
				}
				Type nonNullableType = type.GetNonNullableType();
				if (!testType.GetNonNullableType().IsAssignableFrom(nonNullableType))
				{
					return AnalyzeTypeIsResult.Unknown;
				}
				if (type.IsValueType && !type.IsNullableType())
				{
					return AnalyzeTypeIsResult.KnownTrue;
				}
				return AnalyzeTypeIsResult.KnownAssignable;
			}
		}
	}
}
