using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000102 RID: 258
	public class DebugUIHandlerVBox : DebugUIHandlerWidget
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x0002125D File Offset: 0x0001F45D
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00021274 File Offset: 0x0001F474
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (!fromNext && !this.m_Container.IsDirectChild(previous))
			{
				DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
				DebugManager.instance.ChangeSelection(lastItem, false);
				return true;
			}
			return false;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000212B0 File Offset: 0x0001F4B0
		public override DebugUIHandlerWidget Next()
		{
			if (this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000212EF File Offset: 0x0001F4EF
		public DebugUIHandlerVBox()
		{
		}

		// Token: 0x04000435 RID: 1077
		private DebugUIHandlerContainer m_Container;
	}
}
