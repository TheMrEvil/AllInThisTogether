using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AAF RID: 2735
	public static class CollectionExtensions
	{
		// Token: 0x060061D5 RID: 25045 RVA: 0x00147068 File Offset: 0x00145268
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key)
		{
			return dictionary.GetValueOrDefault(key, default(TValue));
		}

		// Token: 0x060061D6 RID: 25046 RVA: 0x00147088 File Offset: 0x00145288
		public static TValue GetValueOrDefault<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			TValue result;
			if (!dictionary.TryGetValue(key, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x001470B1 File Offset: 0x001452B1
		public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, value);
				return true;
			}
			return false;
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x001470D5 File Offset: 0x001452D5
		public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (dictionary.TryGetValue(key, out value))
			{
				dictionary.Remove(key);
				return true;
			}
			value = default(TValue);
			return false;
		}
	}
}
