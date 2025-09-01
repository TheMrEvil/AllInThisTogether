using System;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.PlayerLoop
{
	// Token: 0x020002F3 RID: 755
	[MovedFrom("UnityEngine.Experimental.PlayerLoop")]
	[RequiredByNativeCode]
	public struct Initialization
	{
		// Token: 0x020002F4 RID: 756
		[RequiredByNativeCode]
		public struct ProfilerStartFrame
		{
		}

		// Token: 0x020002F5 RID: 757
		[Obsolete("PlayerUpdateTime player loop component has been moved to its own category called TimeUpdate. (UnityUpgradable) -> UnityEngine.PlayerLoop.TimeUpdate/WaitForLastPresentationAndUpdateTime", true)]
		public struct PlayerUpdateTime
		{
		}

		// Token: 0x020002F6 RID: 758
		[RequiredByNativeCode]
		public struct UpdateCameraMotionVectors
		{
		}

		// Token: 0x020002F7 RID: 759
		[RequiredByNativeCode]
		public struct DirectorSampleTime
		{
		}

		// Token: 0x020002F8 RID: 760
		[RequiredByNativeCode]
		public struct AsyncUploadTimeSlicedUpdate
		{
		}

		// Token: 0x020002F9 RID: 761
		[RequiredByNativeCode]
		public struct SynchronizeState
		{
		}

		// Token: 0x020002FA RID: 762
		[RequiredByNativeCode]
		public struct SynchronizeInputs
		{
		}

		// Token: 0x020002FB RID: 763
		[RequiredByNativeCode]
		public struct XREarlyUpdate
		{
		}
	}
}
