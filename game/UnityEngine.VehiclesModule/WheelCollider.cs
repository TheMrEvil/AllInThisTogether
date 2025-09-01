using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/Vehicles/WheelCollider.h")]
	[NativeHeader("PhysicsScriptingClasses.h")]
	public class WheelCollider : Collider
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002170 File Offset: 0x00000370
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002186 File Offset: 0x00000386
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19
		// (set) Token: 0x06000014 RID: 20
		public extern float radius { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000015 RID: 21
		// (set) Token: 0x06000016 RID: 22
		public extern float suspensionDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002190 File Offset: 0x00000390
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021A6 File Offset: 0x000003A6
		public JointSpring suspensionSpring
		{
			get
			{
				JointSpring result;
				this.get_suspensionSpring_Injected(out result);
				return result;
			}
			set
			{
				this.set_suspensionSpring_Injected(ref value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000019 RID: 25
		// (set) Token: 0x0600001A RID: 26
		public extern bool suspensionExpansionLimited { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001B RID: 27
		// (set) Token: 0x0600001C RID: 28
		public extern float forceAppPointDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001D RID: 29
		// (set) Token: 0x0600001E RID: 30
		public extern float mass { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31
		// (set) Token: 0x06000020 RID: 32
		public extern float wheelDampingRate { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000021B0 File Offset: 0x000003B0
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000021C6 File Offset: 0x000003C6
		public WheelFrictionCurve forwardFriction
		{
			get
			{
				WheelFrictionCurve result;
				this.get_forwardFriction_Injected(out result);
				return result;
			}
			set
			{
				this.set_forwardFriction_Injected(ref value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000021D0 File Offset: 0x000003D0
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000021E6 File Offset: 0x000003E6
		public WheelFrictionCurve sidewaysFriction
		{
			get
			{
				WheelFrictionCurve result;
				this.get_sidewaysFriction_Injected(out result);
				return result;
			}
			set
			{
				this.set_sidewaysFriction_Injected(ref value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000025 RID: 37
		// (set) Token: 0x06000026 RID: 38
		public extern float motorTorque { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000027 RID: 39
		// (set) Token: 0x06000028 RID: 40
		public extern float brakeTorque { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000029 RID: 41
		// (set) Token: 0x0600002A RID: 42
		public extern float steerAngle { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002B RID: 43
		public extern bool isGrounded { [NativeName("IsGrounded")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600002C RID: 44
		public extern float rpm { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002D RID: 45
		// (set) Token: 0x0600002E RID: 46
		public extern float sprungMass { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600002F RID: 47
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetSprungMasses();

		// Token: 0x06000030 RID: 48
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ConfigureVehicleSubsteps(float speedThreshold, int stepsBelowThreshold, int stepsAboveThreshold);

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetWorldPose(out Vector3 pos, out Quaternion quat);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetGroundHit(out WheelHit hit);

		// Token: 0x06000033 RID: 51 RVA: 0x000021F0 File Offset: 0x000003F0
		public WheelCollider()
		{
		}

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_suspensionSpring_Injected(out JointSpring ret);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_suspensionSpring_Injected(ref JointSpring value);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_forwardFriction_Injected(out WheelFrictionCurve ret);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_forwardFriction_Injected(ref WheelFrictionCurve value);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_sidewaysFriction_Injected(out WheelFrictionCurve ret);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_sidewaysFriction_Injected(ref WheelFrictionCurve value);
	}
}
