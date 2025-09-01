using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000250 RID: 592
	[StaticAccessor("GetTimeManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Input/TimeManager.h")]
	public class Time
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600199D RID: 6557
		[NativeProperty("CurTime")]
		public static extern float time { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600199E RID: 6558
		[NativeProperty("CurTime")]
		public static extern double timeAsDouble { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600199F RID: 6559
		[NativeProperty("TimeSinceSceneLoad")]
		public static extern float timeSinceLevelLoad { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060019A0 RID: 6560
		[NativeProperty("TimeSinceSceneLoad")]
		public static extern double timeSinceLevelLoadAsDouble { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060019A1 RID: 6561
		public static extern float deltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060019A2 RID: 6562
		public static extern float fixedTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060019A3 RID: 6563
		[NativeProperty("FixedTime")]
		public static extern double fixedTimeAsDouble { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060019A4 RID: 6564
		public static extern float unscaledTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060019A5 RID: 6565
		[NativeProperty("UnscaledTime")]
		public static extern double unscaledTimeAsDouble { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060019A6 RID: 6566
		public static extern float fixedUnscaledTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060019A7 RID: 6567
		[NativeProperty("FixedUnscaledTime")]
		public static extern double fixedUnscaledTimeAsDouble { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060019A8 RID: 6568
		public static extern float unscaledDeltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060019A9 RID: 6569
		public static extern float fixedUnscaledDeltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060019AA RID: 6570
		// (set) Token: 0x060019AB RID: 6571
		public static extern float fixedDeltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060019AC RID: 6572
		// (set) Token: 0x060019AD RID: 6573
		public static extern float maximumDeltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060019AE RID: 6574
		public static extern float smoothDeltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060019AF RID: 6575
		// (set) Token: 0x060019B0 RID: 6576
		public static extern float maximumParticleDeltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060019B1 RID: 6577
		// (set) Token: 0x060019B2 RID: 6578
		public static extern float timeScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060019B3 RID: 6579
		public static extern int frameCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060019B4 RID: 6580
		[NativeProperty("RenderFrameCount")]
		public static extern int renderedFrameCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060019B5 RID: 6581
		[NativeProperty("Realtime")]
		public static extern float realtimeSinceStartup { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060019B6 RID: 6582
		[NativeProperty("Realtime")]
		public static extern double realtimeSinceStartupAsDouble { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060019B7 RID: 6583
		// (set) Token: 0x060019B8 RID: 6584
		public static extern float captureDeltaTime { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x00029970 File Offset: 0x00027B70
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x000299A2 File Offset: 0x00027BA2
		public static int captureFramerate
		{
			get
			{
				return (Time.captureDeltaTime == 0f) ? 0 : ((int)Mathf.Round(1f / Time.captureDeltaTime));
			}
			set
			{
				Time.captureDeltaTime = ((value == 0) ? 0f : (1f / (float)value));
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060019BB RID: 6587
		public static extern bool inFixedTimeStep { [NativeName("IsUsingFixedTimeStep")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060019BC RID: 6588 RVA: 0x00002072 File Offset: 0x00000272
		public Time()
		{
		}
	}
}
