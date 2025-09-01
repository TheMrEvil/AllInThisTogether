using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001DE RID: 478
	[Preserve]
	[NativeInputDeviceProfile]
	public class SDLPlayStation4NativeProfile : SDLControllerNativeProfile
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x000462FC File Offset: 0x000444FC
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PlayStation 4 Controller";
			base.DeviceStyle = InputDeviceStyle.PlayStation4;
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1356,
					ProductID = 1476
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1356,
					ProductID = 2976
				},
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1356,
					ProductID = 2508
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				SDLControllerNativeProfile.Action1Mapping("Cross"),
				SDLControllerNativeProfile.Action2Mapping("Circle"),
				SDLControllerNativeProfile.Action3Mapping("Square"),
				SDLControllerNativeProfile.Action4Mapping("Triangle"),
				SDLControllerNativeProfile.LeftCommandMapping("Share", InputControlType.Share),
				SDLControllerNativeProfile.RightCommandMapping("Options", InputControlType.Options),
				SDLControllerNativeProfile.SystemMapping("System", InputControlType.System),
				SDLControllerNativeProfile.LeftStickButtonMapping(),
				SDLControllerNativeProfile.RightStickButtonMapping(),
				SDLControllerNativeProfile.LeftBumperMapping("L1"),
				SDLControllerNativeProfile.RightBumperMapping("R1"),
				SDLControllerNativeProfile.DPadUpMapping(),
				SDLControllerNativeProfile.DPadDownMapping(),
				SDLControllerNativeProfile.DPadLeftMapping(),
				SDLControllerNativeProfile.DPadRightMapping(),
				SDLControllerNativeProfile.TouchPadButtonMapping()
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
				SDLControllerNativeProfile.LeftTriggerMapping("L2"),
				SDLControllerNativeProfile.RightTriggerMapping("R2"),
				SDLControllerNativeProfile.AccelerometerXMapping(),
				SDLControllerNativeProfile.AccelerometerYMapping(),
				SDLControllerNativeProfile.AccelerometerZMapping(),
				SDLControllerNativeProfile.GyroscopeXMapping(),
				SDLControllerNativeProfile.GyroscopeYMapping(),
				SDLControllerNativeProfile.GyroscopeZMapping()
			};
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00046552 File Offset: 0x00044752
		public SDLPlayStation4NativeProfile()
		{
		}

		// Token: 0x0200021D RID: 541
		private enum ProductId : ushort
		{
			// Token: 0x040004BB RID: 1211
			SONY_DS4 = 1476,
			// Token: 0x040004BC RID: 1212
			SONY_DS4_DONGLE = 2976,
			// Token: 0x040004BD RID: 1213
			SONY_DS4_SLIM = 2508
		}
	}
}
