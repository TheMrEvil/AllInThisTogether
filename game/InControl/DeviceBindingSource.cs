using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace InControl
{
	// Token: 0x02000010 RID: 16
	public class DeviceBindingSource : BindingSource
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002576 File Offset: 0x00000776
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000257E File Offset: 0x0000077E
		public InputControlType Control
		{
			[CompilerGenerated]
			get
			{
				return this.<Control>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Control>k__BackingField = value;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002587 File Offset: 0x00000787
		internal DeviceBindingSource()
		{
			this.Control = InputControlType.None;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002596 File Offset: 0x00000796
		public DeviceBindingSource(InputControlType control)
		{
			this.Control = control;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000025A5 File Offset: 0x000007A5
		public override float GetValue(InputDevice inputDevice)
		{
			if (inputDevice == null)
			{
				return 0f;
			}
			return inputDevice.GetControl(this.Control).Value;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000025C1 File Offset: 0x000007C1
		public override bool GetState(InputDevice inputDevice)
		{
			return inputDevice != null && inputDevice.GetControl(this.Control).State;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000025DC File Offset: 0x000007DC
		public override string Name
		{
			get
			{
				if (base.BoundTo == null)
				{
					return "";
				}
				InputDevice device = base.BoundTo.Device;
				if (device.GetControl(this.Control) == InputControl.Null)
				{
					return this.Control.ToString();
				}
				return device.GetControl(this.Control).Handle;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000263C File Offset: 0x0000083C
		public override string DeviceName
		{
			get
			{
				if (base.BoundTo == null)
				{
					return "";
				}
				InputDevice device = base.BoundTo.Device;
				if (device == InputDevice.Null)
				{
					return "Controller";
				}
				return device.Name;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002677 File Offset: 0x00000877
		public override InputDeviceClass DeviceClass
		{
			get
			{
				if (base.BoundTo != null)
				{
					return base.BoundTo.Device.DeviceClass;
				}
				return InputDeviceClass.Unknown;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002693 File Offset: 0x00000893
		public override InputDeviceStyle DeviceStyle
		{
			get
			{
				if (base.BoundTo != null)
				{
					return base.BoundTo.Device.DeviceStyle;
				}
				return InputDeviceStyle.Unknown;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000026B0 File Offset: 0x000008B0
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			DeviceBindingSource deviceBindingSource = other as DeviceBindingSource;
			return deviceBindingSource != null && this.Control == deviceBindingSource.Control;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000026E8 File Offset: 0x000008E8
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			DeviceBindingSource deviceBindingSource = other as DeviceBindingSource;
			return deviceBindingSource != null && this.Control == deviceBindingSource.Control;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000271C File Offset: 0x0000091C
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000273D File Offset: 0x0000093D
		public override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.DeviceBindingSource;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002740 File Offset: 0x00000940
		public override void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000274E File Offset: 0x0000094E
		public override void Load(BinaryReader reader, ushort dataFormatVersion)
		{
			this.Control = (InputControlType)reader.ReadInt32();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000275C File Offset: 0x0000095C
		internal override bool IsValid
		{
			get
			{
				if (base.BoundTo == null)
				{
					Logger.LogError("Cannot query property 'IsValid' for unbound BindingSource.");
					return false;
				}
				return base.BoundTo.Device.HasControl(this.Control) || Utility.TargetIsStandard(this.Control);
			}
		}

		// Token: 0x0400003D RID: 61
		[CompilerGenerated]
		private InputControlType <Control>k__BackingField;
	}
}
