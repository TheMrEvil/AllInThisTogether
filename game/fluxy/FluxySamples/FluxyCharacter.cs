using System;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x02000017 RID: 23
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class FluxyCharacter : MonoBehaviour
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00005FFC File Offset: 0x000041FC
		private void Start()
		{
			Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Ignore Raycast"), true);
			this.m_Animator = base.GetComponent<Animator>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_Capsule = base.GetComponent<CapsuleCollider>();
			this.m_CapsuleHeight = this.m_Capsule.height;
			this.m_CapsuleCenter = this.m_Capsule.center;
			this.m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			this.m_OrigGroundCheckDistance = this.m_GroundCheckDistance;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006084 File Offset: 0x00004284
		public void Move(Vector3 move, bool crouch, bool jump)
		{
			if (move.magnitude > 1f)
			{
				move.Normalize();
			}
			move = base.transform.InverseTransformDirection(move);
			this.CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, this.m_GroundNormal);
			this.m_TurnAmount = Mathf.Atan2(move.x, move.z);
			this.m_ForwardAmount = move.z;
			this.ApplyExtraTurnRotation();
			if (this.m_IsGrounded)
			{
				this.HandleGroundedMovement(crouch, jump);
			}
			else
			{
				this.HandleAirborneMovement();
			}
			this.ScaleCapsuleForCrouching(crouch);
			this.PreventStandingInLowHeadroom();
			this.UpdateAnimator(move);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00006120 File Offset: 0x00004320
		private void ScaleCapsuleForCrouching(bool crouch)
		{
			if (this.m_IsGrounded && crouch)
			{
				if (this.m_Crouching)
				{
					return;
				}
				this.m_Capsule.height = this.m_Capsule.height / 2f;
				this.m_Capsule.center = this.m_Capsule.center / 2f;
				this.m_Crouching = true;
				return;
			}
			else
			{
				Ray ray = new Ray(this.m_Rigidbody.position + Vector3.up * this.m_Capsule.radius * 0.5f, Vector3.up);
				float maxDistance = this.m_CapsuleHeight - this.m_Capsule.radius * 0.5f;
				if (Physics.SphereCast(ray, this.m_Capsule.radius * 0.5f, maxDistance, -5, QueryTriggerInteraction.Ignore))
				{
					this.m_Crouching = true;
					return;
				}
				this.m_Capsule.height = this.m_CapsuleHeight;
				this.m_Capsule.center = this.m_CapsuleCenter;
				this.m_Crouching = false;
				return;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00006224 File Offset: 0x00004424
		private void PreventStandingInLowHeadroom()
		{
			if (!this.m_Crouching)
			{
				Ray ray = new Ray(this.m_Rigidbody.position + Vector3.up * this.m_Capsule.radius * 0.5f, Vector3.up);
				float maxDistance = this.m_CapsuleHeight - this.m_Capsule.radius * 0.5f;
				if (Physics.SphereCast(ray, this.m_Capsule.radius * 0.5f, maxDistance, -5, QueryTriggerInteraction.Ignore))
				{
					this.m_Crouching = true;
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000062B0 File Offset: 0x000044B0
		private void UpdateAnimator(Vector3 move)
		{
			this.m_Animator.SetFloat("Forward", this.m_ForwardAmount, 0.1f, Time.deltaTime);
			this.m_Animator.SetFloat("Turn", this.m_TurnAmount, 0.1f, Time.deltaTime);
			this.m_Animator.SetBool("Crouch", this.m_Crouching);
			this.m_Animator.SetBool("OnGround", this.m_IsGrounded);
			if (!this.m_IsGrounded)
			{
				this.m_Animator.SetFloat("Jump", this.m_Rigidbody.velocity.y);
			}
			float value = (float)((Mathf.Repeat(this.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + this.m_RunCycleLegOffset, 1f) < 0.5f) ? 1 : -1) * this.m_ForwardAmount;
			if (this.m_IsGrounded)
			{
				this.m_Animator.SetFloat("JumpLeg", value);
			}
			if (this.m_IsGrounded && move.magnitude > 0f)
			{
				this.m_Animator.speed = this.m_AnimSpeedMultiplier;
				return;
			}
			this.m_Animator.speed = 1f;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000063DC File Offset: 0x000045DC
		private void HandleAirborneMovement()
		{
			Vector3 force = Physics.gravity * this.m_GravityMultiplier - Physics.gravity;
			this.m_Rigidbody.AddForce(force);
			this.m_GroundCheckDistance = ((this.m_Rigidbody.velocity.y < 0f) ? this.m_OrigGroundCheckDistance : 0.01f);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000643C File Offset: 0x0000463C
		private void HandleGroundedMovement(bool crouch, bool jump)
		{
			if (jump && !crouch && this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				this.m_Rigidbody.velocity = new Vector3(this.m_Rigidbody.velocity.x, this.m_JumpPower, this.m_Rigidbody.velocity.z);
				this.m_IsGrounded = false;
				this.m_Animator.applyRootMotion = false;
				this.m_GroundCheckDistance = 0.1f;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000064C0 File Offset: 0x000046C0
		private void ApplyExtraTurnRotation()
		{
			float num = Mathf.Lerp(this.m_StationaryTurnSpeed, this.m_MovingTurnSpeed, this.m_ForwardAmount);
			base.transform.Rotate(0f, this.m_TurnAmount * num * Time.deltaTime, 0f);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006508 File Offset: 0x00004708
		public void OnAnimatorMove()
		{
			if (this.m_IsGrounded)
			{
				Vector3 velocity = this.m_Animator.deltaPosition * this.m_MoveSpeedMultiplier / Time.deltaTime;
				velocity.y = this.m_Rigidbody.velocity.y;
				this.m_Rigidbody.velocity = velocity;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00006564 File Offset: 0x00004764
		private void CheckGroundStatus()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 0.1f, Vector3.down, out raycastHit, this.m_GroundCheckDistance, -5))
			{
				this.m_GroundNormal = raycastHit.normal;
				this.m_IsGrounded = true;
				this.m_Animator.applyRootMotion = true;
				return;
			}
			this.m_IsGrounded = false;
			this.m_GroundNormal = Vector3.up;
			this.m_Animator.applyRootMotion = false;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000065E8 File Offset: 0x000047E8
		public FluxyCharacter()
		{
		}

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private float m_MovingTurnSpeed = 360f;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private float m_StationaryTurnSpeed = 180f;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private float m_JumpPower = 12f;

		// Token: 0x040000A9 RID: 169
		[Range(1f, 4f)]
		[SerializeField]
		private float m_GravityMultiplier = 2f;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private float m_RunCycleLegOffset = 0.2f;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private float m_MoveSpeedMultiplier = 1f;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		private float m_AnimSpeedMultiplier = 1f;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private float m_GroundCheckDistance = 0.1f;

		// Token: 0x040000AE RID: 174
		private Rigidbody m_Rigidbody;

		// Token: 0x040000AF RID: 175
		private Animator m_Animator;

		// Token: 0x040000B0 RID: 176
		private bool m_IsGrounded;

		// Token: 0x040000B1 RID: 177
		private float m_OrigGroundCheckDistance;

		// Token: 0x040000B2 RID: 178
		private const float k_Half = 0.5f;

		// Token: 0x040000B3 RID: 179
		private float m_TurnAmount;

		// Token: 0x040000B4 RID: 180
		private float m_ForwardAmount;

		// Token: 0x040000B5 RID: 181
		private Vector3 m_GroundNormal;

		// Token: 0x040000B6 RID: 182
		private float m_CapsuleHeight;

		// Token: 0x040000B7 RID: 183
		private Vector3 m_CapsuleCenter;

		// Token: 0x040000B8 RID: 184
		private CapsuleCollider m_Capsule;

		// Token: 0x040000B9 RID: 185
		private bool m_Crouching;
	}
}
