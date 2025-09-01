using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003B4 RID: 948
	public class CharacterJoystickInput : CharacterInput
	{
		// Token: 0x06001F53 RID: 8019 RVA: 0x000BB4A4 File Offset: 0x000B96A4
		public override float GetHorizontalMovementInput()
		{
			float num;
			if (this.useRawInput)
			{
				num = Input.GetAxisRaw(this.horizontalInputAxis);
			}
			else
			{
				num = Input.GetAxis(this.horizontalInputAxis);
			}
			if (Mathf.Abs(num) < this.deadZoneThreshold)
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x000BB4E8 File Offset: 0x000B96E8
		public override float GetVerticalMovementInput()
		{
			float num;
			if (this.useRawInput)
			{
				num = Input.GetAxisRaw(this.verticalInputAxis);
			}
			else
			{
				num = Input.GetAxis(this.verticalInputAxis);
			}
			if (Mathf.Abs(num) < this.deadZoneThreshold)
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x000BB52C File Offset: 0x000B972C
		public override bool IsJumpKeyPressed()
		{
			return Input.GetKey(this.jumpKey);
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x000BB539 File Offset: 0x000B9739
		public CharacterJoystickInput()
		{
		}

		// Token: 0x04001F96 RID: 8086
		public string horizontalInputAxis = "Horizontal";

		// Token: 0x04001F97 RID: 8087
		public string verticalInputAxis = "Vertical";

		// Token: 0x04001F98 RID: 8088
		public KeyCode jumpKey = KeyCode.Joystick1Button0;

		// Token: 0x04001F99 RID: 8089
		public bool useRawInput = true;

		// Token: 0x04001F9A RID: 8090
		public float deadZoneThreshold = 0.2f;
	}
}
