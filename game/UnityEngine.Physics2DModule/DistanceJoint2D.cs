using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/Physics2D/DistanceJoint2D.h")]
	public sealed class DistanceJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003E2 RID: 994
		// (set) Token: 0x060003E3 RID: 995
		public extern bool autoConfigureDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003E4 RID: 996
		// (set) Token: 0x060003E5 RID: 997
		public extern float distance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003E6 RID: 998
		// (set) Token: 0x060003E7 RID: 999
		public extern bool maxDistanceOnly { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060003E8 RID: 1000 RVA: 0x00008551 File Offset: 0x00006751
		public DistanceJoint2D()
		{
		}
	}
}
