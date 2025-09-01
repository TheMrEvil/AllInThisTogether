using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000F3 RID: 243
	internal class EnumerableWrapperWeakToStrong : IEnumerable<object>, IEnumerable
	{
		// Token: 0x0600086F RID: 2159 RVA: 0x0001D09A File Offset: 0x0001B29A
		internal EnumerableWrapperWeakToStrong(IEnumerable wrappedEnumerable)
		{
			this._wrappedEnumerable = wrappedEnumerable;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0001D0A9 File Offset: 0x0001B2A9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<object>)this).GetEnumerator();
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001D0B1 File Offset: 0x0001B2B1
		public IEnumerator<object> GetEnumerator()
		{
			return new EnumerableWrapperWeakToStrong.WrapperEnumeratorWeakToStrong(this._wrappedEnumerable.GetEnumerator());
		}

		// Token: 0x040005DA RID: 1498
		private readonly IEnumerable _wrappedEnumerable;

		// Token: 0x020000F4 RID: 244
		private class WrapperEnumeratorWeakToStrong : IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06000872 RID: 2162 RVA: 0x0001D0C3 File Offset: 0x0001B2C3
			internal WrapperEnumeratorWeakToStrong(IEnumerator wrappedEnumerator)
			{
				this._wrappedEnumerator = wrappedEnumerator;
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001D0D2 File Offset: 0x0001B2D2
			object IEnumerator.Current
			{
				get
				{
					return this._wrappedEnumerator.Current;
				}
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000874 RID: 2164 RVA: 0x0001D0D2 File Offset: 0x0001B2D2
			object IEnumerator<object>.Current
			{
				get
				{
					return this._wrappedEnumerator.Current;
				}
			}

			// Token: 0x06000875 RID: 2165 RVA: 0x0001D0E0 File Offset: 0x0001B2E0
			void IDisposable.Dispose()
			{
				IDisposable disposable = this._wrappedEnumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x06000876 RID: 2166 RVA: 0x0001D102 File Offset: 0x0001B302
			bool IEnumerator.MoveNext()
			{
				return this._wrappedEnumerator.MoveNext();
			}

			// Token: 0x06000877 RID: 2167 RVA: 0x0001D10F File Offset: 0x0001B30F
			void IEnumerator.Reset()
			{
				this._wrappedEnumerator.Reset();
			}

			// Token: 0x040005DB RID: 1499
			private IEnumerator _wrappedEnumerator;
		}
	}
}
