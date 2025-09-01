using System;

namespace System.Diagnostics
{
	/// <summary>Defines a structure that holds the raw data for a performance counter.</summary>
	// Token: 0x0200024E RID: 590
	public struct CounterSample
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to 0 (zero).</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample.</param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters.</param>
		/// <param name="counterFrequency">The frequency with which the counter is read.</param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter.</param>
		/// <param name="timeStamp">The raw time stamp.</param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp.</param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</param>
		// Token: 0x0600122B RID: 4651 RVA: 0x0004E498 File Offset: 0x0004C698
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType)
		{
			this = new CounterSample(rawValue, baseValue, counterFrequency, systemFrequency, timeStamp, timeStamp100nSec, counterType, 0L);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to the value that is passed in.</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample.</param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters.</param>
		/// <param name="counterFrequency">The frequency with which the counter is read.</param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter.</param>
		/// <param name="timeStamp">The raw time stamp.</param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp.</param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</param>
		/// <param name="counterTimeStamp">The time at which the sample was taken.</param>
		// Token: 0x0600122C RID: 4652 RVA: 0x0004E4B8 File Offset: 0x0004C6B8
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType, long counterTimeStamp)
		{
			this.rawValue = rawValue;
			this.baseValue = baseValue;
			this.counterFrequency = counterFrequency;
			this.systemFrequency = systemFrequency;
			this.timeStamp = timeStamp;
			this.timeStamp100nSec = timeStamp100nSec;
			this.counterType = counterType;
			this.counterTimeStamp = counterTimeStamp;
		}

		/// <summary>Gets an optional, base raw value for the counter.</summary>
		/// <returns>The base raw value, which is used only if the sample is based on multiple counters.</returns>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0004E4F7 File Offset: 0x0004C6F7
		public long BaseValue
		{
			get
			{
				return this.baseValue;
			}
		}

		/// <summary>Gets the raw counter frequency.</summary>
		/// <returns>The frequency with which the counter is read.</returns>
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0004E4FF File Offset: 0x0004C6FF
		public long CounterFrequency
		{
			get
			{
				return this.counterFrequency;
			}
		}

		/// <summary>Gets the counter's time stamp.</summary>
		/// <returns>The time at which the sample was taken.</returns>
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0004E507 File Offset: 0x0004C707
		public long CounterTimeStamp
		{
			get
			{
				return this.counterTimeStamp;
			}
		}

		/// <summary>Gets the performance counter type.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</returns>
		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0004E50F File Offset: 0x0004C70F
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		/// <summary>Gets the raw value of the counter.</summary>
		/// <returns>The numeric value that is associated with the performance counter sample.</returns>
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0004E517 File Offset: 0x0004C717
		public long RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		/// <summary>Gets the raw system frequency.</summary>
		/// <returns>The frequency with which the system reads from the counter.</returns>
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0004E51F File Offset: 0x0004C71F
		public long SystemFrequency
		{
			get
			{
				return this.systemFrequency;
			}
		}

		/// <summary>Gets the raw time stamp.</summary>
		/// <returns>The system time stamp.</returns>
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0004E527 File Offset: 0x0004C727
		public long TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		/// <summary>Gets the raw, high-fidelity time stamp.</summary>
		/// <returns>The system time stamp, represented within 0.1 millisecond.</returns>
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0004E52F File Offset: 0x0004C72F
		public long TimeStamp100nSec
		{
			get
			{
				return this.timeStamp100nSec;
			}
		}

		/// <summary>Calculates the performance data of the counter, using a single sample point. This method is generally used for uncalculated performance counter types.</summary>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data.</param>
		/// <returns>The calculated performance value.</returns>
		// Token: 0x06001235 RID: 4661 RVA: 0x0004E537 File Offset: 0x0004C737
		public static float Calculate(CounterSample counterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample);
		}

		/// <summary>Calculates the performance data of the counter, using two sample points. This method is generally used for calculated performance counter types, such as averages.</summary>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data.</param>
		/// <param name="nextCounterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as an ending point for calculating performance data.</param>
		/// <returns>The calculated performance value.</returns>
		// Token: 0x06001236 RID: 4662 RVA: 0x0004E53F File Offset: 0x0004C73F
		public static float Calculate(CounterSample counterSample, CounterSample nextCounterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample, nextCounterSample);
		}

		/// <summary>Indicates whether the specified structure is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <param name="o">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with the current structure.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001237 RID: 4663 RVA: 0x0004E548 File Offset: 0x0004C748
		public override bool Equals(object o)
		{
			return o is CounterSample && this.Equals((CounterSample)o);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Diagnostics.CounterSample" /> structure is equal to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <param name="sample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="sample" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001238 RID: 4664 RVA: 0x0004E560 File Offset: 0x0004C760
		public bool Equals(CounterSample sample)
		{
			return this.rawValue == sample.rawValue && this.baseValue == sample.counterFrequency && this.counterFrequency == sample.counterFrequency && this.systemFrequency == sample.systemFrequency && this.timeStamp == sample.timeStamp && this.timeStamp100nSec == sample.timeStamp100nSec && this.counterTimeStamp == sample.counterTimeStamp && this.counterType == sample.counterType;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are equal.</summary>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001239 RID: 4665 RVA: 0x0004E5DF File Offset: 0x0004C7DF
		public static bool operator ==(CounterSample a, CounterSample b)
		{
			return a.Equals(b);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are not equal.</summary>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, <see langword="false" /></returns>
		// Token: 0x0600123A RID: 4666 RVA: 0x0004E5E9 File Offset: 0x0004C7E9
		public static bool operator !=(CounterSample a, CounterSample b)
		{
			return !a.Equals(b);
		}

		/// <summary>Gets a hash code for the current counter sample.</summary>
		/// <returns>A hash code for the current counter sample.</returns>
		// Token: 0x0600123B RID: 4667 RVA: 0x0004E5F8 File Offset: 0x0004C7F8
		public override int GetHashCode()
		{
			return (int)(this.rawValue << 28 ^ (this.baseValue << 24 ^ (this.counterFrequency << 20 ^ (this.systemFrequency << 16 ^ (this.timeStamp << 8 ^ (this.timeStamp100nSec << 4 ^ (this.counterTimeStamp ^ (long)this.counterType)))))));
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0004E64E File Offset: 0x0004C84E
		// Note: this type is marked as 'beforefieldinit'.
		static CounterSample()
		{
		}

		// Token: 0x04000A8F RID: 2703
		private long rawValue;

		// Token: 0x04000A90 RID: 2704
		private long baseValue;

		// Token: 0x04000A91 RID: 2705
		private long counterFrequency;

		// Token: 0x04000A92 RID: 2706
		private long systemFrequency;

		// Token: 0x04000A93 RID: 2707
		private long timeStamp;

		// Token: 0x04000A94 RID: 2708
		private long timeStamp100nSec;

		// Token: 0x04000A95 RID: 2709
		private long counterTimeStamp;

		// Token: 0x04000A96 RID: 2710
		private PerformanceCounterType counterType;

		/// <summary>Defines an empty, uninitialized performance counter sample of type <see langword="NumberOfItems32" />.</summary>
		// Token: 0x04000A97 RID: 2711
		public static CounterSample Empty = new CounterSample(0L, 0L, 0L, 0L, 0L, 0L, PerformanceCounterType.NumberOfItems32, 0L);
	}
}
