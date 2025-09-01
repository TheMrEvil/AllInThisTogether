using System;
using System.Runtime.CompilerServices;
using XInputDotNetPure;

namespace InControl
{
	// Token: 0x02000073 RID: 115
	public class XInputDevice : InputDevice
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000135D5 File Offset: 0x000117D5
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x000135DD File Offset: 0x000117DD
		public int DeviceIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<DeviceIndex>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DeviceIndex>k__BackingField = value;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000135E8 File Offset: 0x000117E8
		public XInputDevice(int deviceIndex, XInputDeviceManager owner) : base("XInput Controller")
		{
			this.owner = owner;
			this.DeviceIndex = deviceIndex;
			base.SortOrder = deviceIndex;
			base.Meta = "XInput Device #" + deviceIndex.ToString();
			base.DeviceClass = InputDeviceClass.Controller;
			base.DeviceStyle = InputDeviceStyle.XboxOne;
			base.AddControl(InputControlType.LeftStickLeft, "Left Stick Left", 0.2f, 0.9f);
			base.AddControl(InputControlType.LeftStickRight, "Left Stick Right", 0.2f, 0.9f);
			base.AddControl(InputControlType.LeftStickUp, "Left Stick Up", 0.2f, 0.9f);
			base.AddControl(InputControlType.LeftStickDown, "Left Stick Down", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightStickLeft, "Right Stick Left", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightStickRight, "Right Stick Right", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightStickUp, "Right Stick Up", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightStickDown, "Right Stick Down", 0.2f, 0.9f);
			base.AddControl(InputControlType.LeftTrigger, "Left Trigger", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightTrigger, "Right Trigger", 0.2f, 0.9f);
			base.AddControl(InputControlType.DPadUp, "DPad Up", 0.2f, 0.9f);
			base.AddControl(InputControlType.DPadDown, "DPad Down", 0.2f, 0.9f);
			base.AddControl(InputControlType.DPadLeft, "DPad Left", 0.2f, 0.9f);
			base.AddControl(InputControlType.DPadRight, "DPad Right", 0.2f, 0.9f);
			base.AddControl(InputControlType.Action1, "A");
			base.AddControl(InputControlType.Action2, "B");
			base.AddControl(InputControlType.Action3, "X");
			base.AddControl(InputControlType.Action4, "Y");
			base.AddControl(InputControlType.LeftBumper, "Left Bumper");
			base.AddControl(InputControlType.RightBumper, "Right Bumper");
			base.AddControl(InputControlType.LeftStickButton, "Left Stick Button");
			base.AddControl(InputControlType.RightStickButton, "Right Stick Button");
			base.AddControl(InputControlType.View, "View");
			base.AddControl(InputControlType.Menu, "Menu");
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00013810 File Offset: 0x00011A10
		public override void Update(ulong updateTick, float deltaTime)
		{
			this.GetState();
			base.UpdateLeftStickWithValue(this.state.ThumbSticks.Left.Vector, updateTick, deltaTime);
			base.UpdateRightStickWithValue(this.state.ThumbSticks.Right.Vector, updateTick, deltaTime);
			base.UpdateWithValue(InputControlType.LeftTrigger, this.state.Triggers.Left, updateTick, deltaTime);
			base.UpdateWithValue(InputControlType.RightTrigger, this.state.Triggers.Right, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.DPadUp, this.state.DPad.Up == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.DPadDown, this.state.DPad.Down == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.DPadLeft, this.state.DPad.Left == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.DPadRight, this.state.DPad.Right == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action1, this.state.Buttons.A == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action2, this.state.Buttons.B == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action3, this.state.Buttons.X == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action4, this.state.Buttons.Y == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.LeftBumper, this.state.Buttons.LeftShoulder == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.RightBumper, this.state.Buttons.RightShoulder == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.LeftStickButton, this.state.Buttons.LeftStick == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.RightStickButton, this.state.Buttons.RightStick == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.View, this.state.Buttons.Back == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Menu, this.state.Buttons.Start == ButtonState.Pressed, updateTick, deltaTime);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00013A6C File Offset: 0x00011C6C
		public override void Vibrate(float leftSpeed, float rightSpeed)
		{
			GamePad.SetVibration((PlayerIndex)this.DeviceIndex, leftSpeed, rightSpeed);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00013A7B File Offset: 0x00011C7B
		internal void GetState()
		{
			this.state = this.owner.GetState(this.DeviceIndex);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00013A94 File Offset: 0x00011C94
		public bool IsConnected
		{
			get
			{
				return this.state.IsConnected;
			}
		}

		// Token: 0x04000420 RID: 1056
		private const float LowerDeadZone = 0.2f;

		// Token: 0x04000421 RID: 1057
		private const float UpperDeadZone = 0.9f;

		// Token: 0x04000422 RID: 1058
		private readonly XInputDeviceManager owner;

		// Token: 0x04000423 RID: 1059
		private GamePadState state;

		// Token: 0x04000424 RID: 1060
		[CompilerGenerated]
		private int <DeviceIndex>k__BackingField;
	}
}
