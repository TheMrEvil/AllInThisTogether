using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020006A8 RID: 1704
	internal class MonoHttpDate
	{
		// Token: 0x0600367F RID: 13951 RVA: 0x000BF738 File Offset: 0x000BD938
		internal static DateTime Parse(string dateStr)
		{
			return DateTime.ParseExact(dateStr, MonoHttpDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces).ToLocalTime();
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x0000219B File Offset: 0x0000039B
		public MonoHttpDate()
		{
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000BF760 File Offset: 0x000BD960
		// Note: this type is marked as 'beforefieldinit'.
		static MonoHttpDate()
		{
		}

		// Token: 0x04001FD0 RID: 8144
		private static readonly string rfc1123_date = "r";

		// Token: 0x04001FD1 RID: 8145
		private static readonly string rfc850_date = "dddd, dd-MMM-yy HH:mm:ss G\\MT";

		// Token: 0x04001FD2 RID: 8146
		private static readonly string asctime_date = "ddd MMM d HH:mm:ss yyyy";

		// Token: 0x04001FD3 RID: 8147
		private static readonly string[] formats = new string[]
		{
			MonoHttpDate.rfc1123_date,
			MonoHttpDate.rfc850_date,
			MonoHttpDate.asctime_date
		};
	}
}
