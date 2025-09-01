using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200004B RID: 75
	public struct PickerOptions
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0001AA78 File Offset: 0x00018C78
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0001AA80 File Offset: 0x00018C80
		public bool depthTest
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<depthTest>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<depthTest>k__BackingField = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0001AA89 File Offset: 0x00018C89
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x0001AA91 File Offset: 0x00018C91
		public RectSelectMode rectSelectMode
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<rectSelectMode>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rectSelectMode>k__BackingField = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0001AA9A File Offset: 0x00018C9A
		public static PickerOptions Default
		{
			get
			{
				return PickerOptions.k_Default;
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001AAA1 File Offset: 0x00018CA1
		public override bool Equals(object obj)
		{
			return obj is PickerOptions && this.Equals((PickerOptions)obj);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001AAB9 File Offset: 0x00018CB9
		public bool Equals(PickerOptions other)
		{
			return this.depthTest == other.depthTest && this.rectSelectMode == other.rectSelectMode;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001AADC File Offset: 0x00018CDC
		public override int GetHashCode()
		{
			return this.depthTest.GetHashCode() * 397 ^ (int)this.rectSelectMode;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001AB04 File Offset: 0x00018D04
		public static bool operator ==(PickerOptions a, PickerOptions b)
		{
			return a.Equals(b);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001AB0E File Offset: 0x00018D0E
		public static bool operator !=(PickerOptions a, PickerOptions b)
		{
			return !a.Equals(b);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001AB1C File Offset: 0x00018D1C
		// Note: this type is marked as 'beforefieldinit'.
		static PickerOptions()
		{
		}

		// Token: 0x040001B5 RID: 437
		[CompilerGenerated]
		private bool <depthTest>k__BackingField;

		// Token: 0x040001B6 RID: 438
		[CompilerGenerated]
		private RectSelectMode <rectSelectMode>k__BackingField;

		// Token: 0x040001B7 RID: 439
		private static readonly PickerOptions k_Default = new PickerOptions
		{
			depthTest = true,
			rectSelectMode = RectSelectMode.Partial
		};
	}
}
