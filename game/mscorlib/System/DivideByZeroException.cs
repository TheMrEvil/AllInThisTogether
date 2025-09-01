using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to divide an integral or <see cref="T:System.Decimal" /> value by zero.</summary>
	// Token: 0x02000110 RID: 272
	[Serializable]
	public class DivideByZeroException : ArithmeticException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class.</summary>
		// Token: 0x06000A8B RID: 2699 RVA: 0x000281C2 File Offset: 0x000263C2
		public DivideByZeroException() : base("Attempted to divide by zero.")
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06000A8C RID: 2700 RVA: 0x000281DA File Offset: 0x000263DA
		public DivideByZeroException(string message) : base(message)
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000A8D RID: 2701 RVA: 0x000281EE File Offset: 0x000263EE
		public DivideByZeroException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147352558;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DivideByZeroException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000A8E RID: 2702 RVA: 0x00028203 File Offset: 0x00026403
		protected DivideByZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
