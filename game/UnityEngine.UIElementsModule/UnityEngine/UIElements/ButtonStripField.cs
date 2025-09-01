using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200012F RID: 303
	internal class ButtonStripField : BaseField<int>
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00028890 File Offset: 0x00026A90
		private List<Button> buttons
		{
			get
			{
				this.m_Buttons.Clear();
				this.Query(null, null).ToList(this.m_Buttons);
				return this.m_Buttons;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000288CC File Offset: 0x00026ACC
		public void AddButton(string text, string name = "")
		{
			Button button = this.CreateButton(name);
			button.text = text;
			base.Add(button);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000288F4 File Offset: 0x00026AF4
		public void AddButton(Background icon, string name = "")
		{
			Button button = this.CreateButton(name);
			VisualElement visualElement = new VisualElement();
			visualElement.AddToClassList("unity-button-strip-field__button-icon");
			visualElement.style.backgroundImage = icon;
			button.Add(visualElement);
			base.Add(button);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00028940 File Offset: 0x00026B40
		private Button CreateButton(string name)
		{
			Button button = new Button
			{
				name = name
			};
			button.AddToClassList("unity-button-strip-field__button");
			button.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnButtonDetachFromPanel), TrickleDown.NoTrickleDown);
			button.clicked += delegate()
			{
				this.value = this.buttons.IndexOf(button);
			};
			base.Add(button);
			this.RefreshButtonsStyling();
			return button;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000289D0 File Offset: 0x00026BD0
		private void OnButtonDetachFromPanel(DetachFromPanelEvent evt)
		{
			VisualElement visualElement = evt.currentTarget as VisualElement;
			ButtonStripField buttonStripField;
			bool flag;
			if (visualElement != null)
			{
				buttonStripField = (visualElement.parent as ButtonStripField);
				flag = (buttonStripField != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				buttonStripField.RefreshButtonsStyling();
				buttonStripField.EnsureValueIsValid();
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00028A14 File Offset: 0x00026C14
		private void RefreshButtonsStyling()
		{
			for (int i = 0; i < this.buttons.Count; i++)
			{
				Button button = this.m_Buttons[i];
				bool flag = this.m_Buttons.Count == 1;
				bool flag2 = i == 0;
				bool flag3 = i == this.m_Buttons.Count - 1;
				button.EnableInClassList("unity-button-strip-field__button--alone", flag);
				button.EnableInClassList("unity-button-strip-field__button--left", !flag && flag2);
				button.EnableInClassList("unity-button-strip-field__button--right", !flag && flag3);
				button.EnableInClassList("unity-button-strip-field__button--middle", !flag && !flag2 && !flag3);
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00028AC5 File Offset: 0x00026CC5
		public ButtonStripField() : base(null)
		{
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00028ADB File Offset: 0x00026CDB
		public ButtonStripField(string label) : base(label)
		{
			base.AddToClassList("unity-button-strip-field");
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00028AFD File Offset: 0x00026CFD
		public override void SetValueWithoutNotify(int newValue)
		{
			newValue = Mathf.Clamp(newValue, 0, this.buttons.Count - 1);
			base.SetValueWithoutNotify(newValue);
			this.RefreshButtonsState();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00028B25 File Offset: 0x00026D25
		private void EnsureValueIsValid()
		{
			this.SetValueWithoutNotify(Mathf.Clamp(this.value, 0, this.buttons.Count - 1));
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00028B48 File Offset: 0x00026D48
		private void RefreshButtonsState()
		{
			for (int i = 0; i < this.buttons.Count; i++)
			{
				bool flag = i == this.value;
				if (flag)
				{
					this.m_Buttons[i].pseudoStates |= PseudoStates.Checked;
				}
				else
				{
					this.m_Buttons[i].pseudoStates &= ~PseudoStates.Checked;
				}
			}
		}

		// Token: 0x04000451 RID: 1105
		public const string className = "unity-button-strip-field";

		// Token: 0x04000452 RID: 1106
		private const string k_ButtonClass = "unity-button-strip-field__button";

		// Token: 0x04000453 RID: 1107
		private const string k_IconClass = "unity-button-strip-field__button-icon";

		// Token: 0x04000454 RID: 1108
		private const string k_ButtonLeftClass = "unity-button-strip-field__button--left";

		// Token: 0x04000455 RID: 1109
		private const string k_ButtonMiddleClass = "unity-button-strip-field__button--middle";

		// Token: 0x04000456 RID: 1110
		private const string k_ButtonRightClass = "unity-button-strip-field__button--right";

		// Token: 0x04000457 RID: 1111
		private const string k_ButtonAloneClass = "unity-button-strip-field__button--alone";

		// Token: 0x04000458 RID: 1112
		private List<Button> m_Buttons = new List<Button>();

		// Token: 0x02000130 RID: 304
		public new class UxmlFactory : UxmlFactory<ButtonStripField, ButtonStripField.UxmlTraits>
		{
			// Token: 0x06000A31 RID: 2609 RVA: 0x00028BB6 File Offset: 0x00026DB6
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000131 RID: 305
		public new class UxmlTraits : BaseField<int>.UxmlTraits
		{
			// Token: 0x06000A32 RID: 2610 RVA: 0x00028BBF File Offset: 0x00026DBF
			public UxmlTraits()
			{
			}
		}

		// Token: 0x02000132 RID: 306
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06000A33 RID: 2611 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x00028BC8 File Offset: 0x00026DC8
			internal void <CreateButton>b__0()
			{
				this.<>4__this.value = this.<>4__this.buttons.IndexOf(this.button);
			}

			// Token: 0x04000459 RID: 1113
			public ButtonStripField <>4__this;

			// Token: 0x0400045A RID: 1114
			public Button button;
		}
	}
}
