using System;
using System.IO;

namespace InControl
{
	// Token: 0x0200001F RID: 31
	public struct UnknownDeviceControl : IEquatable<UnknownDeviceControl>
	{
		// Token: 0x0600012D RID: 301 RVA: 0x000050D4 File Offset: 0x000032D4
		public UnknownDeviceControl(InputControlType control, InputRangeType sourceRange)
		{
			this.Control = control;
			this.SourceRange = sourceRange;
			this.IsButton = Utility.TargetIsButton(control);
			this.IsAnalog = !this.IsButton;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000050FF File Offset: 0x000032FF
		internal float GetValue(InputDevice device)
		{
			if (device == null)
			{
				return 0f;
			}
			return InputRange.Remap(device.GetControl(this.Control).Value, this.SourceRange, InputRangeType.ZeroToOne);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005127 File Offset: 0x00003327
		public int Index
		{
			get
			{
				return this.Control - (this.IsButton ? InputControlType.Button0 : InputControlType.Analog0);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005144 File Offset: 0x00003344
		public static bool operator ==(UnknownDeviceControl a, UnknownDeviceControl b)
		{
			if (a == null)
			{
				return b == null;
			}
			return a.Equals(b);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005160 File Offset: 0x00003360
		public static bool operator !=(UnknownDeviceControl a, UnknownDeviceControl b)
		{
			return !(a == b);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000516C File Offset: 0x0000336C
		public bool Equals(UnknownDeviceControl other)
		{
			return this.Control == other.Control && this.SourceRange == other.SourceRange;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000518C File Offset: 0x0000338C
		public override bool Equals(object other)
		{
			return this.Equals((UnknownDeviceControl)other);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000519A File Offset: 0x0000339A
		public override int GetHashCode()
		{
			return this.Control.GetHashCode() ^ this.SourceRange.GetHashCode();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000051BF File Offset: 0x000033BF
		public static implicit operator bool(UnknownDeviceControl control)
		{
			return control.Control > InputControlType.None;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000051CA File Offset: 0x000033CA
		public override string ToString()
		{
			return string.Format("UnknownDeviceControl( {0}, {1} )", this.Control.ToString(), this.SourceRange.ToString());
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000051F8 File Offset: 0x000033F8
		internal void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
			writer.Write((int)this.SourceRange);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005212 File Offset: 0x00003412
		internal void Load(BinaryReader reader)
		{
			this.Control = (InputControlType)reader.ReadInt32();
			this.SourceRange = (InputRangeType)reader.ReadInt32();
			this.IsButton = Utility.TargetIsButton(this.Control);
			this.IsAnalog = !this.IsButton;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000524C File Offset: 0x0000344C
		// Note: this type is marked as 'beforefieldinit'.
		static UnknownDeviceControl()
		{
		}

		// Token: 0x04000122 RID: 290
		public static readonly UnknownDeviceControl None = new UnknownDeviceControl(InputControlType.None, InputRangeType.None);

		// Token: 0x04000123 RID: 291
		public InputControlType Control;

		// Token: 0x04000124 RID: 292
		public InputRangeType SourceRange;

		// Token: 0x04000125 RID: 293
		public bool IsButton;

		// Token: 0x04000126 RID: 294
		public bool IsAnalog;
	}
}
