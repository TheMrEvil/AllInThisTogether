using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000033 RID: 51
	[NativeClass("Unity::CharacterJoint")]
	[NativeHeader("Modules/Physics/CharacterJoint.h")]
	public class CharacterJoint : Joint
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000055D4 File Offset: 0x000037D4
		// (set) Token: 0x0600036D RID: 877 RVA: 0x000055EA File Offset: 0x000037EA
		public Vector3 swingAxis
		{
			get
			{
				Vector3 result;
				this.get_swingAxis_Injected(out result);
				return result;
			}
			set
			{
				this.set_swingAxis_Injected(ref value);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000055F4 File Offset: 0x000037F4
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000560A File Offset: 0x0000380A
		public SoftJointLimitSpring twistLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.get_twistLimitSpring_Injected(out result);
				return result;
			}
			set
			{
				this.set_twistLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00005614 File Offset: 0x00003814
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000562A File Offset: 0x0000382A
		public SoftJointLimitSpring swingLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.get_swingLimitSpring_Injected(out result);
				return result;
			}
			set
			{
				this.set_swingLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00005634 File Offset: 0x00003834
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000564A File Offset: 0x0000384A
		public SoftJointLimit lowTwistLimit
		{
			get
			{
				SoftJointLimit result;
				this.get_lowTwistLimit_Injected(out result);
				return result;
			}
			set
			{
				this.set_lowTwistLimit_Injected(ref value);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00005654 File Offset: 0x00003854
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000566A File Offset: 0x0000386A
		public SoftJointLimit highTwistLimit
		{
			get
			{
				SoftJointLimit result;
				this.get_highTwistLimit_Injected(out result);
				return result;
			}
			set
			{
				this.set_highTwistLimit_Injected(ref value);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00005674 File Offset: 0x00003874
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000568A File Offset: 0x0000388A
		public SoftJointLimit swing1Limit
		{
			get
			{
				SoftJointLimit result;
				this.get_swing1Limit_Injected(out result);
				return result;
			}
			set
			{
				this.set_swing1Limit_Injected(ref value);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00005694 File Offset: 0x00003894
		// (set) Token: 0x06000379 RID: 889 RVA: 0x000056AA File Offset: 0x000038AA
		public SoftJointLimit swing2Limit
		{
			get
			{
				SoftJointLimit result;
				this.get_swing2Limit_Injected(out result);
				return result;
			}
			set
			{
				this.set_swing2Limit_Injected(ref value);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600037A RID: 890
		// (set) Token: 0x0600037B RID: 891
		public extern bool enableProjection { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600037C RID: 892
		// (set) Token: 0x0600037D RID: 893
		public extern float projectionDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600037E RID: 894
		// (set) Token: 0x0600037F RID: 895
		public extern float projectionAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000380 RID: 896 RVA: 0x000055C8 File Offset: 0x000037C8
		public CharacterJoint()
		{
		}

		// Token: 0x06000381 RID: 897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_swingAxis_Injected(out Vector3 ret);

		// Token: 0x06000382 RID: 898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_swingAxis_Injected(ref Vector3 value);

		// Token: 0x06000383 RID: 899
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_twistLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x06000384 RID: 900
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_twistLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x06000385 RID: 901
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_swingLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x06000386 RID: 902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_swingLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x06000387 RID: 903
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_lowTwistLimit_Injected(out SoftJointLimit ret);

		// Token: 0x06000388 RID: 904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_lowTwistLimit_Injected(ref SoftJointLimit value);

		// Token: 0x06000389 RID: 905
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_highTwistLimit_Injected(out SoftJointLimit ret);

		// Token: 0x0600038A RID: 906
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_highTwistLimit_Injected(ref SoftJointLimit value);

		// Token: 0x0600038B RID: 907
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_swing1Limit_Injected(out SoftJointLimit ret);

		// Token: 0x0600038C RID: 908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_swing1Limit_Injected(ref SoftJointLimit value);

		// Token: 0x0600038D RID: 909
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_swing2Limit_Injected(out SoftJointLimit ret);

		// Token: 0x0600038E RID: 910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_swing2Limit_Injected(ref SoftJointLimit value);

		// Token: 0x040000B4 RID: 180
		[Obsolete("TargetRotation not in use for Unity 5 and assumed disabled.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Quaternion targetRotation;

		// Token: 0x040000B5 RID: 181
		[Obsolete("TargetAngularVelocity not in use for Unity 5 and assumed disabled.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Vector3 targetAngularVelocity;

		// Token: 0x040000B6 RID: 182
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("RotationDrive not in use for Unity 5 and assumed disabled.", true)]
		public JointDrive rotationDrive;
	}
}
