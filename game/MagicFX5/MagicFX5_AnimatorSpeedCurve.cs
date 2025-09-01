using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000004 RID: 4
	public class MagicFX5_AnimatorSpeedCurve : MagicFX5_IScriptInstance
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000022C0 File Offset: 0x000004C0
		internal override void OnEnableExtended()
		{
			this._animatorStates.Clear();
			if (this.OverrideToUseTarget != null)
			{
				MagicFX5_EffectSettings overrideToUseTarget = this.OverrideToUseTarget;
				overrideToUseTarget.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(overrideToUseTarget.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectCollisionEnter));
				return;
			}
			this._animatorStates.Add(new MagicFX5_AnimatorSpeedCurve.AnimatorState(base.GetComponent<Animator>(), this.StartNormalizeTimeOffset));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000232C File Offset: 0x0000052C
		private void OnEffectCollisionEnter(MagicFX5_EffectSettings.EffectCollisionHit hitInfo)
		{
			Animator component = hitInfo.Target.GetComponent<Animator>();
			this._animatorStates.Add(new MagicFX5_AnimatorSpeedCurve.AnimatorState(component, this.StartNormalizeTimeOffset));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000235C File Offset: 0x0000055C
		internal override void OnDisableExtended()
		{
			if (this.OverrideToUseTarget != null)
			{
				MagicFX5_EffectSettings overrideToUseTarget = this.OverrideToUseTarget;
				overrideToUseTarget.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(overrideToUseTarget.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectCollisionEnter));
			}
			if (this.AnimatorSpeedMode == MagicFX5_AnimatorSpeedCurve.AnimatorSpeedModeEnum.SpeedOverLifetime)
			{
				foreach (MagicFX5_AnimatorSpeedCurve.AnimatorState animatorState in this._animatorStates)
				{
					animatorState.ResetState();
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023EC File Offset: 0x000005EC
		internal override void ManualUpdate()
		{
			MagicFX5_AnimatorSpeedCurve.AnimatorSpeedModeEnum animatorSpeedMode = this.AnimatorSpeedMode;
			if (animatorSpeedMode != MagicFX5_AnimatorSpeedCurve.AnimatorSpeedModeEnum.SpeedOverLifetime)
			{
				if (animatorSpeedMode != MagicFX5_AnimatorSpeedCurve.AnimatorSpeedModeEnum.SpeedOverNormalizedAnimationTime)
				{
					return;
				}
			}
			else
			{
				float deltaTime = Time.deltaTime;
				using (List<MagicFX5_AnimatorSpeedCurve.AnimatorState>.Enumerator enumerator = this._animatorStates.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MagicFX5_AnimatorSpeedCurve.AnimatorState animatorState = enumerator.Current;
						if (animatorState.IsActive)
						{
							if (animatorState.AnimationLeftTime > this.SpeedCurveLifeTime)
							{
								animatorState.IsActive = false;
							}
							else
							{
								animatorState.AnimationLeftTime += deltaTime;
								float time = Mathf.Clamp01(animatorState.AnimationLeftTime / this.SpeedCurveLifeTime);
								float speed = this.SpeedCurve.Evaluate(time);
								animatorState.Animator.speed = speed;
							}
						}
					}
					return;
				}
			}
			AnimatorStateInfo currentAnimatorStateInfo = this._animatorStates[0].Animator.GetCurrentAnimatorStateInfo(0);
			float num = this.SpeedCurve.Evaluate(Mathf.Clamp01(currentAnimatorStateInfo.normalizedTime));
			this._animatorStates[0].Animator.speed = num * this.SpeedMultiplier;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002500 File Offset: 0x00000700
		public MagicFX5_AnimatorSpeedCurve()
		{
		}

		// Token: 0x04000014 RID: 20
		public MagicFX5_AnimatorSpeedCurve.AnimatorSpeedModeEnum AnimatorSpeedMode;

		// Token: 0x04000015 RID: 21
		public MagicFX5_EffectSettings OverrideToUseTarget;

		// Token: 0x04000016 RID: 22
		public AnimationCurve SpeedCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

		// Token: 0x04000017 RID: 23
		public float SpeedMultiplier = 1f;

		// Token: 0x04000018 RID: 24
		public float SpeedCurveLifeTime = 1f;

		// Token: 0x04000019 RID: 25
		public float StartNormalizeTimeOffset = -1f;

		// Token: 0x0400001A RID: 26
		private List<MagicFX5_AnimatorSpeedCurve.AnimatorState> _animatorStates = new List<MagicFX5_AnimatorSpeedCurve.AnimatorState>();

		// Token: 0x02000028 RID: 40
		private class AnimatorState
		{
			// Token: 0x060000CE RID: 206 RVA: 0x00006878 File Offset: 0x00004A78
			public AnimatorState(Animator anim, float startNormalizeTimeOffset)
			{
				this.Animator = anim;
				this.InitialSpeed = anim.speed;
				this.AnimationLeftTime = 0f;
				this.IsActive = true;
				if (startNormalizeTimeOffset > 0f)
				{
					this.Animator.Play(0, -1, startNormalizeTimeOffset);
				}
			}

			// Token: 0x060000CF RID: 207 RVA: 0x000068C6 File Offset: 0x00004AC6
			public void ResetState()
			{
				if (this.Animator != null)
				{
					this.Animator.speed = this.InitialSpeed;
				}
			}

			// Token: 0x04000148 RID: 328
			public Animator Animator;

			// Token: 0x04000149 RID: 329
			public float InitialSpeed;

			// Token: 0x0400014A RID: 330
			public float AnimationLeftTime;

			// Token: 0x0400014B RID: 331
			public bool IsActive;
		}

		// Token: 0x02000029 RID: 41
		public enum AnimatorSpeedModeEnum
		{
			// Token: 0x0400014D RID: 333
			SpeedOverLifetime,
			// Token: 0x0400014E RID: 334
			SpeedOverNormalizedAnimationTime
		}
	}
}
