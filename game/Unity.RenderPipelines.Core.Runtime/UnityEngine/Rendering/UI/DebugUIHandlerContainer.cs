using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F0 RID: 240
	public class DebugUIHandlerContainer : MonoBehaviour
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x0001F970 File Offset: 0x0001DB70
		internal DebugUIHandlerWidget GetFirstItem()
		{
			if (this.contentHolder.childCount == 0)
			{
				return null;
			}
			List<DebugUIHandlerWidget> activeChildren = this.GetActiveChildren();
			if (activeChildren.Count == 0)
			{
				return null;
			}
			return activeChildren[0];
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001F9A4 File Offset: 0x0001DBA4
		internal DebugUIHandlerWidget GetLastItem()
		{
			if (this.contentHolder.childCount == 0)
			{
				return null;
			}
			List<DebugUIHandlerWidget> activeChildren = this.GetActiveChildren();
			if (activeChildren.Count == 0)
			{
				return null;
			}
			return activeChildren[activeChildren.Count - 1];
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001F9E0 File Offset: 0x0001DBE0
		internal bool IsDirectChild(DebugUIHandlerWidget widget)
		{
			return this.contentHolder.childCount != 0 && this.GetActiveChildren().Count((DebugUIHandlerWidget x) => x == widget) > 0;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001FA24 File Offset: 0x0001DC24
		private List<DebugUIHandlerWidget> GetActiveChildren()
		{
			List<DebugUIHandlerWidget> list = new List<DebugUIHandlerWidget>();
			foreach (object obj in this.contentHolder)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeInHierarchy)
				{
					DebugUIHandlerWidget component = transform.GetComponent<DebugUIHandlerWidget>();
					if (component != null)
					{
						list.Add(component);
					}
				}
			}
			return list;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		public DebugUIHandlerContainer()
		{
		}

		// Token: 0x040003EF RID: 1007
		[SerializeField]
		public RectTransform contentHolder;

		// Token: 0x02000185 RID: 389
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x0600092D RID: 2349 RVA: 0x00024AFC File Offset: 0x00022CFC
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x0600092E RID: 2350 RVA: 0x00024B04 File Offset: 0x00022D04
			internal bool <IsDirectChild>b__0(DebugUIHandlerWidget x)
			{
				return x == this.widget;
			}

			// Token: 0x040005D0 RID: 1488
			public DebugUIHandlerWidget widget;
		}
	}
}
