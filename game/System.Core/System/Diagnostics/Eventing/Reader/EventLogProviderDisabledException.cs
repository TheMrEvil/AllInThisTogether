using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Represents the exception that is thrown when a specified event provider name references a disabled event provider. A disabled event provider cannot publish events.</summary>
	// Token: 0x020003A7 RID: 935
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class EventLogProviderDisabledException : EventLogException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException" /> class.</summary>
		// Token: 0x06001BC5 RID: 7109 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogProviderDisabledException()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception thrown.</param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001BC6 RID: 7110 RVA: 0x0000235B File Offset: 0x0000055B
		protected EventLogProviderDisabledException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException" /> class by specifying the error message that describes the current exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		// Token: 0x06001BC7 RID: 7111 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogProviderDisabledException(string message)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.EventLogProviderDisabledException" /> class with an error message and inner exception.</summary>
		/// <param name="message">The error message that describes the current exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		// Token: 0x06001BC8 RID: 7112 RVA: 0x0000235B File Offset: 0x0000055B
		public EventLogProviderDisabledException(string message, Exception innerException)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
