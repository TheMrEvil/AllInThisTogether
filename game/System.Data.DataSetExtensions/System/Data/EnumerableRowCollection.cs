using System;
using System.Collections;

namespace System.Data
{
	/// <summary>Represents a collection of <see cref="T:System.Data.DataRow" /> objects returned from a LINQ to DataSet query. This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	// Token: 0x0200000A RID: 10
	public abstract class EnumerableRowCollection : IEnumerable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46
		internal abstract Type ElementType { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47
		internal abstract DataTable Table { get; }

		// Token: 0x06000030 RID: 48 RVA: 0x000021DF File Offset: 0x000003DF
		internal EnumerableRowCollection()
		{
		}

		/// <summary>Returns an enumerator for the collection of <see cref="T:System.Data.DataRow" /> objects. This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to traverse the collection of <see cref="T:System.Data.DataRow" /> objects.</returns>
		// Token: 0x06000031 RID: 49 RVA: 0x00002A0B File Offset: 0x00000C0B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}
}
