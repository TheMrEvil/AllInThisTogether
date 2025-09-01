using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	/// <summary>The exception that is thrown when authentication fails for an authentication stream and cannot be retried.</summary>
	// Token: 0x0200029F RID: 671
	[Serializable]
	public class InvalidCredentialException : AuthenticationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with no message.</summary>
		// Token: 0x0600151C RID: 5404 RVA: 0x00055749 File Offset: 0x00053949
		public InvalidCredentialException() : base(Locale.GetText("Invalid credentials exception."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		// Token: 0x0600151D RID: 5405 RVA: 0x0005575B File Offset: 0x0005395B
		public InvalidCredentialException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception.</param>
		// Token: 0x0600151E RID: 5406 RVA: 0x00055764 File Offset: 0x00053964
		public InvalidCredentialException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.InvalidCredentialException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to deserialize the new <see cref="T:System.Security.Authentication.InvalidCredentialException" /> instance.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance.</param>
		// Token: 0x0600151F RID: 5407 RVA: 0x0005576E File Offset: 0x0005396E
		protected InvalidCredentialException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
