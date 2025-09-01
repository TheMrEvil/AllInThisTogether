using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	[RequireComponent(typeof(Rigidbody))]
	[NativeHeader("Modules/Physics/Joint.h")]
	[NativeClass("Unity::Joint")]
	public class Joint : Component
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000329 RID: 809
		// (set) Token: 0x0600032A RID: 810
		public extern Rigidbody connectedBody { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600032B RID: 811
		// (set) Token: 0x0600032C RID: 812
		public extern ArticulationBody connectedArticulationBody { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600032D RID: 813 RVA: 0x000054B0 File Offset: 0x000036B0
		// (set) Token: 0x0600032E RID: 814 RVA: 0x000054C6 File Offset: 0x000036C6
		public Vector3 axis
		{
			get
			{
				Vector3 result;
				this.get_axis_Injected(out result);
				return result;
			}
			set
			{
				this.set_axis_Injected(ref value);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600032F RID: 815 RVA: 0x000054D0 File Offset: 0x000036D0
		// (set) Token: 0x06000330 RID: 816 RVA: 0x000054E6 File Offset: 0x000036E6
		public Vector3 anchor
		{
			get
			{
				Vector3 result;
				this.get_anchor_Injected(out result);
				return result;
			}
			set
			{
				this.set_anchor_Injected(ref value);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000331 RID: 817 RVA: 0x000054F0 File Offset: 0x000036F0
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00005506 File Offset: 0x00003706
		public Vector3 connectedAnchor
		{
			get
			{
				Vector3 result;
				this.get_connectedAnchor_Injected(out result);
				return result;
			}
			set
			{
				this.set_connectedAnchor_Injected(ref value);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000333 RID: 819
		// (set) Token: 0x06000334 RID: 820
		public extern bool autoConfigureConnectedAnchor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000335 RID: 821
		// (set) Token: 0x06000336 RID: 822
		public extern float breakForce { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000337 RID: 823
		// (set) Token: 0x06000338 RID: 824
		public extern float breakTorque { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000339 RID: 825
		// (set) Token: 0x0600033A RID: 826
		public extern bool enableCollision { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600033B RID: 827
		// (set) Token: 0x0600033C RID: 828
		public extern bool enablePreprocessing { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600033D RID: 829
		// (set) Token: 0x0600033E RID: 830
		public extern float massScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600033F RID: 831
		// (set) Token: 0x06000340 RID: 832
		public extern float connectedMassScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000341 RID: 833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCurrentForces(ref Vector3 linearForce, ref Vector3 angularForce);

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00005510 File Offset: 0x00003710
		public Vector3 currentForce
		{
			get
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				this.GetCurrentForces(ref zero, ref zero2);
				return zero;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000553C File Offset: 0x0000373C
		public Vector3 currentTorque
		{
			get
			{
				Vector3 zero = Vector3.zero;
				Vector3 zero2 = Vector3.zero;
				this.GetCurrentForces(ref zero, ref zero2);
				return zero2;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000051FB File Offset: 0x000033FB
		public Joint()
		{
		}

		// Token: 0x06000345 RID: 837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_axis_Injected(out Vector3 ret);

		// Token: 0x06000346 RID: 838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_axis_Injected(ref Vector3 value);

		// Token: 0x06000347 RID: 839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_anchor_Injected(out Vector3 ret);

		// Token: 0x06000348 RID: 840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_anchor_Injected(ref Vector3 value);

		// Token: 0x06000349 RID: 841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_connectedAnchor_Injected(out Vector3 ret);

		// Token: 0x0600034A RID: 842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_connectedAnchor_Injected(ref Vector3 value);
	}
}
