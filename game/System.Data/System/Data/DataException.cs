using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when errors are generated using ADO.NET components.</summary>
	// Token: 0x02000088 RID: 136
	[Serializable]
	public class DataException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified serialization information and context.</summary>
		/// <param name="info">The data necessary to serialize or deserialize an object.</param>
		/// <param name="context">Description of the source and destination of the specified serialized stream.</param>
		// Token: 0x060006D4 RID: 1748 RVA: 0x00010C10 File Offset: 0x0000EE10
		protected DataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class. This is the default constructor.</summary>
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001A589 File Offset: 0x00018789
		public DataException() : base("Data Exception.")
		{
			base.HResult = -2146232032;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		// Token: 0x060006D6 RID: 1750 RVA: 0x0001A5A1 File Offset: 0x000187A1
		public DataException(string s) : base(s)
		{
			base.HResult = -2146232032;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataException" /> class with the specified string and inner exception.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		/// <param name="innerException">A reference to an inner exception.</param>
		// Token: 0x060006D7 RID: 1751 RVA: 0x0001A5B5 File Offset: 0x000187B5
		public DataException(string s, Exception innerException) : base(s, innerException)
		{
		}
	}
}
