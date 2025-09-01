using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F1 RID: 241
	public class DebugUIHandlerEnumField : DebugUIHandlerWidget
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x0001FAAC File Offset: 0x0001DCAC
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.EnumField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001FAE0 File Offset: 0x0001DCE0
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (this.nextButtonText != null)
			{
				this.nextButtonText.color = this.colorSelected;
			}
			if (this.previousButtonText != null)
			{
				this.previousButtonText.color = this.colorSelected;
			}
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001FB50 File Offset: 0x0001DD50
		public override void OnDeselection()
		{
			if (this.nextButtonText != null)
			{
				this.nextButtonText.color = this.colorDefault;
			}
			if (this.previousButtonText != null)
			{
				this.previousButtonText.color = this.colorDefault;
			}
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001FBBD File Offset: 0x0001DDBD
		public override void OnAction()
		{
			this.OnIncrement(false);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		public override void OnIncrement(bool fast)
		{
			if (this.m_Field.enumValues.Length == 0)
			{
				return;
			}
			int[] enumValues = this.m_Field.enumValues;
			int num = this.m_Field.currentIndex;
			if (num == enumValues.Length - 1)
			{
				num = 0;
			}
			else if (fast)
			{
				int[] quickSeparators = this.m_Field.quickSeparators;
				if (quickSeparators == null)
				{
					this.m_Field.InitQuickSeparators();
					quickSeparators = this.m_Field.quickSeparators;
				}
				int num2 = 0;
				while (num2 < quickSeparators.Length && num + 1 > quickSeparators[num2])
				{
					num2++;
				}
				if (num2 == quickSeparators.Length)
				{
					num = 0;
				}
				else
				{
					num = quickSeparators[num2];
				}
			}
			else
			{
				num++;
			}
			this.m_Field.SetValue(enumValues[num]);
			this.m_Field.currentIndex = num;
			this.UpdateValueLabel();
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		public override void OnDecrement(bool fast)
		{
			if (this.m_Field.enumValues.Length == 0)
			{
				return;
			}
			int[] enumValues = this.m_Field.enumValues;
			int num = this.m_Field.currentIndex;
			if (num == 0)
			{
				if (fast)
				{
					int[] quickSeparators = this.m_Field.quickSeparators;
					if (quickSeparators == null)
					{
						this.m_Field.InitQuickSeparators();
						quickSeparators = this.m_Field.quickSeparators;
					}
					num = quickSeparators[quickSeparators.Length - 1];
				}
				else
				{
					num = enumValues.Length - 1;
				}
			}
			else if (fast)
			{
				int[] quickSeparators2 = this.m_Field.quickSeparators;
				if (quickSeparators2 == null)
				{
					this.m_Field.InitQuickSeparators();
					quickSeparators2 = this.m_Field.quickSeparators;
				}
				int num2 = quickSeparators2.Length - 1;
				while (num2 > 0 && num <= quickSeparators2[num2])
				{
					num2--;
				}
				num = quickSeparators2[num2];
			}
			else
			{
				num--;
			}
			this.m_Field.SetValue(enumValues[num]);
			this.m_Field.currentIndex = num;
			this.UpdateValueLabel();
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001FD60 File Offset: 0x0001DF60
		protected virtual void UpdateValueLabel()
		{
			int num = this.m_Field.currentIndex;
			if (num < 0)
			{
				num = 0;
			}
			string text = this.m_Field.enumNames[num].text;
			if (text.Length > 26)
			{
				text = text.Substring(0, 23) + "...";
			}
			this.valueLabel.text = text;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public DebugUIHandlerEnumField()
		{
		}

		// Token: 0x040003F0 RID: 1008
		public Text nextButtonText;

		// Token: 0x040003F1 RID: 1009
		public Text previousButtonText;

		// Token: 0x040003F2 RID: 1010
		public Text nameLabel;

		// Token: 0x040003F3 RID: 1011
		public Text valueLabel;

		// Token: 0x040003F4 RID: 1012
		protected internal DebugUI.EnumField m_Field;
	}
}
