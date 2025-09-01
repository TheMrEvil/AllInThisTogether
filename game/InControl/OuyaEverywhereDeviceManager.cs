using System;

namespace InControl
{
	// Token: 0x0200004D RID: 77
	public class OuyaEverywhereDeviceManager : InputDeviceManager
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		public OuyaEverywhereDeviceManager()
		{
			for (int i = 0; i < 4; i++)
			{
				this.devices.Add(new OuyaEverywhereDevice(i));
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000D338 File Offset: 0x0000B538
		public override void Update(ulong updateTick, float deltaTime)
		{
			for (int i = 0; i < 4; i++)
			{
				OuyaEverywhereDevice ouyaEverywhereDevice = this.devices[i] as OuyaEverywhereDevice;
				if (ouyaEverywhereDevice.IsConnected != this.deviceConnected[i])
				{
					if (ouyaEverywhereDevice.IsConnected)
					{
						ouyaEverywhereDevice.BeforeAttach();
						InputManager.AttachDevice(ouyaEverywhereDevice);
					}
					else
					{
						InputManager.DetachDevice(ouyaEverywhereDevice);
					}
					this.deviceConnected[i] = ouyaEverywhereDevice.IsConnected;
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000D39D File Offset: 0x0000B59D
		public static void Enable()
		{
		}

		// Token: 0x0400034B RID: 843
		private bool[] deviceConnected = new bool[4];
	}
}
