using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000040 RID: 64
	public class FreeCamera : MonoBehaviour
	{
		// Token: 0x0600024F RID: 591 RVA: 0x0000C8FE File Offset: 0x0000AAFE
		private void OnEnable()
		{
			this.RegisterInputs();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C906 File Offset: 0x0000AB06
		private void RegisterInputs()
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C908 File Offset: 0x0000AB08
		private void UpdateInputs()
		{
			this.inputRotateAxisX = 0f;
			this.inputRotateAxisY = 0f;
			this.leftShiftBoost = false;
			this.fire1 = false;
			if (Input.GetMouseButton(1))
			{
				this.leftShiftBoost = true;
				this.inputRotateAxisX = Input.GetAxis(FreeCamera.kMouseX) * this.m_LookSpeedMouse;
				this.inputRotateAxisY = Input.GetAxis(FreeCamera.kMouseY) * this.m_LookSpeedMouse;
			}
			this.inputRotateAxisX += Input.GetAxis(FreeCamera.kRightStickX) * this.m_LookSpeedController * 0.01f;
			this.inputRotateAxisY += Input.GetAxis(FreeCamera.kRightStickY) * this.m_LookSpeedController * 0.01f;
			this.leftShift = Input.GetKey(KeyCode.LeftShift);
			this.fire1 = (Input.GetAxis("Fire1") > 0f);
			this.inputChangeSpeed = Input.GetAxis(FreeCamera.kSpeedAxis);
			this.inputVertical = Input.GetAxis(FreeCamera.kVertical);
			this.inputHorizontal = Input.GetAxis(FreeCamera.kHorizontal);
			this.inputYAxis = Input.GetAxis(FreeCamera.kYAxis);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000CA28 File Offset: 0x0000AC28
		private void Update()
		{
			if (DebugManager.instance.displayRuntimeUI)
			{
				return;
			}
			this.UpdateInputs();
			if (this.inputChangeSpeed != 0f)
			{
				this.m_MoveSpeed += this.inputChangeSpeed * this.m_MoveSpeedIncrement;
				if (this.m_MoveSpeed < this.m_MoveSpeedIncrement)
				{
					this.m_MoveSpeed = this.m_MoveSpeedIncrement;
				}
			}
			if (this.inputRotateAxisX != 0f || this.inputRotateAxisY != 0f || this.inputVertical != 0f || this.inputHorizontal != 0f || this.inputYAxis != 0f)
			{
				float x = base.transform.localEulerAngles.x;
				float y = base.transform.localEulerAngles.y + this.inputRotateAxisX;
				float num = x - this.inputRotateAxisY;
				if (x <= 90f && num >= 0f)
				{
					num = Mathf.Clamp(num, 0f, 90f);
				}
				if (x >= 270f)
				{
					num = Mathf.Clamp(num, 270f, 360f);
				}
				base.transform.localRotation = Quaternion.Euler(num, y, base.transform.localEulerAngles.z);
				float num2 = Time.deltaTime * this.m_MoveSpeed;
				if (this.fire1 || (this.leftShiftBoost && this.leftShift))
				{
					num2 *= this.m_Turbo;
				}
				base.transform.position += base.transform.forward * num2 * this.inputVertical;
				base.transform.position += base.transform.right * num2 * this.inputHorizontal;
				base.transform.position += Vector3.up * num2 * this.inputYAxis;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000CC1F File Offset: 0x0000AE1F
		public FreeCamera()
		{
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000CC60 File Offset: 0x0000AE60
		// Note: this type is marked as 'beforefieldinit'.
		static FreeCamera()
		{
		}

		// Token: 0x0400018D RID: 397
		private const float k_MouseSensitivityMultiplier = 0.01f;

		// Token: 0x0400018E RID: 398
		public float m_LookSpeedController = 120f;

		// Token: 0x0400018F RID: 399
		public float m_LookSpeedMouse = 4f;

		// Token: 0x04000190 RID: 400
		public float m_MoveSpeed = 10f;

		// Token: 0x04000191 RID: 401
		public float m_MoveSpeedIncrement = 2.5f;

		// Token: 0x04000192 RID: 402
		public float m_Turbo = 10f;

		// Token: 0x04000193 RID: 403
		private static string kMouseX = "Mouse X";

		// Token: 0x04000194 RID: 404
		private static string kMouseY = "Mouse Y";

		// Token: 0x04000195 RID: 405
		private static string kRightStickX = "Controller Right Stick X";

		// Token: 0x04000196 RID: 406
		private static string kRightStickY = "Controller Right Stick Y";

		// Token: 0x04000197 RID: 407
		private static string kVertical = "Vertical";

		// Token: 0x04000198 RID: 408
		private static string kHorizontal = "Horizontal";

		// Token: 0x04000199 RID: 409
		private static string kYAxis = "YAxis";

		// Token: 0x0400019A RID: 410
		private static string kSpeedAxis = "Speed Axis";

		// Token: 0x0400019B RID: 411
		private float inputRotateAxisX;

		// Token: 0x0400019C RID: 412
		private float inputRotateAxisY;

		// Token: 0x0400019D RID: 413
		private float inputChangeSpeed;

		// Token: 0x0400019E RID: 414
		private float inputVertical;

		// Token: 0x0400019F RID: 415
		private float inputHorizontal;

		// Token: 0x040001A0 RID: 416
		private float inputYAxis;

		// Token: 0x040001A1 RID: 417
		private bool leftShiftBoost;

		// Token: 0x040001A2 RID: 418
		private bool leftShift;

		// Token: 0x040001A3 RID: 419
		private bool fire1;
	}
}
