using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017A RID: 378
	public class SliderInt : BaseSlider<int>
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x000325C6 File Offset: 0x000307C6
		public SliderInt() : this(null, 0, 10, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000325DA File Offset: 0x000307DA
		public SliderInt(int start, int end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f) : this(null, start, end, direction, pageSize)
		{
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000325EA File Offset: 0x000307EA
		public SliderInt(string label, int start = 0, int end = 10, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f) : base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(SliderInt.ussClassName);
			base.labelElement.AddToClassList(SliderInt.labelUssClassName);
			base.visualInput.AddToClassList(SliderInt.inputUssClassName);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0003262C File Offset: 0x0003082C
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00032644 File Offset: 0x00030844
		public override float pageSize
		{
			get
			{
				return base.pageSize;
			}
			set
			{
				base.pageSize = (float)Mathf.RoundToInt(value);
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00032658 File Offset: 0x00030858
		internal override int SliderLerpUnclamped(int a, int b, float interpolant)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped((float)a, (float)b, interpolant));
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0003267C File Offset: 0x0003087C
		internal override float SliderNormalizeValue(int currentValue, int lowerValue, int higherValue)
		{
			return ((float)currentValue - (float)lowerValue) / ((float)higherValue - (float)lowerValue);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0003269C File Offset: 0x0003089C
		internal override int SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000326C0 File Offset: 0x000308C0
		internal override int ParseStringToValue(string stringValue)
		{
			int num;
			bool flag = int.TryParse(stringValue, out num);
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000326E8 File Offset: 0x000308E8
		internal override void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			bool flag = Mathf.Approximately(this.pageSize, 0f);
			if (flag)
			{
				base.ComputeValueAndDirectionFromClick(sliderLength, dragElementLength, dragElementPos, dragElementLastPos);
			}
			else
			{
				float f = sliderLength - dragElementLength;
				bool flag2 = Mathf.Abs(f) < 1E-30f;
				if (!flag2)
				{
					int num = (int)this.pageSize;
					bool flag3 = (base.lowValue > base.highValue && !base.inverted) || (base.lowValue < base.highValue && base.inverted) || (base.direction == SliderDirection.Vertical && !base.inverted);
					if (flag3)
					{
						num = -num;
					}
					bool flag4 = dragElementLastPos < dragElementPos;
					bool flag5 = dragElementLastPos > dragElementPos + dragElementLength;
					bool flag6 = base.inverted ? flag5 : flag4;
					bool flag7 = base.inverted ? flag4 : flag5;
					bool flag8 = flag6 && base.clampedDragger.dragDirection != ClampedDragger<int>.DragDirection.LowToHigh;
					if (flag8)
					{
						base.clampedDragger.dragDirection = ClampedDragger<int>.DragDirection.HighToLow;
						this.value -= num;
					}
					else
					{
						bool flag9 = flag7 && base.clampedDragger.dragDirection != ClampedDragger<int>.DragDirection.HighToLow;
						if (flag9)
						{
							base.clampedDragger.dragDirection = ClampedDragger<int>.DragDirection.LowToHigh;
							this.value += num;
						}
					}
				}
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0003283C File Offset: 0x00030A3C
		internal override void ComputeValueFromKey(BaseSlider<int>.SliderKey sliderKey, bool isShift)
		{
			if (sliderKey != BaseSlider<int>.SliderKey.None)
			{
				if (sliderKey != BaseSlider<int>.SliderKey.Lowest)
				{
					if (sliderKey != BaseSlider<int>.SliderKey.Highest)
					{
						bool flag = sliderKey == BaseSlider<int>.SliderKey.LowerPage || sliderKey == BaseSlider<int>.SliderKey.HigherPage;
						float num = BaseSlider<int>.GetClosestPowerOfTen(Mathf.Abs((float)(base.highValue - base.lowValue) * 0.01f));
						bool flag2 = num < 1f;
						if (flag2)
						{
							num = 1f;
						}
						bool flag3 = flag;
						if (flag3)
						{
							num *= this.pageSize;
						}
						else if (isShift)
						{
							num *= 10f;
						}
						bool flag4 = sliderKey == BaseSlider<int>.SliderKey.Lower || sliderKey == BaseSlider<int>.SliderKey.LowerPage;
						if (flag4)
						{
							num = -num;
						}
						this.value = Mathf.RoundToInt(BaseSlider<int>.RoundToMultipleOf((float)this.value + num * 0.5001f, Mathf.Abs(num)));
					}
					else
					{
						this.value = base.highValue;
					}
				}
				else
				{
					this.value = base.lowValue;
				}
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00032922 File Offset: 0x00030B22
		// Note: this type is marked as 'beforefieldinit'.
		static SliderInt()
		{
		}

		// Token: 0x040005BB RID: 1467
		internal const int kDefaultHighValue = 10;

		// Token: 0x040005BC RID: 1468
		public new static readonly string ussClassName = "unity-slider-int";

		// Token: 0x040005BD RID: 1469
		public new static readonly string labelUssClassName = SliderInt.ussClassName + "__label";

		// Token: 0x040005BE RID: 1470
		public new static readonly string inputUssClassName = SliderInt.ussClassName + "__input";

		// Token: 0x0200017B RID: 379
		public new class UxmlFactory : UxmlFactory<SliderInt, SliderInt.UxmlTraits>
		{
			// Token: 0x06000C0B RID: 3083 RVA: 0x00032956 File Offset: 0x00030B56
			public UxmlFactory()
			{
			}
		}

		// Token: 0x0200017C RID: 380
		public new class UxmlTraits : BaseFieldTraits<int, UxmlIntAttributeDescription>
		{
			// Token: 0x06000C0C RID: 3084 RVA: 0x00032960 File Offset: 0x00030B60
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				SliderInt sliderInt = (SliderInt)ve;
				sliderInt.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				sliderInt.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				sliderInt.direction = this.m_Direction.GetValueFromBag(bag, cc);
				sliderInt.pageSize = (float)this.m_PageSize.GetValueFromBag(bag, cc);
				sliderInt.showInputField = this.m_ShowInputField.GetValueFromBag(bag, cc);
				sliderInt.inverted = this.m_Inverted.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x000329F8 File Offset: 0x00030BF8
			public UxmlTraits()
			{
			}

			// Token: 0x040005BF RID: 1471
			private UxmlIntAttributeDescription m_LowValue = new UxmlIntAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x040005C0 RID: 1472
			private UxmlIntAttributeDescription m_HighValue = new UxmlIntAttributeDescription
			{
				name = "high-value",
				defaultValue = 10
			};

			// Token: 0x040005C1 RID: 1473
			private UxmlIntAttributeDescription m_PageSize = new UxmlIntAttributeDescription
			{
				name = "page-size",
				defaultValue = 0
			};

			// Token: 0x040005C2 RID: 1474
			private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
			{
				name = "show-input-field",
				defaultValue = false
			};

			// Token: 0x040005C3 RID: 1475
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};

			// Token: 0x040005C4 RID: 1476
			private UxmlBoolAttributeDescription m_Inverted = new UxmlBoolAttributeDescription
			{
				name = "inverted",
				defaultValue = false
			};
		}
	}
}
