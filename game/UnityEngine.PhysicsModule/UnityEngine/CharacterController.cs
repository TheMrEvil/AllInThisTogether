using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000029 RID: 41
	[NativeHeader("Modules/Physics/CharacterController.h")]
	public class CharacterController : Collider
	{
		// Token: 0x060002CD RID: 717 RVA: 0x000052AF File Offset: 0x000034AF
		public bool SimpleMove(Vector3 speed)
		{
			return this.SimpleMove_Injected(ref speed);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000052B9 File Offset: 0x000034B9
		public CollisionFlags Move(Vector3 motion)
		{
			return this.Move_Injected(ref motion);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000052C4 File Offset: 0x000034C4
		public Vector3 velocity
		{
			get
			{
				Vector3 result;
				this.get_velocity_Injected(out result);
				return result;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002D0 RID: 720
		public extern bool isGrounded { [NativeName("IsGrounded")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002D1 RID: 721
		public extern CollisionFlags collisionFlags { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002D2 RID: 722
		// (set) Token: 0x060002D3 RID: 723
		public extern float radius { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002D4 RID: 724
		// (set) Token: 0x060002D5 RID: 725
		public extern float height { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000052DC File Offset: 0x000034DC
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000052F2 File Offset: 0x000034F2
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.get_center_Injected(out result);
				return result;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002D8 RID: 728
		// (set) Token: 0x060002D9 RID: 729
		public extern float slopeLimit { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002DA RID: 730
		// (set) Token: 0x060002DB RID: 731
		public extern float stepOffset { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002DC RID: 732
		// (set) Token: 0x060002DD RID: 733
		public extern float skinWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002DE RID: 734
		// (set) Token: 0x060002DF RID: 735
		public extern float minMoveDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E0 RID: 736
		// (set) Token: 0x060002E1 RID: 737
		public extern bool detectCollisions { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E2 RID: 738
		// (set) Token: 0x060002E3 RID: 739
		public extern bool enableOverlapRecovery { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060002E4 RID: 740 RVA: 0x000052FC File Offset: 0x000034FC
		public CharacterController()
		{
		}

		// Token: 0x060002E5 RID: 741
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SimpleMove_Injected(ref Vector3 speed);

		// Token: 0x060002E6 RID: 742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern CollisionFlags Move_Injected(ref Vector3 motion);

		// Token: 0x060002E7 RID: 743
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x060002E8 RID: 744
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x060002E9 RID: 745
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);
	}
}
