using System;

namespace System.Diagnostics.PerformanceData
{
	/// <summary>Specifies whether the counter set allows multiple instances such as processes and physical disks, or a single instance such as memory.</summary>
	// Token: 0x0200038F RID: 911
	public enum CounterSetInstanceType
	{
		/// <summary>The counter set contains single instance counters whose aggregate value is obtained from one or more sources. For example, a counter in this type of counter set might obtain the number of reads from each of the three hard disks on the computer and sum their values.</summary>
		// Token: 0x04000D13 RID: 3347
		GlobalAggregate = 4,
		/// <summary>This type is similar to <see cref="F:System.Diagnostics.PerformanceData.CounterSetInstanceType.GlobalAggregate" /> except that this counter set type stores all counter values for the lifetime of the consumer application (the counter value is cached beyond the lifetime of the counter). For example, if one of the hard disks in the global aggregate example were to become unavailable, the total bytes read by that disk would still be available and used to calculate the aggregate value.</summary>
		// Token: 0x04000D14 RID: 3348
		GlobalAggregateWithHistory = 11,
		/// <summary>This type is similar to <see cref="F:System.Diagnostics.PerformanceData.CounterSetInstanceType.MultipleAggregate" />, except that instead of aggregating all instance data to one aggregated (_Total) instance, it will aggregate counter data from instances of the same name. For example, if multiple provider processes contained instances named IExplore, <see cref="F:System.Diagnostics.PerformanceData.CounterSetInstanceType.Multiple" /> and <see cref="F:System.Diagnostics.PerformanceData.CounterSetInstanceType.MultipleAggregate" /> CounterSet will show multiple IExplore instances (IExplore, IExplore#1, IExplore#2, and so on); however, a <see cref="F:System.Diagnostics.PerformanceData.CounterSetInstanceType.InstanceAggregate" /> instance type will publish only one IExplore instance with aggregated counter data from all instances named IExplore.</summary>
		// Token: 0x04000D15 RID: 3349
		InstanceAggregate = 22,
		/// <summary>The counter set contains multiple instance counters, for example, a counter that measures the average disk I/O for a process.</summary>
		// Token: 0x04000D16 RID: 3350
		Multiple = 2,
		/// <summary>The counter set contains multiple instance counters whose aggregate value is obtained from all instances of the counter. For example, a counter in this type of counter set might obtain the total thread execution time for all threads in a multithreaded application and sum their values.</summary>
		// Token: 0x04000D17 RID: 3351
		MultipleAggregate = 6,
		/// <summary>The counter set contains single instance counters, for example, a counter that measures physical memory.</summary>
		// Token: 0x04000D18 RID: 3352
		Single = 0
	}
}
