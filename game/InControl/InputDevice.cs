using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000033 RID: 51
	public class InputDevice
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00006A01 File Offset: 0x00004C01
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00006A09 File Offset: 0x00004C09
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00006A12 File Offset: 0x00004C12
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00006A1A File Offset: 0x00004C1A
		public string Meta
		{
			[CompilerGenerated]
			get
			{
				return this.<Meta>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Meta>k__BackingField = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00006A23 File Offset: 0x00004C23
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00006A2B File Offset: 0x00004C2B
		public int SortOrder
		{
			[CompilerGenerated]
			get
			{
				return this.<SortOrder>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<SortOrder>k__BackingField = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00006A34 File Offset: 0x00004C34
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00006A3C File Offset: 0x00004C3C
		public InputDeviceClass DeviceClass
		{
			[CompilerGenerated]
			get
			{
				return this.<DeviceClass>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<DeviceClass>k__BackingField = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00006A45 File Offset: 0x00004C45
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00006A4D File Offset: 0x00004C4D
		public InputDeviceStyle DeviceStyle
		{
			[CompilerGenerated]
			get
			{
				return this.<DeviceStyle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<DeviceStyle>k__BackingField = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00006A56 File Offset: 0x00004C56
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00006A5E File Offset: 0x00004C5E
		public Guid GUID
		{
			[CompilerGenerated]
			get
			{
				return this.<GUID>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GUID>k__BackingField = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00006A67 File Offset: 0x00004C67
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00006A6F File Offset: 0x00004C6F
		public ulong LastInputTick
		{
			[CompilerGenerated]
			get
			{
				return this.<LastInputTick>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LastInputTick>k__BackingField = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00006A78 File Offset: 0x00004C78
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00006A80 File Offset: 0x00004C80
		public bool IsActive
		{
			[CompilerGenerated]
			get
			{
				return this.<IsActive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsActive>k__BackingField = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00006A89 File Offset: 0x00004C89
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00006A91 File Offset: 0x00004C91
		public bool IsAttached
		{
			[CompilerGenerated]
			get
			{
				return this.<IsAttached>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsAttached>k__BackingField = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00006A9A File Offset: 0x00004C9A
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00006AA2 File Offset: 0x00004CA2
		private protected bool RawSticks
		{
			[CompilerGenerated]
			protected get
			{
				return this.<RawSticks>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RawSticks>k__BackingField = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00006AAB File Offset: 0x00004CAB
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00006AB3 File Offset: 0x00004CB3
		public ReadOnlyCollection<InputControl> Controls
		{
			[CompilerGenerated]
			get
			{
				return this.<Controls>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Controls>k__BackingField = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00006ABC File Offset: 0x00004CBC
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00006AC4 File Offset: 0x00004CC4
		private protected InputControl[] ControlsByTarget
		{
			[CompilerGenerated]
			protected get
			{
				return this.<ControlsByTarget>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ControlsByTarget>k__BackingField = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00006ACD File Offset: 0x00004CCD
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00006AD5 File Offset: 0x00004CD5
		public TwoAxisInputControl LeftStick
		{
			[CompilerGenerated]
			get
			{
				return this.<LeftStick>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LeftStick>k__BackingField = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00006ADE File Offset: 0x00004CDE
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00006AE6 File Offset: 0x00004CE6
		public TwoAxisInputControl RightStick
		{
			[CompilerGenerated]
			get
			{
				return this.<RightStick>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RightStick>k__BackingField = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00006AEF File Offset: 0x00004CEF
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00006AF7 File Offset: 0x00004CF7
		public TwoAxisInputControl DPad
		{
			[CompilerGenerated]
			get
			{
				return this.<DPad>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DPad>k__BackingField = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006B00 File Offset: 0x00004D00
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00006B08 File Offset: 0x00004D08
		public InputControlType LeftCommandControl
		{
			[CompilerGenerated]
			get
			{
				return this.<LeftCommandControl>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LeftCommandControl>k__BackingField = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00006B11 File Offset: 0x00004D11
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00006B19 File Offset: 0x00004D19
		public InputControlType RightCommandControl
		{
			[CompilerGenerated]
			get
			{
				return this.<RightCommandControl>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RightCommandControl>k__BackingField = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00006B22 File Offset: 0x00004D22
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00006B2A File Offset: 0x00004D2A
		protected InputDevice.AnalogSnapshotEntry[] AnalogSnapshot
		{
			[CompilerGenerated]
			get
			{
				return this.<AnalogSnapshot>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AnalogSnapshot>k__BackingField = value;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006B33 File Offset: 0x00004D33
		public InputDevice() : this("")
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006B40 File Offset: 0x00004D40
		public InputDevice(string name) : this(name, false)
		{
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006B4C File Offset: 0x00004D4C
		public InputDevice(string name, bool rawSticks)
		{
			this.Name = name;
			this.RawSticks = rawSticks;
			this.Meta = "";
			this.GUID = Guid.NewGuid();
			this.LastInputTick = 0UL;
			this.SortOrder = int.MaxValue;
			this.DeviceClass = InputDeviceClass.Unknown;
			this.DeviceStyle = InputDeviceStyle.Unknown;
			this.Passive = false;
			this.ControlsByTarget = new InputControl[531];
			this.controls = new List<InputControl>(32);
			this.Controls = new ReadOnlyCollection<InputControl>(this.controls);
			this.RemoveAliasControls();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006BDF File Offset: 0x00004DDF
		internal void OnAttached()
		{
			this.IsAttached = true;
			this.AddAliasControls();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006BEE File Offset: 0x00004DEE
		internal void OnDetached()
		{
			this.IsAttached = false;
			this.StopVibration();
			this.RemoveAliasControls();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006C04 File Offset: 0x00004E04
		private void AddAliasControls()
		{
			this.RemoveAliasControls();
			if (this.IsKnown)
			{
				this.LeftStick = new TwoAxisInputControl();
				this.RightStick = new TwoAxisInputControl();
				this.DPad = new TwoAxisInputControl();
				this.DPad.DeadZoneFunc = new DeadZoneFunc(DeadZone.Separate);
				this.AddControl(InputControlType.LeftStickX, "Left Stick X");
				this.AddControl(InputControlType.LeftStickY, "Left Stick Y");
				this.AddControl(InputControlType.RightStickX, "Right Stick X");
				this.AddControl(InputControlType.RightStickY, "Right Stick Y");
				this.AddControl(InputControlType.DPadX, "DPad X");
				this.AddControl(InputControlType.DPadY, "DPad Y");
				this.AddControl(InputControlType.Command, "Command");
				this.LeftCommandControl = this.DeviceStyle.LeftCommandControl();
				this.leftCommandSource = this.GetControl(this.LeftCommandControl);
				this.hasLeftCommandControl = !this.leftCommandSource.IsNullControl;
				if (this.hasLeftCommandControl)
				{
					this.AddControl(InputControlType.LeftCommand, this.leftCommandSource.Handle);
				}
				this.RightCommandControl = this.DeviceStyle.RightCommandControl();
				this.rightCommandSource = this.GetControl(this.RightCommandControl);
				this.hasRightCommandControl = !this.rightCommandSource.IsNullControl;
				if (this.hasRightCommandControl)
				{
					this.AddControl(InputControlType.RightCommand, this.rightCommandSource.Handle);
				}
				this.ExpireControlCache();
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00006D84 File Offset: 0x00004F84
		private void RemoveAliasControls()
		{
			this.LeftStick = TwoAxisInputControl.Null;
			this.RightStick = TwoAxisInputControl.Null;
			this.DPad = TwoAxisInputControl.Null;
			this.RemoveControl(InputControlType.LeftStickX);
			this.RemoveControl(InputControlType.LeftStickY);
			this.RemoveControl(InputControlType.RightStickX);
			this.RemoveControl(InputControlType.RightStickY);
			this.RemoveControl(InputControlType.DPadX);
			this.RemoveControl(InputControlType.DPadY);
			this.RemoveControl(InputControlType.Command);
			this.RemoveControl(InputControlType.LeftCommand);
			this.RemoveControl(InputControlType.RightCommand);
			this.leftCommandSource = null;
			this.hasLeftCommandControl = false;
			this.rightCommandSource = null;
			this.hasRightCommandControl = false;
			this.ExpireControlCache();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006E37 File Offset: 0x00005037
		protected void ClearControls()
		{
			Array.Clear(this.ControlsByTarget, 0, this.ControlsByTarget.Length);
			this.controls.Clear();
			this.ExpireControlCache();
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006E5E File Offset: 0x0000505E
		public bool HasControl(InputControlType controlType)
		{
			return this.ControlsByTarget[(int)controlType] != null;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006E6B File Offset: 0x0000506B
		public InputControl GetControl(InputControlType controlType)
		{
			return this.ControlsByTarget[(int)controlType] ?? InputControl.Null;
		}

		// Token: 0x170000B2 RID: 178
		public InputControl this[InputControlType controlType]
		{
			get
			{
				return this.GetControl(controlType);
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006E87 File Offset: 0x00005087
		public static InputControlType GetInputControlTypeByName(string inputControlName)
		{
			return (InputControlType)Enum.Parse(typeof(InputControlType), inputControlName);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006EA0 File Offset: 0x000050A0
		public InputControl GetControlByName(string controlName)
		{
			InputControlType inputControlTypeByName = InputDevice.GetInputControlTypeByName(controlName);
			return this.GetControl(inputControlTypeByName);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00006EBC File Offset: 0x000050BC
		public InputControl AddControl(InputControlType controlType, string handle)
		{
			InputControl inputControl = this.ControlsByTarget[(int)controlType];
			if (inputControl == null)
			{
				inputControl = new InputControl(handle, controlType);
				this.ControlsByTarget[(int)controlType] = inputControl;
				this.controls.Add(inputControl);
				this.ExpireControlCache();
			}
			return inputControl;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006EF9 File Offset: 0x000050F9
		public InputControl AddControl(InputControlType controlType, string handle, float lowerDeadZone, float upperDeadZone)
		{
			InputControl inputControl = this.AddControl(controlType, handle);
			inputControl.LowerDeadZone = lowerDeadZone;
			inputControl.UpperDeadZone = upperDeadZone;
			return inputControl;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006F14 File Offset: 0x00005114
		private void RemoveControl(InputControlType controlType)
		{
			InputControl inputControl = this.ControlsByTarget[(int)controlType];
			if (inputControl != null)
			{
				this.ControlsByTarget[(int)controlType] = null;
				this.controls.Remove(inputControl);
				this.ExpireControlCache();
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00006F4C File Offset: 0x0000514C
		public void ClearInputState()
		{
			this.LeftStick.ClearInputState();
			this.RightStick.ClearInputState();
			this.DPad.ClearInputState();
			int count = this.Controls.Count;
			for (int i = 0; i < count; i++)
			{
				InputControl inputControl = this.Controls[i];
				if (inputControl != null)
				{
					inputControl.ClearInputState();
				}
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006FA8 File Offset: 0x000051A8
		protected void UpdateWithState(InputControlType controlType, bool state, ulong updateTick, float deltaTime)
		{
			this.GetControl(controlType).UpdateWithState(state, updateTick, deltaTime);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006FBB File Offset: 0x000051BB
		protected void UpdateWithValue(InputControlType controlType, float value, ulong updateTick, float deltaTime)
		{
			this.GetControl(controlType).UpdateWithValue(value, updateTick, deltaTime);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006FCE File Offset: 0x000051CE
		protected void UpdateWithRawValue(InputControlType controlType, float value, ulong updateTick, float deltaTime)
		{
			this.GetControl(controlType).UpdateWithRawValue(value, updateTick, deltaTime);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006FE4 File Offset: 0x000051E4
		public void UpdateLeftStickWithValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.LeftStickLeft.UpdateWithValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.LeftStickRight.UpdateWithValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.LeftStickUp.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				return;
			}
			this.LeftStickUp.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			this.LeftStickDown.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000070B0 File Offset: 0x000052B0
		public void UpdateLeftStickWithRawValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.LeftStickLeft.UpdateWithRawValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.LeftStickRight.UpdateWithRawValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.LeftStickUp.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				return;
			}
			this.LeftStickUp.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			this.LeftStickDown.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000717C File Offset: 0x0000537C
		public void CommitLeftStick()
		{
			this.LeftStickUp.Commit();
			this.LeftStickDown.Commit();
			this.LeftStickLeft.Commit();
			this.LeftStickRight.Commit();
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000071AC File Offset: 0x000053AC
		public void UpdateRightStickWithValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.RightStickLeft.UpdateWithValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.RightStickRight.UpdateWithValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.RightStickUp.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				return;
			}
			this.RightStickUp.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			this.RightStickDown.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007278 File Offset: 0x00005478
		public void UpdateRightStickWithRawValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.RightStickLeft.UpdateWithRawValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.RightStickRight.UpdateWithRawValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.RightStickUp.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				return;
			}
			this.RightStickUp.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			this.RightStickDown.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007344 File Offset: 0x00005544
		public void CommitRightStick()
		{
			this.RightStickUp.Commit();
			this.RightStickDown.Commit();
			this.RightStickLeft.Commit();
			this.RightStickRight.Commit();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007372 File Offset: 0x00005572
		public virtual void Update(ulong updateTick, float deltaTime)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007374 File Offset: 0x00005574
		private void ProcessLeftStick(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.LeftStickLeft.NextRawValue, this.LeftStickRight.NextRawValue);
			float y = Utility.ValueFromSides(this.LeftStickDown.NextRawValue, this.LeftStickUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector;
			if (this.RawSticks || this.LeftStickLeft.Raw || this.LeftStickRight.Raw || this.LeftStickUp.Raw || this.LeftStickDown.Raw)
			{
				vector = new Vector2(x, y);
			}
			else
			{
				float lowerDeadZone = Utility.Max(this.LeftStickLeft.LowerDeadZone, this.LeftStickRight.LowerDeadZone, this.LeftStickUp.LowerDeadZone, this.LeftStickDown.LowerDeadZone);
				float upperDeadZone = Utility.Min(this.LeftStickLeft.UpperDeadZone, this.LeftStickRight.UpperDeadZone, this.LeftStickUp.UpperDeadZone, this.LeftStickDown.UpperDeadZone);
				vector = this.LeftStick.DeadZoneFunc(x, y, lowerDeadZone, upperDeadZone);
			}
			this.LeftStick.Raw = true;
			this.LeftStick.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.LeftStickX.Raw = true;
			this.LeftStickX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.LeftStickY.Raw = true;
			this.LeftStickY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.LeftStickLeft.SetValue(this.LeftStick.Left.Value, updateTick);
			this.LeftStickRight.SetValue(this.LeftStick.Right.Value, updateTick);
			this.LeftStickUp.SetValue(this.LeftStick.Up.Value, updateTick);
			this.LeftStickDown.SetValue(this.LeftStick.Down.Value, updateTick);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007554 File Offset: 0x00005754
		private void ProcessRightStick(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.RightStickLeft.NextRawValue, this.RightStickRight.NextRawValue);
			float y = Utility.ValueFromSides(this.RightStickDown.NextRawValue, this.RightStickUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector;
			if (this.RawSticks || this.RightStickLeft.Raw || this.RightStickRight.Raw || this.RightStickUp.Raw || this.RightStickDown.Raw)
			{
				vector = new Vector2(x, y);
			}
			else
			{
				float lowerDeadZone = Utility.Max(this.RightStickLeft.LowerDeadZone, this.RightStickRight.LowerDeadZone, this.RightStickUp.LowerDeadZone, this.RightStickDown.LowerDeadZone);
				float upperDeadZone = Utility.Min(this.RightStickLeft.UpperDeadZone, this.RightStickRight.UpperDeadZone, this.RightStickUp.UpperDeadZone, this.RightStickDown.UpperDeadZone);
				vector = this.RightStick.DeadZoneFunc(x, y, lowerDeadZone, upperDeadZone);
			}
			this.RightStick.Raw = true;
			this.RightStick.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.RightStickX.Raw = true;
			this.RightStickX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.RightStickY.Raw = true;
			this.RightStickY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.RightStickLeft.SetValue(this.RightStick.Left.Value, updateTick);
			this.RightStickRight.SetValue(this.RightStick.Right.Value, updateTick);
			this.RightStickUp.SetValue(this.RightStick.Up.Value, updateTick);
			this.RightStickDown.SetValue(this.RightStick.Down.Value, updateTick);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007734 File Offset: 0x00005934
		private void ProcessDPad(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.DPadLeft.NextRawValue, this.DPadRight.NextRawValue);
			float y = Utility.ValueFromSides(this.DPadDown.NextRawValue, this.DPadUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector;
			if (this.RawSticks || this.DPadLeft.Raw || this.DPadRight.Raw || this.DPadUp.Raw || this.DPadDown.Raw)
			{
				vector = new Vector2(x, y);
			}
			else
			{
				float lowerDeadZone = Utility.Max(this.DPadLeft.LowerDeadZone, this.DPadRight.LowerDeadZone, this.DPadUp.LowerDeadZone, this.DPadDown.LowerDeadZone);
				float upperDeadZone = Utility.Min(this.DPadLeft.UpperDeadZone, this.DPadRight.UpperDeadZone, this.DPadUp.UpperDeadZone, this.DPadDown.UpperDeadZone);
				vector = this.DPad.DeadZoneFunc(x, y, lowerDeadZone, upperDeadZone);
			}
			this.DPad.Raw = true;
			this.DPad.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.DPadX.Raw = true;
			this.DPadX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.DPadY.Raw = true;
			this.DPadY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.DPadLeft.SetValue(this.DPad.Left.Value, updateTick);
			this.DPadRight.SetValue(this.DPad.Right.Value, updateTick);
			this.DPadUp.SetValue(this.DPad.Up.Value, updateTick);
			this.DPadDown.SetValue(this.DPad.Down.Value, updateTick);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007914 File Offset: 0x00005B14
		public void Commit(ulong updateTick, float deltaTime)
		{
			if (this.IsKnown)
			{
				this.ProcessLeftStick(updateTick, deltaTime);
				this.ProcessRightStick(updateTick, deltaTime);
				this.ProcessDPad(updateTick, deltaTime);
			}
			int count = this.Controls.Count;
			for (int i = 0; i < count; i++)
			{
				InputControl inputControl = this.Controls[i];
				if (inputControl != null)
				{
					inputControl.Commit();
				}
			}
			if (this.IsKnown)
			{
				bool passive = true;
				bool state = false;
				for (int j = 100; j <= 116; j++)
				{
					InputControl inputControl2 = this.ControlsByTarget[j];
					if (inputControl2 != null && inputControl2.IsPressed)
					{
						state = true;
						if (!inputControl2.Passive)
						{
							passive = false;
						}
					}
				}
				this.Command.Passive = passive;
				this.Command.CommitWithState(state, updateTick, deltaTime);
				if (this.hasLeftCommandControl)
				{
					this.LeftCommand.Passive = this.leftCommandSource.Passive;
					this.LeftCommand.CommitWithState(this.leftCommandSource.IsPressed, updateTick, deltaTime);
				}
				if (this.hasRightCommandControl)
				{
					this.RightCommand.Passive = this.rightCommandSource.Passive;
					this.RightCommand.CommitWithState(this.rightCommandSource.IsPressed, updateTick, deltaTime);
				}
			}
			this.IsActive = false;
			for (int k = 0; k < count; k++)
			{
				InputControl inputControl3 = this.Controls[k];
				if (inputControl3 != null && inputControl3.HasInput && !inputControl3.Passive)
				{
					this.LastInputTick = updateTick;
					this.IsActive = true;
				}
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007A88 File Offset: 0x00005C88
		public bool LastInputAfter(InputDevice device)
		{
			return device == null || this.LastInputTick > device.LastInputTick;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007A9D File Offset: 0x00005C9D
		public void RequestActivation()
		{
			this.LastInputTick = InputManager.CurrentTick;
			this.IsActive = true;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007AB1 File Offset: 0x00005CB1
		public virtual void Vibrate(float leftSpeed, float rightSpeed)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007AB3 File Offset: 0x00005CB3
		public void Vibrate(float intensity)
		{
			this.Vibrate(intensity, intensity);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007ABD File Offset: 0x00005CBD
		public virtual void VibrateTriggers(float leftTriggerSpeed, float rightTriggerSpeed)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007ABF File Offset: 0x00005CBF
		public void StopVibration()
		{
			this.Vibrate(0f);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007ACC File Offset: 0x00005CCC
		public virtual void SetLightColor(float red, float green, float blue)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007ACE File Offset: 0x00005CCE
		public void SetLightColor(Color color)
		{
			this.SetLightColor(color.r * color.a, color.g * color.a, color.b * color.a);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007AFD File Offset: 0x00005CFD
		public virtual void SetLightFlash(float flashOnDuration, float flashOffDuration)
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007AFF File Offset: 0x00005CFF
		public void StopLightFlash()
		{
			this.SetLightFlash(1f, 0f);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00007B11 File Offset: 0x00005D11
		public virtual bool IsSupportedOnThisPlatform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00007B14 File Offset: 0x00005D14
		public virtual bool IsKnown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00007B17 File Offset: 0x00005D17
		public bool IsUnknown
		{
			get
			{
				return !this.IsKnown;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00007B22 File Offset: 0x00005D22
		[Obsolete("Use InputDevice.CommandIsPressed instead.", false)]
		public bool MenuIsPressed
		{
			get
			{
				return this.IsKnown && this.Command.IsPressed;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00007B39 File Offset: 0x00005D39
		[Obsolete("Use InputDevice.CommandWasPressed instead.", false)]
		public bool MenuWasPressed
		{
			get
			{
				return this.IsKnown && this.Command.WasPressed;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00007B50 File Offset: 0x00005D50
		[Obsolete("Use InputDevice.CommandWasReleased instead.", false)]
		public bool MenuWasReleased
		{
			get
			{
				return this.IsKnown && this.Command.WasReleased;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00007B67 File Offset: 0x00005D67
		public bool CommandIsPressed
		{
			get
			{
				return this.IsKnown && this.Command.IsPressed;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00007B7E File Offset: 0x00005D7E
		public bool CommandWasPressed
		{
			get
			{
				return this.IsKnown && this.Command.WasPressed;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00007B95 File Offset: 0x00005D95
		public bool CommandWasReleased
		{
			get
			{
				return this.IsKnown && this.Command.WasReleased;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00007BAC File Offset: 0x00005DAC
		public InputControl AnyButton
		{
			get
			{
				int count = this.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					InputControl inputControl = this.Controls[i];
					if (inputControl != null && inputControl.IsButton && inputControl.IsPressed)
					{
						return inputControl;
					}
				}
				return InputControl.Null;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public bool AnyButtonIsPressed
		{
			get
			{
				int count = this.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					InputControl inputControl = this.Controls[i];
					if (inputControl != null && inputControl.IsButton && inputControl.IsPressed)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00007C40 File Offset: 0x00005E40
		public bool AnyButtonWasPressed
		{
			get
			{
				int count = this.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					InputControl inputControl = this.Controls[i];
					if (inputControl != null && inputControl.IsButton && inputControl.WasPressed)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00007C88 File Offset: 0x00005E88
		public bool AnyButtonWasReleased
		{
			get
			{
				int count = this.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					InputControl inputControl = this.Controls[i];
					if (inputControl != null && inputControl.IsButton && inputControl.WasReleased)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public TwoAxisInputControl Direction
		{
			get
			{
				if (this.DPad.UpdateTick <= this.LeftStick.UpdateTick)
				{
					return this.LeftStick;
				}
				return this.DPad;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00007CF8 File Offset: 0x00005EF8
		public InputControl LeftStickUp
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftStickUp) == null)
				{
					result = (this.cachedLeftStickUp = this.GetControl(InputControlType.LeftStickUp));
				}
				return result;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007D20 File Offset: 0x00005F20
		public InputControl LeftStickDown
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftStickDown) == null)
				{
					result = (this.cachedLeftStickDown = this.GetControl(InputControlType.LeftStickDown));
				}
				return result;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00007D48 File Offset: 0x00005F48
		public InputControl LeftStickLeft
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftStickLeft) == null)
				{
					result = (this.cachedLeftStickLeft = this.GetControl(InputControlType.LeftStickLeft));
				}
				return result;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00007D70 File Offset: 0x00005F70
		public InputControl LeftStickRight
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftStickRight) == null)
				{
					result = (this.cachedLeftStickRight = this.GetControl(InputControlType.LeftStickRight));
				}
				return result;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00007D98 File Offset: 0x00005F98
		public InputControl RightStickUp
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightStickUp) == null)
				{
					result = (this.cachedRightStickUp = this.GetControl(InputControlType.RightStickUp));
				}
				return result;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public InputControl RightStickDown
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightStickDown) == null)
				{
					result = (this.cachedRightStickDown = this.GetControl(InputControlType.RightStickDown));
				}
				return result;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00007DE8 File Offset: 0x00005FE8
		public InputControl RightStickLeft
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightStickLeft) == null)
				{
					result = (this.cachedRightStickLeft = this.GetControl(InputControlType.RightStickLeft));
				}
				return result;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00007E10 File Offset: 0x00006010
		public InputControl RightStickRight
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightStickRight) == null)
				{
					result = (this.cachedRightStickRight = this.GetControl(InputControlType.RightStickRight));
				}
				return result;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00007E38 File Offset: 0x00006038
		public InputControl DPadUp
		{
			get
			{
				InputControl result;
				if ((result = this.cachedDPadUp) == null)
				{
					result = (this.cachedDPadUp = this.GetControl(InputControlType.DPadUp));
				}
				return result;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00007E60 File Offset: 0x00006060
		public InputControl DPadDown
		{
			get
			{
				InputControl result;
				if ((result = this.cachedDPadDown) == null)
				{
					result = (this.cachedDPadDown = this.GetControl(InputControlType.DPadDown));
				}
				return result;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00007E88 File Offset: 0x00006088
		public InputControl DPadLeft
		{
			get
			{
				InputControl result;
				if ((result = this.cachedDPadLeft) == null)
				{
					result = (this.cachedDPadLeft = this.GetControl(InputControlType.DPadLeft));
				}
				return result;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00007EB0 File Offset: 0x000060B0
		public InputControl DPadRight
		{
			get
			{
				InputControl result;
				if ((result = this.cachedDPadRight) == null)
				{
					result = (this.cachedDPadRight = this.GetControl(InputControlType.DPadRight));
				}
				return result;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00007ED8 File Offset: 0x000060D8
		public InputControl Action1
		{
			get
			{
				InputControl result;
				if ((result = this.cachedAction1) == null)
				{
					result = (this.cachedAction1 = this.GetControl(InputControlType.Action1));
				}
				return result;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00007F00 File Offset: 0x00006100
		public InputControl Action2
		{
			get
			{
				InputControl result;
				if ((result = this.cachedAction2) == null)
				{
					result = (this.cachedAction2 = this.GetControl(InputControlType.Action2));
				}
				return result;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00007F28 File Offset: 0x00006128
		public InputControl Action3
		{
			get
			{
				InputControl result;
				if ((result = this.cachedAction3) == null)
				{
					result = (this.cachedAction3 = this.GetControl(InputControlType.Action3));
				}
				return result;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00007F50 File Offset: 0x00006150
		public InputControl Action4
		{
			get
			{
				InputControl result;
				if ((result = this.cachedAction4) == null)
				{
					result = (this.cachedAction4 = this.GetControl(InputControlType.Action4));
				}
				return result;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00007F78 File Offset: 0x00006178
		public InputControl LeftTrigger
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftTrigger) == null)
				{
					result = (this.cachedLeftTrigger = this.GetControl(InputControlType.LeftTrigger));
				}
				return result;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00007FA0 File Offset: 0x000061A0
		public InputControl RightTrigger
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightTrigger) == null)
				{
					result = (this.cachedRightTrigger = this.GetControl(InputControlType.RightTrigger));
				}
				return result;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007FC8 File Offset: 0x000061C8
		public InputControl LeftBumper
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftBumper) == null)
				{
					result = (this.cachedLeftBumper = this.GetControl(InputControlType.LeftBumper));
				}
				return result;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00007FF0 File Offset: 0x000061F0
		public InputControl RightBumper
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightBumper) == null)
				{
					result = (this.cachedRightBumper = this.GetControl(InputControlType.RightBumper));
				}
				return result;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008018 File Offset: 0x00006218
		public InputControl LeftStickButton
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftStickButton) == null)
				{
					result = (this.cachedLeftStickButton = this.GetControl(InputControlType.LeftStickButton));
				}
				return result;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00008040 File Offset: 0x00006240
		public InputControl RightStickButton
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightStickButton) == null)
				{
					result = (this.cachedRightStickButton = this.GetControl(InputControlType.RightStickButton));
				}
				return result;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00008068 File Offset: 0x00006268
		public InputControl LeftStickX
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftStickX) == null)
				{
					result = (this.cachedLeftStickX = this.GetControl(InputControlType.LeftStickX));
				}
				return result;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00008094 File Offset: 0x00006294
		public InputControl LeftStickY
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftStickY) == null)
				{
					result = (this.cachedLeftStickY = this.GetControl(InputControlType.LeftStickY));
				}
				return result;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000080C0 File Offset: 0x000062C0
		public InputControl RightStickX
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightStickX) == null)
				{
					result = (this.cachedRightStickX = this.GetControl(InputControlType.RightStickX));
				}
				return result;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000080EC File Offset: 0x000062EC
		public InputControl RightStickY
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightStickY) == null)
				{
					result = (this.cachedRightStickY = this.GetControl(InputControlType.RightStickY));
				}
				return result;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00008118 File Offset: 0x00006318
		public InputControl DPadX
		{
			get
			{
				InputControl result;
				if ((result = this.cachedDPadX) == null)
				{
					result = (this.cachedDPadX = this.GetControl(InputControlType.DPadX));
				}
				return result;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00008144 File Offset: 0x00006344
		public InputControl DPadY
		{
			get
			{
				InputControl result;
				if ((result = this.cachedDPadY) == null)
				{
					result = (this.cachedDPadY = this.GetControl(InputControlType.DPadY));
				}
				return result;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00008170 File Offset: 0x00006370
		public InputControl Command
		{
			get
			{
				InputControl result;
				if ((result = this.cachedCommand) == null)
				{
					result = (this.cachedCommand = this.GetControl(InputControlType.Command));
				}
				return result;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000819C File Offset: 0x0000639C
		public InputControl LeftCommand
		{
			get
			{
				InputControl result;
				if ((result = this.cachedLeftCommand) == null)
				{
					result = (this.cachedLeftCommand = this.GetControl(InputControlType.LeftCommand));
				}
				return result;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000081C8 File Offset: 0x000063C8
		public InputControl RightCommand
		{
			get
			{
				InputControl result;
				if ((result = this.cachedRightCommand) == null)
				{
					result = (this.cachedRightCommand = this.GetControl(InputControlType.RightCommand));
				}
				return result;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000081F4 File Offset: 0x000063F4
		private void ExpireControlCache()
		{
			this.cachedLeftStickUp = null;
			this.cachedLeftStickDown = null;
			this.cachedLeftStickLeft = null;
			this.cachedLeftStickRight = null;
			this.cachedRightStickUp = null;
			this.cachedRightStickDown = null;
			this.cachedRightStickLeft = null;
			this.cachedRightStickRight = null;
			this.cachedDPadUp = null;
			this.cachedDPadDown = null;
			this.cachedDPadLeft = null;
			this.cachedDPadRight = null;
			this.cachedAction1 = null;
			this.cachedAction2 = null;
			this.cachedAction3 = null;
			this.cachedAction4 = null;
			this.cachedLeftTrigger = null;
			this.cachedRightTrigger = null;
			this.cachedLeftBumper = null;
			this.cachedRightBumper = null;
			this.cachedLeftStickButton = null;
			this.cachedRightStickButton = null;
			this.cachedLeftStickX = null;
			this.cachedLeftStickY = null;
			this.cachedRightStickX = null;
			this.cachedRightStickY = null;
			this.cachedDPadX = null;
			this.cachedDPadY = null;
			this.cachedCommand = null;
			this.cachedLeftCommand = null;
			this.cachedRightCommand = null;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000082DA File Offset: 0x000064DA
		public virtual int NumUnknownAnalogs
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000082DD File Offset: 0x000064DD
		public virtual int NumUnknownButtons
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000082E0 File Offset: 0x000064E0
		public virtual bool ReadRawButtonState(int index)
		{
			return false;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000082E3 File Offset: 0x000064E3
		public virtual float ReadRawAnalogValue(int index)
		{
			return 0f;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000082EC File Offset: 0x000064EC
		public void TakeSnapshot()
		{
			if (this.AnalogSnapshot == null)
			{
				this.AnalogSnapshot = new InputDevice.AnalogSnapshotEntry[this.NumUnknownAnalogs];
			}
			for (int i = 0; i < this.NumUnknownAnalogs; i++)
			{
				float value = Utility.ApplySnapping(this.ReadRawAnalogValue(i), 0.5f);
				this.AnalogSnapshot[i].value = value;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008348 File Offset: 0x00006548
		public UnknownDeviceControl GetFirstPressedAnalog()
		{
			if (this.AnalogSnapshot != null)
			{
				for (int i = 0; i < this.NumUnknownAnalogs; i++)
				{
					InputControlType control = InputControlType.Analog0 + i;
					float num = Utility.ApplySnapping(this.ReadRawAnalogValue(i), 0.5f);
					float num2 = num - this.AnalogSnapshot[i].value;
					this.AnalogSnapshot[i].TrackMinMaxValue(num);
					if (num2 > 0.1f)
					{
						num2 = this.AnalogSnapshot[i].maxValue - this.AnalogSnapshot[i].value;
					}
					if (num2 < -0.1f)
					{
						num2 = this.AnalogSnapshot[i].minValue - this.AnalogSnapshot[i].value;
					}
					if (num2 > 1.9f)
					{
						return new UnknownDeviceControl(control, InputRangeType.MinusOneToOne);
					}
					if (num2 < -0.9f)
					{
						return new UnknownDeviceControl(control, InputRangeType.ZeroToMinusOne);
					}
					if (num2 > 0.9f)
					{
						return new UnknownDeviceControl(control, InputRangeType.ZeroToOne);
					}
				}
			}
			return UnknownDeviceControl.None;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008444 File Offset: 0x00006644
		public UnknownDeviceControl GetFirstPressedButton()
		{
			for (int i = 0; i < this.NumUnknownButtons; i++)
			{
				if (this.ReadRawButtonState(i))
				{
					return new UnknownDeviceControl(InputControlType.Button0 + i, InputRangeType.ZeroToOne);
				}
			}
			return UnknownDeviceControl.None;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000847E File Offset: 0x0000667E
		// Note: this type is marked as 'beforefieldinit'.
		static InputDevice()
		{
		}

		// Token: 0x04000237 RID: 567
		public static readonly InputDevice Null = new InputDevice("None");

		// Token: 0x04000238 RID: 568
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000239 RID: 569
		[CompilerGenerated]
		private string <Meta>k__BackingField;

		// Token: 0x0400023A RID: 570
		[CompilerGenerated]
		private int <SortOrder>k__BackingField;

		// Token: 0x0400023B RID: 571
		[CompilerGenerated]
		private InputDeviceClass <DeviceClass>k__BackingField;

		// Token: 0x0400023C RID: 572
		[CompilerGenerated]
		private InputDeviceStyle <DeviceStyle>k__BackingField;

		// Token: 0x0400023D RID: 573
		[CompilerGenerated]
		private Guid <GUID>k__BackingField;

		// Token: 0x0400023E RID: 574
		[CompilerGenerated]
		private ulong <LastInputTick>k__BackingField;

		// Token: 0x0400023F RID: 575
		[CompilerGenerated]
		private bool <IsActive>k__BackingField;

		// Token: 0x04000240 RID: 576
		[CompilerGenerated]
		private bool <IsAttached>k__BackingField;

		// Token: 0x04000241 RID: 577
		[CompilerGenerated]
		private bool <RawSticks>k__BackingField;

		// Token: 0x04000242 RID: 578
		private readonly List<InputControl> controls;

		// Token: 0x04000243 RID: 579
		[CompilerGenerated]
		private ReadOnlyCollection<InputControl> <Controls>k__BackingField;

		// Token: 0x04000244 RID: 580
		[CompilerGenerated]
		private InputControl[] <ControlsByTarget>k__BackingField;

		// Token: 0x04000245 RID: 581
		[CompilerGenerated]
		private TwoAxisInputControl <LeftStick>k__BackingField;

		// Token: 0x04000246 RID: 582
		[CompilerGenerated]
		private TwoAxisInputControl <RightStick>k__BackingField;

		// Token: 0x04000247 RID: 583
		[CompilerGenerated]
		private TwoAxisInputControl <DPad>k__BackingField;

		// Token: 0x04000248 RID: 584
		private bool hasLeftCommandControl;

		// Token: 0x04000249 RID: 585
		private InputControl leftCommandSource;

		// Token: 0x0400024A RID: 586
		[CompilerGenerated]
		private InputControlType <LeftCommandControl>k__BackingField;

		// Token: 0x0400024B RID: 587
		private bool hasRightCommandControl;

		// Token: 0x0400024C RID: 588
		private InputControl rightCommandSource;

		// Token: 0x0400024D RID: 589
		[CompilerGenerated]
		private InputControlType <RightCommandControl>k__BackingField;

		// Token: 0x0400024E RID: 590
		public bool Passive;

		// Token: 0x0400024F RID: 591
		[CompilerGenerated]
		private InputDevice.AnalogSnapshotEntry[] <AnalogSnapshot>k__BackingField;

		// Token: 0x04000250 RID: 592
		private InputControl cachedLeftStickUp;

		// Token: 0x04000251 RID: 593
		private InputControl cachedLeftStickDown;

		// Token: 0x04000252 RID: 594
		private InputControl cachedLeftStickLeft;

		// Token: 0x04000253 RID: 595
		private InputControl cachedLeftStickRight;

		// Token: 0x04000254 RID: 596
		private InputControl cachedRightStickUp;

		// Token: 0x04000255 RID: 597
		private InputControl cachedRightStickDown;

		// Token: 0x04000256 RID: 598
		private InputControl cachedRightStickLeft;

		// Token: 0x04000257 RID: 599
		private InputControl cachedRightStickRight;

		// Token: 0x04000258 RID: 600
		private InputControl cachedDPadUp;

		// Token: 0x04000259 RID: 601
		private InputControl cachedDPadDown;

		// Token: 0x0400025A RID: 602
		private InputControl cachedDPadLeft;

		// Token: 0x0400025B RID: 603
		private InputControl cachedDPadRight;

		// Token: 0x0400025C RID: 604
		private InputControl cachedAction1;

		// Token: 0x0400025D RID: 605
		private InputControl cachedAction2;

		// Token: 0x0400025E RID: 606
		private InputControl cachedAction3;

		// Token: 0x0400025F RID: 607
		private InputControl cachedAction4;

		// Token: 0x04000260 RID: 608
		private InputControl cachedLeftTrigger;

		// Token: 0x04000261 RID: 609
		private InputControl cachedRightTrigger;

		// Token: 0x04000262 RID: 610
		private InputControl cachedLeftBumper;

		// Token: 0x04000263 RID: 611
		private InputControl cachedRightBumper;

		// Token: 0x04000264 RID: 612
		private InputControl cachedLeftStickButton;

		// Token: 0x04000265 RID: 613
		private InputControl cachedRightStickButton;

		// Token: 0x04000266 RID: 614
		private InputControl cachedLeftStickX;

		// Token: 0x04000267 RID: 615
		private InputControl cachedLeftStickY;

		// Token: 0x04000268 RID: 616
		private InputControl cachedRightStickX;

		// Token: 0x04000269 RID: 617
		private InputControl cachedRightStickY;

		// Token: 0x0400026A RID: 618
		private InputControl cachedDPadX;

		// Token: 0x0400026B RID: 619
		private InputControl cachedDPadY;

		// Token: 0x0400026C RID: 620
		private InputControl cachedCommand;

		// Token: 0x0400026D RID: 621
		private InputControl cachedLeftCommand;

		// Token: 0x0400026E RID: 622
		private InputControl cachedRightCommand;

		// Token: 0x0200020F RID: 527
		protected struct AnalogSnapshotEntry
		{
			// Token: 0x0600090A RID: 2314 RVA: 0x00052BF4 File Offset: 0x00050DF4
			public void TrackMinMaxValue(float currentValue)
			{
				this.maxValue = Mathf.Max(this.maxValue, currentValue);
				this.minValue = Mathf.Min(this.minValue, currentValue);
			}

			// Token: 0x0400044D RID: 1101
			public float value;

			// Token: 0x0400044E RID: 1102
			public float maxValue;

			// Token: 0x0400044F RID: 1103
			public float minValue;
		}
	}
}
