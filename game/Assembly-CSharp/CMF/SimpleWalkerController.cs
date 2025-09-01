using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A8 RID: 936
	public class SimpleWalkerController : Controller
	{
		// Token: 0x06001EF6 RID: 7926 RVA: 0x000B9432 File Offset: 0x000B7632
		private void Start()
		{
			this.tr = base.transform;
			this.mover = base.GetComponent<Mover>();
			this.characterInput = base.GetComponent<CharacterInput>();
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000B9458 File Offset: 0x000B7658
		private void FixedUpdate()
		{
			this.mover.CheckForGround();
			if (!this.isGrounded && this.mover.IsGrounded())
			{
				this.OnGroundContactRegained(this.lastVelocity);
			}
			this.isGrounded = this.mover.IsGrounded();
			Vector3 vector = Vector3.zero;
			vector += this.CalculateMovementDirection() * this.movementSpeed;
			if (!this.isGrounded)
			{
				this.currentVerticalSpeed -= this.gravity * Time.deltaTime;
			}
			else if (this.currentVerticalSpeed <= 0f)
			{
				this.currentVerticalSpeed = 0f;
			}
			if (this.characterInput != null && this.isGrounded && this.characterInput.IsJumpKeyPressed())
			{
				this.OnJumpStart();
				this.currentVerticalSpeed = this.jumpSpeed;
				this.isGrounded = false;
			}
			vector += this.tr.up * this.currentVerticalSpeed;
			this.lastVelocity = vector;
			this.mover.SetExtendSensorRange(this.isGrounded);
			this.mover.SetVelocity(vector);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000B9578 File Offset: 0x000B7778
		private Vector3 CalculateMovementDirection()
		{
			if (this.characterInput == null)
			{
				return Vector3.zero;
			}
			Vector3 vector = Vector3.zero;
			if (this.cameraTransform == null)
			{
				vector += this.tr.right * this.characterInput.GetHorizontalMovementInput();
				vector += this.tr.forward * this.characterInput.GetVerticalMovementInput();
			}
			else
			{
				vector += Vector3.ProjectOnPlane(this.cameraTransform.right, this.tr.up).normalized * this.characterInput.GetHorizontalMovementInput();
				vector += Vector3.ProjectOnPlane(this.cameraTransform.forward, this.tr.up).normalized * this.characterInput.GetVerticalMovementInput();
			}
			if (vector.magnitude > 1f)
			{
				vector.Normalize();
			}
			return vector;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000B967D File Offset: 0x000B787D
		private void OnGroundContactRegained(Vector3 _collisionVelocity)
		{
			if (this.OnLand != null)
			{
				this.OnLand(_collisionVelocity, this.mover.lastContactPoint, this.mover.surfaceNormal);
			}
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000B96A9 File Offset: 0x000B78A9
		private void OnJumpStart()
		{
			if (this.OnJump != null)
			{
				this.OnJump(this.lastVelocity);
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000B96C4 File Offset: 0x000B78C4
		public override Vector3 GetVelocity()
		{
			return this.lastVelocity;
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000B96CC File Offset: 0x000B78CC
		public override Vector3 GetMovementVelocity()
		{
			return this.lastVelocity;
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000B96D4 File Offset: 0x000B78D4
		public override bool IsGrounded()
		{
			return this.isGrounded;
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000B96DC File Offset: 0x000B78DC
		public SimpleWalkerController()
		{
		}

		// Token: 0x04001F3B RID: 7995
		private Mover mover;

		// Token: 0x04001F3C RID: 7996
		private float currentVerticalSpeed;

		// Token: 0x04001F3D RID: 7997
		private bool isGrounded;

		// Token: 0x04001F3E RID: 7998
		public float movementSpeed = 7f;

		// Token: 0x04001F3F RID: 7999
		public float jumpSpeed = 10f;

		// Token: 0x04001F40 RID: 8000
		public float gravity = 10f;

		// Token: 0x04001F41 RID: 8001
		private Vector3 lastVelocity = Vector3.zero;

		// Token: 0x04001F42 RID: 8002
		public Transform cameraTransform;

		// Token: 0x04001F43 RID: 8003
		private CharacterInput characterInput;

		// Token: 0x04001F44 RID: 8004
		private Transform tr;
	}
}
