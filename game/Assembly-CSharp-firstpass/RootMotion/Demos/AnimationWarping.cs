using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200012E RID: 302
	public class AnimationWarping : OffsetModifier
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x000562FD File Offset: 0x000544FD
		protected override void Start()
		{
			base.Start();
			this.lastMode = this.effectorMode;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00056314 File Offset: 0x00054514
		public float GetWarpWeight(int warpIndex)
		{
			if (warpIndex < 0)
			{
				Debug.LogError("Warp index out of range.");
				return 0f;
			}
			if (warpIndex >= this.warps.Length)
			{
				Debug.LogError("Warp index out of range.");
				return 0f;
			}
			if (this.animator == null)
			{
				Debug.LogError("Animator unassigned in AnimationWarping");
				return 0f;
			}
			AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(this.warps[warpIndex].animationLayer);
			if (!currentAnimatorStateInfo.IsName(this.warps[warpIndex].animationState))
			{
				return 0f;
			}
			return this.warps[warpIndex].weightCurve.Evaluate(currentAnimatorStateInfo.normalizedTime - (float)((int)currentAnimatorStateInfo.normalizedTime));
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000563D4 File Offset: 0x000545D4
		protected override void OnModifyOffset()
		{
			for (int i = 0; i < this.warps.Length; i++)
			{
				float warpWeight = this.GetWarpWeight(i);
				Vector3 vector = this.warps[i].warpTo.position - this.warps[i].warpFrom.position;
				AnimationWarping.EffectorMode effectorMode = this.effectorMode;
				if (effectorMode != AnimationWarping.EffectorMode.PositionOffset)
				{
					if (effectorMode == AnimationWarping.EffectorMode.Position)
					{
						this.ik.solver.GetEffector(this.warps[i].effector).position = this.ik.solver.GetEffector(this.warps[i].effector).bone.position + vector;
						this.ik.solver.GetEffector(this.warps[i].effector).positionWeight = this.weight * warpWeight;
					}
				}
				else
				{
					this.ik.solver.GetEffector(this.warps[i].effector).positionOffset += vector * warpWeight * this.weight;
				}
			}
			if (this.lastMode == AnimationWarping.EffectorMode.Position && this.effectorMode == AnimationWarping.EffectorMode.PositionOffset)
			{
				foreach (AnimationWarping.Warp warp in this.warps)
				{
					this.ik.solver.GetEffector(warp.effector).positionWeight = 0f;
				}
			}
			this.lastMode = this.effectorMode;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00056578 File Offset: 0x00054778
		private void OnDisable()
		{
			if (this.effectorMode != AnimationWarping.EffectorMode.Position)
			{
				return;
			}
			foreach (AnimationWarping.Warp warp in this.warps)
			{
				this.ik.solver.GetEffector(warp.effector).positionWeight = 0f;
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000565CC File Offset: 0x000547CC
		public AnimationWarping()
		{
		}

		// Token: 0x04000A30 RID: 2608
		[Tooltip("Reference to the Animator component to use")]
		public Animator animator;

		// Token: 0x04000A31 RID: 2609
		[Tooltip("Using effector.positionOffset or effector.position with effector.positionWeight? The former will enable you to use effector.position for other things, the latter will weigh in the effectors, hence using Reach and Pull in the process.")]
		public AnimationWarping.EffectorMode effectorMode;

		// Token: 0x04000A32 RID: 2610
		[Space(10f)]
		[Tooltip("The array of warps, can have multiple simultaneous warps.")]
		public AnimationWarping.Warp[] warps;

		// Token: 0x04000A33 RID: 2611
		private AnimationWarping.EffectorMode lastMode;

		// Token: 0x0200022D RID: 557
		[Serializable]
		public struct Warp
		{
			// Token: 0x04001079 RID: 4217
			[Tooltip("Layer of the 'Animation State' in the Animator.")]
			public int animationLayer;

			// Token: 0x0400107A RID: 4218
			[Tooltip("Name of the state in the Animator to warp.")]
			public string animationState;

			// Token: 0x0400107B RID: 4219
			[Tooltip("Warping weight by normalized time of the animation state.")]
			public AnimationCurve weightCurve;

			// Token: 0x0400107C RID: 4220
			[Tooltip("Animated point to warp from. This should be in character space so keep this Transform parented to the root of the character.")]
			public Transform warpFrom;

			// Token: 0x0400107D RID: 4221
			[Tooltip("World space point to warp to.")]
			public Transform warpTo;

			// Token: 0x0400107E RID: 4222
			[Tooltip("Which FBBIK effector to use?")]
			public FullBodyBipedEffector effector;
		}

		// Token: 0x0200022E RID: 558
		[Serializable]
		public enum EffectorMode
		{
			// Token: 0x04001080 RID: 4224
			PositionOffset,
			// Token: 0x04001081 RID: 4225
			Position
		}
	}
}
