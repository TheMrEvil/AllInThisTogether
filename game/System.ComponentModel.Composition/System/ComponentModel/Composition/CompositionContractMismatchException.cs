using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.ComponentModel.Composition
{
	/// <summary>The exception that is thrown when the underlying exported value or metadata of a <see cref="T:System.Lazy`1" /> or <see cref="T:System.Lazy`2" /> object cannot be cast to T or TMetadataView, respectively.</summary>
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class CompositionContractMismatchException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionContractMismatchException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000131 RID: 305 RVA: 0x000043C1 File Offset: 0x000025C1
		public CompositionContractMismatchException() : this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionContractMismatchException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x06000132 RID: 306 RVA: 0x000043CB File Offset: 0x000025CB
		public CompositionContractMismatchException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionContractMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000133 RID: 307 RVA: 0x000043D5 File Offset: 0x000025D5
		public CompositionContractMismatchException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.CompositionContractMismatchException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000134 RID: 308 RVA: 0x000020DC File Offset: 0x000002DC
		[SecuritySafeCritical]
		protected CompositionContractMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
