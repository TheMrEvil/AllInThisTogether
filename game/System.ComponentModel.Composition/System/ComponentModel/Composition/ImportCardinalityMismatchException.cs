using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;

namespace System.ComponentModel.Composition
{
	/// <summary>The exception that is thrown when the cardinality of an import is not compatible with the cardinality of the matching exports.</summary>
	// Token: 0x02000045 RID: 69
	[DebuggerTypeProxy(typeof(ImportCardinalityMismatchExceptionDebuggerProxy))]
	[DebuggerDisplay("{Message}")]
	[Serializable]
	public class ImportCardinalityMismatchException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060001F4 RID: 500 RVA: 0x00005E2C File Offset: 0x0000402C
		public ImportCardinalityMismatchException() : this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">A message that describes the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" />, or <see langword="null" /> to set the <see cref="P:System.Exception.Message" /> property to its default value.</param>
		// Token: 0x060001F5 RID: 501 RVA: 0x00005E36 File Offset: 0x00004036
		public ImportCardinalityMismatchException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060001F6 RID: 502 RVA: 0x000043D5 File Offset: 0x000025D5
		public ImportCardinalityMismatchException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" /> class with serialized data.</summary>
		/// <param name="info">An object that holds the serialized object data about the <see cref="T:System.ComponentModel.Composition.ImportCardinalityMismatchException" />.</param>
		/// <param name="context">An object that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		///   <paramref name="info" /> is missing a required value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="info" /> contains a value that cannot be cast to the correct type.</exception>
		// Token: 0x060001F7 RID: 503 RVA: 0x000020DC File Offset: 0x000002DC
		[SecuritySafeCritical]
		protected ImportCardinalityMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
