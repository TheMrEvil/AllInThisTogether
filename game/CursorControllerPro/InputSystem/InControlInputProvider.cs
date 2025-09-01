using System;
using System.Collections.Generic;
using InControl;
using UnityEngine;

namespace SlimUI.CursorControllerPro.InputSystem
{
	// Token: 0x02000014 RID: 20
	public class InControlInputProvider : MonoBehaviour, IInputProvider
	{
		// Token: 0x06000062 RID: 98 RVA: 0x00003F14 File Offset: 0x00002114
		public InputType GetActiveInputType()
		{
			foreach (InputDevice inputDevice in InputManager.Devices)
			{
				if (inputDevice.AnyButton || Math.Abs(inputDevice.LeftStickX.Value) > this.leftStickDeadZone || Math.Abs(inputDevice.LeftStickY.Value) > this.leftStickDeadZone)
				{
					return InputType.Gamepad;
				}
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

		// Token: 0x06000063 RID: 99 RVA: 0x00003FD0 File Offset: 0x000021D0
		public Vector2 GetAbsolutePosition()
		{
			return Input.mousePosition;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003FDC File Offset: 0x000021DC
		public Vector2 GetRelativeMovement(GamepadPlayerNum player = GamepadPlayerNum.One)
		{
			InputDevice activeDevice;
			if (!this.playerInputMapping.TryGetValue(player, out activeDevice))
			{
				activeDevice = InputManager.ActiveDevice;
				if (this.playerInputMapping.ContainsValue(activeDevice))
				{
					return Vector2.zero;
				}
				if (activeDevice.LeftStick.magnitude > this.leftStickDeadZone)
				{
					this.playerInputMapping.Add(player, activeDevice);
					return activeDevice.LeftStick;
				}
			}
			if (activeDevice.LeftStick.magnitude <= this.leftStickDeadZone)
			{
				return Vector2.zero;
			}
			return activeDevice.LeftStick;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004073 File Offset: 0x00002273
		public bool GetSubmitWasPressed()
		{
			return InputManager.ActiveDevice.GetControl(InputControlType.Action1).WasPressed;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004086 File Offset: 0x00002286
		public bool GetSubmitWasReleased()
		{
			return InputManager.ActiveDevice.GetControl(InputControlType.Action1).WasReleased;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004099 File Offset: 0x00002299
		public InControlInputProvider()
		{
		}

		// Token: 0x0400008B RID: 139
		private Dictionary<GamepadPlayerNum, InputDevice> playerInputMapping = new Dictionary<GamepadPlayerNum, InputDevice>();

		// Token: 0x0400008C RID: 140
		[Tooltip("The value of left joystick movement that is needed until the joystick registers input detection.")]
		[Range(0f, 1f)]
		public float leftStickDeadZone = 0.1f;
	}
}
