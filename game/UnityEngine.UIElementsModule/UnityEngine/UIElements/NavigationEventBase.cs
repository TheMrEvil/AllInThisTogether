using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000208 RID: 520
	public abstract class NavigationEventBase<T> : EventBase<T>, INavigationEvent where T : NavigationEventBase<T>, new()
	{
		// Token: 0x06001022 RID: 4130 RVA: 0x000416EF File Offset: 0x0003F8EF
		protected NavigationEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00041700 File Offset: 0x0003F900
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00041711 File Offset: 0x0003F911
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable | EventBase.EventPropagation.SkipDisabledElements);
			base.propagateToIMGUI = false;
		}
	}
}
