using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CD RID: 461
	internal class DebuggerEventDispatchingStrategy : IEventDispatchingStrategy
	{
		// Token: 0x06000EA1 RID: 3745 RVA: 0x0003D3A4 File Offset: 0x0003B5A4
		public bool CanDispatchEvent(EventBase evt)
		{
			return false;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00002166 File Offset: 0x00000366
		public void DispatchEvent(EventBase evt, IPanel panel)
		{
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00002166 File Offset: 0x00000366
		public void PostDispatch(EventBase evt, IPanel panel)
		{
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000020C2 File Offset: 0x000002C2
		public DebuggerEventDispatchingStrategy()
		{
		}
	}
}
