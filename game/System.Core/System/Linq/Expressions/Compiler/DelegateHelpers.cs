using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002B3 RID: 691
	internal static class DelegateHelpers
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x0003F264 File Offset: 0x0003D464
		internal static Type MakeDelegateType(Type[] types)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			Type delegateType;
			lock (delegateCache)
			{
				DelegateHelpers.TypeInfo typeInfo = DelegateHelpers._DelegateCache;
				for (int i = 0; i < types.Length; i++)
				{
					typeInfo = DelegateHelpers.NextTypeInfo(types[i], typeInfo);
				}
				if (typeInfo.DelegateType == null)
				{
					typeInfo.DelegateType = DelegateHelpers.MakeNewDelegate((Type[])types.Clone());
				}
				delegateType = typeInfo.DelegateType;
			}
			return delegateType;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0003F2EC File Offset: 0x0003D4EC
		internal static DelegateHelpers.TypeInfo NextTypeInfo(Type initialArg)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			DelegateHelpers.TypeInfo result;
			lock (delegateCache)
			{
				result = DelegateHelpers.NextTypeInfo(initialArg, DelegateHelpers._DelegateCache);
			}
			return result;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0003F334 File Offset: 0x0003D534
		internal static DelegateHelpers.TypeInfo GetNextTypeInfo(Type initialArg, DelegateHelpers.TypeInfo curTypeInfo)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			DelegateHelpers.TypeInfo result;
			lock (delegateCache)
			{
				result = DelegateHelpers.NextTypeInfo(initialArg, curTypeInfo);
			}
			return result;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0003F378 File Offset: 0x0003D578
		private static DelegateHelpers.TypeInfo NextTypeInfo(Type initialArg, DelegateHelpers.TypeInfo curTypeInfo)
		{
			if (curTypeInfo.TypeChain == null)
			{
				curTypeInfo.TypeChain = new Dictionary<Type, DelegateHelpers.TypeInfo>();
			}
			DelegateHelpers.TypeInfo typeInfo;
			if (!curTypeInfo.TypeChain.TryGetValue(initialArg, out typeInfo))
			{
				typeInfo = new DelegateHelpers.TypeInfo();
				if (!initialArg.IsCollectible)
				{
					curTypeInfo.TypeChain[initialArg] = typeInfo;
				}
			}
			return typeInfo;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0003F3C8 File Offset: 0x0003D5C8
		internal static Type MakeNewDelegate(Type[] types)
		{
			bool flag;
			if (types.Length > 17)
			{
				flag = true;
			}
			else
			{
				flag = false;
				foreach (Type type in types)
				{
					if (type.IsByRef || type.IsPointer)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				return DelegateHelpers.MakeNewCustomDelegate(types);
			}
			Type result;
			if (types[types.Length - 1] == typeof(void))
			{
				result = DelegateHelpers.GetActionType(types.RemoveLast<Type>());
			}
			else
			{
				result = DelegateHelpers.GetFuncType(types);
			}
			return result;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0003F440 File Offset: 0x0003D640
		internal static Type GetFuncType(Type[] types)
		{
			switch (types.Length)
			{
			case 1:
				return typeof(Func<>).MakeGenericType(types);
			case 2:
				return typeof(Func<, >).MakeGenericType(types);
			case 3:
				return typeof(Func<, , >).MakeGenericType(types);
			case 4:
				return typeof(Func<, , , >).MakeGenericType(types);
			case 5:
				return typeof(Func<, , , , >).MakeGenericType(types);
			case 6:
				return typeof(Func<, , , , , >).MakeGenericType(types);
			case 7:
				return typeof(Func<, , , , , , >).MakeGenericType(types);
			case 8:
				return typeof(Func<, , , , , , , >).MakeGenericType(types);
			case 9:
				return typeof(Func<, , , , , , , , >).MakeGenericType(types);
			case 10:
				return typeof(Func<, , , , , , , , , >).MakeGenericType(types);
			case 11:
				return typeof(Func<, , , , , , , , , , >).MakeGenericType(types);
			case 12:
				return typeof(Func<, , , , , , , , , , , >).MakeGenericType(types);
			case 13:
				return typeof(Func<, , , , , , , , , , , , >).MakeGenericType(types);
			case 14:
				return typeof(Func<, , , , , , , , , , , , , >).MakeGenericType(types);
			case 15:
				return typeof(Func<, , , , , , , , , , , , , , >).MakeGenericType(types);
			case 16:
				return typeof(Func<, , , , , , , , , , , , , , , >).MakeGenericType(types);
			case 17:
				return typeof(Func<, , , , , , , , , , , , , , , , >).MakeGenericType(types);
			default:
				return null;
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0003F5C4 File Offset: 0x0003D7C4
		internal static Type GetActionType(Type[] types)
		{
			switch (types.Length)
			{
			case 0:
				return typeof(Action);
			case 1:
				return typeof(Action<>).MakeGenericType(types);
			case 2:
				return typeof(Action<, >).MakeGenericType(types);
			case 3:
				return typeof(Action<, , >).MakeGenericType(types);
			case 4:
				return typeof(Action<, , , >).MakeGenericType(types);
			case 5:
				return typeof(Action<, , , , >).MakeGenericType(types);
			case 6:
				return typeof(Action<, , , , , >).MakeGenericType(types);
			case 7:
				return typeof(Action<, , , , , , >).MakeGenericType(types);
			case 8:
				return typeof(Action<, , , , , , , >).MakeGenericType(types);
			case 9:
				return typeof(Action<, , , , , , , , >).MakeGenericType(types);
			case 10:
				return typeof(Action<, , , , , , , , , >).MakeGenericType(types);
			case 11:
				return typeof(Action<, , , , , , , , , , >).MakeGenericType(types);
			case 12:
				return typeof(Action<, , , , , , , , , , , >).MakeGenericType(types);
			case 13:
				return typeof(Action<, , , , , , , , , , , , >).MakeGenericType(types);
			case 14:
				return typeof(Action<, , , , , , , , , , , , , >).MakeGenericType(types);
			case 15:
				return typeof(Action<, , , , , , , , , , , , , , >).MakeGenericType(types);
			case 16:
				return typeof(Action<, , , , , , , , , , , , , , , >).MakeGenericType(types);
			default:
				return null;
			}
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0003F740 File Offset: 0x0003D940
		internal static Type MakeCallSiteDelegate(ReadOnlyCollection<Expression> types, Type returnType)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			Type delegateType;
			lock (delegateCache)
			{
				DelegateHelpers.TypeInfo typeInfo = DelegateHelpers._DelegateCache;
				typeInfo = DelegateHelpers.NextTypeInfo(typeof(CallSite), typeInfo);
				for (int i = 0; i < types.Count; i++)
				{
					typeInfo = DelegateHelpers.NextTypeInfo(types[i].Type, typeInfo);
				}
				typeInfo = DelegateHelpers.NextTypeInfo(returnType, typeInfo);
				if (typeInfo.DelegateType == null)
				{
					typeInfo.MakeDelegateType(returnType, types);
				}
				delegateType = typeInfo.DelegateType;
			}
			return delegateType;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0003F7E0 File Offset: 0x0003D9E0
		internal static Type MakeDeferredSiteDelegate(DynamicMetaObject[] args, Type returnType)
		{
			DelegateHelpers.TypeInfo delegateCache = DelegateHelpers._DelegateCache;
			Type delegateType;
			lock (delegateCache)
			{
				DelegateHelpers.TypeInfo typeInfo = DelegateHelpers._DelegateCache;
				typeInfo = DelegateHelpers.NextTypeInfo(typeof(CallSite), typeInfo);
				foreach (DynamicMetaObject dynamicMetaObject in args)
				{
					Type type = dynamicMetaObject.Expression.Type;
					if (DelegateHelpers.IsByRef(dynamicMetaObject))
					{
						type = type.MakeByRefType();
					}
					typeInfo = DelegateHelpers.NextTypeInfo(type, typeInfo);
				}
				typeInfo = DelegateHelpers.NextTypeInfo(returnType, typeInfo);
				if (typeInfo.DelegateType == null)
				{
					Type[] array = new Type[args.Length + 2];
					array[0] = typeof(CallSite);
					array[array.Length - 1] = returnType;
					for (int j = 0; j < args.Length; j++)
					{
						DynamicMetaObject dynamicMetaObject2 = args[j];
						Type type2 = dynamicMetaObject2.Expression.Type;
						if (DelegateHelpers.IsByRef(dynamicMetaObject2))
						{
							type2 = type2.MakeByRefType();
						}
						array[j + 1] = type2;
					}
					typeInfo.DelegateType = DelegateHelpers.MakeNewDelegate(array);
				}
				delegateType = typeInfo.DelegateType;
			}
			return delegateType;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0003F8F4 File Offset: 0x0003DAF4
		private static bool IsByRef(DynamicMetaObject mo)
		{
			ParameterExpression parameterExpression = mo.Expression as ParameterExpression;
			return parameterExpression != null && parameterExpression.IsByRef;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0003F918 File Offset: 0x0003DB18
		private static Type MakeNewCustomDelegate(Type[] types)
		{
			Type returnType = types[types.Length - 1];
			Type[] parameterTypes = types.RemoveLast<Type>();
			TypeBuilder typeBuilder = AssemblyGen.DefineDelegateType("Delegate" + types.Length.ToString());
			typeBuilder.DefineConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName, CallingConventions.Standard, DelegateHelpers.s_delegateCtorSignature).SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			typeBuilder.DefineMethod("Invoke", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, returnType, parameterTypes).SetImplementationFlags(MethodImplAttributes.CodeTypeMask);
			return typeBuilder.CreateTypeInfo();
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0003F982 File Offset: 0x0003DB82
		// Note: this type is marked as 'beforefieldinit'.
		static DelegateHelpers()
		{
		}

		// Token: 0x04000AB8 RID: 2744
		private static DelegateHelpers.TypeInfo _DelegateCache = new DelegateHelpers.TypeInfo();

		// Token: 0x04000AB9 RID: 2745
		private const int MaximumArity = 17;

		// Token: 0x04000ABA RID: 2746
		private const MethodAttributes CtorAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName;

		// Token: 0x04000ABB RID: 2747
		private const MethodImplAttributes ImplAttributes = MethodImplAttributes.CodeTypeMask;

		// Token: 0x04000ABC RID: 2748
		private const MethodAttributes InvokeAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask;

		// Token: 0x04000ABD RID: 2749
		private static readonly Type[] s_delegateCtorSignature = new Type[]
		{
			typeof(object),
			typeof(IntPtr)
		};

		// Token: 0x020002B4 RID: 692
		internal class TypeInfo
		{
			// Token: 0x06001487 RID: 5255 RVA: 0x00002162 File Offset: 0x00000362
			public TypeInfo()
			{
			}

			// Token: 0x04000ABE RID: 2750
			public Type DelegateType;

			// Token: 0x04000ABF RID: 2751
			public Dictionary<Type, DelegateHelpers.TypeInfo> TypeChain;
		}
	}
}
