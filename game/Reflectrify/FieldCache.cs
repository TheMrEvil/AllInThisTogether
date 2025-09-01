using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Reflectrify
{
	// Token: 0x02000007 RID: 7
	public class FieldCache : ICache
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002412 File Offset: 0x00000612
		public void Clear()
		{
			this.cache.Clear();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002420 File Offset: 0x00000620
		public FieldInfo GetField(Type type, string name, BindingFlags flags)
		{
			ValueTuple<Type, string, BindingFlags> key = new ValueTuple<Type, string, BindingFlags>(type, name, flags);
			FieldInfo field;
			if (this.cache.TryGetValue(key, out field))
			{
				return field;
			}
			field = type.GetField(name, flags);
			this.cache.TryAdd(key, field);
			return field;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002461 File Offset: 0x00000661
		public FieldCache()
		{
		}

		// Token: 0x04000006 RID: 6
		private ConcurrentDictionary<ValueTuple<Type, string, BindingFlags>, FieldInfo> cache = new ConcurrentDictionary<ValueTuple<Type, string, BindingFlags>, FieldInfo>();
	}
}
