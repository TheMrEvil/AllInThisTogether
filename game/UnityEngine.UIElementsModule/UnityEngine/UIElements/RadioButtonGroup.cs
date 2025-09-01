using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000165 RID: 357
	public class RadioButtonGroup : BaseField<int>, IGroupBox
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0002EB32 File Offset: 0x0002CD32
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		public IEnumerable<string> choices
		{
			get
			{
				return this.m_Choices;
			}
			set
			{
				this.m_Choices = value;
				foreach (RadioButton radioButton in this.m_RadioButtons)
				{
					radioButton.UnregisterValueChangedCallback(this.m_RadioButtonValueChangedCallback);
					radioButton.RemoveFromHierarchy();
				}
				this.m_RadioButtons.Clear();
				bool flag = this.m_Choices != null;
				if (flag)
				{
					foreach (string text in this.m_Choices)
					{
						RadioButton radioButton2 = new RadioButton
						{
							text = text
						};
						radioButton2.RegisterValueChangedCallback(this.m_RadioButtonValueChangedCallback);
						this.m_RadioButtons.Add(radioButton2);
						base.visualInput.Add(radioButton2);
					}
					this.UpdateRadioButtons();
				}
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002EC40 File Offset: 0x0002CE40
		public RadioButtonGroup() : this(null, null)
		{
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002EC4C File Offset: 0x0002CE4C
		public RadioButtonGroup(string label, List<string> radioButtonChoices = null) : base(label, null)
		{
			base.AddToClassList(RadioButtonGroup.ussClassName);
			this.m_RadioButtonValueChangedCallback = new EventCallback<ChangeEvent<bool>>(this.RadioButtonValueChangedCallback);
			this.choices = radioButtonChoices;
			this.value = -1;
			base.visualInput.focusable = false;
			base.delegatesFocus = true;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
		private void RadioButtonValueChangedCallback(ChangeEvent<bool> evt)
		{
			bool newValue = evt.newValue;
			if (newValue)
			{
				this.value = this.m_RadioButtons.IndexOf(evt.target as RadioButton);
				evt.StopPropagation();
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002ECF2 File Offset: 0x0002CEF2
		public override void SetValueWithoutNotify(int newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.UpdateRadioButtons();
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002ED04 File Offset: 0x0002CF04
		private void UpdateRadioButtons()
		{
			bool flag = this.value >= 0 && this.value < this.m_RadioButtons.Count;
			if (flag)
			{
				this.m_RadioButtons[this.value].value = true;
			}
			else
			{
				foreach (RadioButton radioButton in this.m_RadioButtons)
				{
					radioButton.value = false;
				}
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002EDA0 File Offset: 0x0002CFA0
		// Note: this type is marked as 'beforefieldinit'.
		static RadioButtonGroup()
		{
		}

		// Token: 0x04000535 RID: 1333
		public new static readonly string ussClassName = "unity-radio-button-group";

		// Token: 0x04000536 RID: 1334
		private IEnumerable<string> m_Choices;

		// Token: 0x04000537 RID: 1335
		private List<RadioButton> m_RadioButtons = new List<RadioButton>();

		// Token: 0x04000538 RID: 1336
		private EventCallback<ChangeEvent<bool>> m_RadioButtonValueChangedCallback;

		// Token: 0x02000166 RID: 358
		public new class UxmlFactory : UxmlFactory<RadioButtonGroup, RadioButtonGroup.UxmlTraits>
		{
			// Token: 0x06000B6A RID: 2922 RVA: 0x0002EDAC File Offset: 0x0002CFAC
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000167 RID: 359
		public new class UxmlTraits : BaseFieldTraits<int, UxmlIntAttributeDescription>
		{
			// Token: 0x06000B6B RID: 2923 RVA: 0x0002EDB8 File Offset: 0x0002CFB8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RadioButtonGroup radioButtonGroup = (RadioButtonGroup)ve;
				radioButtonGroup.choices = BaseField<int>.UxmlTraits.ParseChoiceList(this.m_Choices.GetValueFromBag(bag, cc));
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x0002EDF0 File Offset: 0x0002CFF0
			public UxmlTraits()
			{
			}

			// Token: 0x04000539 RID: 1337
			private UxmlStringAttributeDescription m_Choices = new UxmlStringAttributeDescription
			{
				name = "choices"
			};
		}
	}
}
