using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000033 RID: 51
	[AddComponentMenu("UI/Scroll Rect", 37)]
	[SelectionBase]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	public class ScrollRect : UIBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler, ICanvasElement, ILayoutElement, ILayoutGroup, ILayoutController
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000113E2 File Offset: 0x0000F5E2
		// (set) Token: 0x0600035C RID: 860 RVA: 0x000113EA File Offset: 0x0000F5EA
		public RectTransform content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000113F3 File Offset: 0x0000F5F3
		// (set) Token: 0x0600035E RID: 862 RVA: 0x000113FB File Offset: 0x0000F5FB
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00011404 File Offset: 0x0000F604
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0001140C File Offset: 0x0000F60C
		public bool vertical
		{
			get
			{
				return this.m_Vertical;
			}
			set
			{
				this.m_Vertical = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00011415 File Offset: 0x0000F615
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0001141D File Offset: 0x0000F61D
		public ScrollRect.MovementType movementType
		{
			get
			{
				return this.m_MovementType;
			}
			set
			{
				this.m_MovementType = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00011426 File Offset: 0x0000F626
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0001142E File Offset: 0x0000F62E
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00011437 File Offset: 0x0000F637
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0001143F File Offset: 0x0000F63F
		public bool inertia
		{
			get
			{
				return this.m_Inertia;
			}
			set
			{
				this.m_Inertia = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00011448 File Offset: 0x0000F648
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00011450 File Offset: 0x0000F650
		public float decelerationRate
		{
			get
			{
				return this.m_DecelerationRate;
			}
			set
			{
				this.m_DecelerationRate = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00011459 File Offset: 0x0000F659
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00011461 File Offset: 0x0000F661
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				this.m_ScrollSensitivity = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0001146A File Offset: 0x0000F66A
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00011472 File Offset: 0x0000F672
		public RectTransform viewport
		{
			get
			{
				return this.m_Viewport;
			}
			set
			{
				this.m_Viewport = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00011481 File Offset: 0x0000F681
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0001148C File Offset: 0x0000F68C
		public Scrollbar horizontalScrollbar
		{
			get
			{
				return this.m_HorizontalScrollbar;
			}
			set
			{
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.m_HorizontalScrollbar = value;
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000114F8 File Offset: 0x0000F6F8
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00011500 File Offset: 0x0000F700
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.m_VerticalScrollbar = value;
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0001156C File Offset: 0x0000F76C
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00011574 File Offset: 0x0000F774
		public ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility
		{
			get
			{
				return this.m_HorizontalScrollbarVisibility;
			}
			set
			{
				this.m_HorizontalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00011583 File Offset: 0x0000F783
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0001158B File Offset: 0x0000F78B
		public ScrollRect.ScrollbarVisibility verticalScrollbarVisibility
		{
			get
			{
				return this.m_VerticalScrollbarVisibility;
			}
			set
			{
				this.m_VerticalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0001159A File Offset: 0x0000F79A
		// (set) Token: 0x06000376 RID: 886 RVA: 0x000115A2 File Offset: 0x0000F7A2
		public float horizontalScrollbarSpacing
		{
			get
			{
				return this.m_HorizontalScrollbarSpacing;
			}
			set
			{
				this.m_HorizontalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000377 RID: 887 RVA: 0x000115B1 File Offset: 0x0000F7B1
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000115B9 File Offset: 0x0000F7B9
		public float verticalScrollbarSpacing
		{
			get
			{
				return this.m_VerticalScrollbarSpacing;
			}
			set
			{
				this.m_VerticalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000115C8 File Offset: 0x0000F7C8
		// (set) Token: 0x0600037A RID: 890 RVA: 0x000115D0 File Offset: 0x0000F7D0
		public ScrollRect.ScrollRectEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000115DC File Offset: 0x0000F7DC
		protected RectTransform viewRect
		{
			get
			{
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = this.m_Viewport;
				}
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = (RectTransform)base.transform;
				}
				return this.m_ViewRect;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00011628 File Offset: 0x0000F828
		// (set) Token: 0x0600037D RID: 893 RVA: 0x00011630 File Offset: 0x0000F830
		public Vector2 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00011639 File Offset: 0x0000F839
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001165C File Offset: 0x0000F85C
		protected ScrollRect()
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000116E4 File Offset: 0x0000F8E4
		public virtual void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Prelayout)
			{
				this.UpdateCachedData();
			}
			if (executing == CanvasUpdate.PostLayout)
			{
				this.UpdateBounds();
				this.UpdateScrollbars(Vector2.zero);
				this.UpdatePrevData();
				this.m_HasRebuiltLayout = true;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011711 File Offset: 0x0000F911
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011713 File Offset: 0x0000F913
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011718 File Offset: 0x0000F918
		private void UpdateCachedData()
		{
			Transform transform = base.transform;
			this.m_HorizontalScrollbarRect = ((this.m_HorizontalScrollbar == null) ? null : (this.m_HorizontalScrollbar.transform as RectTransform));
			this.m_VerticalScrollbarRect = ((this.m_VerticalScrollbar == null) ? null : (this.m_VerticalScrollbar.transform as RectTransform));
			bool flag = this.viewRect.parent == transform;
			bool flag2 = !this.m_HorizontalScrollbarRect || this.m_HorizontalScrollbarRect.parent == transform;
			bool flag3 = !this.m_VerticalScrollbarRect || this.m_VerticalScrollbarRect.parent == transform;
			bool flag4 = flag && flag2 && flag3;
			this.m_HSliderExpand = (flag4 && this.m_HorizontalScrollbarRect && this.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
			this.m_VSliderExpand = (flag4 && this.m_VerticalScrollbarRect && this.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
			this.m_HSliderHeight = ((this.m_HorizontalScrollbarRect == null) ? 0f : this.m_HorizontalScrollbarRect.rect.height);
			this.m_VSliderWidth = ((this.m_VerticalScrollbarRect == null) ? 0f : this.m_VerticalScrollbarRect.rect.width);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011878 File Offset: 0x0000FA78
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			this.SetDirty();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000118EC File Offset: 0x0000FAEC
		protected override void OnDisable()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_Dragging = false;
			this.m_Scrolling = false;
			this.m_HasRebuiltLayout = false;
			this.m_Tracker.Clear();
			this.m_Velocity = Vector2.zero;
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001198D File Offset: 0x0000FB8D
		public override bool IsActive()
		{
			return base.IsActive() && this.m_Content != null;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000119A5 File Offset: 0x0000FBA5
		private void EnsureLayoutHasRebuilt()
		{
			if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			{
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000119BB File Offset: 0x0000FBBB
		public virtual void StopMovement()
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000119C8 File Offset: 0x0000FBC8
		public virtual void OnScroll(PointerEventData data)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			Vector2 scrollDelta = data.scrollDelta;
			scrollDelta.y *= -1f;
			if (this.vertical && !this.horizontal)
			{
				if (Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y))
				{
					scrollDelta.y = scrollDelta.x;
				}
				scrollDelta.x = 0f;
			}
			if (this.horizontal && !this.vertical)
			{
				if (Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x))
				{
					scrollDelta.x = scrollDelta.y;
				}
				scrollDelta.y = 0f;
			}
			if (data.IsScrolling())
			{
				this.m_Scrolling = true;
			}
			Vector2 vector = this.m_Content.anchoredPosition;
			vector += scrollDelta * this.m_ScrollSensitivity;
			if (this.m_MovementType == ScrollRect.MovementType.Clamped)
			{
				vector += this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			}
			this.SetContentAnchoredPosition(vector);
			this.UpdateBounds();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00011AE5 File Offset: 0x0000FCE5
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00011AFC File Offset: 0x0000FCFC
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateBounds();
			this.m_PointerStartLocalCursor = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out this.m_PointerStartLocalCursor);
			this.m_ContentStartPosition = this.m_Content.anchoredPosition;
			this.m_Dragging = true;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011B62 File Offset: 0x0000FD62
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Dragging = false;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011B74 File Offset: 0x0000FD74
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.m_Dragging)
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			this.UpdateBounds();
			Vector2 b = a - this.m_PointerStartLocalCursor;
			Vector2 vector = this.m_ContentStartPosition + b;
			Vector2 vector2 = this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			vector += vector2;
			if (this.m_MovementType == ScrollRect.MovementType.Elastic)
			{
				if (vector2.x != 0f)
				{
					vector.x -= ScrollRect.RubberDelta(vector2.x, this.m_ViewBounds.size.x);
				}
				if (vector2.y != 0f)
				{
					vector.y -= ScrollRect.RubberDelta(vector2.y, this.m_ViewBounds.size.y);
				}
			}
			this.SetContentAnchoredPosition(vector);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00011C74 File Offset: 0x0000FE74
		protected virtual void SetContentAnchoredPosition(Vector2 position)
		{
			if (!this.m_Horizontal)
			{
				position.x = this.m_Content.anchoredPosition.x;
			}
			if (!this.m_Vertical)
			{
				position.y = this.m_Content.anchoredPosition.y;
			}
			if (position != this.m_Content.anchoredPosition)
			{
				this.m_Content.anchoredPosition = position;
				this.UpdateBounds();
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		protected virtual void LateUpdate()
		{
			if (!this.m_Content)
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			Vector2 vector = this.CalculateOffset(Vector2.zero);
			if (unscaledDeltaTime > 0f)
			{
				if (!this.m_Dragging && (vector != Vector2.zero || this.m_Velocity != Vector2.zero))
				{
					Vector2 vector2 = this.m_Content.anchoredPosition;
					for (int i = 0; i < 2; i++)
					{
						if (this.m_MovementType == ScrollRect.MovementType.Elastic && vector[i] != 0f)
						{
							float num = this.m_Velocity[i];
							float num2 = this.m_Elasticity;
							if (this.m_Scrolling)
							{
								num2 *= 3f;
							}
							vector2[i] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[i], this.m_Content.anchoredPosition[i] + vector[i], ref num, num2, float.PositiveInfinity, unscaledDeltaTime);
							if (Mathf.Abs(num) < 1f)
							{
								num = 0f;
							}
							this.m_Velocity[i] = num;
						}
						else if (this.m_Inertia)
						{
							ref Vector2 ptr = ref this.m_Velocity;
							int index = i;
							ptr[index] *= Mathf.Pow(this.m_DecelerationRate, unscaledDeltaTime);
							if (Mathf.Abs(this.m_Velocity[i]) < 1f)
							{
								this.m_Velocity[i] = 0f;
							}
							ptr = ref vector2;
							index = i;
							ptr[index] += this.m_Velocity[i] * unscaledDeltaTime;
						}
						else
						{
							this.m_Velocity[i] = 0f;
						}
					}
					if (this.m_MovementType == ScrollRect.MovementType.Clamped)
					{
						vector = this.CalculateOffset(vector2 - this.m_Content.anchoredPosition);
						vector2 += vector;
					}
					this.SetContentAnchoredPosition(vector2);
				}
				if (this.m_Dragging && this.m_Inertia)
				{
					Vector3 b = (this.m_Content.anchoredPosition - this.m_PrevPosition) / unscaledDeltaTime;
					this.m_Velocity = Vector3.Lerp(this.m_Velocity, b, unscaledDeltaTime * 10f);
				}
			}
			if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
			{
				this.UpdateScrollbars(vector);
				UISystemProfilerApi.AddMarker("ScrollRect.value", this);
				this.m_OnValueChanged.Invoke(this.normalizedPosition);
				this.UpdatePrevData();
			}
			this.UpdateScrollbarVisibility();
			this.m_Scrolling = false;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00011FBC File Offset: 0x000101BC
		protected void UpdatePrevData()
		{
			if (this.m_Content == null)
			{
				this.m_PrevPosition = Vector2.zero;
			}
			else
			{
				this.m_PrevPosition = this.m_Content.anchoredPosition;
			}
			this.m_PrevViewBounds = this.m_ViewBounds;
			this.m_PrevContentBounds = this.m_ContentBounds;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00012010 File Offset: 0x00010210
		private void UpdateScrollbars(Vector2 offset)
		{
			if (this.m_HorizontalScrollbar)
			{
				if (this.m_ContentBounds.size.x > 0f)
				{
					this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
				}
				else
				{
					this.m_HorizontalScrollbar.size = 1f;
				}
				this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
			}
			if (this.m_VerticalScrollbar)
			{
				if (this.m_ContentBounds.size.y > 0f)
				{
					this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
				}
				else
				{
					this.m_VerticalScrollbar.size = 1f;
				}
				this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00012125 File Offset: 0x00010325
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00012138 File Offset: 0x00010338
		public Vector2 normalizedPosition
		{
			get
			{
				return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
			}
			set
			{
				this.SetNormalizedPosition(value.x, 0);
				this.SetNormalizedPosition(value.y, 1);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00012154 File Offset: 0x00010354
		// (set) Token: 0x06000395 RID: 917 RVA: 0x0001221B File Offset: 0x0001041B
		public float horizontalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x || Mathf.Approximately(this.m_ContentBounds.size.x, this.m_ViewBounds.size.x))
				{
					return (float)((this.m_ViewBounds.min.x > this.m_ContentBounds.min.x) ? 1 : 0);
				}
				return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
			}
			set
			{
				this.SetNormalizedPosition(value, 0);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00012228 File Offset: 0x00010428
		// (set) Token: 0x06000397 RID: 919 RVA: 0x000122EF File Offset: 0x000104EF
		public float verticalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y || Mathf.Approximately(this.m_ContentBounds.size.y, this.m_ViewBounds.size.y))
				{
					return (float)((this.m_ViewBounds.min.y > this.m_ContentBounds.min.y) ? 1 : 0);
				}
				return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
			}
			set
			{
				this.SetNormalizedPosition(value, 1);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000122F9 File Offset: 0x000104F9
		private void SetHorizontalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 0);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00012303 File Offset: 0x00010503
		private void SetVerticalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 1);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00012310 File Offset: 0x00010510
		protected virtual void SetNormalizedPosition(float value, int axis)
		{
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float num = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
			float num2 = this.m_ViewBounds.min[axis] - value * num;
			float num3 = this.m_Content.anchoredPosition[axis] + num2 - this.m_ContentBounds.min[axis];
			Vector3 v = this.m_Content.anchoredPosition;
			if (Mathf.Abs(v[axis] - num3) > 0.01f)
			{
				v[axis] = num3;
				this.m_Content.anchoredPosition = v;
				this.m_Velocity[axis] = 0f;
				this.UpdateBounds();
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000123F5 File Offset: 0x000105F5
		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00012420 File Offset: 0x00010620
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00012428 File Offset: 0x00010628
		private bool hScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.x > this.m_ViewBounds.size.x + 0.01f;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0001245B File Offset: 0x0001065B
		private bool vScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.y > this.m_ViewBounds.size.y + 0.01f;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001248E File Offset: 0x0001068E
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012490 File Offset: 0x00010690
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00012492 File Offset: 0x00010692
		public virtual float minWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00012499 File Offset: 0x00010699
		public virtual float preferredWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000124A0 File Offset: 0x000106A0
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000124A7 File Offset: 0x000106A7
		public virtual float minHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000124AE File Offset: 0x000106AE
		public virtual float preferredHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000124B5 File Offset: 0x000106B5
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x000124BC File Offset: 0x000106BC
		public virtual int layoutPriority
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000124C0 File Offset: 0x000106C0
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.UpdateCachedData();
			if (this.m_HSliderExpand || this.m_VSliderExpand)
			{
				this.m_Tracker.Add(this, this.viewRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.viewRect.anchorMin = Vector2.zero;
				this.viewRect.anchorMax = Vector2.one;
				this.viewRect.sizeDelta = Vector2.zero;
				this.viewRect.anchoredPosition = Vector2.zero;
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_HSliderExpand && this.hScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(this.viewRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded && this.viewRect.sizeDelta.x == 0f && this.viewRect.sizeDelta.y < 0f)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00012724 File Offset: 0x00010924
		public virtual void SetLayoutVertical()
		{
			this.UpdateScrollbarLayout();
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001277E File Offset: 0x0001097E
		private void UpdateScrollbarVisibility()
		{
			ScrollRect.UpdateOneScrollbarVisibility(this.vScrollingNeeded, this.m_Vertical, this.m_VerticalScrollbarVisibility, this.m_VerticalScrollbar);
			ScrollRect.UpdateOneScrollbarVisibility(this.hScrollingNeeded, this.m_Horizontal, this.m_HorizontalScrollbarVisibility, this.m_HorizontalScrollbar);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000127BC File Offset: 0x000109BC
		private static void UpdateOneScrollbarVisibility(bool xScrollingNeeded, bool xAxisEnabled, ScrollRect.ScrollbarVisibility scrollbarVisibility, Scrollbar scrollbar)
		{
			if (scrollbar)
			{
				if (scrollbarVisibility == ScrollRect.ScrollbarVisibility.Permanent)
				{
					if (scrollbar.gameObject.activeSelf != xAxisEnabled)
					{
						scrollbar.gameObject.SetActive(xAxisEnabled);
						return;
					}
				}
				else if (scrollbar.gameObject.activeSelf != xScrollingNeeded)
				{
					scrollbar.gameObject.SetActive(xScrollingNeeded);
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001280C File Offset: 0x00010A0C
		private void UpdateScrollbarLayout()
		{
			if (this.m_VSliderExpand && this.m_HorizontalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_HorizontalScrollbarRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.SizeDeltaX);
				this.m_HorizontalScrollbarRect.anchorMin = new Vector2(0f, this.m_HorizontalScrollbarRect.anchorMin.y);
				this.m_HorizontalScrollbarRect.anchorMax = new Vector2(1f, this.m_HorizontalScrollbarRect.anchorMax.y);
				this.m_HorizontalScrollbarRect.anchoredPosition = new Vector2(0f, this.m_HorizontalScrollbarRect.anchoredPosition.y);
				if (this.vScrollingNeeded)
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
				else
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(0f, this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
			}
			if (this.m_HSliderExpand && this.m_VerticalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_VerticalScrollbarRect, DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaY);
				this.m_VerticalScrollbarRect.anchorMin = new Vector2(this.m_VerticalScrollbarRect.anchorMin.x, 0f);
				this.m_VerticalScrollbarRect.anchorMax = new Vector2(this.m_VerticalScrollbarRect.anchorMax.x, 1f);
				this.m_VerticalScrollbarRect.anchoredPosition = new Vector2(this.m_VerticalScrollbarRect.anchoredPosition.x, 0f);
				if (this.hScrollingNeeded)
				{
					this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
					return;
				}
				this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, 0f);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00012A14 File Offset: 0x00010C14
		protected void UpdateBounds()
		{
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
			if (this.m_Content == null)
			{
				return;
			}
			Vector3 size = this.m_ContentBounds.size;
			Vector3 center = this.m_ContentBounds.center;
			Vector2 pivot = this.m_Content.pivot;
			ScrollRect.AdjustBounds(ref this.m_ViewBounds, ref pivot, ref size, ref center);
			this.m_ContentBounds.size = size;
			this.m_ContentBounds.center = center;
			if (this.movementType == ScrollRect.MovementType.Clamped)
			{
				Vector2 zero = Vector2.zero;
				if (this.m_ViewBounds.max.x > this.m_ContentBounds.max.x)
				{
					zero.x = Math.Min(this.m_ViewBounds.min.x - this.m_ContentBounds.min.x, this.m_ViewBounds.max.x - this.m_ContentBounds.max.x);
				}
				else if (this.m_ViewBounds.min.x < this.m_ContentBounds.min.x)
				{
					zero.x = Math.Max(this.m_ViewBounds.min.x - this.m_ContentBounds.min.x, this.m_ViewBounds.max.x - this.m_ContentBounds.max.x);
				}
				if (this.m_ViewBounds.min.y < this.m_ContentBounds.min.y)
				{
					zero.y = Math.Max(this.m_ViewBounds.min.y - this.m_ContentBounds.min.y, this.m_ViewBounds.max.y - this.m_ContentBounds.max.y);
				}
				else if (this.m_ViewBounds.max.y > this.m_ContentBounds.max.y)
				{
					zero.y = Math.Min(this.m_ViewBounds.min.y - this.m_ContentBounds.min.y, this.m_ViewBounds.max.y - this.m_ContentBounds.max.y);
				}
				if (zero.sqrMagnitude > 1E-45f)
				{
					center = this.m_Content.anchoredPosition + zero;
					if (!this.m_Horizontal)
					{
						center.x = this.m_Content.anchoredPosition.x;
					}
					if (!this.m_Vertical)
					{
						center.y = this.m_Content.anchoredPosition.y;
					}
					ScrollRect.AdjustBounds(ref this.m_ViewBounds, ref pivot, ref size, ref center);
				}
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012D10 File Offset: 0x00010F10
		internal static void AdjustBounds(ref Bounds viewBounds, ref Vector2 contentPivot, ref Vector3 contentSize, ref Vector3 contentPos)
		{
			Vector3 vector = viewBounds.size - contentSize;
			if (vector.x > 0f)
			{
				contentPos.x -= vector.x * (contentPivot.x - 0.5f);
				contentSize.x = viewBounds.size.x;
			}
			if (vector.y > 0f)
			{
				contentPos.y -= vector.y * (contentPivot.y - 0.5f);
				contentSize.y = viewBounds.size.y;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012DA8 File Offset: 0x00010FA8
		private Bounds GetBounds()
		{
			if (this.m_Content == null)
			{
				return default(Bounds);
			}
			this.m_Content.GetWorldCorners(this.m_Corners);
			Matrix4x4 worldToLocalMatrix = this.viewRect.worldToLocalMatrix;
			return ScrollRect.InternalGetBounds(this.m_Corners, ref worldToLocalMatrix);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00012DF8 File Offset: 0x00010FF8
		internal static Bounds InternalGetBounds(Vector3[] corners, ref Matrix4x4 viewWorldToLocalMatrix)
		{
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < 4; i++)
			{
				Vector3 lhs = viewWorldToLocalMatrix.MultiplyPoint3x4(corners[i]);
				vector = Vector3.Min(lhs, vector);
				vector2 = Vector3.Max(lhs, vector2);
			}
			Bounds result = new Bounds(vector, Vector3.zero);
			result.Encapsulate(vector2);
			return result;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00012E6F File Offset: 0x0001106F
		private Vector2 CalculateOffset(Vector2 delta)
		{
			return ScrollRect.InternalCalculateOffset(ref this.m_ViewBounds, ref this.m_ContentBounds, this.m_Horizontal, this.m_Vertical, this.m_MovementType, ref delta);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00012E98 File Offset: 0x00011098
		internal static Vector2 InternalCalculateOffset(ref Bounds viewBounds, ref Bounds contentBounds, bool horizontal, bool vertical, ScrollRect.MovementType movementType, ref Vector2 delta)
		{
			Vector2 zero = Vector2.zero;
			if (movementType == ScrollRect.MovementType.Unrestricted)
			{
				return zero;
			}
			Vector2 vector = contentBounds.min;
			Vector2 vector2 = contentBounds.max;
			if (horizontal)
			{
				vector.x += delta.x;
				vector2.x += delta.x;
				float num = viewBounds.max.x - vector2.x;
				float num2 = viewBounds.min.x - vector.x;
				if (num2 < -0.001f)
				{
					zero.x = num2;
				}
				else if (num > 0.001f)
				{
					zero.x = num;
				}
			}
			if (vertical)
			{
				vector.y += delta.y;
				vector2.y += delta.y;
				float num3 = viewBounds.max.y - vector2.y;
				float num4 = viewBounds.min.y - vector.y;
				if (num3 > 0.001f)
				{
					zero.y = num3;
				}
				else if (num4 < -0.001f)
				{
					zero.y = num4;
				}
			}
			return zero;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012FB1 File Offset: 0x000111B1
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00012FC7 File Offset: 0x000111C7
		protected void SetDirtyCaching()
		{
			if (!this.IsActive())
			{
				return;
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			this.m_ViewRect = null;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00012FEA File Offset: 0x000111EA
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000125 RID: 293
		[SerializeField]
		private RectTransform m_Content;

		// Token: 0x04000126 RID: 294
		[SerializeField]
		private bool m_Horizontal = true;

		// Token: 0x04000127 RID: 295
		[SerializeField]
		private bool m_Vertical = true;

		// Token: 0x04000128 RID: 296
		[SerializeField]
		private ScrollRect.MovementType m_MovementType = ScrollRect.MovementType.Elastic;

		// Token: 0x04000129 RID: 297
		[SerializeField]
		private float m_Elasticity = 0.1f;

		// Token: 0x0400012A RID: 298
		[SerializeField]
		private bool m_Inertia = true;

		// Token: 0x0400012B RID: 299
		[SerializeField]
		private float m_DecelerationRate = 0.135f;

		// Token: 0x0400012C RID: 300
		[SerializeField]
		private float m_ScrollSensitivity = 1f;

		// Token: 0x0400012D RID: 301
		[SerializeField]
		private RectTransform m_Viewport;

		// Token: 0x0400012E RID: 302
		[SerializeField]
		private Scrollbar m_HorizontalScrollbar;

		// Token: 0x0400012F RID: 303
		[SerializeField]
		private Scrollbar m_VerticalScrollbar;

		// Token: 0x04000130 RID: 304
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_HorizontalScrollbarVisibility;

		// Token: 0x04000131 RID: 305
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_VerticalScrollbarVisibility;

		// Token: 0x04000132 RID: 306
		[SerializeField]
		private float m_HorizontalScrollbarSpacing;

		// Token: 0x04000133 RID: 307
		[SerializeField]
		private float m_VerticalScrollbarSpacing;

		// Token: 0x04000134 RID: 308
		[SerializeField]
		private ScrollRect.ScrollRectEvent m_OnValueChanged = new ScrollRect.ScrollRectEvent();

		// Token: 0x04000135 RID: 309
		private Vector2 m_PointerStartLocalCursor = Vector2.zero;

		// Token: 0x04000136 RID: 310
		protected Vector2 m_ContentStartPosition = Vector2.zero;

		// Token: 0x04000137 RID: 311
		private RectTransform m_ViewRect;

		// Token: 0x04000138 RID: 312
		protected Bounds m_ContentBounds;

		// Token: 0x04000139 RID: 313
		private Bounds m_ViewBounds;

		// Token: 0x0400013A RID: 314
		private Vector2 m_Velocity;

		// Token: 0x0400013B RID: 315
		private bool m_Dragging;

		// Token: 0x0400013C RID: 316
		private bool m_Scrolling;

		// Token: 0x0400013D RID: 317
		private Vector2 m_PrevPosition = Vector2.zero;

		// Token: 0x0400013E RID: 318
		private Bounds m_PrevContentBounds;

		// Token: 0x0400013F RID: 319
		private Bounds m_PrevViewBounds;

		// Token: 0x04000140 RID: 320
		[NonSerialized]
		private bool m_HasRebuiltLayout;

		// Token: 0x04000141 RID: 321
		private bool m_HSliderExpand;

		// Token: 0x04000142 RID: 322
		private bool m_VSliderExpand;

		// Token: 0x04000143 RID: 323
		private float m_HSliderHeight;

		// Token: 0x04000144 RID: 324
		private float m_VSliderWidth;

		// Token: 0x04000145 RID: 325
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000146 RID: 326
		private RectTransform m_HorizontalScrollbarRect;

		// Token: 0x04000147 RID: 327
		private RectTransform m_VerticalScrollbarRect;

		// Token: 0x04000148 RID: 328
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000149 RID: 329
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x020000A6 RID: 166
		public enum MovementType
		{
			// Token: 0x040002EE RID: 750
			Unrestricted,
			// Token: 0x040002EF RID: 751
			Elastic,
			// Token: 0x040002F0 RID: 752
			Clamped
		}

		// Token: 0x020000A7 RID: 167
		public enum ScrollbarVisibility
		{
			// Token: 0x040002F2 RID: 754
			Permanent,
			// Token: 0x040002F3 RID: 755
			AutoHide,
			// Token: 0x040002F4 RID: 756
			AutoHideAndExpandViewport
		}

		// Token: 0x020000A8 RID: 168
		[Serializable]
		public class ScrollRectEvent : UnityEvent<Vector2>
		{
			// Token: 0x060006EC RID: 1772 RVA: 0x0001C25D File Offset: 0x0001A45D
			public ScrollRectEvent()
			{
			}
		}
	}
}
