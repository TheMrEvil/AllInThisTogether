using System;

namespace InControl
{
	// Token: 0x0200001E RID: 30
	public class UnknownDeviceBindingSourceListener : BindingSourceListener
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00004F90 File Offset: 0x00003190
		public void Reset()
		{
			this.detectFound = UnknownDeviceControl.None;
			this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForInitialRelease;
			this.TakeSnapshotOnUnknownDevices();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004FAC File Offset: 0x000031AC
		private void TakeSnapshotOnUnknownDevices()
		{
			int count = InputManager.Devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.Devices[i];
				if (inputDevice.IsUnknown)
				{
					inputDevice.TakeSnapshot();
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004FEC File Offset: 0x000031EC
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeUnknownControllers || device.IsKnown)
			{
				return null;
			}
			if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlRelease && this.detectFound && !this.IsPressed(this.detectFound, device))
			{
				BindingSource result = new UnknownDeviceBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			UnknownDeviceControl control = this.ListenForControl(listenOptions, device);
			if (control)
			{
				if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlPress)
				{
					this.detectFound = control;
					this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlRelease;
				}
			}
			else if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForInitialRelease)
			{
				this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlPress;
			}
			return null;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000507C File Offset: 0x0000327C
		private bool IsPressed(UnknownDeviceControl control, InputDevice device)
		{
			return Utility.AbsoluteIsOverThreshold(control.GetValue(device), 0.5f);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005090 File Offset: 0x00003290
		private UnknownDeviceControl ListenForControl(BindingListenOptions listenOptions, InputDevice device)
		{
			if (device.IsUnknown)
			{
				UnknownDeviceControl firstPressedButton = device.GetFirstPressedButton();
				if (firstPressedButton)
				{
					return firstPressedButton;
				}
				UnknownDeviceControl firstPressedAnalog = device.GetFirstPressedAnalog();
				if (firstPressedAnalog)
				{
					return firstPressedAnalog;
				}
			}
			return UnknownDeviceControl.None;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000050CC File Offset: 0x000032CC
		public UnknownDeviceBindingSourceListener()
		{
		}

		// Token: 0x04000120 RID: 288
		private UnknownDeviceControl detectFound;

		// Token: 0x04000121 RID: 289
		private UnknownDeviceBindingSourceListener.DetectPhase detectPhase;

		// Token: 0x0200020E RID: 526
		private enum DetectPhase
		{
			// Token: 0x0400044A RID: 1098
			WaitForInitialRelease,
			// Token: 0x0400044B RID: 1099
			WaitForControlPress,
			// Token: 0x0400044C RID: 1100
			WaitForControlRelease
		}
	}
}
