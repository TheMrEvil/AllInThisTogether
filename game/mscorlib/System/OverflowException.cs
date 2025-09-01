using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an arithmetic, casting, or conversion operation in a checked context results in an overflow.</summary>
	// Token: 0x0200016B RID: 363
	[Serializable]
	public class OverflowException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class.</summary>
		// Token: 0x06000E6E RID: 3694 RVA: 0x0003AE0D File Offset: 0x0003900D
		public OverflowException() : base("Arithmetic operation resulted in an overflow.")
		{
			base.HResult = -2146233066;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000E6F RID: 3695 RVA: 0x0003AE25 File Offset: 0x00039025
		public OverflowException(string message) : base(message)
		{
			base.HResult = -2146233066;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000E70 RID: 3696 RVA: 0x0003AE39 File Offset: 0x00039039
		public OverflowException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233066;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.OverflowException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000E71 RID: 3697 RVA: 0x00028203 File Offset: 0x00026403
		protected OverflowException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
