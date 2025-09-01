using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	[NativeHeader("Modules/Physics2D/HingeJoint2D.h")]
	public sealed class HingeJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003EE RID: 1006
		// (set) Token: 0x060003EF RID: 1007
		public extern bool useMotor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003F0 RID: 1008
		// (set) Token: 0x060003F1 RID: 1009
		public extern bool useLimits { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000855C File Offset: 0x0000675C
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00008572 File Offset: 0x00006772
		public JointMotor2D motor
		{
			get
			{
				JointMotor2D result;
				this.get_motor_Injected(out result);
				return result;
			}
			set
			{
				this.set_motor_Injected(ref value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000857C File Offset: 0x0000677C
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00008592 File Offset: 0x00006792
		public JointAngleLimits2D limits
		{
			get
			{
				JointAngleLimits2D result;
				this.get_limits_Injected(out result);
				return result;
			}
			set
			{
				this.set_limits_Injected(ref value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003F6 RID: 1014
		public extern JointLimitState2D limitState { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003F7 RID: 1015
		public extern float referenceAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003F8 RID: 1016
		public extern float jointAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003F9 RID: 1017
		public extern float jointSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060003FA RID: 1018
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetMotorTorque(float timeStep);

		// Token: 0x060003FB RID: 1019 RVA: 0x00008551 File Offset: 0x00006751
		public HingeJoint2D()
		{
		}

		// Token: 0x060003FC RID: 1020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_motor_Injected(out JointMotor2D ret);

		// Token: 0x060003FD RID: 1021
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_motor_Injected(ref JointMotor2D value);

		// Token: 0x060003FE RID: 1022
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_limits_Injected(out JointAngleLimits2D ret);

		// Token: 0x060003FF RID: 1023
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_limits_Injected(ref JointAngleLimits2D value);
	}
}
