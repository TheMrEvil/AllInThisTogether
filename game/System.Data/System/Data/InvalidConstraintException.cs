using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when incorrectly trying to create or access a relation.</summary>
	// Token: 0x0200008D RID: 141
	[Serializable]
	public class InvalidConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006E8 RID: 1768 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected InvalidConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class.</summary>
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001A6CD File Offset: 0x000188CD
		public InvalidConstraintException() : base("Invalid constraint.")
		{
			base.HResult = -2146232028;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006EA RID: 1770 RVA: 0x0001A6E5 File Offset: 0x000188E5
		public InvalidConstraintException(string s) : base(s)
		{
			base.HResult = -2146232028;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InvalidConstraintException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060006EB RID: 1771 RVA: 0x0001A6F9 File Offset: 0x000188F9
		public InvalidConstraintException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232028;
		}
	}
}
