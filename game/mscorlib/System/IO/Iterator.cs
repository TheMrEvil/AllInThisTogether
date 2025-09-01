using System;
using System.Collections;
using System.Collections.Generic;

namespace System.IO
{
	// Token: 0x02000B3B RID: 2875
	internal abstract class Iterator<TSource> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x060067FA RID: 26618 RVA: 0x0016285A File Offset: 0x00160A5A
		public Iterator()
		{
			this._threadId = Environment.CurrentManagedThreadId;
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x060067FB RID: 26619 RVA: 0x0016286D File Offset: 0x00160A6D
		public TSource Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x060067FC RID: 26620
		protected abstract Iterator<TSource> Clone();

		// Token: 0x060067FD RID: 26621 RVA: 0x00162875 File Offset: 0x00160A75
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060067FE RID: 26622 RVA: 0x00162884 File Offset: 0x00160A84
		protected virtual void Dispose(bool disposing)
		{
			this.current = default(TSource);
			this.state = -1;
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x00162899 File Offset: 0x00160A99
		public IEnumerator<TSource> GetEnumerator()
		{
			if (this.state == 0 && this._threadId == Environment.CurrentManagedThreadId)
			{
				this.state = 1;
				return this;
			}
			Iterator<TSource> iterator = this.Clone();
			iterator.state = 1;
			return iterator;
		}

		// Token: 0x06006800 RID: 26624
		public abstract bool MoveNext();

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06006801 RID: 26625 RVA: 0x001628C6 File Offset: 0x00160AC6
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x001628D3 File Offset: 0x00160AD3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006803 RID: 26627 RVA: 0x000472C8 File Offset: 0x000454C8
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003C73 RID: 15475
		private int _threadId;

		// Token: 0x04003C74 RID: 15476
		internal int state;

		// Token: 0x04003C75 RID: 15477
		internal TSource current;
	}
}
