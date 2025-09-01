using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x02000186 RID: 390
	public abstract class TextInputBaseField<TValueType> : BaseField<TValueType>
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0003336D File Offset: 0x0003156D
		protected internal TextInputBaseField<TValueType>.TextInputBase textInputBase
		{
			get
			{
				return this.m_TextInputBase;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00033378 File Offset: 0x00031578
		internal TextHandle textHandle
		{
			get
			{
				return new TextHandle
				{
					textHandle = this.iTextHandle
				};
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x000333A0 File Offset: 0x000315A0
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x000333A8 File Offset: 0x000315A8
		internal ITextHandle iTextHandle
		{
			[CompilerGenerated]
			get
			{
				return this.<iTextHandle>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<iTextHandle>k__BackingField = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x000333B4 File Offset: 0x000315B4
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x000333D1 File Offset: 0x000315D1
		public string text
		{
			get
			{
				return this.m_TextInputBase.text;
			}
			protected internal set
			{
				this.m_TextInputBase.text = value;
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000C51 RID: 3153 RVA: 0x000333E4 File Offset: 0x000315E4
		// (remove) Token: 0x06000C52 RID: 3154 RVA: 0x0003341C File Offset: 0x0003161C
		protected event Action<bool> onIsReadOnlyChanged
		{
			[CompilerGenerated]
			add
			{
				Action<bool> action = this.onIsReadOnlyChanged;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.onIsReadOnlyChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<bool> action = this.onIsReadOnlyChanged;
				Action<bool> action2;
				do
				{
					action2 = action;
					Action<bool> value2 = (Action<bool>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<bool>>(ref this.onIsReadOnlyChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00033454 File Offset: 0x00031654
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00033471 File Offset: 0x00031671
		public bool isReadOnly
		{
			get
			{
				return this.m_TextInputBase.isReadOnly;
			}
			set
			{
				this.m_TextInputBase.isReadOnly = value;
				Action<bool> action = this.onIsReadOnlyChanged;
				if (action != null)
				{
					action(value);
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00033494 File Offset: 0x00031694
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x000334B4 File Offset: 0x000316B4
		public bool isPasswordField
		{
			get
			{
				return this.m_TextInputBase.isPasswordField;
			}
			set
			{
				bool flag = this.m_TextInputBase.isPasswordField == value;
				if (!flag)
				{
					this.m_TextInputBase.isPasswordField = value;
					this.m_TextInputBase.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000334F4 File Offset: 0x000316F4
		public Color selectionColor
		{
			get
			{
				return this.m_TextInputBase.selectionColor;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00033501 File Offset: 0x00031701
		public Color cursorColor
		{
			get
			{
				return this.m_TextInputBase.cursorColor;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0003350E File Offset: 0x0003170E
		public int cursorIndex
		{
			get
			{
				return this.m_TextInputBase.cursorIndex;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0003351B File Offset: 0x0003171B
		public int selectIndex
		{
			get
			{
				return this.m_TextInputBase.selectIndex;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x00033528 File Offset: 0x00031728
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x00033545 File Offset: 0x00031745
		public int maxLength
		{
			get
			{
				return this.m_TextInputBase.maxLength;
			}
			set
			{
				this.m_TextInputBase.maxLength = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00033558 File Offset: 0x00031758
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x00033575 File Offset: 0x00031775
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_TextInputBase.doubleClickSelectsWord;
			}
			set
			{
				this.m_TextInputBase.doubleClickSelectsWord = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00033588 File Offset: 0x00031788
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x000335A5 File Offset: 0x000317A5
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TextInputBase.tripleClickSelectsLine;
			}
			set
			{
				this.m_TextInputBase.tripleClickSelectsLine = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x000335B8 File Offset: 0x000317B8
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x000335D5 File Offset: 0x000317D5
		public bool isDelayed
		{
			get
			{
				return this.m_TextInputBase.isDelayed;
			}
			set
			{
				this.m_TextInputBase.isDelayed = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x000335E8 File Offset: 0x000317E8
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x00033605 File Offset: 0x00031805
		public char maskChar
		{
			get
			{
				return this.m_TextInputBase.maskChar;
			}
			set
			{
				this.m_TextInputBase.maskChar = value;
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00033618 File Offset: 0x00031818
		public Vector2 MeasureTextSize(string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			return TextUtilities.MeasureVisualElementTextSize(this, textToMeasure, width, widthMode, height, heightMode, this.iTextHandle);
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0003363D File Offset: 0x0003183D
		internal TextEditorEventHandler editorEventHandler
		{
			get
			{
				return this.m_TextInputBase.editorEventHandler;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0003364A File Offset: 0x0003184A
		internal TextEditorEngine editorEngine
		{
			get
			{
				return this.m_TextInputBase.editorEngine;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00033657 File Offset: 0x00031857
		internal bool hasFocus
		{
			get
			{
				return this.m_TextInputBase.hasFocus;
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00024DA2 File Offset: 0x00022FA2
		protected virtual string ValueToString(TValueType value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00024DA2 File Offset: 0x00022FA2
		protected virtual TValueType StringToValue(string str)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x00033664 File Offset: 0x00031864
		public void SelectAll()
		{
			this.m_TextInputBase.SelectAll();
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00033673 File Offset: 0x00031873
		internal void SyncTextEngine()
		{
			this.m_TextInputBase.SyncTextEngine();
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00033682 File Offset: 0x00031882
		internal void DrawWithTextSelectionAndCursor(MeshGenerationContext mgc, string newText)
		{
			this.m_TextInputBase.DrawWithTextSelectionAndCursor(mgc, newText, base.scaledPixelsPerPoint);
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00033699 File Offset: 0x00031899
		protected TextInputBaseField(int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase) : this(null, maxLength, maskChar, textInputBase)
		{
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000336A8 File Offset: 0x000318A8
		protected TextInputBaseField(string label, int maxLength, char maskChar, TextInputBaseField<TValueType>.TextInputBase textInputBase) : base(label, textInputBase)
		{
			base.tabIndex = 0;
			base.delegatesFocus = true;
			base.labelElement.tabIndex = -1;
			base.AddToClassList(TextInputBaseField<TValueType>.ussClassName);
			base.labelElement.AddToClassList(TextInputBaseField<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
			base.visualInput.AddToClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
			this.m_TextInputBase = textInputBase;
			this.m_TextInputBase.maxLength = maxLength;
			this.m_TextInputBase.maskChar = maskChar;
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnFieldCustomStyleResolved), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00033766 File Offset: 0x00031966
		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			this.iTextHandle = ((e.destinationPanel.contextType == ContextType.Editor) ? TextNativeHandle.New() : TextCoreHandle.New());
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0003378A File Offset: 0x0003198A
		private void OnFieldCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this.m_TextInputBase.OnInputCustomStyleResolved(e);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0003379C File Offset: 0x0003199C
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
					char? c = (keyDownEvent != null) ? new char?(keyDownEvent.character) : null;
					int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : null;
					int num2 = 3;
					bool flag3;
					if (!(num.GetValueOrDefault() == num2 & num != null))
					{
						c = ((keyDownEvent != null) ? new char?(keyDownEvent.character) : null);
						num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
						num2 = 10;
						flag3 = (num.GetValueOrDefault() == num2 & num != null);
					}
					else
					{
						flag3 = true;
					}
					bool flag4 = flag3;
					if (flag4)
					{
						VisualElement visualInput = base.visualInput;
						if (visualInput != null)
						{
							visualInput.Focus();
						}
					}
				}
				else
				{
					bool flag5 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
					if (flag5)
					{
						bool showMixedValue = base.showMixedValue;
						if (showMixedValue)
						{
							this.textInputBase.text = "";
						}
						bool flag6 = evt.leafTarget == this || evt.leafTarget == base.labelElement;
						if (flag6)
						{
							this.m_VisualInputTabIndex = base.visualInput.tabIndex;
							base.visualInput.tabIndex = -1;
						}
					}
					else
					{
						bool flag7 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
						if (flag7)
						{
							base.delegatesFocus = false;
						}
						else
						{
							bool flag8 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
							if (flag8)
							{
								bool showMixedValue2 = base.showMixedValue;
								if (showMixedValue2)
								{
									this.UpdateMixedValueContent();
								}
								base.delegatesFocus = true;
								bool flag9 = evt.leafTarget == this || evt.leafTarget == base.labelElement;
								if (flag9)
								{
									base.visualInput.tabIndex = this.m_VisualInputTabIndex;
								}
							}
							else
							{
								bool flag10 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
								if (flag10)
								{
									bool showMixedValue3 = base.showMixedValue;
									if (showMixedValue3)
									{
										this.m_TextInputBase.ResetValueAndText();
									}
									bool flag11 = evt.leafTarget == this || evt.leafTarget == base.labelElement;
									if (flag11)
									{
										this.m_VisualInputTabIndex = base.visualInput.tabIndex;
										base.visualInput.tabIndex = -1;
									}
								}
								else
								{
									bool flag12 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
									if (flag12)
									{
										base.delegatesFocus = false;
									}
									else
									{
										bool flag13 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
										if (flag13)
										{
											base.delegatesFocus = true;
											bool flag14 = evt.leafTarget == this || evt.leafTarget == base.labelElement;
											if (flag14)
											{
												base.visualInput.tabIndex = this.m_VisualInputTabIndex;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00033A8C File Offset: 0x00031C8C
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				this.textInputBase.text = BaseField<TValueType>.mixedValueString;
				base.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				VisualElement visualInput = base.visualInput;
				if (visualInput != null)
				{
					visualInput.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
			}
			else
			{
				this.textInputBase.UpdateTextFromValue();
				VisualElement visualInput2 = base.visualInput;
				if (visualInput2 != null)
				{
					visualInput2.RemoveFromClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
				base.RemoveFromClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00033B10 File Offset: 0x00031D10
		// Note: this type is marked as 'beforefieldinit'.
		static TextInputBaseField()
		{
		}

		// Token: 0x040005CE RID: 1486
		private static CustomStyleProperty<Color> s_SelectionColorProperty = new CustomStyleProperty<Color>("--unity-selection-color");

		// Token: 0x040005CF RID: 1487
		private static CustomStyleProperty<Color> s_CursorColorProperty = new CustomStyleProperty<Color>("--unity-cursor-color");

		// Token: 0x040005D0 RID: 1488
		private int m_VisualInputTabIndex;

		// Token: 0x040005D1 RID: 1489
		private TextInputBaseField<TValueType>.TextInputBase m_TextInputBase;

		// Token: 0x040005D2 RID: 1490
		internal const int kMaxLengthNone = -1;

		// Token: 0x040005D3 RID: 1491
		internal const char kMaskCharDefault = '*';

		// Token: 0x040005D4 RID: 1492
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ITextHandle <iTextHandle>k__BackingField;

		// Token: 0x040005D5 RID: 1493
		public new static readonly string ussClassName = "unity-base-text-field";

		// Token: 0x040005D6 RID: 1494
		public new static readonly string labelUssClassName = TextInputBaseField<TValueType>.ussClassName + "__label";

		// Token: 0x040005D7 RID: 1495
		public new static readonly string inputUssClassName = TextInputBaseField<TValueType>.ussClassName + "__input";

		// Token: 0x040005D8 RID: 1496
		public static readonly string singleLineInputUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--single-line";

		// Token: 0x040005D9 RID: 1497
		public static readonly string multilineInputUssClassName = TextInputBaseField<TValueType>.inputUssClassName + "--multiline";

		// Token: 0x040005DA RID: 1498
		public static readonly string textInputUssName = "unity-text-input";

		// Token: 0x040005DB RID: 1499
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<bool> onIsReadOnlyChanged;

		// Token: 0x02000187 RID: 391
		public new class UxmlTraits : BaseFieldTraits<string, UxmlStringAttributeDescription>
		{
			// Token: 0x06000C75 RID: 3189 RVA: 0x00033BA0 File Offset: 0x00031DA0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)ve;
				textInputBaseField.maxLength = this.m_MaxLength.GetValueFromBag(bag, cc);
				textInputBaseField.isPasswordField = this.m_Password.GetValueFromBag(bag, cc);
				textInputBaseField.isReadOnly = this.m_IsReadOnly.GetValueFromBag(bag, cc);
				textInputBaseField.isDelayed = this.m_IsDelayed.GetValueFromBag(bag, cc);
				string valueFromBag = this.m_MaskCharacter.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					textInputBaseField.maskChar = valueFromBag[0];
				}
				textInputBaseField.text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000C76 RID: 3190 RVA: 0x00033C50 File Offset: 0x00031E50
			public UxmlTraits()
			{
			}

			// Token: 0x040005DC RID: 1500
			private UxmlIntAttributeDescription m_MaxLength = new UxmlIntAttributeDescription
			{
				name = "max-length",
				obsoleteNames = new string[]
				{
					"maxLength"
				},
				defaultValue = -1
			};

			// Token: 0x040005DD RID: 1501
			private UxmlBoolAttributeDescription m_Password = new UxmlBoolAttributeDescription
			{
				name = "password"
			};

			// Token: 0x040005DE RID: 1502
			private UxmlStringAttributeDescription m_MaskCharacter = new UxmlStringAttributeDescription
			{
				name = "mask-character",
				obsoleteNames = new string[]
				{
					"maskCharacter"
				},
				defaultValue = '*'.ToString()
			};

			// Token: 0x040005DF RID: 1503
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x040005E0 RID: 1504
			private UxmlBoolAttributeDescription m_IsReadOnly = new UxmlBoolAttributeDescription
			{
				name = "readonly"
			};

			// Token: 0x040005E1 RID: 1505
			private UxmlBoolAttributeDescription m_IsDelayed = new UxmlBoolAttributeDescription
			{
				name = "is-delayed"
			};
		}

		// Token: 0x02000188 RID: 392
		protected internal abstract class TextInputBase : VisualElement, ITextInputField, IEventHandler, ITextElement
		{
			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00033D35 File Offset: 0x00031F35
			internal string originalText
			{
				get
				{
					return this.m_OriginalText;
				}
			}

			// Token: 0x06000C78 RID: 3192 RVA: 0x00033D40 File Offset: 0x00031F40
			public void ResetValueAndText()
			{
				this.m_OriginalText = (this.text = null);
			}

			// Token: 0x06000C79 RID: 3193 RVA: 0x00033D5F File Offset: 0x00031F5F
			private void SaveValueAndText()
			{
				this.m_OriginalText = this.text;
			}

			// Token: 0x06000C7A RID: 3194 RVA: 0x00033D6E File Offset: 0x00031F6E
			private void RestoreValueAndText()
			{
				this.text = this.m_OriginalText;
			}

			// Token: 0x06000C7B RID: 3195 RVA: 0x00033D7E File Offset: 0x00031F7E
			public void SelectAll()
			{
				TextEditorEngine editorEngine = this.editorEngine;
				if (editorEngine != null)
				{
					editorEngine.SelectAll();
				}
			}

			// Token: 0x06000C7C RID: 3196 RVA: 0x00033D93 File Offset: 0x00031F93
			internal void SelectNone()
			{
				TextEditorEngine editorEngine = this.editorEngine;
				if (editorEngine != null)
				{
					editorEngine.SelectNone();
				}
			}

			// Token: 0x06000C7D RID: 3197 RVA: 0x00033DA8 File Offset: 0x00031FA8
			private void UpdateText(string value)
			{
				bool flag = this.text != value;
				if (flag)
				{
					using (InputEvent pooled = InputEvent.GetPooled(this.text, value))
					{
						pooled.target = base.parent;
						this.text = value;
						VisualElement parent = base.parent;
						if (parent != null)
						{
							parent.SendEvent(pooled);
						}
					}
				}
			}

			// Token: 0x06000C7E RID: 3198 RVA: 0x00033E1C File Offset: 0x0003201C
			protected virtual TValueType StringToValue(string str)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x00033E24 File Offset: 0x00032024
			internal void UpdateValueFromText()
			{
				TValueType value = this.StringToValue(this.text);
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				textInputBaseField.value = value;
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x00033E54 File Offset: 0x00032054
			internal void UpdateTextFromValue()
			{
				TextInputBaseField<TValueType> textInputBaseField = (TextInputBaseField<TValueType>)base.parent;
				this.text = textInputBaseField.ValueToString(textInputBaseField.rawValue);
			}

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00033E84 File Offset: 0x00032084
			public int cursorIndex
			{
				get
				{
					return this.editorEngine.cursorIndex;
				}
			}

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x06000C82 RID: 3202 RVA: 0x00033EA4 File Offset: 0x000320A4
			public int selectIndex
			{
				get
				{
					return this.editorEngine.selectIndex;
				}
			}

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00033EC1 File Offset: 0x000320C1
			bool ITextInputField.isReadOnly
			{
				get
				{
					return this.isReadOnly || !base.enabledInHierarchy;
				}
			}

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00033ED7 File Offset: 0x000320D7
			// (set) Token: 0x06000C85 RID: 3205 RVA: 0x00033EDF File Offset: 0x000320DF
			public bool isReadOnly
			{
				[CompilerGenerated]
				get
				{
					return this.<isReadOnly>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<isReadOnly>k__BackingField = value;
				}
			}

			// Token: 0x17000281 RID: 641
			// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00033EE8 File Offset: 0x000320E8
			// (set) Token: 0x06000C87 RID: 3207 RVA: 0x00033EF0 File Offset: 0x000320F0
			public int maxLength
			{
				[CompilerGenerated]
				get
				{
					return this.<maxLength>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<maxLength>k__BackingField = value;
				}
			}

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06000C88 RID: 3208 RVA: 0x00033EF9 File Offset: 0x000320F9
			// (set) Token: 0x06000C89 RID: 3209 RVA: 0x00033F01 File Offset: 0x00032101
			public char maskChar
			{
				[CompilerGenerated]
				get
				{
					return this.<maskChar>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<maskChar>k__BackingField = value;
				}
			}

			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00033F0A File Offset: 0x0003210A
			// (set) Token: 0x06000C8B RID: 3211 RVA: 0x00033F12 File Offset: 0x00032112
			public virtual bool isPasswordField
			{
				[CompilerGenerated]
				get
				{
					return this.<isPasswordField>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<isPasswordField>k__BackingField = value;
				}
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00033F1B File Offset: 0x0003211B
			// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00033F23 File Offset: 0x00032123
			public bool doubleClickSelectsWord
			{
				[CompilerGenerated]
				get
				{
					return this.<doubleClickSelectsWord>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<doubleClickSelectsWord>k__BackingField = value;
				}
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00033F2C File Offset: 0x0003212C
			// (set) Token: 0x06000C8F RID: 3215 RVA: 0x00033F34 File Offset: 0x00032134
			public bool tripleClickSelectsLine
			{
				[CompilerGenerated]
				get
				{
					return this.<tripleClickSelectsLine>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<tripleClickSelectsLine>k__BackingField = value;
				}
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00033F3D File Offset: 0x0003213D
			// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00033F45 File Offset: 0x00032145
			internal bool isDelayed
			{
				[CompilerGenerated]
				get
				{
					return this.<isDelayed>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<isDelayed>k__BackingField = value;
				}
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00033F4E File Offset: 0x0003214E
			// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00033F56 File Offset: 0x00032156
			internal bool isDragging
			{
				[CompilerGenerated]
				get
				{
					return this.<isDragging>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<isDragging>k__BackingField = value;
				}
			}

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06000C94 RID: 3220 RVA: 0x00033F60 File Offset: 0x00032160
			private bool touchScreenTextField
			{
				get
				{
					return TouchScreenKeyboard.isSupported && !TouchScreenKeyboard.isInPlaceEditingAllowed;
				}
			}

			// Token: 0x17000289 RID: 649
			// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00033F84 File Offset: 0x00032184
			private bool touchScreenTextFieldChanged
			{
				get
				{
					return this.m_TouchScreenTextFieldInitialized != this.touchScreenTextField;
				}
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00033FA7 File Offset: 0x000321A7
			public Color selectionColor
			{
				get
				{
					return this.m_SelectionColor;
				}
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00033FAF File Offset: 0x000321AF
			public Color cursorColor
			{
				get
				{
					return this.m_CursorColor;
				}
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00033FB8 File Offset: 0x000321B8
			internal bool hasFocus
			{
				get
				{
					return base.elementPanel != null && base.elementPanel.focusController.GetLeafFocusedElement() == this;
				}
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06000C99 RID: 3225 RVA: 0x00033FE8 File Offset: 0x000321E8
			// (set) Token: 0x06000C9A RID: 3226 RVA: 0x00033FF0 File Offset: 0x000321F0
			internal TextEditorEventHandler editorEventHandler
			{
				[CompilerGenerated]
				get
				{
					return this.<editorEventHandler>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<editorEventHandler>k__BackingField = value;
				}
			}

			// Token: 0x1700028E RID: 654
			// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00033FF9 File Offset: 0x000321F9
			// (set) Token: 0x06000C9C RID: 3228 RVA: 0x00034001 File Offset: 0x00032201
			internal TextEditorEngine editorEngine
			{
				[CompilerGenerated]
				get
				{
					return this.<editorEngine>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<editorEngine>k__BackingField = value;
				}
			}

			// Token: 0x1700028F RID: 655
			// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0003400C File Offset: 0x0003220C
			// (set) Token: 0x06000C9E RID: 3230 RVA: 0x00034024 File Offset: 0x00032224
			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					bool flag = this.m_Text == value;
					if (!flag)
					{
						this.m_Text = value;
						this.editorEngine.text = value;
						base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					}
				}
			}

			// Token: 0x06000C9F RID: 3231 RVA: 0x00034064 File Offset: 0x00032264
			internal TextInputBase()
			{
				this.isReadOnly = false;
				base.focusable = true;
				base.AddToClassList(TextInputBaseField<TValueType>.inputUssClassName);
				base.AddToClassList(TextInputBaseField<TValueType>.singleLineInputUssClassName);
				this.m_Text = string.Empty;
				base.name = TextInputBaseField<string>.textInputUssName;
				base.requireMeasureFunction = true;
				this.editorEngine = new TextEditorEngine(new TextEditorEngine.OnDetectFocusChangeFunction(this.OnDetectFocusChange), new TextEditorEngine.OnIndexChangeFunction(this.OnCursorIndexChange));
				this.editorEngine.style.richText = false;
				this.InitTextEditorEventHandler();
				this.editorEngine.style = new GUIStyle(this.editorEngine.style);
				base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnInputCustomStyleResolved), TrickleDown.NoTrickleDown);
				base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
				base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			}

			// Token: 0x06000CA0 RID: 3232 RVA: 0x00034180 File Offset: 0x00032380
			private void InitTextEditorEventHandler()
			{
				this.m_TouchScreenTextFieldInitialized = this.touchScreenTextField;
				bool touchScreenTextFieldInitialized = this.m_TouchScreenTextFieldInitialized;
				if (touchScreenTextFieldInitialized)
				{
					this.editorEventHandler = new TouchScreenTextEditorEventHandler(this.editorEngine, this);
				}
				else
				{
					this.doubleClickSelectsWord = true;
					this.tripleClickSelectsLine = true;
					this.editorEventHandler = new KeyboardTextEditorEventHandler(this.editorEngine, this);
				}
			}

			// Token: 0x06000CA1 RID: 3233 RVA: 0x000341E0 File Offset: 0x000323E0
			private DropdownMenuAction.Status CutActionStatus(DropdownMenuAction a)
			{
				return (base.enabledInHierarchy && this.editorEngine.hasSelection && !this.isPasswordField) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
			}

			// Token: 0x06000CA2 RID: 3234 RVA: 0x00034214 File Offset: 0x00032414
			private DropdownMenuAction.Status CopyActionStatus(DropdownMenuAction a)
			{
				return ((!base.enabledInHierarchy || this.editorEngine.hasSelection) && !this.isPasswordField) ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled;
			}

			// Token: 0x06000CA3 RID: 3235 RVA: 0x00034248 File Offset: 0x00032448
			private DropdownMenuAction.Status PasteActionStatus(DropdownMenuAction a)
			{
				return base.enabledInHierarchy ? (this.editorEngine.CanPaste() ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled) : DropdownMenuAction.Status.Hidden;
			}

			// Token: 0x06000CA4 RID: 3236 RVA: 0x00034278 File Offset: 0x00032478
			private void ProcessMenuCommand(string command)
			{
				using (ExecuteCommandEvent pooled = CommandEventBase<ExecuteCommandEvent>.GetPooled(command))
				{
					pooled.target = this;
					this.SendEvent(pooled);
				}
			}

			// Token: 0x06000CA5 RID: 3237 RVA: 0x000342BC File Offset: 0x000324BC
			private void Cut(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Cut");
			}

			// Token: 0x06000CA6 RID: 3238 RVA: 0x000342CB File Offset: 0x000324CB
			private void Copy(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Copy");
			}

			// Token: 0x06000CA7 RID: 3239 RVA: 0x000342DA File Offset: 0x000324DA
			private void Paste(DropdownMenuAction a)
			{
				this.ProcessMenuCommand("Paste");
			}

			// Token: 0x06000CA8 RID: 3240 RVA: 0x000342EC File Offset: 0x000324EC
			internal void OnInputCustomStyleResolved(CustomStyleResolvedEvent e)
			{
				Color clear = Color.clear;
				Color clear2 = Color.clear;
				ICustomStyle customStyle = e.customStyle;
				bool flag = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_SelectionColorProperty, out clear);
				if (flag)
				{
					this.m_SelectionColor = clear;
				}
				bool flag2 = customStyle.TryGetValue(TextInputBaseField<TValueType>.s_CursorColorProperty, out clear2);
				if (flag2)
				{
					this.m_CursorColor = clear2;
				}
				TextInputBaseField<TValueType>.TextInputBase.SyncGUIStyle(this, this.editorEngine.style);
			}

			// Token: 0x06000CA9 RID: 3241 RVA: 0x00034351 File Offset: 0x00032551
			private void OnAttachToPanel(AttachToPanelEvent attachEvent)
			{
				this.m_TextHandle = ((attachEvent.destinationPanel.contextType == ContextType.Editor) ? TextNativeHandle.New() : TextCoreHandle.New());
			}

			// Token: 0x06000CAA RID: 3242 RVA: 0x00034374 File Offset: 0x00032574
			internal virtual void SyncTextEngine()
			{
				this.editorEngine.text = this.CullString(this.text);
				this.editorEngine.SaveBackup();
				this.editorEngine.position = base.layout;
				this.editorEngine.DetectFocusChange();
			}

			// Token: 0x06000CAB RID: 3243 RVA: 0x000343C4 File Offset: 0x000325C4
			internal string CullString(string s)
			{
				bool flag = this.maxLength >= 0 && s != null && s.Length > this.maxLength;
				string result;
				if (flag)
				{
					result = s.Substring(0, this.maxLength);
				}
				else
				{
					result = s;
				}
				return result;
			}

			// Token: 0x06000CAC RID: 3244 RVA: 0x00034408 File Offset: 0x00032608
			internal void OnGenerateVisualContent(MeshGenerationContext mgc)
			{
				string text = this.text;
				bool isPasswordField = this.isPasswordField;
				if (isPasswordField)
				{
					text = "".PadRight(this.text.Length, this.maskChar);
				}
				bool touchScreenTextFieldInitialized = this.m_TouchScreenTextFieldInitialized;
				if (touchScreenTextFieldInitialized)
				{
					TouchScreenTextEditorEventHandler touchScreenTextEditorEventHandler = this.editorEventHandler as TouchScreenTextEditorEventHandler;
					bool flag = touchScreenTextEditorEventHandler != null;
					if (flag)
					{
						mgc.Text(MeshGenerationContextUtils.TextParams.MakeStyleBased(this, text), this.m_TextHandle, base.scaledPixelsPerPoint);
					}
				}
				else
				{
					bool flag2 = !this.hasFocus;
					if (flag2)
					{
						mgc.Text(MeshGenerationContextUtils.TextParams.MakeStyleBased(this, text), this.m_TextHandle, base.scaledPixelsPerPoint);
					}
					else
					{
						this.DrawWithTextSelectionAndCursor(mgc, text, base.scaledPixelsPerPoint);
					}
				}
			}

			// Token: 0x06000CAD RID: 3245 RVA: 0x000344C8 File Offset: 0x000326C8
			internal void DrawWithTextSelectionAndCursor(MeshGenerationContext mgc, string newText, float pixelsPerPoint)
			{
				Color playmodeTintColor = (base.panel.contextType == ContextType.Editor) ? UIElementsUtility.editorPlayModeTintColor : Color.white;
				KeyboardTextEditorEventHandler keyboardTextEditorEventHandler = this.editorEventHandler as KeyboardTextEditorEventHandler;
				bool flag = keyboardTextEditorEventHandler == null;
				if (!flag)
				{
					keyboardTextEditorEventHandler.PreDrawCursor(newText);
					int cursorIndex = this.editorEngine.cursorIndex;
					int selectIndex = this.editorEngine.selectIndex;
					Vector2 scrollOffset = this.editorEngine.scrollOffset;
					float num = TextUtilities.ComputeTextScaling(base.worldTransform, pixelsPerPoint);
					MeshGenerationContextUtils.TextParams textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(this, " ");
					float lineHeight = this.m_TextHandle.GetLineHeight(0, textParams, num, pixelsPerPoint);
					float wordWrapWidth = 0f;
					bool flag2 = this.editorEngine.multiline && base.resolvedStyle.whiteSpace == WhiteSpace.Normal;
					if (flag2)
					{
						wordWrapWidth = base.contentRect.width;
					}
					Vector2 p = this.editorEngine.graphicalCursorPos - scrollOffset;
					p.y += lineHeight;
					GUIUtility.compositionCursorPos = this.LocalToWorld(p);
					int num2 = string.IsNullOrEmpty(GUIUtility.compositionString) ? selectIndex : (cursorIndex + GUIUtility.compositionString.Length);
					bool flag3 = cursorIndex != num2 && !this.isDragging;
					if (flag3)
					{
						int cursorIndex2 = (cursorIndex < num2) ? cursorIndex : num2;
						int cursorIndex3 = (cursorIndex > num2) ? cursorIndex : num2;
						CursorPositionStylePainterParameters @default = CursorPositionStylePainterParameters.GetDefault(this, this.text);
						@default.text = this.editorEngine.text;
						@default.wordWrapWidth = wordWrapWidth;
						@default.cursorIndex = cursorIndex2;
						Vector2 vector = this.m_TextHandle.GetCursorPosition(@default, num);
						@default.cursorIndex = cursorIndex3;
						Vector2 vector2 = this.m_TextHandle.GetCursorPosition(@default, num);
						vector -= scrollOffset;
						vector2 -= scrollOffset;
						lineHeight = this.m_TextHandle.GetLineHeight(cursorIndex, textParams, num, pixelsPerPoint);
						bool flag4 = Mathf.Approximately(vector.y, vector2.y);
						if (flag4)
						{
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector.x, vector.y, vector2.x - vector.x, lineHeight),
								color = this.selectionColor,
								playmodeTintColor = playmodeTintColor
							});
						}
						else
						{
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector.x, vector.y, base.contentRect.xMax - vector.x, lineHeight),
								color = this.selectionColor,
								playmodeTintColor = playmodeTintColor
							});
							float num3 = vector2.y - vector.y - lineHeight;
							bool flag5 = num3 > 0f;
							if (flag5)
							{
								mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
								{
									rect = new Rect(base.contentRect.xMin, vector.y + lineHeight, base.contentRect.width, num3),
									color = this.selectionColor,
									playmodeTintColor = playmodeTintColor
								});
							}
							bool flag6 = vector2.x != base.contentRect.x;
							if (flag6)
							{
								mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
								{
									rect = new Rect(base.contentRect.xMin, vector2.y, vector2.x, lineHeight),
									color = this.selectionColor,
									playmodeTintColor = playmodeTintColor
								});
							}
						}
					}
					bool flag7 = !string.IsNullOrEmpty(this.editorEngine.text) && base.contentRect.width > 0f && base.contentRect.height > 0f;
					if (flag7)
					{
						textParams.rect = new Rect(base.contentRect.x - scrollOffset.x, base.contentRect.y - scrollOffset.y, base.contentRect.width + scrollOffset.x, base.contentRect.height + scrollOffset.y);
						textParams.text = this.editorEngine.text;
						mgc.Text(textParams, this.m_TextHandle, base.scaledPixelsPerPoint);
					}
					bool flag8 = !this.isReadOnly && !this.isDragging;
					if (flag8)
					{
						bool flag9 = cursorIndex == num2 && TextUtilities.IsFontAssigned(this);
						if (flag9)
						{
							CursorPositionStylePainterParameters @default = CursorPositionStylePainterParameters.GetDefault(this, this.text);
							@default.text = this.editorEngine.text;
							@default.wordWrapWidth = wordWrapWidth;
							@default.cursorIndex = cursorIndex;
							Vector2 vector3 = this.m_TextHandle.GetCursorPosition(@default, num);
							vector3 -= scrollOffset;
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector3.x, vector3.y, 1f, lineHeight),
								color = this.cursorColor,
								playmodeTintColor = playmodeTintColor
							});
						}
						bool flag10 = this.editorEngine.altCursorPosition != -1;
						if (flag10)
						{
							CursorPositionStylePainterParameters @default = CursorPositionStylePainterParameters.GetDefault(this, this.text);
							@default.text = this.editorEngine.text.Substring(0, this.editorEngine.altCursorPosition);
							@default.wordWrapWidth = wordWrapWidth;
							@default.cursorIndex = this.editorEngine.altCursorPosition;
							Vector2 vector4 = this.m_TextHandle.GetCursorPosition(@default, num);
							vector4 -= scrollOffset;
							mgc.Rectangle(new MeshGenerationContextUtils.RectangleParams
							{
								rect = new Rect(vector4.x, vector4.y, 1f, lineHeight),
								color = this.cursorColor,
								playmodeTintColor = playmodeTintColor
							});
						}
					}
					keyboardTextEditorEventHandler.PostDrawCursor();
				}
			}

			// Token: 0x06000CAE RID: 3246 RVA: 0x00034AE8 File Offset: 0x00032CE8
			internal virtual bool AcceptCharacter(char c)
			{
				return !this.isReadOnly && base.enabledInHierarchy;
			}

			// Token: 0x06000CAF RID: 3247 RVA: 0x00034B0C File Offset: 0x00032D0C
			protected virtual void BuildContextualMenu(ContextualMenuPopulateEvent evt)
			{
				bool flag = ((evt != null) ? evt.target : null) is TextInputBaseField<TValueType>.TextInputBase;
				if (flag)
				{
					bool flag2 = !this.isReadOnly;
					if (flag2)
					{
						evt.menu.AppendAction("Cut", new Action<DropdownMenuAction>(this.Cut), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CutActionStatus), null);
					}
					evt.menu.AppendAction("Copy", new Action<DropdownMenuAction>(this.Copy), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.CopyActionStatus), null);
					bool flag3 = !this.isReadOnly;
					if (flag3)
					{
						evt.menu.AppendAction("Paste", new Action<DropdownMenuAction>(this.Paste), new Func<DropdownMenuAction, DropdownMenuAction.Status>(this.PasteActionStatus), null);
					}
				}
			}

			// Token: 0x06000CB0 RID: 3248 RVA: 0x00034BD4 File Offset: 0x00032DD4
			private void OnDetectFocusChange()
			{
				bool flag = this.editorEngine.m_HasFocus && !this.hasFocus;
				if (flag)
				{
					this.editorEngine.OnFocus();
				}
				bool flag2 = !this.editorEngine.m_HasFocus && this.hasFocus;
				if (flag2)
				{
					this.editorEngine.OnLostFocus();
				}
			}

			// Token: 0x06000CB1 RID: 3249 RVA: 0x0000E357 File Offset: 0x0000C557
			private void OnCursorIndexChange()
			{
				base.IncrementVersion(VersionChangeType.Repaint);
			}

			// Token: 0x06000CB2 RID: 3250 RVA: 0x00034C38 File Offset: 0x00032E38
			protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
			{
				string text = this.m_Text;
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					text = " ";
				}
				return TextUtilities.MeasureVisualElementTextSize(this, text, desiredWidth, widthMode, desiredHeight, heightMode, this.m_TextHandle);
			}

			// Token: 0x06000CB3 RID: 3251 RVA: 0x00034C75 File Offset: 0x00032E75
			internal override void ExecuteDefaultActionDisabledAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionDisabledAtTarget(evt);
				this.ProcessEventAtTarget(evt);
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x00034C88 File Offset: 0x00032E88
			protected override void ExecuteDefaultActionAtTarget(EventBase evt)
			{
				base.ExecuteDefaultActionAtTarget(evt);
				this.ProcessEventAtTarget(evt);
			}

			// Token: 0x06000CB5 RID: 3253 RVA: 0x00034C9C File Offset: 0x00032E9C
			private void ProcessEventAtTarget(EventBase evt)
			{
				BaseVisualElementPanel elementPanel = base.elementPanel;
				if (elementPanel != null)
				{
					ContextualMenuManager contextualMenuManager = elementPanel.contextualMenuManager;
					if (contextualMenuManager != null)
					{
						contextualMenuManager.DisplayMenuIfEventMatches(evt, this);
					}
				}
				long? num = (evt != null) ? new long?(evt.eventTypeId) : null;
				long num2 = EventBase<ContextualMenuPopulateEvent>.TypeId();
				bool flag = num.GetValueOrDefault() == num2 & num != null;
				if (flag)
				{
					ContextualMenuPopulateEvent contextualMenuPopulateEvent = evt as ContextualMenuPopulateEvent;
					int count = contextualMenuPopulateEvent.menu.MenuItems().Count;
					this.BuildContextualMenu(contextualMenuPopulateEvent);
					bool flag2 = count > 0 && contextualMenuPopulateEvent.menu.MenuItems().Count > count;
					if (flag2)
					{
						contextualMenuPopulateEvent.menu.InsertSeparator(null, count);
					}
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<FocusInEvent>.TypeId();
					if (flag3)
					{
						this.SaveValueAndText();
						bool touchScreenTextFieldChanged = this.touchScreenTextFieldChanged;
						if (touchScreenTextFieldChanged)
						{
							this.InitTextEditorEventHandler();
						}
						bool flag4 = this.m_HardwareKeyboardPoller == null;
						if (flag4)
						{
							this.m_HardwareKeyboardPoller = base.schedule.Execute(delegate()
							{
								bool touchScreenTextFieldChanged2 = this.touchScreenTextFieldChanged;
								if (touchScreenTextFieldChanged2)
								{
									this.InitTextEditorEventHandler();
									this.Blur();
								}
							}).Every(250L);
						}
						else
						{
							this.m_HardwareKeyboardPoller.Resume();
						}
					}
					else
					{
						bool flag5 = evt.eventTypeId == EventBase<FocusOutEvent>.TypeId();
						if (flag5)
						{
							bool flag6 = this.m_HardwareKeyboardPoller != null;
							if (flag6)
							{
								this.m_HardwareKeyboardPoller.Pause();
							}
						}
						else
						{
							bool flag7 = evt.eventTypeId == EventBase<KeyDownEvent>.TypeId();
							if (flag7)
							{
								KeyDownEvent keyDownEvent = evt as KeyDownEvent;
								bool flag8 = keyDownEvent != null && keyDownEvent.keyCode == KeyCode.Escape;
								if (flag8)
								{
									this.RestoreValueAndText();
									base.parent.Focus();
								}
							}
						}
					}
				}
				this.editorEventHandler.ExecuteDefaultActionAtTarget(evt);
			}

			// Token: 0x06000CB6 RID: 3254 RVA: 0x00034E62 File Offset: 0x00033062
			protected override void ExecuteDefaultAction(EventBase evt)
			{
				base.ExecuteDefaultAction(evt);
				this.editorEventHandler.ExecuteDefaultAction(evt);
			}

			// Token: 0x17000290 RID: 656
			// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00034E7A File Offset: 0x0003307A
			bool ITextInputField.hasFocus
			{
				get
				{
					return this.hasFocus;
				}
			}

			// Token: 0x06000CB8 RID: 3256 RVA: 0x00034E82 File Offset: 0x00033082
			void ITextInputField.SyncTextEngine()
			{
				this.SyncTextEngine();
			}

			// Token: 0x06000CB9 RID: 3257 RVA: 0x00034E8C File Offset: 0x0003308C
			bool ITextInputField.AcceptCharacter(char c)
			{
				return this.AcceptCharacter(c);
			}

			// Token: 0x06000CBA RID: 3258 RVA: 0x00034EA8 File Offset: 0x000330A8
			string ITextInputField.CullString(string s)
			{
				return this.CullString(s);
			}

			// Token: 0x06000CBB RID: 3259 RVA: 0x00034EC1 File Offset: 0x000330C1
			void ITextInputField.UpdateText(string value)
			{
				this.UpdateText(value);
			}

			// Token: 0x17000291 RID: 657
			// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00034ECC File Offset: 0x000330CC
			TextEditorEngine ITextInputField.editorEngine
			{
				get
				{
					return this.editorEngine;
				}
			}

			// Token: 0x17000292 RID: 658
			// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00034ED4 File Offset: 0x000330D4
			bool ITextInputField.isDelayed
			{
				get
				{
					return this.isDelayed;
				}
			}

			// Token: 0x06000CBE RID: 3262 RVA: 0x00034EDC File Offset: 0x000330DC
			void ITextInputField.UpdateValueFromText()
			{
				this.UpdateValueFromText();
			}

			// Token: 0x06000CBF RID: 3263 RVA: 0x00034EE6 File Offset: 0x000330E6
			private void DeferGUIStyleRectSync()
			{
				base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPercentResolved), TrickleDown.NoTrickleDown);
			}

			// Token: 0x06000CC0 RID: 3264 RVA: 0x00034F00 File Offset: 0x00033100
			private void OnPercentResolved(GeometryChangedEvent evt)
			{
				base.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnPercentResolved), TrickleDown.NoTrickleDown);
				GUIStyle style = this.editorEngine.style;
				int left = (int)base.resolvedStyle.marginLeft;
				int top = (int)base.resolvedStyle.marginTop;
				int right = (int)base.resolvedStyle.marginRight;
				int bottom = (int)base.resolvedStyle.marginBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.margin, left, top, right, bottom);
				left = (int)base.resolvedStyle.paddingLeft;
				top = (int)base.resolvedStyle.paddingTop;
				right = (int)base.resolvedStyle.paddingRight;
				bottom = (int)base.resolvedStyle.paddingBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.padding, left, top, right, bottom);
			}

			// Token: 0x06000CC1 RID: 3265 RVA: 0x00034FBC File Offset: 0x000331BC
			private unsafe static void SyncGUIStyle(TextInputBaseField<TValueType>.TextInputBase textInput, GUIStyle style)
			{
				ComputedStyle computedStyle = *textInput.computedStyle;
				style.alignment = computedStyle.unityTextAlign;
				style.wordWrap = (computedStyle.whiteSpace == WhiteSpace.Normal);
				style.clipping = ((computedStyle.overflow == OverflowInternal.Visible) ? TextClipping.Overflow : TextClipping.Clip);
				style.font = TextUtilities.GetFont(textInput);
				style.fontSize = (int)computedStyle.fontSize.value;
				style.fontStyle = computedStyle.unityFontStyleAndWeight;
				int left = computedStyle.unitySliceLeft;
				int top = computedStyle.unitySliceTop;
				int right = computedStyle.unitySliceRight;
				int bottom = computedStyle.unitySliceBottom;
				TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.border, left, top, right, bottom);
				bool flag = TextInputBaseField<TValueType>.TextInputBase.IsLayoutUsingPercent(textInput);
				if (flag)
				{
					textInput.DeferGUIStyleRectSync();
				}
				else
				{
					left = (int)computedStyle.marginLeft.value;
					top = (int)computedStyle.marginTop.value;
					right = (int)computedStyle.marginRight.value;
					bottom = (int)computedStyle.marginBottom.value;
					TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.margin, left, top, right, bottom);
					left = (int)computedStyle.paddingLeft.value;
					top = (int)computedStyle.paddingTop.value;
					right = (int)computedStyle.paddingRight.value;
					bottom = (int)computedStyle.paddingBottom.value;
					TextInputBaseField<TValueType>.TextInputBase.AssignRect(style.padding, left, top, right, bottom);
				}
			}

			// Token: 0x06000CC2 RID: 3266 RVA: 0x0003514C File Offset: 0x0003334C
			private unsafe static bool IsLayoutUsingPercent(VisualElement ve)
			{
				ComputedStyle computedStyle = *ve.computedStyle;
				bool flag = computedStyle.marginLeft.unit == LengthUnit.Percent || computedStyle.marginTop.unit == LengthUnit.Percent || computedStyle.marginRight.unit == LengthUnit.Percent || computedStyle.marginBottom.unit == LengthUnit.Percent;
				bool result;
				if (flag)
				{
					result = true;
				}
				else
				{
					bool flag2 = computedStyle.paddingLeft.unit == LengthUnit.Percent || computedStyle.paddingTop.unit == LengthUnit.Percent || computedStyle.paddingRight.unit == LengthUnit.Percent || computedStyle.paddingBottom.unit == LengthUnit.Percent;
					result = flag2;
				}
				return result;
			}

			// Token: 0x06000CC3 RID: 3267 RVA: 0x00035213 File Offset: 0x00033413
			private static void AssignRect(RectOffset rect, int left, int top, int right, int bottom)
			{
				rect.left = left;
				rect.top = top;
				rect.right = right;
				rect.bottom = bottom;
			}

			// Token: 0x06000CC4 RID: 3268 RVA: 0x00035238 File Offset: 0x00033438
			[CompilerGenerated]
			private void <ProcessEventAtTarget>b__99_0()
			{
				bool touchScreenTextFieldChanged = this.touchScreenTextFieldChanged;
				if (touchScreenTextFieldChanged)
				{
					this.InitTextEditorEventHandler();
					this.Blur();
				}
			}

			// Token: 0x040005E2 RID: 1506
			private string m_OriginalText;

			// Token: 0x040005E3 RID: 1507
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private bool <isReadOnly>k__BackingField;

			// Token: 0x040005E4 RID: 1508
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private int <maxLength>k__BackingField;

			// Token: 0x040005E5 RID: 1509
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private char <maskChar>k__BackingField;

			// Token: 0x040005E6 RID: 1510
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool <isPasswordField>k__BackingField;

			// Token: 0x040005E7 RID: 1511
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool <doubleClickSelectsWord>k__BackingField;

			// Token: 0x040005E8 RID: 1512
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private bool <tripleClickSelectsLine>k__BackingField;

			// Token: 0x040005E9 RID: 1513
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private bool <isDelayed>k__BackingField;

			// Token: 0x040005EA RID: 1514
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			[CompilerGenerated]
			private bool <isDragging>k__BackingField;

			// Token: 0x040005EB RID: 1515
			private bool m_TouchScreenTextFieldInitialized;

			// Token: 0x040005EC RID: 1516
			private IVisualElementScheduledItem m_HardwareKeyboardPoller = null;

			// Token: 0x040005ED RID: 1517
			private Color m_SelectionColor = Color.clear;

			// Token: 0x040005EE RID: 1518
			private Color m_CursorColor = Color.grey;

			// Token: 0x040005EF RID: 1519
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private TextEditorEventHandler <editorEventHandler>k__BackingField;

			// Token: 0x040005F0 RID: 1520
			[CompilerGenerated]
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private TextEditorEngine <editorEngine>k__BackingField;

			// Token: 0x040005F1 RID: 1521
			private ITextHandle m_TextHandle;

			// Token: 0x040005F2 RID: 1522
			private string m_Text;
		}
	}
}
