using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown for invalid casting or explicit conversion.</summary>
	// Token: 0x0200014B RID: 331
	[Serializable]
	public class InvalidCastException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class.</summary>
		// Token: 0x06000C89 RID: 3209 RVA: 0x000327CF File Offset: 0x000309CF
		public InvalidCastException() : base("Specified cast is not valid.")
		{
			base.HResult = -2147467262;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000C8A RID: 3210 RVA: 0x000327E7 File Offset: 0x000309E7
		public InvalidCastException(string message) : base(message)
		{
			base.HResult = -2147467262;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000C8B RID: 3211 RVA: 0x000327FB File Offset: 0x000309FB
		public InvalidCastException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467262;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with a specified message and error code.</summary>
		/// <param name="message">The message that indicates the reason the exception occurred.</param>
		/// <param name="errorCode">The error code (HRESULT) value associated with the exception.</param>
		// Token: 0x06000C8C RID: 3212 RVA: 0x00032810 File Offset: 0x00030A10
		public InvalidCastException(string message, int errorCode) : base(message)
		{
			base.HResult = errorCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.InvalidCastException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000C8D RID: 3213 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected InvalidCastException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
