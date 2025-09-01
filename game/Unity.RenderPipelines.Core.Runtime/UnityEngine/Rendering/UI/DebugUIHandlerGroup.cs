using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F5 RID: 245
	public class DebugUIHandlerGroup : DebugUIHandlerWidget
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x00020328 File Offset: 0x0001E528
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Container>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			if (string.IsNullOrEmpty(this.m_Field.displayName))
			{
				this.header.gameObject.SetActive(false);
				return;
			}
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00020390 File Offset: 0x0001E590
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

		// Token: 0x06000745 RID: 1861 RVA: 0x000203CC File Offset: 0x0001E5CC
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

		// Token: 0x06000746 RID: 1862 RVA: 0x0002040B File Offset: 0x0001E60B
		public DebugUIHandlerGroup()
		{
		}

		// Token: 0x04000400 RID: 1024
		public Text nameLabel;

		// Token: 0x04000401 RID: 1025
		public Transform header;

		// Token: 0x04000402 RID: 1026
		private DebugUI.Container m_Field;

		// Token: 0x04000403 RID: 1027
		private DebugUIHandlerContainer m_Container;
	}
}
