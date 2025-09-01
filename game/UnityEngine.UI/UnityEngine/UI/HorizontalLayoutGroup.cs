using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001D RID: 29
	[AddComponentMenu("Layout/Horizontal Layout Group", 150)]
	public class HorizontalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000DD8B File Offset: 0x0000BF8B
		protected HorizontalLayoutGroup()
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000DD93 File Offset: 0x0000BF93
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000DDA3 File Offset: 0x0000BFA3
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000DDAD File Offset: 0x0000BFAD
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DDB7 File Offset: 0x0000BFB7
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
