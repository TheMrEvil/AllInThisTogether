using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	[NativeHeader("Modules/Physics2D/AreaEffector2D.h")]
	public class AreaEffector2D : Effector2D
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000456 RID: 1110
		// (set) Token: 0x06000457 RID: 1111
		public extern float forceAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000458 RID: 1112
		// (set) Token: 0x06000459 RID: 1113
		public extern bool useGlobalAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600045A RID: 1114
		// (set) Token: 0x0600045B RID: 1115
		public extern float forceMagnitude { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600045C RID: 1116
		// (set) Token: 0x0600045D RID: 1117
		public extern float forceVariation { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600045E RID: 1118
		// (set) Token: 0x0600045F RID: 1119
		public extern float drag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000460 RID: 1120
		// (set) Token: 0x06000461 RID: 1121
		public extern float angularDrag { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000462 RID: 1122
		// (set) Token: 0x06000463 RID: 1123
		public extern EffectorSelection2D forceTarget { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000464 RID: 1124 RVA: 0x00008694 File Offset: 0x00006894
		public AreaEffector2D()
		{
		}
	}
}
