using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	/// <summary>The exception thrown when an invalid COM object is used.</summary>
	// Token: 0x020006DA RID: 1754
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		/// <summary>Initializes an instance of the <see langword="InvalidComObjectException" /> with default properties.</summary>
		// Token: 0x06004039 RID: 16441 RVA: 0x000E0CC0 File Offset: 0x000DEEC0
		public InvalidComObjectException() : base("Attempt has been made to use a COM object that does not have a backing class factory.")
		{
			base.HResult = -2146233049;
		}

		/// <summary>Initializes an instance of the <see langword="InvalidComObjectException" /> with a message.</summary>
		/// <param name="message">The message that indicates the reason for the exception.</param>
		// Token: 0x0600403A RID: 16442 RVA: 0x000E0CD8 File Offset: 0x000DEED8
		public InvalidComObjectException(string message) : base(message)
		{
			base.HResult = -2146233049;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.InvalidComObjectException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x0600403B RID: 16443 RVA: 0x000E0CEC File Offset: 0x000DEEEC
		public InvalidComObjectException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233049;
		}

		/// <summary>Initializes a new instance of the <see langword="COMException" /> class from serialization data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x0600403C RID: 16444 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
