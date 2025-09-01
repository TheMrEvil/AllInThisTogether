using System;

namespace InControl.UnityDeviceProfiles
{
	// Token: 0x0200007B RID: 123
	[Preserve]
	[UnityInputDeviceProfile]
	public class BuffaloClassicAmazonUnityProfile : InputDeviceProfile
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x00015830 File Offset: 0x00013A30
		public override void Define()
		{
			base.Define();
			base.DeviceName = "Buffalo Class Gamepad";
			base.DeviceNotes = "Buffalo Class Gamepad on Amazon Fire TV";
			base.DeviceClass = InputDeviceClass.Controller;
			base.IncludePlatforms = new string[]
			{
				"Amazon AFT"
			};
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					NameLiteral = "USB,2-axis 8-button gamepad  "
				}
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Name = "A",
					Target = InputControlType.Action2,
					Source = InputDeviceProfile.Button(15)
				},
				new InputControlMapping
				{
					Name = "B",
					Target = InputControlType.Action1,
					Source = InputDeviceProfile.Button(16)
				},
				new InputControlMapping
				{
					Name = "X",
					Target = InputControlType.Action4,
					Source = InputDeviceProfile.Button(17)
				},
				new InputControlMapping
				{
					Name = "Y",
					Target = InputControlType.Action3,
					Source = InputDeviceProfile.Button(18)
				},
				new InputControlMapping
				{
					Name = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = InputDeviceProfile.Button(19)
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				InputDeviceProfile.DPadLeftMapping(0),
				InputDeviceProfile.DPadRightMapping(0),
				InputDeviceProfile.DPadUpMapping(1),
				InputDeviceProfile.DPadDownMapping(1)
			};
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000159A0 File Offset: 0x00013BA0
		public BuffaloClassicAmazonUnityProfile()
		{
		}
	}
}
