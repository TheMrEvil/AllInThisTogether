using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000132 RID: 306
	public class CharacterController3rdPerson : MonoBehaviour
	{
		// Token: 0x06000CCF RID: 3279 RVA: 0x00056D87 File Offset: 0x00054F87
		private void Start()
		{
			this.animatorController = base.GetComponent<AnimatorController3rdPerson>();
			this.cam.enabled = false;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00056DA4 File Offset: 0x00054FA4
		private void LateUpdate()
		{
			this.cam.UpdateInput();
			this.cam.UpdateTransform();
			Vector3 inputVector = CharacterController3rdPerson.inputVector;
			bool isMoving = CharacterController3rdPerson.inputVector != Vector3.zero || CharacterController3rdPerson.inputVectorRaw != Vector3.zero;
			Vector3 forward = this.cam.transform.forward;
			Vector3 aimTarget = this.cam.transform.position + forward * 10f;
			this.animatorController.Move(inputVector, isMoving, forward, aimTarget);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x00056E32 File Offset: 0x00055032
		private static Vector3 inputVector
		{
			get
			{
				return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00056E52 File Offset: 0x00055052
		private static Vector3 inputVectorRaw
		{
			get
			{
				return new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00056E72 File Offset: 0x00055072
		public CharacterController3rdPerson()
		{
		}

		// Token: 0x04000A49 RID: 2633
		public CameraController cam;

		// Token: 0x04000A4A RID: 2634
		private AnimatorController3rdPerson animatorController;
	}
}
