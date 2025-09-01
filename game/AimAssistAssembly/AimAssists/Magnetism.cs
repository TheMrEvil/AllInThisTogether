using System;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Controls;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Info;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Helper.Numerics;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Model;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.Target;
using UnityEngine;

namespace Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.AimAssists
{
	// Token: 0x02000013 RID: 19
	public class Magnetism : AimAssistBase
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002C69 File Offset: 0x00000E69
		private void Start()
		{
			this.SetUpPlayerPhysicsInfo();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C74 File Offset: 0x00000E74
		public AimAssistResult AssistAim(Vector2 moveInputDelta)
		{
			AimAssistTarget target = base.Target;
			if (!target || this.horizontalSmoothnessAwayFromTarget < 1f || !this.aimAssistEnabled)
			{
				return AimAssistResult.Empty;
			}
			this.lerpDistance = Mathf.Clamp(this.lerpDistance, 0.01f, base.AimAssistRadius);
			bool strafeTowardsTarget = this.IsPlayerMovingTowardsTarget(target, moveInputDelta.x);
			float smoothness = this.CalculateHorizontalSmoothness(moveInputDelta, target, strafeTowardsTarget);
			float f = (!this.MovementInputZero(moveInputDelta)) ? this.CalculateHorizontalAssist(moveInputDelta, target, smoothness) : 0f;
			float f2 = (this.verticalCompensation && !this.VerticalVelocityZero()) ? this.CalculateVerticalAssist(target) : 0f;
			float rotationAdditionInDegrees = f.Sanitized();
			float pitchAdditionInDegrees = f2.Sanitized();
			return new AimAssistResult(rotationAdditionInDegrees, f.Sanitized() * Vector3.up, pitchAdditionInDegrees);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D40 File Offset: 0x00000F40
		private float CalculateHorizontalSmoothness(Vector2 moveInputDelta, AimAssistTarget target, bool strafeTowardsTarget)
		{
			Vector3 position = target.transform.position;
			Vector3 right = base.PlayerCamera.right;
			float magnitude = (position - base.PlayerCamera.transform.position).magnitude;
			Vector3 vector = base.PlayerCamera.forward * magnitude + base.PlayerCamera.position;
			Vector3 vector2 = position - right * (this.lerpDistance / 2f);
			Vector3 vector3 = position + right * (this.lerpDistance / 2f);
			float factor = (vector.x - vector2.x) / this.lerpDistance;
			if (!vector.x.Between(vector2.x, vector3.x))
			{
				return this.SelectSmoothness(strafeTowardsTarget);
			}
			return this.LerpSmoothness(moveInputDelta, factor);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E19 File Offset: 0x00001019
		private float SelectSmoothness(bool strafeTowardsTarget)
		{
			if (!strafeTowardsTarget)
			{
				return this.horizontalSmoothnessAwayFromTarget;
			}
			return this.horizontalSmoothnessTowardsTarget;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E2B File Offset: 0x0000102B
		private float LerpSmoothness(Vector2 moveInputDelta, float factor)
		{
			return Mathf.Lerp(this.horizontalSmoothnessAwayFromTarget, this.horizontalSmoothnessTowardsTarget, (moveInputDelta.x > 0f) ? (1f - factor) : factor);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E55 File Offset: 0x00001055
		private bool VerticalVelocityZero()
		{
			return this.playerPhysics.Velocity.y.EqualsApprox(0f, 0.01f);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E76 File Offset: 0x00001076
		private bool MovementInputZero(Vector2 moveInputDelta)
		{
			return moveInputDelta.EqualsApprox(Vector2.zero, 0.01f);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E88 File Offset: 0x00001088
		private float CalculateVerticalAssist(AimAssistTarget target)
		{
			return this.CalculateAssistPerAxis(target, base.PlayerCamera.right) / this.verticalSmoothness * Mathf.Sign(this.playerPhysics.Velocity.y);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002EB9 File Offset: 0x000010B9
		private float CalculateHorizontalAssist(Vector2 moveInputDelta, AimAssistTarget target, float smoothness)
		{
			return this.CalculateAssistPerAxis(target, base.PlayerCamera.up) / smoothness * -Mathf.Sign(moveInputDelta.x);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EDC File Offset: 0x000010DC
		private bool IsPlayerMovingTowardsTarget(AimAssistTarget target, float xInputDelta)
		{
			return Mathf.RoundToInt(Mathf.Sign(base.PlayerCamera.InverseTransformPoint(target.transform.position).x)) == Mathf.RoundToInt(Mathf.Sign(xInputDelta));
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002F10 File Offset: 0x00001110
		private float CalculateAssistPerAxis(AimAssistTarget target, Vector3 axis)
		{
			Vector3 point = target.transform.position - base.PlayerCamera.position;
			Vector3 rhs = Quaternion.Euler(axis * 90f) * point;
			float d = Vector3.Dot(this.playerPhysics.Velocity, rhs) / (this.playerPhysics.Velocity.magnitude * rhs.magnitude);
			return Mathf.Atan((this.playerPhysics.Velocity * d).magnitude * Time.deltaTime / point.magnitude) * 57.29578f;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002FB0 File Offset: 0x000011B0
		private void SetUpPlayerPhysicsInfo()
		{
			if (this.playerBody)
			{
				this.playerPhysics = new RigidbodyInfo(this.playerBody);
				return;
			}
			if (this.playerController)
			{
				this.playerPhysics = new CharacterControllerInfo(this.playerController);
				return;
			}
			throw new MissingComponentException("Magnetism needs either a Rigidbody or a CharacterController set via Inspector.");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003005 File Offset: 0x00001205
		public Magnetism()
		{
		}

		// Token: 0x0400002E RID: 46
		[HideInInspector]
		public PlayerControlType controlType;

		// Token: 0x0400002F RID: 47
		[HideInInspector]
		public Rigidbody playerBody;

		// Token: 0x04000030 RID: 48
		[HideInInspector]
		public CharacterController playerController;

		// Token: 0x04000031 RID: 49
		private IPlayerPhysicsInfo playerPhysics;

		// Token: 0x04000032 RID: 50
		[Header("Horizontal strafing compensation")]
		[Tooltip("A divisor for the player's strafe movement when they are moving away from the target.")]
		[Min(1.08f)]
		public float horizontalSmoothnessAwayFromTarget = 1.09f;

		// Token: 0x04000033 RID: 51
		[Tooltip("A divisor for the player's strafe movement when they are strafing towards the target. To prevent turning the player away from the target during mirror strafing, it has to be greater than smoothness away from target.")]
		[Min(1.08f)]
		public float horizontalSmoothnessTowardsTarget = 2f;

		// Token: 0x04000034 RID: 52
		[Tooltip("In metres, to avoid stutter when switching immediately between to and away horizontal strafe smoothness this lerps the change over a distance. Has to be less than aim assist radius.")]
		[Min(0.01f)]
		public float lerpDistance = 0.1f;

		// Token: 0x04000035 RID: 53
		[Header("Vertical compensation")]
		[Tooltip("If enabled the assist will compensate for player jumping by tracking the target vertically, smoothed out by a factor.")]
		public bool verticalCompensation;

		// Token: 0x04000036 RID: 54
		[Tooltip("A divisor for the player's pitch to be compensated against.")]
		[Min(1.08f)]
		public float verticalSmoothness = 1.15f;
	}
}
