using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000F RID: 15
	internal class ClampedDragger<T> : Clickable where T : IComparable<T>
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00002BCC File Offset: 0x00000DCC
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x00002C04 File Offset: 0x00000E04
		public event Action dragging
		{
			[CompilerGenerated]
			add
			{
				Action action = this.dragging;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.dragging, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.dragging;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.dragging, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000046 RID: 70 RVA: 0x00002C3C File Offset: 0x00000E3C
		// (remove) Token: 0x06000047 RID: 71 RVA: 0x00002C74 File Offset: 0x00000E74
		public event Action draggingEnded
		{
			[CompilerGenerated]
			add
			{
				Action action = this.draggingEnded;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.draggingEnded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.draggingEnded;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.draggingEnded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002CA9 File Offset: 0x00000EA9
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public ClampedDragger<T>.DragDirection dragDirection
		{
			[CompilerGenerated]
			get
			{
				return this.<dragDirection>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dragDirection>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002CBA File Offset: 0x00000EBA
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002CC2 File Offset: 0x00000EC2
		private BaseSlider<T> slider
		{
			[CompilerGenerated]
			get
			{
				return this.<slider>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<slider>k__BackingField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002CCB File Offset: 0x00000ECB
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002CD3 File Offset: 0x00000ED3
		public Vector2 startMousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<startMousePosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<startMousePosition>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002CDC File Offset: 0x00000EDC
		public Vector2 delta
		{
			get
			{
				return base.lastMousePosition - this.startMousePosition;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CEF File Offset: 0x00000EEF
		public ClampedDragger(BaseSlider<T> slider, Action clickHandler, Action dragHandler) : base(clickHandler, 250L, 30L)
		{
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			this.slider = slider;
			this.dragging += dragHandler;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D1B File Offset: 0x00000F1B
		protected override void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.startMousePosition = localPosition;
			this.dragDirection = ClampedDragger<T>.DragDirection.None;
			base.ProcessDownEvent(evt, localPosition, pointerId);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D38 File Offset: 0x00000F38
		protected override void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			base.ProcessUpEvent(evt, localPosition, pointerId);
			Action action = this.draggingEnded;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D58 File Offset: 0x00000F58
		protected override void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			base.ProcessMoveEvent(evt, localPosition);
			bool flag = this.dragDirection == ClampedDragger<T>.DragDirection.None;
			if (flag)
			{
				this.dragDirection = ClampedDragger<T>.DragDirection.Free;
			}
			bool flag2 = this.dragDirection == ClampedDragger<T>.DragDirection.Free;
			if (flag2)
			{
				bool flag3 = evt.eventTypeId == EventBase<PointerMoveEvent>.TypeId();
				if (flag3)
				{
					PointerMoveEvent pointerMoveEvent = (PointerMoveEvent)evt;
					bool flag4 = pointerMoveEvent.pointerId != PointerId.mousePointerId;
					if (flag4)
					{
						pointerMoveEvent.isHandledByDraggable = true;
					}
				}
				Action action = this.dragging;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x0400001D RID: 29
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action dragging;

		// Token: 0x0400001E RID: 30
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action draggingEnded;

		// Token: 0x0400001F RID: 31
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ClampedDragger<T>.DragDirection <dragDirection>k__BackingField;

		// Token: 0x04000020 RID: 32
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private BaseSlider<T> <slider>k__BackingField;

		// Token: 0x04000021 RID: 33
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <startMousePosition>k__BackingField;

		// Token: 0x02000010 RID: 16
		[Flags]
		public enum DragDirection
		{
			// Token: 0x04000023 RID: 35
			None = 0,
			// Token: 0x04000024 RID: 36
			LowToHigh = 1,
			// Token: 0x04000025 RID: 37
			HighToLow = 2,
			// Token: 0x04000026 RID: 38
			Free = 4
		}
	}
}
