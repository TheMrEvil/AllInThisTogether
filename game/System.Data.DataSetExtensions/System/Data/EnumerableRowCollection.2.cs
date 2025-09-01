using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Data
{
	/// <summary>Represents a collection of <see cref="T:System.Data.DataRow" /> objects returned from a query.</summary>
	/// <typeparam name="TRow">The type of objects in the source sequence, typically <see cref="T:System.Data.DataRow" />.</typeparam>
	// Token: 0x0200000B RID: 11
	public class EnumerableRowCollection<TRow> : EnumerableRowCollection, IEnumerable<TRow>, IEnumerable
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002A0E File Offset: 0x00000C0E
		internal override Type ElementType
		{
			get
			{
				return typeof(TRow);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A1A File Offset: 0x00000C1A
		internal IEnumerable<TRow> EnumerableRows
		{
			get
			{
				return this._enumerableRows;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A22 File Offset: 0x00000C22
		internal override DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A2A File Offset: 0x00000C2A
		internal EnumerableRowCollection(IEnumerable<TRow> enumerableRows, bool isDataViewable, DataTable table)
		{
			this._enumerableRows = enumerableRows;
			if (isDataViewable)
			{
				this._table = table;
			}
			this._listOfPredicates = new List<Func<TRow, bool>>();
			this._sortExpression = new SortExpressionBuilder<TRow>();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A59 File Offset: 0x00000C59
		internal EnumerableRowCollection(DataTable table)
		{
			this._table = table;
			this._enumerableRows = table.Rows.Cast<TRow>();
			this._listOfPredicates = new List<Func<TRow, bool>>();
			this._sortExpression = new SortExpressionBuilder<TRow>();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A90 File Offset: 0x00000C90
		internal EnumerableRowCollection(EnumerableRowCollection<TRow> source, IEnumerable<TRow> enumerableRows, Func<TRow, TRow> selector)
		{
			this._enumerableRows = enumerableRows;
			this._selector = selector;
			if (source != null)
			{
				if (source._selector == null)
				{
					this._table = source._table;
				}
				this._listOfPredicates = new List<Func<TRow, bool>>(source._listOfPredicates);
				this._sortExpression = source._sortExpression.Clone();
				return;
			}
			this._listOfPredicates = new List<Func<TRow, bool>>();
			this._sortExpression = new SortExpressionBuilder<TRow>();
		}

		/// <summary>Returns an enumerator for the collection of <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to traverse the collection of <see cref="T:System.Data.DataRow" /> objects.</returns>
		// Token: 0x06000038 RID: 56 RVA: 0x00002B01 File Offset: 0x00000D01
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Returns an enumerator for the collection of contained row objects.</summary>
		/// <returns>A strongly-typed <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to traverse the collection of <paramref name="TRow" /> objects.</returns>
		// Token: 0x06000039 RID: 57 RVA: 0x00002B09 File Offset: 0x00000D09
		public IEnumerator<TRow> GetEnumerator()
		{
			return this._enumerableRows.GetEnumerator();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B16 File Offset: 0x00000D16
		internal void AddPredicate(Func<TRow, bool> pred)
		{
			this._listOfPredicates.Add(pred);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B24 File Offset: 0x00000D24
		internal void AddSortExpression<TKey>(Func<TRow, TKey> keySelector, bool isDescending, bool isOrderBy)
		{
			this.AddSortExpression<TKey>(keySelector, Comparer<TKey>.Default, isDescending, isOrderBy);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B34 File Offset: 0x00000D34
		internal void AddSortExpression<TKey>(Func<TRow, TKey> keySelector, IComparer<TKey> comparer, bool isDescending, bool isOrderBy)
		{
			DataSetUtil.CheckArgumentNull<Func<TRow, TKey>>(keySelector, "keySelector");
			DataSetUtil.CheckArgumentNull<IComparer<TKey>>(comparer, "comparer");
			this._sortExpression.Add((TRow input) => keySelector(input), (object val1, object val2) => (isDescending ? -1 : 1) * comparer.Compare((TKey)((object)val1), (TKey)((object)val2)), isOrderBy);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002BA1 File Offset: 0x00000DA1
		internal EnumerableRowCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000039 RID: 57
		private readonly DataTable _table;

		// Token: 0x0400003A RID: 58
		private readonly IEnumerable<TRow> _enumerableRows;

		// Token: 0x0400003B RID: 59
		private readonly List<Func<TRow, bool>> _listOfPredicates;

		// Token: 0x0400003C RID: 60
		private readonly SortExpressionBuilder<TRow> _sortExpression;

		// Token: 0x0400003D RID: 61
		private readonly Func<TRow, TRow> _selector;

		// Token: 0x0200000C RID: 12
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0<TKey>
		{
			// Token: 0x0600003E RID: 62 RVA: 0x000021DF File Offset: 0x000003DF
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00002BA8 File Offset: 0x00000DA8
			internal object <AddSortExpression>b__0(TRow input)
			{
				return this.keySelector(input);
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00002BBB File Offset: 0x00000DBB
			internal int <AddSortExpression>b__1(object val1, object val2)
			{
				return (this.isDescending ? -1 : 1) * this.comparer.Compare((TKey)((object)val1), (TKey)((object)val2));
			}

			// Token: 0x0400003E RID: 62
			public Func<TRow, TKey> keySelector;

			// Token: 0x0400003F RID: 63
			public bool isDescending;

			// Token: 0x04000040 RID: 64
			public IComparer<TKey> comparer;
		}
	}
}
