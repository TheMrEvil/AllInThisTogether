using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000018 RID: 24
	[AddComponentMenu("UI/Legacy/Input Field", 103)]
	public class InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement, ILayoutElement
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00009528 File Offset: 0x00007728
		private BaseInput input
		{
			get
			{
				if (EventSystem.current && EventSystem.current.currentInputModule)
				{
					return EventSystem.current.currentInputModule.input;
				}
				return null;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00009558 File Offset: 0x00007758
		private string compositionString
		{
			get
			{
				if (!(this.input != null))
				{
					return Input.compositionString;
				}
				return this.input.compositionString;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000957C File Offset: 0x0000777C
		protected InputField()
		{
			this.EnforceTextHOverflow();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00009636 File Offset: 0x00007836
		protected Mesh mesh
		{
			get
			{
				if (this.m_Mesh == null)
				{
					this.m_Mesh = new Mesh();
				}
				return this.m_Mesh;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00009657 File Offset: 0x00007857
		protected TextGenerator cachedInputTextGenerator
		{
			get
			{
				if (this.m_InputTextCache == null)
				{
					this.m_InputTextCache = new TextGenerator();
				}
				return this.m_InputTextCache;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00009684 File Offset: 0x00007884
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00009672 File Offset: 0x00007872
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform != RuntimePlatform.tvOS) || this.m_HideMobileInput;
			}
			set
			{
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000096B6 File Offset: 0x000078B6
		// (set) Token: 0x06000161 RID: 353 RVA: 0x000096AD File Offset: 0x000078AD
		public virtual bool shouldActivateOnSelect
		{
			get
			{
				return this.m_ShouldActivateOnSelect && Application.platform != RuntimePlatform.tvOS;
			}
			set
			{
				this.m_ShouldActivateOnSelect = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000096CE File Offset: 0x000078CE
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000096D6 File Offset: 0x000078D6
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.SetText(value, true);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000096E0 File Offset: 0x000078E0
		public void SetTextWithoutNotify(string input)
		{
			this.SetText(input, false);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000096EC File Offset: 0x000078EC
		private void SetText(string value, bool sendCallback = true)
		{
			if (this.text == value)
			{
				return;
			}
			if (value == null)
			{
				value = "";
			}
			value = value.Replace("\0", string.Empty);
			if (this.m_LineType == InputField.LineType.SingleLine)
			{
				value = value.Replace("\n", "").Replace("\t", "");
			}
			if (this.onValidateInput != null || this.characterValidation != InputField.CharacterValidation.None)
			{
				this.m_Text = "";
				InputField.OnValidateInput onValidateInput = this.onValidateInput ?? new InputField.OnValidateInput(this.Validate);
				this.m_CaretPosition = (this.m_CaretSelectPosition = value.Length);
				int num = (this.characterLimit > 0) ? Math.Min(this.characterLimit, value.Length) : value.Length;
				for (int i = 0; i < num; i++)
				{
					char c = onValidateInput(this.m_Text, this.m_Text.Length, value[i]);
					if (c != '\0')
					{
						this.m_Text += c.ToString();
					}
				}
			}
			else
			{
				this.m_Text = ((this.characterLimit > 0 && value.Length > this.characterLimit) ? value.Substring(0, this.characterLimit) : value);
			}
			if (this.m_Keyboard != null)
			{
				this.m_Keyboard.text = this.m_Text;
			}
			if (this.m_CaretPosition > this.m_Text.Length)
			{
				this.m_CaretPosition = (this.m_CaretSelectPosition = this.m_Text.Length);
			}
			else if (this.m_CaretSelectPosition > this.m_Text.Length)
			{
				this.m_CaretSelectPosition = this.m_Text.Length;
			}
			if (sendCallback)
			{
				this.SendOnValueChanged();
			}
			this.UpdateLabel();
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000098AC File Offset: 0x00007AAC
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000098B4 File Offset: 0x00007AB4
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000098BC File Offset: 0x00007ABC
		public float caretBlinkRate
		{
			get
			{
				return this.m_CaretBlinkRate;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_CaretBlinkRate, value) && this.m_AllowInput)
				{
					this.SetCaretActive();
				}
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000098DA File Offset: 0x00007ADA
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000098E2 File Offset: 0x00007AE2
		public int caretWidth
		{
			get
			{
				return this.m_CaretWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_CaretWidth, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000098F8 File Offset: 0x00007AF8
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00009900 File Offset: 0x00007B00
		public Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				if (this.m_TextComponent != null)
				{
					this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
					this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
					this.m_TextComponent.UnregisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
				}
				if (SetPropertyUtility.SetClass<Text>(ref this.m_TextComponent, value))
				{
					this.EnforceTextHOverflow();
					if (this.m_TextComponent != null)
					{
						this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
						this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
						this.m_TextComponent.RegisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
					}
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000099C7 File Offset: 0x00007BC7
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000099CF File Offset: 0x00007BCF
		public Graphic placeholder
		{
			get
			{
				return this.m_Placeholder;
			}
			set
			{
				SetPropertyUtility.SetClass<Graphic>(ref this.m_Placeholder, value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000099DE File Offset: 0x00007BDE
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000099FA File Offset: 0x00007BFA
		public Color caretColor
		{
			get
			{
				if (!this.customCaretColor)
				{
					return this.textComponent.color;
				}
				return this.m_CaretColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_CaretColor, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00009A10 File Offset: 0x00007C10
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00009A18 File Offset: 0x00007C18
		public bool customCaretColor
		{
			get
			{
				return this.m_CustomCaretColor;
			}
			set
			{
				if (this.m_CustomCaretColor != value)
				{
					this.m_CustomCaretColor = value;
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00009A30 File Offset: 0x00007C30
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00009A38 File Offset: 0x00007C38
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_SelectionColor, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00009A4E File Offset: 0x00007C4E
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00009A56 File Offset: 0x00007C56
		public InputField.EndEditEvent onEndEdit
		{
			get
			{
				return this.m_OnDidEndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.EndEditEvent>(ref this.m_OnDidEndEdit, value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00009A65 File Offset: 0x00007C65
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00009A6D File Offset: 0x00007C6D
		public InputField.SubmitEvent onSubmit
		{
			get
			{
				return this.m_OnSubmit;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.SubmitEvent>(ref this.m_OnSubmit, value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00009A7C File Offset: 0x00007C7C
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00009A84 File Offset: 0x00007C84
		[Obsolete("onValueChange has been renamed to onValueChanged")]
		public InputField.OnChangeEvent onValueChange
		{
			get
			{
				return this.onValueChanged;
			}
			set
			{
				this.onValueChanged = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009A8D File Offset: 0x00007C8D
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00009A95 File Offset: 0x00007C95
		public InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnChangeEvent>(ref this.m_OnValueChanged, value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00009AA4 File Offset: 0x00007CA4
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00009AAC File Offset: 0x00007CAC
		public InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009ABB File Offset: 0x00007CBB
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00009AC3 File Offset: 0x00007CC3
		public int characterLimit
		{
			get
			{
				return this.m_CharacterLimit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_CharacterLimit, Math.Max(0, value)))
				{
					this.UpdateLabel();
					if (this.m_Keyboard != null)
					{
						this.m_Keyboard.characterLimit = value;
					}
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00009AF3 File Offset: 0x00007CF3
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00009AFB File Offset: 0x00007CFB
		public InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00009B11 File Offset: 0x00007D11
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00009B19 File Offset: 0x00007D19
		public InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new InputField.ContentType[]
					{
						InputField.ContentType.Standard,
						InputField.ContentType.Autocorrected
					});
					this.EnforceTextHOverflow();
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00009B3F File Offset: 0x00007D3F
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00009B47 File Offset: 0x00007D47
		public InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00009B5D File Offset: 0x00007D5D
		public TouchScreenKeyboard touchScreenKeyboard
		{
			get
			{
				return this.m_Keyboard;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00009B65 File Offset: 0x00007D65
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00009B6D File Offset: 0x00007D6D
		public TouchScreenKeyboardType keyboardType
		{
			get
			{
				return this.m_KeyboardType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TouchScreenKeyboardType>(ref this.m_KeyboardType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00009B83 File Offset: 0x00007D83
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00009B8B File Offset: 0x00007D8B
		public InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00009BA1 File Offset: 0x00007DA1
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00009BA9 File Offset: 0x00007DA9
		public bool readOnly
		{
			get
			{
				return this.m_ReadOnly;
			}
			set
			{
				this.m_ReadOnly = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009BB2 File Offset: 0x00007DB2
		public bool multiLine
		{
			get
			{
				return this.m_LineType == InputField.LineType.MultiLineNewline || this.lineType == InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00009BC8 File Offset: 0x00007DC8
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00009BD0 File Offset: 0x00007DD0
		public char asteriskChar
		{
			get
			{
				return this.m_AsteriskChar;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<char>(ref this.m_AsteriskChar, value))
				{
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00009BE6 File Offset: 0x00007DE6
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009BEE File Offset: 0x00007DEE
		protected void ClampPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
				return;
			}
			if (pos > this.text.Length)
			{
				pos = this.text.Length;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00009C15 File Offset: 0x00007E15
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00009C29 File Offset: 0x00007E29
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + this.compositionString.Length;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00009C3E File Offset: 0x00007E3E
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00009C52 File Offset: 0x00007E52
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00009C67 File Offset: 0x00007E67
		private bool hasSelection
		{
			get
			{
				return this.caretPositionInternal != this.caretSelectPositionInternal;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00009C7A File Offset: 0x00007E7A
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00009C8E File Offset: 0x00007E8E
		public int caretPosition
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00009C9E File Offset: 0x00007E9E
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00009CB2 File Offset: 0x00007EB2
		public int selectionAnchorPosition
		{
			get
			{
				return this.m_CaretPosition + this.compositionString.Length;
			}
			set
			{
				if (this.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretPosition = value;
				this.ClampPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009CD5 File Offset: 0x00007ED5
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00009CE9 File Offset: 0x00007EE9
		public int selectionFocusPosition
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionString.Length;
			}
			set
			{
				if (this.compositionString.Length != 0)
				{
					return;
				}
				this.m_CaretSelectPosition = value;
				this.ClampPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009D0C File Offset: 0x00007F0C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			this.m_DrawStart = 0;
			this.m_DrawEnd = this.m_Text.Length;
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				this.m_TextComponent.RegisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
				this.UpdateLabel();
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00009DD4 File Offset: 0x00007FD4
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField();
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				this.m_TextComponent.UnregisterDirtyMaterialCallback(new UnityAction(this.UpdateCaretMaterial));
			}
			CanvasUpdateRegistry.DisableCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.Clear();
			}
			if (this.m_Mesh != null)
			{
				Object.DestroyImmediate(this.m_Mesh);
			}
			this.m_Mesh = null;
			base.OnDisable();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00009E86 File Offset: 0x00008086
		protected override void OnDestroy()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			base.OnDestroy();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00009E94 File Offset: 0x00008094
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while (this.isFocused && this.m_CaretBlinkRate > 0f)
			{
				float num = 1f / this.m_CaretBlinkRate;
				bool flag = (Time.unscaledTime - this.m_BlinkStartTime) % num < num / 2f;
				if (this.m_CaretVisible != flag)
				{
					this.m_CaretVisible = flag;
					if (!this.hasSelection)
					{
						this.MarkGeometryAsDirty();
					}
				}
				yield return null;
			}
			this.m_BlinkCoroutine = null;
			yield break;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009EA3 File Offset: 0x000080A3
		private void SetCaretVisible()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_CaretVisible = true;
			this.m_BlinkStartTime = Time.unscaledTime;
			this.SetCaretActive();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009EC6 File Offset: 0x000080C6
		private void SetCaretActive()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			if (this.m_CaretBlinkRate > 0f)
			{
				if (this.m_BlinkCoroutine == null)
				{
					this.m_BlinkCoroutine = base.StartCoroutine(this.CaretBlink());
					return;
				}
			}
			else
			{
				this.m_CaretVisible = true;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009F00 File Offset: 0x00008100
		private void UpdateCaretMaterial()
		{
			if (this.m_TextComponent != null && this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009F3E File Offset: 0x0000813E
		protected void OnFocus()
		{
			this.SelectAll();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009F46 File Offset: 0x00008146
		protected void SelectAll()
		{
			this.caretPositionInternal = this.text.Length;
			this.caretSelectPositionInternal = 0;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009F60 File Offset: 0x00008160
		public void MoveTextEnd(bool shift)
		{
			int length = this.text.Length;
			if (shift)
			{
				this.caretSelectPositionInternal = length;
			}
			else
			{
				this.caretPositionInternal = length;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009FA0 File Offset: 0x000081A0
		public void MoveTextStart(bool shift)
		{
			int num = 0;
			if (shift)
			{
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.caretPositionInternal = num;
				this.caretSelectPositionInternal = this.caretPositionInternal;
			}
			this.UpdateLabel();
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00009FD4 File Offset: 0x000081D4
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00009FDB File Offset: 0x000081DB
		private static string clipboard
		{
			get
			{
				return GUIUtility.systemCopyBuffer;
			}
			set
			{
				GUIUtility.systemCopyBuffer = value;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009FE3 File Offset: 0x000081E3
		private bool TouchScreenKeyboardShouldBeUsed()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				return TouchScreenKeyboard.isSupported;
			}
			if (InputField.s_IsQuestDevice)
			{
				return TouchScreenKeyboard.isSupported;
			}
			return !TouchScreenKeyboard.isInPlaceEditingAllowed;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A009 File Offset: 0x00008209
		private bool InPlaceEditing()
		{
			return !TouchScreenKeyboard.isSupported || this.m_TouchKeyboardAllowsInPlaceEditing;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A01A File Offset: 0x0000821A
		private bool InPlaceEditingChanged()
		{
			return !InputField.s_IsQuestDevice && this.m_TouchKeyboardAllowsInPlaceEditing != TouchScreenKeyboard.isInPlaceEditingAllowed;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A038 File Offset: 0x00008238
		private void UpdateCaretFromKeyboard()
		{
			RangeInt selection = this.m_Keyboard.selection;
			int start = selection.start;
			int end = selection.end;
			bool flag = false;
			if (this.caretPositionInternal != start)
			{
				flag = true;
				this.caretPositionInternal = start;
			}
			if (this.caretSelectPositionInternal != end)
			{
				this.caretSelectPositionInternal = end;
				flag = true;
			}
			if (flag)
			{
				this.m_BlinkStartTime = Time.unscaledTime;
				this.UpdateLabel();
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A09C File Offset: 0x0000829C
		protected virtual void LateUpdate()
		{
			if (this.m_ShouldActivateNextUpdate)
			{
				if (!this.isFocused)
				{
					this.ActivateInputFieldInternal();
					this.m_ShouldActivateNextUpdate = false;
					return;
				}
				this.m_ShouldActivateNextUpdate = false;
			}
			this.AssignPositioningIfNeeded();
			if (this.isFocused && this.InPlaceEditingChanged())
			{
				if (this.m_CachedInputRenderer != null)
				{
					using (VertexHelper vertexHelper = new VertexHelper())
					{
						vertexHelper.FillMesh(this.mesh);
					}
					this.m_CachedInputRenderer.SetMesh(this.mesh);
				}
				this.DeactivateInputField();
			}
			if (!this.isFocused || this.InPlaceEditing())
			{
				return;
			}
			if (this.m_Keyboard == null || this.m_Keyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_Keyboard != null)
				{
					if (!this.m_ReadOnly)
					{
						this.text = this.m_Keyboard.text;
					}
					if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Canceled)
					{
						this.m_WasCanceled = true;
					}
					else if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Done)
					{
						this.SendOnSubmit();
					}
				}
				this.OnDeselect(null);
				return;
			}
			string text = this.m_Keyboard.text;
			if (this.m_Text != text)
			{
				if (this.m_ReadOnly)
				{
					this.m_Keyboard.text = this.m_Text;
				}
				else
				{
					this.m_Text = "";
					foreach (char c in text)
					{
						if (c == '\r' || c == '\u0003')
						{
							c = '\n';
						}
						if (this.onValidateInput != null)
						{
							c = this.onValidateInput(this.m_Text, this.m_Text.Length, c);
						}
						else if (this.characterValidation != InputField.CharacterValidation.None)
						{
							c = this.Validate(this.m_Text, this.m_Text.Length, c);
						}
						if (this.lineType == InputField.LineType.MultiLineSubmit && c == '\n')
						{
							this.UpdateLabel();
							this.SendOnSubmit();
							this.OnDeselect(null);
							return;
						}
						if (c != '\0')
						{
							this.m_Text += c.ToString();
						}
					}
					if (this.characterLimit > 0 && this.m_Text.Length > this.characterLimit)
					{
						this.m_Text = this.m_Text.Substring(0, this.characterLimit);
					}
					if (this.m_Keyboard.canGetSelection)
					{
						this.UpdateCaretFromKeyboard();
					}
					else
					{
						this.caretPositionInternal = (this.caretSelectPositionInternal = this.m_Text.Length);
					}
					if (this.m_Text != text)
					{
						this.m_Keyboard.text = this.m_Text;
					}
					this.SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (this.m_HideMobileInput && this.m_Keyboard.canSetSelection)
			{
				int start = Mathf.Min(this.caretSelectPositionInternal, this.caretPositionInternal);
				int length = Mathf.Abs(this.caretSelectPositionInternal - this.caretPositionInternal);
				this.m_Keyboard.selection = new RangeInt(start, length);
			}
			else if (this.m_Keyboard.canGetSelection && !this.m_HideMobileInput)
			{
				this.UpdateCaretFromKeyboard();
			}
			if (this.m_Keyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					this.m_WasCanceled = true;
				}
				else if (this.m_Keyboard.status == TouchScreenKeyboard.Status.Done)
				{
					this.SendOnSubmit();
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A3E0 File Offset: 0x000085E0
		[Obsolete("This function is no longer used. Please use RectTransformUtility.ScreenPointToLocalPointInRectangle() instead.")]
		public Vector2 ScreenToLocal(Vector2 screen)
		{
			Canvas canvas = this.m_TextComponent.canvas;
			if (canvas == null)
			{
				return screen;
			}
			Vector3 vector = Vector3.zero;
			if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				vector = this.m_TextComponent.transform.InverseTransformPoint(screen);
			}
			else if (canvas.worldCamera != null)
			{
				Ray ray = canvas.worldCamera.ScreenPointToRay(screen);
				Plane plane = new Plane(this.m_TextComponent.transform.forward, this.m_TextComponent.transform.position);
				float distance;
				plane.Raycast(ray, out distance);
				vector = this.m_TextComponent.transform.InverseTransformPoint(ray.GetPoint(distance));
			}
			return new Vector2(vector.x, vector.y);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A4A8 File Offset: 0x000086A8
		private int GetUnclampedCharacterLineFromPosition(Vector2 pos, TextGenerator generator)
		{
			if (!this.multiLine)
			{
				return 0;
			}
			float num = pos.y * this.m_TextComponent.pixelsPerUnit;
			float num2 = 0f;
			int i = 0;
			while (i < generator.lineCount)
			{
				float topY = generator.lines[i].topY;
				float num3 = topY - (float)generator.lines[i].height;
				if (num > topY)
				{
					float num4 = topY - num2;
					if (num > topY - 0.5f * num4)
					{
						return i - 1;
					}
					return i;
				}
				else
				{
					if (num > num3)
					{
						return i;
					}
					num2 = num3;
					i++;
				}
			}
			return generator.lineCount;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A540 File Offset: 0x00008740
		protected int GetCharacterIndexFromPosition(Vector2 pos)
		{
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator.lineCount == 0)
			{
				return 0;
			}
			int unclampedCharacterLineFromPosition = this.GetUnclampedCharacterLineFromPosition(pos, cachedTextGenerator);
			if (unclampedCharacterLineFromPosition < 0)
			{
				return 0;
			}
			if (unclampedCharacterLineFromPosition >= cachedTextGenerator.lineCount)
			{
				return cachedTextGenerator.characterCountVisible;
			}
			int startCharIdx = cachedTextGenerator.lines[unclampedCharacterLineFromPosition].startCharIdx;
			int lineEndPosition = InputField.GetLineEndPosition(cachedTextGenerator, unclampedCharacterLineFromPosition);
			int num = startCharIdx;
			while (num < lineEndPosition && num < cachedTextGenerator.characterCountVisible)
			{
				UICharInfo uicharInfo = cachedTextGenerator.characters[num];
				Vector2 vector = uicharInfo.cursorPos / this.m_TextComponent.pixelsPerUnit;
				float num2 = pos.x - vector.x;
				float num3 = vector.x + uicharInfo.charWidth / this.m_TextComponent.pixelsPerUnit - pos.x;
				if (num2 < num3)
				{
					return num;
				}
				num++;
			}
			return lineEndPosition;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A60F File Offset: 0x0000880F
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && (this.InPlaceEditing() || this.m_HideMobileInput);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A649 File Offset: 0x00008849
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A65C File Offset: 0x0000885C
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			Vector2 zero = Vector2.zero;
			if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
			{
				return;
			}
			Vector2 pos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, zero, eventData.pressEventCamera, out pos);
			this.caretSelectPositionInternal = this.GetCharacterIndexFromPosition(pos) + this.m_DrawStart;
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textComponent.rectTransform, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			eventData.Use();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A706 File Offset: 0x00008906
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 zero = Vector2.zero;
				if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
				{
					break;
				}
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, zero, eventData.pressEventCamera, out vector);
				Rect rect = this.textComponent.rectTransform.rect;
				if (this.multiLine)
				{
					if (vector.y > rect.yMax)
					{
						this.MoveUp(true, true);
					}
					else if (vector.y < rect.yMin)
					{
						this.MoveDown(true, true);
					}
				}
				else if (vector.x < rect.xMin)
				{
					this.MoveLeft(true, false);
				}
				else if (vector.x > rect.xMax)
				{
					this.MoveRight(true, false);
				}
				this.UpdateLabel();
				float num = this.multiLine ? 0.1f : 0.05f;
				if (this.m_WaitForSecondsRealtime == null)
				{
					this.m_WaitForSecondsRealtime = new WaitForSecondsRealtime(num);
				}
				else
				{
					this.m_WaitForSecondsRealtime.waitTime = num;
				}
				yield return this.m_WaitForSecondsRealtime;
			}
			this.m_DragCoroutine = null;
			yield break;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A71C File Offset: 0x0000891C
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A730 File Offset: 0x00008930
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (this.m_Keyboard == null || !this.m_Keyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			if (allowInput)
			{
				Vector2 pos;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textComponent.rectTransform, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out pos);
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCharacterIndexFromPosition(pos) + this.m_DrawStart);
			}
			this.UpdateLabel();
			eventData.Use();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000A7DC File Offset: 0x000089DC
		protected InputField.EditState KeyPressed(Event evt)
		{
			EventModifiers modifiers = evt.modifiers;
			bool flag = (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX) ? ((modifiers & EventModifiers.Command) > EventModifiers.None) : ((modifiers & EventModifiers.Control) > EventModifiers.None);
			bool flag2 = (modifiers & EventModifiers.Shift) > EventModifiers.None;
			bool flag3 = (modifiers & EventModifiers.Alt) > EventModifiers.None;
			bool flag4 = flag && !flag3 && !flag2;
			bool flag5 = flag2 && !flag && !flag3;
			KeyCode keyCode = evt.keyCode;
			if (keyCode <= KeyCode.A)
			{
				if (keyCode <= KeyCode.Return)
				{
					if (keyCode == KeyCode.Backspace)
					{
						this.Backspace();
						return InputField.EditState.Continue;
					}
					if (keyCode != KeyCode.Return)
					{
						goto IL_213;
					}
				}
				else
				{
					if (keyCode == KeyCode.Escape)
					{
						this.m_WasCanceled = true;
						return InputField.EditState.Finish;
					}
					if (keyCode != KeyCode.A)
					{
						goto IL_213;
					}
					if (flag4)
					{
						this.SelectAll();
						return InputField.EditState.Continue;
					}
					goto IL_213;
				}
			}
			else if (keyCode <= KeyCode.V)
			{
				if (keyCode != KeyCode.C)
				{
					if (keyCode != KeyCode.V)
					{
						goto IL_213;
					}
					if (flag4)
					{
						this.Append(InputField.clipboard);
						this.UpdateLabel();
						return InputField.EditState.Continue;
					}
					goto IL_213;
				}
				else
				{
					if (flag4)
					{
						if (this.inputType != InputField.InputType.Password)
						{
							InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							InputField.clipboard = "";
						}
						return InputField.EditState.Continue;
					}
					goto IL_213;
				}
			}
			else if (keyCode != KeyCode.X)
			{
				if (keyCode == KeyCode.Delete)
				{
					this.ForwardSpace();
					return InputField.EditState.Continue;
				}
				switch (keyCode)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
					goto IL_213;
				case KeyCode.UpArrow:
					this.MoveUp(flag2);
					return InputField.EditState.Continue;
				case KeyCode.DownArrow:
					this.MoveDown(flag2);
					return InputField.EditState.Continue;
				case KeyCode.RightArrow:
					this.MoveRight(flag2, flag);
					return InputField.EditState.Continue;
				case KeyCode.LeftArrow:
					this.MoveLeft(flag2, flag);
					return InputField.EditState.Continue;
				case KeyCode.Insert:
					if (flag4)
					{
						if (this.inputType != InputField.InputType.Password)
						{
							InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							InputField.clipboard = "";
						}
						return InputField.EditState.Continue;
					}
					if (flag5)
					{
						this.Append(InputField.clipboard);
						this.UpdateLabel();
						return InputField.EditState.Continue;
					}
					goto IL_213;
				case KeyCode.Home:
					this.MoveTextStart(flag2);
					return InputField.EditState.Continue;
				case KeyCode.End:
					this.MoveTextEnd(flag2);
					return InputField.EditState.Continue;
				default:
					goto IL_213;
				}
			}
			else
			{
				if (flag4)
				{
					if (this.inputType != InputField.InputType.Password)
					{
						InputField.clipboard = this.GetSelectedString();
					}
					else
					{
						InputField.clipboard = "";
					}
					this.Delete();
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return InputField.EditState.Continue;
				}
				goto IL_213;
			}
			if (this.lineType != InputField.LineType.MultiLineNewline)
			{
				return InputField.EditState.Finish;
			}
			IL_213:
			char c = evt.character;
			if (!this.multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && this.compositionString.Length > 0)
			{
				this.UpdateLabel();
			}
			return InputField.EditState.Continue;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000AA5A File Offset: 0x00008C5A
		private bool IsValidChar(char c)
		{
			return c != '\0' && c != '\u007f' && (c == '\t' || c == '\n' || this.m_TextComponent.font.HasCharacter(c));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000AA85 File Offset: 0x00008C85
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000AA90 File Offset: 0x00008C90
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool flag = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				if (this.m_ProcessingEvent.rawType == EventType.KeyDown)
				{
					flag = true;
					if (this.m_IsCompositionActive && this.compositionString.Length == 0 && this.m_ProcessingEvent.character == '\0' && this.m_ProcessingEvent.modifiers == EventModifiers.None)
					{
						continue;
					}
					if (this.KeyPressed(this.m_ProcessingEvent) == InputField.EditState.Finish)
					{
						if (!this.m_WasCanceled)
						{
							this.SendOnSubmit();
						}
						this.DeactivateInputField();
						continue;
					}
					this.UpdateLabel();
				}
				EventType type = this.m_ProcessingEvent.type;
				if (type - EventType.ValidateCommand <= 1 && this.m_ProcessingEvent.commandName == "SelectAll")
				{
					this.SelectAll();
					flag = true;
				}
			}
			if (flag)
			{
				this.UpdateLabel();
			}
			eventData.Use();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000AB68 File Offset: 0x00008D68
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return "";
			}
			int num = this.caretPositionInternal;
			int num2 = this.caretSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return this.text.Substring(num, num2 - num);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000ABA8 File Offset: 0x00008DA8
		private int FindtNextWordBegin()
		{
			if (this.caretSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int num = this.text.IndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal + 1);
			if (num == -1)
			{
				num = this.text.Length;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000AC08 File Offset: 0x00008E08
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindtNextWordBegin();
			}
			else
			{
				num = this.caretSelectPositionInternal + 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000AC74 File Offset: 0x00008E74
		private int FindtPrevWordBegin()
		{
			if (this.caretSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = this.text.LastIndexOfAny(InputField.kSeparators, this.caretSelectPositionInternal - 2);
			if (num == -1)
			{
				num = 0;
			}
			else
			{
				num++;
			}
			return num;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindtPrevWordBegin();
			}
			else
			{
				num = this.caretSelectPositionInternal - 1;
			}
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000AD20 File Offset: 0x00008F20
		private int DetermineCharacterLine(int charPos, TextGenerator generator)
		{
			for (int i = 0; i < generator.lineCount - 1; i++)
			{
				if (generator.lines[i + 1].startCharIdx > charPos)
				{
					return i;
				}
			}
			return generator.lineCount - 1;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000AD60 File Offset: 0x00008F60
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characters.Count)
			{
				return 0;
			}
			UICharInfo uicharInfo = this.cachedInputTextGenerator.characters[originalPos];
			int num = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (num > 0)
			{
				int num2 = this.cachedInputTextGenerator.lines[num].startCharIdx - 1;
				for (int i = this.cachedInputTextGenerator.lines[num - 1].startCharIdx; i < num2; i++)
				{
					if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= uicharInfo.cursorPos.x)
					{
						return i;
					}
				}
				return num2;
			}
			if (!goToFirstChar)
			{
				return originalPos;
			}
			return 0;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000AE14 File Offset: 0x00009014
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.cachedInputTextGenerator.characterCountVisible)
			{
				return this.text.Length;
			}
			UICharInfo uicharInfo = this.cachedInputTextGenerator.characters[originalPos];
			int num = this.DetermineCharacterLine(originalPos, this.cachedInputTextGenerator);
			if (num + 1 < this.cachedInputTextGenerator.lineCount)
			{
				int lineEndPosition = InputField.GetLineEndPosition(this.cachedInputTextGenerator, num + 1);
				for (int i = this.cachedInputTextGenerator.lines[num + 1].startCharIdx; i < lineEndPosition; i++)
				{
					if (this.cachedInputTextGenerator.characters[i].cursorPos.x >= uicharInfo.cursorPos.x)
					{
						return i;
					}
				}
				return lineEndPosition;
			}
			if (!goToLastChar)
			{
				return originalPos;
			}
			return this.text.Length;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000AED9 File Offset: 0x000090D9
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000AEE4 File Offset: 0x000090E4
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int caretSelectPositionInternal = this.multiLine ? this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : this.text.Length;
			if (shift)
			{
				this.caretSelectPositionInternal = caretSelectPositionInternal;
				return;
			}
			this.caretPositionInternal = (this.caretSelectPositionInternal = caretSelectPositionInternal);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000AF5A File Offset: 0x0000915A
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000AF64 File Offset: 0x00009164
		private void MoveUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = this.multiLine ? this.LineUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar) : 0;
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000AFD0 File Offset: 0x000091D0
		private void Delete()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.caretPositionInternal == this.caretSelectPositionInternal)
			{
				return;
			}
			if (this.caretPositionInternal < this.caretSelectPositionInternal)
			{
				this.m_Text = this.text.Substring(0, this.caretPositionInternal) + this.text.Substring(this.caretSelectPositionInternal, this.text.Length - this.caretSelectPositionInternal);
				this.caretSelectPositionInternal = this.caretPositionInternal;
				return;
			}
			this.m_Text = this.text.Substring(0, this.caretSelectPositionInternal) + this.text.Substring(this.caretPositionInternal, this.text.Length - this.caretPositionInternal);
			this.caretPositionInternal = this.caretSelectPositionInternal;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000B09C File Offset: 0x0000929C
		private void ForwardSpace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.caretPositionInternal < this.text.Length)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal, 1);
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000B104 File Offset: 0x00009304
		private void Backspace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.caretPositionInternal > 0 && this.caretPositionInternal - 1 < this.text.Length)
			{
				this.m_Text = this.text.Remove(this.caretPositionInternal - 1, 1);
				this.caretSelectPositionInternal = --this.caretPositionInternal;
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000B190 File Offset: 0x00009390
		private void Insert(char c)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			string text = c.ToString();
			this.Delete();
			if (this.characterLimit > 0 && this.text.Length >= this.characterLimit)
			{
				return;
			}
			this.m_Text = this.text.Insert(this.m_CaretPosition, text);
			this.caretSelectPositionInternal = (this.caretPositionInternal += text.Length);
			this.UpdateTouchKeyboardFromEditChanges();
			this.SendOnValueChanged();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000B211 File Offset: 0x00009411
		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (this.m_Keyboard != null && this.InPlaceEditing())
			{
				this.m_Keyboard.text = this.m_Text;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B234 File Offset: 0x00009434
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.SendOnValueChanged();
			this.UpdateLabel();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000B242 File Offset: 0x00009442
		private void SendOnValueChanged()
		{
			UISystemProfilerApi.AddMarker("InputField.value", this);
			if (this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.text);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000B268 File Offset: 0x00009468
		protected void SendOnEndEdit()
		{
			UISystemProfilerApi.AddMarker("InputField.onEndEdit", this);
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000B28E File Offset: 0x0000948E
		protected void SendOnSubmit()
		{
			UISystemProfilerApi.AddMarker("InputField.onSubmit", this);
			if (this.onSubmit != null)
			{
				this.onSubmit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000B2B4 File Offset: 0x000094B4
		protected virtual void Append(string input)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int i = 0;
			int length = input.Length;
			while (i < length)
			{
				char c = input[i];
				if (c >= ' ' || c == '\t' || c == '\r' || c == '\n' || c == '\n')
				{
					this.Append(c);
				}
				i++;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000B310 File Offset: 0x00009510
		protected virtual void Append(char input)
		{
			if (char.IsSurrogate(input))
			{
				return;
			}
			if (this.m_ReadOnly || this.text.Length >= 16382)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int num = Math.Min(this.selectionFocusPosition, this.selectionAnchorPosition);
			string text = this.text;
			if (this.selectionFocusPosition != this.selectionAnchorPosition)
			{
				if (this.caretPositionInternal < this.caretSelectPositionInternal)
				{
					text = this.text.Substring(0, this.caretPositionInternal) + this.text.Substring(this.caretSelectPositionInternal, this.text.Length - this.caretSelectPositionInternal);
				}
				else
				{
					text = this.text.Substring(0, this.caretSelectPositionInternal) + this.text.Substring(this.caretPositionInternal, this.text.Length - this.caretPositionInternal);
				}
			}
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(text, num, input);
			}
			else if (this.characterValidation != InputField.CharacterValidation.None)
			{
				input = this.Validate(text, num, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B434 File Offset: 0x00009634
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventFontCallback)
			{
				this.m_PreventFontCallback = true;
				string text;
				if (EventSystem.current != null && base.gameObject == EventSystem.current.currentSelectedGameObject && this.compositionString.Length > 0)
				{
					this.m_IsCompositionActive = true;
					text = this.text.Substring(0, this.m_CaretPosition) + this.compositionString + this.text.Substring(this.m_CaretPosition);
				}
				else
				{
					this.m_IsCompositionActive = false;
					text = this.text;
				}
				string text2;
				if (this.inputType == InputField.InputType.Password)
				{
					text2 = new string(this.asteriskChar, text.Length);
				}
				else
				{
					text2 = text;
				}
				bool flag = string.IsNullOrEmpty(text);
				if (this.m_Placeholder != null)
				{
					this.m_Placeholder.enabled = flag;
				}
				if (!this.m_AllowInput)
				{
					this.m_DrawStart = 0;
					this.m_DrawEnd = this.m_Text.Length;
				}
				if (!flag)
				{
					Vector2 size = this.m_TextComponent.rectTransform.rect.size;
					TextGenerationSettings generationSettings = this.m_TextComponent.GetGenerationSettings(size);
					generationSettings.generateOutOfBounds = true;
					this.cachedInputTextGenerator.PopulateWithErrors(text2, generationSettings, base.gameObject);
					this.SetDrawRangeToContainCaretPosition(this.caretSelectPositionInternal);
					text2 = text2.Substring(this.m_DrawStart, Mathf.Min(this.m_DrawEnd, text2.Length) - this.m_DrawStart);
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = text2;
				this.MarkGeometryAsDirty();
				this.m_PreventFontCallback = false;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B5EA File Offset: 0x000097EA
		private bool IsSelectionVisible()
		{
			return this.m_DrawStart <= this.caretPositionInternal && this.m_DrawStart <= this.caretSelectPositionInternal && this.m_DrawEnd >= this.caretPositionInternal && this.m_DrawEnd >= this.caretSelectPositionInternal;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000B629 File Offset: 0x00009829
		private static int GetLineStartPosition(TextGenerator gen, int line)
		{
			line = Mathf.Clamp(line, 0, gen.lines.Count - 1);
			return gen.lines[line].startCharIdx;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000B652 File Offset: 0x00009852
		private static int GetLineEndPosition(TextGenerator gen, int line)
		{
			line = Mathf.Max(line, 0);
			if (line + 1 < gen.lines.Count)
			{
				return gen.lines[line + 1].startCharIdx - 1;
			}
			return gen.characterCountVisible;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B68C File Offset: 0x0000988C
		private void SetDrawRangeToContainCaretPosition(int caretPos)
		{
			if (this.cachedInputTextGenerator.lineCount <= 0)
			{
				return;
			}
			Vector2 size = this.cachedInputTextGenerator.rectExtents.size;
			if (!this.multiLine)
			{
				IList<UICharInfo> characters = this.cachedInputTextGenerator.characters;
				if (this.m_DrawEnd > this.cachedInputTextGenerator.characterCountVisible)
				{
					this.m_DrawEnd = this.cachedInputTextGenerator.characterCountVisible;
				}
				float num = 0f;
				if (caretPos > this.m_DrawEnd || (caretPos == this.m_DrawEnd && this.m_DrawStart > 0))
				{
					this.m_DrawEnd = caretPos;
					this.m_DrawStart = this.m_DrawEnd - 1;
					while (this.m_DrawStart >= 0 && num + characters[this.m_DrawStart].charWidth <= size.x)
					{
						num += characters[this.m_DrawStart].charWidth;
						this.m_DrawStart--;
					}
					this.m_DrawStart++;
				}
				else
				{
					if (caretPos < this.m_DrawStart)
					{
						this.m_DrawStart = caretPos;
					}
					this.m_DrawEnd = this.m_DrawStart;
				}
				while (this.m_DrawEnd < this.cachedInputTextGenerator.characterCountVisible)
				{
					num += characters[this.m_DrawEnd].charWidth;
					if (num > size.x)
					{
						break;
					}
					this.m_DrawEnd++;
				}
				return;
			}
			IList<UILineInfo> lines = this.cachedInputTextGenerator.lines;
			int num2 = this.DetermineCharacterLine(caretPos, this.cachedInputTextGenerator);
			if (caretPos > this.m_DrawEnd)
			{
				this.m_DrawEnd = InputField.GetLineEndPosition(this.cachedInputTextGenerator, num2);
				float num3 = lines[num2].topY - (float)lines[num2].height;
				if (num2 == lines.Count - 1)
				{
					num3 += lines[num2].leading;
				}
				int num4 = num2;
				while (num4 > 0 && lines[num4 - 1].topY - num3 <= size.y)
				{
					num4--;
				}
				this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, num4);
				return;
			}
			if (caretPos < this.m_DrawStart)
			{
				this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, num2);
			}
			int i = this.DetermineCharacterLine(this.m_DrawStart, this.cachedInputTextGenerator);
			int j = i;
			float topY = lines[i].topY;
			float num5 = lines[j].topY - (float)lines[j].height;
			if (j == lines.Count - 1)
			{
				num5 += lines[j].leading;
			}
			while (j < lines.Count - 1)
			{
				num5 = lines[j + 1].topY - (float)lines[j + 1].height;
				if (j + 1 == lines.Count - 1)
				{
					num5 += lines[j + 1].leading;
				}
				if (topY - num5 > size.y)
				{
					break;
				}
				j++;
			}
			this.m_DrawEnd = InputField.GetLineEndPosition(this.cachedInputTextGenerator, j);
			while (i > 0)
			{
				topY = lines[i - 1].topY;
				if (topY - num5 > size.y)
				{
					break;
				}
				i--;
			}
			this.m_DrawStart = InputField.GetLineStartPosition(this.cachedInputTextGenerator, i);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B9CF File Offset: 0x00009BCF
		public void ForceLabelUpdate()
		{
			this.UpdateLabel();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000B9D7 File Offset: 0x00009BD7
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B9DF File Offset: 0x00009BDF
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000B9EB File Offset: 0x00009BEB
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000B9ED File Offset: 0x00009BED
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000B9F0 File Offset: 0x00009BF0
		private void UpdateGeometry()
		{
			if (!this.InPlaceEditing() && !this.shouldHideMobileInput)
			{
				return;
			}
			if (this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject gameObject = new GameObject(base.transform.name + " Input Caret", new Type[]
				{
					typeof(RectTransform),
					typeof(CanvasRenderer)
				});
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(this.m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				this.caretRectTrans = gameObject.GetComponent<RectTransform>();
				this.m_CachedInputRenderer = gameObject.GetComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(this.m_TextComponent.GetModifiedMaterial(Graphic.defaultGraphicMaterial), Texture2D.whiteTexture);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.mesh);
			this.m_CachedInputRenderer.SetMesh(this.mesh);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000BB20 File Offset: 0x00009D20
		private void AssignPositioningIfNeeded()
		{
			if (this.m_TextComponent != null && this.caretRectTrans != null && (this.caretRectTrans.localPosition != this.m_TextComponent.rectTransform.localPosition || this.caretRectTrans.localRotation != this.m_TextComponent.rectTransform.localRotation || this.caretRectTrans.localScale != this.m_TextComponent.rectTransform.localScale || this.caretRectTrans.anchorMin != this.m_TextComponent.rectTransform.anchorMin || this.caretRectTrans.anchorMax != this.m_TextComponent.rectTransform.anchorMax || this.caretRectTrans.anchoredPosition != this.m_TextComponent.rectTransform.anchoredPosition || this.caretRectTrans.sizeDelta != this.m_TextComponent.rectTransform.sizeDelta || this.caretRectTrans.pivot != this.m_TextComponent.rectTransform.pivot))
			{
				this.caretRectTrans.localPosition = this.m_TextComponent.rectTransform.localPosition;
				this.caretRectTrans.localRotation = this.m_TextComponent.rectTransform.localRotation;
				this.caretRectTrans.localScale = this.m_TextComponent.rectTransform.localScale;
				this.caretRectTrans.anchorMin = this.m_TextComponent.rectTransform.anchorMin;
				this.caretRectTrans.anchorMax = this.m_TextComponent.rectTransform.anchorMax;
				this.caretRectTrans.anchoredPosition = this.m_TextComponent.rectTransform.anchoredPosition;
				this.caretRectTrans.sizeDelta = this.m_TextComponent.rectTransform.sizeDelta;
				this.caretRectTrans.pivot = this.m_TextComponent.rectTransform.pivot;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000BD48 File Offset: 0x00009F48
		private void OnFillVBO(Mesh vbo)
		{
			using (VertexHelper vertexHelper = new VertexHelper())
			{
				if (!this.isFocused)
				{
					vertexHelper.FillMesh(vbo);
				}
				else
				{
					Vector2 roundingOffset = this.m_TextComponent.PixelAdjustPoint(Vector2.zero);
					if (!this.hasSelection)
					{
						this.GenerateCaret(vertexHelper, roundingOffset);
					}
					else
					{
						this.GenerateHighlight(vertexHelper, roundingOffset);
					}
					vertexHelper.FillMesh(vbo);
				}
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000BDBC File Offset: 0x00009FBC
		private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
		{
			if (!this.m_CaretVisible)
			{
				return;
			}
			if (this.m_CursorVerts == null)
			{
				this.CreateCursorVerts();
			}
			float num = (float)this.m_CaretWidth;
			int num2 = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator == null)
			{
				return;
			}
			if (cachedTextGenerator.lineCount == 0)
			{
				return;
			}
			Vector2 zero = Vector2.zero;
			if (num2 < cachedTextGenerator.characters.Count)
			{
				UICharInfo uicharInfo = cachedTextGenerator.characters[num2];
				zero.x = uicharInfo.cursorPos.x;
			}
			zero.x /= this.m_TextComponent.pixelsPerUnit;
			if (zero.x > this.m_TextComponent.rectTransform.rect.xMax)
			{
				zero.x = this.m_TextComponent.rectTransform.rect.xMax;
			}
			int index = this.DetermineCharacterLine(num2, cachedTextGenerator);
			zero.y = cachedTextGenerator.lines[index].topY / this.m_TextComponent.pixelsPerUnit;
			float num3 = (float)cachedTextGenerator.lines[index].height / this.m_TextComponent.pixelsPerUnit;
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i].color = this.caretColor;
			}
			this.m_CursorVerts[0].position = new Vector3(zero.x, zero.y - num3, 0f);
			this.m_CursorVerts[1].position = new Vector3(zero.x + num, zero.y - num3, 0f);
			this.m_CursorVerts[2].position = new Vector3(zero.x + num, zero.y, 0f);
			this.m_CursorVerts[3].position = new Vector3(zero.x, zero.y, 0f);
			if (roundingOffset != Vector2.zero)
			{
				for (int j = 0; j < this.m_CursorVerts.Length; j++)
				{
					UIVertex uivertex = this.m_CursorVerts[j];
					uivertex.position.x = uivertex.position.x + roundingOffset.x;
					uivertex.position.y = uivertex.position.y + roundingOffset.y;
				}
			}
			vbo.AddUIVertexQuad(this.m_CursorVerts);
			int num4 = Screen.height;
			int targetDisplay = this.m_TextComponent.canvas.targetDisplay;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				num4 = Display.displays[targetDisplay].renderingHeight;
			}
			Camera cam;
			if (this.m_TextComponent.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				cam = null;
			}
			else
			{
				cam = this.m_TextComponent.canvas.worldCamera;
			}
			Vector3 worldPoint = this.m_CachedInputRenderer.gameObject.transform.TransformPoint(this.m_CursorVerts[0].position);
			Vector2 vector = RectTransformUtility.WorldToScreenPoint(cam, worldPoint);
			vector.y = (float)num4 - vector.y;
			if (this.input != null)
			{
				this.input.compositionCursorPos = vector;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C150 File Offset: 0x0000A350
		private void GenerateHighlight(VertexHelper vbo, Vector2 roundingOffset)
		{
			int num = Mathf.Max(0, this.caretPositionInternal - this.m_DrawStart);
			int num2 = Mathf.Max(0, this.caretSelectPositionInternal - this.m_DrawStart);
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			num2--;
			TextGenerator cachedTextGenerator = this.m_TextComponent.cachedTextGenerator;
			if (cachedTextGenerator.lineCount <= 0)
			{
				return;
			}
			int num4 = this.DetermineCharacterLine(num, cachedTextGenerator);
			int lineEndPosition = InputField.GetLineEndPosition(cachedTextGenerator, num4);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.uv0 = Vector2.zero;
			simpleVert.color = this.selectionColor;
			int num5 = num;
			while (num5 <= num2 && num5 < cachedTextGenerator.characterCount)
			{
				if (num5 == lineEndPosition || num5 == num2)
				{
					UICharInfo uicharInfo = cachedTextGenerator.characters[num];
					UICharInfo uicharInfo2 = cachedTextGenerator.characters[num5];
					Vector2 vector = new Vector2(uicharInfo.cursorPos.x / this.m_TextComponent.pixelsPerUnit, cachedTextGenerator.lines[num4].topY / this.m_TextComponent.pixelsPerUnit);
					Vector2 vector2 = new Vector2((uicharInfo2.cursorPos.x + uicharInfo2.charWidth) / this.m_TextComponent.pixelsPerUnit, vector.y - (float)cachedTextGenerator.lines[num4].height / this.m_TextComponent.pixelsPerUnit);
					if (vector2.x > this.m_TextComponent.rectTransform.rect.xMax || vector2.x < this.m_TextComponent.rectTransform.rect.xMin)
					{
						vector2.x = this.m_TextComponent.rectTransform.rect.xMax;
					}
					int currentVertCount = vbo.currentVertCount;
					simpleVert.position = new Vector3(vector.x, vector2.y, 0f) + roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector2.y, 0f) + roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector.y, 0f) + roundingOffset;
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector.x, vector.y, 0f) + roundingOffset;
					vbo.AddVert(simpleVert);
					vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					num = num5 + 1;
					num4++;
					lineEndPosition = InputField.GetLineEndPosition(cachedTextGenerator, num4);
				}
				num5++;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000C420 File Offset: 0x0000A620
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == InputField.CharacterValidation.Integer || this.characterValidation == InputField.CharacterValidation.Decimal)
			{
				int num = (pos == 0 && text.Length > 0 && text[0] == '-') ? 1 : 0;
				bool flag = text.Length > 0 && text[0] == '-' && ((this.caretPositionInternal == 0 && this.caretSelectPositionInternal > 0) || (this.caretSelectPositionInternal == 0 && this.caretPositionInternal > 0));
				bool flag2 = this.caretPositionInternal == 0 || this.caretSelectPositionInternal == 0;
				if (num == 0 || flag)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || flag2))
					{
						return ch;
					}
					if ((ch == '.' || ch == ',') && this.characterValidation == InputField.CharacterValidation.Decimal && text.IndexOfAny(new char[]
					{
						'.',
						','
					}) == -1)
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.Alphanumeric)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.Name)
			{
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && (pos == 0 || text[pos - 1] == ' ' || text[pos - 1] == '-'))
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && pos > 0 && text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')
					{
						return char.ToLower(ch);
					}
					return ch;
				}
				else
				{
					if (ch == '\'' && !text.Contains("'") && (pos <= 0 || (text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')) && (pos >= text.Length || (text[pos] != ' ' && text[pos] != '\'' && text[pos] != '-')))
					{
						return ch;
					}
					if ((ch == ' ' || ch == '-') && pos != 0 && (pos <= 0 || (text[pos - 1] != ' ' && text[pos - 1] != '\'' && text[pos - 1] != '-')) && (pos >= text.Length || (text[pos] != ' ' && text[pos] != '\'' && text[pos - 1] != '-')))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == InputField.CharacterValidation.EmailAddress)
			{
				if (ch >= 'A' && ch <= 'Z')
				{
					return ch;
				}
				if (ch >= 'a' && ch <= 'z')
				{
					return ch;
				}
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
				if (ch == '@' && text.IndexOf('@') == -1)
				{
					return ch;
				}
				if ("!#$%&'*+-/=?^_`{|}~".IndexOf(ch) != -1)
				{
					return ch;
				}
				if (ch == '.')
				{
					int num2 = (int)((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char c = (text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n';
					if (num2 != 46 && c != '.')
					{
						return ch;
					}
				}
			}
			return '\0';
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000C760 File Offset: 0x0000A960
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && this.m_Keyboard != null && !this.m_Keyboard.active)
			{
				this.m_Keyboard.active = true;
				this.m_Keyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		private void ActivateInputFieldInternal()
		{
			if (EventSystem.current == null)
			{
				return;
			}
			if (EventSystem.current.currentSelectedGameObject != base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
			this.m_TouchKeyboardAllowsInPlaceEditing = (!InputField.s_IsQuestDevice && TouchScreenKeyboard.isInPlaceEditingAllowed);
			if (this.TouchScreenKeyboardShouldBeUsed())
			{
				if (this.input != null && this.input.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				this.m_Keyboard = ((this.inputType == InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true, false, "", this.characterLimit) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == InputField.InputType.AutoCorrect, this.multiLine, false, false, "", this.characterLimit));
				if (!this.m_TouchKeyboardAllowsInPlaceEditing)
				{
					this.MoveTextEnd(false);
				}
			}
			if (!TouchScreenKeyboard.isSupported || this.m_TouchKeyboardAllowsInPlaceEditing)
			{
				if (this.input != null)
				{
					this.input.imeCompositionMode = IMECompositionMode.On;
				}
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C92F File Offset: 0x0000AB2F
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			if (this.shouldActivateOnSelect)
			{
				this.ActivateInputField();
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C946 File Offset: 0x0000AB46
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C958 File Offset: 0x0000AB58
		public void DeactivateInputField()
		{
			if (!this.m_AllowInput)
			{
				return;
			}
			this.m_HasDoneFocusTransition = false;
			this.m_AllowInput = false;
			if (this.m_Placeholder != null)
			{
				this.m_Placeholder.enabled = string.IsNullOrEmpty(this.m_Text);
			}
			if (this.m_TextComponent != null && this.IsInteractable())
			{
				if (this.m_WasCanceled)
				{
					this.text = this.m_OriginalText;
				}
				this.SendOnEndEdit();
				if (this.m_Keyboard != null)
				{
					this.m_Keyboard.active = false;
					this.m_Keyboard = null;
				}
				this.m_CaretPosition = (this.m_CaretSelectPosition = 0);
				if (this.input != null)
				{
					this.input.imeCompositionMode = IMECompositionMode.Auto;
				}
			}
			this.MarkGeometryAsDirty();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000CA1B File Offset: 0x0000AC1B
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField();
			base.OnDeselect(eventData);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000CA2A File Offset: 0x0000AC2A
		public virtual void OnSubmit(BaseEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (!this.isFocused)
			{
				this.m_ShouldActivateNextUpdate = true;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case InputField.ContentType.Standard:
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.Autocorrected:
				this.m_InputType = InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.IntegerNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				break;
			case InputField.ContentType.DecimalNumber:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = InputField.CharacterValidation.Decimal;
				break;
			case InputField.ContentType.Alphanumeric:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = InputField.CharacterValidation.Alphanumeric;
				break;
			case InputField.ContentType.Name:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NamePhonePad;
				this.m_CharacterValidation = InputField.CharacterValidation.Name;
				break;
			case InputField.ContentType.EmailAddress:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = InputField.CharacterValidation.EmailAddress;
				break;
			case InputField.ContentType.Password:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = InputField.CharacterValidation.None;
				break;
			case InputField.ContentType.Pin:
				this.m_LineType = InputField.LineType.SingleLine;
				this.m_InputType = InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = InputField.CharacterValidation.Integer;
				break;
			}
			this.EnforceTextHOverflow();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000CB9F File Offset: 0x0000AD9F
		private void EnforceTextHOverflow()
		{
			if (this.m_TextComponent != null)
			{
				if (this.multiLine)
				{
					this.m_TextComponent.horizontalOverflow = HorizontalWrapMode.Wrap;
					return;
				}
				this.m_TextComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		private void SetToCustomIfContentTypeIsNot(params InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			for (int i = 0; i < allowedContentTypes.Length; i++)
			{
				if (this.contentType == allowedContentTypes[i])
				{
					return;
				}
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000CC0A File Offset: 0x0000AE0A
		private void SetToCustom()
		{
			if (this.contentType == InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = InputField.ContentType.Custom;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000CC1F File Offset: 0x0000AE1F
		protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (this.m_HasDoneFocusTransition)
			{
				state = Selectable.SelectionState.Selected;
			}
			else if (state == Selectable.SelectionState.Pressed)
			{
				this.m_HasDoneFocusTransition = true;
			}
			base.DoStateTransition(state, instant);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000CC41 File Offset: 0x0000AE41
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000CC43 File Offset: 0x0000AE43
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000CC45 File Offset: 0x0000AE45
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		public virtual float preferredWidth
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				TextGenerationSettings generationSettings = this.textComponent.GetGenerationSettings(Vector2.zero);
				return this.textComponent.cachedTextGeneratorForLayout.GetPreferredWidth(this.m_Text, generationSettings) / this.textComponent.pixelsPerUnit;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000CCA1 File Offset: 0x0000AEA1
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
		public virtual float preferredHeight
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				TextGenerationSettings generationSettings = this.textComponent.GetGenerationSettings(new Vector2(this.textComponent.rectTransform.rect.size.x, 0f));
				return this.textComponent.cachedTextGeneratorForLayout.GetPreferredHeight(this.m_Text, generationSettings) / this.textComponent.pixelsPerUnit;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000CD27 File Offset: 0x0000AF27
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000CD2E File Offset: 0x0000AF2E
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000CD31 File Offset: 0x0000AF31
		// Note: this type is marked as 'beforefieldinit'.
		static InputField()
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000CD4F File Offset: 0x0000AF4F
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000085 RID: 133
		protected TouchScreenKeyboard m_Keyboard;

		// Token: 0x04000086 RID: 134
		private static readonly char[] kSeparators = new char[]
		{
			' ',
			'.',
			',',
			'\t',
			'\r',
			'\n'
		};

		// Token: 0x04000087 RID: 135
		private static bool s_IsQuestDevice = false;

		// Token: 0x04000088 RID: 136
		[SerializeField]
		[FormerlySerializedAs("text")]
		protected Text m_TextComponent;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x0400008A RID: 138
		[SerializeField]
		private InputField.ContentType m_ContentType;

		// Token: 0x0400008B RID: 139
		[FormerlySerializedAs("inputType")]
		[SerializeField]
		private InputField.InputType m_InputType;

		// Token: 0x0400008C RID: 140
		[FormerlySerializedAs("asteriskChar")]
		[SerializeField]
		private char m_AsteriskChar = '*';

		// Token: 0x0400008D RID: 141
		[FormerlySerializedAs("keyboardType")]
		[SerializeField]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x0400008E RID: 142
		[SerializeField]
		private InputField.LineType m_LineType;

		// Token: 0x0400008F RID: 143
		[FormerlySerializedAs("hideMobileInput")]
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x04000090 RID: 144
		[FormerlySerializedAs("validation")]
		[SerializeField]
		private InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x04000091 RID: 145
		[FormerlySerializedAs("characterLimit")]
		[SerializeField]
		private int m_CharacterLimit;

		// Token: 0x04000092 RID: 146
		[FormerlySerializedAs("onSubmit")]
		[FormerlySerializedAs("m_OnSubmit")]
		[FormerlySerializedAs("m_EndEdit")]
		[FormerlySerializedAs("m_OnEndEdit")]
		[SerializeField]
		private InputField.SubmitEvent m_OnSubmit = new InputField.SubmitEvent();

		// Token: 0x04000093 RID: 147
		[SerializeField]
		private InputField.EndEditEvent m_OnDidEndEdit = new InputField.EndEditEvent();

		// Token: 0x04000094 RID: 148
		[FormerlySerializedAs("onValueChange")]
		[FormerlySerializedAs("m_OnValueChange")]
		[SerializeField]
		private InputField.OnChangeEvent m_OnValueChanged = new InputField.OnChangeEvent();

		// Token: 0x04000095 RID: 149
		[FormerlySerializedAs("onValidateInput")]
		[SerializeField]
		private InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x04000096 RID: 150
		[FormerlySerializedAs("selectionColor")]
		[SerializeField]
		private Color m_CaretColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x04000097 RID: 151
		[SerializeField]
		private bool m_CustomCaretColor;

		// Token: 0x04000098 RID: 152
		[SerializeField]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x04000099 RID: 153
		[SerializeField]
		[Multiline]
		[FormerlySerializedAs("mValue")]
		protected string m_Text = string.Empty;

		// Token: 0x0400009A RID: 154
		[SerializeField]
		[Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		// Token: 0x0400009B RID: 155
		[SerializeField]
		[Range(1f, 5f)]
		private int m_CaretWidth = 1;

		// Token: 0x0400009C RID: 156
		[SerializeField]
		private bool m_ReadOnly;

		// Token: 0x0400009D RID: 157
		[SerializeField]
		private bool m_ShouldActivateOnSelect = true;

		// Token: 0x0400009E RID: 158
		protected int m_CaretPosition;

		// Token: 0x0400009F RID: 159
		protected int m_CaretSelectPosition;

		// Token: 0x040000A0 RID: 160
		private RectTransform caretRectTrans;

		// Token: 0x040000A1 RID: 161
		protected UIVertex[] m_CursorVerts;

		// Token: 0x040000A2 RID: 162
		private TextGenerator m_InputTextCache;

		// Token: 0x040000A3 RID: 163
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x040000A4 RID: 164
		private bool m_PreventFontCallback;

		// Token: 0x040000A5 RID: 165
		[NonSerialized]
		protected Mesh m_Mesh;

		// Token: 0x040000A6 RID: 166
		private bool m_AllowInput;

		// Token: 0x040000A7 RID: 167
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x040000A8 RID: 168
		private bool m_UpdateDrag;

		// Token: 0x040000A9 RID: 169
		private bool m_DragPositionOutOfBounds;

		// Token: 0x040000AA RID: 170
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x040000AB RID: 171
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x040000AC RID: 172
		protected bool m_CaretVisible;

		// Token: 0x040000AD RID: 173
		private Coroutine m_BlinkCoroutine;

		// Token: 0x040000AE RID: 174
		private float m_BlinkStartTime;

		// Token: 0x040000AF RID: 175
		protected int m_DrawStart;

		// Token: 0x040000B0 RID: 176
		protected int m_DrawEnd;

		// Token: 0x040000B1 RID: 177
		private Coroutine m_DragCoroutine;

		// Token: 0x040000B2 RID: 178
		private string m_OriginalText = "";

		// Token: 0x040000B3 RID: 179
		private bool m_WasCanceled;

		// Token: 0x040000B4 RID: 180
		private bool m_HasDoneFocusTransition;

		// Token: 0x040000B5 RID: 181
		private WaitForSecondsRealtime m_WaitForSecondsRealtime;

		// Token: 0x040000B6 RID: 182
		private bool m_TouchKeyboardAllowsInPlaceEditing;

		// Token: 0x040000B7 RID: 183
		private bool m_IsCompositionActive;

		// Token: 0x040000B8 RID: 184
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x040000B9 RID: 185
		private const string kOculusQuestDeviceModel = "Oculus Quest";

		// Token: 0x040000BA RID: 186
		private Event m_ProcessingEvent = new Event();

		// Token: 0x040000BB RID: 187
		private const int k_MaxTextLength = 16382;

		// Token: 0x0200008A RID: 138
		public enum ContentType
		{
			// Token: 0x04000281 RID: 641
			Standard,
			// Token: 0x04000282 RID: 642
			Autocorrected,
			// Token: 0x04000283 RID: 643
			IntegerNumber,
			// Token: 0x04000284 RID: 644
			DecimalNumber,
			// Token: 0x04000285 RID: 645
			Alphanumeric,
			// Token: 0x04000286 RID: 646
			Name,
			// Token: 0x04000287 RID: 647
			EmailAddress,
			// Token: 0x04000288 RID: 648
			Password,
			// Token: 0x04000289 RID: 649
			Pin,
			// Token: 0x0400028A RID: 650
			Custom
		}

		// Token: 0x0200008B RID: 139
		public enum InputType
		{
			// Token: 0x0400028C RID: 652
			Standard,
			// Token: 0x0400028D RID: 653
			AutoCorrect,
			// Token: 0x0400028E RID: 654
			Password
		}

		// Token: 0x0200008C RID: 140
		public enum CharacterValidation
		{
			// Token: 0x04000290 RID: 656
			None,
			// Token: 0x04000291 RID: 657
			Integer,
			// Token: 0x04000292 RID: 658
			Decimal,
			// Token: 0x04000293 RID: 659
			Alphanumeric,
			// Token: 0x04000294 RID: 660
			Name,
			// Token: 0x04000295 RID: 661
			EmailAddress
		}

		// Token: 0x0200008D RID: 141
		public enum LineType
		{
			// Token: 0x04000297 RID: 663
			SingleLine,
			// Token: 0x04000298 RID: 664
			MultiLineSubmit,
			// Token: 0x04000299 RID: 665
			MultiLineNewline
		}

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x060006B9 RID: 1721
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		// Token: 0x0200008F RID: 143
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
			// Token: 0x060006BC RID: 1724 RVA: 0x0001BD45 File Offset: 0x00019F45
			public SubmitEvent()
			{
			}
		}

		// Token: 0x02000090 RID: 144
		[Serializable]
		public class EndEditEvent : UnityEvent<string>
		{
			// Token: 0x060006BD RID: 1725 RVA: 0x0001BD4D File Offset: 0x00019F4D
			public EndEditEvent()
			{
			}
		}

		// Token: 0x02000091 RID: 145
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
			// Token: 0x060006BE RID: 1726 RVA: 0x0001BD55 File Offset: 0x00019F55
			public OnChangeEvent()
			{
			}
		}

		// Token: 0x02000092 RID: 146
		protected enum EditState
		{
			// Token: 0x0400029B RID: 667
			Continue,
			// Token: 0x0400029C RID: 668
			Finish
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		private sealed class <CaretBlink>d__170 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060006BF RID: 1727 RVA: 0x0001BD5D File Offset: 0x00019F5D
			[DebuggerHidden]
			public <CaretBlink>d__170(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x0001BD6C File Offset: 0x00019F6C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x0001BD70 File Offset: 0x00019F70
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				InputField inputField = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					inputField.m_CaretVisible = true;
					this.<>2__current = null;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (!inputField.isFocused || inputField.m_CaretBlinkRate <= 0f)
				{
					inputField.m_BlinkCoroutine = null;
					return false;
				}
				float num2 = 1f / inputField.m_CaretBlinkRate;
				bool flag = (Time.unscaledTime - inputField.m_BlinkStartTime) % num2 < num2 / 2f;
				if (inputField.m_CaretVisible != flag)
				{
					inputField.m_CaretVisible = flag;
					if (!inputField.hasSelection)
					{
						inputField.MarkGeometryAsDirty();
					}
				}
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0001BE3D File Offset: 0x0001A03D
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x0001BE45 File Offset: 0x0001A045
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001BE4C File Offset: 0x0001A04C
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400029D RID: 669
			private int <>1__state;

			// Token: 0x0400029E RID: 670
			private object <>2__current;

			// Token: 0x0400029F RID: 671
			public InputField <>4__this;
		}

		// Token: 0x02000094 RID: 148
		[CompilerGenerated]
		private sealed class <MouseDragOutsideRect>d__192 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060006C5 RID: 1733 RVA: 0x0001BE54 File Offset: 0x0001A054
			[DebuggerHidden]
			public <MouseDragOutsideRect>d__192(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x0001BE63 File Offset: 0x0001A063
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060006C7 RID: 1735 RVA: 0x0001BE68 File Offset: 0x0001A068
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				InputField inputField = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
				}
				if (inputField.m_UpdateDrag && inputField.m_DragPositionOutOfBounds)
				{
					Vector2 zero = Vector2.zero;
					if (MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
					{
						Vector2 vector;
						RectTransformUtility.ScreenPointToLocalPointInRectangle(inputField.textComponent.rectTransform, zero, eventData.pressEventCamera, out vector);
						Rect rect = inputField.textComponent.rectTransform.rect;
						if (inputField.multiLine)
						{
							if (vector.y > rect.yMax)
							{
								inputField.MoveUp(true, true);
							}
							else if (vector.y < rect.yMin)
							{
								inputField.MoveDown(true, true);
							}
						}
						else if (vector.x < rect.xMin)
						{
							inputField.MoveLeft(true, false);
						}
						else if (vector.x > rect.xMax)
						{
							inputField.MoveRight(true, false);
						}
						inputField.UpdateLabel();
						float num2 = inputField.multiLine ? 0.1f : 0.05f;
						if (inputField.m_WaitForSecondsRealtime == null)
						{
							inputField.m_WaitForSecondsRealtime = new WaitForSecondsRealtime(num2);
						}
						else
						{
							inputField.m_WaitForSecondsRealtime.waitTime = num2;
						}
						this.<>2__current = inputField.m_WaitForSecondsRealtime;
						this.<>1__state = 1;
						return true;
					}
				}
				inputField.m_DragCoroutine = null;
				return false;
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001BFC5 File Offset: 0x0001A1C5
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x0001BFCD File Offset: 0x0001A1CD
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040002A0 RID: 672
			private int <>1__state;

			// Token: 0x040002A1 RID: 673
			private object <>2__current;

			// Token: 0x040002A2 RID: 674
			public PointerEventData eventData;

			// Token: 0x040002A3 RID: 675
			public InputField <>4__this;
		}
	}
}
