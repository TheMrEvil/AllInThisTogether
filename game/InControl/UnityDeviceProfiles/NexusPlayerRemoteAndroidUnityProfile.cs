using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x020000AA RID: 170
	[Preserve]
	[UnityInputDeviceProfile]
	public class NexusPlayerRemoteAndroidUnityProfile : InputDeviceProfile
	{
		// Token: 0x0600061F RID: 1567 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Nexus Player Remote";
			base.DeviceNotes = "Nexus Player Remote";
			base.DeviceClass = InputDeviceClass.Remote;
			base.IncludePlatforms = new string[]
			{
				"Android"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = "Google Nexus Remote"
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

		// Token: 0x06000620 RID: 1568 RVA: 0x0001D8B5 File Offset: 0x0001BAB5
		public NexusPlayerRemoteAndroidUnityProfile()
		{
		}
	}
}
