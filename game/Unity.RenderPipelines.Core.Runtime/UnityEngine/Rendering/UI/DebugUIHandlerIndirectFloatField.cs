using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F7 RID: 247
	public class DebugUIHandlerIndirectFloatField : DebugUIHandlerWidget
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x000204AB File Offset: 0x0001E6AB
		public void Init()
		{
			this.UpdateValueLabel();
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000204B3 File Offset: 0x0001E6B3
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000204D8 File Offset: 0x0001E6D8
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000204FC File Offset: 0x0001E6FC
		public override void OnIncrement(bool fast)
		{
			this.ChangeValue(fast, 1f);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0002050A File Offset: 0x0001E70A
		public override void OnDecrement(bool fast)
		{
			this.ChangeValue(fast, -1f);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00020518 File Offset: 0x0001E718
		private void ChangeValue(bool fast, float multiplier)
		{
			float num = this.getter();
			num += this.incStepGetter() * (fast ? this.incStepMultGetter() : 1f) * multiplier;
			this.setter(num);
			this.UpdateValueLabel();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0002056C File Offset: 0x0001E76C
		private void UpdateValueLabel()
		{
			if (this.valueLabel != null)
			{
				this.valueLabel.text = this.getter().ToString("N" + this.decimalsGetter().ToString());
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000205C2 File Offset: 0x0001E7C2
		public DebugUIHandlerIndirectFloatField()
		{
		}

		// Token: 0x04000405 RID: 1029
		public Text nameLabel;

		// Token: 0x04000406 RID: 1030
		public Text valueLabel;

		// Token: 0x04000407 RID: 1031
		public Func<float> getter;

		// Token: 0x04000408 RID: 1032
		public Action<float> setter;

		// Token: 0x04000409 RID: 1033
		public Func<float> incStepGetter;

		// Token: 0x0400040A RID: 1034
		public Func<float> incStepMultGetter;

		// Token: 0x0400040B RID: 1035
		public Func<float> decimalsGetter;
	}
}
