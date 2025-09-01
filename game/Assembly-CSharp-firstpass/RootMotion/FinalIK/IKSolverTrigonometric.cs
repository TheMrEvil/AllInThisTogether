using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	public class IKSolverTrigonometric : IKSolver
	{
		// Token: 0x06000AFA RID: 2810 RVA: 0x00049B60 File Offset: 0x00047D60
		public void SetBendGoalPosition(Vector3 goalPosition, float weight)
		{
			if (!base.initiated)
			{
				return;
			}
			if (weight <= 0f)
			{
				return;
			}
			Vector3 vector = Vector3.Cross(goalPosition - this.bone1.transform.position, this.IKPosition - this.bone1.transform.position);
			if (vector != Vector3.zero)
			{
				if (weight >= 1f)
				{
					this.bendNormal = vector;
					return;
				}
				this.bendNormal = Vector3.Lerp(this.bendNormal, vector, weight);
			}
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00049BE8 File Offset: 0x00047DE8
		public void SetBendPlaneToCurrent()
		{
			if (!base.initiated)
			{
				return;
			}
			Vector3 lhs = Vector3.Cross(this.bone2.transform.position - this.bone1.transform.position, this.bone3.transform.position - this.bone2.transform.position);
			if (lhs != Vector3.zero)
			{
				this.bendNormal = lhs;
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00049C62 File Offset: 0x00047E62
		public void SetIKRotation(Quaternion rotation)
		{
			this.IKRotation = rotation;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00049C6B File Offset: 0x00047E6B
		public void SetIKRotationWeight(float weight)
		{
			this.IKRotationWeight = Mathf.Clamp(weight, 0f, 1f);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00049C83 File Offset: 0x00047E83
		public Quaternion GetIKRotation()
		{
			return this.IKRotation;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00049C8B File Offset: 0x00047E8B
		public float GetIKRotationWeight()
		{
			return this.IKRotationWeight;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00049C93 File Offset: 0x00047E93
		public override IKSolver.Point[] GetPoints()
		{
			return new IKSolver.Point[]
			{
				this.bone1,
				this.bone2,
				this.bone3
			};
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00049CB8 File Offset: 0x00047EB8
		public override IKSolver.Point GetPoint(Transform transform)
		{
			if (this.bone1.transform == transform)
			{
				return this.bone1;
			}
			if (this.bone2.transform == transform)
			{
				return this.bone2;
			}
			if (this.bone3.transform == transform)
			{
				return this.bone3;
			}
			return null;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00049D14 File Offset: 0x00047F14
		public override void StoreDefaultLocalState()
		{
			this.bone1.StoreDefaultLocalState();
			this.bone2.StoreDefaultLocalState();
			this.bone3.StoreDefaultLocalState();
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00049D37 File Offset: 0x00047F37
		public override void FixTransforms()
		{
			if (!base.initiated)
			{
				return;
			}
			this.bone1.FixTransform();
			this.bone2.FixTransform();
			this.bone3.FixTransform();
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00049D64 File Offset: 0x00047F64
		public override bool IsValid(ref string message)
		{
			if (this.bone1.transform == null || this.bone2.transform == null || this.bone3.transform == null)
			{
				message = "Please assign all Bones to the IK solver.";
				return false;
			}
			UnityEngine.Object[] objects = new Transform[]
			{
				this.bone1.transform,
				this.bone2.transform,
				this.bone3.transform
			};
			Transform transform = (Transform)Hierarchy.ContainsDuplicate(objects);
			if (transform != null)
			{
				message = transform.name + " is represented multiple times in the Bones.";
				return false;
			}
			if (this.bone1.transform.position == this.bone2.transform.position)
			{
				message = "first bone position is the same as second bone position.";
				return false;
			}
			if (this.bone2.transform.position == this.bone3.transform.position)
			{
				message = "second bone position is the same as third bone position.";
				return false;
			}
			return true;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00049E6E File Offset: 0x0004806E
		public bool SetChain(Transform bone1, Transform bone2, Transform bone3, Transform root)
		{
			this.bone1.transform = bone1;
			this.bone2.transform = bone2;
			this.bone3.transform = bone3;
			base.Initiate(root);
			return base.initiated;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00049EA4 File Offset: 0x000480A4
		public static void Solve(Transform bone1, Transform bone2, Transform bone3, Vector3 targetPosition, Vector3 bendNormal, float weight)
		{
			if (weight <= 0f)
			{
				return;
			}
			targetPosition = Vector3.Lerp(bone3.position, targetPosition, weight);
			Vector3 vector = targetPosition - bone1.position;
			float magnitude = vector.magnitude;
			if (magnitude == 0f)
			{
				return;
			}
			float sqrMagnitude = (bone2.position - bone1.position).sqrMagnitude;
			float sqrMagnitude2 = (bone3.position - bone2.position).sqrMagnitude;
			Vector3 bendDirection = Vector3.Cross(vector, bendNormal);
			Vector3 directionToBendPoint = IKSolverTrigonometric.GetDirectionToBendPoint(vector, magnitude, bendDirection, sqrMagnitude, sqrMagnitude2);
			Quaternion quaternion = Quaternion.FromToRotation(bone2.position - bone1.position, directionToBendPoint);
			if (weight < 1f)
			{
				quaternion = Quaternion.Lerp(Quaternion.identity, quaternion, weight);
			}
			bone1.rotation = quaternion * bone1.rotation;
			Quaternion quaternion2 = Quaternion.FromToRotation(bone3.position - bone2.position, targetPosition - bone2.position);
			if (weight < 1f)
			{
				quaternion2 = Quaternion.Lerp(Quaternion.identity, quaternion2, weight);
			}
			bone2.rotation = quaternion2 * bone2.rotation;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00049FCC File Offset: 0x000481CC
		private static Vector3 GetDirectionToBendPoint(Vector3 direction, float directionMag, Vector3 bendDirection, float sqrMag1, float sqrMag2)
		{
			float num = (directionMag * directionMag + (sqrMag1 - sqrMag2)) / 2f / directionMag;
			float y = (float)Math.Sqrt((double)Mathf.Clamp(sqrMag1 - num * num, 0f, float.PositiveInfinity));
			if (direction == Vector3.zero)
			{
				return Vector3.zero;
			}
			return Quaternion.LookRotation(direction, bendDirection) * new Vector3(0f, y, num);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0004A034 File Offset: 0x00048234
		protected override void OnInitiate()
		{
			if (this.bendNormal == Vector3.zero)
			{
				this.bendNormal = Vector3.right;
			}
			this.OnInitiateVirtual();
			this.IKPosition = this.bone3.transform.position;
			this.IKRotation = this.bone3.transform.rotation;
			this.InitiateBones();
			this.directHierarchy = this.IsDirectHierarchy();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0004A0A4 File Offset: 0x000482A4
		private bool IsDirectHierarchy()
		{
			return !(this.bone3.transform.parent != this.bone2.transform) && !(this.bone2.transform.parent != this.bone1.transform);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0004A0FC File Offset: 0x000482FC
		private void InitiateBones()
		{
			this.bone1.Initiate(this.bone2.transform.position, this.bendNormal);
			this.bone2.Initiate(this.bone3.transform.position, this.bendNormal);
			this.SetBendPlaneToCurrent();
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0004A154 File Offset: 0x00048354
		protected override void OnUpdate()
		{
			this.IKPositionWeight = Mathf.Clamp(this.IKPositionWeight, 0f, 1f);
			this.IKRotationWeight = Mathf.Clamp(this.IKRotationWeight, 0f, 1f);
			if (this.target != null)
			{
				this.IKPosition = this.target.position;
				this.IKRotation = this.target.rotation;
			}
			this.OnUpdateVirtual();
			if (this.IKPositionWeight > 0f)
			{
				if (!this.directHierarchy)
				{
					this.bone1.Initiate(this.bone2.transform.position, this.bendNormal);
					this.bone2.Initiate(this.bone3.transform.position, this.bendNormal);
				}
				this.bone1.sqrMag = (this.bone2.transform.position - this.bone1.transform.position).sqrMagnitude;
				this.bone2.sqrMag = (this.bone3.transform.position - this.bone2.transform.position).sqrMagnitude;
				if (this.bendNormal == Vector3.zero && !Warning.logged)
				{
					base.LogWarning("IKSolverTrigonometric Bend Normal is Vector3.zero.");
				}
				this.weightIKPosition = Vector3.Lerp(this.bone3.transform.position, this.IKPosition, this.IKPositionWeight);
				Vector3 vector = Vector3.Lerp(this.bone1.GetBendNormalFromCurrentRotation(), this.bendNormal, this.IKPositionWeight);
				Vector3 vector2 = Vector3.Lerp(this.bone2.transform.position - this.bone1.transform.position, this.GetBendDirection(this.weightIKPosition, vector), this.IKPositionWeight);
				if (vector2 == Vector3.zero)
				{
					vector2 = this.bone2.transform.position - this.bone1.transform.position;
				}
				this.bone1.transform.rotation = this.bone1.GetRotation(vector2, vector);
				this.bone2.transform.rotation = this.bone2.GetRotation(this.weightIKPosition - this.bone2.transform.position, this.bone2.GetBendNormalFromCurrentRotation());
			}
			if (this.IKRotationWeight > 0f)
			{
				this.bone3.transform.rotation = Quaternion.Slerp(this.bone3.transform.rotation, this.IKRotation, this.IKRotationWeight);
			}
			this.OnPostSolveVirtual();
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0004A412 File Offset: 0x00048612
		protected virtual void OnInitiateVirtual()
		{
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0004A414 File Offset: 0x00048614
		protected virtual void OnUpdateVirtual()
		{
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0004A416 File Offset: 0x00048616
		protected virtual void OnPostSolveVirtual()
		{
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0004A418 File Offset: 0x00048618
		protected Vector3 GetBendDirection(Vector3 IKPosition, Vector3 bendNormal)
		{
			Vector3 vector = IKPosition - this.bone1.transform.position;
			if (vector == Vector3.zero)
			{
				return Vector3.zero;
			}
			float sqrMagnitude = vector.sqrMagnitude;
			float num = (float)Math.Sqrt((double)sqrMagnitude);
			float num2 = (sqrMagnitude + this.bone1.sqrMag - this.bone2.sqrMag) / 2f / num;
			float y = (float)Math.Sqrt((double)Mathf.Clamp(this.bone1.sqrMag - num2 * num2, 0f, float.PositiveInfinity));
			Vector3 upwards = Vector3.Cross(vector / num, bendNormal);
			return Quaternion.LookRotation(vector, upwards) * new Vector3(0f, y, num2);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0004A4D0 File Offset: 0x000486D0
		public IKSolverTrigonometric()
		{
		}

		// Token: 0x04000895 RID: 2197
		public Transform target;

		// Token: 0x04000896 RID: 2198
		[Range(0f, 1f)]
		public float IKRotationWeight = 1f;

		// Token: 0x04000897 RID: 2199
		public Quaternion IKRotation = Quaternion.identity;

		// Token: 0x04000898 RID: 2200
		public Vector3 bendNormal = Vector3.right;

		// Token: 0x04000899 RID: 2201
		public IKSolverTrigonometric.TrigonometricBone bone1 = new IKSolverTrigonometric.TrigonometricBone();

		// Token: 0x0400089A RID: 2202
		public IKSolverTrigonometric.TrigonometricBone bone2 = new IKSolverTrigonometric.TrigonometricBone();

		// Token: 0x0400089B RID: 2203
		public IKSolverTrigonometric.TrigonometricBone bone3 = new IKSolverTrigonometric.TrigonometricBone();

		// Token: 0x0400089C RID: 2204
		protected Vector3 weightIKPosition;

		// Token: 0x0400089D RID: 2205
		protected bool directHierarchy = true;

		// Token: 0x020001FC RID: 508
		[Serializable]
		public class TrigonometricBone : IKSolver.Bone
		{
			// Token: 0x0600108C RID: 4236 RVA: 0x000668FC File Offset: 0x00064AFC
			public void Initiate(Vector3 childPosition, Vector3 bendNormal)
			{
				Quaternion rotation = Quaternion.LookRotation(childPosition - this.transform.position, bendNormal);
				this.targetToLocalSpace = QuaTools.RotationToLocalSpace(this.transform.rotation, rotation);
				this.defaultLocalBendNormal = Quaternion.Inverse(this.transform.rotation) * bendNormal;
			}

			// Token: 0x0600108D RID: 4237 RVA: 0x00066954 File Offset: 0x00064B54
			public Quaternion GetRotation(Vector3 direction, Vector3 bendNormal)
			{
				return Quaternion.LookRotation(direction, bendNormal) * this.targetToLocalSpace;
			}

			// Token: 0x0600108E RID: 4238 RVA: 0x00066968 File Offset: 0x00064B68
			public Vector3 GetBendNormalFromCurrentRotation()
			{
				return this.transform.rotation * this.defaultLocalBendNormal;
			}

			// Token: 0x0600108F RID: 4239 RVA: 0x00066980 File Offset: 0x00064B80
			public TrigonometricBone()
			{
			}

			// Token: 0x04000EF1 RID: 3825
			private Quaternion targetToLocalSpace;

			// Token: 0x04000EF2 RID: 3826
			private Vector3 defaultLocalBendNormal;
		}
	}
}
