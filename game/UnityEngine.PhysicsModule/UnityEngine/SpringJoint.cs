using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/Physics/SpringJoint.h")]
	[NativeClass("Unity::SpringJoint")]
	public class SpringJoint : Joint
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000360 RID: 864
		// (set) Token: 0x06000361 RID: 865
		public extern float spring { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000362 RID: 866
		// (set) Token: 0x06000363 RID: 867
		public extern float damper { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000364 RID: 868
		// (set) Token: 0x06000365 RID: 869
		public extern float minDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000366 RID: 870
		// (set) Token: 0x06000367 RID: 871
		public extern float maxDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000368 RID: 872
		// (set) Token: 0x06000369 RID: 873
		public extern float tolerance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600036A RID: 874 RVA: 0x000055C8 File Offset: 0x000037C8
		public SpringJoint()
		{
		}
	}
}
