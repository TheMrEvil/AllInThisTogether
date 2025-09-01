using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to change the value of a read-only column.</summary>
	// Token: 0x02000090 RID: 144
	[Serializable]
	public class ReadOnlyException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006F4 RID: 1780 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected ReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class.</summary>
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001A790 File Offset: 0x00018990
		public ReadOnlyException() : base("Column is marked read only.")
		{
			base.HResult = -2146232025;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006F6 RID: 1782 RVA: 0x0001A7A8 File Offset: 0x000189A8
		public ReadOnlyException(string s) : base(s)
		{
			base.HResult = -2146232025;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ReadOnlyException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060006F7 RID: 1783 RVA: 0x0001A7BC File Offset: 0x000189BC
		public ReadOnlyException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232025;
		}
	}
}
