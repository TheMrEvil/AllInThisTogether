using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
	/// <summary>Provides a set of <see langword="static" /> (<see langword="Shared" /> in Visual Basic) methods for querying data structures that implement <see cref="T:System.Linq.IQueryable`1" />.</summary>
	// Token: 0x02000095 RID: 149
	public static class Queryable
	{
		/// <summary>Converts a generic <see cref="T:System.Collections.Generic.IEnumerable`1" /> to a generic <see cref="T:System.Linq.IQueryable`1" />.</summary>
		/// <param name="source">A sequence to convert.</param>
		/// <typeparam name="TElement">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that represents the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600044D RID: 1101 RVA: 0x0000CC48 File Offset: 0x0000AE48
		public static IQueryable<TElement> AsQueryable<TElement>(this IEnumerable<TElement> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return (source as IQueryable<TElement>) ?? new EnumerableQuery<TElement>(source);
		}

		/// <summary>Converts an <see cref="T:System.Collections.IEnumerable" /> to an <see cref="T:System.Linq.IQueryable" />.</summary>
		/// <param name="source">A sequence to convert.</param>
		/// <returns>An <see cref="T:System.Linq.IQueryable" /> that represents the input sequence.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="source" /> does not implement <see cref="T:System.Collections.Generic.IEnumerable`1" /> for some <paramref name="T" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600044E RID: 1102 RVA: 0x0000CC68 File Offset: 0x0000AE68
		public static IQueryable AsQueryable(this IEnumerable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			IQueryable queryable = source as IQueryable;
			if (queryable != null)
			{
				return queryable;
			}
			Type type = TypeHelper.FindGenericType(typeof(IEnumerable<>), source.GetType());
			if (type == null)
			{
				throw Error.ArgumentNotIEnumerableGeneric("source");
			}
			return EnumerableQuery.Create(type.GenericTypeArguments[0], source);
		}

		/// <summary>Filters a sequence of values based on a predicate.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified by <paramref name="predicate" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x0600044F RID: 1103 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Where_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Filters a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to filter.</param>
		/// <param name="predicate">A function to test each element for a condition; the second parameter of the function represents the index of the element in the source sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements from the input sequence that satisfy the condition specified by <paramref name="predicate" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06000450 RID: 1104 RVA: 0x0000CD20 File Offset: 0x0000AF20
		public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Where_Index_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Filters the elements of an <see cref="T:System.Linq.IQueryable" /> based on a specified type.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable" /> whose elements to filter.</param>
		/// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
		/// <returns>A collection that contains the elements from <paramref name="source" /> that have type <paramref name="TResult" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000451 RID: 1105 RVA: 0x0000CD75 File Offset: 0x0000AF75
		public static IQueryable<TResult> OfType<TResult>(this IQueryable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.OfType_TResult_1(typeof(TResult)), source.Expression));
		}

		/// <summary>Converts the elements of an <see cref="T:System.Linq.IQueryable" /> to the specified type.</summary>
		/// <param name="source">The <see cref="T:System.Linq.IQueryable" /> that contains the elements to be converted.</param>
		/// <typeparam name="TResult">The type to convert the elements of <paramref name="source" /> to.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains each element of the source sequence converted to the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">An element in the sequence cannot be cast to type <paramref name="TResult" />.</exception>
		// Token: 0x06000452 RID: 1106 RVA: 0x0000CDAB File Offset: 0x0000AFAB
		public static IQueryable<TResult> Cast<TResult>(this IQueryable source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.Cast_TResult_1(typeof(TResult)), source.Expression));
		}

		/// <summary>Projects each element of a sequence into a new form.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by the function represented by <paramref name="selector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> whose elements are the result of invoking a projection function on each element of <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x06000453 RID: 1107 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.Select_TSource_TResult_2(typeof(TSource), typeof(TResult)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Projects each element of a sequence into a new form by incorporating the element's index.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by the function represented by <paramref name="selector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> whose elements are the result of invoking a projection function on each element of <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x06000454 RID: 1108 RVA: 0x0000CE44 File Offset: 0x0000B044
		public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.Select_Index_TSource_TResult_2(typeof(TSource), typeof(TResult)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> and combines the resulting sequences into one sequence.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by the function represented by <paramref name="selector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> whose elements are the result of invoking a one-to-many projection function on each element of the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x06000455 RID: 1109 RVA: 0x0000CEA4 File Offset: 0x0000B0A4
		public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TResult>>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.SelectMany_TSource_TResult_2(typeof(TSource), typeof(TResult)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> and combines the resulting sequences into one sequence. The index of each source element is used in the projected form of that element.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A projection function to apply to each element; the second parameter of this function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by the function represented by <paramref name="selector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> whose elements are the result of invoking a one-to-many projection function on each element of the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x06000456 RID: 1110 RVA: 0x0000CF04 File Offset: 0x0000B104
		public static IQueryable<TResult> SelectMany<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TResult>>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.SelectMany_Index_TSource_TResult_2(typeof(TSource), typeof(TResult)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> that incorporates the index of the source element that produced it. A result selector function is invoked on each element of each intermediate sequence, and the resulting values are combined into a single, one-dimensional sequence and returned.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="collectionSelector">A projection function to apply to each element of the input sequence; the second parameter of this function represents the index of the source element.</param>
		/// <param name="resultSelector">A projection function to apply to each element of each intermediate sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TCollection">The type of the intermediate elements collected by the function represented by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> whose elements are the result of invoking the one-to-many projection function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> and then mapping each of those sequence elements and their corresponding <paramref name="source" /> element to a result element.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="collectionSelector" /> or <paramref name="resultSelector" /> is <see langword="null" />.</exception>
		// Token: 0x06000457 RID: 1111 RVA: 0x0000CF64 File Offset: 0x0000B164
		public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, int, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (collectionSelector == null)
			{
				throw Error.ArgumentNull("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.SelectMany_Index_TSource_TCollection_TResult_3(typeof(TSource), typeof(TCollection), typeof(TResult)), source.Expression, Expression.Quote(collectionSelector), Expression.Quote(resultSelector)));
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> and invokes a result selector function on each element therein. The resulting values from each intermediate sequence are combined into a single, one-dimensional sequence and returned.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="collectionSelector">A projection function to apply to each element of the input sequence.</param>
		/// <param name="resultSelector">A projection function to apply to each element of each intermediate sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TCollection">The type of the intermediate elements collected by the function represented by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> whose elements are the result of invoking the one-to-many projection function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> and then mapping each of those sequence elements and their corresponding <paramref name="source" /> element to a result element.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="collectionSelector" /> or <paramref name="resultSelector" /> is <see langword="null" />.</exception>
		// Token: 0x06000458 RID: 1112 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		public static IQueryable<TResult> SelectMany<TSource, TCollection, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector, Expression<Func<TSource, TCollection, TResult>> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (collectionSelector == null)
			{
				throw Error.ArgumentNull("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.SelectMany_TSource_TCollection_TResult_3(typeof(TSource), typeof(TCollection), typeof(TResult)), source.Expression, Expression.Quote(collectionSelector), Expression.Quote(resultSelector)));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000D064 File Offset: 0x0000B264
		private static Expression GetSourceExpression<TSource>(IEnumerable<TSource> source)
		{
			IQueryable<TSource> queryable = source as IQueryable<TSource>;
			if (queryable == null)
			{
				return Expression.Constant(source, typeof(IEnumerable<TSource>));
			}
			return queryable.Expression;
		}

		/// <summary>Correlates the elements of two sequences based on matching keys. The default equality comparer is used to compare keys.</summary>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that has elements of type <paramref name="TResult" /> obtained by performing an inner join on two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is <see langword="null" />.</exception>
		// Token: 0x0600045A RID: 1114 RVA: 0x0000D094 File Offset: 0x0000B294
		public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.Join_TOuter_TInner_TKey_TResult_5(typeof(TOuter), typeof(TInner), typeof(TKey), typeof(TResult)), new Expression[]
			{
				outer.Expression,
				Queryable.GetSourceExpression<TInner>(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector)
			}));
		}

		/// <summary>Correlates the elements of two sequences based on matching keys. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
		/// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that has elements of type <paramref name="TResult" /> obtained by performing an inner join on two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is <see langword="null" />.</exception>
		// Token: 0x0600045B RID: 1115 RVA: 0x0000D15C File Offset: 0x0000B35C
		public static IQueryable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.Join_TOuter_TInner_TKey_TResult_6(typeof(TOuter), typeof(TInner), typeof(TKey), typeof(TResult)), new Expression[]
			{
				outer.Expression,
				Queryable.GetSourceExpression<TInner>(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		/// <summary>Correlates the elements of two sequences based on key equality and groups the results. The default equality comparer is used to compare keys.</summary>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements of type <paramref name="TResult" /> obtained by performing a grouped join on two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is <see langword="null" />.</exception>
		// Token: 0x0600045C RID: 1116 RVA: 0x0000D238 File Offset: 0x0000B438
		public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.GroupJoin_TOuter_TInner_TKey_TResult_5(typeof(TOuter), typeof(TInner), typeof(TKey), typeof(TResult)), new Expression[]
			{
				outer.Expression,
				Queryable.GetSourceExpression<TInner>(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector)
			}));
		}

		/// <summary>Correlates the elements of two sequences based on key equality and groups the results. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <param name="comparer">A comparer to hash and compare keys.</param>
		/// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements of type <paramref name="TResult" /> obtained by performing a grouped join on two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is <see langword="null" />.</exception>
		// Token: 0x0600045D RID: 1117 RVA: 0x0000D300 File Offset: 0x0000B500
		public static IQueryable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IQueryable<TOuter> outer, IEnumerable<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, IEnumerable<TInner>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw Error.ArgumentNull("outer");
			}
			if (inner == null)
			{
				throw Error.ArgumentNull("inner");
			}
			if (outerKeySelector == null)
			{
				throw Error.ArgumentNull("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw Error.ArgumentNull("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return outer.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.GroupJoin_TOuter_TInner_TKey_TResult_6(typeof(TOuter), typeof(TInner), typeof(TKey), typeof(TResult)), new Expression[]
			{
				outer.Expression,
				Queryable.GetSourceExpression<TInner>(inner),
				Expression.Quote(outerKeySelector),
				Expression.Quote(innerKeySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		/// <summary>Sorts the elements of a sequence in ascending order according to a key.</summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function that is represented by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is <see langword="null" />.</exception>
		// Token: 0x0600045E RID: 1118 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.OrderBy_TSource_TKey_2(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector)));
		}

		/// <summary>Sorts the elements of a sequence in ascending order by using a specified comparer.</summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function that is represented by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x0600045F RID: 1119 RVA: 0x0000D440 File Offset: 0x0000B640
		public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.OrderBy_TSource_TKey_3(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector), Expression.Constant(comparer, typeof(IComparer<TKey>))));
		}

		/// <summary>Sorts the elements of a sequence in descending order according to a key.</summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function that is represented by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is <see langword="null" />.</exception>
		// Token: 0x06000460 RID: 1120 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.OrderByDescending_TSource_TKey_2(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector)));
		}

		/// <summary>Sorts the elements of a sequence in descending order by using a specified comparer.</summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function that is represented by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x06000461 RID: 1121 RVA: 0x0000D518 File Offset: 0x0000B718
		public static IOrderedQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.OrderByDescending_TSource_TKey_3(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector), Expression.Constant(comparer, typeof(IComparer<TKey>))));
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedQueryable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is <see langword="null" />.</exception>
		// Token: 0x06000462 RID: 1122 RVA: 0x0000D58C File Offset: 0x0000B78C
		public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.ThenBy_TSource_TKey_2(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector)));
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedQueryable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x06000463 RID: 1123 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
		public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.ThenBy_TSource_TKey_3(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector), Expression.Constant(comparer, typeof(IComparer<TKey>))));
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in descending order, according to a key.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedQueryable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented by <paramref name="keySelector" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IOrderedQueryable`1" /> whose elements are sorted in descending order according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is <see langword="null" />.</exception>
		// Token: 0x06000464 RID: 1124 RVA: 0x0000D664 File Offset: 0x0000B864
		public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.ThenByDescending_TSource_TKey_2(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector)));
		}

		/// <summary>Performs a subsequent ordering of the elements in a sequence in descending order by using a specified comparer.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IOrderedQueryable`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key that is returned by the <paramref name="keySelector" /> function.</typeparam>
		/// <returns>A collection whose elements are sorted in descending order according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x06000465 RID: 1125 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		public static IOrderedQueryable<TSource> ThenByDescending<TSource, TKey>(this IOrderedQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return (IOrderedQueryable<TSource>)source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.ThenByDescending_TSource_TKey_3(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector), Expression.Constant(comparer, typeof(IComparer<TKey>))));
		}

		/// <summary>Returns a specified number of contiguous elements from the start of a sequence.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="count">The number of elements to return.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains the specified number of elements from the start of <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000466 RID: 1126 RVA: 0x0000D73C File Offset: 0x0000B93C
		public static IQueryable<TSource> Take<TSource>(this IQueryable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Take_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(count)));
		}

		/// <summary>Returns elements from a sequence as long as a specified condition is true.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements from the input sequence occurring before the element at which the test specified by <paramref name="predicate" /> no longer passes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06000467 RID: 1127 RVA: 0x0000D788 File Offset: 0x0000B988
		public static IQueryable<TSource> TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.TakeWhile_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns elements from a sequence as long as a specified condition is true. The element's index is used in the logic of the predicate function.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition; the second parameter of the function represents the index of the element in the source sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements from the input sequence occurring before the element at which the test specified by <paramref name="predicate" /> no longer passes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06000468 RID: 1128 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public static IQueryable<TSource> TakeWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.TakeWhile_Index_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Bypasses a specified number of elements in a sequence and then returns the remaining elements.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return elements from.</param>
		/// <param name="count">The number of elements to skip before returning the remaining elements.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements that occur after the specified index in the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000469 RID: 1129 RVA: 0x0000D838 File Offset: 0x0000BA38
		public static IQueryable<TSource> Skip<TSource>(this IQueryable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Skip_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(count)));
		}

		/// <summary>Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements from <paramref name="source" /> starting at the first element in the linear series that does not pass the test specified by <paramref name="predicate" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x0600046A RID: 1130 RVA: 0x0000D884 File Offset: 0x0000BA84
		public static IQueryable<TSource> SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.SkipWhile_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements. The element's index is used in the logic of the predicate function.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition; the second parameter of this function represents the index of the source element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains elements from <paramref name="source" /> starting at the first element in the linear series that does not pass the test specified by <paramref name="predicate" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x0600046B RID: 1131 RVA: 0x0000D8DC File Offset: 0x0000BADC
		public static IQueryable<TSource> SkipWhile<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.SkipWhile_Index_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented in <paramref name="keySelector" />.</typeparam>
		/// <returns>An IQueryable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IQueryable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a sequence of objects and a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is <see langword="null" />.</exception>
		// Token: 0x0600046C RID: 1132 RVA: 0x0000D934 File Offset: 0x0000BB34
		public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return source.Provider.CreateQuery<IGrouping<TKey, TSource>>(Expression.Call(null, CachedReflectionInfo.GroupBy_TSource_TKey_2(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector)));
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and projects the elements for each group by using a specified function.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented in <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <returns>An IQueryable&lt;IGrouping&lt;TKey, TElement&gt;&gt; in C# or IQueryable(Of IGrouping(Of TKey, TElement)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> contains a sequence of objects of type <paramref name="TElement" /> and a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is <see langword="null" />.</exception>
		// Token: 0x0600046D RID: 1133 RVA: 0x0000D994 File Offset: 0x0000BB94
		public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			return source.Provider.CreateQuery<IGrouping<TKey, TElement>>(Expression.Call(null, CachedReflectionInfo.GroupBy_TSource_TKey_TElement_3(typeof(TSource), typeof(TKey), typeof(TElement)), source.Expression, Expression.Quote(keySelector), Expression.Quote(elementSelector)));
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and compares the keys by using a specified comparer.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented in <paramref name="keySelector" />.</typeparam>
		/// <returns>An IQueryable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IQueryable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> contains a sequence of objects and a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x0600046E RID: 1134 RVA: 0x0000DA14 File Offset: 0x0000BC14
		public static IQueryable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			return source.Provider.CreateQuery<IGrouping<TKey, TSource>>(Expression.Call(null, CachedReflectionInfo.GroupBy_TSource_TKey_3(typeof(TSource), typeof(TKey)), source.Expression, Expression.Quote(keySelector), Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))));
		}

		/// <summary>Groups the elements of a sequence and projects the elements for each group by using a specified function. Key values are compared by using a specified comparer.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented in <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <returns>An IQueryable&lt;IGrouping&lt;TKey, TElement&gt;&gt; in C# or IQueryable(Of IGrouping(Of TKey, TElement)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> contains a sequence of objects of type <paramref name="TElement" /> and a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x0600046F RID: 1135 RVA: 0x0000DA84 File Offset: 0x0000BC84
		public static IQueryable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			return source.Provider.CreateQuery<IGrouping<TKey, TElement>>(Expression.Call(null, CachedReflectionInfo.GroupBy_TSource_TKey_TElement_4(typeof(TSource), typeof(TKey), typeof(TElement)), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The elements of each group are projected by using a specified function.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented in <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <returns>An T:System.Linq.IQueryable`1 that has a type argument of <paramref name="TResult" /> and where each element represents a projection over a group and its key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> or <paramref name="resultSelector" /> is <see langword="null" />.</exception>
		// Token: 0x06000470 RID: 1136 RVA: 0x0000DB24 File Offset: 0x0000BD24
		public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.GroupBy_TSource_TKey_TElement_TResult_4(typeof(TSource), typeof(TKey), typeof(TElement), typeof(TResult)), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Quote(resultSelector)
			}));
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented in <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <returns>An T:System.Linq.IQueryable`1 that has a type argument of <paramref name="TResult" /> and where each element represents a projection over a group and its key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="resultSelector" /> is <see langword="null" />.</exception>
		// Token: 0x06000471 RID: 1137 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
		public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.GroupBy_TSource_TKey_TResult_3(typeof(TSource), typeof(TKey), typeof(TResult)), source.Expression, Expression.Quote(keySelector), Expression.Quote(resultSelector)));
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. Keys are compared by using a specified comparer.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented in <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <returns>An T:System.Linq.IQueryable`1 that has a type argument of <paramref name="TResult" /> and where each element represents a projection over a group and its key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="resultSelector" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x06000472 RID: 1138 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public static IQueryable<TResult> GroupBy<TSource, TKey, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.GroupBy_TSource_TKey_TResult_4(typeof(TSource), typeof(TKey), typeof(TResult)), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. Keys are compared by using a specified comparer and the elements of each group are projected by using a specified function.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by the function represented in <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <returns>An T:System.Linq.IQueryable`1 that has a type argument of <paramref name="TResult" /> and where each element represents a projection over a group and its key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> or <paramref name="resultSelector" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x06000473 RID: 1139 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		public static IQueryable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, Expression<Func<TSource, TElement>> elementSelector, Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (keySelector == null)
			{
				throw Error.ArgumentNull("keySelector");
			}
			if (elementSelector == null)
			{
				throw Error.ArgumentNull("elementSelector");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.GroupBy_TSource_TKey_TElement_TResult_5(typeof(TSource), typeof(TKey), typeof(TElement), typeof(TResult)), new Expression[]
			{
				source.Expression,
				Expression.Quote(keySelector),
				Expression.Quote(elementSelector),
				Expression.Quote(resultSelector),
				Expression.Constant(comparer, typeof(IEqualityComparer<TKey>))
			}));
		}

		/// <summary>Returns distinct elements from a sequence by using the default equality comparer to compare values.</summary>
		/// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to remove duplicates from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains distinct elements from <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000474 RID: 1140 RVA: 0x0000DDB5 File Offset: 0x0000BFB5
		public static IQueryable<TSource> Distinct<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Distinct_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns distinct elements from a sequence by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to remove duplicates from.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains distinct elements from <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="comparer" /> is <see langword="null" />.</exception>
		// Token: 0x06000475 RID: 1141 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		public static IQueryable<TSource> Distinct<TSource>(this IQueryable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Distinct_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))));
		}

		/// <summary>Concatenates two sequences.</summary>
		/// <param name="source1">The first sequence to concatenate.</param>
		/// <param name="source2">The sequence to concatenate to the first sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains the concatenated elements of the two input sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x06000476 RID: 1142 RVA: 0x0000DE40 File Offset: 0x0000C040
		public static IQueryable<TSource> Concat<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Concat_TSource_2(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2)));
		}

		/// <summary>Merges two sequences by using the specified predicate function.</summary>
		/// <param name="source1">The first sequence to merge.</param>
		/// <param name="source2">The second sequence to merge.</param>
		/// <param name="resultSelector">A function that specifies how to merge the elements from the two sequences.</param>
		/// <typeparam name="TFirst">The type of the elements of the first input sequence.</typeparam>
		/// <typeparam name="TSecond">The type of the elements of the second input sequence.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the result sequence.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains merged elements of two input sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" />or <paramref name="source2 " />is <see langword="null" />.</exception>
		// Token: 0x06000477 RID: 1143 RVA: 0x0000DE98 File Offset: 0x0000C098
		public static IQueryable<TResult> Zip<TFirst, TSecond, TResult>(this IQueryable<TFirst> source1, IEnumerable<TSecond> source2, Expression<Func<TFirst, TSecond, TResult>> resultSelector)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			if (resultSelector == null)
			{
				throw Error.ArgumentNull("resultSelector");
			}
			return source1.Provider.CreateQuery<TResult>(Expression.Call(null, CachedReflectionInfo.Zip_TFirst_TSecond_TResult_3(typeof(TFirst), typeof(TSecond), typeof(TResult)), source1.Expression, Queryable.GetSourceExpression<TSecond>(source2), Expression.Quote(resultSelector)));
		}

		/// <summary>Produces the set union of two sequences by using the default equality comparer.</summary>
		/// <param name="source1">A sequence whose distinct elements form the first set for the union operation.</param>
		/// <param name="source2">A sequence whose distinct elements form the second set for the union operation.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x06000478 RID: 1144 RVA: 0x0000DF18 File Offset: 0x0000C118
		public static IQueryable<TSource> Union<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Union_TSource_2(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2)));
		}

		/// <summary>Produces the set union of two sequences by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="source1">A sequence whose distinct elements form the first set for the union operation.</param>
		/// <param name="source2">A sequence whose distinct elements form the second set for the union operation.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x06000479 RID: 1145 RVA: 0x0000DF70 File Offset: 0x0000C170
		public static IQueryable<TSource> Union<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Union_TSource_3(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2), Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))));
		}

		/// <summary>Produces the set intersection of two sequences by using the default equality comparer to compare values.</summary>
		/// <param name="source1">A sequence whose distinct elements that also appear in <paramref name="source2" /> are returned.</param>
		/// <param name="source2">A sequence whose distinct elements that also appear in the first sequence are returned.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>A sequence that contains the set intersection of the two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x0600047A RID: 1146 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public static IQueryable<TSource> Intersect<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Intersect_TSource_2(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2)));
		}

		/// <summary>Produces the set intersection of two sequences by using the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <param name="source1">An <see cref="T:System.Linq.IQueryable`1" /> whose distinct elements that also appear in <paramref name="source2" /> are returned.</param>
		/// <param name="source2">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in the first sequence are returned.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains the set intersection of the two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x0600047B RID: 1147 RVA: 0x0000E030 File Offset: 0x0000C230
		public static IQueryable<TSource> Intersect<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Intersect_TSource_3(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2), Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))));
		}

		/// <summary>Produces the set difference of two sequences by using the default equality comparer to compare values.</summary>
		/// <param name="source1">An <see cref="T:System.Linq.IQueryable`1" /> whose elements that are not also in <paramref name="source2" /> will be returned.</param>
		/// <param name="source2">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that also occur in the first sequence will not appear in the returned sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains the set difference of the two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x0600047C RID: 1148 RVA: 0x0000E098 File Offset: 0x0000C298
		public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Except_TSource_2(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2)));
		}

		/// <summary>Produces the set difference of two sequences by using the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <param name="source1">An <see cref="T:System.Linq.IQueryable`1" /> whose elements that are not also in <paramref name="source2" /> will be returned.</param>
		/// <param name="source2">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that also occur in the first sequence will not appear in the returned sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains the set difference of the two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x0600047D RID: 1149 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		public static IQueryable<TSource> Except<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Except_TSource_3(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2), Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))));
		}

		/// <summary>Returns the first element of a sequence.</summary>
		/// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The first element in <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
		// Token: 0x0600047E RID: 1150 RVA: 0x0000E155 File Offset: 0x0000C355
		public static TSource First<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.First_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns the first element of a sequence that satisfies a specified condition.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The first element in <paramref name="source" /> that passes the test in <paramref name="predicate" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
		// Token: 0x0600047F RID: 1151 RVA: 0x0000E18C File Offset: 0x0000C38C
		public static TSource First<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.First_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns the first element of a sequence, or a default value if the sequence contains no elements.</summary>
		/// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     default(<paramref name="TSource" />) if <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000480 RID: 1152 RVA: 0x0000E1E1 File Offset: 0x0000C3E1
		public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.FirstOrDefault_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     default(<paramref name="TSource" />) if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06000481 RID: 1153 RVA: 0x0000E218 File Offset: 0x0000C418
		public static TSource FirstOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.FirstOrDefault_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns the last element in a sequence.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return the last element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The value at the last position in <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
		// Token: 0x06000482 RID: 1154 RVA: 0x0000E26D File Offset: 0x0000C46D
		public static TSource Last<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.Last_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns the last element of a sequence that satisfies a specified condition.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The last element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
		// Token: 0x06000483 RID: 1155 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		public static TSource Last<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.Last_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns the last element in a sequence, or a default value if the sequence contains no elements.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return the last element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     default(<paramref name="TSource" />) if <paramref name="source" /> is empty; otherwise, the last element in <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000484 RID: 1156 RVA: 0x0000E2F9 File Offset: 0x0000C4F9
		public static TSource LastOrDefault<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.LastOrDefault_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     default(<paramref name="TSource" />) if <paramref name="source" /> is empty or if no elements pass the test in the predicate function; otherwise, the last element of <paramref name="source" /> that passes the test in the predicate function.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06000485 RID: 1157 RVA: 0x0000E330 File Offset: 0x0000C530
		public static TSource LastOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.LastOrDefault_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The single element of the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> has more than one element.</exception>
		// Token: 0x06000486 RID: 1158 RVA: 0x0000E385 File Offset: 0x0000C585
		public static TSource Single<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.Single_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The single element of the input sequence that satisfies the condition in <paramref name="predicate" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-More than one element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
		// Token: 0x06000487 RID: 1159 RVA: 0x0000E3BC File Offset: 0x0000C5BC
		public static TSource Single<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.Single_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The single element of the input sequence, or default(<paramref name="TSource" />) if the sequence contains no elements.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> has more than one element.</exception>
		// Token: 0x06000488 RID: 1160 RVA: 0x0000E411 File Offset: 0x0000C611
		public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.SingleOrDefault_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The single element of the input sequence that satisfies the condition in <paramref name="predicate" />, or default(<paramref name="TSource" />) if no such element is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate" />.</exception>
		// Token: 0x06000489 RID: 1161 RVA: 0x0000E448 File Offset: 0x0000C648
		public static TSource SingleOrDefault<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.SingleOrDefault_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns the element at a specified index in a sequence.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The element at the specified position in <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> is less than zero.</exception>
		// Token: 0x0600048A RID: 1162 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		public static TSource ElementAt<TSource>(this IQueryable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.ElementAt_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(index)));
		}

		/// <summary>Returns the element at a specified index in a sequence or a default value if the index is out of range.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     default(<paramref name="TSource" />) if <paramref name="index" /> is outside the bounds of <paramref name="source" />; otherwise, the element at the specified position in <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600048B RID: 1163 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		public static TSource ElementAtOrDefault<TSource>(this IQueryable<TSource> source, int index)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.ElementAtOrDefault_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(index)));
		}

		/// <summary>Returns the elements of the specified sequence or the type parameter's default value in a singleton collection if the sequence is empty.</summary>
		/// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return a default value for if empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains <see langword="default" />(<paramref name="TSource" />) if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600048C RID: 1164 RVA: 0x0000E548 File Offset: 0x0000C748
		public static IQueryable<TSource> DefaultIfEmpty<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.DefaultIfEmpty_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns the elements of the specified sequence or the specified value in a singleton collection if the sequence is empty.</summary>
		/// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> to return the specified value for if empty.</param>
		/// <param name="defaultValue">The value to return if the sequence is empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> that contains <paramref name="defaultValue" /> if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600048D RID: 1165 RVA: 0x0000E580 File Offset: 0x0000C780
		public static IQueryable<TSource> DefaultIfEmpty<TSource>(this IQueryable<TSource> source, TSource defaultValue)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.DefaultIfEmpty_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(defaultValue, typeof(TSource))));
		}

		/// <summary>Determines whether a sequence contains a specified element by using the default equality comparer.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> in which to locate <paramref name="item" />.</param>
		/// <param name="item">The object to locate in the sequence.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     <see langword="true" /> if the input sequence contains an element that has the specified value; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600048E RID: 1166 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		public static bool Contains<TSource>(this IQueryable<TSource> source, TSource item)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, CachedReflectionInfo.Contains_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(item, typeof(TSource))));
		}

		/// <summary>Determines whether a sequence contains a specified element by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> in which to locate <paramref name="item" />.</param>
		/// <param name="item">The object to locate in the sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     <see langword="true" /> if the input sequence contains an element that has the specified value; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600048F RID: 1167 RVA: 0x0000E630 File Offset: 0x0000C830
		public static bool Contains<TSource>(this IQueryable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, CachedReflectionInfo.Contains_TSource_3(typeof(TSource)), source.Expression, Expression.Constant(item, typeof(TSource)), Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))));
		}

		/// <summary>Inverts the order of the elements in a sequence.</summary>
		/// <param name="source">A sequence of values to reverse.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Linq.IQueryable`1" /> whose elements correspond to those of the input sequence in reverse order.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000490 RID: 1168 RVA: 0x0000E696 File Offset: 0x0000C896
		public static IQueryable<TSource> Reverse<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Reverse_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Determines whether two sequences are equal by using the default equality comparer to compare elements.</summary>
		/// <param name="source1">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to compare to those of <paramref name="source2" />.</param>
		/// <param name="source2">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to compare to those of the first sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>
		///     <see langword="true" /> if the two source sequences are of equal length and their corresponding elements compare equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x06000491 RID: 1169 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		public static bool SequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.Execute<bool>(Expression.Call(null, CachedReflectionInfo.SequenceEqual_TSource_2(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2)));
		}

		/// <summary>Determines whether two sequences are equal by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare elements.</summary>
		/// <param name="source1">An <see cref="T:System.Linq.IQueryable`1" /> whose elements to compare to those of <paramref name="source2" />.</param>
		/// <param name="source2">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to compare to those of the first sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to use to compare elements.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>
		///     <see langword="true" /> if the two source sequences are of equal length and their corresponding elements compare equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source1" /> or <paramref name="source2" /> is <see langword="null" />.</exception>
		// Token: 0x06000492 RID: 1170 RVA: 0x0000E724 File Offset: 0x0000C924
		public static bool SequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
		{
			if (source1 == null)
			{
				throw Error.ArgumentNull("source1");
			}
			if (source2 == null)
			{
				throw Error.ArgumentNull("source2");
			}
			return source1.Provider.Execute<bool>(Expression.Call(null, CachedReflectionInfo.SequenceEqual_TSource_3(typeof(TSource)), source1.Expression, Queryable.GetSourceExpression<TSource>(source2), Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))));
		}

		/// <summary>Determines whether a sequence contains any elements.</summary>
		/// <param name="source">A sequence to check for being empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     <see langword="true" /> if the source sequence contains any elements; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000493 RID: 1171 RVA: 0x0000E789 File Offset: 0x0000C989
		public static bool Any<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, CachedReflectionInfo.Any_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Determines whether any element of a sequence satisfies a condition.</summary>
		/// <param name="source">A sequence whose elements to test for a condition.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     <see langword="true" /> if any elements in the source sequence pass the test in the specified predicate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06000494 RID: 1172 RVA: 0x0000E7C0 File Offset: 0x0000C9C0
		public static bool Any<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, CachedReflectionInfo.Any_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Determines whether all the elements of a sequence satisfy a condition.</summary>
		/// <param name="source">A sequence whose elements to test for a condition.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>
		///     <see langword="true" /> if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		// Token: 0x06000495 RID: 1173 RVA: 0x0000E818 File Offset: 0x0000CA18
		public static bool All<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<bool>(Expression.Call(null, CachedReflectionInfo.All_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns the number of elements in a sequence.</summary>
		/// <param name="source">The <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The number of elements in the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000496 RID: 1174 RVA: 0x0000E86D File Offset: 0x0000CA6D
		public static int Count<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<int>(Expression.Call(null, CachedReflectionInfo.Count_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns the number of elements in the specified sequence that satisfies a condition.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The number of elements in the sequence that satisfies the condition in the predicate function.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000497 RID: 1175 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		public static int Count<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<int>(Expression.Call(null, CachedReflectionInfo.Count_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns an <see cref="T:System.Int64" /> that represents the total number of elements in a sequence.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The number of elements in <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The number of elements exceeds <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000498 RID: 1176 RVA: 0x0000E8F9 File Offset: 0x0000CAF9
		public static long LongCount<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<long>(Expression.Call(null, CachedReflectionInfo.LongCount_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Returns an <see cref="T:System.Int64" /> that represents the number of elements in a sequence that satisfy a condition.</summary>
		/// <param name="source">An <see cref="T:System.Linq.IQueryable`1" /> that contains the elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The number of elements in <paramref name="source" /> that satisfy the condition in the predicate function.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The number of matching elements exceeds <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06000499 RID: 1177 RVA: 0x0000E930 File Offset: 0x0000CB30
		public static long LongCount<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			return source.Provider.Execute<long>(Expression.Call(null, CachedReflectionInfo.LongCount_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(predicate)));
		}

		/// <summary>Returns the minimum value of a generic <see cref="T:System.Linq.IQueryable`1" />.</summary>
		/// <param name="source">A sequence of values to determine the minimum of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600049A RID: 1178 RVA: 0x0000E985 File Offset: 0x0000CB85
		public static TSource Min<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.Min_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Invokes a projection function on each element of a generic <see cref="T:System.Linq.IQueryable`1" /> and returns the minimum resulting value.</summary>
		/// <param name="source">A sequence of values to determine the minimum of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by the function represented by <paramref name="selector" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x0600049B RID: 1179 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		public static TResult Min<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<TResult>(Expression.Call(null, CachedReflectionInfo.Min_TSource_TResult_2(typeof(TSource), typeof(TResult)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Returns the maximum value in a generic <see cref="T:System.Linq.IQueryable`1" />.</summary>
		/// <param name="source">A sequence of values to determine the maximum of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x0600049C RID: 1180 RVA: 0x0000EA1B File Offset: 0x0000CC1B
		public static TSource Max<TSource>(this IQueryable<TSource> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.Max_TSource_1(typeof(TSource)), source.Expression));
		}

		/// <summary>Invokes a projection function on each element of a generic <see cref="T:System.Linq.IQueryable`1" /> and returns the maximum resulting value.</summary>
		/// <param name="source">A sequence of values to determine the maximum of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by the function represented by <paramref name="selector" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x0600049D RID: 1181 RVA: 0x0000EA54 File Offset: 0x0000CC54
		public static TResult Max<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<TResult>(Expression.Call(null, CachedReflectionInfo.Max_TSource_TResult_2(typeof(TSource), typeof(TResult)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600049E RID: 1182 RVA: 0x0000EAB3 File Offset: 0x0000CCB3
		public static int Sum(this IQueryable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<int>(Expression.Call(null, CachedReflectionInfo.Sum_Int32_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x0600049F RID: 1183 RVA: 0x0000EADF File Offset: 0x0000CCDF
		public static int? Sum(this IQueryable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<int?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableInt32_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Int64" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Int64" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000EB0B File Offset: 0x0000CD0B
		public static long Sum(this IQueryable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<long>(Expression.Call(null, CachedReflectionInfo.Sum_Int64_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000EB37 File Offset: 0x0000CD37
		public static long? Sum(this IQueryable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<long?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableInt64_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Single" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Single" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004A2 RID: 1186 RVA: 0x0000EB63 File Offset: 0x0000CD63
		public static float Sum(this IQueryable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<float>(Expression.Call(null, CachedReflectionInfo.Sum_Single_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Single" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004A3 RID: 1187 RVA: 0x0000EB8F File Offset: 0x0000CD8F
		public static float? Sum(this IQueryable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<float?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableSingle_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Double" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Double" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000EBBB File Offset: 0x0000CDBB
		public static double Sum(this IQueryable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double>(Expression.Call(null, CachedReflectionInfo.Sum_Double_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Double" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000EBE7 File Offset: 0x0000CDE7
		public static double? Sum(this IQueryable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableDouble_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000EC13 File Offset: 0x0000CE13
		public static decimal Sum(this IQueryable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<decimal>(Expression.Call(null, CachedReflectionInfo.Sum_Decimal_1, source.Expression));
		}

		/// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to calculate the sum of.</param>
		/// <returns>The sum of the values in the sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000EC3F File Offset: 0x0000CE3F
		public static decimal? Sum(this IQueryable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<decimal?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableDecimal_1, source.Expression));
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Int32" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public static int Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<int>(Expression.Call(null, CachedReflectionInfo.Sum_Int32_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Int32" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		public static int? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<int?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableInt32_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Int64" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060004AA RID: 1194 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public static long Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<long>(Expression.Call(null, CachedReflectionInfo.Sum_Int64_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Int64" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060004AB RID: 1195 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public static long? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<long?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableInt64_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Single" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004AC RID: 1196 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		public static float Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<float>(Expression.Call(null, CachedReflectionInfo.Sum_Single_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Single" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004AD RID: 1197 RVA: 0x0000EE24 File Offset: 0x0000D024
		public static float? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<float?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableSingle_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Double" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004AE RID: 1198 RVA: 0x0000EE7C File Offset: 0x0000D07C
		public static double Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double>(Expression.Call(null, CachedReflectionInfo.Sum_Double_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Double" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004AF RID: 1199 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public static double? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableDouble_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of <see cref="T:System.Decimal" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000EF2C File Offset: 0x0000D12C
		public static decimal Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<decimal>(Expression.Call(null, CachedReflectionInfo.Sum_Decimal_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Decimal" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values of type <paramref name="TSource" />.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x060004B1 RID: 1201 RVA: 0x0000EF84 File Offset: 0x0000D184
		public static decimal? Sum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<decimal?>(Expression.Call(null, CachedReflectionInfo.Sum_NullableDecimal_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Int32" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Int32" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004B2 RID: 1202 RVA: 0x0000EFD9 File Offset: 0x0000D1D9
		public static double Average(this IQueryable<int> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double>(Expression.Call(null, CachedReflectionInfo.Average_Int32_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the source sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000F005 File Offset: 0x0000D205
		public static double? Average(this IQueryable<int?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, CachedReflectionInfo.Average_NullableInt32_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Int64" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Int64" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000F031 File Offset: 0x0000D231
		public static double Average(this IQueryable<long> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double>(Expression.Call(null, CachedReflectionInfo.Average_Int64_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the source sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000F05D File Offset: 0x0000D25D
		public static double? Average(this IQueryable<long?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, CachedReflectionInfo.Average_NullableInt64_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Single" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Single" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000F089 File Offset: 0x0000D289
		public static float Average(this IQueryable<float> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<float>(Expression.Call(null, CachedReflectionInfo.Average_Single_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Single" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the source sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004B7 RID: 1207 RVA: 0x0000F0B5 File Offset: 0x0000D2B5
		public static float? Average(this IQueryable<float?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<float?>(Expression.Call(null, CachedReflectionInfo.Average_NullableSingle_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Double" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Double" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000F0E1 File Offset: 0x0000D2E1
		public static double Average(this IQueryable<double> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double>(Expression.Call(null, CachedReflectionInfo.Average_Double_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Double" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the source sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000F10D File Offset: 0x0000D30D
		public static double? Average(this IQueryable<double?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, CachedReflectionInfo.Average_NullableDouble_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004BA RID: 1210 RVA: 0x0000F139 File Offset: 0x0000D339
		public static decimal Average(this IQueryable<decimal> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<decimal>(Expression.Call(null, CachedReflectionInfo.Average_Decimal_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to calculate the average of.</param>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the source sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x060004BB RID: 1211 RVA: 0x0000F165 File Offset: 0x0000D365
		public static decimal? Average(this IQueryable<decimal?> source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.Execute<decimal?>(Expression.Call(null, CachedReflectionInfo.Average_NullableDecimal_1, source.Expression));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Int32" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004BC RID: 1212 RVA: 0x0000F194 File Offset: 0x0000D394
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double>(Expression.Call(null, CachedReflectionInfo.Average_Int32_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int32" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the <paramref name="source" /> sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004BD RID: 1213 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, CachedReflectionInfo.Average_NullableInt32_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Single" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004BE RID: 1214 RVA: 0x0000F244 File Offset: 0x0000D444
		public static float Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<float>(Expression.Call(null, CachedReflectionInfo.Average_Single_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Single" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the <paramref name="source" /> sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004BF RID: 1215 RVA: 0x0000F29C File Offset: 0x0000D49C
		public static float? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<float?>(Expression.Call(null, CachedReflectionInfo.Average_NullableSingle_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Int64" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004C0 RID: 1216 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double>(Expression.Call(null, CachedReflectionInfo.Average_Int64_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int64" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the <paramref name="source" /> sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004C1 RID: 1217 RVA: 0x0000F34C File Offset: 0x0000D54C
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, CachedReflectionInfo.Average_NullableInt64_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Double" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		public static double Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double>(Expression.Call(null, CachedReflectionInfo.Average_Double_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Double" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the <paramref name="source" /> sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004C3 RID: 1219 RVA: 0x0000F3FC File Offset: 0x0000D5FC
		public static double? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<double?>(Expression.Call(null, CachedReflectionInfo.Average_NullableDouble_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of <see cref="T:System.Decimal" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004C4 RID: 1220 RVA: 0x0000F454 File Offset: 0x0000D654
		public static decimal Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<decimal>(Expression.Call(null, CachedReflectionInfo.Average_Decimal_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Computes the average of a sequence of nullable <see cref="T:System.Decimal" /> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the average of.</param>
		/// <param name="selector">A projection function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values, or <see langword="null" /> if the <paramref name="source" /> sequence is empty or contains only <see langword="null" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		public static decimal? Average<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<decimal?>(Expression.Call(null, CachedReflectionInfo.Average_NullableDecimal_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(selector)));
		}

		/// <summary>Applies an accumulator function over a sequence.</summary>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="func">An accumulator function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The final accumulator value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="func" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000F504 File Offset: 0x0000D704
		public static TSource Aggregate<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource, TSource>> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			return source.Provider.Execute<TSource>(Expression.Call(null, CachedReflectionInfo.Aggregate_TSource_2(typeof(TSource)), source.Expression, Expression.Quote(func)));
		}

		/// <summary>Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value.</summary>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">An accumulator function to invoke on each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <returns>The final accumulator value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="func" /> is <see langword="null" />.</exception>
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000F55C File Offset: 0x0000D75C
		public static TAccumulate Aggregate<TSource, TAccumulate>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			return source.Provider.Execute<TAccumulate>(Expression.Call(null, CachedReflectionInfo.Aggregate_TSource_TAccumulate_3(typeof(TSource), typeof(TAccumulate)), source.Expression, Expression.Constant(seed), Expression.Quote(func)));
		}

		/// <summary>Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.</summary>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">An accumulator function to invoke on each element.</param>
		/// <param name="selector">A function to transform the final accumulator value into the result value.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <typeparam name="TResult">The type of the resulting value.</typeparam>
		/// <returns>The transformed final accumulator value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="func" /> or <paramref name="selector" /> is <see langword="null" />.</exception>
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func, Expression<Func<TAccumulate, TResult>> selector)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			if (func == null)
			{
				throw Error.ArgumentNull("func");
			}
			if (selector == null)
			{
				throw Error.ArgumentNull("selector");
			}
			return source.Provider.Execute<TResult>(Expression.Call(null, CachedReflectionInfo.Aggregate_TSource_TAccumulate_TResult_4(typeof(TSource), typeof(TAccumulate), typeof(TResult)), new Expression[]
			{
				source.Expression,
				Expression.Constant(seed),
				Expression.Quote(func),
				Expression.Quote(selector)
			}));
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000F664 File Offset: 0x0000D864
		public static IQueryable<TSource> SkipLast<TSource>(this IQueryable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.SkipLast_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(count)));
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
		public static IQueryable<TSource> TakeLast<TSource>(this IQueryable<TSource> source, int count)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.TakeLast_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(count)));
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public static IQueryable<TSource> Append<TSource>(this IQueryable<TSource> source, TSource element)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Append_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(element)));
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000F748 File Offset: 0x0000D948
		public static IQueryable<TSource> Prepend<TSource>(this IQueryable<TSource> source, TSource element)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			return source.Provider.CreateQuery<TSource>(Expression.Call(null, CachedReflectionInfo.Prepend_TSource_2(typeof(TSource)), source.Expression, Expression.Constant(element)));
		}
	}
}
