using System;
using System.Runtime.Serialization;

namespace System
{
	/// <summary>The exception that is thrown when an object appears more than once in an array of synchronization objects.</summary>
	// Token: 0x02000113 RID: 275
	[Serializable]
	public class DuplicateWaitObjectException : ArgumentException
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x00028753 File Offset: 0x00026953
		private static string DuplicateWaitObjectMessage
		{
			get
			{
				if (DuplicateWaitObjectException.s_duplicateWaitObjectMessage == null)
				{
					DuplicateWaitObjectException.s_duplicateWaitObjectMessage = "Duplicate objects in argument.";
				}
				return DuplicateWaitObjectException.s_duplicateWaitObjectMessage;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class.</summary>
		// Token: 0x06000AC6 RID: 2758 RVA: 0x00028771 File Offset: 0x00026971
		public DuplicateWaitObjectException() : base(DuplicateWaitObjectException.DuplicateWaitObjectMessage)
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with the name of the parameter that causes this exception.</summary>
		/// <param name="parameterName">The name of the parameter that caused the exception.</param>
		// Token: 0x06000AC7 RID: 2759 RVA: 0x00028789 File Offset: 0x00026989
		public DuplicateWaitObjectException(string parameterName) : base(DuplicateWaitObjectException.DuplicateWaitObjectMessage, parameterName)
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with a specified error message and the name of the parameter that causes this exception.</summary>
		/// <param name="parameterName">The name of the parameter that caused the exception.</param>
		/// <param name="message">The message that describes the error.</param>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x000287A2 File Offset: 0x000269A2
		public DuplicateWaitObjectException(string parameterName, string message) : base(message, parameterName)
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x000287B7 File Offset: 0x000269B7
		public DuplicateWaitObjectException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233047;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DuplicateWaitObjectException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x06000ACA RID: 2762 RVA: 0x0002110F File Offset: 0x0001F30F
		protected DuplicateWaitObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x040010E1 RID: 4321
		private static volatile string s_duplicateWaitObjectMessage;
	}
}
