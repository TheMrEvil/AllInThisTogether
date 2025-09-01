using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when you set a value into a <see cref="N:System.Data.SqlTypes" /> structure would truncate that value.</summary>
	// Token: 0x0200031C RID: 796
	[Serializable]
	public sealed class SqlTruncateException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class.</summary>
		// Token: 0x060025E3 RID: 9699 RVA: 0x000A9601 File Offset: 0x000A7801
		public SqlTruncateException() : this(SQLResource.TruncationMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060025E4 RID: 9700 RVA: 0x000A960F File Offset: 0x000A780F
		public SqlTruncateException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTruncateException" /> class with a specified error message and a reference to the <see cref="T:System.Exception" />.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="e">A reference to an inner <see cref="T:System.Exception" />.</param>
		// Token: 0x060025E5 RID: 9701 RVA: 0x000A9619 File Offset: 0x000A7819
		public SqlTruncateException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232014;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000A962E File Offset: 0x000A782E
		private SqlTruncateException(SerializationInfo si, StreamingContext sc) : base(SqlTruncateException.SqlTruncateExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000A963E File Offset: 0x000A783E
		private static SerializationInfo SqlTruncateExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlTruncateException(si.GetString("SqlTruncateExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
