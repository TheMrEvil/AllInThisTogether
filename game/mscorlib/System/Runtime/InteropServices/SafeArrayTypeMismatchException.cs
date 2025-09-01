using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when the type of the incoming <see langword="SAFEARRAY" /> does not match the type specified in the managed signature.</summary>
	// Token: 0x020006DE RID: 1758
	[Serializable]
	public class SafeArrayTypeMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="SafeArrayTypeMismatchException" /> class with default values.</summary>
		// Token: 0x0600404A RID: 16458 RVA: 0x000E0DBF File Offset: 0x000DEFBF
		public SafeArrayTypeMismatchException() : base("Specified array was not of the expected type.")
		{
			base.HResult = -2146233037;
		}

		/// <summary>Initializes a new instance of the <see langword="SafeArrayTypeMismatchException" /> class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception.</param>
		// Token: 0x0600404B RID: 16459 RVA: 0x000E0DD7 File Offset: 0x000DEFD7
		public SafeArrayTypeMismatchException(string message) : base(message)
		{
			base.HResult = -2146233037;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeArrayTypeMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600404C RID: 16460 RVA: 0x000E0DEB File Offset: 0x000DEFEB
		public SafeArrayTypeMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233037;
		}

		/// <summary>Initializes a new instance of the <see langword="SafeArrayTypeMismatchException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x0600404D RID: 16461 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected SafeArrayTypeMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
