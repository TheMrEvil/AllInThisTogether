using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source.</summary>
	// Token: 0x0200015D RID: 349
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbCommand : DbCommand, IDbCommand, IDisposable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class.</summary>
		// Token: 0x0600129E RID: 4766 RVA: 0x0005ABDF File Offset: 0x00058DDF
		public OleDbCommand()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query.</summary>
		/// <param name="cmdText">The text of the query.</param>
		// Token: 0x0600129F RID: 4767 RVA: 0x0005ABE7 File Offset: 0x00058DE7
		public OleDbCommand(string cmdText)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query and an <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <param name="cmdText">The text of the query.</param>
		/// <param name="connection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection to a data source.</param>
		// Token: 0x060012A0 RID: 4768 RVA: 0x0005ABE7 File Offset: 0x00058DE7
		public OleDbCommand(string cmdText, OleDbConnection connection)
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query, an <see cref="T:System.Data.OleDb.OleDbConnection" />, and the <see cref="P:System.Data.OleDb.OleDbCommand.Transaction" />.</summary>
		/// <param name="cmdText">The text of the query.</param>
		/// <param name="connection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection to a data source.</param>
		/// <param name="transaction">The transaction in which the <see cref="T:System.Data.OleDb.OleDbCommand" /> executes.</param>
		// Token: 0x060012A1 RID: 4769 RVA: 0x0005ABE7 File Offset: 0x00058DE7
		public OleDbCommand(string cmdText, OleDbConnection connection, OleDbTransaction transaction)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets the SQL statement or stored procedure to execute at the data source.</summary>
		/// <returns>The SQL statement or stored procedure to execute. The default value is an empty string.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x00007EED File Offset: 0x000060ED
		public override string CommandText
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the wait time before terminating an attempt to execute a command and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for the command to execute. The default is 30 seconds.</returns>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x00007EED File Offset: 0x000060ED
		public override int CommandTimeout
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates how the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" /> values. The default is Text.</returns>
		/// <exception cref="T:System.ArgumentException">The value was not a valid <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" />.</exception>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x00007EED File Offset: 0x000060ED
		public override CommandType CommandType
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbConnection" /> used by this instance of the <see cref="T:System.Data.OleDb.OleDbCommand" />.</summary>
		/// <returns>The connection to a data source. The default value is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> property was changed while a transaction was in progress.</exception>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x00007EED File Offset: 0x000060ED
		public new OleDbConnection Connection
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x00007EED File Offset: 0x000060ED
		protected override DbConnection DbConnection
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012AE RID: 4782 RVA: 0x00007EED File Offset: 0x000060ED
		protected override DbTransaction DbTransaction
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a value that indicates whether the command object should be visible in a customized Windows Forms Designer control.</summary>
		/// <returns>A value that indicates whether the command object should be visible in a control. The default is <see langword="true" />.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012B0 RID: 4784 RVA: 0x00007EED File Offset: 0x000060ED
		public override bool DesignTimeVisible
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure. The default is an empty collection.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbParameterCollection Parameters
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbTransaction" /> within which the <see cref="T:System.Data.OleDb.OleDbCommand" /> executes.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbTransaction" />. The default value is <see langword="null" />.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x00007EED File Offset: 0x000060ED
		public new OleDbTransaction Transaction
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the <see langword="Update" /> method of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The value entered was not one of the <see cref="T:System.Data.UpdateRowSource" /> values.</exception>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00007EED File Offset: 0x000060ED
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Tries to cancel the execution of an <see cref="T:System.Data.OleDb.OleDbCommand" />.</summary>
		// Token: 0x060012B6 RID: 4790 RVA: 0x00007EED File Offset: 0x000060ED
		public override void Cancel()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Data.OleDb.OleDbCommand" /> object that is a copy of the current instance.</summary>
		/// <returns>A new <see cref="T:System.Data.OleDb.OleDbCommand" /> object that is a copy of this instance.</returns>
		// Token: 0x060012B7 RID: 4791 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public OleDbCommand Clone()
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override DbParameter CreateDbParameter()
		{
			throw ADP.OleDb();
		}

		/// <summary>Creates a new instance of an <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		// Token: 0x060012B9 RID: 4793 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbParameter CreateParameter()
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void Dispose(bool disposing)
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			throw ADP.OleDb();
		}

		/// <summary>Executes an SQL statement against the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.  
		///  -or-  
		///  The connection is not open.  
		///  -or-  
		///  Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted.</exception>
		// Token: 0x060012BC RID: 4796 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override int ExecuteNonQuery()
		{
			throw ADP.OleDb();
		}

		/// <summary>Sends the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> to the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> and builds an <see cref="T:System.Data.OleDb.OleDbDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted.</exception>
		// Token: 0x060012BD RID: 4797 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbDataReader ExecuteReader()
		{
			throw ADP.OleDb();
		}

		/// <summary>Sends the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> to the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" />, and builds an <see cref="T:System.Data.OleDb.OleDbDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted.</exception>
		// Token: 0x060012BE RID: 4798 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbDataReader ExecuteReader(CommandBehavior behavior)
		{
			throw ADP.OleDb();
		}

		/// <summary>Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted.</exception>
		// Token: 0x060012BF RID: 4799 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override object ExecuteScalar()
		{
			throw ADP.OleDb();
		}

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not set.  
		///  -or-  
		///  The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not open.</exception>
		// Token: 0x060012C0 RID: 4800 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Prepare()
		{
			throw ADP.OleDb();
		}

		/// <summary>Resets the <see cref="P:System.Data.OleDb.OleDbCommand.CommandTimeout" /> property to the default value.</summary>
		// Token: 0x060012C1 RID: 4801 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public void ResetCommandTimeout()
		{
			throw ADP.OleDb();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbCommand.ExecuteReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		// Token: 0x060012C2 RID: 4802 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		IDataReader IDbCommand.ExecuteReader()
		{
			throw ADP.OleDb();
		}

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> built using one of the <see cref="T:System.Data.CommandBehavior" /> values.</returns>
		// Token: 0x060012C3 RID: 4803 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			throw ADP.OleDb();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x060012C4 RID: 4804 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}
	}
}
