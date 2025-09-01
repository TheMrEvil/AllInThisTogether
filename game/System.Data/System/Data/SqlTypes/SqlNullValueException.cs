using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The exception that is thrown when the <see langword="Value" /> property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</summary>
	// Token: 0x0200031B RID: 795
	[Serializable]
	public sealed class SqlNullValueException : SqlTypeException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x060025DE RID: 9694 RVA: 0x000A959E File Offset: 0x000A779E
		public SqlNullValueException() : this(SQLResource.NullValueMessage, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a specified message that describes the error.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		// Token: 0x060025DF RID: 9695 RVA: 0x000A95AC File Offset: 0x000A77AC
		public SqlNullValueException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="e">The exception that is the cause of the current exception. If the innerException parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060025E0 RID: 9696 RVA: 0x000A95B6 File Offset: 0x000A77B6
		public SqlNullValueException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232015;
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000A95CB File Offset: 0x000A77CB
		private SqlNullValueException(SerializationInfo si, StreamingContext sc) : base(SqlNullValueException.SqlNullValueExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000A95DB File Offset: 0x000A77DB
		private static SerializationInfo SqlNullValueExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlNullValueException(si.GetString("SqlNullValueExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
