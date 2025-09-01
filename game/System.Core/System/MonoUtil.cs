using System;

namespace System
{
	// Token: 0x02000031 RID: 49
	internal static class MonoUtil
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00002420 File Offset: 0x00000620
		static MonoUtil()
		{
			int platform = (int)Environment.OSVersion.Platform;
			MonoUtil.IsUnix = (platform == 4 || platform == 128 || platform == 6);
		}

		// Token: 0x040002BA RID: 698
		public static readonly bool IsUnix;
	}
}
