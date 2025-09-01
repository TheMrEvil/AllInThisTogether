using System;

namespace System.Transactions
{
	/// <summary>Contains additional information that specifies transaction behaviors.</summary>
	// Token: 0x02000027 RID: 39
	public struct TransactionOptions
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00002DD7 File Offset: 0x00000FD7
		internal TransactionOptions(IsolationLevel level, TimeSpan timeout)
		{
			this.level = level;
			this.timeout = timeout;
		}

		/// <summary>Gets or sets the isolation level of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.IsolationLevel" /> enumeration that specifies the isolation level of the transaction.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002DE7 File Offset: 0x00000FE7
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00002DEF File Offset: 0x00000FEF
		public IsolationLevel IsolationLevel
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		/// <summary>Gets or sets the timeout period for the transaction.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the timeout period for the transaction.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002DF8 File Offset: 0x00000FF8
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00002E00 File Offset: 0x00001000
		public TimeSpan Timeout
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

		/// <summary>Tests whether two specified <see cref="T:System.Transactions.TransactionOptions" /> instances are equivalent.</summary>
		/// <param name="x">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000BC RID: 188 RVA: 0x00002E09 File Offset: 0x00001009
		public static bool operator ==(TransactionOptions x, TransactionOptions y)
		{
			return x.level == y.level && x.timeout == y.timeout;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Transactions.TransactionOptions" /> instances are not equal.</summary>
		/// <param name="x">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the right of the equality operator.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000BD RID: 189 RVA: 0x00002E2C File Offset: 0x0000102C
		public static bool operator !=(TransactionOptions x, TransactionOptions y)
		{
			return x.level != y.level || x.timeout != y.timeout;
		}

		/// <summary>Determines whether this <see cref="T:System.Transactions.TransactionOptions" /> instance and the specified object are equal.</summary>
		/// <param name="obj">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> and this <see cref="T:System.Transactions.TransactionOptions" /> instance are identical; otherwise, <see langword="false" />.</returns>
		// Token: 0x060000BE RID: 190 RVA: 0x00002E4F File Offset: 0x0000104F
		public override bool Equals(object obj)
		{
			return obj is TransactionOptions && this == (TransactionOptions)obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060000BF RID: 191 RVA: 0x00002E6C File Offset: 0x0000106C
		public override int GetHashCode()
		{
			return (int)(this.level ^ (IsolationLevel)this.timeout.GetHashCode());
		}

		// Token: 0x04000065 RID: 101
		private IsolationLevel level;

		// Token: 0x04000066 RID: 102
		private TimeSpan timeout;
	}
}
