using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an attempt to access an unloaded class.</summary>
	// Token: 0x020001A5 RID: 421
	[Serializable]
	public class TypeUnloadedException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class.</summary>
		// Token: 0x060011F0 RID: 4592 RVA: 0x00047D33 File Offset: 0x00045F33
		public TypeUnloadedException() : base("Type had been unloaded.")
		{
			base.HResult = -2146234349;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x060011F1 RID: 4593 RVA: 0x00047D4B File Offset: 0x00045F4B
		public TypeUnloadedException(string message) : base(message)
		{
			base.HResult = -2146234349;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060011F2 RID: 4594 RVA: 0x00047D5F File Offset: 0x00045F5F
		public TypeUnloadedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146234349;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TypeUnloadedException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060011F3 RID: 4595 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected TypeUnloadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
