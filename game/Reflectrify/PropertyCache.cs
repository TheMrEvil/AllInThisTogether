using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectrify
{
	// Token: 0x0200000A RID: 10
	public class PropertyCache : ICache
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000024F8 File Offset: 0x000006F8
		public void Clear()
		{
			this.cache.Clear();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002508 File Offset: 0x00000708
		public PropertyInfo GetProperty(Type type, string name, BindingFlags flags)
		{
			ValueTuple<Type, string, BindingFlags> key = new ValueTuple<Type, string, BindingFlags>(type, name, flags);
			PropertyInfo property;
			if (this.cache.TryGetValue(key, out property))
			{
				return property;
			}
			property = type.GetProperty(name, flags);
			this.cache.TryAdd(key, property);
			return property;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002549 File Offset: 0x00000749
		public PropertyCache()
		{
		}

		// Token: 0x04000008 RID: 8
		private ConcurrentDictionary<ValueTuple<Type, string, BindingFlags>, PropertyInfo> cache = new ConcurrentDictionary<ValueTuple<Type, string, BindingFlags>, PropertyInfo>();
	}
}
