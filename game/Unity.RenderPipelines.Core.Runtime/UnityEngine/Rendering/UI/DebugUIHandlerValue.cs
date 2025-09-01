using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000101 RID: 257
	public class DebugUIHandlerValue : DebugUIHandlerWidget
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x0002116E File Offset: 0x0001F36E
		protected override void OnEnable()
		{
			this.m_Timer = 0f;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0002117B File Offset: 0x0001F37B
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Value>();
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000211A6 File Offset: 0x0001F3A6
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000211CB File Offset: 0x0001F3CB
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000211F0 File Offset: 0x0001F3F0
		private void Update()
		{
			if (this.m_Timer >= this.m_Field.refreshRate)
			{
				this.valueLabel.text = this.m_Field.GetValue().ToString();
				this.m_Timer -= this.m_Field.refreshRate;
			}
			this.m_Timer += Time.deltaTime;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00021255 File Offset: 0x0001F455
		public DebugUIHandlerValue()
		{
		}

		// Token: 0x04000431 RID: 1073
		public Text nameLabel;

		// Token: 0x04000432 RID: 1074
		public Text valueLabel;

		// Token: 0x04000433 RID: 1075
		private DebugUI.Value m_Field;

		// Token: 0x04000434 RID: 1076
		private float m_Timer;
	}
}
