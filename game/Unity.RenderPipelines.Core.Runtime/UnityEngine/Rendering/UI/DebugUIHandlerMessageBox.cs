using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FA RID: 250
	public class DebugUIHandlerMessageBox : DebugUIHandlerWidget
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x000207E0 File Offset: 0x0001E9E0
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.MessageBox>();
			this.nameLabel.text = this.m_Field.displayName;
			Image component = base.GetComponent<Image>();
			DebugUI.MessageBox.Style style = this.m_Field.style;
			if (style == DebugUI.MessageBox.Style.Warning)
			{
				component.color = DebugUIHandlerMessageBox.k_WarningBackgroundColor;
				return;
			}
			if (style != DebugUI.MessageBox.Style.Error)
			{
				return;
			}
			component.color = DebugUIHandlerMessageBox.k_ErrorBackgroundColor;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00020853 File Offset: 0x0001EA53
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			return false;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00020856 File Offset: 0x0001EA56
		public DebugUIHandlerMessageBox()
		{
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00020860 File Offset: 0x0001EA60
		// Note: this type is marked as 'beforefieldinit'.
		static DebugUIHandlerMessageBox()
		{
		}

		// Token: 0x04000415 RID: 1045
		public Text nameLabel;

		// Token: 0x04000416 RID: 1046
		private DebugUI.MessageBox m_Field;

		// Token: 0x04000417 RID: 1047
		private static Color32 k_WarningBackgroundColor = new Color32(231, 180, 3, 30);

		// Token: 0x04000418 RID: 1048
		private static Color32 k_WarningTextColor = new Color32(231, 180, 3, byte.MaxValue);

		// Token: 0x04000419 RID: 1049
		private static Color32 k_ErrorBackgroundColor = new Color32(231, 75, 3, 30);

		// Token: 0x0400041A RID: 1050
		private static Color32 k_ErrorTextColor = new Color32(231, 75, 3, byte.MaxValue);
	}
}
