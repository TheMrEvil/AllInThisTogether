using System;

namespace Fluxy
{
	// Token: 0x0200000A RID: 10
	public class ExecutionOrder : Attribute
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002480 File Offset: 0x00000680
		public ExecutionOrder(int order)
		{
			this.order = order;
		}

		// Token: 0x04000018 RID: 24
		public int order;
	}
}
