using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200006F RID: 111
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class TitleAttribute : Attribute
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00003B80 File Offset: 0x00001D80
		public TitleAttribute(string title, string subtitle = null, TitleAlignments titleAlignment = TitleAlignments.Left, bool horizontalLine = true, bool bold = true)
		{
			this.Title = (title ?? "null");
			this.Subtitle = subtitle;
			this.Bold = bold;
			this.TitleAlignment = titleAlignment;
			this.HorizontalLine = horizontalLine;
		}

		// Token: 0x0400013A RID: 314
		public string Title;

		// Token: 0x0400013B RID: 315
		public string Subtitle;

		// Token: 0x0400013C RID: 316
		public bool Bold;

		// Token: 0x0400013D RID: 317
		public bool HorizontalLine;

		// Token: 0x0400013E RID: 318
		public TitleAlignments TitleAlignment;
	}
}
