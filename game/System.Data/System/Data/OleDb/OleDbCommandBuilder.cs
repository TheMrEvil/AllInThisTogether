using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated database. This class cannot be inherited.</summary>
	// Token: 0x0200015E RID: 350
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommandBuilder" /> class.</summary>
		// Token: 0x060012C5 RID: 4805 RVA: 0x0005ABFB File Offset: 0x00058DFB
		public OleDbCommandBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommandBuilder" /> class with the associated <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object.</summary>
		/// <param name="adapter">An <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</param>
		// Token: 0x060012C6 RID: 4806 RVA: 0x0005ABFB File Offset: 0x00058DFB
		public OleDbCommandBuilder(OleDbDataAdapter adapter)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets an <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object for which SQL statements are automatically generated.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00007EED File Offset: 0x000060ED
		public new OleDbDataAdapter DataAdapter
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			throw ADP.OleDb();
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.OleDb.OleDbCommand" /> and populates the <see cref="P:System.Data.OleDb.OleDbCommand.Parameters" /> collection of the specified <see cref="T:System.Data.OleDb.OleDbCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.OleDb.OleDbCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.OleDb.OleDbCommand.Parameters" /> collection of the <see cref="T:System.Data.OleDb.OleDbCommand" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying OLE DB provider does not support returning stored procedure parameter information, the command text is not a valid stored procedure name, or the <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" /> specified was not <see langword="StoredProcedure" />.</exception>
		// Token: 0x060012CA RID: 4810 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public static void DeriveParameters(OleDbCommand command)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions.</returns>
		// Token: 0x060012CB RID: 4811 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbCommand GetDeleteCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions at the data source.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names, if it is possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions.</returns>
		// Token: 0x060012CC RID: 4812 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions.</returns>
		// Token: 0x060012CD RID: 4813 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbCommand GetInsertCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions at the data source.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names, if it is possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions.</returns>
		// Token: 0x060012CE RID: 4814 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override string GetParameterName(int parameterOrdinal)
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override string GetParameterName(string parameterName)
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates.</returns>
		// Token: 0x060012D2 RID: 4818 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbCommand GetUpdateCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates at the data source, optionally using columns for parameter names.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names, if it is possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates.</returns>
		// Token: 0x060012D3 RID: 4819 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public new OleDbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		// Token: 0x060012D4 RID: 4820 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <param name="unquotedIdentifier">The unquoted identifier to be returned in quoted format.</param>
		/// <param name="connection">When a connection is passed, causes the managed wrapper to get the quote character from the OLE DB provider. When no connection is passed, the string is quoted using values from <see cref="P:System.Data.Common.DbCommandBuilder.QuotePrefix" /> and <see cref="P:System.Data.Common.DbCommandBuilder.QuoteSuffix" />.</param>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		// Token: 0x060012D5 RID: 4821 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public string QuoteIdentifier(string unquotedIdentifier, OleDbConnection connection)
		{
			throw ADP.OleDb();
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly un-escaping any embedded quotes in the identifier.</summary>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <returns>The unquoted identifier, with embedded quotes correctly un-escaped.</returns>
		// Token: 0x060012D7 RID: 4823 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly un-escaping any embedded quotes in the identifier.</summary>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <param name="connection">The <see cref="T:System.Data.OleDb.OleDbConnection" />.</param>
		/// <returns>The unquoted identifier, with embedded quotes correctly un-escaped.</returns>
		// Token: 0x060012D8 RID: 4824 RVA: 0x0005ABF4 File Offset: 0x00058DF4
		public string UnquoteIdentifier(string quotedIdentifier, OleDbConnection connection)
		{
			throw ADP.OleDb();
		}
	}
}
