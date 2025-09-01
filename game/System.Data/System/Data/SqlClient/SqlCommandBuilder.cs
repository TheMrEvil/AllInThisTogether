using System;
using System.Data.Common;
using System.Data.Sql;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.SqlClient
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated SQL Server database. This class cannot be inherited.</summary>
	// Token: 0x020001C5 RID: 453
	public sealed class SqlCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		// Token: 0x06001602 RID: 5634 RVA: 0x000654A9 File Offset: 0x000636A9
		public SqlCommandBuilder()
		{
			GC.SuppressFinalize(this);
			base.QuotePrefix = "[";
			base.QuoteSuffix = "]";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class with the associated <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object.</summary>
		/// <param name="adapter">The name of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />.</param>
		// Token: 0x06001603 RID: 5635 RVA: 0x000654CD File Offset: 0x000636CD
		public SqlCommandBuilder(SqlDataAdapter adapter) : this()
		{
			this.DataAdapter = adapter;
		}

		/// <summary>Sets or gets the <see cref="T:System.Data.Common.CatalogLocation" /> for an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		/// <returns>A <see cref="T:System.Data.Common.CatalogLocation" /> object.</returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00006D61 File Offset: 0x00004F61
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x000654DC File Offset: 0x000636DC
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return CatalogLocation.Start;
			}
			set
			{
				if (CatalogLocation.Start != value)
				{
					throw ADP.SingleValuedProperty("CatalogLocation", "Start");
				}
			}
		}

		/// <summary>Sets or gets a string used as the catalog separator for an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		/// <returns>A string that indicates the catalog separator for use with an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</returns>
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x000654F2 File Offset: 0x000636F2
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x000654F9 File Offset: 0x000636F9
		public override string CatalogSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if ("." != value)
				{
					throw ADP.SingleValuedProperty("CatalogSeparator", ".");
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object for which Transact-SQL statements are automatically generated.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object.</returns>
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00065518 File Offset: 0x00063718
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x00065525 File Offset: 0x00063725
		public new SqlDataAdapter DataAdapter
		{
			get
			{
				return (SqlDataAdapter)base.DataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		/// <summary>Gets or sets the starting character or characters to use when specifying SQL Server database objects, such as tables or columns, whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The starting character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an INSERT, UPDATE, or DELETE command has been generated.</exception>
		// Token: 0x170003BD RID: 957
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0006552E File Offset: 0x0006372E
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x00065536 File Offset: 0x00063736
		public override string QuotePrefix
		{
			get
			{
				return base.QuotePrefix;
			}
			set
			{
				if ("[" != value && "\"" != value)
				{
					throw ADP.DoubleValuedProperty("QuotePrefix", "[", "\"");
				}
				base.QuotePrefix = value;
			}
		}

		/// <summary>Gets or sets the ending character or characters to use when specifying SQL Server database objects, such as tables or columns, whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The ending character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an insert, update, or delete command has been generated.</exception>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0006556E File Offset: 0x0006376E
		// (set) Token: 0x0600160D RID: 5645 RVA: 0x00065576 File Offset: 0x00063776
		public override string QuoteSuffix
		{
			get
			{
				return base.QuoteSuffix;
			}
			set
			{
				if ("]" != value && "\"" != value)
				{
					throw ADP.DoubleValuedProperty("QuoteSuffix", "]", "\"");
				}
				base.QuoteSuffix = value;
			}
		}

		/// <summary>Gets or sets the character to be used for the separator between the schema identifier and any other identifiers.</summary>
		/// <returns>The character to be used as the schema separator.</returns>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x000654F2 File Offset: 0x000636F2
		// (set) Token: 0x0600160F RID: 5647 RVA: 0x000655AE File Offset: 0x000637AE
		public override string SchemaSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if ("." != value)
				{
					throw ADP.SingleValuedProperty("SchemaSeparator", ".");
				}
			}
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000655CD File Offset: 0x000637CD
		private void SqlRowUpdatingHandler(object sender, SqlRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform insertions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform insertions.</returns>
		// Token: 0x06001611 RID: 5649 RVA: 0x000655D6 File Offset: 0x000637D6
		public new SqlCommand GetInsertCommand()
		{
			return (SqlCommand)base.GetInsertCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform insertions on the database.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names if possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform insertions.</returns>
		// Token: 0x06001612 RID: 5650 RVA: 0x000655E3 File Offset: 0x000637E3
		public new SqlCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform updates.</returns>
		// Token: 0x06001613 RID: 5651 RVA: 0x000655F1 File Offset: 0x000637F1
		public new SqlCommand GetUpdateCommand()
		{
			return (SqlCommand)base.GetUpdateCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates on the database.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names if possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates.</returns>
		// Token: 0x06001614 RID: 5652 RVA: 0x000655FE File Offset: 0x000637FE
		public new SqlCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform deletions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform deletions.</returns>
		// Token: 0x06001615 RID: 5653 RVA: 0x0006560C File Offset: 0x0006380C
		public new SqlCommand GetDeleteCommand()
		{
			return (SqlCommand)base.GetDeleteCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform deletions on the database.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names if possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform deletions.</returns>
		// Token: 0x06001616 RID: 5654 RVA: 0x00065619 File Offset: 0x00063819
		public new SqlCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00065628 File Offset: 0x00063828
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			SqlParameter sqlParameter = (SqlParameter)parameter;
			object obj = datarow[SchemaTableColumn.ProviderType];
			sqlParameter.SqlDbType = (SqlDbType)obj;
			sqlParameter.Offset = 0;
			if (sqlParameter.SqlDbType == SqlDbType.Udt && !sqlParameter.SourceColumnNullMapping)
			{
				sqlParameter.UdtTypeName = (datarow["DataTypeName"] as string);
			}
			else
			{
				sqlParameter.UdtTypeName = string.Empty;
			}
			object obj2 = datarow[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				sqlParameter.PrecisionInternal = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = datarow[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				sqlParameter.ScaleInternal = ((byte.MaxValue != b2) ? b2 : 0);
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000656EF File Offset: 0x000638EF
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00065707 File Offset: 0x00063907
		protected override string GetParameterName(string parameterName)
		{
			return "@" + parameterName;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000656EF File Offset: 0x000638EF
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00065714 File Offset: 0x00063914
		private void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
		{
			if (("\"" == quotePrefix && "\"" != quoteSuffix) || ("[" == quotePrefix && "]" != quoteSuffix))
			{
				throw ADP.InvalidPrefixSuffix();
			}
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.SqlClient.SqlCommand" /> and populates the <see cref="P:System.Data.SqlClient.SqlCommand.Parameters" /> collection of the specified <see cref="T:System.Data.SqlClient.SqlCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.SqlClient.SqlCommand.Parameters" /> collection of the <see cref="T:System.Data.SqlClient.SqlCommand" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The command text is not a valid stored procedure name.</exception>
		// Token: 0x0600161C RID: 5660 RVA: 0x00065750 File Offset: 0x00063950
		public static void DeriveParameters(SqlCommand command)
		{
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				command.DeriveParameters();
			}
			catch (OutOfMemoryException e)
			{
				if (command != null)
				{
					SqlConnection connection = command.Connection;
					if (connection != null)
					{
						connection.Abort(e);
					}
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				if (command != null)
				{
					SqlConnection connection2 = command.Connection;
					if (connection2 != null)
					{
						connection2.Abort(e2);
					}
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				if (command != null)
				{
					SqlConnection connection3 = command.Connection;
					if (connection3 != null)
					{
						connection3.Abort(e3);
					}
				}
				throw;
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x000657E8 File Offset: 0x000639E8
		protected override DataTable GetSchemaTable(DbCommand srcCommand)
		{
			SqlCommand sqlCommand = srcCommand as SqlCommand;
			SqlNotificationRequest notification = sqlCommand.Notification;
			sqlCommand.Notification = null;
			DataTable schemaTable;
			try
			{
				using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
				{
					schemaTable = sqlDataReader.GetSchemaTable();
				}
			}
			finally
			{
				sqlCommand.Notification = notification;
			}
			return schemaTable;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0006584C File Offset: 0x00063A4C
		protected override DbCommand InitializeCommand(DbCommand command)
		{
			return (SqlCommand)base.InitializeCommand(command);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		// Token: 0x0600161F RID: 5663 RVA: 0x0006585C File Offset: 0x00063A5C
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			return ADP.BuildQuotedString(quotePrefix, quoteSuffix, unquotedIdentifier);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00065892 File Offset: 0x00063A92
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SqlDataAdapter)adapter).RowUpdating -= this.SqlRowUpdatingHandler;
				return;
			}
			((SqlDataAdapter)adapter).RowUpdating += this.SqlRowUpdatingHandler;
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly unescaping any embedded quotes in the identifier.</summary>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <returns>The unquoted identifier, with embedded quotes properly unescaped.</returns>
		// Token: 0x06001621 RID: 5665 RVA: 0x000658CC File Offset: 0x00063ACC
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			string result;
			ADP.RemoveStringQuotes(quotePrefix, quoteSuffix, quotedIdentifier, out result);
			return result;
		}
	}
}
