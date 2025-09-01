using System;

namespace System.Runtime.Serialization
{
	/// <summary>The exception thrown when an error occurs during serialization or deserialization.</summary>
	// Token: 0x0200064B RID: 1611
	[Serializable]
	public class SerializationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with default properties.</summary>
		// Token: 0x06003C4F RID: 15439 RVA: 0x000D1463 File Offset: 0x000CF663
		public SerializationException() : base(SerializationException.s_nullMessage)
		{
			base.HResult = -2146233076;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with a specified message.</summary>
		/// <param name="message">Indicates the reason why the exception occurred.</param>
		// Token: 0x06003C50 RID: 15440 RVA: 0x000D147B File Offset: 0x000CF67B
		public SerializationException(string message) : base(message)
		{
			base.HResult = -2146233076;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06003C51 RID: 15441 RVA: 0x000D148F File Offset: 0x000CF68F
		public SerializationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233076;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationException" /> class from serialized data.</summary>
		/// <param name="info">The serialization information object holding the serialized object data in the name-value form.</param>
		/// <param name="context">The contextual information about the source or destination of the exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C52 RID: 15442 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected SerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x000D14A4 File Offset: 0x000CF6A4
		// Note: this type is marked as 'beforefieldinit'.
		static SerializationException()
		{
		}

		// Token: 0x04002715 RID: 10005
		private static string s_nullMessage = "Serialization error.";
	}
}
