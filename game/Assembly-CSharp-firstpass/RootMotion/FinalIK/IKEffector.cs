using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public class IKEffector
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x00041C0F File Offset: 0x0003FE0F
		public IKSolver.Node GetNode(IKSolverFullBody solver)
		{
			return solver.chain[this.chainIndex].nodes[this.nodeIndex];
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00041C2A File Offset: 0x0003FE2A
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x00041C32 File Offset: 0x0003FE32
		public bool isEndEffector
		{
			[CompilerGenerated]
			get
			{
				return this.<isEndEffector>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isEndEffector>k__BackingField = value;
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00041C3C File Offset: 0x0003FE3C
		public void PinToBone(float positionWeight, float rotationWeight)
		{
			this.position = this.bone.position;
			this.positionWeight = Mathf.Clamp(positionWeight, 0f, 1f);
			this.rotation = this.bone.rotation;
			this.rotationWeight = Mathf.Clamp(rotationWeight, 0f, 1f);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00041C98 File Offset: 0x0003FE98
		public IKEffector()
		{
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00041D40 File Offset: 0x0003FF40
		public IKEffector(Transform bone, Transform[] childBones)
		{
			this.bone = bone;
			this.childBones = childBones;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00041DF8 File Offset: 0x0003FFF8
		public bool IsValid(IKSolver solver, ref string message)
		{
			if (this.bone == null)
			{
				message = "IK Effector bone is null.";
				return false;
			}
			if (solver.GetPoint(this.bone) == null)
			{
				message = "IK Effector is referencing to a bone '" + this.bone.name + "' that does not excist in the Node Chain.";
				return false;
			}
			Transform[] array = this.childBones;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					message = "IK Effector contains a null reference.";
					return false;
				}
			}
			foreach (Transform transform in this.childBones)
			{
				if (solver.GetPoint(transform) == null)
				{
					message = "IK Effector is referencing to a bone '" + transform.name + "' that does not excist in the Node Chain.";
					return false;
				}
			}
			if (this.planeBone1 != null && solver.GetPoint(this.planeBone1) == null)
			{
				message = "IK Effector is referencing to a bone '" + this.planeBone1.name + "' that does not excist in the Node Chain.";
				return false;
			}
			if (this.planeBone2 != null && solver.GetPoint(this.planeBone2) == null)
			{
				message = "IK Effector is referencing to a bone '" + this.planeBone2.name + "' that does not excist in the Node Chain.";
				return false;
			}
			if (this.planeBone3 != null && solver.GetPoint(this.planeBone3) == null)
			{
				message = "IK Effector is referencing to a bone '" + this.planeBone3.name + "' that does not excist in the Node Chain.";
				return false;
			}
			return true;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00041F5C File Offset: 0x0004015C
		public void Initiate(IKSolverFullBody solver)
		{
			this.position = this.bone.position;
			this.rotation = this.bone.rotation;
			this.animatedPlaneRotation = Quaternion.identity;
			solver.GetChainAndNodeIndexes(this.bone, out this.chainIndex, out this.nodeIndex);
			this.childChainIndexes = new int[this.childBones.Length];
			this.childNodeIndexes = new int[this.childBones.Length];
			for (int i = 0; i < this.childBones.Length; i++)
			{
				solver.GetChainAndNodeIndexes(this.childBones[i], out this.childChainIndexes[i], out this.childNodeIndexes[i]);
			}
			this.localPositions = new Vector3[this.childBones.Length];
			this.usePlaneNodes = false;
			if (this.planeBone1 != null)
			{
				solver.GetChainAndNodeIndexes(this.planeBone1, out this.plane1ChainIndex, out this.plane1NodeIndex);
				if (this.planeBone2 != null)
				{
					solver.GetChainAndNodeIndexes(this.planeBone2, out this.plane2ChainIndex, out this.plane2NodeIndex);
					if (this.planeBone3 != null)
					{
						solver.GetChainAndNodeIndexes(this.planeBone3, out this.plane3ChainIndex, out this.plane3NodeIndex);
						this.usePlaneNodes = true;
					}
				}
				this.isEndEffector = true;
				return;
			}
			this.isEndEffector = false;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000420B0 File Offset: 0x000402B0
		public void ResetOffset(IKSolverFullBody solver)
		{
			solver.GetNode(this.chainIndex, this.nodeIndex).offset = Vector3.zero;
			for (int i = 0; i < this.childChainIndexes.Length; i++)
			{
				solver.GetNode(this.childChainIndexes[i], this.childNodeIndexes[i]).offset = Vector3.zero;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0004210C File Offset: 0x0004030C
		public void SetToTarget()
		{
			if (this.target == null)
			{
				return;
			}
			this.position = this.target.position;
			this.rotation = this.target.rotation;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00042140 File Offset: 0x00040340
		public void OnPreSolve(IKSolverFullBody solver)
		{
			this.positionWeight = Mathf.Clamp(this.positionWeight, 0f, 1f);
			this.rotationWeight = Mathf.Clamp(this.rotationWeight, 0f, 1f);
			this.maintainRelativePositionWeight = Mathf.Clamp(this.maintainRelativePositionWeight, 0f, 1f);
			this.posW = this.positionWeight * solver.IKPositionWeight;
			this.rotW = this.rotationWeight * solver.IKPositionWeight;
			solver.GetNode(this.chainIndex, this.nodeIndex).effectorPositionWeight = this.posW;
			solver.GetNode(this.chainIndex, this.nodeIndex).effectorRotationWeight = this.rotW;
			solver.GetNode(this.chainIndex, this.nodeIndex).solverRotation = this.rotation;
			if (float.IsInfinity(this.positionOffset.x) || float.IsInfinity(this.positionOffset.y) || float.IsInfinity(this.positionOffset.z))
			{
				Debug.LogError("Invalid IKEffector.positionOffset (contains Infinity)! Please make sure not to set IKEffector.positionOffset to infinite values.", this.bone);
			}
			if (float.IsNaN(this.positionOffset.x) || float.IsNaN(this.positionOffset.y) || float.IsNaN(this.positionOffset.z))
			{
				Debug.LogError("Invalid IKEffector.positionOffset (contains NaN)! Please make sure not to set IKEffector.positionOffset to NaN values.", this.bone);
			}
			if (this.positionOffset.sqrMagnitude > 1E+10f)
			{
				Debug.LogError("Additive effector positionOffset detected in Full Body IK (extremely large value). Make sure you are not circularily adding to effector positionOffset each frame.", this.bone);
			}
			if (float.IsInfinity(this.position.x) || float.IsInfinity(this.position.y) || float.IsInfinity(this.position.z))
			{
				Debug.LogError("Invalid IKEffector.position (contains Infinity)!");
			}
			solver.GetNode(this.chainIndex, this.nodeIndex).offset += this.positionOffset * solver.IKPositionWeight;
			if (this.effectChildNodes && solver.iterations > 0)
			{
				for (int i = 0; i < this.childBones.Length; i++)
				{
					this.localPositions[i] = this.childBones[i].transform.position - this.bone.transform.position;
					solver.GetNode(this.childChainIndexes[i], this.childNodeIndexes[i]).offset += this.positionOffset * solver.IKPositionWeight;
				}
			}
			if (this.usePlaneNodes && this.maintainRelativePositionWeight > 0f)
			{
				this.animatedPlaneRotation = Quaternion.LookRotation(this.planeBone2.position - this.planeBone1.position, this.planeBone3.position - this.planeBone1.position);
			}
			this.firstUpdate = true;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0004242A File Offset: 0x0004062A
		public void OnPostWrite()
		{
			this.positionOffset = Vector3.zero;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00042438 File Offset: 0x00040638
		private Quaternion GetPlaneRotation(IKSolverFullBody solver)
		{
			Vector3 solverPosition = solver.GetNode(this.plane1ChainIndex, this.plane1NodeIndex).solverPosition;
			Vector3 solverPosition2 = solver.GetNode(this.plane2ChainIndex, this.plane2NodeIndex).solverPosition;
			Vector3 solverPosition3 = solver.GetNode(this.plane3ChainIndex, this.plane3NodeIndex).solverPosition;
			Vector3 vector = solverPosition2 - solverPosition;
			Vector3 upwards = solverPosition3 - solverPosition;
			if (vector == Vector3.zero)
			{
				Warning.Log("Make sure you are not placing 2 or more FBBIK effectors of the same chain to exactly the same position.", this.bone, false);
				return Quaternion.identity;
			}
			return Quaternion.LookRotation(vector, upwards);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000424C8 File Offset: 0x000406C8
		public void Update(IKSolverFullBody solver)
		{
			if (this.firstUpdate)
			{
				this.animatedPosition = this.bone.position + solver.GetNode(this.chainIndex, this.nodeIndex).offset;
				this.firstUpdate = false;
			}
			solver.GetNode(this.chainIndex, this.nodeIndex).solverPosition = Vector3.Lerp(this.GetPosition(solver, out this.planeRotationOffset), this.position, this.posW);
			if (!this.effectChildNodes)
			{
				return;
			}
			for (int i = 0; i < this.childBones.Length; i++)
			{
				solver.GetNode(this.childChainIndexes[i], this.childNodeIndexes[i]).solverPosition = Vector3.Lerp(solver.GetNode(this.childChainIndexes[i], this.childNodeIndexes[i]).solverPosition, solver.GetNode(this.chainIndex, this.nodeIndex).solverPosition + this.localPositions[i], this.posW);
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000425CC File Offset: 0x000407CC
		private Vector3 GetPosition(IKSolverFullBody solver, out Quaternion planeRotationOffset)
		{
			planeRotationOffset = Quaternion.identity;
			if (!this.isEndEffector)
			{
				return solver.GetNode(this.chainIndex, this.nodeIndex).solverPosition;
			}
			if (this.maintainRelativePositionWeight <= 0f)
			{
				return this.animatedPosition;
			}
			Vector3 a = this.bone.position;
			Vector3 point = a - this.planeBone1.position;
			planeRotationOffset = this.GetPlaneRotation(solver) * Quaternion.Inverse(this.animatedPlaneRotation);
			a = solver.GetNode(this.plane1ChainIndex, this.plane1NodeIndex).solverPosition + planeRotationOffset * point;
			planeRotationOffset = Quaternion.Lerp(Quaternion.identity, planeRotationOffset, this.maintainRelativePositionWeight);
			return Vector3.Lerp(this.animatedPosition, a + solver.GetNode(this.chainIndex, this.nodeIndex).offset, this.maintainRelativePositionWeight);
		}

		// Token: 0x040007D5 RID: 2005
		public Transform bone;

		// Token: 0x040007D6 RID: 2006
		public Transform target;

		// Token: 0x040007D7 RID: 2007
		[Range(0f, 1f)]
		public float positionWeight;

		// Token: 0x040007D8 RID: 2008
		[Range(0f, 1f)]
		public float rotationWeight;

		// Token: 0x040007D9 RID: 2009
		public Vector3 position = Vector3.zero;

		// Token: 0x040007DA RID: 2010
		public Quaternion rotation = Quaternion.identity;

		// Token: 0x040007DB RID: 2011
		public Vector3 positionOffset;

		// Token: 0x040007DC RID: 2012
		[CompilerGenerated]
		private bool <isEndEffector>k__BackingField;

		// Token: 0x040007DD RID: 2013
		public bool effectChildNodes = true;

		// Token: 0x040007DE RID: 2014
		[Range(0f, 1f)]
		public float maintainRelativePositionWeight;

		// Token: 0x040007DF RID: 2015
		public Transform[] childBones = new Transform[0];

		// Token: 0x040007E0 RID: 2016
		public Transform planeBone1;

		// Token: 0x040007E1 RID: 2017
		public Transform planeBone2;

		// Token: 0x040007E2 RID: 2018
		public Transform planeBone3;

		// Token: 0x040007E3 RID: 2019
		public Quaternion planeRotationOffset = Quaternion.identity;

		// Token: 0x040007E4 RID: 2020
		private float posW;

		// Token: 0x040007E5 RID: 2021
		private float rotW;

		// Token: 0x040007E6 RID: 2022
		private Vector3[] localPositions = new Vector3[0];

		// Token: 0x040007E7 RID: 2023
		private bool usePlaneNodes;

		// Token: 0x040007E8 RID: 2024
		private Quaternion animatedPlaneRotation = Quaternion.identity;

		// Token: 0x040007E9 RID: 2025
		private Vector3 animatedPosition;

		// Token: 0x040007EA RID: 2026
		private bool firstUpdate;

		// Token: 0x040007EB RID: 2027
		private int chainIndex = -1;

		// Token: 0x040007EC RID: 2028
		private int nodeIndex = -1;

		// Token: 0x040007ED RID: 2029
		private int plane1ChainIndex;

		// Token: 0x040007EE RID: 2030
		private int plane1NodeIndex = -1;

		// Token: 0x040007EF RID: 2031
		private int plane2ChainIndex = -1;

		// Token: 0x040007F0 RID: 2032
		private int plane2NodeIndex = -1;

		// Token: 0x040007F1 RID: 2033
		private int plane3ChainIndex = -1;

		// Token: 0x040007F2 RID: 2034
		private int plane3NodeIndex = -1;

		// Token: 0x040007F3 RID: 2035
		private int[] childChainIndexes = new int[0];

		// Token: 0x040007F4 RID: 2036
		private int[] childNodeIndexes = new int[0];
	}
}
