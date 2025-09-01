using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000631 RID: 1585
	internal class NetRes
	{
		// Token: 0x0600321C RID: 12828 RVA: 0x0000219B File Offset: 0x0000039B
		private NetRes()
		{
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000AD6D4 File Offset: 0x000AB8D4
		public static string GetWebStatusString(string Res, WebExceptionStatus Status)
		{
			string @string = SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
			string string2 = SR.GetString(Res);
			return string.Format(CultureInfo.CurrentCulture, string2, @string);
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000AD700 File Offset: 0x000AB900
		public static string GetWebStatusString(WebExceptionStatus Status)
		{
			return SR.GetString(WebExceptionMapping.GetWebStatusString(Status));
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000AD710 File Offset: 0x000AB910
		public static string GetWebStatusCodeString(HttpStatusCode statusCode, string statusDescription)
		{
			string str = "(";
			int num = (int)statusCode;
			string text = str + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text2 = null;
			try
			{
				text2 = SR.GetString("net_httpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text2 != null && text2.Length > 0)
			{
				text = text + " " + text2;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text = text + " " + statusDescription;
			}
			return text;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000AD7A8 File Offset: 0x000AB9A8
		public static string GetWebStatusCodeString(FtpStatusCode statusCode, string statusDescription)
		{
			string str = "(";
			int num = (int)statusCode;
			string text = str + num.ToString(NumberFormatInfo.InvariantInfo) + ")";
			string text2 = null;
			try
			{
				text2 = SR.GetString("net_ftpstatuscode_" + statusCode.ToString(), null);
			}
			catch
			{
			}
			if (text2 != null && text2.Length > 0)
			{
				text = text + " " + text2;
			}
			else if (statusDescription != null && statusDescription.Length > 0)
			{
				text = text + " " + statusDescription;
			}
			return text;
		}
	}
}
