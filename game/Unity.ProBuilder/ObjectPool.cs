using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200002D RID: 45
	internal sealed class ObjectPool<T> : IDisposable
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0001603C File Offset: 0x0001423C
		public ObjectPool(int initialSize, int desiredSize, Func<T> constructor, Action<T> destructor, bool lazyInitialization = false)
		{
			if (constructor == null)
			{
				throw new ArgumentNullException("constructor");
			}
			if (destructor == null)
			{
				throw new ArgumentNullException("destructor");
			}
			this.constructor = constructor;
			this.destructor = destructor;
			this.desiredSize = desiredSize;
			int num = 0;
			while (num < initialSize && num < desiredSize && !lazyInitialization)
			{
				this.m_Pool.Enqueue(constructor());
				num++;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000160B2 File Offset: 0x000142B2
		public T Dequeue()
		{
			if (this.m_Pool.Count > 0)
			{
				return this.m_Pool.Dequeue();
			}
			return this.constructor();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000160D9 File Offset: 0x000142D9
		public void Enqueue(T obj)
		{
			if (this.m_Pool.Count < this.desiredSize)
			{
				this.m_Pool.Enqueue(obj);
				return;
			}
			this.destructor(obj);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00016108 File Offset: 0x00014308
		public void Empty()
		{
			int count = this.m_Pool.Count;
			for (int i = 0; i < count; i++)
			{
				this.destructor(this.m_Pool.Dequeue());
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00016143 File Offset: 0x00014343
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0001614C File Offset: 0x0001434C
		private void Dispose(bool disposing)
		{
			if (disposing && !this.m_IsDisposed)
			{
				this.Empty();
				this.m_IsDisposed = true;
			}
		}

		// Token: 0x04000095 RID: 149
		private bool m_IsDisposed;

		// Token: 0x04000096 RID: 150
		private Queue<T> m_Pool = new Queue<T>();

		// Token: 0x04000097 RID: 151
		public int desiredSize;

		// Token: 0x04000098 RID: 152
		public Func<T> constructor;

		// Token: 0x04000099 RID: 153
		public Action<T> destructor;
	}
}
