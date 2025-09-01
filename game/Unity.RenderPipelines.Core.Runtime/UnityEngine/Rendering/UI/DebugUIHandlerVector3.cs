using System;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000104 RID: 260
	public class DebugUIHandlerVector3 : DebugUIHandlerWidget
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x000215D8 File Offset: 0x0001F7D8
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Vector3Field>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			this.fieldX.getter = (() => this.m_Field.GetValue().x);
			this.fieldX.setter = delegate(float v)
			{
				this.SetValue(v, true, false, false);
			};
			this.fieldX.nextUIHandler = this.fieldY;
			this.SetupSettings(this.fieldX);
			this.fieldY.getter = (() => this.m_Field.GetValue().y);
			this.fieldY.setter = delegate(float v)
			{
				this.SetValue(v, false, true, false);
			};
			this.fieldY.previousUIHandler = this.fieldX;
			this.fieldY.nextUIHandler = this.fieldZ;
			this.SetupSettings(this.fieldY);
			this.fieldZ.getter = (() => this.m_Field.GetValue().z);
			this.fieldZ.setter = delegate(float v)
			{
				this.SetValue(v, false, false, true);
			};
			this.fieldZ.previousUIHandler = this.fieldY;
			this.SetupSettings(this.fieldZ);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0002170C File Offset: 0x0001F90C
		private void SetValue(float v, bool x = false, bool y = false, bool z = false)
		{
			Vector3 value = this.m_Field.GetValue();
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
			this.m_Field.SetValue(value);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00021754 File Offset: 0x0001F954
		private void SetupSettings(DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = (() => this.m_Field.incStep);
			field.incStepMultGetter = (() => this.m_Field.incStepMult);
			field.decimalsGetter = (() => (float)this.m_Field.decimals);
			field.Init();
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000217A4 File Offset: 0x0001F9A4
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

		// Token: 0x060007AA RID: 1962 RVA: 0x0002181B File Offset: 0x0001FA1B
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002182E File Offset: 0x0001FA2E
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002183C File Offset: 0x0001FA3C
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002184A File Offset: 0x0001FA4A
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00021868 File Offset: 0x0001FA68
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

		// Token: 0x060007AF RID: 1967 RVA: 0x000218B4 File Offset: 0x0001FAB4
		public DebugUIHandlerVector3()
		{
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000218BC File Offset: 0x0001FABC
		[CompilerGenerated]
		private float <SetWidget>b__7_0()
		{
			return this.m_Field.GetValue().x;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000218CE File Offset: 0x0001FACE
		[CompilerGenerated]
		private void <SetWidget>b__7_1(float v)
		{
			this.SetValue(v, true, false, false);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000218DA File Offset: 0x0001FADA
		[CompilerGenerated]
		private float <SetWidget>b__7_2()
		{
			return this.m_Field.GetValue().y;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000218EC File Offset: 0x0001FAEC
		[CompilerGenerated]
		private void <SetWidget>b__7_3(float v)
		{
			this.SetValue(v, false, true, false);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000218F8 File Offset: 0x0001FAF8
		[CompilerGenerated]
		private float <SetWidget>b__7_4()
		{
			return this.m_Field.GetValue().z;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002190A File Offset: 0x0001FB0A
		[CompilerGenerated]
		private void <SetWidget>b__7_5(float v)
		{
			this.SetValue(v, false, false, true);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00021916 File Offset: 0x0001FB16
		[CompilerGenerated]
		private float <SetupSettings>b__9_0()
		{
			return this.m_Field.incStep;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00021923 File Offset: 0x0001FB23
		[CompilerGenerated]
		private float <SetupSettings>b__9_1()
		{
			return this.m_Field.incStepMult;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00021930 File Offset: 0x0001FB30
		[CompilerGenerated]
		private float <SetupSettings>b__9_2()
		{
			return (float)this.m_Field.decimals;
		}

		// Token: 0x0400043C RID: 1084
		public Text nameLabel;

		// Token: 0x0400043D RID: 1085
		public UIFoldout valueToggle;

		// Token: 0x0400043E RID: 1086
		public DebugUIHandlerIndirectFloatField fieldX;

		// Token: 0x0400043F RID: 1087
		public DebugUIHandlerIndirectFloatField fieldY;

		// Token: 0x04000440 RID: 1088
		public DebugUIHandlerIndirectFloatField fieldZ;

		// Token: 0x04000441 RID: 1089
		private DebugUI.Vector3Field m_Field;

		// Token: 0x04000442 RID: 1090
		private DebugUIHandlerContainer m_Container;
	}
}
