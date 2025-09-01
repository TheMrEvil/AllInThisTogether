using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000EF RID: 239
	[Serializable]
	public class IKSolverAim : IKSolverHeuristic
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x00043A28 File Offset: 0x00041C28
		public float GetAngle()
		{
			return Vector3.Angle(this.transformAxis, this.IKPosition - this.transform.position);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00043A4B File Offset: 0x00041C4B
		public Vector3 transformAxis
		{
			get
			{
				return this.transform.rotation * this.axis;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00043A63 File Offset: 0x00041C63
		public Vector3 transformPoleAxis
		{
			get
			{
				return this.transform.rotation * this.poleAxis;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00043A7C File Offset: 0x00041C7C
		protected override void OnInitiate()
		{
			if ((this.firstInitiation || !Application.isPlaying) && this.transform != null)
			{
				this.IKPosition = this.transform.position + this.transformAxis * 3f;
				this.polePosition = this.transform.position + this.transformPoleAxis * 3f;
			}
			for (int i = 0; i < this.bones.Length; i++)
			{
				if (this.bones[i].rotationLimit != null)
				{
					this.bones[i].rotationLimit.Disable();
				}
			}
			this.step = 1f / (float)this.bones.Length;
			if (Application.isPlaying)
			{
				this.axis = this.axis.normalized;
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00043B5C File Offset: 0x00041D5C
		protected override void OnUpdate()
		{
			if (this.axis == Vector3.zero)
			{
				if (!Warning.logged)
				{
					base.LogWarning("IKSolverAim axis is Vector3.zero.");
				}
				return;
			}
			if (this.poleAxis == Vector3.zero && this.poleWeight > 0f)
			{
				if (!Warning.logged)
				{
					base.LogWarning("IKSolverAim poleAxis is Vector3.zero.");
				}
				return;
			}
			if (this.target != null)
			{
				this.IKPosition = this.target.position;
			}
			if (this.poleTarget != null)
			{
				this.polePosition = this.poleTarget.position;
			}
			if (this.XY)
			{
				this.IKPosition.z = this.bones[0].transform.position.z;
			}
			if (this.IKPositionWeight <= 0f)
			{
				return;
			}
			this.IKPositionWeight = Mathf.Clamp(this.IKPositionWeight, 0f, 1f);
			if (this.transform != this.lastTransform)
			{
				this.transformLimit = this.transform.GetComponent<RotationLimit>();
				if (this.transformLimit != null)
				{
					this.transformLimit.enabled = false;
				}
				this.lastTransform = this.transform;
			}
			if (this.transformLimit != null)
			{
				this.transformLimit.Apply();
			}
			if (this.transform == null)
			{
				if (!Warning.logged)
				{
					base.LogWarning("Aim Transform unassigned in Aim IK solver. Please Assign a Transform (lineal descendant to the last bone in the spine) that you want to be aimed at IKPosition");
				}
				return;
			}
			this.clampWeight = Mathf.Clamp(this.clampWeight, 0f, 1f);
			this.clampedIKPosition = this.GetClampedIKPosition();
			Vector3 b = this.clampedIKPosition - this.transform.position;
			b = Vector3.Slerp(this.transformAxis * b.magnitude, b, this.IKPositionWeight);
			this.clampedIKPosition = this.transform.position + b;
			int num = 0;
			while (num < this.maxIterations && (num < 1 || this.tolerance <= 0f || this.GetAngle() >= this.tolerance))
			{
				this.lastLocalDirection = this.localDirection;
				if (this.OnPreIteration != null)
				{
					this.OnPreIteration(num);
				}
				this.Solve();
				num++;
			}
			this.lastLocalDirection = this.localDirection;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00043DAC File Offset: 0x00041FAC
		protected override int minBones
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00043DB0 File Offset: 0x00041FB0
		private void Solve()
		{
			for (int i = 0; i < this.bones.Length - 1; i++)
			{
				this.RotateToTarget(this.clampedIKPosition, this.bones[i], this.step * (float)(i + 1) * this.IKPositionWeight * this.bones[i].weight);
			}
			this.RotateToTarget(this.clampedIKPosition, this.bones[this.bones.Length - 1], this.IKPositionWeight * this.bones[this.bones.Length - 1].weight);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00043E40 File Offset: 0x00042040
		private Vector3 GetClampedIKPosition()
		{
			if (this.clampWeight <= 0f)
			{
				return this.IKPosition;
			}
			if (this.clampWeight >= 1f)
			{
				return this.transform.position + this.transformAxis * (this.IKPosition - this.transform.position).magnitude;
			}
			float num = Vector3.Angle(this.transformAxis, this.IKPosition - this.transform.position);
			float num2 = 1f - num / 180f;
			float num3 = (this.clampWeight > 0f) ? Mathf.Clamp(1f - (this.clampWeight - num2) / (1f - num2), 0f, 1f) : 1f;
			float num4 = (this.clampWeight > 0f) ? Mathf.Clamp(num2 / this.clampWeight, 0f, 1f) : 1f;
			for (int i = 0; i < this.clampSmoothing; i++)
			{
				num4 = Mathf.Sin(num4 * 3.1415927f * 0.5f);
			}
			return this.transform.position + Vector3.Slerp(this.transformAxis * 10f, this.IKPosition - this.transform.position, num4 * num3);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00043FA8 File Offset: 0x000421A8
		private void RotateToTarget(Vector3 targetPosition, IKSolver.Bone bone, float weight)
		{
			if (this.XY)
			{
				if (weight >= 0f)
				{
					Vector3 transformAxis = this.transformAxis;
					Vector3 vector = targetPosition - this.transform.position;
					float current = Mathf.Atan2(transformAxis.x, transformAxis.y) * 57.29578f;
					float target = Mathf.Atan2(vector.x, vector.y) * 57.29578f;
					bone.transform.rotation = Quaternion.AngleAxis(Mathf.DeltaAngle(current, target), Vector3.back) * bone.transform.rotation;
				}
			}
			else
			{
				if (weight >= 0f)
				{
					Quaternion quaternion = Quaternion.FromToRotation(this.transformAxis, targetPosition - this.transform.position);
					if (weight >= 1f)
					{
						bone.transform.rotation = quaternion * bone.transform.rotation;
					}
					else
					{
						bone.transform.rotation = Quaternion.Lerp(Quaternion.identity, quaternion, weight) * bone.transform.rotation;
					}
				}
				if (this.poleWeight > 0f)
				{
					Vector3 toDirection = this.polePosition - this.transform.position;
					Vector3 transformAxis2 = this.transformAxis;
					Vector3.OrthoNormalize(ref transformAxis2, ref toDirection);
					Quaternion b = Quaternion.FromToRotation(this.transformPoleAxis, toDirection);
					bone.transform.rotation = Quaternion.Lerp(Quaternion.identity, b, weight * this.poleWeight) * bone.transform.rotation;
				}
			}
			if (this.useRotationLimits && bone.rotationLimit != null)
			{
				bone.rotationLimit.Apply();
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00044150 File Offset: 0x00042350
		protected override Vector3 localDirection
		{
			get
			{
				return this.bones[0].transform.InverseTransformDirection(this.bones[this.bones.Length - 1].transform.forward);
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0004417F File Offset: 0x0004237F
		public IKSolverAim()
		{
		}

		// Token: 0x0400081B RID: 2075
		public Transform transform;

		// Token: 0x0400081C RID: 2076
		public Vector3 axis = Vector3.forward;

		// Token: 0x0400081D RID: 2077
		public Vector3 poleAxis = Vector3.up;

		// Token: 0x0400081E RID: 2078
		public Vector3 polePosition;

		// Token: 0x0400081F RID: 2079
		[Range(0f, 1f)]
		public float poleWeight;

		// Token: 0x04000820 RID: 2080
		public Transform poleTarget;

		// Token: 0x04000821 RID: 2081
		[Range(0f, 1f)]
		public float clampWeight = 0.1f;

		// Token: 0x04000822 RID: 2082
		[Range(0f, 2f)]
		public int clampSmoothing = 2;

		// Token: 0x04000823 RID: 2083
		public IKSolver.IterationDelegate OnPreIteration;

		// Token: 0x04000824 RID: 2084
		private float step;

		// Token: 0x04000825 RID: 2085
		private Vector3 clampedIKPosition;

		// Token: 0x04000826 RID: 2086
		private RotationLimit transformLimit;

		// Token: 0x04000827 RID: 2087
		private Transform lastTransform;
	}
}
