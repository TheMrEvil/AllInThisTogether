using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace InControl
{
	// Token: 0x0200000C RID: 12
	public abstract class BindingSource : IEquatable<BindingSource>
	{
		// Token: 0x0600002D RID: 45
		public abstract float GetValue(InputDevice inputDevice);

		// Token: 0x0600002E RID: 46
		public abstract bool GetState(InputDevice inputDevice);

		// Token: 0x0600002F RID: 47
		public abstract bool Equals(BindingSource other);

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000030 RID: 48
		public abstract string Name { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000031 RID: 49
		public abstract string DeviceName { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000032 RID: 50
		public abstract InputDeviceClass DeviceClass { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000033 RID: 51
		public abstract InputDeviceStyle DeviceStyle { get; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002511 File Offset: 0x00000711
		public static bool operator ==(BindingSource a, BindingSource b)
		{
			return a == b || (a != null && b != null && a.BindingSourceType == b.BindingSourceType && a.Equals(b));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002538 File Offset: 0x00000738
		public static bool operator !=(BindingSource a, BindingSource b)
		{
			return !(a == b);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002544 File Offset: 0x00000744
		public override bool Equals(object obj)
		{
			return this.Equals((BindingSource)obj);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002552 File Offset: 0x00000752
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000038 RID: 56
		public abstract BindingSourceType BindingSourceType { get; }

		// Token: 0x06000039 RID: 57
		public abstract void Save(BinaryWriter writer);

		// Token: 0x0600003A RID: 58
		public abstract void Load(BinaryReader reader, ushort dataFormatVersion);

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000255A File Offset: 0x0000075A
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002562 File Offset: 0x00000762
		internal PlayerAction BoundTo
		{
			[CompilerGenerated]
			get
			{
				return this.<BoundTo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BoundTo>k__BackingField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000256B File Offset: 0x0000076B
		internal virtual bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000256E File Offset: 0x0000076E
		protected BindingSource()
		{
		}

		// Token: 0x04000032 RID: 50
		[CompilerGenerated]
		private PlayerAction <BoundTo>k__BackingField;
	}
}
