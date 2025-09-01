using System;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000103 RID: 259
	public class DebugUIHandlerVector2 : DebugUIHandlerWidget
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x000212F8 File Offset: 0x0001F4F8
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector2Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = (() => this.m_Field.GetValue().x);
			this.fieldX.setter = delegate(float x)
			{
				this.SetValue(x, true, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = (() => this.m_Field.GetValue().y);
			this.fieldY.setter = delegate(float x)
			{
				this.SetValue(x, false, true);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.SetupSettings(this.fieldY);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000213D0 File Offset: 0x0001F5D0
		private void SetValue(float v, bool x = false, bool y = false)
		{
			Vector2 value = this.m_Field.GetValue();
			if (x)
			{
				value.x = v;
			}
			if (y)
			{
				value.y = v;
			}
			this.m_Field.SetValue(value);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0002140C File Offset: 0x0001F60C
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = (() => this.m_Field.incStep);
			field.incStepMultGetter = (() => this.m_Field.incStepMult);
			field.decimalsGetter = (() => (float)this.m_Field.decimals);
			field.Init();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0002145C File Offset: 0x0001F65C
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

		// Token: 0x06000799 RID: 1945 RVA: 0x000214D3 File Offset: 0x0001F6D3
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000214E6 File Offset: 0x0001F6E6
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000214F4 File Offset: 0x0001F6F4
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00021502 File Offset: 0x0001F702
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00021520 File Offset: 0x0001F720
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

		// Token: 0x0600079E RID: 1950 RVA: 0x0002156C File Offset: 0x0001F76C
		public DebugUIHandlerVector2()
		{
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00021574 File Offset: 0x0001F774
		[CompilerGenerated]
		private float <SetWidget>b__6_0()
		{
			return this.m_Field.GetValue().x;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00021586 File Offset: 0x0001F786
		[CompilerGenerated]
		private void <SetWidget>b__6_1(float x)
		{
			this.SetValue(x, true, false);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00021591 File Offset: 0x0001F791
		[CompilerGenerated]
		private float <SetWidget>b__6_2()
		{
			return this.m_Field.GetValue().y;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000215A3 File Offset: 0x0001F7A3
		[CompilerGenerated]
		private void <SetWidget>b__6_3(float x)
		{
			this.SetValue(x, false, true);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000215AE File Offset: 0x0001F7AE
		[CompilerGenerated]
		private float <SetupSettings>b__8_0()
		{
			return this.m_Field.incStep;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000215BB File Offset: 0x0001F7BB
		[CompilerGenerated]
		private float <SetupSettings>b__8_1()
		{
			return this.m_Field.incStepMult;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000215C8 File Offset: 0x0001F7C8
		[CompilerGenerated]
		private float <SetupSettings>b__8_2()
		{
			return (float)this.m_Field.decimals;
		}

		// Token: 0x04000436 RID: 1078
		public Text nameLabel;

		// Token: 0x04000437 RID: 1079
		public UIFoldout valueToggle;

		// Token: 0x04000438 RID: 1080
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x04000439 RID: 1081
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x0400043A RID: 1082
		private DebugUI.Vector2Field m_Field;

		// Token: 0x0400043B RID: 1083
		private DebugUIHandlerContainer m_Container;
	}
}
