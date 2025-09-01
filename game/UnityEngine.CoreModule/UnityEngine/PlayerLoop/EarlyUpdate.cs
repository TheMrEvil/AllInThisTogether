using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	// Token: 0x020002FC RID: 764
	[RequiredByNativeCode]
	[MovedFrom("UnityEngine.Experimental.PlayerLoop")]
	public struct EarlyUpdate
	{
		// Token: 0x020002FD RID: 765
		[RequiredByNativeCode]
		public struct PollPlayerConnection
		{
		}

		// Token: 0x020002FE RID: 766
		[Obsolete("ProfilerStartFrame player loop component has been moved to the Initialization category. (UnityUpgradable) -> UnityEngine.PlayerLoop.Initialization/ProfilerStartFrame", true)]
		public struct ProfilerStartFrame
		{
		}

		// Token: 0x020002FF RID: 767
		[RequiredByNativeCode]
		public struct PollHtcsPlayerConnection
		{
		}

		// Token: 0x02000300 RID: 768
		[RequiredByNativeCode]
		public struct GpuTimestamp
		{
		}

		// Token: 0x02000301 RID: 769
		[RequiredByNativeCode]
		public struct AnalyticsCoreStatsUpdate
		{
		}

		// Token: 0x02000302 RID: 770
		[RequiredByNativeCode]
		public struct UnityWebRequestUpdate
		{
		}

		// Token: 0x02000303 RID: 771
		[RequiredByNativeCode]
		public struct UpdateStreamingManager
		{
		}

		// Token: 0x02000304 RID: 772
		[RequiredByNativeCode]
		public struct ExecuteMainThreadJobs
		{
		}

		// Token: 0x02000305 RID: 773
		[RequiredByNativeCode]
		public struct ProcessMouseInWindow
		{
		}

		// Token: 0x02000306 RID: 774
		[RequiredByNativeCode]
		public struct ClearIntermediateRenderers
		{
		}

		// Token: 0x02000307 RID: 775
		[RequiredByNativeCode]
		public struct ClearLines
		{
		}

		// Token: 0x02000308 RID: 776
		[RequiredByNativeCode]
		public struct PresentBeforeUpdate
		{
		}

		// Token: 0x02000309 RID: 777
		[RequiredByNativeCode]
		public struct ResetFrameStatsAfterPresent
		{
		}

		// Token: 0x0200030A RID: 778
		[RequiredByNativeCode]
		public struct UpdateAsyncReadbackManager
		{
		}

		// Token: 0x0200030B RID: 779
		[RequiredByNativeCode]
		public struct UpdateTextureStreamingManager
		{
		}

		// Token: 0x0200030C RID: 780
		[RequiredByNativeCode]
		public struct UpdatePreloading
		{
		}

		// Token: 0x0200030D RID: 781
		[RequiredByNativeCode]
		public struct RendererNotifyInvisible
		{
		}

		// Token: 0x0200030E RID: 782
		[RequiredByNativeCode]
		public struct PlayerCleanupCachedData
		{
		}

		// Token: 0x0200030F RID: 783
		[RequiredByNativeCode]
		public struct UpdateMainGameViewRect
		{
		}

		// Token: 0x02000310 RID: 784
		[RequiredByNativeCode]
		public struct UpdateCanvasRectTransform
		{
		}

		// Token: 0x02000311 RID: 785
		[RequiredByNativeCode]
		public struct UpdateInputManager
		{
		}

		// Token: 0x02000312 RID: 786
		[RequiredByNativeCode]
		public struct ProcessRemoteInput
		{
		}

		// Token: 0x02000313 RID: 787
		[RequiredByNativeCode]
		public struct XRUpdate
		{
		}

		// Token: 0x02000314 RID: 788
		[RequiredByNativeCode]
		public struct ScriptRunDelayedStartupFrame
		{
		}

		// Token: 0x02000315 RID: 789
		[RequiredByNativeCode]
		public struct UpdateKinect
		{
		}

		// Token: 0x02000316 RID: 790
		[RequiredByNativeCode]
		public struct DeliverIosPlatformEvents
		{
		}

		// Token: 0x02000317 RID: 791
		[RequiredByNativeCode]
		public struct DispatchEventQueueEvents
		{
		}

		// Token: 0x02000318 RID: 792
		[RequiredByNativeCode]
		public struct PhysicsResetInterpolatedTransformPosition
		{
		}

		// Token: 0x02000319 RID: 793
		[RequiredByNativeCode]
		public struct SpriteAtlasManagerUpdate
		{
		}

		// Token: 0x0200031A RID: 794
		[Obsolete("TangoUpdate has been deprecated. Use ARCoreUpdate instead (UnityUpgradable) -> UnityEngine.PlayerLoop.EarlyUpdate/ARCoreUpdate", false)]
		[RequiredByNativeCode]
		public struct TangoUpdate
		{
		}

		// Token: 0x0200031B RID: 795
		[RequiredByNativeCode]
		public struct ARCoreUpdate
		{
		}

		// Token: 0x0200031C RID: 796
		[RequiredByNativeCode]
		public struct PerformanceAnalyticsUpdate
		{
		}
	}
}
