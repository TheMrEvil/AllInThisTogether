using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace System.Data.SqlClient
{
	/// <summary>Lets you efficiently bulk load a SQL Server table with data from another source.</summary>
	// Token: 0x02000198 RID: 408
	public sealed class SqlBulkCopy : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class using the specified open instance of <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
		/// <param name="connection">The already open <see cref="T:System.Data.SqlClient.SqlConnection" /> instance that will be used to perform the bulk copy operation. If your connection string does not use <see langword="Integrated Security = true" />, you can use <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x0600147D RID: 5245 RVA: 0x0005CA91 File Offset: 0x0005AC91
		public SqlBulkCopy(SqlConnection connection)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			this._connection = connection;
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class using the supplied existing open instance of <see cref="T:System.Data.SqlClient.SqlConnection" />. The <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance behaves according to options supplied in the <paramref name="copyOptions" /> parameter. If a non-null <see cref="T:System.Data.SqlClient.SqlTransaction" /> is supplied, the copy operations will be performed within that transaction.</summary>
		/// <param name="connection">The already open <see cref="T:System.Data.SqlClient.SqlConnection" /> instance that will be used to perform the bulk copy. If your connection string does not use <see langword="Integrated Security = true" />, you can use <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		/// <param name="copyOptions">A combination of values from the <see cref="T:System.Data.SqlClient.SqlBulkCopyOptions" /> enumeration that determines which data source rows are copied to the destination table.</param>
		/// <param name="externalTransaction">An existing <see cref="T:System.Data.SqlClient.SqlTransaction" /> instance under which the bulk copy will occur.</param>
		// Token: 0x0600147E RID: 5246 RVA: 0x0005CAC1 File Offset: 0x0005ACC1
		public SqlBulkCopy(SqlConnection connection, SqlBulkCopyOptions copyOptions, SqlTransaction externalTransaction) : this(connection)
		{
			this._copyOptions = copyOptions;
			if (externalTransaction != null && this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
			{
				throw SQL.BulkLoadConflictingTransactionOption();
			}
			if (!this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
			{
				this._externalTransaction = externalTransaction;
			}
		}

		/// <summary>Initializes and opens a new instance of <see cref="T:System.Data.SqlClient.SqlConnection" /> based on the supplied <paramref name="connectionString" />. The constructor uses the <see cref="T:System.Data.SqlClient.SqlConnection" /> to initialize a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class.</summary>
		/// <param name="connectionString">The string defining the connection that will be opened for use by the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance. If your connection string does not use <see langword="Integrated Security = true" />, you can use <see cref="M:System.Data.SqlClient.SqlBulkCopy.#ctor(System.Data.SqlClient.SqlConnection)" /> or <see cref="M:System.Data.SqlClient.SqlBulkCopy.#ctor(System.Data.SqlClient.SqlConnection,System.Data.SqlClient.SqlBulkCopyOptions,System.Data.SqlClient.SqlTransaction)" /> and <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		// Token: 0x0600147F RID: 5247 RVA: 0x0005CAF5 File Offset: 0x0005ACF5
		public SqlBulkCopy(string connectionString)
		{
			if (connectionString == null)
			{
				throw ADP.ArgumentNull("connectionString");
			}
			this._connection = new SqlConnection(connectionString);
			this._columnMappings = new SqlBulkCopyColumnMappingCollection();
			this._ownConnection = true;
		}

		/// <summary>Initializes and opens a new instance of <see cref="T:System.Data.SqlClient.SqlConnection" /> based on the supplied <paramref name="connectionString" />. The constructor uses that <see cref="T:System.Data.SqlClient.SqlConnection" /> to initialize a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class. The <see cref="T:System.Data.SqlClient.SqlConnection" /> instance behaves according to options supplied in the <paramref name="copyOptions" /> parameter.</summary>
		/// <param name="connectionString">The string defining the connection that will be opened for use by the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance. If your connection string does not use <see langword="Integrated Security = true" />, you can use <see cref="M:System.Data.SqlClient.SqlBulkCopy.#ctor(System.Data.SqlClient.SqlConnection)" /> or <see cref="M:System.Data.SqlClient.SqlBulkCopy.#ctor(System.Data.SqlClient.SqlConnection,System.Data.SqlClient.SqlBulkCopyOptions,System.Data.SqlClient.SqlTransaction)" /> and <see cref="T:System.Data.SqlClient.SqlCredential" /> to pass the user ID and password more securely than by specifying the user ID and password as text in the connection string.</param>
		/// <param name="copyOptions">A combination of values from the <see cref="T:System.Data.SqlClient.SqlBulkCopyOptions" /> enumeration that determines which data source rows are copied to the destination table.</param>
		// Token: 0x06001480 RID: 5248 RVA: 0x0005CB31 File Offset: 0x0005AD31
		public SqlBulkCopy(string connectionString, SqlBulkCopyOptions copyOptions) : this(connectionString)
		{
			this._copyOptions = copyOptions;
		}

		/// <summary>Number of rows in each batch. At the end of each batch, the rows in the batch are sent to the server.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.BatchSize" /> property, or zero if no value has been set.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0005CB41 File Offset: 0x0005AD41
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x0005CB49 File Offset: 0x0005AD49
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
			set
			{
				if (value >= 0)
				{
					this._batchSize = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("BatchSize");
			}
		}

		/// <summary>Number of seconds for the operation to complete before it times out.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.BulkCopyTimeout" /> property. The default is 30 seconds. A value of 0 indicates no limit; the bulk copy will wait indefinitely.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0005CB61 File Offset: 0x0005AD61
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x0005CB69 File Offset: 0x0005AD69
		public int BulkCopyTimeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (value < 0)
				{
					throw SQL.BulkLoadInvalidTimeout(value);
				}
				this._timeout = value;
			}
		}

		/// <summary>Enables or disables a <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object to stream data from an <see cref="T:System.Data.IDataReader" /> object</summary>
		/// <returns>
		///   <see langword="true" /> if a <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object can stream data from an <see cref="T:System.Data.IDataReader" /> object; otherwise, false. The default is <see langword="false" />.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0005CB7D File Offset: 0x0005AD7D
		// (set) Token: 0x06001486 RID: 5254 RVA: 0x0005CB85 File Offset: 0x0005AD85
		public bool EnableStreaming
		{
			get
			{
				return this._enableStreaming;
			}
			set
			{
				this._enableStreaming = value;
			}
		}

		/// <summary>Returns a collection of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> items. Column mappings define the relationships between columns in the data source and columns in the destination.</summary>
		/// <returns>A collection of column mappings. By default, it is an empty collection.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0005CB8E File Offset: 0x0005AD8E
		public SqlBulkCopyColumnMappingCollection ColumnMappings
		{
			get
			{
				return this._columnMappings;
			}
		}

		/// <summary>Name of the destination table on the server.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property, or null if none as been supplied.</returns>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x0005CB96 File Offset: 0x0005AD96
		// (set) Token: 0x06001489 RID: 5257 RVA: 0x0005CB9E File Offset: 0x0005AD9E
		public string DestinationTableName
		{
			get
			{
				return this._destinationTableName;
			}
			set
			{
				if (value == null)
				{
					throw ADP.ArgumentNull("DestinationTableName");
				}
				if (value.Length == 0)
				{
					throw ADP.ArgumentOutOfRange("DestinationTableName");
				}
				this._destinationTableName = value;
			}
		}

		/// <summary>Defines the number of rows to be processed before generating a notification event.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.NotifyAfter" /> property, or zero if the property has not been set.</returns>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0005CBC8 File Offset: 0x0005ADC8
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x0005CBD0 File Offset: 0x0005ADD0
		public int NotifyAfter
		{
			get
			{
				return this._notifyAfter;
			}
			set
			{
				if (value >= 0)
				{
					this._notifyAfter = value;
					return;
				}
				throw ADP.ArgumentOutOfRange("NotifyAfter");
			}
		}

		/// <summary>Occurs every time that the number of rows specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.NotifyAfter" /> property have been processed.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600148C RID: 5260 RVA: 0x0005CBE8 File Offset: 0x0005ADE8
		// (remove) Token: 0x0600148D RID: 5261 RVA: 0x0005CC01 File Offset: 0x0005AE01
		public event SqlRowsCopiedEventHandler SqlRowsCopied
		{
			add
			{
				this._rowsCopiedEventHandler = (SqlRowsCopiedEventHandler)Delegate.Combine(this._rowsCopiedEventHandler, value);
			}
			remove
			{
				this._rowsCopiedEventHandler = (SqlRowsCopiedEventHandler)Delegate.Remove(this._rowsCopiedEventHandler, value);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0005CC1A File Offset: 0x0005AE1A
		internal SqlStatistics Statistics
		{
			get
			{
				if (this._connection != null && this._connection.StatisticsEnabled)
				{
					return this._connection.Statistics;
				}
				return null;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class.</summary>
		// Token: 0x0600148F RID: 5263 RVA: 0x0005CC3E File Offset: 0x0005AE3E
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0005CC4D File Offset: 0x0005AE4D
		private bool IsCopyOption(SqlBulkCopyOptions copyOption)
		{
			return (this._copyOptions & copyOption) == copyOption;
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0005CC5C File Offset: 0x0005AE5C
		private string CreateInitialQuery()
		{
			string[] array;
			try
			{
				array = MultipartIdentifier.ParseMultipartIdentifier(this.DestinationTableName, "[\"", "]\"", "SqlBulkCopy.WriteToServer failed because the SqlBulkCopy.DestinationTableName is an invalid multipart name", true);
			}
			catch (Exception inner)
			{
				throw SQL.BulkLoadInvalidDestinationTable(this.DestinationTableName, inner);
			}
			if (string.IsNullOrEmpty(array[3]))
			{
				throw SQL.BulkLoadInvalidDestinationTable(this.DestinationTableName, null);
			}
			string text = "select @@trancount; SET FMTONLY ON select * from " + this.DestinationTableName + " SET FMTONLY OFF ";
			string text2;
			if (this._connection.IsKatmaiOrNewer)
			{
				text2 = "sp_tablecollations_100";
			}
			else
			{
				text2 = "sp_tablecollations_90";
			}
			string text3 = array[3];
			bool flag = text3.Length > 0 && '#' == text3[0];
			if (!string.IsNullOrEmpty(text3))
			{
				text3 = SqlServerEscapeHelper.EscapeStringAsLiteral(text3);
				text3 = SqlServerEscapeHelper.EscapeIdentifier(text3);
			}
			string text4 = array[2];
			if (!string.IsNullOrEmpty(text4))
			{
				text4 = SqlServerEscapeHelper.EscapeStringAsLiteral(text4);
				text4 = SqlServerEscapeHelper.EscapeIdentifier(text4);
			}
			string text5 = array[1];
			if (flag && string.IsNullOrEmpty(text5))
			{
				text += string.Format(null, "exec tempdb..{0} N'{1}.{2}'", text2, text4, text3);
			}
			else
			{
				if (!string.IsNullOrEmpty(text5))
				{
					text5 = SqlServerEscapeHelper.EscapeIdentifier(text5);
				}
				text += string.Format(null, "exec {0}..{1} N'{2}.{3}'", new object[]
				{
					text5,
					text2,
					text4,
					text3
				});
			}
			return text;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0005CDA8 File Offset: 0x0005AFA8
		private Task<BulkCopySimpleResultSet> CreateAndExecuteInitialQueryAsync(out BulkCopySimpleResultSet result)
		{
			string text = this.CreateInitialQuery();
			Task task = this._parser.TdsExecuteSQLBatch(text, this.BulkCopyTimeout, null, this._stateObj, !this._isAsyncBulkCopy, true);
			if (task == null)
			{
				result = new BulkCopySimpleResultSet();
				this.RunParser(result);
				return null;
			}
			result = null;
			return task.ContinueWith<BulkCopySimpleResultSet>(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					throw t.Exception.InnerException;
				}
				BulkCopySimpleResultSet bulkCopySimpleResultSet = new BulkCopySimpleResultSet();
				this.RunParserReliably(bulkCopySimpleResultSet);
				return bulkCopySimpleResultSet;
			}, TaskScheduler.Default);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0005CE10 File Offset: 0x0005B010
		private string AnalyzeTargetAndCreateUpdateBulkCommand(BulkCopySimpleResultSet internalResults)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (internalResults[2].Count == 0)
			{
				throw SQL.BulkLoadNoCollation();
			}
			stringBuilder.AppendFormat("insert bulk {0} (", this.DestinationTableName);
			int num = 0;
			int num2 = 0;
			if (this._connection.HasLocalTransaction && this._externalTransaction == null && this._internalTransaction == null && this._connection.Parser != null && this._connection.Parser.CurrentTransaction != null && this._connection.Parser.CurrentTransaction.IsLocal)
			{
				throw SQL.BulkLoadExistingTransaction();
			}
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._sortedColumnMappings = new List<_ColumnMapping>(metaData.Length);
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				bool flag = false;
				if (sqlMetaData.type == SqlDbType.Timestamp || (sqlMetaData.isIdentity && !this.IsCopyOption(SqlBulkCopyOptions.KeepIdentity)))
				{
					metaData[i] = null;
					flag = true;
				}
				int j = 0;
				while (j < this._localColumnMappings.Count)
				{
					if (this._localColumnMappings[j]._destinationColumnOrdinal == sqlMetaData.ordinal || this.UnquotedName(this._localColumnMappings[j]._destinationColumnName) == sqlMetaData.column)
					{
						if (flag)
						{
							num2++;
							break;
						}
						this._sortedColumnMappings.Add(new _ColumnMapping(this._localColumnMappings[j]._internalSourceColumnOrdinal, sqlMetaData));
						num++;
						if (num > 1)
						{
							stringBuilder.Append(", ");
						}
						if (sqlMetaData.type == SqlDbType.Variant)
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, "sql_variant");
						}
						else if (sqlMetaData.type == SqlDbType.Udt)
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, "varbinary");
						}
						else
						{
							this.AppendColumnNameAndTypeName(stringBuilder, sqlMetaData.column, sqlMetaData.type.ToString());
						}
						byte nullableType = sqlMetaData.metaType.NullableType;
						if (nullableType <= 106)
						{
							if (nullableType - 41 > 2)
							{
								if (nullableType != 106)
								{
									goto IL_299;
								}
								goto IL_215;
							}
							else
							{
								stringBuilder.AppendFormat(null, "({0})", sqlMetaData.scale);
							}
						}
						else
						{
							if (nullableType == 108)
							{
								goto IL_215;
							}
							if (nullableType != 240)
							{
								goto IL_299;
							}
							if (sqlMetaData.IsLargeUdt)
							{
								stringBuilder.Append("(max)");
							}
							else
							{
								int length = sqlMetaData.length;
								stringBuilder.AppendFormat(null, "({0})", length);
							}
						}
						IL_32A:
						object obj = internalResults[2][i][3];
						SqlDbType type = sqlMetaData.type;
						if (type <= SqlDbType.NVarChar)
						{
							if (type != SqlDbType.Char && type - SqlDbType.NChar > 2)
							{
								goto IL_36F;
							}
							goto IL_36A;
						}
						else
						{
							if (type == SqlDbType.Text || type == SqlDbType.VarChar)
							{
								goto IL_36A;
							}
							goto IL_36F;
						}
						IL_372:
						bool flag2;
						if (obj == null || !flag2)
						{
							break;
						}
						SqlString sqlString = (SqlString)obj;
						if (sqlString.IsNull)
						{
							break;
						}
						stringBuilder.Append(" COLLATE " + sqlString.Value);
						if (this._SqlDataReaderRowSource == null || sqlMetaData.collation == null)
						{
							break;
						}
						int internalSourceColumnOrdinal = this._localColumnMappings[j]._internalSourceColumnOrdinal;
						int lcid = sqlMetaData.collation.LCID;
						int localeId = this._SqlDataReaderRowSource.GetLocaleId(internalSourceColumnOrdinal);
						if (localeId != lcid)
						{
							throw SQL.BulkLoadLcidMismatch(localeId, this._SqlDataReaderRowSource.GetName(internalSourceColumnOrdinal), lcid, sqlMetaData.column);
						}
						break;
						IL_36F:
						flag2 = false;
						goto IL_372;
						IL_36A:
						flag2 = true;
						goto IL_372;
						IL_215:
						stringBuilder.AppendFormat(null, "({0},{1})", sqlMetaData.precision, sqlMetaData.scale);
						goto IL_32A;
						IL_299:
						if (!sqlMetaData.metaType.IsFixed && !sqlMetaData.metaType.IsLong)
						{
							int num3 = sqlMetaData.length;
							byte nullableType2 = sqlMetaData.metaType.NullableType;
							if (nullableType2 == 99 || nullableType2 == 231 || nullableType2 == 239)
							{
								num3 /= 2;
							}
							stringBuilder.AppendFormat(null, "({0})", num3);
							goto IL_32A;
						}
						if (sqlMetaData.metaType.IsPlp && sqlMetaData.metaType.SqlDbType != SqlDbType.Xml)
						{
							stringBuilder.Append("(max)");
							goto IL_32A;
						}
						goto IL_32A;
					}
					else
					{
						j++;
					}
				}
				if (j == this._localColumnMappings.Count)
				{
					metaData[i] = null;
				}
			}
			if (num + num2 != this._localColumnMappings.Count)
			{
				throw SQL.BulkLoadNonMatchingColumnMapping();
			}
			stringBuilder.Append(")");
			if ((this._copyOptions & (SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.KeepNulls | SqlBulkCopyOptions.FireTriggers)) != SqlBulkCopyOptions.Default)
			{
				bool flag3 = false;
				stringBuilder.Append(" with (");
				if (this.IsCopyOption(SqlBulkCopyOptions.KeepNulls))
				{
					stringBuilder.Append("KEEP_NULLS");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.TableLock))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "TABLOCK");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.CheckConstraints))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "CHECK_CONSTRAINTS");
					flag3 = true;
				}
				if (this.IsCopyOption(SqlBulkCopyOptions.FireTriggers))
				{
					stringBuilder.Append((flag3 ? ", " : "") + "FIRE_TRIGGERS");
				}
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0005D368 File Offset: 0x0005B568
		private Task SubmitUpdateBulkCommand(string TDSCommand)
		{
			Task task = this._parser.TdsExecuteSQLBatch(TDSCommand, this.BulkCopyTimeout, null, this._stateObj, !this._isAsyncBulkCopy, true);
			if (task == null)
			{
				this.RunParser(null);
				return null;
			}
			return task.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					throw t.Exception.InnerException;
				}
				this.RunParserReliably(null);
			}, TaskScheduler.Default);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0005D3BC File Offset: 0x0005B5BC
		private void WriteMetaData(BulkCopySimpleResultSet internalResults)
		{
			this._stateObj.SetTimeoutSeconds(this.BulkCopyTimeout);
			_SqlMetaDataSet metaData = internalResults[1].MetaData;
			this._stateObj._outputMessageType = 7;
			this._parser.WriteBulkCopyMetaData(metaData, this._sortedColumnMappings.Count, this._stateObj);
		}

		/// <summary>Closes the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance.</summary>
		// Token: 0x06001496 RID: 5270 RVA: 0x0005D410 File Offset: 0x0005B610
		public void Close()
		{
			if (this._insideRowsCopiedEvent)
			{
				throw SQL.InvalidOperationInsideEvent();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0005D430 File Offset: 0x0005B630
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._columnMappings = null;
				this._parser = null;
				try
				{
					if (this._internalTransaction != null)
					{
						this._internalTransaction.Rollback();
						this._internalTransaction.Dispose();
						this._internalTransaction = null;
					}
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
				}
				finally
				{
					if (this._connection != null)
					{
						if (this._ownConnection)
						{
							this._connection.Dispose();
						}
						this._connection = null;
					}
				}
			}
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0005D4C0 File Offset: 0x0005B6C0
		private object GetValueFromSourceRow(int destRowIndex, out bool isSqlType, out bool isDataFeed, out bool isNull)
		{
			_SqlMetaData metadata = this._sortedColumnMappings[destRowIndex]._metadata;
			int sourceColumnOrdinal = this._sortedColumnMappings[destRowIndex]._sourceColumnOrdinal;
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			case SqlBulkCopy.ValueSourceType.DbDataReader:
				if (this._currentRowMetadata[destRowIndex].IsDataFeed)
				{
					if (this._DbDataReaderRowSource.IsDBNull(sourceColumnOrdinal))
					{
						isSqlType = false;
						isDataFeed = false;
						isNull = true;
						return DBNull.Value;
					}
					isSqlType = false;
					isDataFeed = true;
					isNull = false;
					switch (this._currentRowMetadata[destRowIndex].Method)
					{
					case SqlBulkCopy.ValueMethod.DataFeedStream:
						return new StreamDataFeed(this._DbDataReaderRowSource.GetStream(sourceColumnOrdinal));
					case SqlBulkCopy.ValueMethod.DataFeedText:
						return new TextDataFeed(this._DbDataReaderRowSource.GetTextReader(sourceColumnOrdinal));
					case SqlBulkCopy.ValueMethod.DataFeedXml:
						return new XmlDataFeed(this._SqlDataReaderRowSource.GetXmlReader(sourceColumnOrdinal));
					default:
					{
						isDataFeed = false;
						object value = this._DbDataReaderRowSource.GetValue(sourceColumnOrdinal);
						ADP.IsNullOrSqlType(value, out isNull, out isSqlType);
						return value;
					}
					}
				}
				else if (this._SqlDataReaderRowSource != null)
				{
					if (this._currentRowMetadata[destRowIndex].IsSqlType)
					{
						isSqlType = true;
						isDataFeed = false;
						INullable nullable;
						switch (this._currentRowMetadata[destRowIndex].Method)
						{
						case SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal:
							nullable = this._SqlDataReaderRowSource.GetSqlDecimal(sourceColumnOrdinal);
							break;
						case SqlBulkCopy.ValueMethod.SqlTypeSqlDouble:
							nullable = new SqlDecimal(this._SqlDataReaderRowSource.GetSqlDouble(sourceColumnOrdinal).Value);
							break;
						case SqlBulkCopy.ValueMethod.SqlTypeSqlSingle:
							nullable = new SqlDecimal((double)this._SqlDataReaderRowSource.GetSqlSingle(sourceColumnOrdinal).Value);
							break;
						default:
							nullable = (INullable)this._SqlDataReaderRowSource.GetSqlValue(sourceColumnOrdinal);
							break;
						}
						isNull = nullable.IsNull;
						return nullable;
					}
					isSqlType = false;
					isDataFeed = false;
					object value2 = this._SqlDataReaderRowSource.GetValue(sourceColumnOrdinal);
					isNull = (value2 == null || value2 == DBNull.Value);
					if (!isNull && metadata.type == SqlDbType.Udt)
					{
						INullable nullable2 = value2 as INullable;
						isNull = (nullable2 != null && nullable2.IsNull);
					}
					return value2;
				}
				else
				{
					isDataFeed = false;
					IDataReader dataReader = (IDataReader)this._rowSource;
					if (this._enableStreaming && this._SqlDataReaderRowSource == null && dataReader.IsDBNull(sourceColumnOrdinal))
					{
						isSqlType = false;
						isNull = true;
						return DBNull.Value;
					}
					object value3 = dataReader.GetValue(sourceColumnOrdinal);
					ADP.IsNullOrSqlType(value3, out isNull, out isSqlType);
					return value3;
				}
				break;
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
			{
				isDataFeed = false;
				object obj = this._currentRow[sourceColumnOrdinal];
				ADP.IsNullOrSqlType(obj, out isNull, out isSqlType);
				if (!isNull && this._currentRowMetadata[destRowIndex].IsSqlType)
				{
					switch (this._currentRowMetadata[destRowIndex].Method)
					{
					case SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal:
						if (isSqlType)
						{
							return (SqlDecimal)obj;
						}
						isSqlType = true;
						return new SqlDecimal((decimal)obj);
					case SqlBulkCopy.ValueMethod.SqlTypeSqlDouble:
					{
						if (isSqlType)
						{
							return new SqlDecimal(((SqlDouble)obj).Value);
						}
						double num = (double)obj;
						if (!double.IsNaN(num))
						{
							isSqlType = true;
							return new SqlDecimal(num);
						}
						break;
					}
					case SqlBulkCopy.ValueMethod.SqlTypeSqlSingle:
					{
						if (isSqlType)
						{
							return new SqlDecimal((double)((SqlSingle)obj).Value);
						}
						float num2 = (float)obj;
						if (!float.IsNaN(num2))
						{
							isSqlType = true;
							return new SqlDecimal((double)num2);
						}
						break;
					}
					}
				}
				return obj;
			}
			default:
				throw ADP.NotSupported();
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0005D848 File Offset: 0x0005BA48
		private Task ReadFromRowSourceAsync(CancellationToken cts)
		{
			if (this._isAsyncBulkCopy && this._DbDataReaderRowSource != null)
			{
				return this._DbDataReaderRowSource.ReadAsync(cts).ContinueWith<Task<bool>>(delegate(Task<bool> t)
				{
					if (t.Status == TaskStatus.RanToCompletion)
					{
						this._hasMoreRowToCopy = t.Result;
					}
					return t;
				}, TaskScheduler.Default).Unwrap<bool>();
			}
			this._hasMoreRowToCopy = false;
			try
			{
				this._hasMoreRowToCopy = this.ReadFromRowSource();
			}
			catch (Exception exception)
			{
				if (this._isAsyncBulkCopy)
				{
					return Task.FromException<bool>(exception);
				}
				throw;
			}
			return null;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0005D8CC File Offset: 0x0005BACC
		private bool ReadFromRowSource()
		{
			switch (this._rowSourceType)
			{
			case SqlBulkCopy.ValueSourceType.IDataReader:
			case SqlBulkCopy.ValueSourceType.DbDataReader:
				return ((IDataReader)this._rowSource).Read();
			case SqlBulkCopy.ValueSourceType.DataTable:
			case SqlBulkCopy.ValueSourceType.RowArray:
				while (this._rowEnumerator.MoveNext())
				{
					this._currentRow = (DataRow)this._rowEnumerator.Current;
					if ((this._currentRow.RowState & this._rowStateToSkip) == (DataRowState)0)
					{
						this._currentRowLength = this._currentRow.ItemArray.Length;
						return true;
					}
				}
				return false;
			default:
				throw ADP.NotSupported();
			}
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0005D960 File Offset: 0x0005BB60
		private SqlBulkCopy.SourceColumnMetadata GetColumnMetadata(int ordinal)
		{
			int sourceColumnOrdinal = this._sortedColumnMappings[ordinal]._sourceColumnOrdinal;
			_SqlMetaData metadata = this._sortedColumnMappings[ordinal]._metadata;
			bool isDataFeed;
			bool isSqlType;
			SqlBulkCopy.ValueMethod method;
			if ((this._SqlDataReaderRowSource != null || this._dataTableSource != null) && (metadata.metaType.NullableType == 106 || metadata.metaType.NullableType == 108))
			{
				isDataFeed = false;
				Type right;
				switch (this._rowSourceType)
				{
				case SqlBulkCopy.ValueSourceType.IDataReader:
				case SqlBulkCopy.ValueSourceType.DbDataReader:
					right = this._SqlDataReaderRowSource.GetFieldType(sourceColumnOrdinal);
					break;
				case SqlBulkCopy.ValueSourceType.DataTable:
				case SqlBulkCopy.ValueSourceType.RowArray:
					right = this._dataTableSource.Columns[sourceColumnOrdinal].DataType;
					break;
				default:
					right = null;
					break;
				}
				if (typeof(SqlDecimal) == right || typeof(decimal) == right)
				{
					isSqlType = true;
					method = SqlBulkCopy.ValueMethod.SqlTypeSqlDecimal;
				}
				else if (typeof(SqlDouble) == right || typeof(double) == right)
				{
					isSqlType = true;
					method = SqlBulkCopy.ValueMethod.SqlTypeSqlDouble;
				}
				else if (typeof(SqlSingle) == right || typeof(float) == right)
				{
					isSqlType = true;
					method = SqlBulkCopy.ValueMethod.SqlTypeSqlSingle;
				}
				else
				{
					isSqlType = false;
					method = SqlBulkCopy.ValueMethod.GetValue;
				}
			}
			else if (this._enableStreaming && metadata.length == 2147483647)
			{
				isSqlType = false;
				if (this._SqlDataReaderRowSource != null)
				{
					MetaType metaType = this._SqlDataReaderRowSource.MetaData[sourceColumnOrdinal].metaType;
					if (metadata.type == SqlDbType.VarBinary && metaType.IsBinType && metaType.SqlDbType != SqlDbType.Timestamp && this._SqlDataReaderRowSource.IsCommandBehavior(CommandBehavior.SequentialAccess))
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedStream;
					}
					else if ((metadata.type == SqlDbType.VarChar || metadata.type == SqlDbType.NVarChar) && metaType.IsCharType && metaType.SqlDbType != SqlDbType.Xml)
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedText;
					}
					else if (metadata.type == SqlDbType.Xml && metaType.SqlDbType == SqlDbType.Xml)
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedXml;
					}
					else
					{
						isDataFeed = false;
						method = SqlBulkCopy.ValueMethod.GetValue;
					}
				}
				else if (this._DbDataReaderRowSource != null)
				{
					if (metadata.type == SqlDbType.VarBinary)
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedStream;
					}
					else if (metadata.type == SqlDbType.VarChar || metadata.type == SqlDbType.NVarChar)
					{
						isDataFeed = true;
						method = SqlBulkCopy.ValueMethod.DataFeedText;
					}
					else
					{
						isDataFeed = false;
						method = SqlBulkCopy.ValueMethod.GetValue;
					}
				}
				else
				{
					isDataFeed = false;
					method = SqlBulkCopy.ValueMethod.GetValue;
				}
			}
			else
			{
				isSqlType = false;
				isDataFeed = false;
				method = SqlBulkCopy.ValueMethod.GetValue;
			}
			return new SqlBulkCopy.SourceColumnMetadata(method, isSqlType, isDataFeed);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0005DBCC File Offset: 0x0005BDCC
		private void CreateOrValidateConnection(string method)
		{
			if (this._connection == null)
			{
				throw ADP.ConnectionRequired(method);
			}
			if (this._ownConnection && this._connection.State != ConnectionState.Open)
			{
				this._connection.Open();
			}
			this._connection.ValidateConnectionForExecute(method, null);
			if (this._externalTransaction != null && this._connection != this._externalTransaction.Connection)
			{
				throw ADP.TransactionConnectionMismatch();
			}
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0005DC38 File Offset: 0x0005BE38
		private void RunParser(BulkCopySimpleResultSet bulkCopyHandler = null)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			openTdsConnection.ThreadHasParserLockForClose = true;
			try
			{
				this._parser.Run(RunBehavior.UntilDone, null, null, bulkCopyHandler, this._stateObj);
			}
			finally
			{
				openTdsConnection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0005DC88 File Offset: 0x0005BE88
		private void RunParserReliably(BulkCopySimpleResultSet bulkCopyHandler = null)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			openTdsConnection.ThreadHasParserLockForClose = true;
			try
			{
				this._parser.Run(RunBehavior.UntilDone, null, null, bulkCopyHandler, this._stateObj);
			}
			finally
			{
				openTdsConnection.ThreadHasParserLockForClose = false;
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0005DCD8 File Offset: 0x0005BED8
		private void CommitTransaction()
		{
			if (this._internalTransaction != null)
			{
				SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
				openTdsConnection.ThreadHasParserLockForClose = true;
				try
				{
					this._internalTransaction.Commit();
					this._internalTransaction.Dispose();
					this._internalTransaction = null;
				}
				finally
				{
					openTdsConnection.ThreadHasParserLockForClose = false;
				}
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0005DD38 File Offset: 0x0005BF38
		private void AbortTransaction()
		{
			if (this._internalTransaction != null)
			{
				if (!this._internalTransaction.IsZombied)
				{
					SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
					openTdsConnection.ThreadHasParserLockForClose = true;
					try
					{
						this._internalTransaction.Rollback();
					}
					finally
					{
						openTdsConnection.ThreadHasParserLockForClose = false;
					}
				}
				this._internalTransaction.Dispose();
				this._internalTransaction = null;
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0005DDA4 File Offset: 0x0005BFA4
		private void AppendColumnNameAndTypeName(StringBuilder query, string columnName, string typeName)
		{
			SqlServerEscapeHelper.EscapeIdentifier(query, columnName);
			query.Append(" ");
			query.Append(typeName);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0005DDC4 File Offset: 0x0005BFC4
		private string UnquotedName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (name[0] == '[')
			{
				int length = name.Length;
				name = name.Substring(1, length - 2);
			}
			return name;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0005DDFC File Offset: 0x0005BFFC
		private object ValidateBulkCopyVariant(object value)
		{
			byte tdstype = MetaType.GetMetaTypeFromValue(value, true).TDSType;
			if (tdstype <= 108)
			{
				if (tdstype <= 43)
				{
					if (tdstype != 36 && tdstype - 40 > 3)
					{
						goto IL_AC;
					}
				}
				else
				{
					switch (tdstype)
					{
					case 48:
					case 50:
					case 52:
					case 56:
					case 59:
					case 60:
					case 61:
					case 62:
						break;
					case 49:
					case 51:
					case 53:
					case 54:
					case 55:
					case 57:
					case 58:
						goto IL_AC;
					default:
						if (tdstype != 108)
						{
							goto IL_AC;
						}
						break;
					}
				}
			}
			else if (tdstype <= 165)
			{
				if (tdstype != 127 && tdstype != 165)
				{
					goto IL_AC;
				}
			}
			else if (tdstype != 167 && tdstype != 231)
			{
				goto IL_AC;
			}
			if (value is INullable)
			{
				return MetaType.GetComValueFromSqlVariant(value);
			}
			return value;
			IL_AC:
			throw SQL.BulkLoadInvalidVariantValue();
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0005DEBC File Offset: 0x0005C0BC
		private object ConvertValue(object value, _SqlMetaData metadata, bool isNull, ref bool isSqlType, out bool coercedToDataFeed)
		{
			coercedToDataFeed = false;
			if (!isNull)
			{
				MetaType metaType = metadata.metaType;
				bool flag = false;
				object result;
				try
				{
					byte nullableType = metaType.NullableType;
					MetaType metaTypeFromSqlDbType;
					if (nullableType <= 165)
					{
						if (nullableType <= 59)
						{
							switch (nullableType)
							{
							case 34:
							case 35:
							case 36:
							case 38:
							case 40:
							case 41:
							case 42:
							case 43:
							case 50:
								break;
							case 37:
							case 39:
							case 44:
							case 45:
							case 46:
							case 47:
							case 48:
							case 49:
								goto IL_2B9;
							default:
								if (nullableType - 58 > 1)
								{
									goto IL_2B9;
								}
								break;
							}
						}
						else if (nullableType - 61 > 1)
						{
							switch (nullableType)
							{
							case 98:
								value = this.ValidateBulkCopyVariant(value);
								flag = true;
								goto IL_2CC;
							case 99:
								goto IL_219;
							case 100:
							case 101:
							case 102:
							case 103:
							case 105:
							case 107:
								goto IL_2B9;
							case 104:
							case 109:
							case 110:
							case 111:
								break;
							case 106:
							case 108:
							{
								metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
								value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType, out coercedToDataFeed, out flag, false);
								SqlDecimal sqlDecimal;
								if (isSqlType && !flag)
								{
									sqlDecimal = (SqlDecimal)value;
								}
								else
								{
									sqlDecimal = new SqlDecimal((decimal)value);
								}
								if (sqlDecimal.Scale != metadata.scale)
								{
									sqlDecimal = TdsParser.AdjustSqlDecimalScale(sqlDecimal, (int)metadata.scale);
								}
								if (sqlDecimal.Precision > metadata.precision)
								{
									try
									{
										sqlDecimal = SqlDecimal.ConvertToPrecScale(sqlDecimal, (int)metadata.precision, (int)sqlDecimal.Scale);
									}
									catch (SqlTruncateException)
									{
										throw SQL.BulkLoadCannotConvertValue(value.GetType(), metaTypeFromSqlDbType, ADP.ParameterValueOutOfRange(sqlDecimal));
									}
								}
								value = sqlDecimal;
								isSqlType = true;
								flag = false;
								goto IL_2CC;
							}
							default:
								if (nullableType != 165)
								{
									goto IL_2B9;
								}
								break;
							}
						}
					}
					else if (nullableType <= 173)
					{
						if (nullableType != 167 && nullableType != 173)
						{
							goto IL_2B9;
						}
					}
					else if (nullableType != 175)
					{
						if (nullableType == 231)
						{
							goto IL_219;
						}
						switch (nullableType)
						{
						case 239:
							goto IL_219;
						case 240:
							if (!(value is byte[]))
							{
								value = this._connection.GetBytes(value);
								flag = true;
								goto IL_2CC;
							}
							goto IL_2CC;
						case 241:
							if (value is XmlReader)
							{
								value = new XmlDataFeed((XmlReader)value);
								flag = true;
								coercedToDataFeed = true;
								goto IL_2CC;
							}
							goto IL_2CC;
						default:
							goto IL_2B9;
						}
					}
					metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType, out coercedToDataFeed, out flag, false);
					goto IL_2CC;
					IL_219:
					metaTypeFromSqlDbType = MetaType.GetMetaTypeFromSqlDbType(metaType.SqlDbType, false);
					value = SqlParameter.CoerceValue(value, metaTypeFromSqlDbType, out coercedToDataFeed, out flag, false);
					if (!coercedToDataFeed && ((isSqlType && !flag) ? ((SqlString)value).Value.Length : ((string)value).Length) > metadata.length / 2)
					{
						throw SQL.BulkLoadStringTooLong();
					}
					goto IL_2CC;
					IL_2B9:
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metadata.metaType, null);
					IL_2CC:
					if (flag)
					{
						isSqlType = false;
					}
					result = value;
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					throw SQL.BulkLoadCannotConvertValue(value.GetType(), metadata.metaType, e);
				}
				return result;
			}
			if (!metadata.isNullable)
			{
				throw SQL.BulkLoadBulkLoadNotAllowDBNull(metadata.column);
			}
			return value;
		}

		/// <summary>Copies all rows from the supplied <see cref="T:System.Data.Common.DbDataReader" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="reader">A <see cref="T:System.Data.Common.DbDataReader" /> whose rows will be copied to the destination table.</param>
		// Token: 0x060014A5 RID: 5285 RVA: 0x0005E1F8 File Offset: 0x0005C3F8
		public void WriteToServer(DbDataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._DbDataReaderRowSource = reader;
				this._SqlDataReaderRowSource = (reader as SqlDataReader);
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DbDataReader;
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(reader.FieldCount, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		/// <summary>Copies all rows in the supplied <see cref="T:System.Data.IDataReader" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> whose rows will be copied to the destination table.</param>
		// Token: 0x060014A6 RID: 5286 RVA: 0x0005E290 File Offset: 0x0005C490
		public void WriteToServer(IDataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = (this._rowSource as SqlDataReader);
				this._DbDataReaderRowSource = (this._rowSource as DbDataReader);
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(reader.FieldCount, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		/// <summary>Copies all rows in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		// Token: 0x060014A7 RID: 5287 RVA: 0x0005E338 File Offset: 0x0005C538
		public void WriteToServer(DataTable table)
		{
			this.WriteToServer(table, (DataRowState)0);
		}

		/// <summary>Copies only rows that match the supplied row state in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="rowState">A value from the <see cref="T:System.Data.DataRowState" /> enumeration. Only rows matching the row state are copied to the destination.</param>
		// Token: 0x060014A8 RID: 5288 RVA: 0x0005E344 File Offset: 0x0005C544
		public void WriteToServer(DataTable table, DataRowState rowState)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowStateToSkip = ((rowState == (DataRowState)0 || rowState == DataRowState.Deleted) ? DataRowState.Deleted : (~rowState | DataRowState.Deleted));
				this._rowSource = table;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(table.Columns.Count, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		/// <summary>Copies all rows from the supplied <see cref="T:System.Data.DataRow" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="rows">An array of <see cref="T:System.Data.DataRow" /> objects that will be copied to the destination table.</param>
		// Token: 0x060014A9 RID: 5289 RVA: 0x0005E3F8 File Offset: 0x0005C5F8
		public void WriteToServer(DataRow[] rows)
		{
			SqlStatistics statistics = this.Statistics;
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			if (rows.Length == 0)
			{
				return;
			}
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				DataTable table = rows[0].Table;
				this._rowStateToSkip = DataRowState.Deleted;
				this._rowSource = rows;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
				this._rowEnumerator = rows.GetEnumerator();
				this._isAsyncBulkCopy = false;
				this.WriteRowSourceToServerAsync(table.Columns.Count, CancellationToken.None);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataRow[])" />, which copies all rows from the supplied <see cref="T:System.Data.DataRow" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="rows">An array of <see cref="T:System.Data.DataRow" /> objects that will be copied to the destination table.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> multiple times for the same instance before task completion.  
		///  Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataRow[])" /> for the same instance before task completion.  
		///  The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> execution.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.  
		///  Returned in the task object, there was a connection pool timeout.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060014AA RID: 5290 RVA: 0x0005E4AC File Offset: 0x0005C6AC
		public Task WriteToServerAsync(DataRow[] rows)
		{
			return this.WriteToServerAsync(rows, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataRow[])" />, which copies all rows from the supplied <see cref="T:System.Data.DataRow" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.  
		///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <param name="rows">An array of <see cref="T:System.Data.DataRow" /> objects that will be copied to the destination table.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" />.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> multiple times for the same instance before task completion.  
		///  Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataRow[])" /> for the same instance before task completion.  
		///  The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataRow[])" /> execution.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.  
		///  Returned in the task object, there was a connection pool timeout.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060014AB RID: 5291 RVA: 0x0005E4BC File Offset: 0x0005C6BC
		public Task WriteToServerAsync(DataRow[] rows, CancellationToken cancellationToken)
		{
			Task result = null;
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (rows.Length == 0)
				{
					return cancellationToken.IsCancellationRequested ? Task.FromCanceled(cancellationToken) : Task.CompletedTask;
				}
				DataTable table = rows[0].Table;
				this._rowStateToSkip = DataRowState.Deleted;
				this._rowSource = rows;
				this._dataTableSource = table;
				this._SqlDataReaderRowSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.RowArray;
				this._rowEnumerator = rows.GetEnumerator();
				this._isAsyncBulkCopy = true;
				result = this.WriteRowSourceToServerAsync(table.Columns.Count, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.Common.DbDataReader)" />, which copies all rows from the supplied <see cref="T:System.Data.Common.DbDataReader" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="reader">A <see cref="T:System.Data.Common.DbDataReader" /> whose rows will be copied to the destination table.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		// Token: 0x060014AC RID: 5292 RVA: 0x0005E588 File Offset: 0x0005C788
		public Task WriteToServerAsync(DbDataReader reader)
		{
			return this.WriteToServerAsync(reader, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.Common.DbDataReader)" />, which copies all rows from the supplied <see cref="T:System.Data.Common.DbDataReader" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="reader">A <see cref="T:System.Data.Common.DbDataReader" /> whose rows will be copied to the destination table.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.Common.DbDataReader)" />.</param>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task" />.</returns>
		// Token: 0x060014AD RID: 5293 RVA: 0x0005E598 File Offset: 0x0005C798
		public Task WriteToServerAsync(DbDataReader reader, CancellationToken cancellationToken)
		{
			Task result = null;
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = (reader as SqlDataReader);
				this._DbDataReaderRowSource = reader;
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DbDataReader;
				this._isAsyncBulkCopy = true;
				result = this.WriteRowSourceToServerAsync(reader.FieldCount, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.IDataReader)" />, which copies all rows in the supplied <see cref="T:System.Data.IDataReader" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> whose rows will be copied to the destination table.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> multiple times for the same instance before task completion.  
		///  Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.IDataReader)" /> for the same instance before task completion.  
		///  The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> execution.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.  
		///  Returned in the task object, there was a connection pool timeout.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.  
		///  The <see cref="T:System.Data.IDataReader" /> was closed before the completed <see cref="T:System.Threading.Tasks.Task" /> returned.  
		///  The <see cref="T:System.Data.IDataReader" />'s associated connection was closed before the completed <see cref="T:System.Threading.Tasks.Task" /> returned.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060014AE RID: 5294 RVA: 0x0005E630 File Offset: 0x0005C830
		public Task WriteToServerAsync(IDataReader reader)
		{
			return this.WriteToServerAsync(reader, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.IDataReader)" />, which copies all rows in the supplied <see cref="T:System.Data.IDataReader" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.  
		///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> whose rows will be copied to the destination table.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" />.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> multiple times for the same instance before task completion.  
		///  Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.IDataReader)" /> for the same instance before task completion.  
		///  The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.IDataReader)" /> execution.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.  
		///  Returned in the task object, there was a connection pool timeout.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.  
		///  The <see cref="T:System.Data.IDataReader" /> was closed before the completed <see cref="T:System.Threading.Tasks.Task" /> returned.  
		///  The <see cref="T:System.Data.IDataReader" />'s associated connection was closed before the completed <see cref="T:System.Threading.Tasks.Task" /> returned.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060014AF RID: 5295 RVA: 0x0005E640 File Offset: 0x0005C840
		public Task WriteToServerAsync(IDataReader reader, CancellationToken cancellationToken)
		{
			Task result = null;
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowSource = reader;
				this._SqlDataReaderRowSource = (this._rowSource as SqlDataReader);
				this._DbDataReaderRowSource = (this._rowSource as DbDataReader);
				this._dataTableSource = null;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.IDataReader;
				this._isAsyncBulkCopy = true;
				result = this.WriteRowSourceToServerAsync(reader.FieldCount, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable)" />, which copies all rows in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> multiple times for the same instance before task completion.  
		///  Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable)" /> for the same instance before task completion.  
		///  The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> execution.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.  
		///  Returned in the task object, there was a connection pool timeout.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060014B0 RID: 5296 RVA: 0x0005E6E4 File Offset: 0x0005C8E4
		public Task WriteToServerAsync(DataTable table)
		{
			return this.WriteToServerAsync(table, (DataRowState)0, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable)" />, which copies all rows in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.  
		///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" />.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> multiple times for the same instance before task completion.  
		///  Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable)" /> for the same instance before task completion.  
		///  The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" /> execution.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.  
		///  Returned in the task object, there was a connection pool timeout.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060014B1 RID: 5297 RVA: 0x0005E6F3 File Offset: 0x0005C8F3
		public Task WriteToServerAsync(DataTable table, CancellationToken cancellationToken)
		{
			return this.WriteToServerAsync(table, (DataRowState)0, cancellationToken);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable,System.Data.DataRowState)" />, which copies only rows that match the supplied row state in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="rowState">A value from the <see cref="T:System.Data.DataRowState" /> enumeration. Only rows matching the row state are copied to the destination.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> multiple times for the same instance before task completion.  
		///  Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable,System.Data.DataRowState)" /> for the same instance before task completion.  
		///  The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> execution.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.  
		///  Returned in the task object, there was a connection pool timeout.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060014B2 RID: 5298 RVA: 0x0005E6FE File Offset: 0x0005C8FE
		public Task WriteToServerAsync(DataTable table, DataRowState rowState)
		{
			return this.WriteToServerAsync(table, rowState, CancellationToken.None);
		}

		/// <summary>The asynchronous version of <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable,System.Data.DataRowState)" />, which copies only rows that match the supplied row state in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.  
		///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="rowState">A value from the <see cref="T:System.Data.DataRowState" /> enumeration. Only rows matching the row state are copied to the destination.</param>
		/// <param name="cancellationToken">The cancellation instruction. A <see cref="P:System.Threading.CancellationToken.None" /> value in this parameter makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable)" />.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> multiple times for the same instance before task completion.  
		///  Calling <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> and <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServer(System.Data.DataTable,System.Data.DataRowState)" /> for the same instance before task completion.  
		///  The connection drops or is closed during <see cref="M:System.Data.SqlClient.SqlBulkCopy.WriteToServerAsync(System.Data.DataTable,System.Data.DataRowState)" /> execution.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object was closed during the method execution.  
		///  Returned in the task object, there was a connection pool timeout.  
		///  Returned in the task object, the <see cref="T:System.Data.SqlClient.SqlConnection" /> object is closed before method execution.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Returned in the task object, any error returned by SQL Server that occurred while opening the connection.</exception>
		// Token: 0x060014B3 RID: 5299 RVA: 0x0005E710 File Offset: 0x0005C910
		public Task WriteToServerAsync(DataTable table, DataRowState rowState, CancellationToken cancellationToken)
		{
			Task result = null;
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this._isBulkCopyingInProgress)
			{
				throw SQL.BulkLoadPendingOperation();
			}
			SqlStatistics statistics = this.Statistics;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this._rowStateToSkip = ((rowState == (DataRowState)0 || rowState == DataRowState.Deleted) ? DataRowState.Deleted : (~rowState | DataRowState.Deleted));
				this._rowSource = table;
				this._SqlDataReaderRowSource = null;
				this._dataTableSource = table;
				this._rowSourceType = SqlBulkCopy.ValueSourceType.DataTable;
				this._rowEnumerator = table.Rows.GetEnumerator();
				this._isAsyncBulkCopy = true;
				result = this.WriteRowSourceToServerAsync(table.Columns.Count, cancellationToken);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005E7C4 File Offset: 0x0005C9C4
		private Task WriteRowSourceToServerAsync(int columnCount, CancellationToken ctoken)
		{
			Task currentReconnectionTask = this._connection._currentReconnectionTask;
			if (currentReconnectionTask != null && !currentReconnectionTask.IsCompleted)
			{
				if (this._isAsyncBulkCopy)
				{
					TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
					Action <>9__2;
					currentReconnectionTask.ContinueWith(delegate(Task t)
					{
						Task task2 = this.WriteRowSourceToServerAsync(columnCount, ctoken);
						TaskCompletionSource<object> tcs;
						if (task2 == null)
						{
							tcs.SetResult(null);
							return;
						}
						Task task3 = task2;
						tcs = tcs;
						Action onSuccess;
						if ((onSuccess = <>9__2) == null)
						{
							onSuccess = (<>9__2 = delegate()
							{
								tcs.SetResult(null);
							});
						}
						AsyncHelper.ContinueTask(task3, tcs, onSuccess, null, null, null, null, null);
					}, ctoken);
					return tcs.Task;
				}
				AsyncHelper.WaitForCompletion(currentReconnectionTask, this.BulkCopyTimeout, delegate
				{
					throw SQL.CR_ReconnectTimeout();
				}, false);
			}
			bool flag = true;
			this._isBulkCopyingInProgress = true;
			this.CreateOrValidateConnection("WriteToServer");
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			this._parserLock = openTdsConnection._parserLock;
			this._parserLock.Wait(this._isAsyncBulkCopy);
			Task result;
			try
			{
				this.WriteRowSourceToServerCommon(columnCount);
				Task task = this.WriteToServerInternalAsync(ctoken);
				if (task != null)
				{
					flag = false;
					result = task.ContinueWith<Task>(delegate(Task t)
					{
						try
						{
							this.AbortTransaction();
						}
						finally
						{
							this._isBulkCopyingInProgress = false;
							if (this._parser != null)
							{
								this._parser._asyncWrite = false;
							}
							if (this._parserLock != null)
							{
								this._parserLock.Release();
								this._parserLock = null;
							}
						}
						return t;
					}, TaskScheduler.Default).Unwrap();
				}
				else
				{
					result = null;
				}
			}
			catch (OutOfMemoryException e)
			{
				this._connection.Abort(e);
				throw;
			}
			catch (StackOverflowException e2)
			{
				this._connection.Abort(e2);
				throw;
			}
			catch (ThreadAbortException e3)
			{
				this._connection.Abort(e3);
				throw;
			}
			finally
			{
				this._columnMappings.ReadOnly = false;
				if (flag)
				{
					try
					{
						this.AbortTransaction();
					}
					finally
					{
						this._isBulkCopyingInProgress = false;
						if (this._parser != null)
						{
							this._parser._asyncWrite = false;
						}
						if (this._parserLock != null)
						{
							this._parserLock.Release();
							this._parserLock = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0005E9D0 File Offset: 0x0005CBD0
		private void WriteRowSourceToServerCommon(int columnCount)
		{
			bool flag = false;
			this._columnMappings.ReadOnly = true;
			this._localColumnMappings = this._columnMappings;
			if (this._localColumnMappings.Count > 0)
			{
				this._localColumnMappings.ValidateCollection();
				using (IEnumerator enumerator = this._localColumnMappings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((SqlBulkCopyColumnMapping)enumerator.Current)._internalSourceColumnOrdinal == -1)
						{
							flag = true;
							break;
						}
					}
					goto IL_8A;
				}
			}
			this._localColumnMappings = new SqlBulkCopyColumnMappingCollection();
			this._localColumnMappings.CreateDefaultMapping(columnCount);
			IL_8A:
			if (flag)
			{
				int num = -1;
				flag = false;
				if (this._localColumnMappings.Count > 0)
				{
					foreach (object obj in this._localColumnMappings)
					{
						SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
						if (sqlBulkCopyColumnMapping._internalSourceColumnOrdinal == -1)
						{
							string text = this.UnquotedName(sqlBulkCopyColumnMapping.SourceColumn);
							switch (this._rowSourceType)
							{
							case SqlBulkCopy.ValueSourceType.IDataReader:
							case SqlBulkCopy.ValueSourceType.DbDataReader:
								try
								{
									num = ((IDataReader)this._rowSource).GetOrdinal(text);
								}
								catch (IndexOutOfRangeException e)
								{
									throw SQL.BulkLoadNonMatchingColumnName(text, e);
								}
								break;
							case SqlBulkCopy.ValueSourceType.DataTable:
								num = ((DataTable)this._rowSource).Columns.IndexOf(text);
								break;
							case SqlBulkCopy.ValueSourceType.RowArray:
								num = ((DataRow[])this._rowSource)[0].Table.Columns.IndexOf(text);
								break;
							}
							if (num == -1)
							{
								throw SQL.BulkLoadNonMatchingColumnName(text);
							}
							sqlBulkCopyColumnMapping._internalSourceColumnOrdinal = num;
						}
					}
				}
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0005EB98 File Offset: 0x0005CD98
		internal void OnConnectionClosed()
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.OnConnectionClosed();
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0005EBB8 File Offset: 0x0005CDB8
		private void OnRowsCopied(SqlRowsCopiedEventArgs value)
		{
			SqlRowsCopiedEventHandler rowsCopiedEventHandler = this._rowsCopiedEventHandler;
			if (rowsCopiedEventHandler != null)
			{
				rowsCopiedEventHandler(this, value);
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0005EBD8 File Offset: 0x0005CDD8
		private bool FireRowsCopiedEvent(long rowsCopied)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			bool canBeReleasedFromAnyThread = openTdsConnection._parserLock.CanBeReleasedFromAnyThread;
			openTdsConnection._parserLock.Release();
			SqlRowsCopiedEventArgs sqlRowsCopiedEventArgs = new SqlRowsCopiedEventArgs(rowsCopied);
			try
			{
				this._insideRowsCopiedEvent = true;
				this.OnRowsCopied(sqlRowsCopiedEventArgs);
			}
			finally
			{
				this._insideRowsCopiedEvent = false;
				openTdsConnection._parserLock.Wait(canBeReleasedFromAnyThread);
			}
			return sqlRowsCopiedEventArgs.Abort;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0005EC4C File Offset: 0x0005CE4C
		private Task ReadWriteColumnValueAsync(int col)
		{
			bool isSqlType;
			bool flag;
			bool isNull;
			object obj = this.GetValueFromSourceRow(col, out isSqlType, out flag, out isNull);
			_SqlMetaData metadata = this._sortedColumnMappings[col]._metadata;
			if (!flag)
			{
				obj = this.ConvertValue(obj, metadata, isNull, ref isSqlType, out flag);
			}
			Task result = null;
			if (metadata.type != SqlDbType.Variant)
			{
				result = this._parser.WriteBulkCopyValue(obj, metadata, this._stateObj, isSqlType, flag, isNull);
			}
			else
			{
				SqlBuffer.StorageType storageType = SqlBuffer.StorageType.Empty;
				if (this._SqlDataReaderRowSource != null && this._connection.IsKatmaiOrNewer)
				{
					storageType = this._SqlDataReaderRowSource.GetVariantInternalStorageType(this._sortedColumnMappings[col]._sourceColumnOrdinal);
				}
				if (storageType == SqlBuffer.StorageType.DateTime2)
				{
					this._parser.WriteSqlVariantDateTime2((DateTime)obj, this._stateObj);
				}
				else if (storageType == SqlBuffer.StorageType.Date)
				{
					this._parser.WriteSqlVariantDate((DateTime)obj, this._stateObj);
				}
				else
				{
					result = this._parser.WriteSqlVariantDataRowValue(obj, this._stateObj, true);
				}
			}
			return result;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0005ED43 File Offset: 0x0005CF43
		private void RegisterForConnectionCloseNotification<T>(ref Task<T> outerTask)
		{
			SqlConnection connection = this._connection;
			if (connection == null)
			{
				throw ADP.ClosedConnectionError();
			}
			connection.RegisterForConnectionCloseNotification<T>(ref outerTask, this, 3);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0005ED5C File Offset: 0x0005CF5C
		private Task CopyColumnsAsync(int col, TaskCompletionSource<object> source = null)
		{
			Task result = null;
			Task task = null;
			try
			{
				int i;
				for (i = col; i < this._sortedColumnMappings.Count; i++)
				{
					task = this.ReadWriteColumnValueAsync(i);
					if (task != null)
					{
						break;
					}
				}
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
						result = source.Task;
					}
					this.CopyColumnsAsyncSetupContinuation(source, task, i);
					return result;
				}
				if (source != null)
				{
					source.SetResult(null);
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
			}
			return result;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		private void CopyColumnsAsyncSetupContinuation(TaskCompletionSource<object> source, Task task, int i)
		{
			AsyncHelper.ContinueTask(task, source, delegate
			{
				if (i + 1 < this._sortedColumnMappings.Count)
				{
					this.CopyColumnsAsync(i + 1, source);
					return;
				}
				source.SetResult(null);
			}, this._connection.GetOpenTdsConnection(), null, null, null, null);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0005EE34 File Offset: 0x0005D034
		private void CheckAndRaiseNotification()
		{
			bool flag = false;
			Exception ex = null;
			this._rowsCopied++;
			if (this._notifyAfter > 0 && this._rowsUntilNotification > 0)
			{
				int num = this._rowsUntilNotification - 1;
				this._rowsUntilNotification = num;
				if (num == 0)
				{
					try
					{
						this._stateObj.BcpLock = true;
						flag = this.FireRowsCopiedEvent((long)this._rowsCopied);
						if (ConnectionState.Open != this._connection.State)
						{
							ex = ADP.OpenConnectionRequired("CheckAndRaiseNotification", this._connection.State);
						}
					}
					catch (Exception ex2)
					{
						if (!ADP.IsCatchableExceptionType(ex2))
						{
							ex = ex2;
						}
						else
						{
							ex = OperationAbortedException.Aborted(ex2);
						}
					}
					finally
					{
						this._stateObj.BcpLock = false;
					}
					if (!flag)
					{
						this._rowsUntilNotification = this._notifyAfter;
					}
				}
			}
			if (!flag && this._rowsUntilNotification > this._notifyAfter)
			{
				this._rowsUntilNotification = this._notifyAfter;
			}
			if (ex == null && flag)
			{
				ex = OperationAbortedException.Aborted(null);
			}
			if (this._connection.State != ConnectionState.Open)
			{
				throw ADP.OpenConnectionRequired("WriteToServer", this._connection.State);
			}
			if (ex != null)
			{
				this._parser._asyncWrite = false;
				this._parser.WriteBulkCopyDone(this._stateObj);
				this.RunParser(null);
				this.AbortTransaction();
				throw ex;
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0005EF8C File Offset: 0x0005D18C
		private Task CheckForCancellation(CancellationToken cts, TaskCompletionSource<object> tcs)
		{
			if (cts.IsCancellationRequested)
			{
				if (tcs == null)
				{
					tcs = new TaskCompletionSource<object>();
				}
				tcs.SetCanceled();
				return tcs.Task;
			}
			return null;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0005EFB0 File Offset: 0x0005D1B0
		private TaskCompletionSource<object> ContinueTaskPend(Task task, TaskCompletionSource<object> source, Func<TaskCompletionSource<object>> action)
		{
			if (task == null)
			{
				return action();
			}
			AsyncHelper.ContinueTask(task, source, delegate
			{
				action();
			}, null, null, null, null, null);
			return null;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0005EFF4 File Offset: 0x0005D1F4
		private Task CopyRowsAsync(int rowsSoFar, int totalRows, CancellationToken cts, TaskCompletionSource<object> source = null)
		{
			Task task = null;
			try
			{
				int i = rowsSoFar;
				Action <>9__1;
				Action <>9__2;
				while ((totalRows <= 0 || i < totalRows) && this._hasMoreRowToCopy)
				{
					if (this._isAsyncBulkCopy)
					{
						task = this.CheckForCancellation(cts, source);
						if (task != null)
						{
							return task;
						}
					}
					this._stateObj.WriteByte(209);
					Task task2 = this.CopyColumnsAsync(0, null);
					if (task2 != null)
					{
						source = (source ?? new TaskCompletionSource<object>());
						task = source.Task;
						AsyncHelper.ContinueTask(task2, source, delegate
						{
							this.CheckAndRaiseNotification();
							Task task5 = this.ReadFromRowSourceAsync(cts);
							if (task5 == null)
							{
								this.CopyRowsAsync(i + 1, totalRows, cts, source);
								return;
							}
							Task task6 = task5;
							TaskCompletionSource<object> source3 = source;
							Action onSuccess2;
							if ((onSuccess2 = <>9__2) == null)
							{
								onSuccess2 = (<>9__2 = delegate()
								{
									this.CopyRowsAsync(i + 1, totalRows, cts, source);
								});
							}
							AsyncHelper.ContinueTask(task6, source3, onSuccess2, this._connection.GetOpenTdsConnection(), null, null, null, null);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return task;
					}
					this.CheckAndRaiseNotification();
					Task task3 = this.ReadFromRowSourceAsync(cts);
					if (task3 != null)
					{
						if (source == null)
						{
							source = new TaskCompletionSource<object>();
						}
						task = source.Task;
						Task task4 = task3;
						TaskCompletionSource<object> source2 = source;
						Action onSuccess;
						if ((onSuccess = <>9__1) == null)
						{
							onSuccess = (<>9__1 = delegate()
							{
								this.CopyRowsAsync(i + 1, totalRows, cts, source);
							});
						}
						AsyncHelper.ContinueTask(task4, source2, onSuccess, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return task;
					}
					int j = i;
					i = j + 1;
				}
				if (source != null)
				{
					source.TrySetResult(null);
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
			}
			return task;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0005F1C8 File Offset: 0x0005D3C8
		private Task CopyBatchesAsync(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source = null)
		{
			try
			{
				Action <>9__0;
				while (this._hasMoreRowToCopy)
				{
					SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
					if (this.IsCopyOption(SqlBulkCopyOptions.UseInternalTransaction))
					{
						openTdsConnection.ThreadHasParserLockForClose = true;
						try
						{
							this._internalTransaction = this._connection.BeginTransaction();
						}
						finally
						{
							openTdsConnection.ThreadHasParserLockForClose = false;
						}
					}
					Task task = this.SubmitUpdateBulkCommand(updateBulkCommandText);
					if (task != null)
					{
						if (source == null)
						{
							source = new TaskCompletionSource<object>();
						}
						Task task2 = task;
						TaskCompletionSource<object> source2 = source;
						Action onSuccess;
						if ((onSuccess = <>9__0) == null)
						{
							onSuccess = (<>9__0 = delegate()
							{
								if (this.CopyBatchesAsyncContinued(internalResults, updateBulkCommandText, cts, source) == null)
								{
									this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
								}
							});
						}
						AsyncHelper.ContinueTask(task2, source2, onSuccess, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return source.Task;
					}
					Task task3 = this.CopyBatchesAsyncContinued(internalResults, updateBulkCommandText, cts, source);
					if (task3 != null)
					{
						return task3;
					}
				}
			}
			catch (Exception exception)
			{
				if (source != null)
				{
					source.TrySetException(exception);
					return source.Task;
				}
				throw;
			}
			if (source != null)
			{
				source.SetResult(null);
				return source.Task;
			}
			return null;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0005F34C File Offset: 0x0005D54C
		private Task CopyBatchesAsyncContinued(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task result;
			try
			{
				this.WriteMetaData(internalResults);
				Task task = this.CopyRowsAsync(0, this._savedBatchSize, cts, null);
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						if (this.CopyBatchesAsyncContinuedOnSuccess(internalResults, updateBulkCommandText, cts, source) == null)
						{
							this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
						}
					}, this._connection.GetOpenTdsConnection(), delegate(Exception _)
					{
						this.CopyBatchesAsyncContinuedOnError(false);
					}, delegate
					{
						this.CopyBatchesAsyncContinuedOnError(true);
					}, null, null);
					result = source.Task;
				}
				else
				{
					result = this.CopyBatchesAsyncContinuedOnSuccess(internalResults, updateBulkCommandText, cts, source);
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
				result = source.Task;
			}
			return result;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0005F460 File Offset: 0x0005D660
		private Task CopyBatchesAsyncContinuedOnSuccess(BulkCopySimpleResultSet internalResults, string updateBulkCommandText, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task result;
			try
			{
				Task task = this._parser.WriteBulkCopyDone(this._stateObj);
				if (task == null)
				{
					this.RunParser(null);
					this.CommitTransaction();
					result = null;
				}
				else
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						try
						{
							this.RunParser(null);
							this.CommitTransaction();
						}
						catch (Exception)
						{
							this.CopyBatchesAsyncContinuedOnError(false);
							throw;
						}
						this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, source);
					}, this._connection.GetOpenTdsConnection(), delegate(Exception _)
					{
						this.CopyBatchesAsyncContinuedOnError(false);
					}, null, null, null);
					result = source.Task;
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
				result = source.Task;
			}
			return result;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0005F548 File Offset: 0x0005D748
		private void CopyBatchesAsyncContinuedOnError(bool cleanupParser)
		{
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			try
			{
				if (cleanupParser && this._parser != null && this._stateObj != null)
				{
					this._parser._asyncWrite = false;
					this._parser.WriteBulkCopyDone(this._stateObj);
					this.RunParser(null);
				}
				if (this._stateObj != null)
				{
					this.CleanUpStateObjectOnError();
				}
			}
			catch (OutOfMemoryException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				openTdsConnection.DoomThisConnection();
				throw;
			}
			this.AbortTransaction();
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0005F5F4 File Offset: 0x0005D7F4
		private void CleanUpStateObjectOnError()
		{
			if (this._stateObj != null)
			{
				this._parser.Connection.ThreadHasParserLockForClose = true;
				try
				{
					this._stateObj.ResetBuffer();
					this._stateObj._outputPacketNumber = 1;
					if (this._parser.State == TdsParserState.OpenNotLoggedIn || this._parser.State == TdsParserState.OpenLoggedIn)
					{
						this._stateObj.CancelRequest();
					}
					this._stateObj._internalTimeout = false;
					this._stateObj.CloseSession();
					this._stateObj._bulkCopyOpperationInProgress = false;
					this._stateObj._bulkCopyWriteTimeout = false;
					this._stateObj = null;
				}
				finally
				{
					this._parser.Connection.ThreadHasParserLockForClose = false;
				}
			}
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0005F6B8 File Offset: 0x0005D8B8
		private void WriteToServerInternalRestContinuedAsync(BulkCopySimpleResultSet internalResults, CancellationToken cts, TaskCompletionSource<object> source)
		{
			Task task = null;
			try
			{
				string updateBulkCommandText = this.AnalyzeTargetAndCreateUpdateBulkCommand(internalResults);
				if (this._sortedColumnMappings.Count != 0)
				{
					this._stateObj.SniContext = SniContext.Snix_SendRows;
					this._savedBatchSize = this._batchSize;
					this._rowsUntilNotification = this._notifyAfter;
					this._rowsCopied = 0;
					this._currentRowMetadata = new SqlBulkCopy.SourceColumnMetadata[this._sortedColumnMappings.Count];
					for (int i = 0; i < this._currentRowMetadata.Length; i++)
					{
						this._currentRowMetadata[i] = this.GetColumnMetadata(i);
					}
					task = this.CopyBatchesAsync(internalResults, updateBulkCommandText, cts, null);
				}
				if (task != null)
				{
					if (source == null)
					{
						source = new TaskCompletionSource<object>();
					}
					AsyncHelper.ContinueTask(task, source, delegate
					{
						if (task.IsCanceled)
						{
							this._localColumnMappings = null;
							try
							{
								this.CleanUpStateObjectOnError();
								return;
							}
							finally
							{
								source.SetCanceled();
							}
						}
						if (task.Exception != null)
						{
							source.SetException(task.Exception.InnerException);
							return;
						}
						this._localColumnMappings = null;
						try
						{
							this.CleanUpStateObjectOnError();
						}
						finally
						{
							if (source != null)
							{
								if (cts.IsCancellationRequested)
								{
									source.SetCanceled();
								}
								else
								{
									source.SetResult(null);
								}
							}
						}
					}, this._connection.GetOpenTdsConnection(), null, null, null, null);
				}
				else
				{
					this._localColumnMappings = null;
					try
					{
						this.CleanUpStateObjectOnError();
					}
					catch (Exception)
					{
					}
					if (source != null)
					{
						source.SetResult(null);
					}
				}
			}
			catch (Exception exception)
			{
				this._localColumnMappings = null;
				try
				{
					this.CleanUpStateObjectOnError();
				}
				catch (Exception)
				{
				}
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0005F868 File Offset: 0x0005DA68
		private void WriteToServerInternalRestAsync(CancellationToken cts, TaskCompletionSource<object> source)
		{
			this._hasMoreRowToCopy = true;
			Task<BulkCopySimpleResultSet> internalResultsTask = null;
			BulkCopySimpleResultSet internalResults = new BulkCopySimpleResultSet();
			SqlInternalConnectionTds openTdsConnection = this._connection.GetOpenTdsConnection();
			try
			{
				this._parser = this._connection.Parser;
				this._parser._asyncWrite = this._isAsyncBulkCopy;
				Task task;
				try
				{
					task = this._connection.ValidateAndReconnect(delegate
					{
						if (this._parserLock != null)
						{
							this._parserLock.Release();
							this._parserLock = null;
						}
					}, this.BulkCopyTimeout);
				}
				catch (SqlException inner)
				{
					throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, inner);
				}
				if (task != null)
				{
					if (this._isAsyncBulkCopy)
					{
						CancellationTokenRegistration regReconnectCancel = default(CancellationTokenRegistration);
						TaskCompletionSource<object> cancellableReconnectTS = new TaskCompletionSource<object>();
						if (cts.CanBeCanceled)
						{
							regReconnectCancel = cts.Register(delegate(object s)
							{
								((TaskCompletionSource<object>)s).TrySetCanceled();
							}, cancellableReconnectTS);
						}
						AsyncHelper.ContinueTask(task, cancellableReconnectTS, delegate
						{
							cancellableReconnectTS.SetResult(null);
						}, null, null, null, null, null);
						AsyncHelper.SetTimeoutException(cancellableReconnectTS, this.BulkCopyTimeout, () => SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, SQL.CR_ReconnectTimeout()), CancellationToken.None);
						AsyncHelper.ContinueTask(cancellableReconnectTS.Task, source, delegate
						{
							regReconnectCancel.Dispose();
							if (this._parserLock != null)
							{
								this._parserLock.Release();
								this._parserLock = null;
							}
							this._parserLock = this._connection.GetOpenTdsConnection()._parserLock;
							this._parserLock.Wait(true);
							this.WriteToServerInternalRestAsync(cts, source);
						}, null, delegate(Exception e)
						{
							regReconnectCancel.Dispose();
						}, delegate
						{
							regReconnectCancel.Dispose();
						}, (Exception ex) => SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, ex), this._connection);
					}
					else
					{
						try
						{
							AsyncHelper.WaitForCompletion(task, this.BulkCopyTimeout, delegate
							{
								throw SQL.CR_ReconnectTimeout();
							}, true);
						}
						catch (SqlException inner2)
						{
							throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, inner2);
						}
						this._parserLock = this._connection.GetOpenTdsConnection()._parserLock;
						this._parserLock.Wait(false);
						this.WriteToServerInternalRestAsync(cts, source);
					}
				}
				else
				{
					if (this._isAsyncBulkCopy)
					{
						this._connection.AddWeakReference(this, 3);
					}
					openTdsConnection.ThreadHasParserLockForClose = true;
					try
					{
						this._stateObj = this._parser.GetSession(this);
						this._stateObj._bulkCopyOpperationInProgress = true;
						this._stateObj.StartSession(this);
					}
					finally
					{
						openTdsConnection.ThreadHasParserLockForClose = false;
					}
					try
					{
						internalResultsTask = this.CreateAndExecuteInitialQueryAsync(out internalResults);
					}
					catch (SqlException inner3)
					{
						throw SQL.BulkLoadInvalidDestinationTable(this._destinationTableName, inner3);
					}
					if (internalResultsTask != null)
					{
						AsyncHelper.ContinueTask(internalResultsTask, source, delegate
						{
							this.WriteToServerInternalRestContinuedAsync(internalResultsTask.Result, cts, source);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
					}
					else
					{
						this.WriteToServerInternalRestContinuedAsync(internalResults, cts, source);
					}
				}
			}
			catch (Exception exception)
			{
				if (source == null)
				{
					throw;
				}
				source.TrySetException(exception);
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0005FC04 File Offset: 0x0005DE04
		private Task WriteToServerInternalAsync(CancellationToken ctoken)
		{
			TaskCompletionSource<object> source = null;
			Task<object> result = null;
			if (this._isAsyncBulkCopy)
			{
				source = new TaskCompletionSource<object>();
				result = source.Task;
				this.RegisterForConnectionCloseNotification<object>(ref result);
			}
			if (this._destinationTableName != null)
			{
				try
				{
					Task task = this.ReadFromRowSourceAsync(ctoken);
					if (task != null)
					{
						AsyncHelper.ContinueTask(task, source, delegate
						{
							if (!this._hasMoreRowToCopy)
							{
								source.SetResult(null);
								return;
							}
							this.WriteToServerInternalRestAsync(ctoken, source);
						}, this._connection.GetOpenTdsConnection(), null, null, null, null);
						return result;
					}
					if (!this._hasMoreRowToCopy)
					{
						if (source != null)
						{
							source.SetResult(null);
						}
						return result;
					}
					this.WriteToServerInternalRestAsync(ctoken, source);
					return result;
				}
				catch (Exception exception)
				{
					if (source == null)
					{
						throw;
					}
					source.TrySetException(exception);
				}
				return result;
			}
			if (source != null)
			{
				source.SetException(SQL.BulkLoadMissingDestinationTable());
				return result;
			}
			throw SQL.BulkLoadMissingDestinationTable();
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005FD24 File Offset: 0x0005DF24
		[CompilerGenerated]
		private BulkCopySimpleResultSet <CreateAndExecuteInitialQueryAsync>b__71_0(Task t)
		{
			if (t.IsFaulted)
			{
				throw t.Exception.InnerException;
			}
			BulkCopySimpleResultSet bulkCopySimpleResultSet = new BulkCopySimpleResultSet();
			this.RunParserReliably(bulkCopySimpleResultSet);
			return bulkCopySimpleResultSet;
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0005FD53 File Offset: 0x0005DF53
		[CompilerGenerated]
		private void <SubmitUpdateBulkCommand>b__73_0(Task t)
		{
			if (t.IsFaulted)
			{
				throw t.Exception.InnerException;
			}
			this.RunParserReliably(null);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0005FD70 File Offset: 0x0005DF70
		[CompilerGenerated]
		private Task<bool> <ReadFromRowSourceAsync>b__78_0(Task<bool> t)
		{
			if (t.Status == TaskStatus.RanToCompletion)
			{
				this._hasMoreRowToCopy = t.Result;
			}
			return t;
		}

		// Token: 0x04000CEC RID: 3308
		private const int MetaDataResultId = 1;

		// Token: 0x04000CED RID: 3309
		private const int CollationResultId = 2;

		// Token: 0x04000CEE RID: 3310
		private const int CollationId = 3;

		// Token: 0x04000CEF RID: 3311
		private const int MAX_LENGTH = 2147483647;

		// Token: 0x04000CF0 RID: 3312
		private const int DefaultCommandTimeout = 30;

		// Token: 0x04000CF1 RID: 3313
		private bool _enableStreaming;

		// Token: 0x04000CF2 RID: 3314
		private int _batchSize;

		// Token: 0x04000CF3 RID: 3315
		private bool _ownConnection;

		// Token: 0x04000CF4 RID: 3316
		private SqlBulkCopyOptions _copyOptions;

		// Token: 0x04000CF5 RID: 3317
		private int _timeout = 30;

		// Token: 0x04000CF6 RID: 3318
		private string _destinationTableName;

		// Token: 0x04000CF7 RID: 3319
		private int _rowsCopied;

		// Token: 0x04000CF8 RID: 3320
		private int _notifyAfter;

		// Token: 0x04000CF9 RID: 3321
		private int _rowsUntilNotification;

		// Token: 0x04000CFA RID: 3322
		private bool _insideRowsCopiedEvent;

		// Token: 0x04000CFB RID: 3323
		private object _rowSource;

		// Token: 0x04000CFC RID: 3324
		private SqlDataReader _SqlDataReaderRowSource;

		// Token: 0x04000CFD RID: 3325
		private DbDataReader _DbDataReaderRowSource;

		// Token: 0x04000CFE RID: 3326
		private DataTable _dataTableSource;

		// Token: 0x04000CFF RID: 3327
		private SqlBulkCopyColumnMappingCollection _columnMappings;

		// Token: 0x04000D00 RID: 3328
		private SqlBulkCopyColumnMappingCollection _localColumnMappings;

		// Token: 0x04000D01 RID: 3329
		private SqlConnection _connection;

		// Token: 0x04000D02 RID: 3330
		private SqlTransaction _internalTransaction;

		// Token: 0x04000D03 RID: 3331
		private SqlTransaction _externalTransaction;

		// Token: 0x04000D04 RID: 3332
		private SqlBulkCopy.ValueSourceType _rowSourceType;

		// Token: 0x04000D05 RID: 3333
		private DataRow _currentRow;

		// Token: 0x04000D06 RID: 3334
		private int _currentRowLength;

		// Token: 0x04000D07 RID: 3335
		private DataRowState _rowStateToSkip;

		// Token: 0x04000D08 RID: 3336
		private IEnumerator _rowEnumerator;

		// Token: 0x04000D09 RID: 3337
		private TdsParser _parser;

		// Token: 0x04000D0A RID: 3338
		private TdsParserStateObject _stateObj;

		// Token: 0x04000D0B RID: 3339
		private List<_ColumnMapping> _sortedColumnMappings;

		// Token: 0x04000D0C RID: 3340
		private SqlRowsCopiedEventHandler _rowsCopiedEventHandler;

		// Token: 0x04000D0D RID: 3341
		private int _savedBatchSize;

		// Token: 0x04000D0E RID: 3342
		private bool _hasMoreRowToCopy;

		// Token: 0x04000D0F RID: 3343
		private bool _isAsyncBulkCopy;

		// Token: 0x04000D10 RID: 3344
		private bool _isBulkCopyingInProgress;

		// Token: 0x04000D11 RID: 3345
		private SqlInternalConnectionTds.SyncAsyncLock _parserLock;

		// Token: 0x04000D12 RID: 3346
		private SqlBulkCopy.SourceColumnMetadata[] _currentRowMetadata;

		// Token: 0x02000199 RID: 409
		private enum ValueSourceType
		{
			// Token: 0x04000D14 RID: 3348
			Unspecified,
			// Token: 0x04000D15 RID: 3349
			IDataReader,
			// Token: 0x04000D16 RID: 3350
			DataTable,
			// Token: 0x04000D17 RID: 3351
			RowArray,
			// Token: 0x04000D18 RID: 3352
			DbDataReader
		}

		// Token: 0x0200019A RID: 410
		private enum ValueMethod : byte
		{
			// Token: 0x04000D1A RID: 3354
			GetValue,
			// Token: 0x04000D1B RID: 3355
			SqlTypeSqlDecimal,
			// Token: 0x04000D1C RID: 3356
			SqlTypeSqlDouble,
			// Token: 0x04000D1D RID: 3357
			SqlTypeSqlSingle,
			// Token: 0x04000D1E RID: 3358
			DataFeedStream,
			// Token: 0x04000D1F RID: 3359
			DataFeedText,
			// Token: 0x04000D20 RID: 3360
			DataFeedXml
		}

		// Token: 0x0200019B RID: 411
		private readonly struct SourceColumnMetadata
		{
			// Token: 0x060014CC RID: 5324 RVA: 0x0005FD88 File Offset: 0x0005DF88
			public SourceColumnMetadata(SqlBulkCopy.ValueMethod method, bool isSqlType, bool isDataFeed)
			{
				this.Method = method;
				this.IsSqlType = isSqlType;
				this.IsDataFeed = isDataFeed;
			}

			// Token: 0x04000D21 RID: 3361
			public readonly SqlBulkCopy.ValueMethod Method;

			// Token: 0x04000D22 RID: 3362
			public readonly bool IsSqlType;

			// Token: 0x04000D23 RID: 3363
			public readonly bool IsDataFeed;
		}

		// Token: 0x0200019C RID: 412
		[CompilerGenerated]
		private sealed class <>c__DisplayClass105_0
		{
			// Token: 0x060014CD RID: 5325 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass105_0()
			{
			}

			// Token: 0x060014CE RID: 5326 RVA: 0x0005FDA0 File Offset: 0x0005DFA0
			internal Task <WriteRowSourceToServerAsync>b__3(Task t)
			{
				try
				{
					this.<>4__this.AbortTransaction();
				}
				finally
				{
					this.<>4__this._isBulkCopyingInProgress = false;
					if (this.<>4__this._parser != null)
					{
						this.<>4__this._parser._asyncWrite = false;
					}
					if (this.<>4__this._parserLock != null)
					{
						this.<>4__this._parserLock.Release();
						this.<>4__this._parserLock = null;
					}
				}
				return t;
			}

			// Token: 0x04000D24 RID: 3364
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D25 RID: 3365
			public int columnCount;

			// Token: 0x04000D26 RID: 3366
			public CancellationToken ctoken;
		}

		// Token: 0x0200019D RID: 413
		[CompilerGenerated]
		private sealed class <>c__DisplayClass105_1
		{
			// Token: 0x060014CF RID: 5327 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass105_1()
			{
			}

			// Token: 0x060014D0 RID: 5328 RVA: 0x0005FE20 File Offset: 0x0005E020
			internal void <WriteRowSourceToServerAsync>b__1(Task t)
			{
				Task task = this.CS$<>8__locals1.<>4__this.WriteRowSourceToServerAsync(this.CS$<>8__locals1.columnCount, this.CS$<>8__locals1.ctoken);
				if (task == null)
				{
					this.tcs.SetResult(null);
					return;
				}
				Task task2 = task;
				TaskCompletionSource<object> completion = this.tcs;
				Action onSuccess;
				if ((onSuccess = this.<>9__2) == null)
				{
					onSuccess = (this.<>9__2 = delegate()
					{
						this.tcs.SetResult(null);
					});
				}
				AsyncHelper.ContinueTask(task2, completion, onSuccess, null, null, null, null, null);
			}

			// Token: 0x060014D1 RID: 5329 RVA: 0x0005FE94 File Offset: 0x0005E094
			internal void <WriteRowSourceToServerAsync>b__2()
			{
				this.tcs.SetResult(null);
			}

			// Token: 0x04000D27 RID: 3367
			public TaskCompletionSource<object> tcs;

			// Token: 0x04000D28 RID: 3368
			public SqlBulkCopy.<>c__DisplayClass105_0 CS$<>8__locals1;

			// Token: 0x04000D29 RID: 3369
			public Action <>9__2;
		}

		// Token: 0x0200019E RID: 414
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060014D2 RID: 5330 RVA: 0x0005FEA2 File Offset: 0x0005E0A2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060014D3 RID: 5331 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c()
			{
			}

			// Token: 0x060014D4 RID: 5332 RVA: 0x0005FEAE File Offset: 0x0005E0AE
			internal void <WriteRowSourceToServerAsync>b__105_0()
			{
				throw SQL.CR_ReconnectTimeout();
			}

			// Token: 0x060014D5 RID: 5333 RVA: 0x0005FEB5 File Offset: 0x0005E0B5
			internal void <WriteToServerInternalRestAsync>b__124_3(object s)
			{
				((TaskCompletionSource<object>)s).TrySetCanceled();
			}

			// Token: 0x060014D6 RID: 5334 RVA: 0x0005FEAE File Offset: 0x0005E0AE
			internal void <WriteToServerInternalRestAsync>b__124_1()
			{
				throw SQL.CR_ReconnectTimeout();
			}

			// Token: 0x04000D2A RID: 3370
			public static readonly SqlBulkCopy.<>c <>9 = new SqlBulkCopy.<>c();

			// Token: 0x04000D2B RID: 3371
			public static Action <>9__105_0;

			// Token: 0x04000D2C RID: 3372
			public static Action<object> <>9__124_3;

			// Token: 0x04000D2D RID: 3373
			public static Action <>9__124_1;
		}

		// Token: 0x0200019F RID: 415
		[CompilerGenerated]
		private sealed class <>c__DisplayClass113_0
		{
			// Token: 0x060014D7 RID: 5335 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass113_0()
			{
			}

			// Token: 0x060014D8 RID: 5336 RVA: 0x0005FEC4 File Offset: 0x0005E0C4
			internal void <CopyColumnsAsyncSetupContinuation>b__0()
			{
				if (this.i + 1 < this.<>4__this._sortedColumnMappings.Count)
				{
					this.<>4__this.CopyColumnsAsync(this.i + 1, this.source);
					return;
				}
				this.source.SetResult(null);
			}

			// Token: 0x04000D2E RID: 3374
			public int i;

			// Token: 0x04000D2F RID: 3375
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D30 RID: 3376
			public TaskCompletionSource<object> source;
		}

		// Token: 0x020001A0 RID: 416
		[CompilerGenerated]
		private sealed class <>c__DisplayClass116_0
		{
			// Token: 0x060014D9 RID: 5337 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass116_0()
			{
			}

			// Token: 0x060014DA RID: 5338 RVA: 0x0005FF12 File Offset: 0x0005E112
			internal void <ContinueTaskPend>b__0()
			{
				this.action();
			}

			// Token: 0x04000D31 RID: 3377
			public Func<TaskCompletionSource<object>> action;
		}

		// Token: 0x020001A1 RID: 417
		[CompilerGenerated]
		private sealed class <>c__DisplayClass117_0
		{
			// Token: 0x060014DB RID: 5339 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass117_0()
			{
			}

			// Token: 0x060014DC RID: 5340 RVA: 0x0005FF20 File Offset: 0x0005E120
			internal void <CopyRowsAsync>b__1()
			{
				this.<>4__this.CopyRowsAsync(this.i + 1, this.totalRows, this.cts, this.source);
			}

			// Token: 0x060014DD RID: 5341 RVA: 0x0005FF48 File Offset: 0x0005E148
			internal void <CopyRowsAsync>b__0()
			{
				this.<>4__this.CheckAndRaiseNotification();
				Task task = this.<>4__this.ReadFromRowSourceAsync(this.cts);
				if (task == null)
				{
					this.<>4__this.CopyRowsAsync(this.i + 1, this.totalRows, this.cts, this.source);
					return;
				}
				Task task2 = task;
				TaskCompletionSource<object> completion = this.source;
				Action onSuccess;
				if ((onSuccess = this.<>9__2) == null)
				{
					onSuccess = (this.<>9__2 = delegate()
					{
						this.<>4__this.CopyRowsAsync(this.i + 1, this.totalRows, this.cts, this.source);
					});
				}
				AsyncHelper.ContinueTask(task2, completion, onSuccess, this.<>4__this._connection.GetOpenTdsConnection(), null, null, null, null);
			}

			// Token: 0x060014DE RID: 5342 RVA: 0x0005FF20 File Offset: 0x0005E120
			internal void <CopyRowsAsync>b__2()
			{
				this.<>4__this.CopyRowsAsync(this.i + 1, this.totalRows, this.cts, this.source);
			}

			// Token: 0x04000D32 RID: 3378
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D33 RID: 3379
			public int i;

			// Token: 0x04000D34 RID: 3380
			public int totalRows;

			// Token: 0x04000D35 RID: 3381
			public CancellationToken cts;

			// Token: 0x04000D36 RID: 3382
			public TaskCompletionSource<object> source;

			// Token: 0x04000D37 RID: 3383
			public Action <>9__1;

			// Token: 0x04000D38 RID: 3384
			public Action <>9__2;
		}

		// Token: 0x020001A2 RID: 418
		[CompilerGenerated]
		private sealed class <>c__DisplayClass118_0
		{
			// Token: 0x060014DF RID: 5343 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass118_0()
			{
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x0005FFDC File Offset: 0x0005E1DC
			internal void <CopyBatchesAsync>b__0()
			{
				if (this.<>4__this.CopyBatchesAsyncContinued(this.internalResults, this.updateBulkCommandText, this.cts, this.source) == null)
				{
					this.<>4__this.CopyBatchesAsync(this.internalResults, this.updateBulkCommandText, this.cts, this.source);
				}
			}

			// Token: 0x04000D39 RID: 3385
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D3A RID: 3386
			public BulkCopySimpleResultSet internalResults;

			// Token: 0x04000D3B RID: 3387
			public string updateBulkCommandText;

			// Token: 0x04000D3C RID: 3388
			public CancellationToken cts;

			// Token: 0x04000D3D RID: 3389
			public TaskCompletionSource<object> source;

			// Token: 0x04000D3E RID: 3390
			public Action <>9__0;
		}

		// Token: 0x020001A3 RID: 419
		[CompilerGenerated]
		private sealed class <>c__DisplayClass119_0
		{
			// Token: 0x060014E1 RID: 5345 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass119_0()
			{
			}

			// Token: 0x060014E2 RID: 5346 RVA: 0x00060034 File Offset: 0x0005E234
			internal void <CopyBatchesAsyncContinued>b__0()
			{
				if (this.<>4__this.CopyBatchesAsyncContinuedOnSuccess(this.internalResults, this.updateBulkCommandText, this.cts, this.source) == null)
				{
					this.<>4__this.CopyBatchesAsync(this.internalResults, this.updateBulkCommandText, this.cts, this.source);
				}
			}

			// Token: 0x060014E3 RID: 5347 RVA: 0x0006008A File Offset: 0x0005E28A
			internal void <CopyBatchesAsyncContinued>b__1(Exception _)
			{
				this.<>4__this.CopyBatchesAsyncContinuedOnError(false);
			}

			// Token: 0x060014E4 RID: 5348 RVA: 0x00060098 File Offset: 0x0005E298
			internal void <CopyBatchesAsyncContinued>b__2()
			{
				this.<>4__this.CopyBatchesAsyncContinuedOnError(true);
			}

			// Token: 0x04000D3F RID: 3391
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D40 RID: 3392
			public BulkCopySimpleResultSet internalResults;

			// Token: 0x04000D41 RID: 3393
			public string updateBulkCommandText;

			// Token: 0x04000D42 RID: 3394
			public CancellationToken cts;

			// Token: 0x04000D43 RID: 3395
			public TaskCompletionSource<object> source;
		}

		// Token: 0x020001A4 RID: 420
		[CompilerGenerated]
		private sealed class <>c__DisplayClass120_0
		{
			// Token: 0x060014E5 RID: 5349 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass120_0()
			{
			}

			// Token: 0x060014E6 RID: 5350 RVA: 0x000600A8 File Offset: 0x0005E2A8
			internal void <CopyBatchesAsyncContinuedOnSuccess>b__0()
			{
				try
				{
					this.<>4__this.RunParser(null);
					this.<>4__this.CommitTransaction();
				}
				catch (Exception)
				{
					this.<>4__this.CopyBatchesAsyncContinuedOnError(false);
					throw;
				}
				this.<>4__this.CopyBatchesAsync(this.internalResults, this.updateBulkCommandText, this.cts, this.source);
			}

			// Token: 0x060014E7 RID: 5351 RVA: 0x00060114 File Offset: 0x0005E314
			internal void <CopyBatchesAsyncContinuedOnSuccess>b__1(Exception _)
			{
				this.<>4__this.CopyBatchesAsyncContinuedOnError(false);
			}

			// Token: 0x04000D44 RID: 3396
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D45 RID: 3397
			public BulkCopySimpleResultSet internalResults;

			// Token: 0x04000D46 RID: 3398
			public string updateBulkCommandText;

			// Token: 0x04000D47 RID: 3399
			public CancellationToken cts;

			// Token: 0x04000D48 RID: 3400
			public TaskCompletionSource<object> source;
		}

		// Token: 0x020001A5 RID: 421
		[CompilerGenerated]
		private sealed class <>c__DisplayClass123_0
		{
			// Token: 0x060014E8 RID: 5352 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass123_0()
			{
			}

			// Token: 0x060014E9 RID: 5353 RVA: 0x00060124 File Offset: 0x0005E324
			internal void <WriteToServerInternalRestContinuedAsync>b__0()
			{
				if (this.task.IsCanceled)
				{
					this.<>4__this._localColumnMappings = null;
					try
					{
						this.<>4__this.CleanUpStateObjectOnError();
						return;
					}
					finally
					{
						this.source.SetCanceled();
					}
				}
				if (this.task.Exception != null)
				{
					this.source.SetException(this.task.Exception.InnerException);
					return;
				}
				this.<>4__this._localColumnMappings = null;
				try
				{
					this.<>4__this.CleanUpStateObjectOnError();
				}
				finally
				{
					if (this.source != null)
					{
						if (this.cts.IsCancellationRequested)
						{
							this.source.SetCanceled();
						}
						else
						{
							this.source.SetResult(null);
						}
					}
				}
			}

			// Token: 0x04000D49 RID: 3401
			public Task task;

			// Token: 0x04000D4A RID: 3402
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D4B RID: 3403
			public TaskCompletionSource<object> source;

			// Token: 0x04000D4C RID: 3404
			public CancellationToken cts;
		}

		// Token: 0x020001A6 RID: 422
		[CompilerGenerated]
		private sealed class <>c__DisplayClass124_0
		{
			// Token: 0x060014EA RID: 5354 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass124_0()
			{
			}

			// Token: 0x060014EB RID: 5355 RVA: 0x000601F0 File Offset: 0x0005E3F0
			internal void <WriteToServerInternalRestAsync>b__0()
			{
				if (this.<>4__this._parserLock != null)
				{
					this.<>4__this._parserLock.Release();
					this.<>4__this._parserLock = null;
				}
			}

			// Token: 0x060014EC RID: 5356 RVA: 0x0006021B File Offset: 0x0005E41B
			internal Exception <WriteToServerInternalRestAsync>b__5()
			{
				return SQL.BulkLoadInvalidDestinationTable(this.<>4__this._destinationTableName, SQL.CR_ReconnectTimeout());
			}

			// Token: 0x060014ED RID: 5357 RVA: 0x00060232 File Offset: 0x0005E432
			internal Exception <WriteToServerInternalRestAsync>b__9(Exception ex)
			{
				return SQL.BulkLoadInvalidDestinationTable(this.<>4__this._destinationTableName, ex);
			}

			// Token: 0x060014EE RID: 5358 RVA: 0x00060245 File Offset: 0x0005E445
			internal void <WriteToServerInternalRestAsync>b__2()
			{
				this.<>4__this.WriteToServerInternalRestContinuedAsync(this.internalResultsTask.Result, this.cts, this.source);
			}

			// Token: 0x04000D4D RID: 3405
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D4E RID: 3406
			public CancellationToken cts;

			// Token: 0x04000D4F RID: 3407
			public TaskCompletionSource<object> source;

			// Token: 0x04000D50 RID: 3408
			public Task<BulkCopySimpleResultSet> internalResultsTask;
		}

		// Token: 0x020001A7 RID: 423
		[CompilerGenerated]
		private sealed class <>c__DisplayClass124_1
		{
			// Token: 0x060014EF RID: 5359 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass124_1()
			{
			}

			// Token: 0x060014F0 RID: 5360 RVA: 0x00060269 File Offset: 0x0005E469
			internal void <WriteToServerInternalRestAsync>b__4()
			{
				this.cancellableReconnectTS.SetResult(null);
			}

			// Token: 0x060014F1 RID: 5361 RVA: 0x00060278 File Offset: 0x0005E478
			internal void <WriteToServerInternalRestAsync>b__6()
			{
				this.regReconnectCancel.Dispose();
				if (this.CS$<>8__locals1.<>4__this._parserLock != null)
				{
					this.CS$<>8__locals1.<>4__this._parserLock.Release();
					this.CS$<>8__locals1.<>4__this._parserLock = null;
				}
				this.CS$<>8__locals1.<>4__this._parserLock = this.CS$<>8__locals1.<>4__this._connection.GetOpenTdsConnection()._parserLock;
				this.CS$<>8__locals1.<>4__this._parserLock.Wait(true);
				this.CS$<>8__locals1.<>4__this.WriteToServerInternalRestAsync(this.CS$<>8__locals1.cts, this.CS$<>8__locals1.source);
			}

			// Token: 0x060014F2 RID: 5362 RVA: 0x0006032E File Offset: 0x0005E52E
			internal void <WriteToServerInternalRestAsync>b__7(Exception e)
			{
				this.regReconnectCancel.Dispose();
			}

			// Token: 0x060014F3 RID: 5363 RVA: 0x0006032E File Offset: 0x0005E52E
			internal void <WriteToServerInternalRestAsync>b__8()
			{
				this.regReconnectCancel.Dispose();
			}

			// Token: 0x04000D51 RID: 3409
			public TaskCompletionSource<object> cancellableReconnectTS;

			// Token: 0x04000D52 RID: 3410
			public CancellationTokenRegistration regReconnectCancel;

			// Token: 0x04000D53 RID: 3411
			public SqlBulkCopy.<>c__DisplayClass124_0 CS$<>8__locals1;
		}

		// Token: 0x020001A8 RID: 424
		[CompilerGenerated]
		private sealed class <>c__DisplayClass125_0
		{
			// Token: 0x060014F4 RID: 5364 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass125_0()
			{
			}

			// Token: 0x060014F5 RID: 5365 RVA: 0x0006033B File Offset: 0x0005E53B
			internal void <WriteToServerInternalAsync>b__0()
			{
				if (!this.<>4__this._hasMoreRowToCopy)
				{
					this.source.SetResult(null);
					return;
				}
				this.<>4__this.WriteToServerInternalRestAsync(this.ctoken, this.source);
			}

			// Token: 0x04000D54 RID: 3412
			public SqlBulkCopy <>4__this;

			// Token: 0x04000D55 RID: 3413
			public TaskCompletionSource<object> source;

			// Token: 0x04000D56 RID: 3414
			public CancellationToken ctoken;
		}
	}
}
