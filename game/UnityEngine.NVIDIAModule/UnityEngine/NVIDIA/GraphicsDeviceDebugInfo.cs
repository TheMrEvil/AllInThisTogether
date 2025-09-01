using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000E RID: 14
	internal struct GraphicsDeviceDebugInfo
	{
		// Token: 0x0400003F RID: 63
		public uint NVDeviceVersion;

		// Token: 0x04000040 RID: 64
		public uint NGXVersion;

		// Token: 0x04000041 RID: 65
		public unsafe DLSSDebugFeatureInfos* dlssInfos;

		// Token: 0x04000042 RID: 66
		public uint dlssInfosCount;
	}
}
