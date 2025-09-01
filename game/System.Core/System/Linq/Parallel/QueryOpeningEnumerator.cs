using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x02000187 RID: 391
	internal class QueryOpeningEnumerator<TOutput> : IEnumerator<!0>, IDisposable, IEnumerator
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x00024D9B File Offset: 0x00022F9B
		internal QueryOpeningEnumerator(QueryOperator<TOutput> queryOperator, ParallelMergeOptions? mergeOptions, bool suppressOrderPreservation)
		{
			this._queryOperator = queryOperator;
			this._mergeOptions = mergeOptions;
			this._suppressOrderPreservation = suppressOrderPreservation;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00024DCF File Offset: 0x00022FCF
		public TOutput Current
		{
			get
			{
				if (this._openedQueryEnumerator == null)
				{
					throw new InvalidOperationException("Enumeration has not started. MoveNext must be called to initiate enumeration.");
				}
				return this._openedQueryEnumerator.Current;
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00024DF0 File Offset: 0x00022FF0
		public void Dispose()
		{
			this._topLevelDisposedFlag.Value = true;
			this._topLevelCancellationTokenSource.Cancel();
			if (this._openedQueryEnumerator != null)
			{
				this._openedQueryEnumerator.Dispose();
				this._querySettings.CleanStateAtQueryEnd();
			}
			QueryLifecycle.LogicalQueryExecutionEnd(this._querySettings.QueryId);
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0001DCA3 File Offset: 0x0001BEA3
		object IEnumerator.Current
		{
			get
			{
				return ((IEnumerator<!0>)this).Current;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00024E44 File Offset: 0x00023044
		public bool MoveNext()
		{
			if (this._topLevelDisposedFlag.Value)
			{
				throw new ObjectDisposedException("enumerator", "The query enumerator has been disposed.");
			}
			if (this._openedQueryEnumerator == null)
			{
				this.OpenQuery();
			}
			bool result = this._openedQueryEnumerator.MoveNext();
			if ((this._moveNextIteration & 63) == 0)
			{
				CancellationState.ThrowWithStandardMessageIfCanceled(this._querySettings.CancellationState.ExternalCancellationToken);
			}
			this._moveNextIteration++;
			return result;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00024EB8 File Offset: 0x000230B8
		private void OpenQuery()
		{
			if (this._hasQueryOpeningFailed)
			{
				throw new InvalidOperationException("The query enumerator previously threw an exception.");
			}
			try
			{
				this._querySettings = this._queryOperator.SpecifiedQuerySettings.WithPerExecutionSettings(this._topLevelCancellationTokenSource, this._topLevelDisposedFlag).WithDefaults();
				QueryLifecycle.LogicalQueryExecutionBegin(this._querySettings.QueryId);
				this._openedQueryEnumerator = this._queryOperator.GetOpenedEnumerator(this._mergeOptions, this._suppressOrderPreservation, false, this._querySettings);
				CancellationState.ThrowWithStandardMessageIfCanceled(this._querySettings.CancellationState.ExternalCancellationToken);
			}
			catch
			{
				this._hasQueryOpeningFailed = true;
				throw;
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000080E3 File Offset: 0x000062E3
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000743 RID: 1859
		private readonly QueryOperator<TOutput> _queryOperator;

		// Token: 0x04000744 RID: 1860
		private IEnumerator<TOutput> _openedQueryEnumerator;

		// Token: 0x04000745 RID: 1861
		private QuerySettings _querySettings;

		// Token: 0x04000746 RID: 1862
		private readonly ParallelMergeOptions? _mergeOptions;

		// Token: 0x04000747 RID: 1863
		private readonly bool _suppressOrderPreservation;

		// Token: 0x04000748 RID: 1864
		private int _moveNextIteration;

		// Token: 0x04000749 RID: 1865
		private bool _hasQueryOpeningFailed;

		// Token: 0x0400074A RID: 1866
		private readonly Shared<bool> _topLevelDisposedFlag = new Shared<bool>(false);

		// Token: 0x0400074B RID: 1867
		private readonly CancellationTokenSource _topLevelCancellationTokenSource = new CancellationTokenSource();
	}
}
