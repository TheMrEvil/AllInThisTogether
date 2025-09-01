using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200031E RID: 798
	[Serializable]
	public sealed class SqlAlreadyFilledException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		// Token: 0x060025EC RID: 9708 RVA: 0x000A9686 File Offset: 0x000A7886
		public SqlAlreadyFilledException() : this(SQLResource.AlreadyFilledMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x060025ED RID: 9709 RVA: 0x000A9694 File Offset: 0x000A7894
		public SqlAlreadyFilledException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlAlreadyFilledException" /> class.</summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="e">A reference to an inner exception.</param>
		// Token: 0x060025EE RID: 9710 RVA: 0x000A95B6 File Offset: 0x000A77B6
		public SqlAlreadyFilledException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000A967C File Offset: 0x000A787C
		private SqlAlreadyFilledException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
		}
	}
}
