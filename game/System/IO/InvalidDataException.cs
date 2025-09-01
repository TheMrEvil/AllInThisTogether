using System;
using System.Runtime.Serialization;

namespace System.IO
{
	/// <summary>The exception that is thrown when a data stream is in an invalid format.</summary>
	// Token: 0x02000519 RID: 1305
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class.</summary>
		// Token: 0x06002A50 RID: 10832 RVA: 0x000918E2 File Offset: 0x0008FAE2
		public InvalidDataException() : base(Locale.GetText("Invalid data format."))
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x06002A51 RID: 10833 RVA: 0x000918FF File Offset: 0x0008FAFF
		public InvalidDataException(string message) : base(message)
		{
			base.HResult = -2146233085;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.InvalidDataException" /> class with a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06002A52 RID: 10834 RVA: 0x00091913 File Offset: 0x0008FB13
		public InvalidDataException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233085;
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x0005573F File Offset: 0x0005393F
		private InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400167A RID: 5754
		private const int Result = -2146233085;
	}
}
