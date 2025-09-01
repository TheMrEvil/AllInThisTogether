using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x0200012F RID: 303
	public class AnimatorController3rdPerson : MonoBehaviour
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x000565D4 File Offset: 0x000547D4
		protected virtual void Start()
		{
			this.animator = base.GetComponent<Animator>();
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000565E4 File Offset: 0x000547E4
		private void OnAnimatorMove()
		{
			this.velocity = Vector3.Lerp(this.velocity, base.transform.rotation * Vector3.ClampMagnitude(this.moveInput, 1f) * this.moveSpeed, Time.deltaTime * this.blendSpeed);
			base.transform.position += Vector3.Lerp(this.velocity * Time.deltaTime, this.animator.deltaPosition, this.rootMotionWeight);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00056678 File Offset: 0x00054878
		public virtual void Move(Vector3 moveInput, bool isMoving, Vector3 faceDirection, Vector3 aimTarget)
		{
			this.moveInput = moveInput;
			Vector3 vector = base.transform.InverseTransformDirection(faceDirection);
			float num = Mathf.Atan2(vector.x, vector.z) * 57.29578f;
			float num2 = num * Time.deltaTime * this.rotateSpeed;
			if (num > this.maxAngle)
			{
				num2 = Mathf.Clamp(num2, num - this.maxAngle, num2);
			}
			if (num < -this.maxAngle)
			{
				num2 = Mathf.Clamp(num2, num2, num + this.maxAngle);
			}
			base.transform.Rotate(Vector3.up, num2);
			this.moveBlend = Vector3.Lerp(this.moveBlend, moveInput, Time.deltaTime * this.blendSpeed);
			this.animator.SetFloat("X", this.moveBlend.x);
			this.animator.SetFloat("Z", this.moveBlend.z);
			this.animator.SetBool("IsMoving", isMoving);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00056769 File Offset: 0x00054969
		public AnimatorController3rdPerson()
		{
		}

		// Token: 0x04000A34 RID: 2612
		public float rotateSpeed = 7f;

		// Token: 0x04000A35 RID: 2613
		public float blendSpeed = 10f;

		// Token: 0x04000A36 RID: 2614
		public float maxAngle = 90f;

		// Token: 0x04000A37 RID: 2615
		public float moveSpeed = 1.5f;

		// Token: 0x04000A38 RID: 2616
		public float rootMotionWeight;

		// Token: 0x04000A39 RID: 2617
		protected Animator animator;

		// Token: 0x04000A3A RID: 2618
		protected Vector3 moveBlend;

		// Token: 0x04000A3B RID: 2619
		protected Vector3 moveInput;

		// Token: 0x04000A3C RID: 2620
		protected Vector3 velocity;
	}
}
