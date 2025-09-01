using System;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the ability to collect statistics for very frequent events through the  <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
	// Token: 0x020009EF RID: 2543
	public class EventCounter : DiagnosticCounter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventCounter" /> class.</summary>
		/// <param name="name">The event counter name.</param>
		/// <param name="eventSource">The event source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="eventSource" /> is <see langword="null" />.</exception>
		// Token: 0x06005AB9 RID: 23225 RVA: 0x00134444 File Offset: 0x00132644
		public EventCounter(string name, EventSource eventSource) : base(name, eventSource)
		{
		}

		/// <summary>Writes the metric if performance counters are on.</summary>
		/// <param name="value">The value to be written.</param>
		// Token: 0x06005ABA RID: 23226 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void WriteMetric(float value)
		{
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void WriteMetric(double value)
		{
		}
	}
}
