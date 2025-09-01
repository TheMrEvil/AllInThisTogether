using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000EC RID: 236
	public class DebugUIHandlerButton : DebugUIHandlerWidget
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001EC00 File Offset: 0x0001CE00
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Button>();
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001EC2B File Offset: 0x0001CE2B
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001EC3F File Offset: 0x0001CE3F
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001EC52 File Offset: 0x0001CE52
		public override void OnAction()
		{
			if (this.m_Field.action != null)
			{
				this.m_Field.action();
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001EC71 File Offset: 0x0001CE71
		public DebugUIHandlerButton()
		{
		}

		// Token: 0x040003DA RID: 986
		public Text nameLabel;

		// Token: 0x040003DB RID: 987
		private DebugUI.Button m_Field;
	}
}
