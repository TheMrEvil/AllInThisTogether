using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using Unity;

namespace System.Data
{
	/// <summary>Represents the collection of tables for the <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x020000CE RID: 206
	[ListBindable(false)]
	[DefaultEvent("CollectionChanged")]
	public sealed class DataTableCollection : InternalDataCollectionBase
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x00032848 File Offset: 0x00030A48
		internal DataTableCollection(DataSet dataSet)
		{
			this._list = new ArrayList();
			this._defaultNameIndex = 1;
			this._objectID = Interlocked.Increment(ref DataTableCollection.s_objectTypeCount);
			base..ctor();
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataTableCollection.DataTableCollection|INFO> {0}, dataSet={1}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			this._dataSet = dataSet;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x000328A5 File Offset: 0x00030AA5
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x000328AD File Offset: 0x00030AAD
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified index; otherwise <see langword="null" /> if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection.</exception>
		// Token: 0x1700021D RID: 541
		public DataTable this[int index]
		{
			get
			{
				DataTable result;
				try
				{
					result = (DataTable)this._list[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw ExceptionBuilder.TableOutOfRange(index);
				}
				return result;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object with the specified name.</summary>
		/// <param name="name">The name of the <see langword="DataTable" /> to find.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified name; otherwise <see langword="null" /> if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		// Token: 0x1700021E RID: 542
		public DataTable this[string name]
		{
			get
			{
				int num = this.InternalIndexOf(name);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num == -3)
				{
					throw ExceptionBuilder.NamespaceNameConflict(name);
				}
				if (num >= 0)
				{
					return (DataTable)this._list[num];
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object with the specified name in the specified namespace.</summary>
		/// <param name="name">The name of the <see langword="DataTable" /> to find.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified name; otherwise <see langword="null" /> if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		// Token: 0x1700021F RID: 543
		public DataTable this[string name, string tableNamespace]
		{
			get
			{
				if (tableNamespace == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableNamespace");
				}
				int num = this.InternalIndexOf(name, tableNamespace);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num >= 0)
				{
					return (DataTable)this._list[num];
				}
				return null;
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00032980 File Offset: 0x00030B80
		internal DataTable GetTable(string name, string ns)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (dataTable.TableName == name && dataTable.Namespace == ns)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000329D4 File Offset: 0x00030BD4
		internal DataTable GetTableSmart(string name, string ns)
		{
			int num = 0;
			DataTable result = null;
			for (int i = 0; i < this._list.Count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (dataTable.TableName == name)
				{
					if (dataTable.Namespace == ns)
					{
						return dataTable;
					}
					num++;
					result = dataTable;
				}
			}
			if (num != 1)
			{
				return null;
			}
			return result;
		}

		/// <summary>Adds the specified <see langword="DataTable" /> to the collection.</summary>
		/// <param name="table">The <see langword="DataTable" /> object to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The value specified for the table is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The table already belongs to this collection, or belongs to another collection.</exception>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. The comparison is not case sensitive.</exception>
		// Token: 0x06000C65 RID: 3173 RVA: 0x00032A38 File Offset: 0x00030C38
		public void Add(DataTable table)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTableCollection.Add|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
				this.BaseAdd(table);
				this.ArrayAdd(table);
				if (table.SetLocaleValue(this._dataSet.Locale, false, false) || table.SetCaseSensitiveValue(this._dataSet.CaseSensitive, false, false))
				{
					table.ResetIndexes();
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataTable" /> array to the end of the collection.</summary>
		/// <param name="tables">The array of <see cref="T:System.Data.DataTable" /> objects to add to the collection.</param>
		// Token: 0x06000C66 RID: 3174 RVA: 0x00032AE0 File Offset: 0x00030CE0
		public void AddRange(DataTable[] tables)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTableCollection.AddRange|API> {0}", this.ObjectID);
			try
			{
				if (this._dataSet._fInitInProgress)
				{
					this._delayedAddRangeTables = tables;
				}
				else if (tables != null)
				{
					foreach (DataTable dataTable in tables)
					{
						if (dataTable != null)
						{
							this.Add(dataTable);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object by using the specified name and adds it to the collection.</summary>
		/// <param name="name">The name to give the created <see cref="T:System.Data.DataTable" />.</param>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. (The comparison is not case sensitive.)</exception>
		// Token: 0x06000C67 RID: 3175 RVA: 0x00032B58 File Offset: 0x00030D58
		public DataTable Add(string name)
		{
			DataTable dataTable = new DataTable(name);
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object by using the specified name and adds it to the collection.</summary>
		/// <param name="name">The name to give the created <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="tableNamespace">The namespace to give the created <see cref="T:System.Data.DataTable" />.</param>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. (The comparison is not case sensitive.)</exception>
		// Token: 0x06000C68 RID: 3176 RVA: 0x00032B74 File Offset: 0x00030D74
		public DataTable Add(string name, string tableNamespace)
		{
			DataTable dataTable = new DataTable(name, tableNamespace);
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Creates a new <see cref="T:System.Data.DataTable" /> object by using a default name and adds it to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		// Token: 0x06000C69 RID: 3177 RVA: 0x00032B94 File Offset: 0x00030D94
		public DataTable Add()
		{
			DataTable dataTable = new DataTable();
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Occurs after the <see cref="T:System.Data.DataTableCollection" /> is changed because of <see cref="T:System.Data.DataTable" /> objects being added or removed.</summary>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000C6A RID: 3178 RVA: 0x00032BAF File Offset: 0x00030DAF
		// (remove) Token: 0x06000C6B RID: 3179 RVA: 0x00032BDD File Offset: 0x00030DDD
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.add_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.remove_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangedDelegate, value);
			}
		}

		/// <summary>Occurs while the <see cref="T:System.Data.DataTableCollection" /> is changing because of <see cref="T:System.Data.DataTable" /> objects being added or removed.</summary>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000C6C RID: 3180 RVA: 0x00032C0B File Offset: 0x00030E0B
		// (remove) Token: 0x06000C6D RID: 3181 RVA: 0x00032C39 File Offset: 0x00030E39
		public event CollectionChangeEventHandler CollectionChanging
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.add_CollectionChanging|API> {0}", this.ObjectID);
				this._onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangingDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.remove_CollectionChanging|API> {0}", this.ObjectID);
				this._onCollectionChangingDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangingDelegate, value);
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00032C67 File Offset: 0x00030E67
		private void ArrayAdd(DataTable table)
		{
			this._list.Add(table);
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00032C78 File Offset: 0x00030E78
		internal string AssignName()
		{
			string result;
			while (this.Contains(result = this.MakeName(this._defaultNameIndex)))
			{
				this._defaultNameIndex++;
			}
			return result;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00032CB0 File Offset: 0x00030EB0
		private void BaseAdd(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			if (table.DataSet == this._dataSet)
			{
				throw ExceptionBuilder.TableAlreadyInTheDataSet();
			}
			if (table.DataSet != null)
			{
				throw ExceptionBuilder.TableAlreadyInOtherDataSet();
			}
			if (table.TableName.Length == 0)
			{
				table.TableName = this.AssignName();
			}
			else
			{
				if (base.NamesEqual(table.TableName, this._dataSet.DataSetName, false, this._dataSet.Locale) != 0 && !table._fNestedInDataset)
				{
					throw ExceptionBuilder.DatasetConflictingName(this._dataSet.DataSetName);
				}
				this.RegisterName(table.TableName, table.Namespace);
			}
			table.SetDataSet(this._dataSet);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00032D64 File Offset: 0x00030F64
		private void BaseGroupSwitch(DataTable[] oldArray, int oldLength, DataTable[] newArray, int newLength)
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
				if (!flag && oldArray[i].DataSet == this._dataSet)
				{
					this.BaseRemove(oldArray[i]);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (newArray[k].DataSet != this._dataSet)
				{
					this.BaseAdd(newArray[k]);
					this._list.Add(newArray[k]);
				}
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00032DFA File Offset: 0x00030FFA
		private void BaseRemove(DataTable table)
		{
			if (this.CanRemove(table, true))
			{
				this.UnregisterName(table.TableName);
				table.SetDataSet(null);
			}
			this._list.Remove(table);
			this._dataSet.OnRemovedTable(table);
		}

		/// <summary>Verifies whether the specified <see cref="T:System.Data.DataTable" /> object can be removed from the collection.</summary>
		/// <param name="table">The <see langword="DataTable" /> in the collection to perform the check against.</param>
		/// <returns>
		///   <see langword="true" /> if the table can be removed; otherwise <see langword="false" />.</returns>
		// Token: 0x06000C73 RID: 3187 RVA: 0x00032E31 File Offset: 0x00031031
		public bool CanRemove(DataTable table)
		{
			return this.CanRemove(table, false);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00032E3C File Offset: 0x0003103C
		internal bool CanRemove(DataTable table, bool fThrowException)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int, bool>("<ds.DataTableCollection.CanRemove|INFO> {0}, table={1}, fThrowException={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, fThrowException);
			bool result;
			try
			{
				if (table == null)
				{
					if (fThrowException)
					{
						throw ExceptionBuilder.ArgumentNull("table");
					}
					result = false;
				}
				else if (table.DataSet != this._dataSet)
				{
					if (fThrowException)
					{
						throw ExceptionBuilder.TableNotInTheDataSet(table.TableName);
					}
					result = false;
				}
				else
				{
					this._dataSet.OnRemoveTable(table);
					if (table.ChildRelations.Count != 0 || table.ParentRelations.Count != 0)
					{
						if (fThrowException)
						{
							throw ExceptionBuilder.TableInRelation();
						}
						result = false;
					}
					else
					{
						ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._dataSet, table);
						while (parentForeignKeyConstraintEnumerator.GetNext())
						{
							ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
							if (foreignKeyConstraint.Table != table || foreignKeyConstraint.RelatedTable != table)
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.TableInConstraint(table, foreignKeyConstraint);
							}
						}
						ChildForeignKeyConstraintEnumerator childForeignKeyConstraintEnumerator = new ChildForeignKeyConstraintEnumerator(this._dataSet, table);
						while (childForeignKeyConstraintEnumerator.GetNext())
						{
							ForeignKeyConstraint foreignKeyConstraint2 = childForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
							if (foreignKeyConstraint2.Table != table || foreignKeyConstraint2.RelatedTable != table)
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.TableInConstraint(table, foreignKeyConstraint2);
							}
						}
						result = true;
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Clears the collection of all <see cref="T:System.Data.DataTable" /> objects.</summary>
		// Token: 0x06000C75 RID: 3189 RVA: 0x00032F84 File Offset: 0x00031184
		public void Clear()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTableCollection.Clear|API> {0}", this.ObjectID);
			try
			{
				int count = this._list.Count;
				DataTable[] array = new DataTable[this._list.Count];
				this._list.CopyTo(array, 0);
				this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
				if (this._dataSet._fInitInProgress && this._delayedAddRangeTables != null)
				{
					this._delayedAddRangeTables = null;
				}
				this.BaseGroupSwitch(array, count, null, 0);
				this._list.Clear();
				this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.DataTable" /> object with the specified name exists in the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> to find.</param>
		/// <returns>
		///   <see langword="true" /> if the specified table exists; otherwise <see langword="false" />.</returns>
		// Token: 0x06000C76 RID: 3190 RVA: 0x00033038 File Offset: 0x00031238
		public bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.DataTable" /> object with the specified name and table namespace exists in the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> to find.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <returns>
		///   <see langword="true" /> if the specified table exists; otherwise <see langword="false" />.</returns>
		// Token: 0x06000C77 RID: 3191 RVA: 0x00033047 File Offset: 0x00031247
		public bool Contains(string name, string tableNamespace)
		{
			if (name == null)
			{
				throw ExceptionBuilder.ArgumentNull("name");
			}
			if (tableNamespace == null)
			{
				throw ExceptionBuilder.ArgumentNull("tableNamespace");
			}
			return this.InternalIndexOf(name, tableNamespace) >= 0;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00033074 File Offset: 0x00031274
		internal bool Contains(string name, string tableNamespace, bool checkProperty, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.InternalIndexOf(name) >= 0;
			}
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				string a = checkProperty ? dataTable.Namespace : dataTable._tableNamespace;
				if (base.NamesEqual(dataTable.TableName, name, true, this._dataSet.Locale) == 1 && a == tableNamespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000330F8 File Offset: 0x000312F8
		internal bool Contains(string name, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.InternalIndexOf(name) >= 0;
			}
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (base.NamesEqual(dataTable.TableName, name, true, this._dataSet.Locale) == 1)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.DataTableCollection" /> to a one-dimensional <see cref="T:System.Array" />, starting at the specified destination array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to copy the current <see cref="T:System.Data.DataTableCollection" /> object's elements into.</param>
		/// <param name="index">The destination <see cref="T:System.Array" /> index to start copying into.</param>
		// Token: 0x06000C7A RID: 3194 RVA: 0x00033160 File Offset: 0x00031360
		public void CopyTo(DataTable[] array, int index)
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
				array[index + i] = (DataTable)this._list[i];
			}
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <param name="table">The <see langword="DataTable" /> to search for.</param>
		/// <returns>The zero-based index of the table, or -1 if the table is not found in the collection.</returns>
		// Token: 0x06000C7B RID: 3195 RVA: 0x000331D0 File Offset: 0x000313D0
		public int IndexOf(DataTable table)
		{
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				if (table == (DataTable)this._list[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index in the collection of the <see cref="T:System.Data.DataTable" /> object with the specified name.</summary>
		/// <param name="tableName">The name of the <see langword="DataTable" /> object to look for.</param>
		/// <returns>The zero-based index of the <see langword="DataTable" /> with the specified name, or -1 if the table does not exist in the collection.  
		///
		///  Returns -1 when two or more tables have the same name but different namespaces. The call does not succeed if there is any ambiguity when matching a table name to exactly one table.</returns>
		// Token: 0x06000C7C RID: 3196 RVA: 0x0003320C File Offset: 0x0003140C
		public int IndexOf(string tableName)
		{
			int num = this.InternalIndexOf(tableName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <param name="tableName">The name of the <see cref="T:System.Data.DataTable" /> object to look for.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <returns>The zero-based index of the <see cref="T:System.Data.DataTable" /> with the specified name, or -1 if the table does not exist in the collection.</returns>
		// Token: 0x06000C7D RID: 3197 RVA: 0x00033228 File Offset: 0x00031428
		public int IndexOf(string tableName, string tableNamespace)
		{
			return this.IndexOf(tableName, tableNamespace, true);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00033234 File Offset: 0x00031434
		internal int IndexOf(string tableName, string tableNamespace, bool chekforNull)
		{
			if (chekforNull)
			{
				if (tableName == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableName");
				}
				if (tableNamespace == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableNamespace");
				}
			}
			int num = this.InternalIndexOf(tableName, tableNamespace);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00033270 File Offset: 0x00031470
		internal void ReplaceFromInference(List<DataTable> tableList)
		{
			this._list.Clear();
			this._list.AddRange(tableList);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0003328C File Offset: 0x0003148C
		internal int InternalIndexOf(string tableName)
		{
			int num = -1;
			if (tableName != null && 0 < tableName.Length)
			{
				int count = this._list.Count;
				for (int i = 0; i < count; i++)
				{
					DataTable dataTable = (DataTable)this._list[i];
					int num2 = base.NamesEqual(dataTable.TableName, tableName, false, this._dataSet.Locale);
					if (num2 == 1)
					{
						for (int j = i + 1; j < count; j++)
						{
							DataTable dataTable2 = (DataTable)this._list[j];
							if (base.NamesEqual(dataTable2.TableName, tableName, false, this._dataSet.Locale) == 1)
							{
								return -3;
							}
						}
						return i;
					}
					if (num2 == -1)
					{
						num = ((num == -1) ? i : -2);
					}
				}
			}
			return num;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00033358 File Offset: 0x00031558
		internal int InternalIndexOf(string tableName, string tableNamespace)
		{
			int num = -1;
			if (tableName != null && 0 < tableName.Length)
			{
				int count = this._list.Count;
				for (int i = 0; i < count; i++)
				{
					DataTable dataTable = (DataTable)this._list[i];
					int num2 = base.NamesEqual(dataTable.TableName, tableName, false, this._dataSet.Locale);
					if (num2 == 1 && dataTable.Namespace == tableNamespace)
					{
						return i;
					}
					if (num2 == -1 && dataTable.Namespace == tableNamespace)
					{
						num = ((num == -1) ? i : -2);
					}
				}
			}
			return num;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000333F4 File Offset: 0x000315F4
		internal void FinishInitCollection()
		{
			if (this._delayedAddRangeTables != null)
			{
				foreach (DataTable dataTable in this._delayedAddRangeTables)
				{
					if (dataTable != null)
					{
						this.Add(dataTable);
					}
				}
				this._delayedAddRangeTables = null;
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00033433 File Offset: 0x00031633
		private string MakeName(int index)
		{
			if (1 != index)
			{
				return "Table" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Table1";
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00033455 File Offset: 0x00031655
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.OnCollectionChanged|INFO> {0}", this.ObjectID);
				this._onCollectionChangedDelegate(this, ccevent);
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00033481 File Offset: 0x00031681
		private void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.OnCollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000334B0 File Offset: 0x000316B0
		internal void RegisterName(string name, string tbNamespace)
		{
			DataCommonEventSource.Log.Trace<int, string, string>("<ds.DataTableCollection.RegisterName|INFO> {0}, name='{1}', tbNamespace='{2}'", this.ObjectID, name, tbNamespace);
			CultureInfo locale = this._dataSet.Locale;
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (base.NamesEqual(name, dataTable.TableName, true, locale) != 0 && tbNamespace == dataTable.Namespace)
				{
					throw ExceptionBuilder.DuplicateTableName(((DataTable)this._list[i]).TableName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataTable" /> object from the collection.</summary>
		/// <param name="table">The <see langword="DataTable" /> to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The value specified for the table is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The table does not belong to this collection.  
		///  -or-  
		///  The table is part of a relationship.</exception>
		// Token: 0x06000C87 RID: 3207 RVA: 0x0003356C File Offset: 0x0003176C
		public void Remove(DataTable table)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTableCollection.Remove|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
				this.BaseRemove(table);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see langword="DataTable" /> to remove.</param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table at the specified index.</exception>
		// Token: 0x06000C88 RID: 3208 RVA: 0x000335DC File Offset: 0x000317DC
		public void RemoveAt(int index)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTableCollection.RemoveAt|API> {0}, index={1}", this.ObjectID, index);
			try
			{
				DataTable dataTable = this[index];
				if (dataTable == null)
				{
					throw ExceptionBuilder.TableOutOfRange(index);
				}
				this.Remove(dataTable);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> object to remove.</param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table with the specified name.</exception>
		// Token: 0x06000C89 RID: 3209 RVA: 0x00033638 File Offset: 0x00031838
		public void Remove(string name)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, string>("<ds.DataTableCollection.Remove|API> {0}, name='{1}'", this.ObjectID, name);
			try
			{
				DataTable dataTable = this[name];
				if (dataTable == null)
				{
					throw ExceptionBuilder.TableNotInTheDataSet(name);
				}
				this.Remove(dataTable);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> object to remove.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table with the specified name.</exception>
		// Token: 0x06000C8A RID: 3210 RVA: 0x00033694 File Offset: 0x00031894
		public void Remove(string name, string tableNamespace)
		{
			if (name == null)
			{
				throw ExceptionBuilder.ArgumentNull("name");
			}
			if (tableNamespace == null)
			{
				throw ExceptionBuilder.ArgumentNull("tableNamespace");
			}
			DataTable dataTable = this[name, tableNamespace];
			if (dataTable == null)
			{
				throw ExceptionBuilder.TableNotInTheDataSet(name);
			}
			this.Remove(dataTable);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000336D8 File Offset: 0x000318D8
		internal void UnregisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataTableCollection.UnregisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._dataSet.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal DataTableCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000802 RID: 2050
		private readonly DataSet _dataSet;

		// Token: 0x04000803 RID: 2051
		private readonly ArrayList _list;

		// Token: 0x04000804 RID: 2052
		private int _defaultNameIndex;

		// Token: 0x04000805 RID: 2053
		private DataTable[] _delayedAddRangeTables;

		// Token: 0x04000806 RID: 2054
		private CollectionChangeEventHandler _onCollectionChangedDelegate;

		// Token: 0x04000807 RID: 2055
		private CollectionChangeEventHandler _onCollectionChangingDelegate;

		// Token: 0x04000808 RID: 2056
		private static int s_objectTypeCount;

		// Token: 0x04000809 RID: 2057
		private readonly int _objectID;
	}
}
