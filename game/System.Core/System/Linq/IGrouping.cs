using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Represents a collection of objects that have a common key.</summary>
	/// <typeparam name="TKey">The type of the key of the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
	/// <typeparam name="TElement">The type of the values in the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
	// Token: 0x020000D0 RID: 208
	public interface IGrouping<out TKey, out TElement> : IEnumerable<!1>, IEnumerable
	{
		/// <summary>Gets the key of the <see cref="T:System.Linq.IGrouping`2" />.</summary>
		/// <returns>The key of the <see cref="T:System.Linq.IGrouping`2" />.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000777 RID: 1911
		TKey Key { get; }
	}
}
