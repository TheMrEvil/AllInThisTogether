using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class IKSolverFABRIK : IKSolverHeuristic
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x00044BEE File Offset: 0x00042DEE
		public void SolveForward(Vector3 position)
		{
			if (!base.initiated)
			{
				if (!Warning.logged)
				{
					base.LogWarning("Trying to solve uninitiated FABRIK chain.");
				}
				return;
			}
			this.OnPreSolve();
			this.ForwardReach(position);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00044C18 File Offset: 0x00042E18
		public void SolveBackward(Vector3 position)
		{
			if (!base.initiated)
			{
				if (!Warning.logged)
				{
					base.LogWarning("Trying to solve uninitiated FABRIK chain.");
				}
				return;
			}
			this.BackwardReach(position);
			this.OnPostSolve();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00044C42 File Offset: 0x00042E42
		public override Vector3 GetIKPosition()
		{
			if (this.target != null)
			{
				return this.target.position;
			}
			return this.IKPosition;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00044C64 File Offset: 0x00042E64
		protected override void OnInitiate()
		{
			if (this.firstInitiation || !Application.isPlaying)
			{
				this.IKPosition = this.bones[this.bones.Length - 1].transform.position;
			}
			for (int i = 0; i < this.bones.Length; i++)
			{
				this.bones[i].solverPosition = this.bones[i].transform.position;
				this.bones[i].solverRotation = this.bones[i].transform.rotation;
			}
			this.limitedBones = new bool[this.bones.Length];
			this.solverLocalPositions = new Vector3[this.bones.Length];
			base.InitiateBones();
			for (int j = 0; j < this.bones.Length; j++)
			{
				this.solverLocalPositions[j] = Quaternion.Inverse(this.GetParentSolverRotation(j)) * (this.bones[j].transform.position - this.GetParentSolverPosition(j));
			}
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00044D6C File Offset: 0x00042F6C
		protected override void OnUpdate()
		{
			if (this.IKPositionWeight <= 0f)
			{
				return;
			}
			this.IKPositionWeight = Mathf.Clamp(this.IKPositionWeight, 0f, 1f);
			this.OnPreSolve();
			if (this.target != null)
			{
				this.IKPosition = this.target.position;
			}
			if (this.XY)
			{
				this.IKPosition.z = this.bones[0].transform.position.z;
			}
			Vector3 vector = (this.maxIterations > 1) ? base.GetSingularityOffset() : Vector3.zero;
			int num = 0;
			while (num < this.maxIterations && (!(vector == Vector3.zero) || num < 1 || this.tolerance <= 0f || base.positionOffset >= this.tolerance * this.tolerance))
			{
				this.lastLocalDirection = this.localDirection;
				if (this.OnPreIteration != null)
				{
					this.OnPreIteration(num);
				}
				this.Solve(this.IKPosition + ((num == 0) ? vector : Vector3.zero));
				num++;
			}
			this.OnPostSolve();
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00044E8E File Offset: 0x0004308E
		protected override bool boneLengthCanBeZero
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00044E94 File Offset: 0x00043094
		private Vector3 SolveJoint(Vector3 pos1, Vector3 pos2, float length)
		{
			if (this.XY)
			{
				pos1.z = pos2.z;
			}
			return pos2 + (pos1 - pos2).normalized * length;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00044ED4 File Offset: 0x000430D4
		private void OnPreSolve()
		{
			this.chainLength = 0f;
			for (int i = 0; i < this.bones.Length; i++)
			{
				this.bones[i].solverPosition = this.bones[i].transform.position;
				this.bones[i].solverRotation = this.bones[i].transform.rotation;
				if (i < this.bones.Length - 1)
				{
					this.bones[i].length = (this.bones[i].transform.position - this.bones[i + 1].transform.position).magnitude;
					this.bones[i].axis = Quaternion.Inverse(this.bones[i].transform.rotation) * (this.bones[i + 1].transform.position - this.bones[i].transform.position);
					this.chainLength += this.bones[i].length;
				}
				if (this.useRotationLimits)
				{
					this.solverLocalPositions[i] = Quaternion.Inverse(this.GetParentSolverRotation(i)) * (this.bones[i].transform.position - this.GetParentSolverPosition(i));
				}
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00045044 File Offset: 0x00043244
		private void OnPostSolve()
		{
			if (!this.useRotationLimits)
			{
				this.MapToSolverPositions();
			}
			else
			{
				this.MapToSolverPositionsLimited();
			}
			this.lastLocalDirection = this.localDirection;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00045068 File Offset: 0x00043268
		private void Solve(Vector3 targetPosition)
		{
			this.ForwardReach(targetPosition);
			this.BackwardReach(this.bones[0].transform.position);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0004508C File Offset: 0x0004328C
		private void ForwardReach(Vector3 position)
		{
			this.bones[this.bones.Length - 1].solverPosition = Vector3.Lerp(this.bones[this.bones.Length - 1].solverPosition, position, this.IKPositionWeight);
			for (int i = 0; i < this.limitedBones.Length; i++)
			{
				this.limitedBones[i] = false;
			}
			for (int j = this.bones.Length - 2; j > -1; j--)
			{
				this.bones[j].solverPosition = this.SolveJoint(this.bones[j].solverPosition, this.bones[j + 1].solverPosition, this.bones[j].length);
				this.LimitForward(j, j + 1);
			}
			this.LimitForward(0, 0);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00045150 File Offset: 0x00043350
		private void SolverMove(int index, Vector3 offset)
		{
			for (int i = index; i < this.bones.Length; i++)
			{
				this.bones[i].solverPosition += offset;
			}
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0004518C File Offset: 0x0004338C
		private void SolverRotate(int index, Quaternion rotation, bool recursive)
		{
			for (int i = index; i < this.bones.Length; i++)
			{
				this.bones[i].solverRotation = rotation * this.bones[i].solverRotation;
				if (!recursive)
				{
					return;
				}
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000451D0 File Offset: 0x000433D0
		private void SolverRotateChildren(int index, Quaternion rotation)
		{
			for (int i = index + 1; i < this.bones.Length; i++)
			{
				this.bones[i].solverRotation = rotation * this.bones[i].solverRotation;
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00045214 File Offset: 0x00043414
		private void SolverMoveChildrenAroundPoint(int index, Quaternion rotation)
		{
			for (int i = index + 1; i < this.bones.Length; i++)
			{
				Vector3 point = this.bones[i].solverPosition - this.bones[index].solverPosition;
				this.bones[i].solverPosition = this.bones[index].solverPosition + rotation * point;
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0004527C File Offset: 0x0004347C
		private Quaternion GetParentSolverRotation(int index)
		{
			if (index > 0)
			{
				return this.bones[index - 1].solverRotation;
			}
			if (this.bones[0].transform.parent == null)
			{
				return Quaternion.identity;
			}
			return this.bones[0].transform.parent.rotation;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000452D4 File Offset: 0x000434D4
		private Vector3 GetParentSolverPosition(int index)
		{
			if (index > 0)
			{
				return this.bones[index - 1].solverPosition;
			}
			if (this.bones[0].transform.parent == null)
			{
				return Vector3.zero;
			}
			return this.bones[0].transform.parent.position;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0004532C File Offset: 0x0004352C
		private Quaternion GetLimitedRotation(int index, Quaternion q, out bool changed)
		{
			changed = false;
			Quaternion parentSolverRotation = this.GetParentSolverRotation(index);
			Quaternion localRotation = Quaternion.Inverse(parentSolverRotation) * q;
			Quaternion limitedLocalRotation = this.bones[index].rotationLimit.GetLimitedLocalRotation(localRotation, out changed);
			if (!changed)
			{
				return q;
			}
			return parentSolverRotation * limitedLocalRotation;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00045374 File Offset: 0x00043574
		private void LimitForward(int rotateBone, int limitBone)
		{
			if (!this.useRotationLimits)
			{
				return;
			}
			if (this.bones[limitBone].rotationLimit == null)
			{
				return;
			}
			Vector3 solverPosition = this.bones[this.bones.Length - 1].solverPosition;
			int num = rotateBone;
			while (num < this.bones.Length - 1 && !this.limitedBones[num])
			{
				Quaternion rotation = Quaternion.FromToRotation(this.bones[num].solverRotation * this.bones[num].axis, this.bones[num + 1].solverPosition - this.bones[num].solverPosition);
				this.SolverRotate(num, rotation, false);
				num++;
			}
			bool flag = false;
			Quaternion limitedRotation = this.GetLimitedRotation(limitBone, this.bones[limitBone].solverRotation, out flag);
			if (flag)
			{
				if (limitBone < this.bones.Length - 1)
				{
					Quaternion rotation2 = QuaTools.FromToRotation(this.bones[limitBone].solverRotation, limitedRotation);
					this.bones[limitBone].solverRotation = limitedRotation;
					this.SolverRotateChildren(limitBone, rotation2);
					this.SolverMoveChildrenAroundPoint(limitBone, rotation2);
					Quaternion rotation3 = Quaternion.FromToRotation(this.bones[this.bones.Length - 1].solverPosition - this.bones[rotateBone].solverPosition, solverPosition - this.bones[rotateBone].solverPosition);
					this.SolverRotate(rotateBone, rotation3, true);
					this.SolverMoveChildrenAroundPoint(rotateBone, rotation3);
					this.SolverMove(rotateBone, solverPosition - this.bones[this.bones.Length - 1].solverPosition);
				}
				else
				{
					this.bones[limitBone].solverRotation = limitedRotation;
				}
			}
			this.limitedBones[limitBone] = true;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0004551C File Offset: 0x0004371C
		private void BackwardReach(Vector3 position)
		{
			if (this.useRotationLimits)
			{
				this.BackwardReachLimited(position);
				return;
			}
			this.BackwardReachUnlimited(position);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00045538 File Offset: 0x00043738
		private void BackwardReachUnlimited(Vector3 position)
		{
			this.bones[0].solverPosition = position;
			for (int i = 1; i < this.bones.Length; i++)
			{
				this.bones[i].solverPosition = this.SolveJoint(this.bones[i].solverPosition, this.bones[i - 1].solverPosition, this.bones[i - 1].length);
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000455A4 File Offset: 0x000437A4
		private void BackwardReachLimited(Vector3 position)
		{
			this.bones[0].solverPosition = position;
			for (int i = 0; i < this.bones.Length - 1; i++)
			{
				Vector3 a = this.SolveJoint(this.bones[i + 1].solverPosition, this.bones[i].solverPosition, this.bones[i].length);
				Quaternion quaternion = Quaternion.FromToRotation(this.bones[i].solverRotation * this.bones[i].axis, a - this.bones[i].solverPosition) * this.bones[i].solverRotation;
				if (this.bones[i].rotationLimit != null)
				{
					bool flag = false;
					quaternion = this.GetLimitedRotation(i, quaternion, out flag);
				}
				Quaternion rotation = QuaTools.FromToRotation(this.bones[i].solverRotation, quaternion);
				this.bones[i].solverRotation = quaternion;
				this.SolverRotateChildren(i, rotation);
				this.bones[i + 1].solverPosition = this.bones[i].solverPosition + this.bones[i].solverRotation * this.solverLocalPositions[i + 1];
			}
			for (int j = 0; j < this.bones.Length; j++)
			{
				this.bones[j].solverRotation = Quaternion.LookRotation(this.bones[j].solverRotation * Vector3.forward, this.bones[j].solverRotation * Vector3.up);
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0004573C File Offset: 0x0004393C
		private void MapToSolverPositions()
		{
			this.bones[0].transform.position = this.bones[0].solverPosition;
			for (int i = 0; i < this.bones.Length - 1; i++)
			{
				if (this.XY)
				{
					this.bones[i].Swing2D(this.bones[i + 1].solverPosition, 1f);
				}
				else
				{
					this.bones[i].Swing(this.bones[i + 1].solverPosition, 1f);
				}
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000457CC File Offset: 0x000439CC
		private void MapToSolverPositionsLimited()
		{
			this.bones[0].transform.position = this.bones[0].solverPosition;
			for (int i = 0; i < this.bones.Length; i++)
			{
				if (i < this.bones.Length - 1)
				{
					this.bones[i].transform.rotation = this.bones[i].solverRotation;
				}
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00045837 File Offset: 0x00043A37
		public IKSolverFABRIK()
		{
		}

		// Token: 0x04000834 RID: 2100
		public IKSolver.IterationDelegate OnPreIteration;

		// Token: 0x04000835 RID: 2101
		private bool[] limitedBones = new bool[0];

		// Token: 0x04000836 RID: 2102
		private Vector3[] solverLocalPositions = new Vector3[0];
	}
}
