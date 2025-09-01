using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion
{
	// Token: 0x020000B0 RID: 176
	public class CameraController : MonoBehaviour
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00036760 File Offset: 0x00034960
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00036768 File Offset: 0x00034968
		public float x
		{
			[CompilerGenerated]
			get
			{
				return this.<x>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<x>k__BackingField = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00036771 File Offset: 0x00034971
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00036779 File Offset: 0x00034979
		public float y
		{
			[CompilerGenerated]
			get
			{
				return this.<y>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<y>k__BackingField = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00036782 File Offset: 0x00034982
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0003678A File Offset: 0x0003498A
		public float distanceTarget
		{
			[CompilerGenerated]
			get
			{
				return this.<distanceTarget>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<distanceTarget>k__BackingField = value;
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00036794 File Offset: 0x00034994
		public void SetAngles(Quaternion rotation)
		{
			Vector3 eulerAngles = rotation.eulerAngles;
			this.x = eulerAngles.y;
			this.y = eulerAngles.x;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000367C1 File Offset: 0x000349C1
		public void SetAngles(float yaw, float pitch)
		{
			this.x = yaw;
			this.y = pitch;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000367D4 File Offset: 0x000349D4
		protected virtual void Awake()
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			this.x = eulerAngles.y;
			this.y = eulerAngles.x;
			this.distanceTarget = this.distance;
			this.smoothPosition = base.transform.position;
			this.cam = base.GetComponent<Camera>();
			this.lastUp = ((this.rotationSpace != null) ? this.rotationSpace.up : Vector3.up);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00036854 File Offset: 0x00034A54
		protected virtual void Update()
		{
			if (this.updateMode == CameraController.UpdateMode.Update)
			{
				this.UpdateTransform();
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00036864 File Offset: 0x00034A64
		protected virtual void FixedUpdate()
		{
			this.fixedFrame = true;
			this.fixedDeltaTime += Time.deltaTime;
			if (this.updateMode == CameraController.UpdateMode.FixedUpdate)
			{
				this.UpdateTransform();
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00036890 File Offset: 0x00034A90
		protected virtual void LateUpdate()
		{
			this.UpdateInput();
			if (this.updateMode == CameraController.UpdateMode.LateUpdate)
			{
				this.UpdateTransform();
			}
			if (this.updateMode == CameraController.UpdateMode.FixedLateUpdate && this.fixedFrame)
			{
				this.UpdateTransform(this.fixedDeltaTime);
				this.fixedDeltaTime = 0f;
				this.fixedFrame = false;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000368E4 File Offset: 0x00034AE4
		public void UpdateInput()
		{
			if (!this.cam.enabled)
			{
				return;
			}
			Cursor.lockState = (this.lockCursor ? CursorLockMode.Locked : CursorLockMode.None);
			Cursor.visible = !this.lockCursor;
			if (this.rotateAlways || (this.rotateOnLeftButton && Input.GetMouseButton(0)) || (this.rotateOnRightButton && Input.GetMouseButton(1)) || (this.rotateOnMiddleButton && Input.GetMouseButton(2)))
			{
				this.x += Input.GetAxis("Mouse X") * this.rotationSensitivity;
				this.y = this.ClampAngle(this.y - Input.GetAxis("Mouse Y") * this.rotationSensitivity, this.yMinLimit, this.yMaxLimit);
			}
			this.distanceTarget = Mathf.Clamp(this.distanceTarget + this.zoomAdd, this.minDistance, this.maxDistance);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000369D1 File Offset: 0x00034BD1
		public void UpdateTransform()
		{
			this.UpdateTransform(Time.deltaTime);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000369E0 File Offset: 0x00034BE0
		public void UpdateTransform(float deltaTime)
		{
			if (!this.cam.enabled)
			{
				return;
			}
			this.rotation = Quaternion.AngleAxis(this.x, Vector3.up) * Quaternion.AngleAxis(this.y, Vector3.right);
			if (this.rotationSpace != null)
			{
				this.r = Quaternion.FromToRotation(this.lastUp, this.rotationSpace.up) * this.r;
				this.rotation = this.r * this.rotation;
				this.lastUp = this.rotationSpace.up;
			}
			if (this.target != null)
			{
				this.distance += (this.distanceTarget - this.distance) * this.zoomSpeed * deltaTime;
				if (!this.smoothFollow)
				{
					this.smoothPosition = this.target.position;
				}
				else
				{
					this.smoothPosition = Vector3.Lerp(this.smoothPosition, this.target.position, deltaTime * this.followSpeed);
				}
				Vector3 a = this.smoothPosition + this.rotation * this.offset;
				Vector3 vector = this.rotation * -Vector3.forward;
				if (this.blockingLayers != -1)
				{
					RaycastHit raycastHit;
					if (Physics.SphereCast(a - vector * this.blockingOriginOffset, this.blockingRadius, vector, out raycastHit, this.blockingOriginOffset + this.distanceTarget - this.blockingRadius, this.blockingLayers))
					{
						this.blockedDistance = Mathf.SmoothDamp(this.blockedDistance, raycastHit.distance + this.blockingRadius * (1f - this.blockedOffset) - this.blockingOriginOffset, ref this.blockedDistanceV, this.blockingSmoothTime);
					}
					else
					{
						this.blockedDistance = this.distanceTarget;
					}
					this.distance = Mathf.Min(this.distance, this.blockedDistance);
				}
				this.position = a + vector * this.distance;
				base.transform.position = this.position;
			}
			base.transform.rotation = this.rotation;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00036C1C File Offset: 0x00034E1C
		private float zoomAdd
		{
			get
			{
				float axis = Input.GetAxis("Mouse ScrollWheel");
				if (axis > 0f)
				{
					return -this.zoomSensitivity;
				}
				if (axis < 0f)
				{
					return this.zoomSensitivity;
				}
				return 0f;
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00036C58 File Offset: 0x00034E58
		private float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
			return Mathf.Clamp(angle, min, max);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00036C84 File Offset: 0x00034E84
		public CameraController()
		{
		}

		// Token: 0x0400064F RID: 1615
		public Transform target;

		// Token: 0x04000650 RID: 1616
		public Transform rotationSpace;

		// Token: 0x04000651 RID: 1617
		public CameraController.UpdateMode updateMode = CameraController.UpdateMode.LateUpdate;

		// Token: 0x04000652 RID: 1618
		public bool lockCursor = true;

		// Token: 0x04000653 RID: 1619
		[Header("Position")]
		public bool smoothFollow;

		// Token: 0x04000654 RID: 1620
		public Vector3 offset = new Vector3(0f, 1.5f, 0.5f);

		// Token: 0x04000655 RID: 1621
		public float followSpeed = 10f;

		// Token: 0x04000656 RID: 1622
		[Header("Rotation")]
		public float rotationSensitivity = 3.5f;

		// Token: 0x04000657 RID: 1623
		public float yMinLimit = -20f;

		// Token: 0x04000658 RID: 1624
		public float yMaxLimit = 80f;

		// Token: 0x04000659 RID: 1625
		public bool rotateAlways = true;

		// Token: 0x0400065A RID: 1626
		public bool rotateOnLeftButton;

		// Token: 0x0400065B RID: 1627
		public bool rotateOnRightButton;

		// Token: 0x0400065C RID: 1628
		public bool rotateOnMiddleButton;

		// Token: 0x0400065D RID: 1629
		[Header("Distance")]
		public float distance = 10f;

		// Token: 0x0400065E RID: 1630
		public float minDistance = 4f;

		// Token: 0x0400065F RID: 1631
		public float maxDistance = 10f;

		// Token: 0x04000660 RID: 1632
		public float zoomSpeed = 10f;

		// Token: 0x04000661 RID: 1633
		public float zoomSensitivity = 1f;

		// Token: 0x04000662 RID: 1634
		[Header("Blocking")]
		public LayerMask blockingLayers;

		// Token: 0x04000663 RID: 1635
		public float blockingRadius = 1f;

		// Token: 0x04000664 RID: 1636
		public float blockingSmoothTime = 0.1f;

		// Token: 0x04000665 RID: 1637
		public float blockingOriginOffset;

		// Token: 0x04000666 RID: 1638
		[Range(0f, 1f)]
		public float blockedOffset = 0.5f;

		// Token: 0x04000667 RID: 1639
		[CompilerGenerated]
		private float <x>k__BackingField;

		// Token: 0x04000668 RID: 1640
		[CompilerGenerated]
		private float <y>k__BackingField;

		// Token: 0x04000669 RID: 1641
		[CompilerGenerated]
		private float <distanceTarget>k__BackingField;

		// Token: 0x0400066A RID: 1642
		private Vector3 targetDistance;

		// Token: 0x0400066B RID: 1643
		private Vector3 position;

		// Token: 0x0400066C RID: 1644
		private Quaternion rotation = Quaternion.identity;

		// Token: 0x0400066D RID: 1645
		private Vector3 smoothPosition;

		// Token: 0x0400066E RID: 1646
		private Camera cam;

		// Token: 0x0400066F RID: 1647
		private bool fixedFrame;

		// Token: 0x04000670 RID: 1648
		private float fixedDeltaTime;

		// Token: 0x04000671 RID: 1649
		private Quaternion r = Quaternion.identity;

		// Token: 0x04000672 RID: 1650
		private Vector3 lastUp;

		// Token: 0x04000673 RID: 1651
		private float blockedDistance = 10f;

		// Token: 0x04000674 RID: 1652
		private float blockedDistanceV;

		// Token: 0x020001E1 RID: 481
		[Serializable]
		public enum UpdateMode
		{
			// Token: 0x04000E57 RID: 3671
			Update,
			// Token: 0x04000E58 RID: 3672
			FixedUpdate,
			// Token: 0x04000E59 RID: 3673
			LateUpdate,
			// Token: 0x04000E5A RID: 3674
			FixedLateUpdate
		}
	}
}
