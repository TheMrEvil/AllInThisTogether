using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000162 RID: 354
	public class RadioButton : BaseBoolField, IGroupBoxOption
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002E882 File Offset: 0x0002CA82
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x0002E88C File Offset: 0x0002CA8C
		public override bool value
		{
			get
			{
				return base.value;
			}
			set
			{
				bool flag = base.value != value;
				if (flag)
				{
					base.value = value;
					this.UpdateCheckmark();
					if (value)
					{
						this.OnOptionSelected<RadioButton>();
					}
				}
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002E8C9 File Offset: 0x0002CAC9
		public RadioButton() : this(null)
		{
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0002E8D4 File Offset: 0x0002CAD4
		public RadioButton(string label) : base(label)
		{
			base.AddToClassList(RadioButton.ussClassName);
			base.visualInput.AddToClassList(RadioButton.inputUssClassName);
			base.labelElement.AddToClassList(RadioButton.labelUssClassName);
			this.m_CheckMark.RemoveFromHierarchy();
			this.m_CheckmarkBackground = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_CheckmarkBackground.Add(this.m_CheckMark);
			this.m_CheckmarkBackground.AddToClassList(RadioButton.checkmarkBackgroundUssClassName);
			this.m_CheckMark.AddToClassList(RadioButton.checkmarkUssClassName);
			base.visualInput.Add(this.m_CheckmarkBackground);
			this.UpdateCheckmark();
			this.RegisterGroupBoxOptionCallbacks<RadioButton>();
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0002E98B File Offset: 0x0002CB8B
		protected override void InitLabel()
		{
			base.InitLabel();
			this.m_Label.AddToClassList(RadioButton.textUssClassName);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0002E9A8 File Offset: 0x0002CBA8
		protected override void ToggleValue()
		{
			bool flag = !this.value;
			if (flag)
			{
				this.value = true;
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0002E9CD File Offset: 0x0002CBCD
		public void SetSelected(bool selected)
		{
			this.value = selected;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002E9D8 File Offset: 0x0002CBD8
		public override void SetValueWithoutNotify(bool newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.UpdateCheckmark();
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002E9EA File Offset: 0x0002CBEA
		private void UpdateCheckmark()
		{
			this.m_CheckMark.style.display = (this.value ? DisplayStyle.Flex : DisplayStyle.None);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002EA10 File Offset: 0x0002CC10
		protected override void UpdateMixedValueContent()
		{
			base.UpdateMixedValueContent();
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.m_CheckmarkBackground.RemoveFromHierarchy();
			}
			else
			{
				this.m_CheckmarkBackground.Add(this.m_CheckMark);
				base.visualInput.Add(this.m_CheckmarkBackground);
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002EA68 File Offset: 0x0002CC68
		// Note: this type is marked as 'beforefieldinit'.
		static RadioButton()
		{
		}

		// Token: 0x0400052D RID: 1325
		public new static readonly string ussClassName = "unity-radio-button";

		// Token: 0x0400052E RID: 1326
		public new static readonly string labelUssClassName = RadioButton.ussClassName + "__label";

		// Token: 0x0400052F RID: 1327
		public new static readonly string inputUssClassName = RadioButton.ussClassName + "__input";

		// Token: 0x04000530 RID: 1328
		public static readonly string checkmarkBackgroundUssClassName = RadioButton.ussClassName + "__checkmark-background";

		// Token: 0x04000531 RID: 1329
		public static readonly string checkmarkUssClassName = RadioButton.ussClassName + "__checkmark";

		// Token: 0x04000532 RID: 1330
		public static readonly string textUssClassName = RadioButton.ussClassName + "__text";

		// Token: 0x04000533 RID: 1331
		private VisualElement m_CheckmarkBackground;

		// Token: 0x02000163 RID: 355
		public new class UxmlFactory : UxmlFactory<RadioButton, RadioButton.UxmlTraits>
		{
			// Token: 0x06000B5F RID: 2911 RVA: 0x0002EAE3 File Offset: 0x0002CCE3
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000164 RID: 356
		public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription>
		{
			// Token: 0x06000B60 RID: 2912 RVA: 0x0002EAEC File Offset: 0x0002CCEC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((RadioButton)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000B61 RID: 2913 RVA: 0x0002EB12 File Offset: 0x0002CD12
			public UxmlTraits()
			{
			}

			// Token: 0x04000534 RID: 1332
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
