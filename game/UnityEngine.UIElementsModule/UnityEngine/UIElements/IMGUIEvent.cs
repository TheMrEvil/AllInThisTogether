using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000233 RID: 563
	public class IMGUIEvent : EventBase<IMGUIEvent>
	{
		// Token: 0x06001101 RID: 4353 RVA: 0x00043660 File Offset: 0x00041860
		public static IMGUIEvent GetPooled(Event systemEvent)
		{
			IMGUIEvent pooled = EventBase<IMGUIEvent>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00043681 File Offset: 0x00041881
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00040BE5 File Offset: 0x0003EDE5
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00043692 File Offset: 0x00041892
		public IMGUIEvent()
		{
			this.LocalInit();
		}
	}
}
