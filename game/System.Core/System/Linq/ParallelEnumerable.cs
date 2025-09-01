using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Parallel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Linq
{
	/// <summary>Provides a set of methods for querying objects that implement ParallelQuery{TSource}. This is the parallel equivalent of <see cref="T:System.Linq.Enumerable" />.</summary>
	// Token: 0x02000082 RID: 130
	public static class ParallelEnumerable
	{
		/// <summary>Enables parallelization of a query.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to convert to a <see cref="T:System.Linq.ParallelQuery`1" />.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The source as a <see cref="T:System.Linq.ParallelQuery`1" /> to bind to ParallelEnumerable extension methods.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x060002AE RID: 686 RVA: 0x000081B7 File Offset: 0x000063B7
		public static ParallelQuery<TSource> AsParallel<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ParallelEnumerableWrapper<TSource>(source);
		}

		/// <summary>Enables parallelization of a query, as sourced by a custom partitioner that is responsible for splitting the input sequence into partitions.</summary>
		/// <param name="source">A partitioner over the input sequence.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The <paramref name="source" /> as a ParallelQuery to bind to ParallelEnumerable extension methods.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x060002AF RID: 687 RVA: 0x000081CD File Offset: 0x000063CD
		public static ParallelQuery<TSource> AsParallel<TSource>(this Partitioner<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new PartitionerQueryOperator<TSource>(source);
		}

		/// <summary>Enables treatment of a data source as if it were ordered, overriding the default of unordered. AsOrdered may only be invoked on generic sequences returned by AsParallel, ParallelEnumerable.Range, and ParallelEnumerable.Repeat.</summary>
		/// <param name="source">The input sequence.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The source sequence which will maintain the original ordering in the subsequent query operators.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidOperationException">Thrown if <paramref name="source" /> contains no elements-or-if <paramref name="source" /> is not one of AsParallel, ParallelEnumerable.Range, or ParallelEnumerable.Repeat.</exception>
		// Token: 0x060002B0 RID: 688 RVA: 0x000081E4 File Offset: 0x000063E4
		public static ParallelQuery<TSource> AsOrdered<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!(source is ParallelEnumerableWrapper<TSource>) && !(source is IParallelPartitionable<TSource>))
			{
				PartitionerQueryOperator<TSource> partitionerQueryOperator = source as PartitionerQueryOperator<TSource>;
				if (partitionerQueryOperator == null)
				{
					throw new InvalidOperationException("AsOrdered may only be called on the result of AsParallel, ParallelEnumerable.Range, or ParallelEnumerable.Repeat.");
				}
				if (!partitionerQueryOperator.Orderable)
				{
					throw new InvalidOperationException("AsOrdered may not be used with a partitioner that is not orderable.");
				}
			}
			return new OrderingQueryOperator<TSource>(QueryOperator<TSource>.AsQueryOperator(source), true);
		}

		/// <summary>Enables treatment of a data source as if it were ordered, overriding the default of unordered. AsOrdered may only be invoked on non-generic sequences returned by AsParallel, ParallelEnumerable.Range, and ParallelEnumerable.Repeat.</summary>
		/// <param name="source">The input sequence.</param>
		/// <returns>The source sequence which will maintain the original ordering in the subsequent query operators.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidOperationException">thrown if <paramref name="source" /> contains no elements-or- if AsOrdered is called midway through a query. It is allowed to be called immediately after <see cref="M:System.Linq.ParallelEnumerable.AsParallel(System.Collections.IEnumerable)" />, <see cref="M:System.Linq.ParallelEnumerable.Range(System.Int32,System.Int32)" /> or <see cref="M:System.Linq.ParallelEnumerable.Repeat``1(``0,System.Int32)" />.</exception>
		// Token: 0x060002B1 RID: 689 RVA: 0x00008243 File Offset: 0x00006443
		public static ParallelQuery AsOrdered(this ParallelQuery source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper parallelEnumerableWrapper = source as ParallelEnumerableWrapper;
			if (parallelEnumerableWrapper == null)
			{
				throw new InvalidOperationException("Non-generic AsOrdered may only be called on the result of the non-generic AsParallel.");
			}
			return new OrderingQueryOperator<object>(QueryOperator<object>.AsQueryOperator(parallelEnumerableWrapper), true);
		}

		/// <summary>Allows an intermediate query to be treated as if no ordering is implied among the elements.</summary>
		/// <param name="source">The input sequence.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The source sequence with arbitrary order.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x060002B2 RID: 690 RVA: 0x00008272 File Offset: 0x00006472
		public static ParallelQuery<TSource> AsUnordered<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new OrderingQueryOperator<TSource>(QueryOperator<TSource>.AsQueryOperator(source), false);
		}

		/// <summary>Enables parallelization of a query.</summary>
		/// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to convert to a <see cref="T:System.Linq.ParallelQuery" />.</param>
		/// <returns>The source as a ParallelQuery to bind to ParallelEnumerable extension methods.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x060002B3 RID: 691 RVA: 0x0000828E File Offset: 0x0000648E
		public static ParallelQuery AsParallel(this IEnumerable source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ParallelEnumerableWrapper(source);
		}

		/// <summary>Converts a <see cref="T:System.Linq.ParallelQuery`1" /> into an <see cref="T:System.Collections.Generic.IEnumerable`1" /> to force sequential evaluation of the query.</summary>
		/// <param name="source">A <see cref="T:System.Linq.ParallelQuery`1" /> to convert to an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The source as an <see cref="T:System.Collections.Generic.IEnumerable`1" /> to bind to sequential extension methods.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x060002B4 RID: 692 RVA: 0x000082A4 File Offset: 0x000064A4
		public static IEnumerable<TSource> AsSequential<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				return parallelEnumerableWrapper.WrappedEnumerable;
			}
			return source;
		}

		/// <summary>Sets the degree of parallelism to use in a query. Degree of parallelism is the maximum number of concurrently executing tasks that will be used to process the query.</summary>
		/// <param name="source">A ParallelQuery on which to set the limit on the degrees of parallelism.</param>
		/// <param name="degreeOfParallelism">The degree of parallelism for the query. The default value is Math.Min(<see cref="P:System.Environment.ProcessorCount" />, <see langword="MAX_SUPPORTED_DOP" />) where <see langword="MAX_SUPPORTED_DOP" /> is 512. </param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>ParallelQuery representing the same query as source, with the limit on the degrees of parallelism set.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="degreeOfParallelism" /> is less than 1 or greater than 511. </exception>
		/// <exception cref="T:System.InvalidOperationException">WithDegreeOfParallelism is used multiple times in the query.</exception>
		// Token: 0x060002B5 RID: 693 RVA: 0x000082D4 File Offset: 0x000064D4
		public static ParallelQuery<TSource> WithDegreeOfParallelism<TSource>(this ParallelQuery<TSource> source, int degreeOfParallelism)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (degreeOfParallelism < 1 || degreeOfParallelism > 512)
			{
				throw new ArgumentOutOfRangeException("degreeOfParallelism");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.DegreeOfParallelism = new int?(degreeOfParallelism);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		/// <summary>Sets the <see cref="T:System.Threading.CancellationToken" /> to associate with the query.</summary>
		/// <param name="source">A ParallelQuery on which to set the option.</param>
		/// <param name="cancellationToken">A cancellation token.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>ParallelQuery representing the same query as source, but with the registered cancellation token.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <paramref name="cancellationToken" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="WithCancellation" /> is used multiple times in the query.</exception>
		// Token: 0x060002B6 RID: 694 RVA: 0x00008328 File Offset: 0x00006528
		public static ParallelQuery<TSource> WithCancellation<TSource>(this ParallelQuery<TSource> source, CancellationToken cancellationToken)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.CancellationState = new CancellationState(cancellationToken);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		/// <summary>Sets the execution mode of the query.</summary>
		/// <param name="source">A ParallelQuery on which to set the option.</param>
		/// <param name="executionMode">The mode in which to execute the query.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>ParallelQuery representing the same query as source, but with the registered execution mode.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="executionMode" /> is not a valid <see cref="T:System.Linq.ParallelExecutionMode" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">WithExecutionMode is used multiple times in the query.</exception>
		// Token: 0x060002B7 RID: 695 RVA: 0x00008364 File Offset: 0x00006564
		public static ParallelQuery<TSource> WithExecutionMode<TSource>(this ParallelQuery<TSource> source, ParallelExecutionMode executionMode)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (executionMode != ParallelExecutionMode.Default && executionMode != ParallelExecutionMode.ForceParallelism)
			{
				throw new ArgumentException("The executionMode argument contains an invalid value.");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.ExecutionMode = new ParallelExecutionMode?(executionMode);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		/// <summary>Sets the merge options for this query, which specify how the query will buffer output.</summary>
		/// <param name="source">A ParallelQuery on which to set the option.</param>
		/// <param name="mergeOptions">The merge options to set for this query.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>ParallelQuery representing the same query as source, but with the registered merge options.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="mergeOptions" /> is not a valid <see cref="T:System.Linq.ParallelMergeOptions" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="WithMergeOptions" /> is used multiple times in the query.</exception>
		// Token: 0x060002B8 RID: 696 RVA: 0x000083B0 File Offset: 0x000065B0
		public static ParallelQuery<TSource> WithMergeOptions<TSource>(this ParallelQuery<TSource> source, ParallelMergeOptions mergeOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (mergeOptions != ParallelMergeOptions.Default && mergeOptions != ParallelMergeOptions.AutoBuffered && mergeOptions != ParallelMergeOptions.NotBuffered && mergeOptions != ParallelMergeOptions.FullyBuffered)
			{
				throw new ArgumentException("The mergeOptions argument contains an invalid value.");
			}
			QuerySettings empty = QuerySettings.Empty;
			empty.MergeOptions = new ParallelMergeOptions?(mergeOptions);
			return new QueryExecutionOption<TSource>(QueryOperator<TSource>.AsQueryOperator(source), empty);
		}

		/// <summary>Generates a parallel sequence of integral numbers within a specified range.</summary>
		/// <param name="start">The value of the first integer in the sequence.</param>
		/// <param name="count">The number of sequential integers to generate.</param>
		/// <returns>An IEnumerable&lt;Int32&gt; in C# or IEnumerable(Of Int32) in Visual Basic that contains a range of sequential integral numbers.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="count" /> is less than 0 -or- <paramref name="start" /> + <paramref name="count" /> - 1 is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060002B9 RID: 697 RVA: 0x00008404 File Offset: 0x00006604
		public static ParallelQuery<int> Range(int start, int count)
		{
			if (count < 0 || (count > 0 && 2147483647 - (count - 1) < start))
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new RangeEnumerable(start, count);
		}

		/// <summary>Generates a parallel sequence that contains one repeated value.</summary>
		/// <param name="element">The value to be repeated.</param>
		/// <param name="count">The number of times to repeat the value in the generated sequence.</param>
		/// <typeparam name="TResult">The type of the value to be repeated in the result sequence.</typeparam>
		/// <returns>A sequence that contains a repeated value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="count" /> is less than 0.</exception>
		// Token: 0x060002BA RID: 698 RVA: 0x0000842C File Offset: 0x0000662C
		public static ParallelQuery<TResult> Repeat<TResult>(TResult element, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return new RepeatEnumerable<TResult>(element, count);
		}

		/// <summary>Returns an empty ParallelQuery{TResult} that has the specified type argument.</summary>
		/// <typeparam name="TResult">The type to assign to the type parameter of the returned generic sequence.</typeparam>
		/// <returns>An empty sequence whose type argument is <paramref name="TResult" />.</returns>
		// Token: 0x060002BB RID: 699 RVA: 0x00008444 File Offset: 0x00006644
		public static ParallelQuery<TResult> Empty<TResult>()
		{
			return EmptyEnumerable<TResult>.Instance;
		}

		/// <summary>Invokes in parallel the specified action for each element in the <paramref name="source" />.</summary>
		/// <param name="source">The <see cref="T:System.Linq.ParallelQuery`1" /> whose elements will be processed by <paramref name="action" />.</param>
		/// <param name="action">An Action to invoke on each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002BC RID: 700 RVA: 0x0000844B File Offset: 0x0000664B
		public static void ForAll<TSource>(this ParallelQuery<TSource> source, Action<TSource> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			new ForAllOperator<TSource>(source, action).RunSynchronously();
		}

		/// <summary>Filters in parallel a sequence of values based on a predicate.</summary>
		/// <param name="source">A sequence to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <returns>A sequence that contains elements from the input sequence that satisfy the condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002BD RID: 701 RVA: 0x00008475 File Offset: 0x00006675
		public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new WhereQueryOperator<TSource>(source, predicate);
		}

		/// <summary>Filters in parallel a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.</summary>
		/// <param name="source">A sequence to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of source.</typeparam>
		/// <returns>A sequence that contains elements from the input sequence that satisfy the condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002BE RID: 702 RVA: 0x0000849A File Offset: 0x0000669A
		public static ParallelQuery<TSource> Where<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new IndexedWhereQueryOperator<TSource>(source, predicate);
		}

		/// <summary>Projects in parallel each element of a sequence into a new form.</summary>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of elements resturned by selector.</typeparam>
		/// <returns>A sequence whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002BF RID: 703 RVA: 0x000084BF File Offset: 0x000066BF
		public static ParallelQuery<TResult> Select<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectQueryOperator<TSource, TResult>(source, selector);
		}

		/// <summary>Projects in parallel each element of a sequence into a new form by incorporating the element's index.</summary>
		/// <param name="source">A sequence of values to invoke a transform function on.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of elements resturned by selector.</typeparam>
		/// <returns>A sequence whose elements are the result of invoking the transform function on each element of <paramref name="source" />, based on the index supplied to <paramref name="selector" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query. This condition might occur in streaming scenarios.</exception>
		// Token: 0x060002C0 RID: 704 RVA: 0x000084E4 File Offset: 0x000066E4
		public static ParallelQuery<TResult> Select<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, TResult> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new IndexedSelectQueryOperator<TSource, TResult>(source, selector);
		}

		/// <summary>Merges in parallel two sequences by using the specified predicate function.</summary>
		/// <param name="first">The first sequence to zip.</param>
		/// <param name="second">The second sequence to zip.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <typeparam name="TFirst">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TSecond">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TResult">The type of the return elements.</typeparam>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing <paramref name="resultSelector" /> pairwise on two sequences. If the sequence lengths are unequal, this truncates to the length of the shorter sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="first" /> or <paramref name="second" /> or <paramref name="resultSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002C1 RID: 705 RVA: 0x00008509 File Offset: 0x00006709
		public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(this ParallelQuery<TFirst> first, ParallelQuery<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new ZipQueryOperator<TFirst, TSecond, TResult>(first, second, resultSelector);
		}

		/// <summary>This Zip overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when invoked.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <typeparam name="TFirst">This type parameter is not used.</typeparam>
		/// <typeparam name="TSecond">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x060002C2 RID: 706 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> Zip<TFirst, TSecond, TResult>(this ParallelQuery<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Correlates in parallel the elements of two sequences based on matching keys. The default equality comparer is used to compare keys.</summary>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <typeparam name="TOuter">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing an inner join on two sequences.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002C3 RID: 707 RVA: 0x00008549 File Offset: 0x00006749
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
		{
			return outer.Join(inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		/// <summary>This Join overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when invoked.</summary>
		/// <param name="outer">This parameter is not used.</param>
		/// <param name="inner">This parameter is not used.</param>
		/// <param name="outerKeySelector">This parameter is not used.</param>
		/// <param name="innerKeySelector">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <typeparam name="TOuter">This type parameter is not used.</typeparam>
		/// <typeparam name="TInner">This type parameter is not used.</typeparam>
		/// <typeparam name="TKey">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002C4 RID: 708 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Correlates in parallel the elements of two sequences based on matching keys. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from two matching elements.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
		/// <typeparam name="TOuter">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing an inner join on two sequences.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002C5 RID: 709 RVA: 0x00008558 File Offset: 0x00006758
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new JoinQueryOperator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		/// <summary>This Join overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when invoked.</summary>
		/// <param name="outer">This parameter is not used.</param>
		/// <param name="inner">This parameter is not used.</param>
		/// <param name="outerKeySelector">This parameter is not used.</param>
		/// <param name="innerKeySelector">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TOuter">This type parameter is not used.</typeparam>
		/// <typeparam name="TInner">This type parameter is not used.</typeparam>
		/// <typeparam name="TKey">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x060002C6 RID: 710 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Correlates in parallel the elements of two sequences based on equality of keys and groups the results. The default equality comparer is used to compare keys.</summary>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <typeparam name="TOuter">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing a grouped join on two sequences.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002C7 RID: 711 RVA: 0x000085B9 File Offset: 0x000067B9
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, null);
		}

		/// <summary>This GroupJoin overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="outer">This parameter is not used.</param>
		/// <param name="inner">This parameter is not used.</param>
		/// <param name="outerKeySelector">This parameter is not used.</param>
		/// <param name="innerKeySelector">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <typeparam name="TOuter">This type parameter is not used.</typeparam>
		/// <typeparam name="TInner">This type parameter is not used.</typeparam>
		/// <typeparam name="TKey">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002C8 RID: 712 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Correlates in parallel the elements of two sequences based on key equality and groups the results. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
		/// <param name="outer">The first sequence to join.</param>
		/// <param name="inner">The sequence to join to the first sequence.</param>
		/// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
		/// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
		/// <typeparam name="TOuter">The type of the elements of the second sequence.</typeparam>
		/// <typeparam name="TInner">The type of the elements of the first sequence.</typeparam>
		/// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
		/// <typeparam name="TResult">The type of the result elements.</typeparam>
		/// <returns>A sequence that has elements of type <paramref name="TResult" /> that are obtained by performing a grouped join on two sequences.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002C9 RID: 713 RVA: 0x000085C8 File Offset: 0x000067C8
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, ParallelQuery<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (outer == null)
			{
				throw new ArgumentNullException("outer");
			}
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			if (outerKeySelector == null)
			{
				throw new ArgumentNullException("outerKeySelector");
			}
			if (innerKeySelector == null)
			{
				throw new ArgumentNullException("innerKeySelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new GroupJoinQueryOperator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
		}

		/// <summary>This GroupJoin overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="outer">This parameter is not used.</param>
		/// <param name="inner">This parameter is not used.</param>
		/// <param name="outerKeySelector">This parameter is not used.</param>
		/// <param name="innerKeySelector">This parameter is not used.</param>
		/// <param name="resultSelector">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TOuter">This type parameter is not used.</typeparam>
		/// <typeparam name="TInner">This type parameter is not used.</typeparam>
		/// <typeparam name="TKey">This type parameter is not used.</typeparam>
		/// <typeparam name="TResult">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x060002CA RID: 714 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this ParallelQuery<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Projects in parallel each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> and flattens the resulting sequences into one sequence.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by selector.</typeparam>
		/// <returns>A sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002CB RID: 715 RVA: 0x00008629 File Offset: 0x00006829
		public static ParallelQuery<TResult> SelectMany<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectManyQueryOperator<TSource, TResult, TResult>(source, selector, null, null);
		}

		/// <summary>Projects in parallel each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, and flattens the resulting sequences into one sequence. The index of each source element is used in the projected form of that element.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the elements of the sequence returned by selector.</typeparam>
		/// <returns>A sequence whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002CC RID: 716 RVA: 0x00008650 File Offset: 0x00006850
		public static ParallelQuery<TResult> SelectMany<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return new SelectManyQueryOperator<TSource, TResult, TResult>(source, null, selector, null);
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="collectionSelector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <typeparam name="TSource">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TCollection">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of elements in the result sequence.</typeparam>
		/// <returns>A sequence whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> based on the index supplied to <paramref name="collectionSelector" />, and then mapping each of those sequence elements and their corresponding source element to a result element. </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002CD RID: 717 RVA: 0x00008677 File Offset: 0x00006877
		public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(this ParallelQuery<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collectionSelector == null)
			{
				throw new ArgumentNullException("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new SelectManyQueryOperator<TSource, TCollection, TResult>(source, collectionSelector, null, resultSelector);
		}

		/// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein. The index of each source element is used in the intermediate projected form of that element.</summary>
		/// <param name="source">A sequence of values to project.</param>
		/// <param name="collectionSelector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
		/// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
		/// <typeparam name="TSource">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
		/// <typeparam name="TCollection">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of elements to return.</typeparam>
		/// <returns>A sequence whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> based on the index supplied to <paramref name="collectionSelector" />, and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002CE RID: 718 RVA: 0x000086AC File Offset: 0x000068AC
		public static ParallelQuery<TResult> SelectMany<TSource, TCollection, TResult>(this ParallelQuery<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (collectionSelector == null)
			{
				throw new ArgumentNullException("collectionSelector");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new SelectManyQueryOperator<TSource, TCollection, TResult>(source, null, collectionSelector, resultSelector);
		}

		/// <summary>Sorts in parallel the elements of a sequence in ascending order according to a key.</summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002CF RID: 719 RVA: 0x000086E1 File Offset: 0x000068E1
		public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, null, false));
		}

		/// <summary>Sorts in parallel the elements of a sequence in ascending order by using a specified comparer.</summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An IComparer{TKey} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D0 RID: 720 RVA: 0x0000870D File Offset: 0x0000690D
		public static OrderedParallelQuery<TSource> OrderBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, comparer, false));
		}

		/// <summary>Sorts in parallel the elements of a sequence in descending order according to a key.</summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted descending according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D1 RID: 721 RVA: 0x00008739 File Offset: 0x00006939
		public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, null, true));
		}

		/// <summary>Sorts the elements of a sequence in descending order by using a specified comparer.</summary>
		/// <param name="source">A sequence of values to order.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An IComparer{TKey} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted descending according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="KeySelector" /> is a null reference (Nothing in Visual Basic)..</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D2 RID: 722 RVA: 0x00008765 File Offset: 0x00006965
		public static OrderedParallelQuery<TSource> OrderByDescending<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>(new SortQueryOperator<TSource, TKey>(source, keySelector, comparer, true));
		}

		/// <summary>Performs in parallel a subsequent ordering of the elements in a sequence in ascending order according to a key.</summary>
		/// <param name="source">An OrderedParallelQuery{TSource} that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D3 RID: 723 RVA: 0x00008791 File Offset: 0x00006991
		public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, null, false));
		}

		/// <summary>Performs in parallel a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer.</summary>
		/// <param name="source">An OrderedParallelQuery{TSource} that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An IComparer{TKey} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>An OrderedParallelQuery{TSource} whose elements are sorted according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D4 RID: 724 RVA: 0x000087C7 File Offset: 0x000069C7
		public static OrderedParallelQuery<TSource> ThenBy<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, comparer, false));
		}

		/// <summary>Performs in parallel a subsequent ordering of the elements in a sequence in descending order, according to a key.</summary>
		/// <param name="source">An OrderedParallelQuery{TSource} that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>A sequence whose elements are sorted descending according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D5 RID: 725 RVA: 0x000087FD File Offset: 0x000069FD
		public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, null, true));
		}

		/// <summary>Performs in parallel a subsequent ordering of the elements in a sequence in descending order by using a specified comparer.</summary>
		/// <param name="source">An OrderedParallelQuery{TSource} that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An IComparer{TKey} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>A sequence whose elements are sorted descending according to a key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D6 RID: 726 RVA: 0x00008833 File Offset: 0x00006A33
		public static OrderedParallelQuery<TSource> ThenByDescending<TSource, TKey>(this OrderedParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new OrderedParallelQuery<TSource>((QueryOperator<TSource>)source.OrderedEnumerable.CreateOrderedEnumerable<TKey>(keySelector, comparer, true));
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function.</summary>
		/// <param name="source">An OrderedParallelQuery{TSource}that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>A sequence of groups that are sorted descending according to <paramref name="TKey" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D7 RID: 727 RVA: 0x00008869 File Offset: 0x00006A69
		public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.GroupBy(keySelector, null);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and compares the keys by using a specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="source">An <see cref="T:System.Linq.OrderedParallelQuery`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />&gt;.</typeparam>
		/// <returns>A sequence of groups that are sorted descending according to <paramref name="TKey" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D8 RID: 728 RVA: 0x00008873 File Offset: 0x00006A73
		public static ParallelQuery<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return new GroupByQueryOperator<TSource, TKey, TSource>(source, keySelector, null, comparer);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and projects the elements for each group by using a specified function.</summary>
		/// <param name="source">An <see cref="T:System.Linq.OrderedParallelQuery`1" /> that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
		/// <returns>A sequence of groups that are sorted descending according to <paramref name="TKey" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002D9 RID: 729 RVA: 0x0000889A File Offset: 0x00006A9A
		public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.GroupBy(keySelector, elementSelector, null);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a key selector function. The keys are compared by using a comparer and each group's elements are projected by using a specified function.</summary>
		/// <param name="source">An OrderedParallelQuery{TSource}that contains elements to sort.</param>
		/// <param name="keySelector">A function to extract a key from an element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an IGrouping.</param>
		/// <param name="comparer">An IComparer{TSource} to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in the IGrouping</typeparam>
		/// <returns>A sequence of groups that are sorted descending according to <paramref name="TKey" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002DA RID: 730 RVA: 0x000088A5 File Offset: 0x00006AA5
		public static ParallelQuery<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			return new GroupByQueryOperator<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and creates a result value from each group and its key.</summary>
		/// <param name="source">A sequence whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <returns>A sequence of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002DB RID: 731 RVA: 0x000088DC File Offset: 0x00006ADC
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector)
			select resultSelector(grouping.Key, grouping);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The keys are compared by using a specified comparer.</summary>
		/// <param name="source">A sequence whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <returns>A sequence of groups.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002DC RID: 732 RVA: 0x00008924 File Offset: 0x00006B24
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, comparer)
			select resultSelector(grouping.Key, grouping);
		}

		/// <summary>Groups in parallel the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The elements of each group are projected by using a specified function.</summary>
		/// <param name="source">A sequence whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an IGrouping&lt;TKey, TElement&gt;.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each IGrouping{TKey, TElement}.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <returns>A sequence of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002DD RID: 733 RVA: 0x0000896C File Offset: 0x00006B6C
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, elementSelector)
			select resultSelector(grouping.Key, grouping);
		}

		/// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. Key values are compared by using a specified comparer, and the elements of each group are projected by using a specified function.</summary>
		/// <param name="source">A sequence whose elements to group.</param>
		/// <param name="keySelector">A function to extract the key for each element.</param>
		/// <param name="elementSelector">A function to map each source element to an element in an IGrouping{Key, TElement}.</param>
		/// <param name="resultSelector">A function to create a result value from each group.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the elements in each IGrouping{TKey, TElement}.</typeparam>
		/// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
		/// <returns>A sequence of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002DE RID: 734 RVA: 0x000089B4 File Offset: 0x00006BB4
		public static ParallelQuery<TResult> GroupBy<TSource, TKey, TElement, TResult>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
		{
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return from grouping in source.GroupBy(keySelector, elementSelector, comparer)
			select resultSelector(grouping.Key, grouping);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000089FC File Offset: 0x00006BFC
		private static T PerformAggregation<T>(this ParallelQuery<T> source, Func<T, T, T> reduce, T seed, bool seedIsSpecified, bool throwIfEmpty, QueryAggregationOptions options)
		{
			return new AssociativeAggregationOperator<T, T, T>(source, seed, null, seedIsSpecified, reduce, reduce, (T obj) => obj, throwIfEmpty, options).Aggregate();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008A3C File Offset: 0x00006C3C
		private static TAccumulate PerformSequentialAggregation<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, bool seedIsSpecified, Func<TAccumulate, TSource, TAccumulate> func)
		{
			TAccumulate result;
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				TAccumulate taccumulate;
				if (seedIsSpecified)
				{
					taccumulate = seed;
				}
				else
				{
					if (!enumerator.MoveNext())
					{
						throw new InvalidOperationException("Sequence contains no elements");
					}
					taccumulate = (TAccumulate)((object)enumerator.Current);
				}
				while (enumerator.MoveNext())
				{
					TSource arg = enumerator.Current;
					try
					{
						taccumulate = func(taccumulate, arg);
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[]
						{
							ex
						});
					}
				}
				result = taccumulate;
			}
			return result;
		}

		/// <summary>Applies in parallel an accumulator function over a sequence.</summary>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The final accumulator value.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="func" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060002E1 RID: 737 RVA: 0x00008AD8 File Offset: 0x00006CD8
		public static TSource Aggregate<TSource>(this ParallelQuery<TSource> source, Func<TSource, TSource, TSource> func)
		{
			return source.Aggregate(func, QueryAggregationOptions.AssociativeCommutative);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00008AE4 File Offset: 0x00006CE4
		internal static TSource Aggregate<TSource>(this ParallelQuery<TSource> source, Func<TSource, TSource, TSource> func, QueryAggregationOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if ((~(QueryAggregationOptions.Associative | QueryAggregationOptions.Commutative) & options) != QueryAggregationOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if ((options & QueryAggregationOptions.Associative) != QueryAggregationOptions.Associative)
			{
				return source.PerformSequentialAggregation(default(TSource), false, func);
			}
			return source.PerformAggregation(func, default(TSource), false, true, options);
		}

		/// <summary>Applies in parallel an accumulator function over a sequence. The specified seed value is used as the initial accumulator value.</summary>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <returns>The final accumulator value.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="func" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060002E3 RID: 739 RVA: 0x00008B49 File Offset: 0x00006D49
		public static TAccumulate Aggregate<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
		{
			return source.Aggregate(seed, func, QueryAggregationOptions.AssociativeCommutative);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008B54 File Offset: 0x00006D54
		internal static TAccumulate Aggregate<TSource, TAccumulate>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, QueryAggregationOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if ((~(QueryAggregationOptions.Associative | QueryAggregationOptions.Commutative) & options) != QueryAggregationOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			return source.PerformSequentialAggregation(seed, true, func);
		}

		/// <summary>Applies in parallel an accumulator function over a sequence. The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.</summary>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="func">An accumulator function to be invoked on each element.</param>
		/// <param name="resultSelector">A function to transform the final accumulator value into the result value.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <typeparam name="TResult">The type of the resulting value.</typeparam>
		/// <returns>The transformed final accumulator value.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="func" /> or <paramref name="resultSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060002E5 RID: 741 RVA: 0x00008B8C File Offset: 0x00006D8C
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (func == null)
			{
				throw new ArgumentNullException("func");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			TAccumulate arg = source.PerformSequentialAggregation(seed, true, func);
			TResult result;
			try
			{
				result = resultSelector(arg);
			}
			catch (Exception ex)
			{
				throw new AggregateException(new Exception[]
				{
					ex
				});
			}
			return result;
		}

		/// <summary>Applies in parallel an accumulator function over a sequence. This overload is not available in the sequential implementation.</summary>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seed">The initial accumulator value.</param>
		/// <param name="updateAccumulatorFunc">An accumulator function to be invoked on each element in a partition. </param>
		/// <param name="combineAccumulatorsFunc">An accumulator function to be invoked on the yielded accumulator result from each partition. </param>
		/// <param name="resultSelector">A function to transform the final accumulator value into the result value. </param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <typeparam name="TResult">The type of the resulting value.</typeparam>
		/// <returns>The transformed final accumulator value.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="updateAccumulatorFunc" /> or <paramref name="combineAccumulatorsFunc" /> or <paramref name="resultSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060002E6 RID: 742 RVA: 0x00008BFC File Offset: 0x00006DFC
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (updateAccumulatorFunc == null)
			{
				throw new ArgumentNullException("updateAccumulatorFunc");
			}
			if (combineAccumulatorsFunc == null)
			{
				throw new ArgumentNullException("combineAccumulatorsFunc");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new AssociativeAggregationOperator<TSource, TAccumulate, TResult>(source, seed, null, true, updateAccumulatorFunc, combineAccumulatorsFunc, resultSelector, false, QueryAggregationOptions.AssociativeCommutative).Aggregate();
		}

		/// <summary>Applies in parallel an accumulator function over a sequence. This overload is not available in the sequential implementation.</summary>
		/// <param name="source">A sequence to aggregate over.</param>
		/// <param name="seedFactory">A function that returns the initial accumulator value. </param>
		/// <param name="updateAccumulatorFunc">An accumulator function to be invoked on each element in a partition. </param>
		/// <param name="combineAccumulatorsFunc">An accumulator function to be invoked on the yielded accumulator result from each partition.</param>
		/// <param name="resultSelector">A function to transform the final accumulator value into the result value. </param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
		/// <typeparam name="TResult">The type of the resulting value.</typeparam>
		/// <returns>The transformed final accumulator value.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="seedFactory" /> or <paramref name="updateAccumulatorFunc" /> or <paramref name="combineAccumulatorsFunc" /> or <paramref name="resultSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x060002E7 RID: 743 RVA: 0x00008C58 File Offset: 0x00006E58
		public static TResult Aggregate<TSource, TAccumulate, TResult>(this ParallelQuery<TSource> source, Func<TAccumulate> seedFactory, Func<TAccumulate, TSource, TAccumulate> updateAccumulatorFunc, Func<TAccumulate, TAccumulate, TAccumulate> combineAccumulatorsFunc, Func<TAccumulate, TResult> resultSelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (seedFactory == null)
			{
				throw new ArgumentNullException("seedFactory");
			}
			if (updateAccumulatorFunc == null)
			{
				throw new ArgumentNullException("updateAccumulatorFunc");
			}
			if (combineAccumulatorsFunc == null)
			{
				throw new ArgumentNullException("combineAccumulatorsFunc");
			}
			if (resultSelector == null)
			{
				throw new ArgumentNullException("resultSelector");
			}
			return new AssociativeAggregationOperator<TSource, TAccumulate, TResult>(source, default(TAccumulate), seedFactory, true, updateAccumulatorFunc, combineAccumulatorsFunc, resultSelector, false, QueryAggregationOptions.AssociativeCommutative).Aggregate();
		}

		/// <summary>Returns the number of elements in a parallel sequence.</summary>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The number of elements in the input sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The number of elements in source is larger than <see cref="F:System.Int32.MaxValue" />. (In this case the InnerException is <see cref="T:System.OverflowException" />) -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002E8 RID: 744 RVA: 0x00008CC8 File Offset: 0x00006EC8
		public static int Count<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				ICollection<TSource> collection = parallelEnumerableWrapper.WrappedEnumerable as ICollection<TSource>;
				if (collection != null)
				{
					return collection.Count;
				}
			}
			return new CountAggregationOperator<TSource>(source).Aggregate();
		}

		/// <summary>Returns a number that represents how many elements in the specified parallel sequence satisfy a condition.</summary>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The number of elements in source is larger than <see cref="F:System.Int32.MaxValue" />. (In this case the InnerException is <see cref="T:System.OverflowException" />) -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002E9 RID: 745 RVA: 0x00008D0E File Offset: 0x00006F0E
		public static int Count<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new CountAggregationOperator<TSource>(source.Where(predicate)).Aggregate();
		}

		/// <summary>Returns an Int64 that represents the total number of elements in a parallel sequence.</summary>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The number of elements in the input sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The number of elements in source is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.OverflowException">The computed result is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060002EA RID: 746 RVA: 0x00008D40 File Offset: 0x00006F40
		public static long LongCount<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			ParallelEnumerableWrapper<TSource> parallelEnumerableWrapper = source as ParallelEnumerableWrapper<TSource>;
			if (parallelEnumerableWrapper != null)
			{
				ICollection<TSource> collection = parallelEnumerableWrapper.WrappedEnumerable as ICollection<TSource>;
				if (collection != null)
				{
					return (long)collection.Count;
				}
			}
			return new LongCountAggregationOperator<TSource>(source).Aggregate();
		}

		/// <summary>Returns an Int64 that represents how many elements in a parallel sequence satisfy a condition.</summary>
		/// <param name="source">A sequence that contains elements to be counted.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The number of elements in source is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.OverflowException">The computed result is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060002EB RID: 747 RVA: 0x00008D87 File Offset: 0x00006F87
		public static long LongCount<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new LongCountAggregationOperator<TSource>(source.Where(predicate)).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002EC RID: 748 RVA: 0x00008DB6 File Offset: 0x00006FB6
		public static int Sum(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002ED RID: 749 RVA: 0x00008DD1 File Offset: 0x00006FD1
		public static int? Sum(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002EE RID: 750 RVA: 0x00008DEC File Offset: 0x00006FEC
		public static long Sum(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002EF RID: 751 RVA: 0x00008E07 File Offset: 0x00007007
		public static long? Sum(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Single.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F0 RID: 752 RVA: 0x00008E22 File Offset: 0x00007022
		public static float Sum(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Single.MaxValue" />. -or-  One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F1 RID: 753 RVA: 0x00008E3D File Offset: 0x0000703D
		public static float? Sum(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Double.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F2 RID: 754 RVA: 0x00008E58 File Offset: 0x00007058
		public static double Sum(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Double.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F3 RID: 755 RVA: 0x00008E73 File Offset: 0x00007073
		public static double? Sum(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F4 RID: 756 RVA: 0x00008E8E File Offset: 0x0000708E
		public static decimal Sum(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of a sequence of values.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F5 RID: 757 RVA: 0x00008EA9 File Offset: 0x000070A9
		public static decimal? Sum(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalSumAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F6 RID: 758 RVA: 0x00008EC4 File Offset: 0x000070C4
		public static int Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F7 RID: 759 RVA: 0x00008ED2 File Offset: 0x000070D2
		public static int? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F8 RID: 760 RVA: 0x00008EE0 File Offset: 0x000070E0
		public static long Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002F9 RID: 761 RVA: 0x00008EEE File Offset: 0x000070EE
		public static long? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Single.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002FA RID: 762 RVA: 0x00008EFC File Offset: 0x000070FC
		public static float Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Single.MaxValue" />. -or-  One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002FB RID: 763 RVA: 0x00008F0A File Offset: 0x0000710A
		public static float? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Double.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002FC RID: 764 RVA: 0x00008F18 File Offset: 0x00007118
		public static double Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Double.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002FD RID: 765 RVA: 0x00008F26 File Offset: 0x00007126
		public static double? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002FE RID: 766 RVA: 0x00008F34 File Offset: 0x00007134
		public static decimal Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Computes in parallel the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values to calculate the sum of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The sum of the projected values in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x060002FF RID: 767 RVA: 0x00008F42 File Offset: 0x00007142
		public static decimal? Sum<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Sum();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000300 RID: 768 RVA: 0x00008F50 File Offset: 0x00007150
		public static int Min(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000301 RID: 769 RVA: 0x00008F6C File Offset: 0x0000716C
		public static int? Min(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000302 RID: 770 RVA: 0x00008F88 File Offset: 0x00007188
		public static long Min(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000303 RID: 771 RVA: 0x00008FA4 File Offset: 0x000071A4
		public static long? Min(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000304 RID: 772 RVA: 0x00008FC0 File Offset: 0x000071C0
		public static float Min(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000305 RID: 773 RVA: 0x00008FDC File Offset: 0x000071DC
		public static float? Min(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000306 RID: 774 RVA: 0x00008FF8 File Offset: 0x000071F8
		public static double Min(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000307 RID: 775 RVA: 0x00009014 File Offset: 0x00007214
		public static double? Min(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000308 RID: 776 RVA: 0x00009030 File Offset: 0x00007230
		public static decimal Min(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000309 RID: 777 RVA: 0x0000904C File Offset: 0x0000724C
		public static decimal? Min(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalMinMaxAggregationOperator(source, -1).Aggregate();
		}

		/// <summary>Returns the minimum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x0600030A RID: 778 RVA: 0x00009068 File Offset: 0x00007268
		public static TSource Min<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return AggregationMinMaxHelpers<TSource>.ReduceMin(source);
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x0600030B RID: 779 RVA: 0x0000907E File Offset: 0x0000727E
		public static int Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Min<int>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600030C RID: 780 RVA: 0x0000908C File Offset: 0x0000728C
		public static int? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Min<int?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x0600030D RID: 781 RVA: 0x0000909A File Offset: 0x0000729A
		public static long Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Min<long>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600030E RID: 782 RVA: 0x000090A8 File Offset: 0x000072A8
		public static long? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Min<long?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x0600030F RID: 783 RVA: 0x000090B6 File Offset: 0x000072B6
		public static float Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Min<float>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000310 RID: 784 RVA: 0x000090C4 File Offset: 0x000072C4
		public static float? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Min<float?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000311 RID: 785 RVA: 0x000090D2 File Offset: 0x000072D2
		public static double Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Min<double>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000312 RID: 786 RVA: 0x000090E0 File Offset: 0x000072E0
		public static double? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Min<double?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000313 RID: 787 RVA: 0x000090EE File Offset: 0x000072EE
		public static decimal Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Min<decimal>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000314 RID: 788 RVA: 0x000090FC File Offset: 0x000072FC
		public static decimal? Min<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Min<decimal?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the minimum value.</summary>
		/// <param name="source">A sequence of values to determine the minimum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <returns>The minimum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000315 RID: 789 RVA: 0x0000910A File Offset: 0x0000730A
		public static TResult Min<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).Min<TResult>();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000316 RID: 790 RVA: 0x00009118 File Offset: 0x00007318
		public static int Max(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000317 RID: 791 RVA: 0x00009134 File Offset: 0x00007334
		public static int? Max(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000318 RID: 792 RVA: 0x00009150 File Offset: 0x00007350
		public static long Max(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000319 RID: 793 RVA: 0x0000916C File Offset: 0x0000736C
		public static long? Max(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600031A RID: 794 RVA: 0x00009188 File Offset: 0x00007388
		public static float Max(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600031B RID: 795 RVA: 0x000091A4 File Offset: 0x000073A4
		public static float? Max(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600031C RID: 796 RVA: 0x000091C0 File Offset: 0x000073C0
		public static double Max(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600031D RID: 797 RVA: 0x000091DC File Offset: 0x000073DC
		public static double? Max(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600031E RID: 798 RVA: 0x000091F8 File Offset: 0x000073F8
		public static decimal Max(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600031F RID: 799 RVA: 0x00009214 File Offset: 0x00007414
		public static decimal? Max(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalMinMaxAggregationOperator(source, 1).Aggregate();
		}

		/// <summary>Returns the maximum value in a parallel sequence of values.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <typeparam name="TSource">The type of elements in the source sequence.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000320 RID: 800 RVA: 0x00009230 File Offset: 0x00007430
		public static TSource Max<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return AggregationMinMaxHelpers<TSource>.ReduceMax(source);
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000321 RID: 801 RVA: 0x00009246 File Offset: 0x00007446
		public static int Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Max<int>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000322 RID: 802 RVA: 0x00009254 File Offset: 0x00007454
		public static int? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Max<int?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000323 RID: 803 RVA: 0x00009262 File Offset: 0x00007462
		public static long Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Max<long>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000324 RID: 804 RVA: 0x00009270 File Offset: 0x00007470
		public static long? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Max<long?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000325 RID: 805 RVA: 0x0000927E File Offset: 0x0000747E
		public static float Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Max<float>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000326 RID: 806 RVA: 0x0000928C File Offset: 0x0000748C
		public static float? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Max<float?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000327 RID: 807 RVA: 0x0000929A File Offset: 0x0000749A
		public static double Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Max<double>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000328 RID: 808 RVA: 0x000092A8 File Offset: 0x000074A8
		public static double? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Max<double?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x06000329 RID: 809 RVA: 0x000092B6 File Offset: 0x000074B6
		public static decimal Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Max<decimal>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600032A RID: 810 RVA: 0x000092C4 File Offset: 0x000074C4
		public static decimal? Max<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Max<decimal?>();
		}

		/// <summary>Invokes in parallel a transform function on each element of a sequence and returns the maximum value.</summary>
		/// <param name="source">A sequence of values to determine the maximum value of.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
		/// <returns>The maximum value in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements and <paramref name="TSource" /> is a non-nullable value type.</exception>
		// Token: 0x0600032B RID: 811 RVA: 0x000092D2 File Offset: 0x000074D2
		public static TResult Max<TSource, TResult>(this ParallelQuery<TSource> source, Func<TSource, TResult> selector)
		{
			return source.Select(selector).Max<TResult>();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600032C RID: 812 RVA: 0x000092E0 File Offset: 0x000074E0
		public static double Average(this ParallelQuery<int> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new IntAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600032D RID: 813 RVA: 0x000092FB File Offset: 0x000074FB
		public static double? Average(this ParallelQuery<int?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableIntAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600032E RID: 814 RVA: 0x00009316 File Offset: 0x00007516
		public static double Average(this ParallelQuery<long> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new LongAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600032F RID: 815 RVA: 0x00009331 File Offset: 0x00007531
		public static double? Average(this ParallelQuery<long?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableLongAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000330 RID: 816 RVA: 0x0000934C File Offset: 0x0000754C
		public static float Average(this ParallelQuery<float> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new FloatAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000331 RID: 817 RVA: 0x00009367 File Offset: 0x00007567
		public static float? Average(this ParallelQuery<float?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableFloatAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000332 RID: 818 RVA: 0x00009382 File Offset: 0x00007582
		public static double Average(this ParallelQuery<double> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DoubleAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">The source sequence.</param>
		/// <returns>Returns the average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">A sequence of values that are used to calculate an average.The average of the sequence of values.<paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000333 RID: 819 RVA: 0x0000939D File Offset: 0x0000759D
		public static double? Average(this ParallelQuery<double?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDoubleAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000334 RID: 820 RVA: 0x000093B8 File Offset: 0x000075B8
		public static decimal Average(this ParallelQuery<decimal> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DecimalAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000335 RID: 821 RVA: 0x000093D3 File Offset: 0x000075D3
		public static decimal? Average(this ParallelQuery<decimal?> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new NullableDecimalAverageAggregationOperator(source).Aggregate();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x06000336 RID: 822 RVA: 0x000093EE File Offset: 0x000075EE
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, int> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x06000337 RID: 823 RVA: 0x000093FC File Offset: 0x000075FC
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, int?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int32.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x06000338 RID: 824 RVA: 0x0000940A File Offset: 0x0000760A
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, long> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">The sum or count of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x06000339 RID: 825 RVA: 0x00009418 File Offset: 0x00007618
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, long?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x0600033A RID: 826 RVA: 0x00009426 File Offset: 0x00007626
		public static float Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, float> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x0600033B RID: 827 RVA: 0x00009434 File Offset: 0x00007634
		public static float? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, float?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x0600033C RID: 828 RVA: 0x00009442 File Offset: 0x00007642
		public static double Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, double> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x0600033D RID: 829 RVA: 0x00009450 File Offset: 0x00007650
		public static double? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, double?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException">(Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x0600033E RID: 830 RVA: 0x0000945E File Offset: 0x0000765E
		public static decimal Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Computes in parallel the average of a sequence of values that are obtained by invoking a transform function on each element of the input sequence.</summary>
		/// <param name="source">A sequence of values that are used to calculate an average.</param>
		/// <param name="selector">A transform function to apply to each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>The average of the sequence of values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="selector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		/// <exception cref="T:System.OverflowException"> (Thrown as inner exception in an <see cref="T:System.AggregateException" />). The <paramref name="selector" /> function returns a value greater than MaxValue for the element type.</exception>
		// Token: 0x0600033F RID: 831 RVA: 0x0000946C File Offset: 0x0000766C
		public static decimal? Average<TSource>(this ParallelQuery<TSource> source, Func<TSource, decimal?> selector)
		{
			return source.Select(selector).Average();
		}

		/// <summary>Determines in parallel whether any element of a sequence satisfies a condition.</summary>
		/// <param name="source">A sequence to whose elements the predicate will be applied.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000340 RID: 832 RVA: 0x0000947A File Offset: 0x0000767A
		public static bool Any<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new AnyAllSearchOperator<TSource>(source, true, predicate).Aggregate();
		}

		/// <summary>Determines whether a parallel sequence contains any elements.</summary>
		/// <param name="source">The sequence to check for emptiness.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000341 RID: 833 RVA: 0x000094A5 File Offset: 0x000076A5
		public static bool Any<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Any((TSource x) => true);
		}

		/// <summary>Determines in parallel whether all elements of a sequence satisfy a condition.</summary>
		/// <param name="source">A sequence whose elements to apply the predicate to.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>true if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false..</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000342 RID: 834 RVA: 0x000094DA File Offset: 0x000076DA
		public static bool All<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new AnyAllSearchOperator<TSource>(source, false, predicate).Aggregate();
		}

		/// <summary>Determines in parallel whether a sequence contains a specified element by using the default equality comparer.</summary>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000343 RID: 835 RVA: 0x00009505 File Offset: 0x00007705
		public static bool Contains<TSource>(this ParallelQuery<TSource> source, TSource value)
		{
			return source.Contains(value, null);
		}

		/// <summary>Determines in parallel whether a sequence contains a specified element by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="source">A sequence in which to locate a value.</param>
		/// <param name="value">The value to locate in the sequence.</param>
		/// <param name="comparer">An equality comparer to compare values.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000344 RID: 836 RVA: 0x0000950F File Offset: 0x0000770F
		public static bool Contains<TSource>(this ParallelQuery<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ContainsSearchOperator<TSource>(source, value, comparer).Aggregate();
		}

		/// <summary>Returns a specified number of contiguous elements from the start of a parallel sequence.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="count">The number of elements to return.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains the specified number of elements from the start of the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000345 RID: 837 RVA: 0x0000952C File Offset: 0x0000772C
		public static ParallelQuery<TSource> Take<TSource>(this ParallelQuery<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count > 0)
			{
				return new TakeOrSkipQueryOperator<TSource>(source, count, true);
			}
			return ParallelEnumerable.Empty<TSource>();
		}

		/// <summary>Returns elements from a parallel sequence as long as a specified condition is true.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains the elements from the input sequence that occur before the element at which the test no longer passes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000346 RID: 838 RVA: 0x0000954E File Offset: 0x0000774E
		public static ParallelQuery<TSource> TakeWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, predicate, null, true);
		}

		/// <summary>Returns elements from a parallel sequence as long as a specified condition is true. The element's index is used in the logic of the predicate function.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element. </param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains elements from the input sequence that occur before the element at which the test no longer passes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by this query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000347 RID: 839 RVA: 0x00009575 File Offset: 0x00007775
		public static ParallelQuery<TSource> TakeWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, null, predicate, true);
		}

		/// <summary>Bypasses a specified number of elements in a parallel sequence and then returns the remaining elements.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="count">The number of elements to skip before returning the remaining elements.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains the elements that occur after the specified index in the input sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">Count is greater than <see cref="F:System.Int32.MaxValue" /></exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000348 RID: 840 RVA: 0x0000959C File Offset: 0x0000779C
		public static ParallelQuery<TSource> Skip<TSource>(this ParallelQuery<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count <= 0)
			{
				return source;
			}
			return new TakeOrSkipQueryOperator<TSource>(source, count, false);
		}

		/// <summary>Bypasses elements in a parallel sequence as long as a specified condition is true and then returns the remaining elements.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by predicate.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000349 RID: 841 RVA: 0x000095BA File Offset: 0x000077BA
		public static ParallelQuery<TSource> SkipWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, predicate, null, false);
		}

		/// <summary>Bypasses elements in a parallel sequence as long as a specified condition is true and then returns the remaining elements. The element's index is used in the logic of the predicate function.</summary>
		/// <param name="source">The sequence to return elements from.</param>
		/// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element. </param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by predicate.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.OverflowException">More than <see cref="F:System.Int32.MaxValue" /> elements are enumerated by the query.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600034A RID: 842 RVA: 0x000095E1 File Offset: 0x000077E1
		public static ParallelQuery<TSource> SkipWhile<TSource>(this ParallelQuery<TSource> source, Func<TSource, int, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return new TakeOrSkipWhileQueryOperator<TSource>(source, null, predicate, false);
		}

		/// <summary>Concatenates two parallel sequences.</summary>
		/// <param name="first">The first sequence to concatenate.</param>
		/// <param name="second">The sequence to concatenate to the first sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>A sequence that contains the concatenated elements of the two input sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600034B RID: 843 RVA: 0x00009608 File Offset: 0x00007808
		public static ParallelQuery<TSource> Concat<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new ConcatQueryOperator<TSource>(first, second);
		}

		/// <summary>This Concat overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x0600034C RID: 844 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Concat<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Determines whether two parallel sequences are equal by comparing the elements by using the default equality comparer for their type.</summary>
		/// <param name="first">A sequence to compare to second.</param>
		/// <param name="second">A sequence to compare to the first input sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>true if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600034D RID: 845 RVA: 0x0000962D File Offset: 0x0000782D
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return first.SequenceEqual(second, null);
		}

		/// <summary>This SequenceEqual overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Thrown every time this method is called.</exception>
		// Token: 0x0600034E RID: 846 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Determines whether two parallel sequences are equal by comparing their elements by using a specified IEqualityComparer{T}.</summary>
		/// <param name="first">A sequence to compare to <paramref name="second" />.</param>
		/// <param name="second">A sequence to compare to the first input sequence.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to use to compare elements.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>true if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600034F RID: 847 RVA: 0x00009654 File Offset: 0x00007854
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			comparer = (comparer ?? EqualityComparer<TSource>.Default);
			ParallelQuery parallelQuery = QueryOperator<TSource>.AsQueryOperator(first);
			QueryOperator<TSource> queryOperator = QueryOperator<TSource>.AsQueryOperator(second);
			QuerySettings querySettings = parallelQuery.SpecifiedQuerySettings.Merge(queryOperator.SpecifiedQuerySettings).WithDefaults().WithPerExecutionSettings(new CancellationTokenSource(), new Shared<bool>(false));
			IEnumerator<TSource> enumerator = first.GetEnumerator();
			try
			{
				IEnumerator<TSource> enumerator2 = second.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator2.MoveNext() || !comparer.Equals(enumerator.Current, enumerator2.Current))
						{
							return false;
						}
					}
					if (enumerator2.MoveNext())
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					ExceptionAggregator.ThrowOCEorAggregateException(ex, querySettings.CancellationState);
				}
				finally
				{
					ParallelEnumerable.DisposeEnumerator<TSource>(enumerator2, querySettings.CancellationState);
				}
			}
			finally
			{
				ParallelEnumerable.DisposeEnumerator<TSource>(enumerator, querySettings.CancellationState);
			}
			return true;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00009768 File Offset: 0x00007968
		private static void DisposeEnumerator<TSource>(IEnumerator<TSource> e, CancellationState cancelState)
		{
			try
			{
				e.Dispose();
			}
			catch (Exception ex)
			{
				ExceptionAggregator.ThrowOCEorAggregateException(ex, cancelState);
			}
		}

		/// <summary>This SequenceEqual overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Thrown every time this method is called.</exception>
		// Token: 0x06000351 RID: 849 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static bool SequenceEqual<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Returns distinct elements from a parallel sequence by using the default equality comparer to compare values.</summary>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains distinct elements from the source sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000352 RID: 850 RVA: 0x00009798 File Offset: 0x00007998
		public static ParallelQuery<TSource> Distinct<TSource>(this ParallelQuery<TSource> source)
		{
			return source.Distinct(null);
		}

		/// <summary>Returns distinct elements from a parallel sequence by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" />  to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains distinct elements from the source sequence.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000353 RID: 851 RVA: 0x000097A1 File Offset: 0x000079A1
		public static ParallelQuery<TSource> Distinct<TSource>(this ParallelQuery<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DistinctQueryOperator<TSource>(source, comparer);
		}

		/// <summary>Produces the set union of two parallel sequences by using the default equality comparer.</summary>
		/// <param name="first">A sequence whose distinct elements form the first set for the union.</param>
		/// <param name="second">A sequence whose distinct elements form the second set for the union.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>A sequence that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000354 RID: 852 RVA: 0x000097B8 File Offset: 0x000079B8
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Union(second, null);
		}

		/// <summary>This Union overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000355 RID: 853 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set union of two parallel sequences by using a specified IEqualityComparer{T}.</summary>
		/// <param name="first">A sequence whose distinct elements form the first set for the union.</param>
		/// <param name="second">A sequence whose distinct elements form the second set for the union.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>A sequence that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000356 RID: 854 RVA: 0x000097C2 File Offset: 0x000079C2
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new UnionQueryOperator<TSource>(first, second, comparer);
		}

		/// <summary>This Union overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000357 RID: 855 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Union<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set intersection of two parallel sequences by using the default equality comparer to compare values.</summary>
		/// <param name="first">A sequence whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
		/// <param name="second">A sequence whose distinct elements that also appear in the first sequence will be returned.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000358 RID: 856 RVA: 0x000097E8 File Offset: 0x000079E8
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Intersect(second, null);
		}

		/// <summary>This Intersect overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x06000359 RID: 857 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set intersection of two parallel sequences by using the specified IEqualityComparer{T} to compare values.</summary>
		/// <param name="first">A sequence whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
		/// <param name="second">A sequence whose distinct elements that also appear in the first sequence will be returned.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="action" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600035A RID: 858 RVA: 0x000097F2 File Offset: 0x000079F2
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new IntersectQueryOperator<TSource>(first, second, comparer);
		}

		/// <summary>This Intersect overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x0600035B RID: 859 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Intersect<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set difference of two parallel sequences by using the default equality comparer to compare values.</summary>
		/// <param name="first">A sequence whose elements that are not also in <paramref name="second" /> will be returned.</param>
		/// <param name="second">A sequence whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600035C RID: 860 RVA: 0x00009818 File Offset: 0x00007A18
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second)
		{
			return first.Except(second, null);
		}

		/// <summary>This Except overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x0600035D RID: 861 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Produces the set difference of two parallel sequences by using the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
		/// <param name="first">A sequence whose elements that are not also in <paramref name="second" /> will be returned.</param>
		/// <param name="second">A sequence whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence. </param>
		/// <param name="comparer">
		///       <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="first" /> or <paramref name="second" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600035E RID: 862 RVA: 0x00009822 File Offset: 0x00007A22
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, ParallelQuery<TSource> second, IEqualityComparer<TSource> comparer)
		{
			if (first == null)
			{
				throw new ArgumentNullException("first");
			}
			if (second == null)
			{
				throw new ArgumentNullException("second");
			}
			return new ExceptQueryOperator<TSource>(first, second, comparer);
		}

		/// <summary>This Except overload should never be called. This method is marked as obsolete and always throws <see cref="T:System.NotSupportedException" /> when called.</summary>
		/// <param name="first">This parameter is not used.</param>
		/// <param name="second">This parameter is not used.</param>
		/// <param name="comparer">This parameter is not used.</param>
		/// <typeparam name="TSource">This type parameter is not used.</typeparam>
		/// <returns>This overload always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The exception that occurs when this method is called.</exception>
		// Token: 0x0600035F RID: 863 RVA: 0x0000853D File Offset: 0x0000673D
		[Obsolete("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.")]
		public static ParallelQuery<TSource> Except<TSource>(this ParallelQuery<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
		{
			throw new NotSupportedException("The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.");
		}

		/// <summary>Converts a <see cref="T:System.Linq.ParallelQuery`1" /> into an <see cref="T:System.Collections.Generic.IEnumerable`1" /> to force sequential evaluation of the query.</summary>
		/// <param name="source">The sequence to cast as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The input sequence typed as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x06000360 RID: 864 RVA: 0x00009848 File Offset: 0x00007A48
		public static IEnumerable<TSource> AsEnumerable<TSource>(this ParallelQuery<TSource> source)
		{
			return source.AsSequential<TSource>();
		}

		/// <summary>Creates an array from a <see cref="T:System.Linq.ParallelQuery`1" />.</summary>
		/// <param name="source">A sequence to create an array from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An array that contains the elements from the input sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000361 RID: 865 RVA: 0x00009850 File Offset: 0x00007A50
		public static TSource[] ToArray<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			if (queryOperator != null)
			{
				return queryOperator.ExecuteAndGetResultsAsArray();
			}
			return source.ToList<TSource>().ToArray<TSource>();
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.List`1" /> from an <see cref="T:System.Linq.ParallelQuery`1" />.</summary>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.List`1" /> from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>A <see cref="T:System.Collections.Generic.List`1" />  that contains elements from the input sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000362 RID: 866 RVA: 0x00009888 File Offset: 0x00007A88
		public static List<TSource> ToList<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			List<TSource> list = new List<TSource>();
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator;
			if (queryOperator != null)
			{
				if (queryOperator.OrdinalIndexState == OrdinalIndexState.Indexable && queryOperator.OutputOrdered)
				{
					return new List<TSource>(source.ToArray<TSource>());
				}
				enumerator = queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered));
			}
			else
			{
				enumerator = source.GetEnumerator();
			}
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource item = enumerator.Current;
					list.Add(item);
				}
			}
			return list;
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function.</summary>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">
		///         <paramref name="keySelector" /> produces a key that is a null reference (Nothing in Visual Basic). -or- <paramref name="keySelector" /> produces duplicate keys for two elements. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000363 RID: 867 RVA: 0x0000991C File Offset: 0x00007B1C
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToDictionary(keySelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" />  from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function and key comparer.</summary>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">
		///         <paramref name="keySelector" /> produces a key that is a null reference (Nothing in Visual Basic). -or- <paramref name="keySelector" /> produces duplicate keys for two elements. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000364 RID: 868 RVA: 0x0000992C File Offset: 0x00007B2C
		public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(comparer);
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator = (queryOperator == null) ? source.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true);
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					try
					{
						TKey key = keySelector(tsource);
						dictionary.Add(key, tsource);
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[]
						{
							ex
						});
					}
				}
			}
			return dictionary;
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to specified key selector and element selector functions.</summary>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element. </param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">
		///         <paramref name="keySelector" /> produces a key that is a null reference (Nothing in Visual Basic). -or- <paramref name="keySelector" /> produces duplicate keys for two elements. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000365 RID: 869 RVA: 0x000099E4 File Offset: 0x00007BE4
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToDictionary(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function, a comparer, and an element selector function.</summary>
		/// <param name="source">A sequence to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">
		///         <paramref name="keySelector" /> produces a key that is a null reference (Nothing in Visual Basic). -or- <paramref name="keySelector" /> produces duplicate keys for two elements. -or- One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000366 RID: 870 RVA: 0x000099F4 File Offset: 0x00007BF4
		public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer);
			QueryOperator<TSource> queryOperator = source as QueryOperator<TSource>;
			IEnumerator<TSource> enumerator = (queryOperator == null) ? source.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered), true);
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					TSource arg = enumerator.Current;
					try
					{
						dictionary.Add(keySelector(arg), elementSelector(arg));
					}
					catch (Exception ex)
					{
						throw new AggregateException(new Exception[]
						{
							ex
						});
					}
				}
			}
			return dictionary;
		}

		/// <summary>Creates an <see cref="T:System.Linq.ILookup`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function.</summary>
		/// <param name="source">The sequence to create a <see cref="T:System.Linq.ILookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>A <see cref="T:System.Linq.ILookup`2" /> that contains keys and values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000367 RID: 871 RVA: 0x00009ABC File Offset: 0x00007CBC
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector)
		{
			return source.ToLookup(keySelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates an <see cref="T:System.Linq.ILookup`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function and key comparer.</summary>
		/// <param name="source">The sequence to create a <see cref="T:System.Linq.ILookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <returns>A <see cref="T:System.Linq.ILookup`2" /> that contains keys and values.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000368 RID: 872 RVA: 0x00009ACC File Offset: 0x00007CCC
		public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			comparer = (comparer ?? EqualityComparer<TKey>.Default);
			ParallelQuery<IGrouping<TKey, TSource>> parallelQuery = source.GroupBy(keySelector, comparer);
			Lookup<TKey, TSource> lookup = new Lookup<TKey, TSource>(comparer);
			QueryOperator<IGrouping<TKey, TSource>> queryOperator = parallelQuery as QueryOperator<IGrouping<TKey, TSource>>;
			IEnumerator<IGrouping<TKey, TSource>> enumerator = (queryOperator == null) ? parallelQuery.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered));
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					IGrouping<TKey, TSource> grouping = enumerator.Current;
					lookup.Add(grouping);
				}
			}
			return lookup;
		}

		/// <summary>Creates an <see cref="T:System.Linq.ILookup`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to specified key selector and element selector functions.</summary>
		/// <param name="source">The sequence to create a <see cref="T:System.Linq.ILookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element. </param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <returns>A <see cref="T:System.Linq.ILookup`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000369 RID: 873 RVA: 0x00009B68 File Offset: 0x00007D68
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
		{
			return source.ToLookup(keySelector, elementSelector, EqualityComparer<TKey>.Default);
		}

		/// <summary>Creates an <see cref="T:System.Linq.ILookup`2" /> from a <see cref="T:System.Linq.ParallelQuery`1" /> according to a specified key selector function, a comparer and an element selector function.</summary>
		/// <param name="source">The sequence to create a <see cref="T:System.Linq.ILookup`2" /> from.</param>
		/// <param name="keySelector">A function to extract a key from each element.</param>
		/// <param name="elementSelector">A transform function to produce a result element value from each element. </param>
		/// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
		/// <typeparam name="TSource">The type of elements of <paramref name="source" />.</typeparam>
		/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
		/// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
		/// <returns>A Lookup&lt;(Of &lt;(TKey, TElement&gt;)&gt;) that contains values of type TElement selected from the input sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600036A RID: 874 RVA: 0x00009B78 File Offset: 0x00007D78
		public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this ParallelQuery<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (elementSelector == null)
			{
				throw new ArgumentNullException("elementSelector");
			}
			comparer = (comparer ?? EqualityComparer<TKey>.Default);
			ParallelQuery<IGrouping<TKey, TElement>> parallelQuery = source.GroupBy(keySelector, elementSelector, comparer);
			Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);
			QueryOperator<IGrouping<TKey, TElement>> queryOperator = parallelQuery as QueryOperator<IGrouping<TKey, TElement>>;
			IEnumerator<IGrouping<TKey, TElement>> enumerator = (queryOperator == null) ? parallelQuery.GetEnumerator() : queryOperator.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered));
			using (enumerator)
			{
				while (enumerator.MoveNext())
				{
					IGrouping<TKey, TElement> grouping = enumerator.Current;
					lookup.Add(grouping);
				}
			}
			return lookup;
		}

		/// <summary>Inverts the order of the elements in a parallel sequence.</summary>
		/// <param name="source">A sequence of values to reverse.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence whose elements correspond to those of the input sequence in reverse order.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600036B RID: 875 RVA: 0x00009C24 File Offset: 0x00007E24
		public static ParallelQuery<TSource> Reverse<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ReverseQueryOperator<TSource>(source);
		}

		/// <summary>Filters the elements of a ParallelQuery based on a specified type.</summary>
		/// <param name="source">The sequence whose elements to filter.</param>
		/// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
		/// <returns>A sequence that contains elements from the input sequence of type .</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600036C RID: 876 RVA: 0x00009C3A File Offset: 0x00007E3A
		public static ParallelQuery<TResult> OfType<TResult>(this ParallelQuery source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.OfType<TResult>();
		}

		/// <summary>Converts the elements of a ParallelQuery to the specified type.</summary>
		/// <param name="source">The sequence that contains the elements to be converted.</param>
		/// <typeparam name="TResult">The type to convert the elements of <paramref name="source" /> to.</typeparam>
		/// <returns>A sequence that contains each element of the source sequence converted to the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source sequence could not be converted to <paramref name="TResult" />.</exception>
		// Token: 0x0600036D RID: 877 RVA: 0x00009C50 File Offset: 0x00007E50
		public static ParallelQuery<TResult> Cast<TResult>(this ParallelQuery source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.Cast<TResult>();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00009C68 File Offset: 0x00007E68
		private static TSource GetOneWithPossibleDefault<TSource>(QueryOperator<TSource> queryOp, bool throwIfTwo, bool defaultIfEmpty)
		{
			using (IEnumerator<TSource> enumerator = queryOp.GetEnumerator(new ParallelMergeOptions?(ParallelMergeOptions.FullyBuffered)))
			{
				if (enumerator.MoveNext())
				{
					TSource result = enumerator.Current;
					if (throwIfTwo && enumerator.MoveNext())
					{
						throw new InvalidOperationException("Sequence contains more than one matching element");
					}
					return result;
				}
			}
			if (defaultIfEmpty)
			{
				return default(TSource);
			}
			throw new InvalidOperationException("Sequence contains no elements");
		}

		/// <summary>Returns the first element of a parallel sequence.</summary>
		/// <param name="source">The sequence to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The first element in the specified sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x0600036F RID: 879 RVA: 0x00009CE0 File Offset: 0x00007EE0
		public static TSource First<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, null);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).First<TSource>();
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, false);
		}

		/// <summary>Returns the first element in a parallel sequence that satisfies a specified condition.</summary>
		/// <param name="source">The sequence to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The first element in the sequence that passes the test in the specified predicate function.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element in <paramref name="source" /> satisfies the condition in <paramref name="predicate" />.</exception>
		// Token: 0x06000370 RID: 880 RVA: 0x00009D7C File Offset: 0x00007F7C
		public static TSource First<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).First(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, false);
		}

		/// <summary>Returns the first element of a parallel sequence, or a default value if the sequence contains no elements.</summary>
		/// <param name="source">The sequence to return the first element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>default(TSource) if <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000371 RID: 881 RVA: 0x00009E34 File Offset: 0x00008034
		public static TSource FirstOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, null);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).FirstOrDefault<TSource>();
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, true);
		}

		/// <summary>Returns the first element of the parallel sequence that satisfies a condition or a default value if no such element is found.</summary>
		/// <param name="source">The sequence to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>default(TSource) if <paramref name="source" /> is empty or if no element passes the test specified by predicate; otherwise, the first element in <paramref name="source" /> that passes the test specified by predicate.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000372 RID: 882 RVA: 0x00009ED0 File Offset: 0x000080D0
		public static TSource FirstOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			FirstQueryOperator<TSource> firstQueryOperator = new FirstQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = firstQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (firstQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(firstQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).FirstOrDefault(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(firstQueryOperator, false, true);
		}

		/// <summary>Returns the last element of a parallel sequence.</summary>
		/// <param name="source">The sequence to return the last element from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The value at the last position in the source sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> contains no elements.</exception>
		// Token: 0x06000373 RID: 883 RVA: 0x00009F88 File Offset: 0x00008188
		public static TSource Last<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, null);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).Last<TSource>();
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, false);
		}

		/// <summary>Returns the last element of a parallel sequence that satisfies a specified condition.</summary>
		/// <param name="source">The sequence to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The last element in the sequence that passes the test in the specified predicate function.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element in <paramref name="source" /> satisfies the condition in <paramref name="predicate" />.</exception>
		// Token: 0x06000374 RID: 884 RVA: 0x0000A024 File Offset: 0x00008224
		public static TSource Last<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).Last(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, false);
		}

		/// <summary>Returns the last element of a parallel sequence, or a default value if the sequence contains no elements.</summary>
		/// <param name="source">The sequence to return an element from.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>default() if the source sequence is empty; otherwise, the last element in the sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000375 RID: 885 RVA: 0x0000A0DC File Offset: 0x000082DC
		public static TSource LastOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, null);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).LastOrDefault<TSource>();
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, true);
		}

		/// <summary>Returns the last element of a parallel sequence that satisfies a condition, or a default value if no such element is found.</summary>
		/// <param name="source">The sequence to return an element from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>default() if the sequence is empty or if no elements pass the test in the predicate function; otherwise, the last element that passes the test in the predicate function.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000376 RID: 886 RVA: 0x0000A178 File Offset: 0x00008378
		public static TSource LastOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			LastQueryOperator<TSource> lastQueryOperator = new LastQueryOperator<TSource>(source, predicate);
			QuerySettings querySettings = lastQueryOperator.SpecifiedQuerySettings.WithDefaults();
			if (lastQueryOperator.LimitsParallelism)
			{
				ParallelExecutionMode? executionMode = querySettings.ExecutionMode;
				ParallelExecutionMode parallelExecutionMode = ParallelExecutionMode.ForceParallelism;
				if (!(executionMode.GetValueOrDefault() == parallelExecutionMode & executionMode != null))
				{
					return ExceptionAggregator.WrapEnumerable<TSource>(CancellableEnumerable.Wrap<TSource>(lastQueryOperator.Child.AsSequentialQuery(querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState.ExternalCancellationToken), querySettings.CancellationState).LastOrDefault(ExceptionAggregator.WrapFunc<TSource, bool>(predicate, querySettings.CancellationState));
				}
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(lastQueryOperator, false, true);
		}

		/// <summary>Returns the only element of a parallel sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
		/// <param name="source">The sequence to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The single element of the input sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">The input sequence contains more than one element. -or- The input sequence is empty.</exception>
		// Token: 0x06000377 RID: 887 RVA: 0x0000A22F File Offset: 0x0000842F
		public static TSource Single<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, null), true, false);
		}

		/// <summary>Returns the only element of a parallel sequence that satisfies a specified condition, and throws an exception if more than one such element exists.</summary>
		/// <param name="source">The sequence to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The single element of the input sequence that satisfies a condition.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />. -or- More than one element satisfies the condition in <paramref name="predicate" />.</exception>
		// Token: 0x06000378 RID: 888 RVA: 0x0000A24D File Offset: 0x0000844D
		public static TSource Single<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, predicate), true, false);
		}

		/// <summary>Returns the only element of a parallel sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
		/// <param name="source">The sequence to return the single element of.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The single element of the input sequence, or default() if the sequence contains no elements.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x06000379 RID: 889 RVA: 0x0000A279 File Offset: 0x00008479
		public static TSource SingleOrDefault<TSource>(this ParallelQuery<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, null), true, true);
		}

		/// <summary>Returns the only element of a parallel sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.</summary>
		/// <param name="source">The sequence to return the single element of.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The single element of the input sequence that satisfies the condition, or default() if no such element is found.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> or <paramref name="predicate" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="source" /> is empty or multiple elements are returned.</exception>
		// Token: 0x0600037A RID: 890 RVA: 0x0000A297 File Offset: 0x00008497
		public static TSource SingleOrDefault<TSource>(this ParallelQuery<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			return ParallelEnumerable.GetOneWithPossibleDefault<TSource>(new SingleQueryOperator<TSource>(source, predicate), true, true);
		}

		/// <summary>Returns the elements of the specified parallel sequence or the type parameter's default value in a singleton collection if the sequence is empty.</summary>
		/// <param name="source">The sequence to return a default value for if it is empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains default(TSource) if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600037B RID: 891 RVA: 0x0000A2C4 File Offset: 0x000084C4
		public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(this ParallelQuery<TSource> source)
		{
			return source.DefaultIfEmpty(default(TSource));
		}

		/// <summary>Returns the elements of the specified parallel sequence or the specified value in a singleton collection if the sequence is empty.</summary>
		/// <param name="source">The sequence to return the specified value for if it is empty.</param>
		/// <param name="defaultValue">The value to return if the sequence is empty.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>A sequence that contains defaultValue if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600037C RID: 892 RVA: 0x0000A2E0 File Offset: 0x000084E0
		public static ParallelQuery<TSource> DefaultIfEmpty<TSource>(this ParallelQuery<TSource> source, TSource defaultValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DefaultIfEmptyQueryOperator<TSource>(source, defaultValue);
		}

		/// <summary>Returns the element at a specified index in a parallel sequence.</summary>
		/// <param name="source">A sequence to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>The element at the specified position in the source sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> is less than 0 or greater than or equal to the number of elements in <paramref name="source" />.</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600037D RID: 893 RVA: 0x0000A2F8 File Offset: 0x000084F8
		public static TSource ElementAt<TSource>(this ParallelQuery<TSource> source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			TSource result;
			if (new ElementAtQueryOperator<TSource>(source, index).Aggregate(out result, false))
			{
				return result;
			}
			throw new ArgumentOutOfRangeException("index");
		}

		/// <summary>Returns the element at a specified index in a parallel sequence or a default value if the index is out of range.</summary>
		/// <param name="source">A sequence to return an element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>default(TSource) if the index is outside the bounds of the source sequence; otherwise, the element at the specified position in the source sequence.</returns>
		/// <exception cref="T:System.OperationCanceledException">The query was canceled with the token passed in through <paramref name="WithCancellation" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="source" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.AggregateException">One or more exceptions occurred during the evaluation of the query.</exception>
		// Token: 0x0600037E RID: 894 RVA: 0x0000A340 File Offset: 0x00008540
		public static TSource ElementAtOrDefault<TSource>(this ParallelQuery<TSource> source, int index)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			TSource result;
			if (index >= 0 && new ElementAtQueryOperator<TSource>(source, index).Aggregate(out result, true))
			{
				return result;
			}
			return default(TSource);
		}

		// Token: 0x040003B9 RID: 953
		private const string RIGHT_SOURCE_NOT_PARALLEL_STR = "The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.";

		// Token: 0x02000083 RID: 131
		[CompilerGenerated]
		private sealed class <>c__DisplayClass46_0<TSource, TKey, TResult>
		{
			// Token: 0x0600037F RID: 895 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass46_0()
			{
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000A37B File Offset: 0x0000857B
			internal TResult <GroupBy>b__0(IGrouping<TKey, TSource> grouping)
			{
				return this.resultSelector(grouping.Key, grouping);
			}

			// Token: 0x040003BA RID: 954
			public Func<TKey, IEnumerable<TSource>, TResult> resultSelector;
		}

		// Token: 0x02000084 RID: 132
		[CompilerGenerated]
		private sealed class <>c__DisplayClass47_0<TSource, TKey, TResult>
		{
			// Token: 0x06000381 RID: 897 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass47_0()
			{
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000A38F File Offset: 0x0000858F
			internal TResult <GroupBy>b__0(IGrouping<TKey, TSource> grouping)
			{
				return this.resultSelector(grouping.Key, grouping);
			}

			// Token: 0x040003BB RID: 955
			public Func<TKey, IEnumerable<TSource>, TResult> resultSelector;
		}

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		private sealed class <>c__DisplayClass48_0<TSource, TKey, TElement, TResult>
		{
			// Token: 0x06000383 RID: 899 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass48_0()
			{
			}

			// Token: 0x06000384 RID: 900 RVA: 0x0000A3A3 File Offset: 0x000085A3
			internal TResult <GroupBy>b__0(IGrouping<TKey, TElement> grouping)
			{
				return this.resultSelector(grouping.Key, grouping);
			}

			// Token: 0x040003BC RID: 956
			public Func<TKey, IEnumerable<TElement>, TResult> resultSelector;
		}

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		private sealed class <>c__DisplayClass49_0<TSource, TKey, TElement, TResult>
		{
			// Token: 0x06000385 RID: 901 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass49_0()
			{
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000A3B7 File Offset: 0x000085B7
			internal TResult <GroupBy>b__0(IGrouping<TKey, TElement> grouping)
			{
				return this.resultSelector(grouping.Key, grouping);
			}

			// Token: 0x040003BD RID: 957
			public Func<TKey, IEnumerable<TElement>, TResult> resultSelector;
		}

		// Token: 0x02000087 RID: 135
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__50<T>
		{
			// Token: 0x06000387 RID: 903 RVA: 0x0000A3CB File Offset: 0x000085CB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__50()
			{
			}

			// Token: 0x06000388 RID: 904 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__50()
			{
			}

			// Token: 0x06000389 RID: 905 RVA: 0x000022AA File Offset: 0x000004AA
			internal T <PerformAggregation>b__50_0(T obj)
			{
				return obj;
			}

			// Token: 0x040003BE RID: 958
			public static readonly ParallelEnumerable.<>c__50<T> <>9 = new ParallelEnumerable.<>c__50<T>();

			// Token: 0x040003BF RID: 959
			public static Func<T, T> <>9__50_0;
		}

		// Token: 0x02000088 RID: 136
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__148<TSource>
		{
			// Token: 0x0600038A RID: 906 RVA: 0x0000A3D7 File Offset: 0x000085D7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__148()
			{
			}

			// Token: 0x0600038B RID: 907 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__148()
			{
			}

			// Token: 0x0600038C RID: 908 RVA: 0x00007E1D File Offset: 0x0000601D
			internal bool <Any>b__148_0(TSource x)
			{
				return true;
			}

			// Token: 0x040003C0 RID: 960
			public static readonly ParallelEnumerable.<>c__148<TSource> <>9 = new ParallelEnumerable.<>c__148<TSource>();

			// Token: 0x040003C1 RID: 961
			public static Func<TSource, bool> <>9__148_0;
		}
	}
}
