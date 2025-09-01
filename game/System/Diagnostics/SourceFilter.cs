using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether a listener should trace a message based on the source of a trace.</summary>
	// Token: 0x02000221 RID: 545
	public class SourceFilter : TraceFilter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceFilter" /> class, specifying the name of the trace source.</summary>
		/// <param name="source">The name of the trace source.</param>
		// Token: 0x06000FDC RID: 4060 RVA: 0x00046415 File Offset: 0x00044615
		public SourceFilter(string source)
		{
			this.Source = source;
		}

		/// <summary>Determines whether the trace listener should trace the event.</summary>
		/// <param name="cache">An object that represents the information cache for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the enumeration values that identifies the event type.</param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">The format to use for writing an array of arguments or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		/// <returns>
		///   <see langword="true" /> if the trace should be produced; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06000FDD RID: 4061 RVA: 0x00046424 File Offset: 0x00044624
		public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return string.Equals(this.src, source);
		}

		/// <summary>Gets or sets the name of the trace source.</summary>
		/// <returns>The name of the trace source.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is <see langword="null" />.</exception>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00046440 File Offset: 0x00044640
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00046448 File Offset: 0x00044648
		public string Source
		{
			get
			{
				return this.src;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("source");
				}
				this.src = value;
			}
		}

		// Token: 0x040009AB RID: 2475
		private string src;
	}
}
