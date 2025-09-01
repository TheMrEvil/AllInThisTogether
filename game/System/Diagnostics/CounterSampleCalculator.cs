using System;

namespace System.Diagnostics
{
	/// <summary>Provides a set of utility functions for interpreting performance counter data.</summary>
	// Token: 0x0200024F RID: 591
	public static class CounterSampleCalculator
	{
		/// <summary>Computes the calculated value of a single raw counter sample.</summary>
		/// <param name="newSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates the most recent sample the system has taken.</param>
		/// <returns>A floating-point representation of the performance counter's calculated value.</returns>
		// Token: 0x0600123D RID: 4669 RVA: 0x0004E670 File Offset: 0x0004C870
		public static float ComputeCounterValue(CounterSample newSample)
		{
			PerformanceCounterType counterType = newSample.CounterType;
			if (counterType <= PerformanceCounterType.NumberOfItemsHEX64)
			{
				if (counterType != PerformanceCounterType.NumberOfItemsHEX32 && counterType != PerformanceCounterType.NumberOfItemsHEX64)
				{
					goto IL_3E;
				}
			}
			else if (counterType != PerformanceCounterType.NumberOfItems32 && counterType != PerformanceCounterType.NumberOfItems64 && counterType != PerformanceCounterType.RawFraction)
			{
				goto IL_3E;
			}
			return (float)newSample.RawValue;
			IL_3E:
			return 0f;
		}

		/// <summary>Computes the calculated value of two raw counter samples.</summary>
		/// <param name="oldSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates a previous sample the system has taken.</param>
		/// <param name="newSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates the most recent sample the system has taken.</param>
		/// <returns>A floating-point representation of the performance counter's calculated value.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="oldSample" /> uses a counter type that is different from <paramref name="newSample" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <paramref name="newSample" /> counter type has a Performance Data Helper (PDH) error. For more information, see "Checking PDH Interface Return Values" in the Win32 and COM Development section of this documentation.</exception>
		// Token: 0x0600123E RID: 4670 RVA: 0x0004E6C0 File Offset: 0x0004C8C0
		[MonoTODO("What's the algorithm?")]
		public static float ComputeCounterValue(CounterSample oldSample, CounterSample newSample)
		{
			if (newSample.CounterType != oldSample.CounterType)
			{
				throw new Exception("The counter samples must be of the same type");
			}
			PerformanceCounterType counterType = newSample.CounterType;
			if (counterType <= PerformanceCounterType.RawFraction)
			{
				if (counterType <= PerformanceCounterType.CounterDelta32)
				{
					if (counterType <= PerformanceCounterType.NumberOfItemsHEX64)
					{
						if (counterType != PerformanceCounterType.NumberOfItemsHEX32 && counterType != PerformanceCounterType.NumberOfItemsHEX64)
						{
							goto IL_36C;
						}
					}
					else if (counterType != PerformanceCounterType.NumberOfItems32 && counterType != PerformanceCounterType.NumberOfItems64)
					{
						if (counterType != PerformanceCounterType.CounterDelta32)
						{
							goto IL_36C;
						}
						goto IL_1C3;
					}
				}
				else if (counterType <= PerformanceCounterType.CountPerTimeInterval64)
				{
					if (counterType == PerformanceCounterType.CounterDelta64)
					{
						goto IL_1C3;
					}
					if (counterType != PerformanceCounterType.CountPerTimeInterval32 && counterType != PerformanceCounterType.CountPerTimeInterval64)
					{
						goto IL_36C;
					}
					goto IL_298;
				}
				else
				{
					if (counterType == PerformanceCounterType.RateOfCountsPerSecond32 || counterType == PerformanceCounterType.RateOfCountsPerSecond64)
					{
						return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp) * 10000000f;
					}
					if (counterType != PerformanceCounterType.RawFraction)
					{
						goto IL_36C;
					}
				}
				return (float)newSample.RawValue;
				IL_1C3:
				return (float)(newSample.RawValue - oldSample.RawValue);
			}
			if (counterType <= PerformanceCounterType.CounterMultiTimer)
			{
				if (counterType <= PerformanceCounterType.Timer100Ns)
				{
					if (counterType != PerformanceCounterType.CounterTimer)
					{
						if (counterType != PerformanceCounterType.Timer100Ns)
						{
							goto IL_36C;
						}
						return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp) * 100f;
					}
				}
				else
				{
					if (counterType == PerformanceCounterType.CounterTimerInverse)
					{
						return (1f - (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec)) * 100f;
					}
					if (counterType == PerformanceCounterType.Timer100NsInverse)
					{
						return (1f - (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp)) * 100f;
					}
					if (counterType != PerformanceCounterType.CounterMultiTimer)
					{
						goto IL_36C;
					}
					return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp) * 100f / (float)newSample.BaseValue;
				}
			}
			else if (counterType <= PerformanceCounterType.CounterMultiTimer100NsInverse)
			{
				if (counterType == PerformanceCounterType.CounterMultiTimer100Ns)
				{
					return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec) * 100f / (float)newSample.BaseValue;
				}
				if (counterType == PerformanceCounterType.CounterMultiTimerInverse)
				{
					return ((float)newSample.BaseValue - (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp)) * 100f;
				}
				if (counterType != PerformanceCounterType.CounterMultiTimer100NsInverse)
				{
					goto IL_36C;
				}
				return ((float)newSample.BaseValue - (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec)) * 100f;
			}
			else
			{
				if (counterType == PerformanceCounterType.AverageTimer32)
				{
					return (float)(newSample.RawValue - oldSample.RawValue) / (float)newSample.SystemFrequency / (float)(newSample.BaseValue - oldSample.BaseValue);
				}
				if (counterType == PerformanceCounterType.ElapsedTime)
				{
					return 0f;
				}
				if (counterType != PerformanceCounterType.AverageCount64)
				{
					goto IL_36C;
				}
				return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.BaseValue - oldSample.BaseValue);
			}
			IL_298:
			return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp);
			IL_36C:
			Console.WriteLine("Counter type {0} not handled", newSample.CounterType);
			return 0f;
		}
	}
}
