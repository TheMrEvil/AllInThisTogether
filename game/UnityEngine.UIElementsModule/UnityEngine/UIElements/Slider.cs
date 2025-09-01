using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000177 RID: 375
	public class Slider : BaseSlider<float>
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x000321C3 File Offset: 0x000303C3
		public Slider() : this(null, 0f, 10f, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000321DE File Offset: 0x000303DE
		public Slider(float start, float end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f) : this(null, start, end, direction, pageSize)
		{
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000321EE File Offset: 0x000303EE
		public Slider(string label, float start = 0f, float end = 10f, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f) : base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(Slider.ussClassName);
			base.labelElement.AddToClassList(Slider.labelUssClassName);
			base.visualInput.AddToClassList(Slider.inputUssClassName);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00032230 File Offset: 0x00030430
		internal override float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			float num = Mathf.LerpUnclamped(a, b, interpolant);
			float num2 = Mathf.Abs((base.highValue - base.lowValue) / (base.dragContainer.resolvedStyle.width - base.dragElement.resolvedStyle.width));
			int digits = (num2 == 0f) ? Mathf.Clamp((int)(5.0 - (double)Mathf.Log10(Mathf.Abs(num2))), 0, 15) : Mathf.Clamp(-Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(num2))), 0, 15);
			return (float)Math.Round((double)num, digits, MidpointRounding.AwayFromZero);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000322D4 File Offset: 0x000304D4
		internal override float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000322F0 File Offset: 0x000304F0
		internal override float SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00032314 File Offset: 0x00030514
		internal override float ParseStringToValue(string stringValue)
		{
			float num;
			bool flag = float.TryParse(stringValue.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out num);
			float result;
			if (flag)
			{
				result = num;
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00032358 File Offset: 0x00030558
		internal override void ComputeValueFromKey(BaseSlider<float>.SliderKey sliderKey, bool isShift)
		{
			if (sliderKey != BaseSlider<float>.SliderKey.None)
			{
				if (sliderKey != BaseSlider<float>.SliderKey.Lowest)
				{
					if (sliderKey != BaseSlider<float>.SliderKey.Highest)
					{
						bool flag = sliderKey == BaseSlider<float>.SliderKey.LowerPage || sliderKey == BaseSlider<float>.SliderKey.HigherPage;
						float num = BaseSlider<float>.GetClosestPowerOfTen(Mathf.Abs((base.highValue - base.lowValue) * 0.01f));
						bool flag2 = flag;
						if (flag2)
						{
							num *= this.pageSize;
						}
						else if (isShift)
						{
							num *= 10f;
						}
						bool flag3 = sliderKey == BaseSlider<float>.SliderKey.Lower || sliderKey == BaseSlider<float>.SliderKey.LowerPage;
						if (flag3)
						{
							num = -num;
						}
						this.value = BaseSlider<float>.RoundToMultipleOf(this.value + num * 0.5001f, Mathf.Abs(num));
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

		// Token: 0x06000BFB RID: 3067 RVA: 0x00032420 File Offset: 0x00030620
		// Note: this type is marked as 'beforefieldinit'.
		static Slider()
		{
		}

		// Token: 0x040005B1 RID: 1457
		internal const float kDefaultHighValue = 10f;

		// Token: 0x040005B2 RID: 1458
		public new static readonly string ussClassName = "unity-slider";

		// Token: 0x040005B3 RID: 1459
		public new static readonly string labelUssClassName = Slider.ussClassName + "__label";

		// Token: 0x040005B4 RID: 1460
		public new static readonly string inputUssClassName = Slider.ussClassName + "__input";

		// Token: 0x02000178 RID: 376
		public new class UxmlFactory : UxmlFactory<Slider, Slider.UxmlTraits>
		{
			// Token: 0x06000BFC RID: 3068 RVA: 0x00032454 File Offset: 0x00030654
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000179 RID: 377
		public new class UxmlTraits : BaseFieldTraits<float, UxmlFloatAttributeDescription>
		{
			// Token: 0x06000BFD RID: 3069 RVA: 0x00032460 File Offset: 0x00030660
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				Slider slider = (Slider)ve;
				slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				slider.direction = this.m_Direction.GetValueFromBag(bag, cc);
				slider.pageSize = this.m_PageSize.GetValueFromBag(bag, cc);
				slider.showInputField = this.m_ShowInputField.GetValueFromBag(bag, cc);
				slider.inverted = this.m_Inverted.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x000324F8 File Offset: 0x000306F8
			public UxmlTraits()
			{
			}

			// Token: 0x040005B5 RID: 1461
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x040005B6 RID: 1462
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 10f
			};

			// Token: 0x040005B7 RID: 1463
			private UxmlFloatAttributeDescription m_PageSize = new UxmlFloatAttributeDescription
			{
				name = "page-size",
				defaultValue = 0f
			};

			// Token: 0x040005B8 RID: 1464
			private UxmlBoolAttributeDescription m_ShowInputField = new UxmlBoolAttributeDescription
			{
				name = "show-input-field",
				defaultValue = false
			};

			// Token: 0x040005B9 RID: 1465
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};

			// Token: 0x040005BA RID: 1466
			private UxmlBoolAttributeDescription m_Inverted = new UxmlBoolAttributeDescription
			{
				name = "inverted",
				defaultValue = false
			};
		}
	}
}
