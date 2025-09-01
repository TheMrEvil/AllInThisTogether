using System;

namespace System.Data
{
	/// <summary>Provides a description of the results of the query and its effect on the database.</summary>
	// Token: 0x020000A4 RID: 164
	[Flags]
	public enum CommandBehavior
	{
		/// <summary>The query may return multiple result sets. Execution of the query may affect the database state. <see langword="Default" /> sets no <see cref="T:System.Data.CommandBehavior" /> flags, so calling <see langword="ExecuteReader(CommandBehavior.Default)" /> is functionally equivalent to calling <see langword="ExecuteReader()" />.</summary>
		// Token: 0x04000762 RID: 1890
		Default = 0,
		/// <summary>The query returns a single result set.</summary>
		// Token: 0x04000763 RID: 1891
		SingleResult = 1,
		/// <summary>The query returns column information only. When using <see cref="F:System.Data.CommandBehavior.SchemaOnly" />, the .NET Framework Data Provider for SQL Server precedes the statement being executed with SET FMTONLY ON.</summary>
		// Token: 0x04000764 RID: 1892
		SchemaOnly = 2,
		/// <summary>The query returns column and primary key information.</summary>
		// Token: 0x04000765 RID: 1893
		KeyInfo = 4,
		/// <summary>The query is expected to return a single row of the first result set. Execution of the query may affect the database state. Some .NET Framework data providers may, but are not required to, use this information to optimize the performance of the command. When you specify <see cref="F:System.Data.CommandBehavior.SingleRow" /> with the <see cref="M:System.Data.OleDb.OleDbCommand.ExecuteReader" /> method of the <see cref="T:System.Data.OleDb.OleDbCommand" /> object, the .NET Framework Data Provider for OLE DB performs binding using the OLE DB <see langword="IRow" /> interface if it is available. Otherwise, it uses the <see langword="IRowset" /> interface. If your SQL statement is expected to return only a single row, specifying <see cref="F:System.Data.CommandBehavior.SingleRow" /> can also improve application performance. It is possible to specify <see langword="SingleRow" /> when executing queries that are expected to return multiple result sets.  In that case, where both a multi-result set SQL query and single row are specified, the result returned will contain only the first row of the first result set. The other result sets of the query will not be returned.</summary>
		// Token: 0x04000766 RID: 1894
		SingleRow = 8,
		/// <summary>Provides a way for the <see langword="DataReader" /> to handle rows that contain columns with large binary values. Rather than loading the entire row, <see langword="SequentialAccess" /> enables the <see langword="DataReader" /> to load data as a stream. You can then use the <see langword="GetBytes" /> or <see langword="GetChars" /> method to specify a byte location to start the read operation, and a limited buffer size for the data being returned.</summary>
		// Token: 0x04000767 RID: 1895
		SequentialAccess = 16,
		/// <summary>When the command is executed, the associated <see langword="Connection" /> object is closed when the associated <see langword="DataReader" /> object is closed.</summary>
		// Token: 0x04000768 RID: 1896
		CloseConnection = 32
	}
}
