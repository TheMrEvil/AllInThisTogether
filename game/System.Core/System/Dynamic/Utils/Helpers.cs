using System;
using System.Collections.Generic;

namespace System.Dynamic.Utils
{
	// Token: 0x0200032A RID: 810
	internal static class Helpers
	{
		// Token: 0x06001873 RID: 6259 RVA: 0x000528EC File Offset: 0x00050AEC
		internal static T CommonNode<T>(T first, T second, Func<T, T> parent) where T : class
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (@default.Equals(first, second))
			{
				return first;
			}
			HashSet<T> hashSet = new HashSet<T>(@default);
			for (T t = first; t != null; t = parent(t))
			{
				hashSet.Add(t);
			}
			for (T t2 = second; t2 != null; t2 = parent(t2))
			{
				if (hashSet.Contains(t2))
				{
					return t2;
				}
			}
			return default(T);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00052958 File Offset: 0x00050B58
		internal static void IncrementCount<T>(T key, Dictionary<T, int> dict)
		{
			int num;
			dict.TryGetValue(key, out num);
			dict[key] = num + 1;
		}
	}
}
