using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x02000083 RID: 131
	[Preserve]
	[UnityInputDeviceProfile]
	public class AndroidTVMiBoxRemoteUnityProfile : InputDeviceProfile
	{
		// Token: 0x060005D1 RID: 1489 RVA: 0x00016DE0 File Offset: 0x00014FE0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Xiaomi Remote";
			base.DeviceNotes = "Xiaomi Remote on Android TV";
			base.DeviceClass = InputDeviceClass.Remote;
			base.IncludePlatforms = new string[]
			{
				"Android"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = "Xiaomi Remote"
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Name = "A",
					Target = InputControlType.Action1,
					Source = InputDeviceProfile.Button(0)
				},
				new InputControlMapping
				{
					Name = "Back",
					Target = InputControlType.Back,
					Source = InputDeviceProfile.EscapeKey
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

		// Token: 0x060005D2 RID: 1490 RVA: 0x00016ED5 File Offset: 0x000150D5
		public AndroidTVMiBoxRemoteUnityProfile()
		{
		}
	}
}
