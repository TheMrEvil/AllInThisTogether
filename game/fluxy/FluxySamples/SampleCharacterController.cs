using System;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x02000018 RID: 24
	[RequireComponent(typeof(FluxyCharacter))]
	public class SampleCharacterController : MonoBehaviour
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00006653 File Offset: 0x00004853
		private void Start()
		{
			if (Camera.main != null)
			{
				this.m_Cam = Camera.main.transform;
			}
			else
			{
				Debug.LogWarning("Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
			}
			this.m_Character = base.GetComponent<FluxyCharacter>();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000668C File Offset: 0x0000488C
		private void Update()
		{
			if (!this.m_Jump)
			{
				this.m_Jump = Input.GetButtonDown("Jump");
			}
			float axis = Input.GetAxis("Horizontal");
			float axis2 = Input.GetAxis("Vertical");
			bool key = Input.GetKey(KeyCode.C);
			if (this.m_Cam != null)
			{
				this.m_CamForward = Vector3.Scale(this.m_Cam.forward, new Vector3(1f, 0f, 1f)).normalized;
				this.m_Move = axis2 * this.m_CamForward + axis * this.m_Cam.right;
			}
			else
			{
				this.m_Move = axis2 * Vector3.forward + axis * Vector3.right;
			}
			if (Input.GetKey(KeyCode.LeftShift))
			{
				this.m_Move *= 0.5f;
			}
			this.m_Character.Move(this.m_Move, key, this.m_Jump);
			this.m_Jump = false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000679B File Offset: 0x0000499B
		public SampleCharacterController()
		{
		}

		// Token: 0x040000BA RID: 186
		private FluxyCharacter m_Character;

		// Token: 0x040000BB RID: 187
		private Transform m_Cam;

		// Token: 0x040000BC RID: 188
		private Vector3 m_CamForward;

		// Token: 0x040000BD RID: 189
		private Vector3 m_Move;

		// Token: 0x040000BE RID: 190
		private bool m_Jump;
	}
}
