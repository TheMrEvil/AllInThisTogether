using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated data source. This class cannot be inherited.</summary>
	// Token: 0x020002D7 RID: 727
	public sealed class OdbcCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> class.</summary>
		// Token: 0x06001FB3 RID: 8115 RVA: 0x00095220 File Offset: 0x00093420
		public OdbcCommandBuilder()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> class with the associated <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object.</summary>
		/// <param name="adapter">An <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object to associate with this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" />.</param>
		// Token: 0x06001FB4 RID: 8116 RVA: 0x0009522E File Offset: 0x0009342E
		public OdbcCommandBuilder(OdbcDataAdapter adapter) : this()
		{
			this.DataAdapter = adapter;
		}

		/// <summary>Gets or sets an <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object for which this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> object will generate SQL statements.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object that is associated with this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" />.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0009523D File Offset: 0x0009343D
		// (set) Token: 0x06001FB6 RID: 8118 RVA: 0x00065525 File Offset: 0x00063725
		public new OdbcDataAdapter DataAdapter
		{
			get
			{
				return base.DataAdapter as OdbcDataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x000655CD File Offset: 0x000637CD
		private void OdbcRowUpdatingHandler(object sender, OdbcRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions.</returns>
		// Token: 0x06001FB8 RID: 8120 RVA: 0x0009524A File Offset: 0x0009344A
		public new OdbcCommand GetInsertCommand()
		{
			return (OdbcCommand)base.GetInsertCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions at the data source.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names, if it is possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions.</returns>
		// Token: 0x06001FB9 RID: 8121 RVA: 0x00095257 File Offset: 0x00093457
		public new OdbcCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates.</returns>
		// Token: 0x06001FBA RID: 8122 RVA: 0x00095265 File Offset: 0x00093465
		public new OdbcCommand GetUpdateCommand()
		{
			return (OdbcCommand)base.GetUpdateCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates at the data source.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names, if it is possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates.</returns>
		// Token: 0x06001FBB RID: 8123 RVA: 0x00095272 File Offset: 0x00093472
		public new OdbcCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions.</returns>
		// Token: 0x06001FBC RID: 8124 RVA: 0x00095280 File Offset: 0x00093480
		public new OdbcCommand GetDeleteCommand()
		{
			return (OdbcCommand)base.GetDeleteCommand();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions at the data source.</summary>
		/// <param name="useColumnsForParameterNames">If <see langword="true" />, generate parameter names matching column names, if it is possible. If <see langword="false" />, generate @p1, @p2, and so on.</param>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions.</returns>
		// Token: 0x06001FBD RID: 8125 RVA: 0x0009528D File Offset: 0x0009348D
		public new OdbcCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0009529B File Offset: 0x0009349B
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x000056BA File Offset: 0x000038BA
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x000952B3 File Offset: 0x000934B3
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "?";
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000952BC File Offset: 0x000934BC
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			OdbcParameter odbcParameter = (OdbcParameter)parameter;
			object obj = datarow[SchemaTableColumn.ProviderType];
			odbcParameter.OdbcType = (OdbcType)obj;
			object obj2 = datarow[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				odbcParameter.PrecisionInternal = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = datarow[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				odbcParameter.ScaleInternal = ((byte.MaxValue != b2) ? b2 : 0);
			}
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.Odbc.OdbcCommand" /> and populates the <see cref="P:System.Data.Odbc.OdbcCommand.Parameters" /> collection of the specified <see cref="T:System.Data.Odbc.OdbcCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.Odbc.OdbcCommand.Parameters" /> collection of the <see cref="T:System.Data.Odbc.OdbcCommand" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The underlying ODBC driver does not support returning stored procedure parameter information, or the command text is not a valid stored procedure name, or the <see cref="T:System.Data.CommandType" /> specified was not <see langword="CommandType.StoredProcedure" />.</exception>
		// Token: 0x06001FC2 RID: 8130 RVA: 0x00095348 File Offset: 0x00093548
		public static void DeriveParameters(OdbcCommand command)
		{
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			CommandType commandType = command.CommandType;
			if (commandType == CommandType.Text)
			{
				throw ADP.DeriveParametersNotSupported(command);
			}
			if (commandType != CommandType.StoredProcedure)
			{
				if (commandType != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(command.CommandType);
				}
				throw ADP.DeriveParametersNotSupported(command);
			}
			else
			{
				if (string.IsNullOrEmpty(command.CommandText))
				{
					throw ADP.CommandTextRequired("DeriveParameters");
				}
				OdbcConnection connection = command.Connection;
				if (connection == null)
				{
					throw ADP.ConnectionRequired("DeriveParameters");
				}
				ConnectionState state = connection.State;
				if (ConnectionState.Open != state)
				{
					throw ADP.OpenConnectionRequired("DeriveParameters", state);
				}
				OdbcParameter[] array = OdbcCommandBuilder.DeriveParametersFromStoredProcedure(connection, command);
				OdbcParameterCollection parameters = command.Parameters;
				parameters.Clear();
				int num = array.Length;
				if (0 < num)
				{
					for (int i = 0; i < array.Length; i++)
					{
						parameters.Add(array[i]);
					}
				}
				return;
			}
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0009541C File Offset: 0x0009361C
		private static OdbcParameter[] DeriveParametersFromStoredProcedure(OdbcConnection connection, OdbcCommand command)
		{
			List<OdbcParameter> list = new List<OdbcParameter>();
			CMDWrapper statementHandle = command.GetStatementHandle();
			OdbcStatementHandle statementHandle2 = statementHandle.StatementHandle;
			string text = connection.QuoteChar("DeriveParameters");
			string[] array = MultipartIdentifier.ParseMultipartIdentifier(command.CommandText, text, text, '.', 4, true, "OdbcCommandBuilder.DeriveParameters failed because the OdbcCommand.CommandText property value is an invalid multipart name", false);
			if (array[3] == null)
			{
				array[3] = command.CommandText;
			}
			ODBC32.RetCode retCode = statementHandle2.ProcedureColumns(array[1], array[2], array[3], null);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				connection.HandleError(statementHandle2, retCode);
			}
			using (OdbcDataReader odbcDataReader = new OdbcDataReader(command, statementHandle, CommandBehavior.Default))
			{
				odbcDataReader.FirstResult();
				int fieldCount = odbcDataReader.FieldCount;
				while (odbcDataReader.Read())
				{
					OdbcParameter odbcParameter = new OdbcParameter();
					odbcParameter.ParameterName = odbcDataReader.GetString(3);
					switch (odbcDataReader.GetInt16(4))
					{
					case 1:
						odbcParameter.Direction = ParameterDirection.Input;
						break;
					case 2:
						odbcParameter.Direction = ParameterDirection.InputOutput;
						break;
					case 4:
						odbcParameter.Direction = ParameterDirection.Output;
						break;
					case 5:
						odbcParameter.Direction = ParameterDirection.ReturnValue;
						break;
					}
					odbcParameter.OdbcType = TypeMap.FromSqlType((ODBC32.SQL_TYPE)odbcDataReader.GetInt16(5))._odbcType;
					odbcParameter.Size = odbcDataReader.GetInt32(7);
					OdbcType odbcType = odbcParameter.OdbcType;
					if (odbcType - OdbcType.Decimal <= 1)
					{
						odbcParameter.ScaleInternal = (byte)odbcDataReader.GetInt16(9);
						odbcParameter.PrecisionInternal = (byte)odbcDataReader.GetInt16(10);
					}
					list.Add(odbcParameter);
				}
			}
			retCode = statementHandle2.CloseCursor();
			return list.ToArray();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		// Token: 0x06001FC4 RID: 8132 RVA: 0x000955B4 File Offset: 0x000937B4
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return this.QuoteIdentifier(unquotedIdentifier, null);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <param name="connection">When a connection is passed, causes the managed wrapper to get the quote character from the ODBC driver, calling SQLGetInfo(SQL_IDENTIFIER_QUOTE_CHAR). When no connection is passed, the string is quoted using values from <see cref="P:System.Data.Common.DbCommandBuilder.QuotePrefix" /> and <see cref="P:System.Data.Common.DbCommandBuilder.QuoteSuffix" />.</param>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		// Token: 0x06001FC5 RID: 8133 RVA: 0x000955C0 File Offset: 0x000937C0
		public string QuoteIdentifier(string unquotedIdentifier, OdbcConnection connection)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string text = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (string.IsNullOrEmpty(text))
			{
				if (connection == null)
				{
					OdbcDataAdapter dataAdapter = this.DataAdapter;
					OdbcConnection odbcConnection;
					if (dataAdapter == null)
					{
						odbcConnection = null;
					}
					else
					{
						OdbcCommand selectCommand = dataAdapter.SelectCommand;
						odbcConnection = ((selectCommand != null) ? selectCommand.Connection : null);
					}
					connection = odbcConnection;
					if (connection == null)
					{
						throw ADP.QuotePrefixNotSet("QuoteIdentifier");
					}
				}
				text = connection.QuoteChar("QuoteIdentifier");
				quoteSuffix = text;
			}
			if (!string.IsNullOrEmpty(text) && text != " ")
			{
				return ADP.BuildQuotedString(text, quoteSuffix, unquotedIdentifier);
			}
			return unquotedIdentifier;
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0009564C File Offset: 0x0009384C
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((OdbcDataAdapter)adapter).RowUpdating -= this.OdbcRowUpdatingHandler;
				return;
			}
			((OdbcDataAdapter)adapter).RowUpdating += this.OdbcRowUpdatingHandler;
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including correctly unescaping any embedded quotes in the identifier.</summary>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <returns>The unquoted identifier, with embedded quotes correctly unescaped.</returns>
		// Token: 0x06001FC7 RID: 8135 RVA: 0x00095686 File Offset: 0x00093886
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return this.UnquoteIdentifier(quotedIdentifier, null);
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including correctly unescaping any embedded quotes in the identifier.</summary>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <param name="connection">The <see cref="T:System.Data.Odbc.OdbcConnection" />.</param>
		/// <returns>The unquoted identifier, with embedded quotes correctly unescaped.</returns>
		// Token: 0x06001FC8 RID: 8136 RVA: 0x00095690 File Offset: 0x00093890
		public string UnquoteIdentifier(string quotedIdentifier, OdbcConnection connection)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string text = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (string.IsNullOrEmpty(text))
			{
				if (connection == null)
				{
					OdbcDataAdapter dataAdapter = this.DataAdapter;
					OdbcConnection odbcConnection;
					if (dataAdapter == null)
					{
						odbcConnection = null;
					}
					else
					{
						OdbcCommand selectCommand = dataAdapter.SelectCommand;
						odbcConnection = ((selectCommand != null) ? selectCommand.Connection : null);
					}
					connection = odbcConnection;
					if (connection == null)
					{
						throw ADP.QuotePrefixNotSet("UnquoteIdentifier");
					}
				}
				text = connection.QuoteChar("UnquoteIdentifier");
				quoteSuffix = text;
			}
			string result;
			if (!string.IsNullOrEmpty(text) || text != " ")
			{
				ADP.RemoveStringQuotes(text, quoteSuffix, quotedIdentifier, out result);
			}
			else
			{
				result = quotedIdentifier;
			}
			return result;
		}
	}
}
