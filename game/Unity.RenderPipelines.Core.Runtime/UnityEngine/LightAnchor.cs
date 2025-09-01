using System;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[AddComponentMenu("Rendering/Light Anchor")]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class LightAnchor : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002082 File Offset: 0x00000282
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000208A File Offset: 0x0000028A
		public float yaw
		{
			get
			{
				return this.m_Yaw;
			}
			set
			{
				this.m_Yaw = LightAnchor.NormalizeAngleDegree(value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002098 File Offset: 0x00000298
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020A0 File Offset: 0x000002A0
		public float pitch
		{
			get
			{
				return this.m_Pitch;
			}
			set
			{
				this.m_Pitch = LightAnchor.NormalizeAngleDegree(value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020AE File Offset: 0x000002AE
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020B6 File Offset: 0x000002B6
		public float roll
		{
			get
			{
				return this.m_Roll;
			}
			set
			{
				this.m_Roll = LightAnchor.NormalizeAngleDegree(value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C4 File Offset: 0x000002C4
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020CC File Offset: 0x000002CC
		public float distance
		{
			get
			{
				return this.m_Distance;
			}
			set
			{
				this.m_Distance = Mathf.Clamp(value, 0f, 10000f);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020E4 File Offset: 0x000002E4
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020EC File Offset: 0x000002EC
		public LightAnchor.UpDirection frameSpace
		{
			get
			{
				return this.m_FrameSpace;
			}
			set
			{
				this.m_FrameSpace = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020F8 File Offset: 0x000002F8
		public Vector3 anchorPosition
		{
			get
			{
				if (this.anchorPositionOverride != null)
				{
					return this.anchorPositionOverride.position + this.anchorPositionOverride.TransformDirection(this.anchorPositionOffset);
				}
				return base.transform.position + base.transform.forward * this.distance;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000215B File Offset: 0x0000035B
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002163 File Offset: 0x00000363
		public Transform anchorPositionOverride
		{
			get
			{
				return this.m_AnchorPositionOverride;
			}
			set
			{
				this.m_AnchorPositionOverride = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000216C File Offset: 0x0000036C
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002174 File Offset: 0x00000374
		public Vector3 anchorPositionOffset
		{
			get
			{
				return this.m_AnchorPositionOffset;
			}
			set
			{
				this.m_AnchorPositionOffset = value;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000217D File Offset: 0x0000037D
		public static float NormalizeAngleDegree(float angle)
		{
			float num = angle - -180f;
			return num - Mathf.Floor(num / 360f) * 360f + -180f;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021A0 File Offset: 0x000003A0
		public void SynchronizeOnTransform(Camera camera)
		{
			LightAnchor.Axes worldSpaceAxes = this.GetWorldSpaceAxes(camera, this.anchorPosition);
			Vector3 vector = base.transform.position - this.anchorPosition;
			if (vector.magnitude == 0f)
			{
				vector = -base.transform.forward;
			}
			Vector3 vector2 = Vector3.ProjectOnPlane(vector, worldSpaceAxes.up);
			if (vector2.magnitude < 0.0001f)
			{
				vector2 = Vector3.ProjectOnPlane(vector, worldSpaceAxes.up + worldSpaceAxes.right * 0.0001f);
			}
			vector2.Normalize();
			float num = Vector3.SignedAngle(worldSpaceAxes.forward, vector2, worldSpaceAxes.up);
			Vector3 axis = Quaternion.AngleAxis(num, worldSpaceAxes.up) * worldSpaceAxes.right;
			float pitch = Vector3.SignedAngle(vector2, vector, axis);
			this.yaw = num;
			this.pitch = pitch;
			this.roll = base.transform.rotation.eulerAngles.z;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000229C File Offset: 0x0000049C
		public void UpdateTransform(Camera camera, Vector3 anchor)
		{
			LightAnchor.Axes worldSpaceAxes = this.GetWorldSpaceAxes(camera, anchor);
			this.UpdateTransform(worldSpaceAxes.up, worldSpaceAxes.right, worldSpaceAxes.forward, anchor);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022CC File Offset: 0x000004CC
		private LightAnchor.Axes GetWorldSpaceAxes(Camera camera, Vector3 anchor)
		{
			if (base.transform.IsChildOf(camera.transform))
			{
				return new LightAnchor.Axes
				{
					up = Vector3.up,
					right = Vector3.right,
					forward = Vector3.forward
				};
			}
			Matrix4x4 lhs = camera.cameraToWorldMatrix;
			if (this.m_FrameSpace == LightAnchor.UpDirection.Local)
			{
				Vector3 up = Camera.main.transform.up;
				lhs = (Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * Matrix4x4.LookAt(camera.transform.position, anchor, up).inverse).inverse;
			}
			else if (!camera.orthographic && camera.transform.position != anchor)
			{
				Quaternion q = Quaternion.LookRotation((anchor - camera.transform.position).normalized);
				lhs = (Matrix4x4.Scale(new Vector3(1f, 1f, -1f)) * Matrix4x4.TRS(camera.transform.position, q, Vector3.one).inverse).inverse;
			}
			Vector3 up2 = (lhs * Vector3.up).normalized;
			Vector3 right = (lhs * Vector3.right).normalized;
			Vector3 forward = (lhs * Vector3.forward).normalized;
			return new LightAnchor.Axes
			{
				up = up2,
				right = right,
				forward = forward
			};
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002488 File Offset: 0x00000688
		private void Update()
		{
			if (this.anchorPositionOverride == null || Camera.main == null)
			{
				return;
			}
			if (this.anchorPositionOverride.hasChanged || Camera.main.transform.hasChanged)
			{
				this.UpdateTransform(Camera.main, this.anchorPosition);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024E0 File Offset: 0x000006E0
		private void OnDrawGizmosSelected()
		{
			Camera main = Camera.main;
			if (main == null)
			{
				return;
			}
			Vector3 anchorPosition = this.anchorPosition;
			LightAnchor.Axes worldSpaceAxes = this.GetWorldSpaceAxes(main, anchorPosition);
			Vector3.ProjectOnPlane(base.transform.position - anchorPosition, worldSpaceAxes.up);
			Mathf.Min(this.distance * 0.25f, 5f);
			Mathf.Min(this.distance * 0.5f, 10f);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002558 File Offset: 0x00000758
		private void UpdateTransform(Vector3 up, Vector3 right, Vector3 forward, Vector3 anchor)
		{
			Quaternion lhs = Quaternion.AngleAxis(this.m_Yaw, up);
			Quaternion rhs = Quaternion.AngleAxis(this.m_Pitch, right);
			Vector3 position = anchor + lhs * rhs * forward * this.distance;
			base.transform.position = position;
			Vector3 eulerAngles = Quaternion.LookRotation(-(lhs * rhs * forward).normalized, up).eulerAngles;
			eulerAngles.z = this.m_Roll;
			base.transform.eulerAngles = eulerAngles;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025EF File Offset: 0x000007EF
		public LightAnchor()
		{
		}

		// Token: 0x04000002 RID: 2
		private const float k_ArcRadius = 5f;

		// Token: 0x04000003 RID: 3
		private const float k_AxisLength = 10f;

		// Token: 0x04000004 RID: 4
		internal const float k_MaxDistance = 10000f;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		[Min(0f)]
		private float m_Distance;

		// Token: 0x04000006 RID: 6
		[SerializeField]
		private LightAnchor.UpDirection m_FrameSpace;

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private Transform m_AnchorPositionOverride;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private Vector3 m_AnchorPositionOffset;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private float m_Yaw;

		// Token: 0x0400000A RID: 10
		[SerializeField]
		private float m_Pitch;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		private float m_Roll;

		// Token: 0x02000109 RID: 265
		public enum UpDirection
		{
			// Token: 0x0400045B RID: 1115
			World,
			// Token: 0x0400045C RID: 1116
			Local
		}

		// Token: 0x0200010A RID: 266
		private struct Axes
		{
			// Token: 0x0400045D RID: 1117
			public Vector3 up;

			// Token: 0x0400045E RID: 1118
			public Vector3 right;

			// Token: 0x0400045F RID: 1119
			public Vector3 forward;
		}
	}
}
