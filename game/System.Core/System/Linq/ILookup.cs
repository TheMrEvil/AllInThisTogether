using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Defines an indexer, size property, and Boolean search method for data structures that map keys to <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequences of values.</summary>
	/// <typeparam name="TKey">The type of the keys in the <see cref="T:System.Linq.ILookup`2" />.</typeparam>
	/// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequences that make up the values in the <see cref="T:System.Linq.ILookup`2" />.</typeparam>
	// Token: 0x020000D7 RID: 215
	public interface ILookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		/// <summary>Gets the number of key/value collection pairs in the <see cref="T:System.Linq.ILookup`2" />.</summary>
		/// <returns>The number of key/value collection pairs in the <see cref="T:System.Linq.ILookup`2" />.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060007A8 RID: 1960
		int Count { get; }

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence of values indexed by a specified key.</summary>
		/// <param name="key">The key of the desired sequence of values.</param>
		/// <returns>The <see cref="T:System.Collections.Generic.IEnumerable`1" /> sequence of values indexed by the specified key.</returns>
		// Token: 0x170000E0 RID: 224
		IEnumerable<TElement> this[TKey key]
		{
			get;
		}

		/// <summary>Determines whether a specified key exists in the <see cref="T:System.Linq.ILookup`2" />.</summary>
		/// <param name="key">The key to search for in the <see cref="T:System.Linq.ILookup`2" />.</param>
		/// <returns>
		///     <see langword="true" /> if <paramref name="key" /> is in the <see cref="T:System.Linq.ILookup`2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007AA RID: 1962
		bool Contains(TKey key);
	}
}
