using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public class IKSolverHeuristic : IKSolver
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x000478BC File Offset: 0x00045ABC
		public bool SetChain(Transform[] hierarchy, Transform root)
		{
			if (this.bones == null || this.bones.Length != hierarchy.Length)
			{
				this.bones = new IKSolver.Bone[hierarchy.Length];
			}
			for (int i = 0; i < hierarchy.Length; i++)
			{
				if (this.bones[i] == null)
				{
					this.bones[i] = new IKSolver.Bone();
				}
				this.bones[i].transform = hierarchy[i];
			}
			base.Initiate(root);
			return base.initiated;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00047930 File Offset: 0x00045B30
		public void AddBone(Transform bone)
		{
			Transform[] array = new Transform[this.bones.Length + 1];
			for (int i = 0; i < this.bones.Length; i++)
			{
				array[i] = this.bones[i].transform;
			}
			array[array.Length - 1] = bone;
			this.SetChain(array, this.root);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00047988 File Offset: 0x00045B88
		public override void StoreDefaultLocalState()
		{
			for (int i = 0; i < this.bones.Length; i++)
			{
				this.bones[i].StoreDefaultLocalState();
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000479B8 File Offset: 0x00045BB8
		public override void FixTransforms()
		{
			if (!base.initiated)
			{
				return;
			}
			if (this.IKPositionWeight <= 0f)
			{
				return;
			}
			for (int i = 0; i < this.bones.Length; i++)
			{
				this.bones[i].FixTransform();
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000479FC File Offset: 0x00045BFC
		public override bool IsValid(ref string message)
		{
			if (this.bones.Length == 0)
			{
				message = "IK chain has no Bones.";
				return false;
			}
			if (this.bones.Length < this.minBones)
			{
				message = "IK chain has less than " + this.minBones.ToString() + " Bones.";
				return false;
			}
			IKSolver.Bone[] array = this.bones;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].transform == null)
				{
					message = "One of the Bones is null.";
					return false;
				}
			}
			Transform transform = IKSolver.ContainsDuplicateBone(this.bones);
			if (transform != null)
			{
				message = transform.name + " is represented multiple times in the Bones.";
				return false;
			}
			if (!this.allowCommonParent && !IKSolver.HierarchyIsValid(this.bones))
			{
				message = "Invalid bone hierarchy detected. IK requires for it's bones to be parented to each other in descending order.";
				return false;
			}
			if (!this.boneLengthCanBeZero)
			{
				for (int j = 0; j < this.bones.Length - 1; j++)
				{
					if ((this.bones[j].transform.position - this.bones[j + 1].transform.position).magnitude == 0f)
					{
						message = "Bone " + j.ToString() + " length is zero.";
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00047B34 File Offset: 0x00045D34
		public override IKSolver.Point[] GetPoints()
		{
			return this.bones;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00047B4C File Offset: 0x00045D4C
		public override IKSolver.Point GetPoint(Transform transform)
		{
			for (int i = 0; i < this.bones.Length; i++)
			{
				if (this.bones[i].transform == transform)
				{
					return this.bones[i];
				}
			}
			return null;
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00047B8B File Offset: 0x00045D8B
		protected virtual int minBones
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00047B8E File Offset: 0x00045D8E
		protected virtual bool boneLengthCanBeZero
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00047B91 File Offset: 0x00045D91
		protected virtual bool allowCommonParent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00047B94 File Offset: 0x00045D94
		protected override void OnInitiate()
		{
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00047B96 File Offset: 0x00045D96
		protected override void OnUpdate()
		{
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00047B98 File Offset: 0x00045D98
		protected void InitiateBones()
		{
			this.chainLength = 0f;
			for (int i = 0; i < this.bones.Length; i++)
			{
				if (i < this.bones.Length - 1)
				{
					this.bones[i].length = (this.bones[i].transform.position - this.bones[i + 1].transform.position).magnitude;
					this.chainLength += this.bones[i].length;
					Vector3 position = this.bones[i + 1].transform.position;
					this.bones[i].axis = Quaternion.Inverse(this.bones[i].transform.rotation) * (position - this.bones[i].transform.position);
					if (this.bones[i].rotationLimit != null)
					{
						if (this.XY && !(this.bones[i].rotationLimit is RotationLimitHinge))
						{
							Warning.Log("Only Hinge Rotation Limits should be used on 2D IK solvers.", this.bones[i].transform, false);
						}
						this.bones[i].rotationLimit.Disable();
					}
				}
				else
				{
					this.bones[i].axis = Quaternion.Inverse(this.bones[i].transform.rotation) * (this.bones[this.bones.Length - 1].transform.position - this.bones[0].transform.position);
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00047D44 File Offset: 0x00045F44
		protected virtual Vector3 localDirection
		{
			get
			{
				return this.bones[0].transform.InverseTransformDirection(this.bones[this.bones.Length - 1].transform.position - this.bones[0].transform.position);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00047D95 File Offset: 0x00045F95
		protected float positionOffset
		{
			get
			{
				return Vector3.SqrMagnitude(this.localDirection - this.lastLocalDirection);
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00047DB0 File Offset: 0x00045FB0
		protected Vector3 GetSingularityOffset()
		{
			if (!this.SingularityDetected())
			{
				return Vector3.zero;
			}
			Vector3 normalized = (this.IKPosition - this.bones[0].transform.position).normalized;
			Vector3 rhs = new Vector3(normalized.y, normalized.z, normalized.x);
			if (this.useRotationLimits && this.bones[this.bones.Length - 2].rotationLimit != null && this.bones[this.bones.Length - 2].rotationLimit is RotationLimitHinge)
			{
				rhs = this.bones[this.bones.Length - 2].transform.rotation * this.bones[this.bones.Length - 2].rotationLimit.axis;
			}
			return Vector3.Cross(normalized, rhs) * this.bones[this.bones.Length - 2].length * 0.5f;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00047EB8 File Offset: 0x000460B8
		private bool SingularityDetected()
		{
			if (!base.initiated)
			{
				return false;
			}
			Vector3 a = this.bones[this.bones.Length - 1].transform.position - this.bones[0].transform.position;
			Vector3 a2 = this.IKPosition - this.bones[0].transform.position;
			float magnitude = a.magnitude;
			float magnitude2 = a2.magnitude;
			return magnitude >= magnitude2 && magnitude >= this.chainLength - this.bones[this.bones.Length - 2].length * 0.1f && magnitude != 0f && magnitude2 != 0f && magnitude2 <= magnitude && Vector3.Dot(a / magnitude, a2 / magnitude2) >= 0.999f;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00047F95 File Offset: 0x00046195
		public IKSolverHeuristic()
		{
		}

		// Token: 0x04000861 RID: 2145
		public Transform target;

		// Token: 0x04000862 RID: 2146
		public float tolerance;

		// Token: 0x04000863 RID: 2147
		public int maxIterations = 4;

		// Token: 0x04000864 RID: 2148
		public bool useRotationLimits = true;

		// Token: 0x04000865 RID: 2149
		public bool XY;

		// Token: 0x04000866 RID: 2150
		public IKSolver.Bone[] bones = new IKSolver.Bone[0];

		// Token: 0x04000867 RID: 2151
		protected Vector3 lastLocalDirection;

		// Token: 0x04000868 RID: 2152
		protected float chainLength;
	}
}
