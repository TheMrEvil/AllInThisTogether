using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E7 RID: 231
	[Serializable]
	public class FBIKChain
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x00040680 File Offset: 0x0003E880
		public FBIKChain()
		{
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000406E8 File Offset: 0x0003E8E8
		public FBIKChain(float pin, float pull, params Transform[] nodeTransforms)
		{
			this.pin = pin;
			this.pull = pull;
			this.SetNodes(nodeTransforms);
			this.children = new int[0];
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00040770 File Offset: 0x0003E970
		public void SetNodes(params Transform[] boneTransforms)
		{
			this.nodes = new IKSolver.Node[boneTransforms.Length];
			for (int i = 0; i < boneTransforms.Length; i++)
			{
				this.nodes[i] = new IKSolver.Node(boneTransforms[i]);
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000407AC File Offset: 0x0003E9AC
		public int GetNodeIndex(Transform boneTransform)
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i].transform == boneTransform)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x000407E4 File Offset: 0x0003E9E4
		public bool IsValid(ref string message)
		{
			if (this.nodes.Length == 0)
			{
				message = "FBIK chain contains no nodes.";
				return false;
			}
			IKSolver.Node[] array = this.nodes;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].transform == null)
				{
					message = "Node transform is null in FBIK chain.";
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00040834 File Offset: 0x0003EA34
		public void Initiate(IKSolverFullBody solver)
		{
			this.initiated = false;
			foreach (IKSolver.Node node in this.nodes)
			{
				node.solverPosition = node.transform.position;
			}
			this.CalculateBoneLengths(solver);
			FBIKChain.ChildConstraint[] array2 = this.childConstraints;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Initiate(solver);
			}
			if (this.nodes.Length == 3)
			{
				this.bendConstraint.SetBones(this.nodes[0].transform, this.nodes[1].transform, this.nodes[2].transform);
				this.bendConstraint.Initiate(solver);
			}
			this.crossFades = new float[this.children.Length];
			this.initiated = true;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000408F8 File Offset: 0x0003EAF8
		public void ReadPose(IKSolverFullBody solver, bool fullBody)
		{
			if (!this.initiated)
			{
				return;
			}
			for (int i = 0; i < this.nodes.Length; i++)
			{
				this.nodes[i].solverPosition = this.nodes[i].transform.position + this.nodes[i].offset;
			}
			this.CalculateBoneLengths(solver);
			if (fullBody)
			{
				for (int j = 0; j < this.childConstraints.Length; j++)
				{
					this.childConstraints[j].OnPreSolve(solver);
				}
				if (this.children.Length != 0)
				{
					float num = this.nodes[this.nodes.Length - 1].effectorPositionWeight;
					for (int k = 0; k < this.children.Length; k++)
					{
						num += solver.chain[this.children[k]].nodes[0].effectorPositionWeight * solver.chain[this.children[k]].pull;
					}
					num = Mathf.Clamp(num, 1f, float.PositiveInfinity);
					for (int l = 0; l < this.children.Length; l++)
					{
						this.crossFades[l] = solver.chain[this.children[l]].nodes[0].effectorPositionWeight * solver.chain[this.children[l]].pull / num;
					}
				}
				this.pullParentSum = 0f;
				for (int m = 0; m < this.children.Length; m++)
				{
					this.pullParentSum += solver.chain[this.children[m]].pull;
				}
				this.pullParentSum = Mathf.Clamp(this.pullParentSum, 1f, float.PositiveInfinity);
				if (this.nodes.Length == 3)
				{
					this.reachForce = this.reach * Mathf.Clamp(this.nodes[2].effectorPositionWeight, 0f, 1f);
				}
				else
				{
					this.reachForce = 0f;
				}
				if (this.push > 0f && this.nodes.Length > 1)
				{
					this.distance = Vector3.Distance(this.nodes[0].transform.position, this.nodes[this.nodes.Length - 1].transform.position);
				}
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00040B40 File Offset: 0x0003ED40
		private void CalculateBoneLengths(IKSolverFullBody solver)
		{
			this.length = 0f;
			for (int i = 0; i < this.nodes.Length - 1; i++)
			{
				this.nodes[i].length = Vector3.Distance(this.nodes[i].transform.position, this.nodes[i + 1].transform.position);
				this.length += this.nodes[i].length;
				if (this.nodes[i].length == 0f)
				{
					Warning.Log(string.Concat(new string[]
					{
						"Bone ",
						this.nodes[i].transform.name,
						" - ",
						this.nodes[i + 1].transform.name,
						" length is zero, can not solve."
					}), this.nodes[i].transform, false);
					return;
				}
			}
			for (int j = 0; j < this.children.Length; j++)
			{
				solver.chain[this.children[j]].rootLength = (solver.chain[this.children[j]].nodes[0].transform.position - this.nodes[this.nodes.Length - 1].transform.position).magnitude;
				if (solver.chain[this.children[j]].rootLength == 0f)
				{
					return;
				}
			}
			if (this.nodes.Length == 3)
			{
				this.sqrMag1 = this.nodes[0].length * this.nodes[0].length;
				this.sqrMag2 = this.nodes[1].length * this.nodes[1].length;
				this.sqrMagDif = this.sqrMag1 - this.sqrMag2;
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00040D2C File Offset: 0x0003EF2C
		public void Reach(IKSolverFullBody solver)
		{
			if (!this.initiated)
			{
				return;
			}
			for (int i = 0; i < this.children.Length; i++)
			{
				solver.chain[this.children[i]].Reach(solver);
			}
			if (this.reachForce <= 0f)
			{
				return;
			}
			Vector3 vector = this.nodes[2].solverPosition - this.nodes[0].solverPosition;
			if (vector == Vector3.zero)
			{
				return;
			}
			float magnitude = vector.magnitude;
			Vector3 a = vector / magnitude * this.length;
			float num = Mathf.Clamp(magnitude / this.length, 1f - this.reachForce, 1f + this.reachForce) - 1f;
			num = Mathf.Clamp(num + this.reachForce, -1f, 1f);
			FBIKChain.Smoothing smoothing = this.reachSmoothing;
			if (smoothing != FBIKChain.Smoothing.Exponential)
			{
				if (smoothing == FBIKChain.Smoothing.Cubic)
				{
					num *= num * num;
				}
			}
			else
			{
				num *= num;
			}
			Vector3 vector2 = a * Mathf.Clamp(num, 0f, magnitude);
			this.nodes[0].solverPosition += vector2 * (1f - this.nodes[0].effectorPositionWeight);
			this.nodes[2].solverPosition += vector2;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00040E8C File Offset: 0x0003F08C
		public Vector3 Push(IKSolverFullBody solver)
		{
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < this.children.Length; i++)
			{
				vector += solver.chain[this.children[i]].Push(solver) * solver.chain[this.children[i]].pushParent;
			}
			this.nodes[this.nodes.Length - 1].solverPosition += vector;
			if (this.nodes.Length < 2)
			{
				return Vector3.zero;
			}
			if (this.push <= 0f)
			{
				return Vector3.zero;
			}
			Vector3 a = this.nodes[2].solverPosition - this.nodes[0].solverPosition;
			float magnitude = a.magnitude;
			if (magnitude == 0f)
			{
				return Vector3.zero;
			}
			float num = 1f - magnitude / this.distance;
			if (num <= 0f)
			{
				return Vector3.zero;
			}
			FBIKChain.Smoothing smoothing = this.pushSmoothing;
			if (smoothing != FBIKChain.Smoothing.Exponential)
			{
				if (smoothing == FBIKChain.Smoothing.Cubic)
				{
					num *= num * num;
				}
			}
			else
			{
				num *= num;
			}
			Vector3 vector2 = -a * num * this.push;
			this.nodes[0].solverPosition += vector2;
			return vector2;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00040FDC File Offset: 0x0003F1DC
		public void SolveTrigonometric(IKSolverFullBody solver, bool calculateBendDirection = false)
		{
			if (!this.initiated)
			{
				return;
			}
			for (int i = 0; i < this.children.Length; i++)
			{
				solver.chain[this.children[i]].SolveTrigonometric(solver, calculateBendDirection);
			}
			if (this.nodes.Length != 3)
			{
				return;
			}
			Vector3 a = this.nodes[2].solverPosition - this.nodes[0].solverPosition;
			float magnitude = a.magnitude;
			if (magnitude == 0f)
			{
				return;
			}
			float num = Mathf.Clamp(magnitude, 0f, this.length * 0.99999f);
			Vector3 direction = a / magnitude * num;
			Vector3 bendDirection = (calculateBendDirection && this.bendConstraint.initiated) ? this.bendConstraint.GetDir(solver) : (this.nodes[1].solverPosition - this.nodes[0].solverPosition);
			Vector3 dirToBendPoint = this.GetDirToBendPoint(direction, bendDirection, num);
			this.nodes[1].solverPosition = this.nodes[0].solverPosition + dirToBendPoint;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000410F0 File Offset: 0x0003F2F0
		public void Stage1(IKSolverFullBody solver)
		{
			for (int i = 0; i < this.children.Length; i++)
			{
				solver.chain[this.children[i]].Stage1(solver);
			}
			if (this.children.Length == 0)
			{
				this.ForwardReach(this.nodes[this.nodes.Length - 1].solverPosition);
				return;
			}
			Vector3 a = this.nodes[this.nodes.Length - 1].solverPosition;
			this.SolveChildConstraints(solver);
			for (int j = 0; j < this.children.Length; j++)
			{
				Vector3 a2 = solver.chain[this.children[j]].nodes[0].solverPosition;
				if (solver.chain[this.children[j]].rootLength > 0f)
				{
					a2 = this.SolveFABRIKJoint(this.nodes[this.nodes.Length - 1].solverPosition, solver.chain[this.children[j]].nodes[0].solverPosition, solver.chain[this.children[j]].rootLength);
				}
				if (this.pullParentSum > 0f)
				{
					a += (a2 - this.nodes[this.nodes.Length - 1].solverPosition) * (solver.chain[this.children[j]].pull / this.pullParentSum);
				}
			}
			this.ForwardReach(Vector3.Lerp(a, this.nodes[this.nodes.Length - 1].solverPosition, this.pin));
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00041280 File Offset: 0x0003F480
		public void Stage2(IKSolverFullBody solver, Vector3 position)
		{
			this.BackwardReach(position);
			int num = Mathf.Clamp(solver.iterations, 2, 4);
			if (this.childConstraints.Length != 0)
			{
				for (int i = 0; i < num; i++)
				{
					this.SolveConstraintSystems(solver);
				}
			}
			for (int j = 0; j < this.children.Length; j++)
			{
				solver.chain[this.children[j]].Stage2(solver, this.nodes[this.nodes.Length - 1].solverPosition);
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000412FC File Offset: 0x0003F4FC
		public void SolveConstraintSystems(IKSolverFullBody solver)
		{
			this.SolveChildConstraints(solver);
			for (int i = 0; i < this.children.Length; i++)
			{
				this.SolveLinearConstraint(this.nodes[this.nodes.Length - 1], solver.chain[this.children[i]].nodes[0], this.crossFades[i], solver.chain[this.children[i]].rootLength);
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0004136C File Offset: 0x0003F56C
		private Vector3 SolveFABRIKJoint(Vector3 pos1, Vector3 pos2, float length)
		{
			return pos2 + (pos1 - pos2).normalized * length;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00041394 File Offset: 0x0003F594
		protected Vector3 GetDirToBendPoint(Vector3 direction, Vector3 bendDirection, float directionMagnitude)
		{
			float num = (directionMagnitude * directionMagnitude + this.sqrMagDif) / 2f / directionMagnitude;
			float y = (float)Math.Sqrt((double)Mathf.Clamp(this.sqrMag1 - num * num, 0f, float.PositiveInfinity));
			if (direction == Vector3.zero)
			{
				return Vector3.zero;
			}
			return Quaternion.LookRotation(direction, bendDirection) * new Vector3(0f, y, num);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00041400 File Offset: 0x0003F600
		private void SolveChildConstraints(IKSolverFullBody solver)
		{
			for (int i = 0; i < this.childConstraints.Length; i++)
			{
				this.childConstraints[i].Solve(solver);
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00041430 File Offset: 0x0003F630
		private void SolveLinearConstraint(IKSolver.Node node1, IKSolver.Node node2, float crossFade, float distance)
		{
			Vector3 a = node2.solverPosition - node1.solverPosition;
			float magnitude = a.magnitude;
			if (distance == magnitude)
			{
				return;
			}
			if (magnitude == 0f)
			{
				return;
			}
			Vector3 a2 = a * (1f - distance / magnitude);
			node1.solverPosition += a2 * crossFade;
			node2.solverPosition -= a2 * (1f - crossFade);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000414B0 File Offset: 0x0003F6B0
		public void ForwardReach(Vector3 position)
		{
			this.nodes[this.nodes.Length - 1].solverPosition = position;
			for (int i = this.nodes.Length - 2; i > -1; i--)
			{
				this.nodes[i].solverPosition = this.SolveFABRIKJoint(this.nodes[i].solverPosition, this.nodes[i + 1].solverPosition, this.nodes[i].length);
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00041528 File Offset: 0x0003F728
		private void BackwardReach(Vector3 position)
		{
			if (this.rootLength > 0f)
			{
				position = this.SolveFABRIKJoint(this.nodes[0].solverPosition, position, this.rootLength);
			}
			this.nodes[0].solverPosition = position;
			for (int i = 1; i < this.nodes.Length; i++)
			{
				this.nodes[i].solverPosition = this.SolveFABRIKJoint(this.nodes[i].solverPosition, this.nodes[i - 1].solverPosition, this.nodes[i - 1].length);
			}
		}

		// Token: 0x040007AC RID: 1964
		[Range(0f, 1f)]
		public float pin;

		// Token: 0x040007AD RID: 1965
		[Range(0f, 1f)]
		public float pull = 1f;

		// Token: 0x040007AE RID: 1966
		[Range(0f, 1f)]
		public float push;

		// Token: 0x040007AF RID: 1967
		[Range(-1f, 1f)]
		public float pushParent;

		// Token: 0x040007B0 RID: 1968
		[Range(0f, 1f)]
		public float reach = 0.1f;

		// Token: 0x040007B1 RID: 1969
		public FBIKChain.Smoothing reachSmoothing = FBIKChain.Smoothing.Exponential;

		// Token: 0x040007B2 RID: 1970
		public FBIKChain.Smoothing pushSmoothing = FBIKChain.Smoothing.Exponential;

		// Token: 0x040007B3 RID: 1971
		public IKSolver.Node[] nodes = new IKSolver.Node[0];

		// Token: 0x040007B4 RID: 1972
		public int[] children = new int[0];

		// Token: 0x040007B5 RID: 1973
		public FBIKChain.ChildConstraint[] childConstraints = new FBIKChain.ChildConstraint[0];

		// Token: 0x040007B6 RID: 1974
		public IKConstraintBend bendConstraint = new IKConstraintBend();

		// Token: 0x040007B7 RID: 1975
		private float rootLength;

		// Token: 0x040007B8 RID: 1976
		private bool initiated;

		// Token: 0x040007B9 RID: 1977
		private float length;

		// Token: 0x040007BA RID: 1978
		private float distance;

		// Token: 0x040007BB RID: 1979
		private IKSolver.Point p;

		// Token: 0x040007BC RID: 1980
		private float reachForce;

		// Token: 0x040007BD RID: 1981
		private float pullParentSum;

		// Token: 0x040007BE RID: 1982
		private float[] crossFades;

		// Token: 0x040007BF RID: 1983
		private float sqrMag1;

		// Token: 0x040007C0 RID: 1984
		private float sqrMag2;

		// Token: 0x040007C1 RID: 1985
		private float sqrMagDif;

		// Token: 0x040007C2 RID: 1986
		private const float maxLimbLength = 0.99999f;

		// Token: 0x020001F0 RID: 496
		[Serializable]
		public class ChildConstraint
		{
			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06001049 RID: 4169 RVA: 0x00065BCF File Offset: 0x00063DCF
			// (set) Token: 0x0600104A RID: 4170 RVA: 0x00065BD7 File Offset: 0x00063DD7
			public float nominalDistance
			{
				[CompilerGenerated]
				get
				{
					return this.<nominalDistance>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<nominalDistance>k__BackingField = value;
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x0600104B RID: 4171 RVA: 0x00065BE0 File Offset: 0x00063DE0
			// (set) Token: 0x0600104C RID: 4172 RVA: 0x00065BE8 File Offset: 0x00063DE8
			public bool isRigid
			{
				[CompilerGenerated]
				get
				{
					return this.<isRigid>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<isRigid>k__BackingField = value;
				}
			}

			// Token: 0x0600104D RID: 4173 RVA: 0x00065BF1 File Offset: 0x00063DF1
			public ChildConstraint(Transform bone1, Transform bone2, float pushElasticity = 0f, float pullElasticity = 0f)
			{
				this.bone1 = bone1;
				this.bone2 = bone2;
				this.pushElasticity = pushElasticity;
				this.pullElasticity = pullElasticity;
			}

			// Token: 0x0600104E RID: 4174 RVA: 0x00065C16 File Offset: 0x00063E16
			public void Initiate(IKSolverFullBody solver)
			{
				this.chain1Index = solver.GetChainIndex(this.bone1);
				this.chain2Index = solver.GetChainIndex(this.bone2);
				this.OnPreSolve(solver);
			}

			// Token: 0x0600104F RID: 4175 RVA: 0x00065C44 File Offset: 0x00063E44
			public void OnPreSolve(IKSolverFullBody solver)
			{
				this.nominalDistance = Vector3.Distance(solver.chain[this.chain1Index].nodes[0].transform.position, solver.chain[this.chain2Index].nodes[0].transform.position);
				this.isRigid = (this.pushElasticity <= 0f && this.pullElasticity <= 0f);
				if (this.isRigid)
				{
					float num = solver.chain[this.chain1Index].pull - solver.chain[this.chain2Index].pull;
					this.crossFade = 1f - (0.5f + num * 0.5f);
				}
				else
				{
					this.crossFade = 0.5f;
				}
				this.inverseCrossFade = 1f - this.crossFade;
			}

			// Token: 0x06001050 RID: 4176 RVA: 0x00065D24 File Offset: 0x00063F24
			public void Solve(IKSolverFullBody solver)
			{
				if (this.pushElasticity >= 1f && this.pullElasticity >= 1f)
				{
					return;
				}
				Vector3 a = solver.chain[this.chain2Index].nodes[0].solverPosition - solver.chain[this.chain1Index].nodes[0].solverPosition;
				float magnitude = a.magnitude;
				if (magnitude == this.nominalDistance)
				{
					return;
				}
				if (magnitude == 0f)
				{
					return;
				}
				float num = 1f;
				if (!this.isRigid)
				{
					float num2 = (magnitude > this.nominalDistance) ? this.pullElasticity : this.pushElasticity;
					num = 1f - num2;
				}
				num *= 1f - this.nominalDistance / magnitude;
				Vector3 a2 = a * num;
				solver.chain[this.chain1Index].nodes[0].solverPosition += a2 * this.crossFade;
				solver.chain[this.chain2Index].nodes[0].solverPosition -= a2 * this.inverseCrossFade;
			}

			// Token: 0x04000EAF RID: 3759
			public float pushElasticity;

			// Token: 0x04000EB0 RID: 3760
			public float pullElasticity;

			// Token: 0x04000EB1 RID: 3761
			[SerializeField]
			private Transform bone1;

			// Token: 0x04000EB2 RID: 3762
			[SerializeField]
			private Transform bone2;

			// Token: 0x04000EB3 RID: 3763
			[CompilerGenerated]
			private float <nominalDistance>k__BackingField;

			// Token: 0x04000EB4 RID: 3764
			[CompilerGenerated]
			private bool <isRigid>k__BackingField;

			// Token: 0x04000EB5 RID: 3765
			private float crossFade;

			// Token: 0x04000EB6 RID: 3766
			private float inverseCrossFade;

			// Token: 0x04000EB7 RID: 3767
			private int chain1Index;

			// Token: 0x04000EB8 RID: 3768
			private int chain2Index;
		}

		// Token: 0x020001F1 RID: 497
		[Serializable]
		public enum Smoothing
		{
			// Token: 0x04000EBA RID: 3770
			None,
			// Token: 0x04000EBB RID: 3771
			Exponential,
			// Token: 0x04000EBC RID: 3772
			Cubic
		}
	}
}
