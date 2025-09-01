using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>Contains a description of a mapped relationship between a source table and a <see cref="T:System.Data.DataTable" />. This class is used by a <see cref="T:System.Data.Common.DataAdapter" /> when populating a <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x02000385 RID: 901
	[TypeConverter(typeof(DataTableMapping.DataTableMappingConverter))]
	public sealed class DataTableMapping : MarshalByRefObject, ITableMapping, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class.</summary>
		// Token: 0x06002ABD RID: 10941 RVA: 0x00003DB9 File Offset: 0x00001FB9
		public DataTableMapping()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class with a source when given a source table name and a <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <param name="sourceTable">The case-sensitive source table name from a data source.</param>
		/// <param name="dataSetTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to.</param>
		// Token: 0x06002ABE RID: 10942 RVA: 0x000BA028 File Offset: 0x000B8228
		public DataTableMapping(string sourceTable, string dataSetTable)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class when given a source table name, a <see cref="T:System.Data.DataTable" /> name, and an array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</summary>
		/// <param name="sourceTable">The case-sensitive source table name from a data source.</param>
		/// <param name="dataSetTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to.</param>
		/// <param name="columnMappings">An array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</param>
		// Token: 0x06002ABF RID: 10943 RVA: 0x000BA03E File Offset: 0x000B823E
		public DataTableMapping(string sourceTable, string dataSetTable, DataColumnMapping[] columnMappings)
		{
			this.SourceTable = sourceTable;
			this.DataSetTable = dataSetTable;
			if (columnMappings != null && columnMappings.Length != 0)
			{
				this.ColumnMappings.AddRange(columnMappings);
			}
		}

		/// <summary>Gets the derived <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A data column mapping collection.</returns>
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x000BA067 File Offset: 0x000B8267
		IColumnMappingCollection ITableMapping.ColumnMappings
		{
			get
			{
				return this.ColumnMappings;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A data column mapping collection.</returns>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000BA070 File Offset: 0x000B8270
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataColumnMappingCollection ColumnMappings
		{
			get
			{
				DataColumnMappingCollection dataColumnMappingCollection = this._columnMappings;
				if (dataColumnMappingCollection == null)
				{
					dataColumnMappingCollection = new DataColumnMappingCollection();
					this._columnMappings = dataColumnMappingCollection;
				}
				return dataColumnMappingCollection;
			}
		}

		/// <summary>Gets or sets the table name from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The table name from a <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x000BA095 File Offset: 0x000B8295
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x000BA0A6 File Offset: 0x000B82A6
		[DefaultValue("")]
		public string DataSetTable
		{
			get
			{
				return this._dataSetTableName ?? string.Empty;
			}
			set
			{
				this._dataSetTableName = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x000BA0AF File Offset: 0x000B82AF
		// (set) Token: 0x06002AC5 RID: 10949 RVA: 0x000BA0B7 File Offset: 0x000B82B7
		internal DataTableMappingCollection Parent
		{
			get
			{
				return this._parent;
			}
			set
			{
				this._parent = value;
			}
		}

		/// <summary>Gets or sets the case-sensitive source table name from a data source.</summary>
		/// <returns>The case-sensitive source table name from a data source.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x000BA0C0 File Offset: 0x000B82C0
		// (set) Token: 0x06002AC7 RID: 10951 RVA: 0x000BA0D1 File Offset: 0x000B82D1
		[DefaultValue("")]
		public string SourceTable
		{
			get
			{
				return this._sourceTableName ?? string.Empty;
			}
			set
			{
				if (this.Parent != null && ADP.SrcCompare(this._sourceTableName, value) != 0)
				{
					this.Parent.ValidateSourceTable(-1, value);
				}
				this._sourceTableName = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of the current instance.</returns>
		// Token: 0x06002AC8 RID: 10952 RVA: 0x000BA100 File Offset: 0x000B8300
		object ICloneable.Clone()
		{
			DataTableMapping dataTableMapping = new DataTableMapping();
			dataTableMapping._dataSetTableName = this._dataSetTableName;
			dataTableMapping._sourceTableName = this._sourceTableName;
			if (this._columnMappings != null && 0 < this.ColumnMappings.Count)
			{
				DataColumnMappingCollection columnMappings = dataTableMapping.ColumnMappings;
				foreach (object obj in this.ColumnMappings)
				{
					ICloneable cloneable = (ICloneable)obj;
					columnMappings.Add(cloneable.Clone());
				}
			}
			return dataTableMapping;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataColumn" /> object for a given column name.</summary>
		/// <param name="sourceColumn">The name of the <see cref="T:System.Data.DataColumn" />.</param>
		/// <param name="dataType">The data type for <paramref name="sourceColumn" />.</param>
		/// <param name="dataTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to.</param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		// Token: 0x06002AC9 RID: 10953 RVA: 0x000BA1A0 File Offset: 0x000B83A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumn(string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			return DataColumnMappingCollection.GetDataColumn(this._columnMappings, sourceColumn, dataType, dataTable, mappingAction, schemaAction);
		}

		/// <summary>Gets a <see cref="T:System.Data.DataColumn" /> from the specified <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Data.MissingMappingAction" /> value and the name of the <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="sourceColumn">The name of the <see cref="T:System.Data.DataColumn" />.</param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
		/// <returns>A data column.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to <see langword="Error" />, and no mapping was specified.</exception>
		// Token: 0x06002ACA RID: 10954 RVA: 0x000BA1B4 File Offset: 0x000B83B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumnMapping GetColumnMappingBySchemaAction(string sourceColumn, MissingMappingAction mappingAction)
		{
			return DataColumnMappingCollection.GetColumnMappingBySchemaAction(this._columnMappings, sourceColumn, mappingAction);
		}

		/// <summary>Gets the current <see cref="T:System.Data.DataTable" /> for the specified <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Data.MissingSchemaAction" /> value.</summary>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> from which to get the <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		/// <returns>A data table.</returns>
		// Token: 0x06002ACB RID: 10955 RVA: 0x000BA1C4 File Offset: 0x000B83C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataTable GetDataTableBySchemaAction(DataSet dataSet, MissingSchemaAction schemaAction)
		{
			if (dataSet == null)
			{
				throw ADP.ArgumentNull("dataSet");
			}
			string dataSetTable = this.DataSetTable;
			if (string.IsNullOrEmpty(dataSetTable))
			{
				return null;
			}
			DataTableCollection tables = dataSet.Tables;
			int num = tables.IndexOf(dataSetTable);
			if (0 <= num && num < tables.Count)
			{
				return tables[num];
			}
			switch (schemaAction)
			{
			case MissingSchemaAction.Add:
			case MissingSchemaAction.AddWithKey:
				return new DataTable(dataSetTable);
			case MissingSchemaAction.Ignore:
				return null;
			case MissingSchemaAction.Error:
				throw ADP.MissingTableSchema(dataSetTable, this.SourceTable);
			default:
				throw ADP.InvalidMissingSchemaAction(schemaAction);
			}
		}

		/// <summary>Converts the current <see cref="P:System.Data.Common.DataTableMapping.SourceTable" /> name to a string.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DataTableMapping.SourceTable" /> name, as a string.</returns>
		// Token: 0x06002ACC RID: 10956 RVA: 0x000BA24A File Offset: 0x000B844A
		public override string ToString()
		{
			return this.SourceTable;
		}

		// Token: 0x04001B11 RID: 6929
		private DataTableMappingCollection _parent;

		// Token: 0x04001B12 RID: 6930
		private DataColumnMappingCollection _columnMappings;

		// Token: 0x04001B13 RID: 6931
		private string _dataSetTableName;

		// Token: 0x04001B14 RID: 6932
		private string _sourceTableName;

		// Token: 0x02000386 RID: 902
		internal sealed class DataTableMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x06002ACD RID: 10957 RVA: 0x0002C704 File Offset: 0x0002A904
			public DataTableMappingConverter()
			{
			}

			// Token: 0x06002ACE RID: 10958 RVA: 0x00079687 File Offset: 0x00077887
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06002ACF RID: 10959 RVA: 0x000BA254 File Offset: 0x000B8454
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is DataTableMapping)
				{
					DataTableMapping dataTableMapping = (DataTableMapping)value;
					DataColumnMapping[] array = new DataColumnMapping[dataTableMapping.ColumnMappings.Count];
					dataTableMapping.ColumnMappings.CopyTo(array, 0);
					object[] arguments = new object[]
					{
						dataTableMapping.SourceTable,
						dataTableMapping.DataSetTable,
						array
					};
					Type[] types = new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(DataColumnMapping[])
					};
					return new InstanceDescriptor(typeof(DataTableMapping).GetConstructor(types), arguments);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
