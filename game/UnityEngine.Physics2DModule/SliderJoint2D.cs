using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/Physics2D/SliderJoint2D.h")]
	public sealed class SliderJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000411 RID: 1041
		// (set) Token: 0x06000412 RID: 1042
		public extern bool autoConfigureAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000413 RID: 1043
		// (set) Token: 0x06000414 RID: 1044
		public extern float angle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000415 RID: 1045
		// (set) Token: 0x06000416 RID: 1046
		public extern bool useMotor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000417 RID: 1047
		// (set) Token: 0x06000418 RID: 1048
		public extern bool useLimits { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000085D4 File Offset: 0x000067D4
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x000085EA File Offset: 0x000067EA
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000085F4 File Offset: 0x000067F4
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000860A File Offset: 0x0000680A
		public JointTranslationLimits2D limits
		{
			get
			{
				JointTranslationLimits2D result;
				this.get_limits_Injected(out result);
				return result;
			}
			set
			{
				this.set_limits_Injected(ref value);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600041D RID: 1053
		public extern JointLimitState2D limitState { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600041E RID: 1054
		public extern float referenceAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600041F RID: 1055
		public extern float jointTranslation { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000420 RID: 1056
		public extern float jointSpeed { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000421 RID: 1057
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetMotorForce(float timeStep);

		// Token: 0x06000422 RID: 1058 RVA: 0x00008551 File Offset: 0x00006751
		public SliderJoint2D()
		{
		}

		// Token: 0x06000423 RID: 1059
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_motor_Injected(out JointMotor2D ret);

		// Token: 0x06000424 RID: 1060
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_motor_Injected(ref JointMotor2D value);

		// Token: 0x06000425 RID: 1061
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_limits_Injected(out JointTranslationLimits2D ret);

		// Token: 0x06000426 RID: 1062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_limits_Injected(ref JointTranslationLimits2D value);
	}
}
