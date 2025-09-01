using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011B RID: 283
	public abstract class BaseField<TValueType> : BindableElement, INotifyValueChanged<TValueType>, IMixedValueSupport, IPrefixLabel
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00024724 File Offset: 0x00022924
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0002473C File Offset: 0x0002293C
		internal VisualElement visualInput
		{
			get
			{
				return this.m_VisualInput;
			}
			set
			{
				bool flag = this.m_VisualInput != null;
				if (flag)
				{
					bool flag2 = this.m_VisualInput.parent == this;
					if (flag2)
					{
						this.m_VisualInput.RemoveFromHierarchy();
					}
					this.m_VisualInput = null;
				}
				bool flag3 = value != null;
				if (flag3)
				{
					this.m_VisualInput = value;
				}
				else
				{
					this.m_VisualInput = new VisualElement
					{
						pickingMode = PickingMode.Ignore
					};
				}
				this.m_VisualInput.focusable = true;
				this.m_VisualInput.AddToClassList(BaseField<TValueType>.inputUssClassName);
				base.Add(this.m_VisualInput);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x000247D4 File Offset: 0x000229D4
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x000247EC File Offset: 0x000229EC
		protected TValueType rawValue
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000247F8 File Offset: 0x000229F8
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x00024810 File Offset: 0x00022A10
		public virtual TValueType value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_Value, value);
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(this.m_Value, value))
						{
							pooled.target = this;
							this.SetValueWithoutNotify(value);
							this.SendEvent(pooled);
						}
					}
					else
					{
						this.SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00024898 File Offset: 0x00022A98
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x000248A0 File Offset: 0x00022AA0
		public Label labelElement
		{
			[CompilerGenerated]
			get
			{
				return this.<labelElement>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<labelElement>k__BackingField = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x000248AC File Offset: 0x00022AAC
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x000248CC File Offset: 0x00022ACC
		public string label
		{
			get
			{
				return this.labelElement.text;
			}
			set
			{
				bool flag = this.labelElement.text != value;
				if (flag)
				{
					this.labelElement.text = value;
					bool flag2 = string.IsNullOrEmpty(this.labelElement.text);
					if (flag2)
					{
						base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						this.labelElement.RemoveFromHierarchy();
					}
					else
					{
						bool flag3 = !base.Contains(this.labelElement);
						if (flag3)
						{
							base.Insert(0, this.labelElement);
							base.RemoveFromClassList(BaseField<TValueType>.noLabelVariantUssClassName);
						}
					}
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0002495F File Offset: 0x00022B5F
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x00024968 File Offset: 0x00022B68
		public bool showMixedValue
		{
			get
			{
				return this.m_ShowMixedValue;
			}
			set
			{
				bool flag = value == this.m_ShowMixedValue;
				if (!flag)
				{
					this.m_ShowMixedValue = value;
					this.UpdateMixedValueContent();
				}
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00024994 File Offset: 0x00022B94
		protected Label mixedValueLabel
		{
			get
			{
				bool flag = this.m_MixedValueLabel == null;
				if (flag)
				{
					this.m_MixedValueLabel = new Label(BaseField<TValueType>.mixedValueString)
					{
						focusable = true,
						tabIndex = -1
					};
					this.m_MixedValueLabel.AddToClassList(BaseField<TValueType>.labelUssClassName);
					this.m_MixedValueLabel.AddToClassList(BaseField<TValueType>.mixedValueLabelUssClassName);
				}
				return this.m_MixedValueLabel;
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00024A00 File Offset: 0x00022C00
		internal BaseField(string label)
		{
			base.isCompositeRoot = true;
			base.focusable = true;
			base.tabIndex = 0;
			base.excludeFromFocusRing = true;
			base.delegatesFocus = true;
			base.AddToClassList(BaseField<TValueType>.ussClassName);
			this.labelElement = new Label
			{
				focusable = true,
				tabIndex = -1
			};
			this.labelElement.AddToClassList(BaseField<TValueType>.labelUssClassName);
			bool flag = label != null;
			if (flag)
			{
				this.label = label;
			}
			else
			{
				base.AddToClassList(BaseField<TValueType>.noLabelVariantUssClassName);
			}
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_VisualInput = null;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00024AB3 File Offset: 0x00022CB3
		protected BaseField(string label, VisualElement visualInput) : this(label)
		{
			this.visualInput = visualInput;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00024AC8 File Offset: 0x00022CC8
		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			for (VisualElement parent = base.parent; parent != null; parent = parent.parent)
			{
				bool flag = parent.ClassListContains("unity-inspector-element");
				if (flag)
				{
					this.m_LabelWidthRatio = 0.45f;
					this.m_LabelExtraPadding = 2f;
					this.m_LabelBaseMinWidth = 120f;
					base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), TrickleDown.NoTrickleDown);
					base.AddToClassList(BaseField<TValueType>.inspectorFieldUssClassName);
					this.m_CachedInspectorElement = parent;
					this.m_CachedListAndFoldoutDepth = this.GetListAndFoldoutDepth();
					base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnInspectorFieldGeometryChanged), TrickleDown.NoTrickleDown);
					break;
				}
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00024B70 File Offset: 0x00022D70
		private void OnCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			float labelWidthRatio;
			bool flag = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelWidthRatioProperty, out labelWidthRatio);
			if (flag)
			{
				this.m_LabelWidthRatio = labelWidthRatio;
			}
			float labelExtraPadding;
			bool flag2 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelExtraPaddingProperty, out labelExtraPadding);
			if (flag2)
			{
				this.m_LabelExtraPadding = labelExtraPadding;
			}
			float labelBaseMinWidth;
			bool flag3 = evt.customStyle.TryGetValue(BaseField<TValueType>.s_LabelBaseMinWidthProperty, out labelBaseMinWidth);
			if (flag3)
			{
				this.m_LabelBaseMinWidth = labelBaseMinWidth;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00024BDF File Offset: 0x00022DDF
		private void OnInspectorFieldGeometryChanged(GeometryChangedEvent e)
		{
			this.AlignLabel();
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00024BEC File Offset: 0x00022DEC
		private void AlignLabel()
		{
			bool flag = !base.ClassListContains(BaseField<TValueType>.alignedFieldUssClassName);
			if (!flag)
			{
				int num = 15 * this.m_CachedListAndFoldoutDepth;
				float num2 = base.resolvedStyle.paddingLeft + base.resolvedStyle.paddingRight + base.resolvedStyle.marginLeft + base.resolvedStyle.marginRight;
				num2 += this.m_CachedInspectorElement.resolvedStyle.paddingLeft + this.m_CachedInspectorElement.resolvedStyle.paddingRight + this.m_CachedInspectorElement.resolvedStyle.marginLeft + this.m_CachedInspectorElement.resolvedStyle.marginRight;
				num2 += this.labelElement.resolvedStyle.paddingLeft + this.labelElement.resolvedStyle.paddingRight + this.labelElement.resolvedStyle.marginLeft + this.labelElement.resolvedStyle.marginRight;
				num2 += base.resolvedStyle.paddingLeft + base.resolvedStyle.paddingRight + base.resolvedStyle.marginLeft + base.resolvedStyle.marginRight;
				num2 += this.m_LabelExtraPadding;
				num2 += (float)num;
				this.labelElement.style.minWidth = Mathf.Max(this.m_LabelBaseMinWidth - (float)num, 0f);
				float num3 = this.m_CachedInspectorElement.resolvedStyle.width * this.m_LabelWidthRatio - num2;
				bool flag2 = Mathf.Abs(this.labelElement.resolvedStyle.width - num3) > 1E-30f;
				if (flag2)
				{
					this.labelElement.style.width = Mathf.Max(0f, num3);
				}
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00024DA2 File Offset: 0x00022FA2
		protected virtual void UpdateMixedValueContent()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00024DAC File Offset: 0x00022FAC
		public virtual void SetValueWithoutNotify(TValueType newValue)
		{
			this.m_Value = newValue;
			bool flag = !string.IsNullOrEmpty(base.viewDataKey);
			if (flag)
			{
				base.SaveViewData();
			}
			base.MarkDirtyRepaint();
			bool showMixedValue = this.showMixedValue;
			if (showMixedValue)
			{
				this.UpdateMixedValueContent();
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00024DF4 File Offset: 0x00022FF4
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			bool flag = this.m_VisualInput != null;
			if (flag)
			{
				string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
				TValueType value = this.m_Value;
				base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
				bool flag2 = !EqualityComparer<TValueType>.Default.Equals(value, this.m_Value);
				if (flag2)
				{
					using (ChangeEvent<TValueType> pooled = ChangeEvent<TValueType>.GetPooled(value, this.m_Value))
					{
						pooled.target = this;
						this.SetValueWithoutNotify(this.m_Value);
						this.SendEvent(pooled);
					}
				}
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00024E98 File Offset: 0x00023098
		internal override Rect GetTooltipRect()
		{
			return (!string.IsNullOrEmpty(this.label)) ? this.labelElement.worldBound : base.worldBound;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00024ECC File Offset: 0x000230CC
		// Note: this type is marked as 'beforefieldinit'.
		static BaseField()
		{
		}

		// Token: 0x040003CE RID: 974
		public static readonly string ussClassName = "unity-base-field";

		// Token: 0x040003CF RID: 975
		public static readonly string labelUssClassName = BaseField<TValueType>.ussClassName + "__label";

		// Token: 0x040003D0 RID: 976
		public static readonly string inputUssClassName = BaseField<TValueType>.ussClassName + "__input";

		// Token: 0x040003D1 RID: 977
		public static readonly string noLabelVariantUssClassName = BaseField<TValueType>.ussClassName + "--no-label";

		// Token: 0x040003D2 RID: 978
		public static readonly string labelDraggerVariantUssClassName = BaseField<TValueType>.labelUssClassName + "--with-dragger";

		// Token: 0x040003D3 RID: 979
		public static readonly string mixedValueLabelUssClassName = BaseField<TValueType>.labelUssClassName + "--mixed-value";

		// Token: 0x040003D4 RID: 980
		public static readonly string alignedFieldUssClassName = BaseField<TValueType>.ussClassName + "__aligned";

		// Token: 0x040003D5 RID: 981
		private static readonly string inspectorFieldUssClassName = BaseField<TValueType>.ussClassName + "__inspector-field";

		// Token: 0x040003D6 RID: 982
		private const int kIndentPerLevel = 15;

		// Token: 0x040003D7 RID: 983
		protected internal static readonly string mixedValueString = "—";

		// Token: 0x040003D8 RID: 984
		protected internal static readonly PropertyName serializedPropertyCopyName = "SerializedPropertyCopyName";

		// Token: 0x040003D9 RID: 985
		private static CustomStyleProperty<float> s_LabelWidthRatioProperty = new CustomStyleProperty<float>("--unity-property-field-label-width-ratio");

		// Token: 0x040003DA RID: 986
		private static CustomStyleProperty<float> s_LabelExtraPaddingProperty = new CustomStyleProperty<float>("--unity-property-field-label-extra-padding");

		// Token: 0x040003DB RID: 987
		private static CustomStyleProperty<float> s_LabelBaseMinWidthProperty = new CustomStyleProperty<float>("--unity-property-field-label-base-min-width");

		// Token: 0x040003DC RID: 988
		private float m_LabelWidthRatio;

		// Token: 0x040003DD RID: 989
		private float m_LabelExtraPadding;

		// Token: 0x040003DE RID: 990
		private float m_LabelBaseMinWidth;

		// Token: 0x040003DF RID: 991
		private VisualElement m_VisualInput;

		// Token: 0x040003E0 RID: 992
		[SerializeField]
		private TValueType m_Value;

		// Token: 0x040003E1 RID: 993
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Label <labelElement>k__BackingField;

		// Token: 0x040003E2 RID: 994
		private bool m_ShowMixedValue;

		// Token: 0x040003E3 RID: 995
		private Label m_MixedValueLabel;

		// Token: 0x040003E4 RID: 996
		private VisualElement m_CachedInspectorElement;

		// Token: 0x040003E5 RID: 997
		private int m_CachedListAndFoldoutDepth;

		// Token: 0x0200011C RID: 284
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x0600094F RID: 2383 RVA: 0x00024FB5 File Offset: 0x000231B5
			public UxmlTraits()
			{
				base.focusIndex.defaultValue = 0;
				base.focusable.defaultValue = true;
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x00024FF0 File Offset: 0x000231F0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((BaseField<TValueType>)ve).label = this.m_Label.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000951 RID: 2385 RVA: 0x00025018 File Offset: 0x00023218
			internal static List<string> ParseChoiceList(string choicesFromBag)
			{
				bool flag = string.IsNullOrEmpty(choicesFromBag.Trim());
				List<string> result;
				if (flag)
				{
					result = null;
				}
				else
				{
					string[] array = choicesFromBag.Split(new char[]
					{
						','
					});
					bool flag2 = array.Length != 0;
					if (flag2)
					{
						List<string> list = new List<string>();
						foreach (string text in array)
						{
							list.Add(text.Trim());
						}
						result = list;
					}
					else
					{
						result = null;
					}
				}
				return result;
			}

			// Token: 0x040003E6 RID: 998
			private UxmlStringAttributeDescription m_Label = new UxmlStringAttributeDescription
			{
				name = "label"
			};
		}
	}
}
