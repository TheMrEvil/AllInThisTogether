using System;

namespace UnityEngine.Analytics
{
	// Token: 0x02000004 RID: 4
	public static class UGSAnalyticsInternalTools
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002071 File Offset: 0x00000271
		public static void SetPrivacyStatus(bool status)
		{
			AnalyticsCommon.ugsAnalyticsEnabled = status;
		}
	}
}
