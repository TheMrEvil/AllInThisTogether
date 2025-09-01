using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("Modules/Physics2D/TargetJoint2D.h")]
	public sealed class TargetJoint2D : Joint2D
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00008614 File Offset: 0x00006814
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0000862A File Offset: 0x0000682A
		public Vector2 anchor
		{
			get
			{
				Vector2 result;
				this.get_anchor_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchor_Injected(ref value);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00008634 File Offset: 0x00006834
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000864A File Offset: 0x0000684A
		public Vector2 target
		{
			get
			{
				Vector2 result;
				this.get_target_Injected(out result);
				return result;
			}
			set
			{
				this.set_target_Injected(ref value);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600042B RID: 1067
		// (set) Token: 0x0600042C RID: 1068
		public extern bool autoConfigureTarget { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600042D RID: 1069
		// (set) Token: 0x0600042E RID: 1070
		public extern float maxForce { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600042F RID: 1071
		// (set) Token: 0x06000430 RID: 1072
		public extern float dampingRatio { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000431 RID: 1073
		// (set) Token: 0x06000432 RID: 1074
		public extern float frequency { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000433 RID: 1075 RVA: 0x00008548 File Offset: 0x00006748
		public TargetJoint2D()
		{
		}

		// Token: 0x06000434 RID: 1076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchor_Injected(out Vector2 ret);

		// Token: 0x06000435 RID: 1077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchor_Injected(ref Vector2 value);

		// Token: 0x06000436 RID: 1078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_target_Injected(out Vector2 ret);

		// Token: 0x06000437 RID: 1079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_target_Injected(ref Vector2 value);
	}
}
