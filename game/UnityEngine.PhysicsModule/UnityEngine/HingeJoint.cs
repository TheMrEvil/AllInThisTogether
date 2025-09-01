using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000030 RID: 48
	[NativeClass("Unity::HingeJoint")]
	[NativeHeader("Modules/Physics/HingeJoint.h")]
	public class HingeJoint : Joint
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00005568 File Offset: 0x00003768
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000557E File Offset: 0x0000377E
		public JointMotor motor
		{
			get
			{
				JointMotor result;
				this.get_motor_Injected(out result);
				return result;
			}
			set
			{
				this.set_motor_Injected(ref value);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00005588 File Offset: 0x00003788
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000559E File Offset: 0x0000379E
		public JointLimits limits
		{
			get
			{
				JointLimits result;
				this.get_limits_Injected(out result);
				return result;
			}
			set
			{
				this.set_limits_Injected(ref value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000055A8 File Offset: 0x000037A8
		// (set) Token: 0x06000350 RID: 848 RVA: 0x000055BE File Offset: 0x000037BE
		public JointSpring spring
		{
			get
			{
				JointSpring result;
				this.get_spring_Injected(out result);
				return result;
			}
			set
			{
				this.set_spring_Injected(ref value);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000351 RID: 849
		// (set) Token: 0x06000352 RID: 850
		public extern bool useMotor { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000353 RID: 851
		// (set) Token: 0x06000354 RID: 852
		public extern bool useLimits { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000355 RID: 853
		// (set) Token: 0x06000356 RID: 854
		public extern bool useSpring { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000357 RID: 855
		public extern float velocity { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000358 RID: 856
		public extern float angle { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000359 RID: 857 RVA: 0x000055C8 File Offset: 0x000037C8
		public HingeJoint()
		{
		}

		// Token: 0x0600035A RID: 858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_motor_Injected(out JointMotor ret);

		// Token: 0x0600035B RID: 859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_motor_Injected(ref JointMotor value);

		// Token: 0x0600035C RID: 860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_limits_Injected(out JointLimits ret);

		// Token: 0x0600035D RID: 861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_limits_Injected(ref JointLimits value);

		// Token: 0x0600035E RID: 862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_spring_Injected(out JointSpring ret);

		// Token: 0x0600035F RID: 863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_spring_Injected(ref JointSpring value);
	}
}
