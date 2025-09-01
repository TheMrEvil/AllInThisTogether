using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	public class BipedIKSolvers
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0003AB98 File Offset: 0x00038D98
		public IKSolverLimb[] limbs
		{
			get
			{
				if (this._limbs == null || (this._limbs != null && this._limbs.Length != 4))
				{
					this._limbs = new IKSolverLimb[]
					{
						this.leftFoot,
						this.rightFoot,
						this.leftHand,
						this.rightHand
					};
				}
				return this._limbs;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x0003ABF8 File Offset: 0x00038DF8
		public IKSolver[] ikSolvers
		{
			get
			{
				if (this._ikSolvers == null || (this._ikSolvers != null && this._ikSolvers.Length != 7))
				{
					this._ikSolvers = new IKSolver[]
					{
						this.leftFoot,
						this.rightFoot,
						this.leftHand,
						this.rightHand,
						this.spine,
						this.lookAt,
						this.aim
					};
				}
				return this._ikSolvers;
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0003AC74 File Offset: 0x00038E74
		public void AssignReferences(BipedReferences references)
		{
			this.leftHand.SetChain(references.leftUpperArm, references.leftForearm, references.leftHand, references.root);
			this.rightHand.SetChain(references.rightUpperArm, references.rightForearm, references.rightHand, references.root);
			this.leftFoot.SetChain(references.leftThigh, references.leftCalf, references.leftFoot, references.root);
			this.rightFoot.SetChain(references.rightThigh, references.rightCalf, references.rightFoot, references.root);
			this.spine.SetChain(references.spine, references.root);
			this.lookAt.SetChain(references.spine, references.head, references.eyes, references.root);
			this.aim.SetChain(references.spine, references.root);
			this.leftFoot.goal = AvatarIKGoal.LeftFoot;
			this.rightFoot.goal = AvatarIKGoal.RightFoot;
			this.leftHand.goal = AvatarIKGoal.LeftHand;
			this.rightHand.goal = AvatarIKGoal.RightHand;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0003AD98 File Offset: 0x00038F98
		public BipedIKSolvers()
		{
		}

		// Token: 0x040006D3 RID: 1747
		public IKSolverLimb leftFoot = new IKSolverLimb(AvatarIKGoal.LeftFoot);

		// Token: 0x040006D4 RID: 1748
		public IKSolverLimb rightFoot = new IKSolverLimb(AvatarIKGoal.RightFoot);

		// Token: 0x040006D5 RID: 1749
		public IKSolverLimb leftHand = new IKSolverLimb(AvatarIKGoal.LeftHand);

		// Token: 0x040006D6 RID: 1750
		public IKSolverLimb rightHand = new IKSolverLimb(AvatarIKGoal.RightHand);

		// Token: 0x040006D7 RID: 1751
		public IKSolverFABRIK spine = new IKSolverFABRIK();

		// Token: 0x040006D8 RID: 1752
		public IKSolverLookAt lookAt = new IKSolverLookAt();

		// Token: 0x040006D9 RID: 1753
		public IKSolverAim aim = new IKSolverAim();

		// Token: 0x040006DA RID: 1754
		public Constraints pelvis = new Constraints();

		// Token: 0x040006DB RID: 1755
		private IKSolverLimb[] _limbs;

		// Token: 0x040006DC RID: 1756
		private IKSolver[] _ikSolvers;
	}
}
