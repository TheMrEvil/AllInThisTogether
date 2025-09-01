using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when a <see cref="T:System.Threading.Thread" /> is in an invalid <see cref="P:System.Threading.Thread.ThreadState" /> for the method call.</summary>
	// Token: 0x0200029A RID: 666
	[Serializable]
	public class ThreadStateException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with default properties.</summary>
		// Token: 0x06001DA2 RID: 7586 RVA: 0x0006E72A File Offset: 0x0006C92A
		public ThreadStateException() : base("Thread was in an invalid state for the operation being executed.")
		{
			base.HResult = -2146233056;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06001DA3 RID: 7587 RVA: 0x0006E742 File Offset: 0x0006C942
		public ThreadStateException(string message) : base(message)
		{
			base.HResult = -2146233056;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06001DA4 RID: 7588 RVA: 0x0006E756 File Offset: 0x0006C956
		public ThreadStateException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233056;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadStateException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x06001DA5 RID: 7589 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected ThreadStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
