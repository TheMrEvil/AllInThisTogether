using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown for errors in an arithmetic, casting, or conversion operation.</summary>
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public class ArithmeticException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class.</summary>
		// Token: 0x06000715 RID: 1813 RVA: 0x00021229 File Offset: 0x0001F429
		public ArithmeticException() : base("Overflow or underflow in the arithmetic operation.")
		{
			base.HResult = -2147024362;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06000716 RID: 1814 RVA: 0x00021241 File Offset: 0x0001F441
		public ArithmeticException(string message) : base(message)
		{
			base.HResult = -2147024362;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000717 RID: 1815 RVA: 0x00021255 File Offset: 0x0001F455
		public ArithmeticException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024362;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArithmeticException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000718 RID: 1816 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected ArithmeticException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
