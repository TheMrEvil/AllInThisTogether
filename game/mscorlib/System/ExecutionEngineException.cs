using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when there is an internal error in the execution engine of the common language runtime. This class cannot be inherited.</summary>
	// Token: 0x02000118 RID: 280
	[Obsolete("This type previously indicated an unspecified fatal error in the runtime. The runtime no longer raises this exception so this type is obsolete.")]
	[Serializable]
	public sealed class ExecutionEngineException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class.</summary>
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00028819 File Offset: 0x00026A19
		public ExecutionEngineException() : base("Internal error in the runtime.")
		{
			base.HResult = -2146233082;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class with a specified error message.</summary>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000ADA RID: 2778 RVA: 0x00028831 File Offset: 0x00026A31
		public ExecutionEngineException(string message) : base(message)
		{
			base.HResult = -2146233082;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ExecutionEngineException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000ADB RID: 2779 RVA: 0x00028845 File Offset: 0x00026A45
		public ExecutionEngineException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233082;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00020A65 File Offset: 0x0001EC65
		internal ExecutionEngineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
