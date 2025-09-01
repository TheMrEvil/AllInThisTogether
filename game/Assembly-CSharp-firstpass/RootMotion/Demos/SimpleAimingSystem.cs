using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000127 RID: 295
	public class SimpleAimingSystem : MonoBehaviour
	{
		// Token: 0x06000C96 RID: 3222 RVA: 0x00054FDD File Offset: 0x000531DD
		private void Start()
		{
			this.aim.enabled = false;
			this.lookAt.enabled = false;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00054FF8 File Offset: 0x000531F8
		private void LateUpdate()
		{
			if (this.aim.solver.target == null)
			{
				Debug.LogWarning("AimIK and LookAtIK need to have their 'Target' value assigned.", base.transform);
			}
			this.Pose();
			this.aim.solver.Update();
			if (this.lookAt != null)
			{
				this.lookAt.solver.Update();
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00055064 File Offset: 0x00053264
		private void Pose()
		{
			this.LimitAimTarget();
			Vector3 direction = this.aim.solver.target.position - this.aim.solver.bones[0].transform.position;
			Vector3 localDirection = base.transform.InverseTransformDirection(direction);
			this.aimPose = this.aimPoser.GetPose(localDirection);
			if (this.aimPose != this.lastPose)
			{
				this.aimPoser.SetPoseActive(this.aimPose);
				this.lastPose = this.aimPose;
			}
			foreach (AimPoser.Pose pose in this.aimPoser.poses)
			{
				if (pose == this.aimPose)
				{
					this.DirectCrossFade(pose.name, 1f);
				}
				else
				{
					this.DirectCrossFade(pose.name, 0f);
				}
			}
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00055148 File Offset: 0x00053348
		private void LimitAimTarget()
		{
			Vector3 position = this.aim.solver.bones[0].transform.position;
			Vector3 b = this.aim.solver.target.position - position;
			b = b.normalized * Mathf.Max(b.magnitude, this.minAimDistance);
			this.aim.solver.target.position = position + b;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000551CC File Offset: 0x000533CC
		private void DirectCrossFade(string state, float target)
		{
			float value = Mathf.MoveTowards(this.animator.GetFloat(state), target, Time.deltaTime * (1f / this.crossfadeTime));
			this.animator.SetFloat(state, value);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0005520B File Offset: 0x0005340B
		public SimpleAimingSystem()
		{
		}

		// Token: 0x040009DF RID: 2527
		[Tooltip("AimPoser is a tool that returns an animation name based on direction.")]
		public AimPoser aimPoser;

		// Token: 0x040009E0 RID: 2528
		[Tooltip("Reference to the AimIK component.")]
		public AimIK aim;

		// Token: 0x040009E1 RID: 2529
		[Tooltip("Reference to the LookAt component (only used for the head in this instance).")]
		public LookAtIK lookAt;

		// Token: 0x040009E2 RID: 2530
		[Tooltip("Reference to the Animator component.")]
		public Animator animator;

		// Token: 0x040009E3 RID: 2531
		[Tooltip("Time of cross-fading from pose to pose.")]
		public float crossfadeTime = 0.2f;

		// Token: 0x040009E4 RID: 2532
		[Tooltip("Will keep the aim target at a distance.")]
		public float minAimDistance = 0.5f;

		// Token: 0x040009E5 RID: 2533
		private AimPoser.Pose aimPose;

		// Token: 0x040009E6 RID: 2534
		private AimPoser.Pose lastPose;
	}
}
