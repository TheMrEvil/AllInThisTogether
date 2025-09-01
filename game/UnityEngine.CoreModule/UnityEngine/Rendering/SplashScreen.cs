using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003E6 RID: 998
	[NativeHeader("Runtime/Graphics/DrawSplashScreenAndWatermarks.h")]
	public class SplashScreen
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060021C1 RID: 8641
		public static extern bool isFinished { [FreeFunction("IsSplashScreenFinished")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060021C2 RID: 8642
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CancelSplashScreen();

		// Token: 0x060021C3 RID: 8643
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSplashScreenFade();

		// Token: 0x060021C4 RID: 8644
		[FreeFunction("BeginSplashScreen_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Begin();

		// Token: 0x060021C5 RID: 8645 RVA: 0x00037108 File Offset: 0x00035308
		public static void Stop(SplashScreen.StopBehavior stopBehavior)
		{
			bool flag = stopBehavior == SplashScreen.StopBehavior.FadeOut;
			if (flag)
			{
				SplashScreen.BeginSplashScreenFade();
			}
			else
			{
				SplashScreen.CancelSplashScreen();
			}
		}

		// Token: 0x060021C6 RID: 8646
		[FreeFunction("DrawSplashScreen_Binding")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Draw();

		// Token: 0x060021C7 RID: 8647
		[FreeFunction("SetSplashScreenTime")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetTime(float time);

		// Token: 0x060021C8 RID: 8648 RVA: 0x00002072 File Offset: 0x00000272
		public SplashScreen()
		{
		}

		// Token: 0x020003E7 RID: 999
		public enum StopBehavior
		{
			// Token: 0x04000C34 RID: 3124
			StopImmediate,
			// Token: 0x04000C35 RID: 3125
			FadeOut
		}
	}
}
