using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000EC RID: 236
	public static class FixedList128BytesExtensions
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x000221A0 File Offset: 0x000203A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCapacity<[IsUnmanaged] T>(this FixedList128Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			return fixedList.Length >= fixedList.Capacity;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000221B3 File Offset: 0x000203B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Set<[IsUnmanaged] T>(this FixedList128Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000221C6 File Offset: 0x000203C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLimit<[IsUnmanaged] T>(this FixedList128Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (fixedList.Length >= fixedList.Capacity)
			{
				Debug.LogWarning(string.Format("FixedSet128.Limit!:{0}", fixedList.Capacity));
				return;
			}
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00022204 File Offset: 0x00020404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveItemAtSwapBack<[IsUnmanaged] T>(this FixedList128Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			for (int i = 0; i < fixedList.Length; i++)
			{
				if (fixedList.ElementAt(i).Equals(item))
				{
					fixedList.RemoveAtSwapBack(i);
					return;
				}
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0002223F File Offset: 0x0002043F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Push<[IsUnmanaged] T>(this FixedList128Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0002224C File Offset: 0x0002044C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Pop<[IsUnmanaged] T>(this FixedList128Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			int index = fixedList.Length - 1;
			T result = fixedList[index];
			fixedList.RemoveAt(index);
			return result;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0002223F File Offset: 0x0002043F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Enqueue<[IsUnmanaged] T>(this FixedList128Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00022270 File Offset: 0x00020470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Dequque<[IsUnmanaged] T>(this FixedList128Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			T result = fixedList[0];
			fixedList.RemoveAt(0);
			return result;
		}
	}
}
