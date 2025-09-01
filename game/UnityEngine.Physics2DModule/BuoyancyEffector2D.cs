using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	[NativeHeader("Modules/Physics2D/BuoyancyEffector2D.h")]
	public class BuoyancyEffector2D : Effector2D
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000465 RID: 1125
		// (set) Token: 0x06000466 RID: 1126
		public extern float surfaceLevel { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000467 RID: 1127
		// (set) Token: 0x06000468 RID: 1128
		public extern float density { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000469 RID: 1129
		// (set) Token: 0x0600046A RID: 1130
		public extern float linearDrag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600046B RID: 1131
		// (set) Token: 0x0600046C RID: 1132
		public extern float angularDrag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600046D RID: 1133
		// (set) Token: 0x0600046E RID: 1134
		public extern float flowAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600046F RID: 1135
		// (set) Token: 0x06000470 RID: 1136
		public extern float flowMagnitude { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000471 RID: 1137
		// (set) Token: 0x06000472 RID: 1138
		public extern float flowVariation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000473 RID: 1139 RVA: 0x00008694 File Offset: 0x00006894
		public BuoyancyEffector2D()
		{
		}
	}
}
