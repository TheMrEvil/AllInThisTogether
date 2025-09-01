using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200012C RID: 300
	[NativeType("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public enum ComputeBufferMode
	{
		// Token: 0x040003C6 RID: 966
		Immutable,
		// Token: 0x040003C7 RID: 967
		Dynamic,
		// Token: 0x040003C8 RID: 968
		[Obsolete("ComputeBufferMode.Circular is deprecated (legacy mode)")]
		Circular,
		// Token: 0x040003C9 RID: 969
		[Obsolete("ComputeBufferMode.StreamOut is deprecated (internal use only)")]
		StreamOut,
		// Token: 0x040003CA RID: 970
		SubUpdates
	}
}
