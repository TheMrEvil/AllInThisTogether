using System;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000008 RID: 8
	[UsedByNativeCode]
	public struct XRNodeState
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002314 File Offset: 0x00000514
		public ulong uniqueID
		{
			get
			{
				return this.m_UniqueID;
			}
			set
			{
				this.m_UniqueID = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002320 File Offset: 0x00000520
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002338 File Offset: 0x00000538
		public XRNode nodeType
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002344 File Offset: 0x00000544
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000235F File Offset: 0x0000055F
		public bool tracked
		{
			get
			{
				return this.m_Tracked == 1;
			}
			set
			{
				this.m_Tracked = (value ? 1 : 0);
			}
		}

		// Token: 0x17000005 RID: 5
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000236F File Offset: 0x0000056F
		public Vector3 position
		{
			set
			{
				this.m_Position = value;
				this.m_AvailableFields |= AvailableTrackingData.PositionAvailable;
			}
		}

		// Token: 0x17000006 RID: 6
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002387 File Offset: 0x00000587
		public Quaternion rotation
		{
			set
			{
				this.m_Rotation = value;
				this.m_AvailableFields |= AvailableTrackingData.RotationAvailable;
			}
		}

		// Token: 0x17000007 RID: 7
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000239F File Offset: 0x0000059F
		public Vector3 velocity
		{
			set
			{
				this.m_Velocity = value;
				this.m_AvailableFields |= AvailableTrackingData.VelocityAvailable;
			}
		}

		// Token: 0x17000008 RID: 8
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000023B7 File Offset: 0x000005B7
		public Vector3 angularVelocity
		{
			set
			{
				this.m_AngularVelocity = value;
				this.m_AvailableFields |= AvailableTrackingData.AngularVelocityAvailable;
			}
		}

		// Token: 0x17000009 RID: 9
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000023CF File Offset: 0x000005CF
		public Vector3 acceleration
		{
			set
			{
				this.m_Acceleration = value;
				this.m_AvailableFields |= AvailableTrackingData.AccelerationAvailable;
			}
		}

		// Token: 0x1700000A RID: 10
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000023E8 File Offset: 0x000005E8
		public Vector3 angularAcceleration
		{
			set
			{
				this.m_AngularAcceleration = value;
				this.m_AvailableFields |= AvailableTrackingData.AngularAccelerationAvailable;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002404 File Offset: 0x00000604
		public bool TryGetPosition(out Vector3 position)
		{
			return this.TryGet(this.m_Position, AvailableTrackingData.PositionAvailable, out position);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002424 File Offset: 0x00000624
		public bool TryGetRotation(out Quaternion rotation)
		{
			return this.TryGet(this.m_Rotation, AvailableTrackingData.RotationAvailable, out rotation);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002444 File Offset: 0x00000644
		public bool TryGetVelocity(out Vector3 velocity)
		{
			return this.TryGet(this.m_Velocity, AvailableTrackingData.VelocityAvailable, out velocity);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002464 File Offset: 0x00000664
		public bool TryGetAngularVelocity(out Vector3 angularVelocity)
		{
			return this.TryGet(this.m_AngularVelocity, AvailableTrackingData.AngularVelocityAvailable, out angularVelocity);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002484 File Offset: 0x00000684
		public bool TryGetAcceleration(out Vector3 acceleration)
		{
			return this.TryGet(this.m_Acceleration, AvailableTrackingData.AccelerationAvailable, out acceleration);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000024A8 File Offset: 0x000006A8
		public bool TryGetAngularAcceleration(out Vector3 angularAcceleration)
		{
			return this.TryGet(this.m_AngularAcceleration, AvailableTrackingData.AngularAccelerationAvailable, out angularAcceleration);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000024CC File Offset: 0x000006CC
		private bool TryGet(Vector3 inValue, AvailableTrackingData availabilityFlag, out Vector3 outValue)
		{
			bool flag = (this.m_AvailableFields & availabilityFlag) > AvailableTrackingData.None;
			bool result;
			if (flag)
			{
				outValue = inValue;
				result = true;
			}
			else
			{
				outValue = Vector3.zero;
				result = false;
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002508 File Offset: 0x00000708
		private bool TryGet(Quaternion inValue, AvailableTrackingData availabilityFlag, out Quaternion outValue)
		{
			bool flag = (this.m_AvailableFields & availabilityFlag) > AvailableTrackingData.None;
			bool result;
			if (flag)
			{
				outValue = inValue;
				result = true;
			}
			else
			{
				outValue = Quaternion.identity;
				result = false;
			}
			return result;
		}

		// Token: 0x0400001C RID: 28
		private XRNode m_Type;

		// Token: 0x0400001D RID: 29
		private AvailableTrackingData m_AvailableFields;

		// Token: 0x0400001E RID: 30
		private Vector3 m_Position;

		// Token: 0x0400001F RID: 31
		private Quaternion m_Rotation;

		// Token: 0x04000020 RID: 32
		private Vector3 m_Velocity;

		// Token: 0x04000021 RID: 33
		private Vector3 m_AngularVelocity;

		// Token: 0x04000022 RID: 34
		private Vector3 m_Acceleration;

		// Token: 0x04000023 RID: 35
		private Vector3 m_AngularAcceleration;

		// Token: 0x04000024 RID: 36
		private int m_Tracked;

		// Token: 0x04000025 RID: 37
		private ulong m_UniqueID;
	}
}
