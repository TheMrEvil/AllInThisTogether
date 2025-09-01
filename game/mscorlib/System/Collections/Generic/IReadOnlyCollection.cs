using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a strongly-typed, read-only collection of elements.</summary>
	/// <typeparam name="T">The type of the elements.</typeparam>
	// Token: 0x02000A9D RID: 2717
	public interface IReadOnlyCollection<out T> : IEnumerable<!0>, IEnumerable
	{
		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>The number of elements in the collection.</returns>
		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06006130 RID: 24880
		int Count { get; }
	}
}
