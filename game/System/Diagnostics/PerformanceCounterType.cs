using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies the formula used to calculate the <see cref="M:System.Diagnostics.PerformanceCounter.NextValue" /> method for a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance.</summary>
	// Token: 0x0200027B RID: 635
	[TypeConverter(typeof(AlphabeticalEnumConverter))]
	public enum PerformanceCounterType
	{
		/// <summary>An instantaneous counter that shows the most recently observed value in hexadecimal format. Used, for example, to maintain a simple count of items or operations.</summary>
		// Token: 0x04000B34 RID: 2868
		NumberOfItemsHEX32,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of a very large number of items or operations. It is the same as <see langword="NumberOfItemsHEX32" /> except that it uses larger fields to accommodate larger values.</summary>
		// Token: 0x04000B35 RID: 2869
		NumberOfItemsHEX64 = 256,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of items or operations.</summary>
		// Token: 0x04000B36 RID: 2870
		NumberOfItems32 = 65536,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of a very large number of items or operations. It is the same as <see langword="NumberOfItems32" /> except that it uses larger fields to accommodate larger values.</summary>
		// Token: 0x04000B37 RID: 2871
		NumberOfItems64 = 65792,
		/// <summary>A difference counter that shows the change in the measured attribute between the two most recent sample intervals.</summary>
		// Token: 0x04000B38 RID: 2872
		CounterDelta32 = 4195328,
		/// <summary>A difference counter that shows the change in the measured attribute between the two most recent sample intervals. It is the same as the <see langword="CounterDelta32" /> counter type except that is uses larger fields to accomodate larger values.</summary>
		// Token: 0x04000B39 RID: 2873
		CounterDelta64 = 4195584,
		/// <summary>An average counter that shows the average number of operations completed in one second. When a counter of this type samples the data, each sampling interrupt returns one or zero. The counter data is the number of ones that were sampled. It measures time in units of ticks of the system performance timer.</summary>
		// Token: 0x04000B3A RID: 2874
		SampleCounter = 4260864,
		/// <summary>An average counter designed to monitor the average length of a queue to a resource over time. It shows the difference between the queue lengths observed during the last two sample intervals divided by the duration of the interval. This type of counter is typically used to track the number of items that are queued or waiting.</summary>
		// Token: 0x04000B3B RID: 2875
		CountPerTimeInterval32 = 4523008,
		/// <summary>An average counter that monitors the average length of a queue to a resource over time. Counters of this type display the difference between the queue lengths observed during the last two sample intervals, divided by the duration of the interval. This counter type is the same as <see langword="CountPerTimeInterval32" /> except that it uses larger fields to accommodate larger values. This type of counter is typically used to track a high-volume or very large number of items that are queued or waiting.</summary>
		// Token: 0x04000B3C RID: 2876
		CountPerTimeInterval64 = 4523264,
		/// <summary>A difference counter that shows the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock.</summary>
		// Token: 0x04000B3D RID: 2877
		RateOfCountsPerSecond32 = 272696320,
		/// <summary>A difference counter that shows the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock. This counter type is the same as the <see langword="RateOfCountsPerSecond32" /> type, but it uses larger fields to accommodate larger values to track a high-volume number of items or operations per second, such as a byte-transmission rate.</summary>
		// Token: 0x04000B3E RID: 2878
		RateOfCountsPerSecond64 = 272696576,
		/// <summary>An instantaneous percentage counter that shows the ratio of a subset to its set as a percentage. For example, it compares the number of bytes in use on a disk to the total number of bytes on the disk. Counters of this type display the current percentage only, not an average over time.</summary>
		// Token: 0x04000B3F RID: 2879
		RawFraction = 537003008,
		/// <summary>A percentage counter that shows the average time that a component is active as a percentage of the total sample time.</summary>
		// Token: 0x04000B40 RID: 2880
		CounterTimer = 541132032,
		/// <summary>A percentage counter that shows the active time of a component as a percentage of the total elapsed time of the sample interval. It measures time in units of 100 nanoseconds (ns). Counters of this type are designed to measure the activity of one component at a time.</summary>
		// Token: 0x04000B41 RID: 2881
		Timer100Ns = 542180608,
		/// <summary>A percentage counter that shows the average ratio of hits to all operations during the last two sample intervals.</summary>
		// Token: 0x04000B42 RID: 2882
		SampleFraction = 549585920,
		/// <summary>A percentage counter that displays the average percentage of active time observed during sample interval. The value of these counters is calculated by monitoring the percentage of time that the service was inactive and then subtracting that value from 100 percent.</summary>
		// Token: 0x04000B43 RID: 2883
		CounterTimerInverse = 557909248,
		/// <summary>A percentage counter that shows the average percentage of active time observed during the sample interval.</summary>
		// Token: 0x04000B44 RID: 2884
		Timer100NsInverse = 558957824,
		/// <summary>A percentage counter that displays the active time of one or more components as a percentage of the total time of the sample interval. Because the numerator records the active time of components operating simultaneously, the resulting percentage can exceed 100 percent.</summary>
		// Token: 0x04000B45 RID: 2885
		CounterMultiTimer = 574686464,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. It measures time in 100 nanosecond (ns) units.</summary>
		// Token: 0x04000B46 RID: 2886
		CounterMultiTimer100Ns = 575735040,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. It derives the active time by measuring the time that the components were not active and subtracting the result from 100 percent by the number of objects monitored.</summary>
		// Token: 0x04000B47 RID: 2887
		CounterMultiTimerInverse = 591463680,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. Counters of this type measure time in 100 nanosecond (ns) units. They derive the active time by measuring the time that the components were not active and subtracting the result from multiplying 100 percent by the number of objects monitored.</summary>
		// Token: 0x04000B48 RID: 2888
		CounterMultiTimer100NsInverse = 592512256,
		/// <summary>An average counter that measures the time it takes, on average, to complete a process or operation. Counters of this type display a ratio of the total elapsed time of the sample interval to the number of processes or operations completed during that time. This counter type measures time in ticks of the system clock.</summary>
		// Token: 0x04000B49 RID: 2889
		AverageTimer32 = 805438464,
		/// <summary>A difference timer that shows the total time between when the component or process started and the time when this value is calculated.</summary>
		// Token: 0x04000B4A RID: 2890
		ElapsedTime = 807666944,
		/// <summary>An average counter that shows how many items are processed, on average, during an operation. Counters of this type display a ratio of the items processed to the number of operations completed. The ratio is calculated by comparing the number of items processed during the last interval to the number of operations completed during the last interval.</summary>
		// Token: 0x04000B4B RID: 2891
		AverageCount64 = 1073874176,
		/// <summary>A base counter that stores the number of sampling interrupts taken and is used as a denominator in the sampling fraction. The sampling fraction is the number of samples that were 1 (or <see langword="true" />) for a sample interrupt. Check that this value is greater than zero before using it as the denominator in a calculation of <see langword="SampleFraction" />.</summary>
		// Token: 0x04000B4C RID: 2892
		SampleBase = 1073939457,
		/// <summary>A base counter that is used in the calculation of time or count averages, such as <see langword="AverageTimer32" /> and <see langword="AverageCount64" />. Stores the denominator for calculating a counter to present "time per operation" or "count per operation".</summary>
		// Token: 0x04000B4D RID: 2893
		AverageBase,
		/// <summary>A base counter that stores the denominator of a counter that presents a general arithmetic fraction. Check that this value is greater than zero before using it as the denominator in a <see langword="RawFraction" /> value calculation.</summary>
		// Token: 0x04000B4E RID: 2894
		RawBase,
		/// <summary>A base counter that indicates the number of items sampled. It is used as the denominator in the calculations to get an average among the items sampled when taking timings of multiple, but similar items. Used with <see langword="CounterMultiTimer" />, <see langword="CounterMultiTimerInverse" />, <see langword="CounterMultiTimer100Ns" />, and <see langword="CounterMultiTimer100NsInverse" />.</summary>
		// Token: 0x04000B4F RID: 2895
		CounterMultiBase = 1107494144
	}
}
