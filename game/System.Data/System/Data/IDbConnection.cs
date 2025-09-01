using System;

namespace System.Data
{
	/// <summary>Represents an open connection to a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	// Token: 0x02000104 RID: 260
	public interface IDbConnection : IDisposable
	{
		/// <summary>Gets or sets the string used to open a database.</summary>
		/// <returns>A string containing connection settings.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000F59 RID: 3929
		// (set) Token: 0x06000F5A RID: 3930
		string ConnectionString { get; set; }

		/// <summary>Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for a connection to open. The default value is 15 seconds.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000F5B RID: 3931
		int ConnectionTimeout { get; }

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used once a connection is open. The default value is an empty string.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000F5C RID: 3932
		string Database { get; }

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000F5D RID: 3933
		ConnectionState State { get; }

		/// <summary>Begins a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		// Token: 0x06000F5E RID: 3934
		IDbTransaction BeginTransaction();

		/// <summary>Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <param name="il">One of the <see cref="T:System.Data.IsolationLevel" /> values.</param>
		/// <returns>An object representing the new transaction.</returns>
		// Token: 0x06000F5F RID: 3935
		IDbTransaction BeginTransaction(IsolationLevel il);

		/// <summary>Closes the connection to the database.</summary>
		// Token: 0x06000F60 RID: 3936
		void Close();

		/// <summary>Changes the current database for an open <see langword="Connection" /> object.</summary>
		/// <param name="databaseName">The name of the database to use in place of the current database.</param>
		// Token: 0x06000F61 RID: 3937
		void ChangeDatabase(string databaseName);

		/// <summary>Creates and returns a Command object associated with the connection.</summary>
		/// <returns>A Command object associated with the connection.</returns>
		// Token: 0x06000F62 RID: 3938
		IDbCommand CreateCommand();

		/// <summary>Opens a database connection with the settings specified by the <see langword="ConnectionString" /> property of the provider-specific Connection object.</summary>
		// Token: 0x06000F63 RID: 3939
		void Open();
	}
}
