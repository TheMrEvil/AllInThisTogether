using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public class IKSolverFABRIKRoot : IKSolver
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x00045858 File Offset: 0x00043A58
		public override bool IsValid(ref string message)
		{
			if (this.chains.Length == 0)
			{
				message = "IKSolverFABRIKRoot contains no chains.";
				return false;
			}
			FABRIKChain[] array = this.chains;
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsValid(ref message))
				{
					return false;
				}
			}
			for (int j = 0; j < this.chains.Length; j++)
			{
				for (int k = 0; k < this.chains.Length; k++)
				{
					if (j != k && this.chains[j].ik == this.chains[k].ik)
					{
						message = this.chains[j].ik.name + " is represented more than once in IKSolverFABRIKRoot chain.";
						return false;
					}
				}
			}
			for (int l = 0; l < this.chains.Length; l++)
			{
				for (int m = 0; m < this.chains[l].children.Length; m++)
				{
					int num = this.chains[l].children[m];
					if (num < 0)
					{
						message = this.chains[l].ik.name + "IKSolverFABRIKRoot chain at index " + l.ToString() + " has invalid children array. Child index is < 0.";
						return false;
					}
					if (num == l)
					{
						message = this.chains[l].ik.name + "IKSolverFABRIKRoot chain at index " + l.ToString() + " has invalid children array. Child index is referencing to itself.";
						return false;
					}
					if (num >= this.chains.Length)
					{
						message = this.chains[l].ik.name + "IKSolverFABRIKRoot chain at index " + l.ToString() + " has invalid children array. Child index > number of chains";
						return false;
					}
					for (int n = 0; n < this.chains.Length; n++)
					{
						if (num == n)
						{
							for (int num2 = 0; num2 < this.chains[n].children.Length; num2++)
							{
								if (this.chains[n].children[num2] == l)
								{
									message = string.Concat(new string[]
									{
										"Circular parenting. ",
										this.chains[n].ik.name,
										" already has ",
										this.chains[l].ik.name,
										" listed as it's child."
									});
									return false;
								}
							}
						}
					}
					for (int num3 = 0; num3 < this.chains[l].children.Length; num3++)
					{
						if (m != num3 && this.chains[l].children[num3] == num)
						{
							message = "Chain number " + num.ToString() + " is represented more than once in the children of " + this.chains[l].ik.name;
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00045B10 File Offset: 0x00043D10
		public override void StoreDefaultLocalState()
		{
			this.rootDefaultPosition = this.root.localPosition;
			for (int i = 0; i < this.chains.Length; i++)
			{
				this.chains[i].ik.solver.StoreDefaultLocalState();
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00045B58 File Offset: 0x00043D58
		public override void FixTransforms()
		{
			if (!base.initiated)
			{
				return;
			}
			this.root.localPosition = this.rootDefaultPosition;
			for (int i = 0; i < this.chains.Length; i++)
			{
				this.chains[i].ik.solver.FixTransforms();
			}
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00045BAC File Offset: 0x00043DAC
		protected override void OnInitiate()
		{
			for (int i = 0; i < this.chains.Length; i++)
			{
				this.chains[i].Initiate();
			}
			this.isRoot = new bool[this.chains.Length];
			for (int j = 0; j < this.chains.Length; j++)
			{
				this.isRoot[j] = this.IsRoot(j);
			}
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00045C10 File Offset: 0x00043E10
		private bool IsRoot(int index)
		{
			for (int i = 0; i < this.chains.Length; i++)
			{
				for (int j = 0; j < this.chains[i].children.Length; j++)
				{
					if (this.chains[i].children[j] == index)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00045C60 File Offset: 0x00043E60
		protected override void OnUpdate()
		{
			if (this.IKPositionWeight <= 0f && this.zeroWeightApplied)
			{
				return;
			}
			this.IKPositionWeight = Mathf.Clamp(this.IKPositionWeight, 0f, 1f);
			for (int i = 0; i < this.chains.Length; i++)
			{
				this.chains[i].ik.solver.IKPositionWeight = this.IKPositionWeight;
			}
			if (this.IKPositionWeight <= 0f)
			{
				this.zeroWeightApplied = true;
				return;
			}
			this.zeroWeightApplied = false;
			for (int j = 0; j < this.iterations; j++)
			{
				for (int k = 0; k < this.chains.Length; k++)
				{
					if (this.isRoot[k])
					{
						this.chains[k].Stage1(this.chains);
					}
				}
				Vector3 centroid = this.GetCentroid();
				this.root.position = centroid;
				for (int l = 0; l < this.chains.Length; l++)
				{
					if (this.isRoot[l])
					{
						this.chains[l].Stage2(centroid, this.chains);
					}
				}
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00045D78 File Offset: 0x00043F78
		public override IKSolver.Point[] GetPoints()
		{
			IKSolver.Point[] result = new IKSolver.Point[0];
			for (int i = 0; i < this.chains.Length; i++)
			{
				this.AddPointsToArray(ref result, this.chains[i]);
			}
			return result;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00045DB0 File Offset: 0x00043FB0
		public override IKSolver.Point GetPoint(Transform transform)
		{
			for (int i = 0; i < this.chains.Length; i++)
			{
				IKSolver.Point point = this.chains[i].ik.solver.GetPoint(transform);
				if (point != null)
				{
					return point;
				}
			}
			return null;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00045DF4 File Offset: 0x00043FF4
		private void AddPointsToArray(ref IKSolver.Point[] array, FABRIKChain chain)
		{
			IKSolver.Point[] points = chain.ik.solver.GetPoints();
			Array.Resize<IKSolver.Point>(ref array, array.Length + points.Length);
			int num = 0;
			for (int i = array.Length - points.Length; i < array.Length; i++)
			{
				array[i] = points[num];
				num++;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00045E44 File Offset: 0x00044044
		private Vector3 GetCentroid()
		{
			Vector3 vector = this.root.position;
			if (this.rootPin >= 1f)
			{
				return vector;
			}
			float num = 0f;
			for (int i = 0; i < this.chains.Length; i++)
			{
				if (this.isRoot[i])
				{
					num += this.chains[i].pull;
				}
			}
			for (int j = 0; j < this.chains.Length; j++)
			{
				if (this.isRoot[j] && num > 0f)
				{
					vector += (this.chains[j].ik.solver.bones[0].solverPosition - this.root.position) * (this.chains[j].pull / Mathf.Clamp(num, 1f, num));
				}
			}
			return Vector3.Lerp(vector, this.root.position, this.rootPin);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00045F2F File Offset: 0x0004412F
		public IKSolverFABRIKRoot()
		{
		}

		// Token: 0x04000837 RID: 2103
		public int iterations = 4;

		// Token: 0x04000838 RID: 2104
		[Range(0f, 1f)]
		public float rootPin;

		// Token: 0x04000839 RID: 2105
		public FABRIKChain[] chains = new FABRIKChain[0];

		// Token: 0x0400083A RID: 2106
		private bool zeroWeightApplied;

		// Token: 0x0400083B RID: 2107
		private bool[] isRoot;

		// Token: 0x0400083C RID: 2108
		private Vector3 rootDefaultPosition;
	}
}
