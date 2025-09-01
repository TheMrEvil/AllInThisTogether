using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data
{
	/// <summary>Contains the extension methods for the data row collection classes.</summary>
	// Token: 0x0200000D RID: 13
	public static class EnumerableRowCollectionExtensions
	{
		/// <summary>Filters a sequence of rows based on the specified predicate.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> that contains rows from the input sequence that satisfy the condition.</returns>
		// Token: 0x06000041 RID: 65 RVA: 0x00002BE1 File Offset: 0x00000DE1
		public static EnumerableRowCollection<TRow> Where<TRow>(this EnumerableRowCollection<TRow> source, Func<TRow, bool> predicate)
		{
			EnumerableRowCollection<TRow> enumerableRowCollection = new EnumerableRowCollection<TRow>(source, source.Where(predicate), null);
			enumerableRowCollection.AddPredicate(predicate);
			return enumerableRowCollection;
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in ascending order according to the specified key.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		// Token: 0x06000042 RID: 66 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public static OrderedEnumerableRowCollection<TRow> OrderBy<TRow, TKey>(this EnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector)
		{
			IEnumerable<TRow> enumerableRows = source.OrderBy(keySelector);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression<TKey>(keySelector, false, true);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in ascending order according to the specified key and comparer.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		// Token: 0x06000043 RID: 67 RVA: 0x00002C20 File Offset: 0x00000E20
		public static OrderedEnumerableRowCollection<TRow> OrderBy<TRow, TKey>(this EnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer)
		{
			IEnumerable<TRow> enumerableRows = source.OrderBy(keySelector, comparer);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression<TKey>(keySelector, comparer, false, true);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in descending order according to the specified key.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		// Token: 0x06000044 RID: 68 RVA: 0x00002C48 File Offset: 0x00000E48
		public static OrderedEnumerableRowCollection<TRow> OrderByDescending<TRow, TKey>(this EnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector)
		{
			IEnumerable<TRow> enumerableRows = source.OrderByDescending(keySelector);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression<TKey>(keySelector, true, true);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Sorts the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in descending order according to the specified key and comparer.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		// Token: 0x06000045 RID: 69 RVA: 0x00002C70 File Offset: 0x00000E70
		public static OrderedEnumerableRowCollection<TRow> OrderByDescending<TRow, TKey>(this EnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer)
		{
			IEnumerable<TRow> enumerableRows = source.OrderByDescending(keySelector, comparer);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression<TKey>(keySelector, comparer, true, true);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Performs a secondary ordering of the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in ascending order according to the specified key.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		// Token: 0x06000046 RID: 70 RVA: 0x00002C98 File Offset: 0x00000E98
		public static OrderedEnumerableRowCollection<TRow> ThenBy<TRow, TKey>(this OrderedEnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector)
		{
			IEnumerable<TRow> enumerableRows = ((IOrderedEnumerable<TRow>)source.EnumerableRows).ThenBy(keySelector);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression<TKey>(keySelector, false, false);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Performs a secondary ordering of the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in ascending order according to the specified key and comparer.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		// Token: 0x06000047 RID: 71 RVA: 0x00002CC8 File Offset: 0x00000EC8
		public static OrderedEnumerableRowCollection<TRow> ThenBy<TRow, TKey>(this OrderedEnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer)
		{
			IEnumerable<TRow> enumerableRows = ((IOrderedEnumerable<TRow>)source.EnumerableRows).ThenBy(keySelector, comparer);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression<TKey>(keySelector, comparer, false, false);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Performs a secondary ordering of the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in descending order according to the specified key.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key.</returns>
		// Token: 0x06000048 RID: 72 RVA: 0x00002CFC File Offset: 0x00000EFC
		public static OrderedEnumerableRowCollection<TRow> ThenByDescending<TRow, TKey>(this OrderedEnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector)
		{
			IEnumerable<TRow> enumerableRows = ((IOrderedEnumerable<TRow>)source.EnumerableRows).ThenByDescending(keySelector);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression<TKey>(keySelector, true, false);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Performs a secondary ordering of the rows of a <see cref="T:System.Data.EnumerableRowCollection" /> in descending order according to the specified key and comparer.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection" /> containing the <see cref="T:System.Data.DataRow" /> elements to be ordered.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Data.OrderedEnumerableRowCollection`1" /> whose elements are sorted by the specified key and comparer.</returns>
		// Token: 0x06000049 RID: 73 RVA: 0x00002D2C File Offset: 0x00000F2C
		public static OrderedEnumerableRowCollection<TRow> ThenByDescending<TRow, TKey>(this OrderedEnumerableRowCollection<TRow> source, Func<TRow, TKey> keySelector, IComparer<TKey> comparer)
		{
			IEnumerable<TRow> enumerableRows = ((IOrderedEnumerable<TRow>)source.EnumerableRows).ThenByDescending(keySelector, comparer);
			OrderedEnumerableRowCollection<TRow> orderedEnumerableRowCollection = new OrderedEnumerableRowCollection<TRow>(source, enumerableRows);
			orderedEnumerableRowCollection.AddSortExpression<TKey>(keySelector, comparer, true, false);
			return orderedEnumerableRowCollection;
		}

		/// <summary>Projects each element of an <see cref="T:System.Data.EnumerableRowCollection`1" /> into a new form.</summary>
		/// <param name="source">An <see cref="T:System.Data.EnumerableRowCollection`1" /> containing the <see cref="T:System.Data.DataRow" /> elements to invoke a transform function upon.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TRow">The type of the row elements in <paramref name="source" />, typically <see cref="T:System.Data.DataRow" />.</typeparam>
		/// <typeparam name="S">The type that <paramref name="TRow" /> will be transformed into.</typeparam>
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection`1" /> whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
		// Token: 0x0600004A RID: 74 RVA: 0x00002D60 File Offset: 0x00000F60
		public static EnumerableRowCollection<S> Select<TRow, S>(this EnumerableRowCollection<TRow> source, Func<TRow, S> selector)
		{
			IEnumerable<S> enumerableRows = source.Select(selector);
			return new EnumerableRowCollection<S>(source as EnumerableRowCollection<S>, enumerableRows, selector as Func<S, S>);
		}

		/// <summary>Converts the elements of an <see cref="T:System.Data.EnumerableRowCollection" /> to the specified type.</summary>
		/// <param name="source">The <see cref="T:System.Data.EnumerableRowCollection" /> that contains the elements to be converted.</param>
		/// <typeparam name="TResult">The type to convert the elements of source to.</typeparam>
		/// <returns>An <see cref="T:System.Data.EnumerableRowCollection" /> that contains each element of the source sequence converted to the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">An element in the sequence cannot be cast to type <paramref name="TResult" />.</exception>
		// Token: 0x0600004B RID: 75 RVA: 0x00002D88 File Offset: 0x00000F88
		public static EnumerableRowCollection<TResult> Cast<TResult>(this EnumerableRowCollection source)
		{
			if (source != null && source.ElementType.Equals(typeof(TResult)))
			{
				return (EnumerableRowCollection<TResult>)source;
			}
			return new EnumerableRowCollection<TResult>(source.Cast<TResult>(), typeof(TResult).IsAssignableFrom(source.ElementType) && typeof(DataRow).IsAssignableFrom(typeof(TResult)), source.Table);
		}
	}
}
