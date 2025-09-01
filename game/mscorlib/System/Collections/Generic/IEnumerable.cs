using System;

namespace System.Collections.Generic
{
	/// <summary>Exposes the enumerator, which supports a simple iteration over a collection of a specified type.</summary>
	/// <typeparam name="T">The type of objects to enumerate.</typeparam>
	// Token: 0x02000A99 RID: 2713
	public interface IEnumerable<out T> : IEnumerable
	{
		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06006127 RID: 24871
		IEnumerator<T> GetEnumerator();
	}
}
