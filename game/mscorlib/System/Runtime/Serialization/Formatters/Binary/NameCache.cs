using System;
using System.Collections.Concurrent;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006BC RID: 1724
	internal sealed class NameCache
	{
		// Token: 0x06003FC0 RID: 16320 RVA: 0x000DF8A8 File Offset: 0x000DDAA8
		internal object GetCachedValue(string name)
		{
			this.name = name;
			object result;
			if (!NameCache.ht.TryGetValue(name, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x000DF8CE File Offset: 0x000DDACE
		internal void SetCachedValue(object value)
		{
			NameCache.ht[this.name] = value;
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x0000259F File Offset: 0x0000079F
		public NameCache()
		{
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x000DF8E1 File Offset: 0x000DDAE1
		// Note: this type is marked as 'beforefieldinit'.
		static NameCache()
		{
		}

		// Token: 0x040029B1 RID: 10673
		private static ConcurrentDictionary<string, object> ht = new ConcurrentDictionary<string, object>();

		// Token: 0x040029B2 RID: 10674
		private string name;
	}
}
