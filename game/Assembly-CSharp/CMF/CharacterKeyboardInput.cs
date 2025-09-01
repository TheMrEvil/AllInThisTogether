using System;
using UnityEngine;

namespace CMF
{
	// Token: 0x020003B5 RID: 949
	public class CharacterKeyboardInput : CharacterInput
	{
		// Token: 0x06001F57 RID: 8023 RVA: 0x000BB574 File Offset: 0x000B9774
		public override float GetHorizontalMovementInput()
		{
			if (this.useRawInput)
			{
				return Input.GetAxisRaw(this.horizontalInputAxis);
			}
			return Input.GetAxis(this.horizontalInputAxis);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000BB595 File Offset: 0x000B9795
		public override float GetVerticalMovementInput()
		{
			if (this.useRawInput)
			{
				return Input.GetAxisRaw(this.verticalInputAxis);
			}
			return Input.GetAxis(this.verticalInputAxis);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x000BB5B6 File Offset: 0x000B97B6
		public override bool IsJumpKeyPressed()
		{
			return Input.GetKey(this.jumpKey);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000BB5C3 File Offset: 0x000B97C3
		public CharacterKeyboardInput()
		{
		}

		// Token: 0x04001F9B RID: 8091
		public string horizontalInputAxis = "Horizontal";

		// Token: 0x04001F9C RID: 8092
		public string verticalInputAxis = "Vertical";

		// Token: 0x04001F9D RID: 8093
		public KeyCode jumpKey = KeyCode.Space;

		// Token: 0x04001F9E RID: 8094
		public bool useRawInput = true;
	}
}
