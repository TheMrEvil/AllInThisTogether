using System;

namespace System.Collections.Generic
{
	/// <summary>Provides the base interface for the abstraction of sets.</summary>
	/// <typeparam name="T">The type of elements in the set.</typeparam>
	// Token: 0x020004F1 RID: 1265
	public interface ISet<T> : ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		/// <summary>Adds an element to the current set and returns a value to indicate if the element was successfully added.</summary>
		/// <param name="item">The element to add to the set.</param>
		/// <returns>
		///   <see langword="true" /> if the element is added to the set; <see langword="false" /> if the element is already in the set.</returns>
		// Token: 0x0600295B RID: 10587
		bool Add(T item);

		/// <summary>Modifies the current set so that it contains all elements that are present in the current set, in the specified collection, or in both.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x0600295C RID: 10588
		void UnionWith(IEnumerable<T> other);

		/// <summary>Modifies the current set so that it contains only elements that are also in a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x0600295D RID: 10589
		void IntersectWith(IEnumerable<T> other);

		/// <summary>Removes all elements in the specified collection from the current set.</summary>
		/// <param name="other">The collection of items to remove from the set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x0600295E RID: 10590
		void ExceptWith(IEnumerable<T> other);

		/// <summary>Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x0600295F RID: 10591
		void SymmetricExceptWith(IEnumerable<T> other);

		/// <summary>Determines whether a set is a subset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002960 RID: 10592
		bool IsSubsetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a superset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002961 RID: 10593
		bool IsSupersetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a proper (strict) superset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a proper superset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002962 RID: 10594
		bool IsProperSupersetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set is a proper (strict) subset of a specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is a proper subset of <paramref name="other" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002963 RID: 10595
		bool IsProperSubsetOf(IEnumerable<T> other);

		/// <summary>Determines whether the current set overlaps with the specified collection.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set and <paramref name="other" /> share at least one common element; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002964 RID: 10596
		bool Overlaps(IEnumerable<T> other);

		/// <summary>Determines whether the current set and the specified collection contain the same elements.</summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <returns>
		///   <see langword="true" /> if the current set is equal to <paramref name="other" />; otherwise, false.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="other" /> is <see langword="null" />.</exception>
		// Token: 0x06002965 RID: 10597
		bool SetEquals(IEnumerable<T> other);
	}
}
