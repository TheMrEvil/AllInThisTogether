using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000CA RID: 202
	internal readonly struct Buffer<TElement>
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0001A900 File Offset: 0x00018B00
		internal Buffer(IEnumerable<TElement> source)
		{
			IIListProvider<TElement> iilistProvider = source as IIListProvider<TElement>;
			if (iilistProvider != null)
			{
				TElement[] array = iilistProvider.ToArray();
				this._items = array;
				this._count = array.Length;
				return;
			}
			this._items = EnumerableHelpers.ToArray<TElement>(source, out this._count);
		}

		// Token: 0x0400056A RID: 1386
		internal readonly TElement[] _items;

		// Token: 0x0400056B RID: 1387
		internal readonly int _count;
	}
}
