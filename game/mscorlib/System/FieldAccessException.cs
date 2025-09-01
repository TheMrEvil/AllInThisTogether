using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a private or protected field inside a class.</summary>
	// Token: 0x02000119 RID: 281
	[Serializable]
	public class FieldAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class.</summary>
		// Token: 0x06000ADD RID: 2781 RVA: 0x0002885A File Offset: 0x00026A5A
		public FieldAccessException() : base("Attempted to access a field that is not accessible by the caller.")
		{
			base.HResult = -2146233081;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06000ADE RID: 2782 RVA: 0x00028872 File Offset: 0x00026A72
		public FieldAccessException(string message) : base(message)
		{
			base.HResult = -2146233081;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000ADF RID: 2783 RVA: 0x00028886 File Offset: 0x00026A86
		public FieldAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233081;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.FieldAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002889B File Offset: 0x00026A9B
		protected FieldAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
