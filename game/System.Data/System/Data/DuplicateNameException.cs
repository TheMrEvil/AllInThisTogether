using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when a duplicate database object name is encountered during an add operation in a <see cref="T:System.Data.DataSet" /> -related object.</summary>
	// Token: 0x0200008B RID: 139
	[Serializable]
	public class DuplicateNameException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected DuplicateNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class.</summary>
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001A64B File Offset: 0x0001884B
		public DuplicateNameException() : base("Duplicate name not allowed.")
		{
			base.HResult = -2146232030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006E2 RID: 1762 RVA: 0x0001A663 File Offset: 0x00018863
		public DuplicateNameException(string s) : base(s)
		{
			base.HResult = -2146232030;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DuplicateNameException" /> class with the specified string and exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001A677 File Offset: 0x00018877
		public DuplicateNameException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232030;
		}
	}
}
