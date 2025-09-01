using System;

namespace System.Collections
{
	/// <summary>Defines methods to support the comparison of objects for equality.</summary>
	// Token: 0x02000A18 RID: 2584
	public interface IEqualityComparer
	{
		/// <summary>Determines whether the specified objects are equal.</summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="x" /> and <paramref name="y" /> are of different types and neither one can handle comparisons with the other.</exception>
		// Token: 0x06005B95 RID: 23445
		bool Equals(object x, object y);

		/// <summary>Returns a hash code for the specified object.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for the specified object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06005B96 RID: 23446
		int GetHashCode(object obj);
	}
}
