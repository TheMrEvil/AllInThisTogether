using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SqlServer.Server;
using Unity;

namespace System.Data.SqlClient
{
	/// <summary>Provides a way of reading a forward-only stream of rows from a SQL Server database. This class cannot be inherited.</summary>
	// Token: 0x020001E5 RID: 485
	public class SqlDataReader : DbDataReader, IDataReader, IDisposable, IDataRecord, IDbColumnSchemaGenerator
	{
		// Token: 0x060017AE RID: 6062 RVA: 0x0006AA0C File Offset: 0x00068C0C
		internal SqlDataReader(SqlCommand command, CommandBehavior behavior)
		{
			this._sharedState = new SqlDataReader.SharedState();
			this._recordsAffected = -1;
			this.ObjectID = Interlocked.Increment(ref SqlDataReader.s_objectTypeCount);
			base..ctor();
			this._command = command;
			this._commandBehavior = behavior;
			if (this._command != null)
			{
				this._defaultTimeoutMilliseconds = (long)command.CommandTimeout * 1000L;
				this._connection = command.Connection;
				if (this._connection != null)
				{
					this._statistics = this._connection.Statistics;
					this._typeSystem = this._connection.TypeSystem;
				}
			}
			this._sharedState._dataReady = false;
			this._metaDataConsumed = false;
			this._hasRows = false;
			this._browseModeInfoConsumed = false;
			this._currentStream = null;
			this._currentTextReader = null;
			this._cancelAsyncOnCloseTokenSource = new CancellationTokenSource();
			this._cancelAsyncOnCloseToken = this._cancelAsyncOnCloseTokenSource.Token;
			this._columnDataCharsIndex = -1;
		}

