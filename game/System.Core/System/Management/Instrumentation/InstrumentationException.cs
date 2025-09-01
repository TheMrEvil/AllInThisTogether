using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Management.Instrumentation
{
	/// <summary>Represents a provider-related exception.Note: the WMI .NET libraries are now considered in final state, and no further development, enhancements, or updates will be available for non-security related issues affecting these libraries. The MI APIs should be used for all new development.</summary>
	// Token: 0x02000377 RID: 887
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class InstrumentationException : InstrumentationBaseException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class. This is the default constructor.</summary>
		// Token: 0x06001AF2 RID: 6898 RVA: 0x0000235B File Offset: 0x0000055B
		public InstrumentationException()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class with the System.Exception that caused the current exception.</summary>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0000235B File Offset: 0x0000055B
		public InstrumentationException(Exception innerException)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x06001AF4 RID: 6900 RVA: 0x0000235B File Offset: 0x0000055B
		protected InstrumentationException(SerializationInfo info, StreamingContext context)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class with a message that describes the exception.</summary>
		/// <param name="message">Message that describes the exception.</param>
		// Token: 0x06001AF5 RID: 6901 RVA: 0x0000235B File Offset: 0x0000055B
		public InstrumentationException(string message)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new <see cref="T:System.Management.Instrumentation.InstrumentationException" /> class with the specified string and exception.</summary>
		/// <param name="message">Message that describes the exception.</param>
		/// <param name="innerException">The Exception instance that caused the current exception.</param>
		// Token: 0x06001AF6 RID: 6902 RVA: 0x0000235B File Offset: 0x0000055B
		public InstrumentationException(string message, Exception innerException)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
