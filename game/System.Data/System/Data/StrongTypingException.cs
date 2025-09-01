using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>The exception that is thrown by a strongly typed <see cref="T:System.Data.DataSet" /> when the user accesses a <see langword="DBNull" /> value.</summary>
	// Token: 0x02000136 RID: 310
	[Serializable]
	public class StrongTypingException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure.</param>
		// Token: 0x060010AF RID: 4271 RVA: 0x0001A5BF File Offset: 0x000187BF
		protected StrongTypingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class.</summary>
		// Token: 0x060010B0 RID: 4272 RVA: 0x00045C17 File Offset: 0x00043E17
		public StrongTypingException()
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class with the specified string.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x060010B1 RID: 4273 RVA: 0x00045C2A File Offset: 0x00043E2A
		public StrongTypingException(string message) : base(message)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StrongTypingException" /> class with the specified string and inner exception.</summary>
		/// <param name="s">The string to display when the exception is thrown.</param>
		/// <param name="innerException">A reference to an inner exception.</param>
		// Token: 0x060010B2 RID: 4274 RVA: 0x00045C3E File Offset: 0x00043E3E
		public StrongTypingException(string s, Exception innerException) : base(s, innerException)
		{
			base.HResult = -2146232021;
		}
	}
}
