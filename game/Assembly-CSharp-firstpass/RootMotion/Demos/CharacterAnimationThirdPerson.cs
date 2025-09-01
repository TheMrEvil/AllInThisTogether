using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200015B RID: 347
	public class CharacterAnimationThirdPerson : CharacterAnimationBase
	{
		// Token: 0x06000D68 RID: 3432 RVA: 0x0005A723 File Offset: 0x00058923
		protected override void Start()
		{
			base.Start();
			this.animator = base.GetComponent<Animator>();
			this.lastForward = base.transform.forward;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0005A748 File Offset: 0x00058948
		public override Vector3 GetPivotPoint()
		{
			return this.animator.pivotPosition;
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0005A758 File Offset: 0x00058958
		public override bool animationGrounded
		{
			get
			{
				return this.animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded Directional") || this.animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded Strafe");
			}
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0005A79C File Offset: 0x0005899C
		protected virtual void Update()
		{
			if (Time.deltaTime == 0f)
			{
				return;
			}
			this.animatePhysics = (this.animator.updateMode == AnimatorUpdateMode.AnimatePhysics);
			if (this.characterController.animState.jump && !this.lastJump)
			{
				float value = (float)((Mathf.Repeat(this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime + this.runCycleLegOffset, 1f) < 0.5f) ? 1 : -1) * this.characterController.animState.moveDirection.z;
				this.animator.SetFloat("JumpLeg", value);
			}
			this.lastJump = this.characterController.animState.jump;
			float num = -base.GetAngleFromForward(this.lastForward) - this.deltaAngle;
			this.deltaAngle = 0f;
			this.lastForward = base.transform.forward;
			num *= this.turnSensitivity * 0.01f;
			num = Mathf.Clamp(num / Time.deltaTime, -1f, 1f);
			this.animator.SetFloat("Turn", Mathf.Lerp(this.animator.GetFloat("Turn"), num, Time.deltaTime * this.turnSpeed));
			this.animator.SetFloat("Forward", this.characterController.animState.moveDirection.z);
			this.animator.SetFloat("Right", this.characterController.animState.moveDirection.x);
			this.animator.SetBool("Crouch", this.characterController.animState.crouch);
			this.animator.SetBool("OnGround", this.characterController.animState.onGround);
			this.animator.SetBool("IsStrafing", this.characterController.animState.isStrafing);
			if (!this.characterController.animState.onGround)
			{
				this.animator.SetFloat("Jump", this.characterController.animState.yVelocity);
			}
			if (this.characterController.doubleJumpEnabled)
			{
				this.animator.SetBool("DoubleJump", this.characterController.animState.doubleJump);
			}
			this.characterController.animState.doubleJump = false;
			if (this.characterController.animState.onGround && this.characterController.animState.moveDirection.z > 0f)
			{
				this.animator.speed = this.animSpeedMultiplier;
				return;
			}
			this.animator.speed = 1f;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0005AA48 File Offset: 0x00058C48
		private void OnAnimatorMove()
		{
			Vector3 vector = this.animator.deltaRotation * Vector3.forward;
			this.deltaAngle += Mathf.Atan2(vector.x, vector.z) * 57.29578f;
			this.characterController.Move(this.animator.deltaPosition, this.animator.deltaRotation);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0005AAB0 File Offset: 0x00058CB0
		public CharacterAnimationThirdPerson()
		{
		}

		// Token: 0x04000B2A RID: 2858
		public CharacterThirdPerson characterController;

		// Token: 0x04000B2B RID: 2859
		[SerializeField]
		private float turnSensitivity = 0.2f;

		// Token: 0x04000B2C RID: 2860
		[SerializeField]
		private float turnSpeed = 5f;

		// Token: 0x04000B2D RID: 2861
		[SerializeField]
		private float runCycleLegOffset = 0.2f;

		// Token: 0x04000B2E RID: 2862
		[Range(0.1f, 3f)]
		[SerializeField]
		private float animSpeedMultiplier = 1f;

		// Token: 0x04000B2F RID: 2863
		protected Animator animator;

		// Token: 0x04000B30 RID: 2864
		private Vector3 lastForward;

		// Token: 0x04000B31 RID: 2865
		private const string groundedDirectional = "Grounded Directional";

		// Token: 0x04000B32 RID: 2866
		private const string groundedStrafe = "Grounded Strafe";

		// Token: 0x04000B33 RID: 2867
		private float deltaAngle;

		// Token: 0x04000B34 RID: 2868
		private float jumpLeg;

		// Token: 0x04000B35 RID: 2869
		private bool lastJump;
	}
}
