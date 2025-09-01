using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000F0 RID: 240
	public static class FixedList64BytesExtensions
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x00022520 File Offset: 0x00020720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCapacity<[IsUnmanaged] T>(this FixedList64Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			return fixedList.Length >= fixedList.Capacity;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00022533 File Offset: 0x00020733
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Set<[IsUnmanaged] T>(this FixedList64Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00022546 File Offset: 0x00020746
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLimit<[IsUnmanaged] T>(this FixedList64Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (fixedList.Length >= fixedList.Capacity)
			{
				Debug.LogWarning(string.Format("FixedSet64.Limit!:{0}", fixedList.Capacity));
				return;
			}
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00022584 File Offset: 0x00020784
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveItemAtSwapBack<[IsUnmanaged] T>(this FixedList64Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000467 RID: 1127 RVA: 0x000225BF File Offset: 0x000207BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Push<[IsUnmanaged] T>(this FixedList64Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000225CC File Offset: 0x000207CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Pop<[IsUnmanaged] T>(this FixedList64Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			int index = fixedList.Length - 1;
			T result = fixedList[index];
			fixedList.RemoveAt(index);
			return result;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000225BF File Offset: 0x000207BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Enqueue<[IsUnmanaged] T>(this FixedList64Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000225F0 File Offset: 0x000207F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Dequque<[IsUnmanaged] T>(this FixedList64Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			T result = fixedList[0];
			fixedList.RemoveAt(0);
			return result;
		}
	}
}
