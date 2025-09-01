using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the execution stack overflows because it contains too many nested method calls. This class cannot be inherited.</summary>
	// Token: 0x02000186 RID: 390
	[Serializable]
	public sealed class StackOverflowException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "The requested operation caused a stack overflow." This message takes into account the current system culture.</summary>
		// Token: 0x06000F9A RID: 3994 RVA: 0x000414BA File Offset: 0x0003F6BA
		public StackOverflowException() : base("Operation caused a stack overflow.")
		{
			base.HResult = -2147023895;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error. The content of message is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06000F9B RID: 3995 RVA: 0x000414D2 File Offset: 0x0003F6D2
		public StackOverflowException(string message) : base(message)
		{
			base.HResult = -2147023895;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.StackOverflowException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000F9C RID: 3996 RVA: 0x000414E6 File Offset: 0x0003F6E6
		public StackOverflowException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147023895;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00020A65 File Offset: 0x0001EC65
		internal StackOverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
