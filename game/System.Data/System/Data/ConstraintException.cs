using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when attempting an action that violates a constraint.</summary>
	// Token: 0x02000089 RID: 137
	[Serializable]
	public class ConstraintException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified serialization and stream context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006D8 RID: 1752 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected ConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class. This is the default constructor.</summary>
		// Token: 0x060006D9 RID: 1753 RVA: 0x0001A5C9 File Offset: 0x000187C9
		public ConstraintException() : base("Constraint Exception.")
		{
			base.HResult = -2146232022;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006DA RID: 1754 RVA: 0x0001A5E1 File Offset: 0x000187E1
		public ConstraintException(string s) : base(s)
		{
			base.HResult = -2146232022;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ConstraintException" /> class using the specified string and inner exception.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="innerException">Gets the <see langword="Exception" /> instance that caused the current exception.</param>
		// Token: 0x060006DB RID: 1755 RVA: 0x0001A5F5 File Offset: 0x000187F5
		public ConstraintException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232022;
		}
	}
}
