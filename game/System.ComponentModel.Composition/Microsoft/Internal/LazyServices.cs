using System;
using System.Globalization;

namespace Microsoft.Internal
{
	// Token: 0x02000008 RID: 8
	internal static class LazyServices
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000027AA File Offset: 0x000009AA
		public static T GetNotNullValue<T>(this Lazy<T> lazy, string argument) where T : class
		{
			Assumes.NotNull<Lazy<T>>(lazy);
			T value = lazy.Value;
			if (value == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.LazyServices_LazyResolvesToNull, typeof(T), argument));
			}
			return value;
		}
	}
}
