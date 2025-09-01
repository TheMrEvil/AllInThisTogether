using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x0200001B RID: 27
	[NativeHeader("Modules/VFX/Public/VFXSystem.h")]
	[UsedByNativeCode]
	public struct VFXParticleSystemInfo
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00003283 File Offset: 0x00001483
		public VFXParticleSystemInfo(uint aliveCount, uint capacity, bool sleeping, Bounds bounds)
		{
			this.aliveCount = aliveCount;
			this.capacity = capacity;
			this.sleeping = sleeping;
			this.bounds = bounds;
		}

		// Token: 0x040000FE RID: 254
		public uint aliveCount;

		// Token: 0x040000FF RID: 255
		public uint capacity;

		// Token: 0x04000100 RID: 256
		public bool sleeping;

		// Token: 0x04000101 RID: 257
		public Bounds bounds;
	}
}
