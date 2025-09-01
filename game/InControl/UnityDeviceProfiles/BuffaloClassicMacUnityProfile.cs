using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x020000CB RID: 203
	[Preserve]
	[UnityInputDeviceProfile]
	public class BuffaloClassicMacUnityProfile : InputDeviceProfile
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x000235D8 File Offset: 0x000217D8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "iBuffalo Classic Controller";
			base.DeviceNotes = "iBuffalo Classic Controller on Mac";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[]
			{
				"OS X"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = " USB,2-axis 8-button gamepad"
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

		// Token: 0x06000662 RID: 1634 RVA: 0x000237B8 File Offset: 0x000219B8
		public BuffaloClassicMacUnityProfile()
		{
		}
	}
}
