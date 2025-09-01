using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000279 RID: 633
	internal class TempAssemblyCache
	{
		// Token: 0x17000459 RID: 1113
		internal TempAssembly this[string ns, object o]
		{
			get
			{
				return (TempAssembly)this.cache[new TempAssemblyCacheKey(ns, o)];
			}
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0008D084 File Offset: 0x0008B284
		internal void Add(string ns, object o, TempAssembly assembly)
		{
			TempAssemblyCacheKey key = new TempAssemblyCacheKey(ns, o);
			lock (this)
			{
				if (this.cache[key] != assembly)
				{
					Hashtable hashtable = new Hashtable();
					foreach (object key2 in this.cache.Keys)
					{
						hashtable.Add(key2, this.cache[key2]);
					}
					this.cache = hashtable;
					this.cache[key] = assembly;
				}
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0008D14C File Offset: 0x0008B34C
		public TempAssemblyCache()
		{
		}

		// Token: 0x0400189D RID: 6301
		private Hashtable cache = new Hashtable();
	}
}
