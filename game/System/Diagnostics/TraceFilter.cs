using System;

namespace System.Diagnostics
{
	/// <summary>Provides the base class for trace filter implementations.</summary>
	// Token: 0x0200022E RID: 558
	public abstract class TraceFilter
	{
		/// <summary>When overridden in a derived class, determines whether the trace listener should trace the event.</summary>
		/// <param name="cache">The <see cref="T:System.Diagnostics.TraceEventCache" /> that contains information for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">Either the format to use for writing an array of arguments specified by the <paramref name="args" /> parameter, or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		/// <returns>
		///   <see langword="true" /> to trace the specified event; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001068 RID: 4200
		public abstract bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data);

		// Token: 0x06001069 RID: 4201 RVA: 0x0004745C File Offset: 0x0004565C
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, null, null, null);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0004747C File Offset: 0x0004567C
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, null, null);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004749C File Offset: 0x0004569C
		internal bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1)
		{
			return this.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, data1, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.TraceFilter" /> class.</summary>
		// Token: 0x0600106C RID: 4204 RVA: 0x0000219B File Offset: 0x0000039B
		protected TraceFilter()
		{
		}

		// Token: 0x040009E3 RID: 2531
		internal string initializeData;
	}
}
