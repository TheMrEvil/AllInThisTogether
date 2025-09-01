using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000070 RID: 112
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class TitleGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00003BB6 File Offset: 0x00001DB6
		public TitleGroupAttribute(string title, string subtitle = null, TitleAlignments alignment = TitleAlignments.Left, bool horizontalLine = true, bool boldTitle = true, bool indent = false, float order = 0f) : base(title, order)
		{
			this.Subtitle = subtitle;
			this.Alignment = alignment;
			this.HorizontalLine = horizontalLine;
			this.BoldTitle = boldTitle;
			this.Indent = indent;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			TitleGroupAttribute titleGroupAttribute = other as TitleGroupAttribute;
			if (this.Subtitle != null)
			{
				titleGroupAttribute.Subtitle = this.Subtitle;
			}
			else
			{
				this.Subtitle = titleGroupAttribute.Subtitle;
			}
			if (this.Alignment != TitleAlignments.Left)
			{
				titleGroupAttribute.Alignment = this.Alignment;
			}
			else
			{
				this.Alignment = titleGroupAttribute.Alignment;
			}
			if (!this.HorizontalLine)
			{
				titleGroupAttribute.HorizontalLine = this.HorizontalLine;
			}
			else
			{
				this.HorizontalLine = titleGroupAttribute.HorizontalLine;
			}
			if (!this.BoldTitle)
			{
				titleGroupAttribute.BoldTitle = this.BoldTitle;
			}
			else
			{
				this.BoldTitle = titleGroupAttribute.BoldTitle;
			}
			if (this.Indent)
			{
				titleGroupAttribute.Indent = this.Indent;
				return;
			}
			this.Indent = titleGroupAttribute.Indent;
		}

		// Token: 0x0400013F RID: 319
		public string Subtitle;

		// Token: 0x04000140 RID: 320
		public TitleAlignments Alignment;

		// Token: 0x04000141 RID: 321
		public bool HorizontalLine;

		// Token: 0x04000142 RID: 322
		public bool BoldTitle;

		// Token: 0x04000143 RID: 323
		public bool Indent;
	}
}
