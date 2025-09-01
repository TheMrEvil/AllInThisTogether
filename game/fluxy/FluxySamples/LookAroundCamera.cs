using System;
using UnityEngine;

namespace FluxySamples
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(Camera))]
	public class LookAroundCamera : MonoBehaviour
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000681C File Offset: 0x00004A1C
		private void Awake()
		{
			this.cam = base.GetComponent<Camera>();
			this.currentShot = new LookAroundCamera.CameraShot(base.transform.position, base.transform.rotation, base.transform.up, this.cam.fieldOfView);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000686C File Offset: 0x00004A6C
		private void LookAt(Vector3 position, Vector3 up)
		{
			this.currentShot.up = up;
			this.currentShot.rotation = Quaternion.LookRotation(position - this.currentShot.position, this.currentShot.up);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000068A8 File Offset: 0x00004AA8
		private void UpdateShot()
		{
			base.transform.position = Vector3.Lerp(base.transform.position, this.currentShot.position, this.translationResponse * Time.deltaTime);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.currentShot.rotation, this.rotationResponse * Time.deltaTime);
			this.cam.fieldOfView = Mathf.Lerp(this.cam.fieldOfView, this.currentShot.fieldOfView, this.fovResponse * Time.deltaTime);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000694C File Offset: 0x00004B4C
		private void LateUpdate()
		{
			Vector3 a = Vector3.zero;
			if (Input.GetKey(KeyCode.W))
			{
				a += this.cam.transform.forward;
			}
			if (Input.GetKey(KeyCode.A))
			{
				a -= this.cam.transform.right;
			}
			if (Input.GetKey(KeyCode.S))
			{
				a -= this.cam.transform.forward;
			}
			if (Input.GetKey(KeyCode.D))
			{
				a += this.cam.transform.right;
			}
			this.currentShot.position = this.currentShot.position + a * Time.deltaTime * this.movementSpeed;
			if (Input.GetKey(KeyCode.Mouse0))
			{
				float angle = Input.GetAxis("Mouse X") * this.rotationSpeed;
				float angle2 = Input.GetAxis("Mouse Y") * this.rotationSpeed;
				Quaternion rotation = this.currentShot.rotation * Quaternion.AngleAxis(angle, Vector3.up) * Quaternion.AngleAxis(angle2, -Vector3.right);
				this.LookAt(this.currentShot.position + rotation * Vector3.forward, Vector3.up);
			}
			this.UpdateShot();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006A9D File Offset: 0x00004C9D
		public LookAroundCamera()
		{
		}

		// Token: 0x040000C1 RID: 193
		private Camera cam;

		// Token: 0x040000C2 RID: 194
		private LookAroundCamera.CameraShot currentShot;

		// Token: 0x040000C3 RID: 195
		public float movementSpeed = 5f;

		// Token: 0x040000C4 RID: 196
		public float rotationSpeed = 8f;

		// Token: 0x040000C5 RID: 197
		public float translationResponse = 10f;

		// Token: 0x040000C6 RID: 198
		public float rotationResponse = 10f;

		// Token: 0x040000C7 RID: 199
		public float fovResponse = 10f;

		// Token: 0x0200002F RID: 47
		public struct CameraShot
		{
			// Token: 0x060000BA RID: 186 RVA: 0x00006F47 File Offset: 0x00005147
			public CameraShot(Vector3 position, Quaternion rotation, Vector3 up, float fieldOfView)
			{
				this.position = position;
				this.rotation = rotation;
				this.up = up;
				this.fieldOfView = fieldOfView;
			}

			// Token: 0x040000F9 RID: 249
			public Vector3 position;

			// Token: 0x040000FA RID: 250
			public Quaternion rotation;

			// Token: 0x040000FB RID: 251
			public Vector3 up;

			// Token: 0x040000FC RID: 252
			public float fieldOfView;
		}
	}
}
