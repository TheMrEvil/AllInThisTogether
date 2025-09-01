using System;

namespace System.Transactions
{
	/// <summary>Provides additional information regarding a transaction.</summary>
	// Token: 0x02000023 RID: 35
	public class TransactionInformation
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00002BF4 File Offset: 0x00000DF4
		internal TransactionInformation()
		{
			this.status = TransactionStatus.Active;
			this.creation_time = DateTime.Now.ToUniversalTime();
			this.local_id = Guid.NewGuid().ToString() + ":1";
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002C50 File Offset: 0x00000E50
		private TransactionInformation(TransactionInformation other)
		{
			this.local_id = other.local_id;
			this.dtcId = other.dtcId;
			this.creation_time = other.creation_time;
			this.status = other.status;
		}

		/// <summary>Gets the creation time of the transaction.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the creation time of the transaction.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002C9E File Offset: 0x00000E9E
		public DateTime CreationTime
		{
			get
			{
				return this.creation_time;
			}
		}

		/// <summary>Gets a unique identifier of the escalated transaction.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that contains the unique identifier of the escalated transaction.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002CA6 File Offset: 0x00000EA6
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002CAE File Offset: 0x00000EAE
		public Guid DistributedIdentifier
		{
			get
			{
				return this.dtcId;
			}
			internal set
			{
				this.dtcId = value;
			}
		}

		/// <summary>Gets a unique identifier of the transaction.</summary>
		/// <returns>A unique identifier of the transaction.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002CB7 File Offset: 0x00000EB7
		public string LocalIdentifier
		{
			get
			{
				return this.local_id;
			}
		}

		/// <summary>Gets the status of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionStatus" /> that contains the status of the transaction.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002CBF File Offset: 0x00000EBF
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002CC7 File Offset: 0x00000EC7
		public TransactionStatus Status
		{
			get
			{
				return this.status;
			}
			internal set
			{
				this.status = value;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002CD0 File Offset: 0x00000ED0
		internal TransactionInformation Clone(TransactionInformation other)
		{
			return new TransactionInformation(other);
		}

		// Token: 0x0400005B RID: 91
		private string local_id;

		// Token: 0x0400005C RID: 92
		private Guid dtcId = Guid.Empty;

		// Token: 0x0400005D RID: 93
		private DateTime creation_time;

		// Token: 0x0400005E RID: 94
		private TransactionStatus status;
	}
}
