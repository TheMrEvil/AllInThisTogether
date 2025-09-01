using System;
using UnityEngine;

namespace Unity.Collections
{
	// Token: 0x0200008F RID: 143
	public static class NativeLeakDetection
	{
		// Token: 0x06000255 RID: 597 RVA: 0x000043A5 File Offset: 0x000025A5
		[RuntimeInitializeOnLoadMethod]
		private static void Initialize()
		{
			NativeLeakDetection.s_NativeLeakDetectionMode = 1;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000256 RID: 598 RVA: 0x000043B0 File Offset: 0x000025B0
		// (set) Token: 0x06000257 RID: 599 RVA: 0x000043DC File Offset: 0x000025DC
		public static NativeLeakDetectionMode Mode
		{
			get
			{
				bool flag = NativeLeakDetection.s_NativeLeakDetectionMode == 0;
				if (flag)
				{
					NativeLeakDetection.Initialize();
				}
				return (NativeLeakDetectionMode)NativeLeakDetection.s_NativeLeakDetectionMode;
			}
			set
			{
				bool flag = NativeLeakDetection.s_NativeLeakDetectionMode != (int)value;
				if (flag)
				{
					NativeLeakDetection.s_NativeLeakDetectionMode = (int)value;
				}
			}
		}

		// Token: 0x04000220 RID: 544
		private static int s_NativeLeakDetectionMode;

		// Token: 0x04000221 RID: 545
		private const string kNativeLeakDetectionModePrefsString = "Unity.Colletions.NativeLeakDetection.Mode";
	}
}
