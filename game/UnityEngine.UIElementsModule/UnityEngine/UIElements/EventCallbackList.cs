using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DD RID: 477
	internal class EventCallbackList
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0003E8A3 File Offset: 0x0003CAA3
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0003E8AB File Offset: 0x0003CAAB
		public int trickleDownCallbackCount
		{
			[CompilerGenerated]
			get
			{
				return this.<trickleDownCallbackCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<trickleDownCallbackCount>k__BackingField = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0003E8B4 File Offset: 0x0003CAB4
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x0003E8BC File Offset: 0x0003CABC
		public int bubbleUpCallbackCount
		{
			[CompilerGenerated]
			get
			{
				return this.<bubbleUpCallbackCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<bubbleUpCallbackCount>k__BackingField = value;
			}
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003E8C5 File Offset: 0x0003CAC5
		public EventCallbackList()
		{
			this.m_List = new List<EventCallbackFunctorBase>();
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003E8EA File Offset: 0x0003CAEA
		public EventCallbackList(EventCallbackList source)
		{
			this.m_List = new List<EventCallbackFunctorBase>(source.m_List);
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003E918 File Offset: 0x0003CB18
		public bool Contains(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.Find(eventTypeId, callback, phase) != null;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003E938 File Offset: 0x0003CB38
		public EventCallbackFunctorBase Find(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			for (int i = 0; i < this.m_List.Count; i++)
			{
				bool flag = this.m_List[i].IsEquivalentTo(eventTypeId, callback, phase);
				if (flag)
				{
					return this.m_List[i];
				}
			}
			return null;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003E990 File Offset: 0x0003CB90
		public bool Remove(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			for (int i = 0; i < this.m_List.Count; i++)
			{
				bool flag = this.m_List[i].IsEquivalentTo(eventTypeId, callback, phase);
				if (flag)
				{
					this.m_List.RemoveAt(i);
					bool flag2 = phase == CallbackPhase.TrickleDownAndTarget;
					if (flag2)
					{
						int num = this.trickleDownCallbackCount;
						this.trickleDownCallbackCount = num - 1;
					}
					else
					{
						bool flag3 = phase == CallbackPhase.TargetAndBubbleUp;
						if (flag3)
						{
							int num = this.bubbleUpCallbackCount;
							this.bubbleUpCallbackCount = num - 1;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003EA28 File Offset: 0x0003CC28
		public void Add(EventCallbackFunctorBase item)
		{
			this.m_List.Add(item);
			bool flag = item.phase == CallbackPhase.TrickleDownAndTarget;
			if (flag)
			{
				int num = this.trickleDownCallbackCount;
				this.trickleDownCallbackCount = num + 1;
			}
			else
			{
				bool flag2 = item.phase == CallbackPhase.TargetAndBubbleUp;
				if (flag2)
				{
					int num = this.bubbleUpCallbackCount;
					this.bubbleUpCallbackCount = num + 1;
				}
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003EA88 File Offset: 0x0003CC88
		public void AddRange(EventCallbackList list)
		{
			this.m_List.AddRange(list.m_List);
			foreach (EventCallbackFunctorBase eventCallbackFunctorBase in list.m_List)
			{
				bool flag = eventCallbackFunctorBase.phase == CallbackPhase.TrickleDownAndTarget;
				if (flag)
				{
					int num = this.trickleDownCallbackCount;
					this.trickleDownCallbackCount = num + 1;
				}
				else
				{
					bool flag2 = eventCallbackFunctorBase.phase == CallbackPhase.TargetAndBubbleUp;
					if (flag2)
					{
						int num = this.bubbleUpCallbackCount;
						this.bubbleUpCallbackCount = num + 1;
					}
				}
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0003EB30 File Offset: 0x0003CD30
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x17000334 RID: 820
		public EventCallbackFunctorBase this[int i]
		{
			get
			{
				return this.m_List[i];
			}
			set
			{
				this.m_List[i] = value;
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0003EB7F File Offset: 0x0003CD7F
		public void Clear()
		{
			this.m_List.Clear();
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x04000701 RID: 1793
		private List<EventCallbackFunctorBase> m_List;

		// Token: 0x04000702 RID: 1794
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <trickleDownCallbackCount>k__BackingField;

		// Token: 0x04000703 RID: 1795
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <bubbleUpCallbackCount>k__BackingField;
	}
}
