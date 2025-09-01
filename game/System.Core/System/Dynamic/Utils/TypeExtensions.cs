using System;
using System.Reflection;

namespace System.Dynamic.Utils
{
	// Token: 0x0200032F RID: 815
	internal static class TypeExtensions
	{
		// Token: 0x06001895 RID: 6293 RVA: 0x00052BB8 File Offset: 0x00050DB8
		public static MethodInfo GetAnyStaticMethodValidated(this Type type, string name, Type[] types)
		{
			MethodInfo method = type.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
			if (!method.MatchesArgumentTypes(types))
			{
				return null;
			}
			return method;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00052BE0 File Offset: 0x00050DE0
		private static bool MatchesArgumentTypes(this MethodInfo mi, Type[] argTypes)
		{
			if (mi == null)
			{
				return false;
			}
			ParameterInfo[] parametersCached = mi.GetParametersCached();
			if (parametersCached.Length != argTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < parametersCached.Length; i++)
			{
				if (!TypeUtils.AreReferenceAssignable(parametersCached[i].ParameterType, argTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00052C2C File Offset: 0x00050E2C
		public static Type GetReturnType(this MethodBase mi)
		{
			if (!mi.IsConstructor)
			{
				return ((MethodInfo)mi).ReturnType;
			}
			return mi.DeclaringType;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00052C48 File Offset: 0x00050E48
		public static TypeCode GetTypeCode(this Type type)
		{
			return Type.GetTypeCode(type);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00052C50 File Offset: 0x00050E50
		internal static ParameterInfo[] GetParametersCached(this MethodBase method)
		{
			CacheDict<MethodBase, ParameterInfo[]> cacheDict = TypeExtensions.s_paramInfoCache;
			ParameterInfo[] parameters;
			if (!cacheDict.TryGetValue(method, out parameters))
			{
				parameters = method.GetParameters();
				Type declaringType = method.DeclaringType;
				if (declaringType != null && !declaringType.IsCollectible)
				{
					cacheDict[method] = parameters;
				}
			}
			return parameters;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00052C95 File Offset: 0x00050E95
		internal static bool IsByRefParameter(this ParameterInfo pi)
		{
			return pi.ParameterType.IsByRef || (pi.Attributes & ParameterAttributes.Out) == ParameterAttributes.Out;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00052CB1 File Offset: 0x00050EB1
		// Note: this type is marked as 'beforefieldinit'.
		static TypeExtensions()
		{
		}

		// Token: 0x04000BE8 RID: 3048
		private static readonly CacheDict<MethodBase, ParameterInfo[]> s_paramInfoCache = new CacheDict<MethodBase, ParameterInfo[]>(75);
	}
}
