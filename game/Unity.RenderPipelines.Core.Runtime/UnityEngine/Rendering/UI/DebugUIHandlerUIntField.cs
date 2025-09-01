using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000100 RID: 256
	public class DebugUIHandlerUIntField : DebugUIHandlerWidget
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0002103B File Offset: 0x0001F23B
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.UIntField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002106C File Offset: 0x0001F26C
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00021091 File Offset: 0x0001F291
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000210B5 File Offset: 0x0001F2B5
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000210BF File Offset: 0x0001F2BF
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000210CC File Offset: 0x0001F2CC
		private void ChangeValue(bool fast, int multiplier)
		{
			long num = (long)((ulong)this.m_Field.GetValue());
			if (num == 0L && multiplier < 0)
			{
				return;
			}
			num += (long)((ulong)(this.m_Field.incStep * (fast ? this.m_Field.intStepMult : 1U)) * (ulong)((long)multiplier));
			this.m_Field.SetValue((uint)num);
			this.UpdateValueLabel();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00021128 File Offset: 0x0001F328
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString("N0");
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00021166 File Offset: 0x0001F366
		public DebugUIHandlerUIntField()
		{
		}

		// Token: 0x0400042E RID: 1070
		public Text nameLabel;

		// Token: 0x0400042F RID: 1071
		public Text valueLabel;

		// Token: 0x04000430 RID: 1072
		private DebugUI.UIntField m_Field;
	}
}
