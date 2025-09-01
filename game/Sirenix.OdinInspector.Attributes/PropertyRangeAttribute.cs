using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000054 RID: 84
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class PropertyRangeAttribute : Attribute
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003431 File Offset: 0x00001631
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00003439 File Offset: 0x00001639
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003442 File Offset: 0x00001642
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000344A File Offset: 0x0000164A
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

		// Token: 0x06000122 RID: 290 RVA: 0x00003453 File Offset: 0x00001653
		public PropertyRangeAttribute(double min, double max)
		{
			this.Min = ((min < max) ? min : max);
			this.Max = ((max > min) ? max : min);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003477 File Offset: 0x00001677
		public PropertyRangeAttribute(string minGetter, double max)
		{
			this.MinGetter = minGetter;
			this.Max = max;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000348D File Offset: 0x0000168D
		public PropertyRangeAttribute(double min, string maxGetter)
		{
			this.Min = min;
			this.MaxGetter = maxGetter;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000034A3 File Offset: 0x000016A3
		public PropertyRangeAttribute(string minGetter, string maxGetter)
		{
			this.MinGetter = minGetter;
			this.MaxGetter = maxGetter;
		}

		// Token: 0x040000ED RID: 237
		public double Min;

		// Token: 0x040000EE RID: 238
		public double Max;

		// Token: 0x040000EF RID: 239
		public string MinGetter;

		// Token: 0x040000F0 RID: 240
		public string MaxGetter;
	}
}
