using System;
using System.Runtime.Serialization;

namespace System.Security
{
	/// <summary>The exception that is thrown when the security policy requires code to be type safe and the verification process is unable to verify that the code is type safe.</summary>
	// Token: 0x020003C4 RID: 964
	[Serializable]
	public class VerificationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with default properties.</summary>
		// Token: 0x06002848 RID: 10312 RVA: 0x000929F0 File Offset: 0x00090BF0
		public VerificationException() : base("Operation could destabilize the runtime.")
		{
			base.HResult = -2146233075;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with an explanatory message.</summary>
		/// <param name="message">A message indicating the reason the exception occurred.</param>
		// Token: 0x06002849 RID: 10313 RVA: 0x00092A08 File Offset: 0x00090C08
		public VerificationException(string message) : base(message)
		{
			base.HResult = -2146233075;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600284A RID: 10314 RVA: 0x00092A1C File Offset: 0x00090C1C
		public VerificationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233075;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.VerificationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x0600284B RID: 10315 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected VerificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
