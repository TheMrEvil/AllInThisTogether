using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when the rank of an incoming <see langword="SAFEARRAY" /> does not match the rank specified in the managed signature.</summary>
	// Token: 0x020006DD RID: 1757
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see langword="SafeArrayTypeMismatchException" /> class with default values.</summary>
		// Token: 0x06004046 RID: 16454 RVA: 0x000E0D7E File Offset: 0x000DEF7E
		public SafeArrayRankMismatchException() : base("Specified array was not of the expected rank.")
		{
			base.HResult = -2146233032;
		}

		/// <summary>Initializes a new instance of the <see langword="SafeArrayRankMismatchException" /> class with the specified message.</summary>
		/// <param name="message">The message that indicates the reason for the exception.</param>
		// Token: 0x06004047 RID: 16455 RVA: 0x000E0D96 File Offset: 0x000DEF96
		public SafeArrayRankMismatchException(string message) : base(message)
		{
			base.HResult = -2146233032;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeArrayRankMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06004048 RID: 16456 RVA: 0x000E0DAA File Offset: 0x000DEFAA
		public SafeArrayRankMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233032;
		}

		/// <summary>Initializes a new instance of the <see langword="SafeArrayTypeMismatchException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06004049 RID: 16457 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
