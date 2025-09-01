using System;
using System.Data.Common;
using System.EnterpriseServices;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;

namespace System.Data.OleDb
{
	/// <summary>Represents an open connection to a data source.</summary>
	// Token: 0x0200015F RID: 351
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbConnection : DbConnection, IDbConnection, IDisposable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnection" /> class.</summary>
		// Token: 0x060012D9 RID: 4825 RVA: 0x0005AC08 File Offset: 0x00058E08
		public OleDbConnection()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnection" /> class with the specified connection string.</summary>
		/// <param name="connectionString">The connection used to open the database.</param>
		// Token: 0x060012DA RID: 4826 RVA: 0x0005AC08 File Offset: 0x00058E08
		public OleDbConnection(string connectionString)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets the string used to open a database.</summary>
		/// <returns>The OLE DB provider connection string that includes the data source name, and other parameters needed to establish the initial connection. The default value is an empty string.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid connection string argument has been supplied or a required connection string argument has not been supplied.</exception>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x00007EED File Offset: 0x000060ED
		public override string ConnectionString
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		/// <summary>Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time in seconds to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0.</exception>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override int ConnectionTimeout
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override string Database
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the server name or file name of the data source.</summary>
		/// <returns>The server name or file name of the data source. The default value is an empty string.</returns>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override string DataSource
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the OLE DB provider specified in the "Provider= " clause of the connection string.</summary>
		/// <returns>The name of the provider as specified in the "Provider= " clause of the connection string. The default value is an empty string.</returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public string Provider
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets a string that contains the version of the server to which the client is connected.</summary>
		/// <returns>The version of the connected server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed.</exception>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override string ServerVersion
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Data.ConnectionState" /> values. The default is Closed.</returns>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override ConnectionState State
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			throw ADP.OleDb();
		}

		/// <summary>Starts a database transaction with the current <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported.</exception>
		// Token: 0x060012E4 RID: 4836 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbTransaction BeginTransaction()
		{
			throw ADP.OleDb();
		}

		/// <summary>Starts a database transaction with the specified isolation level.</summary>
		/// <param name="isolationLevel">The isolation level under which the transaction should run.</param>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported.</exception>
		// Token: 0x060012E5 RID: 4837 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			throw ADP.OleDb();
		}

		/// <summary>Changes the current database for an open <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <param name="value">The database name.</param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open.</exception>
		/// <exception cref="T:System.Data.OleDb.OleDbException">Cannot change the database.</exception>
		// Token: 0x060012E6 RID: 4838 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void ChangeDatabase(string value)
		{
			throw ADP.OleDb();
		}

		/// <summary>Closes the connection to the data source.</summary>
		// Token: 0x060012E7 RID: 4839 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Close()
		{
			throw ADP.OleDb();
		}

		/// <summary>Creates and returns an <see cref="T:System.Data.OleDb.OleDbCommand" /> object associated with the <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> object.</returns>
		// Token: 0x060012E8 RID: 4840 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbCommand CreateCommand()
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override DbCommand CreateDbCommand()
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void Dispose(bool disposing)
		{
			throw ADP.OleDb();
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x060012EB RID: 4843 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			throw ADP.OleDb();
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x060012EC RID: 4844 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void EnlistTransaction(Transaction transaction)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns schema information from a data source as indicated by a GUID, and after it applies the specified restrictions.</summary>
		/// <param name="schema">One of the <see cref="T:System.Data.OleDb.OleDbSchemaGuid" /> values that specifies the schema table to return.</param>
		/// <param name="restrictions">An <see cref="T:System.Object" /> array of restriction values. These are applied in the order of the restriction columns. That is, the first restriction value applies to the first restriction column, the second restriction value applies to the second restriction column, and so on.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains the requested schema information.</returns>
		/// <exception cref="T:System.Data.OleDb.OleDbException">The specified set of restrictions is invalid.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.OleDb.OleDbConnection" /> is closed.</exception>
		/// <exception cref="T:System.ArgumentException">The specified schema rowset is not supported by the OLE DB provider.  
		///  -or-  
		///  The <paramref name="schema" /> parameter contains a value of <see cref="F:System.Data.OleDb.OleDbSchemaGuid.DbInfoLiterals" /> and the <paramref name="restrictions" /> parameter contains one or more restrictions.</exception>
		// Token: 0x060012ED RID: 4845 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public DataTable GetOleDbSchemaTable(Guid schema, object[] restrictions)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x060012EE RID: 4846 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DataTable GetSchema()
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" /> using the specified string for the schema name.</summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x060012EF RID: 4847 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DataTable GetSchema(string collectionName)
		{
			throw ADP.OleDb();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x060012F0 RID: 4848 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			throw ADP.OleDb();
		}

		/// <summary>Opens a database connection with the property settings specified by the <see cref="P:System.Data.OleDb.OleDbConnection.ConnectionString" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The connection is already open.</exception>
		/// <exception cref="T:System.Data.OleDb.OleDbException">A connection-level error occurred while opening the connection.</exception>
		// Token: 0x060012F1 RID: 4849 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override void Open()
		{
			throw ADP.OleDb();
		}

		/// <summary>Indicates that the <see cref="T:System.Data.OleDb.OleDbConnection" /> object pool can be released when the last underlying connection is released.</summary>
		// Token: 0x060012F2 RID: 4850 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public static void ReleaseObjectPool()
		{
			throw ADP.OleDb();
		}

		/// <summary>Updates the <see cref="P:System.Data.OleDb.OleDbConnection.State" /> property of the <see cref="T:System.Data.OleDb.OleDbConnection" /> object.</summary>
		// Token: 0x060012F3 RID: 4851 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public void ResetState()
		{
			throw ADP.OleDb();
		}

		/// <summary>Occurs when the provider sends a warning or an informational message.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060012F4 RID: 4852 RVA: 0x0005AC18 File Offset: 0x00058E18
		// (remove) Token: 0x060012F5 RID: 4853 RVA: 0x0005AC50 File Offset: 0x00058E50
		public event OleDbInfoMessageEventHandler InfoMessage
		{
			[CompilerGenerated]
			add
			{
				OleDbInfoMessageEventHandler oleDbInfoMessageEventHandler = this.InfoMessage;
				OleDbInfoMessageEventHandler oleDbInfoMessageEventHandler2;
				do
				{
					oleDbInfoMessageEventHandler2 = oleDbInfoMessageEventHandler;
					OleDbInfoMessageEventHandler value2 = (OleDbInfoMessageEventHandler)Delegate.Combine(oleDbInfoMessageEventHandler2, value);
					oleDbInfoMessageEventHandler = Interlocked.CompareExchange<OleDbInfoMessageEventHandler>(ref this.InfoMessage, value2, oleDbInfoMessageEventHandler2);
				}
				while (oleDbInfoMessageEventHandler != oleDbInfoMessageEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				OleDbInfoMessageEventHandler oleDbInfoMessageEventHandler = this.InfoMessage;
				OleDbInfoMessageEventHandler oleDbInfoMessageEventHandler2;
				do
				{
					oleDbInfoMessageEventHandler2 = oleDbInfoMessageEventHandler;
					OleDbInfoMessageEventHandler value2 = (OleDbInfoMessageEventHandler)Delegate.Remove(oleDbInfoMessageEventHandler2, value);
					oleDbInfoMessageEventHandler = Interlocked.CompareExchange<OleDbInfoMessageEventHandler>(ref this.InfoMessage, value2, oleDbInfoMessageEventHandler2);
				}
				while (oleDbInfoMessageEventHandler != oleDbInfoMessageEventHandler2);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		/// <returns>A new <see cref="T:System.Object" /> that is a copy of this instance.</returns>
		// Token: 0x060012F6 RID: 4854 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		object ICloneable.Clone()
		{
			throw ADP.OleDb();
		}

		// Token: 0x04000BD1 RID: 3025
		[CompilerGenerated]
		private OleDbInfoMessageEventHandler InfoMessage;
	}
}
