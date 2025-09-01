using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000237 RID: 567
	internal class EventDebugger
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600110B RID: 4363 RVA: 0x000436A3 File Offset: 0x000418A3
		// (set) Token: 0x0600110C RID: 4364 RVA: 0x000436AB File Offset: 0x000418AB
		public IPanel panel
		{
			[CompilerGenerated]
			get
			{
				return this.<panel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<panel>k__BackingField = value;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x000436B4 File Offset: 0x000418B4
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x000436BC File Offset: 0x000418BC
		public bool isReplaying
		{
			[CompilerGenerated]
			get
			{
				return this.<isReplaying>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<isReplaying>k__BackingField = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x000436C5 File Offset: 0x000418C5
		// (set) Token: 0x06001110 RID: 4368 RVA: 0x000436CD File Offset: 0x000418CD
		public float playbackSpeed
		{
			[CompilerGenerated]
			get
			{
				return this.<playbackSpeed>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<playbackSpeed>k__BackingField = value;
			}
		} = 1f;

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x000436D6 File Offset: 0x000418D6
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x000436DE File Offset: 0x000418DE
		public bool isPlaybackPaused
		{
			[CompilerGenerated]
			get
			{
				return this.<isPlaybackPaused>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<isPlaybackPaused>k__BackingField = value;
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000436E8 File Offset: 0x000418E8
		public void UpdateModificationCount()
		{
			bool flag = this.panel == null;
			if (!flag)
			{
				long num;
				bool flag2 = !this.m_ModificationCount.TryGetValue(this.panel, out num);
				if (flag2)
				{
					num = 0L;
				}
				num += 1L;
				this.m_ModificationCount[this.panel] = num;
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0004373C File Offset: 0x0004193C
		public void BeginProcessEvent(EventBase evt, IEventHandler mouseCapture)
		{
			this.AddBeginProcessEvent(evt, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0004374F File Offset: 0x0004194F
		public void EndProcessEvent(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.AddEndProcessEvent(evt, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00043764 File Offset: 0x00041964
		public void LogCall(int cbHashCode, string cbName, EventBase evt, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture)
		{
			this.AddCallObject(cbHashCode, cbName, evt, propagationHasStopped, immediatePropagationHasStopped, defaultHasBeenPrevented, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0004378D File Offset: 0x0004198D
		public void LogIMGUICall(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			this.AddIMGUICall(evt, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000437A1 File Offset: 0x000419A1
		public void LogExecuteDefaultAction(EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
		{
			this.AddExecuteDefaultAction(evt, phase, duration, mouseCapture);
			this.UpdateModificationCount();
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00002166 File Offset: 0x00000366
		public static void LogPropagationPaths(EventBase evt, PropagationPaths paths)
		{
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000437B8 File Offset: 0x000419B8
		private void LogPropagationPathsInternal(EventBase evt, PropagationPaths paths)
		{
			PropagationPaths paths2 = (paths == null) ? new PropagationPaths() : new PropagationPaths(paths);
			this.AddPropagationPaths(evt, paths2);
			this.UpdateModificationCount();
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000437E8 File Offset: 0x000419E8
		public List<EventDebuggerCallTrace> GetCalls(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerCallTrace> list;
			bool flag = !this.m_EventCalledObjects.TryGetValue(panel, out list);
			List<EventDebuggerCallTrace> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerCallTrace> list2 = new List<EventDebuggerCallTrace>();
					foreach (EventDebuggerCallTrace eventDebuggerCallTrace in list)
					{
						bool flag3 = eventDebuggerCallTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list2.Add(eventDebuggerCallTrace);
						}
					}
					list = list2;
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00043898 File Offset: 0x00041A98
		public List<EventDebuggerDefaultActionTrace> GetDefaultActions(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerDefaultActionTrace> list;
			bool flag = !this.m_EventDefaultActionObjects.TryGetValue(panel, out list);
			List<EventDebuggerDefaultActionTrace> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerDefaultActionTrace> list2 = new List<EventDebuggerDefaultActionTrace>();
					foreach (EventDebuggerDefaultActionTrace eventDebuggerDefaultActionTrace in list)
					{
						bool flag3 = eventDebuggerDefaultActionTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list2.Add(eventDebuggerDefaultActionTrace);
						}
					}
					list = list2;
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00043948 File Offset: 0x00041B48
		public List<EventDebuggerPathTrace> GetPropagationPaths(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerPathTrace> list;
			bool flag = !this.m_EventPathObjects.TryGetValue(panel, out list);
			List<EventDebuggerPathTrace> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerPathTrace> list2 = new List<EventDebuggerPathTrace>();
					foreach (EventDebuggerPathTrace eventDebuggerPathTrace in list)
					{
						bool flag3 = eventDebuggerPathTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list2.Add(eventDebuggerPathTrace);
						}
					}
					list = list2;
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000439F8 File Offset: 0x00041BF8
		public List<EventDebuggerTrace> GetBeginEndProcessedEvents(IPanel panel, EventDebuggerEventRecord evt = null)
		{
			List<EventDebuggerTrace> list;
			bool flag = !this.m_EventProcessedEvents.TryGetValue(panel, out list);
			List<EventDebuggerTrace> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = evt != null && list != null;
				if (flag2)
				{
					List<EventDebuggerTrace> list2 = new List<EventDebuggerTrace>();
					foreach (EventDebuggerTrace eventDebuggerTrace in list)
					{
						bool flag3 = eventDebuggerTrace.eventBase.eventId == evt.eventId;
						if (flag3)
						{
							list2.Add(eventDebuggerTrace);
						}
					}
					list = list2;
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00043AA8 File Offset: 0x00041CA8
		public long GetModificationCount(IPanel panel)
		{
			bool flag = panel == null;
			long result;
			if (flag)
			{
				result = -1L;
			}
			else
			{
				long num;
				bool flag2 = !this.m_ModificationCount.TryGetValue(panel, out num);
				if (flag2)
				{
					num = -1L;
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00043AE4 File Offset: 0x00041CE4
		public void ClearLogs()
		{
			this.UpdateModificationCount();
			bool flag = this.panel == null;
			if (flag)
			{
				this.m_EventCalledObjects.Clear();
				this.m_EventDefaultActionObjects.Clear();
				this.m_EventPathObjects.Clear();
				this.m_EventProcessedEvents.Clear();
				this.m_StackOfProcessedEvent.Clear();
				this.m_EventTypeProcessedCount.Clear();
			}
			else
			{
				this.m_EventCalledObjects.Remove(this.panel);
				this.m_EventDefaultActionObjects.Remove(this.panel);
				this.m_EventPathObjects.Remove(this.panel);
				this.m_EventProcessedEvents.Remove(this.panel);
				this.m_StackOfProcessedEvent.Remove(this.panel);
				Dictionary<long, int> dictionary;
				bool flag2 = this.m_EventTypeProcessedCount.TryGetValue(this.panel, out dictionary);
				if (flag2)
				{
					dictionary.Clear();
				}
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00043BCC File Offset: 0x00041DCC
		public void SaveReplaySessionFromSelection(string path, List<EventDebuggerEventRecord> eventList)
		{
			bool flag = string.IsNullOrEmpty(path);
			if (!flag)
			{
				EventDebuggerRecordList obj = new EventDebuggerRecordList
				{
					eventList = eventList
				};
				string contents = JsonUtility.ToJson(obj);
				File.WriteAllText(path, contents);
				Debug.Log("Saved under: " + path);
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00043C14 File Offset: 0x00041E14
		public EventDebuggerRecordList LoadReplaySession(string path)
		{
			bool flag = string.IsNullOrEmpty(path);
			EventDebuggerRecordList result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string json = File.ReadAllText(path);
				result = JsonUtility.FromJson<EventDebuggerRecordList>(json);
			}
			return result;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00043C41 File Offset: 0x00041E41
		public IEnumerator ReplayEvents(IEnumerable<EventDebuggerEventRecord> eventBases, Action<int, int> refreshList)
		{
			bool flag = eventBases == null;
			if (flag)
			{
				yield break;
			}
			this.isReplaying = true;
			IEnumerator doReplay = this.DoReplayEvents(eventBases, refreshList);
			while (doReplay.MoveNext())
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00043C5E File Offset: 0x00041E5E
		public void StopPlayback()
		{
			this.isReplaying = false;
			this.isPlaybackPaused = false;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00043C71 File Offset: 0x00041E71
		private IEnumerator DoReplayEvents(IEnumerable<EventDebuggerEventRecord> eventBases, Action<int, int> refreshList)
		{
			EventDebugger.<>c__DisplayClass34_0 CS$<>8__locals1 = new EventDebugger.<>c__DisplayClass34_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.sortedEvents = (from e in eventBases
			orderby e.timestamp
			select e).ToList<EventDebuggerEventRecord>();
			int sortedEventsCount = CS$<>8__locals1.sortedEvents.Count;
			int i = 0;
			while (i < sortedEventsCount)
			{
				bool flag = !this.isReplaying;
				if (flag)
				{
					break;
				}
				EventDebuggerEventRecord eventBase = CS$<>8__locals1.sortedEvents[i];
				Event newEvent = new Event
				{
					button = eventBase.button,
					clickCount = eventBase.clickCount,
					modifiers = eventBase.modifiers,
					mousePosition = eventBase.mousePosition
				};
				bool flag2 = eventBase.eventTypeId == EventBase<MouseMoveEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag2)
				{
					newEvent.type = EventType.MouseMove;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseMove), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag3 = eventBase.eventTypeId == EventBase<MouseDownEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag3)
				{
					newEvent.type = EventType.MouseDown;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseDown), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag4 = eventBase.eventTypeId == EventBase<MouseUpEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag4)
				{
					newEvent.type = EventType.MouseUp;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseUp), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag5 = eventBase.eventTypeId == EventBase<ContextClickEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag5)
				{
					newEvent.type = EventType.ContextClick;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ContextClick), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag6 = eventBase.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag6)
				{
					newEvent.type = EventType.MouseEnterWindow;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseEnterWindow), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag7 = eventBase.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag7)
				{
					newEvent.type = EventType.MouseLeaveWindow;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseLeaveWindow), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag8 = eventBase.eventTypeId == EventBase<PointerMoveEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag8)
				{
					newEvent.type = EventType.MouseMove;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseMove), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag9 = eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag9)
				{
					newEvent.type = EventType.MouseDown;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseDown), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag10 = eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag10)
				{
					newEvent.type = EventType.MouseUp;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseUp), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag11 = eventBase.eventTypeId == EventBase<WheelEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
				if (flag11)
				{
					newEvent.type = EventType.ScrollWheel;
					newEvent.delta = eventBase.delta;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ScrollWheel), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag12 = eventBase.eventTypeId == EventBase<KeyDownEvent>.TypeId();
				if (flag12)
				{
					newEvent.type = EventType.KeyDown;
					newEvent.character = eventBase.character;
					newEvent.keyCode = eventBase.keyCode;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.KeyDown), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag13 = eventBase.eventTypeId == EventBase<KeyUpEvent>.TypeId();
				if (flag13)
				{
					newEvent.type = EventType.KeyUp;
					newEvent.character = eventBase.character;
					newEvent.keyCode = eventBase.keyCode;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.KeyUp), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag14 = eventBase.eventTypeId == EventBase<ValidateCommandEvent>.TypeId();
				if (flag14)
				{
					newEvent.type = EventType.ValidateCommand;
					newEvent.commandName = eventBase.commandName;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ValidateCommand), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag15 = eventBase.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
				if (flag15)
				{
					newEvent.type = EventType.ExecuteCommand;
					newEvent.commandName = eventBase.commandName;
					this.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ExecuteCommand), this.panel, DispatchMode.Default);
					goto IL_868;
				}
				bool flag16 = eventBase.eventTypeId == EventBase<IMGUIEvent>.TypeId();
				if (flag16)
				{
					string str = "Skipped IMGUI event (";
					string eventBaseName = eventBase.eventBaseName;
					string str2 = "): ";
					EventDebuggerEventRecord eventDebuggerEventRecord = eventBase;
					Debug.Log(str + eventBaseName + str2 + ((eventDebuggerEventRecord != null) ? eventDebuggerEventRecord.ToString() : null));
					IEnumerator awaitSkipped = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
					while (awaitSkipped.MoveNext())
					{
						yield return null;
					}
				}
				else
				{
					string str3 = "Skipped event (";
					string eventBaseName2 = eventBase.eventBaseName;
					string str4 = "): ";
					EventDebuggerEventRecord eventDebuggerEventRecord2 = eventBase;
					Debug.Log(str3 + eventBaseName2 + str4 + ((eventDebuggerEventRecord2 != null) ? eventDebuggerEventRecord2.ToString() : null));
					IEnumerator awaitSkipped2 = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
					while (awaitSkipped2.MoveNext())
					{
						yield return null;
					}
				}
				IL_912:
				int num = i;
				i = num + 1;
				continue;
				IL_868:
				if (refreshList != null)
				{
					refreshList(i, sortedEventsCount);
				}
				Debug.Log(string.Format("Replayed event {0} ({1}): {2}", eventBase.eventId.ToString(), eventBase.eventBaseName, newEvent));
				IEnumerator await = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
				while (await.MoveNext())
				{
					yield return null;
				}
				eventBase = null;
				newEvent = null;
				await = null;
				goto IL_912;
			}
			this.isReplaying = false;
			yield break;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00043C90 File Offset: 0x00041E90
		public Dictionary<string, EventDebugger.HistogramRecord> ComputeHistogram(List<EventDebuggerEventRecord> eventBases)
		{
			List<EventDebuggerTrace> list;
			bool flag = this.panel == null || !this.m_EventProcessedEvents.TryGetValue(this.panel, out list);
			Dictionary<string, EventDebugger.HistogramRecord> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = list == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					Dictionary<string, EventDebugger.HistogramRecord> dictionary = new Dictionary<string, EventDebugger.HistogramRecord>();
					foreach (EventDebuggerTrace eventDebuggerTrace in list)
					{
						bool flag3 = eventBases == null || eventBases.Count == 0 || eventBases.Contains(eventDebuggerTrace.eventBase);
						if (flag3)
						{
							string eventBaseName = eventDebuggerTrace.eventBase.eventBaseName;
							long num = eventDebuggerTrace.duration;
							long num2 = 1L;
							EventDebugger.HistogramRecord histogramRecord;
							bool flag4 = dictionary.TryGetValue(eventBaseName, out histogramRecord);
							if (flag4)
							{
								num += histogramRecord.duration;
								num2 += histogramRecord.count;
							}
							dictionary[eventBaseName] = new EventDebugger.HistogramRecord
							{
								count = num2,
								duration = num
							};
						}
					}
					result = dictionary;
				}
			}
			return result;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00043DBC File Offset: 0x00041FBC
		public Dictionary<long, int> eventTypeProcessedCount
		{
			get
			{
				Dictionary<long, int> dictionary;
				return this.m_EventTypeProcessedCount.TryGetValue(this.panel, out dictionary) ? dictionary : null;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00043DE2 File Offset: 0x00041FE2
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x00043DEA File Offset: 0x00041FEA
		public bool suspended
		{
			[CompilerGenerated]
			get
			{
				return this.<suspended>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<suspended>k__BackingField = value;
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00043DF4 File Offset: 0x00041FF4
		public EventDebugger()
		{
			this.m_EventCalledObjects = new Dictionary<IPanel, List<EventDebuggerCallTrace>>();
			this.m_EventDefaultActionObjects = new Dictionary<IPanel, List<EventDebuggerDefaultActionTrace>>();
			this.m_EventPathObjects = new Dictionary<IPanel, List<EventDebuggerPathTrace>>();
			this.m_StackOfProcessedEvent = new Dictionary<IPanel, Stack<EventDebuggerTrace>>();
			this.m_EventProcessedEvents = new Dictionary<IPanel, List<EventDebuggerTrace>>();
			this.m_EventTypeProcessedCount = new Dictionary<IPanel, Dictionary<long, int>>();
			this.m_ModificationCount = new Dictionary<IPanel, long>();
			this.m_Log = true;
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00043E68 File Offset: 0x00042068
		private void AddCallObject(int cbHashCode, string cbName, EventBase evt, bool propagationHasStopped, bool immediatePropagationHasStopped, bool defaultHasBeenPrevented, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerCallTrace item = new EventDebuggerCallTrace(this.panel, evt, cbHashCode, cbName, propagationHasStopped, immediatePropagationHasStopped, defaultHasBeenPrevented, duration, mouseCapture);
					List<EventDebuggerCallTrace> list;
					bool flag = !this.m_EventCalledObjects.TryGetValue(this.panel, out list);
					if (flag)
					{
						list = new List<EventDebuggerCallTrace>();
						this.m_EventCalledObjects.Add(this.panel, list);
					}
					list.Add(item);
				}
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00043EE8 File Offset: 0x000420E8
		private void AddExecuteDefaultAction(EventBase evt, PropagationPhase phase, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerDefaultActionTrace item = new EventDebuggerDefaultActionTrace(this.panel, evt, phase, duration, mouseCapture);
					List<EventDebuggerDefaultActionTrace> list;
					bool flag = !this.m_EventDefaultActionObjects.TryGetValue(this.panel, out list);
					if (flag)
					{
						list = new List<EventDebuggerDefaultActionTrace>();
						this.m_EventDefaultActionObjects.Add(this.panel, list);
					}
					list.Add(item);
				}
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00043F60 File Offset: 0x00042160
		private void AddPropagationPaths(EventBase evt, PropagationPaths paths)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerPathTrace item = new EventDebuggerPathTrace(this.panel, evt, paths);
					List<EventDebuggerPathTrace> list;
					bool flag = !this.m_EventPathObjects.TryGetValue(this.panel, out list);
					if (flag)
					{
						list = new List<EventDebuggerPathTrace>();
						this.m_EventPathObjects.Add(this.panel, list);
					}
					list.Add(item);
				}
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00043FD4 File Offset: 0x000421D4
		private void AddIMGUICall(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool log = this.m_Log;
				if (log)
				{
					EventDebuggerCallTrace item = new EventDebuggerCallTrace(this.panel, evt, 0, "OnGUI", false, false, false, duration, mouseCapture);
					List<EventDebuggerCallTrace> list;
					bool flag = !this.m_EventCalledObjects.TryGetValue(this.panel, out list);
					if (flag)
					{
						list = new List<EventDebuggerCallTrace>();
						this.m_EventCalledObjects.Add(this.panel, list);
					}
					list.Add(item);
				}
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00044054 File Offset: 0x00042254
		private void AddBeginProcessEvent(EventBase evt, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				EventDebuggerTrace eventDebuggerTrace = new EventDebuggerTrace(this.panel, evt, -1L, mouseCapture);
				Stack<EventDebuggerTrace> stack;
				bool flag = !this.m_StackOfProcessedEvent.TryGetValue(this.panel, out stack);
				if (flag)
				{
					stack = new Stack<EventDebuggerTrace>();
					this.m_StackOfProcessedEvent.Add(this.panel, stack);
				}
				List<EventDebuggerTrace> list;
				bool flag2 = !this.m_EventProcessedEvents.TryGetValue(this.panel, out list);
				if (flag2)
				{
					list = new List<EventDebuggerTrace>();
					this.m_EventProcessedEvents.Add(this.panel, list);
				}
				list.Add(eventDebuggerTrace);
				stack.Push(eventDebuggerTrace);
				Dictionary<long, int> dictionary;
				bool flag3 = !this.m_EventTypeProcessedCount.TryGetValue(this.panel, out dictionary);
				if (!flag3)
				{
					int num;
					bool flag4 = !dictionary.TryGetValue(eventDebuggerTrace.eventBase.eventTypeId, out num);
					if (flag4)
					{
						num = 0;
					}
					dictionary[eventDebuggerTrace.eventBase.eventTypeId] = num + 1;
				}
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00044154 File Offset: 0x00042354
		private void AddEndProcessEvent(EventBase evt, long duration, IEventHandler mouseCapture)
		{
			bool suspended = this.suspended;
			if (!suspended)
			{
				bool flag = false;
				Stack<EventDebuggerTrace> stack;
				bool flag2 = this.m_StackOfProcessedEvent.TryGetValue(this.panel, out stack);
				if (flag2)
				{
					bool flag3 = stack.Count > 0;
					if (flag3)
					{
						EventDebuggerTrace eventDebuggerTrace = stack.Peek();
						bool flag4 = eventDebuggerTrace.eventBase.eventId == evt.eventId;
						if (flag4)
						{
							stack.Pop();
							eventDebuggerTrace.duration = duration;
							bool flag5 = eventDebuggerTrace.eventBase.target == null;
							if (flag5)
							{
								eventDebuggerTrace.eventBase.target = evt.target;
							}
							flag = true;
						}
					}
				}
				bool flag6 = !flag;
				if (flag6)
				{
					EventDebuggerTrace eventDebuggerTrace2 = new EventDebuggerTrace(this.panel, evt, duration, mouseCapture);
					List<EventDebuggerTrace> list;
					bool flag7 = !this.m_EventProcessedEvents.TryGetValue(this.panel, out list);
					if (flag7)
					{
						list = new List<EventDebuggerTrace>();
						this.m_EventProcessedEvents.Add(this.panel, list);
					}
					list.Add(eventDebuggerTrace2);
					Dictionary<long, int> dictionary;
					bool flag8 = !this.m_EventTypeProcessedCount.TryGetValue(this.panel, out dictionary);
					if (!flag8)
					{
						int num;
						bool flag9 = !dictionary.TryGetValue(eventDebuggerTrace2.eventBase.eventTypeId, out num);
						if (flag9)
						{
							num = 0;
						}
						dictionary[eventDebuggerTrace2.eventBase.eventTypeId] = num + 1;
					}
				}
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000442B8 File Offset: 0x000424B8
		public static string GetObjectDisplayName(object obj, bool withHashCode = true)
		{
			bool flag = obj == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				Type type = obj.GetType();
				string text = EventDebugger.GetTypeDisplayName(type);
				bool flag2 = obj is VisualElement;
				if (flag2)
				{
					VisualElement visualElement = obj as VisualElement;
					bool flag3 = !string.IsNullOrEmpty(visualElement.name);
					if (flag3)
					{
						text = text + "#" + visualElement.name;
					}
				}
				if (withHashCode)
				{
					text = text + " (" + obj.GetHashCode().ToString("x8") + ")";
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00044360 File Offset: 0x00042560
		public static string GetTypeDisplayName(Type type)
		{
			return type.IsGenericType ? (type.Name.TrimEnd(new char[]
			{
				'`',
				'1'
			}) + "<" + type.GetGenericArguments()[0].Name + ">") : type.Name;
		}

		// Token: 0x04000777 RID: 1911
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IPanel <panel>k__BackingField;

		// Token: 0x04000778 RID: 1912
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isReplaying>k__BackingField;

		// Token: 0x04000779 RID: 1913
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <playbackSpeed>k__BackingField;

		// Token: 0x0400077A RID: 1914
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isPlaybackPaused>k__BackingField;

		// Token: 0x0400077B RID: 1915
		private Dictionary<IPanel, List<EventDebuggerCallTrace>> m_EventCalledObjects;

		// Token: 0x0400077C RID: 1916
		private Dictionary<IPanel, List<EventDebuggerDefaultActionTrace>> m_EventDefaultActionObjects;

		// Token: 0x0400077D RID: 1917
		private Dictionary<IPanel, List<EventDebuggerPathTrace>> m_EventPathObjects;

		// Token: 0x0400077E RID: 1918
		private Dictionary<IPanel, List<EventDebuggerTrace>> m_EventProcessedEvents;

		// Token: 0x0400077F RID: 1919
		private Dictionary<IPanel, Stack<EventDebuggerTrace>> m_StackOfProcessedEvent;

		// Token: 0x04000780 RID: 1920
		private Dictionary<IPanel, Dictionary<long, int>> m_EventTypeProcessedCount;

		// Token: 0x04000781 RID: 1921
		private readonly Dictionary<IPanel, long> m_ModificationCount;

		// Token: 0x04000782 RID: 1922
		private readonly bool m_Log;

		// Token: 0x04000783 RID: 1923
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <suspended>k__BackingField;

		// Token: 0x02000238 RID: 568
		internal struct HistogramRecord
		{
			// Token: 0x04000784 RID: 1924
			public long count;

			// Token: 0x04000785 RID: 1925
			public long duration;
		}

		// Token: 0x02000239 RID: 569
		[CompilerGenerated]
		private sealed class <ReplayEvents>d__32 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06001133 RID: 4403 RVA: 0x000443B9 File Offset: 0x000425B9
			[DebuggerHidden]
			public <ReplayEvents>d__32(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06001134 RID: 4404 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001135 RID: 4405 RVA: 0x000443CC File Offset: 0x000425CC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
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
					bool flag = eventBases == null;
					if (flag)
					{
						return false;
					}
					base.isReplaying = true;
					doReplay = base.DoReplayEvents(eventBases, refreshList);
				}
				if (!doReplay.MoveNext())
				{
					return false;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170003BD RID: 957
			// (get) Token: 0x06001136 RID: 4406 RVA: 0x0004445D File Offset: 0x0004265D
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001137 RID: 4407 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003BE RID: 958
			// (get) Token: 0x06001138 RID: 4408 RVA: 0x0004445D File Offset: 0x0004265D
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000786 RID: 1926
			private int <>1__state;

			// Token: 0x04000787 RID: 1927
			private object <>2__current;

			// Token: 0x04000788 RID: 1928
			public IEnumerable<EventDebuggerEventRecord> eventBases;

			// Token: 0x04000789 RID: 1929
			public Action<int, int> refreshList;

			// Token: 0x0400078A RID: 1930
			public EventDebugger <>4__this;

			// Token: 0x0400078B RID: 1931
			private IEnumerator <doReplay>5__1;
		}

		// Token: 0x0200023A RID: 570
		[CompilerGenerated]
		private sealed class <>c__DisplayClass34_0
		{
			// Token: 0x06001139 RID: 4409 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass34_0()
			{
			}

			// Token: 0x0600113A RID: 4410 RVA: 0x00044468 File Offset: 0x00042668
			internal IEnumerator <DoReplayEvents>g__AwaitForNextEvent|1(int currentIndex)
			{
				EventDebugger.<>c__DisplayClass34_0.<<DoReplayEvents>g__AwaitForNextEvent|1>d <<DoReplayEvents>g__AwaitForNextEvent|1>d = new EventDebugger.<>c__DisplayClass34_0.<<DoReplayEvents>g__AwaitForNextEvent|1>d(0);
				<<DoReplayEvents>g__AwaitForNextEvent|1>d.<>4__this = this;
				<<DoReplayEvents>g__AwaitForNextEvent|1>d.currentIndex = currentIndex;
				return <<DoReplayEvents>g__AwaitForNextEvent|1>d;
			}

			// Token: 0x0400078C RID: 1932
			public List<EventDebuggerEventRecord> sortedEvents;

			// Token: 0x0400078D RID: 1933
			public EventDebugger <>4__this;

			// Token: 0x0200023B RID: 571
			private sealed class <<DoReplayEvents>g__AwaitForNextEvent|1>d : IEnumerator<object>, IEnumerator, IDisposable
			{
				// Token: 0x0600113B RID: 4411 RVA: 0x0004448D File Offset: 0x0004268D
				[DebuggerHidden]
				public <<DoReplayEvents>g__AwaitForNextEvent|1>d(int <>1__state)
				{
					this.<>1__state = <>1__state;
				}

				// Token: 0x0600113C RID: 4412 RVA: 0x000080DB File Offset: 0x000062DB
				[DebuggerHidden]
				void IDisposable.Dispose()
				{
				}

				// Token: 0x0600113D RID: 4413 RVA: 0x000444A0 File Offset: 0x000426A0
				bool IEnumerator.MoveNext()
				{
					switch (this.<>1__state)
					{
					case 0:
					{
						this.<>1__state = -1;
						bool flag = this.currentIndex == this.<>4__this.sortedEvents.Count - 1;
						if (flag)
						{
							return false;
						}
						this.<deltaTimestampMs>5__1 = this.<>4__this.sortedEvents[this.currentIndex + 1].timestamp - this.<>4__this.sortedEvents[this.currentIndex].timestamp;
						this.<timeMs>5__2 = 0f;
						break;
					}
					case 1:
						this.<>1__state = -1;
						break;
					case 2:
						this.<>1__state = -1;
						this.<delta>5__4 = Panel.TimeSinceStartupMs() - this.<time>5__3;
						this.<timeMs>5__2 += (float)this.<delta>5__4 * this.<>4__this.<>4__this.playbackSpeed;
						break;
					default:
						return false;
					}
					if (this.<timeMs>5__2 >= (float)this.<deltaTimestampMs>5__1)
					{
						return false;
					}
					bool isPlaybackPaused = this.<>4__this.<>4__this.isPlaybackPaused;
					if (isPlaybackPaused)
					{
						this.<>2__current = null;
						this.<>1__state = 1;
						return true;
					}
					this.<time>5__3 = Panel.TimeSinceStartupMs();
					this.<>2__current = null;
					this.<>1__state = 2;
					return true;
				}

				// Token: 0x170003BF RID: 959
				// (get) Token: 0x0600113E RID: 4414 RVA: 0x000445F0 File Offset: 0x000427F0
				object IEnumerator<object>.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x0600113F RID: 4415 RVA: 0x0000810E File Offset: 0x0000630E
				[DebuggerHidden]
				void IEnumerator.Reset()
				{
					throw new NotSupportedException();
				}

				// Token: 0x170003C0 RID: 960
				// (get) Token: 0x06001140 RID: 4416 RVA: 0x000445F0 File Offset: 0x000427F0
				object IEnumerator.Current
				{
					[DebuggerHidden]
					get
					{
						return this.<>2__current;
					}
				}

				// Token: 0x0400078E RID: 1934
				private int <>1__state;

				// Token: 0x0400078F RID: 1935
				private object <>2__current;

				// Token: 0x04000790 RID: 1936
				public int currentIndex;

				// Token: 0x04000791 RID: 1937
				public EventDebugger.<>c__DisplayClass34_0 <>4__this;

				// Token: 0x04000792 RID: 1938
				private long <deltaTimestampMs>5__1;

				// Token: 0x04000793 RID: 1939
				private float <timeMs>5__2;

				// Token: 0x04000794 RID: 1940
				private long <time>5__3;

				// Token: 0x04000795 RID: 1941
				private long <delta>5__4;
			}
		}

		// Token: 0x0200023C RID: 572
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001141 RID: 4417 RVA: 0x000445F8 File Offset: 0x000427F8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001142 RID: 4418 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001143 RID: 4419 RVA: 0x00044604 File Offset: 0x00042804
			internal long <DoReplayEvents>b__34_0(EventDebuggerEventRecord e)
			{
				return e.timestamp;
			}

			// Token: 0x04000796 RID: 1942
			public static readonly EventDebugger.<>c <>9 = new EventDebugger.<>c();

			// Token: 0x04000797 RID: 1943
			public static Func<EventDebuggerEventRecord, long> <>9__34_0;
		}

		// Token: 0x0200023D RID: 573
		[CompilerGenerated]
		private sealed class <DoReplayEvents>d__34 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x06001144 RID: 4420 RVA: 0x0004460C File Offset: 0x0004280C
			[DebuggerHidden]
			public <DoReplayEvents>d__34(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06001145 RID: 4421 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06001146 RID: 4422 RVA: 0x0004461C File Offset: 0x0004281C
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					CS$<>8__locals1 = new EventDebugger.<>c__DisplayClass34_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.sortedEvents = eventBases.OrderBy(new Func<EventDebuggerEventRecord, long>(EventDebugger.<>c.<>9.<DoReplayEvents>b__34_0)).ToList<EventDebuggerEventRecord>();
					sortedEventsCount = CS$<>8__locals1.sortedEvents.Count;
					i = 0;
					goto IL_924;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_852;
				case 3:
					this.<>1__state = -1;
					goto IL_8EB;
				default:
					return false;
				}
				IL_7D9:
				if (!awaitSkipped.MoveNext())
				{
					goto IL_912;
				}
				this.<>2__current = null;
				this.<>1__state = 1;
				return true;
				IL_852:
				if (!awaitSkipped2.MoveNext())
				{
					goto IL_912;
				}
				this.<>2__current = null;
				this.<>1__state = 2;
				return true;
				IL_8EB:
				if (await.MoveNext())
				{
					this.<>2__current = null;
					this.<>1__state = 3;
					return true;
				}
				eventBase = null;
				newEvent = null;
				await = null;
				IL_912:
				int num = i;
				i = num + 1;
				IL_924:
				if (i < sortedEventsCount)
				{
					bool flag = !base.isReplaying;
					if (!flag)
					{
						eventBase = CS$<>8__locals1.sortedEvents[i];
						newEvent = new Event
						{
							button = eventBase.button,
							clickCount = eventBase.clickCount,
							modifiers = eventBase.modifiers,
							mousePosition = eventBase.mousePosition
						};
						bool flag2 = eventBase.eventTypeId == EventBase<MouseMoveEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
						if (flag2)
						{
							newEvent.type = EventType.MouseMove;
							base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseMove), base.panel, DispatchMode.Default);
						}
						else
						{
							bool flag3 = eventBase.eventTypeId == EventBase<MouseDownEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
							if (flag3)
							{
								newEvent.type = EventType.MouseDown;
								base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseDown), base.panel, DispatchMode.Default);
							}
							else
							{
								bool flag4 = eventBase.eventTypeId == EventBase<MouseUpEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
								if (flag4)
								{
									newEvent.type = EventType.MouseUp;
									base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseUp), base.panel, DispatchMode.Default);
								}
								else
								{
									bool flag5 = eventBase.eventTypeId == EventBase<ContextClickEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
									if (flag5)
									{
										newEvent.type = EventType.ContextClick;
										base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ContextClick), base.panel, DispatchMode.Default);
									}
									else
									{
										bool flag6 = eventBase.eventTypeId == EventBase<MouseEnterWindowEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
										if (flag6)
										{
											newEvent.type = EventType.MouseEnterWindow;
											base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseEnterWindow), base.panel, DispatchMode.Default);
										}
										else
										{
											bool flag7 = eventBase.eventTypeId == EventBase<MouseLeaveWindowEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
											if (flag7)
											{
												newEvent.type = EventType.MouseLeaveWindow;
												base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseLeaveWindow), base.panel, DispatchMode.Default);
											}
											else
											{
												bool flag8 = eventBase.eventTypeId == EventBase<PointerMoveEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
												if (flag8)
												{
													newEvent.type = EventType.MouseMove;
													base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseMove), base.panel, DispatchMode.Default);
												}
												else
												{
													bool flag9 = eventBase.eventTypeId == EventBase<PointerDownEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
													if (flag9)
													{
														newEvent.type = EventType.MouseDown;
														base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseDown), base.panel, DispatchMode.Default);
													}
													else
													{
														bool flag10 = eventBase.eventTypeId == EventBase<PointerUpEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
														if (flag10)
														{
															newEvent.type = EventType.MouseUp;
															base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.MouseUp), base.panel, DispatchMode.Default);
														}
														else
														{
															bool flag11 = eventBase.eventTypeId == EventBase<WheelEvent>.TypeId() && eventBase.hasUnderlyingPhysicalEvent;
															if (flag11)
															{
																newEvent.type = EventType.ScrollWheel;
																newEvent.delta = eventBase.delta;
																base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ScrollWheel), base.panel, DispatchMode.Default);
															}
															else
															{
																bool flag12 = eventBase.eventTypeId == EventBase<KeyDownEvent>.TypeId();
																if (flag12)
																{
																	newEvent.type = EventType.KeyDown;
																	newEvent.character = eventBase.character;
																	newEvent.keyCode = eventBase.keyCode;
																	base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.KeyDown), base.panel, DispatchMode.Default);
																}
																else
																{
																	bool flag13 = eventBase.eventTypeId == EventBase<KeyUpEvent>.TypeId();
																	if (flag13)
																	{
																		newEvent.type = EventType.KeyUp;
																		newEvent.character = eventBase.character;
																		newEvent.keyCode = eventBase.keyCode;
																		base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.KeyUp), base.panel, DispatchMode.Default);
																	}
																	else
																	{
																		bool flag14 = eventBase.eventTypeId == EventBase<ValidateCommandEvent>.TypeId();
																		if (flag14)
																		{
																			newEvent.type = EventType.ValidateCommand;
																			newEvent.commandName = eventBase.commandName;
																			base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ValidateCommand), base.panel, DispatchMode.Default);
																		}
																		else
																		{
																			bool flag15 = eventBase.eventTypeId == EventBase<ExecuteCommandEvent>.TypeId();
																			if (flag15)
																			{
																				newEvent.type = EventType.ExecuteCommand;
																				newEvent.commandName = eventBase.commandName;
																				base.panel.dispatcher.Dispatch(UIElementsUtility.CreateEvent(newEvent, EventType.ExecuteCommand), base.panel, DispatchMode.Default);
																			}
																			else
																			{
																				bool flag16 = eventBase.eventTypeId == EventBase<IMGUIEvent>.TypeId();
																				if (flag16)
																				{
																					string str = "Skipped IMGUI event (";
																					string eventBaseName = eventBase.eventBaseName;
																					string str2 = "): ";
																					EventDebuggerEventRecord eventDebuggerEventRecord = eventBase;
																					Debug.Log(str + eventBaseName + str2 + ((eventDebuggerEventRecord != null) ? eventDebuggerEventRecord.ToString() : null));
																					awaitSkipped = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
																					goto IL_7D9;
																				}
																				string str3 = "Skipped event (";
																				string eventBaseName2 = eventBase.eventBaseName;
																				string str4 = "): ";
																				EventDebuggerEventRecord eventDebuggerEventRecord2 = eventBase;
																				Debug.Log(str3 + eventBaseName2 + str4 + ((eventDebuggerEventRecord2 != null) ? eventDebuggerEventRecord2.ToString() : null));
																				awaitSkipped2 = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
																				goto IL_852;
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						Action<int, int> action = refreshList;
						if (action != null)
						{
							action(i, sortedEventsCount);
						}
						Debug.Log(string.Format("Replayed event {0} ({1}): {2}", eventBase.eventId.ToString(), eventBase.eventBaseName, newEvent));
						await = CS$<>8__locals1.<DoReplayEvents>g__AwaitForNextEvent|1(i);
						goto IL_8EB;
					}
				}
				base.isReplaying = false;
				return false;
			}

			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x06001147 RID: 4423 RVA: 0x00044F72 File Offset: 0x00043172
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001148 RID: 4424 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x06001149 RID: 4425 RVA: 0x00044F72 File Offset: 0x00043172
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04000798 RID: 1944
			private int <>1__state;

			// Token: 0x04000799 RID: 1945
			private object <>2__current;

			// Token: 0x0400079A RID: 1946
			public IEnumerable<EventDebuggerEventRecord> eventBases;

			// Token: 0x0400079B RID: 1947
			public Action<int, int> refreshList;

			// Token: 0x0400079C RID: 1948
			public EventDebugger <>4__this;

			// Token: 0x0400079D RID: 1949
			private EventDebugger.<>c__DisplayClass34_0 <>8__1;

			// Token: 0x0400079E RID: 1950
			private int <sortedEventsCount>5__2;

			// Token: 0x0400079F RID: 1951
			private int <i>5__3;

			// Token: 0x040007A0 RID: 1952
			private EventDebuggerEventRecord <eventBase>5__4;

			// Token: 0x040007A1 RID: 1953
			private Event <newEvent>5__5;

			// Token: 0x040007A2 RID: 1954
			private IEnumerator <await>5__6;

			// Token: 0x040007A3 RID: 1955
			private IEnumerator <awaitSkipped>5__7;

			// Token: 0x040007A4 RID: 1956
			private IEnumerator <awaitSkipped>5__8;
		}
	}
}
