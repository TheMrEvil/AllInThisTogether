using System;
using System.Runtime.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>The base exception class for the <see cref="N:System.Data.SqlTypes" />.</summary>
	// Token: 0x0200031A RID: 794
	[Serializable]
	public class SqlTypeException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class.</summary>
		// Token: 0x060025D9 RID: 9689 RVA: 0x000A953B File Offset: 0x000A773B
		public SqlTypeException() : this("SqlType error.", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		// Token: 0x060025DA RID: 9690 RVA: 0x000A9549 File Offset: 0x000A7749
		public SqlTypeException(string message) : this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The message that describes the exception. The caller of this constructor is required to ensure that this string has been localized for the current system culture.</param>
		/// <param name="e">The exception that is the cause of the current exception. If the innerException parameter is not <see langword="null" />, the current exception is raised in a <see langword="catch" /> block that handles the inner exception.</param>
		// Token: 0x060025DB RID: 9691 RVA: 0x000A9553 File Offset: 0x000A7753
		public SqlTypeException(string message, Exception e) : base(message, e)
		{
			base.HResult = -2146232016;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlTypeException" /> class with serialized data.</summary>
		/// <param name="si">The object that holds the serialized object data.</param>
		/// <param name="sc">The contextual information about the source or destination.</param>
		// Token: 0x060025DC RID: 9692 RVA: 0x000A9568 File Offset: 0x000A7768
		protected SqlTypeException(SerializationInfo si, StreamingContext sc) : base(SqlTypeException.SqlTypeExceptionSerialization(si, sc), sc)
		{
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000A9578 File Offset: 0x000A7778
		private static SerializationInfo SqlTypeExceptionSerialization(SerializationInfo si, StreamingContext sc)
		{
			if (si != null && 1 == si.MemberCount)
			{
				new SqlTypeException(si.GetString("SqlTypeExceptionMessage")).GetObjectData(si, sc);
			}
			return si;
		}
	}
}