		// Token: 0x17000445 RID: 1093
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x0006AAF3 File Offset: 0x00068CF3
		internal bool BrowseModeInfoConsumed
		{
			set
			{
				this._browseModeInfoConsumed = value;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0006AAFC File Offset: 0x00068CFC
		internal SqlCommand Command
		{
			get
			{
				return this._command;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlConnection" /> associated with the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlConnection" /> associated with the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</returns>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0006AB04 File Offset: 0x00068D04
		protected SqlConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0006AB0C File Offset: 0x00068D0C
		public override int Depth
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Depth");
				}
				return 0;
			}
		}

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise the number of columns in the current row. The default is -1.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server.</exception>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0006AB22 File Offset: 0x00068D22
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("FieldCount");
				}
				if (this._currentTask != null)
				{
					throw ADP.AsyncOperationPending();
				}
				if (this.MetaData == null)
				{
					return 0;
				}
				return this._metaData.Length;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlDataReader" /> contains one or more rows.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.SqlClient.SqlDataReader" /> contains one or more rows; otherwise <see langword="false" />.</returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0006AB5A File Offset: 0x00068D5A
		public override bool HasRows
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("HasRows");
				}
				if (this._currentTask != null)
				{
					throw ADP.AsyncOperationPending();
				}
				return this._hasRows;
			}
		}

		/// <summary>Retrieves a Boolean value that indicates whether the specified <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance has been closed.</summary>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance is closed; otherwise <see langword="false" />.</returns>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0006AB83 File Offset: 0x00068D83
		public override bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0006AB8B File Offset: 0x00068D8B
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0006AB93 File Offset: 0x00068D93
		internal bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
			set
			{
				this._isInitialized = value;
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0006AB9C File Offset: 0x00068D9C
		internal long ColumnDataBytesRemaining()
		{
			if (-1L == this._sharedState._columnDataBytesRemaining)
			{
				this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
			}
			return this._sharedState._columnDataBytesRemaining;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0006ABD4 File Offset: 0x00068DD4
		internal _SqlMetaDataSet MetaData
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("MetaData");
				}
				if (this._metaData == null && !this._metaDataConsumed)
				{
					if (this._currentTask != null)
					{
						throw SQL.PendingBeginXXXExists();
					}
					if (!this.TryConsumeMetaData())
					{
						throw SQL.SynchronousCallMayNotPend();
					}
				}
				return this._metaData;
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0006AC28 File Offset: 0x00068E28
		internal virtual SmiExtendedMetaData[] GetInternalSmiMetaData()
		{
			SmiExtendedMetaData[] array = null;
			_SqlMetaDataSet metaData = this.MetaData;
			if (metaData != null && 0 < metaData.Length)
			{
				array = new SmiExtendedMetaData[metaData.visibleColumns];
				for (int i = 0; i < metaData.Length; i++)
				{
					_SqlMetaData sqlMetaData = metaData[i];
					if (!sqlMetaData.isHidden)
					{
						SqlCollation collation = sqlMetaData.collation;
						string typeSpecificNamePart = null;
						string typeSpecificNamePart2 = null;
						string typeSpecificNamePart3 = null;
						if (SqlDbType.Xml == sqlMetaData.type)
						{
							typeSpecificNamePart = sqlMetaData.xmlSchemaCollectionDatabase;
							typeSpecificNamePart2 = sqlMetaData.xmlSchemaCollectionOwningSchema;
							typeSpecificNamePart3 = sqlMetaData.xmlSchemaCollectionName;
						}
						else if (SqlDbType.Udt == sqlMetaData.type)
						{
							this.Connection.CheckGetExtendedUDTInfo(sqlMetaData, true);
							typeSpecificNamePart = sqlMetaData.udtDatabaseName;
							typeSpecificNamePart2 = sqlMetaData.udtSchemaName;
							typeSpecificNamePart3 = sqlMetaData.udtTypeName;
						}
						int num = sqlMetaData.length;
						if (num > 8000)
						{
							num = -1;
						}
						else if (SqlDbType.NChar == sqlMetaData.type || SqlDbType.NVarChar == sqlMetaData.type)
						{
							num /= 2;
						}
						array[i] = new SmiQueryMetaData(sqlMetaData.type, (long)num, sqlMetaData.precision, sqlMetaData.scale, (long)((collation != null) ? collation.LCID : this._defaultLCID), (collation != null) ? collation.SqlCompareOptions : SqlCompareOptions.None, sqlMetaData.udtType, false, null, null, sqlMetaData.column, typeSpecificNamePart, typeSpecificNamePart2, typeSpecificNamePart3, sqlMetaData.isNullable, sqlMetaData.serverName, sqlMetaData.catalogName, sqlMetaData.schemaName, sqlMetaData.tableName, sqlMetaData.baseColumn, sqlMetaData.isKey, sqlMetaData.isIdentity, sqlMetaData.updatability == 0, sqlMetaData.isExpression, sqlMetaData.isDifferentName, sqlMetaData.isHidden);
					}
				}
			}
			return array;
		}

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the Transact-SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.</returns>
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x0006ADCE File Offset: 0x00068FCE
		public override int RecordsAffected
		{
			get
			{
				if (this._command != null)
				{
					return this._command.InternalRecordsAffected;
				}
				return this._recordsAffected;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x0006ADEA File Offset: 0x00068FEA
		internal string ResetOptionsString
		{
			set
			{
				this._resetOptionsString = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0006ADF3 File Offset: 0x00068FF3
		private SqlStatistics Statistics
		{
			get
			{
				return this._statistics;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0006ADFB File Offset: 0x00068FFB
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0006AE03 File Offset: 0x00069003
		internal MultiPartTableName[] TableNames
		{
			get
			{
				return this._tableNames;
			}
			set
			{
				this._tableNames = value;
			}
		}

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.SqlClient.SqlDataReader" /> that are not hidden.</summary>
		/// <returns>The number of fields that are not hidden.</returns>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0006AE0C File Offset: 0x0006900C
		public override int VisibleFieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("VisibleFieldCount");
				}
				_SqlMetaDataSet metaData = this.MetaData;
				if (metaData == null)
				{
					return 0;
				}
				return metaData.visibleColumns;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
		// Token: 0x17000453 RID: 1107
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <param name="name">The column name.</param>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found.</exception>
		// Token: 0x17000454 RID: 1108
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0006AE56 File Offset: 0x00069056
		internal void Bind(TdsParserStateObject stateObj)
		{
			stateObj.Owner = this;
			this._stateObj = stateObj;
			this._parser = stateObj.Parser;
			this._defaultLCID = this._parser.DefaultLCID;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0006AE84 File Offset: 0x00069084
		internal DataTable BuildSchemaTable()
		{
			_SqlMetaDataSet metaData = this.MetaData;
			DataTable dataTable = new DataTable("SchemaTable");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.MinimumCapacity = metaData.Length;
			DataColumn column = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
			DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
			DataColumn column2 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
			DataColumn column3 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
			DataColumn column4 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
			DataColumn column5 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
			DataColumn column6 = new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
			DataColumn column7 = new DataColumn(SchemaTableColumn.NonVersionedProviderType, typeof(int));
			DataColumn column8 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
			DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
			DataColumn column9 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
			DataColumn column10 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			DataColumn column11 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			DataColumn column12 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
			DataColumn column13 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
			DataColumn column14 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			DataColumn column15 = new DataColumn(SchemaTableOptionalColumn.IsHidden, typeof(bool));
			DataColumn column16 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			DataColumn column17 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
			DataColumn column18 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
			DataColumn column19 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
			DataColumn column20 = new DataColumn(SchemaTableOptionalColumn.BaseServerName, typeof(string));
			DataColumn column21 = new DataColumn(SchemaTableColumn.IsAliased, typeof(bool));
			DataColumn column22 = new DataColumn(SchemaTableColumn.IsExpression, typeof(bool));
			DataColumn column23 = new DataColumn("IsIdentity", typeof(bool));
			DataColumn column24 = new DataColumn("DataTypeName", typeof(string));
			DataColumn column25 = new DataColumn("UdtAssemblyQualifiedName", typeof(string));
			DataColumn column26 = new DataColumn("XmlSchemaCollectionDatabase", typeof(string));
			DataColumn column27 = new DataColumn("XmlSchemaCollectionOwningSchema", typeof(string));
			DataColumn column28 = new DataColumn("XmlSchemaCollectionName", typeof(string));
			DataColumn column29 = new DataColumn("IsColumnSet", typeof(bool));
			dataColumn.DefaultValue = 0;
			dataColumn2.DefaultValue = false;
			DataColumnCollection columns = dataTable.Columns;
			columns.Add(column);
			columns.Add(dataColumn);
			columns.Add(column2);
			columns.Add(column3);
			columns.Add(column4);
			columns.Add(column12);
			columns.Add(column13);
			columns.Add(column20);
			columns.Add(column16);
			columns.Add(column19);
			columns.Add(column17);
			columns.Add(column18);
			columns.Add(column5);
			columns.Add(column9);
			columns.Add(column8);
			columns.Add(column21);
			columns.Add(column22);
			columns.Add(column23);
			columns.Add(column14);
			columns.Add(column11);
			columns.Add(column15);
			columns.Add(dataColumn2);
			columns.Add(column10);
			columns.Add(column6);
			columns.Add(column24);
			columns.Add(column26);
			columns.Add(column27);
			columns.Add(column28);
			columns.Add(column25);
			columns.Add(column7);
			columns.Add(column29);
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				DataRow dataRow = dataTable.NewRow();
				dataRow[column] = sqlMetaData.column;
				dataRow[dataColumn] = sqlMetaData.ordinal;
				dataRow[column2] = ((sqlMetaData.metaType.IsSizeInCharacters && sqlMetaData.length != int.MaxValue) ? (sqlMetaData.length / 2) : sqlMetaData.length);
				dataRow[column5] = this.GetFieldTypeInternal(sqlMetaData);
				dataRow[column6] = this.GetProviderSpecificFieldTypeInternal(sqlMetaData);
				dataRow[column7] = (int)sqlMetaData.type;
				dataRow[column24] = this.GetDataTypeNameInternal(sqlMetaData);
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
				{
					dataRow[column8] = SqlDbType.NVarChar;
					switch (sqlMetaData.type)
					{
					case SqlDbType.Date:
						dataRow[column2] = 10;
						break;
					case SqlDbType.Time:
						dataRow[column2] = TdsEnums.WHIDBEY_TIME_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					case SqlDbType.DateTime2:
						dataRow[column2] = TdsEnums.WHIDBEY_DATETIME2_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					case SqlDbType.DateTimeOffset:
						dataRow[column2] = TdsEnums.WHIDBEY_DATETIMEOFFSET_LENGTH[(int)((byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale)];
						break;
					}
				}
				else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsLargeUdt)
				{
					if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
					{
						dataRow[column8] = SqlDbType.VarBinary;
					}
					else
					{
						dataRow[column8] = SqlDbType.Image;
					}
				}
				else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
				{
					dataRow[column8] = (int)sqlMetaData.type;
					if (sqlMetaData.type == SqlDbType.Udt)
					{
						dataRow[column25] = sqlMetaData.udtAssemblyQualifiedName;
					}
					else if (sqlMetaData.type == SqlDbType.Xml)
					{
						dataRow[column26] = sqlMetaData.xmlSchemaCollectionDatabase;
						dataRow[column27] = sqlMetaData.xmlSchemaCollectionOwningSchema;
						dataRow[column28] = sqlMetaData.xmlSchemaCollectionName;
					}
				}
				else
				{
					dataRow[column8] = this.GetVersionedMetaType(sqlMetaData.metaType).SqlDbType;
				}
				if (255 != sqlMetaData.precision)
				{
					dataRow[column3] = sqlMetaData.precision;
				}
				else
				{
					dataRow[column3] = sqlMetaData.metaType.Precision;
				}
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
				{
					dataRow[column4] = MetaType.MetaNVarChar.Scale;
				}
				else if (255 != sqlMetaData.scale)
				{
					dataRow[column4] = sqlMetaData.scale;
				}
				else
				{
					dataRow[column4] = sqlMetaData.metaType.Scale;
				}
				dataRow[column9] = sqlMetaData.isNullable;
				if (this._browseModeInfoConsumed)
				{
					dataRow[column21] = sqlMetaData.isDifferentName;
					dataRow[column13] = sqlMetaData.isKey;
					dataRow[column15] = sqlMetaData.isHidden;
					dataRow[column22] = sqlMetaData.isExpression;
				}
				dataRow[column23] = sqlMetaData.isIdentity;
				dataRow[column14] = sqlMetaData.isIdentity;
				dataRow[dataColumn2] = sqlMetaData.metaType.IsLong;
				if (SqlDbType.Timestamp == sqlMetaData.type)
				{
					dataRow[column12] = true;
					dataRow[column11] = true;
				}
				else
				{
					dataRow[column12] = false;
					dataRow[column11] = false;
				}
				dataRow[column10] = (sqlMetaData.updatability == 0);
				dataRow[column29] = sqlMetaData.isColumnSet;
				if (!string.IsNullOrEmpty(sqlMetaData.serverName))
				{
					dataRow[column20] = sqlMetaData.serverName;
				}
				if (!string.IsNullOrEmpty(sqlMetaData.catalogName))
				{
					dataRow[column16] = sqlMetaData.catalogName;
				}
				if (!string.IsNullOrEmpty(sqlMetaData.schemaName))
				{
					dataRow[column17] = sqlMetaData.schemaName;
				}
				if (!string.IsNullOrEmpty(sqlMetaData.tableName))
				{
					dataRow[column18] = sqlMetaData.tableName;
				}
				if (!string.IsNullOrEmpty(sqlMetaData.baseColumn))
				{
					dataRow[column19] = sqlMetaData.baseColumn;
				}
				else if (!string.IsNullOrEmpty(sqlMetaData.column))
				{
					dataRow[column19] = sqlMetaData.column;
				}
				dataTable.Rows.Add(dataRow);
				dataRow.AcceptChanges();
			}
			foreach (object obj in columns)
			{
				((DataColumn)obj).ReadOnly = true;
			}
			return dataTable;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0006B864 File Offset: 0x00069A64
		internal void Cancel(SqlCommand command)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.Cancel(command);
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0006B884 File Offset: 0x00069A84
		private bool TryCleanPartialRead()
		{
			if (this._stateObj._partialHeaderBytesRead > 0 && !this._stateObj.TryProcessHeader())
			{
				return false;
			}
			if (-1 != this._lastColumnWithDataChunkRead)
			{
				this.CloseActiveSequentialStreamAndTextReader();
			}
			if (this._sharedState._nextColumnHeaderToRead == 0)
			{
				if (!this._stateObj.Parser.TrySkipRow(this._metaData, this._stateObj))
				{
					return false;
				}
			}
			else
			{
				if (!this.TryResetBlobState())
				{
					return false;
				}
				if (!this._stateObj.Parser.TrySkipRow(this._metaData, this._sharedState._nextColumnHeaderToRead, this._stateObj))
				{
					return false;
				}
			}
			this._sharedState._dataReady = false;
			return true;
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0006B92C File Offset: 0x00069B2C
		private void CleanPartialReadReliable()
		{
			this.TryCleanPartialRead();
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0006B935 File Offset: 0x00069B35
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		/// <summary>Closes the <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</summary>
		// Token: 0x060017C9 RID: 6089 RVA: 0x0006B948 File Offset: 0x00069B48
		public override void Close()
		{
			SqlStatistics statistics = null;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				TdsParserStateObject stateObj = this._stateObj;
				this._cancelAsyncOnCloseTokenSource.Cancel();
				Task currentTask = this._currentTask;
				if (currentTask != null && !currentTask.IsCompleted)
				{
					try
					{
						((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
						TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
						if (networkPacketTaskSource != null)
						{
							((IAsyncResult)networkPacketTaskSource.Task).AsyncWaitHandle.WaitOne();
						}
					}
					catch (Exception)
					{
						this._connection.InnerConnection.DoomThisConnection();
						this._isClosed = true;
						if (stateObj != null)
						{
							TdsParserStateObject obj = stateObj;
							lock (obj)
							{
								this._stateObj = null;
								this._command = null;
								this._connection = null;
							}
						}
						throw;
					}
				}
				this.CloseActiveSequentialStreamAndTextReader();
				if (stateObj != null)
				{
					TdsParserStateObject obj = stateObj;
					lock (obj)
					{
						if (this._stateObj != null)
						{
							if (this._snapshot != null)
							{
								this.PrepareForAsyncContinuation();
							}
							this.SetTimeout(this._defaultTimeoutMilliseconds);
							stateObj._syncOverAsync = true;
							if (!this.TryCloseInternal(true))
							{
								throw SQL.SynchronousCallMayNotPend();
							}
						}
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0006BAD0 File Offset: 0x00069CD0
		private bool TryCloseInternal(bool closeReader)
		{
			TdsParser parser = this._parser;
			TdsParserStateObject stateObj = this._stateObj;
			bool flag = this.IsCommandBehavior(CommandBehavior.CloseConnection);
			bool flag2 = false;
			bool flag3 = false;
			bool result;
			try
			{
				if (!this._isClosed && parser != null && stateObj != null && stateObj._pendingData && parser.State == TdsParserState.OpenLoggedIn)
				{
					if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
					{
						this._sharedState._dataReady = true;
					}
					this._stateObj._internalTimeout = false;
					if (this._sharedState._dataReady)
					{
						flag3 = true;
						if (!this.TryCleanPartialRead())
						{
							return false;
						}
						flag3 = false;
					}
					bool flag4;
					if (!parser.TryRun(RunBehavior.Clean, this._command, this, null, stateObj, out flag4))
					{
						return false;
					}
				}
				this.RestoreServerSettings(parser, stateObj);
				result = true;
			}
			finally
			{
				if (flag2)
				{
					this._isClosed = true;
					this._command = null;
					this._connection = null;
					this._statistics = null;
					this._stateObj = null;
					this._parser = null;
				}
				else if (closeReader)
				{
					bool isClosed = this._isClosed;
					this._isClosed = true;
					this._parser = null;
					this._stateObj = null;
					this._data = null;
					if (this._snapshot != null)
					{
						this.CleanupAfterAsyncInvocationInternal(stateObj, true);
					}
					if (this.Connection != null)
					{
						this.Connection.RemoveWeakReference(this);
					}
					if (!isClosed && stateObj != null)
					{
						if (!flag3)
						{
							stateObj.CloseSession();
						}
						else if (parser != null)
						{
							parser.State = TdsParserState.Broken;
							parser.PutSession(stateObj);
							parser.Connection.BreakConnection();
						}
					}
					this.TrySetMetaData(null, false);
					this._fieldNameLookup = null;
					if (flag && this.Connection != null)
					{
						this.Connection.Close();
					}
					if (this._command != null)
					{
						this._recordsAffected = this._command.InternalRecordsAffected;
					}
					this._command = null;
					this._connection = null;
					this._statistics = null;
				}
			}
			return result;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0006BCA8 File Offset: 0x00069EA8
		internal virtual void CloseReaderFromConnection()
		{
			TdsParser parser = this._parser;
			if (parser != null && parser.State == TdsParserState.OpenLoggedIn)
			{
				this.Close();
				return;
			}
			TdsParserStateObject stateObj = this._stateObj;
			this._isClosed = true;
			this._cancelAsyncOnCloseTokenSource.Cancel();
			if (stateObj != null)
			{
				TaskCompletionSource<object> networkPacketTaskSource = stateObj._networkPacketTaskSource;
				if (networkPacketTaskSource != null)
				{
					networkPacketTaskSource.TrySetException(ADP.ClosedConnectionError());
				}
				if (this._snapshot != null)
				{
					this.CleanupAfterAsyncInvocationInternal(stateObj, false);
				}
				stateObj._syncOverAsync = true;
				stateObj.RemoveOwner();
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0006BD20 File Offset: 0x00069F20
		private bool TryConsumeMetaData()
		{
			while (this._parser != null && this._stateObj != null && this._stateObj._pendingData && !this._metaDataConsumed)
			{
				if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
				{
					if (this._parser.Connection != null)
					{
						this._parser.Connection.DoomThisConnection();
					}
					throw SQL.ConnectionDoomed();
				}
				bool flag;
				if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag))
				{
					return false;
				}
			}
			if (this._metaData != null)
			{
				if (this._snapshot != null && this._snapshot._metadata == this._metaData)
				{
					this._metaData = (_SqlMetaDataSet)this._metaData.Clone();
				}
				this._metaData.visibleColumns = 0;
				int[] array = new int[this._metaData.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this._metaData.visibleColumns;
					if (!this._metaData[i].isHidden)
					{
						this._metaData.visibleColumns++;
					}
				}
				this._metaData.indexMap = array;
			}
			return true;
		}

		/// <summary>Gets a string representing the data type of the specified column.</summary>
		/// <param name="i">The zero-based ordinal position of the column to find.</param>
		/// <returns>The string representing the data type of the specified column.</returns>
		// Token: 0x060017CD RID: 6093 RVA: 0x0006BE60 File Offset: 0x0006A060
		public override string GetDataTypeName(int i)
		{
			SqlStatistics statistics = null;
			string dataTypeNameInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				dataTypeNameInternal = this.GetDataTypeNameInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return dataTypeNameInternal;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0006BEB0 File Offset: 0x0006A0B0
		private string GetDataTypeNameInternal(_SqlMetaData metaData)
		{
			string result;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				result = MetaType.MetaNVarChar.TypeName;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					result = MetaType.MetaMaxVarBinary.TypeName;
				}
				else
				{
					result = MetaType.MetaImage.TypeName;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					result = string.Concat(new string[]
					{
						metaData.udtDatabaseName,
						".",
						metaData.udtSchemaName,
						".",
						metaData.udtTypeName
					});
				}
				else
				{
					result = metaData.metaType.TypeName;
				}
			}
			else
			{
				result = this.GetVersionedMetaType(metaData.metaType).TypeName;
			}
			return result;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0006BF91 File Offset: 0x0006A191
		internal virtual SqlBuffer.StorageType GetVariantInternalStorageType(int i)
		{
			return this._data[i].VariantInternalStorageType;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</returns>
		// Token: 0x060017D0 RID: 6096 RVA: 0x0006BFA0 File Offset: 0x0006A1A0
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this, this.IsCommandBehavior(CommandBehavior.CloseConnection));
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object. If the type does not exist on the client, in the case of a User-Defined Type (UDT) returned from the database, GetFieldType returns null.</returns>
		// Token: 0x060017D1 RID: 6097 RVA: 0x0006BFB0 File Offset: 0x0006A1B0
		public override Type GetFieldType(int i)
		{
			SqlStatistics statistics = null;
			Type fieldTypeInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				fieldTypeInternal = this.GetFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return fieldTypeInternal;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0006C000 File Offset: 0x0006A200
		private Type GetFieldTypeInternal(_SqlMetaData metaData)
		{
			Type result;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				result = MetaType.MetaNVarChar.ClassType;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					result = MetaType.MetaMaxVarBinary.ClassType;
				}
				else
				{
					result = MetaType.MetaImage.ClassType;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					this.Connection.CheckGetExtendedUDTInfo(metaData, false);
					result = metaData.udtType;
				}
				else
				{
					result = metaData.metaType.ClassType;
				}
			}
			else
			{
				result = this.GetVersionedMetaType(metaData.metaType).ClassType;
			}
			return result;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0006C0C0 File Offset: 0x0006A2C0
		internal virtual int GetLocaleId(int i)
		{
			_SqlMetaData sqlMetaData = this.MetaData[i];
			int result;
			if (sqlMetaData.collation != null)
			{
				result = sqlMetaData.collation.LCID;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		/// <summary>Gets the name of the specified column.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The name of the specified column.</returns>
		// Token: 0x060017D4 RID: 6100 RVA: 0x0006C0F3 File Offset: 0x0006A2F3
		public override string GetName(int i)
		{
			this.CheckMetaDataIsReady(i, false);
			return this._metaData[i].column;
		}

		/// <summary>Gets an <see langword="Object" /> that is a representation of the underlying provider-specific field type.</summary>
		/// <param name="i">An <see cref="T:System.Int32" /> representing the column ordinal.</param>
		/// <returns>Gets an <see cref="T:System.Object" /> that is a representation of the underlying provider-specific field type.</returns>
		// Token: 0x060017D5 RID: 6101 RVA: 0x0006C110 File Offset: 0x0006A310
		public override Type GetProviderSpecificFieldType(int i)
		{
			SqlStatistics statistics = null;
			Type providerSpecificFieldTypeInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckMetaDataIsReady(i, false);
				providerSpecificFieldTypeInternal = this.GetProviderSpecificFieldTypeInternal(this._metaData[i]);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return providerSpecificFieldTypeInternal;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0006C160 File Offset: 0x0006A360
		private Type GetProviderSpecificFieldTypeInternal(_SqlMetaData metaData)
		{
			Type result;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				result = MetaType.MetaNVarChar.SqlType;
			}
			else if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					result = MetaType.MetaMaxVarBinary.SqlType;
				}
				else
				{
					result = MetaType.MetaImage.SqlType;
				}
			}
			else if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type == SqlDbType.Udt)
				{
					this.Connection.CheckGetExtendedUDTInfo(metaData, false);
					result = metaData.udtType;
				}
				else
				{
					result = metaData.metaType.SqlType;
				}
			}
			else
			{
				result = this.GetVersionedMetaType(metaData.metaType).SqlType;
			}
			return result;
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <param name="name">The name of the column.</param>
		/// <returns>The zero-based column ordinal.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name.</exception>
		// Token: 0x060017D7 RID: 6103 RVA: 0x0006C220 File Offset: 0x0006A420
		public override int GetOrdinal(string name)
		{
			SqlStatistics statistics = null;
			int ordinal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this._fieldNameLookup == null)
				{
					this.CheckMetaDataIsReady();
					this._fieldNameLookup = new FieldNameLookup(this, this._defaultLCID);
				}
				ordinal = this._fieldNameLookup.GetOrdinal(name);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return ordinal;
		}

		/// <summary>Gets an <see langword="Object" /> that is a representation of the underlying provider specific value.</summary>
		/// <param name="i">An <see cref="T:System.Int32" /> representing the column ordinal.</param>
		/// <returns>An <see cref="T:System.Object" /> that is a representation of the underlying provider specific value.</returns>
		// Token: 0x060017D8 RID: 6104 RVA: 0x0006C284 File Offset: 0x0006A484
		public override object GetProviderSpecificValue(int i)
		{
			return this.GetSqlValue(i);
		}

		/// <summary>Gets an array of objects that are a representation of the underlying provider specific values.</summary>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the column values.</param>
		/// <returns>The array of objects that are a representation of the underlying provider specific values.</returns>
		// Token: 0x060017D9 RID: 6105 RVA: 0x0006C28D File Offset: 0x0006A48D
		public override int GetProviderSpecificValues(object[] values)
		{
			return this.GetSqlValues(values);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed.</exception>
		// Token: 0x060017DA RID: 6106 RVA: 0x0006C298 File Offset: 0x0006A498
		public override DataTable GetSchemaTable()
		{
			SqlStatistics statistics = null;
			DataTable result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if ((this._metaData == null || this._metaData.schemaTable == null) && this.MetaData != null)
				{
					this._metaData.schemaTable = this.BuildSchemaTable();
				}
				_SqlMetaDataSet metaData = this._metaData;
				result = ((metaData != null) ? metaData.schemaTable : null);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017DB RID: 6107 RVA: 0x0006C310 File Offset: 0x0006A510
		public override bool GetBoolean(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Boolean;
		}

		/// <summary>Retrieves data of type XML as an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="i">The value of the specified column.</param>
		/// <returns>The returned object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
		///  Trying to read a previously read column in sequential mode.  
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not xml.</exception>
		// Token: 0x060017DC RID: 6108 RVA: 0x0006C328 File Offset: 0x0006A528
		public virtual XmlReader GetXmlReader(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetXmlReader");
			if (this._metaData[i].metaType.SqlDbType != SqlDbType.Xml)
			{
				throw SQL.XmlReaderNotSupportOnColumnType(this._metaData[i].column);
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this._currentStream = new SqlSequentialStream(this, i);
				this._lastColumnWithDataChunkRead = i;
				return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this._currentStream, true, false);
			}
			this.ReadColumn(i, true, false);
			if (this._data[i].IsNull)
			{
				return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(new MemoryStream(Array.Empty<byte>(), false), true, false);
			}
			return this._data[i].SqlXml.CreateReader();
		}

		/// <summary>Retrieves binary, image, varbinary, UDT, and variant data types as a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>A stream object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
		///  Tried to read a previously-read column in sequential mode.  
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:  
		///
		/// binary  
		///
		/// image  
		///
		/// varbinary  
		///
		/// udt</exception>
		// Token: 0x060017DD RID: 6109 RVA: 0x0006C3E0 File Offset: 0x0006A5E0
		public override Stream GetStream(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetStream");
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsBinType || metaType.SqlDbType == SqlDbType.Timestamp) && metaType.SqlDbType != SqlDbType.Variant)
			{
				throw SQL.StreamNotSupportOnColumnType(this._metaData[i].column);
			}
			if (metaType.SqlDbType != SqlDbType.Variant && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				this._currentStream = new SqlSequentialStream(this, i);
				this._lastColumnWithDataChunkRead = i;
				return this._currentStream;
			}
			this.ReadColumn(i, true, false);
			byte[] buffer;
			if (this._data[i].IsNull)
			{
				buffer = Array.Empty<byte>();
			}
			else
			{
				buffer = this._data[i].SqlBinary.Value;
			}
			return new MemoryStream(buffer, false);
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column as a byte.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017DE RID: 6110 RVA: 0x0006C4AC File Offset: 0x0006A6AC
		public override byte GetByte(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Byte;
		}

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer an array starting at the given buffer offset.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="dataIndex">The index within the field from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start.</param>
		/// <param name="length">The maximum length to copy into the buffer.</param>
		/// <returns>The actual number of bytes read.</returns>
		// Token: 0x060017DF RID: 6111 RVA: 0x0006C4C4 File Offset: 0x0006A6C4
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			SqlStatistics statistics = null;
			long result = 0L;
			this.CheckDataIsReady(i, true, false, "GetBytes");
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsLong && !metaType.IsBinType) || SqlDbType.Xml == metaType.SqlDbType)
			{
				throw SQL.NonBlobColumn(this._metaData[i].column);
			}
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				result = this.GetBytesInternal(i, dataIndex, buffer, bufferIndex, length);
				this._lastColumnWithDataChunkRead = i;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0006C570 File Offset: 0x0006A770
		internal virtual long GetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			long result;
			if (!this.TryGetBytesInternal(i, dataIndex, buffer, bufferIndex, length, out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x0006C5A4 File Offset: 0x0006A7A4
		private bool TryGetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length, out long remaining)
		{
			remaining = 0L;
			int num = 0;
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				if (this._sharedState._nextColumnHeaderToRead <= i && !this.TryReadColumnHeader(i))
				{
					return false;
				}
				if (this._data[i] != null && this._data[i].IsNull)
				{
					throw new SqlNullValueException();
				}
				if (-1L == this._sharedState._columnDataBytesRemaining && this._metaData[i].metaType.IsPlp)
				{
					ulong columnDataBytesRemaining;
					if (!this._parser.TryPlpBytesLeft(this._stateObj, out columnDataBytesRemaining))
					{
						return false;
					}
					this._sharedState._columnDataBytesRemaining = (long)columnDataBytesRemaining;
				}
				if (this._sharedState._columnDataBytesRemaining == 0L)
				{
					return true;
				}
				if (buffer == null)
				{
					if (this._metaData[i].metaType.IsPlp)
					{
						remaining = (long)this._parser.PlpBytesTotalLength(this._stateObj);
						return true;
					}
					remaining = this._sharedState._columnDataBytesRemaining;
					return true;
				}
				else
				{
					if (dataIndex < 0L)
					{
						throw ADP.NegativeParameter("dataIndex");
					}
					if (dataIndex < this._columnDataBytesRead)
					{
						throw ADP.NonSeqByteAccess(dataIndex, this._columnDataBytesRead, "GetBytes");
					}
					long num2 = dataIndex - this._columnDataBytesRead;
					if (num2 > this._sharedState._columnDataBytesRemaining && !this._metaData[i].metaType.IsPlp)
					{
						return true;
					}
					if (bufferIndex < 0 || bufferIndex >= buffer.Length)
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (length + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
					}
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (num2 > 0L)
					{
						if (this._metaData[i].metaType.IsPlp)
						{
							ulong num3;
							if (!this._parser.TrySkipPlpValue((ulong)num2, this._stateObj, out num3))
							{
								return false;
							}
							this._columnDataBytesRead += (long)num3;
						}
						else
						{
							if (!this._stateObj.TrySkipLongBytes(num2))
							{
								return false;
							}
							this._columnDataBytesRead += num2;
							this._sharedState._columnDataBytesRemaining -= num2;
						}
					}
					int num4;
					bool result = this.TryGetBytesInternalSequential(i, buffer, bufferIndex, length, out num4);
					remaining = (long)num4;
					return result;
				}
			}
			else
			{
				if (dataIndex < 0L)
				{
					throw ADP.NegativeParameter("dataIndex");
				}
				if (dataIndex > 2147483647L)
				{
					throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
				}
				int num5 = (int)dataIndex;
				byte[] array;
				if (this._metaData[i].metaType.IsBinType)
				{
					array = this.GetSqlBinary(i).Value;
				}
				else
				{
					SqlString sqlString = this.GetSqlString(i);
					if (this._metaData[i].metaType.IsNCharType)
					{
						array = sqlString.GetUnicodeBytes();
					}
					else
					{
						array = sqlString.GetNonUnicodeBytes();
					}
				}
				num = array.Length;
				if (buffer == null)
				{
					remaining = (long)num;
					return true;
				}
				if (num5 < 0 || num5 >= num)
				{
					return true;
				}
				try
				{
					if (num5 < num)
					{
						if (num5 + length > num)
						{
							num -= num5;
						}
						else
						{
							num = length;
						}
					}
					Buffer.BlockCopy(array, num5, buffer, bufferIndex, num);
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					num = array.Length;
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (bufferIndex < 0 || bufferIndex >= buffer.Length)
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (num + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
					}
					throw;
				}
				remaining = (long)num;
				return true;
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0006C8E0 File Offset: 0x0006AAE0
		internal int GetBytesInternalSequential(int i, byte[] buffer, int index, int length, long? timeoutMilliseconds = null)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			SqlStatistics statistics = null;
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(timeoutMilliseconds ?? this._defaultTimeoutMilliseconds);
				if (!this.TryReadColumnHeader(i))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
				if (!this.TryGetBytesInternalSequential(i, buffer, index, length, out result))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0006C968 File Offset: 0x0006AB68
		internal bool TryGetBytesInternalSequential(int i, byte[] buffer, int index, int length, out int bytesRead)
		{
			bytesRead = 0;
			if (this._sharedState._columnDataBytesRemaining == 0L || length == 0)
			{
				bytesRead = 0;
				return true;
			}
			if (!this._metaData[i].metaType.IsPlp)
			{
				int len = (int)Math.Min((long)length, this._sharedState._columnDataBytesRemaining);
				bool result = this._stateObj.TryReadByteArray(buffer, index, len, out bytesRead);
				this._columnDataBytesRead += (long)bytesRead;
				this._sharedState._columnDataBytesRemaining -= (long)bytesRead;
				return result;
			}
			bool flag = this._stateObj.TryReadPlpBytes(ref buffer, index, length, out bytesRead);
			this._columnDataBytesRead += (long)bytesRead;
			if (!flag)
			{
				return false;
			}
			ulong columnDataBytesRemaining;
			if (!this._parser.TryPlpBytesLeft(this._stateObj, out columnDataBytesRemaining))
			{
				this._sharedState._columnDataBytesRemaining = -1L;
				return false;
			}
			this._sharedState._columnDataBytesRemaining = (long)columnDataBytesRemaining;
			return true;
		}

		/// <summary>Retrieves Char, NChar, NText, NVarChar, text, varChar, and Variant data types as a <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="i">The column to be retrieved.</param>
		/// <returns>The returned object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
		///  Tried to read a previously-read column in sequential mode.  
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:  
		///
		/// char  
		///
		/// nchar  
		///
		/// ntext  
		///
		/// nvarchar  
		///
		/// text  
		///
		/// varchar</exception>
		// Token: 0x060017E4 RID: 6116 RVA: 0x0006CA50 File Offset: 0x0006AC50
		public override TextReader GetTextReader(int i)
		{
			this.CheckDataIsReady(i, false, false, "GetTextReader");
			MetaType metaType = this._metaData[i].metaType;
			if ((!metaType.IsCharType && metaType.SqlDbType != SqlDbType.Variant) || metaType.SqlDbType == SqlDbType.Xml)
			{
				throw SQL.TextReaderNotSupportOnColumnType(this._metaData[i].column);
			}
			if (metaType.SqlDbType != SqlDbType.Variant && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				Encoding encoding;
				if (metaType.IsNCharType)
				{
					encoding = SqlUnicodeEncoding.SqlUnicodeEncodingInstance;
				}
				else
				{
					encoding = this._metaData[i].encoding;
				}
				this._currentTextReader = new SqlSequentialTextReader(this, i, encoding);
				this._lastColumnWithDataChunkRead = i;
				return this._currentTextReader;
			}
			this.ReadColumn(i, true, false);
			string s;
			if (this._data[i].IsNull)
			{
				s = string.Empty;
			}
			else
			{
				s = this._data[i].SqlString.Value;
			}
			return new StringReader(s);
		}

		/// <summary>Gets the value of the specified column as a single character.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017E5 RID: 6117 RVA: 0x00008E4B File Offset: 0x0000704B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int i)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array starting at the given buffer offset.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="dataIndex">The index within the field from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start.</param>
		/// <param name="length">The maximum length to copy into the buffer.</param>
		/// <returns>The actual number of characters read.</returns>
		// Token: 0x060017E6 RID: 6118 RVA: 0x0006CB40 File Offset: 0x0006AD40
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			SqlStatistics statistics = null;
			this.CheckMetaDataIsReady(i, false);
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			long result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (this._metaData[i].metaType.IsPlp && this.IsCommandBehavior(CommandBehavior.SequentialAccess))
				{
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (bufferIndex < 0 || (buffer != null && bufferIndex >= buffer.Length))
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (buffer != null && length + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
					}
					long num;
					if (this._metaData[i].type == SqlDbType.Xml)
					{
						try
						{
							this.CheckDataIsReady(i, true, false, "GetChars");
						}
						catch (Exception ex)
						{
							if (ADP.IsCatchableExceptionType(ex))
							{
								throw new TargetInvocationException(ex);
							}
							throw;
						}
						num = this.GetStreamingXmlChars(i, dataIndex, buffer, bufferIndex, length);
					}
					else
					{
						this.CheckDataIsReady(i, true, false, "GetChars");
						num = this.GetCharsFromPlpData(i, dataIndex, buffer, bufferIndex, length);
					}
					this._lastColumnWithDataChunkRead = i;
					result = num;
				}
				else
				{
					if (this._sharedState._nextColumnDataToRead == i + 1 && this._sharedState._nextColumnHeaderToRead == i + 1 && this._columnDataChars != null && this.IsCommandBehavior(CommandBehavior.SequentialAccess) && dataIndex < this._columnDataCharsRead)
					{
						throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
					}
					if (this._columnDataCharsIndex != i)
					{
						string value = this.GetSqlString(i).Value;
						this._columnDataChars = value.ToCharArray();
						this._columnDataCharsRead = 0L;
						this._columnDataCharsIndex = i;
					}
					int num2 = this._columnDataChars.Length;
					if (dataIndex > 2147483647L)
					{
						throw ADP.InvalidSourceBufferIndex(num2, dataIndex, "dataIndex");
					}
					int num3 = (int)dataIndex;
					if (buffer == null)
					{
						result = (long)num2;
					}
					else if (num3 < 0 || num3 >= num2)
					{
						result = 0L;
					}
					else
					{
						try
						{
							if (num3 < num2)
							{
								if (num3 + length > num2)
								{
									num2 -= num3;
								}
								else
								{
									num2 = length;
								}
							}
							Array.Copy(this._columnDataChars, num3, buffer, bufferIndex, num2);
							this._columnDataCharsRead += (long)num2;
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							num2 = this._columnDataChars.Length;
							if (length < 0)
							{
								throw ADP.InvalidDataLength((long)length);
							}
							if (bufferIndex < 0 || bufferIndex >= buffer.Length)
							{
								throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
							}
							if (num2 + bufferIndex > buffer.Length)
							{
								throw ADP.InvalidBufferSizeOrIndex(num2, bufferIndex);
							}
							throw;
						}
						result = (long)num2;
					}
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0006CE08 File Offset: 0x0006B008
		private long GetCharsFromPlpData(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (!this._metaData[i].metaType.IsCharType)
			{
				throw SQL.NonCharColumn(this._metaData[i].column);
			}
			if (this._sharedState._nextColumnHeaderToRead <= i)
			{
				this.ReadColumnHeader(i);
			}
			if (this._data[i] != null && this._data[i].IsNull)
			{
				throw new SqlNullValueException();
			}
			if (dataIndex < this._columnDataCharsRead)
			{
				throw ADP.NonSeqByteAccess(dataIndex, this._columnDataCharsRead, "GetChars");
			}
			if (dataIndex == 0L)
			{
				this._stateObj._plpdecoder = null;
			}
			bool isNCharType = this._metaData[i].metaType.IsNCharType;
			if (-1L == this._sharedState._columnDataBytesRemaining)
			{
				this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
			}
			if (this._sharedState._columnDataBytesRemaining == 0L)
			{
				this._stateObj._plpdecoder = null;
				return 0L;
			}
			long num;
			if (buffer != null)
			{
				if (dataIndex > this._columnDataCharsRead)
				{
					this._stateObj._plpdecoder = null;
					num = dataIndex - this._columnDataCharsRead;
					num = (isNCharType ? (num << 1) : num);
					num = (long)this._parser.SkipPlpValue((ulong)num, this._stateObj);
					this._columnDataBytesRead += num;
					this._columnDataCharsRead += ((isNCharType && num > 0L) ? (num >> 1) : num);
				}
				num = (long)length;
				if (isNCharType)
				{
					num = (long)this._parser.ReadPlpUnicodeChars(ref buffer, bufferIndex, length, this._stateObj);
					this._columnDataBytesRead += num << 1;
				}
				else
				{
					num = (long)this._parser.ReadPlpAnsiChars(ref buffer, bufferIndex, length, this._metaData[i], this._stateObj);
					this._columnDataBytesRead += num << 1;
				}
				this._columnDataCharsRead += num;
				this._sharedState._columnDataBytesRemaining = (long)this._parser.PlpBytesLeft(this._stateObj);
				return num;
			}
			num = (long)this._parser.PlpBytesTotalLength(this._stateObj);
			if (!isNCharType || num <= 0L)
			{
				return num;
			}
			return num >> 1;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0006D01C File Offset: 0x0006B21C
		internal long GetStreamingXmlChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (this._streamingXml != null && this._streamingXml.ColumnOrdinal != i)
			{
				this._streamingXml.Close();
				this._streamingXml = null;
			}
			SqlStreamingXml sqlStreamingXml;
			if (this._streamingXml == null)
			{
				sqlStreamingXml = new SqlStreamingXml(i, this);
			}
			else
			{
				sqlStreamingXml = this._streamingXml;
			}
			long chars = sqlStreamingXml.GetChars(dataIndex, buffer, bufferIndex, length);
			if (this._streamingXml == null)
			{
				this._streamingXml = sqlStreamingXml;
			}
			return chars;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017E9 RID: 6121 RVA: 0x0006D088 File Offset: 0x0006B288
		public override DateTime GetDateTime(int i)
		{
			this.ReadColumn(i, true, false);
			DateTime result = this._data[i].DateTime;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				result = (DateTime)this._data[i].String;
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017EA RID: 6122 RVA: 0x0006D0E0 File Offset: 0x0006B2E0
		public override decimal GetDecimal(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Decimal;
		}

		/// <summary>Gets the value of the specified column as a double-precision floating point number.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017EB RID: 6123 RVA: 0x0006D0F8 File Offset: 0x0006B2F8
		public override double GetDouble(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Double;
		}

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017EC RID: 6124 RVA: 0x0006D110 File Offset: 0x0006B310
		public override float GetFloat(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Single;
		}

		/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017ED RID: 6125 RVA: 0x0006D128 File Offset: 0x0006B328
		public override Guid GetGuid(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlGuid.Value;
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017EE RID: 6126 RVA: 0x0006D153 File Offset: 0x0006B353
		public override short GetInt16(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int16;
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017EF RID: 6127 RVA: 0x0006D16B File Offset: 0x0006B36B
		public override int GetInt32(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int32;
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x060017F0 RID: 6128 RVA: 0x0006D183 File Offset: 0x0006B383
		public override long GetInt64(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].Int64;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column.</returns>
		// Token: 0x060017F1 RID: 6129 RVA: 0x0006D19B File Offset: 0x0006B39B
		public virtual SqlBoolean GetSqlBoolean(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlBoolean;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		// Token: 0x060017F2 RID: 6130 RVA: 0x0006D1B3 File Offset: 0x0006B3B3
		public virtual SqlBinary GetSqlBinary(int i)
		{
			this.ReadColumn(i, true, true);
			return this._data[i].SqlBinary;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		// Token: 0x060017F3 RID: 6131 RVA: 0x0006D1CB File Offset: 0x0006B3CB
		public virtual SqlByte GetSqlByte(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlByte;
		}

		/// <summary>Gets the value of the specified column as <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		// Token: 0x060017F4 RID: 6132 RVA: 0x0006D1E3 File Offset: 0x0006B3E3
		public virtual SqlBytes GetSqlBytes(int i)
		{
			this.ReadColumn(i, true, false);
			return new SqlBytes(this._data[i].SqlBinary);
		}

		/// <summary>Gets the value of the specified column as <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		// Token: 0x060017F5 RID: 6133 RVA: 0x0006D200 File Offset: 0x0006B400
		public virtual SqlChars GetSqlChars(int i)
		{
			this.ReadColumn(i, true, false);
			SqlString value;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				value = this._data[i].KatmaiDateTimeSqlString;
			}
			else
			{
				value = this._data[i].SqlString;
			}
			return new SqlChars(value);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		// Token: 0x060017F6 RID: 6134 RVA: 0x0006D25A File Offset: 0x0006B45A
		public virtual SqlDateTime GetSqlDateTime(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDateTime;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x060017F7 RID: 6135 RVA: 0x0006D272 File Offset: 0x0006B472
		public virtual SqlDecimal GetSqlDecimal(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDecimal;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		// Token: 0x060017F8 RID: 6136 RVA: 0x0006D28A File Offset: 0x0006B48A
		public virtual SqlGuid GetSqlGuid(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlGuid;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		// Token: 0x060017F9 RID: 6137 RVA: 0x0006D2A2 File Offset: 0x0006B4A2
		public virtual SqlDouble GetSqlDouble(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlDouble;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		// Token: 0x060017FA RID: 6138 RVA: 0x0006D2BA File Offset: 0x0006B4BA
		public virtual SqlInt16 GetSqlInt16(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt16;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		// Token: 0x060017FB RID: 6139 RVA: 0x0006D2D2 File Offset: 0x0006B4D2
		public virtual SqlInt32 GetSqlInt32(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt32;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		// Token: 0x060017FC RID: 6140 RVA: 0x0006D2EA File Offset: 0x0006B4EA
		public virtual SqlInt64 GetSqlInt64(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlInt64;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		// Token: 0x060017FD RID: 6141 RVA: 0x0006D302 File Offset: 0x0006B502
		public virtual SqlMoney GetSqlMoney(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlMoney;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		// Token: 0x060017FE RID: 6142 RVA: 0x0006D31A File Offset: 0x0006B51A
		public virtual SqlSingle GetSqlSingle(int i)
		{
			this.ReadColumn(i, true, false);
			return this._data[i].SqlSingle;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		// Token: 0x060017FF RID: 6143 RVA: 0x0006D334 File Offset: 0x0006B534
		public virtual SqlString GetSqlString(int i)
		{
			this.ReadColumn(i, true, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeSqlString;
			}
			return this._data[i].SqlString;
		}

		/// <summary>Gets the value of the specified column as an XML value.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlXml" /> value that contains the XML stored within the corresponding field.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access columns in a closed <see cref="T:System.Data.SqlClient.SqlDataReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The retrieved data is not compatible with the <see cref="T:System.Data.SqlTypes.SqlXml" /> type.</exception>
		// Token: 0x06001800 RID: 6144 RVA: 0x0006D388 File Offset: 0x0006B588
		public virtual SqlXml GetSqlXml(int i)
		{
			this.ReadColumn(i, true, false);
			SqlXml result;
			if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				result = (this._data[i].IsNull ? SqlXml.Null : this._data[i].SqlCachedBuffer.ToSqlXml());
			}
			else
			{
				SqlXml sqlXml = this._data[i].IsNull ? SqlXml.Null : this._data[i].SqlCachedBuffer.ToSqlXml();
				result = (SqlXml)this._data[i].String;
			}
			return result;
		}

		/// <summary>Returns the data value in the specified column as a SQL Server type.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlDbType" />.</returns>
		// Token: 0x06001801 RID: 6145 RVA: 0x0006D418 File Offset: 0x0006B618
		public virtual object GetSqlValue(int i)
		{
			SqlStatistics statistics = null;
			object sqlValueInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				sqlValueInternal = this.GetSqlValueInternal(i);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return sqlValueInternal;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0006D464 File Offset: 0x0006B664
		private object GetSqlValueInternal(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetSqlValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0006D4A0 File Offset: 0x0006B6A0
		private object GetSqlValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
		{
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				return data.KatmaiDateTimeSqlString;
			}
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
			{
				return data.SqlValue;
			}
			if (this._typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				if (metaData.type != SqlDbType.Udt)
				{
					return data.SqlValue;
				}
				SqlConnection connection = this._connection;
				if (connection != null)
				{
					connection.CheckGetExtendedUDTInfo(metaData, true);
					return connection.GetUdtValue(data.Value, metaData, false);
				}
				throw ADP.DataReaderClosed("GetSqlValueFromSqlBufferInternal");
			}
			else
			{
				if (metaData.type == SqlDbType.Xml)
				{
					return data.SqlString;
				}
				return data.SqlValue;
			}
		}

		/// <summary>Fills an array of <see cref="T:System.Object" /> that contains the values for all the columns in the record, expressed as SQL Server types.</summary>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the values. The column values are expressed as SQL Server types.</param>
		/// <returns>An integer indicating the number of columns copied.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null.</exception>
		// Token: 0x06001804 RID: 6148 RVA: 0x0006D550 File Offset: 0x0006B750
		public virtual int GetSqlValues(object[] values)
		{
			SqlStatistics statistics = null;
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.CheckDataIsReady();
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				int num = (values.Length < this._metaData.visibleColumns) ? values.Length : this._metaData.visibleColumns;
				for (int i = 0; i < num; i++)
				{
					values[this._metaData.indexMap[i]] = this.GetSqlValueInternal(i);
				}
				result = num;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06001805 RID: 6149 RVA: 0x0006D5EC File Offset: 0x0006B7EC
		public override string GetString(int i)
		{
			this.ReadColumn(i, true, false);
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && this._metaData[i].IsNewKatmaiDateTimeType)
			{
				return this._data[i].KatmaiDateTimeString;
			}
			return this._data[i].String;
		}

		/// <summary>Synchronously gets the value of the specified column as a type. <see cref="M:System.Data.SqlClient.SqlDataReader.GetFieldValueAsync``1(System.Int32,System.Threading.CancellationToken)" /> is the asynchronous version of this method.</summary>
		/// <param name="i">The column to be retrieved.</param>
		/// <typeparam name="T">The type of the value to be returned.</typeparam>
		/// <returns>The returned type object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
		///  Tried to read a previously-read column in sequential mode.  
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the column was null (<see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" /> == <see langword="true" />), retrieving a non-SQL type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn't match the type returned by SQL Server or cannot be cast.</exception>
		// Token: 0x06001806 RID: 6150 RVA: 0x0006D640 File Offset: 0x0006B840
		public override T GetFieldValue<T>(int i)
		{
			SqlStatistics statistics = null;
			T fieldValueInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				fieldValueInternal = this.GetFieldValueInternal<T>(i);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return fieldValueInternal;
		}

		/// <summary>Gets the value of the specified column in its native format.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>This method returns <see cref="T:System.DBNull" /> for null database columns.</returns>
		// Token: 0x06001807 RID: 6151 RVA: 0x0006D68C File Offset: 0x0006B88C
		public override object GetValue(int i)
		{
			SqlStatistics statistics = null;
			object valueInternal;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				valueInternal = this.GetValueInternal(i);
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return valueInternal;
		}

		/// <summary>Retrieves the value of the specified column as a <see cref="T:System.TimeSpan" /> object.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06001808 RID: 6152 RVA: 0x0006D6D8 File Offset: 0x0006B8D8
		public virtual TimeSpan GetTimeSpan(int i)
		{
			this.ReadColumn(i, true, false);
			TimeSpan result = this._data[i].Time;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				result = (TimeSpan)this._data[i].String;
			}
			return result;
		}

		/// <summary>Retrieves the value of the specified column as a <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>The value of the specified column.</returns>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
		// Token: 0x06001809 RID: 6153 RVA: 0x0006D720 File Offset: 0x0006B920
		public virtual DateTimeOffset GetDateTimeOffset(int i)
		{
			this.ReadColumn(i, true, false);
			DateTimeOffset result = this._data[i].DateTimeOffset;
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
			{
				result = (DateTimeOffset)this._data[i].String;
			}
			return result;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0006D765 File Offset: 0x0006B965
		private object GetValueInternal(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0006D7A0 File Offset: 0x0006B9A0
		private object GetValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
		{
			if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
			{
				if (data.IsNull)
				{
					return DBNull.Value;
				}
				return data.KatmaiDateTimeString;
			}
			else
			{
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
				{
					return data.Value;
				}
				if (this._typeSystem == SqlConnectionString.TypeSystem.SQLServer2000)
				{
					return data.Value;
				}
				if (metaData.type != SqlDbType.Udt)
				{
					return data.Value;
				}
				SqlConnection connection = this._connection;
				if (connection != null)
				{
					connection.CheckGetExtendedUDTInfo(metaData, true);
					return connection.GetUdtValue(data.Value, metaData, true);
				}
				throw ADP.DataReaderClosed("GetValueFromSqlBufferInternal");
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0006D843 File Offset: 0x0006BA43
		private T GetFieldValueInternal<T>(int i)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, false, false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return this.GetFieldValueFromSqlBufferInternal<T>(this._data[i], this._metaData[i]);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0006D880 File Offset: 0x0006BA80
		private T GetFieldValueFromSqlBufferInternal<T>(SqlBuffer data, _SqlMetaData metaData)
		{
			Type typeFromHandle = typeof(T);
			if (SqlDataReader._typeofINullable.IsAssignableFrom(typeFromHandle))
			{
				object obj = this.GetSqlValueFromSqlBufferInternal(data, metaData);
				if (typeFromHandle == SqlDataReader.s_typeofSqlString)
				{
					SqlXml sqlXml = obj as SqlXml;
					if (sqlXml != null)
					{
						if (sqlXml.IsNull)
						{
							obj = SqlString.Null;
						}
						else
						{
							obj = new SqlString(sqlXml.Value);
						}
					}
				}
				return (T)((object)obj);
			}
			T result;
			try
			{
				result = (T)((object)this.GetValueFromSqlBufferInternal(data, metaData));
			}
			catch (InvalidCastException)
			{
				if (data.IsNull)
				{
					throw SQL.SqlNullValue();
				}
				throw;
			}
			return result;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		// Token: 0x0600180E RID: 6158 RVA: 0x0006D928 File Offset: 0x0006BB28
		public override int GetValues(object[] values)
		{
			SqlStatistics statistics = null;
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			int result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (values == null)
				{
					throw ADP.ArgumentNull("values");
				}
				this.CheckMetaDataIsReady();
				int num = (values.Length < this._metaData.visibleColumns) ? values.Length : this._metaData.visibleColumns;
				int num2 = num - 1;
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				this._commandBehavior &= ~CommandBehavior.SequentialAccess;
				if (!this.TryReadColumn(num2, false, false))
				{
					throw SQL.SynchronousCallMayNotPend();
				}
				for (int i = 0; i < num; i++)
				{
					values[this._metaData.indexMap[i]] = this.GetValueFromSqlBufferInternal(this._data[i], this._metaData[i]);
					if (flag && i < num2)
					{
						this._data[i].Clear();
					}
				}
				result = num;
			}
			finally
			{
				if (flag)
				{
					this._commandBehavior |= CommandBehavior.SequentialAccess;
				}
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0006DA34 File Offset: 0x0006BC34
		private MetaType GetVersionedMetaType(MetaType actualMetaType)
		{
			MetaType result;
			if (actualMetaType == MetaType.MetaUdt)
			{
				result = MetaType.MetaVarBinary;
			}
			else if (actualMetaType == MetaType.MetaXml)
			{
				result = MetaType.MetaNText;
			}
			else if (actualMetaType == MetaType.MetaMaxVarBinary)
			{
				result = MetaType.MetaImage;
			}
			else if (actualMetaType == MetaType.MetaMaxVarChar)
			{
				result = MetaType.MetaText;
			}
			else if (actualMetaType == MetaType.MetaMaxNVarChar)
			{
				result = MetaType.MetaNText;
			}
			else
			{
				result = actualMetaType;
			}
			return result;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0006DA98 File Offset: 0x0006BC98
		private bool TryHasMoreResults(out bool moreResults)
		{
			if (this._parser != null)
			{
				bool flag;
				if (!this.TryHasMoreRows(out flag))
				{
					moreResults = false;
					return false;
				}
				if (flag)
				{
					moreResults = false;
					return true;
				}
				while (this._stateObj._pendingData)
				{
					byte b;
					if (!this._stateObj.TryPeekByte(out b))
					{
						moreResults = false;
						return false;
					}
					if (b <= 210)
					{
						if (b == 129)
						{
							moreResults = true;
							return true;
						}
						if (b - 209 <= 1)
						{
							moreResults = true;
							return true;
						}
					}
					else
					{
						if (b == 211)
						{
							if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.Null)
							{
								this._altMetaDataSetCollection.metaDataSet = this._metaData;
								this._metaData = null;
							}
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.AltRow;
							this._hasRows = true;
							moreResults = true;
							return true;
						}
						if (b == 253)
						{
							this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
							this._metaData = null;
							this._altMetaDataSetCollection = null;
							moreResults = true;
							return true;
						}
					}
					if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
					bool flag2;
					if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag2))
					{
						moreResults = false;
						return false;
					}
				}
			}
			moreResults = false;
			return true;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0006DBBC File Offset: 0x0006BDBC
		private bool TryHasMoreRows(out bool moreRows)
		{
			if (this._parser != null)
			{
				if (this._sharedState._dataReady)
				{
					moreRows = true;
					return true;
				}
				SqlDataReader.ALTROWSTATUS altRowStatus = this._altRowStatus;
				if (altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
				{
					moreRows = true;
					return true;
				}
				if (altRowStatus == SqlDataReader.ALTROWSTATUS.Done)
				{
					moreRows = false;
					return true;
				}
				if (this._stateObj._pendingData)
				{
					byte b;
					if (!this._stateObj.TryPeekByte(out b))
					{
						moreRows = false;
						return false;
					}
					bool flag = false;
					while (b == 253 || b == 254 || b == 255 || (!flag && (b == 228 || b == 227 || b == 169 || b == 170 || b == 171)))
					{
						if (b == 253 || b == 254 || b == 255)
						{
							flag = true;
						}
						if (this._parser.State == TdsParserState.Broken || this._parser.State == TdsParserState.Closed)
						{
							throw ADP.ClosedConnectionError();
						}
						bool flag2;
						if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag2))
						{
							moreRows = false;
							return false;
						}
						if (!this._stateObj._pendingData)
						{
							break;
						}
						if (!this._stateObj.TryPeekByte(out b))
						{
							moreRows = false;
							return false;
						}
					}
					if (this.IsRowToken(b))
					{
						moreRows = true;
						return true;
					}
				}
			}
			moreRows = false;
			return true;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0006DD15 File Offset: 0x0006BF15
		private bool IsRowToken(byte token)
		{
			return 209 == token || 210 == token;
		}

		/// <summary>Gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <returns>
		///   <see langword="true" /> if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise <see langword="false" />.</returns>
		// Token: 0x06001813 RID: 6163 RVA: 0x0006DD29 File Offset: 0x0006BF29
		public override bool IsDBNull(int i)
		{
			this.CheckHeaderIsReady(i, false, "IsDBNull");
			this.SetTimeout(this._defaultTimeoutMilliseconds);
			this.ReadColumnHeader(i);
			return this._data[i].IsNull;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.CommandBehavior" /> matches that of the <see cref="T:System.Data.SqlClient.SqlDataReader" /> .</summary>
		/// <param name="condition">A <see cref="T:System.Data.CommandBehavior" /> enumeration.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Data.CommandBehavior" /> is true, <see langword="false" /> otherwise.</returns>
		// Token: 0x06001814 RID: 6164 RVA: 0x0006DD58 File Offset: 0x0006BF58
		protected internal bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == (condition & this._commandBehavior);
		}

		/// <summary>Advances the data reader to the next result, when reading the results of batch Transact-SQL statements.</summary>
		/// <returns>
		///   <see langword="true" /> if there are more result sets; otherwise <see langword="false" />.</returns>
		// Token: 0x06001815 RID: 6165 RVA: 0x0006DD68 File Offset: 0x0006BF68
		public override bool NextResult()
		{
			if (this._currentTask != null)
			{
				throw SQL.PendingBeginXXXExists();
			}
			bool result;
			if (!this.TryNextResult(out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0006DD94 File Offset: 0x0006BF94
		private bool TryNextResult(out bool more)
		{
			SqlStatistics statistics = null;
			bool result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("NextResult");
				}
				this._fieldNameLookup = null;
				bool flag = false;
				this._hasRows = false;
				if (this.IsCommandBehavior(CommandBehavior.SingleResult))
				{
					if (!this.TryCloseInternal(false))
					{
						more = false;
						result = false;
					}
					else
					{
						this.ClearMetaData();
						more = flag;
						result = true;
					}
				}
				else
				{
					if (this._parser != null)
					{
						bool flag2 = true;
						while (flag2)
						{
							if (!this.TryReadInternal(false, out flag2))
							{
								more = false;
								return false;
							}
						}
					}
					if (this._parser != null)
					{
						bool flag3;
						if (!this.TryHasMoreResults(out flag3))
						{
							more = false;
							return false;
						}
						if (flag3)
						{
							this._metaDataConsumed = false;
							this._browseModeInfoConsumed = false;
							SqlDataReader.ALTROWSTATUS altRowStatus = this._altRowStatus;
							if (altRowStatus != SqlDataReader.ALTROWSTATUS.AltRow)
							{
								if (altRowStatus != SqlDataReader.ALTROWSTATUS.Done)
								{
									if (!this.TryConsumeMetaData())
									{
										more = false;
										return false;
									}
									if (this._metaData == null)
									{
										more = false;
										return true;
									}
								}
								else
								{
									this._metaData = this._altMetaDataSetCollection.metaDataSet;
									this._altRowStatus = SqlDataReader.ALTROWSTATUS.Null;
								}
							}
							else
							{
								int id;
								if (!this._parser.TryGetAltRowId(this._stateObj, out id))
								{
									more = false;
									return false;
								}
								_SqlMetaDataSet altMetaData = this._altMetaDataSetCollection.GetAltMetaData(id);
								if (altMetaData != null)
								{
									this._metaData = altMetaData;
								}
							}
							flag = true;
						}
						else
						{
							if (!this.TryCloseInternal(false))
							{
								more = false;
								return false;
							}
							if (!this.TrySetMetaData(null, false))
							{
								more = false;
								return false;
							}
						}
					}
					else
					{
						this.ClearMetaData();
					}
					more = flag;
					result = true;
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		/// <summary>Advances the <see cref="T:System.Data.SqlClient.SqlDataReader" /> to the next record.</summary>
		/// <returns>
		///   <see langword="true" /> if there are more rows; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
		// Token: 0x06001817 RID: 6167 RVA: 0x0006DF40 File Offset: 0x0006C140
		public override bool Read()
		{
			if (this._currentTask != null)
			{
				throw SQL.PendingBeginXXXExists();
			}
			bool result;
			if (!this.TryReadInternal(true, out result))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return result;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0006DF70 File Offset: 0x0006C170
		private bool TryReadInternal(bool setTimeout, out bool more)
		{
			SqlStatistics statistics = null;
			bool result;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if (this._parser != null)
				{
					if (setTimeout)
					{
						this.SetTimeout(this._defaultTimeoutMilliseconds);
					}
					if (this._sharedState._dataReady && !this.TryCleanPartialRead())
					{
						more = false;
						return false;
					}
					SqlBuffer.Clear(this._data);
					this._sharedState._nextColumnHeaderToRead = 0;
					this._sharedState._nextColumnDataToRead = 0;
					this._sharedState._columnDataBytesRemaining = -1L;
					this._lastColumnWithDataChunkRead = -1;
					if (!this._haltRead)
					{
						bool flag;
						if (!this.TryHasMoreRows(out flag))
						{
							more = false;
							return false;
						}
						if (flag)
						{
							while (this._stateObj._pendingData)
							{
								if (this._altRowStatus == SqlDataReader.ALTROWSTATUS.AltRow)
								{
									this._altRowStatus = SqlDataReader.ALTROWSTATUS.Done;
									this._sharedState._dataReady = true;
									break;
								}
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out this._sharedState._dataReady))
								{
									more = false;
									return false;
								}
								if (this._sharedState._dataReady)
								{
									break;
								}
							}
							if (this._sharedState._dataReady)
							{
								this._haltRead = this.IsCommandBehavior(CommandBehavior.SingleRow);
								more = true;
								return true;
							}
						}
						if (!this._stateObj._pendingData && !this.TryCloseInternal(false))
						{
							more = false;
							return false;
						}
					}
					else
					{
						bool flag2;
						if (!this.TryHasMoreRows(out flag2))
						{
							more = false;
							return false;
						}
						while (flag2)
						{
							while (this._stateObj._pendingData && !this._sharedState._dataReady)
							{
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out this._sharedState._dataReady))
								{
									more = false;
									return false;
								}
							}
							if (this._sharedState._dataReady && !this.TryCleanPartialRead())
							{
								more = false;
								return false;
							}
							SqlBuffer.Clear(this._data);
							this._sharedState._nextColumnHeaderToRead = 0;
							if (!this.TryHasMoreRows(out flag2))
							{
								more = false;
								return false;
							}
						}
						this._haltRead = false;
					}
				}
				else if (this.IsClosed)
				{
					throw ADP.DataReaderClosed("Read");
				}
				more = false;
				result = true;
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return result;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0006E1C8 File Offset: 0x0006C3C8
		private void ReadColumn(int i, bool setTimeout = true, bool allowPartiallyReadColumn = false)
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this.TryReadColumn(i, setTimeout, allowPartiallyReadColumn))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0006E1E9 File Offset: 0x0006C3E9
		private bool TryReadColumn(int i, bool setTimeout, bool allowPartiallyReadColumn = false)
		{
			this.CheckDataIsReady(i, allowPartiallyReadColumn, true, null);
			if (setTimeout)
			{
				this.SetTimeout(this._defaultTimeoutMilliseconds);
			}
			return this.TryReadColumnInternal(i, false);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0006E214 File Offset: 0x0006C414
		private bool TryReadColumnData()
		{
			if (!this._data[this._sharedState._nextColumnDataToRead].IsNull)
			{
				_SqlMetaData md = this._metaData[this._sharedState._nextColumnDataToRead];
				if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], md, (int)this._sharedState._columnDataBytesRemaining, this._stateObj))
				{
					return false;
				}
				this._sharedState._columnDataBytesRemaining = 0L;
			}
			this._sharedState._nextColumnDataToRead++;
			return true;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x0006E2A6 File Offset: 0x0006C4A6
		private void ReadColumnHeader(int i)
		{
			if (!this.TryReadColumnHeader(i))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0006E2B7 File Offset: 0x0006C4B7
		private bool TryReadColumnHeader(int i)
		{
			if (!this._sharedState._dataReady)
			{
				throw SQL.InvalidRead();
			}
			return this.TryReadColumnInternal(i, true);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0006E2D4 File Offset: 0x0006C4D4
		private bool TryReadColumnInternal(int i, bool readHeaderOnly = false)
		{
			if (i < this._sharedState._nextColumnHeaderToRead)
			{
				return i != this._sharedState._nextColumnDataToRead || readHeaderOnly || this.TryReadColumnData();
			}
			bool flag = this.IsCommandBehavior(CommandBehavior.SequentialAccess);
			if (flag)
			{
				if (0 < this._sharedState._nextColumnDataToRead)
				{
					this._data[this._sharedState._nextColumnDataToRead - 1].Clear();
				}
				if (this._lastColumnWithDataChunkRead > -1 && i > this._lastColumnWithDataChunkRead)
				{
					this.CloseActiveSequentialStreamAndTextReader();
				}
			}
			else if (this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead && !this.TryReadColumnData())
			{
				return false;
			}
			if (!this.TryResetBlobState())
			{
				return false;
			}
			for (;;)
			{
				_SqlMetaData sqlMetaData = this._metaData[this._sharedState._nextColumnHeaderToRead];
				if (flag && this._sharedState._nextColumnHeaderToRead < i)
				{
					if (!this._parser.TrySkipValue(sqlMetaData, this._sharedState._nextColumnHeaderToRead, this._stateObj))
					{
						break;
					}
					this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
					this._sharedState._nextColumnHeaderToRead++;
				}
				else
				{
					bool flag2;
					ulong num;
					if (!this._parser.TryProcessColumnHeader(sqlMetaData, this._stateObj, this._sharedState._nextColumnHeaderToRead, out flag2, out num))
					{
						return false;
					}
					this._sharedState._nextColumnDataToRead = this._sharedState._nextColumnHeaderToRead;
					this._sharedState._nextColumnHeaderToRead++;
					if (flag2 && sqlMetaData.type != SqlDbType.Timestamp)
					{
						this._parser.GetNullSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData);
						if (!readHeaderOnly)
						{
							this._sharedState._nextColumnDataToRead++;
						}
					}
					else if (i > this._sharedState._nextColumnDataToRead || !readHeaderOnly)
					{
						if (!this._parser.TryReadSqlValue(this._data[this._sharedState._nextColumnDataToRead], sqlMetaData, (int)num, this._stateObj))
						{
							return false;
						}
						this._sharedState._nextColumnDataToRead++;
					}
					else
					{
						this._sharedState._columnDataBytesRemaining = (long)num;
					}
				}
				if (this._snapshot != null)
				{
					this._snapshot = null;
					this.PrepareAsyncInvocation(true);
				}
				if (this._sharedState._nextColumnHeaderToRead > i)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0006E510 File Offset: 0x0006C710
		private bool WillHaveEnoughData(int targetColumn, bool headerOnly = false)
		{
			if (this._lastColumnWithDataChunkRead == this._sharedState._nextColumnDataToRead && this._metaData[this._lastColumnWithDataChunkRead].metaType.IsPlp)
			{
				return false;
			}
			int num = Math.Min(checked(this._stateObj._inBytesRead - this._stateObj._inBytesUsed), this._stateObj._inBytesPacket);
			num--;
			if (targetColumn >= this._sharedState._nextColumnDataToRead && this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead)
			{
				if (this._sharedState._columnDataBytesRemaining > (long)num)
				{
					return false;
				}
				checked
				{
					num -= (int)this._sharedState._columnDataBytesRemaining;
				}
			}
			int num2 = this._sharedState._nextColumnHeaderToRead;
			while (num >= 0 && num2 <= targetColumn)
			{
				checked
				{
					if (!this._stateObj.IsNullCompressionBitSet(num2))
					{
						MetaType metaType = this._metaData[num2].metaType;
						if (metaType.IsLong || metaType.IsPlp || metaType.SqlDbType == SqlDbType.Udt || metaType.SqlDbType == SqlDbType.Structured)
						{
							return false;
						}
						byte b = this._metaData[num2].tdsType & 48;
						int num3;
						if (b == 32 || b == 0)
						{
							if ((this._metaData[num2].tdsType & 128) != 0)
							{
								num3 = 2;
							}
							else if ((this._metaData[num2].tdsType & 12) == 0)
							{
								num3 = 4;
							}
							else
							{
								num3 = 1;
							}
						}
						else
						{
							num3 = 0;
						}
						num -= num3;
						if (num2 < targetColumn || !headerOnly)
						{
							num -= this._metaData[num2].length;
						}
					}
				}
				num2++;
			}
			return num >= 0;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0006E6B0 File Offset: 0x0006C8B0
		private bool TryResetBlobState()
		{
			if (this._sharedState._nextColumnDataToRead < this._sharedState._nextColumnHeaderToRead)
			{
				if (this._sharedState._nextColumnHeaderToRead > 0 && this._metaData[this._sharedState._nextColumnHeaderToRead - 1].metaType.IsPlp)
				{
					ulong num;
					if (this._stateObj._longlen != 0UL && !this._stateObj.Parser.TrySkipPlpValue(18446744073709551615UL, this._stateObj, out num))
					{
						return false;
					}
					if (this._streamingXml != null)
					{
						SqlStreamingXml streamingXml = this._streamingXml;
						this._streamingXml = null;
						streamingXml.Close();
					}
				}
				else if (0L < this._sharedState._columnDataBytesRemaining && !this._stateObj.TrySkipLongBytes(this._sharedState._columnDataBytesRemaining))
				{
					return false;
				}
			}
			this._sharedState._columnDataBytesRemaining = 0L;
			this._columnDataBytesRead = 0L;
			this._columnDataCharsRead = 0L;
			this._columnDataChars = null;
			this._columnDataCharsIndex = -1;
			this._stateObj._plpdecoder = null;
			return true;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0006E7B2 File Offset: 0x0006C9B2
		private void CloseActiveSequentialStreamAndTextReader()
		{
			if (this._currentStream != null)
			{
				this._currentStream.SetClosed();
				this._currentStream = null;
			}
			if (this._currentTextReader != null)
			{
				this._currentTextReader.SetClosed();
				this._currentStream = null;
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0006E7E8 File Offset: 0x0006C9E8
		private void RestoreServerSettings(TdsParser parser, TdsParserStateObject stateObj)
		{
			if (parser != null && this._resetOptionsString != null)
			{
				if (parser.State == TdsParserState.OpenLoggedIn)
				{
					parser.TdsExecuteSQLBatch(this._resetOptionsString, (this._command != null) ? this._command.CommandTimeout : 0, null, stateObj, true, false);
					parser.Run(RunBehavior.UntilDone, this._command, this, null, stateObj);
				}
				this._resetOptionsString = null;
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0006E848 File Offset: 0x0006CA48
		internal bool TrySetAltMetaDataSet(_SqlMetaDataSet metaDataSet, bool metaDataConsumed)
		{
			if (this._altMetaDataSetCollection == null)
			{
				this._altMetaDataSetCollection = new _SqlMetaDataSetCollection();
			}
			else if (this._snapshot != null && this._snapshot._altMetaDataSetCollection == this._altMetaDataSetCollection)
			{
				this._altMetaDataSetCollection = (_SqlMetaDataSetCollection)this._altMetaDataSetCollection.Clone();
			}
			this._altMetaDataSetCollection.SetAltMetaData(metaDataSet);
			this._metaDataConsumed = metaDataConsumed;
			if (this._metaDataConsumed && this._parser != null)
			{
				byte b;
				if (!this._stateObj.TryPeekByte(out b))
				{
					return false;
				}
				if (169 == b)
				{
					bool flag;
					if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, this, null, this._stateObj, out flag))
					{
						return false;
					}
					if (!this._stateObj.TryPeekByte(out b))
					{
						return false;
					}
				}
				if (b == 171)
				{
					try
					{
						this._stateObj._accumulateInfoEvents = true;
						bool flag2;
						if (!this._parser.TryRun(RunBehavior.ReturnImmediately, this._command, null, null, this._stateObj, out flag2))
						{
							return false;
						}
					}
					finally
					{
						this._stateObj._accumulateInfoEvents = false;
					}
					if (!this._stateObj.TryPeekByte(out b))
					{
						return false;
					}
				}
				this._hasRows = this.IsRowToken(b);
			}
			if (metaDataSet != null && (this._data == null || this._data.Length < metaDataSet.Length))
			{
				this._data = SqlBuffer.CreateBufferArray(metaDataSet.Length);
			}
			return true;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0006E9B0 File Offset: 0x0006CBB0
		private void ClearMetaData()
		{
			this._metaData = null;
			this._tableNames = null;
			this._fieldNameLookup = null;
			this._metaDataConsumed = false;
			this._browseModeInfoConsumed = false;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0006E9D8 File Offset: 0x0006CBD8
		internal bool TrySetMetaData(_SqlMetaDataSet metaData, bool moreInfo)
		{
			this._metaData = metaData;
			this._tableNames = null;
			if (this._metaData != null)
			{
				this._data = SqlBuffer.CreateBufferArray(metaData.Length);
			}
			this._fieldNameLookup = null;
			if (metaData != null)
			{
				if (!moreInfo)
				{
					this._metaDataConsumed = true;
					if (this._parser != null)
					{
						byte b;
						if (!this._stateObj.TryPeekByte(out b))
						{
							return false;
						}
						if (b == 169)
						{
							bool flag;
							if (!this._parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, this._stateObj, out flag))
							{
								return false;
							}
							if (!this._stateObj.TryPeekByte(out b))
							{
								return false;
							}
						}
						if (b == 171)
						{
							try
							{
								this._stateObj._accumulateInfoEvents = true;
								bool flag2;
								if (!this._parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, this._stateObj, out flag2))
								{
									return false;
								}
							}
							finally
							{
								this._stateObj._accumulateInfoEvents = false;
							}
							if (!this._stateObj.TryPeekByte(out b))
							{
								return false;
							}
						}
						this._hasRows = this.IsRowToken(b);
						if (136 == b)
						{
							this._metaDataConsumed = false;
						}
					}
				}
			}
			else
			{
				this._metaDataConsumed = false;
			}
			this._browseModeInfoConsumed = false;
			return true;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x0006EB08 File Offset: 0x0006CD08
		private void SetTimeout(long timeoutMilliseconds)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null)
			{
				stateObj.SetTimeoutMilliseconds(timeoutMilliseconds);
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x0006EB26 File Offset: 0x0006CD26
		private bool HasActiveStreamOrTextReaderOnColumn(int columnIndex)
		{
			return false | (this._currentStream != null && this._currentStream.ColumnIndex == columnIndex) | (this._currentTextReader != null && this._currentTextReader.ColumnIndex == columnIndex);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x0006EB5D File Offset: 0x0006CD5D
		private void CheckMetaDataIsReady()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x0006EB7B File Offset: 0x0006CD7B
		private void CheckMetaDataIsReady(int columnIndex, bool permitAsync = false)
		{
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (this.MetaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x0006EBB4 File Offset: 0x0006CDB4
		private void CheckDataIsReady()
		{
			if (this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x0006EBE0 File Offset: 0x0006CDE0
		private void CheckHeaderIsReady(int columnIndex, bool permitAsync = false, [CallerMemberName] string methodName = null)
		{
			if (this._isClosed)
			{
				throw ADP.DataReaderClosed(methodName ?? "CheckHeaderIsReady");
			}
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnHeaderToRead > columnIndex + 1 || this._lastColumnWithDataChunkRead > columnIndex))
			{
				throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(this._sharedState._nextColumnHeaderToRead - 1, this._lastColumnWithDataChunkRead));
			}
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x0006EC8C File Offset: 0x0006CE8C
		private void CheckDataIsReady(int columnIndex, bool allowPartiallyReadColumn = false, bool permitAsync = false, [CallerMemberName] string methodName = null)
		{
			if (this._isClosed)
			{
				throw ADP.DataReaderClosed(methodName ?? "CheckDataIsReady");
			}
			if (!permitAsync && this._currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (!this._sharedState._dataReady || this._metaData == null)
			{
				throw SQL.InvalidRead();
			}
			if (columnIndex < 0 || columnIndex >= this._metaData.Length)
			{
				throw ADP.IndexOutOfRange();
			}
			if (this.IsCommandBehavior(CommandBehavior.SequentialAccess) && (this._sharedState._nextColumnDataToRead > columnIndex || this._lastColumnWithDataChunkRead > columnIndex || (!allowPartiallyReadColumn && this._lastColumnWithDataChunkRead == columnIndex) || (allowPartiallyReadColumn && this.HasActiveStreamOrTextReaderOnColumn(columnIndex))))
			{
				throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(this._sharedState._nextColumnDataToRead, this._lastColumnWithDataChunkRead + 1));
			}
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0006ED4E File Offset: 0x0006CF4E
		[Conditional("DEBUG")]
		private void AssertReaderState(bool requireData, bool permitAsync, int? columnIndex = null, bool enforceSequentialAccess = false)
		{
			bool flag = columnIndex != null;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.NextResult" />, which advances the data reader to the next result, when reading the results of batch Transact-SQL statements.  
		///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlDataReader.NextResultAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
		// Token: 0x0600182E RID: 6190 RVA: 0x0006ED58 File Offset: 0x0006CF58
		public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			if (this.IsClosed)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("NextResultAsync")));
				return taskCompletionSource.Task;
			}
			IDisposable objectToDispose = null;
			if (cancellationToken.CanBeCanceled)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					taskCompletionSource.SetCanceled();
					return taskCompletionSource.Task;
				}
				objectToDispose = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this._command);
			}
			if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
				return taskCompletionSource.Task;
			}
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
				this._currentTask = null;
				return taskCompletionSource.Task;
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<bool>> moreFunc = null;
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					this.PrepareForAsyncContinuation();
				}
				bool flag;
				if (!this.TryNextResult(out flag))
				{
					return this.ContinueRetryable<bool>(moreFunc);
				}
				if (!flag)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			};
			return this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, objectToDispose);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0006EE6C File Offset: 0x0006D06C
		internal Task<int> GetBytesAsync(int i, byte[] buffer, int index, int length, int timeout, CancellationToken cancellationToken, out int bytesRead)
		{
			bytesRead = 0;
			if (this.IsClosed)
			{
				return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetBytesAsync")));
			}
			if (this._currentTask != null)
			{
				return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
			}
			if (cancellationToken.CanBeCanceled && cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			if (this._sharedState._nextColumnHeaderToRead > this._lastColumnWithDataChunkRead && this._sharedState._nextColumnDataToRead >= this._lastColumnWithDataChunkRead)
			{
				this.PrepareAsyncInvocation(false);
				Task<int> bytesAsyncReadDataStage;
				try
				{
					bytesAsyncReadDataStage = this.GetBytesAsyncReadDataStage(i, buffer, index, length, timeout, false, cancellationToken, CancellationToken.None, out bytesRead);
				}
				catch
				{
					this.CleanupAfterAsyncInvocation(false);
					throw;
				}
				return bytesAsyncReadDataStage;
			}
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource.Task;
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<int>> moreFunc = null;
			CancellationToken timeoutToken = CancellationToken.None;
			CancellationTokenSource cancellationTokenSource = null;
			if (timeout > 0)
			{
				cancellationTokenSource = new CancellationTokenSource();
				cancellationTokenSource.CancelAfter(timeout);
				timeoutToken = cancellationTokenSource.Token;
			}
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					this.PrepareForAsyncContinuation();
				}
				this.SetTimeout(this._defaultTimeoutMilliseconds);
				if (!this.TryReadColumnHeader(i))
				{
					return this.ContinueRetryable<int>(moreFunc);
				}
				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<int>(cancellationToken);
				}
				if (timeoutToken.IsCancellationRequested)
				{
					return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				this.SwitchToAsyncWithoutSnapshot();
				int result;
				Task<int> bytesAsyncReadDataStage2 = this.GetBytesAsyncReadDataStage(i, buffer, index, length, timeout, true, cancellationToken, timeoutToken, out result);
				if (bytesAsyncReadDataStage2 == null)
				{
					return Task.FromResult<int>(result);
				}
				return bytesAsyncReadDataStage2;
			};
			return this.InvokeRetryable<int>(moreFunc, taskCompletionSource, cancellationTokenSource);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0006F01C File Offset: 0x0006D21C
		private Task<int> GetBytesAsyncReadDataStage(int i, byte[] buffer, int index, int length, int timeout, bool isContinuation, CancellationToken cancellationToken, CancellationToken timeoutToken, out int bytesRead)
		{
			SqlDataReader.<>c__DisplayClass189_0 CS$<>8__locals1 = new SqlDataReader.<>c__DisplayClass189_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.cancellationToken = cancellationToken;
			CS$<>8__locals1.timeoutToken = timeoutToken;
			CS$<>8__locals1.i = i;
			CS$<>8__locals1.buffer = buffer;
			CS$<>8__locals1.index = index;
			CS$<>8__locals1.length = length;
			this._lastColumnWithDataChunkRead = CS$<>8__locals1.i;
			CS$<>8__locals1.source = null;
			CS$<>8__locals1.timeoutCancellationSource = null;
			this.SetTimeout(this._defaultTimeoutMilliseconds);
			if (this.TryGetBytesInternalSequential(CS$<>8__locals1.i, CS$<>8__locals1.buffer, CS$<>8__locals1.index, CS$<>8__locals1.length, out bytesRead))
			{
				if (!isContinuation)
				{
					this.CleanupAfterAsyncInvocation(false);
				}
				return null;
			}
			int totalBytesRead = bytesRead;
			if (!isContinuation)
			{
				CS$<>8__locals1.source = new TaskCompletionSource<int>();
				if (Interlocked.CompareExchange<Task>(ref this._currentTask, CS$<>8__locals1.source.Task, null) != null)
				{
					CS$<>8__locals1.source.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					return CS$<>8__locals1.source.Task;
				}
				if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					CS$<>8__locals1.source.SetCanceled();
					this._currentTask = null;
					return CS$<>8__locals1.source.Task;
				}
				if (timeout > 0)
				{
					CS$<>8__locals1.timeoutCancellationSource = new CancellationTokenSource();
					CS$<>8__locals1.timeoutCancellationSource.CancelAfter(timeout);
					CS$<>8__locals1.timeoutToken = CS$<>8__locals1.timeoutCancellationSource.Token;
				}
			}
			Func<Task, Task<int>> moreFunc = null;
			moreFunc = delegate(Task _)
			{
				CS$<>8__locals1.<>4__this.PrepareForAsyncContinuation();
				if (CS$<>8__locals1.cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<int>(CS$<>8__locals1.cancellationToken);
				}
				if (CS$<>8__locals1.timeoutToken.IsCancellationRequested)
				{
					return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				CS$<>8__locals1.<>4__this.SetTimeout(CS$<>8__locals1.<>4__this._defaultTimeoutMilliseconds);
				int num;
				bool flag = CS$<>8__locals1.<>4__this.TryGetBytesInternalSequential(CS$<>8__locals1.i, CS$<>8__locals1.buffer, CS$<>8__locals1.index + totalBytesRead, CS$<>8__locals1.length - totalBytesRead, out num);
				totalBytesRead += num;
				if (flag)
				{
					return Task.FromResult<int>(totalBytesRead);
				}
				return CS$<>8__locals1.<>4__this.ContinueRetryable<int>(moreFunc);
			};
			Task<int> task = this.ContinueRetryable<int>(moreFunc);
			if (isContinuation)
			{
				return task;
			}
			task.ContinueWith(delegate(Task<int> t)
			{
				CS$<>8__locals1.<>4__this.CompleteRetryable<int>(t, CS$<>8__locals1.source, CS$<>8__locals1.timeoutCancellationSource);
			}, TaskScheduler.Default);
			return CS$<>8__locals1.source.Task;
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.Read" />, which advances the <see cref="T:System.Data.SqlClient.SqlDataReader" /> to the next record.  
		///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses. Exceptions will be reported via the returned Task object.</summary>
		/// <param name="cancellationToken">The cancellation instruction.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlDataReader.ReadAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
		// Token: 0x06001831 RID: 6193 RVA: 0x0006F204 File Offset: 0x0006D404
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			if (this.IsClosed)
			{
				return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("ReadAsync")));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<bool>(cancellationToken);
			}
			if (this._currentTask != null)
			{
				return Task.FromException<bool>(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
			}
			bool rowTokenRead = false;
			bool more = false;
			try
			{
				if (!this._haltRead && (!this._sharedState._dataReady || this.WillHaveEnoughData(this._metaData.Length - 1, false)))
				{
					if (this._sharedState._dataReady)
					{
						this.CleanPartialReadReliable();
					}
					if (this._stateObj.IsRowTokenReady())
					{
						this.TryReadInternal(true, out more);
						rowTokenRead = true;
						if (!more)
						{
							return ADP.FalseTask;
						}
						if (this.IsCommandBehavior(CommandBehavior.SequentialAccess))
						{
							return ADP.TrueTask;
						}
						if (this.WillHaveEnoughData(this._metaData.Length - 1, false))
						{
							this.TryReadColumn(this._metaData.Length - 1, true, false);
							return ADP.TrueTask;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return Task.FromException<bool>(ex);
			}
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
				return taskCompletionSource.Task;
			}
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
				this._currentTask = null;
				return taskCompletionSource.Task;
			}
			IDisposable objectToDispose = null;
			if (cancellationToken.CanBeCanceled)
			{
				objectToDispose = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this._command);
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<bool>> moreFunc = null;
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					this.PrepareForAsyncContinuation();
				}
				if (rowTokenRead || this.TryReadInternal(true, out more))
				{
					if (!more || (this._commandBehavior & CommandBehavior.SequentialAccess) == CommandBehavior.SequentialAccess)
					{
						if (!more)
						{
							return ADP.FalseTask;
						}
						return ADP.TrueTask;
					}
					else
					{
						if (!rowTokenRead)
						{
							rowTokenRead = true;
							this._snapshot = null;
							this.PrepareAsyncInvocation(true);
						}
						if (this.TryReadColumn(this._metaData.Length - 1, true, false))
						{
							return ADP.TrueTask;
						}
					}
				}
				return this.ContinueRetryable<bool>(moreFunc);
			};
			return this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, objectToDispose);
		}

		/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values.  
		///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses. Exceptions will be reported via the returned Task object.</summary>
		/// <param name="i">The zero-based column to be retrieved.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of <see langword="CancellationToken.None" /> makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <returns>
		///   <see langword="true" /> if the specified column value is equivalent to <see langword="DBNull" /> otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
		///  Trying to read a previously read column in sequential mode.  
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		// Token: 0x06001832 RID: 6194 RVA: 0x0006F41C File Offset: 0x0006D61C
		public override Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
		{
			try
			{
				this.CheckHeaderIsReady(i, false, "IsDBNullAsync");
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return Task.FromException<bool>(ex);
			}
			if (this._sharedState._nextColumnHeaderToRead > i && !cancellationToken.IsCancellationRequested && this._currentTask == null)
			{
				SqlBuffer[] data = this._data;
				if (data == null)
				{
					return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("IsDBNullAsync")));
				}
				if (!data[i].IsNull)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			}
			else
			{
				if (this._currentTask != null)
				{
					return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				}
				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<bool>(cancellationToken);
				}
				try
				{
					if (this.WillHaveEnoughData(i, true))
					{
						this.ReadColumnHeader(i);
						return this._data[i].IsNull ? ADP.TrueTask : ADP.FalseTask;
					}
				}
				catch (Exception ex2)
				{
					if (!ADP.IsCatchableExceptionType(ex2))
					{
						throw;
					}
					return Task.FromException<bool>(ex2);
				}
				TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
				if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
				{
					taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					return taskCompletionSource.Task;
				}
				if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					taskCompletionSource.SetCanceled();
					this._currentTask = null;
					return taskCompletionSource.Task;
				}
				IDisposable objectToDispose = null;
				if (cancellationToken.CanBeCanceled)
				{
					objectToDispose = cancellationToken.Register(delegate(object s)
					{
						((SqlCommand)s).CancelIgnoreFailure();
					}, this._command);
				}
				this.PrepareAsyncInvocation(true);
				Func<Task, Task<bool>> moreFunc = null;
				moreFunc = delegate(Task t)
				{
					if (t != null)
					{
						this.PrepareForAsyncContinuation();
					}
					if (!this.TryReadColumnHeader(i))
					{
						return this.ContinueRetryable<bool>(moreFunc);
					}
					if (!this._data[i].IsNull)
					{
						return ADP.FalseTask;
					}
					return ADP.TrueTask;
				};
				return this.InvokeRetryable<bool>(moreFunc, taskCompletionSource, objectToDispose);
			}
			Task<bool> result;
			return result;
		}

		/// <summary>Asynchronously gets the value of the specified column as a type. <see cref="M:System.Data.SqlClient.SqlDataReader.GetFieldValue``1(System.Int32)" /> is the synchronous version of this method.</summary>
		/// <param name="i">The column to be retrieved.</param>
		/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of <see langword="CancellationToken.None" /> makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />. The returned task must be marked as cancelled.</param>
		/// <typeparam name="T">The type of the value to be returned.</typeparam>
		/// <returns>The returned type object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
		///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
		///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
		///  Tried to read a previously-read column in sequential mode.  
		///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.  
		///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the column was null (<see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" /> == <see langword="true" />), retrieving a non-SQL type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="T" /> doesn't match the type returned by SQL Server or cannot be cast.</exception>
		// Token: 0x06001833 RID: 6195 RVA: 0x0006F624 File Offset: 0x0006D824
		public override Task<T> GetFieldValueAsync<T>(int i, CancellationToken cancellationToken)
		{
			try
			{
				this.CheckDataIsReady(i, false, false, "GetFieldValueAsync");
				if (!this.IsCommandBehavior(CommandBehavior.SequentialAccess) && this._sharedState._nextColumnDataToRead > i && !cancellationToken.IsCancellationRequested && this._currentTask == null)
				{
					SqlBuffer[] data = this._data;
					_SqlMetaDataSet metaData = this._metaData;
					if (data != null && metaData != null)
					{
						return Task.FromResult<T>(this.GetFieldValueFromSqlBufferInternal<T>(data[i], metaData[i]));
					}
					return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetFieldValueAsync")));
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				return Task.FromException<T>(ex);
			}
			if (this._currentTask != null)
			{
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<T>(cancellationToken);
			}
			try
			{
				if (this.WillHaveEnoughData(i, false))
				{
					return Task.FromResult<T>(this.GetFieldValueInternal<T>(i));
				}
			}
			catch (Exception ex2)
			{
				if (!ADP.IsCatchableExceptionType(ex2))
				{
					throw;
				}
				return Task.FromException<T>(ex2);
			}
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			if (Interlocked.CompareExchange<Task>(ref this._currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource.Task;
			}
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
				this._currentTask = null;
				return taskCompletionSource.Task;
			}
			IDisposable objectToDispose = null;
			if (cancellationToken.CanBeCanceled)
			{
				objectToDispose = cancellationToken.Register(delegate(object s)
				{
					((SqlCommand)s).CancelIgnoreFailure();
				}, this._command);
			}
			this.PrepareAsyncInvocation(true);
			Func<Task, Task<T>> moreFunc = null;
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					this.PrepareForAsyncContinuation();
				}
				if (this.TryReadColumn(i, false, false))
				{
					return Task.FromResult<T>(this.GetFieldValueFromSqlBufferInternal<T>(this._data[i], this._metaData[i]));
				}
				return this.ContinueRetryable<T>(moreFunc);
			};
			return this.InvokeRetryable<T>(moreFunc, taskCompletionSource, objectToDispose);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0006F83C File Offset: 0x0006DA3C
		private Task<T> ContinueRetryable<T>(Func<Task, Task<T>> moreFunc)
		{
			TaskCompletionSource<object> networkPacketTaskSource = this._stateObj._networkPacketTaskSource;
			if (this._cancelAsyncOnCloseToken.IsCancellationRequested || networkPacketTaskSource == null)
			{
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}
			return networkPacketTaskSource.Task.ContinueWith<Task<T>>(delegate(Task<object> retryTask)
			{
				if (retryTask.IsFaulted)
				{
					return Task.FromException<T>(retryTask.Exception.InnerException);
				}
				if (!this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					TdsParserStateObject stateObj = this._stateObj;
					if (stateObj != null)
					{
						TdsParserStateObject obj = stateObj;
						lock (obj)
						{
							if (this._stateObj != null)
							{
								if (retryTask.IsCanceled)
								{
									if (this._parser != null)
									{
										this._parser.State = TdsParserState.Broken;
										this._parser.Connection.BreakConnection();
										this._parser.ThrowExceptionAndWarning(this._stateObj, false, false);
									}
								}
								else if (!this.IsClosed)
								{
									try
									{
										return moreFunc(retryTask);
									}
									catch (Exception)
									{
										this.CleanupAfterAsyncInvocation(false);
										throw;
									}
								}
							}
						}
					}
				}
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}, TaskScheduler.Default).Unwrap<T>();
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0006F8AC File Offset: 0x0006DAAC
		private Task<T> InvokeRetryable<T>(Func<Task, Task<T>> moreFunc, TaskCompletionSource<T> source, IDisposable objectToDispose = null)
		{
			try
			{
				Task<T> task;
				try
				{
					task = moreFunc(null);
				}
				catch (Exception exception)
				{
					task = Task.FromException<T>(exception);
				}
				if (task.IsCompleted)
				{
					this.CompleteRetryable<T>(task, source, objectToDispose);
				}
				else
				{
					task.ContinueWith(delegate(Task<T> t)
					{
						this.CompleteRetryable<T>(t, source, objectToDispose);
					}, TaskScheduler.Default);
				}
			}
			catch (AggregateException ex)
			{
				source.TrySetException(ex.InnerException);
			}
			catch (Exception exception2)
			{
				source.TrySetException(exception2);
			}
			return source.Task;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0006F978 File Offset: 0x0006DB78
		private void CompleteRetryable<T>(Task<T> task, TaskCompletionSource<T> source, IDisposable objectToDispose)
		{
			if (objectToDispose != null)
			{
				objectToDispose.Dispose();
			}
			TdsParserStateObject stateObj = this._stateObj;
			bool ignoreCloseToken = stateObj != null && stateObj._syncOverAsync;
			this.CleanupAfterAsyncInvocation(ignoreCloseToken);
			Interlocked.CompareExchange<Task>(ref this._currentTask, null, source.Task);
			if (task.IsFaulted)
			{
				Exception innerException = task.Exception.InnerException;
				source.TrySetException(innerException);
				return;
			}
			if (task.IsCanceled)
			{
				source.TrySetCanceled();
				return;
			}
			source.TrySetResult(task.Result);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0006F9F8 File Offset: 0x0006DBF8
		private void PrepareAsyncInvocation(bool useSnapshot)
		{
			if (useSnapshot)
			{
				if (this._snapshot == null)
				{
					this._snapshot = new SqlDataReader.Snapshot
					{
						_dataReady = this._sharedState._dataReady,
						_haltRead = this._haltRead,
						_metaDataConsumed = this._metaDataConsumed,
						_browseModeInfoConsumed = this._browseModeInfoConsumed,
						_hasRows = this._hasRows,
						_altRowStatus = this._altRowStatus,
						_nextColumnDataToRead = this._sharedState._nextColumnDataToRead,
						_nextColumnHeaderToRead = this._sharedState._nextColumnHeaderToRead,
						_columnDataBytesRead = this._columnDataBytesRead,
						_columnDataBytesRemaining = this._sharedState._columnDataBytesRemaining,
						_metadata = this._metaData,
						_altMetaDataSetCollection = this._altMetaDataSetCollection,
						_tableNames = this._tableNames,
						_currentStream = this._currentStream,
						_currentTextReader = this._currentTextReader
					};
					this._stateObj.SetSnapshot();
				}
			}
			else
			{
				this._stateObj._asyncReadWithoutSnapshot = true;
			}
			this._stateObj._syncOverAsync = false;
			this._stateObj._executionContext = ExecutionContext.Capture();
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0006FB20 File Offset: 0x0006DD20
		private void CleanupAfterAsyncInvocation(bool ignoreCloseToken = false)
		{
			TdsParserStateObject stateObj = this._stateObj;
			if (stateObj != null && (ignoreCloseToken || !this._cancelAsyncOnCloseToken.IsCancellationRequested || stateObj._asyncReadWithoutSnapshot))
			{
				TdsParserStateObject obj = stateObj;
				lock (obj)
				{
					if (this._stateObj != null)
					{
						this.CleanupAfterAsyncInvocationInternal(this._stateObj, true);
					}
				}
			}
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x0006FB8C File Offset: 0x0006DD8C
		private void CleanupAfterAsyncInvocationInternal(TdsParserStateObject stateObj, bool resetNetworkPacketTaskSource = true)
		{
			if (resetNetworkPacketTaskSource)
			{
				stateObj._networkPacketTaskSource = null;
			}
			stateObj.ResetSnapshot();
			stateObj._syncOverAsync = true;
			stateObj._executionContext = null;
			stateObj._asyncReadWithoutSnapshot = false;
			this._snapshot = null;
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0006FBBC File Offset: 0x0006DDBC
		private void PrepareForAsyncContinuation()
		{
			if (this._snapshot != null)
			{
				this._sharedState._dataReady = this._snapshot._dataReady;
				this._haltRead = this._snapshot._haltRead;
				this._metaDataConsumed = this._snapshot._metaDataConsumed;
				this._browseModeInfoConsumed = this._snapshot._browseModeInfoConsumed;
				this._hasRows = this._snapshot._hasRows;
				this._altRowStatus = this._snapshot._altRowStatus;
				this._sharedState._nextColumnDataToRead = this._snapshot._nextColumnDataToRead;
				this._sharedState._nextColumnHeaderToRead = this._snapshot._nextColumnHeaderToRead;
				this._columnDataBytesRead = this._snapshot._columnDataBytesRead;
				this._sharedState._columnDataBytesRemaining = this._snapshot._columnDataBytesRemaining;
				this._metaData = this._snapshot._metadata;
				this._altMetaDataSetCollection = this._snapshot._altMetaDataSetCollection;
				this._tableNames = this._snapshot._tableNames;
				this._currentStream = this._snapshot._currentStream;
				this._currentTextReader = this._snapshot._currentTextReader;
				this._stateObj.PrepareReplaySnapshot();
			}
			this._stateObj._executionContext = ExecutionContext.Capture();
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0006FD02 File Offset: 0x0006DF02
		private void SwitchToAsyncWithoutSnapshot()
		{
			this._snapshot = null;
			this._stateObj.ResetSnapshot();
			this._stateObj._asyncReadWithoutSnapshot = true;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x0006FD24 File Offset: 0x0006DF24
		public ReadOnlyCollection<DbColumn> GetColumnSchema()
		{
			SqlStatistics statistics = null;
			ReadOnlyCollection<DbColumn> dbColumnSchema;
			try
			{
				statistics = SqlStatistics.StartTimer(this.Statistics);
				if ((this._metaData == null || this._metaData.dbColumnSchema == null) && this.MetaData != null)
				{
					this._metaData.dbColumnSchema = this.BuildColumnSchema();
				}
				if (this._metaData != null)
				{
					dbColumnSchema = this._metaData.dbColumnSchema;
				}
				else
				{
					dbColumnSchema = SqlDataReader.s_emptySchema;
				}
			}
			finally
			{
				SqlStatistics.StopTimer(statistics);
			}
			return dbColumnSchema;
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x0006FDA4 File Offset: 0x0006DFA4
		private ReadOnlyCollection<DbColumn> BuildColumnSchema()
		{
			_SqlMetaDataSet metaData = this.MetaData;
			DbColumn[] array = new DbColumn[metaData.Length];
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				SqlDbColumn sqlDbColumn = new SqlDbColumn(metaData[i]);
				if (this._typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
				{
					sqlDbColumn.SqlNumericScale = new int?((int)MetaType.MetaNVarChar.Scale);
				}
				else if (255 != sqlMetaData.scale)
				{
					sqlDbColumn.SqlNumericScale = new int?((int)sqlMetaData.scale);
				}
				else
				{
					sqlDbColumn.SqlNumericScale = new int?((int)sqlMetaData.metaType.Scale);
				}
				if (this._browseModeInfoConsumed)
				{
					sqlDbColumn.SqlIsAliased = new bool?(sqlMetaData.isDifferentName);
					sqlDbColumn.SqlIsKey = new bool?(sqlMetaData.isKey);
					sqlDbColumn.SqlIsHidden = new bool?(sqlMetaData.isHidden);
					sqlDbColumn.SqlIsExpression = new bool?(sqlMetaData.isExpression);
				}
				sqlDbColumn.SqlDataType = this.GetFieldTypeInternal(sqlMetaData);
				sqlDbColumn.SqlDataTypeName = this.GetDataTypeNameInternal(sqlMetaData);
				array[i] = sqlDbColumn;
			}
			return new ReadOnlyCollection<DbColumn>(array);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0006FECD File Offset: 0x0006E0CD
		// Note: this type is marked as 'beforefieldinit'.
		static SqlDataReader()
		{
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal SqlDataReader()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000F34 RID: 3892
		internal SqlDataReader.SharedState _sharedState;

		// Token: 0x04000F35 RID: 3893
		private TdsParser _parser;

		// Token: 0x04000F36 RID: 3894
		private TdsParserStateObject _stateObj;

		// Token: 0x04000F37 RID: 3895
		private SqlCommand _command;

		// Token: 0x04000F38 RID: 3896
		private SqlConnection _connection;

		// Token: 0x04000F39 RID: 3897
		private int _defaultLCID;

		// Token: 0x04000F3A RID: 3898
		private bool _haltRead;

		// Token: 0x04000F3B RID: 3899
		private bool _metaDataConsumed;

		// Token: 0x04000F3C RID: 3900
		private bool _browseModeInfoConsumed;

		// Token: 0x04000F3D RID: 3901
		private bool _isClosed;

		// Token: 0x04000F3E RID: 3902
		private bool _isInitialized;

		// Token: 0x04000F3F RID: 3903
		private bool _hasRows;

		// Token: 0x04000F40 RID: 3904
		private SqlDataReader.ALTROWSTATUS _altRowStatus;

		// Token: 0x04000F41 RID: 3905
		private int _recordsAffected;

		// Token: 0x04000F42 RID: 3906
		private long _defaultTimeoutMilliseconds;

		// Token: 0x04000F43 RID: 3907
		private SqlConnectionString.TypeSystem _typeSystem;

		// Token: 0x04000F44 RID: 3908
		private SqlStatistics _statistics;

		// Token: 0x04000F45 RID: 3909
		private SqlBuffer[] _data;

		// Token: 0x04000F46 RID: 3910
		private SqlStreamingXml _streamingXml;

		// Token: 0x04000F47 RID: 3911
		private _SqlMetaDataSet _metaData;

		// Token: 0x04000F48 RID: 3912
		private _SqlMetaDataSetCollection _altMetaDataSetCollection;

		// Token: 0x04000F49 RID: 3913
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000F4A RID: 3914
		private CommandBehavior _commandBehavior;

		// Token: 0x04000F4B RID: 3915
		private static int s_objectTypeCount;

		// Token: 0x04000F4C RID: 3916
		private static readonly ReadOnlyCollection<DbColumn> s_emptySchema = new ReadOnlyCollection<DbColumn>(Array.Empty<DbColumn>());

		// Token: 0x04000F4D RID: 3917
		internal readonly int ObjectID;

		// Token: 0x04000F4E RID: 3918
		private MultiPartTableName[] _tableNames;

		// Token: 0x04000F4F RID: 3919
		private string _resetOptionsString;

		// Token: 0x04000F50 RID: 3920
		private int _lastColumnWithDataChunkRead;

		// Token: 0x04000F51 RID: 3921
		private long _columnDataBytesRead;

		// Token: 0x04000F52 RID: 3922
		private long _columnDataCharsRead;

		// Token: 0x04000F53 RID: 3923
		private char[] _columnDataChars;

		// Token: 0x04000F54 RID: 3924
		private int _columnDataCharsIndex;

		// Token: 0x04000F55 RID: 3925
		private Task _currentTask;

		// Token: 0x04000F56 RID: 3926
		private SqlDataReader.Snapshot _snapshot;

		// Token: 0x04000F57 RID: 3927
		private CancellationTokenSource _cancelAsyncOnCloseTokenSource;

		// Token: 0x04000F58 RID: 3928
		private CancellationToken _cancelAsyncOnCloseToken;

		// Token: 0x04000F59 RID: 3929
		internal static readonly Type _typeofINullable = typeof(INullable);

		// Token: 0x04000F5A RID: 3930
		private static readonly Type s_typeofSqlString = typeof(SqlString);

		// Token: 0x04000F5B RID: 3931
		private SqlSequentialStream _currentStream;

		// Token: 0x04000F5C RID: 3932
		private SqlSequentialTextReader _currentTextReader;

		// Token: 0x020001E6 RID: 486
		private enum ALTROWSTATUS
		{
			// Token: 0x04000F5E RID: 3934
			Null,
			// Token: 0x04000F5F RID: 3935
			AltRow,
			// Token: 0x04000F60 RID: 3936
			Done
		}

		// Token: 0x020001E7 RID: 487
		internal class SharedState
		{
			// Token: 0x06001840 RID: 6208 RVA: 0x00003D93 File Offset: 0x00001F93
			public SharedState()
			{
			}

			// Token: 0x04000F61 RID: 3937
			internal int _nextColumnHeaderToRead;

			// Token: 0x04000F62 RID: 3938
			internal int _nextColumnDataToRead;

			// Token: 0x04000F63 RID: 3939
			internal long _columnDataBytesRemaining;

			// Token: 0x04000F64 RID: 3940
			internal bool _dataReady;
		}

		// Token: 0x020001E8 RID: 488
		private class Snapshot
		{
			// Token: 0x06001841 RID: 6209 RVA: 0x00003D93 File Offset: 0x00001F93
			public Snapshot()
			{
			}

			// Token: 0x04000F65 RID: 3941
			public bool _dataReady;

			// Token: 0x04000F66 RID: 3942
			public bool _haltRead;

			// Token: 0x04000F67 RID: 3943
			public bool _metaDataConsumed;

			// Token: 0x04000F68 RID: 3944
			public bool _browseModeInfoConsumed;

			// Token: 0x04000F69 RID: 3945
			public bool _hasRows;

			// Token: 0x04000F6A RID: 3946
			public SqlDataReader.ALTROWSTATUS _altRowStatus;

			// Token: 0x04000F6B RID: 3947
			public int _nextColumnDataToRead;

			// Token: 0x04000F6C RID: 3948
			public int _nextColumnHeaderToRead;

			// Token: 0x04000F6D RID: 3949
			public long _columnDataBytesRead;

			// Token: 0x04000F6E RID: 3950
			public long _columnDataBytesRemaining;

			// Token: 0x04000F6F RID: 3951
			public _SqlMetaDataSet _metadata;

			// Token: 0x04000F70 RID: 3952
			public _SqlMetaDataSetCollection _altMetaDataSetCollection;

			// Token: 0x04000F71 RID: 3953
			public MultiPartTableName[] _tableNames;

			// Token: 0x04000F72 RID: 3954
			public SqlSequentialStream _currentStream;

			// Token: 0x04000F73 RID: 3955
			public SqlSequentialTextReader _currentTextReader;
		}

		// Token: 0x020001E9 RID: 489
		[CompilerGenerated]
		private sealed class <>c__DisplayClass187_0
		{
			// Token: 0x06001842 RID: 6210 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass187_0()
			{
			}

			// Token: 0x06001843 RID: 6211 RVA: 0x0006FEFC File Offset: 0x0006E0FC
			internal Task<bool> <NextResultAsync>b__1(Task t)
			{
				if (t != null)
				{
					this.<>4__this.PrepareForAsyncContinuation();
				}
				bool flag;
				if (!this.<>4__this.TryNextResult(out flag))
				{
					return this.<>4__this.ContinueRetryable<bool>(this.moreFunc);
				}
				if (!flag)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			}

			// Token: 0x04000F74 RID: 3956
			public SqlDataReader <>4__this;

			// Token: 0x04000F75 RID: 3957
			public Func<Task, Task<bool>> moreFunc;
		}

		// Token: 0x020001EA RID: 490
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001844 RID: 6212 RVA: 0x0006FF46 File Offset: 0x0006E146
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001845 RID: 6213 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c()
			{
			}

			// Token: 0x06001846 RID: 6214 RVA: 0x00064ECE File Offset: 0x000630CE
			internal void <NextResultAsync>b__187_0(object s)
			{
				((SqlCommand)s).CancelIgnoreFailure();
			}

			// Token: 0x06001847 RID: 6215 RVA: 0x00064ECE File Offset: 0x000630CE
			internal void <ReadAsync>b__190_0(object s)
			{
				((SqlCommand)s).CancelIgnoreFailure();
			}

			// Token: 0x06001848 RID: 6216 RVA: 0x00064ECE File Offset: 0x000630CE
			internal void <IsDBNullAsync>b__191_0(object s)
			{
				((SqlCommand)s).CancelIgnoreFailure();
			}

			// Token: 0x04000F76 RID: 3958
			public static readonly SqlDataReader.<>c <>9 = new SqlDataReader.<>c();

			// Token: 0x04000F77 RID: 3959
			public static Action<object> <>9__187_0;

			// Token: 0x04000F78 RID: 3960
			public static Action<object> <>9__190_0;

			// Token: 0x04000F79 RID: 3961
			public static Action<object> <>9__191_0;
		}

		// Token: 0x020001EB RID: 491
		[CompilerGenerated]
		private sealed class <>c__DisplayClass188_0
		{
			// Token: 0x06001849 RID: 6217 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass188_0()
			{
			}

			// Token: 0x0600184A RID: 6218 RVA: 0x0006FF54 File Offset: 0x0006E154
			internal Task<int> <GetBytesAsync>b__0(Task t)
			{
				if (t != null)
				{
					this.<>4__this.PrepareForAsyncContinuation();
				}
				this.<>4__this.SetTimeout(this.<>4__this._defaultTimeoutMilliseconds);
				if (!this.<>4__this.TryReadColumnHeader(this.i))
				{
					return this.<>4__this.ContinueRetryable<int>(this.moreFunc);
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<int>(this.cancellationToken);
				}
				if (this.timeoutToken.IsCancellationRequested)
				{
					return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				this.<>4__this.SwitchToAsyncWithoutSnapshot();
				int result;
				Task<int> bytesAsyncReadDataStage = this.<>4__this.GetBytesAsyncReadDataStage(this.i, this.buffer, this.index, this.length, this.timeout, true, this.cancellationToken, this.timeoutToken, out result);
				if (bytesAsyncReadDataStage == null)
				{
					return Task.FromResult<int>(result);
				}
				return bytesAsyncReadDataStage;
			}

			// Token: 0x04000F7A RID: 3962
			public SqlDataReader <>4__this;

			// Token: 0x04000F7B RID: 3963
			public int i;

			// Token: 0x04000F7C RID: 3964
			public CancellationToken cancellationToken;

			// Token: 0x04000F7D RID: 3965
			public byte[] buffer;

			// Token: 0x04000F7E RID: 3966
			public int index;

			// Token: 0x04000F7F RID: 3967
			public int length;

			// Token: 0x04000F80 RID: 3968
			public int timeout;

			// Token: 0x04000F81 RID: 3969
			public CancellationToken timeoutToken;

			// Token: 0x04000F82 RID: 3970
			public Func<Task, Task<int>> moreFunc;
		}

		// Token: 0x020001EC RID: 492
		[CompilerGenerated]
		private sealed class <>c__DisplayClass189_0
		{
			// Token: 0x0600184B RID: 6219 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass189_0()
			{
			}

			// Token: 0x0600184C RID: 6220 RVA: 0x00070037 File Offset: 0x0006E237
			internal void <GetBytesAsyncReadDataStage>b__1(Task<int> t)
			{
				this.<>4__this.CompleteRetryable<int>(t, this.source, this.timeoutCancellationSource);
			}

			// Token: 0x04000F83 RID: 3971
			public SqlDataReader <>4__this;

			// Token: 0x04000F84 RID: 3972
			public CancellationToken cancellationToken;

			// Token: 0x04000F85 RID: 3973
			public CancellationToken timeoutToken;

			// Token: 0x04000F86 RID: 3974
			public int i;

			// Token: 0x04000F87 RID: 3975
			public byte[] buffer;

			// Token: 0x04000F88 RID: 3976
			public int index;

			// Token: 0x04000F89 RID: 3977
			public int length;

			// Token: 0x04000F8A RID: 3978
			public TaskCompletionSource<int> source;

			// Token: 0x04000F8B RID: 3979
			public CancellationTokenSource timeoutCancellationSource;
		}

		// Token: 0x020001ED RID: 493
		[CompilerGenerated]
		private sealed class <>c__DisplayClass189_1
		{
			// Token: 0x0600184D RID: 6221 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass189_1()
			{
			}

			// Token: 0x0600184E RID: 6222 RVA: 0x00070054 File Offset: 0x0006E254
			internal Task<int> <GetBytesAsyncReadDataStage>b__0(Task _)
			{
				this.CS$<>8__locals1.<>4__this.PrepareForAsyncContinuation();
				if (this.CS$<>8__locals1.cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<int>(this.CS$<>8__locals1.cancellationToken);
				}
				if (this.CS$<>8__locals1.timeoutToken.IsCancellationRequested)
				{
					return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				this.CS$<>8__locals1.<>4__this.SetTimeout(this.CS$<>8__locals1.<>4__this._defaultTimeoutMilliseconds);
				int num;
				bool flag = this.CS$<>8__locals1.<>4__this.TryGetBytesInternalSequential(this.CS$<>8__locals1.i, this.CS$<>8__locals1.buffer, this.CS$<>8__locals1.index + this.totalBytesRead, this.CS$<>8__locals1.length - this.totalBytesRead, out num);
				this.totalBytesRead += num;
				if (flag)
				{
					return Task.FromResult<int>(this.totalBytesRead);
				}
				return this.CS$<>8__locals1.<>4__this.ContinueRetryable<int>(this.moreFunc);
			}

			// Token: 0x04000F8C RID: 3980
			public int totalBytesRead;

			// Token: 0x04000F8D RID: 3981
			public Func<Task, Task<int>> moreFunc;

			// Token: 0x04000F8E RID: 3982
			public SqlDataReader.<>c__DisplayClass189_0 CS$<>8__locals1;
		}

		// Token: 0x020001EE RID: 494
		[CompilerGenerated]
		private sealed class <>c__DisplayClass190_0
		{
			// Token: 0x0600184F RID: 6223 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass190_0()
			{
			}

			// Token: 0x06001850 RID: 6224 RVA: 0x0007015C File Offset: 0x0006E35C
			internal Task<bool> <ReadAsync>b__1(Task t)
			{
				if (t != null)
				{
					this.<>4__this.PrepareForAsyncContinuation();
				}
				if (this.rowTokenRead || this.<>4__this.TryReadInternal(true, out this.more))
				{
					if (!this.more || (this.<>4__this._commandBehavior & CommandBehavior.SequentialAccess) == CommandBehavior.SequentialAccess)
					{
						if (!this.more)
						{
							return ADP.FalseTask;
						}
						return ADP.TrueTask;
					}
					else
					{
						if (!this.rowTokenRead)
						{
							this.rowTokenRead = true;
							this.<>4__this._snapshot = null;
							this.<>4__this.PrepareAsyncInvocation(true);
						}
						if (this.<>4__this.TryReadColumn(this.<>4__this._metaData.Length - 1, true, false))
						{
							return ADP.TrueTask;
						}
					}
				}
				return this.<>4__this.ContinueRetryable<bool>(this.moreFunc);
			}

			// Token: 0x04000F8F RID: 3983
			public SqlDataReader <>4__this;

			// Token: 0x04000F90 RID: 3984
			public bool rowTokenRead;

			// Token: 0x04000F91 RID: 3985
			public bool more;

			// Token: 0x04000F92 RID: 3986
			public Func<Task, Task<bool>> moreFunc;
		}

		// Token: 0x020001EF RID: 495
		[CompilerGenerated]
		private sealed class <>c__DisplayClass191_0
		{
			// Token: 0x06001851 RID: 6225 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass191_0()
			{
			}

			// Token: 0x06001852 RID: 6226 RVA: 0x00070220 File Offset: 0x0006E420
			internal Task<bool> <IsDBNullAsync>b__1(Task t)
			{
				if (t != null)
				{
					this.<>4__this.PrepareForAsyncContinuation();
				}
				if (!this.<>4__this.TryReadColumnHeader(this.i))
				{
					return this.<>4__this.ContinueRetryable<bool>(this.moreFunc);
				}
				if (!this.<>4__this._data[this.i].IsNull)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			}

			// Token: 0x04000F93 RID: 3987
			public SqlDataReader <>4__this;

			// Token: 0x04000F94 RID: 3988
			public int i;

			// Token: 0x04000F95 RID: 3989
			public Func<Task, Task<bool>> moreFunc;
		}

		// Token: 0x020001F0 RID: 496
		[CompilerGenerated]
		private sealed class <>c__DisplayClass192_0<T>
		{
			// Token: 0x06001853 RID: 6227 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass192_0()
			{
			}

			// Token: 0x06001854 RID: 6228 RVA: 0x00070284 File Offset: 0x0006E484
			internal Task<T> <GetFieldValueAsync>b__1(Task t)
			{
				if (t != null)
				{
					this.<>4__this.PrepareForAsyncContinuation();
				}
				if (this.<>4__this.TryReadColumn(this.i, false, false))
				{
					return Task.FromResult<T>(this.<>4__this.GetFieldValueFromSqlBufferInternal<T>(this.<>4__this._data[this.i], this.<>4__this._metaData[this.i]));
				}
				return this.<>4__this.ContinueRetryable<T>(this.moreFunc);
			}

			// Token: 0x04000F96 RID: 3990
			public SqlDataReader <>4__this;

			// Token: 0x04000F97 RID: 3991
			public int i;

			// Token: 0x04000F98 RID: 3992
			public Func<Task, Task<T>> moreFunc;
		}

		// Token: 0x020001F1 RID: 497
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__192<T>
		{
			// Token: 0x06001855 RID: 6229 RVA: 0x000702FE File Offset: 0x0006E4FE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__192()
			{
			}

			// Token: 0x06001856 RID: 6230 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__192()
			{
			}

			// Token: 0x06001857 RID: 6231 RVA: 0x00064ECE File Offset: 0x000630CE
			internal void <GetFieldValueAsync>b__192_0(object s)
			{
				((SqlCommand)s).CancelIgnoreFailure();
			}

			// Token: 0x04000F99 RID: 3993
			public static readonly SqlDataReader.<>c__192<T> <>9 = new SqlDataReader.<>c__192<T>();

			// Token: 0x04000F9A RID: 3994
			public static Action<object> <>9__192_0;
		}

		// Token: 0x020001F2 RID: 498
		[CompilerGenerated]
		private sealed class <>c__DisplayClass194_0<T>
		{
			// Token: 0x06001858 RID: 6232 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass194_0()
			{
			}

			// Token: 0x06001859 RID: 6233 RVA: 0x0007030C File Offset: 0x0006E50C
			internal Task<T> <ContinueRetryable>b__0(Task<object> retryTask)
			{
				if (retryTask.IsFaulted)
				{
					return Task.FromException<T>(retryTask.Exception.InnerException);
				}
				if (!this.<>4__this._cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					TdsParserStateObject stateObj = this.<>4__this._stateObj;
					if (stateObj != null)
					{
						TdsParserStateObject obj = stateObj;
						lock (obj)
						{
							if (this.<>4__this._stateObj != null)
							{
								if (retryTask.IsCanceled)
								{
									if (this.<>4__this._parser != null)
									{
										this.<>4__this._parser.State = TdsParserState.Broken;
										this.<>4__this._parser.Connection.BreakConnection();
										this.<>4__this._parser.ThrowExceptionAndWarning(this.<>4__this._stateObj, false, false);
									}
								}
								else if (!this.<>4__this.IsClosed)
								{
									try
									{
										return this.moreFunc(retryTask);
									}
									catch (Exception)
									{
										this.<>4__this.CleanupAfterAsyncInvocation(false);
										throw;
									}
								}
							}
						}
					}
				}
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
			}

			// Token: 0x04000F9B RID: 3995
			public SqlDataReader <>4__this;

			// Token: 0x04000F9C RID: 3996
			public Func<Task, Task<T>> moreFunc;
		}

		// Token: 0x020001F3 RID: 499
		[CompilerGenerated]
		private sealed class <>c__DisplayClass195_0<T>
		{
			// Token: 0x0600185A RID: 6234 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c__DisplayClass195_0()
			{
			}

			// Token: 0x0600185B RID: 6235 RVA: 0x00070434 File Offset: 0x0006E634
			internal void <InvokeRetryable>b__0(Task<T> t)
			{
				this.<>4__this.CompleteRetryable<T>(t, this.source, this.objectToDispose);
			}

			// Token: 0x04000F9D RID: 3997
			public SqlDataReader <>4__this;

			// Token: 0x04000F9E RID: 3998
			public TaskCompletionSource<T> source;

			// Token: 0x04000F9F RID: 3999
			public IDisposable objectToDispose;
		}
	}
}
