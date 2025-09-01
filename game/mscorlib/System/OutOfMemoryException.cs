using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is not enough memory to continue the execution of a program.</summary>
	// Token: 0x020001D0 RID: 464
	[Serializable]
	public class OutOfMemoryException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class.</summary>
		// Token: 0x060013D7 RID: 5079 RVA: 0x0004EC48 File Offset: 0x0004CE48
		public OutOfMemoryException() : base("Insufficient memory to continue the execution of the program.")
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060013D8 RID: 5080 RVA: 0x0004EC60 File Offset: 0x0004CE60
		public OutOfMemoryException(string message) : base(message)
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060013D9 RID: 5081 RVA: 0x0004EC74 File Offset: 0x0004CE74
		public OutOfMemoryException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024882;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OutOfMemoryException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060013DA RID: 5082 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected OutOfMemoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
