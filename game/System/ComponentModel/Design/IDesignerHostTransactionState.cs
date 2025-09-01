using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies methods for the designer host to report on the state of transactions.</summary>
	// Token: 0x0200045D RID: 1117
	public interface IDesignerHostTransactionState
	{
		/// <summary>Gets a value indicating whether the designer host is closing a transaction.</summary>
		/// <returns>
		///   <see langword="true" /> if the designer is closing a transaction; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600243F RID: 9279
		bool IsClosingTransaction { get; }
	}
}
