using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x02000011 RID: 17
	public class Clickable : PointerManipulator
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000053 RID: 83 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (remove) Token: 0x06000054 RID: 84 RVA: 0x00002E18 File Offset: 0x00001018
		public event Action<EventBase> clickedWithEventInfo
		{
			[CompilerGenerated]
			add
			{
				Action<EventBase> action = this.clickedWithEventInfo;
				Action<EventBase> action2;
				do
				{
					action2 = action;
					Action<EventBase> value2 = (Action<EventBase>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<EventBase>>(ref this.clickedWithEventInfo, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<EventBase> action = this.clickedWithEventInfo;
				Action<EventBase> action2;
				do
				{
					action2 = action;
					Action<EventBase> value2 = (Action<EventBase>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<EventBase>>(ref this.clickedWithEventInfo, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000055 RID: 85 RVA: 0x00002E50 File Offset: 0x00001050
		// (remove) Token: 0x06000056 RID: 86 RVA: 0x00002E88 File Offset: 0x00001088
		public event Action clicked
		{
			[CompilerGenerated]
			add
			{
				Action action = this.clicked;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.clicked, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.clicked;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.clicked, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002EBD File Offset: 0x000010BD
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002EC5 File Offset: 0x000010C5
		protected bool active
		{
			[CompilerGenerated]
			get
			{
				return this.<active>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<active>k__BackingField = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002ECE File Offset: 0x000010CE
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002ED6 File Offset: 0x000010D6
		public Vector2 lastMousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<lastMousePosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<lastMousePosition>k__BackingField = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002EDF File Offset: 0x000010DF
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002EE8 File Offset: 0x000010E8
		internal bool acceptClicksIfDisabled
		{
			get
			{
				return this.m_AcceptClicksIfDisabled;
			}
			set
			{
				bool flag = this.m_AcceptClicksIfDisabled == value;
				if (!flag)
				{
					this.UnregisterCallbacksFromTarget();
					this.m_AcceptClicksIfDisabled = value;
					this.RegisterCallbacksOnTarget();
				}
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002F1A File Offset: 0x0000111A
		private InvokePolicy invokePolicy
		{
			get
			{
				return this.acceptClicksIfDisabled ? InvokePolicy.IncludeDisabled : InvokePolicy.Default;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F28 File Offset: 0x00001128
		public Clickable(Action handler, long delay, long interval) : this(handler)
		{
			this.m_Delay = delay;
			this.m_Interval = interval;
			this.active = false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F4C File Offset: 0x0000114C
		public Clickable(Action<EventBase> handler)
		{
			this.m_ActivePointerId = -1;
			base..ctor();
			this.clickedWithEventInfo = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F90 File Offset: 0x00001190
		public Clickable(Action handler)
		{
			this.m_ActivePointerId = -1;
			base..ctor();
			this.clicked = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.active = false;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002FDC File Offset: 0x000011DC
		private void OnTimer(TimerState timerState)
		{
			bool flag = (this.clicked != null || this.clickedWithEventInfo != null) && this.IsRepeatable();
			if (flag)
			{
				bool flag2 = this.ContainsPointer(this.m_ActivePointerId);
				if (flag2)
				{
					this.Invoke(null);
					base.target.pseudoStates |= PseudoStates.Active;
				}
				else
				{
					base.target.pseudoStates &= ~PseudoStates.Active;
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003050 File Offset: 0x00001250
		private bool IsRepeatable()
		{
			return this.m_Delay > 0L || this.m_Interval > 0L;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000307C File Offset: 0x0000127C
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseCaptureOutEvent>(new EventCallback<MouseCaptureOutEvent>(this.OnMouseCaptureOut), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), this.invokePolicy, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), InvokePolicy.IncludeDisabled, TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003188 File Offset: 0x00001388
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseCaptureOutEvent>(new EventCallback<MouseCaptureOutEvent>(this.OnMouseCaptureOut), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCancelEvent>(new EventCallback<PointerCancelEvent>(this.OnPointerCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerCaptureOutEvent>(new EventCallback<PointerCaptureOutEvent>(this.OnPointerCaptureOut), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003278 File Offset: 0x00001478
		protected void OnMouseDown(MouseDownEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				this.ProcessDownEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000032A4 File Offset: 0x000014A4
		protected void OnMouseMove(MouseMoveEvent evt)
		{
			bool active = this.active;
			if (active)
			{
				this.ProcessMoveEvent(evt, evt.localMousePosition);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000032CC File Offset: 0x000014CC
		protected void OnMouseUp(MouseUpEvent evt)
		{
			bool flag = this.active && base.CanStopManipulation(evt);
			if (flag)
			{
				this.ProcessUpEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003304 File Offset: 0x00001504
		private void OnMouseCaptureOut(MouseCaptureOutEvent evt)
		{
			bool active = this.active;
			if (active)
			{
				this.ProcessCancelEvent(evt, PointerId.mousePointerId);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000332C File Offset: 0x0000152C
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.mousePointerId;
				if (flag2)
				{
					this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
					base.target.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				}
				else
				{
					evt.StopImmediatePropagation();
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000339C File Offset: 0x0000159C
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.mousePointerId;
				if (flag2)
				{
					this.ProcessMoveEvent(evt, evt.localPosition);
					base.target.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				}
				else
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003404 File Offset: 0x00001604
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.mousePointerId;
				if (flag2)
				{
					this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
					base.target.panel.PreventCompatibilityMouseEvents(evt.pointerId);
				}
				else
				{
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003480 File Offset: 0x00001680
		private void OnPointerCancel(PointerCancelEvent evt)
		{
			bool flag = !this.active || !base.CanStopManipulation(evt);
			if (!flag)
			{
				bool flag2 = Clickable.IsNotMouseEvent(evt.pointerId);
				if (flag2)
				{
					this.ProcessCancelEvent(evt, evt.pointerId);
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000034C8 File Offset: 0x000016C8
		private void OnPointerCaptureOut(PointerCaptureOutEvent evt)
		{
			bool flag = !this.active;
			if (!flag)
			{
				bool flag2 = Clickable.IsNotMouseEvent(evt.pointerId);
				if (flag2)
				{
					this.ProcessCancelEvent(evt, evt.pointerId);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003504 File Offset: 0x00001704
		private bool ContainsPointer(int pointerId)
		{
			VisualElement topElementUnderPointer = base.target.elementPanel.GetTopElementUnderPointer(pointerId);
			return base.target == topElementUnderPointer || base.target.Contains(topElementUnderPointer);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003540 File Offset: 0x00001740
		private static bool IsNotMouseEvent(int pointerId)
		{
			return pointerId != PointerId.mousePointerId;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000355D File Offset: 0x0000175D
		protected void Invoke(EventBase evt)
		{
			Action action = this.clicked;
			if (action != null)
			{
				action();
			}
			Action<EventBase> action2 = this.clickedWithEventInfo;
			if (action2 != null)
			{
				action2(evt);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003588 File Offset: 0x00001788
		internal void SimulateSingleClick(EventBase evt, int delayMs = 100)
		{
			base.target.pseudoStates |= PseudoStates.Active;
			base.target.schedule.Execute(delegate()
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}).ExecuteLater((long)delayMs);
			this.Invoke(evt);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035D8 File Offset: 0x000017D8
		protected virtual void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = true;
			this.m_ActivePointerId = pointerId;
			base.target.CapturePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			this.lastMousePosition = localPosition;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				bool flag3 = this.ContainsPointer(pointerId);
				if (flag3)
				{
					this.Invoke(evt);
				}
				bool flag4 = this.m_Repeater == null;
				if (flag4)
				{
					this.m_Repeater = base.target.schedule.Execute(new Action<TimerState>(this.OnTimer)).Every(this.m_Interval).StartingIn(this.m_Delay);
				}
				else
				{
					this.m_Repeater.ExecuteLater(this.m_Delay);
				}
			}
			base.target.pseudoStates |= PseudoStates.Active;
			evt.StopImmediatePropagation();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000036C8 File Offset: 0x000018C8
		protected virtual void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			this.lastMousePosition = localPosition;
			bool flag = this.ContainsPointer(this.m_ActivePointerId);
			if (flag)
			{
				base.target.pseudoStates |= PseudoStates.Active;
			}
			else
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}
			evt.StopPropagation();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003724 File Offset: 0x00001924
		protected virtual void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			this.m_ActivePointerId = -1;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			else
			{
				bool flag3 = this.ContainsPointer(pointerId) && base.target.enabledInHierarchy;
				if (flag3)
				{
					this.Invoke(evt);
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000037D4 File Offset: 0x000019D4
		protected virtual void ProcessCancelEvent(EventBase evt, int pointerId)
		{
			this.active = false;
			this.m_ActivePointerId = -1;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(pointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000385A File Offset: 0x00001A5A
		[CompilerGenerated]
		private void <SimulateSingleClick>b__43_0()
		{
			base.target.pseudoStates &= ~PseudoStates.Active;
		}

		// Token: 0x04000027 RID: 39
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action<EventBase> clickedWithEventInfo;

		// Token: 0x04000028 RID: 40
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action clicked;

		// Token: 0x04000029 RID: 41
		private readonly long m_Delay;

		// Token: 0x0400002A RID: 42
		private readonly long m_Interval;

		// Token: 0x0400002B RID: 43
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <active>k__BackingField;

		// Token: 0x0400002C RID: 44
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector2 <lastMousePosition>k__BackingField;

		// Token: 0x0400002D RID: 45
		private int m_ActivePointerId;

		// Token: 0x0400002E RID: 46
		private bool m_AcceptClicksIfDisabled;

		// Token: 0x0400002F RID: 47
		private IVisualElementScheduledItem m_Repeater;
	}
}
