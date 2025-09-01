using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D0 RID: 464
	public abstract class EventBase : IDisposable
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003DA68 File Offset: 0x0003BC68
		protected static long RegisterEventType()
		{
			return EventBase.s_LastTypeId += 1L;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0003DA88 File Offset: 0x0003BC88
		public virtual long eventTypeId
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0003DA8C File Offset: 0x0003BC8C
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x0003DA94 File Offset: 0x0003BC94
		public long timestamp
		{
			[CompilerGenerated]
			get
			{
				return this.<timestamp>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<timestamp>k__BackingField = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0003DA9D File Offset: 0x0003BC9D
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x0003DAA5 File Offset: 0x0003BCA5
		internal ulong eventId
		{
			[CompilerGenerated]
			get
			{
				return this.<eventId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<eventId>k__BackingField = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0003DAAE File Offset: 0x0003BCAE
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0003DAB6 File Offset: 0x0003BCB6
		internal ulong triggerEventId
		{
			[CompilerGenerated]
			get
			{
				return this.<triggerEventId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<triggerEventId>k__BackingField = value;
			}
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003DABF File Offset: 0x0003BCBF
		internal void SetTriggerEventId(ulong id)
		{
			this.triggerEventId = id;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0003DACA File Offset: 0x0003BCCA
		// (set) Token: 0x06000EBB RID: 3771 RVA: 0x0003DAD2 File Offset: 0x0003BCD2
		internal EventBase.EventPropagation propagation
		{
			[CompilerGenerated]
			get
			{
				return this.<propagation>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<propagation>k__BackingField = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0003DADC File Offset: 0x0003BCDC
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x0003DB44 File Offset: 0x0003BD44
		internal PropagationPaths path
		{
			get
			{
				bool flag = this.m_Path == null;
				if (flag)
				{
					PropagationPaths.Type type = this.tricklesDown ? PropagationPaths.Type.TrickleDown : PropagationPaths.Type.None;
					type |= (this.bubbles ? PropagationPaths.Type.BubbleUp : PropagationPaths.Type.None);
					this.m_Path = PropagationPaths.Build(this.leafTarget as VisualElement, this, type);
					EventDebugger.LogPropagationPaths(this, this.m_Path);
				}
				return this.m_Path;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					this.m_Path = PropagationPaths.Copy(value);
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x0003DB66 File Offset: 0x0003BD66
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x0003DB6E File Offset: 0x0003BD6E
		private EventBase.LifeCycleStatus lifeCycleStatus
		{
			[CompilerGenerated]
			get
			{
				return this.<lifeCycleStatus>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<lifeCycleStatus>k__BackingField = value;
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00002166 File Offset: 0x00000366
		[Obsolete("Override PreDispatch(IPanel panel) instead.")]
		protected virtual void PreDispatch()
		{
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003DB77 File Offset: 0x0003BD77
		protected internal virtual void PreDispatch(IPanel panel)
		{
			this.PreDispatch();
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00002166 File Offset: 0x00000366
		[Obsolete("Override PostDispatch(IPanel panel) instead.")]
		protected virtual void PostDispatch()
		{
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003DB81 File Offset: 0x0003BD81
		protected internal virtual void PostDispatch(IPanel panel)
		{
			this.PostDispatch();
			this.processed = true;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0003DB94 File Offset: 0x0003BD94
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x0003DBB4 File Offset: 0x0003BDB4
		public bool bubbles
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.Bubbles) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.Bubbles;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.Bubbles;
				}
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0003DBEC File Offset: 0x0003BDEC
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x0003DC0C File Offset: 0x0003BE0C
		public bool tricklesDown
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.TricklesDown) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.TricklesDown;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.TricklesDown;
				}
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0003DC44 File Offset: 0x0003BE44
		// (set) Token: 0x06000EC9 RID: 3785 RVA: 0x0003DC64 File Offset: 0x0003BE64
		internal bool skipDisabledElements
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.SkipDisabledElements) > EventBase.EventPropagation.None;
			}
			set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.SkipDisabledElements;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.SkipDisabledElements;
				}
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0003DC9C File Offset: 0x0003BE9C
		// (set) Token: 0x06000ECB RID: 3787 RVA: 0x0003DCBC File Offset: 0x0003BEBC
		internal bool ignoreCompositeRoots
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.IgnoreCompositeRoots) > EventBase.EventPropagation.None;
			}
			set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.IgnoreCompositeRoots;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.IgnoreCompositeRoots;
				}
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0003DCF5 File Offset: 0x0003BEF5
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x0003DCFD File Offset: 0x0003BEFD
		internal IEventHandler leafTarget
		{
			[CompilerGenerated]
			get
			{
				return this.<leafTarget>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<leafTarget>k__BackingField = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0003DD08 File Offset: 0x0003BF08
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0003DD20 File Offset: 0x0003BF20
		public IEventHandler target
		{
			get
			{
				return this.m_Target;
			}
			set
			{
				this.m_Target = value;
				bool flag = this.leafTarget == null;
				if (flag)
				{
					this.leafTarget = value;
				}
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0003DD4C File Offset: 0x0003BF4C
		internal List<IEventHandler> skipElements
		{
			[CompilerGenerated]
			get
			{
				return this.<skipElements>k__BackingField;
			}
		} = new List<IEventHandler>();

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003DD54 File Offset: 0x0003BF54
		internal bool Skip(IEventHandler h)
		{
			return this.skipElements.Contains(h);
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0003DD74 File Offset: 0x0003BF74
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x0003DD94 File Offset: 0x0003BF94
		public bool isPropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagationStopped;
				}
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003DDCC File Offset: 0x0003BFCC
		public void StopPropagation()
		{
			this.isPropagationStopped = true;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0003DDD8 File Offset: 0x0003BFD8
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x0003DDF8 File Offset: 0x0003BFF8
		public bool isImmediatePropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ImmediatePropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003DE30 File Offset: 0x0003C030
		public void StopImmediatePropagation()
		{
			this.isPropagationStopped = true;
			this.isImmediatePropagationStopped = true;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0003DE44 File Offset: 0x0003C044
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x0003DE64 File Offset: 0x0003C064
		public bool isDefaultPrevented
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.DefaultPrevented) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.DefaultPrevented;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.DefaultPrevented;
				}
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003DE9C File Offset: 0x0003C09C
		public void PreventDefault()
		{
			bool flag = (this.propagation & EventBase.EventPropagation.Cancellable) == EventBase.EventPropagation.Cancellable;
			if (flag)
			{
				this.isDefaultPrevented = true;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0003DEC3 File Offset: 0x0003C0C3
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0003DECB File Offset: 0x0003C0CB
		public PropagationPhase propagationPhase
		{
			[CompilerGenerated]
			get
			{
				return this.<propagationPhase>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<propagationPhase>k__BackingField = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003DED4 File Offset: 0x0003C0D4
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		public virtual IEventHandler currentTarget
		{
			get
			{
				return this.m_CurrentTarget;
			}
			internal set
			{
				this.m_CurrentTarget = value;
				bool flag = this.imguiEvent != null;
				if (flag)
				{
					VisualElement visualElement = this.currentTarget as VisualElement;
					bool flag2 = visualElement != null;
					if (flag2)
					{
						this.imguiEvent.mousePosition = visualElement.WorldToLocal(this.originalMousePosition);
					}
					else
					{
						this.imguiEvent.mousePosition = this.originalMousePosition;
					}
				}
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0003DF54 File Offset: 0x0003C154
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x0003DF74 File Offset: 0x0003C174
		public bool dispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatching) > EventBase.LifeCycleStatus.None;
			}
			internal set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatching;
					this.dispatched = true;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatching;
				}
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003DFB4 File Offset: 0x0003C1B4
		internal void MarkReceivedByDispatcher()
		{
			Debug.Assert(!this.dispatched, "Events cannot be dispatched more than once.");
			this.dispatched = true;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0003DFD4 File Offset: 0x0003C1D4
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x0003DFF8 File Offset: 0x0003C1F8
		private bool dispatched
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatched) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatched;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatched;
				}
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0003E038 File Offset: 0x0003C238
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x0003E05C File Offset: 0x0003C25C
		internal bool processed
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Processed) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Processed;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Processed;
				}
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0003E09C File Offset: 0x0003C29C
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x0003E0C0 File Offset: 0x0003C2C0
		internal bool processedByFocusController
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ProcessedByFocusController) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0003E100 File Offset: 0x0003C300
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x0003E120 File Offset: 0x0003C320
		internal bool stopDispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.StopDispatch) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.StopDispatch;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.StopDispatch;
				}
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0003E15C File Offset: 0x0003C35C
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x0003E180 File Offset: 0x0003C380
		internal bool propagateToIMGUI
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagateToIMGUI) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0003E1C0 File Offset: 0x0003C3C0
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x0003E1E0 File Offset: 0x0003C3E0
		private bool imguiEventIsValid
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.IMGUIEventIsValid) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0003E21C File Offset: 0x0003C41C
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x0003E240 File Offset: 0x0003C440
		public Event imguiEvent
		{
			get
			{
				return this.imguiEventIsValid ? this.m_ImguiEvent : null;
			}
			protected set
			{
				bool flag = this.m_ImguiEvent == null;
				if (flag)
				{
					this.m_ImguiEvent = new Event();
				}
				bool flag2 = value != null;
				if (flag2)
				{
					this.m_ImguiEvent.CopyFrom(value);
					this.imguiEventIsValid = true;
					this.originalMousePosition = value.mousePosition;
				}
				else
				{
					this.imguiEventIsValid = false;
				}
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0003E2A0 File Offset: 0x0003C4A0
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0003E2A8 File Offset: 0x0003C4A8
		public Vector2 originalMousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<originalMousePosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<originalMousePosition>k__BackingField = value;
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003E2B1 File Offset: 0x0003C4B1
		protected virtual void Init()
		{
			this.LocalInit();
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		private void LocalInit()
		{
			this.timestamp = Panel.TimeSinceStartupMs();
			this.triggerEventId = 0UL;
			ulong num = EventBase.s_NextEventId;
			EventBase.s_NextEventId = num + 1UL;
			this.eventId = num;
			this.propagation = EventBase.EventPropagation.None;
			PropagationPaths path = this.m_Path;
			if (path != null)
			{
				path.Release();
			}
			this.m_Path = null;
			this.leafTarget = null;
			this.target = null;
			this.skipElements.Clear();
			this.isPropagationStopped = false;
			this.isImmediatePropagationStopped = false;
			this.isDefaultPrevented = false;
			this.propagationPhase = PropagationPhase.None;
			this.originalMousePosition = Vector2.zero;
			this.m_CurrentTarget = null;
			this.dispatch = false;
			this.stopDispatch = false;
			this.propagateToIMGUI = true;
			this.dispatched = false;
			this.processed = false;
			this.processedByFocusController = false;
			this.imguiEventIsValid = false;
			this.pooled = false;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003E3A4 File Offset: 0x0003C5A4
		protected EventBase()
		{
			this.m_ImguiEvent = null;
			this.LocalInit();
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0003E3C8 File Offset: 0x0003C5C8
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x0003E3E8 File Offset: 0x0003C5E8
		protected bool pooled
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Pooled) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Pooled;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Pooled;
				}
			}
		}

		// Token: 0x06000EF7 RID: 3831
		internal abstract void Acquire();

		// Token: 0x06000EF8 RID: 3832
		public abstract void Dispose();

		// Token: 0x040006CA RID: 1738
		private static long s_LastTypeId;

		// Token: 0x040006CB RID: 1739
		private static ulong s_NextEventId;

		// Token: 0x040006CC RID: 1740
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private long <timestamp>k__BackingField;

		// Token: 0x040006CD RID: 1741
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <eventId>k__BackingField;

		// Token: 0x040006CE RID: 1742
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <triggerEventId>k__BackingField;

		// Token: 0x040006CF RID: 1743
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventBase.EventPropagation <propagation>k__BackingField;

		// Token: 0x040006D0 RID: 1744
		private PropagationPaths m_Path;

		// Token: 0x040006D1 RID: 1745
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventBase.LifeCycleStatus <lifeCycleStatus>k__BackingField;

		// Token: 0x040006D2 RID: 1746
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private IEventHandler <leafTarget>k__BackingField;

		// Token: 0x040006D3 RID: 1747
		private IEventHandler m_Target;

		// Token: 0x040006D4 RID: 1748
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly List<IEventHandler> <skipElements>k__BackingField;

		// Token: 0x040006D5 RID: 1749
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PropagationPhase <propagationPhase>k__BackingField;

		// Token: 0x040006D6 RID: 1750
		private IEventHandler m_CurrentTarget;

		// Token: 0x040006D7 RID: 1751
		private Event m_ImguiEvent;

		// Token: 0x040006D8 RID: 1752
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector2 <originalMousePosition>k__BackingField;

		// Token: 0x020001D1 RID: 465
		[Flags]
		internal enum EventPropagation
		{
			// Token: 0x040006DA RID: 1754
			None = 0,
			// Token: 0x040006DB RID: 1755
			Bubbles = 1,
			// Token: 0x040006DC RID: 1756
			TricklesDown = 2,
			// Token: 0x040006DD RID: 1757
			Cancellable = 4,
			// Token: 0x040006DE RID: 1758
			SkipDisabledElements = 8,
			// Token: 0x040006DF RID: 1759
			IgnoreCompositeRoots = 16
		}

		// Token: 0x020001D2 RID: 466
		[Flags]
		private enum LifeCycleStatus
		{
			// Token: 0x040006E1 RID: 1761
			None = 0,
			// Token: 0x040006E2 RID: 1762
			PropagationStopped = 1,
			// Token: 0x040006E3 RID: 1763
			ImmediatePropagationStopped = 2,
			// Token: 0x040006E4 RID: 1764
			DefaultPrevented = 4,
			// Token: 0x040006E5 RID: 1765
			Dispatching = 8,
			// Token: 0x040006E6 RID: 1766
			Pooled = 16,
			// Token: 0x040006E7 RID: 1767
			IMGUIEventIsValid = 32,
			// Token: 0x040006E8 RID: 1768
			StopDispatch = 64,
			// Token: 0x040006E9 RID: 1769
			PropagateToIMGUI = 128,
			// Token: 0x040006EA RID: 1770
			Dispatched = 512,
			// Token: 0x040006EB RID: 1771
			Processed = 1024,
			// Token: 0x040006EC RID: 1772
			ProcessedByFocusController = 2048
		}
	}
}
