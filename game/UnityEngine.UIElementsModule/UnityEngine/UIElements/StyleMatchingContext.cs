using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FA RID: 250
	internal class StyleMatchingContext
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0001C7BF File Offset: 0x0001A9BF
		public int styleSheetCount
		{
			get
			{
				return this.m_StyleSheetStack.Count;
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001C7CC File Offset: 0x0001A9CC
		public StyleMatchingContext(Action<VisualElement, MatchResultInfo> processResult)
		{
			this.m_StyleSheetStack = new List<StyleSheet>();
			this.variableContext = StyleVariableContext.none;
			this.currentElement = null;
			this.processResult = processResult;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001C7FC File Offset: 0x0001A9FC
		public void AddStyleSheet(StyleSheet sheet)
		{
			bool flag = sheet == null;
			if (!flag)
			{
				this.m_StyleSheetStack.Add(sheet);
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001C824 File Offset: 0x0001AA24
		public void RemoveStyleSheetRange(int index, int count)
		{
			this.m_StyleSheetStack.RemoveRange(index, count);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001C838 File Offset: 0x0001AA38
		public StyleSheet GetStyleSheetAt(int index)
		{
			return this.m_StyleSheetStack[index];
		}

		// Token: 0x0400032C RID: 812
		private List<StyleSheet> m_StyleSheetStack;

		// Token: 0x0400032D RID: 813
		public StyleVariableContext variableContext;

		// Token: 0x0400032E RID: 814
		public VisualElement currentElement;

		// Token: 0x0400032F RID: 815
		public Action<VisualElement, MatchResultInfo> processResult;
	}
}
