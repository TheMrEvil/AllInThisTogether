using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	[NativeHeader("Modules/Physics2D/SurfaceEffector2D.h")]
	public class SurfaceEffector2D : Effector2D
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000494 RID: 1172
		// (set) Token: 0x06000495 RID: 1173
		public extern float speed { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000496 RID: 1174
		// (set) Token: 0x06000497 RID: 1175
		public extern float speedVariation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000498 RID: 1176
		// (set) Token: 0x06000499 RID: 1177
		public extern float forceScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600049A RID: 1178
		// (set) Token: 0x0600049B RID: 1179
		public extern bool useContactForce { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600049C RID: 1180
		// (set) Token: 0x0600049D RID: 1181
		public extern bool useFriction { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600049E RID: 1182
		// (set) Token: 0x0600049F RID: 1183
		public extern bool useBounce { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060004A0 RID: 1184 RVA: 0x00008694 File Offset: 0x00006894
		public SurfaceEffector2D()
		{
		}
	}
}
