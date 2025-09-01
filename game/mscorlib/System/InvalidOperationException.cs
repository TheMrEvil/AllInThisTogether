using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when a method call is invalid for the object's current state.</summary>
	// Token: 0x0200014C RID: 332
	[Serializable]
	public class InvalidOperationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class.</summary>
		// Token: 0x06000C8E RID: 3214 RVA: 0x00032820 File Offset: 0x00030A20
		public InvalidOperationException() : base("Operation is not valid due to the current state of the object.")
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000C8F RID: 3215 RVA: 0x00032838 File Offset: 0x00030A38
		public InvalidOperationException(string message) : base(message)
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000C90 RID: 3216 RVA: 0x0003284C File Offset: 0x00030A4C
		public InvalidOperationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233079;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidOperationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000C91 RID: 3217 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected InvalidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
