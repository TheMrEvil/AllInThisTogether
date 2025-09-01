using System;

namespace System.Buffers.Text
{
	// Token: 0x02000AF5 RID: 2805
	internal static class Utf8Constants
	{
		// Token: 0x06006418 RID: 25624 RVA: 0x0014F44A File Offset: 0x0014D64A
		// Note: this type is marked as 'beforefieldinit'.
		static Utf8Constants()
		{
		}

		// Token: 0x04003A9A RID: 15002
		public const byte Colon = 58;

		// Token: 0x04003A9B RID: 15003
		public const byte Comma = 44;

		// Token: 0x04003A9C RID: 15004
		public const byte Minus = 45;

		// Token: 0x04003A9D RID: 15005
		public const byte Period = 46;

		// Token: 0x04003A9E RID: 15006
		public const byte Plus = 43;

		// Token: 0x04003A9F RID: 15007
		public const byte Slash = 47;

		// Token: 0x04003AA0 RID: 15008
		public const byte Space = 32;

		// Token: 0x04003AA1 RID: 15009
		public const byte Hyphen = 45;

		// Token: 0x04003AA2 RID: 15010
		public const byte Separator = 44;

		// Token: 0x04003AA3 RID: 15011
		public const int GroupSize = 3;

		// Token: 0x04003AA4 RID: 15012
		public static readonly TimeSpan s_nullUtcOffset = TimeSpan.MinValue;

		// Token: 0x04003AA5 RID: 15013
		public const int DateTimeMaxUtcOffsetHours = 14;

		// Token: 0x04003AA6 RID: 15014
		public const int DateTimeNumFractionDigits = 7;

		// Token: 0x04003AA7 RID: 15015
		public const int MaxDateTimeFraction = 9999999;

		// Token: 0x04003AA8 RID: 15016
		public const ulong BillionMaxUIntValue = 4294967295000000000UL;

		// Token: 0x04003AA9 RID: 15017
		public const uint Billion = 1000000000U;
	}
}
