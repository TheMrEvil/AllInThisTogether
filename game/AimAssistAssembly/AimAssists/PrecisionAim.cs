using System;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target;
using UnityEngine;
using UnityEngine.Events;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.AimAssists
{
	// Token: 0x02000014 RID: 20
	public class PrecisionAim : AimAssistBase
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003039 File Offset: 0x00001239
		protected override void Awake()
		{
			base.Awake();
			this.SubscribeToTargetSelectorEvents();
			this.SetUpTimeAccumulator();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000304D File Offset: 0x0000124D
		private void OnDestroy()
		{
			this.TearDownTargetSelectorEvents();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003058 File Offset: 0x00001258
		public Vector2 AssistAim(Vector2 lookInputDelta)
		{
			AimAssistTarget target = base.Target;
			if (!this.aimAssistEnabled)
			{
				return lookInputDelta;
			}
			if (this.sensitivityMultiplierAtEdge < this.sensitivityMultiplierAtCenter)
			{
				this.sensitivityMultiplierAtEdge = this.sensitivityMultiplierAtCenter;
			}
			if (target == null)
			{
				return this.LerpEaseOut(lookInputDelta);
			}
			if (this.TargetBelowPlayer(target))
			{
				return lookInputDelta;
			}
			Vector3 playerAimToTargetLocal = this.CalculatePlayerAimToTargetLocal(target.transform.position);
			float num = this.SampleCurveForVertical(playerAimToTargetLocal);
			return new Vector2(this.SampleCurveForHorizontal(playerAimToTargetLocal) * lookInputDelta.x, num * lookInputDelta.y);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000030E4 File Offset: 0x000012E4
		private bool TargetBelowPlayer(AimAssistTarget target)
		{
			return Mathf.Acos(Vector3.Dot(Vector3.down, (target.transform.position - base.PlayerCamera.position).normalized)) * 57.29578f < 15f;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003130 File Offset: 0x00001330
		private Vector3 CalculatePlayerAimToTargetLocal(Vector3 target)
		{
			Vector3 a = target - base.PlayerCamera.position;
			Vector3 b = base.PlayerCamera.forward * a.magnitude;
			Vector3 vector = a - b;
			return base.PlayerCamera.InverseTransformVector(vector);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000317C File Offset: 0x0000137C
		private float SampleCurveForHorizontal(Vector3 playerAimToTargetLocal)
		{
			float factor = Mathf.Abs(playerAimToTargetLocal.x) / base.AimAssistRadius;
			return this.CalculatePlayerAimToTargetMultiplier(factor);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031A4 File Offset: 0x000013A4
		private float SampleCurveForVertical(Vector3 playerAimToTargetLocal)
		{
			float factor = Mathf.Abs(playerAimToTargetLocal.y) / base.AimAssistRadius;
			return this.CalculatePlayerAimToTargetMultiplier(factor);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000031CB File Offset: 0x000013CB
		private float CalculatePlayerAimToTargetMultiplier(float factor)
		{
			return Mathf.Lerp(this.sensitivityMultiplierAtCenter, 1f, factor / base.AimAssistRadius);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000031E8 File Offset: 0x000013E8
		private Vector2 LerpEaseOut(Vector2 lookInputDelta)
		{
			this.timeAccumulator = Mathf.Min(this.timeAccumulator + Time.deltaTime, this.timeToRegainOriginalInputSensitivity);
			return Mathf.Lerp(this.sensitivityMultiplierAtEdge, 1f, this.timeAccumulator / this.timeToRegainOriginalInputSensitivity) * lookInputDelta;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003235 File Offset: 0x00001435
		private void SubscribeToTargetSelectorEvents()
		{
			base.OnTargetLost.AddListener(new UnityAction<AimAssistTarget>(this.ResetEaseOut));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000324E File Offset: 0x0000144E
		private void SetUpTimeAccumulator()
		{
			this.timeAccumulator = this.timeToRegainOriginalInputSensitivity;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000325C File Offset: 0x0000145C
		private void ResetEaseOut(AimAssistTarget target)
		{
			this.timeAccumulator = 0f;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003269 File Offset: 0x00001469
		private void TearDownTargetSelectorEvents()
		{
			if (base.OnTargetLost != null)
			{
				base.OnTargetLost.RemoveListener(new UnityAction<AimAssistTarget>(this.ResetEaseOut));
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000328A File Offset: 0x0000148A
		public PrecisionAim()
		{
		}

		// Token: 0x04000037 RID: 55
		private const float AngleThresholdBelowTarget = 15f;

		// Token: 0x04000038 RID: 56
		[Header("Sensitivity")]
		[Tooltip("The sensitivity multiplier at the center of the aim assist. This will be lerped from the outer edge of the radius.")]
		[Range(0.001f, 0.99f)]
		public float sensitivityMultiplierAtCenter = 0.18f;

		// Token: 0x04000039 RID: 57
		[Tooltip("The sensitivity multiplier at the edge of the aim assist. This will be eased out back to the original sensitivity when the assist loses the target. Has to be more than center multiplier (or will be set to center multiplier).")]
		[Range(0.1f, 0.99f)]
		public float sensitivityMultiplierAtEdge = 0.5f;

		// Token: 0x0400003A RID: 58
		[Header("Ease Out")]
		[Tooltip("The time in seconds to regain the original input sensitivity after leaving the target.Helps get rid of unnatural, robotic stutter from the aim.")]
		[Min(0.01f)]
		public float timeToRegainOriginalInputSensitivity = 0.5f;

		// Token: 0x0400003B RID: 59
		private float timeAccumulator;
	}
}
