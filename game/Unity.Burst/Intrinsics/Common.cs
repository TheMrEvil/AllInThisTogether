using System;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x0200001A RID: 26
	public static class Common
	{
		// Token: 0x060000BC RID: 188 RVA: 0x000056AB File Offset: 0x000038AB
		public static void Pause()
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000056B0 File Offset: 0x000038B0
		public static ulong umul128(ulong x, ulong y, out ulong high)
		{
			ulong num = (ulong)((uint)x);
			ulong num2 = x >> 32;
			ulong num3 = (ulong)((uint)y);
			ulong num4 = y >> 32;
			ulong num5 = num2 * num4;
			ulong num6 = num2 * num3;
			ulong num7 = num4 * num;
			ulong num8 = num * num3;
			ulong num9 = (ulong)((uint)num6);
			ulong num10 = num8 >> 32;
			ulong num11 = num6 >> 32;
			high = num5 + num11 + (num10 + num9 + num7 >> 32);
			return x * y;
		}
	}
}
