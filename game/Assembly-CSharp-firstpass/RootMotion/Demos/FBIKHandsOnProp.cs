using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000138 RID: 312
	public class FBIKHandsOnProp : MonoBehaviour
	{
		// Token: 0x06000CE4 RID: 3300 RVA: 0x00057647 File Offset: 0x00055847
		private void Awake()
		{
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreRead, new IKSolver.UpdateDelegate(this.OnPreRead));
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00057678 File Offset: 0x00055878
		private void OnPreRead()
		{
			if (this.leftHanded)
			{
				this.HandsOnProp(this.ik.solver.leftHandEffector, this.ik.solver.rightHandEffector);
				return;
			}
			this.HandsOnProp(this.ik.solver.rightHandEffector, this.ik.solver.leftHandEffector);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000576DC File Offset: 0x000558DC
		private void HandsOnProp(IKEffector mainHand, IKEffector otherHand)
		{
			Vector3 vector = otherHand.bone.position - mainHand.bone.position;
			Vector3 point = Quaternion.Inverse(mainHand.bone.rotation) * vector;
			Vector3 b = mainHand.bone.position + vector * 0.5f;
			Quaternion rhs = Quaternion.Inverse(mainHand.bone.rotation) * otherHand.bone.rotation;
			Vector3 toDirection = otherHand.bone.position + otherHand.positionOffset - (mainHand.bone.position + mainHand.positionOffset);
			Vector3 a = mainHand.bone.position + mainHand.positionOffset + vector * 0.5f;
			mainHand.position = mainHand.bone.position + mainHand.positionOffset + (a - b);
			mainHand.positionWeight = 1f;
			Quaternion lhs = Quaternion.FromToRotation(vector, toDirection);
			mainHand.bone.rotation = lhs * mainHand.bone.rotation;
			otherHand.position = mainHand.position + mainHand.bone.rotation * point;
			otherHand.positionWeight = 1f;
			otherHand.bone.rotation = mainHand.bone.rotation * rhs;
			this.ik.solver.leftArmMapping.maintainRotationWeight = 1f;
			this.ik.solver.rightArmMapping.maintainRotationWeight = 1f;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0005788C File Offset: 0x00055A8C
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreRead, new IKSolver.UpdateDelegate(this.OnPreRead));
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000578C8 File Offset: 0x00055AC8
		public FBIKHandsOnProp()
		{
		}

		// Token: 0x04000A73 RID: 2675
		public FullBodyBipedIK ik;

		// Token: 0x04000A74 RID: 2676
		public bool leftHanded;
	}
}
