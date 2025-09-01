using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a method requires the caller to own the lock on a given Monitor, and the method is invoked by a caller that does not own that lock.</summary>
	// Token: 0x02000295 RID: 661
	[Serializable]
	public class SynchronizationLockException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with default properties.</summary>
		// Token: 0x06001D97 RID: 7575 RVA: 0x0006E6B8 File Offset: 0x0006C8B8
		public SynchronizationLockException() : base("Object synchronization method was called from an unsynchronized block of code.")
		{
			base.HResult = -2146233064;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001D98 RID: 7576 RVA: 0x0006E6D0 File Offset: 0x0006C8D0
		public SynchronizationLockException(string message) : base(message)
		{
			base.HResult = -2146233064;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001D99 RID: 7577 RVA: 0x0006E6E4 File Offset: 0x0006C8E4
		public SynchronizationLockException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233064;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SynchronizationLockException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06001D9A RID: 7578 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected SynchronizationLockException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
