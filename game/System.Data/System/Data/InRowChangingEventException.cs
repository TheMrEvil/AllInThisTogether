using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you call the <see cref="M:System.Data.DataRow.EndEdit" /> method within the <see cref="E:System.Data.DataTable.RowChanging" /> event.</summary>
	// Token: 0x0200008C RID: 140
	[Serializable]
	public class InRowChangingEventException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006E4 RID: 1764 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected InRowChangingEventException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class.</summary>
		// Token: 0x060006E5 RID: 1765 RVA: 0x0001A68C File Offset: 0x0001888C
		public InRowChangingEventException() : base("Operation not supported in the RowChanging event.")
		{
			base.HResult = -2146232029;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006E6 RID: 1766 RVA: 0x0001A6A4 File Offset: 0x000188A4
		public InRowChangingEventException(string s) : base(s)
		{
			base.HResult = -2146232029;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.InRowChangingEventException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060006E7 RID: 1767 RVA: 0x0001A6B8 File Offset: 0x000188B8
		public InRowChangingEventException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232029;
		}
	}
}
