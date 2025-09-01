using System;
using System.Collections;

namespace System.Resources
{
	/// <summary>Provides the base functionality for reading data from resource files.</summary>
	// Token: 0x02000859 RID: 2137
	public interface IResourceReader : IEnumerable, IDisposable
	{
		/// <summary>Closes the resource reader after releasing any resources associated with it.</summary>
		// Token: 0x06004734 RID: 18228
		void Close();

		/// <summary>Returns a dictionary enumerator of the resources for this reader.</summary>
		/// <returns>A dictionary enumerator for the resources for this reader.</returns>
		// Token: 0x06004735 RID: 18229
		IDictionaryEnumerator GetEnumerator();
	}
}
