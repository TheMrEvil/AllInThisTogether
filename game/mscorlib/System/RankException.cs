using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an array with the wrong number of dimensions is passed to a method.</summary>
	// Token: 0x02000174 RID: 372
	[Serializable]
	public class RankException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class.</summary>
		// Token: 0x06000EAE RID: 3758 RVA: 0x0003C131 File Offset: 0x0003A331
		public RankException() : base("Attempted to operate on an array with the incorrect number of dimensions.")
		{
			base.HResult = -2146233065;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error.</param>
		// Token: 0x06000EAF RID: 3759 RVA: 0x0003C149 File Offset: 0x0003A349
		public RankException(string message) : base(message)
		{
			base.HResult = -2146233065;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not a null reference (<see langword="Nothing" /> in Visual Basic), the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003C15D File Offset: 0x0003A35D
		public RankException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233065;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.RankException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00020A65 File Offset: 0x0001EC65
		protected RankException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
