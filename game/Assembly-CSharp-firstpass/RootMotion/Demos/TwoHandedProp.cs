using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200014E RID: 334
	public class TwoHandedProp : MonoBehaviour
	{
		// Token: 0x06000D3A RID: 3386 RVA: 0x0005986C File Offset: 0x00057A6C
		private void Start()
		{
			this.ik = base.GetComponent<FullBodyBipedIK>();
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
			this.ik.solver.leftHandEffector.positionWeight = 1f;
			this.ik.solver.rightHandEffector.positionWeight = 1f;
			if (this.ik.solver.rightHandEffector.target == null)
			{
				Debug.LogError("Right Hand Effector needs a Target in this demo.");
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0005990C File Offset: 0x00057B0C
		private void LateUpdate()
		{
			this.targetPosRelativeToRight = this.ik.references.rightHand.InverseTransformPoint(this.leftHandTarget.position);
			this.targetRotRelativeToRight = Quaternion.Inverse(this.ik.references.rightHand.rotation) * this.leftHandTarget.rotation;
			this.ik.solver.leftHandEffector.position = this.ik.solver.rightHandEffector.target.position + this.ik.solver.rightHandEffector.target.rotation * this.targetPosRelativeToRight;
			this.ik.solver.leftHandEffector.rotation = this.ik.solver.rightHandEffector.target.rotation * this.targetRotRelativeToRight;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00059A04 File Offset: 0x00057C04
		private void AfterFBBIK()
		{
			this.ik.solver.leftHandEffector.bone.rotation = this.ik.solver.leftHandEffector.rotation;
			this.ik.solver.rightHandEffector.bone.rotation = this.ik.solver.rightHandEffector.rotation;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00059A6F File Offset: 0x00057C6F
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPostUpdate = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPostUpdate, new IKSolver.UpdateDelegate(this.AfterFBBIK));
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00059AAB File Offset: 0x00057CAB
		public TwoHandedProp()
		{
		}

		// Token: 0x04000AE6 RID: 2790
		[Tooltip("The left hand target parented to the right hand.")]
		public Transform leftHandTarget;

		// Token: 0x04000AE7 RID: 2791
		private FullBodyBipedIK ik;

		// Token: 0x04000AE8 RID: 2792
		private Vector3 targetPosRelativeToRight;

		// Token: 0x04000AE9 RID: 2793
		private Quaternion targetRotRelativeToRight;
	}
}
