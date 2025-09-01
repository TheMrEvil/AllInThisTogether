using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FE RID: 254
	public class DebugUIHandlerToggle : DebugUIHandlerWidget
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x00020D88 File Offset: 0x0001EF88
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.BoolField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
			this.valueToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00020DE0 File Offset: 0x0001EFE0
		private void OnToggleValueChanged(bool value)
		{
			this.m_Field.SetValue(value);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00020DEE File Offset: 0x0001EFEE
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.checkmarkImage.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00020E13 File Offset: 0x0001F013
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.checkmarkImage.color = this.colorDefault;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00020E38 File Offset: 0x0001F038
		public override void OnAction()
		{
			bool value = !this.m_Field.GetValue();
			this.m_Field.SetValue(value);
			this.UpdateValueLabel();
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00020E66 File Offset: 0x0001F066
		protected internal virtual void UpdateValueLabel()
		{
			if (this.valueToggle != null)
			{
				this.valueToggle.isOn = this.m_Field.GetValue();
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00020E8C File Offset: 0x0001F08C
		public DebugUIHandlerToggle()
		{
		}

		// Token: 0x04000428 RID: 1064
		public Text nameLabel;

		// Token: 0x04000429 RID: 1065
		public Toggle valueToggle;

		// Token: 0x0400042A RID: 1066
		public Image checkmarkImage;

		// Token: 0x0400042B RID: 1067
		protected internal DebugUI.BoolField m_Field;
	}
}
