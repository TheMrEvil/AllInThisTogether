using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x020001DD RID: 477
	[Preserve]
	[NativeInputDeviceProfile]
	public class SDLPlayStation3NativeProfile : SDLControllerNativeProfile
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x000460E8 File Offset: 0x000442E8
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PlayStation 3 Controller";
			base.DeviceStyle = InputDeviceStyle.PlayStation3;
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.SDLController,
					VendorID = 1356,
					ProductID = 616
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				SDLControllerNativeProfile.Action1Mapping("Cross"),
				SDLControllerNativeProfile.Action2Mapping("Circle"),
				SDLControllerNativeProfile.Action3Mapping("Square"),
				SDLControllerNativeProfile.Action4Mapping("Triangle"),
				SDLControllerNativeProfile.LeftCommandMapping("Start", InputControlType.Start),
				SDLControllerNativeProfile.RightCommandMapping("Select", InputControlType.Select),
				SDLControllerNativeProfile.SystemMapping("System", InputControlType.System),
				SDLControllerNativeProfile.LeftStickButtonMapping(),
				SDLControllerNativeProfile.RightStickButtonMapping(),
				SDLControllerNativeProfile.LeftBumperMapping("L1"),
				SDLControllerNativeProfile.RightBumperMapping("R1"),
				SDLControllerNativeProfile.DPadUpMapping(),
				SDLControllerNativeProfile.DPadDownMapping(),
				SDLControllerNativeProfile.DPadLeftMapping(),
				SDLControllerNativeProfile.DPadRightMapping(),
				SDLControllerNativeProfile.Misc1Mapping("Mute", InputControlType.Mute),
				SDLControllerNativeProfile.Paddle1Mapping(),
				SDLControllerNativeProfile.Paddle2Mapping(),
				SDLControllerNativeProfile.Paddle3Mapping(),
				SDLControllerNativeProfile.Paddle4Mapping(),
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

		// Token: 0x060008AB RID: 2219 RVA: 0x000462F2 File Offset: 0x000444F2
		public SDLPlayStation3NativeProfile()
		{
		}
	}
}
