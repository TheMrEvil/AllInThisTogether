using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeClass("Unity::ConfigurableJoint")]
	[NativeHeader("Modules/Physics/ConfigurableJoint.h")]
	public class ConfigurableJoint : Joint
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000056B4 File Offset: 0x000038B4
		// (set) Token: 0x06000390 RID: 912 RVA: 0x000056CA File Offset: 0x000038CA
		public Vector3 secondaryAxis
		{
			get
			{
				Vector3 result;
				this.get_secondaryAxis_Injected(out result);
				return result;
			}
			set
			{
				this.set_secondaryAxis_Injected(ref value);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000391 RID: 913
		// (set) Token: 0x06000392 RID: 914
		public extern ConfigurableJointMotion xMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000393 RID: 915
		// (set) Token: 0x06000394 RID: 916
		public extern ConfigurableJointMotion yMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000395 RID: 917
		// (set) Token: 0x06000396 RID: 918
		public extern ConfigurableJointMotion zMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000397 RID: 919
		// (set) Token: 0x06000398 RID: 920
		public extern ConfigurableJointMotion angularXMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000399 RID: 921
		// (set) Token: 0x0600039A RID: 922
		public extern ConfigurableJointMotion angularYMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600039B RID: 923
		// (set) Token: 0x0600039C RID: 924
		public extern ConfigurableJointMotion angularZMotion { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000056D4 File Offset: 0x000038D4
		// (set) Token: 0x0600039E RID: 926 RVA: 0x000056EA File Offset: 0x000038EA
		public SoftJointLimitSpring linearLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.get_linearLimitSpring_Injected(out result);
				return result;
			}
			set
			{
				this.set_linearLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000056F4 File Offset: 0x000038F4
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000570A File Offset: 0x0000390A
		public SoftJointLimitSpring angularXLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.get_angularXLimitSpring_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularXLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00005714 File Offset: 0x00003914
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000572A File Offset: 0x0000392A
		public SoftJointLimitSpring angularYZLimitSpring
		{
			get
			{
				SoftJointLimitSpring result;
				this.get_angularYZLimitSpring_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularYZLimitSpring_Injected(ref value);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00005734 File Offset: 0x00003934
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000574A File Offset: 0x0000394A
		public SoftJointLimit linearLimit
		{
			get
			{
				SoftJointLimit result;
				this.get_linearLimit_Injected(out result);
				return result;
			}
			set
			{
				this.set_linearLimit_Injected(ref value);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00005754 File Offset: 0x00003954
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000576A File Offset: 0x0000396A
		public SoftJointLimit lowAngularXLimit
		{
			get
			{
				SoftJointLimit result;
				this.get_lowAngularXLimit_Injected(out result);
				return result;
			}
			set
			{
				this.set_lowAngularXLimit_Injected(ref value);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00005774 File Offset: 0x00003974
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000578A File Offset: 0x0000398A
		public SoftJointLimit highAngularXLimit
		{
			get
			{
				SoftJointLimit result;
				this.get_highAngularXLimit_Injected(out result);
				return result;
			}
			set
			{
				this.set_highAngularXLimit_Injected(ref value);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00005794 File Offset: 0x00003994
		// (set) Token: 0x060003AA RID: 938 RVA: 0x000057AA File Offset: 0x000039AA
		public SoftJointLimit angularYLimit
		{
			get
			{
				SoftJointLimit result;
				this.get_angularYLimit_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularYLimit_Injected(ref value);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000057B4 File Offset: 0x000039B4
		// (set) Token: 0x060003AC RID: 940 RVA: 0x000057CA File Offset: 0x000039CA
		public SoftJointLimit angularZLimit
		{
			get
			{
				SoftJointLimit result;
				this.get_angularZLimit_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularZLimit_Injected(ref value);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003AD RID: 941 RVA: 0x000057D4 File Offset: 0x000039D4
		// (set) Token: 0x060003AE RID: 942 RVA: 0x000057EA File Offset: 0x000039EA
		public Vector3 targetPosition
		{
			get
			{
				Vector3 result;
				this.get_targetPosition_Injected(out result);
				return result;
			}
			set
			{
				this.set_targetPosition_Injected(ref value);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000057F4 File Offset: 0x000039F4
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000580A File Offset: 0x00003A0A
		public Vector3 targetVelocity
		{
			get
			{
				Vector3 result;
				this.get_targetVelocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_targetVelocity_Injected(ref value);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00005814 File Offset: 0x00003A14
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000582A File Offset: 0x00003A2A
		public JointDrive xDrive
		{
			get
			{
				JointDrive result;
				this.get_xDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_xDrive_Injected(ref value);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00005834 File Offset: 0x00003A34
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000584A File Offset: 0x00003A4A
		public JointDrive yDrive
		{
			get
			{
				JointDrive result;
				this.get_yDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_yDrive_Injected(ref value);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00005854 File Offset: 0x00003A54
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000586A File Offset: 0x00003A6A
		public JointDrive zDrive
		{
			get
			{
				JointDrive result;
				this.get_zDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_zDrive_Injected(ref value);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00005874 File Offset: 0x00003A74
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000588A File Offset: 0x00003A8A
		public Quaternion targetRotation
		{
			get
			{
				Quaternion result;
				this.get_targetRotation_Injected(out result);
				return result;
			}
			set
			{
				this.set_targetRotation_Injected(ref value);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00005894 File Offset: 0x00003A94
		// (set) Token: 0x060003BA RID: 954 RVA: 0x000058AA File Offset: 0x00003AAA
		public Vector3 targetAngularVelocity
		{
			get
			{
				Vector3 result;
				this.get_targetAngularVelocity_Injected(out result);
				return result;
			}
			set
			{
				this.set_targetAngularVelocity_Injected(ref value);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003BB RID: 955
		// (set) Token: 0x060003BC RID: 956
		public extern RotationDriveMode rotationDriveMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000058B4 File Offset: 0x00003AB4
		// (set) Token: 0x060003BE RID: 958 RVA: 0x000058CA File Offset: 0x00003ACA
		public JointDrive angularXDrive
		{
			get
			{
				JointDrive result;
				this.get_angularXDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularXDrive_Injected(ref value);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000058D4 File Offset: 0x00003AD4
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x000058EA File Offset: 0x00003AEA
		public JointDrive angularYZDrive
		{
			get
			{
				JointDrive result;
				this.get_angularYZDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_angularYZDrive_Injected(ref value);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000058F4 File Offset: 0x00003AF4
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000590A File Offset: 0x00003B0A
		public JointDrive slerpDrive
		{
			get
			{
				JointDrive result;
				this.get_slerpDrive_Injected(out result);
				return result;
			}
			set
			{
				this.set_slerpDrive_Injected(ref value);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003C3 RID: 963
		// (set) Token: 0x060003C4 RID: 964
		public extern JointProjectionMode projectionMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003C5 RID: 965
		// (set) Token: 0x060003C6 RID: 966
		public extern float projectionDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003C7 RID: 967
		// (set) Token: 0x060003C8 RID: 968
		public extern float projectionAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003C9 RID: 969
		// (set) Token: 0x060003CA RID: 970
		public extern bool configuredInWorldSpace { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003CB RID: 971
		// (set) Token: 0x060003CC RID: 972
		public extern bool swapBodies { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x060003CD RID: 973 RVA: 0x000055C8 File Offset: 0x000037C8
		public ConfigurableJoint()
		{
		}

		// Token: 0x060003CE RID: 974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_secondaryAxis_Injected(out Vector3 ret);

		// Token: 0x060003CF RID: 975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_secondaryAxis_Injected(ref Vector3 value);

		// Token: 0x060003D0 RID: 976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_linearLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x060003D1 RID: 977
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_linearLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x060003D2 RID: 978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularXLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x060003D3 RID: 979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularXLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x060003D4 RID: 980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularYZLimitSpring_Injected(out SoftJointLimitSpring ret);

		// Token: 0x060003D5 RID: 981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularYZLimitSpring_Injected(ref SoftJointLimitSpring value);

		// Token: 0x060003D6 RID: 982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_linearLimit_Injected(out SoftJointLimit ret);

		// Token: 0x060003D7 RID: 983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_linearLimit_Injected(ref SoftJointLimit value);

		// Token: 0x060003D8 RID: 984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_lowAngularXLimit_Injected(out SoftJointLimit ret);

		// Token: 0x060003D9 RID: 985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_lowAngularXLimit_Injected(ref SoftJointLimit value);

		// Token: 0x060003DA RID: 986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_highAngularXLimit_Injected(out SoftJointLimit ret);

		// Token: 0x060003DB RID: 987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_highAngularXLimit_Injected(ref SoftJointLimit value);

		// Token: 0x060003DC RID: 988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularYLimit_Injected(out SoftJointLimit ret);

		// Token: 0x060003DD RID: 989
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularYLimit_Injected(ref SoftJointLimit value);

		// Token: 0x060003DE RID: 990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularZLimit_Injected(out SoftJointLimit ret);

		// Token: 0x060003DF RID: 991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularZLimit_Injected(ref SoftJointLimit value);

		// Token: 0x060003E0 RID: 992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_targetPosition_Injected(out Vector3 ret);

		// Token: 0x060003E1 RID: 993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_targetPosition_Injected(ref Vector3 value);

		// Token: 0x060003E2 RID: 994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_targetVelocity_Injected(out Vector3 ret);

		// Token: 0x060003E3 RID: 995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_targetVelocity_Injected(ref Vector3 value);

		// Token: 0x060003E4 RID: 996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_xDrive_Injected(out JointDrive ret);

		// Token: 0x060003E5 RID: 997
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_xDrive_Injected(ref JointDrive value);

		// Token: 0x060003E6 RID: 998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_yDrive_Injected(out JointDrive ret);

		// Token: 0x060003E7 RID: 999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_yDrive_Injected(ref JointDrive value);

		// Token: 0x060003E8 RID: 1000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_zDrive_Injected(out JointDrive ret);

		// Token: 0x060003E9 RID: 1001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_zDrive_Injected(ref JointDrive value);

		// Token: 0x060003EA RID: 1002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_targetRotation_Injected(out Quaternion ret);

		// Token: 0x060003EB RID: 1003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_targetRotation_Injected(ref Quaternion value);

		// Token: 0x060003EC RID: 1004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_targetAngularVelocity_Injected(out Vector3 ret);

		// Token: 0x060003ED RID: 1005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_targetAngularVelocity_Injected(ref Vector3 value);

		// Token: 0x060003EE RID: 1006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularXDrive_Injected(out JointDrive ret);

		// Token: 0x060003EF RID: 1007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularXDrive_Injected(ref JointDrive value);

		// Token: 0x060003F0 RID: 1008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_angularYZDrive_Injected(out JointDrive ret);

		// Token: 0x060003F1 RID: 1009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_angularYZDrive_Injected(ref JointDrive value);

		// Token: 0x060003F2 RID: 1010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_slerpDrive_Injected(out JointDrive ret);

		// Token: 0x060003F3 RID: 1011
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_slerpDrive_Injected(ref JointDrive value);
	}
}
