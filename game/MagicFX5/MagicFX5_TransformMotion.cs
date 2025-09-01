using System;
using UnityEngine;

namespace MagicFX5
{
	// Token: 0x02000024 RID: 36
	public class MagicFX5_TransformMotion : MagicFX5_IScriptInstance
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x0000633C File Offset: 0x0000453C
		internal override void OnEnableExtended()
		{
			this._currentSpeed = 0f;
			this._leftTime = 0f;
			this._isFinished = false;
			this._velocity = Vector3.zero;
			this._startLocalPos = this.Transform.localPosition;
			this._startWorldPos = this.Transform.position;
			if (this.ImpactGameObjectAtFinish != null)
			{
				this.ImpactGameObjectAtFinish.SetActive(false);
			}
			if (this.DeactivateGameobjectAfterImpact != null)
			{
				this.DeactivateGameobjectAfterImpact.SetActive(true);
			}
			if (this.MoveMode == MagicFX5_TransformMotion.MoveModeEnum.AnimatorRootMotion)
			{
				MagicFX5_EffectSettings effectSettings = this.EffectSettings;
				effectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Combine(effectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectCollisionEnter));
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000063F7 File Offset: 0x000045F7
		private void OnEffectCollisionEnter(MagicFX5_EffectSettings.EffectCollisionHit hit)
		{
			this._isFinished = true;
			if (this.ImpactGameObjectAtFinish != null)
			{
				this.ImpactGameObjectAtFinish.SetActive(true);
			}
			if (this.DeactivateGameobjectAfterImpact != null)
			{
				this.DeactivateGameobjectAfterImpact.SetActive(false);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00006434 File Offset: 0x00004634
		internal override void OnDisableExtended()
		{
			this.Transform.localPosition = this._startLocalPos;
			if (this.MoveMode == MagicFX5_TransformMotion.MoveModeEnum.AnimatorRootMotion)
			{
				MagicFX5_EffectSettings effectSettings = this.EffectSettings;
				effectSettings.OnEffectCollisionEnter = (Action<MagicFX5_EffectSettings.EffectCollisionHit>)Delegate.Remove(effectSettings.OnEffectCollisionEnter, new Action<MagicFX5_EffectSettings.EffectCollisionHit>(this.OnEffectCollisionEnter));
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006484 File Offset: 0x00004684
		internal override void ManualUpdate()
		{
			if (this._isFinished)
			{
				return;
			}
			float deltaTime = Time.deltaTime;
			this._leftTime += deltaTime;
			if (this._leftTime < this.StartDelay)
			{
				return;
			}
			float projectileSpeed = this.EffectSettings.ProjectileSpeed;
			switch (this.MoveMode)
			{
			case MagicFX5_TransformMotion.MoveModeEnum.ForwardDirection:
				this._velocity += Vector3.up * this.Gravity * deltaTime;
				this._currentSpeed = ((this._leftTime - this.StartDelay <= this.AccelerationTime) ? Mathf.Lerp(0f, projectileSpeed, (this._leftTime - this.StartDelay) / this.AccelerationTime) : projectileSpeed);
				this.Transform.position += (this.Transform.forward * this._currentSpeed + this._velocity) * deltaTime;
				if (Vector3.Distance(this._startWorldPos, this.Transform.position) >= this.MaxDistance)
				{
					this._isFinished = true;
					this.TriggerImpact(this.Target);
					return;
				}
				break;
			case MagicFX5_TransformMotion.MoveModeEnum.TargetDirection:
				if (this.Target != null)
				{
					this.Transform.position = Vector3.MoveTowards(this.Transform.position, this.Target.position, projectileSpeed * deltaTime);
					this.CheckTargetDistance(this.Target, this.Target.position);
					return;
				}
				break;
			case MagicFX5_TransformMotion.MoveModeEnum.EffectSettingsTargetDirection:
				if (this.EffectSettings.Targets.Length != 0)
				{
					this.Target = this.EffectSettings.Targets[0];
					Vector3 targetCenter = this.EffectSettings.GetTargetCenter(this.Target);
					this.Transform.position = Vector3.MoveTowards(this.Transform.position, targetCenter, projectileSpeed * deltaTime);
					this.CheckTargetDistance(this.Target, targetCenter);
					return;
				}
				break;
			case MagicFX5_TransformMotion.MoveModeEnum.AnimatorRootMotion:
				if (Vector3.Distance(this._startWorldPos, this.Transform.position) >= this.MaxDistance)
				{
					this._isFinished = true;
					this.TriggerImpact(this.Target);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000066A3 File Offset: 0x000048A3
		private void CheckTargetDistance(Transform target, Vector3 targetPosition)
		{
			if (this.TargetDistanceThreshold > 0f && this.VectorMagnitudeXZ(this.Transform.position - targetPosition) < this.TargetDistanceThreshold)
			{
				this._isFinished = true;
				this.TriggerImpact(target);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000066E0 File Offset: 0x000048E0
		private void TriggerImpact(Transform target)
		{
			if (this.ImpactGameObjectAtFinish != null)
			{
				this.ImpactGameObjectAtFinish.SetActive(true);
			}
			if (this.DeactivateGameobjectAfterImpact != null)
			{
				this.DeactivateGameobjectAfterImpact.SetActive(false);
			}
			if (target != null)
			{
				Vector3 normalized = (this.Transform.position - this._startWorldPos).normalized;
				MagicFX5_EffectSettings.EffectCollisionHit obj = new MagicFX5_EffectSettings.EffectCollisionHit
				{
					Target = target,
					Position = this.Transform.position,
					Normal = normalized
				};
				Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectCollisionEnter = this.EffectSettings.OnEffectCollisionEnter;
				if (onEffectCollisionEnter != null)
				{
					onEffectCollisionEnter(obj);
				}
				Action<MagicFX5_EffectSettings.EffectCollisionHit> onEffectSkinActivated = this.EffectSettings.OnEffectSkinActivated;
				if (onEffectSkinActivated == null)
				{
					return;
				}
				onEffectSkinActivated(obj);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000067A2 File Offset: 0x000049A2
		private float VectorMagnitudeXZ(Vector3 vector)
		{
			return Mathf.Sqrt(vector.x * vector.x + vector.z * vector.z);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000067C4 File Offset: 0x000049C4
		public MagicFX5_TransformMotion()
		{
		}

		// Token: 0x0400012C RID: 300
		public MagicFX5_EffectSettings EffectSettings;

		// Token: 0x0400012D RID: 301
		public Transform Transform;

		// Token: 0x0400012E RID: 302
		public MagicFX5_TransformMotion.MoveModeEnum MoveMode;

		// Token: 0x0400012F RID: 303
		public Transform Target;

		// Token: 0x04000130 RID: 304
		public float Speed = 10f;

		// Token: 0x04000131 RID: 305
		public float Gravity;

		// Token: 0x04000132 RID: 306
		public float AccelerationTime = 1f;

		// Token: 0x04000133 RID: 307
		public float MaxDistance = 100f;

		// Token: 0x04000134 RID: 308
		public float TargetDistanceThreshold = -1f;

		// Token: 0x04000135 RID: 309
		public float StartDelay;

		// Token: 0x04000136 RID: 310
		[Space]
		public GameObject ImpactGameObjectAtFinish;

		// Token: 0x04000137 RID: 311
		public GameObject DeactivateGameobjectAfterImpact;

		// Token: 0x04000138 RID: 312
		private bool _isFinished;

		// Token: 0x04000139 RID: 313
		private float _leftTime;

		// Token: 0x0400013A RID: 314
		private Vector3 _startLocalPos;

		// Token: 0x0400013B RID: 315
		private Vector3 _startWorldPos;

		// Token: 0x0400013C RID: 316
		private float _currentSpeed;

		// Token: 0x0400013D RID: 317
		private Vector3 _velocity;

		// Token: 0x0200003D RID: 61
		public enum MoveModeEnum
		{
			// Token: 0x040001A3 RID: 419
			ForwardDirection,
			// Token: 0x040001A4 RID: 420
			TargetDirection,
			// Token: 0x040001A5 RID: 421
			EffectSettingsTargetDirection,
			// Token: 0x040001A6 RID: 422
			AnimatorRootMotion
		}
	}
}
