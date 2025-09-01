using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a read-only collection of elements that can be accessed by index.</summary>
	/// <typeparam name="T">The type of elements in the read-only list.</typeparam>
	// Token: 0x02000A9F RID: 2719
	public interface IReadOnlyList<out T> : IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		/// <summary>Gets the element at the specified index in the read-only list.</summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified index in the read-only list.</returns>
		// Token: 0x1700114B RID: 4427
		T this[int index]
		{
			get;
		}
	}
}
