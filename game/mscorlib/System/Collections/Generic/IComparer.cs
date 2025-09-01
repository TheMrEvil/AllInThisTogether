using System;

namespace System.Collections.Generic
{
	/// <summary>Defines a method that a type implements to compare two objects.</summary>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	// Token: 0x02000A94 RID: 2708
	public interface IComparer<in T>
	{
		/// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
		/// <param name="x">The first object to compare.</param>
		/// <param name="y">The second object to compare.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="x" /> is less than <paramref name="y" />.  
		///
		///   Zero  
		///
		///  <paramref name="x" /> equals <paramref name="y" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="x" /> is greater than <paramref name="y" />.</returns>
		// Token: 0x06006118 RID: 24856
		int Compare(T x, T y);
	}
}
