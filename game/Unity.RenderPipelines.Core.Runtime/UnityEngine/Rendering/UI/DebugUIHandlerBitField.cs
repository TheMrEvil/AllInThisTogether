using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000EB RID: 235
	public class DebugUIHandlerBitField : DebugUIHandlerWidget
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.BitField>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			int i = 0;
			foreach (GUIContent guicontent in this.m_Field.enumNames)
			{
				if (i < this.toggles.Count)
				{
					DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = this.toggles[i];
					debugUIHandlerIndirectToggle.getter = new Func<int, bool>(this.GetValue);
					debugUIHandlerIndirectToggle.setter = new Action<int, bool>(this.SetValue);
					debugUIHandlerIndirectToggle.nextUIHandler = ((i < this.m_Field.enumNames.Length - 1) ? this.toggles[i + 1] : null);
					debugUIHandlerIndirectToggle.previousUIHandler = ((i > 0) ? this.toggles[i - 1] : null);
					debugUIHandlerIndirectToggle.parentUIHandler = this;
					debugUIHandlerIndirectToggle.index = i;
					debugUIHandlerIndirectToggle.nameLabel.text = guicontent.text;
					debugUIHandlerIndirectToggle.Init();
					i++;
				}
			}
			while (i < this.toggles.Count)
			{
				CoreUtils.Destroy(this.toggles[i].gameObject);
				this.toggles[i] = null;
				i++;
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001E9F0 File Offset: 0x0001CBF0
		private bool GetValue(int index)
		{
			if (index == 0)
			{
				return false;
			}
			index--;
			return (Convert.ToInt32(this.m_Field.GetValue()) & 1 << index) != 0;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001EA18 File Offset: 0x0001CC18
		private void SetValue(int index, bool value)
		{
			if (index == 0)
			{
				this.m_Field.SetValue(Enum.ToObject(this.m_Field.enumType, 0));
				using (List<DebugUIHandlerIndirectToggle>.Enumerator enumerator = this.toggles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = enumerator.Current;
						if (debugUIHandlerIndirectToggle != null && debugUIHandlerIndirectToggle.getter != null)
						{
							debugUIHandlerIndirectToggle.UpdateValueLabel();
						}
					}
					return;
				}
			}
			int num = Convert.ToInt32(this.m_Field.GetValue());
			if (value)
			{
				num |= this.m_Field.enumValues[index];
			}
			else
			{
				num &= ~this.m_Field.enumValues[index];
			}
			this.m_Field.SetValue(Enum.ToObject(this.m_Field.enumType, num));
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001EB5F File Offset: 0x0001CD5F
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001EB72 File Offset: 0x0001CD72
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001EB80 File Offset: 0x0001CD80
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001EB8E File Offset: 0x0001CD8E
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		public override DebugUIHandlerWidget Next()
		{
			if (!this.valueToggle.isOn || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001EBF8 File Offset: 0x0001CDF8
		public DebugUIHandlerBitField()
		{
		}

		// Token: 0x040003D5 RID: 981
		public Text nameLabel;

		// Token: 0x040003D6 RID: 982
		public UIFoldout valueToggle;

		// Token: 0x040003D7 RID: 983
		public List<DebugUIHandlerIndirectToggle> toggles;

		// Token: 0x040003D8 RID: 984
		private DebugUI.BitField m_Field;

		// Token: 0x040003D9 RID: 985
		private DebugUIHandlerContainer m_Container;
	}
}
