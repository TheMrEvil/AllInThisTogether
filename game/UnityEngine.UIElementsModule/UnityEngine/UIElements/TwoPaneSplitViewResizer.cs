using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019C RID: 412
	internal class TwoPaneSplitViewResizer : PointerManipulator
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00038554 File Offset: 0x00036754
		private VisualElement fixedPane
		{
			get
			{
				return this.m_SplitView.fixedPane;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x00038561 File Offset: 0x00036761
		private VisualElement flexedPane
		{
			get
			{
				return this.m_SplitView.flexedPane;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00038570 File Offset: 0x00036770
		private float fixedPaneMinDimension
		{
			get
			{
				bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				float value;
				if (flag)
				{
					value = this.fixedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					value = this.fixedPane.resolvedStyle.minHeight.value;
				}
				return value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x000385C4 File Offset: 0x000367C4
		private float flexedPaneMinDimension
		{
			get
			{
				bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				float value;
				if (flag)
				{
					value = this.flexedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					value = this.flexedPane.resolvedStyle.minHeight.value;
				}
				return value;
			}
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00038618 File Offset: 0x00036818
		public TwoPaneSplitViewResizer(TwoPaneSplitView splitView, int dir, TwoPaneSplitViewOrientation orientation)
		{
			this.m_Orientation = orientation;
			this.m_SplitView = splitView;
			this.m_Direction = dir;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.m_Active = false;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00038668 File Offset: 0x00036868
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000386C4 File Offset: 0x000368C4
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00038720 File Offset: 0x00036920
		public void ApplyDelta(float delta)
		{
			float num = (this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.fixedPane.resolvedStyle.width : this.fixedPane.resolvedStyle.height;
			float num2 = num + delta;
			bool flag = num2 < num && num2 < this.fixedPaneMinDimension;
			if (flag)
			{
				num2 = this.fixedPaneMinDimension;
			}
			float num3 = (this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.m_SplitView.resolvedStyle.width : this.m_SplitView.resolvedStyle.height;
			num3 -= this.flexedPaneMinDimension;
			bool flag2 = num2 > num && num2 > num3;
			if (flag2)
			{
				num2 = num3;
			}
			bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag3)
			{
				this.fixedPane.style.width = num2;
				bool flag4 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag4)
				{
					base.target.style.left = num2;
				}
				else
				{
					base.target.style.left = this.m_SplitView.resolvedStyle.width - num2;
				}
			}
			else
			{
				this.fixedPane.style.height = num2;
				bool flag5 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag5)
				{
					base.target.style.top = num2;
				}
				else
				{
					base.target.style.top = this.m_SplitView.resolvedStyle.height - num2;
				}
			}
			this.m_SplitView.fixedPaneDimension = num2;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000388BC File Offset: 0x00036ABC
		protected void OnPointerDown(PointerDownEvent e)
		{
			bool active = this.m_Active;
			if (active)
			{
				e.StopImmediatePropagation();
			}
			else
			{
				bool flag = base.CanStartManipulation(e);
				if (flag)
				{
					this.m_Start = e.localPosition;
					this.m_Active = true;
					base.target.CapturePointer(e.pointerId);
					e.StopPropagation();
				}
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00038918 File Offset: 0x00036B18
		protected void OnPointerMove(PointerMoveEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId);
			if (!flag)
			{
				Vector2 vector = e.localPosition - this.m_Start;
				float num = vector.x;
				bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Vertical;
				if (flag2)
				{
					num = vector.y;
				}
				float delta = (float)this.m_Direction * num;
				this.ApplyDelta(delta);
				e.StopPropagation();
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00038998 File Offset: 0x00036B98
		protected void OnPointerUp(PointerUpEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId) || !base.CanStopManipulation(e);
			if (!flag)
			{
				this.m_Active = false;
				base.target.ReleasePointer(e.pointerId);
				e.StopPropagation();
			}
		}

		// Token: 0x0400064E RID: 1614
		private Vector3 m_Start;

		// Token: 0x0400064F RID: 1615
		protected bool m_Active;

		// Token: 0x04000650 RID: 1616
		private TwoPaneSplitView m_SplitView;

		// Token: 0x04000651 RID: 1617
		private int m_Direction;

		// Token: 0x04000652 RID: 1618
		private TwoPaneSplitViewOrientation m_Orientation;
	}
}
