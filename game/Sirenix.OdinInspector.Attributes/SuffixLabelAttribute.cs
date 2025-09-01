using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000068 RID: 104
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Conditional("UNITY_EDITOR")]
	public sealed class SuffixLabelAttribute : Attribute
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003827 File Offset: 0x00001A27
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000382F File Offset: 0x00001A2F
		public SdfIconType Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				this.icon = value;
				this.HasDefinedIcon = true;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000383F File Offset: 0x00001A3F
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00003847 File Offset: 0x00001A47
		public bool HasDefinedIcon
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedIcon>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedIcon>k__BackingField = value;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003850 File Offset: 0x00001A50
		public SuffixLabelAttribute(string label, bool overlay = false)
		{
			this.Label = label;
			this.Overlay = overlay;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003866 File Offset: 0x00001A66
		public SuffixLabelAttribute(string label, SdfIconType icon, bool overlay = false)
		{
			this.Label = label;
			this.Icon = icon;
			this.Overlay = overlay;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003883 File Offset: 0x00001A83
		public SuffixLabelAttribute(SdfIconType icon)
		{
			this.Icon = icon;
		}

		// Token: 0x0400010E RID: 270
		public string Label;

		// Token: 0x0400010F RID: 271
		public bool Overlay;

		// Token: 0x04000110 RID: 272
		public string IconColor;

		// Token: 0x04000111 RID: 273
		[CompilerGenerated]
		private bool <HasDefinedIcon>k__BackingField;

		// Token: 0x04000112 RID: 274
		private SdfIconType icon;
	}
}
