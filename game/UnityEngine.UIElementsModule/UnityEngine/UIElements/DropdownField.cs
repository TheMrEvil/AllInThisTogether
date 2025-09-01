using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000133 RID: 307
	public class DropdownField : BaseField<string>
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00028BF0 File Offset: 0x00026DF0
		protected TextElement textElement
		{
			get
			{
				return this.m_TextElement;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00028C08 File Offset: 0x00026E08
		public string text
		{
			get
			{
				return this.m_TextElement.text;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00028C28 File Offset: 0x00026E28
		internal string GetValueToDisplay()
		{
			bool flag = this.m_FormatSelectedValueCallback != null;
			string result;
			if (flag)
			{
				result = this.m_FormatSelectedValueCallback(this.value);
			}
			else
			{
				result = (this.value ?? string.Empty);
			}
			return result;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00028C6C File Offset: 0x00026E6C
		internal string GetListItemToDisplay(string value)
		{
			bool flag = this.m_FormatListItemCallback != null;
			string result;
			if (flag)
			{
				result = this.m_FormatListItemCallback(value);
			}
			else
			{
				result = ((value != null && this.m_Choices.Contains(value)) ? value : string.Empty);
			}
			return result;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00028CB4 File Offset: 0x00026EB4
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00028CCC File Offset: 0x00026ECC
		internal virtual Func<string, string> formatSelectedValueCallback
		{
			get
			{
				return this.m_FormatSelectedValueCallback;
			}
			set
			{
				this.m_FormatSelectedValueCallback = value;
				this.textElement.text = this.GetValueToDisplay();
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00028CE8 File Offset: 0x00026EE8
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x00028D00 File Offset: 0x00026F00
		internal virtual Func<string, string> formatListItemCallback
		{
			get
			{
				return this.m_FormatListItemCallback;
			}
			set
			{
				this.m_FormatListItemCallback = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00028D0C File Offset: 0x00026F0C
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00028D24 File Offset: 0x00026F24
		public int index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				this.m_Index = value;
				bool flag = this.m_Choices == null || value >= this.m_Choices.Count || value < 0;
				if (flag)
				{
					this.value = null;
				}
				else
				{
					this.value = this.m_Choices[this.m_Index];
				}
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00028D7C File Offset: 0x00026F7C
		public DropdownField() : this(null)
		{
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00028D88 File Offset: 0x00026F88
		public DropdownField(string label) : base(label, null)
		{
			base.AddToClassList(DropdownField.ussClassNameBasePopupField);
			base.labelElement.AddToClassList(DropdownField.labelUssClassNameBasePopupField);
			this.m_TextElement = new DropdownField.PopupTextElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_TextElement.AddToClassList(DropdownField.textUssClassNameBasePopupField);
			base.visualInput.AddToClassList(DropdownField.inputUssClassNameBasePopupField);
			base.visualInput.Add(this.m_TextElement);
			this.m_ArrowElement = new VisualElement();
			this.m_ArrowElement.AddToClassList(DropdownField.arrowUssClassNameBasePopupField);
			this.m_ArrowElement.pickingMode = PickingMode.Ignore;
			base.visualInput.Add(this.m_ArrowElement);
			this.choices = new List<string>();
			base.AddToClassList(DropdownField.ussClassNamePopupField);
			base.labelElement.AddToClassList(DropdownField.labelUssClassNamePopupField);
			base.visualInput.AddToClassList(DropdownField.inputUssClassNamePopupField);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00028E86 File Offset: 0x00027086
		public DropdownField(List<string> choices, string defaultValue, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null) : this(null, choices, defaultValue, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00028E98 File Offset: 0x00027098
		public DropdownField(string label, List<string> choices, string defaultValue, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null) : this(label)
		{
			bool flag = defaultValue == null;
			if (flag)
			{
				throw new ArgumentNullException("defaultValue");
			}
			this.choices = choices;
			this.SetValueWithoutNotify(defaultValue);
			this.formatListItemCallback = formatListItemCallback;
			this.formatSelectedValueCallback = formatSelectedValueCallback;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00028EE3 File Offset: 0x000270E3
		public DropdownField(List<string> choices, int defaultIndex, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null) : this(null, choices, defaultIndex, formatSelectedValueCallback, formatListItemCallback)
		{
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00028EF3 File Offset: 0x000270F3
		public DropdownField(string label, List<string> choices, int defaultIndex, Func<string, string> formatSelectedValueCallback = null, Func<string, string> formatListItemCallback = null) : this(label)
		{
			this.choices = choices;
			this.index = defaultIndex;
			this.formatListItemCallback = formatListItemCallback;
			this.formatSelectedValueCallback = formatSelectedValueCallback;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00028F20 File Offset: 0x00027120
		internal void AddMenuItems(IGenericMenu menu)
		{
			bool flag = menu == null;
			if (flag)
			{
				throw new ArgumentNullException("menu");
			}
			bool flag2 = this.m_Choices == null;
			if (!flag2)
			{
				using (List<string>.Enumerator enumerator = this.m_Choices.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string item = enumerator.Current;
						bool isChecked = item == this.value;
						menu.AddItem(this.GetListItemToDisplay(item), isChecked, delegate()
						{
							this.ChangeValueFromMenu(item);
						});
					}
				}
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00028FDC File Offset: 0x000271DC
		private void ChangeValueFromMenu(string menuItem)
		{
			this.value = menuItem;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x00028FE7 File Offset: 0x000271E7
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x00028FEF File Offset: 0x000271EF
		public virtual List<string> choices
		{
			get
			{
				return this.m_Choices;
			}
			set
			{
				this.m_Choices = value;
				this.SetValueWithoutNotify(base.rawValue);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x00029008 File Offset: 0x00027208
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x00029020 File Offset: 0x00027220
		public override string value
		{
			get
			{
				return base.value;
			}
			set
			{
				List<string> choices = this.m_Choices;
				this.m_Index = ((choices != null) ? choices.IndexOf(value) : -1);
				base.value = value;
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00029044 File Offset: 0x00027244
		public override void SetValueWithoutNotify(string newValue)
		{
			List<string> choices = this.m_Choices;
			this.m_Index = ((choices != null) ? choices.IndexOf(newValue) : -1);
			base.SetValueWithoutNotify(newValue);
			((INotifyValueChanged<string>)this.m_TextElement).SetValueWithoutNotify(this.GetValueToDisplay());
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002907C File Offset: 0x0002727C
		protected override void ExecuteDefaultActionAtTarget(EventBase evt)
		{
			base.ExecuteDefaultActionAtTarget(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = false;
				KeyDownEvent keyDownEvent = evt as KeyDownEvent;
				bool flag3 = keyDownEvent != null;
				if (flag3)
				{
					bool flag4 = keyDownEvent.keyCode == KeyCode.Space || keyDownEvent.keyCode == KeyCode.KeypadEnter || keyDownEvent.keyCode == KeyCode.Return;
					if (flag4)
					{
						flag2 = true;
					}
				}
				else
				{
					MouseDownEvent mouseDownEvent = evt as MouseDownEvent;
					bool flag5 = mouseDownEvent != null && mouseDownEvent.button == 0;
					if (flag5)
					{
						MouseDownEvent mouseDownEvent2 = (MouseDownEvent)evt;
						bool flag6 = base.visualInput.ContainsPoint(base.visualInput.WorldToLocal(mouseDownEvent2.mousePosition));
						if (flag6)
						{
							flag2 = true;
						}
					}
				}
				bool flag7 = flag2;
				if (flag7)
				{
					this.ShowMenu();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00029148 File Offset: 0x00027348
		private void ShowMenu()
		{
			bool flag = this.createMenuCallback != null;
			IGenericMenu genericMenu;
			if (flag)
			{
				genericMenu = this.createMenuCallback();
			}
			else
			{
				BaseVisualElementPanel elementPanel = base.elementPanel;
				IGenericMenu genericMenu2;
				if (elementPanel == null || elementPanel.contextType != ContextType.Player)
				{
					genericMenu2 = DropdownUtility.CreateDropdown();
				}
				else
				{
					IGenericMenu genericMenu3 = new GenericDropdownMenu();
					genericMenu2 = genericMenu3;
				}
				genericMenu = genericMenu2;
			}
			this.AddMenuItems(genericMenu);
			genericMenu.DropDown(base.visualInput.worldBound, this, true);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000291B8 File Offset: 0x000273B8
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.textElement.text = BaseField<string>.mixedValueString;
			}
			this.textElement.EnableInClassList(BaseField<string>.mixedValueLabelUssClassName, base.showMixedValue);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000291FC File Offset: 0x000273FC
		// Note: this type is marked as 'beforefieldinit'.
		static DropdownField()
		{
		}

		// Token: 0x0400045B RID: 1115
		internal List<string> m_Choices;

		// Token: 0x0400045C RID: 1116
		private TextElement m_TextElement;

		// Token: 0x0400045D RID: 1117
		private VisualElement m_ArrowElement;

		// Token: 0x0400045E RID: 1118
		internal Func<string, string> m_FormatSelectedValueCallback;

		// Token: 0x0400045F RID: 1119
		internal Func<string, string> m_FormatListItemCallback;

		// Token: 0x04000460 RID: 1120
		internal Func<IGenericMenu> createMenuCallback = null;

		// Token: 0x04000461 RID: 1121
		private int m_Index = -1;

		// Token: 0x04000462 RID: 1122
		internal static readonly string ussClassNameBasePopupField = "unity-base-popup-field";

		// Token: 0x04000463 RID: 1123
		internal static readonly string textUssClassNameBasePopupField = DropdownField.ussClassNameBasePopupField + "__text";

		// Token: 0x04000464 RID: 1124
		internal static readonly string arrowUssClassNameBasePopupField = DropdownField.ussClassNameBasePopupField + "__arrow";

		// Token: 0x04000465 RID: 1125
		internal static readonly string labelUssClassNameBasePopupField = DropdownField.ussClassNameBasePopupField + "__label";

		// Token: 0x04000466 RID: 1126
		internal static readonly string inputUssClassNameBasePopupField = DropdownField.ussClassNameBasePopupField + "__input";

		// Token: 0x04000467 RID: 1127
		internal static readonly string ussClassNamePopupField = "unity-popup-field";

		// Token: 0x04000468 RID: 1128
		internal static readonly string labelUssClassNamePopupField = DropdownField.ussClassNamePopupField + "__label";

		// Token: 0x04000469 RID: 1129
		internal static readonly string inputUssClassNamePopupField = DropdownField.ussClassNamePopupField + "__input";

		// Token: 0x02000134 RID: 308
		public new class UxmlFactory : UxmlFactory<DropdownField, DropdownField.UxmlTraits>
		{
			// Token: 0x06000A50 RID: 2640 RVA: 0x00029295 File Offset: 0x00027495
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000135 RID: 309
		public new class UxmlTraits : BaseField<string>.UxmlTraits
		{
			// Token: 0x06000A51 RID: 2641 RVA: 0x000292A0 File Offset: 0x000274A0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				DropdownField dropdownField = (DropdownField)ve;
				dropdownField.choices = BaseField<string>.UxmlTraits.ParseChoiceList(this.m_Choices.GetValueFromBag(bag, cc));
				dropdownField.index = this.m_Index.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000A52 RID: 2642 RVA: 0x000292EC File Offset: 0x000274EC
			public UxmlTraits()
			{
			}

			// Token: 0x0400046A RID: 1130
			private UxmlIntAttributeDescription m_Index = new UxmlIntAttributeDescription
			{
				name = "index",
				defaultValue = -1
			};

			// Token: 0x0400046B RID: 1131
			private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
			{
				name = "choices"
			};
		}

		// Token: 0x02000136 RID: 310
		private class PopupTextElement : TextElement
		{
			// Token: 0x06000A53 RID: 2643 RVA: 0x0002932C File Offset: 0x0002752C
			protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
			{
				string text = this.text;
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					text = " ";
				}
				return base.MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
			}

			// Token: 0x06000A54 RID: 2644 RVA: 0x00029363 File Offset: 0x00027563
			public PopupTextElement()
			{
			}
		}

		// Token: 0x02000137 RID: 311
		[CompilerGenerated]
		private sealed class <>c__DisplayClass38_0
		{
			// Token: 0x06000A55 RID: 2645 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass38_0()
			{
			}

			// Token: 0x06000A56 RID: 2646 RVA: 0x0002936C File Offset: 0x0002756C
			internal void <AddMenuItems>b__0()
			{
				this.<>4__this.ChangeValueFromMenu(this.item);
			}

			// Token: 0x0400046C RID: 1132
			public string item;

			// Token: 0x0400046D RID: 1133
			public DropdownField <>4__this;
		}
	}
}
