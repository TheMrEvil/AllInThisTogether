using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000051 RID: 81
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ProgressBarAttribute : Attribute
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000308A File Offset: 0x0000128A
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00003092 File Offset: 0x00001292
		[Obsolete("Use the MinGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MinMember
		{
			get
			{
				return this.MinGetter;
			}
			set
			{
				this.MinGetter = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000309B File Offset: 0x0000129B
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000030A3 File Offset: 0x000012A3
		[Obsolete("Use the MaxGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MaxMember
		{
			get
			{
				return this.MaxGetter;
			}
			set
			{
				this.MaxGetter = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000030AC File Offset: 0x000012AC
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000030B4 File Offset: 0x000012B4
		[Obsolete("Use the ColorGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string ColorMember
		{
			get
			{
				return this.ColorGetter;
			}
			set
			{
				this.ColorGetter = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000030BD File Offset: 0x000012BD
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000030C5 File Offset: 0x000012C5
		[Obsolete("Use the BackgroundColorGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string BackgroundColorMember
		{
			get
			{
				return this.BackgroundColorGetter;
			}
			set
			{
				this.BackgroundColorGetter = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000030CE File Offset: 0x000012CE
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000030D6 File Offset: 0x000012D6
		[Obsolete("Use the CustomValueStringGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string CustomValueStringMember
		{
			get
			{
				return this.CustomValueStringGetter;
			}
			set
			{
				this.CustomValueStringGetter = value;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000030E0 File Offset: 0x000012E0
		public ProgressBarAttribute(double min, double max, float r = 0.15f, float g = 0.47f, float b = 0.74f)
		{
			this.Min = min;
			this.Max = max;
			this.R = r;
			this.G = g;
			this.B = b;
			this.Height = 12;
			this.Segmented = false;
			this.drawValueLabel = true;
			this.DrawValueLabelHasValue = false;
			this.valueLabelAlignment = TextAlignment.Center;
			this.ValueLabelAlignmentHasValue = false;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003144 File Offset: 0x00001344
		public ProgressBarAttribute(string minGetter, double max, float r = 0.15f, float g = 0.47f, float b = 0.74f)
		{
			this.MinGetter = minGetter;
			this.Max = max;
			this.R = r;
			this.G = g;
			this.B = b;
			this.Height = 12;
			this.Segmented = false;
			this.drawValueLabel = true;
			this.DrawValueLabelHasValue = false;
			this.valueLabelAlignment = TextAlignment.Center;
			this.ValueLabelAlignmentHasValue = false;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000031A8 File Offset: 0x000013A8
		public ProgressBarAttribute(double min, string maxGetter, float r = 0.15f, float g = 0.47f, float b = 0.74f)
		{
			this.Min = min;
			this.MaxGetter = maxGetter;
			this.R = r;
			this.G = g;
			this.B = b;
			this.Height = 12;
			this.Segmented = false;
			this.drawValueLabel = true;
			this.DrawValueLabelHasValue = false;
			this.valueLabelAlignment = TextAlignment.Center;
			this.ValueLabelAlignmentHasValue = false;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000320C File Offset: 0x0000140C
		public ProgressBarAttribute(string minGetter, string maxGetter, float r = 0.15f, float g = 0.47f, float b = 0.74f)
		{
			this.MinGetter = minGetter;
			this.MaxGetter = maxGetter;
			this.R = r;
			this.G = g;
			this.B = b;
			this.Height = 12;
			this.Segmented = false;
			this.drawValueLabel = true;
			this.DrawValueLabelHasValue = false;
			this.valueLabelAlignment = TextAlignment.Center;
			this.ValueLabelAlignmentHasValue = false;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000326F File Offset: 0x0000146F
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00003277 File Offset: 0x00001477
		public bool DrawValueLabel
		{
			get
			{
				return this.drawValueLabel;
			}
			set
			{
				this.drawValueLabel = value;
				this.DrawValueLabelHasValue = true;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00003287 File Offset: 0x00001487
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000328F File Offset: 0x0000148F
		public bool DrawValueLabelHasValue
		{
			[CompilerGenerated]
			get
			{
				return this.<DrawValueLabelHasValue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DrawValueLabelHasValue>k__BackingField = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00003298 File Offset: 0x00001498
		// (set) Token: 0x06000114 RID: 276 RVA: 0x000032A0 File Offset: 0x000014A0
		public TextAlignment ValueLabelAlignment
		{
			get
			{
				return this.valueLabelAlignment;
			}
			set
			{
				this.valueLabelAlignment = value;
				this.ValueLabelAlignmentHasValue = true;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000032B0 File Offset: 0x000014B0
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000032B8 File Offset: 0x000014B8
		public bool ValueLabelAlignmentHasValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ValueLabelAlignmentHasValue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ValueLabelAlignmentHasValue>k__BackingField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000032C1 File Offset: 0x000014C1
		public Color Color
		{
			get
			{
				return new Color(this.R, this.G, this.B, 1f);
			}
		}

		// Token: 0x040000D6 RID: 214
		public double Min;

		// Token: 0x040000D7 RID: 215
		public double Max;

		// Token: 0x040000D8 RID: 216
		public string MinGetter;

		// Token: 0x040000D9 RID: 217
		public string MaxGetter;

		// Token: 0x040000DA RID: 218
		public float R;

		// Token: 0x040000DB RID: 219
		public float G;

		// Token: 0x040000DC RID: 220
		public float B;

		// Token: 0x040000DD RID: 221
		public int Height;

		// Token: 0x040000DE RID: 222
		public string ColorGetter;

		// Token: 0x040000DF RID: 223
		public string BackgroundColorGetter;

		// Token: 0x040000E0 RID: 224
		public bool Segmented;

		// Token: 0x040000E1 RID: 225
		public string CustomValueStringGetter;

		// Token: 0x040000E2 RID: 226
		private bool drawValueLabel;

		// Token: 0x040000E3 RID: 227
		private TextAlignment valueLabelAlignment;

		// Token: 0x040000E4 RID: 228
		[CompilerGenerated]
		private bool <DrawValueLabelHasValue>k__BackingField;

		// Token: 0x040000E5 RID: 229
		[CompilerGenerated]
		private bool <ValueLabelAlignmentHasValue>k__BackingField;
	}
}
