using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E6 RID: 230
	public class FBBIKHeadEffector : MonoBehaviour
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x0003F424 File Offset: 0x0003D624
		private void Start()
		{
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreRead, new IKSolver.UpdateDelegate(this.OnPreRead));
			IKSolverFullBodyBiped solver2 = this.ik.solver;
			solver2.OnPreIteration = (IKSolver.IterationDelegate)Delegate.Combine(solver2.OnPreIteration, new IKSolver.IterationDelegate(this.Iterate));
			IKSolverFullBodyBiped solver3 = this.ik.solver;
			solver3.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver3.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostUpdate));
			IKSolverFullBodyBiped solver4 = this.ik.solver;
			solver4.OnStoreDefaultLocalState = (IKSolver.UpdateDelegate)Delegate.Combine(solver4.OnStoreDefaultLocalState, new IKSolver.UpdateDelegate(this.OnStoreDefaultLocalState));
			IKSolverFullBodyBiped solver5 = this.ik.solver;
			solver5.OnFixTransforms = (IKSolver.UpdateDelegate)Delegate.Combine(solver5.OnFixTransforms, new IKSolver.UpdateDelegate(this.OnFixTransforms));
			this.OnStoreDefaultLocalState();
			this.headRotationRelativeToRoot = Quaternion.Inverse(this.ik.references.root.rotation) * this.ik.references.head.rotation;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0003F550 File Offset: 0x0003D750
		private void OnStoreDefaultLocalState()
		{
			foreach (FBBIKHeadEffector.BendBone bendBone in this.bendBones)
			{
				if (bendBone != null)
				{
					bendBone.StoreDefaultLocalState();
				}
			}
			this.ccdDefaultLocalRotations = new Quaternion[this.CCDBones.Length];
			for (int j = 0; j < this.CCDBones.Length; j++)
			{
				if (this.CCDBones[j] != null)
				{
					this.ccdDefaultLocalRotations[j] = this.CCDBones[j].localRotation;
				}
			}
			this.headLocalPosition = this.ik.references.head.localPosition;
			this.headLocalRotation = this.ik.references.head.localRotation;
			this.stretchLocalPositions = new Vector3[this.stretchBones.Length];
			this.stretchLocalRotations = new Quaternion[this.stretchBones.Length];
			for (int k = 0; k < this.stretchBones.Length; k++)
			{
				if (this.stretchBones[k] != null)
				{
					this.stretchLocalPositions[k] = this.stretchBones[k].localPosition;
					this.stretchLocalRotations[k] = this.stretchBones[k].localRotation;
				}
			}
			this.chestLocalPositions = new Vector3[this.chestBones.Length];
			this.chestLocalRotations = new Quaternion[this.chestBones.Length];
			for (int l = 0; l < this.chestBones.Length; l++)
			{
				if (this.chestBones[l] != null)
				{
					this.chestLocalPositions[l] = this.chestBones[l].localPosition;
					this.chestLocalRotations[l] = this.chestBones[l].localRotation;
				}
			}
			this.bendBonesCount = this.bendBones.Length;
			this.ccdBonesCount = this.CCDBones.Length;
			this.stretchBonesCount = this.stretchBones.Length;
			this.chestBonesCount = this.chestBones.Length;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0003F744 File Offset: 0x0003D944
		private void OnFixTransforms()
		{
			if (!base.enabled)
			{
				return;
			}
			foreach (FBBIKHeadEffector.BendBone bendBone in this.bendBones)
			{
				if (bendBone != null)
				{
					bendBone.FixTransforms();
				}
			}
			for (int j = 0; j < this.CCDBones.Length; j++)
			{
				if (this.CCDBones[j] != null)
				{
					this.CCDBones[j].localRotation = this.ccdDefaultLocalRotations[j];
				}
			}
			this.ik.references.head.localPosition = this.headLocalPosition;
			this.ik.references.head.localRotation = this.headLocalRotation;
			for (int k = 0; k < this.stretchBones.Length; k++)
			{
				if (this.stretchBones[k] != null)
				{
					this.stretchBones[k].localPosition = this.stretchLocalPositions[k];
					this.stretchBones[k].localRotation = this.stretchLocalRotations[k];
				}
			}
			for (int l = 0; l < this.chestBones.Length; l++)
			{
				if (this.chestBones[l] != null)
				{
					this.chestBones[l].localPosition = this.chestLocalPositions[l];
					this.chestBones[l].localRotation = this.chestLocalRotations[l];
				}
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0003F8AC File Offset: 0x0003DAAC
		private void OnPreRead()
		{
			if (!base.enabled)
			{
				return;
			}
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.ik.solver.iterations == 0)
			{
				return;
			}
			this.ik.solver.FABRIKPass = this.handsPullBody;
			if (this.bendBonesCount != this.bendBones.Length || this.ccdBonesCount != this.CCDBones.Length || this.stretchBonesCount != this.stretchBones.Length || this.chestBonesCount != this.chestBones.Length)
			{
				this.OnStoreDefaultLocalState();
			}
			this.ChestDirection();
			this.SpineBend();
			this.CCDPass();
			this.offset = base.transform.position - this.ik.references.head.position;
			this.shoulderDist = Vector3.Distance(this.ik.references.leftUpperArm.position, this.ik.references.rightUpperArm.position);
			this.leftShoulderDist = Vector3.Distance(this.ik.references.head.position, this.ik.references.leftUpperArm.position);
			this.rightShoulderDist = Vector3.Distance(this.ik.references.head.position, this.ik.references.rightUpperArm.position);
			this.headToBody = this.ik.solver.rootNode.position - this.ik.references.head.position;
			this.headToLeftThigh = this.ik.references.leftThigh.position - this.ik.references.head.position;
			this.headToRightThigh = this.ik.references.rightThigh.position - this.ik.references.head.position;
			this.leftShoulderPos = this.ik.references.leftUpperArm.position + this.offset * this.bodyWeight;
			this.rightShoulderPos = this.ik.references.rightUpperArm.position + this.offset * this.bodyWeight;
			this.chestRotation = Quaternion.LookRotation(this.ik.references.head.position - this.ik.references.leftUpperArm.position, this.ik.references.rightUpperArm.position - this.ik.references.leftUpperArm.position);
			if (this.OnPostHeadEffectorFK != null)
			{
				this.OnPostHeadEffectorFK();
			}
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0003FB98 File Offset: 0x0003DD98
		private void SpineBend()
		{
			float num = this.bendWeight * this.ik.solver.IKPositionWeight;
			if (num <= 0f)
			{
				return;
			}
			if (this.bendBones.Length == 0)
			{
				return;
			}
			Quaternion quaternion = base.transform.rotation * Quaternion.Inverse(this.ik.references.root.rotation * this.headRotationRelativeToRoot);
			quaternion = QuaTools.ClampRotation(quaternion, this.bodyClampWeight, 2);
			float num2 = 1f / (float)this.bendBones.Length;
			for (int i = 0; i < this.bendBones.Length; i++)
			{
				if (this.bendBones[i].transform != null)
				{
					this.bendBones[i].transform.rotation = Quaternion.Lerp(Quaternion.identity, quaternion, num2 * this.bendBones[i].weight * num) * this.bendBones[i].transform.rotation;
				}
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0003FC94 File Offset: 0x0003DE94
		private void CCDPass()
		{
			float num = this.CCDWeight * this.ik.solver.IKPositionWeight;
			if (num <= 0f)
			{
				return;
			}
			for (int i = this.CCDBones.Length - 1; i > -1; i--)
			{
				Quaternion quaternion = Quaternion.FromToRotation(this.ik.references.head.position - this.CCDBones[i].position, base.transform.position - this.CCDBones[i].position) * this.CCDBones[i].rotation;
				float num2 = Mathf.Lerp((float)((this.CCDBones.Length - i) / this.CCDBones.Length), 1f, this.roll);
				float num3 = Quaternion.Angle(Quaternion.identity, quaternion);
				num3 = Mathf.Lerp(0f, num3, (this.damper - num3) / this.damper);
				this.CCDBones[i].rotation = Quaternion.RotateTowards(this.CCDBones[i].rotation, quaternion, num3 * num * num2);
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0003FDB0 File Offset: 0x0003DFB0
		private void Iterate(int iteration)
		{
			if (!base.enabled)
			{
				return;
			}
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (this.ik.solver.iterations == 0)
			{
				return;
			}
			this.leftShoulderPos = base.transform.position + (this.leftShoulderPos - base.transform.position).normalized * this.leftShoulderDist;
			this.rightShoulderPos = base.transform.position + (this.rightShoulderPos - base.transform.position).normalized * this.rightShoulderDist;
			this.Solve(ref this.leftShoulderPos, ref this.rightShoulderPos, this.shoulderDist);
			this.LerpSolverPosition(this.ik.solver.leftShoulderEffector, this.leftShoulderPos, this.positionWeight * this.ik.solver.IKPositionWeight, this.ik.solver.leftShoulderEffector.positionOffset);
			this.LerpSolverPosition(this.ik.solver.rightShoulderEffector, this.rightShoulderPos, this.positionWeight * this.ik.solver.IKPositionWeight, this.ik.solver.rightShoulderEffector.positionOffset);
			Quaternion to = Quaternion.LookRotation(base.transform.position - this.leftShoulderPos, this.rightShoulderPos - this.leftShoulderPos);
			Quaternion quaternion = QuaTools.FromToRotation(this.chestRotation, to);
			Vector3 b = quaternion * this.headToBody;
			this.LerpSolverPosition(this.ik.solver.bodyEffector, base.transform.position + b, this.positionWeight * this.ik.solver.IKPositionWeight, this.ik.solver.bodyEffector.positionOffset - this.ik.solver.pullBodyOffset);
			Quaternion rotation = Quaternion.Lerp(Quaternion.identity, quaternion, this.thighWeight);
			Vector3 b2 = rotation * this.headToLeftThigh;
			Vector3 b3 = rotation * this.headToRightThigh;
			this.LerpSolverPosition(this.ik.solver.leftThighEffector, base.transform.position + b2, this.positionWeight * this.ik.solver.IKPositionWeight, this.ik.solver.bodyEffector.positionOffset - this.ik.solver.pullBodyOffset + this.ik.solver.leftThighEffector.positionOffset);
			this.LerpSolverPosition(this.ik.solver.rightThighEffector, base.transform.position + b3, this.positionWeight * this.ik.solver.IKPositionWeight, this.ik.solver.bodyEffector.positionOffset - this.ik.solver.pullBodyOffset + this.ik.solver.rightThighEffector.positionOffset);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000400F0 File Offset: 0x0003E2F0
		private void OnPostUpdate()
		{
			if (!base.enabled)
			{
				return;
			}
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			this.PostStretching();
			Quaternion quaternion = QuaTools.FromToRotation(this.ik.references.head.rotation, base.transform.rotation);
			quaternion = QuaTools.ClampRotation(quaternion, this.headClampWeight, 2);
			this.ik.references.head.rotation = Quaternion.Lerp(Quaternion.identity, quaternion, this.rotationWeight * this.ik.solver.IKPositionWeight) * this.ik.references.head.rotation;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x000401A0 File Offset: 0x0003E3A0
		private void ChestDirection()
		{
			float num = this.chestDirectionWeight * this.ik.solver.IKPositionWeight;
			if (num <= 0f)
			{
				return;
			}
			bool flag = false;
			this.chestDirection = V3Tools.ClampDirection(this.chestDirection, this.ik.references.root.forward, 0.45f, 2, out flag);
			if (this.chestDirection == Vector3.zero)
			{
				return;
			}
			Quaternion quaternion = Quaternion.FromToRotation(this.ik.references.root.forward, this.chestDirection);
			quaternion = Quaternion.Lerp(Quaternion.identity, quaternion, num * (1f / (float)this.chestBones.Length));
			foreach (Transform transform in this.chestBones)
			{
				transform.rotation = quaternion * transform.rotation;
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00040284 File Offset: 0x0003E484
		private void PostStretching()
		{
			float num = this.postStretchWeight * this.ik.solver.IKPositionWeight;
			if (num > 0f)
			{
				Vector3 a = Vector3.ClampMagnitude(base.transform.position - this.ik.references.head.position, this.maxStretch);
				a *= num;
				this.stretchDamper = Mathf.Max(this.stretchDamper, 0f);
				if (this.stretchDamper > 0f)
				{
					a /= (1f + a.magnitude) * (1f + this.stretchDamper);
				}
				for (int i = 0; i < this.stretchBones.Length; i++)
				{
					if (this.stretchBones[i] != null)
					{
						this.stretchBones[i].position += a / (float)this.stretchBones.Length;
					}
				}
			}
			if (this.fixHead && this.ik.solver.IKPositionWeight > 0f)
			{
				this.ik.references.head.position = base.transform.position;
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x000403BA File Offset: 0x0003E5BA
		private void LerpSolverPosition(IKEffector effector, Vector3 position, float weight, Vector3 offset)
		{
			effector.GetNode(this.ik.solver).solverPosition = Vector3.Lerp(effector.GetNode(this.ik.solver).solverPosition, position + offset, weight);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000403F8 File Offset: 0x0003E5F8
		private void Solve(ref Vector3 pos1, ref Vector3 pos2, float nominalDistance)
		{
			Vector3 a = pos2 - pos1;
			float magnitude = a.magnitude;
			if (magnitude == nominalDistance)
			{
				return;
			}
			if (magnitude == 0f)
			{
				return;
			}
			float num = 1f;
			num *= 1f - nominalDistance / magnitude;
			Vector3 b = a * num * 0.5f;
			pos1 += b;
			pos2 -= b;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00040478 File Offset: 0x0003E678
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreRead, new IKSolver.UpdateDelegate(this.OnPreRead));
				IKSolverFullBodyBiped solver2 = this.ik.solver;
				solver2.OnPreIteration = (IKSolver.IterationDelegate)Delegate.Remove(solver2.OnPreIteration, new IKSolver.IterationDelegate(this.Iterate));
				IKSolverFullBodyBiped solver3 = this.ik.solver;
				solver3.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver3.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostUpdate));
				IKSolverFullBodyBiped solver4 = this.ik.solver;
				solver4.OnStoreDefaultLocalState = (IKSolver.UpdateDelegate)Delegate.Remove(solver4.OnStoreDefaultLocalState, new IKSolver.UpdateDelegate(this.OnStoreDefaultLocalState));
				IKSolverFullBodyBiped solver5 = this.ik.solver;
				solver5.OnFixTransforms = (IKSolver.UpdateDelegate)Delegate.Remove(solver5.OnFixTransforms, new IKSolver.UpdateDelegate(this.OnFixTransforms));
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00040574 File Offset: 0x0003E774
		public FBBIKHeadEffector()
		{
		}

		// Token: 0x0400077E RID: 1918
		[Tooltip("Reference to the FBBIK component.")]
		public FullBodyBipedIK ik;

		// Token: 0x0400077F RID: 1919
		[LargeHeader("Position")]
		[Tooltip("Master weight for positioning the head.")]
		[Range(0f, 1f)]
		public float positionWeight = 1f;

		// Token: 0x04000780 RID: 1920
		[Tooltip("The weight of moving the body along with the head")]
		[Range(0f, 1f)]
		public float bodyWeight = 0.8f;

		// Token: 0x04000781 RID: 1921
		[Tooltip("The weight of moving the thighs along with the head")]
		[Range(0f, 1f)]
		public float thighWeight = 0.8f;

		// Token: 0x04000782 RID: 1922
		[Tooltip("If false, hands will not pull the head away if they are too far. Disabling this will improve performance significantly.")]
		public bool handsPullBody = true;

		// Token: 0x04000783 RID: 1923
		[LargeHeader("Rotation")]
		[Tooltip("The weight of rotating the head bone after solving")]
		[Range(0f, 1f)]
		public float rotationWeight;

		// Token: 0x04000784 RID: 1924
		[Tooltip("Clamping the rotation of the body")]
		[Range(0f, 1f)]
		public float bodyClampWeight = 0.5f;

		// Token: 0x04000785 RID: 1925
		[Tooltip("Clamping the rotation of the head")]
		[Range(0f, 1f)]
		public float headClampWeight = 0.5f;

		// Token: 0x04000786 RID: 1926
		[Tooltip("The master weight of bending/twisting the spine to the rotation of the head effector. This is similar to CCD, but uses the rotation of the head effector not the position.")]
		[Range(0f, 1f)]
		public float bendWeight = 1f;

		// Token: 0x04000787 RID: 1927
		[Tooltip("The bones to use for bending.")]
		public FBBIKHeadEffector.BendBone[] bendBones = new FBBIKHeadEffector.BendBone[0];

		// Token: 0x04000788 RID: 1928
		[LargeHeader("CCD")]
		[Tooltip("Optional. The master weight of the CCD (Cyclic Coordinate Descent) IK effect that bends the spine towards the head effector before FBBIK solves.")]
		[Range(0f, 1f)]
		public float CCDWeight = 1f;

		// Token: 0x04000789 RID: 1929
		[Tooltip("The weight of rolling the bones in towards the target")]
		[Range(0f, 1f)]
		public float roll;

		// Token: 0x0400078A RID: 1930
		[Tooltip("Smoothing the CCD effect.")]
		[Range(0f, 1000f)]
		public float damper = 500f;

		// Token: 0x0400078B RID: 1931
		[Tooltip("Bones to use for the CCD pass. Assign spine and/or neck bones.")]
		public Transform[] CCDBones = new Transform[0];

		// Token: 0x0400078C RID: 1932
		[LargeHeader("Stretching")]
		[Tooltip("Stretching the spine/neck to help reach the target. This is useful for making sure the head stays locked relative to the VR headset. NB! Stretching is done after FBBIK has solved so if you have the hand effectors pinned and spine bones included in the 'Stretch Bones', the hands might become offset from their target positions.")]
		[Range(0f, 1f)]
		public float postStretchWeight = 1f;

		// Token: 0x0400078D RID: 1933
		[Tooltip("Stretch magnitude limit.")]
		public float maxStretch = 0.1f;

		// Token: 0x0400078E RID: 1934
		[Tooltip("If > 0, dampers the stretching effect.")]
		public float stretchDamper;

		// Token: 0x0400078F RID: 1935
		[Tooltip("If true, will fix head position to this Transform no matter what. Good for making sure the head will not budge away from the VR headset")]
		public bool fixHead;

		// Token: 0x04000790 RID: 1936
		[Tooltip("Bones to use for stretching. The more bones you add, the less noticable the effect.")]
		public Transform[] stretchBones = new Transform[0];

		// Token: 0x04000791 RID: 1937
		[LargeHeader("Chest Direction")]
		public Vector3 chestDirection = Vector3.forward;

		// Token: 0x04000792 RID: 1938
		[Range(0f, 1f)]
		public float chestDirectionWeight = 1f;

		// Token: 0x04000793 RID: 1939
		public Transform[] chestBones = new Transform[0];

		// Token: 0x04000794 RID: 1940
		public IKSolver.UpdateDelegate OnPostHeadEffectorFK;

		// Token: 0x04000795 RID: 1941
		private Vector3 offset;

		// Token: 0x04000796 RID: 1942
		private Vector3 headToBody;

		// Token: 0x04000797 RID: 1943
		private Vector3 shoulderCenterToHead;

		// Token: 0x04000798 RID: 1944
		private Vector3 headToLeftThigh;

		// Token: 0x04000799 RID: 1945
		private Vector3 headToRightThigh;

		// Token: 0x0400079A RID: 1946
		private Vector3 leftShoulderPos;

		// Token: 0x0400079B RID: 1947
		private Vector3 rightShoulderPos;

		// Token: 0x0400079C RID: 1948
		private float shoulderDist;

		// Token: 0x0400079D RID: 1949
		private float leftShoulderDist;

		// Token: 0x0400079E RID: 1950
		private float rightShoulderDist;

		// Token: 0x0400079F RID: 1951
		private Quaternion chestRotation;

		// Token: 0x040007A0 RID: 1952
		private Quaternion headRotationRelativeToRoot;

		// Token: 0x040007A1 RID: 1953
		private Quaternion[] ccdDefaultLocalRotations = new Quaternion[0];

		// Token: 0x040007A2 RID: 1954
		private Vector3 headLocalPosition;

		// Token: 0x040007A3 RID: 1955
		private Quaternion headLocalRotation;

		// Token: 0x040007A4 RID: 1956
		private Vector3[] stretchLocalPositions = new Vector3[0];

		// Token: 0x040007A5 RID: 1957
		private Quaternion[] stretchLocalRotations = new Quaternion[0];

		// Token: 0x040007A6 RID: 1958
		private Vector3[] chestLocalPositions = new Vector3[0];

		// Token: 0x040007A7 RID: 1959
		private Quaternion[] chestLocalRotations = new Quaternion[0];

		// Token: 0x040007A8 RID: 1960
		private int bendBonesCount;

		// Token: 0x040007A9 RID: 1961
		private int ccdBonesCount;

		// Token: 0x040007AA RID: 1962
		private int stretchBonesCount;

		// Token: 0x040007AB RID: 1963
		private int chestBonesCount;

		// Token: 0x020001EF RID: 495
		[Serializable]
		public class BendBone
		{
			// Token: 0x06001045 RID: 4165 RVA: 0x00065B5F File Offset: 0x00063D5F
			public BendBone()
			{
			}

			// Token: 0x06001046 RID: 4166 RVA: 0x00065B7D File Offset: 0x00063D7D
			public BendBone(Transform transform, float weight)
			{
				this.transform = transform;
				this.weight = weight;
			}

			// Token: 0x06001047 RID: 4167 RVA: 0x00065BA9 File Offset: 0x00063DA9
			public void StoreDefaultLocalState()
			{
				this.defaultLocalRotation = this.transform.localRotation;
			}

			// Token: 0x06001048 RID: 4168 RVA: 0x00065BBC File Offset: 0x00063DBC
			public void FixTransforms()
			{
				this.transform.localRotation = this.defaultLocalRotation;
			}

			// Token: 0x04000EAC RID: 3756
			[Tooltip("Assign spine and/or neck bones.")]
			public Transform transform;

			// Token: 0x04000EAD RID: 3757
			[Tooltip("The weight of rotating this bone.")]
			[Range(0f, 1f)]
			public float weight = 0.5f;

			// Token: 0x04000EAE RID: 3758
			private Quaternion defaultLocalRotation = Quaternion.identity;
		}
	}
}
