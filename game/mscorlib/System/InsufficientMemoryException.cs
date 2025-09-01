using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a check for sufficient available memory fails. This class cannot be inherited.</summary>
	// Token: 0x020001CD RID: 461
	[Serializable]
	public sealed class InsufficientMemoryException : OutOfMemoryException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060013C5 RID: 5061 RVA: 0x0004E9E3 File Offset: 0x0004CBE3
		public InsufficientMemoryException() : base("Insufficient memory to continue the execution of the program.")
		{
			base.HResult = -2146233027;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060013C6 RID: 5062 RVA: 0x0004E9FB File Offset: 0x0004CBFB
		public InsufficientMemoryException(string message) : base(message)
		{
			base.HResult = -2146233027;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InsufficientMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060013C7 RID: 5063 RVA: 0x0004EA0F File Offset: 0x0004CC0F
		public InsufficientMemoryException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233027;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0004EA24 File Offset: 0x0004CC24
		private InsufficientMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
