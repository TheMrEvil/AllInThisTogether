using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000ED RID: 237
	public static class FixedList32BytesExtensions
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x00022280 File Offset: 0x00020480
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCapacity<[IsUnmanaged] T>(this FixedList32Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			return fixedList.Length >= fixedList.Capacity;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00022293 File Offset: 0x00020493
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Set<[IsUnmanaged] T>(this FixedList32Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000222A6 File Offset: 0x000204A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLimit<[IsUnmanaged] T>(this FixedList32Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (fixedList.Length >= fixedList.Capacity)
			{
				Debug.LogWarning(string.Format("FixedSet32.Limit!:{0}", fixedList.Capacity));
				return;
			}
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000222E4 File Offset: 0x000204E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveItemAtSwapBack<[IsUnmanaged] T>(this FixedList32Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600044F RID: 1103 RVA: 0x0002231F File Offset: 0x0002051F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Push<[IsUnmanaged] T>(this FixedList32Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0002232C File Offset: 0x0002052C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Pop<[IsUnmanaged] T>(this FixedList32Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			int index = fixedList.Length - 1;
			T result = fixedList[index];
			fixedList.RemoveAt(index);
			return result;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0002231F File Offset: 0x0002051F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Enqueue<[IsUnmanaged] T>(this FixedList32Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00022350 File Offset: 0x00020550
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Dequque<[IsUnmanaged] T>(this FixedList32Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			T result = fixedList[0];
			fixedList.RemoveAt(0);
			return result;
		}
	}
}
