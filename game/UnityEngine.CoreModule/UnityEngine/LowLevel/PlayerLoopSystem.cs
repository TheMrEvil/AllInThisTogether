using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.LowLevel
{
	// Token: 0x020002ED RID: 749
	[MovedFrom("UnityEngine.Experimental.LowLevel")]
	public struct PlayerLoopSystem
	{
		// Token: 0x06001E86 RID: 7814 RVA: 0x0003189E File Offset: 0x0002FA9E
		public override string ToString()
		{
			return this.type.Name;
		}

		// Token: 0x040009FD RID: 2557
		public Type type;

		// Token: 0x040009FE RID: 2558
		public PlayerLoopSystem[] subSystemList;

		// Token: 0x040009FF RID: 2559
		public PlayerLoopSystem.UpdateFunction updateDelegate;

		// Token: 0x04000A00 RID: 2560
		public IntPtr updateFunction;

		// Token: 0x04000A01 RID: 2561
		public IntPtr loopConditionFunction;

		// Token: 0x020002EE RID: 750
		// (Invoke) Token: 0x06001E88 RID: 7816
		public delegate void UpdateFunction();
	}
}
