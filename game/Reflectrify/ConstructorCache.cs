using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectrify
{
	// Token: 0x02000006 RID: 6
	public class ConstructorCache : ICache
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000023B2 File Offset: 0x000005B2
		public void Clear()
		{
			this.cache.Clear();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023C0 File Offset: 0x000005C0
		public ConstructorInfo GetConstructor(Type type, Type[] parameterTypes)
		{
			ValueTuple<Type, Type[]> key = new ValueTuple<Type, Type[]>(type, parameterTypes);
			ConstructorInfo constructor;
			if (this.cache.TryGetValue(key, out constructor))
			{
				return constructor;
			}
			constructor = type.GetConstructor(parameterTypes);
			this.cache.TryAdd(key, constructor);
			return constructor;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023FF File Offset: 0x000005FF
		public ConstructorCache()
		{
		}

		// Token: 0x04000005 RID: 5
		private ConcurrentDictionary<ValueTuple<Type, Type[]>, ConstructorInfo> cache = new ConcurrentDictionary<ValueTuple<Type, Type[]>, ConstructorInfo>();
	}
}
