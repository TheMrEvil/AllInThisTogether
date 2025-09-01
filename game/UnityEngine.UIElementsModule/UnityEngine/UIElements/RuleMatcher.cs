using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C6 RID: 198
	internal struct RuleMatcher
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0001909F File Offset: 0x0001729F
		public RuleMatcher(StyleSheet sheet, StyleComplexSelector complexSelector, int styleSheetIndexInStack)
		{
			this.sheet = sheet;
			this.complexSelector = complexSelector;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000190B0 File Offset: 0x000172B0
		public override string ToString()
		{
			return this.complexSelector.ToString();
		}

		// Token: 0x0400029E RID: 670
		public StyleSheet sheet;

		// Token: 0x0400029F RID: 671
		public StyleComplexSelector complexSelector;
	}
}
