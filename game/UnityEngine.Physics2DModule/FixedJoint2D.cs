using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	[NativeHeader("Modules/Physics2D/FixedJoint2D.h")]
	public sealed class FixedJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000438 RID: 1080
		// (set) Token: 0x06000439 RID: 1081
		public extern float dampingRatio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600043A RID: 1082
		// (set) Token: 0x0600043B RID: 1083
		public extern float frequency { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600043C RID: 1084
		public extern float referenceAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600043D RID: 1085 RVA: 0x00008551 File Offset: 0x00006751
		public FixedJoint2D()
		{
		}
	}
}
