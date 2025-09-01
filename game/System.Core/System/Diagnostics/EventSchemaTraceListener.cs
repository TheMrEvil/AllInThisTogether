using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output of end-to-end events to an XML-encoded, schema-compliant log file.</summary>
	// Token: 0x0200038A RID: 906
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventSchemaTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class, using the specified file as the recipient of debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		// Token: 0x06001B2E RID: 6958 RVA: 0x0000235B File Offset: 0x0000055B
		public EventSchemaTraceListener(string fileName)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name, using the specified file as the recipient of debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		// Token: 0x06001B2F RID: 6959 RVA: 0x0000235B File Offset: 0x0000055B
		public EventSchemaTraceListener(string fileName, string name)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name and specified buffer size, using the specified file as the recipient of debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <param name="bufferSize">The size of the output buffer, in bytes.</param>
		// Token: 0x06001B30 RID: 6960 RVA: 0x0000235B File Offset: 0x0000055B
		public EventSchemaTraceListener(string fileName, string name, int bufferSize)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name and specified buffer size, using the specified file with the specified log retention policy as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <param name="bufferSize">The size of the output buffer, in bytes.</param>
		/// <param name="logRetentionOption">One of the <see cref="T:System.Diagnostics.TraceLogRetentionOption" /> values. </param>
		// Token: 0x06001B31 RID: 6961 RVA: 0x0000235B File Offset: 0x0000055B
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name and specified buffer size, using the specified file with the specified log retention policy and maximum size as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <param name="bufferSize">The size of the output buffer, in bytes.</param>
		/// <param name="logRetentionOption">One of the <see cref="T:System.Diagnostics.TraceLogRetentionOption" /> values.</param>
		/// <param name="maximumFileSize">The maximum file size, in bytes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="maximumFileSize" /> is less than <paramref name="bufferSize" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="maximumFileSize" /> is a negative number.</exception>
		// Token: 0x06001B32 RID: 6962 RVA: 0x0000235B File Offset: 0x0000055B
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption, long maximumFileSize)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSchemaTraceListener" /> class with the specified name and specified buffer size, using the specified file with the specified log retention policy, maximum size, and file count as the recipient of the debugging and tracing output.</summary>
		/// <param name="fileName">The path for the log file.</param>
		/// <param name="name">The name of the listener.</param>
		/// <param name="bufferSize">The size of the output buffer, in bytes.</param>
		/// <param name="logRetentionOption">One of the <see cref="T:System.Diagnostics.TraceLogRetentionOption" /> values.</param>
		/// <param name="maximumFileSize">The maximum file size, in bytes.</param>
		/// <param name="maximumNumberOfFiles">The maximum number of output log files.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="maximumFileSize" /> is less than <paramref name="bufferSize" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="maximumFileSize" /> is a negative number.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="maximumNumberOfFiles" /> is less than 1, and <paramref name="logRetentionOption" /> is <see cref="F:System.Diagnostics.TraceLogRetentionOption.LimitedSequentialFiles" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="maximumNumberOfFiles" /> is less than 2, and <paramref name="logRetentionOption" /> is <see cref="F:System.Diagnostics.TraceLogRetentionOption.LimitedCircularFiles" />.</exception>
		// Token: 0x06001B33 RID: 6963 RVA: 0x0000235B File Offset: 0x0000055B
		public EventSchemaTraceListener(string fileName, string name, int bufferSize, TraceLogRetentionOption logRetentionOption, long maximumFileSize, int maximumNumberOfFiles)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the size of the output buffer.</summary>
		/// <returns>The size of the output buffer, in bytes. </returns>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0005A294 File Offset: 0x00058494
		public int BufferSize
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the maximum size of the log file.</summary>
		/// <returns>The maximum file size, in bytes.</returns>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x0005A2B0 File Offset: 0x000584B0
		public long MaximumFileSize
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Gets the maximum number of log files.</summary>
		/// <returns>The maximum number of log files, determined by the value of the <see cref="P:System.Diagnostics.EventSchemaTraceListener.TraceLogRetentionOption" /> property for the file.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0005A2CC File Offset: 0x000584CC
		public int MaximumNumberOfFiles
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the trace log retention option for the file.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.TraceLogRetentionOption" /> values. The default is <see cref="F:System.Diagnostics.TraceLogRetentionOption.SingleFileUnboundedSize" />. </returns>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x0005A2E8 File Offset: 0x000584E8
		public TraceLogRetentionOption TraceLogRetentionOption
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return TraceLogRetentionOption.UnlimitedSequentialFiles;
			}
		}
	}
}
