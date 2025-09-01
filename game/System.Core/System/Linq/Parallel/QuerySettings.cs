using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x0200018D RID: 397
	internal struct QuerySettings
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x000253DA File Offset: 0x000235DA
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x000253E2 File Offset: 0x000235E2
		internal CancellationState CancellationState
		{
			get
			{
				return this._cancellationState;
			}
			set
			{
				this._cancellationState = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x000253EB File Offset: 0x000235EB
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x000253F3 File Offset: 0x000235F3
		internal TaskScheduler TaskScheduler
		{
			get
			{
				return this._taskScheduler;
			}
			set
			{
				this._taskScheduler = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000253FC File Offset: 0x000235FC
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00025404 File Offset: 0x00023604
		internal int? DegreeOfParallelism
		{
			get
			{
				return this._degreeOfParallelism;
			}
			set
			{
				this._degreeOfParallelism = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002540D File Offset: 0x0002360D
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00025415 File Offset: 0x00023615
		internal ParallelExecutionMode? ExecutionMode
		{
			get
			{
				return this._executionMode;
			}
			set
			{
				this._executionMode = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002541E File Offset: 0x0002361E
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00025426 File Offset: 0x00023626
		internal ParallelMergeOptions? MergeOptions
		{
			get
			{
				return this._mergeOptions;
			}
			set
			{
				this._mergeOptions = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0002542F File Offset: 0x0002362F
		internal int QueryId
		{
			get
			{
				return this._queryId;
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00025437 File Offset: 0x00023637
		internal QuerySettings(TaskScheduler taskScheduler, int? degreeOfParallelism, CancellationToken externalCancellationToken, ParallelExecutionMode? executionMode, ParallelMergeOptions? mergeOptions)
		{
			this._taskScheduler = taskScheduler;
			this._degreeOfParallelism = degreeOfParallelism;
			this._cancellationState = new CancellationState(externalCancellationToken);
			this._executionMode = executionMode;
			this._mergeOptions = mergeOptions;
			this._queryId = -1;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002546C File Offset: 0x0002366C
		internal QuerySettings Merge(QuerySettings settings2)
		{
			if (this.TaskScheduler != null && settings2.TaskScheduler != null)
			{
				throw new InvalidOperationException("The WithTaskScheduler operator may be used at most once in a query.");
			}
			if (this.DegreeOfParallelism != null && settings2.DegreeOfParallelism != null)
			{
				throw new InvalidOperationException("The WithDegreeOfParallelism operator may be used at most once in a query.");
			}
			if (this.CancellationState.ExternalCancellationToken.CanBeCanceled && settings2.CancellationState.ExternalCancellationToken.CanBeCanceled)
			{
				throw new InvalidOperationException("The WithCancellation operator may by used at most once in a query.");
			}
			if (this.ExecutionMode != null && settings2.ExecutionMode != null)
			{
				throw new InvalidOperationException("The WithExecutionMode operator may be used at most once in a query.");
			}
			if (this.MergeOptions != null && settings2.MergeOptions != null)
			{
				throw new InvalidOperationException("The WithMergeOptions operator may be used at most once in a query.");
			}
			TaskScheduler taskScheduler = (this.TaskScheduler == null) ? settings2.TaskScheduler : this.TaskScheduler;
			int? degreeOfParallelism = (this.DegreeOfParallelism != null) ? this.DegreeOfParallelism : settings2.DegreeOfParallelism;
			CancellationToken externalCancellationToken = this.CancellationState.ExternalCancellationToken.CanBeCanceled ? this.CancellationState.ExternalCancellationToken : settings2.CancellationState.ExternalCancellationToken;
			ParallelExecutionMode? executionMode = (this.ExecutionMode != null) ? this.ExecutionMode : settings2.ExecutionMode;
			ParallelMergeOptions? mergeOptions = (this.MergeOptions != null) ? this.MergeOptions : settings2.MergeOptions;
			return new QuerySettings(taskScheduler, degreeOfParallelism, externalCancellationToken, executionMode, mergeOptions);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000255FE File Offset: 0x000237FE
		internal QuerySettings WithPerExecutionSettings()
		{
			return this.WithPerExecutionSettings(new CancellationTokenSource(), new Shared<bool>(false));
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00025614 File Offset: 0x00023814
		internal QuerySettings WithPerExecutionSettings(CancellationTokenSource topLevelCancellationTokenSource, Shared<bool> topLevelDisposedFlag)
		{
			QuerySettings result = new QuerySettings(this.TaskScheduler, this.DegreeOfParallelism, this.CancellationState.ExternalCancellationToken, this.ExecutionMode, this.MergeOptions);
			result.CancellationState.InternalCancellationTokenSource = topLevelCancellationTokenSource;
			result.CancellationState.MergedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(result.CancellationState.InternalCancellationTokenSource.Token, result.CancellationState.ExternalCancellationToken);
			result.CancellationState.TopLevelDisposedFlag = topLevelDisposedFlag;
			result._queryId = PlinqEtwProvider.NextQueryId();
			return result;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000256A0 File Offset: 0x000238A0
		internal QuerySettings WithDefaults()
		{
			QuerySettings result = this;
			if (result.TaskScheduler == null)
			{
				result.TaskScheduler = TaskScheduler.Default;
			}
			if (result.DegreeOfParallelism == null)
			{
				result.DegreeOfParallelism = new int?(Scheduling.GetDefaultDegreeOfParallelism());
			}
			if (result.ExecutionMode == null)
			{
				result.ExecutionMode = new ParallelExecutionMode?(ParallelExecutionMode.Default);
			}
			if (result.MergeOptions == null)
			{
				result.MergeOptions = new ParallelMergeOptions?(ParallelMergeOptions.Default);
			}
			ParallelMergeOptions? mergeOptions = result.MergeOptions;
			ParallelMergeOptions parallelMergeOptions = ParallelMergeOptions.Default;
			if (mergeOptions.GetValueOrDefault() == parallelMergeOptions & mergeOptions != null)
			{
				result.MergeOptions = new ParallelMergeOptions?(ParallelMergeOptions.AutoBuffered);
			}
			return result;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00025758 File Offset: 0x00023958
		internal static QuerySettings Empty
		{
			get
			{
				return new QuerySettings(null, null, default(CancellationToken), null, null);
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002578F File Offset: 0x0002398F
		public void CleanStateAtQueryEnd()
		{
			this._cancellationState.MergedCancellationTokenSource.Dispose();
		}

		// Token: 0x04000753 RID: 1875
		private TaskScheduler _taskScheduler;

		// Token: 0x04000754 RID: 1876
		private int? _degreeOfParallelism;

		// Token: 0x04000755 RID: 1877
		private CancellationState _cancellationState;

		// Token: 0x04000756 RID: 1878
		private ParallelExecutionMode? _executionMode;

		// Token: 0x04000757 RID: 1879
		private ParallelMergeOptions? _mergeOptions;

		// Token: 0x04000758 RID: 1880
		private int _queryId;
	}
}
