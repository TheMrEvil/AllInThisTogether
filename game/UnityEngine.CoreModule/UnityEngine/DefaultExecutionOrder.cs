using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F8 RID: 504
	[UsedByNativeCode]
	[AttributeUsage(AttributeTargets.Class)]
	public class DefaultExecutionOrder : Attribute
	{
		// Token: 0x06001671 RID: 5745 RVA: 0x00023FC5 File Offset: 0x000221C5
		public DefaultExecutionOrder(int order)
		{
			this.m_Order = order;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x00023FD8 File Offset: 0x000221D8
		public int order
		{
			get
			{
				return this.m_Order;
			}
		}

		// Token: 0x040007DD RID: 2013
		private int m_Order;
	}
}
