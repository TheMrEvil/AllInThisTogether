using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000129 RID: 297
	public class BipedIKvsAnimatorIK : MonoBehaviour
	{
		// Token: 0x06000C9F RID: 3231 RVA: 0x000553D0 File Offset: 0x000535D0
		private void OnAnimatorIK(int layer)
		{
			this.animator.transform.rotation = this.bipedIK.transform.rotation;
			Vector3 b = this.animator.transform.position - this.bipedIK.transform.position;
			this.lookAtTargetAnimator.position = this.lookAtTargetBiped.position + b;
			this.bipedIK.SetLookAtPosition(this.lookAtTargetBiped.position);
			this.bipedIK.SetLookAtWeight(this.lookAtWeight, this.lookAtBodyWeight, this.lookAtHeadWeight, this.lookAtEyesWeight, this.lookAtClampWeight, this.lookAtClampWeightHead, this.lookAtClampWeightEyes);
			this.animator.SetLookAtPosition(this.lookAtTargetAnimator.position);
			this.animator.SetLookAtWeight(this.lookAtWeight, this.lookAtBodyWeight, this.lookAtHeadWeight, this.lookAtEyesWeight, this.lookAtClampWeight);
			this.footTargetAnimator.position = this.footTargetBiped.position + b;
			this.footTargetAnimator.rotation = this.footTargetBiped.rotation;
			this.bipedIK.SetIKPosition(AvatarIKGoal.LeftFoot, this.footTargetBiped.position);
			this.bipedIK.SetIKRotation(AvatarIKGoal.LeftFoot, this.footTargetBiped.rotation);
			this.bipedIK.SetIKPositionWeight(AvatarIKGoal.LeftFoot, this.footPositionWeight);
			this.bipedIK.SetIKRotationWeight(AvatarIKGoal.LeftFoot, this.footRotationWeight);
			this.animator.SetIKPosition(AvatarIKGoal.LeftFoot, this.footTargetAnimator.position);
			this.animator.SetIKRotation(AvatarIKGoal.LeftFoot, this.footTargetAnimator.rotation);
			this.animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, this.footPositionWeight);
			this.animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, this.footRotationWeight);
			this.handTargetAnimator.position = this.handTargetBiped.position + b;
			this.handTargetAnimator.rotation = this.handTargetBiped.rotation;
			this.bipedIK.SetIKPosition(AvatarIKGoal.LeftHand, this.handTargetBiped.position);
			this.bipedIK.SetIKRotation(AvatarIKGoal.LeftHand, this.handTargetBiped.rotation);
			this.bipedIK.SetIKPositionWeight(AvatarIKGoal.LeftHand, this.handPositionWeight);
			this.bipedIK.SetIKRotationWeight(AvatarIKGoal.LeftHand, this.handRotationWeight);
			this.animator.SetIKPosition(AvatarIKGoal.LeftHand, this.handTargetAnimator.position);
			this.animator.SetIKRotation(AvatarIKGoal.LeftHand, this.handTargetAnimator.rotation);
			this.animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, this.handPositionWeight);
			this.animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, this.handRotationWeight);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00055678 File Offset: 0x00053878
		public BipedIKvsAnimatorIK()
		{
		}

		// Token: 0x040009EF RID: 2543
		[LargeHeader("References")]
		public Animator animator;

		// Token: 0x040009F0 RID: 2544
		public BipedIK bipedIK;

		// Token: 0x040009F1 RID: 2545
		[LargeHeader("Look At")]
		public Transform lookAtTargetBiped;

		// Token: 0x040009F2 RID: 2546
		public Transform lookAtTargetAnimator;

		// Token: 0x040009F3 RID: 2547
		[Range(0f, 1f)]
		public float lookAtWeight = 1f;

		// Token: 0x040009F4 RID: 2548
		[Range(0f, 1f)]
		public float lookAtBodyWeight = 1f;

		// Token: 0x040009F5 RID: 2549
		[Range(0f, 1f)]
		public float lookAtHeadWeight = 1f;

		// Token: 0x040009F6 RID: 2550
		[Range(0f, 1f)]
		public float lookAtEyesWeight = 1f;

		// Token: 0x040009F7 RID: 2551
		[Range(0f, 1f)]
		public float lookAtClampWeight = 0.5f;

		// Token: 0x040009F8 RID: 2552
		[Range(0f, 1f)]
		public float lookAtClampWeightHead = 0.5f;

		// Token: 0x040009F9 RID: 2553
		[Range(0f, 1f)]
		public float lookAtClampWeightEyes = 0.5f;

		// Token: 0x040009FA RID: 2554
		[LargeHeader("Foot")]
		public Transform footTargetBiped;

		// Token: 0x040009FB RID: 2555
		public Transform footTargetAnimator;

		// Token: 0x040009FC RID: 2556
		[Range(0f, 1f)]
		public float footPositionWeight;

		// Token: 0x040009FD RID: 2557
		[Range(0f, 1f)]
		public float footRotationWeight;

		// Token: 0x040009FE RID: 2558
		[LargeHeader("Hand")]
		public Transform handTargetBiped;

		// Token: 0x040009FF RID: 2559
		public Transform handTargetAnimator;

		// Token: 0x04000A00 RID: 2560
		[Range(0f, 1f)]
		public float handPositionWeight;

		// Token: 0x04000A01 RID: 2561
		[Range(0f, 1f)]
		public float handRotationWeight;
	}
}
