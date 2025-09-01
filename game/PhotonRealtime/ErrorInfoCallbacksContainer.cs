using System;
using System.Collections.Generic;

namespace Photon.Realtime
{
	// Token: 0x0200001B RID: 27
	internal class ErrorInfoCallbacksContainer : List<IErrorInfoCallback>, IErrorInfoCallback
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00007000 File Offset: 0x00005200
		public ErrorInfoCallbacksContainer(LoadBalancingClient client)
		{
			this.client = client;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007010 File Offset: 0x00005210
		public void OnErrorInfo(ErrorInfo errorInfo)
		{
			this.client.UpdateCallbackTargets();
			foreach (IErrorInfoCallback errorInfoCallback in this)
			{
				errorInfoCallback.OnErrorInfo(errorInfo);
			}
		}

		// Token: 0x040000AF RID: 175
		private LoadBalancingClient client;
	}
}
