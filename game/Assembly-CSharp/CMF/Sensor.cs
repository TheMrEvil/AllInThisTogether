using System;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003AA RID: 938
	[Serializable]
	public class Sensor
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x000BA368 File Offset: 0x000B8568
		public Sensor(Transform _transform, Collider _collider)
		{
			this.tr = _transform;
			if (_collider == null)
			{
				return;
			}
			this.ignoreList = new Collider[1];
			this.ignoreList[0] = _collider;
			this.ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
			this.ignoreListLayers = new int[this.ignoreList.Length];
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000BA430 File Offset: 0x000B8630
		private void ResetFlags()
		{
			this.hasDetectedHit = false;
			this.hitPosition = Vector3.zero;
			this.hitNormal = -this.GetCastDirection();
			this.hitDistance = 0f;
			if (this.hitColliders.Count > 0)
			{
				this.hitColliders.Clear();
			}
			if (this.hitTransforms.Count > 0)
			{
				this.hitTransforms.Clear();
			}
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000BA4A0 File Offset: 0x000B86A0
		public static Vector3[] GetRaycastStartPositions(int sensorRows, int sensorRayCount, bool offsetRows, float sensorRadius)
		{
			List<Vector3> list = new List<Vector3>();
			Vector3 zero = Vector3.zero;
			list.Add(zero);
			for (int i = 0; i < sensorRows; i++)
			{
				float num = (float)(i + 1) / (float)sensorRows;
				for (int j = 0; j < sensorRayCount * (i + 1); j++)
				{
					float num2 = 360f / (float)(sensorRayCount * (i + 1)) * (float)j;
					if (offsetRows && i % 2 == 0)
					{
						num2 += 360f / (float)(sensorRayCount * (i + 1)) / 2f;
					}
					float x = num * Mathf.Cos(0.017453292f * num2);
					float z = num * Mathf.Sin(0.017453292f * num2);
					list.Add(new Vector3(x, 0f, z) * sensorRadius);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x000BA564 File Offset: 0x000B8764
		public void Cast(ref Vector3 contactPt, ref Vector3 normal)
		{
			this.ResetFlags();
			Vector3 direction = this.GetCastDirection();
			Vector3 vector = this.tr.TransformPoint(this.origin);
			if (this.ignoreListLayers.Length != this.ignoreList.Length)
			{
				this.ignoreListLayers = new int[this.ignoreList.Length];
			}
			for (int i = 0; i < this.ignoreList.Length; i++)
			{
				this.ignoreListLayers[i] = this.ignoreList[i].gameObject.layer;
				this.ignoreList[i].gameObject.layer = this.ignoreRaycastLayer;
			}
			switch (this.castType)
			{
			case Sensor.CastType.Raycast:
				this.CastRay(vector, direction);
				break;
			case Sensor.CastType.RaycastArray:
				this.CastRayArray(vector, direction);
				break;
			case Sensor.CastType.Spherecast:
				this.CastSphere(vector, direction);
				break;
			default:
				this.hasDetectedHit = false;
				break;
			}
			if (this.hasDetectedHit)
			{
				normal = this.hitNormal;
				contactPt = this.hitPosition;
			}
			for (int j = 0; j < this.ignoreList.Length; j++)
			{
				this.ignoreList[j].gameObject.layer = this.ignoreListLayers[j];
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000BA690 File Offset: 0x000B8890
		private void CastRayArray(Vector3 _origin, Vector3 _direction)
		{
			Vector3 zero = Vector3.zero;
			Vector3 direction = this.GetCastDirection();
			this.arrayNormals.Clear();
			this.arrayPoints.Clear();
			for (int i = 0; i < this.raycastArrayStartPositions.Length; i++)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(_origin + this.tr.TransformDirection(this.raycastArrayStartPositions[i]), direction, out raycastHit, this.castLength, this.layermask, QueryTriggerInteraction.Ignore))
				{
					if (this.isInDebugMode)
					{
						Debug.DrawRay(raycastHit.point, raycastHit.normal, Color.red, Time.fixedDeltaTime * 1.01f);
					}
					this.hitColliders.Add(raycastHit.collider);
					this.hitTransforms.Add(raycastHit.transform);
					this.arrayNormals.Add(raycastHit.normal);
					this.arrayPoints.Add(raycastHit.point);
				}
			}
			this.hasDetectedHit = (this.arrayPoints.Count > 0);
			if (this.hasDetectedHit)
			{
				Vector3 a = Vector3.zero;
				for (int j = 0; j < this.arrayNormals.Count; j++)
				{
					a += this.arrayNormals[j];
				}
				a.Normalize();
				Vector3 a2 = Vector3.zero;
				for (int k = 0; k < this.arrayPoints.Count; k++)
				{
					a2 += this.arrayPoints[k];
				}
				a2 /= (float)this.arrayPoints.Count;
				this.hitPosition = a2;
				this.hitNormal = a;
				this.hitDistance = VectorMath.ExtractDotVector(_origin - this.hitPosition, _direction).magnitude;
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000BA85C File Offset: 0x000B8A5C
		private void CastRay(Vector3 _origin, Vector3 _direction)
		{
			RaycastHit raycastHit;
			this.hasDetectedHit = Physics.Raycast(_origin, _direction, out raycastHit, this.castLength, this.layermask, QueryTriggerInteraction.Ignore);
			if (this.hasDetectedHit)
			{
				this.hitPosition = raycastHit.point;
				this.hitNormal = raycastHit.normal;
				this.hitColliders.Add(raycastHit.collider);
				this.hitTransforms.Add(raycastHit.transform);
				this.hitDistance = raycastHit.distance;
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000BA8E0 File Offset: 0x000B8AE0
		private void CastSphere(Vector3 _origin, Vector3 _direction)
		{
			RaycastHit raycastHit;
			this.hasDetectedHit = Physics.SphereCast(_origin, this.sphereCastRadius, _direction, out raycastHit, this.castLength - this.sphereCastRadius, this.layermask, QueryTriggerInteraction.Ignore);
			if (this.hasDetectedHit)
			{
				this.hitPosition = raycastHit.point;
				this.hitNormal = raycastHit.normal;
				this.hitColliders.Add(raycastHit.collider);
				this.hitTransforms.Add(raycastHit.transform);
				this.hitDistance = raycastHit.distance;
				this.hitDistance += this.sphereCastRadius;
				if (this.calculateRealDistance)
				{
					this.hitDistance = VectorMath.ExtractDotVector(_origin - this.hitPosition, _direction).magnitude;
				}
				Collider collider = this.hitColliders[0];
				if (this.calculateRealSurfaceNormal)
				{
					if (collider.Raycast(new Ray(this.hitPosition - _direction, _direction), out raycastHit, 1.5f))
					{
						if (Vector3.Angle(raycastHit.normal, -_direction) >= 89f)
						{
							this.hitNormal = this.backupNormal;
						}
						else
						{
							this.hitNormal = raycastHit.normal;
						}
					}
					else
					{
						this.hitNormal = this.backupNormal;
					}
					this.backupNormal = this.hitNormal;
				}
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000BAA30 File Offset: 0x000B8C30
		private Vector3 GetCastDirection()
		{
			switch (this.castDirection)
			{
			case Sensor.CastDirection.Forward:
				return this.tr.forward;
			case Sensor.CastDirection.Right:
				return this.tr.right;
			case Sensor.CastDirection.Up:
				return this.tr.up;
			case Sensor.CastDirection.Backward:
				return -this.tr.forward;
			case Sensor.CastDirection.Left:
				return -this.tr.right;
			case Sensor.CastDirection.Down:
				return -this.tr.up;
			default:
				return Vector3.one;
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000BAAC0 File Offset: 0x000B8CC0
		public void DrawDebug()
		{
			if (this.hasDetectedHit && this.isInDebugMode)
			{
				Debug.DrawRay(this.hitPosition, this.hitNormal, Color.red, Time.deltaTime);
				float d = 0.2f;
				Debug.DrawLine(this.hitPosition + Vector3.up * d, this.hitPosition - Vector3.up * d, Color.green, Time.deltaTime);
				Debug.DrawLine(this.hitPosition + Vector3.right * d, this.hitPosition - Vector3.right * d, Color.green, Time.deltaTime);
				Debug.DrawLine(this.hitPosition + Vector3.forward * d, this.hitPosition - Vector3.forward * d, Color.green, Time.deltaTime);
			}
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000BABB5 File Offset: 0x000B8DB5
		public bool HasDetectedHit()
		{
			return this.hasDetectedHit;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000BABBD File Offset: 0x000B8DBD
		public float GetDistance()
		{
			return this.hitDistance;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x000BABC5 File Offset: 0x000B8DC5
		public Vector3 GetNormal()
		{
			return this.hitNormal;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000BABCD File Offset: 0x000B8DCD
		public Vector3 GetPosition()
		{
			return this.hitPosition;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x000BABD5 File Offset: 0x000B8DD5
		public Collider GetCollider()
		{
			return this.hitColliders[0];
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000BABE3 File Offset: 0x000B8DE3
		public Transform GetTransform()
		{
			return this.hitTransforms[0];
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000BABF1 File Offset: 0x000B8DF1
		public void SetCastOrigin(Vector3 _origin)
		{
			if (this.tr == null)
			{
				return;
			}
			this.origin = this.tr.InverseTransformPoint(_origin);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000BAC14 File Offset: 0x000B8E14
		public void SetCastDirection(Sensor.CastDirection _direction)
		{
			if (this.tr == null)
			{
				return;
			}
			this.castDirection = _direction;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000BAC2C File Offset: 0x000B8E2C
		public void RecalibrateRaycastArrayPositions()
		{
			this.raycastArrayStartPositions = Sensor.GetRaycastStartPositions(this.ArrayRows, this.arrayRayCount, this.offsetArrayRows, this.sphereCastRadius);
		}

		// Token: 0x04001F64 RID: 8036
		public float castLength = 1f;

		// Token: 0x04001F65 RID: 8037
		public float sphereCastRadius = 0.2f;

		// Token: 0x04001F66 RID: 8038
		private Vector3 origin = Vector3.zero;

		// Token: 0x04001F67 RID: 8039
		private Sensor.CastDirection castDirection;

		// Token: 0x04001F68 RID: 8040
		private bool hasDetectedHit;

		// Token: 0x04001F69 RID: 8041
		private Vector3 hitPosition;

		// Token: 0x04001F6A RID: 8042
		private Vector3 hitNormal;

		// Token: 0x04001F6B RID: 8043
		private float hitDistance;

		// Token: 0x04001F6C RID: 8044
		private List<Collider> hitColliders = new List<Collider>();

		// Token: 0x04001F6D RID: 8045
		private List<Transform> hitTransforms = new List<Transform>();

		// Token: 0x04001F6E RID: 8046
		private Vector3 backupNormal;

		// Token: 0x04001F6F RID: 8047
		private Transform tr;

		// Token: 0x04001F70 RID: 8048
		private Collider col;

		// Token: 0x04001F71 RID: 8049
		public Sensor.CastType castType;

		// Token: 0x04001F72 RID: 8050
		public LayerMask layermask = 255;

		// Token: 0x04001F73 RID: 8051
		private int ignoreRaycastLayer;

		// Token: 0x04001F74 RID: 8052
		public bool calculateRealSurfaceNormal;

		// Token: 0x04001F75 RID: 8053
		public bool calculateRealDistance;

		// Token: 0x04001F76 RID: 8054
		public int arrayRayCount = 9;

		// Token: 0x04001F77 RID: 8055
		public int ArrayRows = 3;

		// Token: 0x04001F78 RID: 8056
		public bool offsetArrayRows;

		// Token: 0x04001F79 RID: 8057
		private Vector3[] raycastArrayStartPositions;

		// Token: 0x04001F7A RID: 8058
		private Collider[] ignoreList;

		// Token: 0x04001F7B RID: 8059
		private int[] ignoreListLayers;

		// Token: 0x04001F7C RID: 8060
		public bool isInDebugMode;

		// Token: 0x04001F7D RID: 8061
		private List<Vector3> arrayNormals = new List<Vector3>();

		// Token: 0x04001F7E RID: 8062
		private List<Vector3> arrayPoints = new List<Vector3>();

		// Token: 0x02000695 RID: 1685
		public enum CastDirection
		{
			// Token: 0x04002C2D RID: 11309
			Forward,
			// Token: 0x04002C2E RID: 11310
			Right,
			// Token: 0x04002C2F RID: 11311
			Up,
			// Token: 0x04002C30 RID: 11312
			Backward,
			// Token: 0x04002C31 RID: 11313
			Left,
			// Token: 0x04002C32 RID: 11314
			Down
		}

		// Token: 0x02000696 RID: 1686
		[SerializeField]
		public enum CastType
		{
			// Token: 0x04002C34 RID: 11316
			Raycast,
			// Token: 0x04002C35 RID: 11317
			RaycastArray,
			// Token: 0x04002C36 RID: 11318
			Spherecast
		}
	}
}
