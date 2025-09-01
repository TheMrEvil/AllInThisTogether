using System;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	internal class AndroidJavaRunnableProxy : AndroidJavaProxy
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002143 File Offset: 0x00000343
		public AndroidJavaRunnableProxy(AndroidJavaRunnable runnable) : base("java/lang/Runnable")
		{
			this.mRunnable = runnable;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002159 File Offset: 0x00000359
		public void run()
		{
			this.mRunnable();
		}

		// Token: 0x04000004 RID: 4
		private AndroidJavaRunnable mRunnable;
	}
}
