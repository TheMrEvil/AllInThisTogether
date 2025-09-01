using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Linq.Parallel
{
	// Token: 0x02000101 RID: 257
	internal class ArrayMergeHelper<TInputOutput> : IMergeHelper<TInputOutput>
	{
		// Token: 0x06000895 RID: 2197 RVA: 0x0001D700 File Offset: 0x0001B900
		public ArrayMergeHelper(QuerySettings settings, QueryResults<TInputOutput> queryResults)
		{
			this._settings = settings;
			this._queryResults = queryResults;
			int count = this._queryResults.Count;
			this._outputArray = new TInputOutput[count];
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001D739 File Offset: 0x0001B939
		private void ToArrayElement(int index)
		{
			this._outputArray[index] = this._queryResults[index];
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001D753 File Offset: 0x0001B953
		public void Execute()
		{
			new QueryExecutionOption<int>(QueryOperator<int>.AsQueryOperator(ParallelEnumerable.Range(0, this._queryResults.Count)), this._settings).ForAll(new Action<int>(this.ToArrayElement));
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001D787 File Offset: 0x0001B987
		[ExcludeFromCodeCoverage]
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			return this.GetResultsAsArray().GetEnumerator();
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001D794 File Offset: 0x0001B994
		public TInputOutput[] GetResultsAsArray()
		{
			return this._outputArray;
		}

		// Token: 0x040005F8 RID: 1528
		private QueryResults<TInputOutput> _queryResults;

		// Token: 0x040005F9 RID: 1529
		private TInputOutput[] _outputArray;

		// Token: 0x040005FA RID: 1530
		private QuerySettings _settings;
	}
}
