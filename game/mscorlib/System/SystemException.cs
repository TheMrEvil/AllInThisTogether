using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>Serves as the base class for system exceptions namespace.</summary>
	// Token: 0x0200018E RID: 398
	[Serializable]
	public class SystemException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class.</summary>
		// Token: 0x06000FC8 RID: 4040 RVA: 0x000419FA File Offset: 0x0003FBFA
		public SystemException() : base("System error.")
		{
			base.HResult = -2146233087;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000FC9 RID: 4041 RVA: 0x00041A12 File Offset: 0x0003FC12
		public SystemException(string message) : base(message)
		{
			base.HResult = -2146233087;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000FCA RID: 4042 RVA: 0x00041A26 File Offset: 0x0003FC26
		public SystemException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233087;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.SystemException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000FCB RID: 4043 RVA: 0x00020FA7 File Offset: 0x0001F1A7
		protected SystemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
