using System;
using System.ComponentModel;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace System.Data.Common
{
	/// <summary>Represents a set of SQL commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	// Token: 0x0200037C RID: 892
	public class DataAdapter : Component, IDataAdapter
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		private void AssertReaderHandleFieldCount(DataReaderContainer readerHandler)
		{
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		private void AssertSchemaMapping(SchemaMapping mapping)
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DataAdapter" /> class.</summary>
		// Token: 0x060029EE RID: 10734 RVA: 0x000B7EB0 File Offset: 0x000B60B0
		protected DataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DataAdapter" /> class from an existing object of the same type.</summary>
		/// <param name="from">A <see cref="T:System.Data.Common.DataAdapter" /> object used to create the new <see cref="T:System.Data.Common.DataAdapter" />.</param>
		// Token: 0x060029EF RID: 10735 RVA: 0x000B7EFC File Offset: 0x000B60FC
		protected DataAdapter(DataAdapter from)
		{
			this.CloneFrom(from);
		}

		/// <summary>Gets or sets a value indicating whether <see cref="M:System.Data.DataRow.AcceptChanges" /> is called on a <see cref="T:System.Data.DataRow" /> after it is added to the <see cref="T:System.Data.DataTable" /> during any of the Fill operations.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.Data.DataRow.AcceptChanges" /> is called on the <see cref="T:System.Data.DataRow" />; otherwise <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060029F0 RID: 10736 RVA: 0x000B7F49 File Offset: 0x000B6149
		// (set) Token: 0x060029F1 RID: 10737 RVA: 0x000B7F51 File Offset: 0x000B6151
		[DefaultValue(true)]
		public bool AcceptChangesDuringFill
		{
			get
			{
				return this._acceptChangesDuringFill;
			}
			set
			{
				this._acceptChangesDuringFill = value;
			}
		}

		/// <summary>Determines whether the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" /> property is persisted; otherwise <see langword="false" />.</returns>
		// Token: 0x060029F2 RID: 10738 RVA: 0x000B7F5A File Offset: 0x000B615A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeAcceptChangesDuringFill()
		{
			return this._fillLoadOption == (LoadOption)0;
		}

		/// <summary>Gets or sets whether <see cref="M:System.Data.DataRow.AcceptChanges" /> is called during a <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.Data.DataRow.AcceptChanges" /> is called during an <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)" />; otherwise <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x000B7F65 File Offset: 0x000B6165
		// (set) Token: 0x060029F4 RID: 10740 RVA: 0x000B7F6D File Offset: 0x000B616D
		[DefaultValue(true)]
		public bool AcceptChangesDuringUpdate
		{
			get
			{
				return this._acceptChangesDuringUpdate;
			}
			set
			{
				this._acceptChangesDuringUpdate = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether to generate an exception when an error is encountered during a row update.</summary>
		/// <returns>
		///   <see langword="true" /> to continue the update without generating an exception; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x000B7F76 File Offset: 0x000B6176
		// (set) Token: 0x060029F6 RID: 10742 RVA: 0x000B7F7E File Offset: 0x000B617E
		[DefaultValue(false)]
		public bool ContinueUpdateOnError
		{
			get
			{
				return this._continueUpdateOnError;
			}
			set
			{
				this._continueUpdateOnError = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.LoadOption" /> that determines how the adapter fills the <see cref="T:System.Data.DataTable" /> from the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.LoadOption" /> value.</returns>
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x000B7F88 File Offset: 0x000B6188
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x000B7FA7 File Offset: 0x000B61A7
		[RefreshProperties(RefreshProperties.All)]
		public LoadOption FillLoadOption
		{
			get
			{
				if (this._fillLoadOption == (LoadOption)0)
				{
					return LoadOption.OverwriteChanges;
				}
				return this._fillLoadOption;
			}
			set
			{
				if (value <= LoadOption.Upsert)
				{
					this._fillLoadOption = value;
					return;
				}
				throw ADP.InvalidLoadOption(value);
			}
		}

		/// <summary>Resets <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> to its default state and causes <see cref="M:System.Data.Common.DataAdapter.Fill(System.Data.DataSet)" /> to honor <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" />.</summary>
		// Token: 0x060029F9 RID: 10745 RVA: 0x000B7FBB File Offset: 0x000B61BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetFillLoadOption()
		{
			this._fillLoadOption = (LoadOption)0;
		}

		/// <summary>Determines whether the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> property is persisted; otherwise <see langword="false" />.</returns>
		// Token: 0x060029FA RID: 10746 RVA: 0x000B7FC4 File Offset: 0x000B61C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeFillLoadOption()
		{
			return this._fillLoadOption > (LoadOption)0;
		}

		/// <summary>Determines the action to take when incoming data does not have a matching table or column.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingMappingAction" /> values. The default is <see langword="Passthrough" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingMappingAction" /> values.</exception>
		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x000B7FCF File Offset: 0x000B61CF
		// (set) Token: 0x060029FC RID: 10748 RVA: 0x000B7FD7 File Offset: 0x000B61D7
		[DefaultValue(MissingMappingAction.Passthrough)]
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return this._missingMappingAction;
			}
			set
			{
				if (value - MissingMappingAction.Passthrough <= 2)
				{
					this._missingMappingAction = value;
					return;
				}
				throw ADP.InvalidMissingMappingAction(value);
			}
		}

		/// <summary>Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingSchemaAction" /> values. The default is <see langword="Add" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingSchemaAction" /> values.</exception>
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x000B7FED File Offset: 0x000B61ED
		// (set) Token: 0x060029FE RID: 10750 RVA: 0x000B7FF5 File Offset: 0x000B61F5
		[DefaultValue(MissingSchemaAction.Add)]
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return this._missingSchemaAction;
			}
			set
			{
				if (value - MissingSchemaAction.Add <= 3)
				{
					this._missingSchemaAction = value;
					return;
				}
				throw ADP.InvalidMissingSchemaAction(value);
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x000B800B File Offset: 0x000B620B
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets or sets whether the <see langword="Fill" /> method should return provider-specific values or common CLS-compliant values.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Fill" /> method should return provider-specific values; otherwise <see langword="false" /> to return common CLS-compliant values.</returns>
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x000B8013 File Offset: 0x000B6213
		// (set) Token: 0x06002A01 RID: 10753 RVA: 0x000B801B File Offset: 0x000B621B
		[DefaultValue(false)]
		public virtual bool ReturnProviderSpecificTypes
		{
			get
			{
				return this._returnProviderSpecificTypes;
			}
			set
			{
				this._returnProviderSpecificTypes = value;
			}
		}

		/// <summary>Gets a collection that provides the master mapping between a source table and a <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x000B8024 File Offset: 0x000B6224
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataTableMappingCollection TableMappings
		{
			get
			{
				DataTableMappingCollection dataTableMappingCollection = this._tableMappings;
				if (dataTableMappingCollection == null)
				{
					dataTableMappingCollection = this.CreateTableMappings();
					if (dataTableMappingCollection == null)
					{
						dataTableMappingCollection = new DataTableMappingCollection();
					}
					this._tableMappings = dataTableMappingCollection;
				}
				return dataTableMappingCollection;
			}
		}

		/// <summary>Indicates how a source table is mapped to a dataset table.</summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x000B8053 File Offset: 0x000B6253
		ITableMappingCollection IDataAdapter.TableMappings
		{
			get
			{
				return this.TableMappings;
			}
		}

		/// <summary>Determines whether one or more <see cref="T:System.Data.Common.DataTableMapping" /> objects exist and they should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if one or more <see cref="T:System.Data.Common.DataTableMapping" /> objects exist; otherwise <see langword="false" />.</returns>
		// Token: 0x06002A04 RID: 10756 RVA: 0x00006D61 File Offset: 0x00004F61
		protected virtual bool ShouldSerializeTableMappings()
		{
			return true;
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DataTableMappingCollection" /> has been created.</summary>
		/// <returns>
		///   <see langword="true" /> if a <see cref="T:System.Data.Common.DataTableMappingCollection" /> has been created; otherwise <see langword="false" />.</returns>
		// Token: 0x06002A05 RID: 10757 RVA: 0x000B805B File Offset: 0x000B625B
		protected bool HasTableMappings()
		{
			return this._tableMappings != null && 0 < this.TableMappings.Count;
		}

		/// <summary>Returned when an error occurs during a fill operation.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06002A06 RID: 10758 RVA: 0x000B8075 File Offset: 0x000B6275
		// (remove) Token: 0x06002A07 RID: 10759 RVA: 0x000B808F File Offset: 0x000B628F
		public event FillErrorEventHandler FillError
		{
			add
			{
				this._hasFillErrorHandler = true;
				base.Events.AddHandler(DataAdapter.s_eventFillError, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataAdapter.s_eventFillError, value);
			}
		}

		/// <summary>Creates a copy of this instance of <see cref="T:System.Data.Common.DataAdapter" />.</summary>
		/// <returns>The cloned instance of <see cref="T:System.Data.Common.DataAdapter" />.</returns>
		// Token: 0x06002A08 RID: 10760 RVA: 0x000B80A2 File Offset: 0x000B62A2
		[Obsolete("CloneInternals() has been deprecated.  Use the DataAdapter(DataAdapter from) constructor.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual DataAdapter CloneInternals()
		{
			DataAdapter dataAdapter = (DataAdapter)Activator.CreateInstance(base.GetType(), BindingFlags.Instance | BindingFlags.Public, null, null, CultureInfo.InvariantCulture, null);
			dataAdapter.CloneFrom(this);
			return dataAdapter;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x000B80C8 File Offset: 0x000B62C8
		private void CloneFrom(DataAdapter from)
		{
			this._acceptChangesDuringUpdate = from._acceptChangesDuringUpdate;
			this._acceptChangesDuringUpdateAfterInsert = from._acceptChangesDuringUpdateAfterInsert;
			this._continueUpdateOnError = from._continueUpdateOnError;
			this._returnProviderSpecificTypes = from._returnProviderSpecificTypes;
			this._acceptChangesDuringFill = from._acceptChangesDuringFill;
			this._fillLoadOption = from._fillLoadOption;
			this._missingMappingAction = from._missingMappingAction;
			this._missingSchemaAction = from._missingSchemaAction;
			if (from._tableMappings != null && 0 < from.TableMappings.Count)
			{
				DataTableMappingCollection tableMappings = this.TableMappings;
				foreach (object obj in from.TableMappings)
				{
					tableMappings.Add((obj is ICloneable) ? ((ICloneable)obj).Clone() : obj);
				}
			}
		}

		/// <summary>Creates a new <see cref="T:System.Data.Common.DataTableMappingCollection" />.</summary>
		/// <returns>A new table mapping collection.</returns>
		// Token: 0x06002A0A RID: 10762 RVA: 0x000B81B0 File Offset: 0x000B63B0
		protected virtual DataTableMappingCollection CreateTableMappings()
		{
			DataCommonEventSource.Log.Trace<int>("<comm.DataAdapter.CreateTableMappings|API> {0}", this.ObjectID);
			return new DataTableMappingCollection();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DataAdapter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002A0B RID: 10763 RVA: 0x000B81CC File Offset: 0x000B63CC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._tableMappings = null;
			}
			base.Dispose(disposing);
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		// Token: 0x06002A0C RID: 10764 RVA: 0x00008E4B File Offset: 0x0000704B
		public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <param name="srcTable">The name of the source table to use for table mapping.</param>
		/// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x06002A0D RID: 10765 RVA: 0x000B81E0 File Offset: 0x000B63E0
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, SchemaType>("<comm.DataAdapter.FillSchema|API> {0}, dataSet, schemaType={1}, srcTable, dataReader", this.ObjectID, schemaType);
			DataTable[] result;
			try
			{
				if (dataSet == null)
				{
					throw ADP.ArgumentNull("dataSet");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillSchemaRequiresSourceTableName("srcTable");
				}
				if (dataReader == null || dataReader.IsClosed)
				{
					throw ADP.FillRequires("dataReader");
				}
				result = (DataTable[])this.FillSchemaFromReader(dataSet, null, schemaType, srcTable, dataReader);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		// Token: 0x06002A0E RID: 10766 RVA: 0x000B8280 File Offset: 0x000B6480
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.FillSchema|API> {0}, dataTable, schemaType, dataReader", this.ObjectID);
			DataTable result;
			try
			{
				if (dataTable == null)
				{
					throw ADP.ArgumentNull("dataTable");
				}
				if (SchemaType.Source != schemaType && SchemaType.Mapped != schemaType)
				{
					throw ADP.InvalidSchemaType(schemaType);
				}
				if (dataReader == null || dataReader.IsClosed)
				{
					throw ADP.FillRequires("dataReader");
				}
				result = (DataTable)this.FillSchemaFromReader(null, dataTable, schemaType, null, dataReader);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x000B8308 File Offset: 0x000B6508
		internal object FillSchemaFromReader(DataSet dataset, DataTable datatable, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			DataTable[] array = null;
			int num = 0;
			SchemaMapping schemaMapping;
			for (;;)
			{
				DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
				if (0 < dataReaderContainer.FieldCount)
				{
					string sourceTableName = null;
					if (dataset != null)
					{
						sourceTableName = DataAdapter.GetSourceTableName(srcTable, num);
						num++;
					}
					schemaMapping = new SchemaMapping(this, dataset, datatable, dataReaderContainer, true, schemaType, sourceTableName, false, null, null);
					if (datatable != null)
					{
						break;
					}
					if (schemaMapping.DataTable != null)
					{
						if (array == null)
						{
							array = new DataTable[]
							{
								schemaMapping.DataTable
							};
						}
						else
						{
							array = DataAdapter.AddDataTableToArray(array, schemaMapping.DataTable);
						}
					}
				}
				if (!dataReader.NextResult())
				{
					goto Block_6;
				}
			}
			return schemaMapping.DataTable;
			Block_6:
			object obj = array;
			if (obj == null && datatable == null)
			{
				obj = Array.Empty<DataTable>();
			}
			return obj;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source.</summary>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema.</param>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		// Token: 0x06002A10 RID: 10768 RVA: 0x00008E4B File Offset: 0x0000704B
		public virtual int Fill(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records.</param>
		/// <param name="srcTable">A string indicating the name of the source table.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="startRecord">The zero-based index of the starting record.</param>
		/// <param name="maxRecords">An integer indicating the maximum number of records.</param>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		// Token: 0x06002A11 RID: 10769 RVA: 0x000B83A8 File Offset: 0x000B65A8
		protected virtual int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.Fill|API> {0}, dataSet, srcTable, dataReader, startRecord, maxRecords", this.ObjectID);
			int result;
			try
			{
				if (dataSet == null)
				{
					throw ADP.FillRequires("dataSet");
				}
				if (string.IsNullOrEmpty(srcTable))
				{
					throw ADP.FillRequiresSourceTableName("srcTable");
				}
				if (dataReader == null)
				{
					throw ADP.FillRequires("dataReader");
				}
				if (startRecord < 0)
				{
					throw ADP.InvalidStartRecord("startRecord", startRecord);
				}
				if (maxRecords < 0)
				{
					throw ADP.InvalidMaxRecords("maxRecords", maxRecords);
				}
				if (dataReader.IsClosed)
				{
					result = 0;
				}
				else
				{
					DataReaderContainer dataReader2 = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
					result = this.FillFromReader(dataSet, null, srcTable, dataReader2, startRecord, maxRecords, null, null);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataTable" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name and the specified <see cref="T:System.Data.IDataReader" />.</summary>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		// Token: 0x06002A12 RID: 10770 RVA: 0x000B8468 File Offset: 0x000B6668
		protected virtual int Fill(DataTable dataTable, IDataReader dataReader)
		{
			DataTable[] dataTables = new DataTable[]
			{
				dataTable
			};
			return this.Fill(dataTables, dataReader, 0, 0);
		}

		/// <summary>Adds or refreshes rows in a specified range in the collection of <see cref="T:System.Data.DataTable" /> objects to match those in the data source.</summary>
		/// <param name="dataTables">A collection of <see cref="T:System.Data.DataTable" /> objects to fill with records.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="startRecord">The zero-based index of the starting record.</param>
		/// <param name="maxRecords">An integer indicating the maximum number of records.</param>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		// Token: 0x06002A13 RID: 10771 RVA: 0x000B848C File Offset: 0x000B668C
		protected virtual int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<comm.DataAdapter.Fill|API> {0}, dataTables[], dataReader, startRecord, maxRecords", this.ObjectID);
			int result;
			try
			{
				ADP.CheckArgumentLength(dataTables, "dataTables");
				if (dataTables == null || dataTables.Length == 0 || dataTables[0] == null)
				{
					throw ADP.FillRequires("dataTable");
				}
				if (dataReader == null)
				{
					throw ADP.FillRequires("dataReader");
				}
				if (1 < dataTables.Length && (startRecord != 0 || maxRecords != 0))
				{
					throw ADP.NotSupported();
				}
				int num = 0;
				bool flag = false;
				DataSet dataSet = dataTables[0].DataSet;
				try
				{
					if (dataSet != null)
					{
						flag = dataSet.EnforceConstraints;
						dataSet.EnforceConstraints = false;
					}
					int num2 = 0;
					while (num2 < dataTables.Length && !dataReader.IsClosed)
					{
						DataReaderContainer dataReaderContainer = DataReaderContainer.Create(dataReader, this.ReturnProviderSpecificTypes);
						if (dataReaderContainer.FieldCount > 0)
						{
							goto IL_BC;
						}
						if (num2 == 0)
						{
							bool flag2;
							do
							{
								flag2 = this.FillNextResult(dataReaderContainer);
							}
							while (flag2 && dataReaderContainer.FieldCount <= 0);
							if (flag2)
							{
								goto IL_BC;
							}
							break;
						}
						IL_E7:
						num2++;
						continue;
						IL_BC:
						if (0 < num2 && !this.FillNextResult(dataReaderContainer))
						{
							break;
						}
						int num3 = this.FillFromReader(null, dataTables[num2], null, dataReaderContainer, startRecord, maxRecords, null, null);
						if (num2 == 0)
						{
							num = num3;
							goto IL_E7;
						}
						goto IL_E7;
					}
				}
				catch (ConstraintException)
				{
					flag = false;
					throw;
				}
				finally
				{
					if (flag)
					{
						dataSet.EnforceConstraints = true;
					}
				}
				result = num;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000B85DC File Offset: 0x000B67DC
		internal int FillFromReader(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int startRecord, int maxRecords, DataColumn parentChapterColumn, object parentChapterValue)
		{
			int result = 0;
			int num = 0;
			do
			{
				if (0 < dataReader.FieldCount)
				{
					SchemaMapping schemaMapping = this.FillMapping(dataset, datatable, srcTable, dataReader, num, parentChapterColumn, parentChapterValue);
					num++;
					if (schemaMapping != null && schemaMapping.DataValues != null && schemaMapping.DataTable != null)
					{
						schemaMapping.DataTable.BeginLoadData();
						try
						{
							if (1 == num && (0 < startRecord || 0 < maxRecords))
							{
								result = this.FillLoadDataRowChunk(schemaMapping, startRecord, maxRecords);
							}
							else
							{
								int num2 = this.FillLoadDataRow(schemaMapping);
								if (1 == num)
								{
									result = num2;
								}
							}
						}
						finally
						{
							schemaMapping.DataTable.EndLoadData();
						}
						if (datatable != null)
						{
							break;
						}
					}
				}
			}
			while (this.FillNextResult(dataReader));
			return result;
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000B8684 File Offset: 0x000B6884
		private int FillLoadDataRowChunk(SchemaMapping mapping, int startRecord, int maxRecords)
		{
			DataReaderContainer dataReader = mapping.DataReader;
			while (0 < startRecord)
			{
				if (!dataReader.Read())
				{
					return 0;
				}
				startRecord--;
			}
			int i = 0;
			if (0 < maxRecords)
			{
				while (i < maxRecords)
				{
					if (!dataReader.Read())
					{
						break;
					}
					if (this._hasFillErrorHandler)
					{
						try
						{
							mapping.LoadDataRowWithClear();
							i++;
							continue;
						}
						catch (Exception e) when (ADP.IsCatchableExceptionType(e))
						{
							ADP.TraceExceptionForCapture(e);
							this.OnFillErrorHandler(e, mapping.DataTable, mapping.DataValues);
							continue;
						}
					}
					mapping.LoadDataRow();
					i++;
				}
			}
			else
			{
				i = this.FillLoadDataRow(mapping);
			}
			return i;
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000B8730 File Offset: 0x000B6930
		private int FillLoadDataRow(SchemaMapping mapping)
		{
			int num = 0;
			DataReaderContainer dataReader = mapping.DataReader;
			if (this._hasFillErrorHandler)
			{
				while (dataReader.Read())
				{
					try
					{
						mapping.LoadDataRowWithClear();
						num++;
					}
					catch (Exception e) when (ADP.IsCatchableExceptionType(e))
					{
						ADP.TraceExceptionForCapture(e);
						this.OnFillErrorHandler(e, mapping.DataTable, mapping.DataValues);
					}
				}
			}
			else
			{
				while (dataReader.Read())
				{
					mapping.LoadDataRow();
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000B87C0 File Offset: 0x000B69C0
		private SchemaMapping FillMappingInternal(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int schemaCount, DataColumn parentChapterColumn, object parentChapterValue)
		{
			bool keyInfo = MissingSchemaAction.AddWithKey == this.MissingSchemaAction;
			string sourceTableName = null;
			if (dataset != null)
			{
				sourceTableName = DataAdapter.GetSourceTableName(srcTable, schemaCount);
			}
			return new SchemaMapping(this, dataset, datatable, dataReader, keyInfo, SchemaType.Mapped, sourceTableName, true, parentChapterColumn, parentChapterValue);
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x000B87F8 File Offset: 0x000B69F8
		private SchemaMapping FillMapping(DataSet dataset, DataTable datatable, string srcTable, DataReaderContainer dataReader, int schemaCount, DataColumn parentChapterColumn, object parentChapterValue)
		{
			SchemaMapping result = null;
			if (this._hasFillErrorHandler)
			{
				try
				{
					return this.FillMappingInternal(dataset, datatable, srcTable, dataReader, schemaCount, parentChapterColumn, parentChapterValue);
				}
				catch (Exception e) when (ADP.IsCatchableExceptionType(e))
				{
					ADP.TraceExceptionForCapture(e);
					this.OnFillErrorHandler(e, null, null);
					return result;
				}
			}
			result = this.FillMappingInternal(dataset, datatable, srcTable, dataReader, schemaCount, parentChapterColumn, parentChapterValue);
			return result;
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x000B8870 File Offset: 0x000B6A70
		private bool FillNextResult(DataReaderContainer dataReader)
		{
			bool result = true;
			if (this._hasFillErrorHandler)
			{
				try
				{
					return dataReader.NextResult();
				}
				catch (Exception e) when (ADP.IsCatchableExceptionType(e))
				{
					ADP.TraceExceptionForCapture(e);
					this.OnFillErrorHandler(e, null, null);
					return result;
				}
			}
			result = dataReader.NextResult();
			return result;
		}

		/// <summary>Gets the parameters set by the user when executing an SQL SELECT statement.</summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		// Token: 0x06002A1A RID: 10778 RVA: 0x000B88D4 File Offset: 0x000B6AD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual IDataParameter[] GetFillParameters()
		{
			return Array.Empty<IDataParameter>();
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000B88DB File Offset: 0x000B6ADB
		internal DataTableMapping GetTableMappingBySchemaAction(string sourceTableName, string dataSetTableName, MissingMappingAction mappingAction)
		{
			return DataTableMappingCollection.GetTableMappingBySchemaAction(this._tableMappings, sourceTableName, dataSetTableName, mappingAction);
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000B88EB File Offset: 0x000B6AEB
		internal int IndexOfDataSetTable(string dataSetTable)
		{
			if (this._tableMappings != null)
			{
				return this.TableMappings.IndexOfDataSetTable(dataSetTable);
			}
			return -1;
		}

		/// <summary>Invoked when an error occurs during a <see langword="Fill" />.</summary>
		/// <param name="value">A <see cref="T:System.Data.FillErrorEventArgs" /> object.</param>
		// Token: 0x06002A1D RID: 10781 RVA: 0x000B8903 File Offset: 0x000B6B03
		protected virtual void OnFillError(FillErrorEventArgs value)
		{
			FillErrorEventHandler fillErrorEventHandler = (FillErrorEventHandler)base.Events[DataAdapter.s_eventFillError];
			if (fillErrorEventHandler == null)
			{
				return;
			}
			fillErrorEventHandler(this, value);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x000B8928 File Offset: 0x000B6B28
		private void OnFillErrorHandler(Exception e, DataTable dataTable, object[] dataValues)
		{
			FillErrorEventArgs fillErrorEventArgs = new FillErrorEventArgs(dataTable, dataValues);
			fillErrorEventArgs.Errors = e;
			this.OnFillError(fillErrorEventArgs);
			if (fillErrorEventArgs.Continue)
			{
				return;
			}
			if (fillErrorEventArgs.Errors != null)
			{
				throw fillErrorEventArgs.Errors;
			}
			throw e;
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataSet" /> from a <see cref="T:System.Data.DataTable" /> named "Table."</summary>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> used to update the data source.</param>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid.</exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected.</exception>
		// Token: 0x06002A1F RID: 10783 RVA: 0x00008E4B File Offset: 0x0000704B
		public virtual int Update(DataSet dataSet)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000B8964 File Offset: 0x000B6B64
		private static DataTable[] AddDataTableToArray(DataTable[] tables, DataTable newTable)
		{
			for (int i = 0; i < tables.Length; i++)
			{
				if (tables[i] == newTable)
				{
					return tables;
				}
			}
			DataTable[] array = new DataTable[tables.Length + 1];
			for (int j = 0; j < tables.Length; j++)
			{
				array[j] = tables[j];
			}
			array[tables.Length] = newTable;
			return array;
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000B89AD File Offset: 0x000B6BAD
		private static string GetSourceTableName(string srcTable, int index)
		{
			if (index == 0)
			{
				return srcTable;
			}
			return srcTable + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000B89C6 File Offset: 0x000B6BC6
		// Note: this type is marked as 'beforefieldinit'.
		static DataAdapter()
		{
		}

		// Token: 0x04001AC1 RID: 6849
		private static readonly object s_eventFillError = new object();

		// Token: 0x04001AC2 RID: 6850
		private bool _acceptChangesDuringUpdate = true;

		// Token: 0x04001AC3 RID: 6851
		private bool _acceptChangesDuringUpdateAfterInsert = true;

		// Token: 0x04001AC4 RID: 6852
		private bool _continueUpdateOnError;

		// Token: 0x04001AC5 RID: 6853
		private bool _hasFillErrorHandler;

		// Token: 0x04001AC6 RID: 6854
		private bool _returnProviderSpecificTypes;

		// Token: 0x04001AC7 RID: 6855
		private bool _acceptChangesDuringFill = true;

		// Token: 0x04001AC8 RID: 6856
		private LoadOption _fillLoadOption;

		// Token: 0x04001AC9 RID: 6857
		private MissingMappingAction _missingMappingAction = MissingMappingAction.Passthrough;

		// Token: 0x04001ACA RID: 6858
		private MissingSchemaAction _missingSchemaAction = MissingSchemaAction.Add;

		// Token: 0x04001ACB RID: 6859
		private DataTableMappingCollection _tableMappings;

		// Token: 0x04001ACC RID: 6860
		private static int s_objectTypeCount;

		// Token: 0x04001ACD RID: 6861
		internal readonly int _objectID = Interlocked.Increment(ref DataAdapter.s_objectTypeCount);
	}
}
