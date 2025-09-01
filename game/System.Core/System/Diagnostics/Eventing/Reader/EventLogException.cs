using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents the base class for all the exceptions that are thrown when an error occurs while reading event log related information. </summary>
	// Token: 0x020003A2 RID: 930
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogException" /> class.</summary>
		// Token: 0x06001BB1 RID: 7089 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogException()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogException" /> class with the error code for the exception.</summary>
		/// <param name="errorCode">The error code for the error that occurred while reading or configuring event log related information. For more information and a list of event log related error codes, see http://go.microsoft.com/fwlink/?LinkId=82629.</param>
		// Token: 0x06001BB2 RID: 7090 RVA: 0x0000235B File Offset: 0x0000055B
		protected EventLogException(int errorCode)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001BB3 RID: 7091 RVA: 0x0000235B File Offset: 0x0000055B
		protected EventLogException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogException" /> class by specifying the error message that describes the current exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		// Token: 0x06001BB4 RID: 7092 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogException(string message)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogException" /> class with an error message and inner exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		// Token: 0x06001BB5 RID: 7093 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogException(string message, Exception innerException)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
