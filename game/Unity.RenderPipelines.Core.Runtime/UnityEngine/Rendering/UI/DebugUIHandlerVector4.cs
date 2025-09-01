using System;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000105 RID: 261
	public class DebugUIHandlerVector4 : DebugUIHandlerWidget
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x00021940 File Offset: 0x0001FB40
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector4Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = (() => this.m_Field.GetValue().x);
			this.fieldX.setter = delegate(float x)
			{
				this.SetValue(x, true, false, false, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = (() => this.m_Field.GetValue().y);
			this.fieldY.setter = delegate(float x)
			{
				this.SetValue(x, false, true, false, false);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.fieldY.nextUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldY);
			this.fieldZ.getter = (() => this.m_Field.GetValue().z);
			this.fieldZ.setter = delegate(float x)
			{
				this.SetValue(x, false, false, true, false);
			};
			this.fieldZ.previousUIHandler = this.fieldY;
			this.fieldZ.nextUIHandler = this.fieldW;
			this.SetupSettings(this.fieldZ);
			this.fieldW.getter = (() => this.m_Field.GetValue().w);
			this.fieldW.setter = delegate(float x)
			{
				this.SetValue(x, false, false, false, true);
			};
			this.fieldW.previousUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldW);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00021AD0 File Offset: 0x0001FCD0
		private void SetValue(float v, bool x = false, bool y = false, bool z = false, bool w = false)
		{
			Vector4 value = this.m_Field.GetValue();
			if (x)
			{
				value.x = v;
			}
			if (y)
			{
				value.y = v;
			}
			if (z)
			{
				value.z = v;
			}
			if (w)
			{
				value.w = v;
			}
			this.m_Field.SetValue(value);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00021B24 File Offset: 0x0001FD24
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = (() => this.m_Field.incStep);
			field.incStepMultGetter = (() => this.m_Field.incStepMult);
			field.decimalsGetter = (() => (float)this.m_Field.decimals);
			field.Init();
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00021B74 File Offset: 0x0001FD74
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

		// Token: 0x060007BD RID: 1981 RVA: 0x00021BEB File Offset: 0x0001FDEB
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00021BFE File Offset: 0x0001FDFE
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00021C0C File Offset: 0x0001FE0C
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00021C1A File Offset: 0x0001FE1A
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00021C38 File Offset: 0x0001FE38
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

		// Token: 0x060007C2 RID: 1986 RVA: 0x00021C84 File Offset: 0x0001FE84
		public DebugUIHandlerVector4()
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00021C8C File Offset: 0x0001FE8C
		[CompilerGenerated]
		private float <SetWidget>b__8_0()
		{
			return this.m_Field.GetValue().x;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00021C9E File Offset: 0x0001FE9E
		[CompilerGenerated]
		private void <SetWidget>b__8_1(float x)
		{
			this.SetValue(x, true, false, false, false);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00021CAB File Offset: 0x0001FEAB
		[CompilerGenerated]
		private float <SetWidget>b__8_2()
		{
			return this.m_Field.GetValue().y;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00021CBD File Offset: 0x0001FEBD
		[CompilerGenerated]
		private void <SetWidget>b__8_3(float x)
		{
			this.SetValue(x, false, true, false, false);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00021CCA File Offset: 0x0001FECA
		[CompilerGenerated]
		private float <SetWidget>b__8_4()
		{
			return this.m_Field.GetValue().z;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00021CDC File Offset: 0x0001FEDC
		[CompilerGenerated]
		private void <SetWidget>b__8_5(float x)
		{
			this.SetValue(x, false, false, true, false);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00021CE9 File Offset: 0x0001FEE9
		[CompilerGenerated]
		private float <SetWidget>b__8_6()
		{
			return this.m_Field.GetValue().w;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00021CFB File Offset: 0x0001FEFB
		[CompilerGenerated]
		private void <SetWidget>b__8_7(float x)
		{
			this.SetValue(x, false, false, false, true);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00021D08 File Offset: 0x0001FF08
		[CompilerGenerated]
		private float <SetupSettings>b__10_0()
		{
			return this.m_Field.incStep;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00021D15 File Offset: 0x0001FF15
		[CompilerGenerated]
		private float <SetupSettings>b__10_1()
		{
			return this.m_Field.incStepMult;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00021D22 File Offset: 0x0001FF22
		[CompilerGenerated]
		private float <SetupSettings>b__10_2()
		{
			return (float)this.m_Field.decimals;
		}

		// Token: 0x04000443 RID: 1091
		public Text nameLabel;

		// Token: 0x04000444 RID: 1092
		public UIFoldout valueToggle;

		// Token: 0x04000445 RID: 1093
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x04000446 RID: 1094
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000447 RID: 1095
		public DebugUIHandlerIndirectFloatField fieldZ;

		// Token: 0x04000448 RID: 1096
		public DebugUIHandlerIndirectFloatField fieldW;

		// Token: 0x04000449 RID: 1097
		private DebugUI.Vector4Field m_Field;

		// Token: 0x0400044A RID: 1098
		private DebugUIHandlerContainer m_Container;
	}
}
