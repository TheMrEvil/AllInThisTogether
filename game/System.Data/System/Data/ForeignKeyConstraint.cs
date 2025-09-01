using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	/// <summary>Represents an action restriction enforced on a set of columns in a primary key/foreign key relationship when a value or row is either deleted or updated.</summary>
	// Token: 0x020000FB RID: 251
	[DefaultProperty("ConstraintName")]
	public class ForeignKeyConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the constraint.</param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the constraint.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.  
		///  -Or -  
		///  The tables don't belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003DA08 File Offset: 0x0003BC08
		public ForeignKeyConstraint(DataColumn parentColumn, DataColumn childColumn) : this(null, parentColumn, childColumn)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified name, parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="constraintName">The name of the constraint.</param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the constraint.</param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the constraint.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.  
		///  -Or -  
		///  The tables don't belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003DA14 File Offset: 0x0003BC14
		public ForeignKeyConstraint(string constraintName, DataColumn parentColumn, DataColumn childColumn)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			DataColumn[] parentColumns = new DataColumn[]
			{
				parentColumn
			};
			DataColumn[] childColumns = new DataColumn[]
			{
				childColumn
			};
			this.Create(constraintName, parentColumns, childColumns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> in the constraint.</param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> in the constraint.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.  
		///  -Or -  
		///  The tables don't belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003DA54 File Offset: 0x0003BC54
		public ForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns) : this(null, parentColumns, childColumns)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified name, and arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="constraintName">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. If <see langword="null" /> or empty string, a default name will be given when added to the constraints collection.</param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> in the constraint.</param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> in the constraint.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.  
		///  -Or -  
		///  The tables don't belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x0003DA5F File Offset: 0x0003BC5F
		public ForeignKeyConstraint(string constraintName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			this.Create(constraintName, parentColumns, childColumns);
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio  environment. <see cref="T:System.Data.ForeignKeyConstraint" /> objects created by using this constructor must then be added to the collection via <see cref="M:System.Data.ConstraintCollection.AddRange(System.Data.Constraint[])" />. Tables and columns with the specified names must exist at the time the method is called, or if <see cref="M:System.Data.DataTable.BeginInit" /> has been called prior to calling this constructor, the tables and columns with the specified names must exist at the time that <see cref="M:System.Data.DataTable.EndInit" /> is called.</summary>
		/// <param name="constraintName">The name of the constraint.</param>
		/// <param name="parentTableName">The name of the parent <see cref="T:System.Data.DataTable" /> that contains parent <see cref="T:System.Data.DataColumn" /> objects in the constraint.</param>
		/// <param name="parentColumnNames">An array of the names of parent <see cref="T:System.Data.DataColumn" /> objects in the constraint.</param>
		/// <param name="childColumnNames">An array of the names of child <see cref="T:System.Data.DataColumn" /> objects in the constraint.</param>
		/// <param name="acceptRejectRule">One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include <see langword="None" />, <see langword="Cascade" />, and <see langword="Default" />.</param>
		/// <param name="deleteRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is deleted. The default is <see langword="Cascade" />. Possible values include: <see langword="None" />, <see langword="Cascade" />, <see langword="SetNull" />, <see langword="SetDefault" />, and <see langword="Default" />.</param>
		/// <param name="updateRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is updated. The default is <see langword="Cascade" />. Possible values include: <see langword="None" />, <see langword="Cascade" />, <see langword="SetNull" />, <see langword="SetDefault" />, and <see langword="Default" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.  
		///  -Or -  
		///  The tables don't belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0003DA80 File Offset: 0x0003BC80
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			this._constraintName = constraintName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._acceptRejectRule = acceptRejectRule;
			this._deleteRule = deleteRule;
			this._updateRule = updateRule;
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio  environment. <see cref="T:System.Data.ForeignKeyConstraint" /> objects created by using this constructor must then be added to the collection via <see cref="M:System.Data.ConstraintCollection.AddRange(System.Data.Constraint[])" />. Tables and columns with the specified names must exist at the time the method is called, or if <see cref="M:System.Data.DataTable.BeginInit" /> has been called prior to calling this constructor, the tables and columns with the specified names must exist at the time that <see cref="M:System.Data.DataTable.EndInit" /> is called.</summary>
		/// <param name="constraintName">The name of the constraint.</param>
		/// <param name="parentTableName">The name of the parent <see cref="T:System.Data.DataTable" /> that contains parent <see cref="T:System.Data.DataColumn" /> objects in the constraint.</param>
		/// <param name="parentTableNamespace">The name of the <see cref="P:System.Data.DataTable.Namespace" />.</param>
		/// <param name="parentColumnNames">An array of the names of parent <see cref="T:System.Data.DataColumn" /> objects in the constraint.</param>
		/// <param name="childColumnNames">An array of the names of child <see cref="T:System.Data.DataColumn" /> objects in the constraint.</param>
		/// <param name="acceptRejectRule">One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include <see langword="None" />, <see langword="Cascade" />, and <see langword="Default" />.</param>
		/// <param name="deleteRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is deleted. The default is <see langword="Cascade" />. Possible values include: <see langword="None" />, <see langword="Cascade" />, <see langword="SetNull" />, <see langword="SetDefault" />, and <see langword="Default" />.</param>
		/// <param name="updateRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is updated. The default is <see langword="Cascade" />. Possible values include: <see langword="None" />, <see langword="Cascade" />, <see langword="SetNull" />, <see langword="SetDefault" />, and <see langword="Default" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is <see langword="null" />.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.  
		///  -Or -  
		///  The tables don't belong to the same <see cref="T:System.Data.DataSet" />.</exception>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x0003DAD8 File Offset: 0x0003BCD8
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string parentTableNamespace, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			this._constraintName = constraintName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._parentTableNamespace = parentTableNamespace;
			this._acceptRejectRule = acceptRejectRule;
			this._deleteRule = deleteRule;
			this._updateRule = updateRule;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0003DB36 File Offset: 0x0003BD36
		internal DataKey ChildKey
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey;
			}
		}

		/// <summary>Gets the child columns of this constraint.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the child columns of the constraint.</returns>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0003DB44 File Offset: 0x0003BD44
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey.ToArray();
			}
		}

		/// <summary>Gets the child table of this constraint.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the child table in the constraint.</returns>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0003DB57 File Offset: 0x0003BD57
		[ReadOnly(true)]
		public override DataTable Table
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey.Table;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0003DB6A File Offset: 0x0003BD6A
		internal string[] ParentColumnNames
		{
			get
			{
				return this._parentKey.GetColumnNames();
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003DB77 File Offset: 0x0003BD77
		internal string[] ChildColumnNames
		{
			get
			{
				return this._childKey.GetColumnNames();
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003DB84 File Offset: 0x0003BD84
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
			if (this.Table != constraints.Table)
			{
				throw ExceptionBuilder.ConstraintAddFailed(constraints.Table);
			}
			if (this.Table.Locale.LCID != this.RelatedTable.Locale.LCID || this.Table.CaseSensitive != this.RelatedTable.CaseSensitive)
			{
				throw ExceptionBuilder.CaseLocaleMismatch();
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00006D61 File Offset: 0x00004F61
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			return true;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003DBEC File Offset: 0x0003BDEC
		internal bool IsKeyNull(object[] values)
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

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003DC14 File Offset: 0x0003BE14
		internal override bool IsConstraintViolated()
		{
			Index sortIndex = this._childKey.GetSortIndex();
			object[] uniqueKeyValues = sortIndex.GetUniqueKeyValues();
			bool result = false;
			Index sortIndex2 = this._parentKey.GetSortIndex();
			foreach (object[] array in uniqueKeyValues)
			{
				if (!this.IsKeyNull(array) && !sortIndex2.IsKeyInIndex(array))
				{
					DataRow[] rows = sortIndex.GetRows(sortIndex.FindRecords(array));
					string rowError = SR.Format("ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.", this.ConstraintName, ExceptionBuilder.KeysToString(array));
					for (int j = 0; j < rows.Length; j++)
					{
						rows[j].RowError = rowError;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003DCC0 File Offset: 0x0003BEC0
		internal override bool CanEnableConstraint()
		{
			if (this.Table.DataSet == null || !this.Table.DataSet.EnforceConstraints)
			{
				return true;
			}
			object[] uniqueKeyValues = this._childKey.GetSortIndex().GetUniqueKeyValues();
			Index sortIndex = this._parentKey.GetSortIndex();
			foreach (object[] array in uniqueKeyValues)
			{
				if (!this.IsKeyNull(array) && !sortIndex.IsKeyInIndex(array))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003DD38 File Offset: 0x0003BF38
		internal void CascadeCommit(DataRow row)
		{
			if (row.RowState == DataRowState.Detached)
			{
				return;
			}
			if (this._acceptRejectRule == AcceptRejectRule.Cascade)
			{
				Index sortIndex = this._childKey.GetSortIndex((row.RowState == DataRowState.Deleted) ? DataViewRowState.Deleted : DataViewRowState.CurrentRows);
				object[] keyValues = row.GetKeyValues(this._parentKey, (row.RowState == DataRowState.Deleted) ? DataRowVersion.Original : DataRowVersion.Default);
				if (this.IsKeyNull(keyValues))
				{
					return;
				}
				Range range = sortIndex.FindRecords(keyValues);
				if (!range.IsNull)
				{
					foreach (DataRow dataRow in sortIndex.GetRows(range))
					{
						if (DataRowState.Detached != dataRow.RowState && !dataRow._inCascade)
						{
							dataRow.AcceptChanges();
						}
					}
				}
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003DDEC File Offset: 0x0003BFEC
		internal void CascadeDelete(DataRow row)
		{
			if (-1 == row._newRecord)
			{
				return;
			}
			object[] keyValues = row.GetKeyValues(this._parentKey, DataRowVersion.Current);
			if (this.IsKeyNull(keyValues))
			{
				return;
			}
			Index sortIndex = this._childKey.GetSortIndex();
			switch (this.DeleteRule)
			{
			case Rule.None:
				if (row.Table.DataSet.EnforceConstraints)
				{
					Range range = sortIndex.FindRecords(keyValues);
					if (!range.IsNull)
					{
						if (range.Count == 1 && sortIndex.GetRow(range.Min) == row)
						{
							return;
						}
						throw ExceptionBuilder.FailedCascadeDelete(this.ConstraintName);
					}
				}
				break;
			case Rule.Cascade:
			{
				object[] keyValues2 = row.GetKeyValues(this._parentKey, DataRowVersion.Default);
				Range range2 = sortIndex.FindRecords(keyValues2);
				if (!range2.IsNull)
				{
					foreach (DataRow dataRow in sortIndex.GetRows(range2))
					{
						if (!dataRow._inCascade)
						{
							dataRow.Table.DeleteRow(dataRow);
						}
					}
					return;
				}
				break;
			}
			case Rule.SetNull:
			{
				object[] array = new object[this._childKey.ColumnsReference.Length];
				for (int j = 0; j < this._childKey.ColumnsReference.Length; j++)
				{
					array[j] = DBNull.Value;
				}
				Range range3 = sortIndex.FindRecords(keyValues);
				if (!range3.IsNull)
				{
					DataRow[] rows2 = sortIndex.GetRows(range3);
					for (int k = 0; k < rows2.Length; k++)
					{
						if (row != rows2[k])
						{
							rows2[k].SetKeyValues(this._childKey, array);
						}
					}
					return;
				}
				break;
			}
			case Rule.SetDefault:
			{
				object[] array2 = new object[this._childKey.ColumnsReference.Length];
				for (int l = 0; l < this._childKey.ColumnsReference.Length; l++)
				{
					array2[l] = this._childKey.ColumnsReference[l].DefaultValue;
				}
				Range range4 = sortIndex.FindRecords(keyValues);
				if (!range4.IsNull)
				{
					DataRow[] rows3 = sortIndex.GetRows(range4);
					for (int m = 0; m < rows3.Length; m++)
					{
						if (row != rows3[m])
						{
							rows3[m].SetKeyValues(this._childKey, array2);
						}
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003E018 File Offset: 0x0003C218
		internal void CascadeRollback(DataRow row)
		{
			Index sortIndex = this._childKey.GetSortIndex((row.RowState == DataRowState.Deleted) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			object[] keyValues = row.GetKeyValues(this._parentKey, (row.RowState == DataRowState.Modified) ? DataRowVersion.Current : DataRowVersion.Default);
			if (this.IsKeyNull(keyValues))
			{
				return;
			}
			Range range = sortIndex.FindRecords(keyValues);
			if (this._acceptRejectRule == AcceptRejectRule.Cascade)
			{
				if (!range.IsNull)
				{
					DataRow[] rows = sortIndex.GetRows(range);
					for (int i = 0; i < rows.Length; i++)
					{
						if (!rows[i]._inCascade)
						{
							rows[i].RejectChanges();
						}
					}
					return;
				}
			}
			else if (row.RowState != DataRowState.Deleted && row.Table.DataSet.EnforceConstraints && !range.IsNull)
			{
				if (range.Count == 1 && sortIndex.GetRow(range.Min) == row)
				{
					return;
				}
				if (row.HasKeyChanged(this._parentKey))
				{
					throw ExceptionBuilder.FailedCascadeUpdate(this.ConstraintName);
				}
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0003E114 File Offset: 0x0003C314
		internal void CascadeUpdate(DataRow row)
		{
			if (-1 == row._newRecord)
			{
				return;
			}
			object[] keyValues = row.GetKeyValues(this._parentKey, DataRowVersion.Current);
			if (!this.Table.DataSet._fInReadXml && this.IsKeyNull(keyValues))
			{
				return;
			}
			Index sortIndex = this._childKey.GetSortIndex();
			switch (this.UpdateRule)
			{
			case Rule.None:
				if (row.Table.DataSet.EnforceConstraints && !sortIndex.FindRecords(keyValues).IsNull)
				{
					throw ExceptionBuilder.FailedCascadeUpdate(this.ConstraintName);
				}
				break;
			case Rule.Cascade:
			{
				Range range = sortIndex.FindRecords(keyValues);
				if (!range.IsNull)
				{
					object[] keyValues2 = row.GetKeyValues(this._parentKey, DataRowVersion.Proposed);
					DataRow[] rows = sortIndex.GetRows(range);
					for (int i = 0; i < rows.Length; i++)
					{
						rows[i].SetKeyValues(this._childKey, keyValues2);
					}
					return;
				}
				break;
			}
			case Rule.SetNull:
			{
				object[] array = new object[this._childKey.ColumnsReference.Length];
				for (int j = 0; j < this._childKey.ColumnsReference.Length; j++)
				{
					array[j] = DBNull.Value;
				}
				Range range2 = sortIndex.FindRecords(keyValues);
				if (!range2.IsNull)
				{
					DataRow[] rows2 = sortIndex.GetRows(range2);
					for (int k = 0; k < rows2.Length; k++)
					{
						rows2[k].SetKeyValues(this._childKey, array);
					}
					return;
				}
				break;
			}
			case Rule.SetDefault:
			{
				object[] array2 = new object[this._childKey.ColumnsReference.Length];
				for (int l = 0; l < this._childKey.ColumnsReference.Length; l++)
				{
					array2[l] = this._childKey.ColumnsReference[l].DefaultValue;
				}
				Range range3 = sortIndex.FindRecords(keyValues);
				if (!range3.IsNull)
				{
					DataRow[] rows3 = sortIndex.GetRows(range3);
					for (int m = 0; m < rows3.Length; m++)
					{
						rows3[m].SetKeyValues(this._childKey, array2);
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003E318 File Offset: 0x0003C518
		internal void CheckCanClearParentTable(DataTable table)
		{
			if (this.Table.DataSet.EnforceConstraints && this.Table.Rows.Count > 0)
			{
				throw ExceptionBuilder.FailedClearParentTable(table.TableName, this.ConstraintName, this.Table.TableName);
			}
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003E367 File Offset: 0x0003C567
		internal void CheckCanRemoveParentRow(DataRow row)
		{
			if (!this.Table.DataSet.EnforceConstraints)
			{
				return;
			}
			if (DataRelation.GetChildRows(this.ParentKey, this.ChildKey, row, DataRowVersion.Default).Length != 0)
			{
				throw ExceptionBuilder.RemoveParentRow(this);
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003E3A0 File Offset: 0x0003C5A0
		internal void CheckCascade(DataRow row, DataRowAction action)
		{
			if (row._inCascade)
			{
				return;
			}
			row._inCascade = true;
			try
			{
				if (action == DataRowAction.Change)
				{
					if (row.HasKeyChanged(this._parentKey))
					{
						this.CascadeUpdate(row);
					}
				}
				else if (action == DataRowAction.Delete)
				{
					this.CascadeDelete(row);
				}
				else if (action == DataRowAction.Commit)
				{
					this.CascadeCommit(row);
				}
				else if (action == DataRowAction.Rollback)
				{
					this.CascadeRollback(row);
				}
			}
			finally
			{
				row._inCascade = false;
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003E420 File Offset: 0x0003C620
		internal override void CheckConstraint(DataRow childRow, DataRowAction action)
		{
			if ((action == DataRowAction.Change || action == DataRowAction.Add || action == DataRowAction.Rollback) && this.Table.DataSet != null && this.Table.DataSet.EnforceConstraints && childRow.HasKeyChanged(this._childKey))
			{
				DataRowVersion dataRowVersion = (action == DataRowAction.Rollback) ? DataRowVersion.Original : DataRowVersion.Current;
				object[] keyValues = childRow.GetKeyValues(this._childKey);
				if (childRow.HasVersion(dataRowVersion))
				{
					DataRow parentRow = DataRelation.GetParentRow(this.ParentKey, this.ChildKey, childRow, dataRowVersion);
					if (parentRow != null && parentRow._inCascade)
					{
						object[] keyValues2 = parentRow.GetKeyValues(this._parentKey, (action == DataRowAction.Rollback) ? dataRowVersion : DataRowVersion.Default);
						int num = childRow.Table.NewRecord();
						childRow.Table.SetKeyValues(this._childKey, keyValues2, num);
						if (this._childKey.RecordsEqual(childRow._tempRecord, num))
						{
							return;
						}
					}
				}
				object[] keyValues3 = childRow.GetKeyValues(this._childKey);
				if (!this.IsKeyNull(keyValues3) && !this._parentKey.GetSortIndex().IsKeyInIndex(keyValues3))
				{
					if (this._childKey.Table == this._parentKey.Table && childRow._tempRecord != -1)
					{
						int i;
						for (i = 0; i < keyValues3.Length; i++)
						{
							DataColumn dataColumn = this._parentKey.ColumnsReference[i];
							object value = dataColumn.ConvertValue(keyValues3[i]);
							if (dataColumn.CompareValueTo(childRow._tempRecord, value) != 0)
							{
								break;
							}
						}
						if (i == keyValues3.Length)
						{
							return;
						}
					}
					throw ExceptionBuilder.ForeignKeyViolation(this.ConstraintName, keyValues);
				}
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003E5B0 File Offset: 0x0003C7B0
		private void NonVirtualCheckState()
		{
			if (this._DataSet == null)
			{
				this._parentKey.CheckState();
				this._childKey.CheckState();
				if (this._parentKey.Table.DataSet != this._childKey.Table.DataSet)
				{
					throw ExceptionBuilder.TablesInDifferentSets();
				}
				for (int i = 0; i < this._parentKey.ColumnsReference.Length; i++)
				{
					if (this._parentKey.ColumnsReference[i].DataType != this._childKey.ColumnsReference[i].DataType || (this._parentKey.ColumnsReference[i].DataType == typeof(DateTime) && this._parentKey.ColumnsReference[i].DateTimeMode != this._childKey.ColumnsReference[i].DateTimeMode && (this._parentKey.ColumnsReference[i].DateTimeMode & this._childKey.ColumnsReference[i].DateTimeMode) != DataSetDateTime.Unspecified))
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
				}
				if (this._childKey.ColumnsEqual(this._parentKey))
				{
					throw ExceptionBuilder.KeyColumnsIdentical();
				}
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0003E6DF File Offset: 0x0003C8DF
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		/// <summary>Indicates the action that should take place across this constraint when <see cref="M:System.Data.DataTable.AcceptChanges" /> is invoked.</summary>
		/// <returns>One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include <see langword="None" />, and <see langword="Cascade" />. The default is <see langword="None" />.</returns>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0003E6E7 File Offset: 0x0003C8E7
		// (set) Token: 0x06000EEE RID: 3822 RVA: 0x0003E6F5 File Offset: 0x0003C8F5
		[DefaultValue(AcceptRejectRule.None)]
		public virtual AcceptRejectRule AcceptRejectRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._acceptRejectRule;
			}
			set
			{
				if (value <= AcceptRejectRule.Cascade)
				{
					this._acceptRejectRule = value;
					return;
				}
				throw ADP.InvalidAcceptRejectRule(value);
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003E709 File Offset: 0x0003C909
		internal override bool ContainsColumn(DataColumn column)
		{
			return this._parentKey.ContainsColumn(column) || this._childKey.ContainsColumn(column);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003E727 File Offset: 0x0003C927
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003E734 File Offset: 0x0003C934
		internal override Constraint Clone(DataSet destination, bool ignorNSforTableLookup)
		{
			int num;
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.Table.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.Table.TableName, this.Table.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable = destination.Tables[num];
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.RelatedTable.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.RelatedTable.TableName, this.RelatedTable.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable2 = destination.Tables[num];
			int num2 = this.Columns.Length;
			DataColumn[] array = new DataColumn[num2];
			DataColumn[] array2 = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
				dataColumn = this.RelatedColumnsReference[i];
				num = dataTable2.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array2[i] = dataTable2.Columns[num];
			}
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(this.ConstraintName, array2, array);
			foreignKeyConstraint.UpdateRule = this.UpdateRule;
			foreignKeyConstraint.DeleteRule = this.DeleteRule;
			foreignKeyConstraint.AcceptRejectRule = this.AcceptRejectRule;
			foreach (object key in base.ExtendedProperties.Keys)
			{
				foreignKeyConstraint.ExtendedProperties[key] = base.ExtendedProperties[key];
			}
			return foreignKeyConstraint;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003E91C File Offset: 0x0003CB1C
		internal ForeignKeyConstraint Clone(DataTable destination)
		{
			int num = this.Columns.Length;
			DataColumn[] array = new DataColumn[num];
			DataColumn[] array2 = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				int num2 = destination.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = destination.Columns[num2];
				dataColumn = this.RelatedColumnsReference[i];
				num2 = destination.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array2[i] = destination.Columns[num2];
			}
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(this.ConstraintName, array2, array);
			foreignKeyConstraint.UpdateRule = this.UpdateRule;
			foreignKeyConstraint.DeleteRule = this.DeleteRule;
			foreignKeyConstraint.AcceptRejectRule = this.AcceptRejectRule;
			foreach (object key in base.ExtendedProperties.Keys)
			{
				foreignKeyConstraint.ExtendedProperties[key] = base.ExtendedProperties[key];
			}
			return foreignKeyConstraint;
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003EA5C File Offset: 0x0003CC5C
		private void Create(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			if (parentColumns.Length == 0 || childColumns.Length == 0)
			{
				throw ExceptionBuilder.KeyLengthZero();
			}
			if (parentColumns.Length != childColumns.Length)
			{
				throw ExceptionBuilder.KeyLengthMismatch();
			}
			for (int i = 0; i < parentColumns.Length; i++)
			{
				if (parentColumns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(parentColumns[i]);
				}
				if (childColumns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(childColumns[i]);
				}
			}
			this._parentKey = new DataKey(parentColumns, true);
			this._childKey = new DataKey(childColumns, true);
			this.ConstraintName = relationName;
			this.NonVirtualCheckState();
		}

		/// <summary>Gets or sets the action that occurs across this constraint when a row is deleted.</summary>
		/// <returns>One of the <see cref="T:System.Data.Rule" /> values. The default is <see langword="Cascade" />.</returns>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0003EAE0 File Offset: 0x0003CCE0
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x0003EAEE File Offset: 0x0003CCEE
		[DefaultValue(Rule.Cascade)]
		public virtual Rule DeleteRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._deleteRule;
			}
			set
			{
				if (value <= Rule.SetDefault)
				{
					this._deleteRule = value;
					return;
				}
				throw ADP.InvalidRule(value);
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.ForeignKeyConstraint" /> is identical to the specified object.</summary>
		/// <param name="key">The object to which this <see cref="T:System.Data.ForeignKeyConstraint" /> is compared. Two <see cref="T:System.Data.ForeignKeyConstraint" /> are equal if they constrain the same columns.</param>
		/// <returns>
		///   <see langword="true" />, if the objects are identical; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003EB04 File Offset: 0x0003CD04
		public override bool Equals(object key)
		{
			if (!(key is ForeignKeyConstraint))
			{
				return false;
			}
			ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)key;
			return this.ParentKey.ColumnsEqual(foreignKeyConstraint.ParentKey) && this.ChildKey.ColumnsEqual(foreignKeyConstraint.ChildKey);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003EB4E File Offset: 0x0003CD4E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>The parent columns of this constraint.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of the constraint.</returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0003EB56 File Offset: 0x0003CD56
		[ReadOnly(true)]
		public virtual DataColumn[] RelatedColumns
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.ToArray();
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x0003EB69 File Offset: 0x0003CD69
		internal DataColumn[] RelatedColumnsReference
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.ColumnsReference;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0003EB7C File Offset: 0x0003CD7C
		internal DataKey ParentKey
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey;
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003EB8C File Offset: 0x0003CD8C
		internal DataRelation FindParentRelation()
		{
			DataRelationCollection parentRelations = this.Table.ParentRelations;
			for (int i = 0; i < parentRelations.Count; i++)
			{
				if (parentRelations[i].ChildKeyConstraint == this)
				{
					return parentRelations[i];
				}
			}
			return null;
		}

		/// <summary>Gets the parent table of this constraint.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataTable" /> of this constraint.</returns>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0003EBCE File Offset: 0x0003CDCE
		[ReadOnly(true)]
		public virtual DataTable RelatedTable
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.Table;
			}
		}

		/// <summary>Gets or sets the action that occurs across this constraint on when a row is updated.</summary>
		/// <returns>One of the <see cref="T:System.Data.Rule" /> values. The default is <see langword="Cascade" />.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0003EBE1 File Offset: 0x0003CDE1
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0003EBEF File Offset: 0x0003CDEF
		[DefaultValue(Rule.Cascade)]
		public virtual Rule UpdateRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._updateRule;
			}
			set
			{
				if (value <= Rule.SetDefault)
				{
					this._updateRule = value;
					return;
				}
				throw ADP.InvalidRule(value);
			}
		}

		// Token: 0x04000960 RID: 2400
		internal const Rule Rule_Default = Rule.Cascade;

		// Token: 0x04000961 RID: 2401
		internal const AcceptRejectRule AcceptRejectRule_Default = AcceptRejectRule.None;

		// Token: 0x04000962 RID: 2402
		internal Rule _deleteRule;

		// Token: 0x04000963 RID: 2403
		internal Rule _updateRule;

		// Token: 0x04000964 RID: 2404
		internal AcceptRejectRule _acceptRejectRule;

		// Token: 0x04000965 RID: 2405
		private DataKey _childKey;

		// Token: 0x04000966 RID: 2406
		private DataKey _parentKey;

		// Token: 0x04000967 RID: 2407
		internal string _constraintName;

		// Token: 0x04000968 RID: 2408
		internal string[] _parentColumnNames;

		// Token: 0x04000969 RID: 2409
		internal string[] _childColumnNames;

		// Token: 0x0400096A RID: 2410
		internal string _parentTableName;

		// Token: 0x0400096B RID: 2411
		internal string _parentTableNamespace;
	}
}
