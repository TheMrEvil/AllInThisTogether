using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an attempt is made to store an element of the wrong type within an array.</summary>
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public class ArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class.</summary>
		// Token: 0x06000743 RID: 1859 RVA: 0x000216D3 File Offset: 0x0001F8D3
		public ArrayTypeMismatchException() : base("Attempted to access an element as a type incompatible with the array.")
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06000744 RID: 1860 RVA: 0x000216EB File Offset: 0x0001F8EB
		public ArrayTypeMismatchException(string message) : base(message)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000745 RID: 1861 RVA: 0x000216FF File Offset: 0x0001F8FF
		public ArrayTypeMismatchException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ArrayTypeMismatchException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000746 RID: 1862 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected ArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
