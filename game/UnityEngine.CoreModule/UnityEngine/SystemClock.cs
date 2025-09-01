using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200024F RID: 591
	[VisibleToOtherModules]
	internal class SystemClock
	{
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x000298E0 File Offset: 0x00027AE0
		public static DateTime now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000298F8 File Offset: 0x00027AF8
		public static long ToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalMilliseconds);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00029928 File Offset: 0x00027B28
		public static long ToUnixTimeSeconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalSeconds);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00002072 File Offset: 0x00000272
		public SystemClock()
		{
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00029958 File Offset: 0x00027B58
		// Note: this type is marked as 'beforefieldinit'.
		static SystemClock()
		{
		}

		// Token: 0x04000877 RID: 2167
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
