using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>The exception that is thrown when a promotion fails.</summary>
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class TransactionPromotionException : TransactionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class.</summary>
		// Token: 0x060000C0 RID: 192 RVA: 0x00002B8A File Offset: 0x00000D8A
		public TransactionPromotionException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified message.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		// Token: 0x060000C1 RID: 193 RVA: 0x00002B92 File Offset: 0x00000D92
		public TransactionPromotionException(string message) : base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified message and inner exception.</summary>
		/// <param name="message">A <see cref="T:System.String" /> that contains a message that explains why the exception occurred.</param>
		/// <param name="innerException">Gets the exception instance that causes the current exception. For more information, see the <see cref="P:System.Exception.InnerException" /> property.</param>
		// Token: 0x060000C2 RID: 194 RVA: 0x00002B9B File Offset: 0x00000D9B
		public TransactionPromotionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionPromotionException" /> class with the specified serialization and streaming context information.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that describes a failed serialization context.</param>
		// Token: 0x060000C3 RID: 195 RVA: 0x00002BA5 File Offset: 0x00000DA5
		protected TransactionPromotionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
