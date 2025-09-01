using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FC RID: 252
	internal class DebugUIHandlerPersistentCanvas : MonoBehaviour
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x00020AB4 File Offset: 0x0001ECB4
		internal void Toggle(DebugUI.Value widget)
		{
			int num = this.m_Items.FindIndex((DebugUIHandlerValue x) => x.GetWidget() == widget);
			if (num > -1)
			{
				CoreUtils.Destroy(this.m_Items[num].gameObject);
				this.m_Items.RemoveAt(num);
				return;
			}
			GameObject gameObject = Object.Instantiate<RectTransform>(this.valuePrefab, this.panel, false).gameObject;
			gameObject.name = widget.displayName;
			DebugUIHandlerValue component = gameObject.GetComponent<DebugUIHandlerValue>();
			component.SetWidget(widget);
			this.m_Items.Add(component);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00020B54 File Offset: 0x0001ED54
		internal void Clear()
		{
			if (this.m_Items == null)
			{
				return;
			}
			foreach (DebugUIHandlerValue debugUIHandlerValue in this.m_Items)
			{
				CoreUtils.Destroy(debugUIHandlerValue.gameObject);
			}
			this.m_Items.Clear();
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		public DebugUIHandlerPersistentCanvas()
		{
		}

		// Token: 0x04000424 RID: 1060
		public RectTransform panel;

		// Token: 0x04000425 RID: 1061
		public RectTransform valuePrefab;

		// Token: 0x04000426 RID: 1062
		private List<DebugUIHandlerValue> m_Items = new List<DebugUIHandlerValue>();

		// Token: 0x02000187 RID: 391
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06000935 RID: 2357 RVA: 0x00024BC0 File Offset: 0x00022DC0
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000936 RID: 2358 RVA: 0x00024BC8 File Offset: 0x00022DC8
			internal bool <Toggle>b__0(DebugUIHandlerValue x)
			{
				return x.GetWidget() == this.widget;
			}

			// Token: 0x040005D4 RID: 1492
			public DebugUI.Value widget;
		}
	}
}
