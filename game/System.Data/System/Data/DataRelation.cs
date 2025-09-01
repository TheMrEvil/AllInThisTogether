using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents a parent/child relationship between two <see cref="T:System.Data.DataTable" /> objects.</summary>
	// Token: 0x020000B8 RID: 184
	[DefaultProperty("RelationName")]
	[TypeConverter(typeof(RelationshipConverter))]
	public class DataRelation
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified <see cref="T:System.Data.DataRelation" /> name, and parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="relationName">The name of the <see cref="T:System.Data.DataRelation" />. If <see langword="null" /> or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />.</param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the relationship.</param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the relationship.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types  
		///  -Or-  
		///  The tables do not belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000B25 RID: 2853 RVA: 0x0002E512 File Offset: 0x0002C712
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn) : this(relationName, parentColumn, childColumn, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified name, parent and child <see cref="T:System.Data.DataColumn" /> objects, and a value that indicates whether to create constraints.</summary>
		/// <param name="relationName">The name of the relation. If <see langword="null" /> or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />.</param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the relation.</param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the relation.</param>
		/// <param name="createConstraints">A value that indicates whether constraints are created. <see langword="true" />, if constraints are created. Otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types  
		///  -Or-  
		///  The tables do not belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000B26 RID: 2854 RVA: 0x0002E520 File Offset: 0x0002C720
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
		{
			this._relationName = string.Empty;
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);
			base..ctor();
			DataCommonEventSource.Log.Trace<int, string, int, int, bool>("<ds.DataRelation.DataRelation|API> {0}, relationName='{1}', parentColumn={2}, childColumn={3}, createConstraints={4}", this.ObjectID, relationName, (parentColumn != null) ? parentColumn.ObjectID : 0, (childColumn != null) ? childColumn.ObjectID : 0, createConstraints);
			this.Create(relationName, new DataColumn[]
			{
				parentColumn
			}, new DataColumn[]
			{
				childColumn
			}, createConstraints);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified <see cref="T:System.Data.DataRelation" /> name and matched arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="relationName">The name of the relation. If <see langword="null" /> or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />.</param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects.</param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The <see cref="T:System.Data.DataColumn" /> objects have different data types  
		///  -Or-  
		///  One or both of the arrays are not composed of distinct columns from the same table.  
		///  -Or-  
		///  The tables do not belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000B27 RID: 2855 RVA: 0x0002E5A6 File Offset: 0x0002C7A6
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns) : this(relationName, parentColumns, childColumns, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified name, matched arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects, and value that indicates whether to create constraints.</summary>
		/// <param name="relationName">The name of the relation. If <see langword="null" /> or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />.</param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects.</param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> objects.</param>
		/// <param name="createConstraints">A value that indicates whether to create constraints. <see langword="true" />, if constraints are created. Otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects is <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types  
		///  -Or-  
		///  The tables do not belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000B28 RID: 2856 RVA: 0x0002E5B2 File Offset: 0x0002C7B2
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			this._relationName = string.Empty;
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);
			base..ctor();
			this.Create(relationName, parentColumns, childColumns, createConstraints);
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio environment.</summary>
		/// <param name="relationName">The name of the relation. If <see langword="null" /> or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />.</param>
		/// <param name="parentTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the parent table of the relation.</param>
		/// <param name="childTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the child table of the relation.</param>
		/// <param name="parentColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the parent <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="childColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the child <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="nested">A value that indicates whether relationships are nested.</param>
		// Token: 0x06000B29 RID: 2857 RVA: 0x0002E5E8 File Offset: 0x0002C7E8
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string childTableName, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this._relationName = string.Empty;
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);
			base..ctor();
			this._relationName = relationName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._childTableName = childTableName;
			this._nested = nested;
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio environment.</summary>
		/// <param name="relationName">The name of the <see cref="T:System.Data.DataRelation" />. If <see langword="null" /> or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />.</param>
		/// <param name="parentTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the parent table of the relation.</param>
		/// <param name="parentTableNamespace">The name of the parent table namespace.</param>
		/// <param name="childTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the child table of the relation.</param>
		/// <param name="childTableNamespace">The name of the child table namespace.</param>
		/// <param name="parentColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the parent <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="childColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the child <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="nested">A value that indicates whether relationships are nested.</param>
		// Token: 0x06000B2A RID: 2858 RVA: 0x0002E64C File Offset: 0x0002C84C
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string parentTableNamespace, string childTableName, string childTableNamespace, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this._relationName = string.Empty;
			this._checkMultipleNested = true;
			this._objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);
			base..ctor();
			this._relationName = relationName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._childTableName = childTableName;
			this._parentTableNamespace = parentTableNamespace;
			this._childTableNamespace = childTableNamespace;
			this._nested = nested;
		}

		/// <summary>Gets the child <see cref="T:System.Data.DataColumn" /> objects of this relation.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0002E6BE File Offset: 0x0002C8BE
		public virtual DataColumn[] ChildColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.ToArray();
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0002E6D1 File Offset: 0x0002C8D1
		internal DataColumn[] ChildColumnsReference
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.ColumnsReference;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0002E6E4 File Offset: 0x0002C8E4
		internal DataKey ChildKey
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey;
			}
		}

		/// <summary>Gets the child table of this relation.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the child table of the relation.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002E6F2 File Offset: 0x0002C8F2
		public virtual DataTable ChildTable
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.Table;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which the <see cref="T:System.Data.DataRelation" /> belongs.</summary>
		/// <returns>A <see cref="T:System.Data.DataSet" /> to which the <see cref="T:System.Data.DataRelation" /> belongs.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0002E705 File Offset: 0x0002C905
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DataSet DataSet
		{
			get
			{
				this.CheckStateForProperty();
				return this._dataSet;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002E713 File Offset: 0x0002C913
		internal string[] ParentColumnNames
		{
			get
			{
				return this._parentKey.GetColumnNames();
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0002E720 File Offset: 0x0002C920
		internal string[] ChildColumnNames
		{
			get
			{
				return this._childKey.GetColumnNames();
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002E730 File Offset: 0x0002C930
		private static bool IsKeyNull(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (!DataStorage.IsObjectNull(values[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002E758 File Offset: 0x0002C958
		internal static DataRow[] GetChildRows(DataKey parentKey, DataKey childKey, DataRow parentRow, DataRowVersion version)
		{
			object[] keyValues = parentRow.GetKeyValues(parentKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return childKey.Table.NewRowArray(0);
			}
			return childKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows).GetRows(keyValues);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002E7A0 File Offset: 0x0002C9A0
		internal static DataRow[] GetParentRows(DataKey parentKey, DataKey childKey, DataRow childRow, DataRowVersion version)
		{
			object[] keyValues = childRow.GetKeyValues(childKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return parentKey.Table.NewRowArray(0);
			}
			return parentKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows).GetRows(keyValues);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002E7E8 File Offset: 0x0002C9E8
		internal static DataRow GetParentRow(DataKey parentKey, DataKey childKey, DataRow childRow, DataRowVersion version)
		{
			if (!childRow.HasVersion((version == DataRowVersion.Original) ? DataRowVersion.Original : DataRowVersion.Current) && childRow._tempRecord == -1)
			{
				return null;
			}
			object[] keyValues = childRow.GetKeyValues(childKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return null;
			}
			Index sortIndex = parentKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			Range range = sortIndex.FindRecords(keyValues);
			if (range.IsNull)
			{
				return null;
			}
			if (range.Count > 1)
			{
				throw ExceptionBuilder.MultipleParents();
			}
			return parentKey.Table._recordManager[sortIndex.GetRecord(range.Min)];
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002E886 File Offset: 0x0002CA86
		internal void SetDataSet(DataSet dataSet)
		{
			if (this._dataSet != dataSet)
			{
				this._dataSet = dataSet;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002E898 File Offset: 0x0002CA98
		internal void SetParentRowRecords(DataRow childRow, DataRow parentRow)
		{
			object[] keyValues = parentRow.GetKeyValues(this.ParentKey);
			if (childRow._tempRecord != -1)
			{
				this.ChildTable._recordManager.SetKeyValues(childRow._tempRecord, this.ChildKey, keyValues);
			}
			if (childRow._newRecord != -1)
			{
				this.ChildTable._recordManager.SetKeyValues(childRow._newRecord, this.ChildKey, keyValues);
			}
			if (childRow._oldRecord != -1)
			{
				this.ChildTable._recordManager.SetKeyValues(childRow._oldRecord, this.ChildKey, keyValues);
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of this <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of this <see cref="T:System.Data.DataRelation" />.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0002E924 File Offset: 0x0002CB24
		public virtual DataColumn[] ParentColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey.ToArray();
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0002E937 File Offset: 0x0002CB37
		internal DataColumn[] ParentColumnsReference
		{
			get
			{
				return this._parentKey.ColumnsReference;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0002E944 File Offset: 0x0002CB44
		internal DataKey ParentKey
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey;
			}
		}

		/// <summary>Gets the parent <see cref="T:System.Data.DataTable" /> of this <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the parent table of this relation.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0002E952 File Offset: 0x0002CB52
		public virtual DataTable ParentTable
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey.Table;
			}
		}

		/// <summary>Gets or sets the name used to retrieve a <see cref="T:System.Data.DataRelation" /> from the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <returns>The name of the a <see cref="T:System.Data.DataRelation" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see langword="null" /> or empty string ("") was passed into a <see cref="T:System.Data.DataColumn" /> that is a <see cref="T:System.Data.DataRelation" />.</exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The <see cref="T:System.Data.DataRelation" /> belongs to a collection that already contains a <see cref="T:System.Data.DataRelation" /> with the same name.</exception>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0002E965 File Offset: 0x0002CB65
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0002E974 File Offset: 0x0002CB74
		[DefaultValue("")]
		public virtual string RelationName
		{
			get
			{
				this.CheckStateForProperty();
				return this._relationName;
			}
			set
			{
				long scopeId = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataRelation.set_RelationName|API> {0}, '{1}'", this.ObjectID, value);
				try
				{
					if (value == null)
					{
						value = string.Empty;
					}
					CultureInfo culture = (this._dataSet != null) ? this._dataSet.Locale : CultureInfo.CurrentCulture;
					if (string.Compare(this._relationName, value, true, culture) != 0)
					{
						if (this._dataSet != null)
						{
							if (value.Length == 0)
							{
								throw ExceptionBuilder.NoRelationName();
							}
							this._dataSet.Relations.RegisterName(value);
							if (this._relationName.Length != 0)
							{
								this._dataSet.Relations.UnregisterName(this._relationName);
							}
						}
						this._relationName = value;
						((DataRelationCollection.DataTableRelationCollection)this.ParentTable.ChildRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						((DataRelationCollection.DataTableRelationCollection)this.ChildTable.ParentRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
					}
					else if (string.Compare(this._relationName, value, false, culture) != 0)
					{
						this._relationName = value;
						((DataRelationCollection.DataTableRelationCollection)this.ParentTable.ChildRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
						((DataRelationCollection.DataTableRelationCollection)this.ChildTable.ParentRelations).OnRelationPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(scopeId);
				}
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
		internal void CheckNamespaceValidityForNestedRelations(string ns)
		{
			foreach (object obj in this.ChildTable.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if ((dataRelation == this || dataRelation.Nested) && dataRelation.ParentTable.Namespace != ns)
				{
					throw ExceptionBuilder.InValidNestedRelation(this.ChildTable.TableName);
				}
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0002EB5C File Offset: 0x0002CD5C
		internal void CheckNestedRelations()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataRelation.CheckNestedRelations|INFO> {0}", this.ObjectID);
			DataTable parentTable = this.ParentTable;
			if (this.ChildTable != this.ParentTable)
			{
				List<DataTable> list = new List<DataTable>();
				list.Add(this.ChildTable);
				for (int i = 0; i < list.Count; i++)
				{
					foreach (DataRelation dataRelation in list[i].NestedParentRelations)
					{
						if (dataRelation.ParentTable == this.ChildTable && dataRelation.ChildTable != this.ChildTable)
						{
							throw ExceptionBuilder.LoopInNestedRelations(this.ChildTable.TableName);
						}
						if (!list.Contains(dataRelation.ParentTable))
						{
							list.Add(dataRelation.ParentTable);
						}
					}
				}
				return;
			}
			if (string.Compare(this.ChildTable.TableName, this.ChildTable.DataSet.DataSetName, true, this.ChildTable.DataSet.Locale) == 0)
			{
				throw ExceptionBuilder.SelfnestedDatasetConflictingName(this.ChildTable.TableName);
			}
		}

		/// <summary>Gets or sets a value that indicates whether <see cref="T:System.Data.DataRelation" /> objects are nested.</summary>
		/// <returns>
		///   <see langword="true" />, if <see cref="T:System.Data.DataRelation" /> objects are nested; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0002EC66 File Offset: 0x0002CE66
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x0002EC74 File Offset: 0x0002CE74
		[DefaultValue(false)]
		public virtual bool Nested
		{
			get
			{
				this.CheckStateForProperty();
				return this._nested;
			}
			set
			{
				long scopeId = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataRelation.set_Nested|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._nested != value)
					{
						if (this._dataSet != null && value)
						{
							if (this.ChildTable.IsNamespaceInherited())
							{
								this.CheckNamespaceValidityForNestedRelations(this.ParentTable.Namespace);
							}
							ForeignKeyConstraint foreignKeyConstraint = this.ChildTable.Constraints.FindForeignKeyConstraint(this.ChildKey.ColumnsReference, this.ParentKey.ColumnsReference);
							if (foreignKeyConstraint != null)
							{
								foreignKeyConstraint.CheckConstraint();
							}
							this.ValidateMultipleNestedRelations();
						}
						if (!value && this._parentKey.ColumnsReference[0].ColumnMapping == MappingType.Hidden)
						{
							throw ExceptionBuilder.RelationNestedReadOnly();
						}
						if (value)
						{
							this.ParentTable.Columns.RegisterColumnName(this.ChildTable.TableName, null);
						}
						else
						{
							this.ParentTable.Columns.UnregisterName(this.ChildTable.TableName);
						}
						this.RaisePropertyChanging("Nested");
						if (value)
						{
							this.CheckNestedRelations();
							if (this.DataSet != null)
							{
								if (this.ParentTable == this.ChildTable)
								{
									foreach (object obj in this.ChildTable.Rows)
									{
										((DataRow)obj).CheckForLoops(this);
									}
									if (this.ChildTable.DataSet != null && string.Compare(this.ChildTable.TableName, this.ChildTable.DataSet.DataSetName, true, this.ChildTable.DataSet.Locale) == 0)
									{
										throw ExceptionBuilder.DatasetConflictingName(this._dataSet.DataSetName);
									}
									this.ChildTable._fNestedInDataset = false;
								}
								else
								{
									foreach (object obj2 in this.ChildTable.Rows)
									{
										((DataRow)obj2).GetParentRow(this);
									}
								}
							}
							DataTable parentTable = this.ParentTable;
							int elementColumnCount = parentTable.ElementColumnCount;
							parentTable.ElementColumnCount = elementColumnCount + 1;
						}
						else
						{
							DataTable parentTable2 = this.ParentTable;
							int elementColumnCount = parentTable2.ElementColumnCount;
							parentTable2.ElementColumnCount = elementColumnCount - 1;
						}
						this._nested = value;
						this.ChildTable.CacheNestedParent();
						if (value && string.IsNullOrEmpty(this.ChildTable.Namespace) && (this.ChildTable.NestedParentsCount > 1 || (this.ChildTable.NestedParentsCount > 0 && !this.ChildTable.DataSet.Relations.Contains(this.RelationName))))
						{
							string text = null;
							foreach (object obj3 in this.ChildTable.ParentRelations)
							{
								DataRelation dataRelation = (DataRelation)obj3;
								if (dataRelation.Nested)
								{
									if (text == null)
									{
										text = dataRelation.ParentTable.Namespace;
									}
									else if (string.Compare(text, dataRelation.ParentTable.Namespace, StringComparison.Ordinal) != 0)
									{
										this._nested = false;
										throw ExceptionBuilder.InvalidParentNamespaceinNestedRelation(this.ChildTable.TableName);
									}
								}
							}
							if (this.CheckMultipleNested && this.ChildTable._tableNamespace != null && this.ChildTable._tableNamespace.Length == 0)
							{
								throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
							}
							this.ChildTable._tableNamespace = null;
						}
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(scopeId);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.UniqueConstraint" /> that guarantees that values in the parent column of a <see cref="T:System.Data.DataRelation" /> are unique.</summary>
		/// <returns>A <see cref="T:System.Data.UniqueConstraint" /> that makes sure that values in a parent column are unique.</returns>
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0002F058 File Offset: 0x0002D258
		public virtual UniqueConstraint ParentKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKeyConstraint;
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0002F066 File Offset: 0x0002D266
		internal void SetParentKeyConstraint(UniqueConstraint value)
		{
			this._parentKeyConstraint = value;
		}

		/// <summary>Gets the <see cref="T:System.Data.ForeignKeyConstraint" /> for the relation.</summary>
		/// <returns>A <see langword="ForeignKeyConstraint" />.</returns>
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0002F06F File Offset: 0x0002D26F
		public virtual ForeignKeyConstraint ChildKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKeyConstraint;
			}
		}

		/// <summary>Gets the collection that stores customized properties.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> that contains customized properties.</returns>
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0002F080 File Offset: 0x0002D280
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection result;
				if ((result = this._extendedProperties) == null)
				{
					result = (this._extendedProperties = new PropertyCollection());
				}
				return result;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002F0A5 File Offset: 0x0002D2A5
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x0002F0AD File Offset: 0x0002D2AD
		internal bool CheckMultipleNested
		{
			get
			{
				return this._checkMultipleNested;
			}
			set
			{
				this._checkMultipleNested = value;
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002F0B6 File Offset: 0x0002D2B6
		internal void SetChildKeyConstraint(ForeignKeyConstraint value)
		{
			this._childKeyConstraint = value;
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000B49 RID: 2889 RVA: 0x0002F0C0 File Offset: 0x0002D2C0
		// (remove) Token: 0x06000B4A RID: 2890 RVA: 0x0002F0F8 File Offset: 0x0002D2F8
		internal event PropertyChangedEventHandler PropertyChanging
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanging;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanging, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanging;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanging, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0002F130 File Offset: 0x0002D330
		internal void CheckState()
		{
			if (this._dataSet == null)
			{
				this._parentKey.CheckState();
				this._childKey.CheckState();
				if (this._parentKey.Table.DataSet != this._childKey.Table.DataSet)
				{
					throw ExceptionBuilder.RelationDataSetMismatch();
				}
				if (this._childKey.ColumnsEqual(this._parentKey))
				{
					throw ExceptionBuilder.KeyColumnsIdentical();
				}
				for (int i = 0; i < this._parentKey.ColumnsReference.Length; i++)
				{
					if (this._parentKey.ColumnsReference[i].DataType != this._childKey.ColumnsReference[i].DataType || (this._parentKey.ColumnsReference[i].DataType == typeof(DateTime) && this._parentKey.ColumnsReference[i].DateTimeMode != this._childKey.ColumnsReference[i].DateTimeMode && (this._parentKey.ColumnsReference[i].DateTimeMode & this._childKey.ColumnsReference[i].DateTimeMode) != DataSetDateTime.Unspecified))
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
				}
			}
		}

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <exception cref="T:System.Data.DataException">The parent and child tables belong to different <see cref="T:System.Data.DataSet" /> objects.  
		///  -Or-  
		///  One or more pairs of parent and child <see cref="T:System.Data.DataColumn" /> objects have mismatched data types.  
		///  -Or-  
		///  The parent and child <see cref="T:System.Data.DataColumn" /> objects are identical.</exception>
		// Token: 0x06000B4C RID: 2892 RVA: 0x0002F260 File Offset: 0x0002D460
		protected void CheckStateForProperty()
		{
			try
			{
				this.CheckState();
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExceptionBuilder.BadObjectPropertyAccess(ex.Message);
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002F2AC File Offset: 0x0002D4AC
		private void Create(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, string, bool>("<ds.DataRelation.Create|INFO> {0}, relationName='{1}', createConstraints={2}", this.ObjectID, relationName, createConstraints);
			try
			{
				this._parentKey = new DataKey(parentColumns, true);
				this._childKey = new DataKey(childColumns, true);
				if (parentColumns.Length != childColumns.Length)
				{
					throw ExceptionBuilder.KeyLengthMismatch();
				}
				for (int i = 0; i < parentColumns.Length; i++)
				{
					if (parentColumns[i].Table.DataSet == null || childColumns[i].Table.DataSet == null)
					{
						throw ExceptionBuilder.ParentOrChildColumnsDoNotHaveDataSet();
					}
				}
				this.CheckState();
				this._relationName = ((relationName == null) ? "" : relationName);
				this._createConstraints = createConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0002F36C File Offset: 0x0002D56C
		internal DataRelation Clone(DataSet destination)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelation.Clone|INFO> {0}, destination={1}", this.ObjectID, (destination != null) ? destination.ObjectID : 0);
			DataTable dataTable = destination.Tables[this.ParentTable.TableName, this.ParentTable.Namespace];
			DataTable dataTable2 = destination.Tables[this.ChildTable.TableName, this.ChildTable.Namespace];
			int num = this._parentKey.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			DataColumn[] array2 = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = dataTable.Columns[this.ParentKey.ColumnsReference[i].ColumnName];
				array2[i] = dataTable2.Columns[this.ChildKey.ColumnsReference[i].ColumnName];
			}
			DataRelation dataRelation = new DataRelation(this._relationName, array, array2, false);
			dataRelation.CheckMultipleNested = false;
			dataRelation.Nested = this.Nested;
			dataRelation.CheckMultipleNested = true;
			if (this._extendedProperties != null)
			{
				foreach (object key in this._extendedProperties.Keys)
				{
					dataRelation.ExtendedProperties[key] = this._extendedProperties[key];
				}
			}
			return dataRelation;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="pcevent">Parameter reference.</param>
		// Token: 0x06000B4F RID: 2895 RVA: 0x0002F4F8 File Offset: 0x0002D6F8
		protected internal void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.PropertyChanging != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelation.OnPropertyChanging|INFO> {0}", this.ObjectID);
				this.PropertyChanging(this, pcevent);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="name">Parameter reference.</param>
		// Token: 0x06000B50 RID: 2896 RVA: 0x0002F524 File Offset: 0x0002D724
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		/// <summary>Gets the <see cref="P:System.Data.DataRelation.RelationName" />, if one exists.</summary>
		/// <returns>The value of the <see cref="P:System.Data.DataRelation.RelationName" /> property.</returns>
		// Token: 0x06000B51 RID: 2897 RVA: 0x0002F532 File Offset: 0x0002D732
		public override string ToString()
		{
			return this.RelationName;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0002F53C File Offset: 0x0002D73C
		internal void ValidateMultipleNestedRelations()
		{
			if (!this.Nested || !this.CheckMultipleNested)
			{
				return;
			}
			if (this.ChildTable.NestedParentRelations.Length != 0)
			{
				DataColumn[] childColumns = this.ChildColumns;
				if (childColumns.Length != 1 || !this.IsAutoGenerated(childColumns[0]))
				{
					throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
				}
				if (!XmlTreeGen.AutoGenerated(this))
				{
					throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
				}
				foreach (object obj in this.ChildTable.Constraints)
				{
					Constraint constraint = (Constraint)obj;
					if (constraint is ForeignKeyConstraint)
					{
						if (!XmlTreeGen.AutoGenerated((ForeignKeyConstraint)constraint, true))
						{
							throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
						}
					}
					else if (!XmlTreeGen.AutoGenerated((UniqueConstraint)constraint))
					{
						throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
					}
				}
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0002F63C File Offset: 0x0002D83C
		private bool IsAutoGenerated(DataColumn col)
		{
			if (col.ColumnMapping != MappingType.Hidden)
			{
				return false;
			}
			if (col.DataType != typeof(int))
			{
				return false;
			}
			string text = col.Table.TableName + "_Id";
			if (col.ColumnName == text || col.ColumnName == text + "_0")
			{
				return true;
			}
			text = this.ParentColumnsReference[0].Table.TableName + "_Id";
			return col.ColumnName == text || col.ColumnName == text + "_0";
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002F6F1 File Offset: 0x0002D8F1
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x040007A6 RID: 1958
		private DataSet _dataSet;

		// Token: 0x040007A7 RID: 1959
		internal PropertyCollection _extendedProperties;

		// Token: 0x040007A8 RID: 1960
		internal string _relationName;

		// Token: 0x040007A9 RID: 1961
		private DataKey _childKey;

		// Token: 0x040007AA RID: 1962
		private DataKey _parentKey;

		// Token: 0x040007AB RID: 1963
		private UniqueConstraint _parentKeyConstraint;

		// Token: 0x040007AC RID: 1964
		private ForeignKeyConstraint _childKeyConstraint;

		// Token: 0x040007AD RID: 1965
		internal string[] _parentColumnNames;

		// Token: 0x040007AE RID: 1966
		internal string[] _childColumnNames;

		// Token: 0x040007AF RID: 1967
		internal string _parentTableName;

		// Token: 0x040007B0 RID: 1968
		internal string _childTableName;

		// Token: 0x040007B1 RID: 1969
		internal string _parentTableNamespace;

		// Token: 0x040007B2 RID: 1970
		internal string _childTableNamespace;

		// Token: 0x040007B3 RID: 1971
		internal bool _nested;

		// Token: 0x040007B4 RID: 1972
		internal bool _createConstraints;

		// Token: 0x040007B5 RID: 1973
		private bool _checkMultipleNested;

		// Token: 0x040007B6 RID: 1974
		private static int s_objectTypeCount;

		// Token: 0x040007B7 RID: 1975
		private readonly int _objectID;

		// Token: 0x040007B8 RID: 1976
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanging;
	}
}
