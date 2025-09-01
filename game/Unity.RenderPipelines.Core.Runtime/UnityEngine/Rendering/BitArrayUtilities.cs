using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A2 RID: 162
	public static class BitArrayUtilities
	{
		// Token: 0x06000543 RID: 1347 RVA: 0x00018746 File Offset: 0x00016946
		public static bool Get8(uint index, byte data)
		{
			return ((int)data & 1 << (int)index) != 0;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00018753 File Offset: 0x00016953
		public static bool Get16(uint index, ushort data)
		{
			return ((int)data & 1 << (int)index) != 0;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00018760 File Offset: 0x00016960
		public static bool Get32(uint index, uint data)
		{
			return (data & 1U << (int)index) > 0U;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001876D File Offset: 0x0001696D
		public static bool Get64(uint index, ulong data)
		{
			return (data & 1UL << (int)index) > 0UL;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001877C File Offset: 0x0001697C
		public static bool Get128(uint index, ulong data1, ulong data2)
		{
			if (index >= 64U)
			{
				return (data2 & 1UL << (int)(index - 64U)) > 0UL;
			}
			return (data1 & 1UL << (int)index) > 0UL;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000187A4 File Offset: 0x000169A4
		public static bool Get256(uint index, ulong data1, ulong data2, ulong data3, ulong data4)
		{
			if (index >= 128U)
			{
				if (index >= 192U)
				{
					return (data4 & 1UL << (int)(index - 192U)) > 0UL;
				}
				return (data3 & 1UL << (int)(index - 128U)) > 0UL;
			}
			else
			{
				if (index >= 64U)
				{
					return (data2 & 1UL << (int)(index - 64U)) > 0UL;
				}
				return (data1 & 1UL << (int)index) > 0UL;
			}
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001880D File Offset: 0x00016A0D
		public static void Set8(uint index, ref byte data, bool value)
		{
			data = (byte)(value ? ((int)data | 1 << (int)index) : ((int)data & ~(1 << (int)index)));
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001882A File Offset: 0x00016A2A
		public static void Set16(uint index, ref ushort data, bool value)
		{
			data = (ushort)(value ? ((int)data | 1 << (int)index) : ((int)data & ~(1 << (int)index)));
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00018847 File Offset: 0x00016A47
		public static void Set32(uint index, ref uint data, bool value)
		{
			data = (value ? (data | 1U << (int)index) : (data & ~(1U << (int)index)));
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018863 File Offset: 0x00016A63
		public static void Set64(uint index, ref ulong data, bool value)
		{
			data = (value ? (data | 1UL << (int)index) : (data & ~(1UL << (int)index)));
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00018884 File Offset: 0x00016A84
		public static void Set128(uint index, ref ulong data1, ref ulong data2, bool value)
		{
			if (index < 64U)
			{
				data1 = (value ? (data1 | 1UL << (int)index) : (data1 & ~(1UL << (int)index)));
				return;
			}
			data2 = (value ? (data2 | 1UL << (int)(index - 64U)) : (data2 & ~(1UL << (int)(index - 64U))));
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000188D8 File Offset: 0x00016AD8
		public static void Set256(uint index, ref ulong data1, ref ulong data2, ref ulong data3, ref ulong data4, bool value)
		{
			if (index < 64U)
			{
				data1 = (value ? (data1 | 1UL << (int)index) : (data1 & ~(1UL << (int)index)));
				return;
			}
			if (index < 128U)
			{
				data2 = (value ? (data2 | 1UL << (int)(index - 64U)) : (data2 & ~(1UL << (int)(index - 64U))));
				return;
			}
			if (index < 192U)
			{
				data3 = (value ? (data3 | 1UL << (int)(index - 64U)) : (data3 & ~(1UL << (int)(index - 128U))));
				return;
			}
			data4 = (value ? (data4 | 1UL << (int)(index - 64U)) : (data4 & ~(1UL << (int)(index - 192U))));
		}
	}
}
