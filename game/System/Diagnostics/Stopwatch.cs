using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that you can use to accurately measure elapsed time.</summary>
	// Token: 0x02000280 RID: 640
	public class Stopwatch
	{
		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>A long integer representing the tick counter value of the underlying timer mechanism.</returns>
		// Token: 0x0600145A RID: 5210
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTimestamp();

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Stopwatch" /> instance, sets the elapsed time property to zero, and starts measuring elapsed time.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Stopwatch" /> that has just begun measuring elapsed time.</returns>
		// Token: 0x0600145B RID: 5211 RVA: 0x0005313A File Offset: 0x0005133A
		public static Stopwatch StartNew()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Stopwatch" /> class.</summary>
		// Token: 0x0600145C RID: 5212 RVA: 0x0000219B File Offset: 0x0000039B
		public Stopwatch()
		{
		}

		/// <summary>Gets the total elapsed time measured by the current instance.</summary>
		/// <returns>A read-only <see cref="T:System.TimeSpan" /> representing the total elapsed time measured by the current instance.</returns>
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00053147 File Offset: 0x00051347
		public TimeSpan Elapsed
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return TimeSpan.FromTicks(this.ElapsedTicks / (Stopwatch.Frequency / 10000000L));
				}
				return TimeSpan.FromTicks(this.ElapsedTicks);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in milliseconds.</summary>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00053174 File Offset: 0x00051374
		public long ElapsedMilliseconds
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return this.ElapsedTicks / (Stopwatch.Frequency / 1000L);
				}
				return checked((long)this.Elapsed.TotalMilliseconds);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in timer ticks.</summary>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x000531AB File Offset: 0x000513AB
		public long ElapsedTicks
		{
			get
			{
				if (!this.is_running)
				{
					return this.elapsed;
				}
				return Stopwatch.GetTimestamp() - this.started + this.elapsed;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.Stopwatch" /> timer is running.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Diagnostics.Stopwatch" /> instance is currently running and measuring elapsed time for an interval; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x000531CF File Offset: 0x000513CF
		public bool IsRunning
		{
			get
			{
				return this.is_running;
			}
		}

		/// <summary>Stops time interval measurement and resets the elapsed time to zero.</summary>
		// Token: 0x06001461 RID: 5217 RVA: 0x000531D7 File Offset: 0x000513D7
		public void Reset()
		{
			this.elapsed = 0L;
			this.is_running = false;
		}

		/// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
		// Token: 0x06001462 RID: 5218 RVA: 0x000531E8 File Offset: 0x000513E8
		public void Start()
		{
			if (this.is_running)
			{
				return;
			}
			this.started = Stopwatch.GetTimestamp();
			this.is_running = true;
		}

		/// <summary>Stops measuring elapsed time for an interval.</summary>
		// Token: 0x06001463 RID: 5219 RVA: 0x00053205 File Offset: 0x00051405
		public void Stop()
		{
			if (!this.is_running)
			{
				return;
			}
			this.elapsed += Stopwatch.GetTimestamp() - this.started;
			if (this.elapsed < 0L)
			{
				this.elapsed = 0L;
			}
			this.is_running = false;
		}

		/// <summary>Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.</summary>
		// Token: 0x06001464 RID: 5220 RVA: 0x00053242 File Offset: 0x00051442
		public void Restart()
		{
			this.started = Stopwatch.GetTimestamp();
			this.elapsed = 0L;
			this.is_running = true;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0005325E File Offset: 0x0005145E
		// Note: this type is marked as 'beforefieldinit'.
		static Stopwatch()
		{
		}

		/// <summary>Gets the frequency of the timer as the number of ticks per second. This field is read-only.</summary>
		// Token: 0x04000B62 RID: 2914
		public static readonly long Frequency = 10000000L;

		/// <summary>Indicates whether the timer is based on a high-resolution performance counter. This field is read-only.</summary>
		// Token: 0x04000B63 RID: 2915
		public static readonly bool IsHighResolution = true;

		// Token: 0x04000B64 RID: 2916
		private long elapsed;

		// Token: 0x04000B65 RID: 2917
		private long started;

		// Token: 0x04000B66 RID: 2918
		private bool is_running;
	}
}
