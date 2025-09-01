using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F6 RID: 246
	public class DebugUIHandlerHBox : DebugUIHandlerWidget
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00020413 File Offset: 0x0001E613
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00020428 File Offset: 0x0001E628
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

		// Token: 0x06000749 RID: 1865 RVA: 0x00020464 File Offset: 0x0001E664
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

		// Token: 0x0600074A RID: 1866 RVA: 0x000204A3 File Offset: 0x0001E6A3
		public DebugUIHandlerHBox()
		{
		}

		// Token: 0x04000404 RID: 1028
		private DebugUIHandlerContainer m_Container;
	}
}
