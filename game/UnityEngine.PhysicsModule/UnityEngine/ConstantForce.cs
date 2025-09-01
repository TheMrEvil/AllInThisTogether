using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	[RequireComponent(typeof(Rigidbody))]
	[NativeHeader("Modules/Physics/ConstantForce.h")]
	public class ConstantForce : Behaviour
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00005430 File Offset: 0x00003630
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00005446 File Offset: 0x00003646
		public Vector3 force
		{
			get
			{
				Vector3 result;
				this.get_force_Injected(out result);
				return result;
			}
			set
			{
				this.set_force_Injected(ref value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00005450 File Offset: 0x00003650
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00005466 File Offset: 0x00003666
		public Vector3 relativeForce
		{
			get
			{
				Vector3 result;
				this.get_relativeForce_Injected(out result);
				return result;
			}
			set
			{
				this.set_relativeForce_Injected(ref value);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00005470 File Offset: 0x00003670
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00005486 File Offset: 0x00003686
		public Vector3 torque
		{
			get
			{
				Vector3 result;
				this.get_torque_Injected(out result);
				return result;
			}
			set
			{
				this.set_torque_Injected(ref value);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00005490 File Offset: 0x00003690
		// (set) Token: 0x0600031F RID: 799 RVA: 0x000054A6 File Offset: 0x000036A6
		public Vector3 relativeTorque
		{
			get
			{
				Vector3 result;
				this.get_relativeTorque_Injected(out result);
				return result;
			}
			set
			{
				this.set_relativeTorque_Injected(ref value);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00002E1D File Offset: 0x0000101D
		public ConstantForce()
		{
		}

		// Token: 0x06000321 RID: 801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_force_Injected(out Vector3 ret);

		// Token: 0x06000322 RID: 802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_force_Injected(ref Vector3 value);

		// Token: 0x06000323 RID: 803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_relativeForce_Injected(out Vector3 ret);

		// Token: 0x06000324 RID: 804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_relativeForce_Injected(ref Vector3 value);

		// Token: 0x06000325 RID: 805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_torque_Injected(out Vector3 ret);

		// Token: 0x06000326 RID: 806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_torque_Injected(ref Vector3 value);

		// Token: 0x06000327 RID: 807
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_relativeTorque_Injected(out Vector3 ret);

		// Token: 0x06000328 RID: 808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_relativeTorque_Injected(ref Vector3 value);
	}
}
