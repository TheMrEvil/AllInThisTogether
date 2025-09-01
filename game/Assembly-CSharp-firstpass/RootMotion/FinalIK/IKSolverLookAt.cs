using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public class IKSolverLookAt : IKSolver
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x00048D4D File Offset: 0x00046F4D
		public void SetLookAtWeight(float weight)
		{
			this.IKPositionWeight = Mathf.Clamp(weight, 0f, 1f);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00048D65 File Offset: 0x00046F65
		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			this.IKPositionWeight = Mathf.Clamp(weight, 0f, 1f);
			this.bodyWeight = Mathf.Clamp(bodyWeight, 0f, 1f);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00048D94 File Offset: 0x00046F94
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			this.IKPositionWeight = Mathf.Clamp(weight, 0f, 1f);
			this.bodyWeight = Mathf.Clamp(bodyWeight, 0f, 1f);
			this.headWeight = Mathf.Clamp(headWeight, 0f, 1f);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00048DE4 File Offset: 0x00046FE4
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			this.IKPositionWeight = Mathf.Clamp(weight, 0f, 1f);
			this.bodyWeight = Mathf.Clamp(bodyWeight, 0f, 1f);
			this.headWeight = Mathf.Clamp(headWeight, 0f, 1f);
			this.eyesWeight = Mathf.Clamp(eyesWeight, 0f, 1f);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00048E4C File Offset: 0x0004704C
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight)
		{
			this.IKPositionWeight = Mathf.Clamp(weight, 0f, 1f);
			this.bodyWeight = Mathf.Clamp(bodyWeight, 0f, 1f);
			this.headWeight = Mathf.Clamp(headWeight, 0f, 1f);
			this.eyesWeight = Mathf.Clamp(eyesWeight, 0f, 1f);
			this.clampWeight = Mathf.Clamp(clampWeight, 0f, 1f);
			this.clampWeightHead = this.clampWeight;
			this.clampWeightEyes = this.clampWeight;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00048EE4 File Offset: 0x000470E4
		public void SetLookAtWeight(float weight, float bodyWeight = 0f, float headWeight = 1f, float eyesWeight = 0.5f, float clampWeight = 0.5f, float clampWeightHead = 0.5f, float clampWeightEyes = 0.3f)
		{
			this.IKPositionWeight = Mathf.Clamp(weight, 0f, 1f);
			this.bodyWeight = Mathf.Clamp(bodyWeight, 0f, 1f);
			this.headWeight = Mathf.Clamp(headWeight, 0f, 1f);
			this.eyesWeight = Mathf.Clamp(eyesWeight, 0f, 1f);
			this.clampWeight = Mathf.Clamp(clampWeight, 0f, 1f);
			this.clampWeightHead = Mathf.Clamp(clampWeightHead, 0f, 1f);
			this.clampWeightEyes = Mathf.Clamp(clampWeightEyes, 0f, 1f);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00048F90 File Offset: 0x00047190
		public override void StoreDefaultLocalState()
		{
			for (int i = 0; i < this.spine.Length; i++)
			{
				this.spine[i].StoreDefaultLocalState();
			}
			for (int j = 0; j < this.eyes.Length; j++)
			{
				this.eyes[j].StoreDefaultLocalState();
			}
			if (this.head != null && this.head.transform != null)
			{
				this.head.StoreDefaultLocalState();
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00049003 File Offset: 0x00047203
		public void SetDirty()
		{
			this.isDirty = true;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0004900C File Offset: 0x0004720C
		public override void FixTransforms()
		{
			if (!base.initiated)
			{
				return;
			}
			if (this.IKPositionWeight <= 0f && !this.isDirty)
			{
				return;
			}
			for (int i = 0; i < this.spine.Length; i++)
			{
				this.spine[i].FixTransform();
			}
			for (int j = 0; j < this.eyes.Length; j++)
			{
				this.eyes[j].FixTransform();
			}
			if (this.head != null && this.head.transform != null)
			{
				this.head.FixTransform();
			}
			this.isDirty = false;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000490A8 File Offset: 0x000472A8
		public override bool IsValid(ref string message)
		{
			if (!this.spineIsValid)
			{
				message = "IKSolverLookAt spine setup is invalid. Can't initiate solver.";
				return false;
			}
			if (!this.headIsValid)
			{
				message = "IKSolverLookAt head transform is null. Can't initiate solver.";
				return false;
			}
			if (!this.eyesIsValid)
			{
				message = "IKSolverLookAt eyes setup is invalid. Can't initiate solver.";
				return false;
			}
			if (this.spineIsEmpty && this.headIsEmpty && this.eyesIsEmpty)
			{
				message = "IKSolverLookAt eyes setup is invalid. Can't initiate solver.";
				return false;
			}
			IKSolver.Bone[] bones = this.spine;
			Transform transform = IKSolver.ContainsDuplicateBone(bones);
			if (transform != null)
			{
				message = transform.name + " is represented multiple times in a single IK chain. Can't initiate solver.";
				return false;
			}
			bones = this.eyes;
			Transform transform2 = IKSolver.ContainsDuplicateBone(bones);
			if (transform2 != null)
			{
				message = transform2.name + " is represented multiple times in a single IK chain. Can't initiate solver.";
				return false;
			}
			return true;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00049160 File Offset: 0x00047360
		public override IKSolver.Point[] GetPoints()
		{
			IKSolver.Point[] array = new IKSolver.Point[this.spine.Length + this.eyes.Length + ((this.head.transform != null) ? 1 : 0)];
			for (int i = 0; i < this.spine.Length; i++)
			{
				array[i] = this.spine[i];
			}
			int num = 0;
			for (int j = this.spine.Length; j < this.spine.Length + this.eyes.Length; j++)
			{
				array[j] = this.eyes[num];
				num++;
			}
			if (this.head.transform != null)
			{
				array[array.Length - 1] = this.head;
			}
			return array;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00049210 File Offset: 0x00047410
		public override IKSolver.Point GetPoint(Transform transform)
		{
			foreach (IKSolverLookAt.LookAtBone lookAtBone in this.spine)
			{
				if (lookAtBone.transform == transform)
				{
					return lookAtBone;
				}
			}
			foreach (IKSolverLookAt.LookAtBone lookAtBone2 in this.eyes)
			{
				if (lookAtBone2.transform == transform)
				{
					return lookAtBone2;
				}
			}
			if (this.head.transform == transform)
			{
				return this.head;
			}
			return null;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0004928A File Offset: 0x0004748A
		public bool SetChain(Transform[] spine, Transform head, Transform[] eyes, Transform root)
		{
			this.SetBones(spine, ref this.spine);
			this.head = new IKSolverLookAt.LookAtBone(head);
			this.SetBones(eyes, ref this.eyes);
			base.Initiate(root);
			return base.initiated;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000492C0 File Offset: 0x000474C0
		protected override void OnInitiate()
		{
			if (this.firstInitiation || !Application.isPlaying)
			{
				if (this.spine.Length != 0)
				{
					this.IKPosition = this.spine[this.spine.Length - 1].transform.position + this.root.forward * 3f;
				}
				else if (this.head.transform != null)
				{
					this.IKPosition = this.head.transform.position + this.root.forward * 3f;
				}
				else if (this.eyes.Length != 0 && this.eyes[0].transform != null)
				{
					this.IKPosition = this.eyes[0].transform.position + this.root.forward * 3f;
				}
			}
			IKSolverLookAt.LookAtBone[] array = this.spine;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Initiate(this.root);
			}
			if (this.head != null)
			{
				this.head.Initiate(this.root);
			}
			array = this.eyes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Initiate(this.root);
			}
			if (this.spineForwards == null || this.spineForwards.Length != this.spine.Length)
			{
				this.spineForwards = new Vector3[this.spine.Length];
			}
			if (this.headForwards == null)
			{
				this.headForwards = new Vector3[1];
			}
			if (this.eyeForward == null)
			{
				this.eyeForward = new Vector3[1];
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00049474 File Offset: 0x00047674
		protected override void OnUpdate()
		{
			if (this.IKPositionWeight <= 0f)
			{
				return;
			}
			this.IKPositionWeight = Mathf.Clamp(this.IKPositionWeight, 0f, 1f);
			if (this.target != null)
			{
				this.IKPosition = this.target.position;
			}
			this.SolveSpine();
			this.SolveHead();
			this.SolveEyes();
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000494DC File Offset: 0x000476DC
		protected bool spineIsValid
		{
			get
			{
				if (this.spine == null)
				{
					return false;
				}
				if (this.spine.Length == 0)
				{
					return true;
				}
				for (int i = 0; i < this.spine.Length; i++)
				{
					if (this.spine[i] == null || this.spine[i].transform == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x00049533 File Offset: 0x00047733
		protected bool spineIsEmpty
		{
			get
			{
				return this.spine.Length == 0;
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00049540 File Offset: 0x00047740
		protected void SolveSpine()
		{
			if (this.bodyWeight <= 0f)
			{
				return;
			}
			if (this.spineIsEmpty)
			{
				return;
			}
			Vector3 normalized = (this.IKPosition + this.spineTargetOffset - this.spine[this.spine.Length - 1].transform.position).normalized;
			this.GetForwards(ref this.spineForwards, this.spine[0].forward, normalized, this.spine.Length, this.clampWeight);
			for (int i = 0; i < this.spine.Length; i++)
			{
				this.spine[i].LookAt(this.spineForwards[i], this.bodyWeight * this.IKPositionWeight);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x00049600 File Offset: 0x00047800
		protected bool headIsValid
		{
			get
			{
				return this.head != null;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0004960D File Offset: 0x0004780D
		protected bool headIsEmpty
		{
			get
			{
				return this.head.transform == null;
			}
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00049620 File Offset: 0x00047820
		protected void SolveHead()
		{
			if (this.headWeight <= 0f)
			{
				return;
			}
			if (this.headIsEmpty)
			{
				return;
			}
			Vector3 vector = (this.spine.Length != 0 && this.spine[this.spine.Length - 1].transform != null) ? this.spine[this.spine.Length - 1].forward : this.head.forward;
			Vector3 normalized = Vector3.Lerp(vector, (this.IKPosition - this.head.transform.position).normalized, this.headWeight * this.IKPositionWeight).normalized;
			this.GetForwards(ref this.headForwards, vector, normalized, 1, this.clampWeightHead);
			this.head.LookAt(this.headForwards[0], this.headWeight * this.IKPositionWeight);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00049708 File Offset: 0x00047908
		protected bool eyesIsValid
		{
			get
			{
				if (this.eyes == null)
				{
					return false;
				}
				if (this.eyes.Length == 0)
				{
					return true;
				}
				for (int i = 0; i < this.eyes.Length; i++)
				{
					if (this.eyes[i] == null || this.eyes[i].transform == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0004975F File Offset: 0x0004795F
		protected bool eyesIsEmpty
		{
			get
			{
				return this.eyes.Length == 0;
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0004976C File Offset: 0x0004796C
		protected void SolveEyes()
		{
			if (this.eyesWeight <= 0f)
			{
				return;
			}
			if (this.eyesIsEmpty)
			{
				return;
			}
			for (int i = 0; i < this.eyes.Length; i++)
			{
				Quaternion quaternion = (this.head.transform != null) ? this.head.transform.rotation : ((this.spine.Length != 0) ? this.spine[this.spine.Length - 1].transform.rotation : this.root.rotation);
				Vector3 point = (this.head.transform != null) ? this.head.axis : ((this.spine.Length != 0) ? this.spine[this.spine.Length - 1].axis : this.root.forward);
				if (this.eyes[i].baseForwardOffsetEuler != Vector3.zero)
				{
					quaternion *= Quaternion.Euler(this.eyes[i].baseForwardOffsetEuler);
				}
				Vector3 baseForward = quaternion * point;
				this.GetForwards(ref this.eyeForward, baseForward, (this.IKPosition - this.eyes[i].transform.position).normalized, 1, this.clampWeightEyes);
				this.eyes[i].LookAt(this.eyeForward[0], this.eyesWeight * this.IKPositionWeight);
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000498E8 File Offset: 0x00047AE8
		protected Vector3[] GetForwards(ref Vector3[] forwards, Vector3 baseForward, Vector3 targetForward, int bones, float clamp)
		{
			if (clamp >= 1f || this.IKPositionWeight <= 0f)
			{
				for (int i = 0; i < forwards.Length; i++)
				{
					forwards[i] = baseForward;
				}
				return forwards;
			}
			float num = Vector3.Angle(baseForward, targetForward);
			float num2 = 1f - num / 180f;
			float num3 = (clamp > 0f) ? Mathf.Clamp(1f - (clamp - num2) / (1f - num2), 0f, 1f) : 1f;
			float num4 = (clamp > 0f) ? Mathf.Clamp(num2 / clamp, 0f, 1f) : 1f;
			for (int j = 0; j < this.clampSmoothing; j++)
			{
				num4 = Mathf.Sin(num4 * 3.1415927f * 0.5f);
			}
			if (forwards.Length == 1)
			{
				forwards[0] = Vector3.Slerp(baseForward, targetForward, num4 * num3);
			}
			else
			{
				float num5 = 1f / (float)(forwards.Length - 1);
				for (int k = 0; k < forwards.Length; k++)
				{
					forwards[k] = Vector3.Slerp(baseForward, targetForward, this.spineWeightCurve.Evaluate(num5 * (float)k) * num4 * num3);
				}
			}
			return forwards;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00049A24 File Offset: 0x00047C24
		protected void SetBones(Transform[] array, ref IKSolverLookAt.LookAtBone[] bones)
		{
			if (array == null)
			{
				bones = new IKSolverLookAt.LookAtBone[0];
				return;
			}
			if (bones.Length != array.Length)
			{
				bones = new IKSolverLookAt.LookAtBone[array.Length];
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (bones[i] == null)
				{
					bones[i] = new IKSolverLookAt.LookAtBone(array[i]);
				}
				else
				{
					bones[i].transform = array[i];
				}
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00049A80 File Offset: 0x00047C80
		public IKSolverLookAt()
		{
		}

		// Token: 0x04000884 RID: 2180
		public Transform target;

		// Token: 0x04000885 RID: 2181
		public IKSolverLookAt.LookAtBone[] spine = new IKSolverLookAt.LookAtBone[0];

		// Token: 0x04000886 RID: 2182
		public IKSolverLookAt.LookAtBone head = new IKSolverLookAt.LookAtBone();

		// Token: 0x04000887 RID: 2183
		public IKSolverLookAt.LookAtBone[] eyes = new IKSolverLookAt.LookAtBone[0];

		// Token: 0x04000888 RID: 2184
		[Range(0f, 1f)]
		public float bodyWeight = 0.5f;

		// Token: 0x04000889 RID: 2185
		[Range(0f, 1f)]
		public float headWeight = 0.5f;

		// Token: 0x0400088A RID: 2186
		[Range(0f, 1f)]
		public float eyesWeight = 1f;

		// Token: 0x0400088B RID: 2187
		[Range(0f, 1f)]
		public float clampWeight = 0.5f;

		// Token: 0x0400088C RID: 2188
		[Range(0f, 1f)]
		public float clampWeightHead = 0.5f;

		// Token: 0x0400088D RID: 2189
		[Range(0f, 1f)]
		public float clampWeightEyes = 0.5f;

		// Token: 0x0400088E RID: 2190
		[Range(0f, 2f)]
		public int clampSmoothing = 2;

		// Token: 0x0400088F RID: 2191
		public AnimationCurve spineWeightCurve = new AnimationCurve(new Keyframe[]
		{
			new Keyframe(0f, 0.3f),
			new Keyframe(1f, 1f)
		});

		// Token: 0x04000890 RID: 2192
		public Vector3 spineTargetOffset;

		// Token: 0x04000891 RID: 2193
		protected Vector3[] spineForwards = new Vector3[0];

		// Token: 0x04000892 RID: 2194
		protected Vector3[] headForwards = new Vector3[1];

		// Token: 0x04000893 RID: 2195
		protected Vector3[] eyeForward = new Vector3[1];

		// Token: 0x04000894 RID: 2196
		private bool isDirty;

		// Token: 0x020001FB RID: 507
		[Serializable]
		public class LookAtBone : IKSolver.Bone
		{
			// Token: 0x06001087 RID: 4231 RVA: 0x0006685A File Offset: 0x00064A5A
			public LookAtBone()
			{
			}

			// Token: 0x06001088 RID: 4232 RVA: 0x00066862 File Offset: 0x00064A62
			public LookAtBone(Transform transform)
			{
				this.transform = transform;
			}

			// Token: 0x06001089 RID: 4233 RVA: 0x00066871 File Offset: 0x00064A71
			public void Initiate(Transform root)
			{
				if (this.transform == null)
				{
					return;
				}
				this.axis = Quaternion.Inverse(this.transform.rotation) * root.forward;
			}

			// Token: 0x0600108A RID: 4234 RVA: 0x000668A4 File Offset: 0x00064AA4
			public void LookAt(Vector3 direction, float weight)
			{
				Quaternion lhs = Quaternion.FromToRotation(this.forward, direction);
				Quaternion rotation = this.transform.rotation;
				this.transform.rotation = Quaternion.Lerp(rotation, lhs * rotation, weight);
			}

			// Token: 0x17000222 RID: 546
			// (get) Token: 0x0600108B RID: 4235 RVA: 0x000668E3 File Offset: 0x00064AE3
			public Vector3 forward
			{
				get
				{
					return this.transform.rotation * this.axis;
				}
			}

			// Token: 0x04000EF0 RID: 3824
			public Vector3 baseForwardOffsetEuler;
		}
	}
}
