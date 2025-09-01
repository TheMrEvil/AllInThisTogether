using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when an attempt is made to open a system mutex, semaphore, or event wait handle that does not exist.</summary>
	// Token: 0x0200029C RID: 668
	[Serializable]
	public class WaitHandleCannotBeOpenedException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with default values.</summary>
		// Token: 0x06001DA7 RID: 7591 RVA: 0x0006E77C File Offset: 0x0006C97C
		public WaitHandleCannotBeOpenedException() : base("No handle of the given name exists.")
		{
			base.HResult = -2146233044;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001DA8 RID: 7592 RVA: 0x0006E794 File Offset: 0x0006C994
		public WaitHandleCannotBeOpenedException(string message) : base(message)
		{
			base.HResult = -2146233044;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001DA9 RID: 7593 RVA: 0x0006E7A8 File Offset: 0x0006C9A8
		public WaitHandleCannotBeOpenedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233044;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandleCannotBeOpenedException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x06001DAA RID: 7594 RVA: 0x0006E7BD File Offset: 0x0006C9BD
		protected WaitHandleCannotBeOpenedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
