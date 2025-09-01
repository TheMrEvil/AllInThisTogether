using System;
using System.Collections.Concurrent;

namespace QFSW.QC.Pooling
{
	// Token: 0x0200005E RID: 94
	public class ConcurrentPool<T> : IPool<T> where T : class, new()
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x00009C31 File Offset: 0x00007E31
		public ConcurrentPool()
		{
			this._objs = new ConcurrentBag<T>();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009C44 File Offset: 0x00007E44
		public ConcurrentPool(int objCount)
		{
			this._objs = new ConcurrentBag<T>();
			for (int i = 0; i < objCount; i++)
			{
				this._objs.Add(Activator.CreateInstance<T>());
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009C80 File Offset: 0x00007E80
		public T GetObject()
		{
			T result;
			if (this._objs.TryTake(out result))
			{
				return result;
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00009CA3 File Offset: 0x00007EA3
		public void Release(T obj)
		{
			this._objs.Add(obj);
		}

		// Token: 0x0400013F RID: 319
		private readonly ConcurrentBag<T> _objs;
	}
}
