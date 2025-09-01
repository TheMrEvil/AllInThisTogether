using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace InControl
{
	// Token: 0x02000013 RID: 19
	public class KeyBindingSource : BindingSource
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000028C8 File Offset: 0x00000AC8
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000028D0 File Offset: 0x00000AD0
		public KeyCombo Control
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

		// Token: 0x0600005A RID: 90 RVA: 0x000028D9 File Offset: 0x00000AD9
		internal KeyBindingSource()
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000028E1 File Offset: 0x00000AE1
		public KeyBindingSource(KeyCombo keyCombo)
		{
			this.Control = keyCombo;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000028F0 File Offset: 0x00000AF0
		public KeyBindingSource(params Key[] keys)
		{
			this.Control = new KeyCombo(keys);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002904 File Offset: 0x00000B04
		public override float GetValue(InputDevice inputDevice)
		{
			if (!this.GetState(inputDevice))
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000291C File Offset: 0x00000B1C
		public override bool GetState(InputDevice inputDevice)
		{
			return this.Control.IsPressed;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002938 File Offset: 0x00000B38
		public override string Name
		{
			get
			{
				return this.Control.ToString();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002959 File Offset: 0x00000B59
		public override string DeviceName
		{
			get
			{
				return "Keyboard";
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002960 File Offset: 0x00000B60
		public override InputDeviceClass DeviceClass
		{
			get
			{
				return InputDeviceClass.Keyboard;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002963 File Offset: 0x00000B63
		public override InputDeviceStyle DeviceStyle
		{
			get
			{
				return InputDeviceStyle.Unknown;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002968 File Offset: 0x00000B68
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			KeyBindingSource keyBindingSource = other as KeyBindingSource;
			return keyBindingSource != null && this.Control == keyBindingSource.Control;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000029A4 File Offset: 0x00000BA4
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			KeyBindingSource keyBindingSource = other as KeyBindingSource;
			return keyBindingSource != null && this.Control == keyBindingSource.Control;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000029DC File Offset: 0x00000BDC
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000029FD File Offset: 0x00000BFD
		public override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.KeyBindingSource;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002A00 File Offset: 0x00000C00
		public override void Load(BinaryReader reader, ushort dataFormatVersion)
		{
			KeyCombo control = default(KeyCombo);
			control.Load(reader, dataFormatVersion);
			this.Control = control;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002A28 File Offset: 0x00000C28
		public override void Save(BinaryWriter writer)
		{
			this.Control.Save(writer);
		}

		// Token: 0x040000C5 RID: 197
		[CompilerGenerated]
		private KeyCombo <Control>k__BackingField;
	}
}
