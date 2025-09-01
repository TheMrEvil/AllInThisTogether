using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when the format of an argument is invalid, or when a composite format string is not well formed.</summary>
	// Token: 0x0200011B RID: 283
	[Serializable]
	public class FormatException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class.</summary>
		// Token: 0x06000AE2 RID: 2786 RVA: 0x000288A5 File Offset: 0x00026AA5
		public FormatException() : base("One of the identified items was in an invalid format.")
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x000288BD File Offset: 0x00026ABD
		public FormatException(string message) : base(message)
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x000288D1 File Offset: 0x00026AD1
		public FormatException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233033;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FormatException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected FormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
