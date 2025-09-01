using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> and <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> events.</summary>
	// Token: 0x02000449 RID: 1097
	public class DesignerTransactionCloseEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class, using the specified value that indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		// Token: 0x060023B9 RID: 9145 RVA: 0x000812A3 File Offset: 0x0007F4A3
		[Obsolete("This constructor is obsolete. Use DesignerTransactionCloseEventArgs(bool, bool) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public DesignerTransactionCloseEventArgs(bool commit) : this(commit, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerTransactionCloseEventArgs" /> class.</summary>
		/// <param name="commit">A value indicating whether the transaction was committed.</param>
		/// <param name="lastTransaction">
		///   <see langword="true" /> if this is the last transaction to close; otherwise, <see langword="false" />.</param>
		// Token: 0x060023BA RID: 9146 RVA: 0x000812AD File Offset: 0x0007F4AD
		public DesignerTransactionCloseEventArgs(bool commit, bool lastTransaction)
		{
			this.TransactionCommitted = commit;
			this.LastTransaction = lastTransaction;
		}

		/// <summary>Indicates whether the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer called <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on the transaction; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000812C3 File Offset: 0x0007F4C3
		public bool TransactionCommitted
		{
			[CompilerGenerated]
			get
			{
				return this.<TransactionCommitted>k__BackingField;
			}
		}

		/// <summary>Gets a value indicating whether this is the last transaction to close.</summary>
		/// <returns>
		///   <see langword="true" />, if this is the last transaction to close; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x000812CB File Offset: 0x0007F4CB
		public bool LastTransaction
		{
			[CompilerGenerated]
			get
			{
				return this.<LastTransaction>k__BackingField;
			}
		}

		// Token: 0x040010BE RID: 4286
		[CompilerGenerated]
		private readonly bool <TransactionCommitted>k__BackingField;

		// Token: 0x040010BF RID: 4287
		[CompilerGenerated]
		private readonly bool <LastTransaction>k__BackingField;
	}
}
