using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200003F RID: 63
	[AddComponentMenu("UI/TextMeshPro - Input Field", 11)]
	public class TMP_InputField : Selectable, IUpdateSelectedHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement, ILayoutElement, IScrollHandler
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0001D0FC File Offset: 0x0001B2FC
		private BaseInput inputSystem
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0001D12C File Offset: 0x0001B32C
		private string compositionString
		{
			get
			{
				if (!(this.inputSystem != null))
				{
					return Input.compositionString;
				}
				return this.inputSystem.compositionString;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0001D14D File Offset: 0x0001B34D
		private int compositionLength
		{
			get
			{
				if (this.m_ReadOnly)
				{
					return 0;
				}
				return this.compositionString.Length;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001D164 File Offset: 0x0001B364
		protected TMP_InputField()
		{
			this.SetTextComponentWrapMode();
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0001D296 File Offset: 0x0001B496
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0001D2B8 File Offset: 0x0001B4B8
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		public bool shouldHideMobileInput
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform != RuntimePlatform.tvOS) || this.m_HideMobileInput;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.Android || platform == RuntimePlatform.tvOS)
				{
					SetPropertyUtility.SetStruct<bool>(ref this.m_HideMobileInput, value);
					return;
				}
				this.m_HideMobileInput = true;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0001D31C File Offset: 0x0001B51C
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0001D36C File Offset: 0x0001B56C
		public bool shouldHideSoftKeyboard
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.MetroPlayerARM)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform - RuntimePlatform.MetroPlayerX86 > 2)
					{
						return true;
					}
				}
				else if (platform <= RuntimePlatform.Switch)
				{
					if (platform != RuntimePlatform.PS4 && platform - RuntimePlatform.tvOS > 1)
					{
						return true;
					}
				}
				else if (platform != RuntimePlatform.Stadia && platform != RuntimePlatform.PS5)
				{
					return true;
				}
				return this.m_HideSoftKeyboard;
			}
			set
			{
				RuntimePlatform platform = Application.platform;
				if (platform <= RuntimePlatform.MetroPlayerARM)
				{
					if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android && platform - RuntimePlatform.MetroPlayerX86 > 2)
					{
						goto IL_49;
					}
				}
				else if (platform <= RuntimePlatform.Switch)
				{
					if (platform != RuntimePlatform.PS4 && platform - RuntimePlatform.tvOS > 1)
					{
						goto IL_49;
					}
				}
				else if (platform != RuntimePlatform.Stadia && platform != RuntimePlatform.PS5)
				{
					goto IL_49;
				}
				SetPropertyUtility.SetStruct<bool>(ref this.m_HideSoftKeyboard, value);
				goto IL_50;
				IL_49:
				this.m_HideSoftKeyboard = true;
				IL_50:
				if (this.m_HideSoftKeyboard && this.m_SoftKeyboard != null && TouchScreenKeyboard.isSupported && this.m_SoftKeyboard.active)
				{
					this.m_SoftKeyboard.active = false;
					this.m_SoftKeyboard = null;
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0001D400 File Offset: 0x0001B600
		private bool isKeyboardUsingEvents()
		{
			RuntimePlatform platform = Application.platform;
			if (platform <= RuntimePlatform.Android)
			{
				if (platform != RuntimePlatform.IPhonePlayer && platform != RuntimePlatform.Android)
				{
					return true;
				}
			}
			else if (platform != RuntimePlatform.PS4 && platform - RuntimePlatform.tvOS > 1 && platform != RuntimePlatform.PS5)
			{
				return true;
			}
			return false;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0001D437 File Offset: 0x0001B637
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0001D43F File Offset: 0x0001B63F
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

		// Token: 0x06000251 RID: 593 RVA: 0x0001D449 File Offset: 0x0001B649
		public void SetTextWithoutNotify(string input)
		{
			this.SetText(input, false);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0001D454 File Offset: 0x0001B654
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
			this.m_Text = value;
			if (this.m_SoftKeyboard != null)
			{
				this.m_SoftKeyboard.text = this.m_Text;
			}
			if (this.m_StringPosition > this.m_Text.Length)
			{
				this.m_StringPosition = (this.m_StringSelectPosition = this.m_Text.Length);
			}
			else if (this.m_StringSelectPosition > this.m_Text.Length)
			{
				this.m_StringSelectPosition = this.m_Text.Length;
			}
			this.m_forceRectTransformAdjustment = true;
			this.m_IsTextComponentUpdateRequired = true;
			this.UpdateLabel();
			if (sendCallback)
			{
				this.SendOnValueChanged();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0001D51C File Offset: 0x0001B71C
		public bool isFocused
		{
			get
			{
				return this.m_AllowInput;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0001D524 File Offset: 0x0001B724
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0001D52C File Offset: 0x0001B72C
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0001D54A File Offset: 0x0001B74A
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0001D552 File Offset: 0x0001B752
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0001D568 File Offset: 0x0001B768
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0001D570 File Offset: 0x0001B770
		public RectTransform textViewport
		{
			get
			{
				return this.m_TextViewport;
			}
			set
			{
				SetPropertyUtility.SetClass<RectTransform>(ref this.m_TextViewport, value);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0001D57F File Offset: 0x0001B77F
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0001D587 File Offset: 0x0001B787
		public TMP_Text textComponent
		{
			get
			{
				return this.m_TextComponent;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_Text>(ref this.m_TextComponent, value))
				{
					this.SetTextComponentWrapMode();
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0001D59D File Offset: 0x0001B79D
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0001D5A5 File Offset: 0x0001B7A5
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0001D5B4 File Offset: 0x0001B7B4
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0001D5BC File Offset: 0x0001B7BC
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
				SetPropertyUtility.SetClass<Scrollbar>(ref this.m_VerticalScrollbar, value);
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0001D629 File Offset: 0x0001B829
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0001D631 File Offset: 0x0001B831
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_ScrollSensitivity, value))
				{
					this.MarkGeometryAsDirty();
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0001D647 File Offset: 0x0001B847
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0001D663 File Offset: 0x0001B863
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0001D679 File Offset: 0x0001B879
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0001D681 File Offset: 0x0001B881
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0001D699 File Offset: 0x0001B899
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0001D6A1 File Offset: 0x0001B8A1
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0001D6B7 File Offset: 0x0001B8B7
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0001D6BF File Offset: 0x0001B8BF
		public TMP_InputField.SubmitEvent onEndEdit
		{
			get
			{
				return this.m_OnEndEdit;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SubmitEvent>(ref this.m_OnEndEdit, value);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0001D6CE File Offset: 0x0001B8CE
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0001D6D6 File Offset: 0x0001B8D6
		public TMP_InputField.SubmitEvent onSubmit
		{
			get
			{
				return this.m_OnSubmit;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SubmitEvent>(ref this.m_OnSubmit, value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0001D6E5 File Offset: 0x0001B8E5
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0001D6ED File Offset: 0x0001B8ED
		public TMP_InputField.SelectionEvent onSelect
		{
			get
			{
				return this.m_OnSelect;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SelectionEvent>(ref this.m_OnSelect, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0001D704 File Offset: 0x0001B904
		public TMP_InputField.SelectionEvent onDeselect
		{
			get
			{
				return this.m_OnDeselect;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.SelectionEvent>(ref this.m_OnDeselect, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0001D713 File Offset: 0x0001B913
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0001D71B File Offset: 0x0001B91B
		public TMP_InputField.TextSelectionEvent onTextSelection
		{
			get
			{
				return this.m_OnTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TextSelectionEvent>(ref this.m_OnTextSelection, value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0001D72A File Offset: 0x0001B92A
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0001D732 File Offset: 0x0001B932
		public TMP_InputField.TextSelectionEvent onEndTextSelection
		{
			get
			{
				return this.m_OnEndTextSelection;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TextSelectionEvent>(ref this.m_OnEndTextSelection, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0001D741 File Offset: 0x0001B941
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0001D749 File Offset: 0x0001B949
		public TMP_InputField.OnChangeEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.OnChangeEvent>(ref this.m_OnValueChanged, value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0001D758 File Offset: 0x0001B958
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0001D760 File Offset: 0x0001B960
		public TMP_InputField.TouchScreenKeyboardEvent onTouchScreenKeyboardStatusChanged
		{
			get
			{
				return this.m_OnTouchScreenKeyboardStatusChanged;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.TouchScreenKeyboardEvent>(ref this.m_OnTouchScreenKeyboardStatusChanged, value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0001D76F File Offset: 0x0001B96F
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0001D777 File Offset: 0x0001B977
		public TMP_InputField.OnValidateInput onValidateInput
		{
			get
			{
				return this.m_OnValidateInput;
			}
			set
			{
				SetPropertyUtility.SetClass<TMP_InputField.OnValidateInput>(ref this.m_OnValidateInput, value);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0001D786 File Offset: 0x0001B986
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0001D78E File Offset: 0x0001B98E
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
					if (this.m_SoftKeyboard != null)
					{
						this.m_SoftKeyboard.characterLimit = value;
					}
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0001D7BE File Offset: 0x0001B9BE
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0001D7C6 File Offset: 0x0001B9C6
		public float pointSize
		{
			get
			{
				return this.m_GlobalPointSize;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_GlobalPointSize, Math.Max(0f, value)))
				{
					this.SetGlobalPointSize(this.m_GlobalPointSize);
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0001D7F2 File Offset: 0x0001B9F2
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0001D7FA File Offset: 0x0001B9FA
		public TMP_FontAsset fontAsset
		{
			get
			{
				return this.m_GlobalFontAsset;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_FontAsset>(ref this.m_GlobalFontAsset, value))
				{
					this.SetGlobalFontAsset(this.m_GlobalFontAsset);
					this.UpdateLabel();
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0001D81C File Offset: 0x0001BA1C
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0001D824 File Offset: 0x0001BA24
		public bool onFocusSelectAll
		{
			get
			{
				return this.m_OnFocusSelectAll;
			}
			set
			{
				this.m_OnFocusSelectAll = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0001D82D File Offset: 0x0001BA2D
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0001D835 File Offset: 0x0001BA35
		public bool resetOnDeActivation
		{
			get
			{
				return this.m_ResetOnDeActivation;
			}
			set
			{
				this.m_ResetOnDeActivation = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0001D83E File Offset: 0x0001BA3E
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0001D846 File Offset: 0x0001BA46
		public bool restoreOriginalTextOnEscape
		{
			get
			{
				return this.m_RestoreOriginalTextOnEscape;
			}
			set
			{
				this.m_RestoreOriginalTextOnEscape = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0001D84F File Offset: 0x0001BA4F
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0001D857 File Offset: 0x0001BA57
		public bool isRichTextEditingAllowed
		{
			get
			{
				return this.m_isRichTextEditingAllowed;
			}
			set
			{
				this.m_isRichTextEditingAllowed = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0001D860 File Offset: 0x0001BA60
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0001D868 File Offset: 0x0001BA68
		public TMP_InputField.ContentType contentType
		{
			get
			{
				return this.m_ContentType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.ContentType>(ref this.m_ContentType, value))
				{
					this.EnforceContentType();
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0001D87E File Offset: 0x0001BA7E
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0001D886 File Offset: 0x0001BA86
		public TMP_InputField.LineType lineType
		{
			get
			{
				return this.m_LineType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.LineType>(ref this.m_LineType, value))
				{
					this.SetToCustomIfContentTypeIsNot(new TMP_InputField.ContentType[]
					{
						TMP_InputField.ContentType.Standard,
						TMP_InputField.ContentType.Autocorrected
					});
					this.SetTextComponentWrapMode();
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0001D8AC File Offset: 0x0001BAAC
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0001D8B4 File Offset: 0x0001BAB4
		public int lineLimit
		{
			get
			{
				return this.m_LineLimit;
			}
			set
			{
				if (this.m_LineType == TMP_InputField.LineType.SingleLine)
				{
					this.m_LineLimit = 1;
					return;
				}
				SetPropertyUtility.SetStruct<int>(ref this.m_LineLimit, value);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0001D8D3 File Offset: 0x0001BAD3
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0001D8DB File Offset: 0x0001BADB
		public TMP_InputField.InputType inputType
		{
			get
			{
				return this.m_InputType;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.InputType>(ref this.m_InputType, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0001D8F1 File Offset: 0x0001BAF1
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0001D8F9 File Offset: 0x0001BAF9
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

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0001D90F File Offset: 0x0001BB0F
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0001D917 File Offset: 0x0001BB17
		public TMP_InputField.CharacterValidation characterValidation
		{
			get
			{
				return this.m_CharacterValidation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<TMP_InputField.CharacterValidation>(ref this.m_CharacterValidation, value))
				{
					this.SetToCustom();
				}
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0001D92D File Offset: 0x0001BB2D
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0001D935 File Offset: 0x0001BB35
		public TMP_InputValidator inputValidator
		{
			get
			{
				return this.m_InputValidator;
			}
			set
			{
				if (SetPropertyUtility.SetClass<TMP_InputValidator>(ref this.m_InputValidator, value))
				{
					this.SetToCustom(TMP_InputField.CharacterValidation.CustomValidator);
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0001D94C File Offset: 0x0001BB4C
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0001D954 File Offset: 0x0001BB54
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

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0001D95D File Offset: 0x0001BB5D
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0001D965 File Offset: 0x0001BB65
		public bool richText
		{
			get
			{
				return this.m_RichText;
			}
			set
			{
				this.m_RichText = value;
				this.SetTextComponentRichTextMode();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0001D974 File Offset: 0x0001BB74
		public bool multiLine
		{
			get
			{
				return this.m_LineType == TMP_InputField.LineType.MultiLineNewline || this.lineType == TMP_InputField.LineType.MultiLineSubmit;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0001D98A File Offset: 0x0001BB8A
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0001D992 File Offset: 0x0001BB92
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		public bool wasCanceled
		{
			get
			{
				return this.m_WasCanceled;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		protected void ClampStringPos(ref int pos)
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

		// Token: 0x0600029F RID: 671 RVA: 0x0001D9D7 File Offset: 0x0001BBD7
		protected void ClampCaretPos(ref int pos)
		{
			if (pos < 0)
			{
				pos = 0;
				return;
			}
			if (pos > this.m_TextComponent.textInfo.characterCount - 1)
			{
				pos = this.m_TextComponent.textInfo.characterCount - 1;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0001DA0C File Offset: 0x0001BC0C
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0001DA1B File Offset: 0x0001BC1B
		protected int caretPositionInternal
		{
			get
			{
				return this.m_CaretPosition + this.compositionLength;
			}
			set
			{
				this.m_CaretPosition = value;
				this.ClampCaretPos(ref this.m_CaretPosition);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0001DA30 File Offset: 0x0001BC30
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0001DA3F File Offset: 0x0001BC3F
		protected int stringPositionInternal
		{
			get
			{
				return this.m_StringPosition + this.compositionLength;
			}
			set
			{
				this.m_StringPosition = value;
				this.ClampStringPos(ref this.m_StringPosition);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0001DA54 File Offset: 0x0001BC54
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0001DA63 File Offset: 0x0001BC63
		protected int caretSelectPositionInternal
		{
			get
			{
				return this.m_CaretSelectPosition + this.compositionLength;
			}
			set
			{
				this.m_CaretSelectPosition = value;
				this.ClampCaretPos(ref this.m_CaretSelectPosition);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0001DA78 File Offset: 0x0001BC78
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0001DA87 File Offset: 0x0001BC87
		protected int stringSelectPositionInternal
		{
			get
			{
				return this.m_StringSelectPosition + this.compositionLength;
			}
			set
			{
				this.m_StringSelectPosition = value;
				this.ClampStringPos(ref this.m_StringSelectPosition);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0001DA9C File Offset: 0x0001BC9C
		private bool hasSelection
		{
			get
			{
				return this.stringPositionInternal != this.stringSelectPositionInternal;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0001DAAF File Offset: 0x0001BCAF
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
		public int caretPosition
		{
			get
			{
				return this.caretSelectPositionInternal;
			}
			set
			{
				this.selectionAnchorPosition = value;
				this.selectionFocusPosition = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0001DACE File Offset: 0x0001BCCE
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0001DAD6 File Offset: 0x0001BCD6
		public int selectionAnchorPosition
		{
			get
			{
				return this.caretPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.caretPositionInternal = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0001DAEF File Offset: 0x0001BCEF
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0001DAF7 File Offset: 0x0001BCF7
		public int selectionFocusPosition
		{
			get
			{
				return this.caretSelectPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.caretSelectPositionInternal = value;
				this.m_IsStringPositionDirty = true;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0001DB10 File Offset: 0x0001BD10
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0001DB18 File Offset: 0x0001BD18
		public int stringPosition
		{
			get
			{
				return this.stringSelectPositionInternal;
			}
			set
			{
				this.selectionStringAnchorPosition = value;
				this.selectionStringFocusPosition = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0001DB2F File Offset: 0x0001BD2F
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0001DB37 File Offset: 0x0001BD37
		public int selectionStringAnchorPosition
		{
			get
			{
				return this.stringPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.stringPositionInternal = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0001DB50 File Offset: 0x0001BD50
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0001DB58 File Offset: 0x0001BD58
		public int selectionStringFocusPosition
		{
			get
			{
				return this.stringSelectPositionInternal;
			}
			set
			{
				if (this.compositionLength != 0)
				{
					return;
				}
				this.stringSelectPositionInternal = value;
				this.m_IsCaretPositionDirty = true;
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0001DB74 File Offset: 0x0001BD74
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_Text == null)
			{
				this.m_Text = string.Empty;
			}
			if (base.GetComponent<ILayoutController>() != null)
			{
				this.m_IsDrivenByLayoutComponents = true;
				this.m_LayoutGroup = base.GetComponent<LayoutGroup>();
			}
			else
			{
				this.m_IsDrivenByLayoutComponents = false;
			}
			if (Application.isPlaying && this.m_CachedInputRenderer == null && this.m_TextComponent != null)
			{
				GameObject gameObject = new GameObject("Caret", new Type[]
				{
					typeof(TMP_SelectionCaret)
				});
				gameObject.hideFlags = HideFlags.DontSave;
				gameObject.transform.SetParent(this.m_TextComponent.transform.parent);
				gameObject.transform.SetAsFirstSibling();
				gameObject.layer = base.gameObject.layer;
				this.caretRectTrans = gameObject.GetComponent<RectTransform>();
				this.m_CachedInputRenderer = gameObject.GetComponent<CanvasRenderer>();
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				this.AssignPositioningIfNeeded();
			}
			this.m_RectTransform = base.GetComponent<RectTransform>();
			IScrollHandler[] componentsInParent = base.GetComponentsInParent<IScrollHandler>();
			if (componentsInParent.Length > 1)
			{
				this.m_IScrollHandlerParent = (componentsInParent[1] as ScrollRect);
			}
			if (this.m_TextViewport != null)
			{
				this.m_TextViewportRectMask = this.m_TextViewport.GetComponent<RectMask2D>();
				this.UpdateMaskRegions();
			}
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.SetMaterial(Graphic.defaultGraphicMaterial, Texture2D.whiteTexture);
			}
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.RegisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
				this.UpdateLabel();
			}
			TMPro_EventManager.TEXT_CHANGED_EVENT.Add(new Action<UnityEngine.Object>(this.ON_TEXT_CHANGED));
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0001DD78 File Offset: 0x0001BF78
		protected override void OnDisable()
		{
			this.m_BlinkCoroutine = null;
			this.DeactivateInputField(false);
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.MarkGeometryAsDirty));
				this.m_TextComponent.UnregisterDirtyVerticesCallback(new UnityAction(this.UpdateLabel));
				if (this.m_VerticalScrollbar != null)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.OnScrollbarValueChange));
				}
			}
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_CachedInputRenderer != null)
			{
				this.m_CachedInputRenderer.Clear();
			}
			if (this.m_Mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_Mesh);
			}
			this.m_Mesh = null;
			TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(new Action<UnityEngine.Object>(this.ON_TEXT_CHANGED));
			base.OnDisable();
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001DE54 File Offset: 0x0001C054
		private void ON_TEXT_CHANGED(UnityEngine.Object obj)
		{
			if (obj == this.m_TextComponent)
			{
				if (Application.isPlaying && this.compositionLength == 0)
				{
					this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
					this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				}
				if (this.m_VerticalScrollbar)
				{
					this.UpdateScrollbar();
				}
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0001DEB5 File Offset: 0x0001C0B5
		private IEnumerator CaretBlink()
		{
			this.m_CaretVisible = true;
			yield return null;
			while ((this.isFocused || this.m_SelectionStillActive) && this.m_CaretBlinkRate > 0f)
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

		// Token: 0x060002B9 RID: 697 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
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

		// Token: 0x060002BA RID: 698 RVA: 0x0001DEE7 File Offset: 0x0001C0E7
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

		// Token: 0x060002BB RID: 699 RVA: 0x0001DF21 File Offset: 0x0001C121
		protected void OnFocus()
		{
			if (this.m_OnFocusSelectAll)
			{
				this.SelectAll();
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001DF31 File Offset: 0x0001C131
		protected void SelectAll()
		{
			this.m_isSelectAll = true;
			this.stringPositionInternal = this.text.Length;
			this.stringSelectPositionInternal = 0;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001DF54 File Offset: 0x0001C154
		public void MoveTextEnd(bool shift)
		{
			if (this.m_isRichTextEditingAllowed)
			{
				int length = this.text.Length;
				if (shift)
				{
					this.stringSelectPositionInternal = length;
				}
				else
				{
					this.stringPositionInternal = length;
					this.stringSelectPositionInternal = this.stringPositionInternal;
				}
			}
			else
			{
				int num = this.m_TextComponent.textInfo.characterCount - 1;
				if (shift)
				{
					this.caretSelectPositionInternal = num;
					this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(num);
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = num);
					this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(num));
				}
			}
			this.UpdateLabel();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001DFF0 File Offset: 0x0001C1F0
		public void MoveTextStart(bool shift)
		{
			if (this.m_isRichTextEditingAllowed)
			{
				int num = 0;
				if (shift)
				{
					this.stringSelectPositionInternal = num;
				}
				else
				{
					this.stringPositionInternal = num;
					this.stringSelectPositionInternal = this.stringPositionInternal;
				}
			}
			else
			{
				int num2 = 0;
				if (shift)
				{
					this.caretSelectPositionInternal = num2;
					this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(num2);
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = num2);
					this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(num2));
				}
			}
			this.UpdateLabel();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001E070 File Offset: 0x0001C270
		public void MoveToEndOfLine(bool shift, bool ctrl)
		{
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			int num = ctrl ? (this.m_TextComponent.textInfo.characterCount - 1) : this.m_TextComponent.textInfo.lineInfo[lineNumber].lastCharacterIndex;
			int index = this.m_TextComponent.textInfo.characterInfo[num].index;
			if (shift)
			{
				this.stringSelectPositionInternal = index;
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.stringPositionInternal = index;
				this.stringSelectPositionInternal = this.stringPositionInternal;
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			}
			this.UpdateLabel();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0001E12C File Offset: 0x0001C32C
		public void MoveToStartOfLine(bool shift, bool ctrl)
		{
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			int num = ctrl ? 0 : this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex;
			int num2 = 0;
			if (num > 0)
			{
				num2 = this.m_TextComponent.textInfo.characterInfo[num - 1].index + this.m_TextComponent.textInfo.characterInfo[num - 1].stringLength;
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num2;
				this.caretSelectPositionInternal = num;
			}
			else
			{
				this.stringPositionInternal = num2;
				this.stringSelectPositionInternal = this.stringPositionInternal;
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			}
			this.UpdateLabel();
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0001E1FA File Offset: 0x0001C3FA
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0001E201 File Offset: 0x0001C401
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

		// Token: 0x060002C3 RID: 707 RVA: 0x0001E20C File Offset: 0x0001C40C
		private bool InPlaceEditing()
		{
			if (Application.platform == RuntimePlatform.MetroPlayerX86 || Application.platform == RuntimePlatform.MetroPlayerX64 || Application.platform == RuntimePlatform.MetroPlayerARM)
			{
				return !TouchScreenKeyboard.isSupported || this.m_TouchKeyboardAllowsInPlaceEditing;
			}
			return (TouchScreenKeyboard.isSupported && this.shouldHideSoftKeyboard) || !TouchScreenKeyboard.isSupported || this.shouldHideSoftKeyboard || this.shouldHideMobileInput;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001E270 File Offset: 0x0001C470
		private void UpdateStringPositionFromKeyboard()
		{
			RangeInt selection = this.m_SoftKeyboard.selection;
			int start = selection.start;
			int end = selection.end;
			bool flag = false;
			if (this.stringPositionInternal != start)
			{
				flag = true;
				this.stringPositionInternal = start;
				this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			}
			if (this.stringSelectPositionInternal != end)
			{
				this.stringSelectPositionInternal = end;
				flag = true;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			}
			if (flag)
			{
				this.m_BlinkStartTime = Time.unscaledTime;
				this.UpdateLabel();
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001E2F8 File Offset: 0x0001C4F8
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
			if (!this.isFocused && this.m_SelectionStillActive)
			{
				GameObject gameObject = (EventSystem.current != null) ? EventSystem.current.currentSelectedGameObject : null;
				if (gameObject == null && this.m_ResetOnDeActivation)
				{
					this.ReleaseSelection();
					return;
				}
				if (gameObject != null && gameObject != base.gameObject)
				{
					if (gameObject == this.m_PreviouslySelectedObject)
					{
						return;
					}
					this.m_PreviouslySelectedObject = gameObject;
					if (this.m_VerticalScrollbar && gameObject == this.m_VerticalScrollbar.gameObject)
					{
						return;
					}
					if (this.m_ResetOnDeActivation)
					{
						this.ReleaseSelection();
						return;
					}
					if (gameObject.GetComponent<TMP_InputField>() != null)
					{
						this.ReleaseSelection();
					}
					return;
				}
				else if (Input.GetKeyDown(KeyCode.Mouse0))
				{
					bool flag = false;
					float unscaledTime = Time.unscaledTime;
					if (this.m_KeyDownStartTime + this.m_DoubleClickDelay > unscaledTime)
					{
						flag = true;
					}
					this.m_KeyDownStartTime = unscaledTime;
					if (flag)
					{
						this.ReleaseSelection();
						return;
					}
				}
			}
			this.UpdateMaskRegions();
			if ((this.InPlaceEditing() && this.isKeyboardUsingEvents()) || !this.isFocused)
			{
				return;
			}
			this.AssignPositioningIfNeeded();
			if (this.m_SoftKeyboard == null || this.m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_SoftKeyboard != null)
				{
					if (!this.m_ReadOnly)
					{
						this.text = this.m_SoftKeyboard.text;
					}
					if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.LostFocus)
					{
						this.SendTouchScreenKeyboardStatusChanged();
					}
					if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
					{
						this.m_ReleaseSelection = true;
						this.m_WasCanceled = true;
						this.SendTouchScreenKeyboardStatusChanged();
					}
					if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.Done)
					{
						this.m_ReleaseSelection = true;
						this.OnSubmit(null);
						this.SendTouchScreenKeyboardStatusChanged();
					}
				}
				this.OnDeselect(null);
				return;
			}
			string text = this.m_SoftKeyboard.text;
			if (this.m_Text != text)
			{
				if (this.m_ReadOnly)
				{
					this.m_SoftKeyboard.text = this.m_Text;
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
						else if (this.characterValidation != TMP_InputField.CharacterValidation.None)
						{
							c = this.Validate(this.m_Text, this.m_Text.Length, c);
						}
						if (this.lineType == TMP_InputField.LineType.MultiLineSubmit && c == '\n')
						{
							this.m_SoftKeyboard.text = this.m_Text;
							this.OnSubmit(null);
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
					this.UpdateStringPositionFromKeyboard();
					if (this.m_Text != text)
					{
						this.m_SoftKeyboard.text = this.m_Text;
					}
					this.SendOnValueChangedAndUpdateLabel();
				}
			}
			else if (this.m_HideMobileInput && Application.platform == RuntimePlatform.Android)
			{
				this.UpdateStringPositionFromKeyboard();
			}
			if (this.m_SoftKeyboard != null && this.m_SoftKeyboard.status != TouchScreenKeyboard.Status.Visible)
			{
				if (this.m_SoftKeyboard.status == TouchScreenKeyboard.Status.Canceled)
				{
					this.m_WasCanceled = true;
				}
				this.OnDeselect(null);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0001E698 File Offset: 0x0001C898
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left && this.m_TextComponent != null && (this.m_SoftKeyboard == null || this.shouldHideSoftKeyboard || this.shouldHideMobileInput);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0001E6E5 File Offset: 0x0001C8E5
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = true;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			CaretPosition caretPosition;
			int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(this.m_TextComponent, eventData.position, eventData.pressEventCamera, out caretPosition);
			if (this.m_isRichTextEditingAllowed)
			{
				if (caretPosition == CaretPosition.Left)
				{
					this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
				}
				else if (caretPosition == CaretPosition.Right)
				{
					this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
				}
			}
			else if (caretPosition == CaretPosition.Left)
			{
				this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
			}
			else if (caretPosition == CaretPosition.Right)
			{
				this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
			}
			this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			this.MarkGeometryAsDirty();
			this.m_DragPositionOutOfBounds = !RectTransformUtility.RectangleContainsScreenPoint(this.textViewport, eventData.position, eventData.pressEventCamera);
			if (this.m_DragPositionOutOfBounds && this.m_DragCoroutine == null)
			{
				this.m_DragCoroutine = base.StartCoroutine(this.MouseDragOutsideRect(eventData));
			}
			eventData.Use();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0001E8B2 File Offset: 0x0001CAB2
		private IEnumerator MouseDragOutsideRect(PointerEventData eventData)
		{
			while (this.m_UpdateDrag && this.m_DragPositionOutOfBounds)
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.textViewport, eventData.position, eventData.pressEventCamera, out vector);
				Rect rect = this.textViewport.rect;
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

		// Token: 0x060002CA RID: 714 RVA: 0x0001E8C8 File Offset: 0x0001CAC8
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.m_UpdateDrag = false;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0001E8DC File Offset: 0x0001CADC
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			bool allowInput = this.m_AllowInput;
			base.OnPointerDown(eventData);
			if (!this.InPlaceEditing() && (this.m_SoftKeyboard == null || !this.m_SoftKeyboard.active))
			{
				this.OnSelect(eventData);
				return;
			}
			bool flag = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			bool flag2 = false;
			float unscaledTime = Time.unscaledTime;
			if (this.m_PointerDownClickStartTime + this.m_DoubleClickDelay > unscaledTime)
			{
				flag2 = true;
			}
			this.m_PointerDownClickStartTime = unscaledTime;
			if (allowInput || !this.m_OnFocusSelectAll)
			{
				CaretPosition caretPosition;
				int cursorIndexFromPosition = TMP_TextUtilities.GetCursorIndexFromPosition(this.m_TextComponent, eventData.position, eventData.pressEventCamera, out caretPosition);
				if (flag)
				{
					if (this.m_isRichTextEditingAllowed)
					{
						if (caretPosition == CaretPosition.Left)
						{
							this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
						}
						else if (caretPosition == CaretPosition.Right)
						{
							this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
						}
					}
					else if (caretPosition == CaretPosition.Left)
					{
						this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength));
					}
					else if (caretPosition == CaretPosition.Right)
					{
						this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					}
				}
				else if (this.m_isRichTextEditingAllowed)
				{
					if (caretPosition == CaretPosition.Left)
					{
						this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index);
					}
					else if (caretPosition == CaretPosition.Right)
					{
						this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
					}
				}
				else if (caretPosition == CaretPosition.Left)
				{
					this.stringPositionInternal = (this.stringSelectPositionInternal = ((cursorIndexFromPosition == 0) ? this.m_TextComponent.textInfo.characterInfo[0].index : (this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition - 1].stringLength)));
				}
				else if (caretPosition == CaretPosition.Right)
				{
					this.stringPositionInternal = (this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength);
				}
				if (flag2)
				{
					int num = TMP_TextUtilities.FindIntersectingWord(this.m_TextComponent, eventData.position, eventData.pressEventCamera);
					if (num != -1)
					{
						this.caretPositionInternal = this.m_TextComponent.textInfo.wordInfo[num].firstCharacterIndex;
						this.caretSelectPositionInternal = this.m_TextComponent.textInfo.wordInfo[num].lastCharacterIndex + 1;
						this.stringPositionInternal = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index;
						this.stringSelectPositionInternal = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].stringLength;
					}
					else
					{
						this.caretPositionInternal = cursorIndexFromPosition;
						this.caretSelectPositionInternal = this.caretPositionInternal + 1;
						this.stringPositionInternal = this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].index;
						this.stringSelectPositionInternal = this.stringPositionInternal + this.m_TextComponent.textInfo.characterInfo[cursorIndexFromPosition].stringLength;
					}
				}
				else
				{
					this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal));
				}
				this.m_isSelectAll = false;
			}
			this.UpdateLabel();
			eventData.Use();
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0001EDBC File Offset: 0x0001CFBC
		protected TMP_InputField.EditState KeyPressed(Event evt)
		{
			EventModifiers modifiers = evt.modifiers;
			bool flag = (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX) ? ((modifiers & EventModifiers.Command) > EventModifiers.None) : ((modifiers & EventModifiers.Control) > EventModifiers.None);
			bool flag2 = (modifiers & EventModifiers.Shift) > EventModifiers.None;
			bool flag3 = (modifiers & EventModifiers.Alt) > EventModifiers.None;
			bool flag4 = flag && !flag3 && !flag2;
			KeyCode keyCode = evt.keyCode;
			if (keyCode <= KeyCode.A)
			{
				if (keyCode <= KeyCode.Return)
				{
					if (keyCode == KeyCode.Backspace)
					{
						this.Backspace();
						return TMP_InputField.EditState.Continue;
					}
					if (keyCode != KeyCode.Return)
					{
						goto IL_1EB;
					}
				}
				else
				{
					if (keyCode == KeyCode.Escape)
					{
						this.m_ReleaseSelection = true;
						this.m_WasCanceled = true;
						return TMP_InputField.EditState.Finish;
					}
					if (keyCode != KeyCode.A)
					{
						goto IL_1EB;
					}
					if (flag4)
					{
						this.SelectAll();
						return TMP_InputField.EditState.Continue;
					}
					goto IL_1EB;
				}
			}
			else if (keyCode <= KeyCode.V)
			{
				if (keyCode != KeyCode.C)
				{
					if (keyCode != KeyCode.V)
					{
						goto IL_1EB;
					}
					if (flag4)
					{
						this.Append(TMP_InputField.clipboard);
						return TMP_InputField.EditState.Continue;
					}
					goto IL_1EB;
				}
				else
				{
					if (flag4)
					{
						if (this.inputType != TMP_InputField.InputType.Password)
						{
							TMP_InputField.clipboard = this.GetSelectedString();
						}
						else
						{
							TMP_InputField.clipboard = "";
						}
						return TMP_InputField.EditState.Continue;
					}
					goto IL_1EB;
				}
			}
			else if (keyCode != KeyCode.X)
			{
				if (keyCode == KeyCode.Delete)
				{
					this.DeleteKey();
					return TMP_InputField.EditState.Continue;
				}
				switch (keyCode)
				{
				case KeyCode.KeypadEnter:
					break;
				case KeyCode.KeypadEquals:
				case KeyCode.Insert:
					goto IL_1EB;
				case KeyCode.UpArrow:
					this.MoveUp(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.DownArrow:
					this.MoveDown(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.RightArrow:
					this.MoveRight(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.LeftArrow:
					this.MoveLeft(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.Home:
					this.MoveToStartOfLine(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.End:
					this.MoveToEndOfLine(flag2, flag);
					return TMP_InputField.EditState.Continue;
				case KeyCode.PageUp:
					this.MovePageUp(flag2);
					return TMP_InputField.EditState.Continue;
				case KeyCode.PageDown:
					this.MovePageDown(flag2);
					return TMP_InputField.EditState.Continue;
				default:
					goto IL_1EB;
				}
			}
			else
			{
				if (flag4)
				{
					if (this.inputType != TMP_InputField.InputType.Password)
					{
						TMP_InputField.clipboard = this.GetSelectedString();
					}
					else
					{
						TMP_InputField.clipboard = "";
					}
					this.Delete();
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return TMP_InputField.EditState.Continue;
				}
				goto IL_1EB;
			}
			if (this.lineType != TMP_InputField.LineType.MultiLineNewline)
			{
				this.m_ReleaseSelection = true;
				return TMP_InputField.EditState.Finish;
			}
			IL_1EB:
			char c = evt.character;
			if (!this.multiLine && (c == '\t' || c == '\r' || c == '\n'))
			{
				return TMP_InputField.EditState.Continue;
			}
			if (c == '\r' || c == '\u0003')
			{
				c = '\n';
			}
			if (flag2 && c == '\n')
			{
				c = '\v';
			}
			if (this.IsValidChar(c))
			{
				this.Append(c);
			}
			if (c == '\0' && this.compositionLength > 0)
			{
				this.UpdateLabel();
			}
			return TMP_InputField.EditState.Continue;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001F01A File Offset: 0x0001D21A
		protected virtual bool IsValidChar(char c)
		{
			if (c == '\0')
			{
				return false;
			}
			if (c == '\u007f')
			{
				return false;
			}
			if (c != '\t')
			{
			}
			return true;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0001F033 File Offset: 0x0001D233
		public void ProcessEvent(Event e)
		{
			this.KeyPressed(e);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001F040 File Offset: 0x0001D240
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.isFocused)
			{
				return;
			}
			bool flag = false;
			while (Event.PopEvent(this.m_ProcessingEvent))
			{
				EventType rawType = this.m_ProcessingEvent.rawType;
				if (rawType != EventType.KeyDown)
				{
					if (rawType != EventType.KeyUp)
					{
						if (rawType - EventType.ValidateCommand <= 1)
						{
							if (this.m_ProcessingEvent.commandName == "SelectAll")
							{
								this.SelectAll();
								flag = true;
							}
						}
					}
				}
				else
				{
					flag = true;
					if (!this.m_IsCompositionActive || this.compositionLength != 0 || this.m_ProcessingEvent.character != '\0' || this.m_ProcessingEvent.modifiers != EventModifiers.None)
					{
						if (this.KeyPressed(this.m_ProcessingEvent) == TMP_InputField.EditState.Finish)
						{
							if (!this.m_WasCanceled)
							{
								this.SendOnSubmit();
							}
							this.DeactivateInputField(false);
						}
						else
						{
							this.m_IsTextComponentUpdateRequired = true;
							this.UpdateLabel();
						}
					}
				}
			}
			if (flag)
			{
				this.UpdateLabel();
			}
			eventData.Use();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001F120 File Offset: 0x0001D320
		public virtual void OnScroll(PointerEventData eventData)
		{
			if (this.m_LineType == TMP_InputField.LineType.SingleLine)
			{
				if (this.m_IScrollHandlerParent != null)
				{
					this.m_IScrollHandlerParent.OnScroll(eventData);
				}
				return;
			}
			if (this.m_TextComponent.preferredHeight < this.m_TextViewport.rect.height)
			{
				return;
			}
			float num = -eventData.scrollDelta.y;
			this.m_ScrollPosition = this.GetScrollPositionRelativeToViewport();
			this.m_ScrollPosition += 1f / (float)this.m_TextComponent.textInfo.lineCount * num * this.m_ScrollSensitivity;
			this.m_ScrollPosition = Mathf.Clamp01(this.m_ScrollPosition);
			this.AdjustTextPositionRelativeToViewport(this.m_ScrollPosition);
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.value = this.m_ScrollPosition;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		private float GetScrollPositionRelativeToViewport()
		{
			Rect rect = this.m_TextViewport.rect;
			return (float)((int)((this.m_TextComponent.textInfo.lineInfo[0].ascender - rect.yMax + this.m_TextComponent.rectTransform.anchoredPosition.y) / (this.m_TextComponent.preferredHeight - rect.height) * 1000f + 0.5f)) / 1000f;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001F268 File Offset: 0x0001D468
		private string GetSelectedString()
		{
			if (!this.hasSelection)
			{
				return "";
			}
			int num = this.stringPositionInternal;
			int num2 = this.stringSelectPositionInternal;
			if (num > num2)
			{
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			return this.text.Substring(num, num2 - num);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		private int FindNextWordBegin()
		{
			if (this.stringSelectPositionInternal + 1 >= this.text.Length)
			{
				return this.text.Length;
			}
			int num = this.text.IndexOfAny(TMP_InputField.kSeparators, this.stringSelectPositionInternal + 1);
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

		// Token: 0x060002D4 RID: 724 RVA: 0x0001F308 File Offset: 0x0001D508
		private void MoveRight(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.stringPositionInternal = (this.stringSelectPositionInternal = Mathf.Max(this.stringPositionInternal, this.stringSelectPositionInternal));
				this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindNextWordBegin();
			}
			else if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringSelectPositionInternal < this.text.Length && char.IsHighSurrogate(this.text[this.stringSelectPositionInternal]))
				{
					num = this.stringSelectPositionInternal + 2;
				}
				else
				{
					num = this.stringSelectPositionInternal + 1;
				}
			}
			else
			{
				num = this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].index + this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal].stringLength;
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				return;
			}
			this.stringSelectPositionInternal = (this.stringPositionInternal = num);
			if (this.stringPositionInternal >= this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index + this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].stringLength)
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001F488 File Offset: 0x0001D688
		private int FindPrevWordBegin()
		{
			if (this.stringSelectPositionInternal - 2 < 0)
			{
				return 0;
			}
			int num = this.text.LastIndexOfAny(TMP_InputField.kSeparators, this.stringSelectPositionInternal - 2);
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

		// Token: 0x060002D6 RID: 726 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		private void MoveLeft(bool shift, bool ctrl)
		{
			if (this.hasSelection && !shift)
			{
				this.stringPositionInternal = (this.stringSelectPositionInternal = Mathf.Min(this.stringPositionInternal, this.stringSelectPositionInternal));
				this.caretPositionInternal = (this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
				return;
			}
			int num;
			if (ctrl)
			{
				num = this.FindPrevWordBegin();
			}
			else if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringSelectPositionInternal > 0 && char.IsLowSurrogate(this.text[this.stringSelectPositionInternal - 1]))
				{
					num = this.stringSelectPositionInternal - 2;
				}
				else
				{
					num = this.stringSelectPositionInternal - 1;
				}
			}
			else
			{
				num = ((this.caretSelectPositionInternal < 1) ? this.m_TextComponent.textInfo.characterInfo[0].index : this.m_TextComponent.textInfo.characterInfo[this.caretSelectPositionInternal - 1].index);
			}
			if (shift)
			{
				this.stringSelectPositionInternal = num;
				this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
				return;
			}
			this.stringSelectPositionInternal = (this.stringPositionInternal = num);
			if (this.caretPositionInternal > 0 && this.stringPositionInternal <= this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].index)
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal));
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001F630 File Offset: 0x0001D830
		private int LineUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			else
			{
				int num = this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex - 1;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[lineNumber - 1].firstCharacterIndex;
				while (i < num)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return num;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001F770 File Offset: 0x0001D970
		private int LineDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= this.m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			else
			{
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[lineNumber + 1].lastCharacterIndex;
				int num = -1;
				float num2 = 32767f;
				float num3 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[lineNumber + 1].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num4 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num5 = num4 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num5 >= 0f && num5 <= 1f)
					{
						if (num5 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num4 = Mathf.Abs(num4);
						if (num4 < num2)
						{
							num = i;
							num2 = num4;
							num3 = num5;
						}
						i++;
					}
				}
				if (num == -1)
				{
					return lastCharacterIndex;
				}
				if (num3 < 0.5f)
				{
					return num;
				}
				return num + 1;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001F8DC File Offset: 0x0001DADC
		private int PageUpCharacterPosition(int originalPos, bool goToFirstChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				originalPos--;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber - 1 < 0)
			{
				if (!goToFirstChar)
				{
					return originalPos;
				}
				return 0;
			}
			else
			{
				float height = this.m_TextViewport.rect.height;
				int num = lineNumber - 1;
				while (num > 0 && this.m_TextComponent.textInfo.lineInfo[num].baseline <= this.m_TextComponent.textInfo.lineInfo[lineNumber].baseline + height)
				{
					num--;
				}
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return lastCharacterIndex;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001FA7C File Offset: 0x0001DC7C
		private int PageDownCharacterPosition(int originalPos, bool goToLastChar)
		{
			if (originalPos >= this.m_TextComponent.textInfo.characterCount)
			{
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			TMP_CharacterInfo tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[originalPos];
			int lineNumber = tmp_CharacterInfo.lineNumber;
			if (lineNumber + 1 >= this.m_TextComponent.textInfo.lineCount)
			{
				if (!goToLastChar)
				{
					return originalPos;
				}
				return this.m_TextComponent.textInfo.characterCount - 1;
			}
			else
			{
				float height = this.m_TextViewport.rect.height;
				int num = lineNumber + 1;
				while (num < this.m_TextComponent.textInfo.lineCount - 1 && this.m_TextComponent.textInfo.lineInfo[num].baseline >= this.m_TextComponent.textInfo.lineInfo[lineNumber].baseline - height)
				{
					num++;
				}
				int lastCharacterIndex = this.m_TextComponent.textInfo.lineInfo[num].lastCharacterIndex;
				int num2 = -1;
				float num3 = 32767f;
				float num4 = 0f;
				int i = this.m_TextComponent.textInfo.lineInfo[num].firstCharacterIndex;
				while (i < lastCharacterIndex)
				{
					TMP_CharacterInfo tmp_CharacterInfo2 = this.m_TextComponent.textInfo.characterInfo[i];
					float num5 = tmp_CharacterInfo.origin - tmp_CharacterInfo2.origin;
					float num6 = num5 / (tmp_CharacterInfo2.xAdvance - tmp_CharacterInfo2.origin);
					if (num6 >= 0f && num6 <= 1f)
					{
						if (num6 < 0.5f)
						{
							return i;
						}
						return i + 1;
					}
					else
					{
						num5 = Mathf.Abs(num5);
						if (num5 < num3)
						{
							num2 = i;
							num3 = num5;
							num4 = num6;
						}
						i++;
					}
				}
				if (num2 == -1)
				{
					return lastCharacterIndex;
				}
				if (num4 < 0.5f)
				{
					return num2;
				}
				return num2 + 1;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0001FC5A File Offset: 0x0001DE5A
		private void MoveDown(bool shift)
		{
			this.MoveDown(shift, true);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001FC64 File Offset: 0x0001DE64
		private void MoveDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = this.multiLine ? this.LineDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : (this.m_TextComponent.textInfo.characterCount - 1);
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001FD0E File Offset: 0x0001DF0E
		private void MoveUp(bool shift)
		{
			this.MoveUp(shift, true);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001FD18 File Offset: 0x0001DF18
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
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
				return;
			}
			this.caretSelectPositionInternal = (this.caretPositionInternal = num);
			this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001FDB1 File Offset: 0x0001DFB1
		private void MovePageUp(bool shift)
		{
			this.MovePageUp(shift, true);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		private void MovePageUp(bool shift, bool goToFirstChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Min(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = this.multiLine ? this.PageUpCharacterPosition(this.caretSelectPositionInternal, goToFirstChar) : 0;
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
			}
			else
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
				this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num2 = this.m_TextViewport.rect.height;
				float num3 = this.m_TextComponent.rectTransform.position.y + this.m_TextComponent.textBounds.max.y;
				float num4 = this.m_TextViewport.position.y + this.m_TextViewport.rect.yMax;
				num2 = ((num4 > num3 + num2) ? num2 : (num4 - num3));
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num2);
				this.AssignPositioningIfNeeded();
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001FF08 File Offset: 0x0001E108
		private void MovePageDown(bool shift)
		{
			this.MovePageDown(shift, true);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001FF14 File Offset: 0x0001E114
		private void MovePageDown(bool shift, bool goToLastChar)
		{
			if (this.hasSelection && !shift)
			{
				this.caretPositionInternal = (this.caretSelectPositionInternal = Mathf.Max(this.caretPositionInternal, this.caretSelectPositionInternal));
			}
			int num = this.multiLine ? this.PageDownCharacterPosition(this.caretSelectPositionInternal, goToLastChar) : (this.m_TextComponent.textInfo.characterCount - 1);
			if (shift)
			{
				this.caretSelectPositionInternal = num;
				this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal);
			}
			else
			{
				this.caretSelectPositionInternal = (this.caretPositionInternal = num);
				this.stringSelectPositionInternal = (this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.caretSelectPositionInternal));
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num2 = this.m_TextViewport.rect.height;
				float num3 = this.m_TextComponent.rectTransform.position.y + this.m_TextComponent.textBounds.min.y;
				float num4 = this.m_TextViewport.position.y + this.m_TextViewport.rect.yMin;
				num2 = ((num4 > num3 + num2) ? num2 : (num4 - num3));
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num2);
				this.AssignPositioningIfNeeded();
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00020074 File Offset: 0x0001E274
		private void Delete()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.m_StringPosition == this.m_StringSelectPosition)
			{
				return;
			}
			if (this.m_isRichTextEditingAllowed || this.m_isSelectAll)
			{
				if (this.m_StringPosition < this.m_StringSelectPosition)
				{
					this.m_Text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					this.m_StringSelectPosition = this.m_StringPosition;
				}
				else
				{
					this.m_Text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
					this.m_StringPosition = this.m_StringSelectPosition;
				}
				if (this.m_isSelectAll)
				{
					this.m_CaretPosition = (this.m_CaretSelectPosition = 0);
					this.m_isSelectAll = false;
					return;
				}
			}
			else
			{
				if (this.m_CaretPosition < this.m_CaretSelectPosition)
				{
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition].index;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].stringLength;
					this.m_Text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					this.m_StringSelectPosition = this.m_StringPosition;
					this.m_CaretSelectPosition = this.m_CaretPosition;
					return;
				}
				this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].stringLength;
				this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition].index;
				this.m_Text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
				this.m_StringPosition = this.m_StringSelectPosition;
				this.m_CaretPosition = this.m_CaretSelectPosition;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000202AC File Offset: 0x0001E4AC
		private void DeleteKey()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.m_isLastKeyBackspace = true;
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringPositionInternal < this.text.Length)
				{
					if (char.IsHighSurrogate(this.text[this.stringPositionInternal]))
					{
						this.m_Text = this.text.Remove(this.stringPositionInternal, 2);
					}
					else
					{
						this.m_Text = this.text.Remove(this.stringPositionInternal, 1);
					}
					this.m_isLastKeyBackspace = true;
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return;
				}
			}
			else if (this.caretPositionInternal < this.m_TextComponent.textInfo.characterCount - 1)
			{
				int stringLength = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].stringLength;
				int index = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].index;
				this.m_Text = this.text.Remove(index, stringLength);
				this.m_isLastKeyBackspace = true;
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000203DC File Offset: 0x0001E5DC
		private void Backspace()
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (this.hasSelection)
			{
				this.m_isLastKeyBackspace = true;
				this.Delete();
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
				return;
			}
			if (this.m_isRichTextEditingAllowed)
			{
				if (this.stringPositionInternal > 0)
				{
					int num = 1;
					if (char.IsLowSurrogate(this.text[this.stringPositionInternal - 1]))
					{
						num = 2;
					}
					this.stringSelectPositionInternal = (this.stringPositionInternal -= num);
					this.m_Text = this.text.Remove(this.stringPositionInternal, num);
					this.caretSelectPositionInternal = --this.caretPositionInternal;
					this.m_isLastKeyBackspace = true;
					this.UpdateTouchKeyboardFromEditChanges();
					this.SendOnValueChangedAndUpdateLabel();
					return;
				}
			}
			else
			{
				if (this.caretPositionInternal > 0)
				{
					int stringLength = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].stringLength;
					this.m_Text = this.text.Remove(this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].index, stringLength);
					this.stringSelectPositionInternal = (this.stringPositionInternal = ((this.caretPositionInternal < 1) ? this.m_TextComponent.textInfo.characterInfo[0].index : this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1].index));
					this.caretSelectPositionInternal = --this.caretPositionInternal;
				}
				this.m_isLastKeyBackspace = true;
				this.UpdateTouchKeyboardFromEditChanges();
				this.SendOnValueChangedAndUpdateLabel();
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00020588 File Offset: 0x0001E788
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

		// Token: 0x060002E7 RID: 743 RVA: 0x000205E4 File Offset: 0x0001E7E4
		protected virtual void Append(char input)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			if (!this.InPlaceEditing())
			{
				return;
			}
			int num = Mathf.Min(this.stringPositionInternal, this.stringSelectPositionInternal);
			string text = this.text;
			if (this.selectionFocusPosition != this.selectionAnchorPosition)
			{
				if (this.m_isRichTextEditingAllowed || this.m_isSelectAll)
				{
					if (this.m_StringPosition < this.m_StringSelectPosition)
					{
						text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
					}
					else
					{
						text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
					}
				}
				else if (this.m_CaretPosition < this.m_CaretSelectPosition)
				{
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition].index;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition - 1].stringLength;
					text = this.text.Remove(this.m_StringPosition, this.m_StringSelectPosition - this.m_StringPosition);
				}
				else
				{
					this.m_StringPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].index + this.m_TextComponent.textInfo.characterInfo[this.m_CaretPosition - 1].stringLength;
					this.m_StringSelectPosition = this.m_TextComponent.textInfo.characterInfo[this.m_CaretSelectPosition].index;
					text = this.text.Remove(this.m_StringSelectPosition, this.m_StringPosition - this.m_StringSelectPosition);
				}
			}
			if (this.onValidateInput != null)
			{
				input = this.onValidateInput(text, num, input);
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.CustomValidator)
			{
				input = this.Validate(text, num, input);
				if (input == '\0')
				{
					return;
				}
				this.SendOnValueChanged();
				this.UpdateLabel();
				return;
			}
			else if (this.characterValidation != TMP_InputField.CharacterValidation.None)
			{
				input = this.Validate(text, num, input);
			}
			if (input == '\0')
			{
				return;
			}
			this.Insert(input);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00020828 File Offset: 0x0001EA28
		private void Insert(char c)
		{
			if (this.m_ReadOnly)
			{
				return;
			}
			string value = c.ToString();
			this.Delete();
			if (this.characterLimit > 0 && this.text.Length >= this.characterLimit)
			{
				return;
			}
			this.m_Text = this.text.Insert(this.m_StringPosition, value);
			if (!char.IsHighSurrogate(c))
			{
				this.m_CaretSelectPosition = ++this.m_CaretPosition;
			}
			this.m_StringSelectPosition = ++this.m_StringPosition;
			this.UpdateTouchKeyboardFromEditChanges();
			this.SendOnValueChanged();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000208C3 File Offset: 0x0001EAC3
		private void UpdateTouchKeyboardFromEditChanges()
		{
			if (this.m_SoftKeyboard != null && this.InPlaceEditing())
			{
				this.m_SoftKeyboard.text = this.m_Text;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000208E6 File Offset: 0x0001EAE6
		private void SendOnValueChangedAndUpdateLabel()
		{
			this.UpdateLabel();
			this.SendOnValueChanged();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000208F4 File Offset: 0x0001EAF4
		private void SendOnValueChanged()
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged.Invoke(this.text);
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0002090F File Offset: 0x0001EB0F
		protected void SendOnEndEdit()
		{
			if (this.onEndEdit != null)
			{
				this.onEndEdit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0002092A File Offset: 0x0001EB2A
		protected void SendOnSubmit()
		{
			if (this.onSubmit != null)
			{
				this.onSubmit.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00020945 File Offset: 0x0001EB45
		protected void SendOnFocus()
		{
			if (this.onSelect != null)
			{
				this.onSelect.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00020960 File Offset: 0x0001EB60
		protected void SendOnFocusLost()
		{
			if (this.onDeselect != null)
			{
				this.onDeselect.Invoke(this.m_Text);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0002097B File Offset: 0x0001EB7B
		protected void SendOnTextSelection()
		{
			this.m_isSelected = true;
			if (this.onTextSelection != null)
			{
				this.onTextSelection.Invoke(this.m_Text, this.stringPositionInternal, this.stringSelectPositionInternal);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000209A9 File Offset: 0x0001EBA9
		protected void SendOnEndTextSelection()
		{
			if (!this.m_isSelected)
			{
				return;
			}
			if (this.onEndTextSelection != null)
			{
				this.onEndTextSelection.Invoke(this.m_Text, this.stringPositionInternal, this.stringSelectPositionInternal);
			}
			this.m_isSelected = false;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000209E0 File Offset: 0x0001EBE0
		protected void SendTouchScreenKeyboardStatusChanged()
		{
			if (this.onTouchScreenKeyboardStatusChanged != null)
			{
				this.onTouchScreenKeyboardStatusChanged.Invoke(this.m_SoftKeyboard.status);
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00020A00 File Offset: 0x0001EC00
		protected void UpdateLabel()
		{
			if (this.m_TextComponent != null && this.m_TextComponent.font != null && !this.m_PreventCallback)
			{
				this.m_PreventCallback = true;
				string text;
				if (this.compositionLength > 0 && !this.m_ReadOnly)
				{
					this.Delete();
					if (this.m_RichText)
					{
						text = string.Concat(new string[]
						{
							this.text.Substring(0, this.m_StringPosition),
							"<u>",
							this.compositionString,
							"</u>",
							this.text.Substring(this.m_StringPosition)
						});
					}
					else
					{
						text = this.text.Substring(0, this.m_StringPosition) + this.compositionString + this.text.Substring(this.m_StringPosition);
					}
					this.m_IsCompositionActive = true;
				}
				else
				{
					text = this.text;
					this.m_IsCompositionActive = false;
					this.m_ShouldUpdateIMEWindowPosition = true;
				}
				string text2;
				if (this.inputType == TMP_InputField.InputType.Password)
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
				if (!flag && !this.m_ReadOnly)
				{
					this.SetCaretVisible();
				}
				this.m_TextComponent.text = text2 + "​";
				if (this.m_IsDrivenByLayoutComponents)
				{
					LayoutRebuilder.MarkLayoutForRebuild(this.m_RectTransform);
				}
				if (this.m_LineLimit > 0)
				{
					this.m_TextComponent.ForceMeshUpdate(false, false);
					TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
					if (textInfo != null && textInfo.lineCount > this.m_LineLimit)
					{
						int lastCharacterIndex = textInfo.lineInfo[this.m_LineLimit - 1].lastCharacterIndex;
						int num = textInfo.characterInfo[lastCharacterIndex].index + textInfo.characterInfo[lastCharacterIndex].stringLength;
						this.text = text2.Remove(num, text2.Length - num);
						this.m_TextComponent.text = this.text + "​";
					}
				}
				if (this.m_IsTextComponentUpdateRequired || this.m_VerticalScrollbar)
				{
					this.m_IsTextComponentUpdateRequired = false;
					this.m_TextComponent.ForceMeshUpdate(false, false);
				}
				this.MarkGeometryAsDirty();
				this.m_PreventCallback = false;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00020C64 File Offset: 0x0001EE64
		private void UpdateScrollbar()
		{
			if (this.m_VerticalScrollbar)
			{
				float size = this.m_TextViewport.rect.height / this.m_TextComponent.preferredHeight;
				this.m_VerticalScrollbar.size = size;
				this.m_VerticalScrollbar.value = this.GetScrollPositionRelativeToViewport();
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00020CBB File Offset: 0x0001EEBB
		private void OnScrollbarValueChange(float value)
		{
			if (value < 0f || value > 1f)
			{
				return;
			}
			this.AdjustTextPositionRelativeToViewport(value);
			this.m_ScrollPosition = value;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00020CDC File Offset: 0x0001EEDC
		private void UpdateMaskRegions()
		{
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		private void AdjustTextPositionRelativeToViewport(float relativePosition)
		{
			if (this.m_TextViewport == null)
			{
				return;
			}
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			if (textInfo == null || textInfo.lineInfo == null || textInfo.lineCount == 0 || textInfo.lineCount > textInfo.lineInfo.Length)
			{
				return;
			}
			float num = 0f;
			float num2 = this.m_TextComponent.preferredHeight;
			VerticalAlignmentOptions verticalAlignment = this.m_TextComponent.verticalAlignment;
			if (verticalAlignment <= VerticalAlignmentOptions.Bottom)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Top)
				{
					if (verticalAlignment != VerticalAlignmentOptions.Middle)
					{
						if (verticalAlignment == VerticalAlignmentOptions.Bottom)
						{
							num = 1f;
						}
					}
					else
					{
						num = 0.5f;
					}
				}
				else
				{
					num = 0f;
				}
			}
			else if (verticalAlignment != VerticalAlignmentOptions.Baseline)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Geometry)
				{
					if (verticalAlignment == VerticalAlignmentOptions.Capline)
					{
						num = 0.5f;
					}
				}
				else
				{
					num = 0.5f;
					num2 = this.m_TextComponent.bounds.size.y;
				}
			}
			this.m_TextComponent.rectTransform.anchoredPosition = new Vector2(this.m_TextComponent.rectTransform.anchoredPosition.x, (num2 - this.m_TextViewport.rect.height) * (relativePosition - num));
			this.AssignPositioningIfNeeded();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00020E10 File Offset: 0x0001F010
		private int GetCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (this.m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00020E5C File Offset: 0x0001F05C
		private int GetMinCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (stringIndex < this.m_TextComponent.textInfo.characterInfo[i].index + this.m_TextComponent.textInfo.characterInfo[i].stringLength)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00020EC4 File Offset: 0x0001F0C4
		private int GetMaxCaretPositionFromStringIndex(int stringIndex)
		{
			int characterCount = this.m_TextComponent.textInfo.characterCount;
			for (int i = 0; i < characterCount; i++)
			{
				if (this.m_TextComponent.textInfo.characterInfo[i].index >= stringIndex)
				{
					return i;
				}
			}
			return characterCount;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00020F0F File Offset: 0x0001F10F
		private int GetStringIndexFromCaretPosition(int caretPosition)
		{
			this.ClampCaretPos(ref caretPosition);
			return this.m_TextComponent.textInfo.characterInfo[caretPosition].index;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00020F34 File Offset: 0x0001F134
		public void ForceLabelUpdate()
		{
			this.UpdateLabel();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00020F3C File Offset: 0x0001F13C
		private void MarkGeometryAsDirty()
		{
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00020F44 File Offset: 0x0001F144
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.LatePreRender)
			{
				this.UpdateGeometry();
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00020F50 File Offset: 0x0001F150
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00020F52 File Offset: 0x0001F152
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00020F54 File Offset: 0x0001F154
		private void UpdateGeometry()
		{
			if (!this.InPlaceEditing())
			{
				return;
			}
			if (this.m_CachedInputRenderer == null)
			{
				return;
			}
			this.OnFillVBO(this.mesh);
			this.m_CachedInputRenderer.SetMesh(this.mesh);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00020F8C File Offset: 0x0001F18C
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

		// Token: 0x06000303 RID: 771 RVA: 0x000211B4 File Offset: 0x0001F3B4
		private void OnFillVBO(Mesh vbo)
		{
			using (VertexHelper vertexHelper = new VertexHelper())
			{
				if (!this.isFocused && !this.m_SelectionStillActive)
				{
					vertexHelper.FillMesh(vbo);
				}
				else
				{
					if (this.m_IsStringPositionDirty)
					{
						this.stringPositionInternal = this.GetStringIndexFromCaretPosition(this.m_CaretPosition);
						this.stringSelectPositionInternal = this.GetStringIndexFromCaretPosition(this.m_CaretSelectPosition);
						this.m_IsStringPositionDirty = false;
					}
					if (this.m_IsCaretPositionDirty)
					{
						this.caretPositionInternal = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
						this.caretSelectPositionInternal = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
						this.m_IsCaretPositionDirty = false;
					}
					if (!this.hasSelection)
					{
						this.GenerateCaret(vertexHelper, Vector2.zero);
						this.SendOnEndTextSelection();
					}
					else
					{
						this.GenerateHightlight(vertexHelper, Vector2.zero);
						this.SendOnTextSelection();
					}
					vertexHelper.FillMesh(vbo);
				}
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0002129C File Offset: 0x0001F49C
		private void GenerateCaret(VertexHelper vbo, Vector2 roundingOffset)
		{
			if (!this.m_CaretVisible || this.m_TextComponent.canvas == null || this.m_ReadOnly)
			{
				return;
			}
			if (this.m_CursorVerts == null)
			{
				this.CreateCursorVerts();
			}
			float num = (float)this.m_CaretWidth;
			Vector2 zero = Vector2.zero;
			if (this.caretPositionInternal >= this.m_TextComponent.textInfo.characterInfo.Length)
			{
				return;
			}
			int lineNumber = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal].lineNumber;
			TMP_CharacterInfo tmp_CharacterInfo;
			float num2;
			if (this.caretPositionInternal == this.m_TextComponent.textInfo.lineInfo[lineNumber].firstCharacterIndex)
			{
				tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal];
				num2 = tmp_CharacterInfo.ascender - tmp_CharacterInfo.descender;
				if (this.m_TextComponent.verticalAlignment == VerticalAlignmentOptions.Geometry)
				{
					zero = new Vector2(tmp_CharacterInfo.origin, 0f - num2 / 2f);
				}
				else
				{
					zero = new Vector2(tmp_CharacterInfo.origin, tmp_CharacterInfo.descender);
				}
			}
			else
			{
				tmp_CharacterInfo = this.m_TextComponent.textInfo.characterInfo[this.caretPositionInternal - 1];
				num2 = tmp_CharacterInfo.ascender - tmp_CharacterInfo.descender;
				if (this.m_TextComponent.verticalAlignment == VerticalAlignmentOptions.Geometry)
				{
					zero = new Vector2(tmp_CharacterInfo.xAdvance, 0f - num2 / 2f);
				}
				else
				{
					zero = new Vector2(tmp_CharacterInfo.xAdvance, tmp_CharacterInfo.descender);
				}
			}
			if (this.m_SoftKeyboard != null)
			{
				int num3 = this.m_StringPosition;
				int num4 = (this.m_SoftKeyboard.text == null) ? 0 : this.m_SoftKeyboard.text.Length;
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num3 > num4)
				{
					num3 = num4;
				}
				this.m_SoftKeyboard.selection = new RangeInt(num3, 0);
			}
			if ((this.isFocused && zero != this.m_LastPosition) || this.m_forceRectTransformAdjustment || this.m_isLastKeyBackspace)
			{
				this.AdjustRectTransformRelativeToViewport(zero, num2, tmp_CharacterInfo.isVisible);
			}
			this.m_LastPosition = zero;
			float num5 = zero.y + num2;
			float y = num5 - num2;
			float scaleFactor = this.m_TextComponent.canvas.scaleFactor;
			this.m_CursorVerts[0].position = new Vector3(zero.x, y, 0f);
			this.m_CursorVerts[1].position = new Vector3(zero.x, num5, 0f);
			this.m_CursorVerts[2].position = new Vector3(zero.x + num, num5, 0f);
			this.m_CursorVerts[3].position = new Vector3(zero.x + num, y, 0f);
			this.m_CursorVerts[0].color = this.caretColor;
			this.m_CursorVerts[1].color = this.caretColor;
			this.m_CursorVerts[2].color = this.caretColor;
			this.m_CursorVerts[3].color = this.caretColor;
			vbo.AddUIVertexQuad(this.m_CursorVerts);
			if (this.m_ShouldUpdateIMEWindowPosition || lineNumber != this.m_PreviousIMEInsertionLine)
			{
				this.m_ShouldUpdateIMEWindowPosition = false;
				this.m_PreviousIMEInsertionLine = lineNumber;
				Camera camera;
				if (this.m_TextComponent.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					camera = null;
				}
				else
				{
					camera = this.m_TextComponent.canvas.worldCamera;
					if (camera == null)
					{
						camera = Camera.current;
					}
				}
				Vector3 worldPoint = this.m_CachedInputRenderer.gameObject.transform.TransformPoint(this.m_CursorVerts[0].position);
				Vector2 vector = RectTransformUtility.WorldToScreenPoint(camera, worldPoint);
				vector.y = (float)Screen.height - vector.y;
				if (this.inputSystem != null)
				{
					this.inputSystem.compositionCursorPos = vector;
				}
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000216B0 File Offset: 0x0001F8B0
		private void CreateCursorVerts()
		{
			this.m_CursorVerts = new UIVertex[4];
			for (int i = 0; i < this.m_CursorVerts.Length; i++)
			{
				this.m_CursorVerts[i] = UIVertex.simpleVert;
				this.m_CursorVerts[i].uv0 = Vector2.zero;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00021708 File Offset: 0x0001F908
		private void GenerateHightlight(VertexHelper vbo, Vector2 roundingOffset)
		{
			this.UpdateMaskRegions();
			TMP_TextInfo textInfo = this.m_TextComponent.textInfo;
			this.m_CaretPosition = this.GetCaretPositionFromStringIndex(this.stringPositionInternal);
			this.m_CaretSelectPosition = this.GetCaretPositionFromStringIndex(this.stringSelectPositionInternal);
			if (this.m_SoftKeyboard != null)
			{
				int num = (this.m_CaretPosition < this.m_CaretSelectPosition) ? textInfo.characterInfo[this.m_CaretPosition].index : textInfo.characterInfo[this.m_CaretSelectPosition].index;
				int length = (this.m_CaretPosition < this.m_CaretSelectPosition) ? (this.stringSelectPositionInternal - num) : (this.stringPositionInternal - num);
				this.m_SoftKeyboard.selection = new RangeInt(num, length);
			}
			Vector2 startPosition;
			float height;
			if (this.m_CaretSelectPosition < textInfo.characterCount)
			{
				startPosition = new Vector2(textInfo.characterInfo[this.m_CaretSelectPosition].origin, textInfo.characterInfo[this.m_CaretSelectPosition].descender);
				height = textInfo.characterInfo[this.m_CaretSelectPosition].ascender - textInfo.characterInfo[this.m_CaretSelectPosition].descender;
			}
			else
			{
				startPosition = new Vector2(textInfo.characterInfo[this.m_CaretSelectPosition - 1].xAdvance, textInfo.characterInfo[this.m_CaretSelectPosition - 1].descender);
				height = textInfo.characterInfo[this.m_CaretSelectPosition - 1].ascender - textInfo.characterInfo[this.m_CaretSelectPosition - 1].descender;
			}
			this.AdjustRectTransformRelativeToViewport(startPosition, height, true);
			int num2 = Mathf.Max(0, this.m_CaretPosition);
			int num3 = Mathf.Max(0, this.m_CaretSelectPosition);
			if (num2 > num3)
			{
				int num4 = num2;
				num2 = num3;
				num3 = num4;
			}
			num3--;
			int num5 = textInfo.characterInfo[num2].lineNumber;
			int lastCharacterIndex = textInfo.lineInfo[num5].lastCharacterIndex;
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.uv0 = Vector2.zero;
			simpleVert.color = this.selectionColor;
			int num6 = num2;
			while (num6 <= num3 && num6 < textInfo.characterCount)
			{
				if (num6 == lastCharacterIndex || num6 == num3)
				{
					TMP_CharacterInfo tmp_CharacterInfo = textInfo.characterInfo[num2];
					TMP_CharacterInfo tmp_CharacterInfo2 = textInfo.characterInfo[num6];
					if (num6 > 0 && tmp_CharacterInfo2.character == '\n' && textInfo.characterInfo[num6 - 1].character == '\r')
					{
						tmp_CharacterInfo2 = textInfo.characterInfo[num6 - 1];
					}
					Vector2 vector = new Vector2(tmp_CharacterInfo.origin, textInfo.lineInfo[num5].ascender);
					Vector2 vector2 = new Vector2(tmp_CharacterInfo2.xAdvance, textInfo.lineInfo[num5].descender);
					int currentVertCount = vbo.currentVertCount;
					simpleVert.position = new Vector3(vector.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector2.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector2.x, vector.y, 0f);
					vbo.AddVert(simpleVert);
					simpleVert.position = new Vector3(vector.x, vector.y, 0f);
					vbo.AddVert(simpleVert);
					vbo.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
					vbo.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
					num2 = num6 + 1;
					num5++;
					if (num5 < textInfo.lineCount)
					{
						lastCharacterIndex = textInfo.lineInfo[num5].lastCharacterIndex;
					}
				}
				num6++;
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		private void AdjustRectTransformRelativeToViewport(Vector2 startPosition, float height, bool isCharVisible)
		{
			if (this.m_TextViewport == null)
			{
				return;
			}
			Vector3 localPosition = base.transform.localPosition;
			Vector3 localPosition2 = this.m_TextComponent.rectTransform.localPosition;
			Vector3 localPosition3 = this.m_TextViewport.localPosition;
			Rect rect = this.m_TextViewport.rect;
			Vector2 vector = new Vector2(startPosition.x + localPosition2.x + localPosition3.x + localPosition.x, startPosition.y + localPosition2.y + localPosition3.y + localPosition.y);
			Rect rect2 = new Rect(localPosition.x + localPosition3.x + rect.x, localPosition.y + localPosition3.y + rect.y, rect.width, rect.height);
			float num = rect2.xMax - (vector.x + this.m_TextComponent.margin.z + (float)this.m_CaretWidth);
			if (num < 0f && (!this.multiLine || (this.multiLine && isCharVisible)))
			{
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num, 0f);
				this.AssignPositioningIfNeeded();
			}
			float num2 = vector.x - this.m_TextComponent.margin.x - rect2.xMin;
			if (num2 < 0f)
			{
				this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(-num2, 0f);
				this.AssignPositioningIfNeeded();
			}
			if (this.m_LineType != TMP_InputField.LineType.SingleLine)
			{
				float num3 = rect2.yMax - (vector.y + height);
				if (num3 < -0.0001f)
				{
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(0f, num3);
					this.AssignPositioningIfNeeded();
				}
				float num4 = vector.y - rect2.yMin;
				if (num4 < 0f)
				{
					this.m_TextComponent.rectTransform.anchoredPosition -= new Vector2(0f, num4);
					this.AssignPositioningIfNeeded();
				}
			}
			if (this.m_isLastKeyBackspace)
			{
				float x = this.m_TextComponent.rectTransform.anchoredPosition.x;
				float num5 = localPosition.x + localPosition3.x + localPosition2.x + this.m_TextComponent.textInfo.characterInfo[0].origin - this.m_TextComponent.margin.x;
				float num6 = localPosition.x + localPosition3.x + localPosition2.x + this.m_TextComponent.textInfo.characterInfo[this.m_TextComponent.textInfo.characterCount - 1].origin + this.m_TextComponent.margin.z + (float)this.m_CaretWidth;
				if (x > 0.0001f && num5 > rect2.xMin)
				{
					float num7 = rect2.xMin - num5;
					if (x < -num7)
					{
						num7 = -x;
					}
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num7, 0f);
					this.AssignPositioningIfNeeded();
				}
				else if (x < -0.0001f && num6 < rect2.xMax)
				{
					float num8 = rect2.xMax - num6;
					if (-x < num8)
					{
						num8 = -x;
					}
					this.m_TextComponent.rectTransform.anchoredPosition += new Vector2(num8, 0f);
					this.AssignPositioningIfNeeded();
				}
				this.m_isLastKeyBackspace = false;
			}
			this.m_forceRectTransformAdjustment = false;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00021E9C File Offset: 0x0002009C
		protected char Validate(string text, int pos, char ch)
		{
			if (this.characterValidation == TMP_InputField.CharacterValidation.None || !base.enabled)
			{
				return ch;
			}
			if (this.characterValidation == TMP_InputField.CharacterValidation.Integer || this.characterValidation == TMP_InputField.CharacterValidation.Decimal)
			{
				bool flag = pos == 0 && text.Length > 0 && text[0] == '-';
				bool flag2 = this.stringPositionInternal == 0 || this.stringSelectPositionInternal == 0;
				if (!flag)
				{
					if (ch >= '0' && ch <= '9')
					{
						return ch;
					}
					if (ch == '-' && (pos == 0 || flag2))
					{
						return ch;
					}
					string numberDecimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
					if (ch == Convert.ToChar(numberDecimalSeparator) && this.characterValidation == TMP_InputField.CharacterValidation.Decimal && !text.Contains(numberDecimalSeparator))
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Digit)
			{
				if (ch >= '0' && ch <= '9')
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Alphanumeric)
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
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Name)
			{
				char c = (text.Length > 0) ? text[Mathf.Clamp(pos - 1, 0, text.Length - 1)] : ' ';
				char c2 = (text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ';
				char c3 = (text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n';
				if (char.IsLetter(ch))
				{
					if (char.IsLower(ch) && pos == 0)
					{
						return char.ToUpper(ch);
					}
					if (char.IsLower(ch) && (c == ' ' || c == '-'))
					{
						return char.ToUpper(ch);
					}
					if (char.IsUpper(ch) && pos > 0 && c != ' ' && c != '\'' && c != '-' && !char.IsLower(c))
					{
						return char.ToLower(ch);
					}
					if (char.IsUpper(ch) && char.IsUpper(c2))
					{
						return '\0';
					}
					return ch;
				}
				else
				{
					if (ch == '\'' && c2 != ' ' && c2 != '\'' && c3 != '\'' && !text.Contains("'"))
					{
						return ch;
					}
					if (char.IsLetter(c) && ch == '-' && c2 != '-')
					{
						return ch;
					}
					if ((ch == ' ' || ch == '-') && pos != 0 && c != ' ' && c != '\'' && c != '-' && c2 != ' ' && c2 != '\'' && c2 != '-' && c3 != ' ' && c3 != '\'' && c3 != '-')
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.EmailAddress)
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
					int num = (int)((text.Length > 0) ? text[Mathf.Clamp(pos, 0, text.Length - 1)] : ' ');
					char c4 = (text.Length > 0) ? text[Mathf.Clamp(pos + 1, 0, text.Length - 1)] : '\n';
					if (num != 46 && c4 != '.')
					{
						return ch;
					}
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.Regex)
			{
				if (Regex.IsMatch(ch.ToString(), this.m_RegexValue))
				{
					return ch;
				}
			}
			else if (this.characterValidation == TMP_InputField.CharacterValidation.CustomValidator && this.m_InputValidator != null)
			{
				char result = this.m_InputValidator.Validate(ref text, ref pos, ch);
				this.m_Text = text;
				this.stringSelectPositionInternal = (this.stringPositionInternal = pos);
				return result;
			}
			return '\0';
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0002223C File Offset: 0x0002043C
		public void ActivateInputField()
		{
			if (this.m_TextComponent == null || this.m_TextComponent.font == null || !this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			if (this.isFocused && this.m_SoftKeyboard != null && !this.m_SoftKeyboard.active)
			{
				this.m_SoftKeyboard.active = true;
				this.m_SoftKeyboard.text = this.m_Text;
			}
			this.m_ShouldActivateNextUpdate = true;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000222BC File Offset: 0x000204BC
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
			if (TouchScreenKeyboard.isSupported && !this.shouldHideSoftKeyboard)
			{
				if (this.inputSystem != null && this.inputSystem.touchSupported)
				{
					TouchScreenKeyboard.hideInput = this.shouldHideMobileInput;
				}
				if (!this.shouldHideSoftKeyboard && !this.m_ReadOnly)
				{
					this.m_SoftKeyboard = ((this.inputType == TMP_InputField.InputType.Password) ? TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, false, this.multiLine, true, false, "", this.characterLimit) : TouchScreenKeyboard.Open(this.m_Text, this.keyboardType, this.inputType == TMP_InputField.InputType.AutoCorrect, this.multiLine, false, false, "", this.characterLimit));
					this.OnFocus();
					if (this.m_SoftKeyboard != null)
					{
						int length = (this.stringPositionInternal < this.stringSelectPositionInternal) ? (this.stringSelectPositionInternal - this.stringPositionInternal) : (this.stringPositionInternal - this.stringSelectPositionInternal);
						this.m_SoftKeyboard.selection = new RangeInt((this.stringPositionInternal < this.stringSelectPositionInternal) ? this.stringPositionInternal : this.stringSelectPositionInternal, length);
					}
				}
				this.m_TouchKeyboardAllowsInPlaceEditing = TouchScreenKeyboard.isInPlaceEditingAllowed;
			}
			else
			{
				if (!TouchScreenKeyboard.isSupported && !this.m_ReadOnly && this.inputSystem != null)
				{
					this.inputSystem.imeCompositionMode = IMECompositionMode.On;
				}
				this.OnFocus();
			}
			this.m_AllowInput = true;
			this.m_OriginalText = this.text;
			this.m_WasCanceled = false;
			this.SetCaretVisible();
			this.UpdateLabel();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0002247A File Offset: 0x0002067A
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.SendOnFocus();
			this.ActivateInputField();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0002248F File Offset: 0x0002068F
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.ActivateInputField();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000224A0 File Offset: 0x000206A0
		public void OnControlClick()
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000224A2 File Offset: 0x000206A2
		public void ReleaseSelection()
		{
			this.m_SelectionStillActive = false;
			this.m_ReleaseSelection = false;
			this.m_PreviouslySelectedObject = null;
			this.MarkGeometryAsDirty();
			this.SendOnEndEdit();
			this.SendOnEndTextSelection();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000224CC File Offset: 0x000206CC
		public void DeactivateInputField(bool clearSelection = false)
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
				if (this.m_WasCanceled && this.m_RestoreOriginalTextOnEscape)
				{
					this.text = this.m_OriginalText;
				}
				if (this.m_SoftKeyboard != null)
				{
					this.m_SoftKeyboard.active = false;
					this.m_SoftKeyboard = null;
				}
				this.m_SelectionStillActive = true;
				if ((this.m_ResetOnDeActivation || this.m_ReleaseSelection) && this.m_VerticalScrollbar == null)
				{
					this.ReleaseSelection();
				}
				if (this.inputSystem != null)
				{
					this.inputSystem.imeCompositionMode = IMECompositionMode.Auto;
				}
			}
			this.MarkGeometryAsDirty();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000225AF File Offset: 0x000207AF
		public override void OnDeselect(BaseEventData eventData)
		{
			this.DeactivateInputField(false);
			base.OnDeselect(eventData);
			this.SendOnFocusLost();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000225C5 File Offset: 0x000207C5
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
			this.SendOnSubmit();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000225F0 File Offset: 0x000207F0
		private void EnforceContentType()
		{
			switch (this.contentType)
			{
			case TMP_InputField.ContentType.Standard:
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.Autocorrected:
				this.m_InputType = TMP_InputField.InputType.AutoCorrect;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.IntegerNumber:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Integer;
				break;
			case TMP_InputField.ContentType.DecimalNumber:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.NumbersAndPunctuation;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Decimal;
				break;
			case TMP_InputField.ContentType.Alphanumeric:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.ASCIICapable;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Alphanumeric;
				break;
			case TMP_InputField.ContentType.Name:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Name;
				break;
			case TMP_InputField.ContentType.EmailAddress:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Standard;
				this.m_KeyboardType = TouchScreenKeyboardType.EmailAddress;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.EmailAddress;
				break;
			case TMP_InputField.ContentType.Password:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.Default;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.None;
				break;
			case TMP_InputField.ContentType.Pin:
				this.m_LineType = TMP_InputField.LineType.SingleLine;
				this.m_InputType = TMP_InputField.InputType.Password;
				this.m_KeyboardType = TouchScreenKeyboardType.NumberPad;
				this.m_CharacterValidation = TMP_InputField.CharacterValidation.Digit;
				break;
			}
			this.SetTextComponentWrapMode();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00022743 File Offset: 0x00020943
		private void SetTextComponentWrapMode()
		{
			if (this.m_TextComponent == null)
			{
				return;
			}
			if (this.multiLine)
			{
				this.m_TextComponent.enableWordWrapping = true;
				return;
			}
			this.m_TextComponent.enableWordWrapping = false;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00022775 File Offset: 0x00020975
		private void SetTextComponentRichTextMode()
		{
			if (this.m_TextComponent == null)
			{
				return;
			}
			this.m_TextComponent.richText = this.m_RichText;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00022798 File Offset: 0x00020998
		private void SetToCustomIfContentTypeIsNot(params TMP_InputField.ContentType[] allowedContentTypes)
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
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
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000227D2 File Offset: 0x000209D2
		private void SetToCustom()
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000227E7 File Offset: 0x000209E7
		private void SetToCustom(TMP_InputField.CharacterValidation characterValidation)
		{
			if (this.contentType == TMP_InputField.ContentType.Custom)
			{
				return;
			}
			this.contentType = TMP_InputField.ContentType.Custom;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00022802 File Offset: 0x00020A02
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

		// Token: 0x06000319 RID: 793 RVA: 0x00022824 File Offset: 0x00020A24
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00022826 File Offset: 0x00020A26
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00022828 File Offset: 0x00020A28
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00022830 File Offset: 0x00020A30
		public virtual float preferredWidth
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				float num = 0f;
				if (this.m_LayoutGroup != null)
				{
					num = (float)this.m_LayoutGroup.padding.horizontal;
				}
				if (this.m_TextViewport != null)
				{
					num += this.m_TextViewport.offsetMin.x - this.m_TextViewport.offsetMax.x;
				}
				return this.m_TextComponent.preferredWidth + num;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600031D RID: 797 RVA: 0x000228B6 File Offset: 0x00020AB6
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000228BD File Offset: 0x00020ABD
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000228C4 File Offset: 0x00020AC4
		public virtual float preferredHeight
		{
			get
			{
				if (this.textComponent == null)
				{
					return 0f;
				}
				float num = 0f;
				if (this.m_LayoutGroup != null)
				{
					num = (float)this.m_LayoutGroup.padding.vertical;
				}
				if (this.m_TextViewport != null)
				{
					num += this.m_TextViewport.offsetMin.y - this.m_TextViewport.offsetMax.y;
				}
				return this.m_TextComponent.preferredHeight + num;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0002294A File Offset: 0x00020B4A
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00022951 File Offset: 0x00020B51
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00022954 File Offset: 0x00020B54
		public void SetGlobalPointSize(float pointSize)
		{
			TMP_Text tmp_Text = this.m_Placeholder as TMP_Text;
			if (tmp_Text != null)
			{
				tmp_Text.fontSize = pointSize;
			}
			this.textComponent.fontSize = pointSize;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0002298C File Offset: 0x00020B8C
		public void SetGlobalFontAsset(TMP_FontAsset fontAsset)
		{
			TMP_Text tmp_Text = this.m_Placeholder as TMP_Text;
			if (tmp_Text != null)
			{
				tmp_Text.font = fontAsset;
			}
			this.textComponent.font = fontAsset;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000229C1 File Offset: 0x00020BC1
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_InputField()
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000229D9 File Offset: 0x00020BD9
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000200 RID: 512
		protected TouchScreenKeyboard m_SoftKeyboard;

		// Token: 0x04000201 RID: 513
		private static readonly char[] kSeparators = new char[]
		{
			' ',
			'.',
			',',
			'\t',
			'\r',
			'\n'
		};

		// Token: 0x04000202 RID: 514
		protected RectTransform m_RectTransform;

		// Token: 0x04000203 RID: 515
		[SerializeField]
		protected RectTransform m_TextViewport;

		// Token: 0x04000204 RID: 516
		protected RectMask2D m_TextComponentRectMask;

		// Token: 0x04000205 RID: 517
		protected RectMask2D m_TextViewportRectMask;

		// Token: 0x04000206 RID: 518
		private Rect m_CachedViewportRect;

		// Token: 0x04000207 RID: 519
		[SerializeField]
		protected TMP_Text m_TextComponent;

		// Token: 0x04000208 RID: 520
		protected RectTransform m_TextComponentRectTransform;

		// Token: 0x04000209 RID: 521
		[SerializeField]
		protected Graphic m_Placeholder;

		// Token: 0x0400020A RID: 522
		[SerializeField]
		protected Scrollbar m_VerticalScrollbar;

		// Token: 0x0400020B RID: 523
		[SerializeField]
		protected TMP_ScrollbarEventHandler m_VerticalScrollbarEventHandler;

		// Token: 0x0400020C RID: 524
		private bool m_IsDrivenByLayoutComponents;

		// Token: 0x0400020D RID: 525
		[SerializeField]
		private LayoutGroup m_LayoutGroup;

		// Token: 0x0400020E RID: 526
		private IScrollHandler m_IScrollHandlerParent;

		// Token: 0x0400020F RID: 527
		private float m_ScrollPosition;

		// Token: 0x04000210 RID: 528
		[SerializeField]
		protected float m_ScrollSensitivity = 1f;

		// Token: 0x04000211 RID: 529
		[SerializeField]
		private TMP_InputField.ContentType m_ContentType;

		// Token: 0x04000212 RID: 530
		[SerializeField]
		private TMP_InputField.InputType m_InputType;

		// Token: 0x04000213 RID: 531
		[SerializeField]
		private char m_AsteriskChar = '*';

		// Token: 0x04000214 RID: 532
		[SerializeField]
		private TouchScreenKeyboardType m_KeyboardType;

		// Token: 0x04000215 RID: 533
		[SerializeField]
		private TMP_InputField.LineType m_LineType;

		// Token: 0x04000216 RID: 534
		[SerializeField]
		private bool m_HideMobileInput;

		// Token: 0x04000217 RID: 535
		[SerializeField]
		private bool m_HideSoftKeyboard;

		// Token: 0x04000218 RID: 536
		[SerializeField]
		private TMP_InputField.CharacterValidation m_CharacterValidation;

		// Token: 0x04000219 RID: 537
		[SerializeField]
		private string m_RegexValue = string.Empty;

		// Token: 0x0400021A RID: 538
		[SerializeField]
		private float m_GlobalPointSize = 14f;

		// Token: 0x0400021B RID: 539
		[SerializeField]
		private int m_CharacterLimit;

		// Token: 0x0400021C RID: 540
		[SerializeField]
		private TMP_InputField.SubmitEvent m_OnEndEdit = new TMP_InputField.SubmitEvent();

		// Token: 0x0400021D RID: 541
		[SerializeField]
		private TMP_InputField.SubmitEvent m_OnSubmit = new TMP_InputField.SubmitEvent();

		// Token: 0x0400021E RID: 542
		[SerializeField]
		private TMP_InputField.SelectionEvent m_OnSelect = new TMP_InputField.SelectionEvent();

		// Token: 0x0400021F RID: 543
		[SerializeField]
		private TMP_InputField.SelectionEvent m_OnDeselect = new TMP_InputField.SelectionEvent();

		// Token: 0x04000220 RID: 544
		[SerializeField]
		private TMP_InputField.TextSelectionEvent m_OnTextSelection = new TMP_InputField.TextSelectionEvent();

		// Token: 0x04000221 RID: 545
		[SerializeField]
		private TMP_InputField.TextSelectionEvent m_OnEndTextSelection = new TMP_InputField.TextSelectionEvent();

		// Token: 0x04000222 RID: 546
		[SerializeField]
		private TMP_InputField.OnChangeEvent m_OnValueChanged = new TMP_InputField.OnChangeEvent();

		// Token: 0x04000223 RID: 547
		[SerializeField]
		private TMP_InputField.TouchScreenKeyboardEvent m_OnTouchScreenKeyboardStatusChanged = new TMP_InputField.TouchScreenKeyboardEvent();

		// Token: 0x04000224 RID: 548
		[SerializeField]
		private TMP_InputField.OnValidateInput m_OnValidateInput;

		// Token: 0x04000225 RID: 549
		[SerializeField]
		private Color m_CaretColor = new Color(0.19607843f, 0.19607843f, 0.19607843f, 1f);

		// Token: 0x04000226 RID: 550
		[SerializeField]
		private bool m_CustomCaretColor;

		// Token: 0x04000227 RID: 551
		[SerializeField]
		private Color m_SelectionColor = new Color(0.65882355f, 0.80784315f, 1f, 0.7529412f);

		// Token: 0x04000228 RID: 552
		[SerializeField]
		[TextArea(5, 10)]
		protected string m_Text = string.Empty;

		// Token: 0x04000229 RID: 553
		[SerializeField]
		[Range(0f, 4f)]
		private float m_CaretBlinkRate = 0.85f;

		// Token: 0x0400022A RID: 554
		[SerializeField]
		[Range(1f, 5f)]
		private int m_CaretWidth = 1;

		// Token: 0x0400022B RID: 555
		[SerializeField]
		private bool m_ReadOnly;

		// Token: 0x0400022C RID: 556
		[SerializeField]
		private bool m_RichText = true;

		// Token: 0x0400022D RID: 557
		protected int m_StringPosition;

		// Token: 0x0400022E RID: 558
		protected int m_StringSelectPosition;

		// Token: 0x0400022F RID: 559
		protected int m_CaretPosition;

		// Token: 0x04000230 RID: 560
		protected int m_CaretSelectPosition;

		// Token: 0x04000231 RID: 561
		private RectTransform caretRectTrans;

		// Token: 0x04000232 RID: 562
		protected UIVertex[] m_CursorVerts;

		// Token: 0x04000233 RID: 563
		private CanvasRenderer m_CachedInputRenderer;

		// Token: 0x04000234 RID: 564
		private Vector2 m_LastPosition;

		// Token: 0x04000235 RID: 565
		[NonSerialized]
		protected Mesh m_Mesh;

		// Token: 0x04000236 RID: 566
		private bool m_AllowInput;

		// Token: 0x04000237 RID: 567
		private bool m_ShouldActivateNextUpdate;

		// Token: 0x04000238 RID: 568
		private bool m_UpdateDrag;

		// Token: 0x04000239 RID: 569
		private bool m_DragPositionOutOfBounds;

		// Token: 0x0400023A RID: 570
		private const float kHScrollSpeed = 0.05f;

		// Token: 0x0400023B RID: 571
		private const float kVScrollSpeed = 0.1f;

		// Token: 0x0400023C RID: 572
		protected bool m_CaretVisible;

		// Token: 0x0400023D RID: 573
		private Coroutine m_BlinkCoroutine;

		// Token: 0x0400023E RID: 574
		private float m_BlinkStartTime;

		// Token: 0x0400023F RID: 575
		private Coroutine m_DragCoroutine;

		// Token: 0x04000240 RID: 576
		private string m_OriginalText = "";

		// Token: 0x04000241 RID: 577
		private bool m_WasCanceled;

		// Token: 0x04000242 RID: 578
		private bool m_HasDoneFocusTransition;

		// Token: 0x04000243 RID: 579
		private WaitForSecondsRealtime m_WaitForSecondsRealtime;

		// Token: 0x04000244 RID: 580
		private bool m_PreventCallback;

		// Token: 0x04000245 RID: 581
		private bool m_TouchKeyboardAllowsInPlaceEditing;

		// Token: 0x04000246 RID: 582
		private bool m_IsTextComponentUpdateRequired;

		// Token: 0x04000247 RID: 583
		private bool m_isLastKeyBackspace;

		// Token: 0x04000248 RID: 584
		private float m_PointerDownClickStartTime;

		// Token: 0x04000249 RID: 585
		private float m_KeyDownStartTime;

		// Token: 0x0400024A RID: 586
		private float m_DoubleClickDelay = 0.5f;

		// Token: 0x0400024B RID: 587
		private const string kEmailSpecialCharacters = "!#$%&'*+-/=?^_`{|}~";

		// Token: 0x0400024C RID: 588
		private bool m_IsCompositionActive;

		// Token: 0x0400024D RID: 589
		private bool m_ShouldUpdateIMEWindowPosition;

		// Token: 0x0400024E RID: 590
		private int m_PreviousIMEInsertionLine;

		// Token: 0x0400024F RID: 591
		[SerializeField]
		protected TMP_FontAsset m_GlobalFontAsset;

		// Token: 0x04000250 RID: 592
		[SerializeField]
		protected bool m_OnFocusSelectAll = true;

		// Token: 0x04000251 RID: 593
		protected bool m_isSelectAll;

		// Token: 0x04000252 RID: 594
		[SerializeField]
		protected bool m_ResetOnDeActivation = true;

		// Token: 0x04000253 RID: 595
		private bool m_SelectionStillActive;

		// Token: 0x04000254 RID: 596
		private bool m_ReleaseSelection;

		// Token: 0x04000255 RID: 597
		private GameObject m_PreviouslySelectedObject;

		// Token: 0x04000256 RID: 598
		[SerializeField]
		private bool m_RestoreOriginalTextOnEscape = true;

		// Token: 0x04000257 RID: 599
		[SerializeField]
		protected bool m_isRichTextEditingAllowed;

		// Token: 0x04000258 RID: 600
		[SerializeField]
		protected int m_LineLimit;

		// Token: 0x04000259 RID: 601
		[SerializeField]
		protected TMP_InputValidator m_InputValidator;

		// Token: 0x0400025A RID: 602
		private bool m_isSelected;

		// Token: 0x0400025B RID: 603
		private bool m_IsStringPositionDirty;

		// Token: 0x0400025C RID: 604
		private bool m_IsCaretPositionDirty;

		// Token: 0x0400025D RID: 605
		private bool m_forceRectTransformAdjustment;

		// Token: 0x0400025E RID: 606
		private Event m_ProcessingEvent = new Event();

		// Token: 0x0200008A RID: 138
		public enum ContentType
		{
			// Token: 0x040005AF RID: 1455
			Standard,
			// Token: 0x040005B0 RID: 1456
			Autocorrected,
			// Token: 0x040005B1 RID: 1457
			IntegerNumber,
			// Token: 0x040005B2 RID: 1458
			DecimalNumber,
			// Token: 0x040005B3 RID: 1459
			Alphanumeric,
			// Token: 0x040005B4 RID: 1460
			Name,
			// Token: 0x040005B5 RID: 1461
			EmailAddress,
			// Token: 0x040005B6 RID: 1462
			Password,
			// Token: 0x040005B7 RID: 1463
			Pin,
			// Token: 0x040005B8 RID: 1464
			Custom
		}

		// Token: 0x0200008B RID: 139
		public enum InputType
		{
			// Token: 0x040005BA RID: 1466
			Standard,
			// Token: 0x040005BB RID: 1467
			AutoCorrect,
			// Token: 0x040005BC RID: 1468
			Password
		}

		// Token: 0x0200008C RID: 140
		public enum CharacterValidation
		{
			// Token: 0x040005BE RID: 1470
			None,
			// Token: 0x040005BF RID: 1471
			Digit,
			// Token: 0x040005C0 RID: 1472
			Integer,
			// Token: 0x040005C1 RID: 1473
			Decimal,
			// Token: 0x040005C2 RID: 1474
			Alphanumeric,
			// Token: 0x040005C3 RID: 1475
			Name,
			// Token: 0x040005C4 RID: 1476
			Regex,
			// Token: 0x040005C5 RID: 1477
			EmailAddress,
			// Token: 0x040005C6 RID: 1478
			CustomValidator
		}

		// Token: 0x0200008D RID: 141
		public enum LineType
		{
			// Token: 0x040005C8 RID: 1480
			SingleLine,
			// Token: 0x040005C9 RID: 1481
			MultiLineSubmit,
			// Token: 0x040005CA RID: 1482
			MultiLineNewline
		}

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x06000617 RID: 1559
		public delegate char OnValidateInput(string text, int charIndex, char addedChar);

		// Token: 0x0200008F RID: 143
		[Serializable]
		public class SubmitEvent : UnityEvent<string>
		{
			// Token: 0x0600061A RID: 1562 RVA: 0x000387DB File Offset: 0x000369DB
			public SubmitEvent()
			{
			}
		}

		// Token: 0x02000090 RID: 144
		[Serializable]
		public class OnChangeEvent : UnityEvent<string>
		{
			// Token: 0x0600061B RID: 1563 RVA: 0x000387E3 File Offset: 0x000369E3
			public OnChangeEvent()
			{
			}
		}

		// Token: 0x02000091 RID: 145
		[Serializable]
		public class SelectionEvent : UnityEvent<string>
		{
			// Token: 0x0600061C RID: 1564 RVA: 0x000387EB File Offset: 0x000369EB
			public SelectionEvent()
			{
			}
		}

		// Token: 0x02000092 RID: 146
		[Serializable]
		public class TextSelectionEvent : UnityEvent<string, int, int>
		{
			// Token: 0x0600061D RID: 1565 RVA: 0x000387F3 File Offset: 0x000369F3
			public TextSelectionEvent()
			{
			}
		}

		// Token: 0x02000093 RID: 147
		[Serializable]
		public class TouchScreenKeyboardEvent : UnityEvent<TouchScreenKeyboard.Status>
		{
			// Token: 0x0600061E RID: 1566 RVA: 0x000387FB File Offset: 0x000369FB
			public TouchScreenKeyboardEvent()
			{
			}
		}

		// Token: 0x02000094 RID: 148
		protected enum EditState
		{
			// Token: 0x040005CC RID: 1484
			Continue,
			// Token: 0x040005CD RID: 1485
			Finish
		}

		// Token: 0x02000095 RID: 149
		[CompilerGenerated]
		private sealed class <CaretBlink>d__276 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x0600061F RID: 1567 RVA: 0x00038803 File Offset: 0x00036A03
			[DebuggerHidden]
			public <CaretBlink>d__276(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x00038812 File Offset: 0x00036A12
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x00038814 File Offset: 0x00036A14
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TMP_InputField tmp_InputField = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					tmp_InputField.m_CaretVisible = true;
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
				if ((!tmp_InputField.isFocused && !tmp_InputField.m_SelectionStillActive) || tmp_InputField.m_CaretBlinkRate <= 0f)
				{
					tmp_InputField.m_BlinkCoroutine = null;
					return false;
				}
				float num2 = 1f / tmp_InputField.m_CaretBlinkRate;
				bool flag = (Time.unscaledTime - tmp_InputField.m_BlinkStartTime) % num2 < num2 / 2f;
				if (tmp_InputField.m_CaretVisible != flag)
				{
					tmp_InputField.m_CaretVisible = flag;
					if (!tmp_InputField.hasSelection)
					{
						tmp_InputField.MarkGeometryAsDirty();
					}
				}
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000622 RID: 1570 RVA: 0x000388E9 File Offset: 0x00036AE9
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x000388F1 File Offset: 0x00036AF1
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000624 RID: 1572 RVA: 0x000388F8 File Offset: 0x00036AF8
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040005CE RID: 1486
			private int <>1__state;

			// Token: 0x040005CF RID: 1487
			private object <>2__current;

			// Token: 0x040005D0 RID: 1488
			public TMP_InputField <>4__this;
		}

		// Token: 0x02000096 RID: 150
		[CompilerGenerated]
		private sealed class <MouseDragOutsideRect>d__294 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06000625 RID: 1573 RVA: 0x00038900 File Offset: 0x00036B00
			[DebuggerHidden]
			public <MouseDragOutsideRect>d__294(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x0003890F File Offset: 0x00036B0F
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x00038914 File Offset: 0x00036B14
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TMP_InputField tmp_InputField = this;
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
				if (!tmp_InputField.m_UpdateDrag || !tmp_InputField.m_DragPositionOutOfBounds)
				{
					tmp_InputField.m_DragCoroutine = null;
					return false;
				}
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(tmp_InputField.textViewport, eventData.position, eventData.pressEventCamera, out vector);
				Rect rect = tmp_InputField.textViewport.rect;
				if (tmp_InputField.multiLine)
				{
					if (vector.y > rect.yMax)
					{
						tmp_InputField.MoveUp(true, true);
					}
					else if (vector.y < rect.yMin)
					{
						tmp_InputField.MoveDown(true, true);
					}
				}
				else if (vector.x < rect.xMin)
				{
					tmp_InputField.MoveLeft(true, false);
				}
				else if (vector.x > rect.xMax)
				{
					tmp_InputField.MoveRight(true, false);
				}
				tmp_InputField.UpdateLabel();
				float num2 = tmp_InputField.multiLine ? 0.1f : 0.05f;
				if (tmp_InputField.m_WaitForSecondsRealtime == null)
				{
					tmp_InputField.m_WaitForSecondsRealtime = new WaitForSecondsRealtime(num2);
				}
				else
				{
					tmp_InputField.m_WaitForSecondsRealtime.waitTime = num2;
				}
				this.<>2__current = tmp_InputField.m_WaitForSecondsRealtime;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000628 RID: 1576 RVA: 0x00038A58 File Offset: 0x00036C58
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x00038A60 File Offset: 0x00036C60
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x0600062A RID: 1578 RVA: 0x00038A67 File Offset: 0x00036C67
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040005D1 RID: 1489
			private int <>1__state;

			// Token: 0x040005D2 RID: 1490
			private object <>2__current;

			// Token: 0x040005D3 RID: 1491
			public TMP_InputField <>4__this;

			// Token: 0x040005D4 RID: 1492
			public PointerEventData eventData;
		}
	}
}
