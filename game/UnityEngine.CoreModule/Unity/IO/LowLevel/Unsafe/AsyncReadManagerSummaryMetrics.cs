using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000083 RID: 131
	[NativeAsStruct]
	[NativeConditional("ENABLE_PROFILER")]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncReadManagerSummaryMetrics
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000040B5 File Offset: 0x000022B5
		[NativeName("totalBytesRead")]
		public ulong TotalBytesRead
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalBytesRead>k__BackingField;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000040BD File Offset: 0x000022BD
		[NativeName("averageBandwidthMBPerSecond")]
		public float AverageBandwidthMBPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageBandwidthMBPerSecond>k__BackingField;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000040C5 File Offset: 0x000022C5
		[NativeName("averageReadSizeInBytes")]
		public float AverageReadSizeInBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageReadSizeInBytes>k__BackingField;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000040CD File Offset: 0x000022CD
		[NativeName("averageWaitTimeMicroseconds")]
		public float AverageWaitTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageWaitTimeMicroseconds>k__BackingField;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000040D5 File Offset: 0x000022D5
		[NativeName("averageReadTimeMicroseconds")]
		public float AverageReadTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageReadTimeMicroseconds>k__BackingField;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000040DD File Offset: 0x000022DD
		[NativeName("averageTotalRequestTimeMicroseconds")]
		public float AverageTotalRequestTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageTotalRequestTimeMicroseconds>k__BackingField;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000040E5 File Offset: 0x000022E5
		[NativeName("averageThroughputMBPerSecond")]
		public float AverageThroughputMBPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<AverageThroughputMBPerSecond>k__BackingField;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000040ED File Offset: 0x000022ED
		[NativeName("longestWaitTimeMicroseconds")]
		public float LongestWaitTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestWaitTimeMicroseconds>k__BackingField;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000040F5 File Offset: 0x000022F5
		[NativeName("longestReadTimeMicroseconds")]
		public float LongestReadTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestReadTimeMicroseconds>k__BackingField;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000040FD File Offset: 0x000022FD
		[NativeName("longestReadAssetType")]
		public ulong LongestReadAssetType
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestReadAssetType>k__BackingField;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00004105 File Offset: 0x00002305
		[NativeName("longestWaitAssetType")]
		public ulong LongestWaitAssetType
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestWaitAssetType>k__BackingField;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000410D File Offset: 0x0000230D
		[NativeName("longestReadSubsystem")]
		public AssetLoadingSubsystem LongestReadSubsystem
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestReadSubsystem>k__BackingField;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00004115 File Offset: 0x00002315
		[NativeName("longestWaitSubsystem")]
		public AssetLoadingSubsystem LongestWaitSubsystem
		{
			[CompilerGenerated]
			get
			{
				return this.<LongestWaitSubsystem>k__BackingField;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000411D File Offset: 0x0000231D
		[NativeName("numberOfInProgressRequests")]
		public int NumberOfInProgressRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfInProgressRequests>k__BackingField;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00004125 File Offset: 0x00002325
		[NativeName("numberOfCompletedRequests")]
		public int NumberOfCompletedRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfCompletedRequests>k__BackingField;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000412D File Offset: 0x0000232D
		[NativeName("numberOfFailedRequests")]
		public int NumberOfFailedRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfFailedRequests>k__BackingField;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00004135 File Offset: 0x00002335
		[NativeName("numberOfWaitingRequests")]
		public int NumberOfWaitingRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfWaitingRequests>k__BackingField;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000413D File Offset: 0x0000233D
		[NativeName("numberOfCanceledRequests")]
		public int NumberOfCanceledRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfCanceledRequests>k__BackingField;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00004145 File Offset: 0x00002345
		[NativeName("totalNumberOfRequests")]
		public int TotalNumberOfRequests
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalNumberOfRequests>k__BackingField;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000414D File Offset: 0x0000234D
		[NativeName("numberOfCachedReads")]
		public int NumberOfCachedReads
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfCachedReads>k__BackingField;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00004155 File Offset: 0x00002355
		[NativeName("numberOfAsyncReads")]
		public int NumberOfAsyncReads
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfAsyncReads>k__BackingField;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000415D File Offset: 0x0000235D
		[NativeName("numberOfSyncReads")]
		public int NumberOfSyncReads
		{
			[CompilerGenerated]
			get
			{
				return this.<NumberOfSyncReads>k__BackingField;
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00002072 File Offset: 0x00000272
		public AsyncReadManagerSummaryMetrics()
		{
		}

		// Token: 0x040001EF RID: 495
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <TotalBytesRead>k__BackingField;

		// Token: 0x040001F0 RID: 496
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly float <AverageBandwidthMBPerSecond>k__BackingField;

		// Token: 0x040001F1 RID: 497
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly float <AverageReadSizeInBytes>k__BackingField;

		// Token: 0x040001F2 RID: 498
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly float <AverageWaitTimeMicroseconds>k__BackingField;

		// Token: 0x040001F3 RID: 499
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly float <AverageReadTimeMicroseconds>k__BackingField;

		// Token: 0x040001F4 RID: 500
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly float <AverageTotalRequestTimeMicroseconds>k__BackingField;

		// Token: 0x040001F5 RID: 501
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly float <AverageThroughputMBPerSecond>k__BackingField;

		// Token: 0x040001F6 RID: 502
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly float <LongestWaitTimeMicroseconds>k__BackingField;

		// Token: 0x040001F7 RID: 503
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly float <LongestReadTimeMicroseconds>k__BackingField;

		// Token: 0x040001F8 RID: 504
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <LongestReadAssetType>k__BackingField;

		// Token: 0x040001F9 RID: 505
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <LongestWaitAssetType>k__BackingField;

		// Token: 0x040001FA RID: 506
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly AssetLoadingSubsystem <LongestReadSubsystem>k__BackingField;

		// Token: 0x040001FB RID: 507
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly AssetLoadingSubsystem <LongestWaitSubsystem>k__BackingField;

		// Token: 0x040001FC RID: 508
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <NumberOfInProgressRequests>k__BackingField;

		// Token: 0x040001FD RID: 509
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <NumberOfCompletedRequests>k__BackingField;

		// Token: 0x040001FE RID: 510
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <NumberOfFailedRequests>k__BackingField;

		// Token: 0x040001FF RID: 511
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <NumberOfWaitingRequests>k__BackingField;

		// Token: 0x04000200 RID: 512
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <NumberOfCanceledRequests>k__BackingField;

		// Token: 0x04000201 RID: 513
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <TotalNumberOfRequests>k__BackingField;

		// Token: 0x04000202 RID: 514
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <NumberOfCachedReads>k__BackingField;

		// Token: 0x04000203 RID: 515
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly int <NumberOfAsyncReads>k__BackingField;

		// Token: 0x04000204 RID: 516
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly int <NumberOfSyncReads>k__BackingField;
	}
}
