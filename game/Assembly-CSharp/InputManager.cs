using System;
using System.Collections.Generic;
using Agoston_R.Aim_Assist_Pro.Scripts.AimAssistCode.TargetSelection;
using InControl;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class InputManager : MonoBehaviour
{
	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000896 RID: 2198 RVA: 0x0003AFD9 File Offset: 0x000391D9
	public static bool IsUsingController
	{
		get
		{
			global::InputManager inputManager = global::InputManager.instance;
			return inputManager != null && inputManager.m_State == global::InputManager.eInputState.Controller;
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0003AFEE File Offset: 0x000391EE
	private void Awake()
	{
		global::InputManager.instance = this;
		global::InputManager.IsBindingKeys = false;
		global::InputManager.Actions = InputActions.CreateWithDefaultBindings();
		global::InputManager.UIAct = UIActions.CreateWithDefaultBindings();
		InControl.InputManager.OnDeviceAttached += this.ControllerConnected;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x0003B024 File Offset: 0x00039224
	private void Update()
	{
		if (global::InputManager.AnyKeyboardKeyDown() || this.HasMouseMoved())
		{
			this.SetInputType(global::InputManager.eInputState.MouseKeyboard);
		}
		else if (InControl.InputManager.ActiveDevice.AnyButtonIsPressed || InControl.InputManager.ActiveDevice.LeftStick.HasChanged || InControl.InputManager.ActiveDevice.RightStick.HasChanged || InControl.InputManager.ActiveDevice.LeftCommand.WasPressed || InControl.InputManager.ActiveDevice.RightCommand.WasPressed)
		{
			this.SetInputType(global::InputManager.eInputState.Controller);
		}
		global::InputManager.CacheMousePos();
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0003B0A8 File Offset: 0x000392A8
	public static void SaveBindings()
	{
		string value = global::InputManager.Actions.Save();
		PlayerPrefs.SetString("InputBindings", value);
		PlayerPrefs.Save();
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0003B0D0 File Offset: 0x000392D0
	public static void ResetBindings()
	{
		global::InputManager.Actions.Reset();
		global::InputManager.SaveBindings();
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0003B0E4 File Offset: 0x000392E4
	private void SetInputType(global::InputManager.eInputState input)
	{
		if (this.m_State == input)
		{
			return;
		}
		this.m_State = input;
		Action onInputMethodChanged = global::InputManager.OnInputMethodChanged;
		if (onInputMethodChanged != null)
		{
			onInputMethodChanged();
		}
		TargetSelector.IsEnabled = global::InputManager.IsUsingController;
		global::InputManager.CacheMousePos();
		Debug.Log("Input Changed: " + input.ToString());
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0003B140 File Offset: 0x00039340
	public static void CacheMousePos()
	{
		if (global::InputManager.instance == null)
		{
			return;
		}
		if (global::InputManager.instance.mousePositions.Count >= global::InputManager.instance.framesToCheck)
		{
			global::InputManager.instance.mousePositions.Dequeue();
		}
		global::InputManager.instance.mousePositions.Enqueue(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x0003B1B4 File Offset: 0x000393B4
	private bool HasMouseMoved()
	{
		if (global::InputManager.instance.mousePositions.Count < global::InputManager.instance.framesToCheck)
		{
			return false;
		}
		Vector3? vector = null;
		foreach (Vector3 vector2 in this.mousePositions)
		{
			if (vector != null && Vector2.SqrMagnitude(vector2 - vector.Value) <= 0.033f)
			{
				return false;
			}
			vector = new Vector3?(vector2);
		}
		return true;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0003B25C File Offset: 0x0003945C
	private void ControllerConnected(InputDevice inputDevide)
	{
		this.UpdateControllerType();
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0003B264 File Offset: 0x00039464
	private void UpdateControllerType()
	{
		string[] joystickNames = Input.GetJoystickNames();
		for (int i = 0; i < joystickNames.Length; i++)
		{
			string text = joystickNames[i].ToLower();
			if (text.Contains("xbox"))
			{
				Debug.Log("Detected Playstation Controller");
				global::InputManager.Controller = global::InputManager.ControllerType.Xbox;
				return;
			}
			if (text.Contains("playstation") || text.Contains("dualsense"))
			{
				Debug.Log("Detected Playstation Controller");
				global::InputManager.Controller = global::InputManager.ControllerType.Playstation;
				return;
			}
		}
		global::InputManager.Controller = global::InputManager.ControllerType.Unknown;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0003B2E0 File Offset: 0x000394E0
	private static bool AnyKeyboardKeyDown()
	{
		return Input.anyKeyDown && (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.J) || (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)));
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x0003B40D File Offset: 0x0003960D
	public InputManager()
	{
	}

	// Token: 0x04000747 RID: 1863
	public static global::InputManager instance;

	// Token: 0x04000748 RID: 1864
	public static InputActions Actions;

	// Token: 0x04000749 RID: 1865
	public static UIActions UIAct;

	// Token: 0x0400074A RID: 1866
	public InControlManager Control;

	// Token: 0x0400074B RID: 1867
	public static bool IsBindingKeys;

	// Token: 0x0400074C RID: 1868
	public static global::InputManager.ControllerType Controller;

	// Token: 0x0400074D RID: 1869
	private global::InputManager.eInputState m_State;

	// Token: 0x0400074E RID: 1870
	public static Action OnInputMethodChanged;

	// Token: 0x0400074F RID: 1871
	public static Action OnBindingChanged;

	// Token: 0x04000750 RID: 1872
	private Queue<Vector3> mousePositions = new Queue<Vector3>();

	// Token: 0x04000751 RID: 1873
	private int framesToCheck = 5;

	// Token: 0x020004B9 RID: 1209
	private enum eInputState
	{
		// Token: 0x04002426 RID: 9254
		MouseKeyboard,
		// Token: 0x04002427 RID: 9255
		Controller
	}

	// Token: 0x020004BA RID: 1210
	public enum ControllerType
	{
		// Token: 0x04002429 RID: 9257
		Unknown,
		// Token: 0x0400242A RID: 9258
		Xbox,
		// Token: 0x0400242B RID: 9259
		Playstation,
		// Token: 0x0400242C RID: 9260
		Nintendo
	}
}
