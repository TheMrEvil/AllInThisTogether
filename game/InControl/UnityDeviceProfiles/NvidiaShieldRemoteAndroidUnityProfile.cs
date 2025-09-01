using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x020000AF RID: 175
	[Preserve]
	[UnityInputDeviceProfile]
	public class NvidiaShieldRemoteAndroidUnityProfile : InputDeviceProfile
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "NVIDIA Shield Remote";
			base.DeviceNotes = "NVIDIA Shield Remote on Android";
			base.DeviceClass = InputDeviceClass.Remote;
			base.DeviceStyle = InputDeviceStyle.NVIDIAShield;
			base.IncludePlatforms = new string[]
			{
				"Android"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = "SHIELD Remote"
				},
				new InputDeviceMatcher
				{
					NamePattern = "SHIELD Remote"
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Name = "A",
					Target = InputControlType.Action1,
					Source = InputDeviceProfile.Button(0)
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				InputDeviceProfile.DPadLeftMapping(4),
				InputDeviceProfile.DPadRightMapping(4),
				InputDeviceProfile.DPadUpMapping(5),
				InputDeviceProfile.DPadDownMapping(5)
			};
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001E4D3 File Offset: 0x0001C6D3
		public NvidiaShieldRemoteAndroidUnityProfile()
		{
		}
	}
}
