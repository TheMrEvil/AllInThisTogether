using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Linq
{
	// Token: 0x020000E7 RID: 231
	internal sealed class EmptyPartition<TElement> : IPartition<TElement>, IIListProvider<TElement>, IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x00002162 File Offset: 0x00000362
		private EmptyPartition()
		{
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000022A7 File Offset: 0x000004A7
		public IEnumerator<TElement> GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000022A7 File Offset: 0x000004A7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000023D1 File Offset: 0x000005D1
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001C3EC File Offset: 0x0001A5EC
		[ExcludeFromCodeCoverage]
		public TElement Current
		{
			get
			{
				return default(TElement);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001C404 File Offset: 0x0001A604
		[ExcludeFromCodeCoverage]
		object IEnumerator.Current
		{
			get
			{
				return default(TElement);
			}
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001585F File Offset: 0x00013A5F
		void IEnumerator.Reset()
		{
			throw Error.NotSupported();
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00003A59 File Offset: 0x00001C59
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000022A7 File Offset: 0x000004A7
		public IPartition<TElement> Skip(int count)
		{
			return this;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000022A7 File Offset: 0x000004A7
		public IPartition<TElement> Take(int count)
		{
			return this;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001C420 File Offset: 0x0001A620
		public TElement TryGetElementAt(int index, out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001C43C File Offset: 0x0001A63C
		public TElement TryGetFirst(out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001C458 File Offset: 0x0001A658
		public TElement TryGetLast(out bool found)
		{
			found = false;
			return default(TElement);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001C471 File Offset: 0x0001A671
		public TElement[] ToArray()
		{
			return Array.Empty<TElement>();
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001C478 File Offset: 0x0001A678
		public List<TElement> ToList()
		{
			return new List<TElement>();
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000023D1 File Offset: 0x000005D1
		public int GetCount(bool onlyIfCheap)
		{
			return 0;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001C47F File Offset: 0x0001A67F
		// Note: this type is marked as 'beforefieldinit'.
		static EmptyPartition()
		{
		}

		// Token: 0x040005B7 RID: 1463
		public static readonly IPartition<TElement> Instance = new EmptyPartition<TElement>();
	}
}
