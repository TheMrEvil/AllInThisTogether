using System;

namespace InControl
{
	// Token: 0x02000057 RID: 87
	public class TouchInputDevice : InputDevice
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		public TouchInputDevice() : base("Touch Input Device", true)
		{
			base.DeviceClass = InputDeviceClass.TouchScreen;
		}
	}
}
