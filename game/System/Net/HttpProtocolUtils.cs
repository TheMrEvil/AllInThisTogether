using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020005F8 RID: 1528
	internal class HttpProtocolUtils
	{
		// Token: 0x06003078 RID: 12408 RVA: 0x0000219B File Offset: 0x0000039B
		private HttpProtocolUtils()
		{
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000A6DF4 File Offset: 0x000A4FF4
		internal static DateTime string2date(string S)
		{
			DateTime result;
			if (HttpDateParse.ParseHttpDate(S, out result))
			{
				return result;
			}
			throw new ProtocolViolationException(SR.GetString("The value of the date string in the header is invalid."));
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000A6E1C File Offset: 0x000A501C
		internal static string date2string(DateTime D)
		{
			DateTimeFormatInfo provider = new DateTimeFormatInfo();
			return D.ToUniversalTime().ToString("R", provider);
		}
	}
}
