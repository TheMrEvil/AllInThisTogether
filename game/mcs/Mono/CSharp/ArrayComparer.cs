using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x020002CB RID: 715
	internal static class ArrayComparer
	{
		// Token: 0x06002256 RID: 8790 RVA: 0x000A8038 File Offset: 0x000A6238
		public static bool IsEqual<T>(T[] array1, T[] array2)
		{
			if (array1 == null || array2 == null)
			{
				return array1 == array2;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < array1.Length; i++)
			{
				if (!@default.Equals(array1[i], array2[i]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
