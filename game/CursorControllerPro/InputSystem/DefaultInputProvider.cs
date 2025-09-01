using System;
using UnityEngine;

namespace SlimUI.CursorControllerPro.InputSystem
{
	// Token: 0x02000013 RID: 19
	public class DefaultInputProvider : MonoBehaviour, IInputProvider
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003C18 File Offset: 0x00001E18
		public InputType GetActiveInputType()
		{
			if (Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Joystick1Button6) || Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKey(KeyCode.Joystick1Button8) || Input.GetKey(KeyCode.Joystick1Button9) || Input.GetKey(KeyCode.Joystick1Button10) || Input.GetKey(KeyCode.Joystick1Button11) || Input.GetKey(KeyCode.Joystick1Button12) || Input.GetKey(KeyCode.Joystick1Button13) || Input.GetKey(KeyCode.Joystick1Button14) || Input.GetKey(KeyCode.Joystick1Button15) || Input.GetKey(KeyCode.Joystick1Button16) || Input.GetKey(KeyCode.Joystick1Button17) || Input.GetKey(KeyCode.Joystick1Button18) || Input.GetKey(KeyCode.Joystick1Button19))
			{
				return InputType.Gamepad;
			}
			if (Input.GetAxis(this.horizontalAxis) > this.deadZone || Input.GetAxis(this.horizontalAxis) < -this.deadZone || Input.GetAxis(this.verticalAxis) > this.deadZone || Input.GetAxis(this.verticalAxis) < -this.deadZone)
			{
				return InputType.Gamepad;
			}
			if (Event.current.isMouse)
			{
				return InputType.MouseAndKeyboard;
			}
			if (Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
			{
				return InputType.MouseAndKeyboard;
			}
			return InputType.None;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003DB5 File Offset: 0x00001FB5
		public Vector2 GetAbsolutePosition()
		{
			return Input.mousePosition;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public Vector2 GetRelativeMovement(GamepadPlayerNum player = GamepadPlayerNum.One)
		{
			switch (player)
			{
			case GamepadPlayerNum.One:
				return new Vector2(Input.GetAxis(this.horizontalAxis), Input.GetAxis(this.verticalAxis));
			case GamepadPlayerNum.Two:
				return new Vector2(Input.GetAxis(this.horizontalAxis2), Input.GetAxis(this.verticalAxis2));
			case GamepadPlayerNum.Three:
				return new Vector2(Input.GetAxis(this.horizontalAxis3), Input.GetAxis(this.verticalAxis3));
			case GamepadPlayerNum.Four:
				return new Vector2(Input.GetAxis(this.horizontalAxis4), Input.GetAxis(this.verticalAxis4));
			default:
				return new Vector2(Input.GetAxis(this.horizontalAxis), Input.GetAxis(this.verticalAxis));
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003E74 File Offset: 0x00002074
		public bool GetSubmitWasPressed()
		{
			return Input.GetButtonDown(this.submitButtonName);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003E81 File Offset: 0x00002081
		public bool GetSubmitWasReleased()
		{
			return Input.GetButtonUp(this.submitButtonName);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003E90 File Offset: 0x00002090
		public DefaultInputProvider()
		{
		}

		// Token: 0x04000081 RID: 129
		private string submitButtonName = "Submit";

		// Token: 0x04000082 RID: 130
		[Tooltip("The axis name in the Input Manager")]
		public string horizontalAxis = "Horizontal";

		// Token: 0x04000083 RID: 131
		public string horizontalAxis2 = "Horizontal2";

		// Token: 0x04000084 RID: 132
		public string horizontalAxis3 = "Horizontal3";

		// Token: 0x04000085 RID: 133
		public string horizontalAxis4 = "Horizontal4";

		// Token: 0x04000086 RID: 134
		[Tooltip("The axis name in the Input Manager")]
		public string verticalAxis = "Vertical";

		// Token: 0x04000087 RID: 135
		public string verticalAxis2 = "Vertical2";

		// Token: 0x04000088 RID: 136
		public string verticalAxis3 = "Vertical3";

		// Token: 0x04000089 RID: 137
		public string verticalAxis4 = "Vertical4";

		// Token: 0x0400008A RID: 138
		[Tooltip("The value of joystick movement that is needed until the joystick registers input detection.")]
		[Range(0f, 1f)]
		public float deadZone = 0.1f;
	}
}
