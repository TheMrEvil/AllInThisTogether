using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to access a row in a table that has no primary key.</summary>
	// Token: 0x0200008E RID: 142
	[Serializable]
	public class MissingPrimaryKeyException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">A description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected MissingPrimaryKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class.</summary>
		// Token: 0x060006ED RID: 1773 RVA: 0x0001A70E File Offset: 0x0001890E
		public MissingPrimaryKeyException() : base("Missing primary key.")
		{
			base.HResult = -2146232027;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006EE RID: 1774 RVA: 0x0001A726 File Offset: 0x00018926
		public MissingPrimaryKeyException(string s) : base(s)
		{
			base.HResult = -2146232027;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.MissingPrimaryKeyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060006EF RID: 1775 RVA: 0x0001A73A File Offset: 0x0001893A
		public MissingPrimaryKeyException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232027;
		}
	}
}
