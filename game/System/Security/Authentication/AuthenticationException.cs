using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	/// <summary>The exception that is thrown when authentication fails for an authentication stream.</summary>
	// Token: 0x0200029B RID: 667
	[Serializable]
	public class AuthenticationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with no message.</summary>
		// Token: 0x06001518 RID: 5400 RVA: 0x0005572D File Offset: 0x0005392D
		public AuthenticationException() : base(Locale.GetText("Authentication exception."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		// Token: 0x06001519 RID: 5401 RVA: 0x0002F15C File Offset: 0x0002D35C
		public AuthenticationException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the authentication failure.</param>
		/// <param name="innerException">The <see cref="T:System.Exception" /> that is the cause of the current exception.</param>
		// Token: 0x0600151A RID: 5402 RVA: 0x0002F191 File Offset: 0x0002D391
		public AuthenticationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.AuthenticationException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to deserialize the new <see cref="T:System.Security.Authentication.AuthenticationException" /> instance.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> instance.</param>
		// Token: 0x0600151B RID: 5403 RVA: 0x0005573F File Offset: 0x0005393F
		protected AuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
