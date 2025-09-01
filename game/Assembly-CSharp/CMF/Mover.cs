using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A9 RID: 937
	public class Mover : MonoBehaviour
	{
		// Token: 0x06001EFF RID: 7935 RVA: 0x000B9710 File Offset: 0x000B7910
		private void Awake()
		{
			this.Setup();
			this.control = base.GetComponentInParent<EntityControl>();
			this.sensor = new Sensor(this.tr, this.col);
			this.RecalculateColliderDimensions();
			this.RecalibrateSensor();
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000B9747 File Offset: 0x000B7947
		private void Reset()
		{
			this.Setup();
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000B974F File Offset: 0x000B794F
		private void OnValidate()
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.RecalculateColliderDimensions();
			}
			if (this.sensorType == Sensor.CastType.RaycastArray)
			{
				this.raycastArrayPreviewPositions = Sensor.GetRaycastStartPositions(this.sensorArrayRows, this.sensorArrayRayCount, this.sensorArrayRowsAreOffset, 1f);
			}
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x000B978F File Offset: 0x000B798F
		public void Pause()
		{
			if (this.isPaused)
			{
				return;
			}
			this.isPaused = true;
			this.savedVel = this.rig.velocity;
			this.rig.isKinematic = true;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000B97BE File Offset: 0x000B79BE
		public void Resume()
		{
			if (!this.isPaused)
			{
				return;
			}
			this.isPaused = false;
			this.rig.isKinematic = false;
			this.rig.velocity = this.savedVel;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000B97F0 File Offset: 0x000B79F0
		private void Setup()
		{
			this.tr = base.transform;
			this.col = base.GetComponent<Collider>();
			if (this.col == null)
			{
				this.tr.gameObject.AddComponent<CapsuleCollider>();
				this.col = base.GetComponent<Collider>();
			}
			this.rig = base.GetComponent<Rigidbody>();
			if (this.rig == null)
			{
				this.tr.gameObject.AddComponent<Rigidbody>();
				this.rig = base.GetComponent<Rigidbody>();
			}
			this.boxCollider = base.GetComponent<BoxCollider>();
			this.sphereCollider = base.GetComponent<SphereCollider>();
			this.capsuleCollider = base.GetComponent<CapsuleCollider>();
			this.rig.freezeRotation = true;
			this.rig.useGravity = false;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000B98B3 File Offset: 0x000B7AB3
		private void LateUpdate()
		{
			if (this.isInDebugMode)
			{
				this.sensor.DrawDebug();
			}
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000B98C8 File Offset: 0x000B7AC8
		public void RecalculateColliderDimensions()
		{
			if (this.col == null)
			{
				this.Setup();
				if (this.col == null)
				{
					Debug.LogWarning("There is no collider attached to " + base.gameObject.name + "!");
					return;
				}
			}
			if (this.boxCollider)
			{
				Vector3 zero = Vector3.zero;
				zero.x = this.colliderThickness;
				zero.z = this.colliderThickness;
				this.boxCollider.center = this.colliderOffset * this.colliderHeight;
				zero.y = this.colliderHeight * (1f - this.stepHeightRatio);
				this.boxCollider.size = zero;
				this.boxCollider.center = this.boxCollider.center + new Vector3(0f, this.stepHeightRatio * this.colliderHeight / 2f, 0f);
			}
			else if (this.sphereCollider)
			{
				this.sphereCollider.radius = this.colliderHeight / 2f;
				this.sphereCollider.center = this.colliderOffset * this.colliderHeight;
				this.sphereCollider.center = this.sphereCollider.center + new Vector3(0f, this.stepHeightRatio * this.sphereCollider.radius, 0f);
				this.sphereCollider.radius *= 1f - this.stepHeightRatio;
			}
			else if (this.capsuleCollider)
			{
				this.capsuleCollider.height = this.colliderHeight;
				this.capsuleCollider.center = this.colliderOffset * this.colliderHeight;
				this.capsuleCollider.radius = this.colliderThickness / 2f;
				this.capsuleCollider.center = this.capsuleCollider.center + new Vector3(0f, this.stepHeightRatio * this.capsuleCollider.height / 2f, 0f);
				this.capsuleCollider.height *= 1f - this.stepHeightRatio;
				if (this.capsuleCollider.height / 2f < this.capsuleCollider.radius)
				{
					this.capsuleCollider.radius = this.capsuleCollider.height / 2f;
				}
			}
			if (this.sensor != null)
			{
				this.RecalibrateSensor();
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000B9B6C File Offset: 0x000B7D6C
		private void RecalibrateSensor()
		{
			this.sensor.SetCastOrigin(this.GetColliderCenter());
			this.sensor.SetCastDirection(Sensor.CastDirection.Down);
			this.RecalculateSensorLayerMask();
			this.sensor.castType = this.sensorType;
			float num = this.colliderThickness / 2f * this.sensorRadiusModifier;
			float num2 = 0.001f;
			if (this.boxCollider)
			{
				num = Mathf.Clamp(num, num2, this.boxCollider.size.y / 2f * (1f - num2));
			}
			else if (this.sphereCollider)
			{
				num = Mathf.Clamp(num, num2, this.sphereCollider.radius * (1f - num2));
			}
			else if (this.capsuleCollider)
			{
				num = Mathf.Clamp(num, num2, this.capsuleCollider.height / 2f * (1f - num2));
			}
			this.sensor.sphereCastRadius = num * this.tr.localScale.x;
			float num3 = 0f;
			num3 += this.colliderHeight * (1f - this.stepHeightRatio) * 0.5f;
			num3 += this.colliderHeight * this.stepHeightRatio;
			this.baseSensorRange = num3 * (1f + num2) * this.tr.localScale.x;
			this.sensor.castLength = num3 * this.tr.localScale.x;
			this.sensor.ArrayRows = this.sensorArrayRows;
			this.sensor.arrayRayCount = this.sensorArrayRayCount;
			this.sensor.offsetArrayRows = this.sensorArrayRowsAreOffset;
			this.sensor.isInDebugMode = this.isInDebugMode;
			this.sensor.calculateRealDistance = true;
			this.sensor.calculateRealSurfaceNormal = true;
			this.sensor.RecalibrateRaycastArrayPositions();
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000B9D48 File Offset: 0x000B7F48
		public void RecalculateSensorLayerMask()
		{
			int layer = base.gameObject.layer;
			this.sensor.layermask = this.GetLayerMask();
			this.currentLayer = layer;
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000B9D80 File Offset: 0x000B7F80
		public int GetLayerMask()
		{
			int layer = base.gameObject.layer;
			int num = 0;
			for (int i = 0; i < 32; i++)
			{
				if (!Physics.GetIgnoreLayerCollision(layer, i))
				{
					num |= 1 << i;
				}
			}
			if (num == (num | 1 << LayerMask.NameToLayer("Ignore Raycast")))
			{
				num ^= 1 << LayerMask.NameToLayer("Ignore Raycast");
			}
			return num;
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000B9DE4 File Offset: 0x000B7FE4
		private Vector3 GetColliderCenter()
		{
			if (this.col == null)
			{
				this.Setup();
			}
			return this.col.bounds.center;
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000B9E18 File Offset: 0x000B8018
		private void Check()
		{
			this.currentGroundAdjustmentVelocity = Vector3.zero;
			if (this.IsUsingExtendedSensorRange)
			{
				this.sensor.castLength = this.baseSensorRange + this.colliderHeight * this.tr.localScale.x * this.stepHeightRatio;
			}
			else
			{
				this.sensor.castLength = this.baseSensorRange;
			}
			this.sensor.Cast(ref this.lastContactPoint, ref this.surfaceNormal);
			if (!this.sensor.HasDetectedHit())
			{
				this.isGrounded = false;
				if (this.curSurface != null && this.control.IsMine)
				{
					this.curSurface.LeftSurface();
					if (this.curSurface.ClearOn == StatusSurface.ClearType.Exit)
					{
						if (this.curSurface.RemoveStatusOnExit)
						{
							this.control.net.RemoveStatus(this.curSurface.Status.HashCode, this.control.view.ViewID, 0, false, true);
						}
						this.curSurface = null;
					}
				}
				if (this.StatusSurfaceGraceT > 0f)
				{
					this.StatusSurfaceGraceT -= Time.fixedDeltaTime * 0.5f;
				}
				return;
			}
			this.isGrounded = true;
			if (this.control.IsMine)
			{
				this.UpdateColliderSurfaceInfo();
			}
			float distance = this.sensor.GetDistance();
			float num = this.colliderHeight * this.tr.localScale.x * (1f - this.stepHeightRatio) * 0.5f + this.colliderHeight * this.tr.localScale.x * this.stepHeightRatio - distance;
			this.currentGroundAdjustmentVelocity = this.tr.up * (num / Time.fixedDeltaTime);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000B9FD8 File Offset: 0x000B81D8
		private void UpdateColliderSurfaceInfo()
		{
			Collider collider = this.sensor.GetCollider();
			if (collider == null && this.curSurface != null && this.curSurface.ClearOn == StatusSurface.ClearType.Land)
			{
				return;
			}
			StatusSurface component = collider.GetComponent<StatusSurface>();
			if (component == null && this.curSurface != null && this.StatusSurfaceGraceT > 0f)
			{
				this.StatusSurfaceGraceT -= Time.fixedDeltaTime;
				return;
			}
			if (this.curSurface == component && component != null)
			{
				this.StatusSurfaceGraceT = this.curSurface.GracePeriod;
				component.TickUpdate(this.control);
				return;
			}
			if (this.curSurface != null && this.curSurface.ClearOn == StatusSurface.ClearType.Land && this.curSurface.RemoveStatusOnExit && (component == null || this.curSurface.Status != component.Status))
			{
				this.control.net.RemoveStatus(this.curSurface.Status.HashCode, this.control.view.ViewID, 0, false, true);
			}
			this.curSurface = component;
			if (this.curSurface != null)
			{
				this.StatusSurfaceGraceT = this.curSurface.GracePeriod;
				this.curSurface.Apply(this.control);
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x000BA13E File Offset: 0x000B833E
		public void CheckForGround()
		{
			if (this.currentLayer != base.gameObject.layer)
			{
				this.RecalculateSensorLayerMask();
			}
			this.Check();
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x000BA160 File Offset: 0x000B8360
		public void CheckColliderAbove()
		{
			if (this.currentLayer != base.gameObject.layer)
			{
				this.RecalculateSensorLayerMask();
			}
			this.sensor.castLength = this.baseSensorRange;
			Vector3 origin = this.tr.position + this.colliderHeight * this.tr.localScale.x * this.tr.up;
			this.isColliderAbove = Physics.Raycast(origin, Vector3.up, 0.5f, this.sensor.layermask);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000BA1F8 File Offset: 0x000B83F8
		public void SetVelocity(Vector3 _velocity)
		{
			if (this.control != null && this.control.IsDead)
			{
				this.rig.velocity = Vector3.zero;
				this.rig.angularVelocity = Vector3.zero;
				return;
			}
			this.rig.velocity = _velocity + this.currentGroundAdjustmentVelocity;
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000BA258 File Offset: 0x000B8458
		public bool IsGrounded()
		{
			return this.isGrounded;
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000BA260 File Offset: 0x000B8460
		public bool IsColliderAbove()
		{
			return this.isColliderAbove;
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000BA268 File Offset: 0x000B8468
		public void SetExtendSensorRange(bool _isExtended)
		{
			this.IsUsingExtendedSensorRange = _isExtended;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x000BA271 File Offset: 0x000B8471
		public void SetColliderHeight(float _newColliderHeight)
		{
			if (this.colliderHeight == _newColliderHeight)
			{
				return;
			}
			this.colliderHeight = _newColliderHeight;
			this.RecalculateColliderDimensions();
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x000BA28A File Offset: 0x000B848A
		public void SetColliderThickness(float _newColliderThickness)
		{
			if (this.colliderThickness == _newColliderThickness)
			{
				return;
			}
			if (_newColliderThickness < 0f)
			{
				_newColliderThickness = 0f;
			}
			this.colliderThickness = _newColliderThickness;
			this.RecalculateColliderDimensions();
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000BA2B2 File Offset: 0x000B84B2
		public void SetStepHeightRatio(float _newStepHeightRatio)
		{
			_newStepHeightRatio = Mathf.Clamp(_newStepHeightRatio, 0f, 1f);
			this.stepHeightRatio = _newStepHeightRatio;
			this.RecalculateColliderDimensions();
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x000BA2D3 File Offset: 0x000B84D3
		public Vector3 GetGroundNormal()
		{
			return this.sensor.GetNormal();
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x000BA2E0 File Offset: 0x000B84E0
		public Vector3 GetGroundPoint()
		{
			return this.sensor.GetPosition();
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000BA2ED File Offset: 0x000B84ED
		public Collider GetGroundCollider()
		{
			return this.sensor.GetCollider();
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000BA2FC File Offset: 0x000B84FC
		public Mover()
		{
		}

		// Token: 0x04001F45 RID: 8005
		[Header("Mover Options :")]
		[Range(0f, 1f)]
		[SerializeField]
		private float stepHeightRatio = 0.25f;

		// Token: 0x04001F46 RID: 8006
		[Header("Collider Options :")]
		[SerializeField]
		private float colliderHeight = 2f;

		// Token: 0x04001F47 RID: 8007
		[SerializeField]
		private float colliderThickness = 1f;

		// Token: 0x04001F48 RID: 8008
		[SerializeField]
		private Vector3 colliderOffset = Vector3.zero;

		// Token: 0x04001F49 RID: 8009
		private BoxCollider boxCollider;

		// Token: 0x04001F4A RID: 8010
		private SphereCollider sphereCollider;

		// Token: 0x04001F4B RID: 8011
		private CapsuleCollider capsuleCollider;

		// Token: 0x04001F4C RID: 8012
		[Header("Sensor Options :")]
		[SerializeField]
		public Sensor.CastType sensorType;

		// Token: 0x04001F4D RID: 8013
		private float sensorRadiusModifier = 0.8f;

		// Token: 0x04001F4E RID: 8014
		private int currentLayer;

		// Token: 0x04001F4F RID: 8015
		[SerializeField]
		private bool isInDebugMode;

		// Token: 0x04001F50 RID: 8016
		[Header("Sensor Array Options :")]
		[SerializeField]
		[Range(1f, 5f)]
		private int sensorArrayRows = 1;

		// Token: 0x04001F51 RID: 8017
		[SerializeField]
		[Range(3f, 10f)]
		private int sensorArrayRayCount = 6;

		// Token: 0x04001F52 RID: 8018
		[SerializeField]
		private bool sensorArrayRowsAreOffset;

		// Token: 0x04001F53 RID: 8019
		[HideInInspector]
		public Vector3[] raycastArrayPreviewPositions;

		// Token: 0x04001F54 RID: 8020
		private bool isGrounded;

		// Token: 0x04001F55 RID: 8021
		private bool isColliderAbove;

		// Token: 0x04001F56 RID: 8022
		public Vector3 surfaceNormal;

		// Token: 0x04001F57 RID: 8023
		public Vector3 lastContactPoint;

		// Token: 0x04001F58 RID: 8024
		public StatusSurface curSurface;

		// Token: 0x04001F59 RID: 8025
		private float StatusSurfaceGraceT;

		// Token: 0x04001F5A RID: 8026
		private bool IsUsingExtendedSensorRange = true;

		// Token: 0x04001F5B RID: 8027
		private float baseSensorRange;

		// Token: 0x04001F5C RID: 8028
		private Vector3 currentGroundAdjustmentVelocity = Vector3.zero;

		// Token: 0x04001F5D RID: 8029
		private Collider col;

		// Token: 0x04001F5E RID: 8030
		private Rigidbody rig;

		// Token: 0x04001F5F RID: 8031
		private Transform tr;

		// Token: 0x04001F60 RID: 8032
		private Sensor sensor;

		// Token: 0x04001F61 RID: 8033
		private EntityControl control;

		// Token: 0x04001F62 RID: 8034
		private Vector3 savedVel;

		// Token: 0x04001F63 RID: 8035
		private bool isPaused;
	}
}
