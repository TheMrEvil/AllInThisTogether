using System;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Numerics;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Model;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.AimAssists
{
	// Token: 0x02000011 RID: 17
	public class AimLock : AimAssistBase
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002768 File Offset: 0x00000968
		public AimAssistResult AssistAim()
		{
			if (!this.aimAssistEnabled)
			{
				return AimAssistResult.Empty;
			}
			AimAssistTarget target = base.Target;
			if (!target || this.IsTargetBelowPlayer(target))
			{
				return AimAssistResult.Empty;
			}
			Vector3 position = target.transform.position;
			float totalRotation = this.CalculateTotalRotationAngles(Vector3.up, position);
			float totalRotation2 = this.CalculateTotalRotationAngles(base.PlayerCamera.right, position);
			Vector3 playerAimToTargetLocal = this.CalculatePlayerAimToTargetLocal(position);
			float num = this.CalculateDeltaRotationDegrees(totalRotation2, this.verticalTimeToAim, Time.deltaTime, position);
			float num2 = this.CalculateDeltaRotationDegrees(totalRotation, this.horizontalTimeToAim, Time.deltaTime, position);
			if (this.enableAngularVelocityCurve)
			{
				num *= this.SampleCurveForVertical(playerAimToTargetLocal);
				num2 *= this.SampleCurveForHorizontal(playerAimToTargetLocal);
			}
			float pitchAdditionInDegrees = num.Sanitized();
			return new AimAssistResult(num2.Sanitized(), num2.Sanitized() * Vector3.up, pitchAdditionInDegrees);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000284C File Offset: 0x00000A4C
		private Vector3 CalculatePlayerAimToTargetLocal(Vector3 target)
		{
			Vector3 a = target - base.PlayerCamera.position;
			Vector3 b = base.PlayerCamera.forward * a.magnitude;
			Vector3 vector = a - b;
			return base.PlayerCamera.InverseTransformVector(vector);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002898 File Offset: 0x00000A98
		private bool IsTargetBelowPlayer(AimAssistTarget target)
		{
			return Mathf.Acos(Vector3.Dot(Vector3.down, (target.transform.position - base.PlayerCamera.position).normalized)) * 57.29578f < 15f;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028E4 File Offset: 0x00000AE4
		private float CalculateDeltaRotationDegrees(float totalRotation, float timeToAim, float deltaTime, Vector3 target)
		{
			float num = timeToAim * base.AimAssistRadius;
			float magnitude = (target - base.PlayerCamera.transform.position).magnitude;
			return Mathf.Min(Mathf.Atan2(1f, magnitude) * 57.29578f / num * deltaTime, Mathf.Abs(totalRotation) * 0.85f) * Mathf.Sign(totalRotation);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002948 File Offset: 0x00000B48
		private float SampleCurveForHorizontal(Vector3 playerAimToTargetLocal)
		{
			float time = Mathf.Abs(playerAimToTargetLocal.x) / base.AimAssistRadius;
			return this.angularVelocityCurve.Evaluate(time);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002974 File Offset: 0x00000B74
		private float SampleCurveForVertical(Vector3 playerAimToTargetLocal)
		{
			float time = Mathf.Abs(playerAimToTargetLocal.y) / base.AimAssistRadius;
			return this.angularVelocityCurve.Evaluate(time);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029A0 File Offset: 0x00000BA0
		private float CalculateTotalRotationAngles(Vector3 planeNormal, Vector3 target)
		{
			Vector3 from = Vector3.ProjectOnPlane(base.PlayerCamera.forward, planeNormal);
			Vector3 to = Vector3.ProjectOnPlane((target - base.PlayerCamera.position).normalized, planeNormal);
			return Vector3.SignedAngle(from, to, planeNormal);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029E5 File Offset: 0x00000BE5
		public AimLock()
		{
		}

		// Token: 0x04000022 RID: 34
		private const float AngleThresholdBelowTarget = 15f;

		// Token: 0x04000023 RID: 35
		private const float UnderAimMultiplier = 0.85f;

		// Token: 0x04000024 RID: 36
		[Header("Timings")]
		[Tooltip("How much time it should take to get from the edge of the aim assist to the center of the target, on horizontal axis.")]
		public float horizontalTimeToAim = 2f;

		// Token: 0x04000025 RID: 37
		[Tooltip("How much time it should take to get from the edge of the aim assist to the center of the target, on vertical axis.")]
		public float verticalTimeToAim = 1f;

		// Token: 0x04000026 RID: 38
		[Header("Smooth aimlock")]
		[Tooltip("Enables or disables the angular velocity dampening curve.")]
		public bool enableAngularVelocityCurve = true;

		// Token: 0x04000027 RID: 39
		[Tooltip("Angular velocity curve to multiply the aim assist with. Values closer to 0 refer to the crosshair being close to the target, e.g. looking at its center.")]
		public AnimationCurve angularVelocityCurve;
	}
}
