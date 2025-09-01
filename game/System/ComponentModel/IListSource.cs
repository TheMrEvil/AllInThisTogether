using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to an object to return a list that can be bound to a data source.</summary>
	// Token: 0x020003B7 RID: 951
	[MergableProperty(false)]
	public interface IListSource
	{
		/// <summary>Gets a value indicating whether the collection is a collection of <see cref="T:System.Collections.IList" /> objects.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is a collection of <see cref="T:System.Collections.IList" /> objects; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001F15 RID: 7957
		bool ContainsListCollection { get; }

		/// <summary>Returns an <see cref="T:System.Collections.IList" /> that can be bound to a data source from an object that does not implement an <see cref="T:System.Collections.IList" /> itself.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that can be bound to a data source from the object.</returns>
		// Token: 0x06001F16 RID: 7958
		IList GetList();
	}
}
