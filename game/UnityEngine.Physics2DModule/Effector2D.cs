using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000035 RID: 53
	[NativeHeader("Modules/Physics2D/Effector2D.h")]
	public class Effector2D : Behaviour
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600044E RID: 1102
		// (set) Token: 0x0600044F RID: 1103
		public extern bool useColliderMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000450 RID: 1104
		// (set) Token: 0x06000451 RID: 1105
		public extern int colliderMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000452 RID: 1106
		internal extern bool requiresCollider { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000453 RID: 1107
		internal extern bool designedForTrigger { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000454 RID: 1108
		internal extern bool designedForNonTrigger { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000455 RID: 1109 RVA: 0x00007C39 File Offset: 0x00005E39
		public Effector2D()
		{
		}
	}
}
