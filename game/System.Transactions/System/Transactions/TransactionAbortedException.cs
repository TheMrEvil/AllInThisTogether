using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when an operation is attempted on a transaction that has already been rolled back, or an attempt is made to commit the transaction and the transaction aborts.</summary>
	// Token: 0x0200001F RID: 31
	[Serializable]
	public class TransactionAbortedException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class.</summary>
		// Token: 0x0600008A RID: 138 RVA: 0x00002B8A File Offset: 0x00000D8A
		public TransactionAbortedException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x0600008B RID: 139 RVA: 0x00002B92 File Offset: 0x00000D92
		public TransactionAbortedException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x0600008C RID: 140 RVA: 0x00002B9B File Offset: 0x00000D9B
		public TransactionAbortedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionAbortedException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x0600008D RID: 141 RVA: 0x00002BA5 File Offset: 0x00000DA5
		protected TransactionAbortedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
