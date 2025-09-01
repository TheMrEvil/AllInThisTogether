using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x02000106 RID: 262
	[Preserve]
	[UnityInputDeviceProfile]
	public class GameCubeWindowsUnityProfile : InputDeviceProfile
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x0003126C File Offset: 0x0002F46C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "GameCube Controller";
			base.DeviceNotes = "GameCube Controller on Windows";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[]
			{
				"Windows"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = "USB GamePad"
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Name = "A",
					Target = InputControlType.Action1,
					Source = InputDeviceProfile.Button(1)
				},
				new InputControlMapping
				{
					Name = "X",
					Target = InputControlType.Action2,
					Source = InputDeviceProfile.Button(2)
				},
				new InputControlMapping
				{
					Name = "B",
					Target = InputControlType.Action3,
					Source = InputDeviceProfile.Button(0)
				},
				new InputControlMapping
				{
					Name = "Y",
					Target = InputControlType.Action4,
					Source = InputDeviceProfile.Button(3)
				},
				new InputControlMapping
				{
					Name = "Start",
					Target = InputControlType.Start,
					Source = InputDeviceProfile.Button(9)
				},
				new InputControlMapping
				{
					Name = "Z",
					Target = InputControlType.RightBumper,
					Source = InputDeviceProfile.Button(7)
				},
				new InputControlMapping
				{
					Name = "L",
					Target = InputControlType.LeftTrigger,
					Source = InputDeviceProfile.Button(4)
				},
				new InputControlMapping
				{
					Name = "R",
					Target = InputControlType.RightTrigger,
					Source = InputDeviceProfile.Button(5)
				},
				new InputControlMapping
				{
					Name = "DPad Up",
					Target = InputControlType.DPadUp,
					Source = InputDeviceProfile.Button(12)
				},
				new InputControlMapping
				{
					Name = "DPad Down",
					Target = InputControlType.DPadDown,
					Source = InputDeviceProfile.Button(14)
				},
				new InputControlMapping
				{
					Name = "DPad Left",
					Target = InputControlType.DPadLeft,
					Source = InputDeviceProfile.Button(15)
				},
				new InputControlMapping
				{
					Name = "DPad Right",
					Target = InputControlType.DPadRight,
					Source = InputDeviceProfile.Button(13)
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				InputDeviceProfile.LeftStickLeftMapping(0),
				InputDeviceProfile.LeftStickRightMapping(0),
				InputDeviceProfile.LeftStickUpMapping(1),
				InputDeviceProfile.LeftStickDownMapping(1),
				InputDeviceProfile.RightStickLeftMapping(5),
				InputDeviceProfile.RightStickRightMapping(5),
				InputDeviceProfile.RightStickUpMapping(2),
				InputDeviceProfile.RightStickDownMapping(2)
			};
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00031515 File Offset: 0x0002F715
		public GameCubeWindowsUnityProfile()
		{
		}
	}
}
