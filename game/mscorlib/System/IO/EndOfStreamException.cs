using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when reading is attempted past the end of a stream.</summary>
	// Token: 0x02000B03 RID: 2819
	[Serializable]
	public class EndOfStreamException : IOException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with its message string set to a system-supplied message and its HRESULT set to COR_E_ENDOFSTREAM.</summary>
		// Token: 0x060064BC RID: 25788 RVA: 0x0015671A File Offset: 0x0015491A
		public EndOfStreamException() : base("Attempted to read past the end of the stream.")
		{
			base.HResult = -2147024858;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with its message string set to <paramref name="message" /> and its HRESULT set to COR_E_ENDOFSTREAM.</summary>
		/// <param name="message">A string that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060064BD RID: 25789 RVA: 0x00156732 File Offset: 0x00154932
		public EndOfStreamException(string message) : base(message)
		{
			base.HResult = -2147024858;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the error. The content of <paramref name="message" /> is intended to be understood by humans. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060064BE RID: 25790 RVA: 0x00156746 File Offset: 0x00154946
		public EndOfStreamException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147024858;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.EndOfStreamException" /> class with the specified serialization and context information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
		// Token: 0x060064BF RID: 25791 RVA: 0x00156710 File Offset: 0x00154910
		protected EndOfStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
