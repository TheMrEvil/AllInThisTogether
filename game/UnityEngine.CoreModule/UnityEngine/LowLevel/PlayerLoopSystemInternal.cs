using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x020002EC RID: 748
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	[RequiredByNativeCode]
	[NativeType(Header = "Runtime/Misc/PlayerLoop.h")]
	internal struct PlayerLoopSystemInternal
	{
		// Token: 0x040009F8 RID: 2552
		public Type type;

		// Token: 0x040009F9 RID: 2553
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x040009FA RID: 2554
		public IntPtr updateFunction;

		// Token: 0x040009FB RID: 2555
		public IntPtr loopConditionFunction;

		// Token: 0x040009FC RID: 2556
		public int numSubSystems;
	}
}
