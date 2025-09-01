using System;
using System.Collections.Generic;
using Unity;

namespace System.Data
{
	/// <summary>Represents a collection of ordered <see cref="T:System.Data.DataRow" /> objects returned from a query.</summary>
	/// <typeparam name="TRow">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
	// Token: 0x0200000E RID: 14
	public sealed class OrderedEnumerableRowCollection<TRow> : EnumerableRowCollection<TRow>
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002DFA File Offset: 0x00000FFA
		internal OrderedEnumerableRowCollection(EnumerableRowCollection<TRow> enumerableTable, IEnumerable<TRow> enumerableRows) : base(enumerableTable, enumerableRows, null)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BA1 File Offset: 0x00000DA1
		internal OrderedEnumerableRowCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
