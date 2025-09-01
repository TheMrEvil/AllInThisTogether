using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000210 RID: 528
	[NativeHeader("Runtime/Export/Scripting/NoAllocHelpers.bindings.h")]
	internal sealed class NoAllocHelpers
	{
		// Token: 0x0600174B RID: 5963 RVA: 0x00025770 File Offset: 0x00023970
		public static void ResizeList<T>(List<T> list, int size)
		{
			bool flag = list == null;
			if (flag)
			{
				throw new ArgumentNullException("list");
			}
			bool flag2 = size < 0 || size > list.Capacity;
			if (flag2)
			{
				throw new ArgumentException("invalid size to resize.", "list");
			}
			bool flag3 = size != list.Count;
			if (flag3)
			{
				NoAllocHelpers.Internal_ResizeList(list, size);
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000257D0 File Offset: 0x000239D0
		public static void EnsureListElemCount<T>(List<T> list, int count)
		{
			list.Clear();
			bool flag = list.Capacity < count;
			if (flag)
			{
				list.Capacity = count;
			}
			NoAllocHelpers.ResizeList<T>(list, count);
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00025804 File Offset: 0x00023A04
		public static int SafeLength(Array values)
		{
			return (values != null) ? values.Length : 0;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00025824 File Offset: 0x00023A24
		public static int SafeLength<T>(List<T> values)
		{
			return (values != null) ? values.Count : 0;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00025844 File Offset: 0x00023A44
		public static T[] ExtractArrayFromListT<T>(List<T> list)
		{
			return (T[])NoAllocHelpers.ExtractArrayFromList(list);
		}

		// Token: 0x06001750 RID: 5968
		[FreeFunction("NoAllocHelpers_Bindings::Internal_ResizeList")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_ResizeList(object list, int size);

		// Token: 0x06001751 RID: 5969
		[FreeFunction("NoAllocHelpers_Bindings::ExtractArrayFromList")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Array ExtractArrayFromList(object list);

		// Token: 0x06001752 RID: 5970 RVA: 0x00002072 File Offset: 0x00000272
		public NoAllocHelpers()
		{
		}
	}
}
