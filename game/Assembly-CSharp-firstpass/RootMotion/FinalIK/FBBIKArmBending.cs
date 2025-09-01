using System;
using UnityEngine;

namespace RootMotion.FinalIK
{
	// Token: 0x020000E5 RID: 229
	public class FBBIKArmBending : MonoBehaviour
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x0003F140 File Offset: 0x0003D340
		private void LateUpdate()
		{
			if (this.ik == null)
			{
				return;
			}
			if (!this.initiated)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostFBBIK));
				this.initiated = true;
			}
			if (this.ik.solver.leftHandEffector.target != null)
			{
				Vector3 left = Vector3.left;
				this.ik.solver.leftArmChain.bendConstraint.direction = this.ik.solver.leftHandEffector.target.rotation * left + this.ik.solver.leftHandEffector.target.rotation * this.bendDirectionOffsetLeft + this.ik.transform.rotation * this.characterSpaceBendOffsetLeft;
				this.ik.solver.leftArmChain.bendConstraint.weight = 1f;
			}
			if (this.ik.solver.rightHandEffector.target != null)
			{
				Vector3 right = Vector3.right;
				this.ik.solver.rightArmChain.bendConstraint.direction = this.ik.solver.rightHandEffector.target.rotation * right + this.ik.solver.rightHandEffector.target.rotation * this.bendDirectionOffsetRight + this.ik.transform.rotation * this.characterSpaceBendOffsetRight;
				this.ik.solver.rightArmChain.bendConstraint.weight = 1f;
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0003F32C File Offset: 0x0003D52C
		private void OnPostFBBIK()
		{
			if (this.ik == null)
			{
				return;
			}
			if (this.ik.solver.leftHandEffector.target != null)
			{
				this.ik.references.leftHand.rotation = this.ik.solver.leftHandEffector.target.rotation;
			}
			if (this.ik.solver.rightHandEffector.target != null)
			{
				this.ik.references.rightHand.rotation = this.ik.solver.rightHandEffector.target.rotation;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0003F3E0 File Offset: 0x0003D5E0
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.OnPostFBBIK));
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0003F41C File Offset: 0x0003D61C
		public FBBIKArmBending()
		{
		}

		// Token: 0x04000776 RID: 1910
		public FullBodyBipedIK ik;

		// Token: 0x04000777 RID: 1911
		public Vector3 bendDirectionOffsetLeft;

		// Token: 0x04000778 RID: 1912
		public Vector3 bendDirectionOffsetRight;

		// Token: 0x04000779 RID: 1913
		public Vector3 characterSpaceBendOffsetLeft;

		// Token: 0x0400077A RID: 1914
		public Vector3 characterSpaceBendOffsetRight;

		// Token: 0x0400077B RID: 1915
		private Quaternion leftHandTargetRotation;

		// Token: 0x0400077C RID: 1916
		private Quaternion rightHandTargetRotation;

		// Token: 0x0400077D RID: 1917
		private bool initiated;
	}
}
