using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Threading.Tasks
{
	// Token: 0x0200034D RID: 845
	internal interface IProducerConsumerQueue<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06002359 RID: 9049
		void Enqueue(T item);

		// Token: 0x0600235A RID: 9050
		bool TryDequeue(out T result);

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600235B RID: 9051
		bool IsEmpty { get; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600235C RID: 9052
		int Count { get; }

		// Token: 0x0600235D RID: 9053
		int GetCountSafe(object syncObj);
	}
}
