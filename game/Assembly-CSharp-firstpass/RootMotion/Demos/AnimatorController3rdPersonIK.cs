using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000130 RID: 304
	public class AnimatorController3rdPersonIK : AnimatorController3rdPerson
	{
		// Token: 0x06000CC2 RID: 3266 RVA: 0x000567A0 File Offset: 0x000549A0
		protected override void Start()
		{
			base.Start();
			this.aim = base.GetComponent<AimIK>();
			this.ik = base.GetComponent<FullBodyBipedIK>();
			IKSolverFullBodyBiped solver = this.ik.solver;
			solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Combine(solver.OnPreRead, new IKSolver.UpdateDelegate(this.OnPreRead));
			this.aim.enabled = false;
			this.ik.enabled = false;
			this.headLookAxis = this.ik.references.head.InverseTransformVector(this.ik.references.root.forward);
			this.animator.SetLayerWeight(1, 1f);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00056850 File Offset: 0x00054A50
		public override void Move(Vector3 moveInput, bool isMoving, Vector3 faceDirection, Vector3 aimTarget)
		{
			base.Move(moveInput, isMoving, faceDirection, aimTarget);
			this.aimTarget = aimTarget;
			this.Read();
			this.AimIK();
			this.FBBIK();
			this.AimIK();
			this.HeadLookAt(aimTarget);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00056888 File Offset: 0x00054A88
		private void Read()
		{
			this.leftHandPosRelToRightHand = this.ik.references.rightHand.InverseTransformPoint(this.ik.references.leftHand.position);
			this.leftHandRotRelToRightHand = Quaternion.Inverse(this.ik.references.rightHand.rotation) * this.ik.references.leftHand.rotation;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000568FF File Offset: 0x00054AFF
		private void AimIK()
		{
			this.aim.solver.IKPosition = this.aimTarget;
			this.aim.solver.Update();
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00056928 File Offset: 0x00054B28
		private void FBBIK()
		{
			this.rightHandRotation = this.ik.references.rightHand.rotation;
			Vector3 b = this.ik.references.rightHand.rotation * this.gunHoldOffset;
			this.ik.solver.rightHandEffector.positionOffset += b;
			if (this.recoil != null)
			{
				this.recoil.SetHandRotations(this.rightHandRotation * this.leftHandRotRelToRightHand, this.rightHandRotation);
			}
			this.ik.solver.Update();
			if (this.recoil != null)
			{
				this.ik.references.rightHand.rotation = this.recoil.rotationOffset * this.rightHandRotation;
				this.ik.references.leftHand.rotation = this.recoil.rotationOffset * this.rightHandRotation * this.leftHandRotRelToRightHand;
				return;
			}
			this.ik.references.rightHand.rotation = this.rightHandRotation;
			this.ik.references.leftHand.rotation = this.rightHandRotation * this.leftHandRotRelToRightHand;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00056A84 File Offset: 0x00054C84
		private void OnPreRead()
		{
			Quaternion rotation = (this.recoil != null) ? (this.recoil.rotationOffset * this.rightHandRotation) : this.rightHandRotation;
			Vector3 a = this.ik.references.rightHand.position + this.ik.solver.rightHandEffector.positionOffset + rotation * this.leftHandPosRelToRightHand;
			this.ik.solver.leftHandEffector.positionOffset += a - this.ik.references.leftHand.position - this.ik.solver.leftHandEffector.positionOffset + rotation * this.leftHandOffset;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00056B68 File Offset: 0x00054D68
		private void HeadLookAt(Vector3 lookAtTarget)
		{
			Quaternion b = Quaternion.FromToRotation(this.ik.references.head.rotation * this.headLookAxis, lookAtTarget - this.ik.references.head.position);
			this.ik.references.head.rotation = Quaternion.Lerp(Quaternion.identity, b, this.headLookWeight) * this.ik.references.head.rotation;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00056BF6 File Offset: 0x00054DF6
		private void OnDestroy()
		{
			if (this.ik != null)
			{
				IKSolverFullBodyBiped solver = this.ik.solver;
				solver.OnPreRead = (IKSolver.UpdateDelegate)Delegate.Remove(solver.OnPreRead, new IKSolver.UpdateDelegate(this.OnPreRead));
			}
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00056C32 File Offset: 0x00054E32
		public AnimatorController3rdPersonIK()
		{
		}

		// Token: 0x04000A3D RID: 2621
		[Range(0f, 1f)]
		public float headLookWeight = 1f;

		// Token: 0x04000A3E RID: 2622
		public Vector3 gunHoldOffset;

		// Token: 0x04000A3F RID: 2623
		public Vector3 leftHandOffset;

		// Token: 0x04000A40 RID: 2624
		public Recoil recoil;

		// Token: 0x04000A41 RID: 2625
		private AimIK aim;

		// Token: 0x04000A42 RID: 2626
		private FullBodyBipedIK ik;

		// Token: 0x04000A43 RID: 2627
		private Vector3 headLookAxis;

		// Token: 0x04000A44 RID: 2628
		private Vector3 leftHandPosRelToRightHand;

		// Token: 0x04000A45 RID: 2629
		private Quaternion leftHandRotRelToRightHand;

		// Token: 0x04000A46 RID: 2630
		private Vector3 aimTarget;

		// Token: 0x04000A47 RID: 2631
		private Quaternion rightHandRotation;
	}
}
