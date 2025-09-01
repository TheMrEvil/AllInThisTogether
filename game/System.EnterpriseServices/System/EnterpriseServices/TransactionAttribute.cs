using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the type of transaction that is available to the attributed object. Permissible values are members of the <see cref="T:System.EnterpriseServices.TransactionOption" /> enumeration.</summary>
	// Token: 0x02000051 RID: 81
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class TransactionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.TransactionAttribute" /> class, setting the component's requested transaction type to <see cref="F:System.EnterpriseServices.TransactionOption.Required" />.</summary>
		// Token: 0x0600014E RID: 334 RVA: 0x00002664 File Offset: 0x00000864
		public TransactionAttribute() : this(TransactionOption.Required)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.TransactionAttribute" /> class, specifying the transaction type.</summary>
		/// <param name="val">The specified transaction type, a <see cref="T:System.EnterpriseServices.TransactionOption" /> value.</param>
		// Token: 0x0600014F RID: 335 RVA: 0x0000266D File Offset: 0x0000086D
		public TransactionAttribute(TransactionOption val)
		{
			this.isolation = TransactionIsolationLevel.Serializable;
			this.timeout = -1;
			this.val = val;
		}

		/// <summary>Gets or sets the transaction isolation level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionIsolationLevel" /> values.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000268A File Offset: 0x0000088A
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00002692 File Offset: 0x00000892
		public TransactionIsolationLevel Isolation
		{
			get
			{
				return this.isolation;
			}
			set
			{
				this.isolation = value;
			}
		}

		/// <summary>Gets or sets the time-out for this transaction.</summary>
		/// <returns>The transaction time-out in seconds.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000269B File Offset: 0x0000089B
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000026A3 File Offset: 0x000008A3
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.EnterpriseServices.TransactionOption" /> value for the transaction, optionally disabling the transaction service.</summary>
		/// <returns>The specified transaction type, a <see cref="T:System.EnterpriseServices.TransactionOption" /> value.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000026AC File Offset: 0x000008AC
		public TransactionOption Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000098 RID: 152
		private TransactionIsolationLevel isolation;

		// Token: 0x04000099 RID: 153
		private int timeout;

		// Token: 0x0400009A RID: 154
		private TransactionOption val;
	}
}
