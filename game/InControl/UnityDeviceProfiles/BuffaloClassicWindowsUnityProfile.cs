using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x02000100 RID: 256
	[Preserve]
	[UnityInputDeviceProfile]
	public class BuffaloClassicWindowsUnityProfile : InputDeviceProfile
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Buffalo Class Gamepad";
			base.DeviceNotes = "Buffalo Class Gamepad on Windows";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[]
			{
				"Windows"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = "USB,2-axis 8-button gamepad  "
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Name = "A",
					Target = InputControlType.Action2,
					Source = InputDeviceProfile.Button(0)
				},
				new InputControlMapping
				{
					Name = "B",
					Target = InputControlType.Action1,
					Source = InputDeviceProfile.Button(1)
				},
				new InputControlMapping
				{
					Name = "X",
					Target = InputControlType.Action4,
					Source = InputDeviceProfile.Button(2)
				},
				new InputControlMapping
				{
					Name = "Y",
					Target = InputControlType.Action3,
					Source = InputDeviceProfile.Button(3)
				},
				new InputControlMapping
				{
					Name = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = InputDeviceProfile.Button(4)
				},
				new InputControlMapping
				{
					Name = "Right Bumper",
					Target = InputControlType.RightBumper,
					Source = InputDeviceProfile.Button(5)
				},
				new InputControlMapping
				{
					Name = "Select",
					Target = InputControlType.Select,
					Source = InputDeviceProfile.Button(6)
				},
				new InputControlMapping
				{
					Name = "Start",
					Target = InputControlType.Start,
					Source = InputDeviceProfile.Button(7)
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				InputDeviceProfile.DPadLeftMapping(0),
				InputDeviceProfile.DPadRightMapping(0),
				InputDeviceProfile.DPadUpMapping(1),
				InputDeviceProfile.DPadDownMapping(1)
			};
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		public BuffaloClassicWindowsUnityProfile()
		{
		}
	}
}
