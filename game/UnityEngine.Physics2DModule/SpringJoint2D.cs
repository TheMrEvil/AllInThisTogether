using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/Physics2D/SpringJoint2D.h")]
	public sealed class SpringJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003D9 RID: 985
		// (set) Token: 0x060003DA RID: 986
		public extern bool autoConfigureDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003DB RID: 987
		// (set) Token: 0x060003DC RID: 988
		public extern float distance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003DD RID: 989
		// (set) Token: 0x060003DE RID: 990
		public extern float dampingRatio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003DF RID: 991
		// (set) Token: 0x060003E0 RID: 992
		public extern float frequency { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060003E1 RID: 993 RVA: 0x00008551 File Offset: 0x00006751
		public SpringJoint2D()
		{
		}
	}
}
