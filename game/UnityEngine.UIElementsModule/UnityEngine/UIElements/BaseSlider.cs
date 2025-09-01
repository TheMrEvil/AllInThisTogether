using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011F RID: 287
	public abstract class BaseSlider<TValueType> : BaseField<TValueType> where TValueType : IComparable<TValueType>
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x000250E5 File Offset: 0x000232E5
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x000250ED File Offset: 0x000232ED
		internal VisualElement dragContainer
		{
			[CompilerGenerated]
			get
			{
				return this.<dragContainer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<dragContainer>k__BackingField = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x000250F6 File Offset: 0x000232F6
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x000250FE File Offset: 0x000232FE
		internal VisualElement dragElement
		{
			[CompilerGenerated]
			get
			{
				return this.<dragElement>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<dragElement>k__BackingField = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00025107 File Offset: 0x00023307
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0002510F File Offset: 0x0002330F
		internal VisualElement dragBorderElement
		{
			[CompilerGenerated]
			get
			{
				return this.<dragBorderElement>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<dragBorderElement>k__BackingField = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00025118 File Offset: 0x00023318
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00025120 File Offset: 0x00023320
		internal TextField inputTextField
		{
			[CompilerGenerated]
			get
			{
				return this.<inputTextField>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<inputTextField>k__BackingField = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0002512C File Offset: 0x0002332C
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x00025144 File Offset: 0x00023344
		public TValueType lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_LowValue, value);
				if (flag)
				{
					this.m_LowValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00025188 File Offset: 0x00023388
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x000251A0 File Offset: 0x000233A0
		public TValueType highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_HighValue, value);
				if (flag)
				{
					this.m_HighValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000251E4 File Offset: 0x000233E4
		internal void SetHighValueWithoutNotify(TValueType newHighValue)
		{
			this.m_HighValue = newHighValue;
			TValueType valueWithoutNotify = this.clamped ? this.GetClampedValue(this.value) : this.value;
			this.SetValueWithoutNotify(valueWithoutNotify);
			this.UpdateDragElementPosition();
			base.SaveViewData();
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0002522C File Offset: 0x0002342C
		public TValueType range
		{
			get
			{
				return this.SliderRange();
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00025244 File Offset: 0x00023444
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x0002525C File Offset: 0x0002345C
		public virtual float pageSize
		{
			get
			{
				return this.m_PageSize;
			}
			set
			{
				this.m_PageSize = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00025268 File Offset: 0x00023468
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00025280 File Offset: 0x00023480
		public virtual bool showInputField
		{
			get
			{
				return this.m_ShowInputField;
			}
			set
			{
				bool flag = this.m_ShowInputField != value;
				if (flag)
				{
					this.m_ShowInputField = value;
					this.UpdateTextFieldVisibility();
				}
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x000252AE File Offset: 0x000234AE
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x000252B6 File Offset: 0x000234B6
		internal bool clamped
		{
			[CompilerGenerated]
			get
			{
				return this.<clamped>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<clamped>k__BackingField = value;
			}
		} = true;

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x000252BF File Offset: 0x000234BF
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x000252C7 File Offset: 0x000234C7
		internal ClampedDragger<TValueType> clampedDragger
		{
			[CompilerGenerated]
			get
			{
				return this.<clampedDragger>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<clampedDragger>k__BackingField = value;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000252D0 File Offset: 0x000234D0
		private TValueType Clamp(TValueType value, TValueType lowBound, TValueType highBound)
		{
			TValueType result = value;
			bool flag = lowBound.CompareTo(value) > 0;
			if (flag)
			{
				result = lowBound;
			}
			else
			{
				bool flag2 = highBound.CompareTo(value) < 0;
				if (flag2)
				{
					result = highBound;
				}
			}
			return result;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002531C File Offset: 0x0002351C
		private TValueType GetClampedValue(TValueType newValue)
		{
			TValueType tvalueType = this.lowValue;
			TValueType tvalueType2 = this.highValue;
			bool flag = tvalueType.CompareTo(tvalueType2) > 0;
			if (flag)
			{
				TValueType tvalueType3 = tvalueType;
				tvalueType = tvalueType2;
				tvalueType2 = tvalueType3;
			}
			return this.Clamp(newValue, tvalueType, tvalueType2);
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00025364 File Offset: 0x00023564
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0002537C File Offset: 0x0002357C
		public override TValueType value
		{
			get
			{
				return base.value;
			}
			set
			{
				TValueType value2 = this.clamped ? this.GetClampedValue(value) : value;
				base.value = value2;
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000253A8 File Offset: 0x000235A8
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			TValueType valueWithoutNotify = this.clamped ? this.GetClampedValue(newValue) : newValue;
			base.SetValueWithoutNotify(valueWithoutNotify);
			this.UpdateDragElementPosition();
			this.UpdateTextFieldValue();
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000253E0 File Offset: 0x000235E0
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x000253F8 File Offset: 0x000235F8
		public SliderDirection direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value;
				bool flag = this.m_Direction == SliderDirection.Horizontal;
				if (flag)
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
				}
				else
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00025450 File Offset: 0x00023650
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x00025468 File Offset: 0x00023668
		public bool inverted
		{
			get
			{
				return this.m_Inverted;
			}
			set
			{
				bool flag = this.m_Inverted != value;
				if (flag)
				{
					this.m_Inverted = value;
					this.UpdateDragElementPosition();
				}
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00025498 File Offset: 0x00023698
		internal BaseSlider(string label, TValueType start, TValueType end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f) : base(label, null)
		{
			base.AddToClassList(BaseSlider<TValueType>.ussClassName);
			base.labelElement.AddToClassList(BaseSlider<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(BaseSlider<TValueType>.inputUssClassName);
			this.direction = direction;
			this.pageSize = pageSize;
			this.lowValue = start;
			this.highValue = end;
			base.pickingMode = PickingMode.Ignore;
			this.dragContainer = new VisualElement
			{
				name = "unity-drag-container"
			};
			this.dragContainer.AddToClassList(BaseSlider<TValueType>.dragContainerUssClassName);
			base.visualInput.Add(this.dragContainer);
			VisualElement visualElement = new VisualElement
			{
				name = "unity-tracker"
			};
			visualElement.AddToClassList(BaseSlider<TValueType>.trackerUssClassName);
			this.dragContainer.Add(visualElement);
			this.dragBorderElement = new VisualElement
			{
				name = "unity-dragger-border"
			};
			this.dragBorderElement.AddToClassList(BaseSlider<TValueType>.draggerBorderUssClassName);
			this.dragContainer.Add(this.dragBorderElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			this.dragElement.AddToClassList(BaseSlider<TValueType>.draggerUssClassName);
			this.dragContainer.Add(this.dragElement);
			this.clampedDragger = new ClampedDragger<TValueType>(this, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			this.dragContainer.pickingMode = PickingMode.Position;
			this.dragContainer.AddManipulator(this.clampedDragger);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			this.UpdateTextFieldVisibility();
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00025670 File Offset: 0x00023870
		protected static float GetClosestPowerOfTen(float positiveNumber)
		{
			bool flag = positiveNumber <= 0f;
			float result;
			if (flag)
			{
				result = 1f;
			}
			else
			{
				result = Mathf.Pow(10f, (float)Mathf.RoundToInt(Mathf.Log10(positiveNumber)));
			}
			return result;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000256B0 File Offset: 0x000238B0
		protected static float RoundToMultipleOf(float value, float roundingValue)
		{
			bool flag = roundingValue == 0f;
			float result;
			if (flag)
			{
				result = value;
			}
			else
			{
				result = Mathf.Round(value / roundingValue) * roundingValue;
			}
			return result;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000256DC File Offset: 0x000238DC
		private void ClampValue()
		{
			this.value = base.rawValue;
		}

		// Token: 0x06000977 RID: 2423
		internal abstract TValueType SliderLerpUnclamped(TValueType a, TValueType b, float interpolant);

		// Token: 0x06000978 RID: 2424
		internal abstract float SliderNormalizeValue(TValueType currentValue, TValueType lowerValue, TValueType higherValue);

		// Token: 0x06000979 RID: 2425
		internal abstract TValueType SliderRange();

		// Token: 0x0600097A RID: 2426
		internal abstract TValueType ParseStringToValue(string stringValue);

		// Token: 0x0600097B RID: 2427
		internal abstract void ComputeValueFromKey(BaseSlider<TValueType>.SliderKey sliderKey, bool isShift);

		// Token: 0x0600097C RID: 2428 RVA: 0x000256EC File Offset: 0x000238EC
		private TValueType SliderLerpDirectionalUnclamped(TValueType a, TValueType b, float positionInterpolant)
		{
			float interpolant = (this.direction == SliderDirection.Vertical) ? (1f - positionInterpolant) : positionInterpolant;
			bool inverted = this.inverted;
			TValueType result;
			if (inverted)
			{
				result = this.SliderLerpUnclamped(b, a, interpolant);
			}
			else
			{
				result = this.SliderLerpUnclamped(a, b, interpolant);
			}
			return result;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00025734 File Offset: 0x00023934
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				Vector2 delta = this.clampedDragger.delta;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x + delta.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y + delta.y);
				}
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000257EC File Offset: 0x000239EC
		private void ComputeValueAndDirectionFromDrag(float sliderLength, float dragElementLength, float dragElementPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < 1E-30f;
			if (!flag)
			{
				bool clamped = this.clamped;
				float positionInterpolant;
				if (clamped)
				{
					positionInterpolant = Mathf.Max(0f, Mathf.Min(dragElementPos, num)) / num;
				}
				else
				{
					positionInterpolant = dragElementPos / num;
				}
				this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, positionInterpolant);
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00025850 File Offset: 0x00023A50
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.None;
				if (flag2)
				{
					bool flag3 = Mathf.Approximately(this.pageSize, 0f);
					if (flag3)
					{
						float x = (this.direction == SliderDirection.Horizontal) ? (this.clampedDragger.startMousePosition.x - this.dragElement.resolvedStyle.width / 2f) : this.dragElement.transform.position.x;
						float y = (this.direction == SliderDirection.Horizontal) ? this.dragElement.transform.position.y : (this.clampedDragger.startMousePosition.y - this.dragElement.resolvedStyle.height / 2f);
						Vector3 position = new Vector3(x, y, 0f);
						this.dragElement.transform.position = position;
						this.dragBorderElement.transform.position = position;
						this.m_DragElementStartPos = new Rect(x, y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.Free;
						bool flag4 = this.direction == SliderDirection.Horizontal;
						if (flag4)
						{
							this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x);
						}
						else
						{
							this.ComputeValueAndDirectionFromDrag(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y);
						}
						return;
					}
					this.m_DragElementStartPos = new Rect(this.dragElement.transform.position.x, this.dragElement.transform.position.y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
				}
				bool flag5 = this.direction == SliderDirection.Horizontal;
				if (flag5)
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.dragElement.transform.position.x, this.clampedDragger.lastMousePosition.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromClick(this.dragContainer.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.dragElement.transform.position.y, this.clampedDragger.lastMousePosition.y);
				}
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00025B1C File Offset: 0x00023D1C
		private void OnKeyDown(KeyDownEvent evt)
		{
			BaseSlider<TValueType>.SliderKey sliderKey = BaseSlider<TValueType>.SliderKey.None;
			bool flag = this.direction == SliderDirection.Horizontal;
			bool flag2 = (flag && evt.keyCode == KeyCode.Home) || (!flag && evt.keyCode == KeyCode.End);
			if (flag2)
			{
				sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Highest : BaseSlider<TValueType>.SliderKey.Lowest);
			}
			else
			{
				bool flag3 = (flag && evt.keyCode == KeyCode.End) || (!flag && evt.keyCode == KeyCode.Home);
				if (flag3)
				{
					sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Lowest : BaseSlider<TValueType>.SliderKey.Highest);
				}
				else
				{
					bool flag4 = (flag && evt.keyCode == KeyCode.PageUp) || (!flag && evt.keyCode == KeyCode.PageDown);
					if (flag4)
					{
						sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.HigherPage : BaseSlider<TValueType>.SliderKey.LowerPage);
					}
					else
					{
						bool flag5 = (flag && evt.keyCode == KeyCode.PageDown) || (!flag && evt.keyCode == KeyCode.PageUp);
						if (flag5)
						{
							sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.LowerPage : BaseSlider<TValueType>.SliderKey.HigherPage);
						}
						else
						{
							bool flag6 = (flag && evt.keyCode == KeyCode.LeftArrow) || (!flag && evt.keyCode == KeyCode.DownArrow);
							if (flag6)
							{
								sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Higher : BaseSlider<TValueType>.SliderKey.Lower);
							}
							else
							{
								bool flag7 = (flag && evt.keyCode == KeyCode.RightArrow) || (!flag && evt.keyCode == KeyCode.UpArrow);
								if (flag7)
								{
									sliderKey = (this.inverted ? BaseSlider<TValueType>.SliderKey.Lower : BaseSlider<TValueType>.SliderKey.Higher);
								}
							}
						}
					}
				}
			}
			bool flag8 = sliderKey == BaseSlider<TValueType>.SliderKey.None;
			if (!flag8)
			{
				this.ComputeValueFromKey(sliderKey, evt.shiftKey);
				evt.StopPropagation();
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00025CBC File Offset: 0x00023EBC
		internal virtual void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < 1E-30f;
			if (!flag)
			{
				bool flag2 = dragElementLastPos < dragElementPos;
				bool flag3 = dragElementLastPos > dragElementPos + dragElementLength;
				bool flag4 = this.inverted ? flag3 : flag2;
				bool flag5 = this.inverted ? flag2 : flag3;
				float num2 = this.inverted ? (-this.pageSize) : this.pageSize;
				bool flag6 = flag4 && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.LowToHigh;
				if (flag6)
				{
					this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.HighToLow;
					float positionInterpolant = Mathf.Max(0f, Mathf.Min(dragElementPos - num2, num)) / num;
					this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, positionInterpolant);
				}
				else
				{
					bool flag7 = flag5 && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.HighToLow;
					if (flag7)
					{
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.LowToHigh;
						float positionInterpolant2 = Mathf.Max(0f, Mathf.Min(dragElementPos + num2, num)) / num;
						this.value = this.SliderLerpDirectionalUnclamped(this.lowValue, this.highValue, positionInterpolant2);
					}
				}
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00025DEC File Offset: 0x00023FEC
		public void AdjustDragElement(float factor)
		{
			bool flag = factor < 1f;
			this.dragElement.visible = flag;
			bool flag2 = flag;
			if (flag2)
			{
				IStyle style = this.dragElement.style;
				this.dragElement.style.visibility = StyleKeyword.Null;
				bool flag3 = this.direction == SliderDirection.Horizontal;
				if (flag3)
				{
					float b = (base.resolvedStyle.minWidth == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minWidth.value;
					style.width = Mathf.Round(Mathf.Max(this.dragContainer.layout.width * factor, b));
				}
				else
				{
					float b2 = (base.resolvedStyle.minHeight == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minHeight.value;
					style.height = Mathf.Round(Mathf.Max(this.dragContainer.layout.height * factor, b2));
				}
			}
			this.dragBorderElement.visible = this.dragElement.visible;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00025F30 File Offset: 0x00024130
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00025F6D File Offset: 0x0002416D
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			this.UpdateDragElementPosition();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00025F80 File Offset: 0x00024180
		private bool SameValues(float a, float b, float epsilon)
		{
			return Mathf.Abs(b - a) < epsilon;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00025FA0 File Offset: 0x000241A0
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = this.SliderNormalizeValue(this.value, this.lowValue, this.highValue);
				float num2 = this.inverted ? (1f - num) : num;
				float epsilon = base.scaledPixelsPerPoint * 0.5f;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					float width = this.dragElement.resolvedStyle.width;
					float num3 = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
					float num4 = this.dragContainer.layout.width - width + num3;
					float num5 = num2 * num4;
					bool flag3 = float.IsNaN(num5);
					if (!flag3)
					{
						float x = this.dragElement.transform.position.x;
						bool flag4 = !this.SameValues(x, num5, epsilon);
						if (flag4)
						{
							Vector3 position = new Vector3(num5, 0f, 0f);
							this.dragElement.transform.position = position;
							this.dragBorderElement.transform.position = position;
						}
					}
				}
				else
				{
					float height = this.dragElement.resolvedStyle.height;
					float num6 = this.dragContainer.resolvedStyle.height - height;
					float num7 = (1f - num2) * num6;
					bool flag5 = float.IsNaN(num7);
					if (!flag5)
					{
						float y = this.dragElement.transform.position.y;
						bool flag6 = !this.SameValues(y, num7, epsilon);
						if (flag6)
						{
							Vector3 position2 = new Vector3(0f, num7, 0f);
							this.dragElement.transform.position = position2;
							this.dragBorderElement.transform.position = position2;
						}
					}
				}
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002618C File Offset: 0x0002438C
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<GeometryChangedEvent>.TypeId();
				if (flag2)
				{
					this.UpdateDragElementPosition((GeometryChangedEvent)evt);
				}
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000261D0 File Offset: 0x000243D0
		private void UpdateTextFieldVisibility()
		{
			bool showInputField = this.showInputField;
			if (showInputField)
			{
				bool flag = this.inputTextField == null;
				if (flag)
				{
					this.inputTextField = new TextField
					{
						name = "unity-text-field"
					};
					this.inputTextField.AddToClassList(BaseSlider<TValueType>.textFieldClassName);
					this.inputTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					base.visualInput.Add(this.inputTextField);
					this.UpdateTextFieldValue();
				}
			}
			else
			{
				bool flag2 = this.inputTextField != null && this.inputTextField.panel != null;
				if (flag2)
				{
					bool flag3 = this.inputTextField.panel != null;
					if (flag3)
					{
						this.inputTextField.RemoveFromHierarchy();
					}
					this.inputTextField.UnregisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnTextFieldValueChange));
					this.inputTextField.UnregisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnTextFieldFocusOut), TrickleDown.NoTrickleDown);
					this.inputTextField = null;
				}
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000262E8 File Offset: 0x000244E8
		private void UpdateTextFieldValue()
		{
			bool flag = this.inputTextField == null;
			if (!flag)
			{
				this.inputTextField.SetValueWithoutNotify(string.Format(CultureInfo.InvariantCulture, "{0:g7}", new object[]
				{
					this.value
				}));
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00026334 File Offset: 0x00024534
		private void OnTextFieldFocusOut(FocusOutEvent evt)
		{
			this.UpdateTextFieldValue();
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00026340 File Offset: 0x00024540
		private void OnTextFieldValueChange(ChangeEvent<string> evt)
		{
			TValueType clampedValue = this.GetClampedValue(this.ParseStringToValue(evt.newValue));
			bool flag = !EqualityComparer<TValueType>.Default.Equals(clampedValue, this.value);
			if (flag)
			{
				this.value = clampedValue;
				evt.StopPropagation();
				bool flag2 = base.elementPanel != null;
				if (flag2)
				{
					this.OnViewDataReady();
				}
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000263A0 File Offset: 0x000245A0
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				VisualElement dragElement = this.dragElement;
				if (dragElement != null)
				{
					dragElement.RemoveFromHierarchy();
				}
			}
			else
			{
				this.dragContainer.Add(this.dragElement);
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x000263E4 File Offset: 0x000245E4
		// Note: this type is marked as 'beforefieldinit'.
		static BaseSlider()
		{
		}

		// Token: 0x040003EB RID: 1003
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualElement <dragContainer>k__BackingField;

		// Token: 0x040003EC RID: 1004
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private VisualElement <dragElement>k__BackingField;

		// Token: 0x040003ED RID: 1005
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualElement <dragBorderElement>k__BackingField;

		// Token: 0x040003EE RID: 1006
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private TextField <inputTextField>k__BackingField;

		// Token: 0x040003EF RID: 1007
		[SerializeField]
		private TValueType m_LowValue;

		// Token: 0x040003F0 RID: 1008
		[SerializeField]
		private TValueType m_HighValue;

		// Token: 0x040003F1 RID: 1009
		private float m_PageSize;

		// Token: 0x040003F2 RID: 1010
		private bool m_ShowInputField = false;

		// Token: 0x040003F3 RID: 1011
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <clamped>k__BackingField;

		// Token: 0x040003F4 RID: 1012
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ClampedDragger<TValueType> <clampedDragger>k__BackingField;

		// Token: 0x040003F5 RID: 1013
		private Rect m_DragElementStartPos;

		// Token: 0x040003F6 RID: 1014
		private SliderDirection m_Direction;

		// Token: 0x040003F7 RID: 1015
		private bool m_Inverted = false;

		// Token: 0x040003F8 RID: 1016
		internal const float kDefaultPageSize = 0f;

		// Token: 0x040003F9 RID: 1017
		internal const bool kDefaultShowInputField = false;

		// Token: 0x040003FA RID: 1018
		internal const bool kDefaultInverted = false;

		// Token: 0x040003FB RID: 1019
		public new static readonly string ussClassName = "unity-base-slider";

		// Token: 0x040003FC RID: 1020
		public new static readonly string labelUssClassName = BaseSlider<TValueType>.ussClassName + "__label";

		// Token: 0x040003FD RID: 1021
		public new static readonly string inputUssClassName = BaseSlider<TValueType>.ussClassName + "__input";

		// Token: 0x040003FE RID: 1022
		public static readonly string horizontalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--horizontal";

		// Token: 0x040003FF RID: 1023
		public static readonly string verticalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--vertical";

		// Token: 0x04000400 RID: 1024
		public static readonly string dragContainerUssClassName = BaseSlider<TValueType>.ussClassName + "__drag-container";

		// Token: 0x04000401 RID: 1025
		public static readonly string trackerUssClassName = BaseSlider<TValueType>.ussClassName + "__tracker";

		// Token: 0x04000402 RID: 1026
		public static readonly string draggerUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger";

		// Token: 0x04000403 RID: 1027
		public static readonly string draggerBorderUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger-border";

		// Token: 0x04000404 RID: 1028
		public static readonly string textFieldClassName = BaseSlider<TValueType>.ussClassName + "__text-field";

		// Token: 0x02000120 RID: 288
		internal enum SliderKey
		{
			// Token: 0x04000406 RID: 1030
			None,
			// Token: 0x04000407 RID: 1031
			Lowest,
			// Token: 0x04000408 RID: 1032
			LowerPage,
			// Token: 0x04000409 RID: 1033
			Lower,
			// Token: 0x0400040A RID: 1034
			Higher,
			// Token: 0x0400040B RID: 1035
			HigherPage,
			// Token: 0x0400040C RID: 1036
			Highest
		}
	}
}
