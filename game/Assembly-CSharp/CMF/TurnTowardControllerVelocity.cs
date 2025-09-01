using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003A1 RID: 929
	public class TurnTowardControllerVelocity : MonoBehaviour
	{
		// Token: 0x06001EAD RID: 7853 RVA: 0x000B75F9 File Offset: 0x000B57F9
		private void Start()
		{
			this.tr = base.transform;
			this.parentTransform = this.tr.parent;
			if (this.controller == null)
			{
				Debug.LogWarning("No controller script has been assigned to this 'TurnTowardControllerVelocity' component!", this);
				base.enabled = false;
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000B7638 File Offset: 0x000B5838
		private void LateUpdate()
		{
			Vector3 vector;
			if (this.ignoreControllerMomentum)
			{
				vector = this.controller.GetMovementVelocity();
			}
			else
			{
				vector = this.controller.GetVelocity();
			}
			vector = Vector3.ProjectOnPlane(vector, this.parentTransform.up);
			float num = 0.001f;
			if (vector.magnitude < num)
			{
				return;
			}
			vector.Normalize();
			float angle = VectorMath.GetAngle(this.tr.forward, vector, this.parentTransform.up);
			float num2 = Mathf.InverseLerp(0f, this.fallOffAngle, Mathf.Abs(angle));
			float num3 = Mathf.Sign(angle) * num2 * Time.deltaTime * this.turnSpeed;
			if (angle < 0f && num3 < angle)
			{
				num3 = angle;
			}
			else if (angle > 0f && num3 > angle)
			{
				num3 = angle;
			}
			this.currentYRotation += num3;
			if (this.currentYRotation > 360f)
			{
				this.currentYRotation -= 360f;
			}
			if (this.currentYRotation < -360f)
			{
				this.currentYRotation += 360f;
			}
			this.tr.localRotation = Quaternion.Euler(0f, this.currentYRotation, 0f);
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000B776B File Offset: 0x000B596B
		private void OnDisable()
		{
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000B776D File Offset: 0x000B596D
		private void OnEnable()
		{
			this.currentYRotation = base.transform.localEulerAngles.y;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000B7785 File Offset: 0x000B5985
		public TurnTowardControllerVelocity()
		{
		}

		// Token: 0x04001EEC RID: 7916
		public Controller controller;

		// Token: 0x04001EED RID: 7917
		public float turnSpeed = 500f;

		// Token: 0x04001EEE RID: 7918
		private Transform parentTransform;

		// Token: 0x04001EEF RID: 7919
		private Transform tr;

		// Token: 0x04001EF0 RID: 7920
		private float currentYRotation;

		// Token: 0x04001EF1 RID: 7921
		private float fallOffAngle = 90f;

		// Token: 0x04001EF2 RID: 7922
		public bool ignoreControllerMomentum;
	}
}
