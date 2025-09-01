using System;
using UnityEngine;

namespace FIMSpace.Basics
{
	// Token: 0x02000046 RID: 70
	public class FBasic_FreeCameraBehaviour : MonoBehaviour
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000FA78 File Offset: 0x0000DC78
		private void Start()
		{
			this.speeds = Vector3.zero;
			this.ySpeed = 0f;
			this.rotation = base.transform.rotation.eulerAngles;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000FAB4 File Offset: 0x0000DCB4
		private void Update()
		{
			float axis = Input.GetAxis("Vertical");
			float axis2 = Input.GetAxis("Horizontal");
			float num = axis * Time.smoothDeltaTime * this.SpeedMultiplier;
			float num2 = axis2 * Time.smoothDeltaTime * this.SpeedMultiplier;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				this.turbo = Mathf.Lerp(this.turbo, this.turboModeMultiply, Time.smoothDeltaTime * 5f);
			}
			else
			{
				this.turbo = Mathf.Lerp(this.turbo, 1f, Time.smoothDeltaTime * 5f);
			}
			num *= this.turbo;
			num2 *= this.turbo;
			if (Cursor.lockState != CursorLockMode.None && (Input.GetMouseButton(1) || !this.NeedRMB))
			{
				this.rotation.x = this.rotation.x - Input.GetAxis("Mouse Y") * 1f * this.MouseSensitivity;
				this.rotation.y = this.rotation.y + Input.GetAxis("Mouse X") * 1f * this.MouseSensitivity;
			}
			this.speeds.z = Mathf.Lerp(this.speeds.z, num, Time.smoothDeltaTime * this.AccelerationSmothnessValue);
			this.speeds.x = Mathf.Lerp(this.speeds.x, num2, Time.smoothDeltaTime * this.AccelerationSmothnessValue);
			base.transform.position += base.transform.forward * this.speeds.z;
			base.transform.position += base.transform.right * this.speeds.x;
			base.transform.position += base.transform.up * this.speeds.y;
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(this.rotation), Time.smoothDeltaTime * this.RotationSmothnessValue);
			if (Input.GetKey(KeyCode.LeftControl))
			{
				this.ySpeed = Mathf.Lerp(this.ySpeed, 1f, Time.smoothDeltaTime * this.AccelerationSmothnessValue);
			}
			else if (Input.GetButton("Jump"))
			{
				this.ySpeed = Mathf.Lerp(this.ySpeed, -1f, Time.smoothDeltaTime * this.AccelerationSmothnessValue);
			}
			else
			{
				this.ySpeed = Mathf.Lerp(this.ySpeed, 0f, Time.smoothDeltaTime * this.AccelerationSmothnessValue);
			}
			base.transform.position += Vector3.down * this.ySpeed * this.turbo * Time.smoothDeltaTime * this.SpeedMultiplier;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000FD96 File Offset: 0x0000DF96
		public void FixedUpdate()
		{
			if (Input.GetMouseButton(1))
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				return;
			}
			if (this.NeedRMB)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		public FBasic_FreeCameraBehaviour()
		{
		}

		// Token: 0x040001F0 RID: 496
		[Header("> Hold right mouse button to rotate camera <")]
		[Tooltip("How fast camera should fly")]
		public float SpeedMultiplier = 10f;

		// Token: 0x040001F1 RID: 497
		[Tooltip("Value of acceleration smoothness")]
		public float AccelerationSmothnessValue = 10f;

		// Token: 0x040001F2 RID: 498
		[Tooltip("Value of rotation smoothness")]
		public float RotationSmothnessValue = 10f;

		// Token: 0x040001F3 RID: 499
		public float MouseSensitivity = 5f;

		// Token: 0x040001F4 RID: 500
		public bool NeedRMB = true;

		// Token: 0x040001F5 RID: 501
		private float turboModeMultiply = 5f;

		// Token: 0x040001F6 RID: 502
		private Vector3 speeds;

		// Token: 0x040001F7 RID: 503
		private float ySpeed;

		// Token: 0x040001F8 RID: 504
		private Vector3 rotation;

		// Token: 0x040001F9 RID: 505
		private float turbo = 1f;
	}
}
