using System;

namespace UnityEngine.UI
{
	// Token: 0x02000028 RID: 40
	[AddComponentMenu("Layout/Vertical Layout Group", 151)]
	public class VerticalLayoutGroup : HorizontalOrVerticalLayoutGroup
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000F284 File Offset: 0x0000D484
		protected VerticalLayoutGroup()
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000F28C File Offset: 0x0000D48C
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, true);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000F29C File Offset: 0x0000D49C
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, true);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000F2A6 File Offset: 0x0000D4A6
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, true);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, true);
		}
	}
}
