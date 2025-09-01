using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001D9 RID: 473
	[Preserve]
	[NativeInputDeviceProfile]
	public class SDLLogitechF310ModeXNativeProfile : SDLControllerNativeProfile
	{
		// Token: 0x060008A2 RID: 2210 RVA: 0x00045A3C File Offset: 0x00043C3C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech F310 Controller";
			base.DeviceStyle = InputDeviceStyle.Xbox360;
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1133,
					ProductID = 49693
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

		// Token: 0x060008A3 RID: 2211 RVA: 0x00045BC3 File Offset: 0x00043DC3
		public SDLLogitechF310ModeXNativeProfile()
		{
		}
	}
}
