using System;
using UnityEngine;
using UnityEngine.Playables;

namespace RootMotion
{
	// Token: 0x020000AF RID: 175
	public class HumanoidBaker : Baker
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x000361CC File Offset: 0x000343CC
		private void Awake()
		{
			this.animator = base.GetComponent<Animator>();
			this.director = base.GetComponent<PlayableDirector>();
			if (this.mode == Baker.Mode.AnimationStates || this.mode == Baker.Mode.AnimationClips)
			{
				if (this.animator == null || !this.animator.isHuman)
				{
					Debug.LogError("HumanoidBaker GameObject does not have a Humanoid Animator component, can not bake.");
					base.enabled = false;
					return;
				}
				this.animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
			}
			else if (this.mode == Baker.Mode.PlayableDirector && this.director == null)
			{
				Debug.LogError("HumanoidBaker GameObject does not have a PlayableDirector component, can not bake.");
			}
			this.muscles = new float[HumanTrait.MuscleCount];
			this.bakerMuscles = new BakerMuscle[HumanTrait.MuscleCount];
			for (int i = 0; i < this.bakerMuscles.Length; i++)
			{
				this.bakerMuscles[i] = new BakerMuscle(i);
			}
			this.rootQT = new BakerHumanoidQT("Root");
			this.leftFootQT = new BakerHumanoidQT(this.animator.GetBoneTransform(HumanBodyBones.LeftFoot), AvatarIKGoal.LeftFoot, "LeftFoot");
			this.rightFootQT = new BakerHumanoidQT(this.animator.GetBoneTransform(HumanBodyBones.RightFoot), AvatarIKGoal.RightFoot, "RightFoot");
			this.leftHandQT = new BakerHumanoidQT(this.animator.GetBoneTransform(HumanBodyBones.LeftHand), AvatarIKGoal.LeftHand, "LeftHand");
			this.rightHandQT = new BakerHumanoidQT(this.animator.GetBoneTransform(HumanBodyBones.RightHand), AvatarIKGoal.RightHand, "RightHand");
			this.handler = new HumanPoseHandler(this.animator.avatar, this.animator.transform);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00036346 File Offset: 0x00034546
		protected override Transform GetCharacterRoot()
		{
			return this.animator.transform;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00036354 File Offset: 0x00034554
		protected override void OnStartBaking()
		{
			this.rootQT.Reset();
			this.leftFootQT.Reset();
			this.rightFootQT.Reset();
			this.leftHandQT.Reset();
			this.rightHandQT.Reset();
			for (int i = 0; i < this.bakerMuscles.Length; i++)
			{
				this.bakerMuscles[i].Reset();
			}
			this.mN = this.muscleFrameRateDiv;
			this.lastBodyRotation = Quaternion.identity;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000363D0 File Offset: 0x000345D0
		protected override void OnSetLoopFrame(float time)
		{
			for (int i = 0; i < this.bakerMuscles.Length; i++)
			{
				this.bakerMuscles[i].SetLoopFrame(time);
			}
			this.rootQT.MoveLastKeyframes(time);
			this.leftFootQT.SetLoopFrame(time);
			this.rightFootQT.SetLoopFrame(time);
			this.leftHandQT.SetLoopFrame(time);
			this.rightHandQT.SetLoopFrame(time);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0003643C File Offset: 0x0003463C
		protected override void OnSetCurves(ref AnimationClip clip)
		{
			float time = this.bakerMuscles[0].curve.keys[this.bakerMuscles[0].curve.keys.Length - 1].time;
			float lengthMlp = (this.mode != Baker.Mode.Realtime) ? (base.clipLength / time) : 1f;
			for (int i = 0; i < this.bakerMuscles.Length; i++)
			{
				this.bakerMuscles[i].SetCurves(ref clip, this.keyReductionError, lengthMlp);
			}
			this.rootQT.SetCurves(ref clip, this.IKKeyReductionError, lengthMlp);
			this.leftFootQT.SetCurves(ref clip, this.IKKeyReductionError, lengthMlp);
			this.rightFootQT.SetCurves(ref clip, this.IKKeyReductionError, lengthMlp);
			if (this.bakeHandIK)
			{
				this.leftHandQT.SetCurves(ref clip, this.IKKeyReductionError, lengthMlp);
				this.rightHandQT.SetCurves(ref clip, this.IKKeyReductionError, lengthMlp);
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00036524 File Offset: 0x00034724
		protected override void OnSetKeyframes(float time, bool lastFrame)
		{
			this.mN++;
			bool flag = true;
			if (this.mN < this.muscleFrameRateDiv && !lastFrame)
			{
				flag = false;
			}
			if (this.mN >= this.muscleFrameRateDiv)
			{
				this.mN = 0;
			}
			this.UpdateHumanPose();
			if (flag)
			{
				for (int i = 0; i < this.bakerMuscles.Length; i++)
				{
					this.bakerMuscles[i].SetKeyframe(time, this.muscles);
				}
			}
			this.rootQT.SetKeyframes(time, this.bodyPosition, this.bodyRotation);
			Vector3 vector = this.bodyPosition * this.animator.humanScale;
			this.leftFootQT.SetIKKeyframes(time, this.animator.avatar, this.animator.transform, this.animator.humanScale, vector, this.bodyRotation);
			this.rightFootQT.SetIKKeyframes(time, this.animator.avatar, this.animator.transform, this.animator.humanScale, vector, this.bodyRotation);
			this.leftHandQT.SetIKKeyframes(time, this.animator.avatar, this.animator.transform, this.animator.humanScale, vector, this.bodyRotation);
			this.rightHandQT.SetIKKeyframes(time, this.animator.avatar, this.animator.transform, this.animator.humanScale, vector, this.bodyRotation);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00036698 File Offset: 0x00034898
		private void UpdateHumanPose()
		{
			this.handler.GetHumanPose(ref this.pose);
			this.bodyPosition = this.pose.bodyPosition;
			this.bodyRotation = this.pose.bodyRotation;
			this.bodyRotation = BakerUtilities.EnsureQuaternionContinuity(this.lastBodyRotation, this.bodyRotation);
			this.lastBodyRotation = this.bodyRotation;
			for (int i = 0; i < this.pose.muscles.Length; i++)
			{
				this.muscles[i] = this.pose.muscles[i];
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00036728 File Offset: 0x00034928
		public HumanoidBaker()
		{
		}

		// Token: 0x0400063F RID: 1599
		[Tooltip("Should the hand IK curves be added to the animation? Disable this if the original hand positions are not important when using the clip on another character via Humanoid retargeting.")]
		public bool bakeHandIK = true;

		// Token: 0x04000640 RID: 1600
		[Tooltip("Max keyframe reduction error for the Root.Q/T, LeftFoot IK and RightFoot IK channels. Having a larger error value for 'Key Reduction Error' and a smaller one for this enables you to optimize clip data size without the floating feet effect by enabling 'Foot IK' in the Animator.")]
		[Range(0f, 0.1f)]
		public float IKKeyReductionError;

		// Token: 0x04000641 RID: 1601
		[Tooltip("Frame rate divider for the muscle curves. If you have 'Frame Rate' set to 30, and this value set to 3, the muscle curves will be baked at 10 fps. Only the Root Q/T and Hand and Foot IK curves will be baked at 30. This enables you to optimize clip data size without the floating feet effect by enabling 'Foot IK' in the Animator.")]
		[Range(1f, 9f)]
		public int muscleFrameRateDiv = 1;

		// Token: 0x04000642 RID: 1602
		private BakerMuscle[] bakerMuscles;

		// Token: 0x04000643 RID: 1603
		private BakerHumanoidQT rootQT;

		// Token: 0x04000644 RID: 1604
		private BakerHumanoidQT leftFootQT;

		// Token: 0x04000645 RID: 1605
		private BakerHumanoidQT rightFootQT;

		// Token: 0x04000646 RID: 1606
		private BakerHumanoidQT leftHandQT;

		// Token: 0x04000647 RID: 1607
		private BakerHumanoidQT rightHandQT;

		// Token: 0x04000648 RID: 1608
		private float[] muscles = new float[0];

		// Token: 0x04000649 RID: 1609
		private HumanPose pose;

		// Token: 0x0400064A RID: 1610
		private HumanPoseHandler handler;

		// Token: 0x0400064B RID: 1611
		private Vector3 bodyPosition;

		// Token: 0x0400064C RID: 1612
		private Quaternion bodyRotation = Quaternion.identity;

		// Token: 0x0400064D RID: 1613
		private int mN;

		// Token: 0x0400064E RID: 1614
		private Quaternion lastBodyRotation = Quaternion.identity;
	}
}
