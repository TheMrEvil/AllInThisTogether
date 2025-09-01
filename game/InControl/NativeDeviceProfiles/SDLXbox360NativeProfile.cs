using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001E0 RID: 480
	[Preserve]
	[NativeInputDeviceProfile]
	public class SDLXbox360NativeProfile : SDLControllerNativeProfile
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x0004674C File Offset: 0x0004494C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Xbox 360 Controller";
			base.DeviceStyle = InputDeviceStyle.Xbox360;
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1118,
					ProductID = 654
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1118,
					ProductID = 673
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				SDLControllerNativeProfile.Action1Mapping("A"),
				SDLControllerNativeProfile.Action2Mapping("B"),
				SDLControllerNativeProfile.Action3Mapping("X"),
				SDLControllerNativeProfile.Action4Mapping("Y"),
				SDLControllerNativeProfile.LeftCommandMapping("Back", InputControlType.Back),
				SDLControllerNativeProfile.RightCommandMapping("Start", InputControlType.Start),
				SDLControllerNativeProfile.SystemMapping("Guide", InputControlType.Guide),
				SDLControllerNativeProfile.LeftStickButtonMapping(),
				SDLControllerNativeProfile.RightStickButtonMapping(),
				SDLControllerNativeProfile.LeftBumperMapping("Left Bumper"),
				SDLControllerNativeProfile.RightBumperMapping("Right Bumper"),
				SDLControllerNativeProfile.DPadUpMapping(),
				SDLControllerNativeProfile.DPadDownMapping(),
				SDLControllerNativeProfile.DPadLeftMapping(),
				SDLControllerNativeProfile.DPadRightMapping()
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				SDLControllerNativeProfile.LeftStickLeftMapping(),
				SDLControllerNativeProfile.LeftStickRightMapping(),
				SDLControllerNativeProfile.LeftStickUpMapping(),
				SDLControllerNativeProfile.LeftStickDownMapping(),
				SDLControllerNativeProfile.RightStickLeftMapping(),
				SDLControllerNativeProfile.RightStickRightMapping(),
				SDLControllerNativeProfile.RightStickUpMapping(),
				SDLControllerNativeProfile.RightStickDownMapping(),
				SDLControllerNativeProfile.LeftTriggerMapping("Left Trigger"),
				SDLControllerNativeProfile.RightTriggerMapping("Right Trigger")
			};
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00046923 File Offset: 0x00044B23
		public SDLXbox360NativeProfile()
		{
		}
	}
}
