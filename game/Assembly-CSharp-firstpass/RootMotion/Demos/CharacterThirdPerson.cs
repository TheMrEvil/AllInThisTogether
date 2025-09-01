using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200015D RID: 349
	public class CharacterThirdPerson : CharacterBase
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0005AE69 File Offset: 0x00059069
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0005AE71 File Offset: 0x00059071
		public bool onGround
		{
			[CompilerGenerated]
			get
			{
				return this.<onGround>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<onGround>k__BackingField = value;
			}
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0005AE7C File Offset: 0x0005907C
		protected override void Start()
		{
			base.Start();
			this.animator = base.GetComponent<Animator>();
			if (this.animator == null)
			{
				this.animator = this.characterAnimation.GetComponent<Animator>();
			}
			this.wallNormal = -this.gravity.normalized;
			this.onGround = true;
			this.animState.onGround = true;
			if (this.cam != null)
			{
				this.cam.enabled = false;
			}
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0005AEFD File Offset: 0x000590FD
		private void OnAnimatorMove()
		{
			this.Move(this.animator.deltaPosition, this.animator.deltaRotation);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0005AF1B File Offset: 0x0005911B
		public override void Move(Vector3 deltaPosition, Quaternion deltaRotation)
		{
			this.fixedDeltaTime += Time.deltaTime;
			this.fixedDeltaPosition += deltaPosition;
			this.fixedDeltaRotation *= deltaRotation;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0005AF54 File Offset: 0x00059154
		private void FixedUpdate()
		{
			this.gravity = base.GetGravity();
			this.verticalVelocity = V3Tools.ExtractVertical(this.r.velocity, this.gravity, 1f);
			this.velocityY = this.verticalVelocity.magnitude;
			if (Vector3.Dot(this.verticalVelocity, this.gravity) > 0f)
			{
				this.velocityY = -this.velocityY;
			}
			this.r.interpolation = (this.smoothPhysics ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None);
			this.characterAnimation.smoothFollow = this.smoothPhysics;
			this.MoveFixed(this.fixedDeltaPosition);
			this.fixedDeltaTime = 0f;
			this.fixedDeltaPosition = Vector3.zero;
			this.r.MoveRotation(base.transform.rotation * this.fixedDeltaRotation);
			this.fixedDeltaRotation = Quaternion.identity;
			this.Rotate();
			this.GroundCheck();
			if (this.userControl.state.move == Vector3.zero && this.groundDistance < this.airborneThreshold * 0.5f)
			{
				base.HighFriction();
			}
			else
			{
				base.ZeroFriction();
			}
			bool flag = this.onGround && this.userControl.state.move == Vector3.zero && this.r.velocity.magnitude < 0.5f && this.groundDistance < this.airborneThreshold * 0.5f;
			if (this.gravityTarget != null)
			{
				this.r.useGravity = false;
				if (!flag)
				{
					this.r.AddForce(this.gravity);
				}
			}
			if (flag)
			{
				this.r.useGravity = false;
				this.r.velocity = Vector3.zero;
			}
			else if (this.gravityTarget == null)
			{
				this.r.useGravity = true;
			}
			if (this.onGround)
			{
				this.animState.jump = this.Jump();
				this.jumpReleased = false;
				this.doubleJumped = false;
			}
			else
			{
				if (!this.userControl.state.jump)
				{
					this.jumpReleased = true;
				}
				if (this.jumpReleased && this.userControl.state.jump && !this.doubleJumped && this.doubleJumpEnabled)
				{
					this.jumpEndTime = Time.time + 0.1f;
					this.animState.doubleJump = true;
					Vector3 velocity = this.userControl.state.move * this.airSpeed;
					this.r.velocity = velocity;
					this.r.velocity += base.transform.up * this.jumpPower * this.doubleJumpPowerMlp;
					this.doubleJumped = true;
				}
			}
			base.ScaleCapsule(this.userControl.state.crouch ? this.crouchCapsuleScaleMlp : 1f);
			this.fixedFrame = true;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0005B270 File Offset: 0x00059470
		protected virtual void Update()
		{
			this.animState.onGround = this.onGround;
			this.animState.moveDirection = this.GetMoveDirection();
			this.animState.yVelocity = Mathf.Lerp(this.animState.yVelocity, this.velocityY, Time.deltaTime * 10f);
			this.animState.crouch = this.userControl.state.crouch;
			this.animState.isStrafing = (this.moveMode == CharacterThirdPerson.MoveMode.Strafe);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0005B2FC File Offset: 0x000594FC
		protected virtual void LateUpdate()
		{
			if (this.cam == null)
			{
				return;
			}
			this.cam.UpdateInput();
			if (!this.fixedFrame && this.r.interpolation == RigidbodyInterpolation.None)
			{
				return;
			}
			this.cam.UpdateTransform((this.r.interpolation == RigidbodyInterpolation.None) ? Time.fixedDeltaTime : Time.deltaTime);
			this.fixedFrame = false;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0005B364 File Offset: 0x00059564
		private void MoveFixed(Vector3 deltaPosition)
		{
			this.WallRun();
			Vector3 vector = (this.fixedDeltaTime > 0f) ? (deltaPosition / this.fixedDeltaTime) : Vector3.zero;
			vector += V3Tools.ExtractHorizontal(this.platformVelocity, this.gravity, 1f);
			if (this.onGround)
			{
				if (this.velocityToGroundTangentWeight > 0f)
				{
					Quaternion b = Quaternion.FromToRotation(base.transform.up, this.normal);
					vector = Quaternion.Lerp(Quaternion.identity, b, this.velocityToGroundTangentWeight) * vector;
				}
			}
			else
			{
				Vector3 b2 = V3Tools.ExtractHorizontal(this.userControl.state.move * this.airSpeed, this.gravity, 1f);
				vector = Vector3.Lerp(this.r.velocity, b2, Time.deltaTime * this.airControl);
			}
			if (this.onGround && Time.time > this.jumpEndTime)
			{
				this.r.velocity = this.r.velocity - base.transform.up * this.stickyForce * Time.deltaTime;
			}
			Vector3 vector2 = V3Tools.ExtractVertical(this.r.velocity, this.gravity, 1f);
			Vector3 a = V3Tools.ExtractHorizontal(vector, this.gravity, 1f);
			if (this.onGround && Vector3.Dot(vector2, this.gravity) < 0f)
			{
				vector2 = Vector3.ClampMagnitude(vector2, this.maxVerticalVelocityOnGround);
			}
			this.r.velocity = a + vector2;
			this.forwardMlp = 1f;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0005B50C File Offset: 0x0005970C
		private void WallRun()
		{
			bool flag = this.CanWallRun();
			if (this.wallRunWeight > 0f && !flag)
			{
				this.wallRunEndTime = Time.time;
			}
			if (Time.time < this.wallRunEndTime + 0.5f)
			{
				flag = false;
			}
			this.wallRunWeight = Mathf.MoveTowards(this.wallRunWeight, flag ? 1f : 0f, Time.deltaTime * this.wallRunWeightSpeed);
			if (this.wallRunWeight <= 0f && this.lastWallRunWeight > 0f)
			{
				Vector3 forward = V3Tools.ExtractHorizontal(base.transform.forward, this.gravity, 1f);
				base.transform.rotation = Quaternion.LookRotation(forward, -this.gravity);
				this.wallNormal = -this.gravity.normalized;
			}
			this.lastWallRunWeight = this.wallRunWeight;
			if (this.wallRunWeight <= 0f)
			{
				return;
			}
			if (this.onGround && this.velocityY < 0f)
			{
				this.r.velocity = V3Tools.ExtractHorizontal(this.r.velocity, this.gravity, 1f);
			}
			Vector3 vector = V3Tools.ExtractHorizontal(base.transform.forward, this.gravity, 1f);
			RaycastHit raycastHit = default(RaycastHit);
			raycastHit.normal = -this.gravity.normalized;
			Physics.Raycast(this.onGround ? base.transform.position : this.capsule.bounds.center, vector, out raycastHit, 3f, this.wallRunLayers);
			this.wallNormal = Vector3.Lerp(this.wallNormal, raycastHit.normal, Time.deltaTime * this.wallRunRotationSpeed);
			this.wallNormal = Vector3.RotateTowards(-this.gravity.normalized, this.wallNormal, this.wallRunMaxRotationAngle * 0.017453292f, 0f);
			Vector3 forward2 = base.transform.forward;
			Vector3 vector2 = this.wallNormal;
			Vector3.OrthoNormalize(ref vector2, ref forward2);
			base.transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(vector, -this.gravity), Quaternion.LookRotation(forward2, this.wallNormal), this.wallRunWeight);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0005B760 File Offset: 0x00059960
		private bool CanWallRun()
		{
			return Time.time >= this.jumpEndTime - 0.1f && Time.time <= this.jumpEndTime - 0.1f + this.wallRunMaxLength && this.velocityY >= this.wallRunMinVelocityY && this.userControl.state.move.magnitude >= this.wallRunMinMoveMag;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0005B7D0 File Offset: 0x000599D0
		private Vector3 GetMoveDirection()
		{
			CharacterThirdPerson.MoveMode moveMode = this.moveMode;
			if (moveMode == CharacterThirdPerson.MoveMode.Directional)
			{
				this.moveDirection = Vector3.SmoothDamp(this.moveDirection, new Vector3(0f, 0f, this.userControl.state.move.magnitude), ref this.moveDirectionVelocity, this.smoothAccelerationTime);
				this.moveDirection = Vector3.MoveTowards(this.moveDirection, new Vector3(0f, 0f, this.userControl.state.move.magnitude), Time.deltaTime * this.linearAccelerationSpeed);
				return this.moveDirection * this.forwardMlp;
			}
			if (moveMode != CharacterThirdPerson.MoveMode.Strafe)
			{
				return Vector3.zero;
			}
			this.moveDirection = Vector3.SmoothDamp(this.moveDirection, this.userControl.state.move, ref this.moveDirectionVelocity, this.smoothAccelerationTime);
			this.moveDirection = Vector3.MoveTowards(this.moveDirection, this.userControl.state.move, Time.deltaTime * this.linearAccelerationSpeed);
			return base.transform.InverseTransformDirection(this.moveDirection);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0005B8F8 File Offset: 0x00059AF8
		protected virtual void Rotate()
		{
			if (this.gravityTarget != null)
			{
				this.r.MoveRotation(Quaternion.FromToRotation(base.transform.up, base.transform.position - this.gravityTarget.position) * base.transform.rotation);
			}
			if (this.platformAngularVelocity != Vector3.zero)
			{
				this.r.MoveRotation(Quaternion.Euler(this.platformAngularVelocity) * base.transform.rotation);
			}
			float num = base.GetAngleFromForward(this.GetForwardDirection());
			if (this.userControl.state.move == Vector3.zero)
			{
				num *= (1.01f - Mathf.Abs(num) / 180f) * this.stationaryTurnSpeedMlp;
			}
			this.r.MoveRotation(Quaternion.AngleAxis(num * Time.deltaTime * this.turnSpeed, base.transform.up) * this.r.rotation);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0005BA10 File Offset: 0x00059C10
		private Vector3 GetForwardDirection()
		{
			bool flag = this.userControl.state.move != Vector3.zero;
			CharacterThirdPerson.MoveMode moveMode = this.moveMode;
			if (moveMode != CharacterThirdPerson.MoveMode.Directional)
			{
				if (moveMode != CharacterThirdPerson.MoveMode.Strafe)
				{
					return Vector3.zero;
				}
				if (flag)
				{
					return this.userControl.state.lookPos - this.r.position;
				}
				if (!this.lookInCameraDirection)
				{
					return base.transform.forward;
				}
				return this.userControl.state.lookPos - this.r.position;
			}
			else
			{
				if (flag)
				{
					return this.userControl.state.move;
				}
				if (!this.lookInCameraDirection)
				{
					return base.transform.forward;
				}
				return this.userControl.state.lookPos - this.r.position;
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0005BAF4 File Offset: 0x00059CF4
		protected virtual bool Jump()
		{
			if (!this.userControl.state.jump)
			{
				return false;
			}
			if (this.userControl.state.crouch)
			{
				return false;
			}
			if (!this.characterAnimation.animationGrounded)
			{
				return false;
			}
			if (Time.time < this.lastAirTime + this.jumpRepeatDelayTime)
			{
				return false;
			}
			this.onGround = false;
			this.jumpEndTime = Time.time + 0.1f;
			Vector3 vector = this.userControl.state.move * this.airSpeed;
			vector += base.transform.up * this.jumpPower;
			if (this.smoothJump)
			{
				base.StopAllCoroutines();
				base.StartCoroutine(this.JumpSmooth(vector - this.r.velocity));
			}
			else
			{
				this.r.velocity = vector;
			}
			return true;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0005BBDA File Offset: 0x00059DDA
		private IEnumerator JumpSmooth(Vector3 jumpVelocity)
		{
			int steps = 0;
			int stepsToTake = 3;
			while (steps < stepsToTake)
			{
				this.r.AddForce(jumpVelocity / (float)stepsToTake, ForceMode.VelocityChange);
				int num = steps;
				steps = num + 1;
				yield return new WaitForFixedUpdate();
			}
			yield break;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0005BBF0 File Offset: 0x00059DF0
		private void GroundCheck()
		{
			Vector3 b = Vector3.zero;
			this.platformAngularVelocity = Vector3.zero;
			float num = 0f;
			this.hit = this.GetSpherecastHit();
			this.normal = base.transform.up;
			this.groundDistance = Vector3.Project(this.r.position - this.hit.point, base.transform.up).magnitude;
			if (Time.time > this.jumpEndTime && this.velocityY < this.jumpPower * 0.5f)
			{
				bool onGround = this.onGround;
				this.onGround = false;
				float num2 = (!onGround) ? (this.airborneThreshold * 0.5f) : this.airborneThreshold;
				float magnitude = V3Tools.ExtractHorizontal(this.r.velocity, this.gravity, 1f).magnitude;
				if (this.groundDistance < num2)
				{
					num = this.groundStickyEffect * magnitude * num2;
					if (this.hit.rigidbody != null)
					{
						b = this.hit.rigidbody.GetPointVelocity(this.hit.point);
						this.platformAngularVelocity = Vector3.Project(this.hit.rigidbody.angularVelocity, base.transform.up);
					}
					this.onGround = true;
				}
			}
			this.platformVelocity = Vector3.Lerp(this.platformVelocity, b, Time.deltaTime * this.platformFriction);
			this.stickyForce = num;
			if (!this.onGround)
			{
				this.lastAirTime = Time.time;
			}
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0005BD88 File Offset: 0x00059F88
		public CharacterThirdPerson()
		{
		}

		// Token: 0x04000B44 RID: 2884
		[Header("References")]
		public CharacterAnimationBase characterAnimation;

		// Token: 0x04000B45 RID: 2885
		public UserControlThirdPerson userControl;

		// Token: 0x04000B46 RID: 2886
		public CameraController cam;

		// Token: 0x04000B47 RID: 2887
		[Header("Movement")]
		public CharacterThirdPerson.MoveMode moveMode;

		// Token: 0x04000B48 RID: 2888
		public bool smoothPhysics = true;

		// Token: 0x04000B49 RID: 2889
		public float smoothAccelerationTime = 0.2f;

		// Token: 0x04000B4A RID: 2890
		public float linearAccelerationSpeed = 3f;

		// Token: 0x04000B4B RID: 2891
		public float platformFriction = 7f;

		// Token: 0x04000B4C RID: 2892
		public float groundStickyEffect = 4f;

		// Token: 0x04000B4D RID: 2893
		public float maxVerticalVelocityOnGround = 3f;

		// Token: 0x04000B4E RID: 2894
		public float velocityToGroundTangentWeight;

		// Token: 0x04000B4F RID: 2895
		[Header("Rotation")]
		public bool lookInCameraDirection;

		// Token: 0x04000B50 RID: 2896
		public float turnSpeed = 5f;

		// Token: 0x04000B51 RID: 2897
		public float stationaryTurnSpeedMlp = 1f;

		// Token: 0x04000B52 RID: 2898
		[Header("Jumping and Falling")]
		public bool smoothJump = true;

		// Token: 0x04000B53 RID: 2899
		public float airSpeed = 6f;

		// Token: 0x04000B54 RID: 2900
		public float airControl = 2f;

		// Token: 0x04000B55 RID: 2901
		public float jumpPower = 12f;

		// Token: 0x04000B56 RID: 2902
		public float jumpRepeatDelayTime;

		// Token: 0x04000B57 RID: 2903
		public bool doubleJumpEnabled;

		// Token: 0x04000B58 RID: 2904
		public float doubleJumpPowerMlp = 1f;

		// Token: 0x04000B59 RID: 2905
		[Header("Wall Running")]
		public LayerMask wallRunLayers;

		// Token: 0x04000B5A RID: 2906
		public float wallRunMaxLength = 1f;

		// Token: 0x04000B5B RID: 2907
		public float wallRunMinMoveMag = 0.6f;

		// Token: 0x04000B5C RID: 2908
		public float wallRunMinVelocityY = -1f;

		// Token: 0x04000B5D RID: 2909
		public float wallRunRotationSpeed = 1.5f;

		// Token: 0x04000B5E RID: 2910
		public float wallRunMaxRotationAngle = 70f;

		// Token: 0x04000B5F RID: 2911
		public float wallRunWeightSpeed = 5f;

		// Token: 0x04000B60 RID: 2912
		[Header("Crouching")]
		public float crouchCapsuleScaleMlp = 0.6f;

		// Token: 0x04000B61 RID: 2913
		[CompilerGenerated]
		private bool <onGround>k__BackingField;

		// Token: 0x04000B62 RID: 2914
		public CharacterThirdPerson.AnimState animState;

		// Token: 0x04000B63 RID: 2915
		protected Vector3 moveDirection;

		// Token: 0x04000B64 RID: 2916
		private Animator animator;

		// Token: 0x04000B65 RID: 2917
		private Vector3 normal;

		// Token: 0x04000B66 RID: 2918
		private Vector3 platformVelocity;

		// Token: 0x04000B67 RID: 2919
		private Vector3 platformAngularVelocity;

		// Token: 0x04000B68 RID: 2920
		private RaycastHit hit;

		// Token: 0x04000B69 RID: 2921
		private float jumpLeg;

		// Token: 0x04000B6A RID: 2922
		private float jumpEndTime;

		// Token: 0x04000B6B RID: 2923
		private float forwardMlp;

		// Token: 0x04000B6C RID: 2924
		private float groundDistance;

		// Token: 0x04000B6D RID: 2925
		private float lastAirTime;

		// Token: 0x04000B6E RID: 2926
		private float stickyForce;

		// Token: 0x04000B6F RID: 2927
		private Vector3 wallNormal = Vector3.up;

		// Token: 0x04000B70 RID: 2928
		private Vector3 moveDirectionVelocity;

		// Token: 0x04000B71 RID: 2929
		private float wallRunWeight;

		// Token: 0x04000B72 RID: 2930
		private float lastWallRunWeight;

		// Token: 0x04000B73 RID: 2931
		private float fixedDeltaTime;

		// Token: 0x04000B74 RID: 2932
		private Vector3 fixedDeltaPosition;

		// Token: 0x04000B75 RID: 2933
		private Quaternion fixedDeltaRotation = Quaternion.identity;

		// Token: 0x04000B76 RID: 2934
		private bool fixedFrame;

		// Token: 0x04000B77 RID: 2935
		private float wallRunEndTime;

		// Token: 0x04000B78 RID: 2936
		private Vector3 gravity;

		// Token: 0x04000B79 RID: 2937
		private Vector3 verticalVelocity;

		// Token: 0x04000B7A RID: 2938
		private float velocityY;

		// Token: 0x04000B7B RID: 2939
		private bool doubleJumped;

		// Token: 0x04000B7C RID: 2940
		private bool jumpReleased;

		// Token: 0x02000239 RID: 569
		[Serializable]
		public enum MoveMode
		{
			// Token: 0x040010BD RID: 4285
			Directional,
			// Token: 0x040010BE RID: 4286
			Strafe
		}

		// Token: 0x0200023A RID: 570
		public struct AnimState
		{
			// Token: 0x040010BF RID: 4287
			public Vector3 moveDirection;

			// Token: 0x040010C0 RID: 4288
			public bool jump;

			// Token: 0x040010C1 RID: 4289
			public bool crouch;

			// Token: 0x040010C2 RID: 4290
			public bool onGround;

			// Token: 0x040010C3 RID: 4291
			public bool isStrafing;

			// Token: 0x040010C4 RID: 4292
			public float yVelocity;

			// Token: 0x040010C5 RID: 4293
			public bool doubleJump;
		}

		// Token: 0x0200023B RID: 571
		[CompilerGenerated]
		private sealed class <JumpSmooth>d__75 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060011CC RID: 4556 RVA: 0x0006E623 File Offset: 0x0006C823
			[DebuggerHidden]
			public <JumpSmooth>d__75(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060011CD RID: 4557 RVA: 0x0006E632 File Offset: 0x0006C832
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060011CE RID: 4558 RVA: 0x0006E634 File Offset: 0x0006C834
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				CharacterThirdPerson characterThirdPerson = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					steps = 0;
					stepsToTake = 3;
				}
				if (steps >= stepsToTake)
				{
					return false;
				}
				characterThirdPerson.r.AddForce(jumpVelocity / (float)stepsToTake, ForceMode.VelocityChange);
				int num2 = steps;
				steps = num2 + 1;
				this.<>2__current = new WaitForFixedUpdate();
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x060011CF RID: 4559 RVA: 0x0006E6C7 File Offset: 0x0006C8C7
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060011D0 RID: 4560 RVA: 0x0006E6CF File Offset: 0x0006C8CF
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0006E6D6 File Offset: 0x0006C8D6
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040010C6 RID: 4294
			private int <>1__state;

			// Token: 0x040010C7 RID: 4295
			private object <>2__current;

			// Token: 0x040010C8 RID: 4296
			public CharacterThirdPerson <>4__this;

			// Token: 0x040010C9 RID: 4297
			public Vector3 jumpVelocity;

			// Token: 0x040010CA RID: 4298
			private int <steps>5__2;

			// Token: 0x040010CB RID: 4299
			private int <stepsToTake>5__3;
		}
	}
}
