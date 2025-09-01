using System;

namespace System.Threading.Tasks
{
	/// <summary>Stores options that configure the operation of methods on the <see cref="T:System.Threading.Tasks.Parallel" /> class.</summary>
	// Token: 0x0200031C RID: 796
	public class ParallelOptions
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Tasks.ParallelOptions" /> class.</summary>
		// Token: 0x060021DD RID: 8669 RVA: 0x00079356 File Offset: 0x00077556
		public ParallelOptions()
		{
			this._scheduler = TaskScheduler.Default;
			this._maxDegreeOfParallelism = -1;
			this._cancellationToken = CancellationToken.None;
		}

		/// <summary>Gets or sets the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with this <see cref="T:System.Threading.Tasks.ParallelOptions" /> instance. Setting this property to null indicates that the current scheduler should be used.</summary>
		/// <returns>The task scheduler that is associated with this instance.</returns>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x0007937B File Offset: 0x0007757B
		// (set) Token: 0x060021DF RID: 8671 RVA: 0x00079383 File Offset: 0x00077583
		public TaskScheduler TaskScheduler
		{
			get
			{
				return this._scheduler;
			}
			set
			{
				this._scheduler = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x0007938C File Offset: 0x0007758C
		internal TaskScheduler EffectiveTaskScheduler
		{
			get
			{
				if (this._scheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this._scheduler;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent tasks enabled by this <see cref="T:System.Threading.Tasks.ParallelOptions" /> instance.</summary>
		/// <returns>An integer that represents the maximum degree of parallelism.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to zero or to a value that is less than -1.</exception>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060021E1 RID: 8673 RVA: 0x000793A2 File Offset: 0x000775A2
		// (set) Token: 0x060021E2 RID: 8674 RVA: 0x000793AA File Offset: 0x000775AA
		public int MaxDegreeOfParallelism
		{
			get
			{
				return this._maxDegreeOfParallelism;
			}
			set
			{
				if (value == 0 || value < -1)
				{
					throw new ArgumentOutOfRangeException("MaxDegreeOfParallelism");
				}
				this._maxDegreeOfParallelism = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.Tasks.ParallelOptions" /> instance.</summary>
		/// <returns>The token that is associated with this instance.</returns>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000793C5 File Offset: 0x000775C5
		// (set) Token: 0x060021E4 RID: 8676 RVA: 0x000793CD File Offset: 0x000775CD
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
			set
			{
				this._cancellationToken = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000793D8 File Offset: 0x000775D8
		internal int EffectiveMaxConcurrencyLevel
		{
			get
			{
				int num = this.MaxDegreeOfParallelism;
				int maximumConcurrencyLevel = this.EffectiveTaskScheduler.MaximumConcurrencyLevel;
				if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel != 2147483647)
				{
					num = ((num == -1) ? maximumConcurrencyLevel : Math.Min(maximumConcurrencyLevel, num));
				}
				return num;
			}
		}

		// Token: 0x04001BE9 RID: 7145
		private TaskScheduler _scheduler;

		// Token: 0x04001BEA RID: 7146
		private int _maxDegreeOfParallelism;

		// Token: 0x04001BEB RID: 7147
		private CancellationToken _cancellationToken;
	}
}
