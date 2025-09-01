using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	[NativeHeader("Modules/Physics2D/FrictionJoint2D.h")]
	public sealed class FrictionJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003E9 RID: 1001
		// (set) Token: 0x060003EA RID: 1002
		public extern float maxForce { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003EB RID: 1003
		// (set) Token: 0x060003EC RID: 1004
		public extern float maxTorque { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060003ED RID: 1005 RVA: 0x00008551 File Offset: 0x00006751
		public FrictionJoint2D()
		{
		}
	}
}
