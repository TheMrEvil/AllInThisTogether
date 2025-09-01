using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when you attempt to do work on a transaction that cannot accept new work.</summary>
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class TransactionException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class.</summary>
		// Token: 0x06000091 RID: 145 RVA: 0x00002BCE File Offset: 0x00000DCE
		public TransactionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x06000092 RID: 146 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public TransactionException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x06000093 RID: 147 RVA: 0x00002BDF File Offset: 0x00000DDF
		public TransactionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x06000094 RID: 148 RVA: 0x00002BE9 File Offset: 0x00000DE9
		protected TransactionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
