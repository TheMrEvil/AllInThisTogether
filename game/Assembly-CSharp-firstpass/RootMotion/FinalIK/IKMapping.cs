using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	public class IKMapping
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x000426C6 File Offset: 0x000408C6
		public virtual bool IsValid(IKSolver solver, ref string message)
		{
			return true;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000426C9 File Offset: 0x000408C9
		public virtual void Initiate(IKSolverFullBody solver)
		{
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x000426CC File Offset: 0x000408CC
		protected bool BoneIsValid(Transform bone, IKSolver solver, ref string message, Warning.Logger logger = null)
		{
			if (bone == null)
			{
				message = "IKMappingLimb contains a null reference.";
				if (logger != null)
				{
					logger(message);
				}
				return false;
			}
			if (solver.GetPoint(bone) == null)
			{
				message = "IKMappingLimb is referencing to a bone '" + bone.name + "' that does not excist in the Node Chain.";
				if (logger != null)
				{
					logger(message);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00042728 File Offset: 0x00040928
		protected Vector3 SolveFABRIKJoint(Vector3 pos1, Vector3 pos2, float length)
		{
			return pos2 + (pos1 - pos2).normalized * length;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00042750 File Offset: 0x00040950
		public IKMapping()
		{
		}

		// Token: 0x020001F2 RID: 498
		[Serializable]
		public class BoneMap
		{
			// Token: 0x06001051 RID: 4177 RVA: 0x00065E48 File Offset: 0x00064048
			public void Initiate(Transform transform, IKSolverFullBody solver)
			{
				this.transform = transform;
				solver.GetChainAndNodeIndexes(transform, out this.chainIndex, out this.nodeIndex);
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x06001052 RID: 4178 RVA: 0x00065E64 File Offset: 0x00064064
			public Vector3 swingDirection
			{
				get
				{
					return this.transform.rotation * this.localSwingAxis;
				}
			}

			// Token: 0x06001053 RID: 4179 RVA: 0x00065E7C File Offset: 0x0006407C
			public void StoreDefaultLocalState()
			{
				this.defaultLocalPosition = this.transform.localPosition;
				this.defaultLocalRotation = this.transform.localRotation;
			}

			// Token: 0x06001054 RID: 4180 RVA: 0x00065EA0 File Offset: 0x000640A0
			public void FixTransform(bool position)
			{
				if (position)
				{
					this.transform.localPosition = this.defaultLocalPosition;
				}
				this.transform.localRotation = this.defaultLocalRotation;
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x06001055 RID: 4181 RVA: 0x00065EC7 File Offset: 0x000640C7
			public bool isNodeBone
			{
				get
				{
					return this.nodeIndex != -1;
				}
			}

			// Token: 0x06001056 RID: 4182 RVA: 0x00065ED5 File Offset: 0x000640D5
			public void SetLength(IKMapping.BoneMap nextBone)
			{
				this.length = Vector3.Distance(this.transform.position, nextBone.transform.position);
			}

			// Token: 0x06001057 RID: 4183 RVA: 0x00065EF8 File Offset: 0x000640F8
			public void SetLocalSwingAxis(IKMapping.BoneMap swingTarget)
			{
				this.SetLocalSwingAxis(swingTarget, this);
			}

			// Token: 0x06001058 RID: 4184 RVA: 0x00065F02 File Offset: 0x00064102
			public void SetLocalSwingAxis(IKMapping.BoneMap bone1, IKMapping.BoneMap bone2)
			{
				this.localSwingAxis = Quaternion.Inverse(this.transform.rotation) * (bone1.transform.position - bone2.transform.position);
			}

			// Token: 0x06001059 RID: 4185 RVA: 0x00065F3A File Offset: 0x0006413A
			public void SetLocalTwistAxis(Vector3 twistDirection, Vector3 normalDirection)
			{
				Vector3.OrthoNormalize(ref normalDirection, ref twistDirection);
				this.localTwistAxis = Quaternion.Inverse(this.transform.rotation) * twistDirection;
			}

			// Token: 0x0600105A RID: 4186 RVA: 0x00065F64 File Offset: 0x00064164
			public void SetPlane(IKSolverFullBody solver, Transform planeBone1, Transform planeBone2, Transform planeBone3)
			{
				this.planeBone1 = planeBone1;
				this.planeBone2 = planeBone2;
				this.planeBone3 = planeBone3;
				solver.GetChainAndNodeIndexes(planeBone1, out this.plane1ChainIndex, out this.plane1NodeIndex);
				solver.GetChainAndNodeIndexes(planeBone2, out this.plane2ChainIndex, out this.plane2NodeIndex);
				solver.GetChainAndNodeIndexes(planeBone3, out this.plane3ChainIndex, out this.plane3NodeIndex);
				this.UpdatePlane(true, true);
			}

			// Token: 0x0600105B RID: 4187 RVA: 0x00065FCC File Offset: 0x000641CC
			public void UpdatePlane(bool rotation, bool position)
			{
				Quaternion lastAnimatedTargetRotation = this.lastAnimatedTargetRotation;
				if (rotation)
				{
					this.defaultLocalTargetRotation = QuaTools.RotationToLocalSpace(this.transform.rotation, lastAnimatedTargetRotation);
				}
				if (position)
				{
					this.planePosition = Quaternion.Inverse(lastAnimatedTargetRotation) * (this.transform.position - this.planeBone1.position);
				}
			}

			// Token: 0x0600105C RID: 4188 RVA: 0x00066029 File Offset: 0x00064229
			public void SetIKPosition()
			{
				this.ikPosition = this.transform.position;
			}

			// Token: 0x0600105D RID: 4189 RVA: 0x0006603C File Offset: 0x0006423C
			public void MaintainRotation()
			{
				this.maintainRotation = this.transform.rotation;
			}

			// Token: 0x0600105E RID: 4190 RVA: 0x0006604F File Offset: 0x0006424F
			public void SetToIKPosition()
			{
				this.transform.position = this.ikPosition;
			}

			// Token: 0x0600105F RID: 4191 RVA: 0x00066064 File Offset: 0x00064264
			public void FixToNode(IKSolverFullBody solver, float weight, IKSolver.Node fixNode = null)
			{
				if (fixNode == null)
				{
					fixNode = solver.GetNode(this.chainIndex, this.nodeIndex);
				}
				if (weight >= 1f)
				{
					this.transform.position = fixNode.solverPosition;
					return;
				}
				this.transform.position = Vector3.Lerp(this.transform.position, fixNode.solverPosition, weight);
			}

			// Token: 0x06001060 RID: 4192 RVA: 0x000660C4 File Offset: 0x000642C4
			public Vector3 GetPlanePosition(IKSolverFullBody solver)
			{
				return solver.GetNode(this.plane1ChainIndex, this.plane1NodeIndex).solverPosition + this.GetTargetRotation(solver) * this.planePosition;
			}

			// Token: 0x06001061 RID: 4193 RVA: 0x000660F4 File Offset: 0x000642F4
			public void PositionToPlane(IKSolverFullBody solver)
			{
				this.transform.position = this.GetPlanePosition(solver);
			}

			// Token: 0x06001062 RID: 4194 RVA: 0x00066108 File Offset: 0x00064308
			public void RotateToPlane(IKSolverFullBody solver, float weight)
			{
				Quaternion quaternion = this.GetTargetRotation(solver) * this.defaultLocalTargetRotation;
				if (weight >= 1f)
				{
					this.transform.rotation = quaternion;
					return;
				}
				this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, weight);
			}

			// Token: 0x06001063 RID: 4195 RVA: 0x0006615A File Offset: 0x0006435A
			public void Swing(Vector3 swingTarget, float weight)
			{
				this.Swing(swingTarget, this.transform.position, weight);
			}

			// Token: 0x06001064 RID: 4196 RVA: 0x00066170 File Offset: 0x00064370
			public void Swing(Vector3 pos1, Vector3 pos2, float weight)
			{
				Quaternion quaternion = Quaternion.FromToRotation(this.transform.rotation * this.localSwingAxis, pos1 - pos2) * this.transform.rotation;
				if (weight >= 1f)
				{
					this.transform.rotation = quaternion;
					return;
				}
				this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, weight);
			}

			// Token: 0x06001065 RID: 4197 RVA: 0x000661E4 File Offset: 0x000643E4
			public void Twist(Vector3 twistDirection, Vector3 normalDirection, float weight)
			{
				Vector3.OrthoNormalize(ref normalDirection, ref twistDirection);
				Quaternion quaternion = Quaternion.FromToRotation(this.transform.rotation * this.localTwistAxis, twistDirection) * this.transform.rotation;
				if (weight >= 1f)
				{
					this.transform.rotation = quaternion;
					return;
				}
				this.transform.rotation = Quaternion.Lerp(this.transform.rotation, quaternion, weight);
			}

			// Token: 0x06001066 RID: 4198 RVA: 0x00066259 File Offset: 0x00064459
			public void RotateToMaintain(float weight)
			{
				if (weight <= 0f)
				{
					return;
				}
				this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.maintainRotation, weight);
			}

			// Token: 0x06001067 RID: 4199 RVA: 0x00066288 File Offset: 0x00064488
			public void RotateToEffector(IKSolverFullBody solver, float weight)
			{
				if (!this.isNodeBone)
				{
					return;
				}
				float num = weight * solver.GetNode(this.chainIndex, this.nodeIndex).effectorRotationWeight;
				if (num <= 0f)
				{
					return;
				}
				if (num >= 1f)
				{
					this.transform.rotation = solver.GetNode(this.chainIndex, this.nodeIndex).solverRotation;
					return;
				}
				this.transform.rotation = Quaternion.Lerp(this.transform.rotation, solver.GetNode(this.chainIndex, this.nodeIndex).solverRotation, num);
			}

			// Token: 0x06001068 RID: 4200 RVA: 0x00066320 File Offset: 0x00064520
			private Quaternion GetTargetRotation(IKSolverFullBody solver)
			{
				Vector3 solverPosition = solver.GetNode(this.plane1ChainIndex, this.plane1NodeIndex).solverPosition;
				Vector3 solverPosition2 = solver.GetNode(this.plane2ChainIndex, this.plane2NodeIndex).solverPosition;
				Vector3 solverPosition3 = solver.GetNode(this.plane3ChainIndex, this.plane3NodeIndex).solverPosition;
				if (solverPosition == solverPosition3)
				{
					return Quaternion.identity;
				}
				return Quaternion.LookRotation(solverPosition2 - solverPosition, solverPosition3 - solverPosition);
			}

			// Token: 0x17000220 RID: 544
			// (get) Token: 0x06001069 RID: 4201 RVA: 0x00066398 File Offset: 0x00064598
			private Quaternion lastAnimatedTargetRotation
			{
				get
				{
					if (this.planeBone1.position == this.planeBone3.position)
					{
						return Quaternion.identity;
					}
					return Quaternion.LookRotation(this.planeBone2.position - this.planeBone1.position, this.planeBone3.position - this.planeBone1.position);
				}
			}

			// Token: 0x0600106A RID: 4202 RVA: 0x00066403 File Offset: 0x00064603
			public BoneMap()
			{
			}

			// Token: 0x04000EBD RID: 3773
			public Transform transform;

			// Token: 0x04000EBE RID: 3774
			public int chainIndex = -1;

			// Token: 0x04000EBF RID: 3775
			public int nodeIndex = -1;

			// Token: 0x04000EC0 RID: 3776
			public Vector3 defaultLocalPosition;

			// Token: 0x04000EC1 RID: 3777
			public Quaternion defaultLocalRotation;

			// Token: 0x04000EC2 RID: 3778
			public Vector3 localSwingAxis;

			// Token: 0x04000EC3 RID: 3779
			public Vector3 localTwistAxis;

			// Token: 0x04000EC4 RID: 3780
			public Vector3 planePosition;

			// Token: 0x04000EC5 RID: 3781
			public Vector3 ikPosition;

			// Token: 0x04000EC6 RID: 3782
			public Quaternion defaultLocalTargetRotation;

			// Token: 0x04000EC7 RID: 3783
			private Quaternion maintainRotation;

			// Token: 0x04000EC8 RID: 3784
			public float length;

			// Token: 0x04000EC9 RID: 3785
			public Quaternion animatedRotation;

			// Token: 0x04000ECA RID: 3786
			private Transform planeBone1;

			// Token: 0x04000ECB RID: 3787
			private Transform planeBone2;

			// Token: 0x04000ECC RID: 3788
			private Transform planeBone3;

			// Token: 0x04000ECD RID: 3789
			private int plane1ChainIndex = -1;

			// Token: 0x04000ECE RID: 3790
			private int plane1NodeIndex = -1;

			// Token: 0x04000ECF RID: 3791
			private int plane2ChainIndex = -1;

			// Token: 0x04000ED0 RID: 3792
			private int plane2NodeIndex = -1;

			// Token: 0x04000ED1 RID: 3793
			private int plane3ChainIndex = -1;

			// Token: 0x04000ED2 RID: 3794
			private int plane3NodeIndex = -1;
		}
	}
}
