using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000F4 RID: 244
	[Serializable]
	public class IKSolverFullBody : IKSolver
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x00045F4C File Offset: 0x0004414C
		public IKEffector GetEffector(Transform t)
		{
			for (int i = 0; i < this.effectors.Length; i++)
			{
				if (this.effectors[i].bone == t)
				{
					return this.effectors[i];
				}
			}
			return null;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00045F8C File Offset: 0x0004418C
		public FBIKChain GetChain(Transform transform)
		{
			int chainIndex = this.GetChainIndex(transform);
			if (chainIndex == -1)
			{
				return null;
			}
			return this.chain[chainIndex];
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00045FB0 File Offset: 0x000441B0
		public int GetChainIndex(Transform transform)
		{
			for (int i = 0; i < this.chain.Length; i++)
			{
				for (int j = 0; j < this.chain[i].nodes.Length; j++)
				{
					if (this.chain[i].nodes[j].transform == transform)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00046009 File Offset: 0x00044209
		public IKSolver.Node GetNode(int chainIndex, int nodeIndex)
		{
			return this.chain[chainIndex].nodes[nodeIndex];
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0004601A File Offset: 0x0004421A
		public void GetChainAndNodeIndexes(Transform transform, out int chainIndex, out int nodeIndex)
		{
			chainIndex = this.GetChainIndex(transform);
			if (chainIndex == -1)
			{
				nodeIndex = -1;
				return;
			}
			nodeIndex = this.chain[chainIndex].GetNodeIndex(transform);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00046040 File Offset: 0x00044240
		public override IKSolver.Point[] GetPoints()
		{
			int num = 0;
			for (int i = 0; i < this.chain.Length; i++)
			{
				num += this.chain[i].nodes.Length;
			}
			IKSolver.Point[] array = new IKSolver.Point[num];
			int num2 = 0;
			for (int j = 0; j < this.chain.Length; j++)
			{
				for (int k = 0; k < this.chain[j].nodes.Length; k++)
				{
					array[num2] = this.chain[j].nodes[k];
					num2++;
				}
			}
			return array;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000460CC File Offset: 0x000442CC
		public override IKSolver.Point GetPoint(Transform transform)
		{
			for (int i = 0; i < this.chain.Length; i++)
			{
				for (int j = 0; j < this.chain[i].nodes.Length; j++)
				{
					if (this.chain[i].nodes[j].transform == transform)
					{
						return this.chain[i].nodes[j];
					}
				}
			}
			return null;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00046134 File Offset: 0x00044334
		public override bool IsValid(ref string message)
		{
			if (this.chain == null)
			{
				message = "FBIK chain is null, can't initiate solver.";
				return false;
			}
			if (this.chain.Length == 0)
			{
				message = "FBIK chain length is 0, can't initiate solver.";
				return false;
			}
			for (int i = 0; i < this.chain.Length; i++)
			{
				if (!this.chain[i].IsValid(ref message))
				{
					return false;
				}
			}
			IKEffector[] array = this.effectors;
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j].IsValid(this, ref message))
				{
					return false;
				}
			}
			if (!this.spineMapping.IsValid(this, ref message))
			{
				return false;
			}
			IKMappingLimb[] array2 = this.limbMappings;
			for (int j = 0; j < array2.Length; j++)
			{
				if (!array2[j].IsValid(this, ref message))
				{
					return false;
				}
			}
			IKMappingBone[] array3 = this.boneMappings;
			for (int j = 0; j < array3.Length; j++)
			{
				if (!array3[j].IsValid(this, ref message))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00046208 File Offset: 0x00044408
		public override void StoreDefaultLocalState()
		{
			this.spineMapping.StoreDefaultLocalState();
			for (int i = 0; i < this.limbMappings.Length; i++)
			{
				this.limbMappings[i].StoreDefaultLocalState();
			}
			for (int j = 0; j < this.boneMappings.Length; j++)
			{
				this.boneMappings[j].StoreDefaultLocalState();
			}
			if (this.OnStoreDefaultLocalState != null)
			{
				this.OnStoreDefaultLocalState();
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00046274 File Offset: 0x00044474
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
			this.spineMapping.FixTransforms();
			for (int i = 0; i < this.limbMappings.Length; i++)
			{
				this.limbMappings[i].FixTransforms();
			}
			for (int j = 0; j < this.boneMappings.Length; j++)
			{
				this.boneMappings[j].FixTransforms();
			}
			if (this.OnFixTransforms != null)
			{
				this.OnFixTransforms();
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000462F8 File Offset: 0x000444F8
		protected override void OnInitiate()
		{
			for (int i = 0; i < this.chain.Length; i++)
			{
				this.chain[i].Initiate(this);
			}
			IKEffector[] array = this.effectors;
			for (int j = 0; j < array.Length; j++)
			{
				array[j].Initiate(this);
			}
			this.spineMapping.Initiate(this);
			IKMappingBone[] array2 = this.boneMappings;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].Initiate(this);
			}
			IKMappingLimb[] array3 = this.limbMappings;
			for (int j = 0; j < array3.Length; j++)
			{
				array3[j].Initiate(this);
			}
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00046390 File Offset: 0x00044590
		protected override void OnUpdate()
		{
			if (this.IKPositionWeight <= 0f)
			{
				for (int i = 0; i < this.effectors.Length; i++)
				{
					this.effectors[i].positionOffset = Vector3.zero;
				}
				return;
			}
			if (this.chain.Length == 0)
			{
				return;
			}
			this.IKPositionWeight = Mathf.Clamp(this.IKPositionWeight, 0f, 1f);
			if (this.OnPreRead != null)
			{
				this.OnPreRead();
			}
			this.ReadPose();
			if (this.OnPreSolve != null)
			{
				this.OnPreSolve();
			}
			this.Solve();
			if (this.OnPostSolve != null)
			{
				this.OnPostSolve();
			}
			this.WritePose();
			for (int j = 0; j < this.effectors.Length; j++)
			{
				this.effectors[j].OnPostWrite();
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00046460 File Offset: 0x00044660
		protected virtual void ReadPose()
		{
			for (int i = 0; i < this.chain.Length; i++)
			{
				if (this.chain[i].bendConstraint.initiated)
				{
					this.chain[i].bendConstraint.LimitBend(this.IKPositionWeight, this.GetEffector(this.chain[i].nodes[2].transform).positionWeight);
				}
			}
			for (int j = 0; j < this.effectors.Length; j++)
			{
				this.effectors[j].ResetOffset(this);
			}
			for (int k = 0; k < this.effectors.Length; k++)
			{
				this.effectors[k].OnPreSolve(this);
			}
			for (int l = 0; l < this.chain.Length; l++)
			{
				this.chain[l].ReadPose(this, this.iterations > 0);
			}
			if (this.iterations > 0)
			{
				this.spineMapping.ReadPose();
				for (int m = 0; m < this.boneMappings.Length; m++)
				{
					this.boneMappings[m].ReadPose();
				}
			}
			for (int n = 0; n < this.limbMappings.Length; n++)
			{
				this.limbMappings[n].ReadPose();
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00046598 File Offset: 0x00044798
		protected virtual void Solve()
		{
			if (this.iterations > 0)
			{
				for (int i = 0; i < (this.FABRIKPass ? this.iterations : 1); i++)
				{
					if (this.OnPreIteration != null)
					{
						this.OnPreIteration(i);
					}
					for (int j = 0; j < this.effectors.Length; j++)
					{
						if (this.effectors[j].isEndEffector)
						{
							this.effectors[j].Update(this);
						}
					}
					if (this.FABRIKPass)
					{
						this.chain[0].Push(this);
						if (this.FABRIKPass)
						{
							this.chain[0].Reach(this);
						}
						for (int k = 0; k < this.effectors.Length; k++)
						{
							if (!this.effectors[k].isEndEffector)
							{
								this.effectors[k].Update(this);
							}
						}
					}
					this.chain[0].SolveTrigonometric(this, false);
					if (this.FABRIKPass)
					{
						this.chain[0].Stage1(this);
						for (int l = 0; l < this.effectors.Length; l++)
						{
							if (!this.effectors[l].isEndEffector)
							{
								this.effectors[l].Update(this);
							}
						}
						this.chain[0].Stage2(this, this.chain[0].nodes[0].solverPosition);
					}
					if (this.OnPostIteration != null)
					{
						this.OnPostIteration(i);
					}
				}
			}
			if (this.OnPreBend != null)
			{
				this.OnPreBend();
			}
			for (int m = 0; m < this.effectors.Length; m++)
			{
				if (this.effectors[m].isEndEffector)
				{
					this.effectors[m].Update(this);
				}
			}
			this.ApplyBendConstraints();
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0004674E File Offset: 0x0004494E
		protected virtual void ApplyBendConstraints()
		{
			this.chain[0].SolveTrigonometric(this, true);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00046760 File Offset: 0x00044960
		protected virtual void WritePose()
		{
			if (this.IKPositionWeight <= 0f)
			{
				return;
			}
			if (this.iterations > 0)
			{
				this.spineMapping.WritePose(this);
				for (int i = 0; i < this.boneMappings.Length; i++)
				{
					this.boneMappings[i].WritePose(this.IKPositionWeight);
				}
			}
			for (int j = 0; j < this.limbMappings.Length; j++)
			{
				this.limbMappings[j].WritePose(this, this.iterations > 0);
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000467E0 File Offset: 0x000449E0
		public IKSolverFullBody()
		{
		}

		// Token: 0x0400083D RID: 2109
		[Range(0f, 10f)]
		public int iterations = 4;

		// Token: 0x0400083E RID: 2110
		public FBIKChain[] chain = new FBIKChain[0];

		// Token: 0x0400083F RID: 2111
		public IKEffector[] effectors = new IKEffector[0];

		// Token: 0x04000840 RID: 2112
		public IKMappingSpine spineMapping = new IKMappingSpine();

		// Token: 0x04000841 RID: 2113
		public IKMappingBone[] boneMappings = new IKMappingBone[0];

		// Token: 0x04000842 RID: 2114
		public IKMappingLimb[] limbMappings = new IKMappingLimb[0];

		// Token: 0x04000843 RID: 2115
		public bool FABRIKPass = true;

		// Token: 0x04000844 RID: 2116
		public IKSolver.UpdateDelegate OnPreRead;

		// Token: 0x04000845 RID: 2117
		public IKSolver.UpdateDelegate OnPreSolve;

		// Token: 0x04000846 RID: 2118
		public IKSolver.IterationDelegate OnPreIteration;

		// Token: 0x04000847 RID: 2119
		public IKSolver.IterationDelegate OnPostIteration;

		// Token: 0x04000848 RID: 2120
		public IKSolver.UpdateDelegate OnPreBend;

		// Token: 0x04000849 RID: 2121
		public IKSolver.UpdateDelegate OnPostSolve;

		// Token: 0x0400084A RID: 2122
		public IKSolver.UpdateDelegate OnStoreDefaultLocalState;

		// Token: 0x0400084B RID: 2123
		public IKSolver.UpdateDelegate OnFixTransforms;
	}
}
