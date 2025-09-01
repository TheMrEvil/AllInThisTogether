using System;

namespace UnityEngine.Android
{
	// Token: 0x02000019 RID: 25
	internal static class Common
	{
		// Token: 0x060001BC RID: 444 RVA: 0x0000853C File Offset: 0x0000673C
		public static AndroidJavaObject GetActivity()
		{
			bool flag = Common.m_Activity != null;
			AndroidJavaObject activity;
			if (flag)
			{
				activity = Common.m_Activity;
			}
			else
			{
				using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
				{
					Common.m_Activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
				}
				activity = Common.m_Activity;
			}
			return activity;
		}

		// Token: 0x04000049 RID: 73
		private static AndroidJavaObject m_Activity;
	}
}
