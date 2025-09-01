using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class MinMaxSliderAttribute : Attribute
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002D54 File Offset: 0x00000F54
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00002D5C File Offset: 0x00000F5C
		[Obsolete("Use the MinValueGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MinMember
		{
			get
			{
				return this.MinValueGetter;
			}
			set
			{
				this.MinValueGetter = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002D65 File Offset: 0x00000F65
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00002D6D File Offset: 0x00000F6D
		[Obsolete("Use the MaxValueGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MaxMember
		{
			get
			{
				return this.MaxValueGetter;
			}
			set
			{
				this.MaxValueGetter = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002D76 File Offset: 0x00000F76
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002D7E File Offset: 0x00000F7E
		[Obsolete("Use the MinMaxValueGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MinMaxMember
		{
			get
			{
				return this.MinMaxValueGetter;
			}
			set
			{
				this.MinMaxValueGetter = value;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002D87 File Offset: 0x00000F87
		public MinMaxSliderAttribute(float minValue, float maxValue, bool showFields = false)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.ShowFields = showFields;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public MinMaxSliderAttribute(string minValueGetter, float maxValue, bool showFields = false)
		{
			this.MinValueGetter = minValueGetter;
			this.MaxValue = maxValue;
			this.ShowFields = showFields;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002DC1 File Offset: 0x00000FC1
		public MinMaxSliderAttribute(float minValue, string maxValueGetter, bool showFields = false)
		{
			this.MinValue = minValue;
			this.MaxValueGetter = maxValueGetter;
			this.ShowFields = showFields;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002DDE File Offset: 0x00000FDE
		public MinMaxSliderAttribute(string minValueGetter, string maxValueGetter, bool showFields = false)
		{
			this.MinValueGetter = minValueGetter;
			this.MaxValueGetter = maxValueGetter;
			this.ShowFields = showFields;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002DFB File Offset: 0x00000FFB
		public MinMaxSliderAttribute(string minMaxValueGetter, bool showFields = false)
		{
			this.MinMaxValueGetter = minMaxValueGetter;
			this.ShowFields = showFields;
		}

		// Token: 0x040000B0 RID: 176
		public float MinValue;

		// Token: 0x040000B1 RID: 177
		public float MaxValue;

		// Token: 0x040000B2 RID: 178
		public string MinValueGetter;

		// Token: 0x040000B3 RID: 179
		public string MaxValueGetter;

		// Token: 0x040000B4 RID: 180
		public string MinMaxValueGetter;

		// Token: 0x040000B5 RID: 181
		public bool ShowFields;
	}
}
