using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x02000084 RID: 132
	[Preserve]
	[UnityInputDeviceProfile]
	public class AndroidTVRemoteUnityProfile : InputDeviceProfile
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x00016EE0 File Offset: 0x000150E0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Android TV Remote";
			base.DeviceNotes = "Android TV Remote on Android TV";
			base.DeviceClass = InputDeviceClass.Remote;
			base.IncludePlatforms = new string[]
			{
				"Android"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = ""
				},
				new InputDeviceMatcher
				{
					NameLiteral = "touch-input"
				},
				new InputDeviceMatcher
				{
					NameLiteral = "navigation-input"
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

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001700D File Offset: 0x0001520D
		public AndroidTVRemoteUnityProfile()
		{
		}
	}
}
