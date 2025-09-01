using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(LineRenderer))]
	public class MagicFX5_RopePhysics : MonoBehaviour
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00004AB8 File Offset: 0x00002CB8
		private void OnEnable()
		{
			this._isInitialized = false;
			this._animLeftTime = 0f;
			this._forceLeftTime = 0f;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004AD7 File Offset: 0x00002CD7
		public void ForceInitialize()
		{
			if (!this._isInitialized)
			{
				this.Initialize();
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004AE8 File Offset: 0x00002CE8
		private void Initialize()
		{
			this.TargetRigidbody = this.Target.GetComponent<Rigidbody>();
			this.LineRenderer = base.GetComponent<LineRenderer>();
			this.LineRenderer.positionCount = 0;
			this._segmentPositions = new Vector3[this.SegmentCount];
			this._previousPositions = new Vector3[this.SegmentCount];
			this._lineRendererPositions = new Vector3[this.SegmentCount];
			Vector3 position = base.transform.position;
			Vector3 normalized = (this.Target.position - base.transform.position).normalized;
			this._segmentLength = ((this.Target.position - base.transform.position).magnitude * this.TargetForceDistanceMultiplier + this.TargetDistanceOffset) / (float)this.SegmentCount;
			for (int i = 0; i < this.SegmentCount; i++)
			{
				this._segmentPositions[i] = position + normalized * this._segmentLength * (float)i;
				this._previousPositions[i] = this._segmentPositions[i];
			}
			this.DrawRope();
			this._isInitialized = true;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004C1C File Offset: 0x00002E1C
		private void Update()
		{
			if (this.Target == null)
			{
				return;
			}
			if (!this._isInitialized)
			{
				this.Initialize();
			}
			float deltaTime = Time.deltaTime;
			this._forceLeftTime += deltaTime;
			this.Simulate(deltaTime);
			this.DrawRope();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004C68 File Offset: 0x00002E68
		private void FixedUpdate()
		{
			if (this.Target == null || !this.UseTargetForce || this.TargetRigidbody == null)
			{
				return;
			}
			float tensionForce = this.GetTensionForce();
			if (tensionForce > 0f)
			{
				this.TargetRigidbody.velocity = (this.GetForceEndPoint() - this.Target.position) * (1f / Time.fixedDeltaTime) * Mathf.Clamp01(tensionForce);
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004CE8 File Offset: 0x00002EE8
		private void Simulate(float deltaTime)
		{
			Vector3 vector = new Vector3(0f, this.Gravity, 0f) * deltaTime;
			if (this.UseTargetWindMode)
			{
				Vector3 normalized = (this.Target.position - base.transform.position).normalized;
				vector += normalized * this.WindToTargetStrength * deltaTime;
			}
			for (int i = 1; i < this.SegmentCount; i++)
			{
				Vector3 vector2 = this._segmentPositions[i];
				Vector3 b = (this._segmentPositions[i] - this._previousPositions[i]) * this.Damping;
				Vector3 vector3 = this._segmentPositions[i] + b + vector;
				if (this.UseTurbulence)
				{
					vector3 += this.GenerateTurbulentNoise(vector2, Time.time * this.TurbulenceTimeScale) * deltaTime;
				}
				this._previousPositions[i] = vector2;
				this._segmentPositions[i] = vector3;
			}
			this.UpdatePositions();
			for (int j = 0; j < this.ConstraintIterations; j++)
			{
				this.ApplyConstraints();
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004E28 File Offset: 0x00003028
		public Vector3 GenerateTurbulentNoise(Vector3 position, float time)
		{
			Vector3 zero = Vector3.zero;
			float num = position.x * this.TurbulenceFrequency + time;
			float num2 = position.y * this.TurbulenceFrequency + time;
			float num3 = position.z * this.TurbulenceFrequency + time;
			float x = Mathf.PerlinNoise(num, num2) * 2f - 1f;
			float y = Mathf.PerlinNoise(num2, num3) * 2f - 1f;
			float z = Mathf.PerlinNoise(num3, num) * 2f - 1f;
			return zero + new Vector3(x, y, z) * this.TurbulenceAmplitude;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004EC4 File Offset: 0x000030C4
		private void UpdatePositions()
		{
			if (this.UseTargetWindMode)
			{
				this._segmentPositions[0] = base.transform.position;
				return;
			}
			Vector3 b = (this.Target.position - base.transform.position).normalized * this._segmentLength * (float)this.SegmentCount;
			this._segmentPositions[0] = base.transform.position;
			this._segmentPositions[this.SegmentCount - 1] = base.transform.position + b;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004F68 File Offset: 0x00003168
		private void ApplyConstraints()
		{
			int i = 0;
			while (i < this.SegmentCount - 1)
			{
				Vector3 vector = this._segmentPositions[i];
				Vector3 vector2 = this._segmentPositions[i + 1];
				float magnitude = (vector - vector2).magnitude;
				float d = Mathf.Abs(magnitude - this._segmentLength);
				Vector3 a = Vector3.zero;
				if (magnitude > this._segmentLength)
				{
					a = (vector - vector2).normalized;
					goto IL_82;
				}
				if (magnitude < this._segmentLength)
				{
					a = (vector2 - vector).normalized;
					goto IL_82;
				}
				IL_EC:
				i++;
				continue;
				IL_82:
				Vector3 vector3 = a * d;
				if (i != 0)
				{
					vector2 += vector3 * 0.5f;
					this._segmentPositions[i + 1] = vector2;
					vector -= vector3 * 0.5f;
					this._segmentPositions[i] = vector;
					goto IL_EC;
				}
				vector2 += vector3;
				this._segmentPositions[i + 1] = vector2;
				goto IL_EC;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005074 File Offset: 0x00003274
		private void DrawRope()
		{
			if (this._animLeftTime < this.MoveToTargetAnimationTime)
			{
				this._animLerpVal = Mathf.Clamp01(this._animLeftTime / (this.MoveToTargetAnimationTime + 1E-05f));
				int num = Mathf.CeilToInt(this._animLerpVal * (float)this.SegmentCount);
				float t = this._animLerpVal * (float)this.SegmentCount % 1f;
				this.LineRenderer.positionCount = num;
				for (int i = 0; i < num; i++)
				{
					this._lineRendererPositions[i] = this._segmentPositions[num - 1 - i];
				}
				if (!this.UseTargetWindMode && num == this.SegmentCount)
				{
					this._lineRendererPositions[0] = this.Target.position;
				}
				Vector3 b = this._lineRendererPositions[0];
				Vector3 a = this._lineRendererPositions[1];
				this._lineRendererPositions[0] = Vector3.Lerp(a, b, t);
				this.LineRenderer.SetPositions(this._lineRendererPositions);
				this._animLeftTime += Time.deltaTime;
				return;
			}
			for (int j = 0; j < this.SegmentCount; j++)
			{
				this._lineRendererPositions[j] = this._segmentPositions[this.SegmentCount - 1 - j];
			}
			if (!this.UseTargetWindMode)
			{
				this._lineRendererPositions[0] = this.Target.position;
			}
			this.LineRenderer.SetPositions(this._lineRendererPositions);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000051F5 File Offset: 0x000033F5
		public Vector3 GetForceEndPoint()
		{
			return this._segmentPositions[this.SegmentCount - 1];
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000520C File Offset: 0x0000340C
		public float GetTensionForce()
		{
			if (this._forceLeftTime < this.ForceStartDelay || (this.ForceLifeTime > 0f && this._forceLeftTime > this.ForceLifeTime + this.ForceStartDelay))
			{
				return 0f;
			}
			float num = (this.Target.position - base.transform.position).magnitude / (float)this.SegmentCount;
			if (num > this._segmentLength)
			{
				return (num - this._segmentLength) * (float)this.SegmentCount * this.Force * this._animLerpVal;
			}
			return 0f;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000052A8 File Offset: 0x000034A8
		public MagicFX5_RopePhysics()
		{
		}

		// Token: 0x040000BB RID: 187
		public Transform Target;

		// Token: 0x040000BC RID: 188
		public int SegmentCount = 10;

		// Token: 0x040000BD RID: 189
		public float Gravity = -1f;

		// Token: 0x040000BE RID: 190
		public float Damping = 0.95f;

		// Token: 0x040000BF RID: 191
		public int ConstraintIterations = 1;

		// Token: 0x040000C0 RID: 192
		public float MoveToTargetAnimationTime = 0.5f;

		// Token: 0x040000C1 RID: 193
		[Space]
		public bool UseTargetForce = true;

		// Token: 0x040000C2 RID: 194
		public float Force = 1f;

		// Token: 0x040000C3 RID: 195
		public float ForceStartDelay;

		// Token: 0x040000C4 RID: 196
		public float ForceLifeTime = -1f;

		// Token: 0x040000C5 RID: 197
		public float TargetDistanceOffset = 0.25f;

		// Token: 0x040000C6 RID: 198
		public float TargetForceDistanceMultiplier = 1f;

		// Token: 0x040000C7 RID: 199
		[Space]
		public bool UseTargetWindMode;

		// Token: 0x040000C8 RID: 200
		public float WindToTargetStrength = 1f;

		// Token: 0x040000C9 RID: 201
		[Space]
		public bool UseTurbulence;

		// Token: 0x040000CA RID: 202
		public float TurbulenceFrequency = 1.5f;

		// Token: 0x040000CB RID: 203
		public float TurbulenceAmplitude = 0.1f;

		// Token: 0x040000CC RID: 204
		public float TurbulenceTimeScale = 1f;

		// Token: 0x040000CD RID: 205
		private float _segmentLength;

		// Token: 0x040000CE RID: 206
		internal LineRenderer LineRenderer;

		// Token: 0x040000CF RID: 207
		private Vector3[] _segmentPositions;

		// Token: 0x040000D0 RID: 208
		private Vector3[] _lineRendererPositions;

		// Token: 0x040000D1 RID: 209
		private Vector3[] _previousPositions;

		// Token: 0x040000D2 RID: 210
		internal Rigidbody TargetRigidbody;

		// Token: 0x040000D3 RID: 211
		private float _animLeftTime;

		// Token: 0x040000D4 RID: 212
		private float _animLerpVal;

		// Token: 0x040000D5 RID: 213
		private float _forceLeftTime;

		// Token: 0x040000D6 RID: 214
		private bool _isInitialized;
	}
}
