using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000075 RID: 117
	public class TestInputManager : MonoBehaviour
	{
		// Token: 0x060005A3 RID: 1443 RVA: 0x00013DB0 File Offset: 0x00011FB0
		private void OnEnable()
		{
			Application.targetFrameRate = -1;
			QualitySettings.vSyncCount = 0;
			this.isPaused = false;
			Time.timeScale = 1f;
			Logger.OnLogMessage += delegate(LogMessage logMessage)
			{
				this.logMessages.Add(logMessage);
			};
			InputManager.OnDeviceAttached += delegate(InputDevice inputDevice)
			{
				Debug.Log("Attached: " + inputDevice.Name);
			};
			InputManager.OnDeviceDetached += delegate(InputDevice inputDevice)
			{
				Debug.Log("Detached: " + inputDevice.Name);
			};
			InputManager.OnActiveDeviceChanged += delegate(InputDevice inputDevice)
			{
				Debug.Log("Active device changed to: " + inputDevice.Name);
			};
			InputManager.OnUpdate += this.HandleInputUpdate;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00013E68 File Offset: 0x00012068
		private void HandleInputUpdate(ulong updateTick, float deltaTime)
		{
			this.CheckForPauseButton();
			int count = InputManager.Devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.Devices[i];
				if (inputDevice.LeftBumper || inputDevice.RightBumper)
				{
					inputDevice.VibrateTriggers(inputDevice.LeftTrigger, inputDevice.RightTrigger);
					inputDevice.Vibrate(0f, 0f);
				}
				else
				{
					inputDevice.Vibrate(inputDevice.LeftTrigger, inputDevice.RightTrigger);
					inputDevice.VibrateTriggers(0f, 0f);
				}
				Color color = Color.HSVToRGB(Mathf.Repeat(Time.realtimeSinceStartup * 0.1f, 1f), 1f, 1f);
				inputDevice.SetLightColor(color.r, color.g, color.b);
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00013F55 File Offset: 0x00012155
		private void Start()
		{
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00013F57 File Offset: 0x00012157
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Utility.LoadScene("TestInputManager");
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				InputManager.Enabled = !InputManager.Enabled;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00013F82 File Offset: 0x00012182
		private void CheckForPauseButton()
		{
			if (Input.GetKeyDown(KeyCode.P) || InputManager.CommandWasPressed)
			{
				Time.timeScale = (this.isPaused ? 1f : 0f);
				this.isPaused = !this.isPaused;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00013FBC File Offset: 0x000121BC
		private void SetColor(Color color)
		{
			this.style.normal.textColor = color;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00013FD0 File Offset: 0x000121D0
		private void OnGUI()
		{
			int num = Mathf.FloorToInt((float)(Screen.width / Mathf.Max(1, InputManager.Devices.Count)));
			int num2 = 10;
			int num3 = 10;
			GUI.skin.font = this.font;
			this.SetColor(Color.white);
			string text = "Devices:";
			text = text + " (Platform: " + InputManager.Platform + ")";
			text = text + " " + InputManager.ActiveDevice.Direction.Vector.ToString();
			if (this.isPaused)
			{
				this.SetColor(Color.red);
				text = "+++ PAUSED +++";
			}
			GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text, this.style);
			this.SetColor(Color.white);
			foreach (InputDevice inputDevice in InputManager.Devices)
			{
				Color color = inputDevice.IsActive ? new Color(0.9f, 0.7f, 0.2f) : Color.white;
				bool flag = InputManager.ActiveDevice == inputDevice;
				if (flag)
				{
					color = new Color(1f, 0.9f, 0f);
				}
				num3 = 35;
				if (inputDevice.IsUnknown)
				{
					this.SetColor(Color.red);
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "Unknown Device", this.style);
				}
				else
				{
					this.SetColor(color);
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), inputDevice.Name, this.style);
				}
				num3 += 15;
				this.SetColor(color);
				if (inputDevice.IsUnknown)
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), inputDevice.Meta, this.style);
					num3 += 15;
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "Style: " + inputDevice.DeviceStyle.ToString(), this.style);
				num3 += 15;
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "GUID: " + inputDevice.GUID.ToString(), this.style);
				num3 += 15;
				NativeInputDevice nativeInputDevice = inputDevice as NativeInputDevice;
				if (nativeInputDevice != null)
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "Profile: " + nativeInputDevice.ProfileName, this.style);
					num3 += 15;
					string text2 = string.Format("VID: 0x{0:x}, PID: 0x{1:x}, VER: 0x{2:x}", nativeInputDevice.Info.vendorID, nativeInputDevice.Info.productID, nativeInputDevice.Info.versionNumber);
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text2, this.style);
					num3 += 15;
					text2 = string.Format("DRV: {0}, TSP: {1}", nativeInputDevice.Info.driverType, nativeInputDevice.Info.transportType);
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text2, this.style);
					num3 += 15;
				}
				num3 += 15;
				foreach (InputControl inputControl in inputDevice.Controls)
				{
					if (inputControl != null && !Utility.TargetIsAlias(inputControl.Target))
					{
						string arg = (inputDevice.IsKnown && nativeInputDevice != null) ? nativeInputDevice.GetAppleGlyphNameForControl(inputControl.Target) : "";
						string arg2 = inputDevice.IsKnown ? string.Format("{0} ({1}) {2}", inputControl.Target, inputControl.Handle, arg) : inputControl.Handle;
						this.SetColor(inputControl.State ? Color.green : color);
						string text3 = string.Format("{0} {1}", arg2, inputControl.State ? ("= " + inputControl.Value.ToString()) : "");
						GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
						num3 += 15;
					}
				}
				num3 += 15;
				color = (flag ? new Color(0.85f, 0.65f, 0.12f) : Color.white);
				if (inputDevice.IsKnown)
				{
					InputControl inputControl2 = inputDevice.Command;
					this.SetColor(inputControl2.State ? Color.green : color);
					string text4 = string.Format("{0} {1}", "Command", inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					inputControl2 = inputDevice.LeftCommand;
					this.SetColor(inputControl2.State ? Color.green : color);
					string arg3 = inputDevice.IsKnown ? string.Format("{0} ({1})", inputControl2.Target, inputControl2.Handle) : inputControl2.Handle;
					text4 = string.Format("{0} {1}", arg3, inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					inputControl2 = inputDevice.RightCommand;
					this.SetColor(inputControl2.State ? Color.green : color);
					arg3 = (inputDevice.IsKnown ? string.Format("{0} ({1})", inputControl2.Target, inputControl2.Handle) : inputControl2.Handle);
					text4 = string.Format("{0} {1}", arg3, inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					inputControl2 = inputDevice.LeftStickX;
					this.SetColor(inputControl2.State ? Color.green : color);
					text4 = string.Format("{0} {1}", "Left Stick X", inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					inputControl2 = inputDevice.LeftStickY;
					this.SetColor(inputControl2.State ? Color.green : color);
					text4 = string.Format("{0} {1}", "Left Stick Y", inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					this.SetColor(inputDevice.LeftStick.State ? Color.green : color);
					text4 = string.Format("{0} {1}", "Left Stick A", inputDevice.LeftStick.State ? ("= " + inputDevice.LeftStick.Angle.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					inputControl2 = inputDevice.RightStickX;
					this.SetColor(inputControl2.State ? Color.green : color);
					text4 = string.Format("{0} {1}", "Right Stick X", inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					inputControl2 = inputDevice.RightStickY;
					this.SetColor(inputControl2.State ? Color.green : color);
					text4 = string.Format("{0} {1}", "Right Stick Y", inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					this.SetColor(inputDevice.RightStick.State ? Color.green : color);
					text4 = string.Format("{0} {1}", "Right Stick A", inputDevice.RightStick.State ? ("= " + inputDevice.RightStick.Angle.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					inputControl2 = inputDevice.DPadX;
					this.SetColor(inputControl2.State ? Color.green : color);
					text4 = string.Format("{0} {1}", "DPad X", inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
					inputControl2 = inputDevice.DPadY;
					this.SetColor(inputControl2.State ? Color.green : color);
					text4 = string.Format("{0} {1}", "DPad Y", inputControl2.State ? ("= " + inputControl2.Value.ToString()) : "");
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
					num3 += 15;
				}
				this.SetColor(Color.cyan);
				InputControl anyButton = inputDevice.AnyButton;
				if (anyButton)
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "AnyButton = " + anyButton.Handle, this.style);
				}
				num2 += num;
			}
			Color[] array = new Color[]
			{
				Color.gray,
				Color.yellow,
				Color.white
			};
			this.SetColor(Color.white);
			num2 = 10;
			num3 = Screen.height - 25;
			for (int i = this.logMessages.Count - 1; i >= 0; i--)
			{
				LogMessage logMessage = this.logMessages[i];
				if (logMessage.Type != LogMessageType.Info)
				{
					this.SetColor(array[(int)logMessage.Type]);
					foreach (string text5 in logMessage.Text.Split('\n', StringSplitOptions.None))
					{
						GUI.Label(new Rect((float)num2, (float)num3, (float)Screen.width, (float)(num3 + 10)), text5, this.style);
						num3 -= 15;
					}
				}
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00014BE4 File Offset: 0x00012DE4
		private void DrawUnityInputDebugger()
		{
			int num = 300;
			int num2 = Screen.width / 2;
			int num3 = 10;
			int num4 = 20;
			this.SetColor(Color.white);
			string[] joystickNames = Input.GetJoystickNames();
			int num5 = joystickNames.Length;
			for (int i = 0; i < num5; i++)
			{
				string text = joystickNames[i];
				int num6 = i + 1;
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), string.Concat(new string[]
				{
					"Joystick ",
					num6.ToString(),
					": \"",
					text,
					"\""
				}), this.style);
				num3 += num4;
				string text2 = "Buttons: ";
				for (int j = 0; j < 20; j++)
				{
					if (Input.GetKey("joystick " + num6.ToString() + " button " + j.ToString()))
					{
						text2 = text2 + "B" + j.ToString() + "  ";
					}
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text2, this.style);
				num3 += num4;
				string text3 = "Analogs: ";
				for (int k = 0; k < 20; k++)
				{
					float axisRaw = Input.GetAxisRaw("joystick " + num6.ToString() + " analog " + k.ToString());
					if (Utility.AbsoluteIsOverThreshold(axisRaw, 0.2f))
					{
						text3 = string.Concat(new string[]
						{
							text3,
							"A",
							k.ToString(),
							": ",
							axisRaw.ToString("0.00"),
							"  "
						});
					}
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
				num3 += num4;
				num3 += 25;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00014DC8 File Offset: 0x00012FC8
		private void OnDrawGizmos()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			Vector2 vector = activeDevice.Direction.Vector;
			Gizmos.color = Color.blue;
			Vector2 vector2 = new Vector2(-3f, -1f);
			Vector2 v = vector2 + vector * 2f;
			Gizmos.DrawSphere(vector2, 0.1f);
			Gizmos.DrawLine(vector2, v);
			Gizmos.DrawSphere(v, 1f);
			Gizmos.color = Color.red;
			Vector2 vector3 = new Vector2(3f, -1f);
			Vector2 v2 = vector3 + activeDevice.RightStick.Vector * 2f;
			Gizmos.DrawSphere(vector3, 0.1f);
			Gizmos.DrawLine(vector3, v2);
			Gizmos.DrawSphere(v2, 1f);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00014EA7 File Offset: 0x000130A7
		public TestInputManager()
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00014EC5 File Offset: 0x000130C5
		[CompilerGenerated]
		private void <OnEnable>b__4_0(LogMessage logMessage)
		{
			this.logMessages.Add(logMessage);
		}

		// Token: 0x0400042B RID: 1067
		public Font font;

		// Token: 0x0400042C RID: 1068
		private readonly GUIStyle style = new GUIStyle();

		// Token: 0x0400042D RID: 1069
		private readonly List<LogMessage> logMessages = new List<LogMessage>();

		// Token: 0x0400042E RID: 1070
		private bool isPaused;

		// Token: 0x0200021B RID: 539
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000928 RID: 2344 RVA: 0x00052EB8 File Offset: 0x000510B8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000929 RID: 2345 RVA: 0x00052EC4 File Offset: 0x000510C4
			public <>c()
			{
			}

			// Token: 0x0600092A RID: 2346 RVA: 0x00052ECC File Offset: 0x000510CC
			internal void <OnEnable>b__4_1(InputDevice inputDevice)
			{
				Debug.Log("Attached: " + inputDevice.Name);
			}

			// Token: 0x0600092B RID: 2347 RVA: 0x00052EE3 File Offset: 0x000510E3
			internal void <OnEnable>b__4_2(InputDevice inputDevice)
			{
				Debug.Log("Detached: " + inputDevice.Name);
			}

			// Token: 0x0600092C RID: 2348 RVA: 0x00052EFA File Offset: 0x000510FA
			internal void <OnEnable>b__4_3(InputDevice inputDevice)
			{
				Debug.Log("Active device changed to: " + inputDevice.Name);
			}

			// Token: 0x0400049E RID: 1182
			public static readonly TestInputManager.<>c <>9 = new TestInputManager.<>c();

			// Token: 0x0400049F RID: 1183
			public static Action<InputDevice> <>9__4_1;

			// Token: 0x040004A0 RID: 1184
			public static Action<InputDevice> <>9__4_2;

			// Token: 0x040004A1 RID: 1185
			public static Action<InputDevice> <>9__4_3;
		}
	}
}
