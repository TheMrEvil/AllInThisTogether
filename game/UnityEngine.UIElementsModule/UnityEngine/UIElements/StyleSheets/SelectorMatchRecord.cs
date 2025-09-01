using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000366 RID: 870
	internal struct SelectorMatchRecord
	{
		// Token: 0x06001C2B RID: 7211 RVA: 0x00084C1E File Offset: 0x00082E1E
		public SelectorMatchRecord(StyleSheet sheet, int styleSheetIndexInStack)
		{
			this = default(SelectorMatchRecord);
			this.sheet = sheet;
			this.styleSheetIndexInStack = styleSheetIndexInStack;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00084C38 File Offset: 0x00082E38
		public static int Compare(SelectorMatchRecord a, SelectorMatchRecord b)
		{
			bool flag = a.sheet.isDefaultStyleSheet != b.sheet.isDefaultStyleSheet;
			int result;
			if (flag)
			{
				result = (a.sheet.isDefaultStyleSheet ? -1 : 1);
			}
			else
			{
				int num = a.complexSelector.specificity.CompareTo(b.complexSelector.specificity);
				bool flag2 = num == 0;
				if (flag2)
				{
					num = a.styleSheetIndexInStack.CompareTo(b.styleSheetIndexInStack);
				}
				bool flag3 = num == 0;
				if (flag3)
				{
					num = a.complexSelector.orderInStyleSheet.CompareTo(b.complexSelector.orderInStyleSheet);
				}
				result = num;
			}
			return result;
		}

		// Token: 0x04000E06 RID: 3590
		public StyleSheet sheet;

		// Token: 0x04000E07 RID: 3591
		public int styleSheetIndexInStack;

		// Token: 0x04000E08 RID: 3592
		public StyleComplexSelector complexSelector;
	}
}
