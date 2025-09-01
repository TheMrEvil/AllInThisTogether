using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200005F RID: 95
	public class UnityInputDevice : InputDevice
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00010526 File Offset: 0x0000E726
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0001052E File Offset: 0x0000E72E
		public int JoystickId
		{
			[CompilerGenerated]
			get
			{
				return this.<JoystickId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<JoystickId>k__BackingField = value;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00010537 File Offset: 0x0000E737
		public UnityInputDevice(int joystickId, string joystickName) : this(null, joystickId, joystickName)
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00010544 File Offset: 0x0000E744
		public UnityInputDevice(InputDeviceProfile deviceProfile, int joystickId, string joystickName)
		{
			this.profile = deviceProfile;
			this.JoystickId = joystickId;
			if (joystickId != 0)
			{
				base.SortOrder = 100 + joystickId;
			}
			UnityInputDevice.SetupAnalogQueries();
			UnityInputDevice.SetupButtonQueries();
			base.AnalogSnapshot = null;
			if (this.IsKnown)
			{
				base.Name = this.profile.DeviceName;
				base.Meta = this.profile.DeviceNotes;
				base.DeviceClass = this.profile.DeviceClass;
				base.DeviceStyle = this.profile.DeviceStyle;
				int analogCount = this.profile.AnalogCount;
				for (int i = 0; i < analogCount; i++)
				{
					InputControlMapping inputControlMapping = this.profile.AnalogMappings[i];
					if (Utility.TargetIsAlias(inputControlMapping.Target))
					{
						Logger.LogError(string.Concat(new string[]
						{
							"Cannot map control \"",
							inputControlMapping.Name,
							"\" as InputControlType.",
							inputControlMapping.Target.ToString(),
							" in profile \"",
							deviceProfile.DeviceName,
							"\" because this target is reserved as an alias. The mapping will be ignored."
						}));
					}
					else
					{
						InputControl inputControl = base.AddControl(inputControlMapping.Target, inputControlMapping.Name);
						inputControl.Sensitivity = Mathf.Min(this.profile.Sensitivity, inputControlMapping.Sensitivity);
						inputControl.LowerDeadZone = Mathf.Max(this.profile.LowerDeadZone, inputControlMapping.LowerDeadZone);
						inputControl.UpperDeadZone = Mathf.Min(this.profile.UpperDeadZone, inputControlMapping.UpperDeadZone);
						inputControl.Raw = inputControlMapping.Raw;
						inputControl.Passive = inputControlMapping.Passive;
					}
				}
				int buttonCount = this.profile.ButtonCount;
				for (int j = 0; j < buttonCount; j++)
				{
					InputControlMapping inputControlMapping2 = this.profile.ButtonMappings[j];
					if (Utility.TargetIsAlias(inputControlMapping2.Target))
					{
						Logger.LogError(string.Concat(new string[]
						{
							"Cannot map control \"",
							inputControlMapping2.Name,
							"\" as InputControlType.",
							inputControlMapping2.Target.ToString(),
							" in profile \"",
							deviceProfile.DeviceName,
							"\" because this target is reserved as an alias. The mapping will be ignored."
						}));
					}
					else
					{
						base.AddControl(inputControlMapping2.Target, inputControlMapping2.Name).Passive = inputControlMapping2.Passive;
					}
				}
				return;
			}
			base.Name = "Unknown Device";
			base.Meta = "\"" + joystickName + "\"";
			for (int k = 0; k < this.NumUnknownButtons; k++)
			{
				base.AddControl(InputControlType.Button0 + k, "Button " + k.ToString());
			}
			for (int l = 0; l < this.NumUnknownAnalogs; l++)
			{
				base.AddControl(InputControlType.Analog0 + l, "Analog " + l.ToString(), 0.2f, 0.9f);
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00010838 File Offset: 0x0000EA38
		public override void Update(ulong updateTick, float deltaTime)
		{
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

		// Token: 0x0600048C RID: 1164 RVA: 0x00010974 File Offset: 0x0000EB74
		private static void SetupAnalogQueries()
		{
			if (UnityInputDevice.analogQueries == null)
			{
				UnityInputDevice.analogQueries = new string[10, 20];
				for (int i = 1; i <= 10; i++)
				{
					for (int j = 0; j < 20; j++)
					{
						UnityInputDevice.analogQueries[i - 1, j] = "joystick " + i.ToString() + " analog " + j.ToString();
					}
				}
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000109DC File Offset: 0x0000EBDC
		private static void SetupButtonQueries()
		{
			if (UnityInputDevice.buttonQueries == null)
			{
				UnityInputDevice.buttonQueries = new string[10, 20];
				for (int i = 1; i <= 10; i++)
				{
					for (int j = 0; j < 20; j++)
					{
						UnityInputDevice.buttonQueries[i - 1, j] = "joystick " + i.ToString() + " button " + j.ToString();
					}
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00010A43 File Offset: 0x0000EC43
		public override bool ReadRawButtonState(int index)
		{
			return index < 20 && Input.GetKey(UnityInputDevice.buttonQueries[this.JoystickId - 1, index]);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00010A64 File Offset: 0x0000EC64
		public override float ReadRawAnalogValue(int index)
		{
			if (index < 20)
			{
				return Input.GetAxisRaw(UnityInputDevice.analogQueries[this.JoystickId - 1, index]);
			}
			return 0f;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00010A89 File Offset: 0x0000EC89
		public override bool IsSupportedOnThisPlatform
		{
			get
			{
				return this.profile == null || this.profile.IsSupportedOnThisPlatform;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		public override bool IsKnown
		{
			get
			{
				return this.profile != null;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00010AAB File Offset: 0x0000ECAB
		public override int NumUnknownButtons
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00010AAF File Offset: 0x0000ECAF
		public override int NumUnknownAnalogs
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x040003F1 RID: 1009
		private static string[,] analogQueries;

		// Token: 0x040003F2 RID: 1010
		private static string[,] buttonQueries;

		// Token: 0x040003F3 RID: 1011
		public const int MaxDevices = 10;

		// Token: 0x040003F4 RID: 1012
		public const int MaxButtons = 20;

		// Token: 0x040003F5 RID: 1013
		public const int MaxAnalogs = 20;

		// Token: 0x040003F6 RID: 1014
		[CompilerGenerated]
		private int <JoystickId>k__BackingField;

		// Token: 0x040003F7 RID: 1015
		private readonly InputDeviceProfile profile;
	}
}
