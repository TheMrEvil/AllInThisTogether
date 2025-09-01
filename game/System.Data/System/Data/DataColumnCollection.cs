using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity;

namespace System.Data
{
	/// <summary>Represents a collection of <see cref="T:System.Data.DataColumn" /> objects for a <see cref="T:System.Data.DataTable" />.</summary>
	// Token: 0x020000B3 RID: 179
	[DefaultEvent("CollectionChanged")]
	public sealed class DataColumnCollection : InternalDataCollectionBase
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002CB30 File Offset: 0x0002AD30
		internal DataColumnCollection(DataTable table)
		{
			this._list = new ArrayList();
			this._defaultNameIndex = 1;
			this._columnsImplementingIChangeTracking = Array.Empty<DataColumn>();
			base..ctor();
			this._table = table;
			this._columnFromName = new Dictionary<string, DataColumn>();
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0002CB67 File Offset: 0x0002AD67
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0002CB6F File Offset: 0x0002AD6F
		internal DataColumn[] ColumnsImplementingIChangeTracking
		{
			get
			{
				return this._columnsImplementingIChangeTracking;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0002CB77 File Offset: 0x0002AD77
		internal int ColumnsImplementingIChangeTrackingCount
		{
			get
			{
				return this._nColumnsImplementingIChangeTracking;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002CB7F File Offset: 0x0002AD7F
		internal int ColumnsImplementingIRevertibleChangeTrackingCount
		{
			get
			{
				return this._nColumnsImplementingIRevertibleChangeTracking;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> from the collection at the specified index.</summary>
		/// <param name="index">The zero-based index of the column to return.</param>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> at the specified index.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection.</exception>
		// Token: 0x170001CB RID: 459
		public DataColumn this[int index]
		{
			get
			{
				DataColumn result;
				try
				{
					result = (DataColumn)this._list[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw ExceptionBuilder.ColumnOutOfRange(index);
				}
				return result;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> from the collection with the specified name.</summary>
		/// <param name="name">The <see cref="P:System.Data.DataColumn.ColumnName" /> of the column to return.</param>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> in the collection with the specified <see cref="P:System.Data.DataColumn.ColumnName" />; otherwise a null value if the <see cref="T:System.Data.DataColumn" /> does not exist.</returns>
		// Token: 0x170001CC RID: 460
		public DataColumn this[string name]
		{
			get
			{
				if (name == null)
				{
					throw ExceptionBuilder.ArgumentNull("name");
				}
				DataColumn dataColumn;
				if (!this._columnFromName.TryGetValue(name, out dataColumn) || dataColumn == null)
				{
					int num = this.IndexOfCaseInsensitive(name);
					if (0 <= num)
					{
						dataColumn = (DataColumn)this._list[num];
					}
					else if (-2 == num)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
				}
				return dataColumn;
			}
		}

		// Token: 0x170001CD RID: 461
		internal DataColumn this[string name, string ns]
		{
			get
			{
				DataColumn dataColumn;
				if (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null && dataColumn.Namespace == ns)
				{
					return dataColumn;
				}
				return null;
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0002CC51 File Offset: 0x0002AE51
		internal void EnsureAdditionalCapacity(int capacity)
		{
			if (this._list.Capacity < capacity + this._list.Count)
			{
				this._list.Capacity = capacity + this._list.Count;
			}
		}

		/// <summary>Creates and adds the specified <see cref="T:System.Data.DataColumn" /> object to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The column already belongs to this collection, or to another collection.</exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.)</exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions.</exception>
		// Token: 0x06000ACC RID: 2764 RVA: 0x0002CC85 File Offset: 0x0002AE85
		public void Add(DataColumn column)
		{
			this.AddAt(-1, column);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0002CC90 File Offset: 0x0002AE90
		internal void AddAt(int index, DataColumn column)
		{
			if (column != null && column.ColumnMapping == MappingType.SimpleContent)
			{
				if (this._table.XmlText != null && this._table.XmlText != column)
				{
					throw ExceptionBuilder.CannotAddColumn3();
				}
				if (this._table.ElementColumnCount > 0)
				{
					throw ExceptionBuilder.CannotAddColumn4(column.ColumnName);
				}
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
				this.BaseAdd(column);
				if (index != -1)
				{
					this.ArrayAdd(index, column);
				}
				else
				{
					this.ArrayAdd(column);
				}
				this._table.XmlText = column;
			}
			else
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
				this.BaseAdd(column);
				if (index != -1)
				{
					this.ArrayAdd(index, column);
				}
				else
				{
					this.ArrayAdd(column);
				}
				if (column.ColumnMapping == MappingType.Element)
				{
					DataTable table = this._table;
					int elementColumnCount = table.ElementColumnCount;
					table.ElementColumnCount = elementColumnCount + 1;
				}
			}
			if (!this._table.fInitInProgress && column != null && column.Computed)
			{
				column.Expression = column.Expression;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataColumn" /> array to the end of the collection.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to add to the collection.</param>
		// Token: 0x06000ACE RID: 2766 RVA: 0x0002CD94 File Offset: 0x0002AF94
		public void AddRange(DataColumn[] columns)
		{
			if (this._table.fInitInProgress)
			{
				this._delayedAddRangeColumns = columns;
				return;
			}
			if (columns != null)
			{
				foreach (DataColumn dataColumn in columns)
				{
					if (dataColumn != null)
					{
						this.Add(dataColumn);
					}
				}
			}
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name, type, and expression to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <param name="columnName">The name to use when you create the column.</param>
		/// <param name="type">The <see cref="P:System.Data.DataColumn.DataType" /> of the new column.</param>
		/// <param name="expression">The expression to assign to the <see cref="P:System.Data.DataColumn.Expression" /> property.</param>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.)</exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions.</exception>
		// Token: 0x06000ACF RID: 2767 RVA: 0x0002CDD8 File Offset: 0x0002AFD8
		public DataColumn Add(string columnName, Type type, string expression)
		{
			DataColumn dataColumn = new DataColumn(columnName, type, expression);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name and type to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <param name="columnName">The <see cref="P:System.Data.DataColumn.ColumnName" /> to use when you create the column.</param>
		/// <param name="type">The <see cref="P:System.Data.DataColumn.DataType" /> of the new column.</param>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.)</exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions.</exception>
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002CDF8 File Offset: 0x0002AFF8
		public DataColumn Add(string columnName, Type type)
		{
			DataColumn dataColumn = new DataColumn(columnName, type);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <param name="columnName">The name of the column.</param>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.)</exception>
		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002CE18 File Offset: 0x0002B018
		public DataColumn Add(string columnName)
		{
			DataColumn dataColumn = new DataColumn(columnName);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		// Token: 0x06000AD2 RID: 2770 RVA: 0x0002CE34 File Offset: 0x0002B034
		public DataColumn Add()
		{
			DataColumn dataColumn = new DataColumn();
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Occurs when the columns collection changes, either by adding or removing a column.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000AD3 RID: 2771 RVA: 0x0002CE50 File Offset: 0x0002B050
		// (remove) Token: 0x06000AD4 RID: 2772 RVA: 0x0002CE88 File Offset: 0x0002B088
		public event CollectionChangeEventHandler CollectionChanged
		{
			[CompilerGenerated]
			add
			{
				CollectionChangeEventHandler collectionChangeEventHandler = this.CollectionChanged;
				CollectionChangeEventHandler collectionChangeEventHandler2;
				do
				{
					collectionChangeEventHandler2 = collectionChangeEventHandler;
					CollectionChangeEventHandler value2 = (CollectionChangeEventHandler)Delegate.Combine(collectionChangeEventHandler2, value);
					collectionChangeEventHandler = Interlocked.CompareExchange<CollectionChangeEventHandler>(ref this.CollectionChanged, value2, collectionChangeEventHandler2);
				}
				while (collectionChangeEventHandler != collectionChangeEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				CollectionChangeEventHandler collectionChangeEventHandler = this.CollectionChanged;
				CollectionChangeEventHandler collectionChangeEventHandler2;
				do
				{
					collectionChangeEventHandler2 = collectionChangeEventHandler;
					CollectionChangeEventHandler value2 = (CollectionChangeEventHandler)Delegate.Remove(collectionChangeEventHandler2, value);
					collectionChangeEventHandler = Interlocked.CompareExchange<CollectionChangeEventHandler>(ref this.CollectionChanged, value2, collectionChangeEventHandler2);
				}
				while (collectionChangeEventHandler != collectionChangeEventHandler2);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000AD5 RID: 2773 RVA: 0x0002CEC0 File Offset: 0x0002B0C0
		// (remove) Token: 0x06000AD6 RID: 2774 RVA: 0x0002CEF8 File Offset: 0x0002B0F8
		internal event CollectionChangeEventHandler CollectionChanging
		{
			[CompilerGenerated]
			add
			{
				CollectionChangeEventHandler collectionChangeEventHandler = this.CollectionChanging;
				CollectionChangeEventHandler collectionChangeEventHandler2;
				do
				{
					collectionChangeEventHandler2 = collectionChangeEventHandler;
					CollectionChangeEventHandler value2 = (CollectionChangeEventHandler)Delegate.Combine(collectionChangeEventHandler2, value);
					collectionChangeEventHandler = Interlocked.CompareExchange<CollectionChangeEventHandler>(ref this.CollectionChanging, value2, collectionChangeEventHandler2);
				}
				while (collectionChangeEventHandler != collectionChangeEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				CollectionChangeEventHandler collectionChangeEventHandler = this.CollectionChanging;
				CollectionChangeEventHandler collectionChangeEventHandler2;
				do
				{
					collectionChangeEventHandler2 = collectionChangeEventHandler;
					CollectionChangeEventHandler value2 = (CollectionChangeEventHandler)Delegate.Remove(collectionChangeEventHandler2, value);
					collectionChangeEventHandler = Interlocked.CompareExchange<CollectionChangeEventHandler>(ref this.CollectionChanging, value2, collectionChangeEventHandler2);
				}
				while (collectionChangeEventHandler != collectionChangeEventHandler2);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000AD7 RID: 2775 RVA: 0x0002CF30 File Offset: 0x0002B130
		// (remove) Token: 0x06000AD8 RID: 2776 RVA: 0x0002CF68 File Offset: 0x0002B168
		internal event CollectionChangeEventHandler ColumnPropertyChanged
		{
			[CompilerGenerated]
			add
			{
				CollectionChangeEventHandler collectionChangeEventHandler = this.ColumnPropertyChanged;
				CollectionChangeEventHandler collectionChangeEventHandler2;
				do
				{
					collectionChangeEventHandler2 = collectionChangeEventHandler;
					CollectionChangeEventHandler value2 = (CollectionChangeEventHandler)Delegate.Combine(collectionChangeEventHandler2, value);
					collectionChangeEventHandler = Interlocked.CompareExchange<CollectionChangeEventHandler>(ref this.ColumnPropertyChanged, value2, collectionChangeEventHandler2);
				}
				while (collectionChangeEventHandler != collectionChangeEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				CollectionChangeEventHandler collectionChangeEventHandler = this.ColumnPropertyChanged;
				CollectionChangeEventHandler collectionChangeEventHandler2;
				do
				{
					collectionChangeEventHandler2 = collectionChangeEventHandler;
					CollectionChangeEventHandler value2 = (CollectionChangeEventHandler)Delegate.Remove(collectionChangeEventHandler2, value);
					collectionChangeEventHandler = Interlocked.CompareExchange<CollectionChangeEventHandler>(ref this.ColumnPropertyChanged, value2, collectionChangeEventHandler2);
				}
				while (collectionChangeEventHandler != collectionChangeEventHandler2);
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002CF9D File Offset: 0x0002B19D
		private void ArrayAdd(DataColumn column)
		{
			this._list.Add(column);
			column.SetOrdinalInternal(this._list.Count - 1);
			this.CheckIChangeTracking(column);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0002CFC6 File Offset: 0x0002B1C6
		private void ArrayAdd(int index, DataColumn column)
		{
			this._list.Insert(index, column);
			this.CheckIChangeTracking(column);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0002CFDC File Offset: 0x0002B1DC
		private void ArrayRemove(DataColumn column)
		{
			column.SetOrdinalInternal(-1);
			this._list.Remove(column);
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				((DataColumn)this._list[i]).SetOrdinalInternal(i);
			}
			if (column.ImplementsIChangeTracking)
			{
				this.RemoveColumnsImplementingIChangeTrackingList(column);
			}
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0002D03C File Offset: 0x0002B23C
		internal string AssignName()
		{
			int defaultNameIndex = this._defaultNameIndex;
			this._defaultNameIndex = defaultNameIndex + 1;
			string text = this.MakeName(defaultNameIndex);
			while (this._columnFromName.ContainsKey(text))
			{
				defaultNameIndex = this._defaultNameIndex;
				this._defaultNameIndex = defaultNameIndex + 1;
				text = this.MakeName(defaultNameIndex);
			}
			return text;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0002D08C File Offset: 0x0002B28C
		private void BaseAdd(DataColumn column)
		{
			if (column == null)
			{
				throw ExceptionBuilder.ArgumentNull("column");
			}
			if (column._table == this._table)
			{
				throw ExceptionBuilder.CannotAddColumn1(column.ColumnName);
			}
			if (column._table != null)
			{
				throw ExceptionBuilder.CannotAddColumn2(column.ColumnName);
			}
			if (column.ColumnName.Length == 0)
			{
				column.ColumnName = this.AssignName();
			}
			this.RegisterColumnName(column.ColumnName, column);
			try
			{
				column.SetTable(this._table);
				if (!this._table.fInitInProgress && column.Computed && column.DataExpression.DependsOn(column))
				{
					throw ExceptionBuilder.ExpressionCircular();
				}
				if (0 < this._table.RecordCapacity)
				{
					column.SetCapacity(this._table.RecordCapacity);
				}
				for (int i = 0; i < this._table.RecordCapacity; i++)
				{
					column.InitializeRecord(i);
				}
				if (this._table.DataSet != null)
				{
					column.OnSetDataSet();
				}
			}
			catch (Exception e) when (ADP.IsCatchableOrSecurityExceptionType(e))
			{
				this.UnregisterName(column.ColumnName);
				throw;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002D1B8 File Offset: 0x0002B3B8
		private void BaseGroupSwitch(DataColumn[] oldArray, int oldLength, DataColumn[] newArray, int newLength)
		{
			int num = 0;
			for (int i = 0; i < oldLength; i++)
			{
				bool flag = false;
				for (int j = num; j < newLength; j++)
				{
					if (oldArray[i] == newArray[j])
					{
						if (num == j)
						{
							num++;
						}
						flag = true;
						break;
					}
				}
				if (!flag && oldArray[i].Table == this._table)
				{
					this.BaseRemove(oldArray[i]);
					this._list.Remove(oldArray[i]);
					oldArray[i].SetOrdinalInternal(-1);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (newArray[k].Table != this._table)
				{
					this.BaseAdd(newArray[k]);
					this._list.Add(newArray[k]);
				}
				newArray[k].SetOrdinalInternal(k);
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002D270 File Offset: 0x0002B470
		private void BaseRemove(DataColumn column)
		{
			if (this.CanRemove(column, true))
			{
				if (column._errors > 0)
				{
					for (int i = 0; i < this._table.Rows.Count; i++)
					{
						this._table.Rows[i].ClearError(column);
					}
				}
				this.UnregisterName(column.ColumnName);
				column.SetTable(null);
			}
		}

		/// <summary>Checks whether a specific column can be removed from the collection.</summary>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the column can be removed. <see langword="false" /> if,  
		///
		/// The <paramref name="column" /> parameter is <see langword="null" />.  
		///
		/// The column does not belong to this collection.  
		///
		/// The column is part of a relationship.  
		///
		/// Another column's expression depends on this column.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this collection.
		/// -or-
		/// The column is part of a relationship.
		/// -or-
		/// Another column's expression depends on this column.</exception>
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002D2D5 File Offset: 0x0002B4D5
		public bool CanRemove(DataColumn column)
		{
			return this.CanRemove(column, false);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002D2E0 File Offset: 0x0002B4E0
		internal bool CanRemove(DataColumn column, bool fThrowException)
		{
			if (column == null)
			{
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.ArgumentNull("column");
			}
			else if (column._table != this._table)
			{
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.CannotRemoveColumn();
			}
			else
			{
				this._table.OnRemoveColumnInternal(column);
				if (this._table._primaryKey == null || !this._table._primaryKey.Key.ContainsColumn(column))
				{
					int i = 0;
					while (i < this._table.ParentRelations.Count)
					{
						if (this._table.ParentRelations[i].ChildKey.ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveChildKey(this._table.ParentRelations[i].RelationName);
						}
						else
						{
							i++;
						}
					}
					int j = 0;
					while (j < this._table.ChildRelations.Count)
					{
						if (this._table.ChildRelations[j].ParentKey.ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveChildKey(this._table.ChildRelations[j].RelationName);
						}
						else
						{
							j++;
						}
					}
					int k = 0;
					while (k < this._table.Constraints.Count)
					{
						if (this._table.Constraints[k].ContainsColumn(column))
						{
							if (!fThrowException)
							{
								return false;
							}
							throw ExceptionBuilder.CannotRemoveConstraint(this._table.Constraints[k].ConstraintName, this._table.Constraints[k].Table.TableName);
						}
						else
						{
							k++;
						}
					}
					if (this._table.DataSet != null)
					{
						ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._table.DataSet, this._table);
						while (parentForeignKeyConstraintEnumerator.GetNext())
						{
							Constraint constraint = parentForeignKeyConstraintEnumerator.GetConstraint();
							if (((ForeignKeyConstraint)constraint).ParentKey.ContainsColumn(column))
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.CannotRemoveConstraint(constraint.ConstraintName, constraint.Table.TableName);
							}
						}
					}
					if (column._dependentColumns != null)
					{
						for (int l = 0; l < column._dependentColumns.Count; l++)
						{
							DataColumn dataColumn = column._dependentColumns[l];
							if ((!this._fInClear || (dataColumn.Table != this._table && dataColumn.Table != null)) && dataColumn.Table != null)
							{
								DataExpression dataExpression = dataColumn.DataExpression;
								if (dataExpression != null && dataExpression.DependsOn(column))
								{
									if (!fThrowException)
									{
										return false;
									}
									throw ExceptionBuilder.CannotRemoveExpression(dataColumn.ColumnName, dataColumn.Expression);
								}
							}
						}
					}
					foreach (Index index in this._table.LiveIndexes)
					{
					}
					return true;
				}
				if (!fThrowException)
				{
					return false;
				}
				throw ExceptionBuilder.CannotRemovePrimaryKey();
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002D5C4 File Offset: 0x0002B7C4
		private void CheckIChangeTracking(DataColumn column)
		{
			if (column.ImplementsIRevertibleChangeTracking)
			{
				this._nColumnsImplementingIRevertibleChangeTracking++;
				this._nColumnsImplementingIChangeTracking++;
				this.AddColumnsImplementingIChangeTrackingList(column);
				return;
			}
			if (column.ImplementsIChangeTracking)
			{
				this._nColumnsImplementingIChangeTracking++;
				this.AddColumnsImplementingIChangeTrackingList(column);
			}
		}

		/// <summary>Clears the collection of any columns.</summary>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002D61C File Offset: 0x0002B81C
		public void Clear()
		{
			int count = this._list.Count;
			DataColumn[] array = new DataColumn[this._list.Count];
			this._list.CopyTo(array, 0);
			this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
			if (this._table.fInitInProgress && this._delayedAddRangeColumns != null)
			{
				this._delayedAddRangeColumns = null;
			}
			try
			{
				this._fInClear = true;
				this.BaseGroupSwitch(array, count, null, 0);
				this._fInClear = false;
			}
			catch (Exception e) when (ADP.IsCatchableOrSecurityExceptionType(e))
			{
				this._fInClear = false;
				this.BaseGroupSwitch(null, 0, array, count);
				this._list.Clear();
				for (int i = 0; i < count; i++)
				{
					this._list.Add(array[i]);
				}
				throw;
			}
			this._list.Clear();
			this._table.ElementColumnCount = 0;
			this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
		}

		/// <summary>Checks whether the collection contains a column with the specified name.</summary>
		/// <param name="name">The <see cref="P:System.Data.DataColumn.ColumnName" /> of the column to look for.</param>
		/// <returns>
		///   <see langword="true" /> if a column exists with this name; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002D718 File Offset: 0x0002B918
		public bool Contains(string name)
		{
			DataColumn dataColumn;
			return (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null) || this.IndexOfCaseInsensitive(name) >= 0;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002D748 File Offset: 0x0002B948
		internal bool Contains(string name, bool caseSensitive)
		{
			DataColumn dataColumn;
			return (this._columnFromName.TryGetValue(name, out dataColumn) && dataColumn != null) || (!caseSensitive && this.IndexOfCaseInsensitive(name) >= 0);
		}

		/// <summary>Copies the entire collection into an existing array, starting at a specified index within the array.</summary>
		/// <param name="array">An array of <see cref="T:System.Data.DataColumn" /> objects to copy the collection into.</param>
		/// <param name="index">The index to start from.</param>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002D77C File Offset: 0x0002B97C
		public void CopyTo(DataColumn[] array, int index)
		{
			if (array == null)
			{
				throw ExceptionBuilder.ArgumentNull("array");
			}
			if (index < 0)
			{
				throw ExceptionBuilder.ArgumentOutOfRange("index");
			}
			if (array.Length - index < this._list.Count)
			{
				throw ExceptionBuilder.InvalidOffsetLength();
			}
			for (int i = 0; i < this._list.Count; i++)
			{
				array[index + i] = (DataColumn)this._list[i];
			}
		}

		/// <summary>Gets the index of a column specified by name.</summary>
		/// <param name="column">The name of the column to return.</param>
		/// <returns>The index of the column specified by <paramref name="column" /> if it is found; otherwise, -1.</returns>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002D7EC File Offset: 0x0002B9EC
		public int IndexOf(DataColumn column)
		{
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				if (column == (DataColumn)this._list[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index of the column with the specific name (the name is not case sensitive).</summary>
		/// <param name="columnName">The name of the column to find.</param>
		/// <returns>The zero-based index of the column with the specified name, or -1 if the column does not exist in the collection.</returns>
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002D828 File Offset: 0x0002BA28
		public int IndexOf(string columnName)
		{
			if (columnName != null && 0 < columnName.Length)
			{
				int count = this.Count;
				DataColumn dataColumn;
				if (this._columnFromName.TryGetValue(columnName, out dataColumn) && dataColumn != null)
				{
					for (int i = 0; i < count; i++)
					{
						if (dataColumn == this._list[i])
						{
							return i;
						}
					}
				}
				else
				{
					int num = this.IndexOfCaseInsensitive(columnName);
					if (num >= 0)
					{
						return num;
					}
					return -1;
				}
			}
			return -1;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002D88C File Offset: 0x0002BA8C
		internal int IndexOfCaseInsensitive(string name)
		{
			int specialHashCode = this._table.GetSpecialHashCode(name);
			int num = -1;
			for (int i = 0; i < this.Count; i++)
			{
				DataColumn dataColumn = (DataColumn)this._list[i];
				if ((specialHashCode == 0 || dataColumn._hashCode == 0 || dataColumn._hashCode == specialHashCode) && base.NamesEqual(dataColumn.ColumnName, name, false, this._table.Locale) != 0)
				{
					if (num != -1)
					{
						return -2;
					}
					num = i;
				}
			}
			return num;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002D908 File Offset: 0x0002BB08
		internal void FinishInitCollection()
		{
			if (this._delayedAddRangeColumns != null)
			{
				foreach (DataColumn dataColumn in this._delayedAddRangeColumns)
				{
					if (dataColumn != null)
					{
						this.Add(dataColumn);
					}
				}
				foreach (DataColumn dataColumn2 in this._delayedAddRangeColumns)
				{
					if (dataColumn2 != null)
					{
						dataColumn2.FinishInitInProgress();
					}
				}
				this._delayedAddRangeColumns = null;
			}
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002D969 File Offset: 0x0002BB69
		private string MakeName(int index)
		{
			if (index != 1)
			{
				return "Column" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Column1";
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002D98C File Offset: 0x0002BB8C
		internal void MoveTo(DataColumn column, int newPosition)
		{
			if (0 > newPosition || newPosition > this.Count - 1)
			{
				throw ExceptionBuilder.InvalidOrdinal("ordinal", newPosition);
			}
			if (column.ImplementsIChangeTracking)
			{
				this.RemoveColumnsImplementingIChangeTrackingList(column);
			}
			this._list.Remove(column);
			this._list.Insert(newPosition, column);
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				((DataColumn)this._list[i]).SetOrdinalInternal(i);
			}
			this.CheckIChangeTracking(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, column));
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002DA20 File Offset: 0x0002BC20
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			this._table.UpdatePropertyDescriptorCollectionCache();
			if (ccevent != null && !this._table.SchemaLoading && !this._table.fInitInProgress)
			{
				DataColumn dataColumn = (DataColumn)ccevent.Element;
			}
			CollectionChangeEventHandler collectionChanged = this.CollectionChanged;
			if (collectionChanged == null)
			{
				return;
			}
			collectionChanged(this, ccevent);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002DA73 File Offset: 0x0002BC73
		private void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			CollectionChangeEventHandler collectionChanging = this.CollectionChanging;
			if (collectionChanging == null)
			{
				return;
			}
			collectionChanging(this, ccevent);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002DA87 File Offset: 0x0002BC87
		internal void OnColumnPropertyChanged(CollectionChangeEventArgs ccevent)
		{
			this._table.UpdatePropertyDescriptorCollectionCache();
			CollectionChangeEventHandler columnPropertyChanged = this.ColumnPropertyChanged;
			if (columnPropertyChanged == null)
			{
				return;
			}
			columnPropertyChanged(this, ccevent);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002DAA8 File Offset: 0x0002BCA8
		internal void RegisterColumnName(string name, DataColumn column)
		{
			try
			{
				this._columnFromName.Add(name, column);
				if (column != null)
				{
					column._hashCode = this._table.GetSpecialHashCode(name);
				}
			}
			catch (ArgumentException)
			{
				if (this._columnFromName[name] == null)
				{
					throw ExceptionBuilder.CannotAddDuplicate2(name);
				}
				if (column != null)
				{
					throw ExceptionBuilder.CannotAddDuplicate(name);
				}
				throw ExceptionBuilder.CannotAddDuplicate3(name);
			}
			if (column == null && base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex++;
				}
				while (this.Contains(this.MakeName(this._defaultNameIndex)));
			}
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002DB58 File Offset: 0x0002BD58
		internal bool CanRegisterName(string name)
		{
			return !this._columnFromName.ContainsKey(name);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataColumn" /> object from the collection.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this collection.  
		///  -Or-  
		///  The column is part of a relationship.  
		///  -Or-  
		///  Another column's expression depends on this column.</exception>
		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002DB6C File Offset: 0x0002BD6C
		public void Remove(DataColumn column)
		{
			this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			this.BaseRemove(column);
			this.ArrayRemove(column);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
			if (column.ColumnMapping == MappingType.Element)
			{
				DataTable table = this._table;
				int elementColumnCount = table.ElementColumnCount;
				table.ElementColumnCount = elementColumnCount - 1;
			}
		}

		/// <summary>Removes the column at the specified index from the collection.</summary>
		/// <param name="index">The index of the column to remove.</param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a column at the specified index.</exception>
		// Token: 0x06000AF3 RID: 2803 RVA: 0x0002DBC0 File Offset: 0x0002BDC0
		public void RemoveAt(int index)
		{
			DataColumn dataColumn = this[index];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnOutOfRange(index);
			}
			this.Remove(dataColumn);
		}

		/// <summary>Removes the <see cref="T:System.Data.DataColumn" /> object that has the specified name from the collection.</summary>
		/// <param name="name">The name of the column to remove.</param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a column with the specified name.</exception>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002DBE8 File Offset: 0x0002BDE8
		public void Remove(string name)
		{
			DataColumn dataColumn = this[name];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnNotInTheTable(name, this._table.TableName);
			}
			this.Remove(dataColumn);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002DC1C File Offset: 0x0002BE1C
		internal void UnregisterName(string name)
		{
			this._columnFromName.Remove(name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002DC88 File Offset: 0x0002BE88
		private void AddColumnsImplementingIChangeTrackingList(DataColumn dataColumn)
		{
			DataColumn[] columnsImplementingIChangeTracking = this._columnsImplementingIChangeTracking;
			DataColumn[] array = new DataColumn[columnsImplementingIChangeTracking.Length + 1];
			columnsImplementingIChangeTracking.CopyTo(array, 0);
			array[columnsImplementingIChangeTracking.Length] = dataColumn;
			this._columnsImplementingIChangeTracking = array;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002DCBC File Offset: 0x0002BEBC
		private void RemoveColumnsImplementingIChangeTrackingList(DataColumn dataColumn)
		{
			DataColumn[] columnsImplementingIChangeTracking = this._columnsImplementingIChangeTracking;
			DataColumn[] array = new DataColumn[columnsImplementingIChangeTracking.Length - 1];
			int i = 0;
			int num = 0;
			while (i < columnsImplementingIChangeTracking.Length)
			{
				if (columnsImplementingIChangeTracking[i] != dataColumn)
				{
					array[num++] = columnsImplementingIChangeTracking[i];
				}
				i++;
			}
			this._columnsImplementingIChangeTracking = array;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal DataColumnCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000791 RID: 1937
		private readonly DataTable _table;

		// Token: 0x04000792 RID: 1938
		private readonly ArrayList _list;

		// Token: 0x04000793 RID: 1939
		private int _defaultNameIndex;

		// Token: 0x04000794 RID: 1940
		private DataColumn[] _delayedAddRangeColumns;

		// Token: 0x04000795 RID: 1941
		private readonly Dictionary<string, DataColumn> _columnFromName;

		// Token: 0x04000796 RID: 1942
		private bool _fInClear;

		// Token: 0x04000797 RID: 1943
		private DataColumn[] _columnsImplementingIChangeTracking;

		// Token: 0x04000798 RID: 1944
		private int _nColumnsImplementingIChangeTracking;

		// Token: 0x04000799 RID: 1945
		private int _nColumnsImplementingIRevertibleChangeTracking;

		// Token: 0x0400079A RID: 1946
		[CompilerGenerated]
		private CollectionChangeEventHandler CollectionChanged;

		// Token: 0x0400079B RID: 1947
		[CompilerGenerated]
		private CollectionChangeEventHandler CollectionChanging;

		// Token: 0x0400079C RID: 1948
		[CompilerGenerated]
		private CollectionChangeEventHandler ColumnPropertyChanged;
	}
}
