using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents the exception thrown when an event provider publishes invalid data in an event.</summary>
	// Token: 0x020003A3 RID: 931
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogInvalidDataException : EventLogException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInvalidDataException" /> class.</summary>
		// Token: 0x06001BB6 RID: 7094 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogInvalidDataException()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInvalidDataException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001BB7 RID: 7095 RVA: 0x0000235B File Offset: 0x0000055B
		protected EventLogInvalidDataException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInvalidDataException" /> class by specifying the error message that describes the current exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		// Token: 0x06001BB8 RID: 7096 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogInvalidDataException(string message)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogInvalidDataException" /> class with an error message and inner exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		// Token: 0x06001BB9 RID: 7097 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogInvalidDataException(string message, Exception innerException)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
