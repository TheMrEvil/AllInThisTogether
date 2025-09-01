using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to either the standard output or the standard error stream.</summary>
	// Token: 0x02000213 RID: 531
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class ConsoleTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with trace output written to the standard output stream.</summary>
		// Token: 0x06000F4C RID: 3916 RVA: 0x00044B40 File Offset: 0x00042D40
		public ConsoleTraceListener() : base(Console.Out)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with an option to write trace output to the standard output stream or the standard error stream.</summary>
		/// <param name="useErrorStream">
		///   <see langword="true" /> to write tracing and debugging output to the standard error stream; <see langword="false" /> to write tracing and debugging output to the standard output stream.</param>
		// Token: 0x06000F4D RID: 3917 RVA: 0x00044B4D File Offset: 0x00042D4D
		public ConsoleTraceListener(bool useErrorStream) : base(useErrorStream ? Console.Error : Console.Out)
		{
		}

		/// <summary>Closes the output to the stream specified for this trace listener.</summary>
		// Token: 0x06000F4E RID: 3918 RVA: 0x00003917 File Offset: 0x00001B17
		public override void Close()
		{
		}
	}
}
