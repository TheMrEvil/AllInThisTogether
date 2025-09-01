using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000181 RID: 385
	public class TextField : TextInputBaseField<string>
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00032BE3 File Offset: 0x00030DE3
		private TextField.TextInput textInput
		{
			get
			{
				return (TextField.TextInput)base.textInputBase;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x00032BF0 File Offset: 0x00030DF0
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x00032C0D File Offset: 0x00030E0D
		public bool multiline
		{
			get
			{
				return this.textInput.multiline;
			}
			set
			{
				this.textInput.multiline = value;
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00032C1D File Offset: 0x00030E1D
		public void SelectRange(int rangeCursorIndex, int selectionIndex)
		{
			this.textInput.SelectRange(rangeCursorIndex, selectionIndex);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00032C2E File Offset: 0x00030E2E
		public TextField() : this(null)
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00032C39 File Offset: 0x00030E39
		public TextField(int maxLength, bool multiline, bool isPasswordField, char maskChar) : this(null, maxLength, multiline, isPasswordField, maskChar)
		{
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00032C49 File Offset: 0x00030E49
		public TextField(string label) : this(label, -1, false, false, '*')
		{
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00032C5C File Offset: 0x00030E5C
		public TextField(string label, int maxLength, bool multiline, bool isPasswordField, char maskChar) : base(label, maxLength, maskChar, new TextField.TextInput())
		{
			base.AddToClassList(TextField.ussClassName);
			base.labelElement.AddToClassList(TextField.labelUssClassName);
			base.visualInput.AddToClassList(TextField.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.SetValueWithoutNotify("");
			this.multiline = multiline;
			base.isPasswordField = isPasswordField;
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00032CD0 File Offset: 0x00030ED0
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00032CE8 File Offset: 0x00030EE8
		public override string value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = value;
				base.text = base.rawValue;
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00032D00 File Offset: 0x00030F00
		public override void SetValueWithoutNotify(string newValue)
		{
			base.SetValueWithoutNotify(newValue);
			base.text = base.rawValue;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00032D18 File Offset: 0x00030F18
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			base.text = base.rawValue;
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0000A501 File Offset: 0x00008701
		protected override string ValueToString(string value)
		{
			return value;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0000A501 File Offset: 0x00008701
		protected override string StringToValue(string str)
		{
			return str;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00032D4A File Offset: 0x00030F4A
		// Note: this type is marked as 'beforefieldinit'.
		static TextField()
		{
		}

		// Token: 0x040005C9 RID: 1481
		public new static readonly string ussClassName = "unity-text-field";

		// Token: 0x040005CA RID: 1482
		public new static readonly string labelUssClassName = TextField.ussClassName + "__label";

		// Token: 0x040005CB RID: 1483
		public new static readonly string inputUssClassName = TextField.ussClassName + "__input";

		// Token: 0x02000182 RID: 386
		public new class UxmlFactory : UxmlFactory<TextField, TextField.UxmlTraits>
		{
			// Token: 0x06000C31 RID: 3121 RVA: 0x00032D7E File Offset: 0x00030F7E
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000183 RID: 387
		public new class UxmlTraits : TextInputBaseField<string>.UxmlTraits
		{
			// Token: 0x06000C32 RID: 3122 RVA: 0x00032D88 File Offset: 0x00030F88
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				TextField textField = (TextField)ve;
				textField.multiline = this.m_Multiline.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x06000C33 RID: 3123 RVA: 0x00032DBB File Offset: 0x00030FBB
			public UxmlTraits()
			{
			}

			// Token: 0x040005CC RID: 1484
			private UxmlBoolAttributeDescription m_Multiline = new UxmlBoolAttributeDescription
			{
				name = "multiline"
			};
		}

		// Token: 0x02000184 RID: 388
		private class TextInput : TextInputBaseField<string>.TextInputBase
		{
			// Token: 0x17000260 RID: 608
			// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00032DDB File Offset: 0x00030FDB
			private TextField parentTextField
			{
				get
				{
					return (TextField)base.parent;
				}
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00032DE8 File Offset: 0x00030FE8
			// (set) Token: 0x06000C36 RID: 3126 RVA: 0x00032E00 File Offset: 0x00031000
			public bool multiline
			{
				get
				{
					return this.m_Multiline;
				}
				set
				{
					this.m_Multiline = value;
					bool flag = !value;
					if (flag)
					{
						base.text = base.text.Replace("\n", "");
					}
					this.SetTextAlign();
				}
			}

			// Token: 0x06000C37 RID: 3127 RVA: 0x00032E40 File Offset: 0x00031040
			private void SetTextAlign()
			{
				bool multiline = this.m_Multiline;
				if (multiline)
				{
					base.RemoveFromClassList(TextInputBaseField<string>.singleLineInputUssClassName);
					base.AddToClassList(TextInputBaseField<string>.multilineInputUssClassName);
				}
				else
				{
					base.RemoveFromClassList(TextInputBaseField<string>.multilineInputUssClassName);
					base.AddToClassList(TextInputBaseField<string>.singleLineInputUssClassName);
				}
			}

			// Token: 0x17000262 RID: 610
			// (set) Token: 0x06000C38 RID: 3128 RVA: 0x00032E90 File Offset: 0x00031090
			public override bool isPasswordField
			{
				set
				{
					base.isPasswordField = value;
					if (value)
					{
						this.multiline = false;
					}
				}
			}

			// Token: 0x06000C39 RID: 3129 RVA: 0x00032EB4 File Offset: 0x000310B4
			protected override string StringToValue(string str)
			{
				return str;
			}

			// Token: 0x06000C3A RID: 3130 RVA: 0x00032EC8 File Offset: 0x000310C8
			public void SelectRange(int cursorIndex, int selectionIndex)
			{
				bool flag = base.editorEngine != null;
				if (flag)
				{
					base.editorEngine.cursorIndex = cursorIndex;
					base.editorEngine.selectIndex = selectionIndex;
				}
			}

			// Token: 0x06000C3B RID: 3131 RVA: 0x00032F00 File Offset: 0x00031100
			internal override void SyncTextEngine()
			{
				bool flag = this.parentTextField != null;
				if (flag)
				{
					base.editorEngine.multiline = this.multiline;
					base.editorEngine.isPasswordField = this.isPasswordField;
				}
				base.SyncTextEngine();
			}

			// Token: 0x06000C3C RID: 3132 RVA: 0x00032F48 File Offset: 0x00031148
			protected override void ExecuteDefaultActionAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionAtTarget(evt);
				bool flag = evt == null;
				if (!flag)
				{
					bool flag2 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
					if (flag2)
					{
						KeyDownEvent keyDownEvent = evt as KeyDownEvent;
						bool flag3 = !this.parentTextField.isDelayed || (!this.multiline && ((keyDownEvent != null && keyDownEvent.keyCode == KeyCode.KeypadEnter) || (keyDownEvent != null && keyDownEvent.keyCode == KeyCode.Return)));
						if (flag3)
						{
							this.parentTextField.value = base.text;
						}
						bool multiline = this.multiline;
						if (multiline)
						{
							char? c = (keyDownEvent != null) ? new char?(keyDownEvent.character) : null;
							int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
							int num2 = 9;
							bool flag4 = (num.GetValueOrDefault() == num2 & num != null) && keyDownEvent.modifiers == EventModifiers.None;
							if (flag4)
							{
								if (keyDownEvent != null)
								{
									keyDownEvent.StopPropagation();
								}
								if (keyDownEvent != null)
								{
									keyDownEvent.PreventDefault();
								}
							}
							else
							{
								c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : null);
								num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
								num2 = 3;
								bool flag5;
								if (!(num.GetValueOrDefault() == num2 & num != null) || keyDownEvent == null || !keyDownEvent.shiftKey)
								{
									c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : null);
									num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
									num2 = 10;
									flag5 = ((num.GetValueOrDefault() == num2 & num != null) && keyDownEvent != null && keyDownEvent.shiftKey);
								}
								else
								{
									flag5 = true;
								}
								bool flag6 = flag5;
								if (flag6)
								{
									base.parent.Focus();
									evt.StopPropagation();
									evt.PreventDefault();
								}
							}
						}
						else
						{
							char? c = (keyDownEvent != null) ? new char?(keyDownEvent.character) : null;
							int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
							int num2 = 3;
							bool flag7;
							if (!(num.GetValueOrDefault() == num2 & num != null))
							{
								c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : null);
								num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
								num2 = 10;
								flag7 = (num.GetValueOrDefault() == num2 & num != null);
							}
							else
							{
								flag7 = true;
							}
							bool flag8 = flag7;
							if (flag8)
							{
								base.parent.Focus();
								evt.StopPropagation();
								evt.PreventDefault();
							}
						}
					}
					else
					{
						bool flag9 = evt.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
						if (flag9)
						{
							ExecuteCommandEvent executeCommandEvent = evt as ExecuteCommandEvent;
							string commandName = executeCommandEvent.commandName;
							bool flag10 = !this.parentTextField.isDelayed && (commandName == "Paste" || commandName == "Cut");
							if (flag10)
							{
								this.parentTextField.value = base.text;
							}
						}
						else
						{
							bool flag11 = evt.eventTypeId == EventBase<NavigationSubmitEvent>.TypeId() || evt.eventTypeId == EventBase<NavigationCancelEvent>.TypeId() || evt.eventTypeId == EventBase<NavigationMoveEvent>.TypeId();
							if (flag11)
							{
								evt.StopPropagation();
								evt.PreventDefault();
							}
						}
					}
				}
			}

			// Token: 0x06000C3D RID: 3133 RVA: 0x000332F4 File Offset: 0x000314F4
			protected override void ExecuteDefaultAction(EventBase evt)
			{
				base.ExecuteDefaultAction(evt);
				bool flag;
				if (this.parentTextField.isDelayed)
				{
					long? num = (evt != null) ? new long?(evt.eventTypeId) : null;
					long num2 = EventBase<BlurEvent>.TypeId();
					flag = (num.GetValueOrDefault() == num2 & num != null);
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					this.parentTextField.value = base.text;
				}
			}

			// Token: 0x06000C3E RID: 3134 RVA: 0x00033364 File Offset: 0x00031564
			public TextInput()
			{
			}

			// Token: 0x040005CD RID: 1485
			private bool m_Multiline;
		}
	}
}
