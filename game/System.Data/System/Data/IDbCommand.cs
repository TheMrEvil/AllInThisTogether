using System;

namespace System.Data
{
	/// <summary>Represents an SQL statement that is executed while connected to a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	// Token: 0x02000103 RID: 259
	public interface IDbCommand : IDisposable
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.IDbConnection" /> used by this instance of the <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000F45 RID: 3909
		// (set) Token: 0x06000F46 RID: 3910
		IDbConnection Connection { get; set; }

		/// <summary>Gets or sets the transaction within which the <see langword="Command" /> object of a .NET Framework data provider executes.</summary>
		/// <returns>the <see langword="Command" /> object of a .NET Framework data provider executes. The default value is <see langword="null" />.</returns>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000F47 RID: 3911
		// (set) Token: 0x06000F48 RID: 3912
		IDbTransaction Transaction { get; set; }

		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000F49 RID: 3913
		// (set) Token: 0x06000F4A RID: 3914
		string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for the command to execute. The default value is 30 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The property value assigned is less than 0.</exception>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000F4B RID: 3915
		// (set) Token: 0x06000F4C RID: 3916
		int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.IDbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is <see langword="Text" />.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000F4D RID: 3917
		// (set) Token: 0x06000F4E RID: 3918
		CommandType CommandType { get; set; }

		/// <summary>Gets the <see cref="T:System.Data.IDataParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000F4F RID: 3919
		IDataParameterCollection Parameters { get; }

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not set.  
		///  -or-  
		///  The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not <see cref="M:System.Data.OleDb.OleDbConnection.Open" />.</exception>
		// Token: 0x06000F50 RID: 3920
		void Prepare();

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is <see langword="Both" /> unless the command is automatically generated. Then the default is <see langword="None" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value entered was not one of the <see cref="T:System.Data.UpdateRowSource" /> values.</exception>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000F51 RID: 3921
		// (set) Token: 0x06000F52 RID: 3922
		UpdateRowSource UpdatedRowSource { get; set; }

		/// <summary>Attempts to cancels the execution of an <see cref="T:System.Data.IDbCommand" />.</summary>
		// Token: 0x06000F53 RID: 3923
		void Cancel();

		/// <summary>Creates a new instance of an <see cref="T:System.Data.IDbDataParameter" /> object.</summary>
		/// <returns>An <see langword="IDbDataParameter" /> object.</returns>
		// Token: 0x06000F54 RID: 3924
		IDbDataParameter CreateParameter();

		/// <summary>Executes an SQL statement against the <see langword="Connection" /> object of a .NET Framework data provider, and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.  
		///  -or-  
		///  The connection is not open.</exception>
		// Token: 0x06000F55 RID: 3925
		int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x06000F56 RID: 3926
		IDataReader ExecuteReader();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x06000F57 RID: 3927
		IDataReader ExecuteReader(CommandBehavior behavior);

		/// <summary>Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the resultset.</returns>
		// Token: 0x06000F58 RID: 3928
		object ExecuteScalar();
	}
}
