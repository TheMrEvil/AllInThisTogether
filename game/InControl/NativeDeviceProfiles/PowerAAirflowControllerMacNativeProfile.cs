using System;

namespace InControl.NativeDeviceProfiles
{
	// Token: 0x02000194 RID: 404
	[Preserve]
	[NativeInputDeviceProfile]
	public class PowerAAirflowControllerMacNativeProfile : Xbox360DriverMacNativeProfile
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x0003CB00 File Offset: 0x0003AD00
		public override void Define()
		{
			base.Define();
			base.DeviceName = "PowerA Airflow Controller";
			base.DeviceNotes = "PowerA Airflow Controller on Mac";
			base.Matchers = new InputDeviceMatcher[]
			{
				new InputDeviceMatcher
				{
					DriverType = InputDeviceDriverType.HID,
					VendorID = 5604,
					ProductID = 16138
				}
			};
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0003CB74 File Offset: 0x0003AD74
		public PowerAAirflowControllerMacNativeProfile()
		{
		}
	}
}
