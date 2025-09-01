using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Represents the result of a sorting operation.</summary>
	/// <typeparam name="T">The type of the content of the data source.</typeparam>
	// Token: 0x02000078 RID: 120
	public interface IOrderedQueryable<out T> : IQueryable<T>, IEnumerable<!0>, IEnumerable, IQueryable, IOrderedQueryable
	{
	}
}
