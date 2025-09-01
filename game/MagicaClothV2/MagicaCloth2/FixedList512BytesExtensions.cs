using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000EF RID: 239
	public static class FixedList512BytesExtensions
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x00022440 File Offset: 0x00020640
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsCapacity<[IsUnmanaged] T>(this FixedList512Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			return fixedList.Length >= fixedList.Capacity;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00022453 File Offset: 0x00020653
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Set<[IsUnmanaged] T>(this FixedList512Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00022466 File Offset: 0x00020666
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetLimit<[IsUnmanaged] T>(this FixedList512Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			if (fixedList.Length >= fixedList.Capacity)
			{
				Debug.LogWarning(string.Format("FixedSet512.Limit!:{0}", fixedList.Capacity));
				return;
			}
			if (!ref fixedList.Contains(item))
			{
				fixedList.Add(item);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000224A4 File Offset: 0x000206A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void RemoveItemAtSwapBack<[IsUnmanaged] T>(this FixedList512Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
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

		// Token: 0x0600045F RID: 1119 RVA: 0x000224DF File Offset: 0x000206DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Push<[IsUnmanaged] T>(this FixedList512Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000224EC File Offset: 0x000206EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Pop<[IsUnmanaged] T>(this FixedList512Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			int index = fixedList.Length - 1;
			T result = fixedList[index];
			fixedList.RemoveAt(index);
			return result;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000224DF File Offset: 0x000206DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Enqueue<[IsUnmanaged] T>(this FixedList512Bytes<T> fixedList, T item) where T : struct, ValueType, IEquatable<T>
		{
			fixedList.Add(item);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00022510 File Offset: 0x00020710
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Dequque<[IsUnmanaged] T>(this FixedList512Bytes<T> fixedList) where T : struct, ValueType, IEquatable<T>
		{
			T result = fixedList[0];
			fixedList.RemoveAt(0);
			return result;
		}
	}
}
