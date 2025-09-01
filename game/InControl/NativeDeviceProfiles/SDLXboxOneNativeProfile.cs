using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001E1 RID: 481
	[Preserve]
	[NativeInputDeviceProfile]
	public class SDLXboxOneNativeProfile : SDLControllerNativeProfile
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x0004692C File Offset: 0x00044B2C
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Xbox One Controller";
			base.DeviceStyle = InputDeviceStyle.XboxOne;
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1118,
					ProductID = 746
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1118,
					ProductID = 736
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1118,
					ProductID = 765
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1118,
					ProductID = 767
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1118,
					ProductID = 766
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				SDLControllerNativeProfile.Action1Mapping("A"),
				SDLControllerNativeProfile.Action2Mapping("B"),
				SDLControllerNativeProfile.Action3Mapping("X"),
				SDLControllerNativeProfile.Action4Mapping("Y"),
				SDLControllerNativeProfile.LeftCommandMapping("View", InputControlType.View),
				SDLControllerNativeProfile.RightCommandMapping("Menu", InputControlType.Menu),
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

		// Token: 0x060008B3 RID: 2227 RVA: 0x00046BC3 File Offset: 0x00044DC3
		public SDLXboxOneNativeProfile()
		{
		}

		// Token: 0x0200021F RID: 543
		private enum ProductId : ushort
		{
			// Token: 0x040004C1 RID: 1217
			XBOX_ONE_S = 746,
			// Token: 0x040004C2 RID: 1218
			XBOX_ONE_S_REV1_BLUETOOTH = 736,
			// Token: 0x040004C3 RID: 1219
			XBOX_ONE_S_REV2_BLUETOOTH = 765,
			// Token: 0x040004C4 RID: 1220
			XBOX_ONE_RAW_INPUT_CONTROLLER = 767,
			// Token: 0x040004C5 RID: 1221
			XBOX_ONE_XINPUT_CONTROLLER = 766
		}
	}
}
