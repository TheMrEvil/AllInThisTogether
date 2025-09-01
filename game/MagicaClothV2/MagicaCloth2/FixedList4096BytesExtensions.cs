using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000EE RID: 238
	public static class FixedList4096BytesExtensions
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00022360 File Offset: 0x00020560
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCapacity<[IsUnmanaged] T>(this FixedList4096Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			return fixedList.Length >= fixedList.Capacity;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00022373 File Offset: 0x00020573
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Set<[IsUnmanaged] T>(this FixedList4096Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00022386 File Offset: 0x00020586
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLimit<[IsUnmanaged] T>(this FixedList4096Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (fixedList.Length >= fixedList.Capacity)
			{
				Debug.LogWarning(string.Format("FixedSet4096.Limit!:{0}", fixedList.Capacity));
				return;
			}
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000223C4 File Offset: 0x000205C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveItemAtSwapBack<[IsUnmanaged] T>(this FixedList4096Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x06000457 RID: 1111 RVA: 0x000223FF File Offset: 0x000205FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Push<[IsUnmanaged] T>(this FixedList4096Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0002240C File Offset: 0x0002060C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Pop<[IsUnmanaged] T>(this FixedList4096Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			int index = fixedList.Length - 1;
			T result = fixedList[index];
			fixedList.RemoveAt(index);
			return result;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000223FF File Offset: 0x000205FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Enqueue<[IsUnmanaged] T>(this FixedList4096Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00022430 File Offset: 0x00020630
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Dequque<[IsUnmanaged] T>(this FixedList4096Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			T result = fixedList[0];
			fixedList.RemoveAt(0);
			return result;
		}
	}
}
