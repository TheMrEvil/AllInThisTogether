using System;
using System.Runtime.CompilerServices;

namespace InControl
{
	// Token: 0x0200004C RID: 76
	public class OuyaEverywhereDevice : InputDevice
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000D16B File Offset: 0x0000B36B
		// (set) Token: 0x060003AD RID: 941 RVA: 0x0000D173 File Offset: 0x0000B373
		public int DeviceIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<DeviceIndex>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DeviceIndex>k__BackingField = value;
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000D17C File Offset: 0x0000B37C
		public OuyaEverywhereDevice(int deviceIndex) : base("OUYA Controller")
		{
			this.DeviceIndex = deviceIndex;
			base.SortOrder = deviceIndex;
			base.Meta = "OUYA Everywhere Device #" + deviceIndex.ToString();
			base.AddControl(InputControlType.LeftStickLeft, "Left Stick Left");
			base.AddControl(InputControlType.LeftStickRight, "Left Stick Right");
			base.AddControl(InputControlType.LeftStickUp, "Left Stick Up");
			base.AddControl(InputControlType.LeftStickDown, "Left Stick Down");
			base.AddControl(InputControlType.RightStickLeft, "Right Stick Left");
			base.AddControl(InputControlType.RightStickRight, "Right Stick Right");
			base.AddControl(InputControlType.RightStickUp, "Right Stick Up");
			base.AddControl(InputControlType.RightStickDown, "Right Stick Down");
			base.AddControl(InputControlType.LeftTrigger, "Left Trigger");
			base.AddControl(InputControlType.RightTrigger, "Right Trigger");
			base.AddControl(InputControlType.DPadUp, "DPad Up");
			base.AddControl(InputControlType.DPadDown, "DPad Down");
			base.AddControl(InputControlType.DPadLeft, "DPad Left");
			base.AddControl(InputControlType.DPadRight, "DPad Right");
			base.AddControl(InputControlType.Action1, "O");
			base.AddControl(InputControlType.Action2, "A");
			base.AddControl(InputControlType.Action3, "Y");
			base.AddControl(InputControlType.Action4, "U");
			base.AddControl(InputControlType.LeftBumper, "Left Bumper");
			base.AddControl(InputControlType.RightBumper, "Right Bumper");
			base.AddControl(InputControlType.LeftStickButton, "Left Stick Button");
			base.AddControl(InputControlType.RightStickButton, "Right Stick Button");
			base.AddControl(InputControlType.Menu, "Menu");
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000D2F3 File Offset: 0x0000B4F3
		public void BeforeAttach()
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
		public override void Update(ulong updateTick, float deltaTime)
		{
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000D2F7 File Offset: 0x0000B4F7
		public bool IsConnected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000348 RID: 840
		private const float LowerDeadZone = 0.2f;

		// Token: 0x04000349 RID: 841
		private const float UpperDeadZone = 0.9f;

		// Token: 0x0400034A RID: 842
		[CompilerGenerated]
		private int <DeviceIndex>k__BackingField;
	}
}
