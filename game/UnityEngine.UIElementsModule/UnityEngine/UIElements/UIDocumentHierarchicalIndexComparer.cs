using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000254 RID: 596
	internal class UIDocumentHierarchicalIndexComparer : IComparer<UIDocumentHierarchicalIndex>
	{
		// Token: 0x06001218 RID: 4632 RVA: 0x000476BC File Offset: 0x000458BC
		public int Compare(UIDocumentHierarchicalIndex x, UIDocumentHierarchicalIndex y)
		{
			return x.CompareTo(y);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000020C2 File Offset: 0x000002C2
		public UIDocumentHierarchicalIndexComparer()
		{
		}
	}
}
