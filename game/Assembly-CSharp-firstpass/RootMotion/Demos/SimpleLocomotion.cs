using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200015E RID: 350
	public class SimpleLocomotion : MonoBehaviour
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0005BE85 File Offset: 0x0005A085
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x0005BE8D File Offset: 0x0005A08D
		public bool isGrounded
		{
			[CompilerGenerated]
			get
			{
				return this.<isGrounded>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isGrounded>k__BackingField = value;
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0005BE96 File Offset: 0x0005A096
		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
			this.characterController = base.GetComponent<CharacterController>();
			this.cameraController.enabled = false;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0005BEBC File Offset: 0x0005A0BC
		private void Update()
		{
			this.isGrounded = (base.transform.position.y < 0.1f);
			this.Rotate();
			this.Move();
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0005BEE7 File Offset: 0x0005A0E7
		private void LateUpdate()
		{
			this.cameraController.UpdateInput();
			this.cameraController.UpdateTransform();
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0005BF00 File Offset: 0x0005A100
		private void Rotate()
		{
			if (!this.isGrounded)
			{
				return;
			}
			Vector3 inputVector = this.GetInputVector();
			if (inputVector == Vector3.zero)
			{
				return;
			}
			Vector3 vector = base.transform.forward;
			SimpleLocomotion.RotationMode rotationMode = this.rotationMode;
			if (rotationMode == SimpleLocomotion.RotationMode.Smooth)
			{
				Vector3 vector2 = this.cameraController.transform.rotation * inputVector;
				float current = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
				float target = Mathf.Atan2(vector2.x, vector2.z) * 57.29578f;
				float angle = Mathf.SmoothDampAngle(current, target, ref this.angleVel, this.turnTime);
				base.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
				return;
			}
			if (rotationMode != SimpleLocomotion.RotationMode.Linear)
			{
				return;
			}
			Vector3 inputVectorRaw = this.GetInputVectorRaw();
			if (inputVectorRaw != Vector3.zero)
			{
				this.linearTargetDirection = this.cameraController.transform.rotation * inputVectorRaw;
			}
			vector = Vector3.RotateTowards(vector, this.linearTargetDirection, Time.deltaTime * (1f / this.turnTime), 1f);
			vector.y = 0f;
			base.transform.rotation = Quaternion.LookRotation(vector);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0005C030 File Offset: 0x0005A230
		private void Move()
		{
			float target = this.walkByDefault ? (Input.GetKey(KeyCode.LeftShift) ? 1f : 0.5f) : (Input.GetKey(KeyCode.LeftShift) ? 0.5f : 1f);
			this.speed = Mathf.SmoothDamp(this.speed, target, ref this.speedVel, this.accelerationTime);
			float num = this.GetInputVector().magnitude * this.speed;
			this.animator.SetFloat("Speed", num);
			if (!this.animator.hasRootMotion && this.isGrounded)
			{
				Vector3 a = base.transform.forward * num * this.moveSpeed;
				if (this.characterController != null)
				{
					this.characterController.SimpleMove(a);
					return;
				}
				base.transform.position += a * Time.deltaTime;
			}
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0005C130 File Offset: 0x0005A330
		private Vector3 GetInputVector()
		{
			Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
			vector.z += Mathf.Abs(vector.x) * 0.05f;
			vector.x -= Mathf.Abs(vector.z) * 0.05f;
			return vector;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0005C196 File Offset: 0x0005A396
		private Vector3 GetInputVectorRaw()
		{
			return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0005C1B6 File Offset: 0x0005A3B6
		public SimpleLocomotion()
		{
		}

		// Token: 0x04000B7D RID: 2941
		[Tooltip("The component that updates the camera.")]
		public CameraController cameraController;

		// Token: 0x04000B7E RID: 2942
		[Tooltip("Acceleration of movement.")]
		public float accelerationTime = 0.2f;

		// Token: 0x04000B7F RID: 2943
		[Tooltip("Turning speed.")]
		public float turnTime = 0.2f;

		// Token: 0x04000B80 RID: 2944
		[Tooltip("If true, will run on left shift, if not will walk on left shift.")]
		public bool walkByDefault = true;

		// Token: 0x04000B81 RID: 2945
		[Tooltip("Smooth or linear rotation.")]
		public SimpleLocomotion.RotationMode rotationMode;

		// Token: 0x04000B82 RID: 2946
		[Tooltip("Procedural motion speed (if not using root motion).")]
		public float moveSpeed = 3f;

		// Token: 0x04000B83 RID: 2947
		[CompilerGenerated]
		private bool <isGrounded>k__BackingField;

		// Token: 0x04000B84 RID: 2948
		private Animator animator;

		// Token: 0x04000B85 RID: 2949
		private float speed;

		// Token: 0x04000B86 RID: 2950
		private float angleVel;

		// Token: 0x04000B87 RID: 2951
		private float speedVel;

		// Token: 0x04000B88 RID: 2952
		private Vector3 linearTargetDirection;

		// Token: 0x04000B89 RID: 2953
		private CharacterController characterController;

		// Token: 0x0200023C RID: 572
		[Serializable]
		public enum RotationMode
		{
			// Token: 0x040010CD RID: 4301
			Smooth,
			// Token: 0x040010CE RID: 4302
			Linear
		}
	}
}
