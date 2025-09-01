using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002B RID: 43
	public sealed class EventDispatcher
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000560D File Offset: 0x0000380D
		internal PointerDispatchState pointerState
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerState>k__BackingField;
			}
		} = new PointerDispatchState();

		// Token: 0x06000104 RID: 260 RVA: 0x00005618 File Offset: 0x00003818
		internal static EventDispatcher CreateDefault()
		{
			return new EventDispatcher(EventDispatcher.s_EditorStrategies);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005634 File Offset: 0x00003834
		internal static EventDispatcher CreateForRuntime(IList<IEventDispatchingStrategy> strategies)
		{
			return new EventDispatcher(strategies);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000564C File Offset: 0x0000384C
		[Obsolete("Please use EventDispatcher.CreateDefault().")]
		internal EventDispatcher() : this(EventDispatcher.s_EditorStrategies)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000565C File Offset: 0x0000385C
		private EventDispatcher(IList<IEventDispatchingStrategy> strategies)
		{
			this.m_DispatchingStrategies = new List<IEventDispatchingStrategy>();
			this.m_DispatchingStrategies.AddRange(strategies);
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000056C4 File Offset: 0x000038C4
		private bool dispatchImmediately
		{
			get
			{
				return this.m_Immediate || this.m_GateCount == 0U;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000056EA File Offset: 0x000038EA
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000056F2 File Offset: 0x000038F2
		internal bool processingEvents
		{
			[CompilerGenerated]
			get
			{
				return this.<processingEvents>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<processingEvents>k__BackingField = value;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000056FC File Offset: 0x000038FC
		internal void Dispatch(EventBase evt, IPanel panel, DispatchMode dispatchMode)
		{
			evt.MarkReceivedByDispatcher();
			bool flag = evt.eventTypeId == EventBase<IMGUIEvent>.TypeId();
			if (flag)
			{
				Event imguiEvent = evt.imguiEvent;
				bool flag2 = imguiEvent.rawType == EventType.Repaint;
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = this.dispatchImmediately || dispatchMode == DispatchMode.Immediate;
			if (flag3)
			{
				this.ProcessEvent(evt, panel);
			}
			else
			{
				evt.Acquire();
				this.m_Queue.Enqueue(new EventDispatcher.EventRecord
				{
					m_Event = evt,
					m_Panel = panel
				});
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000578C File Offset: 0x0000398C
		internal void PushDispatcherContext()
		{
			this.ProcessEventQueue();
			this.m_DispatchContexts.Push(new EventDispatcher.DispatchContext
			{
				m_GateCount = this.m_GateCount,
				m_Queue = this.m_Queue
			});
			this.m_GateCount = 0U;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000057E8 File Offset: 0x000039E8
		internal void PopDispatcherContext()
		{
			Debug.Assert(this.m_GateCount == 0U, "All gates should have been opened before popping dispatch context.");
			Debug.Assert(this.m_Queue.Count == 0, "Queue should be empty when popping dispatch context.");
			EventDispatcher.k_EventQueuePool.Release(this.m_Queue);
			this.m_GateCount = this.m_DispatchContexts.Peek().m_GateCount;
			this.m_Queue = this.m_DispatchContexts.Peek().m_Queue;
			this.m_DispatchContexts.Pop();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000586C File Offset: 0x00003A6C
		internal void CloseGate()
		{
			this.m_GateCount += 1U;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005880 File Offset: 0x00003A80
		internal void OpenGate()
		{
			Debug.Assert(this.m_GateCount > 0U);
			bool flag = this.m_GateCount > 0U;
			if (flag)
			{
				this.m_GateCount -= 1U;
			}
			bool flag2 = this.m_GateCount == 0U;
			if (flag2)
			{
				this.ProcessEventQueue();
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000058D0 File Offset: 0x00003AD0
		private void ProcessEventQueue()
		{
			Queue<EventDispatcher.EventRecord> queue = this.m_Queue;
			this.m_Queue = EventDispatcher.k_EventQueuePool.Get();
			ExitGUIException ex = null;
			try
			{
				this.processingEvents = true;
				while (queue.Count > 0)
				{
					EventDispatcher.EventRecord eventRecord = queue.Dequeue();
					EventBase @event = eventRecord.m_Event;
					IPanel panel = eventRecord.m_Panel;
					try
					{
						this.ProcessEvent(@event, panel);
					}
					catch (ExitGUIException ex2)
					{
						Debug.Assert(ex == null);
						ex = ex2;
					}
					finally
					{
						@event.Dispose();
					}
				}
			}
			finally
			{
				this.processingEvents = false;
				EventDispatcher.k_EventQueuePool.Release(queue);
			}
			bool flag = ex != null;
			if (flag)
			{
				throw ex;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000059A8 File Offset: 0x00003BA8
		private void ProcessEvent(EventBase evt, IPanel panel)
		{
			Event imguiEvent = evt.imguiEvent;
			bool flag = imguiEvent != null && imguiEvent.rawType == EventType.Used;
			using (new EventDispatcherGate(this))
			{
				evt.PreDispatch(panel);
				bool flag2 = !evt.stopDispatch && !evt.isPropagationStopped;
				if (flag2)
				{
					this.ApplyDispatchingStrategies(evt, panel, flag);
				}
				bool flag3 = evt.path != null;
				if (flag3)
				{
					foreach (VisualElement target in evt.path.targetElements)
					{
						evt.target = target;
						EventDispatchUtilities.ExecuteDefaultAction(evt, panel);
					}
					evt.target = evt.leafTarget;
				}
				else
				{
					EventDispatchUtilities.ExecuteDefaultAction(evt, panel);
				}
				evt.PostDispatch(panel);
				this.m_ClickDetector.ProcessEvent(evt);
				Debug.Assert(flag || evt.isPropagationStopped || imguiEvent == null || imguiEvent.rawType != EventType.Used, "Event is used but not stopped.");
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005AE4 File Offset: 0x00003CE4
		private void ApplyDispatchingStrategies(EventBase evt, IPanel panel, bool imguiEventIsInitiallyUsed)
		{
			foreach (IEventDispatchingStrategy eventDispatchingStrategy in this.m_DispatchingStrategies)
			{
				bool flag = eventDispatchingStrategy.CanDispatchEvent(evt);
				if (flag)
				{
					eventDispatchingStrategy.DispatchEvent(evt, panel);
					Debug.Assert(imguiEventIsInitiallyUsed || evt.isPropagationStopped || evt.imguiEvent == null || evt.imguiEvent.rawType != EventType.Used, "Unexpected condition: !evt.isPropagationStopped && evt.imguiEvent.rawType == EventType.Used.");
					bool flag2 = evt.stopDispatch || evt.isPropagationStopped;
					if (flag2)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005B98 File Offset: 0x00003D98
		// Note: this type is marked as 'beforefieldinit'.
		static EventDispatcher()
		{
		}

		// Token: 0x04000071 RID: 113
		internal ClickDetector m_ClickDetector = new ClickDetector();

		// Token: 0x04000072 RID: 114
		private List<IEventDispatchingStrategy> m_DispatchingStrategies;

		// Token: 0x04000073 RID: 115
		private static readonly ObjectPool<Queue<EventDispatcher.EventRecord>> k_EventQueuePool = new ObjectPool<Queue<EventDispatcher.EventRecord>>(100);

		// Token: 0x04000074 RID: 116
		private Queue<EventDispatcher.EventRecord> m_Queue;

		// Token: 0x04000075 RID: 117
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly PointerDispatchState <pointerState>k__BackingField;

		// Token: 0x04000076 RID: 118
		private uint m_GateCount;

		// Token: 0x04000077 RID: 119
		private Stack<EventDispatcher.DispatchContext> m_DispatchContexts = new Stack<EventDispatcher.DispatchContext>();

		// Token: 0x04000078 RID: 120
		private static readonly IEventDispatchingStrategy[] s_EditorStrategies = new IEventDispatchingStrategy[]
		{
			new PointerCaptureDispatchingStrategy(),
			new MouseCaptureDispatchingStrategy(),
			new KeyboardEventDispatchingStrategy(),
			new PointerEventDispatchingStrategy(),
			new MouseEventDispatchingStrategy(),
			new CommandEventDispatchingStrategy(),
			new IMGUIEventDispatchingStrategy(),
			new DefaultDispatchingStrategy()
		};

		// Token: 0x04000079 RID: 121
		private bool m_Immediate = false;

		// Token: 0x0400007A RID: 122
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <processingEvents>k__BackingField;

		// Token: 0x0200002C RID: 44
		private struct EventRecord
		{
			// Token: 0x0400007B RID: 123
			public EventBase m_Event;

			// Token: 0x0400007C RID: 124
			public IPanel m_Panel;
		}

		// Token: 0x0200002D RID: 45
		private struct DispatchContext
		{
			// Token: 0x0400007D RID: 125
			public uint m_GateCount;

			// Token: 0x0400007E RID: 126
			public Queue<EventDispatcher.EventRecord> m_Queue;
		}
	}
}
