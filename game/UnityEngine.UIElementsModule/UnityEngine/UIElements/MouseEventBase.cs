using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F7 RID: 503
	public abstract class MouseEventBase<T> : EventBase<T>, IMouseEvent, IMouseEventInternal where T : MouseEventBase<T>, new()
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x000402C5 File Offset: 0x0003E4C5
		// (set) Token: 0x06000FBF RID: 4031 RVA: 0x000402CD File Offset: 0x0003E4CD
		public EventModifiers modifiers
		{
			[CompilerGenerated]
			get
			{
				return this.<modifiers>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<modifiers>k__BackingField = value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x000402D6 File Offset: 0x0003E4D6
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x000402DE File Offset: 0x0003E4DE
		public Vector2 mousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<mousePosition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<mousePosition>k__BackingField = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x000402E7 File Offset: 0x0003E4E7
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x000402EF File Offset: 0x0003E4EF
		public Vector2 localMousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<localMousePosition>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<localMousePosition>k__BackingField = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x000402F8 File Offset: 0x0003E4F8
		// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x00040300 File Offset: 0x0003E500
		public Vector2 mouseDelta
		{
			[CompilerGenerated]
			get
			{
				return this.<mouseDelta>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<mouseDelta>k__BackingField = value;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00040309 File Offset: 0x0003E509
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x00040311 File Offset: 0x0003E511
		public int clickCount
		{
			[CompilerGenerated]
			get
			{
				return this.<clickCount>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<clickCount>k__BackingField = value;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0004031A File Offset: 0x0003E51A
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x00040322 File Offset: 0x0003E522
		public int button
		{
			[CompilerGenerated]
			get
			{
				return this.<button>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<button>k__BackingField = value;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0004032B File Offset: 0x0003E52B
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x00040333 File Offset: 0x0003E533
		public int pressedButtons
		{
			[CompilerGenerated]
			get
			{
				return this.<pressedButtons>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<pressedButtons>k__BackingField = value;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0004033C File Offset: 0x0003E53C
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0004035C File Offset: 0x0003E55C
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0004037C File Offset: 0x0003E57C
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0004039C File Offset: 0x0003E59C
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x000403BC File Offset: 0x0003E5BC
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool result;
				if (flag)
				{
					result = this.commandKey;
				}
				else
				{
					result = this.ctrlKey;
				}
				return result;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x000403F5 File Offset: 0x0003E5F5
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x000403FD File Offset: 0x0003E5FD
		bool IMouseEventInternal.triggeredByOS
		{
			[CompilerGenerated]
			get
			{
				return this.<UnityEngine.UIElements.IMouseEventInternal.triggeredByOS>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnityEngine.UIElements.IMouseEventInternal.triggeredByOS>k__BackingField = value;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00040406 File Offset: 0x0003E606
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x0004040E File Offset: 0x0003E60E
		bool IMouseEventInternal.recomputeTopElementUnderMouse
		{
			[CompilerGenerated]
			get
			{
				return this.<UnityEngine.UIElements.IMouseEventInternal.recomputeTopElementUnderMouse>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnityEngine.UIElements.IMouseEventInternal.recomputeTopElementUnderMouse>k__BackingField = value;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00040417 File Offset: 0x0003E617
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x0004041F File Offset: 0x0003E61F
		IPointerEvent IMouseEventInternal.sourcePointerEvent
		{
			[CompilerGenerated]
			get
			{
				return this.<UnityEngine.UIElements.IMouseEventInternal.sourcePointerEvent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnityEngine.UIElements.IMouseEventInternal.sourcePointerEvent>k__BackingField = value;
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00040428 File Offset: 0x0003E628
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0004043C File Offset: 0x0003E63C
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
			this.modifiers = EventModifiers.None;
			this.mousePosition = Vector2.zero;
			this.localMousePosition = Vector2.zero;
			this.mouseDelta = Vector2.zero;
			this.clickCount = 0;
			this.button = 0;
			this.pressedButtons = 0;
			((IMouseEventInternal)this).triggeredByOS = false;
			((IMouseEventInternal)this).recomputeTopElementUnderMouse = true;
			((IMouseEventInternal)this).sourcePointerEvent = null;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x000404B0 File Offset: 0x0003E6B0
		// (set) Token: 0x06000FDA RID: 4058 RVA: 0x000404C8 File Offset: 0x0003E6C8
		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement visualElement = this.currentTarget as VisualElement;
				bool flag = visualElement != null;
				if (flag)
				{
					this.localMousePosition = visualElement.WorldToLocal(this.mousePosition);
				}
				else
				{
					this.localMousePosition = this.mousePosition;
				}
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00040518 File Offset: 0x0003E718
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IMouseEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(PointerId.mousePointerId, this.mousePosition, panel, panel.contextType);
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00040554 File Offset: 0x0003E754
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase != null;
			if (flag)
			{
				Debug.Assert(eventBase.processed);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
				bool isPropagationStopped = base.isPropagationStopped;
				if (isPropagationStopped)
				{
					eventBase.StopPropagation();
				}
				bool isImmediatePropagationStopped = base.isImmediatePropagationStopped;
				if (isImmediatePropagationStopped)
				{
					eventBase.StopImmediatePropagation();
				}
				bool isDefaultPrevented = base.isDefaultPrevented;
				if (isDefaultPrevented)
				{
					eventBase.PreventDefault();
				}
				eventBase.processedByFocusController |= base.processedByFocusController;
			}
			base.PostDispatch(panel);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000405F0 File Offset: 0x0003E7F0
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.mousePosition = systemEvent.mousePosition;
				pooled.localMousePosition = systemEvent.mousePosition;
				pooled.mouseDelta = systemEvent.delta;
				pooled.button = systemEvent.button;
				pooled.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
				pooled.clickCount = systemEvent.clickCount;
				pooled.triggeredByOS = true;
				pooled.recomputeTopElementUnderMouse = true;
			}
			return pooled;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x000406C0 File Offset: 0x0003E8C0
		public static T GetPooled(Vector2 position, int button, int clickCount, Vector2 delta, EventModifiers modifiers = EventModifiers.None)
		{
			return MouseEventBase<T>.GetPooled(position, button, clickCount, delta, modifiers, false);
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000406E0 File Offset: 0x0003E8E0
		internal static T GetPooled(Vector2 position, int button, int clickCount, Vector2 delta, EventModifiers modifiers, bool fromOS)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.mousePosition = position;
			pooled.localMousePosition = position;
			pooled.mouseDelta = delta;
			pooled.button = button;
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
			pooled.clickCount = clickCount;
			pooled.triggeredByOS = fromOS;
			pooled.recomputeTopElementUnderMouse = true;
			return pooled;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0004077C File Offset: 0x0003E97C
		internal static T GetPooled(IMouseEvent triggerEvent, Vector2 mousePosition, bool recomputeTopElementUnderMouse)
		{
			bool flag = triggerEvent != null;
			T result;
			if (flag)
			{
				result = MouseEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.mousePosition = mousePosition;
				pooled.localMousePosition = mousePosition;
				pooled.recomputeTopElementUnderMouse = recomputeTopElementUnderMouse;
				result = pooled;
			}
			return result;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000407D0 File Offset: 0x0003E9D0
		public static T GetPooled(IMouseEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled(triggerEvent as EventBase);
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.modifiers = triggerEvent.modifiers;
				pooled.mousePosition = triggerEvent.mousePosition;
				pooled.localMousePosition = triggerEvent.mousePosition;
				pooled.mouseDelta = triggerEvent.mouseDelta;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.clickCount = triggerEvent.clickCount;
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag2 = mouseEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS = mouseEventInternal.triggeredByOS;
					pooled.recomputeTopElementUnderMouse = false;
				}
			}
			return pooled;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000408AC File Offset: 0x0003EAAC
		protected static T GetPooled(IPointerEvent pointerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			EventBase eventBase = pooled;
			EventBase eventBase2 = pointerEvent as EventBase;
			eventBase.target = ((eventBase2 != null) ? eventBase2.target : null);
			EventBase eventBase3 = pooled;
			EventBase eventBase4 = pointerEvent as EventBase;
			eventBase3.imguiEvent = ((eventBase4 != null) ? eventBase4.imguiEvent : null);
			EventBase eventBase5 = pointerEvent as EventBase;
			bool flag = ((eventBase5 != null) ? eventBase5.path : null) != null;
			if (flag)
			{
				pooled.path = (pointerEvent as EventBase).path;
			}
			pooled.modifiers = pointerEvent.modifiers;
			pooled.mousePosition = pointerEvent.position;
			pooled.localMousePosition = pointerEvent.position;
			pooled.mouseDelta = pointerEvent.deltaPosition;
			pooled.button = ((pointerEvent.button == -1) ? 0 : pointerEvent.button);
			pooled.pressedButtons = pointerEvent.pressedButtons;
			pooled.clickCount = pointerEvent.clickCount;
			IPointerEventInternal pointerEventInternal = pointerEvent as IPointerEventInternal;
			bool flag2 = pointerEventInternal != null;
			if (flag2)
			{
				pooled.triggeredByOS = pointerEventInternal.triggeredByOS;
				pooled.recomputeTopElementUnderMouse = true;
				pooled.sourcePointerEvent = pointerEvent;
			}
			return pooled;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00040A09 File Offset: 0x0003EC09
		protected MouseEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x04000720 RID: 1824
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventModifiers <modifiers>k__BackingField;

		// Token: 0x04000721 RID: 1825
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <mousePosition>k__BackingField;

		// Token: 0x04000722 RID: 1826
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <localMousePosition>k__BackingField;

		// Token: 0x04000723 RID: 1827
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <mouseDelta>k__BackingField;

		// Token: 0x04000724 RID: 1828
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <clickCount>k__BackingField;

		// Token: 0x04000725 RID: 1829
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <button>k__BackingField;

		// Token: 0x04000726 RID: 1830
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <pressedButtons>k__BackingField;

		// Token: 0x04000727 RID: 1831
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <UnityEngine.UIElements.IMouseEventInternal.triggeredByOS>k__BackingField;

		// Token: 0x04000728 RID: 1832
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <UnityEngine.UIElements.IMouseEventInternal.recomputeTopElementUnderMouse>k__BackingField;

		// Token: 0x04000729 RID: 1833
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private IPointerEvent <UnityEngine.UIElements.IMouseEventInternal.sourcePointerEvent>k__BackingField;
	}
}
