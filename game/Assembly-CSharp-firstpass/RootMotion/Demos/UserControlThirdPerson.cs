using System;
using UnityEngine;

namespace RootMotion.Demos
{
	// Token: 0x02000160 RID: 352
	public class UserControlThirdPerson : MonoBehaviour
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x0005C335 File Offset: 0x0005A535
		protected virtual void Start()
		{
			this.cam = Camera.main.transform;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0005C348 File Offset: 0x0005A548
		protected virtual void Update()
		{
			this.state.crouch = (this.canCrouch && Input.GetKey(KeyCode.C));
			this.state.jump = (this.canJump && Input.GetButton("Jump"));
			float axisRaw = Input.GetAxisRaw("Horizontal");
			float axisRaw2 = Input.GetAxisRaw("Vertical");
			Vector3 vector = this.cam.rotation * new Vector3(axisRaw, 0f, axisRaw2).normalized;
			if (vector != Vector3.zero)
			{
				Vector3 up = base.transform.up;
				Vector3.OrthoNormalize(ref up, ref vector);
				this.state.move = vector;
			}
			else
			{
				this.state.move = Vector3.zero;
			}
			bool key = Input.GetKey(KeyCode.LeftShift);
			float d = this.walkByDefault ? (key ? 1f : 0.5f) : (key ? 0.5f : 1f);
			this.state.move = this.state.move * d;
			this.state.lookPos = base.transform.position + this.cam.forward * 100f;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0005C491 File Offset: 0x0005A691
		public UserControlThirdPerson()
		{
		}

		// Token: 0x04000B8E RID: 2958
		public bool walkByDefault;

		// Token: 0x04000B8F RID: 2959
		public bool canCrouch = true;

		// Token: 0x04000B90 RID: 2960
		public bool canJump = true;

		// Token: 0x04000B91 RID: 2961
		public UserControlThirdPerson.State state;

		// Token: 0x04000B92 RID: 2962
		protected Transform cam;

		// Token: 0x0200023D RID: 573
		public struct State
		{
			// Token: 0x040010CF RID: 4303
			public Vector3 move;

			// Token: 0x040010D0 RID: 4304
			public Vector3 lookPos;

			// Token: 0x040010D1 RID: 4305
			public bool crouch;

			// Token: 0x040010D2 RID: 4306
			public bool jump;

			// Token: 0x040010D3 RID: 4307
			public int actionIndex;
		}
	}
}
