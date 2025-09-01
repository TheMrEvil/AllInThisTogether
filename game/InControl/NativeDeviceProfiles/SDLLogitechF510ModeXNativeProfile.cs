using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001DA RID: 474
	[Preserve]
	[NativeInputDeviceProfile]
	public class SDLLogitechF510ModeXNativeProfile : SDLControllerNativeProfile
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x00045BCC File Offset: 0x00043DCC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Logitech F510 Controller";
			base.DeviceStyle = InputDeviceStyle.Xbox360;
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1133,
					ProductID = 49694
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

		// Token: 0x060008A5 RID: 2213 RVA: 0x00045D53 File Offset: 0x00043F53
		public SDLLogitechF510ModeXNativeProfile()
		{
		}
	}
}
