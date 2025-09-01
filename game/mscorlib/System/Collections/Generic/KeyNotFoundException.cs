using System;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
	/// <summary>The exception that is thrown when the key specified for accessing an element in a collection does not match any key in the collection.</summary>
	// Token: 0x02000AA0 RID: 2720
	[Serializable]
	public class KeyNotFoundException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class using default property values.</summary>
		// Token: 0x06006137 RID: 24887 RVA: 0x001451CC File Offset: 0x001433CC
		public KeyNotFoundException() : base("The given key was not present in the dictionary.")
		{
			base.HResult = -2146232969;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06006138 RID: 24888 RVA: 0x001451E4 File Offset: 0x001433E4
		public KeyNotFoundException(string message) : base(message)
		{
			base.HResult = -2146232969;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with the specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06006139 RID: 24889 RVA: 0x001451F8 File Offset: 0x001433F8
		public KeyNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232969;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.KeyNotFoundException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x0600613A RID: 24890 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected KeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
