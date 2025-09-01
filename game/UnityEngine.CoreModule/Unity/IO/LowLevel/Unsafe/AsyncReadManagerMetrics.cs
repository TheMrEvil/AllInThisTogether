using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000081 RID: 129
	[NativeConditional("ENABLE_PROFILER")]
	public static class AsyncReadManagerMetrics
	{
		// Token: 0x06000201 RID: 513
		[FreeFunction("AreMetricsEnabled_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsEnabled();

		// Token: 0x06000202 RID: 514
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->ClearMetrics")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearMetrics_Internal();

		// Token: 0x06000203 RID: 515 RVA: 0x00003F6C File Offset: 0x0000216C
		public static void ClearCompletedMetrics()
		{
			AsyncReadManagerMetrics.ClearMetrics_Internal();
		}

		// Token: 0x06000204 RID: 516
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMarshalledMetrics")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerRequestMetric[] GetMetrics_Internal(bool clear);

		// Token: 0x06000205 RID: 517
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMetrics_NoAlloc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetMetrics_NoAlloc_Internal([NotNull("ArgumentNullException")] List<AsyncReadManagerRequestMetric> metrics, bool clear);

		// Token: 0x06000206 RID: 518
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMarshalledMetrics_Filtered_Managed")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerRequestMetric[] GetMetrics_Filtered_Internal(AsyncReadManagerMetricsFilters filters, bool clear);

		// Token: 0x06000207 RID: 519
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMetrics_NoAlloc_Filtered_Managed")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetMetrics_NoAlloc_Filtered_Internal([NotNull("ArgumentNullException")] List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters filters, bool clear);

		// Token: 0x06000208 RID: 520 RVA: 0x00003F78 File Offset: 0x00002178
		public static AsyncReadManagerRequestMetric[] GetMetrics(AsyncReadManagerMetricsFilters filters, AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetMetrics_Filtered_Internal(filters, clear);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00003F9C File Offset: 0x0000219C
		public static void GetMetrics(List<AsyncReadManagerRequestMetric> outMetrics, AsyncReadManagerMetricsFilters filters, AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Filtered_Internal(outMetrics, filters, clear);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00003FC0 File Offset: 0x000021C0
		public static AsyncReadManagerRequestMetric[] GetMetrics(AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetMetrics_Internal(clear);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00003FE4 File Offset: 0x000021E4
		public static void GetMetrics(List<AsyncReadManagerRequestMetric> outMetrics, AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Internal(outMetrics, clear);
		}

		// Token: 0x0600020C RID: 524
		[FreeFunction("GetAsyncReadManagerMetrics()->StartCollecting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StartCollectingMetrics();

		// Token: 0x0600020D RID: 525
		[FreeFunction("GetAsyncReadManagerMetrics()->StopCollecting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopCollectingMetrics();

		// Token: 0x0600020E RID: 526
		[FreeFunction("GetAsyncReadManagerMetrics()->GetCurrentSummaryMetrics")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryMetrics_Internal(bool clear);

		// Token: 0x0600020F RID: 527 RVA: 0x00004008 File Offset: 0x00002208
		public static AsyncReadManagerSummaryMetrics GetCurrentSummaryMetrics(AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetSummaryMetrics_Internal(clear);
		}

		// Token: 0x06000210 RID: 528
		[FreeFunction("GetAsyncReadManagerMetrics()->GetCurrentSummaryMetricsWithFilters")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryMetricsWithFilters_Internal(AsyncReadManagerMetricsFilters metricsFilters, bool clear);

		// Token: 0x06000211 RID: 529 RVA: 0x0000402C File Offset: 0x0000222C
		public static AsyncReadManagerSummaryMetrics GetCurrentSummaryMetrics(AsyncReadManagerMetricsFilters metricsFilters, AsyncReadManagerMetrics.Flags flags)
		{
			bool clear = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetSummaryMetricsWithFilters_Internal(metricsFilters, clear);
		}

		// Token: 0x06000212 RID: 530
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetrics_Managed")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetrics_Internal(AsyncReadManagerRequestMetric[] metrics);

		// Token: 0x06000213 RID: 531 RVA: 0x00004050 File Offset: 0x00002250
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(AsyncReadManagerRequestMetric[] metrics)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetrics_Internal(metrics);
		}

		// Token: 0x06000214 RID: 532
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetrics_FromContainer_Managed", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetrics_FromContainer_Internal(List<AsyncReadManagerRequestMetric> metrics);

		// Token: 0x06000215 RID: 533 RVA: 0x00004068 File Offset: 0x00002268
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(List<AsyncReadManagerRequestMetric> metrics)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetrics_FromContainer_Internal(metrics);
		}

		// Token: 0x06000216 RID: 534
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetricsWithFilters_Managed")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetricsWithFilters_Internal(AsyncReadManagerRequestMetric[] metrics, AsyncReadManagerMetricsFilters metricsFilters);

		// Token: 0x06000217 RID: 535 RVA: 0x00004080 File Offset: 0x00002280
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(AsyncReadManagerRequestMetric[] metrics, AsyncReadManagerMetricsFilters metricsFilters)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetricsWithFilters_Internal(metrics, metricsFilters);
		}

		// Token: 0x06000218 RID: 536
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetricsWithFilters_FromContainer_Managed", ThrowsException = true)]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetricsWithFilters_FromContainer_Internal(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters metricsFilters);

		// Token: 0x06000219 RID: 537 RVA: 0x0000409C File Offset: 0x0000229C
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters metricsFilters)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetricsWithFilters_FromContainer_Internal(metrics, metricsFilters);
		}

		// Token: 0x0600021A RID: 538
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetTotalSizeNonASRMReadsBytes")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetTotalSizeOfNonASRMReadsBytes(bool emptyAfterRead);

		// Token: 0x02000082 RID: 130
		[Flags]
		public enum Flags
		{
			// Token: 0x040001ED RID: 493
			None = 0,
			// Token: 0x040001EE RID: 494
			ClearOnRead = 1
		}
	}
}
