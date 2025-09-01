using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F3 RID: 243
	public class DebugUIHandlerFloatField : DebugUIHandlerWidget
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x0001FF34 File Offset: 0x0001E134
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.FloatField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001FF65 File Offset: 0x0001E165
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001FF8A File Offset: 0x0001E18A
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001FFAE File Offset: 0x0001E1AE
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1f);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001FFBC File Offset: 0x0001E1BC
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1f);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001FFCC File Offset: 0x0001E1CC
		private void ChangeValue(bool fast, float multiplier)
		{
			float num = this.m_Field.GetValue();
			num += this.m_Field.incStep * (fast ? this.m_Field.incStepMult : 1f) * multiplier;
			this.m_Field.SetValue(num);
			this.UpdateValueLabel();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00020020 File Offset: 0x0001E220
		private void UpdateValueLabel()
		{
			this.valueLabel.text = this.m_Field.GetValue().ToString("N" + this.m_Field.decimals.ToString());
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00020065 File Offset: 0x0001E265
		public DebugUIHandlerFloatField()
		{
		}

		// Token: 0x040003F7 RID: 1015
		public Text nameLabel;

		// Token: 0x040003F8 RID: 1016
		public Text valueLabel;

		// Token: 0x040003F9 RID: 1017
		private DebugUI.FloatField m_Field;
	}
}
