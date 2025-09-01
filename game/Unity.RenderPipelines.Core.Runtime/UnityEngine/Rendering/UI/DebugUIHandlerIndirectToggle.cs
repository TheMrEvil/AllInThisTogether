using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F8 RID: 248
	public class DebugUIHandlerIndirectToggle : DebugUIHandlerWidget
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x000205CA File Offset: 0x0001E7CA
		public void Init()
		{
			this.UpdateValueLabel();
			this.valueToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000205EE File Offset: 0x0001E7EE
		private void OnToggleValueChanged(bool value)
		{
			this.setter(this.index, value);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00020602 File Offset: 0x0001E802
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			this.nameLabel.color = this.colorSelected;
			this.checkmarkImage.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00020627 File Offset: 0x0001E827
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
			this.checkmarkImage.color = this.colorDefault;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002064C File Offset: 0x0001E84C
		public override void OnAction()
		{
			bool arg = !this.getter(this.index);
			this.setter(this.index, arg);
			this.UpdateValueLabel();
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00020686 File Offset: 0x0001E886
		internal void UpdateValueLabel()
		{
			if (this.valueToggle != null)
			{
				this.valueToggle.isOn = this.getter(this.index);
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000206B2 File Offset: 0x0001E8B2
		public DebugUIHandlerIndirectToggle()
		{
		}

		// Token: 0x0400040C RID: 1036
		public Text nameLabel;

		// Token: 0x0400040D RID: 1037
		public Toggle valueToggle;

		// Token: 0x0400040E RID: 1038
		public Image checkmarkImage;

		// Token: 0x0400040F RID: 1039
		public Func<int, bool> getter;

		// Token: 0x04000410 RID: 1040
		public Action<int, bool> setter;

		// Token: 0x04000411 RID: 1041
		internal int index;
	}
}
