using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000380 RID: 896
	public interface IObjectPool<T> where T : class
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001EA8 RID: 7848
		int CountInactive { get; }

		// Token: 0x06001EA9 RID: 7849
		T Get();

		// Token: 0x06001EAA RID: 7850
		PooledObject<T> Get(out T v);

		// Token: 0x06001EAB RID: 7851
		void Release(T element);

		// Token: 0x06001EAC RID: 7852
		void Clear();
	}
}
