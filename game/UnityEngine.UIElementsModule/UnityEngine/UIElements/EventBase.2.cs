using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D3 RID: 467
	public abstract class EventBase<T> : EventBase where T : EventBase<T>, new()
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003E421 File Offset: 0x0003C621
		protected EventBase()
		{
			this.m_RefCount = 0;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003E434 File Offset: 0x0003C634
		public static long TypeId()
		{
			return EventBase<T>.s_TypeId;
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003E44C File Offset: 0x0003C64C
		protected override void Init()
		{
			base.Init();
			bool flag = this.m_RefCount != 0;
			if (flag)
			{
				Debug.Log("Event improperly released.");
				this.m_RefCount = 0;
			}
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0003E484 File Offset: 0x0003C684
		public static T GetPooled()
		{
			T t = EventBase<T>.s_Pool.Get();
			t.Init();
			t.pooled = true;
			t.Acquire();
			return t;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0003E4C8 File Offset: 0x0003C6C8
		internal static T GetPooled(EventBase e)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = e != null;
			if (flag)
			{
				pooled.SetTriggerEventId(e.eventId);
			}
			return pooled;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0003E500 File Offset: 0x0003C700
		private static void ReleasePooled(T evt)
		{
			bool pooled = evt.pooled;
			if (pooled)
			{
				evt.Init();
				EventBase<T>.s_Pool.Release(evt);
				evt.pooled = false;
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0003E544 File Offset: 0x0003C744
		internal override void Acquire()
		{
			this.m_RefCount++;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003E558 File Offset: 0x0003C758
		public sealed override void Dispose()
		{
			int num = this.m_RefCount - 1;
			this.m_RefCount = num;
			bool flag = num == 0;
			if (flag)
			{
				EventBase<T>.ReleasePooled((T)((object)this));
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0003E58C File Offset: 0x0003C78C
		public override long eventTypeId
		{
			get
			{
				return EventBase<T>.s_TypeId;
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003E593 File Offset: 0x0003C793
		// Note: this type is marked as 'beforefieldinit'.
		static EventBase()
		{
		}

		// Token: 0x040006ED RID: 1773
		private static readonly long s_TypeId = EventBase.RegisterEventType();

		// Token: 0x040006EE RID: 1774
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(100);

		// Token: 0x040006EF RID: 1775
		private int m_RefCount;
	}
}
