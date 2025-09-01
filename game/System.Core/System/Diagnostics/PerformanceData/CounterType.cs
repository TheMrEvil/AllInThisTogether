using System;

namespace System.Diagnostics.PerformanceData
{
	/// <summary>Defines the possible types of counters. Each counter is assigned a counter type. The counter type determines how the counter data is calculated, averaged, and displayed.</summary>
	// Token: 0x02000390 RID: 912
	public enum CounterType
	{
		/// <summary>This counter is used as the base data (denominator) in the computation of time or count averages for the <see cref="F:System.Diagnostics.PerformanceData.CounterType.AverageCount64" /> and <see cref="F:System.Diagnostics.PerformanceData.CounterType.AverageTimer32" /> counter types. This counter type collects the last observed value only. (See the PERF_AVERAGE_BASE counter type in the deployment kit.)</summary>
		// Token: 0x04000D1A RID: 3354
		AverageBase = 1073939458,
		/// <summary>This counter type shows how many items are processed, on average, during an operation. Counters of this type display a ratio of the items processed (such as bytes sent) to the number of operations completed. The ratio is calculated by comparing the number of items processed during the last interval to the number of operations completed during the last interval. (See the PERF_AVERAGE_BULK counter type in the deployment kit.)</summary>
		// Token: 0x04000D1B RID: 3355
		AverageCount64 = 1073874176,
		/// <summary>This counter type measures the average time it takes to complete a process or operation. Counters of this type display a ratio of the total elapsed time of the sample interval to the number of processes or operations completed during that time. This counter type measures time in ticks of the system clock. (See the PERF_AVERAGE_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D1C RID: 3356
		AverageTimer32 = 805438464,
		/// <summary>This counter type shows the change in the measured attribute between the two most recent sample intervals. (See the PERF_COUNTER_DELTA counter type in the deployment kit.)</summary>
		// Token: 0x04000D1D RID: 3357
		Delta32 = 4195328,
		/// <summary>This counter type shows the change in the measured attribute between the two most recent sample intervals. It is the same as the <see cref="F:System.Diagnostics.PerformanceData.CounterType.Delta32" /> counter type, except that it uses larger fields to accommodate larger values. (See the PERF_COUNTER_LARGE_DELTA counter type in the deployment kit.)</summary>
		// Token: 0x04000D1E RID: 3358
		Delta64 = 4195584,
		/// <summary>This counter type shows the total time between when the component or process started and the time when this value is calculated. (See the PERF_ELAPSED_TIME counter type in the deployment kit.)</summary>
		// Token: 0x04000D1F RID: 3359
		ElapsedTime = 807666944,
		/// <summary>This counter type monitors the average length of a queue to a resource over time. Counters of this type display the difference between the queue lengths observed during the last two sample intervals, divided by the duration of the interval. This counter type is the same as the <see cref="F:System.Diagnostics.PerformanceData.CounterType.QueueLength" /> counter type, except that it uses larger fields to accommodate larger values. (See the PERF_COUNTER_LARGE_QUEUELEN_TYPE counter type in the deployment kit.)</summary>
		// Token: 0x04000D20 RID: 3360
		LargeQueueLength = 4523264,
		/// <summary>Indicates the number of items sampled. It is used as the denominator in the calculations to get an average among the items sampled when taking timings of multiple, but similar, items. This type supports the following counter types: <see cref="F:System.Diagnostics.PerformanceData.CounterType.MultiTimerPercentageActive" />, <see cref="F:System.Diagnostics.PerformanceData.CounterType.MultiTimerPercentageNotActive" />, <see cref="F:System.Diagnostics.PerformanceData.CounterType.MultiTimerPercentageActive100Ns" />, and <see cref="F:System.Diagnostics.PerformanceData.CounterType.MultiTimerPercentageNotActive100Ns" />.</summary>
		// Token: 0x04000D21 RID: 3361
		MultiTimerBase = 1107494144,
		/// <summary>This counter type is a multitimer. Multitimers collect data from more than one instance of a component, such as a processor or disk. Counters of this type display the active time of one or more components as a percentage of the total time of the sample interval. Because the numerator records the active time of components operating simultaneously, the resulting percentage can exceed 100 percent. This counter type differs from <see cref="F:System.Diagnostics.PerformanceData.CounterType.MultiTimerPercentageActive100Ns" /> in that it measures time in units of ticks of the system performance timer, rather than in 100 nanosecond units. (See the PERF_COUNTER_MULTI_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D22 RID: 3362
		MultiTimerPercentageActive = 574686464,
		/// <summary>This counter type shows the active time of one or more components as a percentage of the total time of the sample interval. It measures time in 100 nanosecond units. This counter type is a multitimer. Multitimers are designed to monitor more than one instance of a component, such as a processor or disk. (See the PERF_100NSEC_MULTI_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D23 RID: 3363
		MultiTimerPercentageActive100Ns = 575735040,
		/// <summary>This counter type shows the active time of one or more components as a percentage of the total time of the sample interval. This counter type is an inverse multitimer. Multitimers monitor more than one instance of a component, such as a processor or disk. Inverse counters measure the time that a component is not active and derive the active time from that measurement. This counter differs from <see cref="F:System.Diagnostics.PerformanceData.CounterType.MultiTimerPercentageNotActive100Ns" /> in that it measures time in units of ticks of the system performance timer, rather than in 100 nanosecond units. (See the PERF_COUNTER_MULTI_TIMER_INV counter type in the deployment kit.)</summary>
		// Token: 0x04000D24 RID: 3364
		MultiTimerPercentageNotActive = 591463680,
		/// <summary>This counter type shows the active time of one or more components as a percentage of the total time of the sample interval. Counters of this type measure time in 100 nanosecond units. This counter type is an inverse multitimer. Multitimers are designed to monitor more than one instance of a component, such as a processor or disk. Inverse counters measure the time that a component is not active and derive its active time from the measurement of inactive time. (See the PERF_100NSEC_MULTI_TIMER_INV counter type in the deployment kit.)</summary>
		// Token: 0x04000D25 RID: 3365
		MultiTimerPercentageNotActive100Ns = 592512256,
		/// <summary>This 64-bit counter type is a timer that displays in object-specific units. (See the PERF_OBJ_TIME_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D26 RID: 3366
		ObjectSpecificTimer = 543229184,
		/// <summary>This counter type shows the average time that a component was active as a percentage of the total sample time. (See the PERF_COUNTER_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D27 RID: 3367
		PercentageActive = 541132032,
		/// <summary>This counter type shows the active time of a component as a percentage of the total elapsed time of the sample interval. It measures time in units of 100 nanoseconds. Counters of this type are designed to measure the activity of one component at a time. (See the PERF_100NSEC_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D28 RID: 3368
		PercentageActive100Ns = 542180608,
		/// <summary>This is an inverse counter type. Inverse counters measure the time that a component is not active and derive the active time from that measurement. Counters of this type display the average percentage of active time observed during sample interval. The value of these counters is calculated by monitoring the percentage of time that the service was inactive and then subtracting that value from 100 percent. This counter type is the same as the <see cref="F:System.Diagnostics.PerformanceData.CounterType.PercentageNotActive100Ns" /> counter type, except that it measures time in units of ticks of the system performance timer, rather than in 100 nanosecond units. (See the PERF_COUNTER_TIMER_INV counter type in the deployment kit.)</summary>
		// Token: 0x04000D29 RID: 3369
		PercentageNotActive = 557909248,
		/// <summary>This counter type shows the average percentage of active time observed during the sample interval. This is an inverse counter. Inverse counters are calculated by monitoring the percentage of time that the service was inactive and then subtracting that value from 100 percent. (See the PERF_100NSEC_TIMER_INV counter type in the deployment kit.)</summary>
		// Token: 0x04000D2A RID: 3370
		PercentageNotActive100Ns = 558957824,
		/// <summary>This counter type shows a value that consists of two counter values: the count of the elapsed time of the event being monitored, and the frequency specified in the PerfFreq field of the object header. This counter type differs from other counter timers in that the clock tick value accompanies the counter value so as to eliminate any possible difference due to latency from the function call. Precision counter types are used when standard system timers are not precise enough for accurate readings. (See the PERF_PRECISION_OBJECT_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D2B RID: 3371
		PrecisionObjectSpecificTimer = 543622400,
		/// <summary>This counter type shows a value that consists of two counter values: the count of the elapsed time of the event being monitored, and the frequency from the system performance timer. This counter type differs from other counter timers in that the clock tick value accompanies the counter value, eliminating any possible difference due to latency from the function call. Precision counter types are used when standard system timers are not precise enough for accurate readings. (See the PERF_PRECISION_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D2C RID: 3372
		PrecisionSystemTimer = 541525248,
		/// <summary>This counter type shows a value that consists of two counter values: the count of the elapsed time of the event being monitored, and the "clock" time from a private timer in the same units. It measures time in 100 nanosecond units. This counter type differs from other counter timers in that the clock tick value accompanies the counter value eliminating any possible difference due to latency from the function call. Precision counter types are used when standard system timers are not precise enough for accurate readings. (See the PERF_PRECISION_100NS_TIMER counter type in the deployment kit.)</summary>
		// Token: 0x04000D2D RID: 3373
		PrecisionTimer100Ns = 542573824,
		/// <summary>This counter type is designed to monitor the average length of a queue to a resource over time. It shows the difference between the queue lengths observed during the last two sample intervals divided by the duration of the interval. (See the PERF_COUNTER_QUEUELEN_TYPE counter type in the deployment kit.)</summary>
		// Token: 0x04000D2E RID: 3374
		QueueLength = 4523008,
		/// <summary>This counter type measures the queue-length space-time product using a 100-nanosecond time base. (See the PERF_COUNTER_100NS_QUEUELEN_TYPE counter type in the deployment kit.)</summary>
		// Token: 0x04000D2F RID: 3375
		QueueLength100Ns = 5571840,
		/// <summary>This counter type measures the queue-length space-time product using an object-specific time base. (See the PERF_COUNTER_OBJ_QUEUELEN_TYPE counter type in the deployment kit.)</summary>
		// Token: 0x04000D30 RID: 3376
		QueueLengthObjectTime = 6620416,
		/// <summary>This counter type shows the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock. (See the PERF_COUNTER_COUNTER counter type in the deployment kit.)</summary>
		// Token: 0x04000D31 RID: 3377
		RateOfCountPerSecond32 = 272696320,
		/// <summary>This counter type shows the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock. This counter type is the same as the <see cref="F:System.Diagnostics.PerformanceData.CounterType.RateOfCountPerSecond32" /> type, but it uses larger fields to accommodate larger values. (See the PERF_COUNTER_BULK_COUNT counter type in the deployment kit.)</summary>
		// Token: 0x04000D32 RID: 3378
		RateOfCountPerSecond64 = 272696576,
		/// <summary>This counter type collects the last observed value only. The value is used as the denominator of a counter that presents a general arithmetic fraction. This type supports the <see cref="F:System.Diagnostics.PerformanceData.CounterType.RawFraction32" /> counter type. (See the PERF_RAW_BASE counter type in the deployment kit.)</summary>
		// Token: 0x04000D33 RID: 3379
		RawBase32 = 1073939459,
		/// <summary>This counter type collects the last observed value. It is the same as the <see cref="F:System.Diagnostics.PerformanceData.CounterType.RawBase32" />counter type except that it uses larger fields to accommodate larger values. This type supports the <see cref="F:System.Diagnostics.PerformanceData.CounterType.RawFraction64" /> counter type. (See the PERF_LARGE_RAW_BASE counter type in the deployment kit.)</summary>
		// Token: 0x04000D34 RID: 3380
		RawBase64 = 1073939712,
		/// <summary>This counter type shows the last observed value only. It does not display an average. (See the PERF_COUNTER_RAWCOUNT counter type in the deployment kit.)</summary>
		// Token: 0x04000D35 RID: 3381
		RawData32 = 65536,
		/// <summary>This counter type shows the last observed value only, not an average. It is the same as the <see cref="F:System.Diagnostics.PerformanceData.CounterType.RawData32" /> counter type, except that it uses larger fields to accommodate larger values. (See the PERF_COUNTER_LARGE_RAWCOUNT counter type in the deployment kit.)</summary>
		// Token: 0x04000D36 RID: 3382
		RawData64 = 65792,
		/// <summary>This counter type shows the most recently observed value, in hexadecimal format. It does not display an average. (See the PERF_COUNTER_RAWCOUNT_HEX counter type in the deployment kit.)</summary>
		// Token: 0x04000D37 RID: 3383
		RawDataHex32 = 0,
		/// <summary>This counter type shows the last observed value, in hexadecimal format. It is the same as the <see cref="F:System.Diagnostics.PerformanceData.CounterType.RawDataHex32" /> counter type, except that it uses larger fields to accommodate larger values. (See the PERF_COUNTER_LARGE_RAWCOUNT_HEX counter type in the deployment kit.)</summary>
		// Token: 0x04000D38 RID: 3384
		RawDataHex64 = 256,
		/// <summary>This counter type shows the ratio of a subset to its set as a percentage. For example, it compares the number of bytes in use on a disk to the total number of bytes on the disk. Counters of this type display the current percentage only, not an average over time. (See the PERF_RAW_FRACTION counter type in the deployment kit.)</summary>
		// Token: 0x04000D39 RID: 3385
		RawFraction32 = 537003008,
		/// <summary>This counter type shows the ratio of a subset to its set as a percentage. For example, it compares the number of bytes in use on a disk to the total number of bytes on the disk. Counters of this type display the current percentage only, not an average over time. It is the same as the <see cref="F:System.Diagnostics.PerformanceData.CounterType.RawFraction32" /> counter type, except that it uses larger fields to accommodate larger values.</summary>
		// Token: 0x04000D3A RID: 3386
		RawFraction64 = 537003264,
		/// <summary>This counter stores the number of sampling interrupts taken and is used as a denominator in the sampling fraction. This type supports the <see cref="F:System.Diagnostics.PerformanceData.CounterType.SampleFraction" /> counter type.</summary>
		// Token: 0x04000D3B RID: 3387
		SampleBase = 1073939457,
		/// <summary>This counter type shows the average number of operations completed in one second. It measures time in units of ticks of the system performance timer. The variable F represents the number of ticks that occur in one second. The value of F is factored into the equation so that the result is displayed in seconds. (See the PERF_SAMPLE_COUNTER counter type in the deployment kit.)</summary>
		// Token: 0x04000D3C RID: 3388
		SampleCounter = 4260864,
		/// <summary>This counter type shows the average ratio of hits to all operations during the last two sample intervals. (See the PERF_SAMPLE_FRACTION counter type in the deployment kit.)</summary>
		// Token: 0x04000D3D RID: 3389
		SampleFraction = 549585920
	}
}
