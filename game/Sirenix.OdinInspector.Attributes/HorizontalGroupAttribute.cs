using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class HorizontalGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00002907 File Offset: 0x00000B07
		public HorizontalGroupAttribute(string group, float width = 0f, int marginLeft = 0, int marginRight = 0, float order = 0f) : base(group, order)
		{
			this.Width = width;
			this.MarginLeft = (float)marginLeft;
			this.MarginRight = (float)marginRight;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002935 File Offset: 0x00000B35
		public HorizontalGroupAttribute(float width = 0f, int marginLeft = 0, int marginRight = 0, float order = 0f) : this("_DefaultHorizontalGroup", width, marginLeft, marginRight, order)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002948 File Offset: 0x00000B48
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			HorizontalGroupAttribute horizontalGroupAttribute = other as HorizontalGroupAttribute;
			if (horizontalGroupAttribute != null)
			{
				this.Title = (this.Title ?? horizontalGroupAttribute.Title);
				this.DisableAutomaticLabelWidth = (this.DisableAutomaticLabelWidth || horizontalGroupAttribute.DisableAutomaticLabelWidth);
				if (this.LabelWidth == 0f && horizontalGroupAttribute.LabelWidth != 0f)
				{
					this.LabelWidth = horizontalGroupAttribute.LabelWidth;
				}
				if (horizontalGroupAttribute.Gap != 3f)
				{
					this.Gap = horizontalGroupAttribute.Gap;
				}
			}
		}

		// Token: 0x04000066 RID: 102
		private const int DefaultHorizontalGroupGap = 3;

		// Token: 0x04000067 RID: 103
		public float Width;

		// Token: 0x04000068 RID: 104
		public float MarginLeft;

		// Token: 0x04000069 RID: 105
		public float MarginRight;

		// Token: 0x0400006A RID: 106
		public float PaddingLeft;

		// Token: 0x0400006B RID: 107
		public float PaddingRight;

		// Token: 0x0400006C RID: 108
		public float MinWidth;

		// Token: 0x0400006D RID: 109
		public float MaxWidth;

		// Token: 0x0400006E RID: 110
		public float Gap = 3f;

		// Token: 0x0400006F RID: 111
		public string Title;

		// Token: 0x04000070 RID: 112
		public bool DisableAutomaticLabelWidth;

		// Token: 0x04000071 RID: 113
		public float LabelWidth;
	}
}
