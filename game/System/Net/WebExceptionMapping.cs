using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000608 RID: 1544
	internal static class WebExceptionMapping
	{
		// Token: 0x060030BB RID: 12475 RVA: 0x000A7834 File Offset: 0x000A5A34
		internal static string GetWebStatusString(WebExceptionStatus status)
		{
			int num = (int)status;
			if (num >= WebExceptionMapping.s_Mapping.Length || num < 0)
			{
				throw new InternalException();
			}
			string text = Volatile.Read<string>(ref WebExceptionMapping.s_Mapping[num]);
			if (text == null)
			{
				text = "net_webstatus_" + status.ToString();
				Volatile.Write<string>(ref WebExceptionMapping.s_Mapping[num], text);
			}
			return text;
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000A7895 File Offset: 0x000A5A95
		// Note: this type is marked as 'beforefieldinit'.
		static WebExceptionMapping()
		{
		}

		// Token: 0x04001C5E RID: 7262
		private static readonly string[] s_Mapping = new string[21];
	}
}
