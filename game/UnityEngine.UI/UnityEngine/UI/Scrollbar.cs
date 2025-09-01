using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000032 RID: 50
	[AddComponentMenu("UI/Scrollbar", 36)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class Scrollbar : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00010B2B File Offset: 0x0000ED2B
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00010B33 File Offset: 0x0000ED33
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00010B4F File Offset: 0x0000ED4F
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00010B57 File Offset: 0x0000ED57
		public Scrollbar.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Scrollbar.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010B6D File Offset: 0x0000ED6D
		protected Scrollbar()
		{
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00010B98 File Offset: 0x0000ED98
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00010BD1 File Offset: 0x0000EDD1
		public float value
		{
			get
			{
				float num = this.m_Value;
				if (this.m_NumberOfSteps > 1)
				{
					num = Mathf.Round(num * (float)(this.m_NumberOfSteps - 1)) / (float)(this.m_NumberOfSteps - 1);
				}
				return num;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00010BDB File Offset: 0x0000EDDB
		public virtual void SetValueWithoutNotify(float input)
		{
			this.Set(input, false);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00010BE5 File Offset: 0x0000EDE5
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00010BED File Offset: 0x0000EDED
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_Size, Mathf.Clamp01(value)))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00010C08 File Offset: 0x0000EE08
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00010C10 File Offset: 0x0000EE10
		public int numberOfSteps
		{
			get
			{
				return this.m_NumberOfSteps;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_NumberOfSteps, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00010C33 File Offset: 0x0000EE33
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00010C3B File Offset: 0x0000EE3B
		public Scrollbar.ScrollEvent onValueChanged
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00010C44 File Offset: 0x0000EE44
		private float stepSize
		{
			get
			{
				if (this.m_NumberOfSteps <= 1)
				{
					return 0.1f;
				}
				return 1f / (float)(this.m_NumberOfSteps - 1);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010C64 File Offset: 0x0000EE64
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010C66 File Offset: 0x0000EE66
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010C68 File Offset: 0x0000EE68
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010C6A File Offset: 0x0000EE6A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010C8B File Offset: 0x0000EE8B
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00010C9E File Offset: 0x0000EE9E
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.UpdateVisuals();
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010CB5 File Offset: 0x0000EEB5
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect && this.m_HandleRect.parent != null)
			{
				this.m_ContainerRect = this.m_HandleRect.parent.GetComponent<RectTransform>();
				return;
			}
			this.m_ContainerRect = null;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010CF5 File Offset: 0x0000EEF5
		private void Set(float input, bool sendCallback = true)
		{
			float value = this.m_Value;
			this.m_Value = input;
			if (value == this.value)
			{
				return;
			}
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Scrollbar.value", this);
				this.m_OnValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010D32 File Offset: 0x0000EF32
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00010D49 File Offset: 0x0000EF49
		private Scrollbar.Axis axis
		{
			get
			{
				if (this.m_Direction != Scrollbar.Direction.LeftToRight && this.m_Direction != Scrollbar.Direction.RightToLeft)
				{
					return Scrollbar.Axis.Vertical;
				}
				return Scrollbar.Axis.Horizontal;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00010D5F File Offset: 0x0000EF5F
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Scrollbar.Direction.RightToLeft || this.m_Direction == Scrollbar.Direction.TopToBottom;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010D78 File Offset: 0x0000EF78
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_ContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				float num = Mathf.Clamp01(this.value) * (1f - this.size);
				if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - num - this.size;
					one[(int)this.axis] = 1f - num;
				}
				else
				{
					zero[(int)this.axis] = num;
					one[(int)this.axis] = num + this.size;
				}
				this.m_HandleRect.anchorMin = zero;
				this.m_HandleRect.anchorMax = one;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010E54 File Offset: 0x0000F054
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			Vector2 zero = Vector2.zero;
			if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_ContainerRect, zero, eventData.pressEventCamera, out a))
			{
				return;
			}
			Vector2 handleCorner = a - this.m_Offset - this.m_ContainerRect.rect.position - (this.m_HandleRect.rect.size - this.m_HandleRect.sizeDelta) * 0.5f;
			float num = ((this.axis == Scrollbar.Axis.Horizontal) ? this.m_ContainerRect.rect.width : this.m_ContainerRect.rect.height) * (1f - this.size);
			if (num <= 0f)
			{
				return;
			}
			this.DoUpdateDrag(handleCorner, num);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010F4C File Offset: 0x0000F14C
		private void DoUpdateDrag(Vector2 handleCorner, float remainingSize)
		{
			switch (this.m_Direction)
			{
			case Scrollbar.Direction.LeftToRight:
				this.Set(Mathf.Clamp01(handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.RightToLeft:
				this.Set(Mathf.Clamp01(1f - handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.BottomToTop:
				this.Set(Mathf.Clamp01(handleCorner.y / remainingSize), true);
				return;
			case Scrollbar.Direction.TopToBottom:
				this.Set(Mathf.Clamp01(1f - handleCorner.y / remainingSize), true);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010FD6 File Offset: 0x0000F1D6
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isPointerDownAndNotDragging = false;
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			this.m_Offset = Vector2.zero;
			Vector2 a;
			if (RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out a))
			{
				this.m_Offset = a - this.m_HandleRect.rect.center;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00011089 File Offset: 0x0000F289
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect != null)
			{
				this.UpdateDrag(eventData);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000110AA File Offset: 0x0000F2AA
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.isPointerDownAndNotDragging = true;
			this.m_PointerDownRepeat = base.StartCoroutine(this.ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000110E7 File Offset: 0x0000F2E7
		protected IEnumerator ClickRepeat(PointerEventData eventData)
		{
			return this.ClickRepeat(eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00011100 File Offset: 0x0000F300
		protected IEnumerator ClickRepeat(Vector2 screenPosition, Camera camera)
		{
			while (this.isPointerDownAndNotDragging)
			{
				Vector2 vector;
				if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, screenPosition, camera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, screenPosition, camera, out vector))
				{
					float num = (((this.axis == Scrollbar.Axis.Horizontal) ? vector.x : vector.y) < 0f) ? this.size : (-this.size);
					this.value += (this.reverseValue ? num : (-num));
					this.value = Mathf.Clamp01(this.value);
					this.value = Mathf.Round(this.value * 10000f) / 10000f;
				}
				yield return new WaitForEndOfFrame();
			}
			base.StopCoroutine(this.m_PointerDownRepeat);
			yield break;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001111D File Offset: 0x0000F31D
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.isPointerDownAndNotDragging = false;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00011130 File Offset: 0x0000F330
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Up:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Right:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Down:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000112E0 File Offset: 0x0000F4E0
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00011310 File Offset: 0x0000F510
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011340 File Offset: 0x0000F540
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001136F File Offset: 0x0000F56F
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00011378 File Offset: 0x0000F578
		public void SetDirection(Scrollbar.Direction direction, bool includeRectLayouts)
		{
			Scrollbar.Axis axis = this.axis;
			bool reverseValue = this.reverseValue;
			this.direction = direction;
			if (!includeRectLayouts)
			{
				return;
			}
			if (this.axis != axis)
			{
				RectTransformUtility.FlipLayoutAxes(base.transform as RectTransform, true, true);
			}
			if (this.reverseValue != reverseValue)
			{
				RectTransformUtility.FlipLayoutOnAxis(base.transform as RectTransform, (int)this.axis, true, true);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000113DA File Offset: 0x0000F5DA
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000119 RID: 281
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x0400011A RID: 282
		[SerializeField]
		private Scrollbar.Direction m_Direction;

		// Token: 0x0400011B RID: 283
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Value;

		// Token: 0x0400011C RID: 284
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Size = 0.2f;

		// Token: 0x0400011D RID: 285
		[Range(0f, 11f)]
		[SerializeField]
		private int m_NumberOfSteps;

		// Token: 0x0400011E RID: 286
		[Space(6f)]
		[SerializeField]
		private Scrollbar.ScrollEvent m_OnValueChanged = new Scrollbar.ScrollEvent();

		// Token: 0x0400011F RID: 287
		private RectTransform m_ContainerRect;

		// Token: 0x04000120 RID: 288
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x04000121 RID: 289
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000122 RID: 290
		private Coroutine m_PointerDownRepeat;

		// Token: 0x04000123 RID: 291
		private bool isPointerDownAndNotDragging;

		// Token: 0x04000124 RID: 292
		private bool m_DelayedUpdateVisuals;

		// Token: 0x020000A2 RID: 162
		public enum Direction
		{
			// Token: 0x040002E1 RID: 737
			LeftToRight,
			// Token: 0x040002E2 RID: 738
			RightToLeft,
			// Token: 0x040002E3 RID: 739
			BottomToTop,
			// Token: 0x040002E4 RID: 740
			TopToBottom
		}

		// Token: 0x020000A3 RID: 163
		[Serializable]
		public class ScrollEvent : UnityEvent<float>
		{
			// Token: 0x060006E5 RID: 1765 RVA: 0x0001C11B File Offset: 0x0001A31B
			public ScrollEvent()
			{
			}
		}

		// Token: 0x020000A4 RID: 164
		private enum Axis
		{
			// Token: 0x040002E6 RID: 742
			Horizontal,
			// Token: 0x040002E7 RID: 743
			Vertical
		}

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		private sealed class <ClickRepeat>d__58 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060006E6 RID: 1766 RVA: 0x0001C123 File Offset: 0x0001A323
			[DebuggerHidden]
			public <ClickRepeat>d__58(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x0001C132 File Offset: 0x0001A332
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060006E8 RID: 1768 RVA: 0x0001C134 File Offset: 0x0001A334
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				Scrollbar scrollbar = this;
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
				if (!scrollbar.isPointerDownAndNotDragging)
				{
					scrollbar.StopCoroutine(scrollbar.m_PointerDownRepeat);
					return false;
				}
				Vector2 vector;
				if (!RectTransformUtility.RectangleContainsScreenPoint(scrollbar.m_HandleRect, screenPosition, camera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(scrollbar.m_HandleRect, screenPosition, camera, out vector))
				{
					float num2 = (((scrollbar.axis == Scrollbar.Axis.Horizontal) ? vector.x : vector.y) < 0f) ? scrollbar.size : (-scrollbar.size);
					scrollbar.value += (scrollbar.reverseValue ? num2 : (-num2));
					scrollbar.value = Mathf.Clamp01(scrollbar.value);
					scrollbar.value = Mathf.Round(scrollbar.value * 10000f) / 10000f;
				}
				this.<>2__current = new WaitForEndOfFrame();
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001D6 RID: 470
			// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0001C246 File Offset: 0x0001A446
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x0001C24E File Offset: 0x0001A44E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001C255 File Offset: 0x0001A455
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040002E8 RID: 744
			private int <>1__state;

			// Token: 0x040002E9 RID: 745
			private object <>2__current;

			// Token: 0x040002EA RID: 746
			public Scrollbar <>4__this;

			// Token: 0x040002EB RID: 747
			public Vector2 screenPosition;

			// Token: 0x040002EC RID: 748
			public Camera camera;
		}
	}
}
