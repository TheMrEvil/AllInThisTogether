using System;
using System.ComponentModel;

namespace System.Drawing
{
	// Token: 0x02000089 RID: 137
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x00015BD3 File Offset: 0x00013DD3
		public SRDescriptionAttribute(string description) : base(description)
		{
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00015BDC File Offset: 0x00013DDC
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

		// Token: 0x04000567 RID: 1383
		private bool isReplaced;
	}
}
