using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public class IKConstraintBend
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x000415C0 File Offset: 0x0003F7C0
		public bool IsValid(IKSolverFullBody solver, Warning.Logger logger)
		{
			if (this.bone1 == null || this.bone2 == null || this.bone3 == null)
			{
				if (logger != null)
				{
					logger("Bend Constraint contains a null reference.");
				}
				return false;
			}
			if (solver.GetPoint(this.bone1) == null)
			{
				if (logger != null)
				{
					logger("Bend Constraint is referencing to a bone '" + this.bone1.name + "' that does not excist in the Node Chain.");
				}
				return false;
			}
			if (solver.GetPoint(this.bone2) == null)
			{
				if (logger != null)
				{
					logger("Bend Constraint is referencing to a bone '" + this.bone2.name + "' that does not excist in the Node Chain.");
				}
				return false;
			}
			if (solver.GetPoint(this.bone3) == null)
			{
				if (logger != null)
				{
					logger("Bend Constraint is referencing to a bone '" + this.bone3.name + "' that does not excist in the Node Chain.");
				}
				return false;
			}
			return true;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x000416A1 File Offset: 0x0003F8A1
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x000416A9 File Offset: 0x0003F8A9
		public bool initiated
		{
			[CompilerGenerated]
			get
			{
				return this.<initiated>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<initiated>k__BackingField = value;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000416B2 File Offset: 0x0003F8B2
		public IKConstraintBend()
		{
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000416D0 File Offset: 0x0003F8D0
		public IKConstraintBend(Transform bone1, Transform bone2, Transform bone3)
		{
			this.SetBones(bone1, bone2, bone3);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000416F7 File Offset: 0x0003F8F7
		public void SetBones(Transform bone1, Transform bone2, Transform bone3)
		{
			this.bone1 = bone1;
			this.bone2 = bone2;
			this.bone3 = bone3;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00041710 File Offset: 0x0003F910
		public void Initiate(IKSolverFullBody solver)
		{
			solver.GetChainAndNodeIndexes(this.bone1, out this.chainIndex1, out this.nodeIndex1);
			solver.GetChainAndNodeIndexes(this.bone2, out this.chainIndex2, out this.nodeIndex2);
			solver.GetChainAndNodeIndexes(this.bone3, out this.chainIndex3, out this.nodeIndex3);
			this.direction = this.OrthoToBone1(solver, this.OrthoToLimb(solver, this.bone2.position - this.bone1.position));
			if (!this.limbOrientationsSet)
			{
				this.defaultLocalDirection = Quaternion.Inverse(this.bone1.rotation) * this.direction;
				Vector3 point = Vector3.Cross((this.bone3.position - this.bone1.position).normalized, this.direction);
				this.defaultChildDirection = Quaternion.Inverse(this.bone3.rotation) * point;
			}
			this.initiated = true;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00041810 File Offset: 0x0003FA10
		public void SetLimbOrientation(Vector3 upper, Vector3 lower, Vector3 last)
		{
			if (upper == Vector3.zero)
			{
				Debug.LogError("Attempting to set limb orientation to Vector3.zero axis");
			}
			if (lower == Vector3.zero)
			{
				Debug.LogError("Attempting to set limb orientation to Vector3.zero axis");
			}
			if (last == Vector3.zero)
			{
				Debug.LogError("Attempting to set limb orientation to Vector3.zero axis");
			}
			this.defaultLocalDirection = upper.normalized;
			this.defaultChildDirection = last.normalized;
			this.limbOrientationsSet = true;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00041884 File Offset: 0x0003FA84
		public void LimitBend(float solverWeight, float positionWeight)
		{
			if (!this.initiated)
			{
				return;
			}
			Vector3 vector = this.bone1.rotation * -this.defaultLocalDirection;
			Vector3 fromDirection = this.bone3.position - this.bone2.position;
			bool flag = false;
			Vector3 toDirection = V3Tools.ClampDirection(fromDirection, vector, this.clampF * solverWeight, 0, out flag);
			Quaternion rotation = this.bone3.rotation;
			if (flag)
			{
				Quaternion lhs = Quaternion.FromToRotation(fromDirection, toDirection);
				this.bone2.rotation = lhs * this.bone2.rotation;
			}
			if (positionWeight > 0f)
			{
				Vector3 vector2 = this.bone2.position - this.bone1.position;
				Vector3 fromDirection2 = this.bone3.position - this.bone2.position;
				Vector3.OrthoNormalize(ref vector2, ref fromDirection2);
				Quaternion lhs2 = Quaternion.FromToRotation(fromDirection2, vector);
				this.bone2.rotation = Quaternion.Lerp(this.bone2.rotation, lhs2 * this.bone2.rotation, positionWeight * solverWeight);
			}
			if (flag || positionWeight > 0f)
			{
				this.bone3.rotation = rotation;
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000419BC File Offset: 0x0003FBBC
		public Vector3 GetDir(IKSolverFullBody solver)
		{
			if (!this.initiated)
			{
				return Vector3.zero;
			}
			float num = this.weight * solver.IKPositionWeight;
			if (this.bendGoal != null)
			{
				Vector3 lhs = this.bendGoal.position - solver.GetNode(this.chainIndex1, this.nodeIndex1).solverPosition;
				if (lhs != Vector3.zero)
				{
					this.direction = lhs;
				}
			}
			if (num >= 1f)
			{
				return this.direction.normalized;
			}
			Vector3 vector = solver.GetNode(this.chainIndex3, this.nodeIndex3).solverPosition - solver.GetNode(this.chainIndex1, this.nodeIndex1).solverPosition;
			Vector3 vector2 = Quaternion.FromToRotation(this.bone3.position - this.bone1.position, vector) * (this.bone2.position - this.bone1.position);
			if (solver.GetNode(this.chainIndex3, this.nodeIndex3).effectorRotationWeight > 0f)
			{
				Vector3 b = -Vector3.Cross(vector, solver.GetNode(this.chainIndex3, this.nodeIndex3).solverRotation * this.defaultChildDirection);
				vector2 = Vector3.Lerp(vector2, b, solver.GetNode(this.chainIndex3, this.nodeIndex3).effectorRotationWeight);
			}
			if (this.rotationOffset != Quaternion.identity)
			{
				vector2 = Quaternion.FromToRotation(this.rotationOffset * vector, vector) * this.rotationOffset * vector2;
			}
			if (num <= 0f)
			{
				return vector2;
			}
			return Vector3.Lerp(vector2, this.direction.normalized, num);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00041B78 File Offset: 0x0003FD78
		private Vector3 OrthoToLimb(IKSolverFullBody solver, Vector3 tangent)
		{
			Vector3 vector = solver.GetNode(this.chainIndex3, this.nodeIndex3).solverPosition - solver.GetNode(this.chainIndex1, this.nodeIndex1).solverPosition;
			Vector3.OrthoNormalize(ref vector, ref tangent);
			return tangent;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00041BC4 File Offset: 0x0003FDC4
		private Vector3 OrthoToBone1(IKSolverFullBody solver, Vector3 tangent)
		{
			Vector3 vector = solver.GetNode(this.chainIndex2, this.nodeIndex2).solverPosition - solver.GetNode(this.chainIndex1, this.nodeIndex1).solverPosition;
			Vector3.OrthoNormalize(ref vector, ref tangent);
			return tangent;
		}

		// Token: 0x040007C3 RID: 1987
		public Transform bone1;

		// Token: 0x040007C4 RID: 1988
		public Transform bone2;

		// Token: 0x040007C5 RID: 1989
		public Transform bone3;

		// Token: 0x040007C6 RID: 1990
		public Transform bendGoal;

		// Token: 0x040007C7 RID: 1991
		public Vector3 direction = Vector3.right;

		// Token: 0x040007C8 RID: 1992
		public Quaternion rotationOffset;

		// Token: 0x040007C9 RID: 1993
		[Range(0f, 1f)]
		public float weight;

		// Token: 0x040007CA RID: 1994
		public Vector3 defaultLocalDirection;

		// Token: 0x040007CB RID: 1995
		public Vector3 defaultChildDirection;

		// Token: 0x040007CC RID: 1996
		[NonSerialized]
		public float clampF = 0.505f;

		// Token: 0x040007CD RID: 1997
		private int chainIndex1;

		// Token: 0x040007CE RID: 1998
		private int nodeIndex1;

		// Token: 0x040007CF RID: 1999
		private int chainIndex2;

		// Token: 0x040007D0 RID: 2000
		private int nodeIndex2;

		// Token: 0x040007D1 RID: 2001
		private int chainIndex3;

		// Token: 0x040007D2 RID: 2002
		private int nodeIndex3;

		// Token: 0x040007D3 RID: 2003
		[CompilerGenerated]
		private bool <initiated>k__BackingField;

		// Token: 0x040007D4 RID: 2004
		private bool limbOrientationsSet;
	}
}
