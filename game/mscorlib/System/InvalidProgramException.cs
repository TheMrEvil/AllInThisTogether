using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a program contains invalid Microsoft intermediate language (MSIL) or metadata. Generally this indicates a bug in the compiler that generated the program.</summary>
	// Token: 0x0200014D RID: 333
	[Serializable]
	public sealed class InvalidProgramException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with default properties.</summary>
		// Token: 0x06000C92 RID: 3218 RVA: 0x00032861 File Offset: 0x00030A61
		public InvalidProgramException() : base("Common Language Runtime detected an invalid program.")
		{
			base.HResult = -2146233030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000C93 RID: 3219 RVA: 0x00032879 File Offset: 0x00030A79
		public InvalidProgramException(string message) : base(message)
		{
			base.HResult = -2146233030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidProgramException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000C94 RID: 3220 RVA: 0x0003288D File Offset: 0x00030A8D
		public InvalidProgramException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233030;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00020A65 File Offset: 0x0001EC65
		internal InvalidProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
