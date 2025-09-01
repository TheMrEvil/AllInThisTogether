using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>Contains a generic column mapping for an object that inherits from <see cref="T:System.Data.Common.DataAdapter" />. This class cannot be inherited.</summary>
	// Token: 0x0200037E RID: 894
	[TypeConverter(typeof(DataColumnMapping.DataColumnMappingConverter))]
	public sealed class DataColumnMapping : MarshalByRefObject, IColumnMapping, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class.</summary>
		// Token: 0x06002A25 RID: 10789 RVA: 0x00003DB9 File Offset: 0x00001FB9
		public DataColumnMapping()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class with the specified source column name and <see cref="T:System.Data.DataSet" /> column name to map to.</summary>
		/// <param name="sourceColumn">The case-sensitive column name from a data source.</param>
		/// <param name="dataSetColumn">The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to.</param>
		// Token: 0x06002A26 RID: 10790 RVA: 0x000B89E7 File Offset: 0x000B6BE7
		public DataColumnMapping(string sourceColumn, string dataSetColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DataSetColumn = dataSetColumn;
		}

		/// <summary>Gets or sets the name of the column within the <see cref="T:System.Data.DataSet" /> to map to.</summary>
		/// <returns>The name of the column within the <see cref="T:System.Data.DataSet" /> to map to. The name is not case sensitive.</returns>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002A27 RID: 10791 RVA: 0x000B89FD File Offset: 0x000B6BFD
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x000B8A0E File Offset: 0x000B6C0E
		[DefaultValue("")]
		public string DataSetColumn
		{
			get
			{
				return this._dataSetColumnName ?? string.Empty;
			}
			set
			{
				this._dataSetColumnName = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000B8A17 File Offset: 0x000B6C17
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x000B8A1F File Offset: 0x000B6C1F
		internal DataColumnMappingCollection Parent
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

		/// <summary>Gets or sets the name of the column within the data source to map from. The name is case-sensitive.</summary>
		/// <returns>The case-sensitive name of the column in the data source.</returns>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000B8A28 File Offset: 0x000B6C28
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x000B8A39 File Offset: 0x000B6C39
		[DefaultValue("")]
		public string SourceColumn
		{
			get
			{
				return this._sourceColumnName ?? string.Empty;
			}
			set
			{
				if (this.Parent != null && ADP.SrcCompare(this._sourceColumnName, value) != 0)
				{
					this.Parent.ValidateSourceColumn(-1, value);
				}
				this._sourceColumnName = value;
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A copy of the current object.</returns>
		// Token: 0x06002A2D RID: 10797 RVA: 0x000B8A65 File Offset: 0x000B6C65
		object ICloneable.Clone()
		{
			return new DataColumnMapping
			{
				_sourceColumnName = this._sourceColumnName,
				_dataSetColumnName = this._dataSetColumnName
			};
		}

		/// <summary>Gets a <see cref="T:System.Data.DataColumn" /> from the given <see cref="T:System.Data.DataTable" /> using the <see cref="T:System.Data.MissingSchemaAction" /> and the <see cref="P:System.Data.Common.DataColumnMapping.DataSetColumn" /> property.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to get the column from.</param>
		/// <param name="dataType">The <see cref="T:System.Type" /> of the data column.</param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		/// <returns>A data column.</returns>
		// Token: 0x06002A2E RID: 10798 RVA: 0x000B8A84 File Offset: 0x000B6C84
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumnBySchemaAction(DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			return DataColumnMapping.GetDataColumnBySchemaAction(this.SourceColumn, this.DataSetColumn, dataTable, dataType, schemaAction);
		}

		/// <summary>A static version of <see cref="M:System.Data.Common.DataColumnMapping.GetDataColumnBySchemaAction(System.Data.DataTable,System.Type,System.Data.MissingSchemaAction)" /> that can be called without instantiating a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</summary>
		/// <param name="sourceColumn">The case-sensitive column name from a data source.</param>
		/// <param name="dataSetColumn">The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to.</param>
		/// <param name="dataTable">An instance of <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="dataType">The data type for the column being mapped.</param>
		/// <param name="schemaAction">Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		// Token: 0x06002A2F RID: 10799 RVA: 0x000B8A9C File Offset: 0x000B6C9C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (dataTable == null)
			{
				throw ADP.ArgumentNull("dataTable");
			}
			if (string.IsNullOrEmpty(dataSetColumn))
			{
				return null;
			}
			DataColumnCollection columns = dataTable.Columns;
			int num = columns.IndexOf(dataSetColumn);
			if (0 > num || num >= columns.Count)
			{
				return DataColumnMapping.CreateDataColumnBySchemaAction(sourceColumn, dataSetColumn, dataTable, dataType, schemaAction);
			}
			DataColumn dataColumn = columns[num];
			if (!string.IsNullOrEmpty(dataColumn.Expression))
			{
				throw ADP.ColumnSchemaExpression(sourceColumn, dataSetColumn);
			}
			if (null == dataType || dataType.IsArray == dataColumn.DataType.IsArray)
			{
				return dataColumn;
			}
			throw ADP.ColumnSchemaMismatch(sourceColumn, dataType, dataColumn);
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x000B8B2C File Offset: 0x000B6D2C
		internal static DataColumn CreateDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (string.IsNullOrEmpty(dataSetColumn))
			{
				return null;
			}
			switch (schemaAction)
			{
			case MissingSchemaAction.Add:
			case MissingSchemaAction.AddWithKey:
				return new DataColumn(dataSetColumn, dataType);
			case MissingSchemaAction.Ignore:
				return null;
			case MissingSchemaAction.Error:
				throw ADP.ColumnSchemaMissing(dataSetColumn, dataTable.TableName, sourceColumn);
			default:
				throw ADP.InvalidMissingSchemaAction(schemaAction);
			}
		}

		/// <summary>Converts the current <see cref="P:System.Data.Common.DataColumnMapping.SourceColumn" /> name to a string.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DataColumnMapping.SourceColumn" /> name as a string.</returns>
		// Token: 0x06002A31 RID: 10801 RVA: 0x000B8B7D File Offset: 0x000B6D7D
		public override string ToString()
		{
			return this.SourceColumn;
		}

		// Token: 0x04001ACE RID: 6862
		private DataColumnMappingCollection _parent;

		// Token: 0x04001ACF RID: 6863
		private string _dataSetColumnName;

		// Token: 0x04001AD0 RID: 6864
		private string _sourceColumnName;

		// Token: 0x0200037F RID: 895
		internal sealed class DataColumnMappingConverter : ExpandableObjectConverter
		{
			// Token: 0x06002A32 RID: 10802 RVA: 0x0002C704 File Offset: 0x0002A904
			public DataColumnMappingConverter()
			{
			}

			// Token: 0x06002A33 RID: 10803 RVA: 0x00079687 File Offset: 0x00077887
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				return typeof(InstanceDescriptor) == destinationType || base.CanConvertTo(context, destinationType);
			}

			// Token: 0x06002A34 RID: 10804 RVA: 0x000B8B88 File Offset: 0x000B6D88
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (null == destinationType)
				{
					throw ADP.ArgumentNull("destinationType");
				}
				if (typeof(InstanceDescriptor) == destinationType && value is DataColumnMapping)
				{
					DataColumnMapping dataColumnMapping = (DataColumnMapping)value;
					object[] arguments = new object[]
					{
						dataColumnMapping.SourceColumn,
						dataColumnMapping.DataSetColumn
					};
					Type[] types = new Type[]
					{
						typeof(string),
						typeof(string)
					};
					return new InstanceDescriptor(typeof(DataColumnMapping).GetConstructor(types), arguments);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}
}
