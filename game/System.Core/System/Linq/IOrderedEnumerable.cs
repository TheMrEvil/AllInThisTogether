using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Represents a sorted sequence.</summary>
	/// <typeparam name="TElement">The type of the elements of the sequence.</typeparam>
	// Token: 0x020000DB RID: 219
	public interface IOrderedEnumerable<out TElement> : IEnumerable<!0>, IEnumerable
	{
		/// <summary>Performs a subsequent ordering on the elements of an <see cref="T:System.Linq.IOrderedEnumerable`1" /> according to a key.</summary>
		/// <param name="keySelector">The <see cref="T:System.Func`2" /> used to extract the key for each element.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> used to compare keys for placement in the returned sequence.</param>
		/// <param name="descending">
		///       <see langword="true" /> to sort the elements in descending order; <see langword="false" /> to sort the elements in ascending order.</param>
		/// <typeparam name="TKey">The type of the key produced by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
		// Token: 0x060007CC RID: 1996
		IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);
	}
}
