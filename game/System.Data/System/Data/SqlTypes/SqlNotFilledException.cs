using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200031D RID: 797
	[Serializable]
	public sealed class SqlNotFilledException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		// Token: 0x060025E8 RID: 9704 RVA: 0x000A9664 File Offset: 0x000A7864
		public SqlNotFilledException() : this(SQLResource.NotFilledMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x060025E9 RID: 9705 RVA: 0x000A9672 File Offset: 0x000A7872
		public SqlNotFilledException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNotFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="e">A reference to an inner exception.</param>
		// Token: 0x060025EA RID: 9706 RVA: 0x000A95B6 File Offset: 0x000A77B6
		public SqlNotFilledException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000A967C File Offset: 0x000A787C
		private SqlNotFilledException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}
	}
}
