using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectrify
{
	// Token: 0x02000009 RID: 9
	public class MethodCache : ICache
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002474 File Offset: 0x00000674
		public void Clear()
		{
			this.cache.Clear();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002484 File Offset: 0x00000684
		public MethodInfo GetMethod(Type type, string name, BindingFlags flags, Type[] genericTypes = null)
		{
			ValueTuple<Type, string, Type[], BindingFlags> key = new ValueTuple<Type, string, Type[], BindingFlags>(type, name, genericTypes, flags);
			MethodInfo methodInfo;
			if (this.cache.TryGetValue(key, out methodInfo))
			{
				return methodInfo;
			}
			methodInfo = type.GetMethod(name, flags);
			if (methodInfo != null && methodInfo.IsGenericMethodDefinition && genericTypes != null)
			{
				methodInfo = methodInfo.MakeGenericMethod(genericTypes);
			}
			this.cache.TryAdd(key, methodInfo);
			return methodInfo;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024E5 File Offset: 0x000006E5
		public MethodCache()
		{
		}

		// Token: 0x04000007 RID: 7
		private ConcurrentDictionary<ValueTuple<Type, string, Type[], BindingFlags>, MethodInfo> cache = new ConcurrentDictionary<ValueTuple<Type, string, Type[], BindingFlags>, MethodInfo>();
	}
}
