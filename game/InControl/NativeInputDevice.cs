using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000046 RID: 70
	public class NativeInputDevice : InputDevice
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000B820 File Offset: 0x00009A20
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000B828 File Offset: 0x00009A28
		public uint Handle
		{
			[CompilerGenerated]
			get
			{
				return this.<Handle>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Handle>k__BackingField = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000B831 File Offset: 0x00009A31
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000B839 File Offset: 0x00009A39
		public InputDeviceInfo Info
		{
			[CompilerGenerated]
			get
			{
				return this.<Info>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Info>k__BackingField = value;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000B842 File Offset: 0x00009A42
		internal NativeInputDevice()
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000B85C File Offset: 0x00009A5C
		internal void Initialize(uint deviceHandle, InputDeviceInfo deviceInfo, InputDeviceProfile deviceProfile)
		{
			this.Handle = deviceHandle;
			this.Info = deviceInfo;
			this.profile = deviceProfile;
			base.SortOrder = (int)(1000U + this.Handle);
			this.numUnknownButtons = Math.Min((int)this.Info.numButtons, 20);
			this.numUnknownAnalogs = Math.Min((int)this.Info.numAnalogs, 20);
			this.buttons = new short[this.Info.numButtons];
			this.analogs = new short[this.Info.numAnalogs];
			base.AnalogSnapshot = null;
			this.controlSourceByTarget = new InputControlSource[531];
			base.ClearInputState();
			base.ClearControls();
			if (this.IsKnown)
			{
				base.Name = (this.profile.DeviceName ?? this.Info.name);
				base.Name = base.Name.Replace("{NAME}", this.Info.name).Trim();
				base.Meta = (this.profile.DeviceNotes ?? this.Info.name);
				base.DeviceClass = this.profile.DeviceClass;
				base.DeviceStyle = this.profile.DeviceStyle;
				int analogCount = this.profile.AnalogCount;
				for (int i = 0; i < analogCount; i++)
				{
					InputControlMapping inputControlMapping = this.profile.AnalogMappings[i];
					InputControl inputControl = base.AddControl(inputControlMapping.Target, inputControlMapping.Name);
					inputControl.Sensitivity = Mathf.Min(this.profile.Sensitivity, inputControlMapping.Sensitivity);
					inputControl.LowerDeadZone = Mathf.Max(this.profile.LowerDeadZone, inputControlMapping.LowerDeadZone);
					inputControl.UpperDeadZone = Mathf.Min(this.profile.UpperDeadZone, inputControlMapping.UpperDeadZone);
					inputControl.Raw = inputControlMapping.Raw;
					inputControl.Passive = inputControlMapping.Passive;
					this.controlSourceByTarget[(int)inputControlMapping.Target] = inputControlMapping.Source;
				}
				int buttonCount = this.profile.ButtonCount;
				for (int j = 0; j < buttonCount; j++)
				{
					InputControlMapping inputControlMapping2 = this.profile.ButtonMappings[j];
					base.AddControl(inputControlMapping2.Target, inputControlMapping2.Name).Passive = inputControlMapping2.Passive;
					this.controlSourceByTarget[(int)inputControlMapping2.Target] = inputControlMapping2.Source;
				}
			}
			else
			{
				base.Name = "Unknown Device";
				base.Meta = this.Info.name;
				for (int k = 0; k < this.NumUnknownButtons; k++)
				{
					base.AddControl(InputControlType.Button0 + k, "Button " + k.ToString());
				}
				for (int l = 0; l < this.NumUnknownAnalogs; l++)
				{
					base.AddControl(InputControlType.Analog0 + l, "Analog " + l.ToString(), 0.2f, 0.9f);
				}
			}
			this.skipUpdateFrames = 1;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000BB63 File Offset: 0x00009D63
		internal void Initialize(uint deviceHandle, InputDeviceInfo deviceInfo)
		{
			this.Initialize(deviceHandle, deviceInfo, this.profile);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000BB74 File Offset: 0x00009D74
		public override void Update(ulong updateTick, float deltaTime)
		{
			this.SendStatusUpdates();
			if (this.skipUpdateFrames > 0)
			{
				this.skipUpdateFrames--;
				return;
			}
			IntPtr source;
			if (Native.GetDeviceState(this.Handle, out source))
			{
				Marshal.Copy(source, this.buttons, 0, this.buttons.Length);
				source = new IntPtr(source.ToInt64() + (long)(this.buttons.Length * 2));
				Marshal.Copy(source, this.analogs, 0, this.analogs.Length);
			}
			if (this.IsKnown)
			{
				int analogCount = this.profile.AnalogCount;
				for (int i = 0; i < analogCount; i++)
				{
					InputControlMapping inputControlMapping = this.profile.AnalogMappings[i];
					float value = inputControlMapping.Source.GetValue(this);
					InputControl control = base.GetControl(inputControlMapping.Target);
					if (!inputControlMapping.IgnoreInitialZeroValue || !control.IsOnZeroTick || !Utility.IsZero(value))
					{
						float value2 = inputControlMapping.ApplyToValue(value);
						control.UpdateWithValue(value2, updateTick, deltaTime);
					}
				}
				int buttonCount = this.profile.ButtonCount;
				for (int j = 0; j < buttonCount; j++)
				{
					InputControlMapping inputControlMapping2 = this.profile.ButtonMappings[j];
					bool state = inputControlMapping2.Source.GetState(this);
					base.UpdateWithState(inputControlMapping2.Target, state, updateTick, deltaTime);
				}
				return;
			}
			for (int k = 0; k < this.NumUnknownButtons; k++)
			{
				base.UpdateWithState(InputControlType.Button0 + k, this.ReadRawButtonState(k), updateTick, deltaTime);
			}
			for (int l = 0; l < this.NumUnknownAnalogs; l++)
			{
				base.UpdateWithValue(InputControlType.Analog0 + l, this.ReadRawAnalogValue(l), updateTick, deltaTime);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000BD23 File Offset: 0x00009F23
		public override bool ReadRawButtonState(int index)
		{
			return index < this.buttons.Length && this.buttons[index] > -32767;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000BD41 File Offset: 0x00009F41
		public override float ReadRawAnalogValue(int index)
		{
			if (index < this.analogs.Length)
			{
				return (float)this.analogs[index] / 32767f;
			}
			return 0f;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000BD63 File Offset: 0x00009F63
		private static byte FloatToByte(float value)
		{
			return (byte)(Mathf.Clamp01(value) * 255f);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000BD72 File Offset: 0x00009F72
		public override void Vibrate(float leftSpeed, float rightSpeed)
		{
			this.sendVibrate = true;
			this.vibrateToSend = new Vector2(leftSpeed, rightSpeed);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000BD88 File Offset: 0x00009F88
		public override void VibrateTriggers(float leftTriggerSpeed, float rightTriggerSpeed)
		{
			this.sendVibrateTriggers = true;
			this.vibrateTriggersToSend = new Vector2(leftTriggerSpeed, rightTriggerSpeed);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000BD9E File Offset: 0x00009F9E
		public override void SetLightColor(float red, float green, float blue)
		{
			this.sendLightColor = true;
			this.lightColorToSend = new Vector3(red, green, blue);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000BDB5 File Offset: 0x00009FB5
		public override void SetLightFlash(float flashOnDuration, float flashOffDuration)
		{
			this.sendLightFlash = true;
			this.lightFlashToSend = new Vector2(flashOnDuration, flashOffDuration);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000BDCC File Offset: 0x00009FCC
		private void SendStatusUpdates()
		{
			if (this.sendVibrate && InputManager.CurrentTime - this.lastTimeVibrateWasSent > 0.02f)
			{
				Native.SetHapticState(this.Handle, NativeInputDevice.FloatToByte(this.vibrateToSend.x), NativeInputDevice.FloatToByte(this.vibrateToSend.y));
				this.sendVibrate = false;
				this.lastTimeVibrateWasSent = InputManager.CurrentTime;
				this.vibrateToSend = Vector2.zero;
			}
			if (this.sendVibrateTriggers && InputManager.CurrentTime - this.lastTimeVibrateTriggersWasSent > 0.02f)
			{
				Native.SetTriggersHapticState(this.Handle, NativeInputDevice.FloatToByte(this.vibrateTriggersToSend.x), NativeInputDevice.FloatToByte(this.vibrateTriggersToSend.y));
				this.sendVibrateTriggers = false;
				this.lastTimeVibrateTriggersWasSent = InputManager.CurrentTime;
				this.vibrateTriggersToSend = Vector2.zero;
			}
			if (this.sendLightColor && InputManager.CurrentTime - this.lastTimeLightColorWasSent > 0.02f)
			{
				Native.SetLightColor(this.Handle, NativeInputDevice.FloatToByte(this.lightColorToSend.x), NativeInputDevice.FloatToByte(this.lightColorToSend.y), NativeInputDevice.FloatToByte(this.lightColorToSend.z));
				this.sendLightColor = false;
				this.lastTimeLightColorWasSent = InputManager.CurrentTime;
				this.lightColorToSend = Vector3.zero;
			}
			if (this.sendLightFlash && InputManager.CurrentTime - this.lastTimeLightFlashWasSent > 0.02f)
			{
				Native.SetLightFlash(this.Handle, NativeInputDevice.FloatToByte(this.lightFlashToSend.x), NativeInputDevice.FloatToByte(this.lightFlashToSend.y));
				this.sendLightFlash = false;
				this.lastTimeLightFlashWasSent = InputManager.CurrentTime;
				this.lightFlashToSend = Vector2.zero;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000BF78 File Offset: 0x0000A178
		public string GetAppleGlyphNameForControl(InputControlType controlType)
		{
			InputControlSource inputControlSource = this.controlSourceByTarget[(int)controlType];
			if (inputControlSource.SourceType != InputControlSourceType.None)
			{
				InputControlSourceType sourceType = inputControlSource.SourceType;
				IntPtr zero;
				uint num;
				if (sourceType != InputControlSourceType.Button)
				{
					if (sourceType != InputControlSourceType.Analog)
					{
						zero = IntPtr.Zero;
						num = 0U;
					}
					else
					{
						num = Native.GetAnalogGlyphName(this.Handle, (uint)inputControlSource.Index, out zero);
					}
				}
				else
				{
					num = Native.GetButtonGlyphName(this.Handle, (uint)inputControlSource.Index, out zero);
				}
				if (num > 0U)
				{
					this.glyphName.Clear();
					int num2 = 0;
					while ((long)num2 < (long)((ulong)num))
					{
						this.glyphName.Append((char)Marshal.ReadByte(zero, num2));
						num2++;
					}
					return this.glyphName.ToString();
				}
			}
			return "";
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000C030 File Offset: 0x0000A230
		public bool HasSameVendorID(InputDeviceInfo deviceInfo)
		{
			return this.Info.HasSameVendorID(deviceInfo);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000C04C File Offset: 0x0000A24C
		public bool HasSameProductID(InputDeviceInfo deviceInfo)
		{
			return this.Info.HasSameProductID(deviceInfo);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000C068 File Offset: 0x0000A268
		public bool HasSameVersionNumber(InputDeviceInfo deviceInfo)
		{
			return this.Info.HasSameVersionNumber(deviceInfo);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000C084 File Offset: 0x0000A284
		public bool HasSameLocation(InputDeviceInfo deviceInfo)
		{
			return this.Info.HasSameLocation(deviceInfo);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public bool HasSameSerialNumber(InputDeviceInfo deviceInfo)
		{
			return this.Info.HasSameSerialNumber(deviceInfo);
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000C0BC File Offset: 0x0000A2BC
		public string ProfileName
		{
			get
			{
				if (this.profile != null)
				{
					return this.profile.GetType().Name;
				}
				return "N/A";
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000C0DC File Offset: 0x0000A2DC
		public override bool IsSupportedOnThisPlatform
		{
			get
			{
				return this.profile == null || this.profile.IsSupportedOnThisPlatform;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
		public override bool IsKnown
		{
			get
			{
				return this.profile != null;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000C0FE File Offset: 0x0000A2FE
		public override int NumUnknownButtons
		{
			get
			{
				return this.numUnknownButtons;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000C106 File Offset: 0x0000A306
		public override int NumUnknownAnalogs
		{
			get
			{
				return this.numUnknownAnalogs;
			}
		}

		// Token: 0x04000320 RID: 800
		private const int maxUnknownButtons = 20;

		// Token: 0x04000321 RID: 801
		private const int maxUnknownAnalogs = 20;

		// Token: 0x04000322 RID: 802
		[CompilerGenerated]
		private uint <Handle>k__BackingField;

		// Token: 0x04000323 RID: 803
		[CompilerGenerated]
		private InputDeviceInfo <Info>k__BackingField;

		// Token: 0x04000324 RID: 804
		private short[] buttons;

		// Token: 0x04000325 RID: 805
		private short[] analogs;

		// Token: 0x04000326 RID: 806
		private InputDeviceProfile profile;

		// Token: 0x04000327 RID: 807
		private int skipUpdateFrames;

		// Token: 0x04000328 RID: 808
		private int numUnknownButtons;

		// Token: 0x04000329 RID: 809
		private int numUnknownAnalogs;

		// Token: 0x0400032A RID: 810
		private InputControlSource[] controlSourceByTarget;

		// Token: 0x0400032B RID: 811
		private bool sendVibrate;

		// Token: 0x0400032C RID: 812
		private float lastTimeVibrateWasSent;

		// Token: 0x0400032D RID: 813
		private Vector2 vibrateToSend;

		// Token: 0x0400032E RID: 814
		private bool sendVibrateTriggers;

		// Token: 0x0400032F RID: 815
		private float lastTimeVibrateTriggersWasSent;

		// Token: 0x04000330 RID: 816
		private Vector2 vibrateTriggersToSend;

		// Token: 0x04000331 RID: 817
		private bool sendLightColor;

		// Token: 0x04000332 RID: 818
		private float lastTimeLightColorWasSent;

		// Token: 0x04000333 RID: 819
		private Vector3 lightColorToSend;

		// Token: 0x04000334 RID: 820
		private bool sendLightFlash;

		// Token: 0x04000335 RID: 821
		private float lastTimeLightFlashWasSent;

		// Token: 0x04000336 RID: 822
		private Vector2 lightFlashToSend;

		// Token: 0x04000337 RID: 823
		private readonly StringBuilder glyphName = new StringBuilder(256);

		// Token: 0x04000338 RID: 824
		private const string defaultGlyphName = "";
	}
}
