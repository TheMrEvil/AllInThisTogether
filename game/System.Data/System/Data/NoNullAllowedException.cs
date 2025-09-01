using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to insert a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is set to <see langword="false" />.</summary>
	// Token: 0x0200008F RID: 143
	[Serializable]
	public class NoNullAllowedException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006F0 RID: 1776 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected NoNullAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class.</summary>
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001A74F File Offset: 0x0001894F
		public NoNullAllowedException() : base("Null not allowed.")
		{
			base.HResult = -2146232026;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001A767 File Offset: 0x00018967
		public NoNullAllowedException(string s) : base(s)
		{
			base.HResult = -2146232026;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.NoNullAllowedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001A77B File Offset: 0x0001897B
		public NoNullAllowedException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232026;
		}
	}
}
