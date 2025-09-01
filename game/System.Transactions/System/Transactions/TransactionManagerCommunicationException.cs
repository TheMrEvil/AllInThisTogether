using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when a resource manager cannot communicate with the transaction manager.</summary>
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class TransactionManagerCommunicationException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionManagerCommunicationException" /> class.</summary>
		// Token: 0x060000B3 RID: 179 RVA: 0x00002B8A File Offset: 0x00000D8A
		public TransactionManagerCommunicationException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionManagerCommunicationException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x060000B4 RID: 180 RVA: 0x00002B92 File Offset: 0x00000D92
		public TransactionManagerCommunicationException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionManagerCommunicationException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x060000B5 RID: 181 RVA: 0x00002B9B File Offset: 0x00000D9B
		public TransactionManagerCommunicationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionManagerCommunicationException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x060000B6 RID: 182 RVA: 0x00002BA5 File Offset: 0x00000DA5
		protected TransactionManagerCommunicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
