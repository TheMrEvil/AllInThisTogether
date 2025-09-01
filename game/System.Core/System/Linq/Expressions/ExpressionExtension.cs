using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions.Compiler;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000254 RID: 596
	internal static class ExpressionExtension
	{
		// Token: 0x06001078 RID: 4216 RVA: 0x000375C2 File Offset: 0x000357C2
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, params Expression[] arguments)
		{
			return ExpressionExtension.MakeDynamic(delegateType, binder, arguments);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00037B28 File Offset: 0x00035D28
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, IEnumerable<Expression> arguments)
		{
			IReadOnlyList<Expression> readOnlyList = (arguments as IReadOnlyList<Expression>) ?? arguments.ToReadOnly<Expression>();
			switch (readOnlyList.Count)
			{
			case 1:
				return ExpressionExtension.MakeDynamic(delegateType, binder, readOnlyList[0]);
			case 2:
				return ExpressionExtension.MakeDynamic(delegateType, binder, readOnlyList[0], readOnlyList[1]);
			case 3:
				return ExpressionExtension.MakeDynamic(delegateType, binder, readOnlyList[0], readOnlyList[1], readOnlyList[2]);
			case 4:
				return ExpressionExtension.MakeDynamic(delegateType, binder, readOnlyList[0], readOnlyList[1], readOnlyList[2], readOnlyList[3]);
			default:
			{
				ContractUtils.RequiresNotNull(delegateType, "delegateType");
				ContractUtils.RequiresNotNull(binder, "binder");
				if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
				{
					throw Error.TypeMustBeDerivedFromSystemDelegate();
				}
				MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
				ReadOnlyCollection<Expression> arguments2 = arguments.ToReadOnly<Expression>();
				ExpressionUtils.ValidateArgumentTypes(validMethodForDynamic, ExpressionType.Dynamic, ref arguments2, "delegateType");
				return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arguments2);
			}
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00037C24 File Offset: 0x00035E24
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			ExpressionUtils.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 2, parametersCached);
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1], "delegateType", "arg0", -1);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x00037CA8 File Offset: 0x00035EA8
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			ExpressionUtils.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 3, parametersCached);
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1], "delegateType", "arg0", -1);
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2], "delegateType", "arg1", -1);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00037D50 File Offset: 0x00035F50
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			ExpressionUtils.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 4, parametersCached);
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1], "delegateType", "arg0", -1);
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2], "delegateType", "arg1", -1);
			ExpressionExtension.ValidateDynamicArgument(arg2, "arg2");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg2, parametersCached[3], "delegateType", "arg2", -1);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1, arg2);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00037E20 File Offset: 0x00036020
		public static DynamicExpression MakeDynamic(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(delegateType, "delegateType");
			ContractUtils.RequiresNotNull(binder, "binder");
			if (!delegateType.IsSubclassOf(typeof(MulticastDelegate)))
			{
				throw Error.TypeMustBeDerivedFromSystemDelegate();
			}
			MethodInfo validMethodForDynamic = ExpressionExtension.GetValidMethodForDynamic(delegateType);
			ParameterInfo[] parametersCached = validMethodForDynamic.GetParametersCached();
			ExpressionUtils.ValidateArgumentCount(validMethodForDynamic, ExpressionType.Dynamic, 5, parametersCached);
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg0, parametersCached[1], "delegateType", "arg0", -1);
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg1, parametersCached[2], "delegateType", "arg1", -1);
			ExpressionExtension.ValidateDynamicArgument(arg2, "arg2");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg2, parametersCached[3], "delegateType", "arg2", -1);
			ExpressionExtension.ValidateDynamicArgument(arg3, "arg3");
			ExpressionUtils.ValidateOneArgument(validMethodForDynamic, ExpressionType.Dynamic, arg3, parametersCached[4], "delegateType", "arg3", -1);
			return DynamicExpression.Make(validMethodForDynamic.GetReturnType(), delegateType, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00037F14 File Offset: 0x00036114
		private static MethodInfo GetValidMethodForDynamic(Type delegateType)
		{
			MethodInfo invokeMethod = delegateType.GetInvokeMethod();
			ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
			if (parametersCached.Length == 0 || parametersCached[0].ParameterType != typeof(CallSite))
			{
				throw Error.FirstArgumentMustBeCallSite();
			}
			return invokeMethod;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x00037587 File Offset: 0x00035787
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, params Expression[] arguments)
		{
			return ExpressionExtension.Dynamic(binder, returnType, arguments);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00037F54 File Offset: 0x00036154
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite))));
			Type type;
			if ((type = nextTypeInfo.DelegateType) == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[]
				{
					arg0
				});
			}
			Type delegateType = type;
			return DynamicExpression.Make(returnType, delegateType, binder, arg0);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x00037FC0 File Offset: 0x000361C0
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite)))));
			Type type;
			if ((type = nextTypeInfo.DelegateType) == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[]
				{
					arg0,
					arg1
				});
			}
			Type delegateType = type;
			return DynamicExpression.Make(returnType, delegateType, binder, arg0, arg1);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x00038044 File Offset: 0x00036244
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionExtension.ValidateDynamicArgument(arg2, "arg2");
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg2.Type, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite))))));
			Type type;
			if ((type = nextTypeInfo.DelegateType) == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[]
				{
					arg0,
					arg1,
					arg2
				});
			}
			Type delegateType = type;
			return DynamicExpression.Make(returnType, delegateType, binder, arg0, arg1, arg2);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x000380E8 File Offset: 0x000362E8
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, Expression arg0, Expression arg1, Expression arg2, Expression arg3)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			ExpressionExtension.ValidateDynamicArgument(arg0, "arg0");
			ExpressionExtension.ValidateDynamicArgument(arg1, "arg1");
			ExpressionExtension.ValidateDynamicArgument(arg2, "arg2");
			ExpressionExtension.ValidateDynamicArgument(arg3, "arg3");
			DelegateHelpers.TypeInfo nextTypeInfo = DelegateHelpers.GetNextTypeInfo(returnType, DelegateHelpers.GetNextTypeInfo(arg3.Type, DelegateHelpers.GetNextTypeInfo(arg2.Type, DelegateHelpers.GetNextTypeInfo(arg1.Type, DelegateHelpers.GetNextTypeInfo(arg0.Type, DelegateHelpers.NextTypeInfo(typeof(CallSite)))))));
			Type type;
			if ((type = nextTypeInfo.DelegateType) == null)
			{
				type = nextTypeInfo.MakeDelegateType(returnType, new Expression[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				});
			}
			Type delegateType = type;
			return DynamicExpression.Make(returnType, delegateType, binder, arg0, arg1, arg2, arg3);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x000381AC File Offset: 0x000363AC
		public static DynamicExpression Dynamic(CallSiteBinder binder, Type returnType, IEnumerable<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(arguments, "arguments");
			ContractUtils.RequiresNotNull(returnType, "returnType");
			ReadOnlyCollection<Expression> readOnlyCollection = arguments.ToReadOnly<Expression>();
			ContractUtils.RequiresNotEmpty<Expression>(readOnlyCollection, "arguments");
			return ExpressionExtension.MakeDynamic(binder, returnType, readOnlyCollection);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x000381EC File Offset: 0x000363EC
		private static DynamicExpression MakeDynamic(CallSiteBinder binder, Type returnType, ReadOnlyCollection<Expression> arguments)
		{
			ContractUtils.RequiresNotNull(binder, "binder");
			int count = arguments.Count;
			for (int i = 0; i < count; i++)
			{
				ExpressionExtension.ValidateDynamicArgument(arguments[i], "arguments", i);
			}
			Type delegateType = DelegateHelpers.MakeCallSiteDelegate(arguments, returnType);
			switch (count)
			{
			case 1:
				return DynamicExpression.Make(returnType, delegateType, binder, arguments[0]);
			case 2:
				return DynamicExpression.Make(returnType, delegateType, binder, arguments[0], arguments[1]);
			case 3:
				return DynamicExpression.Make(returnType, delegateType, binder, arguments[0], arguments[1], arguments[2]);
			case 4:
				return DynamicExpression.Make(returnType, delegateType, binder, arguments[0], arguments[1], arguments[2], arguments[3]);
			default:
				return DynamicExpression.Make(returnType, delegateType, binder, arguments);
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000382BE File Offset: 0x000364BE
		private static void ValidateDynamicArgument(Expression arg, string paramName)
		{
			ExpressionExtension.ValidateDynamicArgument(arg, paramName, -1);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000382C8 File Offset: 0x000364C8
		private static void ValidateDynamicArgument(Expression arg, string paramName, int index)
		{
			ExpressionUtils.RequiresCanRead(arg, paramName, index);
			Type type = arg.Type;
			ContractUtils.RequiresNotNull(type, "type");
			TypeUtils.ValidateType(type, "type", true, true);
			if (type == typeof(void))
			{
				throw Error.ArgumentTypeCannotBeVoid();
			}
		}
	}
}
