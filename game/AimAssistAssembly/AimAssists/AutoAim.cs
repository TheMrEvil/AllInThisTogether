using System;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target;
using UnityEngine;
using UnityEngine.Events;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.AimAssists
{
	// Token: 0x02000012 RID: 18
	public class AutoAim : AimAssistBase
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002A0A File Offset: 0x00000C0A
		protected override void Awake()
		{
			base.Awake();
			this.SubscribeToTargetSelectorEvents();
			this.SetUpTimeAccumulator();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A1E File Offset: 0x00000C1E
		private void OnDestroy()
		{
			this.TearDownTargetSelectorEvents();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A28 File Offset: 0x00000C28
		public Vector2 AssistAim(Vector2 lookInputDelta)
		{
			AimAssistTarget target = base.Target;
			if (!this.aimAssistEnabled)
			{
				return lookInputDelta;
			}
			if (target == null)
			{
				return this.LerpEaseOut(lookInputDelta);
			}
			if (this.AimIsInDeadZone(target))
			{
				return lookInputDelta;
			}
			Vector3 position = target.transform.position;
			float x = this.CalculateTotalRotationAngles(Vector3.up, position);
			float y = this.CalculateTotalRotationAngles(base.PlayerCamera.right, position);
			Vector2 vector = new Vector2(x, y).normalized * lookInputDelta.magnitude;
			if (this.InputAndCalculatedAimAngleDifference(vector, lookInputDelta) > this.aimAngleThreshold)
			{
				return lookInputDelta * this.aimEaseOutDampeningMultiplier;
			}
			return Vector2.Lerp(lookInputDelta, vector, this.factor);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AD4 File Offset: 0x00000CD4
		private Vector2 LerpEaseOut(Vector2 lookInputDelta)
		{
			this.timeAccumulator = Mathf.Min(this.timeAccumulator + Time.deltaTime, this.timeToRegainOriginalInputSensitivity);
			return Mathf.Lerp(this.aimEaseOutDampeningMultiplier, 1f, this.timeAccumulator / this.timeToRegainOriginalInputSensitivity) * lookInputDelta;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B21 File Offset: 0x00000D21
		private float InputAndCalculatedAimAngleDifference(Vector2 aimInputToTarget, Vector2 lookInputDelta)
		{
			return Mathf.Abs(Vector2.SignedAngle(aimInputToTarget, lookInputDelta));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B30 File Offset: 0x00000D30
		private bool AimIsInDeadZone(AimAssistTarget target)
		{
			Vector3 a = target.transform.position - base.PlayerCamera.position;
			Vector3 b = base.PlayerCamera.forward * a.magnitude;
			return (a - b).sqrMagnitude < this.deadzoneRadius * this.deadzoneRadius;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B90 File Offset: 0x00000D90
		private float CalculateTotalRotationAngles(Vector3 planeNormal, Vector3 target)
		{
			Vector3 from = Vector3.ProjectOnPlane(base.PlayerCamera.forward, planeNormal);
			Vector3 to = Vector3.ProjectOnPlane((target - base.PlayerCamera.position).normalized, planeNormal);
			return Vector3.SignedAngle(from, to, planeNormal);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BD5 File Offset: 0x00000DD5
		private void SubscribeToTargetSelectorEvents()
		{
			base.OnTargetLost.AddListener(new UnityAction<AimAssistTarget>(this.ResetEaseOut));
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BEE File Offset: 0x00000DEE
		private void SetUpTimeAccumulator()
		{
			this.timeAccumulator = this.timeToRegainOriginalInputSensitivity;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BFC File Offset: 0x00000DFC
		private void ResetEaseOut(AimAssistTarget target)
		{
			this.timeAccumulator = 0f;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C09 File Offset: 0x00000E09
		private void TearDownTargetSelectorEvents()
		{
			if (base.OnTargetLost != null)
			{
				base.OnTargetLost.RemoveListener(new UnityAction<AimAssistTarget>(this.ResetEaseOut));
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002C2A File Offset: 0x00000E2A
		public AutoAim()
		{
		}

		// Token: 0x04000028 RID: 40
		[Header("Ease In")]
		[Tooltip("The radius of the center of the target in metres, where the adjustment is not active.")]
		[Min(0.001f)]
		public float deadzoneRadius = 0.01f;

		// Token: 0x04000029 RID: 41
		[Tooltip("The mix factor between player input and the aim adjustment (0 is raw player input, 1 is raw aim adjustment)")]
		[Range(0f, 1f)]
		public float factor = 0.5f;

		// Token: 0x0400002A RID: 42
		[Tooltip("An angle in degrees to activate the aim assist. If the difference between player input and input needed to aim at the target is larger than this, the aim assist will not interfere, because it assumes the players wants to look away from the target. Without this the player would get stuck at aiming at the target.")]
		[Range(1f, 120f)]
		public float aimAngleThreshold = 120f;

		// Token: 0x0400002B RID: 43
		[Header("Ease Out")]
		[Tooltip("The multiplier that slows down the aim of the player when they look away from the target. Helps with overshoot when they look away from the target.")]
		[Range(0.01f, 1f)]
		public float aimEaseOutDampeningMultiplier = 0.6f;

		// Token: 0x0400002C RID: 44
		[Tooltip("The time in seconds to regain the original input sensitivity after leaving the target.Helps get rid of unnatural, robotic stutter from the aim.")]
		[Min(0.01f)]
		public float timeToRegainOriginalInputSensitivity = 0.5f;

		// Token: 0x0400002D RID: 45
		private float timeAccumulator;
	}
}
