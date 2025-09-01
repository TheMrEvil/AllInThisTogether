using System;
using System.Collections.Generic;

namespace System.Data
{
	/// <summary>Contains the extension methods for the <see cref="T:System.Data.TypedTableBase`1" /> class.</summary>
	// Token: 0x02000012 RID: 18
	public static class TypedTableBaseExtensions
	{
		/// <summary>Filters a sequence of rows based on the specified predicate.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> that contains rows from the input sequence that satisfy the condition.</returns>
		// Token: 0x0600005D RID: 93 RVA: 0x000031DB File Offset: 0x000013DB
		public static EnumerableRowCollection<TRow> Where<TRow>(this TypedTableBase<TRow> source, Func<TRow, bool> predicate) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull<TypedTableBase<TRow>>(source, "source");
			return new EnumerableRowCollection<TRow>(source).Where(predicate);
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.TypedTableBase`1" /> in ascending order according to the specified key.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		// Token: 0x0600005E RID: 94 RVA: 0x000031F4 File Offset: 0x000013F4
		public static OrderedEnumerableRowCollection<TRow> OrderBy<TRow, TKey>(this TypedTableBase<TRow> source, Func<TRow, TKey> keySelector) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull<TypedTableBase<TRow>>(source, "source");
			return new EnumerableRowCollection<TRow>(source).OrderBy(keySelector);
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.TypedTableBase`1" /> in ascending order according to the specified key and comparer.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		// Token: 0x0600005F RID: 95 RVA: 0x0000320D File Offset: 0x0000140D
		public static OrderedEnumerableRowCollection<TRow> OrderBy<TRow, TKey>(this TypedTableBase<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull<TypedTableBase<TRow>>(source, "source");
			return new EnumerableRowCollection<TRow>(source).OrderBy(keySelector, comparer);
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.TypedTableBase`1" /> in descending order according to the specified key.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		// Token: 0x06000060 RID: 96 RVA: 0x00003227 File Offset: 0x00001427
		public static OrderedEnumerableRowCollection<TRow> OrderByDescending<TRow, TKey>(this TypedTableBase<TRow> source, Func<TRow, TKey> keySelector) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull<TypedTableBase<TRow>>(source, "source");
			return new EnumerableRowCollection<TRow>(source).OrderByDescending(keySelector);
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.TypedTableBase`1" /> in descending order according to the specified key and comparer.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		// Token: 0x06000061 RID: 97 RVA: 0x00003240 File Offset: 0x00001440
		public static OrderedEnumerableRowCollection<TRow> OrderByDescending<TRow, TKey>(this TypedTableBase<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull<TypedTableBase<TRow>>(source, "source");
			return new EnumerableRowCollection<TRow>(source).OrderByDescending(keySelector, comparer);
		}

		/// <summary>Projects each element of a <see cref="T:System.Data.TypedTableBase`1" /> into a new form.</summary>
		/// <param name="source">A <see cref="T:System.Data.TypedTableBase`1" /> that contains the <see cref="T:System.Data.DataRow" /> elements to invoke a transformation function upon.</param>
		/// <param name="selector">A transformation function to apply to each element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="S" />
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection`1" /> whose elements are the result of invoking the transformation function on each element of <paramref name="source" />.</returns>
		// Token: 0x06000062 RID: 98 RVA: 0x0000325A File Offset: 0x0000145A
		public static EnumerableRowCollection<S> Select<TRow, S>(this TypedTableBase<TRow> source, Func<TRow, S> selector) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull<TypedTableBase<TRow>>(source, "source");
			return new EnumerableRowCollection<TRow>(source).Select(selector);
		}

		/// <summary>Enumerates the data row elements of the <see cref="T:System.Data.TypedTableBase`1" /> and returns an <see cref="T:System.Data.EnumerableRowCollection`1" /> object, where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />. This object can be used in a LINQ expression or method query.</summary>
		/// <param name="source">The source <see cref="T:System.Data.TypedTableBase`1" /> to make enumerable.</param>
		/// <typeparam name="TRow">The type to convert the elements of the source to.</typeparam>
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection`1" /> object, where the generic parameter <paramref name="T" /> is <see cref="T:System.Data.DataRow" />.</returns>
		// Token: 0x06000063 RID: 99 RVA: 0x00003273 File Offset: 0x00001473
		public static EnumerableRowCollection<TRow> AsEnumerable<TRow>(this TypedTableBase<TRow> source) where TRow : DataRow
		{
			DataSetUtil.CheckArgumentNull<TypedTableBase<TRow>>(source, "source");
			return new EnumerableRowCollection<TRow>(source);
		}

		/// <summary>Returns the element at a specified row in a sequence or a default value if the row is out of range.</summary>
		/// <param name="source">An enumerable object to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TRow">The type of the elements or the row.</typeparam>
		/// <returns>The element at a specified row in a sequence.</returns>
		// Token: 0x06000064 RID: 100 RVA: 0x00003288 File Offset: 0x00001488
		public static TRow ElementAtOrDefault<TRow>(this TypedTableBase<TRow> source, int index) where TRow : DataRow
		{
			if (index >= 0 && index < source.Rows.Count)
			{
				return (TRow)((object)source.Rows[index]);
			}
			return default(TRow);
		}
	}
}
