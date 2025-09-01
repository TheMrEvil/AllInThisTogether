using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	[NativeHeader("Modules/Physics2D/PointEffector2D.h")]
	public class PointEffector2D : Effector2D
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000474 RID: 1140
		// (set) Token: 0x06000475 RID: 1141
		public extern float forceMagnitude { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000476 RID: 1142
		// (set) Token: 0x06000477 RID: 1143
		public extern float forceVariation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000478 RID: 1144
		// (set) Token: 0x06000479 RID: 1145
		public extern float distanceScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600047A RID: 1146
		// (set) Token: 0x0600047B RID: 1147
		public extern float drag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600047C RID: 1148
		// (set) Token: 0x0600047D RID: 1149
		public extern float angularDrag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600047E RID: 1150
		// (set) Token: 0x0600047F RID: 1151
		public extern EffectorSelection2D forceSource { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000480 RID: 1152
		// (set) Token: 0x06000481 RID: 1153
		public extern EffectorSelection2D forceTarget { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000482 RID: 1154
		// (set) Token: 0x06000483 RID: 1155
		public extern EffectorForceMode2D forceMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000484 RID: 1156 RVA: 0x00008694 File Offset: 0x00006894
		public PointEffector2D()
		{
		}
	}
}
