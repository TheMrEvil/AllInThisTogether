using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A5 RID: 933
	public class AdvancedWalkerController : Controller
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x000B8170 File Offset: 0x000B6370
		private float airControlRate
		{
			get
			{
				if (this.controller == null)
				{
					return this.airControl;
				}
				return Mathf.Max(0f, this.controller.GetPassiveMod(Passive.EntityValue.P_AirControl, this.airControl));
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000B81A7 File Offset: 0x000B63A7
		private float acceleration
		{
			get
			{
				if (this.controller == null)
				{
					return this.Acceleration;
				}
				return Mathf.Max(0f, this.controller.GetPassiveMod(Passive.EntityValue.P_Acceleration, this.Acceleration));
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x000B81DE File Offset: 0x000B63DE
		private float jumpSpeed
		{
			get
			{
				if (this.controller == null)
				{
					return this.JumpHeight;
				}
				return Mathf.Max(0f, this.controller.GetPassiveMod(Passive.EntityValue.P_JumpHeight, this.JumpHeight));
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x000B8215 File Offset: 0x000B6415
		private int airJumps
		{
			get
			{
				if (this.controller == null)
				{
					return 0;
				}
				return (int)Mathf.Max(0f, this.controller.GetPassiveMod(Passive.EntityValue.P_AirJumps, 0f));
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x000B8247 File Offset: 0x000B6447
		private float glideStrength
		{
			get
			{
				if (this.controller == null)
				{
					return 0f;
				}
				return Mathf.Max(0f, this.controller.GetPassiveMod(Passive.EntityValue.P_GlideForce, 0f));
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001ECF RID: 7887 RVA: 0x000B827C File Offset: 0x000B647C
		public float airFriction
		{
			get
			{
				if (this.controller == null)
				{
					return this.AirFriction;
				}
				return Mathf.Max(0f, this.frictionMultAir * this.controller.GetPassiveMod(Passive.EntityValue.P_AirFriction, this.AirFriction));
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x000B82BA File Offset: 0x000B64BA
		[SerializeField]
		private float groundFriction
		{
			get
			{
				if (this.controller == null)
				{
					return this.GroundFriction;
				}
				return Mathf.Max(0f, this.frictionMultGround * this.controller.GetPassiveMod(Passive.EntityValue.P_GroundFriction, this.GroundFriction));
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001ED1 RID: 7889 RVA: 0x000B82F8 File Offset: 0x000B64F8
		private float gravity
		{
			get
			{
				return this.controller.GetPassiveMod(Passive.EntityValue.P_Gravity, this.Gravity);
			}
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x000B8310 File Offset: 0x000B6510
		private void Awake()
		{
			this.mover = base.GetComponent<Mover>();
			this.tr = base.transform;
			this.characterInput = base.GetComponentInParent<PlayerInput>();
			this.sync = base.GetComponentInParent<PlayerNetwork>();
			this.ceilingDetector = base.GetComponent<CeilingDetector>();
			this.movement = base.GetComponentInParent<PlayerMovement>();
			this.controller = base.GetComponentInParent<PlayerControl>();
			if (this.characterInput == null)
			{
				Debug.LogWarning("No character input script has been attached to this gameobject", base.gameObject);
			}
			this.Setup();
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x000B8395 File Offset: 0x000B6595
		protected virtual void Setup()
		{
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x000B8397 File Offset: 0x000B6597
		private void Update()
		{
			this.HandleJumpKeyInput();
			if (this.characterInput == null)
			{
				this.characterInput = base.GetComponentInParent<PlayerInput>();
			}
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x000B83BC File Offset: 0x000B65BC
		private void HandleJumpKeyInput()
		{
			bool flag = this.IsJumpKeyPressed();
			if (!this.jumpKeyIsPressed && flag)
			{
				this.jumpKeyWasPressed = true;
			}
			if (this.jumpKeyIsPressed && !flag)
			{
				this.jumpKeyWasLetGo = true;
				this.jumpInputIsLocked = false;
			}
			this.jumpKeyIsPressed = flag;
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x000B8402 File Offset: 0x000B6602
		private void FixedUpdate()
		{
			if (PausePanel.IsGamePaused)
			{
				this.mover.Pause();
				return;
			}
			this.mover.Resume();
			this.ControllerUpdate();
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x000B8428 File Offset: 0x000B6628
		private void ControllerUpdate()
		{
			this.mover.CheckForGround();
			this.mover.CheckColliderAbove();
			this.currentControllerState = this.DetermineControllerState();
			this.HandleMomentum();
			this.HandleJumping();
			Vector3 vector = Vector3.zero;
			if (this.currentControllerState == AdvancedWalkerController.ControllerState.Grounded)
			{
				vector = this.CalculateMovementVelocity();
			}
			Vector3 b = this.momentum;
			if (this.useLocalMomentum)
			{
				b = this.tr.localToWorldMatrix * this.momentum;
			}
			vector += b;
			vector += this.controller.Movement.extraVel;
			this.mover.SetExtendSensorRange(this.IsGrounded());
			this.mover.SetVelocity(vector);
			this.savedVelocity = vector;
			this.savedMovementVelocity = this.CalculateMovementVelocity();
			this.jumpKeyWasLetGo = false;
			this.jumpKeyWasPressed = false;
			if (this.ceilingDetector != null)
			{
				this.ceilingDetector.ResetFlags();
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000B8520 File Offset: 0x000B6720
		protected virtual Vector3 CalculateMovementDirection()
		{
			if (this.characterInput == null)
			{
				return Vector3.zero;
			}
			Vector3 vector = Vector3.zero;
			if (this.cameraTransform == null)
			{
				vector += this.tr.right * this.characterInput.movementAxis.x;
				vector += this.tr.forward * this.characterInput.movementAxis.y;
			}
			else
			{
				vector += Vector3.ProjectOnPlane(this.cameraTransform.right, this.tr.up).normalized * this.characterInput.movementAxis.x;
				vector += Vector3.ProjectOnPlane(this.cameraTransform.forward, this.tr.up).normalized * this.characterInput.movementAxis.y;
			}
			if (vector.magnitude > 1f)
			{
				vector.Normalize();
			}
			return vector;
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x000B863C File Offset: 0x000B683C
		protected virtual Vector3 CalculateMovementVelocity()
		{
			Vector3 vector = this.CalculateMovementDirection();
			Vector3 normalized = vector.normalized;
			if (!this.controller.Movement.AllowedToMove())
			{
				vector = Vector2.zero;
			}
			vector = Vector3.Lerp(this.prevVelocity, vector, Time.fixedDeltaTime * this.acceleration);
			Vector3 normalized2 = this.prevVelocity.normalized;
			this.prevVelocity = vector;
			bool flag = Vector3.Dot(normalized2, normalized) < 0f;
			float num = this.movement.CurrentSpeed;
			if (flag)
			{
				num = Mathf.Max(num, this.movement.BaseSpeed);
			}
			Vector3 forward = this.movement.GetForward();
			float num2 = Vector3.Dot(vector.normalized, forward);
			if (num2 < 0f)
			{
				num = Mathf.Lerp(num, num * this.BackwardsMult, -num2);
			}
			return vector * num;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x000B870C File Offset: 0x000B690C
		protected virtual bool IsJumpKeyPressed()
		{
			return !(this.characterInput == null) && this.characterInput.jumpPressed;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x000B872C File Offset: 0x000B692C
		private AdvancedWalkerController.ControllerState DetermineControllerState()
		{
			bool flag = this.IsRisingOrFalling() && VectorMath.GetDotProduct(this.GetMomentum(), this.tr.up) > 0f;
			bool flag2 = this.mover.IsGrounded() && this.IsGroundTooSteep();
			if (this.currentControllerState == AdvancedWalkerController.ControllerState.Grounded)
			{
				if (flag)
				{
					this.OnGroundContactLost();
					return AdvancedWalkerController.ControllerState.Rising;
				}
				if (!this.mover.IsGrounded())
				{
					this.OnGroundContactLost();
					this.lastGroundedTime = Time.realtimeSinceStartup;
					return AdvancedWalkerController.ControllerState.Falling;
				}
				if (flag2)
				{
					this.OnGroundContactLost();
					return AdvancedWalkerController.ControllerState.Sliding;
				}
				return AdvancedWalkerController.ControllerState.Grounded;
			}
			else if (this.currentControllerState == AdvancedWalkerController.ControllerState.Falling)
			{
				if (flag)
				{
					return AdvancedWalkerController.ControllerState.Rising;
				}
				if (this.mover.IsGrounded() && !flag2)
				{
					this.OnGroundContactRegained();
					return AdvancedWalkerController.ControllerState.Grounded;
				}
				if (flag2)
				{
					return AdvancedWalkerController.ControllerState.Sliding;
				}
				return AdvancedWalkerController.ControllerState.Falling;
			}
			else if (this.currentControllerState == AdvancedWalkerController.ControllerState.Sliding)
			{
				if (flag)
				{
					this.OnGroundContactLost();
					return AdvancedWalkerController.ControllerState.Rising;
				}
				if (!this.mover.IsGrounded())
				{
					this.OnGroundContactLost();
					return AdvancedWalkerController.ControllerState.Falling;
				}
				if (this.mover.IsGrounded() && !flag2)
				{
					this.OnGroundContactRegained();
					return AdvancedWalkerController.ControllerState.Grounded;
				}
				return AdvancedWalkerController.ControllerState.Sliding;
			}
			else if (this.currentControllerState == AdvancedWalkerController.ControllerState.Rising)
			{
				if (!flag)
				{
					if (this.mover.IsGrounded() && !flag2)
					{
						this.OnGroundContactRegained();
						return AdvancedWalkerController.ControllerState.Grounded;
					}
					if (flag2)
					{
						return AdvancedWalkerController.ControllerState.Sliding;
					}
					if (!this.mover.IsGrounded())
					{
						return AdvancedWalkerController.ControllerState.Falling;
					}
				}
				if (this.ceilingDetector != null && this.ceilingDetector.HitCeiling())
				{
					this.OnCeilingContact();
					return AdvancedWalkerController.ControllerState.Falling;
				}
				return AdvancedWalkerController.ControllerState.Rising;
			}
			else
			{
				if (this.currentControllerState != AdvancedWalkerController.ControllerState.Jumping)
				{
					return AdvancedWalkerController.ControllerState.Falling;
				}
				if (Time.time - this.currentJumpStartTime > this.jumpDuration)
				{
					return AdvancedWalkerController.ControllerState.Rising;
				}
				if (this.jumpKeyWasLetGo)
				{
					return AdvancedWalkerController.ControllerState.Rising;
				}
				if (this.ceilingDetector != null && this.ceilingDetector.HitCeiling())
				{
					this.OnCeilingContact();
					return AdvancedWalkerController.ControllerState.Falling;
				}
				return AdvancedWalkerController.ControllerState.Jumping;
			}
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x000B88D8 File Offset: 0x000B6AD8
		private void HandleJumping()
		{
			if ((this.currentControllerState == AdvancedWalkerController.ControllerState.Grounded || Time.realtimeSinceStartup - this.lastGroundedTime < 0.15f) && (this.jumpKeyIsPressed || this.jumpKeyWasPressed) && !this.jumpInputIsLocked && this.jumpSpeed > 0f)
			{
				this.OnGroundContactLost();
				this.OnJumpStart(false);
				this.currentControllerState = AdvancedWalkerController.ControllerState.Jumping;
			}
			if (this.currentControllerState != AdvancedWalkerController.ControllerState.Grounded && (this.jumpKeyIsPressed || this.jumpKeyWasPressed) && !this.jumpInputIsLocked && this.airJumpsUsed < this.airJumps && this.jumpSpeed > 0f)
			{
				this.airJumpsUsed++;
				this.OnJumpStart(true);
			}
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x000B898C File Offset: 0x000B6B8C
		private void HandleMomentum()
		{
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.localToWorldMatrix * this.momentum;
			}
			Vector3 vector = Vector3.zero;
			Vector3 vector2 = Vector3.zero;
			if (this.momentum != Vector3.zero)
			{
				vector = VectorMath.ExtractDotVector(this.momentum, this.tr.up);
				vector2 = this.momentum - vector;
			}
			Vector3 b = this.tr.up * this.gravity * Time.deltaTime;
			if (this.jumpKeyIsPressed && this.currentControllerState == AdvancedWalkerController.ControllerState.Falling && this.glideStrength > 0f && vector.y < -5f)
			{
				b = Vector3.zero;
			}
			vector -= b;
			if (this.currentControllerState == AdvancedWalkerController.ControllerState.Grounded && VectorMath.GetDotProduct(vector, this.tr.up) < 0f)
			{
				vector = Vector3.zero;
			}
			else if (this.currentControllerState != AdvancedWalkerController.ControllerState.Grounded && VectorMath.GetDotProduct(vector, this.tr.up) > 0f && this.mover.IsColliderAbove())
			{
				vector = Vector3.zero;
			}
			if (!this.IsGrounded())
			{
				Vector3 vector3 = this.CalculateMovementVelocity();
				if (vector2.magnitude > this.movement.CurrentSpeed)
				{
					if (VectorMath.GetDotProduct(vector3, vector2.normalized) > 0f)
					{
						vector3 = VectorMath.RemoveDotVector(vector3, vector2.normalized);
					}
					float d = 0.25f;
					vector2 += vector3 * Time.deltaTime * this.airControlRate * d;
				}
				else
				{
					vector2 += vector3 * Time.deltaTime * this.airControlRate;
					vector2 = Vector3.ClampMagnitude(vector2, this.movement.CurrentSpeed);
				}
			}
			if (this.currentControllerState == AdvancedWalkerController.ControllerState.Sliding)
			{
				Vector3 normalized = Vector3.ProjectOnPlane(this.mover.GetGroundNormal(), this.tr.up).normalized;
				Vector3 vector4 = this.CalculateMovementVelocity();
				vector4 = VectorMath.RemoveDotVector(vector4, normalized);
				vector2 += vector4 * Time.fixedDeltaTime;
			}
			if (this.currentControllerState == AdvancedWalkerController.ControllerState.Grounded)
			{
				vector2 = VectorMath.IncrementVectorTowardTargetVector(vector2, this.groundFriction, Time.deltaTime, Vector3.zero);
			}
			else
			{
				vector2 = VectorMath.IncrementVectorTowardTargetVector(vector2, this.airFriction, Time.deltaTime, Vector3.zero);
			}
			this.momentum = vector2 + vector;
			if (this.currentControllerState == AdvancedWalkerController.ControllerState.Sliding)
			{
				this.momentum = Vector3.ProjectOnPlane(this.momentum, this.mover.GetGroundNormal());
				if (VectorMath.GetDotProduct(this.momentum, this.tr.up) > 0f)
				{
					this.momentum = VectorMath.RemoveDotVector(this.momentum, this.tr.up);
				}
				Vector3 normalized2 = Vector3.ProjectOnPlane(-this.tr.up, this.mover.GetGroundNormal()).normalized;
				this.momentum += normalized2 * this.slideGravity * Time.deltaTime;
			}
			if (this.currentControllerState == AdvancedWalkerController.ControllerState.Jumping)
			{
				this.momentum = VectorMath.RemoveDotVector(this.momentum, this.tr.up);
				this.momentum += this.tr.up * this.jumpSpeed;
			}
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.worldToLocalMatrix * this.momentum;
			}
			if (this.momentum.magnitude > (float)(MapManager.InLobbyScene ? 150 : 35))
			{
				this.momentum = Vector3.Lerp(this.momentum, this.momentum.normalized * 35f, Time.deltaTime * 8f);
			}
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x000B8D7C File Offset: 0x000B6F7C
		private void OnJumpStart(bool isAirJump = false)
		{
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.localToWorldMatrix * this.momentum;
			}
			if (isAirJump)
			{
				this.momentum += this.tr.up * this.jumpSpeed + this.tr.up * -this.momentum.y;
			}
			else if (this.momentum.y < this.jumpSpeed / 2f)
			{
				this.momentum += this.tr.up * this.jumpSpeed;
			}
			this.currentJumpStartTime = Time.time;
			this.jumpInputIsLocked = true;
			if (this.OnJump != null)
			{
				this.OnJump(this.momentum);
			}
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.worldToLocalMatrix * this.momentum;
			}
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x000B8EA0 File Offset: 0x000B70A0
		private void OnGroundContactLost()
		{
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.localToWorldMatrix * this.momentum;
			}
			Vector3 vector = this.GetMovementVelocity();
			if (vector.sqrMagnitude >= 0f && this.momentum.sqrMagnitude > 0f)
			{
				Vector3 b = Vector3.Project(this.momentum, vector.normalized);
				float dotProduct = VectorMath.GetDotProduct(b.normalized, vector.normalized);
				if (b.sqrMagnitude >= vector.sqrMagnitude && dotProduct > 0f)
				{
					vector = Vector3.zero;
				}
				else if (dotProduct > 0f)
				{
					vector -= b;
				}
			}
			this.momentum += vector;
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.worldToLocalMatrix * this.momentum;
			}
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000B8F9C File Offset: 0x000B719C
		private void OnGroundContactRegained()
		{
			this.airJumpsUsed = 0;
			if (this.OnLand != null)
			{
				Vector3 vector = this.momentum;
				if (this.useLocalMomentum)
				{
					vector = this.tr.localToWorldMatrix * vector;
				}
				this.OnLand(vector, this.mover.lastContactPoint, this.mover.surfaceNormal);
			}
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x000B9008 File Offset: 0x000B7208
		private void OnCeilingContact()
		{
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.localToWorldMatrix * this.momentum;
			}
			this.momentum = VectorMath.RemoveDotVector(this.momentum, this.tr.up);
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.worldToLocalMatrix * this.momentum;
			}
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x000B9090 File Offset: 0x000B7290
		private bool IsRisingOrFalling()
		{
			Vector3 vector = VectorMath.ExtractDotVector(this.GetMomentum(), this.tr.up);
			float num = 0.001f;
			return vector.magnitude > num;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000B90C4 File Offset: 0x000B72C4
		private bool IsGroundTooSteep()
		{
			return !this.mover.IsGrounded() || Vector3.Angle(this.mover.GetGroundNormal(), this.tr.up) > this.slopeLimit;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x000B90F8 File Offset: 0x000B72F8
		public override Vector3 GetVelocity()
		{
			return this.savedVelocity;
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000B9100 File Offset: 0x000B7300
		public override Vector3 GetMovementVelocity()
		{
			return this.savedMovementVelocity;
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000B9108 File Offset: 0x000B7308
		public Vector3 GetMomentum()
		{
			Vector3 result = this.momentum;
			if (this.useLocalMomentum)
			{
				result = this.tr.localToWorldMatrix * this.momentum;
			}
			return result;
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x000B9146 File Offset: 0x000B7346
		public override bool IsGrounded()
		{
			return this.currentControllerState == AdvancedWalkerController.ControllerState.Grounded || this.currentControllerState == AdvancedWalkerController.ControllerState.Sliding;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x000B915B File Offset: 0x000B735B
		public bool IsSliding()
		{
			return this.currentControllerState == AdvancedWalkerController.ControllerState.Sliding;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x000B9168 File Offset: 0x000B7368
		public float GetAirHeight()
		{
			if (this.IsGrounded())
			{
				return 0f;
			}
			int layerMask = this.mover.GetLayerMask();
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, 100f, layerMask))
			{
				return raycastHit.distance;
			}
			return 99f;
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x000B91BC File Offset: 0x000B73BC
		public void AddMomentum(Vector3 _momentum)
		{
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.localToWorldMatrix * this.momentum;
			}
			this.momentum += _momentum;
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.worldToLocalMatrix * this.momentum;
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000B9237 File Offset: 0x000B7437
		public void ResetMomentum()
		{
			this.momentum = Vector3.zero;
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000B9244 File Offset: 0x000B7444
		public void ApplyVerticalMomentum(Vector3 force)
		{
			Vector3 b = Vector3.ProjectOnPlane(force, Vector3.up);
			float y = force.y;
			this.momentum += b;
			this.momentum.y = y;
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000B9282 File Offset: 0x000B7482
		public void SetMomentum(Vector3 _newMomentum)
		{
			if (this.useLocalMomentum)
			{
				this.momentum = this.tr.worldToLocalMatrix * _newMomentum;
				return;
			}
			this.momentum = _newMomentum;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000B92B5 File Offset: 0x000B74B5
		public void ResetVelocity()
		{
			this.savedVelocity = Vector3.zero;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000B92C4 File Offset: 0x000B74C4
		public AdvancedWalkerController()
		{
		}

		// Token: 0x04001F17 RID: 7959
		protected Transform tr;

		// Token: 0x04001F18 RID: 7960
		protected Mover mover;

		// Token: 0x04001F19 RID: 7961
		protected PlayerControl controller;

		// Token: 0x04001F1A RID: 7962
		protected PlayerInput characterInput;

		// Token: 0x04001F1B RID: 7963
		protected PlayerNetwork sync;

		// Token: 0x04001F1C RID: 7964
		protected PlayerMovement movement;

		// Token: 0x04001F1D RID: 7965
		protected CeilingDetector ceilingDetector;

		// Token: 0x04001F1E RID: 7966
		private bool jumpInputIsLocked;

		// Token: 0x04001F1F RID: 7967
		private bool jumpKeyWasPressed;

		// Token: 0x04001F20 RID: 7968
		private bool jumpKeyWasLetGo;

		// Token: 0x04001F21 RID: 7969
		private bool jumpKeyIsPressed;

		// Token: 0x04001F22 RID: 7970
		public float airControl = 2f;

		// Token: 0x04001F23 RID: 7971
		private float Acceleration = 15f;

		// Token: 0x04001F24 RID: 7972
		[Range(0.1f, 1f)]
		public float BackwardsMult = 0.8f;

		// Token: 0x04001F25 RID: 7973
		private float JumpHeight = 11f;

		// Token: 0x04001F26 RID: 7974
		public float jumpDuration = 0.2f;

		// Token: 0x04001F27 RID: 7975
		private float currentJumpStartTime;

		// Token: 0x04001F28 RID: 7976
		public float frictionMultAir = 1f;

		// Token: 0x04001F29 RID: 7977
		private float AirFriction = 0.6f;

		// Token: 0x04001F2A RID: 7978
		public float frictionMultGround = 1f;

		// Token: 0x04001F2B RID: 7979
		private float GroundFriction = 100f;

		// Token: 0x04001F2C RID: 7980
		protected Vector3 momentum = Vector3.zero;

		// Token: 0x04001F2D RID: 7981
		private Vector3 savedVelocity = Vector3.zero;

		// Token: 0x04001F2E RID: 7982
		private Vector3 savedMovementVelocity = Vector3.zero;

		// Token: 0x04001F2F RID: 7983
		private float Gravity = 30f;

		// Token: 0x04001F30 RID: 7984
		[Tooltip("How fast the character will slide down steep slopes.")]
		public float slideGravity = 5f;

		// Token: 0x04001F31 RID: 7985
		public float slopeLimit = 80f;

		// Token: 0x04001F32 RID: 7986
		[Tooltip("Whether to calculate and apply momentum relative to the controller's transform.")]
		public bool useLocalMomentum;

		// Token: 0x04001F33 RID: 7987
		private AdvancedWalkerController.ControllerState currentControllerState = AdvancedWalkerController.ControllerState.Falling;

		// Token: 0x04001F34 RID: 7988
		[Tooltip("Optional camera transform used for calculating movement direction. If assigned, character movement will take camera view into account.")]
		public Transform cameraTransform;

		// Token: 0x04001F35 RID: 7989
		private Vector3 prevVelocity;

		// Token: 0x04001F36 RID: 7990
		private Vector3 moveInput;

		// Token: 0x04001F37 RID: 7991
		private float lastGroundedTime;

		// Token: 0x04001F38 RID: 7992
		private int airJumpsUsed;

		// Token: 0x02000693 RID: 1683
		public enum ControllerState
		{
			// Token: 0x04002C27 RID: 11303
			Grounded,
			// Token: 0x04002C28 RID: 11304
			Sliding,
			// Token: 0x04002C29 RID: 11305
			Falling,
			// Token: 0x04002C2A RID: 11306
			Rising,
			// Token: 0x04002C2B RID: 11307
			Jumping
		}
	}
}
