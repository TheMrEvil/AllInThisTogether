using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000171 RID: 369
	public class ScrollView : VisualElement
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002F5A8 File Offset: 0x0002D7A8
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x0002F5C0 File Offset: 0x0002D7C0
		public ScrollerVisibility horizontalScrollerVisibility
		{
			get
			{
				return this.m_HorizontalScrollerVisibility;
			}
			set
			{
				this.m_HorizontalScrollerVisibility = value;
				this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0002F5E0 File Offset: 0x0002D7E0
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x0002F5F8 File Offset: 0x0002D7F8
		public ScrollerVisibility verticalScrollerVisibility
		{
			get
			{
				return this.m_VerticalScrollerVisibility;
			}
			set
			{
				this.m_VerticalScrollerVisibility = value;
				this.UpdateScrollers(this.needsHorizontal, this.needsVertical);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002F615 File Offset: 0x0002D815
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x0002F620 File Offset: 0x0002D820
		[Obsolete("showHorizontal is obsolete. Use horizontalScrollerVisibility instead")]
		public bool showHorizontal
		{
			get
			{
				return this.horizontalScrollerVisibility == ScrollerVisibility.AlwaysVisible;
			}
			set
			{
				this.m_HorizontalScrollerVisibility = (value ? ScrollerVisibility.AlwaysVisible : ScrollerVisibility.Auto);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002F62F File Offset: 0x0002D82F
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x0002F63A File Offset: 0x0002D83A
		[Obsolete("showVertical is obsolete. Use verticalScrollerVisibility instead")]
		public bool showVertical
		{
			get
			{
				return this.verticalScrollerVisibility == ScrollerVisibility.AlwaysVisible;
			}
			set
			{
				this.m_VerticalScrollerVisibility = (value ? ScrollerVisibility.AlwaysVisible : ScrollerVisibility.Auto);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002F64C File Offset: 0x0002D84C
		internal bool needsHorizontal
		{
			get
			{
				return this.horizontalScrollerVisibility == ScrollerVisibility.AlwaysVisible || (this.horizontalScrollerVisibility == ScrollerVisibility.Auto && this.scrollableWidth > 0.001f);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002F684 File Offset: 0x0002D884
		internal bool needsVertical
		{
			get
			{
				return this.verticalScrollerVisibility == ScrollerVisibility.AlwaysVisible || (this.verticalScrollerVisibility == ScrollerVisibility.Auto && this.scrollableHeight > 0.001f);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002F6BC File Offset: 0x0002D8BC
		internal bool isVerticalScrollDisplayed
		{
			get
			{
				return this.verticalScroller.resolvedStyle.display == DisplayStyle.Flex;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0002F6E4 File Offset: 0x0002D8E4
		internal bool isHorizontalScrollDisplayed
		{
			get
			{
				return this.horizontalScroller.resolvedStyle.display == DisplayStyle.Flex;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0002F70C File Offset: 0x0002D90C
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0002F73C File Offset: 0x0002D93C
		public Vector2 scrollOffset
		{
			get
			{
				return new Vector2(this.horizontalScroller.value, this.verticalScroller.value);
			}
			set
			{
				bool flag = value != this.scrollOffset;
				if (flag)
				{
					this.horizontalScroller.value = value.x;
					this.verticalScroller.value = value.y;
					this.UpdateContentViewTransform();
				}
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0002F788 File Offset: 0x0002D988
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0002F7A0 File Offset: 0x0002D9A0
		public float horizontalPageSize
		{
			get
			{
				return this.m_HorizontalPageSize;
			}
			set
			{
				this.m_HorizontalPageSize = value;
				this.UpdateHorizontalSliderPageSize();
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0002F7B4 File Offset: 0x0002D9B4
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0002F7CC File Offset: 0x0002D9CC
		public float verticalPageSize
		{
			get
			{
				return this.m_VerticalPageSize;
			}
			set
			{
				this.m_VerticalPageSize = value;
				this.UpdateVerticalSliderPageSize();
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0002F7E0 File Offset: 0x0002D9E0
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0002F7F8 File Offset: 0x0002D9F8
		public float mouseWheelScrollSize
		{
			get
			{
				return this.m_MouseWheelScrollSize;
			}
			set
			{
				float mouseWheelScrollSize = this.m_MouseWheelScrollSize;
				bool flag = Math.Abs(this.m_MouseWheelScrollSize - value) > float.Epsilon;
				if (flag)
				{
					this.m_MouseWheelScrollSizeIsInline = true;
					this.m_MouseWheelScrollSize = value;
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002F838 File Offset: 0x0002DA38
		internal float scrollableWidth
		{
			get
			{
				return this.contentContainer.boundingBox.width - this.contentViewport.layout.width;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0002F874 File Offset: 0x0002DA74
		internal float scrollableHeight
		{
			get
			{
				return this.contentContainer.boundingBox.height - this.contentViewport.layout.height;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002F8AD File Offset: 0x0002DAAD
		private bool hasInertia
		{
			get
			{
				return this.scrollDecelerationRate > 0f;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0002F8BC File Offset: 0x0002DABC
		// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x0002F8D4 File Offset: 0x0002DAD4
		public float scrollDecelerationRate
		{
			get
			{
				return this.m_ScrollDecelerationRate;
			}
			set
			{
				this.m_ScrollDecelerationRate = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0002F8E8 File Offset: 0x0002DAE8
		// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x0002F900 File Offset: 0x0002DB00
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0002F914 File Offset: 0x0002DB14
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x0002F92C File Offset: 0x0002DB2C
		public ScrollView.TouchScrollBehavior touchScrollBehavior
		{
			get
			{
				return this.m_TouchScrollBehavior;
			}
			set
			{
				this.m_TouchScrollBehavior = value;
				bool flag = this.m_TouchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
				if (flag)
				{
					this.horizontalScroller.slider.clamped = true;
					this.verticalScroller.slider.clamped = true;
				}
				else
				{
					this.horizontalScroller.slider.clamped = false;
					this.verticalScroller.slider.clamped = false;
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0002F99C File Offset: 0x0002DB9C
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0002F9A4 File Offset: 0x0002DBA4
		public ScrollView.NestedInteractionKind nestedInteractionKind
		{
			get
			{
				return this.m_NestedInteractionKind;
			}
			set
			{
				this.m_NestedInteractionKind = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002F9B0 File Offset: 0x0002DBB0
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0002F9C8 File Offset: 0x0002DBC8
		public long elasticAnimationIntervalMs
		{
			get
			{
				return this.m_ElasticAnimationIntervalMs;
			}
			set
			{
				long elasticAnimationIntervalMs = this.m_ElasticAnimationIntervalMs;
				this.m_ElasticAnimationIntervalMs = value;
				bool flag = elasticAnimationIntervalMs != this.m_ElasticAnimationIntervalMs;
				if (flag)
				{
					this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(this.m_ElasticAnimationIntervalMs);
				}
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0002FA20 File Offset: 0x0002DC20
		private void OnHorizontalScrollDragElementChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateHorizontalSliderPageSize();
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002FA60 File Offset: 0x0002DC60
		private void OnVerticalScrollDragElementChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateVerticalSliderPageSize();
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002FAA0 File Offset: 0x0002DCA0
		private void UpdateHorizontalSliderPageSize()
		{
			float width = this.horizontalScroller.resolvedStyle.width;
			float num = this.m_HorizontalPageSize;
			bool flag = width > 0f;
			if (flag)
			{
				bool flag2 = Mathf.Approximately(this.m_HorizontalPageSize, -1f);
				if (flag2)
				{
					float width2 = this.horizontalScroller.slider.dragElement.resolvedStyle.width;
					num = width2 * 0.9f;
				}
			}
			bool flag3 = num >= 0f;
			if (flag3)
			{
				this.horizontalScroller.slider.pageSize = num;
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002FB34 File Offset: 0x0002DD34
		private void UpdateVerticalSliderPageSize()
		{
			float height = this.verticalScroller.resolvedStyle.height;
			float num = this.m_VerticalPageSize;
			bool flag = height > 0f;
			if (flag)
			{
				bool flag2 = Mathf.Approximately(this.m_VerticalPageSize, -1f);
				if (flag2)
				{
					float height2 = this.verticalScroller.slider.dragElement.resolvedStyle.height;
					num = height2 * 0.9f;
				}
			}
			bool flag3 = num >= 0f;
			if (flag3)
			{
				this.verticalScroller.slider.pageSize = num;
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002FBC8 File Offset: 0x0002DDC8
		internal void UpdateContentViewTransform()
		{
			Vector3 position = this.contentContainer.transform.position;
			Vector2 scrollOffset = this.scrollOffset;
			bool needsVertical = this.needsVertical;
			if (needsVertical)
			{
				scrollOffset.y += this.contentContainer.resolvedStyle.top;
			}
			position.x = GUIUtility.RoundToPixelGrid(-scrollOffset.x);
			position.y = GUIUtility.RoundToPixelGrid(-scrollOffset.y);
			this.contentContainer.transform.position = position;
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0002FC58 File Offset: 0x0002DE58
		public void ScrollTo(VisualElement child)
		{
			bool flag = child == null;
			if (flag)
			{
				throw new ArgumentNullException("child");
			}
			bool flag2 = !this.contentContainer.Contains(child);
			if (flag2)
			{
				throw new ArgumentException("Cannot scroll to a VisualElement that's not a child of the ScrollView content-container.");
			}
			this.m_Velocity = Vector2.zero;
			float num = 0f;
			float num2 = 0f;
			bool flag3 = this.scrollableHeight > 0f;
			if (flag3)
			{
				num = this.GetYDeltaOffset(child);
				this.verticalScroller.value = this.scrollOffset.y + num;
			}
			bool flag4 = this.scrollableWidth > 0f;
			if (flag4)
			{
				num2 = this.GetXDeltaOffset(child);
				this.horizontalScroller.value = this.scrollOffset.x + num2;
			}
			bool flag5 = num == 0f && num2 == 0f;
			if (!flag5)
			{
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002FD3C File Offset: 0x0002DF3C
		private float GetXDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.x * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.xMin + num;
			float num3 = worldBound.xMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.xMin + num;
			float num5 = worldBound2.xMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				result = deltaDistance * this.horizontalScroller.highValue / this.scrollableWidth;
			}
			return result;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002FDFC File Offset: 0x0002DFFC
		private float GetYDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.y * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.yMin + num;
			float num3 = worldBound.yMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.yMin + num;
			float num5 = worldBound2.yMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				result = deltaDistance * this.verticalScroller.highValue / this.scrollableHeight;
			}
			return result;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002FEBC File Offset: 0x0002E0BC
		private float GetDeltaDistance(float viewMin, float viewMax, float childBoundaryMin, float childBoundaryMax)
		{
			float num = viewMax - viewMin;
			float num2 = childBoundaryMax - childBoundaryMin;
			bool flag = num2 > num;
			float result;
			if (flag)
			{
				bool flag2 = viewMin > childBoundaryMin && childBoundaryMax > viewMax;
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					result = ((childBoundaryMin > viewMin) ? (childBoundaryMin - viewMin) : (childBoundaryMax - viewMax));
				}
			}
			else
			{
				float num3 = childBoundaryMax - viewMax;
				bool flag3 = num3 < -1f;
				if (flag3)
				{
					num3 = childBoundaryMin - viewMin;
				}
				result = num3;
			}
			return result;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0002FF28 File Offset: 0x0002E128
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0002FF30 File Offset: 0x0002E130
		public VisualElement contentViewport
		{
			[CompilerGenerated]
			get
			{
				return this.<contentViewport>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<contentViewport>k__BackingField = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002FF39 File Offset: 0x0002E139
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0002FF41 File Offset: 0x0002E141
		public Scroller horizontalScroller
		{
			[CompilerGenerated]
			get
			{
				return this.<horizontalScroller>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<horizontalScroller>k__BackingField = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0002FF4A File Offset: 0x0002E14A
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0002FF52 File Offset: 0x0002E152
		public Scroller verticalScroller
		{
			[CompilerGenerated]
			get
			{
				return this.<verticalScroller>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<verticalScroller>k__BackingField = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0002FF5C File Offset: 0x0002E15C
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002FF74 File Offset: 0x0002E174
		public ScrollView() : this(ScrollViewMode.Vertical)
		{
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0002FF80 File Offset: 0x0002E180
		public ScrollView(ScrollViewMode scrollViewMode)
		{
			base.AddToClassList(ScrollView.ussClassName);
			this.m_ContentAndVerticalScrollContainer = new VisualElement
			{
				name = "unity-content-and-vertical-scroll-container"
			};
			this.m_ContentAndVerticalScrollContainer.AddToClassList(ScrollView.contentAndVerticalScrollUssClassName);
			base.hierarchy.Add(this.m_ContentAndVerticalScrollContainer);
			this.contentViewport = new VisualElement
			{
				name = "unity-content-viewport"
			};
			this.contentViewport.AddToClassList(ScrollView.viewportUssClassName);
			this.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.contentViewport.pickingMode = PickingMode.Ignore;
			this.m_ContentAndVerticalScrollContainer.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.m_ContentAndVerticalScrollContainer.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			this.m_ContentAndVerticalScrollContainer.Add(this.contentViewport);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.disableClipping = true;
			this.m_ContentContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.m_ContentContainer.AddToClassList(ScrollView.contentUssClassName);
			this.m_ContentContainer.usageHints = UsageHints.GroupTransform;
			this.contentViewport.Add(this.m_ContentContainer);
			this.SetScrollViewMode(scrollViewMode);
			this.horizontalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(value, this.scrollOffset.y);
				this.UpdateContentViewTransform();
			}, SliderDirection.Horizontal)
			{
				viewDataKey = "HorizontalScroller"
			};
			this.horizontalScroller.AddToClassList(ScrollView.hScrollerUssClassName);
			this.horizontalScroller.style.display = DisplayStyle.None;
			base.hierarchy.Add(this.horizontalScroller);
			this.verticalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(this.scrollOffset.x, value);
				this.UpdateContentViewTransform();
			}, SliderDirection.Vertical)
			{
				viewDataKey = "VerticalScroller"
			};
			this.horizontalScroller.slider.clampedDragger.draggingEnded += this.UpdateElasticBehaviour;
			this.verticalScroller.slider.clampedDragger.draggingEnded += this.UpdateElasticBehaviour;
			this.horizontalScroller.lowButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.horizontalScroller.highButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.lowButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.highButton.AddAction(new Action(this.UpdateElasticBehaviour));
			this.verticalScroller.AddToClassList(ScrollView.vScrollerUssClassName);
			this.verticalScroller.style.display = DisplayStyle.None;
			this.m_ContentAndVerticalScrollContainer.Add(this.verticalScroller);
			this.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			base.RegisterCallback<WheelEvent>(new EventCallback<WheelEvent>(this.OnScrollWheel), TrickleDown.NoTrickleDown);
			this.verticalScroller.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnScrollersGeometryChanged), TrickleDown.NoTrickleDown);
			this.horizontalScroller.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnScrollersGeometryChanged), TrickleDown.NoTrickleDown);
			this.horizontalPageSize = -1f;
			this.verticalPageSize = -1f;
			this.horizontalScroller.slider.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnHorizontalScrollDragElementChanged), TrickleDown.NoTrickleDown);
			this.verticalScroller.slider.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnVerticalScrollDragElementChanged), TrickleDown.NoTrickleDown);
			this.m_CapturedTargetPointerMoveCallback = new EventCallback<PointerMoveEvent>(this.OnPointerMove);
			this.m_CapturedTargetPointerUpCallback = new EventCallback<PointerUpEvent>(this.OnPointerUp);
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x000303CF File Offset: 0x0002E5CF
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x000303D8 File Offset: 0x0002E5D8
		public ScrollViewMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				bool flag = this.m_Mode == value;
				if (!flag)
				{
					this.SetScrollViewMode(value);
				}
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00030400 File Offset: 0x0002E600
		private void SetScrollViewMode(ScrollViewMode mode)
		{
			this.m_Mode = mode;
			base.RemoveFromClassList(ScrollView.verticalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.horizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.verticalHorizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.scrollVariantUssClassName);
			switch (mode)
			{
			case ScrollViewMode.Vertical:
				base.AddToClassList(ScrollView.verticalVariantUssClassName);
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				break;
			case ScrollViewMode.Horizontal:
				base.AddToClassList(ScrollView.horizontalVariantUssClassName);
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				break;
			case ScrollViewMode.VerticalAndHorizontal:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.verticalHorizontalVariantUssClassName);
				break;
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000304AC File Offset: 0x0002E6AC
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				this.m_AttachedRootVisualContainer = base.GetRootVisualContainer();
				VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
				if (attachedRootVisualContainer != null)
				{
					attachedRootVisualContainer.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				}
				this.ReadSingleLineHeight();
				bool flag2 = evt.destinationPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.m_ContentAndVerticalScrollContainer.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
					this.contentContainer.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
					this.contentContainer.RegisterCallback<PointerCaptureEvent>(new EventCallback<PointerCaptureEvent>(this.OnPointerCapture), TrickleDown.NoTrickleDown);
					this.contentContainer.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
					evt.destinationPanel.visualTree.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnRootPointerUp), TrickleDown.TrickleDown);
				}
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000305CC File Offset: 0x0002E7CC
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			IVisualElementScheduledItem scheduledLayoutPassResetItem = this.m_ScheduledLayoutPassResetItem;
			if (scheduledLayoutPassResetItem != null)
			{
				scheduledLayoutPassResetItem.Pause();
			}
			this.ResetLayoutPass();
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
				if (attachedRootVisualContainer != null)
				{
					attachedRootVisualContainer.UnregisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnRootCustomStyleResolved), TrickleDown.NoTrickleDown);
				}
				this.m_AttachedRootVisualContainer = null;
				bool flag2 = evt.originPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.m_ContentAndVerticalScrollContainer.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.TrickleDown);
					this.contentContainer.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.TrickleDown);
					this.contentContainer.UnregisterCallback<PointerCaptureEvent>(new EventCallback<PointerCaptureEvent>(this.OnPointerCapture), TrickleDown.NoTrickleDown);
					this.contentContainer.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
					evt.originPanel.visualTree.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnRootPointerUp), TrickleDown.TrickleDown);
				}
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000306F8 File Offset: 0x0002E8F8
		private void OnPointerCapture(PointerCaptureEvent evt)
		{
			this.m_CapturedTarget = (evt.target as VisualElement);
			bool flag = this.m_CapturedTarget == null;
			if (!flag)
			{
				this.m_TouchPointerMoveAllowed = true;
				this.m_CapturedTarget.RegisterCallback<PointerMoveEvent>(this.m_CapturedTargetPointerMoveCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget.RegisterCallback<PointerUpEvent>(this.m_CapturedTargetPointerUpCallback, TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00030754 File Offset: 0x0002E954
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			this.ReleaseScrolling(evt.pointerId, evt.target);
			bool flag = this.m_CapturedTarget == null;
			if (!flag)
			{
				this.m_CapturedTarget.UnregisterCallback<PointerMoveEvent>(this.m_CapturedTargetPointerMoveCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget.UnregisterCallback<PointerUpEvent>(this.m_CapturedTargetPointerUpCallback, TrickleDown.NoTrickleDown);
				this.m_CapturedTarget = null;
			}
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000307B4 File Offset: 0x0002E9B4
		private void OnGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool flag2 = this.needsVertical;
				bool flag3 = this.needsHorizontal;
				bool flag4 = this.m_FirstLayoutPass == -1;
				if (flag4)
				{
					this.m_FirstLayoutPass = evt.layoutPass;
				}
				else
				{
					bool flag5 = evt.layoutPass - this.m_FirstLayoutPass > 5;
					if (flag5)
					{
						flag2 = (flag2 || this.isVerticalScrollDisplayed);
						flag3 = (flag3 || this.isHorizontalScrollDisplayed);
					}
				}
				this.UpdateScrollers(flag3, flag2);
				this.UpdateContentViewTransform();
				this.ScheduleResetLayoutPass();
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00030860 File Offset: 0x0002EA60
		private void ScheduleResetLayoutPass()
		{
			bool flag = this.m_ScheduledLayoutPassResetItem == null;
			if (flag)
			{
				this.m_ScheduledLayoutPassResetItem = base.schedule.Execute(new Action(this.ResetLayoutPass));
			}
			else
			{
				this.m_ScheduledLayoutPassResetItem.Pause();
				this.m_ScheduledLayoutPassResetItem.Resume();
			}
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000308B6 File Offset: 0x0002EAB6
		private void ResetLayoutPass()
		{
			this.m_FirstLayoutPass = -1;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000308C0 File Offset: 0x0002EAC0
		private static float ComputeElasticOffset(float deltaPointer, float initialScrollOffset, float lowLimit, float hardLowLimit, float highLimit, float hardHighLimit)
		{
			initialScrollOffset = Mathf.Max(initialScrollOffset, hardLowLimit * 0.95f);
			initialScrollOffset = Mathf.Min(initialScrollOffset, hardHighLimit * 0.95f);
			bool flag = initialScrollOffset < lowLimit && hardLowLimit < lowLimit;
			float num;
			float num3;
			if (flag)
			{
				num = lowLimit - hardLowLimit;
				float num2 = (lowLimit - initialScrollOffset) / num;
				num3 = num2 * num / (1f - num2);
				num3 += deltaPointer;
				initialScrollOffset = lowLimit;
			}
			else
			{
				bool flag2 = initialScrollOffset > highLimit && hardHighLimit > highLimit;
				if (flag2)
				{
					num = hardHighLimit - highLimit;
					float num4 = (initialScrollOffset - highLimit) / num;
					num3 = -1f * num4 * num / (1f - num4);
					num3 += deltaPointer;
					initialScrollOffset = highLimit;
				}
				else
				{
					num3 = deltaPointer;
				}
			}
			float num5 = initialScrollOffset - num3;
			bool flag3 = num5 < lowLimit;
			float num6;
			if (flag3)
			{
				num3 = lowLimit - num5;
				initialScrollOffset = lowLimit;
				num = lowLimit - hardLowLimit;
				num6 = 1f;
			}
			else
			{
				bool flag4 = num5 <= highLimit;
				if (flag4)
				{
					return num5;
				}
				num3 = num5 - highLimit;
				initialScrollOffset = highLimit;
				num = hardHighLimit - highLimit;
				num6 = -1f;
			}
			bool flag5 = Mathf.Abs(num3) < 1E-30f;
			float result;
			if (flag5)
			{
				result = initialScrollOffset;
			}
			else
			{
				float num7 = num3 / (num3 + num);
				num7 *= num;
				num7 *= num6;
				num5 = initialScrollOffset - num7;
				result = num5;
			}
			return result;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000309F0 File Offset: 0x0002EBF0
		private void ComputeInitialSpringBackVelocity()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				bool flag2 = this.scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					this.m_SpringBackVelocity.x = this.m_LowBounds.x - this.scrollOffset.x;
				}
				else
				{
					bool flag3 = this.scrollOffset.x > this.m_HighBounds.x;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = this.m_HighBounds.x - this.scrollOffset.x;
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag4 = this.scrollOffset.y < this.m_LowBounds.y;
				if (flag4)
				{
					this.m_SpringBackVelocity.y = this.m_LowBounds.y - this.scrollOffset.y;
				}
				else
				{
					bool flag5 = this.scrollOffset.y > this.m_HighBounds.y;
					if (flag5)
					{
						this.m_SpringBackVelocity.y = this.m_HighBounds.y - this.scrollOffset.y;
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00030B50 File Offset: 0x0002ED50
		private void SpringBack()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				Vector2 scrollOffset = this.scrollOffset;
				bool flag2 = scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_LowBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastHorizontalTouchScroll);
					bool flag3 = Mathf.Abs(this.m_SpringBackVelocity.x) < base.scaledPixelsPerPoint;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				else
				{
					bool flag4 = scrollOffset.x > this.m_HighBounds.x;
					if (flag4)
					{
						scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_HighBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastHorizontalTouchScroll);
						bool flag5 = Mathf.Abs(this.m_SpringBackVelocity.x) < base.scaledPixelsPerPoint;
						if (flag5)
						{
							this.m_SpringBackVelocity.x = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag6 = scrollOffset.y < this.m_LowBounds.y;
				if (flag6)
				{
					scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_LowBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastVerticalTouchScroll);
					bool flag7 = Mathf.Abs(this.m_SpringBackVelocity.y) < base.scaledPixelsPerPoint;
					if (flag7)
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				else
				{
					bool flag8 = scrollOffset.y > this.m_HighBounds.y;
					if (flag8)
					{
						scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_HighBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, this.elapsedTimeSinceLastVerticalTouchScroll);
						bool flag9 = Mathf.Abs(this.m_SpringBackVelocity.y) < base.scaledPixelsPerPoint;
						if (flag9)
						{
							this.m_SpringBackVelocity.y = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				this.scrollOffset = scrollOffset;
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00030DCC File Offset: 0x0002EFCC
		internal void ApplyScrollInertia()
		{
			bool flag = this.hasInertia && this.m_Velocity != Vector2.zero;
			if (flag)
			{
				Vector2 vector = Vector2.zero;
				float num = 0f;
				while (num < this.elapsedTimeSinceLastVerticalTouchScroll)
				{
					this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, this.k_TouchScrollInertiaBaseTimeInterval);
					num += this.k_TouchScrollInertiaBaseTimeInterval;
					vector += this.m_Velocity * this.k_TouchScrollInertiaBaseTimeInterval;
				}
				float num2 = this.elapsedTimeSinceLastVerticalTouchScroll - num;
				bool flag2 = num2 > 0f && num2 < this.k_TouchScrollInertiaBaseTimeInterval;
				if (flag2)
				{
					this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, num2);
					vector += this.m_Velocity * num2;
				}
				float num3 = base.scaledPixelsPerPoint * this.k_ScaledPixelsPerPointMultiplier;
				bool flag3 = Mathf.Abs(this.m_Velocity.x) <= num3 || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.x < this.m_LowBounds.x || this.scrollOffset.x > this.m_HighBounds.x));
				if (flag3)
				{
					this.m_Velocity.x = 0f;
				}
				bool flag4 = Mathf.Abs(this.m_Velocity.y) <= num3 || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.y < this.m_LowBounds.y || this.scrollOffset.y > this.m_HighBounds.y));
				if (flag4)
				{
					this.m_Velocity.y = 0f;
				}
				this.scrollOffset += vector;
			}
			else
			{
				this.m_Velocity = Vector2.zero;
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00030FC4 File Offset: 0x0002F1C4
		private void PostPointerUpAnimation()
		{
			this.elapsedTimeSinceLastVerticalTouchScroll = Time.unscaledTime - this.previousVerticalTouchScrollTimeStamp;
			this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
			this.elapsedTimeSinceLastHorizontalTouchScroll = Time.unscaledTime - this.previousHorizontalTouchScrollTimeStamp;
			this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
			this.ApplyScrollInertia();
			this.SpringBack();
			bool flag = this.m_SpringBackVelocity == Vector2.zero && this.m_Velocity == Vector2.zero;
			if (flag)
			{
				this.m_PostPointerUpAnimation.Pause();
				this.elapsedTimeSinceLastVerticalTouchScroll = 0f;
				this.elapsedTimeSinceLastHorizontalTouchScroll = 0f;
				this.previousVerticalTouchScrollTimeStamp = 0f;
				this.previousHorizontalTouchScrollTimeStamp = 0f;
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00031080 File Offset: 0x0002F280
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = evt.pointerType == PointerType.mouse || !evt.isPrimary;
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.invalidPointerId;
				if (flag2)
				{
					this.ReleaseScrolling(evt.pointerId, evt.target);
				}
				IVisualElementScheduledItem postPointerUpAnimation = this.m_PostPointerUpAnimation;
				if (postPointerUpAnimation != null)
				{
					postPointerUpAnimation.Pause();
				}
				bool flag3 = Mathf.Abs(this.m_Velocity.x) > 10f || Mathf.Abs(this.m_Velocity.y) > 10f;
				this.m_TouchPointerMoveAllowed = true;
				this.m_StartedMoving = false;
				this.InitTouchScrolling(evt.position);
				bool flag4 = flag3;
				if (flag4)
				{
					this.contentContainer.CapturePointer(evt.pointerId);
					this.contentContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
					evt.StopPropagation();
					this.m_TouchStoppedVelocity = true;
				}
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00031180 File Offset: 0x0002F380
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.pointerType == PointerType.mouse || !evt.isPrimary || !this.m_TouchPointerMoveAllowed;
			if (!flag)
			{
				bool isHandledByDraggable = evt.isHandledByDraggable;
				if (isHandledByDraggable)
				{
					this.m_PointerStartPosition = evt.position;
					this.m_StartPosition = this.scrollOffset;
				}
				else
				{
					Vector2 a = evt.position;
					Vector2 vector = a - this.m_PointerStartPosition;
					bool flag2 = this.mode == ScrollViewMode.Horizontal;
					if (flag2)
					{
						vector.y = 0f;
					}
					else
					{
						bool flag3 = this.mode == ScrollViewMode.Vertical;
						if (flag3)
						{
							vector.x = 0f;
						}
					}
					bool flag4 = !this.m_TouchStoppedVelocity && !this.m_StartedMoving && vector.sqrMagnitude < 100f;
					if (!flag4)
					{
						ScrollView.TouchScrollingResult touchScrollingResult = this.ComputeTouchScrolling(evt.position);
						bool flag5 = touchScrollingResult != ScrollView.TouchScrollingResult.Forward;
						if (flag5)
						{
							evt.isHandledByDraggable = true;
							evt.StopPropagation();
							bool flag6 = !this.contentContainer.HasPointerCapture(evt.pointerId);
							if (flag6)
							{
								this.contentContainer.CapturePointer(evt.pointerId);
							}
						}
						else
						{
							this.m_Velocity = Vector2.zero;
						}
					}
				}
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000312D1 File Offset: 0x0002F4D1
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			this.ReleaseScrolling(evt.pointerId, evt.target);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000312E8 File Offset: 0x0002F4E8
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = this.ReleaseScrolling(evt.pointerId, evt.target);
			if (flag)
			{
				this.contentContainer.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				evt.StopPropagation();
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003132C File Offset: 0x0002F52C
		internal void InitTouchScrolling(Vector2 position)
		{
			this.m_PointerStartPosition = position;
			this.m_StartPosition = this.scrollOffset;
			this.m_Velocity = Vector2.zero;
			this.m_SpringBackVelocity = Vector2.zero;
			this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
			this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000313E8 File Offset: 0x0002F5E8
		internal ScrollView.TouchScrollingResult ComputeTouchScrolling(Vector2 position)
		{
			bool flag = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
			Vector2 vector;
			if (flag)
			{
				vector = this.m_StartPosition - (position - this.m_PointerStartPosition);
				vector = Vector2.Max(vector, this.m_LowBounds);
				vector = Vector2.Min(vector, this.m_HighBounds);
			}
			else
			{
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
				if (flag2)
				{
					Vector2 vector2 = position - this.m_PointerStartPosition;
					vector.x = ScrollView.ComputeElasticOffset(vector2.x, this.m_StartPosition.x, this.m_LowBounds.x, this.m_LowBounds.x - this.contentViewport.resolvedStyle.width, this.m_HighBounds.x, this.m_HighBounds.x + this.contentViewport.resolvedStyle.width);
					vector.y = ScrollView.ComputeElasticOffset(vector2.y, this.m_StartPosition.y, this.m_LowBounds.y, this.m_LowBounds.y - this.contentViewport.resolvedStyle.height, this.m_HighBounds.y, this.m_HighBounds.y + this.contentViewport.resolvedStyle.height);
					this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
					this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
				}
				else
				{
					vector = this.m_StartPosition - (position - this.m_PointerStartPosition);
				}
			}
			bool flag3 = this.mode == ScrollViewMode.Vertical;
			if (flag3)
			{
				vector.x = this.m_LowBounds.x;
			}
			else
			{
				bool flag4 = this.mode == ScrollViewMode.Horizontal;
				if (flag4)
				{
					vector.y = this.m_LowBounds.y;
				}
			}
			bool flag5 = this.scrollOffset != vector;
			bool flag6 = flag5;
			ScrollView.TouchScrollingResult result;
			if (flag6)
			{
				result = (this.ApplyTouchScrolling(vector) ? ScrollView.TouchScrollingResult.Apply : ScrollView.TouchScrollingResult.Forward);
			}
			else
			{
				result = ((this.m_StartedMoving && this.nestedInteractionKind != ScrollView.NestedInteractionKind.ForwardScrolling) ? ScrollView.TouchScrollingResult.Block : ScrollView.TouchScrollingResult.Forward);
			}
			return result;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000315F8 File Offset: 0x0002F7F8
		private bool ApplyTouchScrolling(Vector2 newScrollOffset)
		{
			this.m_StartedMoving = true;
			bool hasInertia = this.hasInertia;
			if (hasInertia)
			{
				bool flag = newScrollOffset == this.m_LowBounds || newScrollOffset == this.m_HighBounds;
				if (flag)
				{
					this.m_Velocity = Vector2.zero;
					this.scrollOffset = newScrollOffset;
					return false;
				}
				bool flag2 = this.m_LastVelocityLerpTime > 0f;
				if (flag2)
				{
					float num = Time.unscaledTime - this.m_LastVelocityLerpTime;
					this.m_Velocity = Vector2.Lerp(this.m_Velocity, Vector2.zero, num * 10f);
				}
				this.m_LastVelocityLerpTime = Time.unscaledTime;
				float num2 = this.k_TouchScrollInertiaBaseTimeInterval;
				Vector2 b = (newScrollOffset - this.scrollOffset) / num2;
				this.m_Velocity = Vector2.Lerp(this.m_Velocity, b, num2 * 10f);
			}
			bool result = this.scrollOffset != newScrollOffset;
			this.scrollOffset = newScrollOffset;
			return result;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000316F8 File Offset: 0x0002F8F8
		private bool ReleaseScrolling(int pointerId, IEventHandler target)
		{
			this.m_TouchStoppedVelocity = false;
			this.m_StartedMoving = false;
			this.m_TouchPointerMoveAllowed = false;
			bool flag = target != this.contentContainer || !this.contentContainer.HasPointerCapture(pointerId);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.previousVerticalTouchScrollTimeStamp = Time.unscaledTime;
				this.previousHorizontalTouchScrollTimeStamp = Time.unscaledTime;
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic || this.hasInertia;
				if (flag2)
				{
					this.ExecuteElasticSpringAnimation();
				}
				this.contentContainer.ReleasePointer(pointerId);
				result = true;
			}
			return result;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00031788 File Offset: 0x0002F988
		private void ExecuteElasticSpringAnimation()
		{
			this.ComputeInitialSpringBackVelocity();
			bool flag = this.m_PostPointerUpAnimation == null;
			if (flag)
			{
				this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(this.m_ElasticAnimationIntervalMs);
			}
			else
			{
				this.m_PostPointerUpAnimation.Resume();
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000317E4 File Offset: 0x0002F9E4
		private void AdjustScrollers()
		{
			float factor = (this.contentContainer.boundingBox.width > 1E-30f) ? (this.contentViewport.layout.width / this.contentContainer.boundingBox.width) : 1f;
			float factor2 = (this.contentContainer.boundingBox.height > 1E-30f) ? (this.contentViewport.layout.height / this.contentContainer.boundingBox.height) : 1f;
			this.horizontalScroller.Adjust(factor);
			this.verticalScroller.Adjust(factor2);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000318A0 File Offset: 0x0002FAA0
		internal void UpdateScrollers(bool displayHorizontal, bool displayVertical)
		{
			this.AdjustScrollers();
			this.horizontalScroller.SetEnabled(this.contentContainer.boundingBox.width - this.contentViewport.layout.width > 0f);
			this.verticalScroller.SetEnabled(this.contentContainer.boundingBox.height - this.contentViewport.layout.height > 0f);
			bool flag = displayHorizontal && this.m_HorizontalScrollerVisibility != ScrollerVisibility.Hidden;
			bool flag2 = displayVertical && this.m_VerticalScrollerVisibility != ScrollerVisibility.Hidden;
			DisplayStyle v = flag ? DisplayStyle.Flex : DisplayStyle.None;
			DisplayStyle v2 = flag2 ? DisplayStyle.Flex : DisplayStyle.None;
			bool flag3 = v != this.horizontalScroller.style.display;
			if (flag3)
			{
				this.horizontalScroller.style.display = v;
			}
			bool flag4 = v2 != this.verticalScroller.style.display;
			if (flag4)
			{
				this.verticalScroller.style.display = v2;
			}
			this.verticalScroller.lowValue = 0f;
			this.verticalScroller.highValue = this.scrollableHeight;
			this.horizontalScroller.lowValue = 0f;
			this.horizontalScroller.highValue = this.scrollableWidth;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00031A20 File Offset: 0x0002FC20
		private void OnScrollersGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool flag2 = this.needsHorizontal && this.m_HorizontalScrollerVisibility != ScrollerVisibility.Hidden;
				bool flag3 = flag2;
				if (flag3)
				{
					this.horizontalScroller.style.marginRight = this.verticalScroller.layout.width;
				}
				this.AdjustScrollers();
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00031AA8 File Offset: 0x0002FCA8
		private void OnScrollWheel(WheelEvent evt)
		{
			bool flag = false;
			bool flag2 = this.contentContainer.boundingBox.height - base.layout.height > 0f;
			bool flag3 = this.contentContainer.boundingBox.width - base.layout.width > 0f;
			float num = (flag3 && !flag2) ? evt.delta.y : evt.delta.x;
			float num2 = this.m_MouseWheelScrollSizeIsInline ? this.mouseWheelScrollSize : this.m_SingleLineHeight;
			bool flag4 = flag2;
			if (flag4)
			{
				float value = this.verticalScroller.value;
				this.verticalScroller.value += evt.delta.y * ((this.verticalScroller.lowValue < this.verticalScroller.highValue) ? 1f : -1f) * num2;
				bool flag5 = this.nestedInteractionKind == ScrollView.NestedInteractionKind.StopScrolling || !Mathf.Approximately(this.verticalScroller.value, value);
				if (flag5)
				{
					evt.StopPropagation();
					flag = true;
				}
			}
			bool flag6 = flag3;
			if (flag6)
			{
				float value2 = this.horizontalScroller.value;
				this.horizontalScroller.value += num * ((this.horizontalScroller.lowValue < this.horizontalScroller.highValue) ? 1f : -1f) * num2;
				bool flag7 = this.nestedInteractionKind == ScrollView.NestedInteractionKind.StopScrolling || !Mathf.Approximately(this.horizontalScroller.value, value2);
				if (flag7)
				{
					evt.StopPropagation();
					flag = true;
				}
			}
			bool flag8 = flag;
			if (flag8)
			{
				this.UpdateElasticBehaviour();
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00031C73 File Offset: 0x0002FE73
		private void OnRootCustomStyleResolved(CustomStyleResolvedEvent evt)
		{
			this.ReadSingleLineHeight();
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00031C7D File Offset: 0x0002FE7D
		private void OnRootPointerUp(PointerUpEvent evt)
		{
			this.m_TouchPointerMoveAllowed = false;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00031C88 File Offset: 0x0002FE88
		private void ReadSingleLineHeight()
		{
			VisualElement attachedRootVisualContainer = this.m_AttachedRootVisualContainer;
			StylePropertyValue stylePropertyValue;
			bool flag = ((attachedRootVisualContainer != null) ? attachedRootVisualContainer.computedStyle.customProperties : null) != null && this.m_AttachedRootVisualContainer.computedStyle.customProperties.TryGetValue("--unity-metrics-single_line-height", out stylePropertyValue);
			if (flag)
			{
				Dimension dimension;
				bool flag2 = stylePropertyValue.sheet.TryReadDimension(stylePropertyValue.handle, out dimension);
				if (flag2)
				{
					this.m_SingleLineHeight = dimension.value;
				}
			}
			else
			{
				this.m_SingleLineHeight = UIElementsUtility.singleLineHeight;
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00031D08 File Offset: 0x0002FF08
		private void UpdateElasticBehaviour()
		{
			bool flag = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				this.ExecuteElasticSpringAnimation();
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00031DB4 File Offset: 0x0002FFB4
		// Note: this type is marked as 'beforefieldinit'.
		static ScrollView()
		{
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00031E9B File Offset: 0x0003009B
		[CompilerGenerated]
		private void <.ctor>b__123_0(float value)
		{
			this.scrollOffset = new Vector2(value, this.scrollOffset.y);
			this.UpdateContentViewTransform();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00031EBD File Offset: 0x000300BD
		[CompilerGenerated]
		private void <.ctor>b__123_1(float value)
		{
			this.scrollOffset = new Vector2(this.scrollOffset.x, value);
			this.UpdateContentViewTransform();
		}

		// Token: 0x04000559 RID: 1369
		private const int k_MaxLocalLayoutPassCount = 5;

		// Token: 0x0400055A RID: 1370
		private int m_FirstLayoutPass = -1;

		// Token: 0x0400055B RID: 1371
		private ScrollerVisibility m_HorizontalScrollerVisibility;

		// Token: 0x0400055C RID: 1372
		private ScrollerVisibility m_VerticalScrollerVisibility;

		// Token: 0x0400055D RID: 1373
		private const float k_SizeThreshold = 0.001f;

		// Token: 0x0400055E RID: 1374
		private VisualElement m_AttachedRootVisualContainer;

		// Token: 0x0400055F RID: 1375
		private float m_SingleLineHeight = UIElementsUtility.singleLineHeight;

		// Token: 0x04000560 RID: 1376
		private const string k_SingleLineHeightPropertyName = "--unity-metrics-single_line-height";

		// Token: 0x04000561 RID: 1377
		private const float k_ScrollPageOverlapFactor = 0.1f;

		// Token: 0x04000562 RID: 1378
		internal const float k_UnsetPageSizeValue = -1f;

		// Token: 0x04000563 RID: 1379
		internal const float k_MouseWheelScrollSizeDefaultValue = 18f;

		// Token: 0x04000564 RID: 1380
		internal const float k_MouseWheelScrollSizeUnset = -1f;

		// Token: 0x04000565 RID: 1381
		internal bool m_MouseWheelScrollSizeIsInline;

		// Token: 0x04000566 RID: 1382
		private float m_HorizontalPageSize;

		// Token: 0x04000567 RID: 1383
		private float m_VerticalPageSize;

		// Token: 0x04000568 RID: 1384
		private float m_MouseWheelScrollSize = 18f;

		// Token: 0x04000569 RID: 1385
		private static readonly float k_DefaultScrollDecelerationRate = 0.135f;

		// Token: 0x0400056A RID: 1386
		private float m_ScrollDecelerationRate = ScrollView.k_DefaultScrollDecelerationRate;

		// Token: 0x0400056B RID: 1387
		private float k_ScaledPixelsPerPointMultiplier = 10f;

		// Token: 0x0400056C RID: 1388
		private float k_TouchScrollInertiaBaseTimeInterval = 0.004167f;

		// Token: 0x0400056D RID: 1389
		private static readonly float k_DefaultElasticity = 0.1f;

		// Token: 0x0400056E RID: 1390
		private float m_Elasticity = ScrollView.k_DefaultElasticity;

		// Token: 0x0400056F RID: 1391
		private ScrollView.TouchScrollBehavior m_TouchScrollBehavior;

		// Token: 0x04000570 RID: 1392
		private ScrollView.NestedInteractionKind m_NestedInteractionKind;

		// Token: 0x04000571 RID: 1393
		private static readonly long k_DefaultElasticAnimationInterval = 16L;

		// Token: 0x04000572 RID: 1394
		private long m_ElasticAnimationIntervalMs = ScrollView.k_DefaultElasticAnimationInterval;

		// Token: 0x04000573 RID: 1395
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VisualElement <contentViewport>k__BackingField;

		// Token: 0x04000574 RID: 1396
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Scroller <horizontalScroller>k__BackingField;

		// Token: 0x04000575 RID: 1397
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Scroller <verticalScroller>k__BackingField;

		// Token: 0x04000576 RID: 1398
		private VisualElement m_ContentContainer;

		// Token: 0x04000577 RID: 1399
		private VisualElement m_ContentAndVerticalScrollContainer;

		// Token: 0x04000578 RID: 1400
		private float previousVerticalTouchScrollTimeStamp = 0f;

		// Token: 0x04000579 RID: 1401
		private float previousHorizontalTouchScrollTimeStamp = 0f;

		// Token: 0x0400057A RID: 1402
		private float elapsedTimeSinceLastVerticalTouchScroll = 0f;

		// Token: 0x0400057B RID: 1403
		private float elapsedTimeSinceLastHorizontalTouchScroll = 0f;

		// Token: 0x0400057C RID: 1404
		public static readonly string ussClassName = "unity-scroll-view";

		// Token: 0x0400057D RID: 1405
		public static readonly string viewportUssClassName = ScrollView.ussClassName + "__content-viewport";

		// Token: 0x0400057E RID: 1406
		public static readonly string contentAndVerticalScrollUssClassName = ScrollView.ussClassName + "__content-and-vertical-scroll-container";

		// Token: 0x0400057F RID: 1407
		public static readonly string contentUssClassName = ScrollView.ussClassName + "__content-container";

		// Token: 0x04000580 RID: 1408
		public static readonly string hScrollerUssClassName = ScrollView.ussClassName + "__horizontal-scroller";

		// Token: 0x04000581 RID: 1409
		public static readonly string vScrollerUssClassName = ScrollView.ussClassName + "__vertical-scroller";

		// Token: 0x04000582 RID: 1410
		public static readonly string horizontalVariantUssClassName = ScrollView.ussClassName + "--horizontal";

		// Token: 0x04000583 RID: 1411
		public static readonly string verticalVariantUssClassName = ScrollView.ussClassName + "--vertical";

		// Token: 0x04000584 RID: 1412
		public static readonly string verticalHorizontalVariantUssClassName = ScrollView.ussClassName + "--vertical-horizontal";

		// Token: 0x04000585 RID: 1413
		public static readonly string scrollVariantUssClassName = ScrollView.ussClassName + "--scroll";

		// Token: 0x04000586 RID: 1414
		private ScrollViewMode m_Mode;

		// Token: 0x04000587 RID: 1415
		private IVisualElementScheduledItem m_ScheduledLayoutPassResetItem;

		// Token: 0x04000588 RID: 1416
		private const float k_VelocityLerpTimeFactor = 10f;

		// Token: 0x04000589 RID: 1417
		internal const float ScrollThresholdSquared = 100f;

		// Token: 0x0400058A RID: 1418
		private Vector2 m_StartPosition;

		// Token: 0x0400058B RID: 1419
		private Vector2 m_PointerStartPosition;

		// Token: 0x0400058C RID: 1420
		private Vector2 m_Velocity;

		// Token: 0x0400058D RID: 1421
		private Vector2 m_SpringBackVelocity;

		// Token: 0x0400058E RID: 1422
		private Vector2 m_LowBounds;

		// Token: 0x0400058F RID: 1423
		private Vector2 m_HighBounds;

		// Token: 0x04000590 RID: 1424
		private float m_LastVelocityLerpTime;

		// Token: 0x04000591 RID: 1425
		private bool m_StartedMoving;

		// Token: 0x04000592 RID: 1426
		private bool m_TouchPointerMoveAllowed;

		// Token: 0x04000593 RID: 1427
		private bool m_TouchStoppedVelocity;

		// Token: 0x04000594 RID: 1428
		private VisualElement m_CapturedTarget;

		// Token: 0x04000595 RID: 1429
		private EventCallback<PointerMoveEvent> m_CapturedTargetPointerMoveCallback;

		// Token: 0x04000596 RID: 1430
		private EventCallback<PointerUpEvent> m_CapturedTargetPointerUpCallback;

		// Token: 0x04000597 RID: 1431
		internal IVisualElementScheduledItem m_PostPointerUpAnimation;

		// Token: 0x02000172 RID: 370
		public new class UxmlFactory : UxmlFactory<ScrollView, ScrollView.UxmlTraits>
		{
			// Token: 0x06000BF0 RID: 3056 RVA: 0x00031EDF File Offset: 0x000300DF
			public UxmlFactory()
			{
			}
		}

		// Token: 0x02000173 RID: 371
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x06000BF1 RID: 3057 RVA: 0x00031EE8 File Offset: 0x000300E8
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ScrollView scrollView = (ScrollView)ve;
				scrollView.mode = this.m_ScrollViewMode.GetValueFromBag(bag, cc);
				ScrollerVisibility horizontalScrollerVisibility = ScrollerVisibility.Auto;
				bool flag = this.m_HorizontalScrollerVisibility.TryGetValueFromBag(bag, cc, ref horizontalScrollerVisibility);
				if (flag)
				{
					scrollView.horizontalScrollerVisibility = horizontalScrollerVisibility;
				}
				else
				{
					scrollView.showHorizontal = this.m_ShowHorizontal.GetValueFromBag(bag, cc);
				}
				ScrollerVisibility verticalScrollerVisibility = ScrollerVisibility.Auto;
				bool flag2 = this.m_VerticalScrollerVisibility.TryGetValueFromBag(bag, cc, ref verticalScrollerVisibility);
				if (flag2)
				{
					scrollView.verticalScrollerVisibility = verticalScrollerVisibility;
				}
				else
				{
					scrollView.showVertical = this.m_ShowVertical.GetValueFromBag(bag, cc);
				}
				scrollView.nestedInteractionKind = this.m_NestedInteractionKind.GetValueFromBag(bag, cc);
				scrollView.horizontalPageSize = this.m_HorizontalPageSize.GetValueFromBag(bag, cc);
				scrollView.verticalPageSize = this.m_VerticalPageSize.GetValueFromBag(bag, cc);
				scrollView.mouseWheelScrollSize = this.m_MouseWheelScrollSize.GetValueFromBag(bag, cc);
				scrollView.scrollDecelerationRate = this.m_ScrollDecelerationRate.GetValueFromBag(bag, cc);
				scrollView.touchScrollBehavior = this.m_TouchScrollBehavior.GetValueFromBag(bag, cc);
				scrollView.elasticity = this.m_Elasticity.GetValueFromBag(bag, cc);
				scrollView.elasticAnimationIntervalMs = this.m_ElasticAnimationIntervalMs.GetValueFromBag(bag, cc);
			}

			// Token: 0x06000BF2 RID: 3058 RVA: 0x00032024 File Offset: 0x00030224
			public UxmlTraits()
			{
			}

			// Token: 0x04000598 RID: 1432
			private UxmlEnumAttributeDescription<ScrollViewMode> m_ScrollViewMode = new UxmlEnumAttributeDescription<ScrollViewMode>
			{
				name = "mode",
				defaultValue = ScrollViewMode.Vertical
			};

			// Token: 0x04000599 RID: 1433
			private UxmlEnumAttributeDescription<ScrollView.NestedInteractionKind> m_NestedInteractionKind = new UxmlEnumAttributeDescription<ScrollView.NestedInteractionKind>
			{
				name = "nested-interaction-kind",
				defaultValue = ScrollView.NestedInteractionKind.Default
			};

			// Token: 0x0400059A RID: 1434
			private UxmlBoolAttributeDescription m_ShowHorizontal = new UxmlBoolAttributeDescription
			{
				name = "show-horizontal-scroller"
			};

			// Token: 0x0400059B RID: 1435
			private UxmlBoolAttributeDescription m_ShowVertical = new UxmlBoolAttributeDescription
			{
				name = "show-vertical-scroller"
			};

			// Token: 0x0400059C RID: 1436
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_HorizontalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "horizontal-scroller-visibility"
			};

			// Token: 0x0400059D RID: 1437
			private UxmlEnumAttributeDescription<ScrollerVisibility> m_VerticalScrollerVisibility = new UxmlEnumAttributeDescription<ScrollerVisibility>
			{
				name = "vertical-scroller-visibility"
			};

			// Token: 0x0400059E RID: 1438
			private UxmlFloatAttributeDescription m_HorizontalPageSize = new UxmlFloatAttributeDescription
			{
				name = "horizontal-page-size",
				defaultValue = -1f
			};

			// Token: 0x0400059F RID: 1439
			private UxmlFloatAttributeDescription m_VerticalPageSize = new UxmlFloatAttributeDescription
			{
				name = "vertical-page-size",
				defaultValue = -1f
			};

			// Token: 0x040005A0 RID: 1440
			private UxmlFloatAttributeDescription m_MouseWheelScrollSize = new UxmlFloatAttributeDescription
			{
				name = "mouse-wheel-scroll-size",
				defaultValue = 18f
			};

			// Token: 0x040005A1 RID: 1441
			private UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior> m_TouchScrollBehavior = new UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior>
			{
				name = "touch-scroll-type",
				defaultValue = ScrollView.TouchScrollBehavior.Clamped
			};

			// Token: 0x040005A2 RID: 1442
			private UxmlFloatAttributeDescription m_ScrollDecelerationRate = new UxmlFloatAttributeDescription
			{
				name = "scroll-deceleration-rate",
				defaultValue = ScrollView.k_DefaultScrollDecelerationRate
			};

			// Token: 0x040005A3 RID: 1443
			private UxmlFloatAttributeDescription m_Elasticity = new UxmlFloatAttributeDescription
			{
				name = "elasticity",
				defaultValue = ScrollView.k_DefaultElasticity
			};

			// Token: 0x040005A4 RID: 1444
			private UxmlLongAttributeDescription m_ElasticAnimationIntervalMs = new UxmlLongAttributeDescription
			{
				name = "elastic-animation-interval-ms",
				defaultValue = ScrollView.k_DefaultElasticAnimationInterval
			};
		}

		// Token: 0x02000174 RID: 372
		public enum TouchScrollBehavior
		{
			// Token: 0x040005A6 RID: 1446
			Unrestricted,
			// Token: 0x040005A7 RID: 1447
			Elastic,
			// Token: 0x040005A8 RID: 1448
			Clamped
		}

		// Token: 0x02000175 RID: 373
		public enum NestedInteractionKind
		{
			// Token: 0x040005AA RID: 1450
			Default,
			// Token: 0x040005AB RID: 1451
			StopScrolling,
			// Token: 0x040005AC RID: 1452
			ForwardScrolling
		}

		// Token: 0x02000176 RID: 374
		internal enum TouchScrollingResult
		{
			// Token: 0x040005AE RID: 1454
			Apply,
			// Token: 0x040005AF RID: 1455
			Forward,
			// Token: 0x040005B0 RID: 1456
			Block
		}
	}
}
