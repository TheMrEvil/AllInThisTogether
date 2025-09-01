using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.Collections
{
	// Token: 0x02000354 RID: 852
	internal static class DictionaryExtensions
	{
		// Token: 0x06001B7B RID: 7035 RVA: 0x0007ED64 File Offset: 0x0007CF64
		public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue fallbackValue = default(TValue))
		{
			TValue tvalue;
			return dict.TryGetValue(key, out tvalue) ? tvalue : fallbackValue;
		}
	}
}
