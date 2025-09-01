using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when you try to return a version of a <see cref="T:System.Data.DataRow" /> that has been deleted.</summary>
	// Token: 0x02000092 RID: 146
	[Serializable]
	public class VersionNotFoundException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with serialization information.</summary>
		/// <param name="info">The data that is required to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006FC RID: 1788 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected VersionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class.</summary>
		// Token: 0x060006FD RID: 1789 RVA: 0x0001A812 File Offset: 0x00018A12
		public VersionNotFoundException() : base("Version not found.")
		{
			base.HResult = -2146232023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006FE RID: 1790 RVA: 0x0001A82A File Offset: 0x00018A2A
		public VersionNotFoundException(string s) : base(s)
		{
			base.HResult = -2146232023;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.VersionNotFoundException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
		// Token: 0x060006FF RID: 1791 RVA: 0x0001A83E File Offset: 0x00018A3E
		public VersionNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232023;
		}
	}
}
