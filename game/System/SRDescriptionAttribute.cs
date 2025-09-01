using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000175 RID: 373
	[AttributeUsage(AttributeTargets.All)]
	internal class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002BADD File Offset: 0x00029CDD
		public override string Description
		{
			get
			{
				if (!this.isReplaced)
				{
					this.isReplaced = true;
					base.DescriptionValue = Locale.GetText(base.DescriptionValue);
				}
				return base.DescriptionValue;
			}
		}

		// Token: 0x040006B6 RID: 1718
		private bool isReplaced;
	}
}
