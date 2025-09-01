using System;

namespace Steamworks
{
	// Token: 0x020000B6 RID: 182
	internal static class Epoch
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0001207C File Offset: 0x0001027C
		public static int Current
		{
			get
			{
				return (int)DateTime.UtcNow.Subtract(Epoch.epoch).TotalSeconds;
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000120A4 File Offset: 0x000102A4
		public static DateTime ToDateTime(decimal unixTime)
		{
			return Epoch.epoch.AddSeconds((double)((long)unixTime));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000120C8 File Offset: 0x000102C8
		public static uint FromDateTime(DateTime dt)
		{
			return (uint)dt.Subtract(Epoch.epoch).TotalSeconds;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000120EA File Offset: 0x000102EA
		// Note: this type is marked as 'beforefieldinit'.
		static Epoch()
		{
		}

		// Token: 0x0400076D RID: 1901
		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
