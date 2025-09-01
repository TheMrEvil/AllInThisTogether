using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F9 RID: 249
	public class DebugUIHandlerIntField : DebugUIHandlerWidget
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x000206BA File Offset: 0x0001E8BA
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.IntField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000206EB File Offset: 0x0001E8EB
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00020710 File Offset: 0x0001E910
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00020734 File Offset: 0x0001E934
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0002073E File Offset: 0x0001E93E
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00020748 File Offset: 0x0001E948
		private void ChangeValue(bool fast, int multiplier)
		{
			int num = this.m_Field.GetValue();
			num += this.m_Field.incStep * (fast ? this.m_Field.intStepMult : 1) * multiplier;
			this.m_Field.SetValue(num);
			this.UpdateValueLabel();
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00020798 File Offset: 0x0001E998
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString("N0");
			}
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000207D6 File Offset: 0x0001E9D6
		public DebugUIHandlerIntField()
		{
		}

		// Token: 0x04000412 RID: 1042
		public Text nameLabel;

		// Token: 0x04000413 RID: 1043
		public Text valueLabel;

		// Token: 0x04000414 RID: 1044
		private DebugUI.IntField m_Field;
	}
}
