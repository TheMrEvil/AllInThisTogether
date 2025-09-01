using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x0200039E RID: 926
	public class AnimationControl : MonoBehaviour
	{
		// Token: 0x06001E9C RID: 7836 RVA: 0x000B70C3 File Offset: 0x000B52C3
		private void Awake()
		{
			this.controller = base.GetComponent<Controller>();
			this.animator = base.GetComponentInChildren<Animator>();
			this.animatorTransform = this.animator.transform;
			this.tr = base.transform;
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x000B70FC File Offset: 0x000B52FC
		private void OnEnable()
		{
			Controller controller = this.controller;
			controller.OnLand = (Action<Vector3, Vector3, Vector3>)Delegate.Combine(controller.OnLand, new Action<Vector3, Vector3, Vector3>(this.OnLand));
			Controller controller2 = this.controller;
			controller2.OnJump = (Controller.VectorEvent)Delegate.Combine(controller2.OnJump, new Controller.VectorEvent(this.OnJump));
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x000B7158 File Offset: 0x000B5358
		private void OnDisable()
		{
			Controller controller = this.controller;
			controller.OnLand = (Action<Vector3, Vector3, Vector3>)Delegate.Remove(controller.OnLand, new Action<Vector3, Vector3, Vector3>(this.OnLand));
			Controller controller2 = this.controller;
			controller2.OnJump = (Controller.VectorEvent)Delegate.Remove(controller2.OnJump, new Controller.VectorEvent(this.OnJump));
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000B71B4 File Offset: 0x000B53B4
		private void Update()
		{
			Vector3 velocity = this.controller.GetVelocity();
			Vector3 vector = VectorMath.RemoveDotVector(velocity, this.tr.up);
			Vector3 vector2 = velocity - vector;
			vector = Vector3.Lerp(this.oldMovementVelocity, vector, this.smoothingFactor * Time.deltaTime);
			this.oldMovementVelocity = vector;
			this.animator.SetFloat("VerticalSpeed", vector2.magnitude * VectorMath.GetDotProduct(vector2.normalized, this.tr.up));
			this.animator.SetFloat("HorizontalSpeed", vector.magnitude);
			if (this.useStrafeAnimations)
			{
				Vector3 vector3 = this.animatorTransform.InverseTransformVector(vector);
				this.animator.SetFloat("ForwardSpeed", vector3.z);
				this.animator.SetFloat("StrafeSpeed", vector3.x);
			}
			this.animator.SetBool("IsGrounded", this.controller.IsGrounded());
			this.animator.SetBool("IsStrafing", this.useStrafeAnimations);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000B72BD File Offset: 0x000B54BD
		private void OnLand(Vector3 _v, Vector3 point, Vector3 surfaceNormal)
		{
			if (VectorMath.GetDotProduct(_v, this.tr.up) > -this.landVelocityThreshold)
			{
				return;
			}
			this.animator.SetTrigger("OnLand");
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000B72EA File Offset: 0x000B54EA
		private void OnJump(Vector3 _v)
		{
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000B72EC File Offset: 0x000B54EC
		public AnimationControl()
		{
		}

		// Token: 0x04001ED3 RID: 7891
		private Controller controller;

		// Token: 0x04001ED4 RID: 7892
		private Animator animator;

		// Token: 0x04001ED5 RID: 7893
		private Transform animatorTransform;

		// Token: 0x04001ED6 RID: 7894
		private Transform tr;

		// Token: 0x04001ED7 RID: 7895
		public bool useStrafeAnimations;

		// Token: 0x04001ED8 RID: 7896
		public float landVelocityThreshold = 5f;

		// Token: 0x04001ED9 RID: 7897
		private float smoothingFactor = 40f;

		// Token: 0x04001EDA RID: 7898
		private Vector3 oldMovementVelocity = Vector3.zero;
	}
}
