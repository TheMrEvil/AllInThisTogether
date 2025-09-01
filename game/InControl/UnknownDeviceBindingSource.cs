using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace InControl
{
	// Token: 0x0200001D RID: 29
	public class UnknownDeviceBindingSource : BindingSource
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00004CFF File Offset: 0x00002EFF
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00004D07 File Offset: 0x00002F07
		public UnknownDeviceControl Control
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

		// Token: 0x06000118 RID: 280 RVA: 0x00004D10 File Offset: 0x00002F10
		internal UnknownDeviceBindingSource()
		{
			this.Control = UnknownDeviceControl.None;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004D23 File Offset: 0x00002F23
		public UnknownDeviceBindingSource(UnknownDeviceControl control)
		{
			this.Control = control;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004D34 File Offset: 0x00002F34
		public override float GetValue(InputDevice device)
		{
			return this.Control.GetValue(device);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004D50 File Offset: 0x00002F50
		public override bool GetState(InputDevice device)
		{
			return device != null && Utility.IsNotZero(this.GetValue(device));
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004D64 File Offset: 0x00002F64
		public override string Name
		{
			get
			{
				if (base.BoundTo == null)
				{
					return "";
				}
				string text = "";
				if (this.Control.SourceRange == InputRangeType.ZeroToMinusOne)
				{
					text = "Negative ";
				}
				else if (this.Control.SourceRange == InputRangeType.ZeroToOne)
				{
					text = "Positive ";
				}
				InputDevice device = base.BoundTo.Device;
				if (device == InputDevice.Null)
				{
					string str = text;
					UnknownDeviceControl control = this.Control;
					return str + control.Control.ToString();
				}
				InputControl control2 = device.GetControl(this.Control.Control);
				if (control2 == InputControl.Null)
				{
					string str2 = text;
					UnknownDeviceControl control = this.Control;
					return str2 + control.Control.ToString();
				}
				return text + control2.Handle;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004E2C File Offset: 0x0000302C
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
					return "Unknown Controller";
				}
				return device.Name;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004E67 File Offset: 0x00003067
		public override InputDeviceClass DeviceClass
		{
			get
			{
				return InputDeviceClass.Controller;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004E6A File Offset: 0x0000306A
		public override InputDeviceStyle DeviceStyle
		{
			get
			{
				return InputDeviceStyle.Unknown;
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004E70 File Offset: 0x00003070
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			UnknownDeviceBindingSource unknownDeviceBindingSource = other as UnknownDeviceBindingSource;
			return unknownDeviceBindingSource != null && this.Control == unknownDeviceBindingSource.Control;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004EAC File Offset: 0x000030AC
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			UnknownDeviceBindingSource unknownDeviceBindingSource = other as UnknownDeviceBindingSource;
			return unknownDeviceBindingSource != null && this.Control == unknownDeviceBindingSource.Control;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004EE4 File Offset: 0x000030E4
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004F05 File Offset: 0x00003105
		public override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.UnknownDeviceBindingSource;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00004F08 File Offset: 0x00003108
		internal override bool IsValid
		{
			get
			{
				if (base.BoundTo == null)
				{
					Logger.LogError("Cannot query property 'IsValid' for unbound BindingSource.");
					return false;
				}
				InputDevice device = base.BoundTo.Device;
				return device == InputDevice.Null || device.HasControl(this.Control.Control);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004F50 File Offset: 0x00003150
		public override void Load(BinaryReader reader, ushort dataFormatVersion)
		{
			UnknownDeviceControl control = default(UnknownDeviceControl);
			control.Load(reader);
			this.Control = control;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004F74 File Offset: 0x00003174
		public override void Save(BinaryWriter writer)
		{
			this.Control.Save(writer);
		}

		// Token: 0x0400011F RID: 287
		[CompilerGenerated]
		private UnknownDeviceControl <Control>k__BackingField;
	}
}
