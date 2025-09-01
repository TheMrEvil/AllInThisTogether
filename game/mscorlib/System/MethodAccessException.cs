using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an invalid attempt to access a method, such as accessing a private method from partially trusted code.</summary>
	// Token: 0x0200015A RID: 346
	[Serializable]
	public class MethodAccessException : MemberAccessException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class, setting the <see cref="P:System.Exception.Message" /> property of the new instance to a system-supplied message that describes the error, such as "Attempt to access the method failed." This message takes into account the current system culture.</summary>
		// Token: 0x06000DC4 RID: 3524 RVA: 0x00035D39 File Offset: 0x00033F39
		public MethodAccessException() : base("Attempt to access the method failed.")
		{
			base.HResult = -2146233072;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06000DC5 RID: 3525 RVA: 0x00035D51 File Offset: 0x00033F51
		public MethodAccessException(string message) : base(message)
		{
			base.HResult = -2146233072;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000DC6 RID: 3526 RVA: 0x00035D65 File Offset: 0x00033F65
		public MethodAccessException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233072;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.MethodAccessException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002889B File Offset: 0x00026A9B
		protected MethodAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
