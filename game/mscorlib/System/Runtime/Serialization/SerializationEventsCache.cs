using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x02000657 RID: 1623
	internal static class SerializationEventsCache
	{
		// Token: 0x06003CAF RID: 15535 RVA: 0x000D1CBC File Offset: 0x000CFEBC
		internal static SerializationEvents GetSerializationEventsForType(Type t)
		{
			return SerializationEventsCache.s_cache.GetOrAdd(t, (Type type) => new SerializationEvents(type));
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000D1CE8 File Offset: 0x000CFEE8
		// Note: this type is marked as 'beforefieldinit'.
		static SerializationEventsCache()
		{
		}

		// Token: 0x04002728 RID: 10024
		private static readonly ConcurrentDictionary<Type, SerializationEvents> s_cache = new ConcurrentDictionary<Type, SerializationEvents>();

		// Token: 0x02000658 RID: 1624
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003CB1 RID: 15537 RVA: 0x000D1CF4 File Offset: 0x000CFEF4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003CB2 RID: 15538 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06003CB3 RID: 15539 RVA: 0x000D1D00 File Offset: 0x000CFF00
			internal SerializationEvents <GetSerializationEventsForType>b__1_0(Type type)
			{
				return new SerializationEvents(type);
			}

			// Token: 0x04002729 RID: 10025
			public static readonly SerializationEventsCache.<>c <>9 = new SerializationEventsCache.<>c();

			// Token: 0x0400272A RID: 10026
			public static Func<Type, SerializationEvents> <>9__1_0;
		}
	}
}
