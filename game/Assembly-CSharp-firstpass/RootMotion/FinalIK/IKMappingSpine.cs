using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000ED RID: 237
	[Serializable]
	public class IKMappingSpine : IKMapping
	{
		// Token: 0x06000A15 RID: 2581 RVA: 0x00042D30 File Offset: 0x00040F30
		public override bool IsValid(IKSolver solver, ref string message)
		{
			if (!base.IsValid(solver, ref message))
			{
				return false;
			}
			Transform[] array = this.spineBones;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					message = "Spine bones contains a null reference.";
					return false;
				}
			}
			int num = 0;
			for (int j = 0; j < this.spineBones.Length; j++)
			{
				if (solver.GetPoint(this.spineBones[j]) != null)
				{
					num++;
				}
			}
			if (num == 0)
			{
				message = "IKMappingSpine does not contain any nodes.";
				return false;
			}
			if (this.leftUpperArmBone == null)
			{
				message = "IKMappingSpine is missing the left upper arm bone.";
				return false;
			}
			if (this.rightUpperArmBone == null)
			{
				message = "IKMappingSpine is missing the right upper arm bone.";
				return false;
			}
			if (this.leftThighBone == null)
			{
				message = "IKMappingSpine is missing the left thigh bone.";
				return false;
			}
			if (this.rightThighBone == null)
			{
				message = "IKMappingSpine is missing the right thigh bone.";
				return false;
			}
			if (solver.GetPoint(this.leftUpperArmBone) == null)
			{
				message = "Full Body IK is missing the left upper arm node.";
				return false;
			}
			if (solver.GetPoint(this.rightUpperArmBone) == null)
			{
				message = "Full Body IK is missing the right upper arm node.";
				return false;
			}
			if (solver.GetPoint(this.leftThighBone) == null)
			{
				message = "Full Body IK is missing the left thigh node.";
				return false;
			}
			if (solver.GetPoint(this.rightThighBone) == null)
			{
				message = "Full Body IK is missing the right thigh node.";
				return false;
			}
			return true;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00042E60 File Offset: 0x00041060
		public IKMappingSpine()
		{
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00042EC0 File Offset: 0x000410C0
		public IKMappingSpine(Transform[] spineBones, Transform leftUpperArmBone, Transform rightUpperArmBone, Transform leftThighBone, Transform rightThighBone)
		{
			this.SetBones(spineBones, leftUpperArmBone, rightUpperArmBone, leftThighBone, rightThighBone);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00042F2A File Offset: 0x0004112A
		public void SetBones(Transform[] spineBones, Transform leftUpperArmBone, Transform rightUpperArmBone, Transform leftThighBone, Transform rightThighBone)
		{
			this.spineBones = spineBones;
			this.leftUpperArmBone = leftUpperArmBone;
			this.rightUpperArmBone = rightUpperArmBone;
			this.leftThighBone = leftThighBone;
			this.rightThighBone = rightThighBone;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00042F54 File Offset: 0x00041154
		public void StoreDefaultLocalState()
		{
			for (int i = 0; i < this.spine.Length; i++)
			{
				this.spine[i].StoreDefaultLocalState();
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00042F84 File Offset: 0x00041184
		public void FixTransforms()
		{
			for (int i = 0; i < this.spine.Length; i++)
			{
				this.spine[i].FixTransform(i == 0 || i == this.spine.Length - 1);
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00042FC4 File Offset: 0x000411C4
		public override void Initiate(IKSolverFullBody solver)
		{
			if (this.iterations <= 0)
			{
				this.iterations = 3;
			}
			if (this.spine == null || this.spine.Length != this.spineBones.Length)
			{
				this.spine = new IKMapping.BoneMap[this.spineBones.Length];
			}
			this.rootNodeIndex = -1;
			for (int i = 0; i < this.spineBones.Length; i++)
			{
				if (this.spine[i] == null)
				{
					this.spine[i] = new IKMapping.BoneMap();
				}
				this.spine[i].Initiate(this.spineBones[i], solver);
				if (this.spine[i].isNodeBone)
				{
					this.rootNodeIndex = i;
				}
			}
			if (this.leftUpperArm == null)
			{
				this.leftUpperArm = new IKMapping.BoneMap();
			}
			if (this.rightUpperArm == null)
			{
				this.rightUpperArm = new IKMapping.BoneMap();
			}
			if (this.leftThigh == null)
			{
				this.leftThigh = new IKMapping.BoneMap();
			}
			if (this.rightThigh == null)
			{
				this.rightThigh = new IKMapping.BoneMap();
			}
			this.leftUpperArm.Initiate(this.leftUpperArmBone, solver);
			this.rightUpperArm.Initiate(this.rightUpperArmBone, solver);
			this.leftThigh.Initiate(this.leftThighBone, solver);
			this.rightThigh.Initiate(this.rightThighBone, solver);
			for (int j = 0; j < this.spine.Length; j++)
			{
				this.spine[j].SetIKPosition();
			}
			this.spine[0].SetPlane(solver, this.spine[this.rootNodeIndex].transform, this.leftThigh.transform, this.rightThigh.transform);
			for (int k = 0; k < this.spine.Length - 1; k++)
			{
				this.spine[k].SetLength(this.spine[k + 1]);
				this.spine[k].SetLocalSwingAxis(this.spine[k + 1]);
				this.spine[k].SetLocalTwistAxis(this.leftUpperArm.transform.position - this.rightUpperArm.transform.position, this.spine[k + 1].transform.position - this.spine[k].transform.position);
			}
			this.spine[this.spine.Length - 1].SetPlane(solver, this.spine[this.rootNodeIndex].transform, this.leftUpperArm.transform, this.rightUpperArm.transform);
			this.spine[this.spine.Length - 1].SetLocalSwingAxis(this.leftUpperArm, this.rightUpperArm);
			this.useFABRIK = this.UseFABRIK();
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00043268 File Offset: 0x00041468
		private bool UseFABRIK()
		{
			return this.spine.Length > 3 || this.rootNodeIndex != 1;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00043284 File Offset: 0x00041484
		public void ReadPose()
		{
			this.spine[0].UpdatePlane(true, true);
			for (int i = 0; i < this.spine.Length - 1; i++)
			{
				this.spine[i].SetLength(this.spine[i + 1]);
				this.spine[i].SetLocalSwingAxis(this.spine[i + 1]);
				this.spine[i].SetLocalTwistAxis(this.leftUpperArm.transform.position - this.rightUpperArm.transform.position, this.spine[i + 1].transform.position - this.spine[i].transform.position);
			}
			this.spine[this.spine.Length - 1].UpdatePlane(true, true);
			this.spine[this.spine.Length - 1].SetLocalSwingAxis(this.leftUpperArm, this.rightUpperArm);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00043380 File Offset: 0x00041580
		public void WritePose(IKSolverFullBody solver)
		{
			Vector3 planePosition = this.spine[0].GetPlanePosition(solver);
			Vector3 solverPosition = solver.GetNode(this.spine[this.rootNodeIndex].chainIndex, this.spine[this.rootNodeIndex].nodeIndex).solverPosition;
			Vector3 planePosition2 = this.spine[this.spine.Length - 1].GetPlanePosition(solver);
			if (this.useFABRIK)
			{
				Vector3 b = solver.GetNode(this.spine[this.rootNodeIndex].chainIndex, this.spine[this.rootNodeIndex].nodeIndex).solverPosition - this.spine[this.rootNodeIndex].transform.position;
				for (int i = 0; i < this.spine.Length; i++)
				{
					this.spine[i].ikPosition = this.spine[i].transform.position + b;
				}
				for (int j = 0; j < this.iterations; j++)
				{
					this.ForwardReach(planePosition2);
					this.BackwardReach(planePosition);
					this.spine[this.rootNodeIndex].ikPosition = solverPosition;
				}
			}
			else
			{
				this.spine[0].ikPosition = planePosition;
				this.spine[this.rootNodeIndex].ikPosition = solverPosition;
			}
			this.spine[this.spine.Length - 1].ikPosition = planePosition2;
			this.MapToSolverPositions(solver);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x000434F0 File Offset: 0x000416F0
		public void ForwardReach(Vector3 position)
		{
			this.spine[this.spineBones.Length - 1].ikPosition = position;
			for (int i = this.spine.Length - 2; i > -1; i--)
			{
				this.spine[i].ikPosition = base.SolveFABRIKJoint(this.spine[i].ikPosition, this.spine[i + 1].ikPosition, this.spine[i].length);
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00043568 File Offset: 0x00041768
		private void BackwardReach(Vector3 position)
		{
			this.spine[0].ikPosition = position;
			for (int i = 1; i < this.spine.Length; i++)
			{
				this.spine[i].ikPosition = base.SolveFABRIKJoint(this.spine[i].ikPosition, this.spine[i - 1].ikPosition, this.spine[i - 1].length);
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x000435D4 File Offset: 0x000417D4
		private void MapToSolverPositions(IKSolverFullBody solver)
		{
			this.spine[0].SetToIKPosition();
			this.spine[0].RotateToPlane(solver, 1f);
			for (int i = 1; i < this.spine.Length - 1; i++)
			{
				this.spine[i].Swing(this.spine[i + 1].ikPosition, 1f);
				if (this.twistWeight > 0f)
				{
					float num = (float)i / ((float)this.spine.Length - 2f);
					Vector3 solverPosition = solver.GetNode(this.leftUpperArm.chainIndex, this.leftUpperArm.nodeIndex).solverPosition;
					Vector3 solverPosition2 = solver.GetNode(this.rightUpperArm.chainIndex, this.rightUpperArm.nodeIndex).solverPosition;
					this.spine[i].Twist(solverPosition - solverPosition2, this.spine[i + 1].ikPosition - this.spine[i].transform.position, num * this.twistWeight);
				}
			}
			this.spine[this.spine.Length - 1].SetToIKPosition();
			this.spine[this.spine.Length - 1].RotateToPlane(solver, 1f);
		}

		// Token: 0x04000803 RID: 2051
		public Transform[] spineBones;

		// Token: 0x04000804 RID: 2052
		public Transform leftUpperArmBone;

		// Token: 0x04000805 RID: 2053
		public Transform rightUpperArmBone;

		// Token: 0x04000806 RID: 2054
		public Transform leftThighBone;

		// Token: 0x04000807 RID: 2055
		public Transform rightThighBone;

		// Token: 0x04000808 RID: 2056
		[Range(1f, 3f)]
		public int iterations = 3;

		// Token: 0x04000809 RID: 2057
		[Range(0f, 1f)]
		public float twistWeight = 1f;

		// Token: 0x0400080A RID: 2058
		private int rootNodeIndex;

		// Token: 0x0400080B RID: 2059
		private IKMapping.BoneMap[] spine = new IKMapping.BoneMap[0];

		// Token: 0x0400080C RID: 2060
		private IKMapping.BoneMap leftUpperArm = new IKMapping.BoneMap();

		// Token: 0x0400080D RID: 2061
		private IKMapping.BoneMap rightUpperArm = new IKMapping.BoneMap();

		// Token: 0x0400080E RID: 2062
		private IKMapping.BoneMap leftThigh = new IKMapping.BoneMap();

		// Token: 0x0400080F RID: 2063
		private IKMapping.BoneMap rightThigh = new IKMapping.BoneMap();

		// Token: 0x04000810 RID: 2064
		private bool useFABRIK;
	}
}
