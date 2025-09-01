using System;

namespace System.Data
{
	/// <summary>Represents a transaction to be performed at a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	// Token: 0x02000107 RID: 263
	public interface IDbTransaction : IDisposable
	{
		/// <summary>Specifies the Connection object to associate with the transaction.</summary>
		/// <returns>The Connection object to associate with the transaction.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000F72 RID: 3954
		IDbConnection Connection { get; }

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is <see langword="ReadCommitted" />.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000F73 RID: 3955
		IsolationLevel IsolationLevel { get; }

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction.</exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.  
		///  -or-  
		///  The connection is broken.</exception>
		// Token: 0x06000F74 RID: 3956
		void Commit();

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction.</exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.  
		///  -or-  
		///  The connection is broken.</exception>
		// Token: 0x06000F75 RID: 3957
		void Rollback();
	}
}
