using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Serves as the base class for application-defined exceptions.</summary>
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class ApplicationException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class.</summary>
		// Token: 0x060006FA RID: 1786 RVA: 0x00020F66 File Offset: 0x0001F166
		public ApplicationException() : base("Error in the application.")
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message.</summary>
		/// <param name="message">A message that describes the error.</param>
		// Token: 0x060006FB RID: 1787 RVA: 0x00020F7E File Offset: 0x0001F17E
		public ApplicationException(string message) : base(message)
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060006FC RID: 1788 RVA: 0x00020F92 File Offset: 0x0001F192
		public ApplicationException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232832;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ApplicationException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060006FD RID: 1789 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
