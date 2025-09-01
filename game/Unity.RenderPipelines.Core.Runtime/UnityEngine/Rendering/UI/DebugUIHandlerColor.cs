using System;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000EF RID: 239
	public class DebugUIHandlerColor : DebugUIHandlerWidget
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0001F520 File Offset: 0x0001D720
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.ColorField>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldR.getter = (() => this.m_Field.GetValue().r);
			this.fieldR.setter = delegate(float x)
			{
				this.SetValue(x, true, false, false, false);
			};
			this.fieldR.nextUIHandler = this.fieldG;
			this.SetupSettings(this.fieldR);
			this.fieldG.getter = (() => this.m_Field.GetValue().g);
			this.fieldG.setter = delegate(float x)
			{
				this.SetValue(x, false, true, false, false);
			};
			this.fieldG.previousUIHandler = this.fieldR;
			this.fieldG.nextUIHandler = this.fieldB;
			this.SetupSettings(this.fieldG);
			this.fieldB.getter = (() => this.m_Field.GetValue().b);
			this.fieldB.setter = delegate(float x)
			{
				this.SetValue(x, false, false, true, false);
			};
			this.fieldB.previousUIHandler = this.fieldG;
			this.fieldB.nextUIHandler = (this.m_Field.showAlpha ? this.fieldA : null);
			this.SetupSettings(this.fieldB);
			this.fieldA.gameObject.SetActive(this.m_Field.showAlpha);
			this.fieldA.getter = (() => this.m_Field.GetValue().a);
			this.fieldA.setter = delegate(float x)
			{
				this.SetValue(x, false, false, false, true);
			};
			this.fieldA.previousUIHandler = this.fieldB;
			this.SetupSettings(this.fieldA);
			this.UpdateColor();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
		private void SetValue(float x, bool r = false, bool g = false, bool b = false, bool a = false)
		{
			Color value = this.m_Field.GetValue();
			if (r)
			{
				value.r = x;
			}
			if (g)
			{
				value.g = x;
			}
			if (b)
			{
				value.b = x;
			}
			if (a)
			{
				value.a = x;
			}
			this.m_Field.SetValue(value);
			this.UpdateColor();
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001F740 File Offset: 0x0001D940
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = (() => this.m_Field.incStep);
			field.incStepMultGetter = (() => this.m_Field.incStepMult);
			field.decimalsGetter = (() => (float)this.m_Field.decimals);
			field.Init();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001F790 File Offset: 0x0001D990
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

		// Token: 0x0600070F RID: 1807 RVA: 0x0001F807 File Offset: 0x0001DA07
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001F81A File Offset: 0x0001DA1A
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001F828 File Offset: 0x0001DA28
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001F836 File Offset: 0x0001DA36
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001F851 File Offset: 0x0001DA51
		internal void UpdateColor()
		{
			if (this.colorImage != null)
			{
				this.colorImage.color = this.m_Field.GetValue();
			}
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001F878 File Offset: 0x0001DA78
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

		// Token: 0x06000715 RID: 1813 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		public DebugUIHandlerColor()
		{
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001F8CC File Offset: 0x0001DACC
		[CompilerGenerated]
		private float <SetWidget>b__9_0()
		{
			return this.m_Field.GetValue().r;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001F8DE File Offset: 0x0001DADE
		[CompilerGenerated]
		private void <SetWidget>b__9_1(float x)
		{
			this.SetValue(x, true, false, false, false);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001F8EB File Offset: 0x0001DAEB
		[CompilerGenerated]
		private float <SetWidget>b__9_2()
		{
			return this.m_Field.GetValue().g;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001F8FD File Offset: 0x0001DAFD
		[CompilerGenerated]
		private void <SetWidget>b__9_3(float x)
		{
			this.SetValue(x, false, true, false, false);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001F90A File Offset: 0x0001DB0A
		[CompilerGenerated]
		private float <SetWidget>b__9_4()
		{
			return this.m_Field.GetValue().b;
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001F91C File Offset: 0x0001DB1C
		[CompilerGenerated]
		private void <SetWidget>b__9_5(float x)
		{
			this.SetValue(x, false, false, true, false);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001F929 File Offset: 0x0001DB29
		[CompilerGenerated]
		private float <SetWidget>b__9_6()
		{
			return this.m_Field.GetValue().a;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001F93B File Offset: 0x0001DB3B
		[CompilerGenerated]
		private void <SetWidget>b__9_7(float x)
		{
			this.SetValue(x, false, false, false, true);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001F948 File Offset: 0x0001DB48
		[CompilerGenerated]
		private float <SetupSettings>b__11_0()
		{
			return this.m_Field.incStep;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001F955 File Offset: 0x0001DB55
		[CompilerGenerated]
		private float <SetupSettings>b__11_1()
		{
			return this.m_Field.incStepMult;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001F962 File Offset: 0x0001DB62
		[CompilerGenerated]
		private float <SetupSettings>b__11_2()
		{
			return (float)this.m_Field.decimals;
		}

		// Token: 0x040003E6 RID: 998
		public Text nameLabel;

		// Token: 0x040003E7 RID: 999
		public UIFoldout valueToggle;

		// Token: 0x040003E8 RID: 1000
		public Image colorImage;

		// Token: 0x040003E9 RID: 1001
		public DebugUIHandlerIndirectFloatField fieldR;

		// Token: 0x040003EA RID: 1002
		public DebugUIHandlerIndirectFloatField fieldG;

		// Token: 0x040003EB RID: 1003
		public DebugUIHandlerIndirectFloatField fieldB;

		// Token: 0x040003EC RID: 1004
		public DebugUIHandlerIndirectFloatField fieldA;

		// Token: 0x040003ED RID: 1005
		private DebugUI.ColorField m_Field;

		// Token: 0x040003EE RID: 1006
		private DebugUIHandlerContainer m_Container;
	}
}
