using System;
using System.Data.Common;
using System.Globalization;
using System.IO;

namespace System.Data.ProviderBase
{
	// Token: 0x02000351 RID: 849
	internal class DbMetaDataFactory
	{
		// Token: 0x060026DD RID: 9949 RVA: 0x000ABD3C File Offset: 0x000A9F3C
		public DbMetaDataFactory(Stream xmlStream, string serverVersion, string normalizedServerVersion)
		{
			ADP.CheckArgumentNull(xmlStream, "xmlStream");
			ADP.CheckArgumentNull(serverVersion, "serverVersion");
			ADP.CheckArgumentNull(normalizedServerVersion, "normalizedServerVersion");
			this.LoadDataSetFromXml(xmlStream);
			this._serverVersionString = serverVersion;
			this._normalizedServerVersion = normalizedServerVersion;
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x000ABD7A File Offset: 0x000A9F7A
		protected DataSet CollectionDataSet
		{
			get
			{
				return this._metaDataCollectionsDataSet;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x000ABD82 File Offset: 0x000A9F82
		protected string ServerVersion
		{
			get
			{
				return this._serverVersionString;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000ABD8A File Offset: 0x000A9F8A
		protected string ServerVersionNormalized
		{
			get
			{
				return this._normalizedServerVersion;
			}
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000ABD94 File Offset: 0x000A9F94
		protected DataTable CloneAndFilterCollection(string collectionName, string[] hiddenColumnNames)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[collectionName];
			if (dataTable == null || collectionName != dataTable.TableName)
			{
				throw ADP.DataTableDoesNotExist(collectionName);
			}
			DataTable dataTable2 = new DataTable(collectionName)
			{
				Locale = CultureInfo.InvariantCulture
			};
			DataColumnCollection columns = dataTable2.Columns;
			DataColumn[] array = this.FilterColumns(dataTable, hiddenColumnNames, columns);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.SupportedByCurrentVersion(dataRow))
				{
					DataRow dataRow2 = dataTable2.NewRow();
					for (int i = 0; i < columns.Count; i++)
					{
						dataRow2[columns[i]] = dataRow[array[i], DataRowVersion.Current];
					}
					dataTable2.Rows.Add(dataRow2);
					dataRow2.AcceptChanges();
				}
			}
			return dataTable2;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000ABE98 File Offset: 0x000AA098
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x000ABEA1 File Offset: 0x000AA0A1
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._normalizedServerVersion = null;
				this._serverVersionString = null;
				this._metaDataCollectionsDataSet.Dispose();
			}
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x000ABEC0 File Offset: 0x000AA0C0
		private DataTable ExecuteCommand(DataRow requestedCollectionRow, string[] restrictions, DbConnection connection)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn column = dataTable.Columns["PopulationString"];
			DataColumn column2 = dataTable.Columns["NumberOfRestrictions"];
			DataColumn column3 = dataTable.Columns["CollectionName"];
			DataTable dataTable2 = null;
			string commandText = requestedCollectionRow[column, DataRowVersion.Current] as string;
			int num = (int)requestedCollectionRow[column2, DataRowVersion.Current];
			string text = requestedCollectionRow[column3, DataRowVersion.Current] as string;
			if (restrictions != null && restrictions.Length > num)
			{
				throw ADP.TooManyRestrictions(text);
			}
			DbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandText = commandText;
			dbCommand.CommandTimeout = Math.Max(dbCommand.CommandTimeout, 180);
			for (int i = 0; i < num; i++)
			{
				DbParameter dbParameter = dbCommand.CreateParameter();
				if (restrictions != null && restrictions.Length > i && restrictions[i] != null)
				{
					dbParameter.Value = restrictions[i];
				}
				else
				{
					dbParameter.Value = DBNull.Value;
				}
				dbParameter.ParameterName = this.GetParameterName(text, i + 1);
				dbParameter.Direction = ParameterDirection.Input;
				dbCommand.Parameters.Add(dbParameter);
			}
			DbDataReader dbDataReader = null;
			try
			{
				try
				{
					dbDataReader = dbCommand.ExecuteReader();
				}
				catch (Exception e)
				{
					if (!ADP.IsCatchableExceptionType(e))
					{
						throw;
					}
					throw ADP.QueryFailed(text, e);
				}
				dataTable2 = new DataTable(text)
				{
					Locale = CultureInfo.InvariantCulture
				};
				foreach (object obj in dbDataReader.GetSchemaTable().Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataTable2.Columns.Add(dataRow["ColumnName"] as string, (Type)dataRow["DataType"]);
				}
				object[] values = new object[dataTable2.Columns.Count];
				while (dbDataReader.Read())
				{
					dbDataReader.GetValues(values);
					dataTable2.Rows.Add(values);
				}
			}
			finally
			{
				if (dbDataReader != null)
				{
					dbDataReader.Dispose();
					dbDataReader = null;
				}
			}
			return dataTable2;
		}

		// Token: 0x060026E5 RID: 9957 RVA: 0x000AC114 File Offset: 0x000AA314
		private DataColumn[] FilterColumns(DataTable sourceTable, string[] hiddenColumnNames, DataColumnCollection destinationColumns)
		{
			int num = 0;
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn sourceColumn = (DataColumn)obj;
				if (this.IncludeThisColumn(sourceColumn, hiddenColumnNames))
				{
					num++;
				}
			}
			if (num == 0)
			{
				throw ADP.NoColumns();
			}
			int num2 = 0;
			DataColumn[] array = new DataColumn[num];
			foreach (object obj2 in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj2;
				if (this.IncludeThisColumn(dataColumn, hiddenColumnNames))
				{
					DataColumn column = new DataColumn(dataColumn.ColumnName, dataColumn.DataType);
					destinationColumns.Add(column);
					array[num2] = dataColumn;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x000AC204 File Offset: 0x000AA404
		internal DataRow FindMetaDataCollectionRow(string collectionName)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			if (dataTable == null)
			{
				throw ADP.InvalidXml();
			}
			DataColumn dataColumn = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			if (dataColumn == null || typeof(string) != dataColumn.DataType)
			{
				throw ADP.InvalidXmlMissingColumn(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
			}
			DataRow dataRow = null;
			string text = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj;
				string text2 = dataRow2[dataColumn, DataRowVersion.Current] as string;
				if (string.IsNullOrEmpty(text2))
				{
					throw ADP.InvalidXmlInvalidValue(DbMetaDataCollectionNames.MetaDataCollections, DbMetaDataColumnNames.CollectionName);
				}
				if (ADP.CompareInsensitiveInvariant(text2, collectionName))
				{
					if (!this.SupportedByCurrentVersion(dataRow2))
					{
						flag = true;
					}
					else if (collectionName == text2)
					{
						if (flag2)
						{
							throw ADP.CollectionNameIsNotUnique(collectionName);
						}
						dataRow = dataRow2;
						text = text2;
						flag2 = true;
					}
					else
					{
						if (text != null)
						{
							flag3 = true;
						}
						dataRow = dataRow2;
						text = text2;
					}
				}
			}
			if (dataRow == null)
			{
				if (!flag)
				{
					throw ADP.UndefinedCollection(collectionName);
				}
				throw ADP.UnsupportedVersion(collectionName);
			}
			else
			{
				if (!flag2 && flag3)
				{
					throw ADP.AmbigousCollectionName(collectionName);
				}
				return dataRow;
			}
		}

		// Token: 0x060026E7 RID: 9959 RVA: 0x000AC35C File Offset: 0x000AA55C
		private void FixUpVersion(DataTable dataSourceInfoTable)
		{
			DataColumn dataColumn = dataSourceInfoTable.Columns["DataSourceProductVersion"];
			DataColumn dataColumn2 = dataSourceInfoTable.Columns["DataSourceProductVersionNormalized"];
			if (dataColumn == null || dataColumn2 == null)
			{
				throw ADP.MissingDataSourceInformationColumn();
			}
			if (dataSourceInfoTable.Rows.Count != 1)
			{
				throw ADP.IncorrectNumberOfDataSourceInformationRows();
			}
			DataRow dataRow = dataSourceInfoTable.Rows[0];
			dataRow[dataColumn] = this._serverVersionString;
			dataRow[dataColumn2] = this._normalizedServerVersion;
			dataRow.AcceptChanges();
		}

		// Token: 0x060026E8 RID: 9960 RVA: 0x000AC3D8 File Offset: 0x000AA5D8
		private string GetParameterName(string neededCollectionName, int neededRestrictionNumber)
		{
			DataColumn dataColumn = null;
			DataColumn dataColumn2 = null;
			DataColumn dataColumn3 = null;
			DataColumn dataColumn4 = null;
			string text = null;
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.Restrictions];
			if (dataTable != null)
			{
				DataColumnCollection columns = dataTable.Columns;
				if (columns != null)
				{
					dataColumn = columns["CollectionName"];
					dataColumn2 = columns["ParameterName"];
					dataColumn3 = columns["RestrictionName"];
					dataColumn4 = columns["RestrictionNumber"];
				}
			}
			if (dataColumn2 == null || dataColumn == null || dataColumn3 == null || dataColumn4 == null)
			{
				throw ADP.MissingRestrictionColumn();
			}
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((string)dataRow[dataColumn] == neededCollectionName && (int)dataRow[dataColumn4] == neededRestrictionNumber && this.SupportedByCurrentVersion(dataRow))
				{
					text = (string)dataRow[dataColumn2];
					break;
				}
			}
			if (text == null)
			{
				throw ADP.MissingRestrictionRow();
			}
			return text;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000AC4FC File Offset: 0x000AA6FC
		public virtual DataTable GetSchema(DbConnection connection, string collectionName, string[] restrictions)
		{
			DataTable dataTable = this._metaDataCollectionsDataSet.Tables[DbMetaDataCollectionNames.MetaDataCollections];
			DataColumn column = dataTable.Columns["PopulationMechanism"];
			DataColumn column2 = dataTable.Columns[DbMetaDataColumnNames.CollectionName];
			DataRow dataRow = this.FindMetaDataCollectionRow(collectionName);
			string text = dataRow[column2, DataRowVersion.Current] as string;
			if (!ADP.IsEmptyArray(restrictions))
			{
				for (int i = 0; i < restrictions.Length; i++)
				{
					if (restrictions[i] != null && restrictions[i].Length > 4096)
					{
						throw ADP.NotSupported();
					}
				}
			}
			string text2 = dataRow[column, DataRowVersion.Current] as string;
			DataTable dataTable2;
			if (!(text2 == "DataTable"))
			{
				if (!(text2 == "SQLCommand"))
				{
					if (!(text2 == "PrepareCollection"))
					{
						throw ADP.UndefinedPopulationMechanism(text2);
					}
					dataTable2 = this.PrepareCollection(text, restrictions, connection);
				}
				else
				{
					dataTable2 = this.ExecuteCommand(dataRow, restrictions, connection);
				}
			}
			else
			{
				string[] hiddenColumnNames;
				if (text == DbMetaDataCollectionNames.MetaDataCollections)
				{
					hiddenColumnNames = new string[]
					{
						"PopulationMechanism",
						"PopulationString"
					};
				}
				else
				{
					hiddenColumnNames = null;
				}
				if (!ADP.IsEmptyArray(restrictions))
				{
					throw ADP.TooManyRestrictions(text);
				}
				dataTable2 = this.CloneAndFilterCollection(text, hiddenColumnNames);
				if (text == DbMetaDataCollectionNames.DataSourceInformation)
				{
					this.FixUpVersion(dataTable2);
				}
			}
			return dataTable2;
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000AC658 File Offset: 0x000AA858
		private bool IncludeThisColumn(DataColumn sourceColumn, string[] hiddenColumnNames)
		{
			bool result = true;
			string columnName = sourceColumn.ColumnName;
			if (columnName == "MinimumVersion" || columnName == "MaximumVersion")
			{
				result = false;
			}
			else if (hiddenColumnNames != null)
			{
				for (int i = 0; i < hiddenColumnNames.Length; i++)
				{
					if (hiddenColumnNames[i] == columnName)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x000AC6AD File Offset: 0x000AA8AD
		private void LoadDataSetFromXml(Stream XmlStream)
		{
			this._metaDataCollectionsDataSet = new DataSet();
			this._metaDataCollectionsDataSet.Locale = CultureInfo.InvariantCulture;
			this._metaDataCollectionsDataSet.ReadXml(XmlStream);
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x00008E4B File Offset: 0x0000704B
		protected virtual DataTable PrepareCollection(string collectionName, string[] restrictions, DbConnection connection)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x000AC6D8 File Offset: 0x000AA8D8
		private bool SupportedByCurrentVersion(DataRow requestedCollectionRow)
		{
			bool flag = true;
			DataColumnCollection columns = requestedCollectionRow.Table.Columns;
			DataColumn dataColumn = columns["MinimumVersion"];
			if (dataColumn != null)
			{
				object obj = requestedCollectionRow[dataColumn];
				if (obj != null && obj != DBNull.Value && 0 > string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
				{
					flag = false;
				}
			}
			if (flag)
			{
				dataColumn = columns["MaximumVersion"];
				if (dataColumn != null)
				{
					object obj = requestedCollectionRow[dataColumn];
					if (obj != null && obj != DBNull.Value && 0 < string.Compare(this._normalizedServerVersion, (string)obj, StringComparison.OrdinalIgnoreCase))
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x0400195D RID: 6493
		private DataSet _metaDataCollectionsDataSet;

		// Token: 0x0400195E RID: 6494
		private string _normalizedServerVersion;

		// Token: 0x0400195F RID: 6495
		private string _serverVersionString;

		// Token: 0x04001960 RID: 6496
		private const string _collectionName = "CollectionName";

		// Token: 0x04001961 RID: 6497
		private const string _populationMechanism = "PopulationMechanism";

		// Token: 0x04001962 RID: 6498
		private const string _populationString = "PopulationString";

		// Token: 0x04001963 RID: 6499
		private const string _maximumVersion = "MaximumVersion";

		// Token: 0x04001964 RID: 6500
		private const string _minimumVersion = "MinimumVersion";

		// Token: 0x04001965 RID: 6501
		private const string _dataSourceProductVersionNormalized = "DataSourceProductVersionNormalized";

		// Token: 0x04001966 RID: 6502
		private const string _dataSourceProductVersion = "DataSourceProductVersion";

		// Token: 0x04001967 RID: 6503
		private const string _restrictionDefault = "RestrictionDefault";

		// Token: 0x04001968 RID: 6504
		private const string _restrictionNumber = "RestrictionNumber";

		// Token: 0x04001969 RID: 6505
		private const string _numberOfRestrictions = "NumberOfRestrictions";

		// Token: 0x0400196A RID: 6506
		private const string _restrictionName = "RestrictionName";

		// Token: 0x0400196B RID: 6507
		private const string _parameterName = "ParameterName";

		// Token: 0x0400196C RID: 6508
		private const string _dataTable = "DataTable";

		// Token: 0x0400196D RID: 6509
		private const string _sqlCommand = "SQLCommand";

		// Token: 0x0400196E RID: 6510
		private const string _prepareCollection = "PrepareCollection";
	}
}
