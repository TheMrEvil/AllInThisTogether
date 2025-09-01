using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflectrify
{
	// Token: 0x02000004 RID: 4
	public class AttributeMethodCache : ICache
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020BB File Offset: 0x000002BB
		public void Clear()
		{
			this.multiAttributeCache.Clear();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020C8 File Offset: 0x000002C8
		public T GetSingleAttribute<T>(MethodInfo method, string parameterName, bool inherit) where T : Attribute
		{
			ValueTuple<MethodInfo, string, bool> key = new ValueTuple<MethodInfo, string, bool>(method, parameterName, inherit);
			Attribute[] array;
			if (!this.multiAttributeCache.TryGetValue(key, out array))
			{
				ParameterInfo parameterInfo = method.GetParameters().FirstOrDefault((ParameterInfo p) => p.Name == parameterName);
				if (parameterInfo != null)
				{
					array = parameterInfo.GetCustomAttributes(typeof(T), inherit).Cast<Attribute>().ToArray<Attribute>();
					this.multiAttributeCache.TryAdd(key, array);
				}
			}
			return ((array != null) ? array.FirstOrDefault<Attribute>() : null) as T;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000215C File Offset: 0x0000035C
		public IEnumerable<T> GetMultiAttributes<T>(MethodInfo method, string parameterName, bool inherit) where T : Attribute
		{
			ValueTuple<MethodInfo, string, bool> key = new ValueTuple<MethodInfo, string, bool>(method, parameterName, inherit);
			Attribute[] array;
			if (!this.multiAttributeCache.TryGetValue(key, out array))
			{
				ParameterInfo parameterInfo = method.GetParameters().FirstOrDefault((ParameterInfo p) => p.Name == parameterName);
				if (parameterInfo != null)
				{
					array = parameterInfo.GetCustomAttributes(typeof(T), inherit).Cast<Attribute>().ToArray<Attribute>();
					this.multiAttributeCache.TryAdd(key, array);
				}
			}
			if (array == null)
			{
				return null;
			}
			return array.Cast<T>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E5 File Offset: 0x000003E5
		public AttributeMethodCache()
		{
		}

		// Token: 0x04000003 RID: 3
		private ConcurrentDictionary<ValueTuple<MethodInfo, string, bool>, Attribute[]> multiAttributeCache = new ConcurrentDictionary<ValueTuple<MethodInfo, string, bool>, Attribute[]>();

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0<T> where T : Attribute
		{
			// Token: 0x06000046 RID: 70 RVA: 0x00003406 File Offset: 0x00001606
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06000047 RID: 71 RVA: 0x0000340E File Offset: 0x0000160E
			internal bool <GetSingleAttribute>b__0(ParameterInfo p)
			{
				return p.Name == this.parameterName;
			}

			// Token: 0x04000012 RID: 18
			public string parameterName;
		}

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0<T> where T : Attribute
		{
			// Token: 0x06000048 RID: 72 RVA: 0x00003421 File Offset: 0x00001621
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00003429 File Offset: 0x00001629
			internal bool <GetMultiAttributes>b__0(ParameterInfo p)
			{
				return p.Name == this.parameterName;
			}

			// Token: 0x04000013 RID: 19
			public string parameterName;
		}
	}
}
