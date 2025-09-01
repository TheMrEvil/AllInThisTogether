using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003B1 RID: 945
	public class CameraJoystickInput : CameraInput
	{
		// Token: 0x06001F49 RID: 8009 RVA: 0x000BB2FC File Offset: 0x000B94FC
		public override float GetHorizontalCameraInput()
		{
			float num = Input.GetAxisRaw(this.joystickHorizontalAxis);
			if (Mathf.Abs(num) < this.deadZoneThreshold)
			{
				num = 0f;
			}
			if (this.invertHorizontalInput)
			{
				return num * -1f;
			}
			return num;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000BB33C File Offset: 0x000B953C
		public override float GetVerticalCameraInput()
		{
			float num = Input.GetAxisRaw(this.joystickVerticalAxis);
			if (Mathf.Abs(num) < this.deadZoneThreshold)
			{
				num = 0f;
			}
			if (this.invertVerticalInput)
			{
				return num;
			}
			return num * -1f;
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x000BB37C File Offset: 0x000B957C
		public CameraJoystickInput()
		{
		}

		// Token: 0x04001F8C RID: 8076
		public string joystickHorizontalAxis = "Joystick X";

		// Token: 0x04001F8D RID: 8077
		public string joystickVerticalAxis = "Joystick Y";

		// Token: 0x04001F8E RID: 8078
		public bool invertHorizontalInput;

		// Token: 0x04001F8F RID: 8079
		public bool invertVerticalInput;

		// Token: 0x04001F90 RID: 8080
		public float deadZoneThreshold = 0.2f;
	}
}
