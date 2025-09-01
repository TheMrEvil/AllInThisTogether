using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x0200016B RID: 363
	public class Scroller : VisualElement
	{
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000B75 RID: 2933 RVA: 0x0002EF04 File Offset: 0x0002D104
		// (remove) Token: 0x06000B76 RID: 2934 RVA: 0x0002EF3C File Offset: 0x0002D13C
		public event Action<float> valueChanged
		{
			[CompilerGenerated]
			add
			{
				Action<float> action = this.valueChanged;
				Action<float> action2;
				do
				{
					action2 = action;
					Action<float> value2 = (Action<float>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<float>>(ref this.valueChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<float> action = this.valueChanged;
				Action<float> action2;
				do
				{
					action2 = action;
					Action<float> value2 = (Action<float>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<float>>(ref this.valueChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0002EF71 File Offset: 0x0002D171
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x0002EF79 File Offset: 0x0002D179
		public Slider slider
		{
			[CompilerGenerated]
			get
			{
				return this.<slider>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<slider>k__BackingField = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002EF82 File Offset: 0x0002D182
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x0002EF8A File Offset: 0x0002D18A
		public RepeatButton lowButton
		{
			[CompilerGenerated]
			get
			{
				return this.<lowButton>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<lowButton>k__BackingField = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002EF93 File Offset: 0x0002D193
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x0002EF9B File Offset: 0x0002D19B
		public RepeatButton highButton
		{
			[CompilerGenerated]
			get
			{
				return this.<highButton>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<highButton>k__BackingField = value;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0002EFA4 File Offset: 0x0002D1A4
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x0002EFC1 File Offset: 0x0002D1C1
		public float value
		{
			get
			{
				return this.slider.value;
			}
			set
			{
				this.slider.value = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0002EFD4 File Offset: 0x0002D1D4
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0002EFF1 File Offset: 0x0002D1F1
		public float lowValue
		{
			get
			{
				return this.slider.lowValue;
			}
			set
			{
				this.slider.lowValue = value;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0002F004 File Offset: 0x0002D204
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0002F021 File Offset: 0x0002D221
		public float highValue
		{
			get
			{
				return this.slider.highValue;
			}
			set
			{
				this.slider.highValue = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0002F034 File Offset: 0x0002D234
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x0002F058 File Offset: 0x0002D258
		public SliderDirection direction
		{
			get
			{
				return (base.resolvedStyle.flexDirection == FlexDirection.Row) ? SliderDirection.Horizontal : SliderDirection.Vertical;
			}
			set
			{
				this.slider.direction = value;
				bool flag = value == SliderDirection.Horizontal;
				if (flag)
				{
					base.style.flexDirection = FlexDirection.Row;
					base.AddToClassList(Scroller.horizontalVariantUssClassName);
					base.RemoveFromClassList(Scroller.verticalVariantUssClassName);
				}
				else
				{
					base.style.flexDirection = FlexDirection.Column;
					base.AddToClassList(Scroller.verticalVariantUssClassName);
					base.RemoveFromClassList(Scroller.horizontalVariantUssClassName);
				}
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002F0D5 File Offset: 0x0002D2D5
		public Scroller() : this(0f, 0f, null, SliderDirection.Vertical)
		{
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002F0EC File Offset: 0x0002D2EC
		public Scroller(float lowValue, float highValue, Action<float> valueChanged, SliderDirection direction = SliderDirection.Vertical)
		{
			base.AddToClassList(Scroller.ussClassName);
			this.slider = new Slider(lowValue, highValue, direction, 20f)
			{
				name = "unity-slider",
				viewDataKey = "Slider"
			};
			this.slider.AddToClassList(Scroller.sliderUssClassName);
			this.slider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnSliderValueChange));
			this.slider.inverted = (direction == SliderDirection.Vertical);
			this.lowButton = new RepeatButton(new Action(this.ScrollPageUp), 250L, 30L)
			{
				name = "unity-low-button"
			};
			this.lowButton.AddToClassList(Scroller.lowButtonUssClassName);
			base.Add(this.lowButton);
			this.highButton = new RepeatButton(new Action(this.ScrollPageDown), 250L, 30L)
			{
				name = "unity-high-button"
			};
			this.highButton.AddToClassList(Scroller.highButtonUssClassName);
			base.Add(this.highButton);
			base.Add(this.slider);
			this.direction = direction;
			this.valueChanged = valueChanged;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002F227 File Offset: 0x0002D427
		public void Adjust(float factor)
		{
			base.SetEnabled(factor < 1f);
			this.slider.AdjustDragElement(factor);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002F246 File Offset: 0x0002D446
		private void OnSliderValueChange(ChangeEvent<float> evt)
		{
			this.value = evt.newValue;
			Action<float> action = this.valueChanged;
			if (action != null)
			{
				action(this.slider.value);
			}
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002F27F File Offset: 0x0002D47F
		public void ScrollPageUp()
		{
			this.ScrollPageUp(1f);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002F28E File Offset: 0x0002D48E
		public void ScrollPageDown()
		{
			this.ScrollPageDown(1f);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002F2A0 File Offset: 0x0002D4A0
		public void ScrollPageUp(float factor)
		{
			this.value -= factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : -1f));
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002F2F0 File Offset: 0x0002D4F0
		public void ScrollPageDown(float factor)
		{
			this.value += factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : -1f));
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002F340 File Offset: 0x0002D540
		// Note: this type is marked as 'beforefieldinit'.
		static Scroller()
		{
		}

		// Token: 0x0400053E RID: 1342
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<float> valueChanged;

		// Token: 0x0400053F RID: 1343
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Slider <slider>k__BackingField;

		// Token: 0x04000540 RID: 1344
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RepeatButton <lowButton>k__BackingField;

		// Token: 0x04000541 RID: 1345
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RepeatButton <highButton>k__BackingField;

		// Token: 0x04000542 RID: 1346
		internal const float kDefaultPageSize = 20f;

		// Token: 0x04000543 RID: 1347
		public static readonly string ussClassName = "unity-scroller";

		// Token: 0x04000544 RID: 1348
		public static readonly string horizontalVariantUssClassName = Scroller.ussClassName + "--horizontal";

		// Token: 0x04000545 RID: 1349
		public static readonly string verticalVariantUssClassName = Scroller.ussClassName + "--vertical";

		// Token: 0x04000546 RID: 1350
		public static readonly string sliderUssClassName = Scroller.ussClassName + "__slider";

		// Token: 0x04000547 RID: 1351
		public static readonly string lowButtonUssClassName = Scroller.ussClassName + "__low-button";

		// Token: 0x04000548 RID: 1352
		public static readonly string highButtonUssClassName = Scroller.ussClassName + "__high-button";

		// Token: 0x0200016C RID: 364
		public new class UxmlFactory : UxmlFactory<Scroller, Scroller.UxmlTraits>
		{
			// Token: 0x06000B8E RID: 2958 RVA: 0x0002F3BB File Offset: 0x0002D5BB
			public UxmlFactory()
			{
			}
		}

		// Token: 0x0200016D RID: 365
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x1700023D RID: 573
			// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002F3C4 File Offset: 0x0002D5C4
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000B90 RID: 2960 RVA: 0x0002F3E4 File Offset: 0x0002D5E4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Scroller scroller = (Scroller)ve;
				scroller.slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				scroller.slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				scroller.direction = this.m_Direction.GetValueFromBag(bag, cc);
				scroller.value = this.m_Value.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000B91 RID: 2961 RVA: 0x0002F460 File Offset: 0x0002D660
			public UxmlTraits()
			{
			}

			// Token: 0x04000549 RID: 1353
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				obsoleteNames = new string[]
				{
					"lowValue"
				}
			};

			// Token: 0x0400054A RID: 1354
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				obsoleteNames = new string[]
				{
					"highValue"
				}
			};

			// Token: 0x0400054B RID: 1355
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Vertical
			};

			// Token: 0x0400054C RID: 1356
			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value"
			};

			// Token: 0x0200016E RID: 366
			[CompilerGenerated]
			private sealed class <get_uxmlChildElementsDescription>d__5 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
			{
				// Token: 0x06000B92 RID: 2962 RVA: 0x0002F506 File Offset: 0x0002D706
				[DebuggerHidden]
				public <get_uxmlChildElementsDescription>d__5(int <>1__state)
				{
					this.<>1__state = <>1__state;
					this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
				}

				// Token: 0x06000B93 RID: 2963 RVA: 0x000080DB File Offset: 0x000062DB
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x06000B94 RID: 2964 RVA: 0x0002F528 File Offset: 0x0002D728
				bool IEnumerator.MoveNext()
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						return false;
					}
					this.<>1__state = -1;
					return false;
				}

				// Token: 0x1700023E RID: 574
				// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002F54E File Offset: 0x0002D74E
				UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000B96 RID: 2966 RVA: 0x0000810E File Offset: 0x0000630E
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x1700023F RID: 575
				// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0002F54E File Offset: 0x0002D74E
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x06000B98 RID: 2968 RVA: 0x0002F558 File Offset: 0x0002D758
				[DebuggerHidden]
				IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
				{
					Scroller.UxmlTraits.<get_uxmlChildElementsDescription>d__5 <get_uxmlChildElementsDescription>d__;
					if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
					{
						this.<>1__state = 0;
						<get_uxmlChildElementsDescription>d__ = this;
					}
					else
					{
						<get_uxmlChildElementsDescription>d__ = new Scroller.UxmlTraits.<get_uxmlChildElementsDescription>d__5(0);
						<get_uxmlChildElementsDescription>d__.<>4__this = this;
					}
					return <get_uxmlChildElementsDescription>d__;
				}

				// Token: 0x06000B99 RID: 2969 RVA: 0x0002F5A0 File Offset: 0x0002D7A0
				[DebuggerHidden]
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
				}

				// Token: 0x0400054D RID: 1357
				private int <>1__state;

				// Token: 0x0400054E RID: 1358
				private UxmlChildElementDescription <>2__current;

				// Token: 0x0400054F RID: 1359
				private int <>l__initialThreadId;

				// Token: 0x04000550 RID: 1360
				public Scroller.UxmlTraits <>4__this;
			}
		}
	}
}
