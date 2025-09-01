using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000384 RID: 900
	public struct PooledObject<T> : IDisposable where T : class
	{
		// Token: 0x06001EC0 RID: 7872 RVA: 0x00032082 File Offset: 0x00030282
		public PooledObject(T value, IObjectPool<T> pool)
		{
			this.m_ToReturn = value;
			this.m_Pool = pool;
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00032093 File Offset: 0x00030293
		void IDisposable.Dispose()
		{
			this.m_Pool.Release(this.m_ToReturn);
		}

		// Token: 0x04000A19 RID: 2585
		private readonly T m_ToReturn;

		// Token: 0x04000A1A RID: 2586
		private readonly IObjectPool<T> m_Pool;
	}
}
