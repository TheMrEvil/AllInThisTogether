using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x02000078 RID: 120
	[Preserve]
	[UnityInputDeviceProfile]
	public class AmazonFireTVRemoteUnityProfile : InputDeviceProfile
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x00015154 File Offset: 0x00013354
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Amazon Fire TV Remote";
			base.DeviceNotes = "Amazon Fire TV Remote on Amazon Fire TV";
			base.DeviceClass = InputDeviceClass.Remote;
			base.DeviceStyle = InputDeviceStyle.AmazonFireTV;
			base.IncludePlatforms = new string[]
			{
				"Amazon AFT"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = ""
				},
				new InputDeviceMatcher
				{
					NameLiteral = "Amazon Fire TV Remote"
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
				},
				new InputControlMapping
				{
					Name = "Menu",
					Target = InputControlType.Menu,
					Source = InputDeviceProfile.MenuKey
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

		// Token: 0x060005BC RID: 1468 RVA: 0x00015293 File Offset: 0x00013493
		public AmazonFireTVRemoteUnityProfile()
		{
		}
	}
}
