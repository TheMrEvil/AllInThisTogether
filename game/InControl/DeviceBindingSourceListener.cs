using System;

namespace InControl
{
	// Token: 0x02000011 RID: 17
	public class DeviceBindingSourceListener : BindingSourceListener
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00002797 File Offset: 0x00000997
		public void Reset()
		{
			this.detectFound = InputControlType.None;
			this.detectPhase = 0;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000027A8 File Offset: 0x000009A8
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeControllers || device.IsUnknown)
			{
				return null;
			}
			if (this.detectFound != InputControlType.None && !this.IsPressed(this.detectFound, device) && this.detectPhase == 2)
			{
				BindingSource result = new DeviceBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			InputControlType inputControlType = this.ListenForControl(listenOptions, device);
			if (inputControlType != InputControlType.None)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = inputControlType;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000282E File Offset: 0x00000A2E
		private bool IsPressed(InputControl control)
		{
			return Utility.AbsoluteIsOverThreshold(control.Value, 0.5f);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002840 File Offset: 0x00000A40
		private bool IsPressed(InputControlType control, InputDevice device)
		{
			return this.IsPressed(device.GetControl(control));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002850 File Offset: 0x00000A50
		private InputControlType ListenForControl(BindingListenOptions listenOptions, InputDevice device)
		{
			if (device.IsKnown)
			{
				int count = device.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					InputControl inputControl = device.Controls[i];
					if (inputControl != null && this.IsPressed(inputControl) && (listenOptions.IncludeNonStandardControls || inputControl.IsStandard))
					{
						InputControlType target = inputControl.Target;
						if (target != InputControlType.Command || !listenOptions.IncludeNonStandardControls)
						{
							return target;
						}
					}
				}
			}
			return InputControlType.None;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000028C0 File Offset: 0x00000AC0
		public DeviceBindingSourceListener()
		{
		}

		// Token: 0x0400003E RID: 62
		private InputControlType detectFound;

		// Token: 0x0400003F RID: 63
		private int detectPhase;
	}
}
