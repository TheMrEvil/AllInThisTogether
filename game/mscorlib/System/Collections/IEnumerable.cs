using System;

namespace System.Collections
{
	/// <summary>Exposes an enumerator, which supports a simple iteration over a non-generic collection.</summary>
	// Token: 0x02000A16 RID: 2582
	public interface IEnumerable
	{
		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		// Token: 0x06005B91 RID: 23441
		IEnumerator GetEnumerator();
	}
}
