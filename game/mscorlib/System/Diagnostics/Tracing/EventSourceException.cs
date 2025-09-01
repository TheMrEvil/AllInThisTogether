using System;
using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
	/// <summary>The exception that is thrown when an error occurs during event tracing for Windows (ETW).</summary>
	// Token: 0x020009E4 RID: 2532
	[Serializable]
	public class EventSourceException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class.</summary>
		// Token: 0x06005A8F RID: 23183 RVA: 0x001342B8 File Offset: 0x001324B8
		public EventSourceException() : base("An error occurred when writing to a listener.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06005A90 RID: 23184 RVA: 0x000328A2 File Offset: 0x00030AA2
		public EventSourceException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null" /> if no inner exception is specified.</param>
		// Token: 0x06005A91 RID: 23185 RVA: 0x000328AB File Offset: 0x00030AAB
		public EventSourceException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSourceException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06005A92 RID: 23186 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		protected EventSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x001342C5 File Offset: 0x001324C5
		internal EventSourceException(Exception innerException) : base("An error occurred when writing to a listener.", innerException)
		{
		}
	}
}
