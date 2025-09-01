using System;

namespace System.Collections
{
	// Token: 0x02000494 RID: 1172
	internal static class HashtableExtensions
	{
		// Token: 0x06002564 RID: 9572 RVA: 0x0008338B File Offset: 0x0008158B
		public static bool TryGetValue<T>(this Hashtable table, object key, out T value)
		{
			if (table.ContainsKey(key))
			{
				value = (T)((object)table[key]);
				return true;
			}
			value = default(T);
			return false;
		}
	}
}
