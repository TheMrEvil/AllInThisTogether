using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>A collection of <see cref="T:System.Data.Common.DataTableMapping" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000387 RID: 903
	[ListBindable(false)]
	public sealed class DataTableMappingCollection : MarshalByRefObject, ITableMappingCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMappingCollection" /> class. This new instance is empty, that is, it does not yet contain any <see cref="T:System.Data.Common.DataTableMapping" /> objects.</summary>
		// Token: 0x06002AD0 RID: 10960 RVA: 0x00003DB9 File Offset: 0x00001FB9
		public DataTableMappingCollection()
		{
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x00006D64 File Offset: 0x00004F64
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x00005696 File Offset: 0x00003896
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the item to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x17000733 RID: 1843
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataTableMapping)value;
			}
		}

		/// <summary>Gets or sets the instance of <see cref="T:System.Data.ITableMapping" /> with the specified <see cref="P:System.Data.ITableMapping.SourceTable" /> name.</summary>
		/// <param name="index">The <see langword="SourceTable" /> name of the <see cref="T:System.Data.ITableMapping" />.</param>
		/// <returns>The instance of <see cref="T:System.Data.ITableMapping" /> with the specified <see langword="SourceTable" /> name.</returns>
		// Token: 0x17000734 RID: 1844
		object ITableMappingCollection.this[string index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataTableMapping)value;
			}
		}

		/// <summary>Adds a table mapping to the collection.</summary>
		/// <param name="sourceTableName">The case-sensitive name of the source table.</param>
		/// <param name="dataSetTableName">The name of the <see cref="T:System.Data.DataSet" /> table.</param>
		/// <returns>A reference to the newly-mapped <see cref="T:System.Data.ITableMapping" /> object.</returns>
		// Token: 0x06002AD9 RID: 10969 RVA: 0x000BA367 File Offset: 0x000B8567
		ITableMapping ITableMappingCollection.Add(string sourceTableName, string dataSetTableName)
		{
			return this.Add(sourceTableName, dataSetTableName);
		}

		/// <summary>Gets the TableMapping object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <param name="dataSetTableName">The name of the <see langword="DataSet" /> table within the collection.</param>
		/// <returns>The TableMapping object with the specified <see langword="DataSet" /> table name.</returns>
		// Token: 0x06002ADA RID: 10970 RVA: 0x000BA371 File Offset: 0x000B8571
		ITableMapping ITableMappingCollection.GetByDataSetTable(string dataSetTableName)
		{
			return this.GetByDataSetTable(dataSetTableName);
		}

		/// <summary>Gets the number of <see cref="T:System.Data.Common.DataTableMapping" /> objects in the collection.</summary>
		/// <returns>The number of <see langword="DataTableMapping" /> objects in the collection.</returns>
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000BA37A File Offset: 0x000B857A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				if (this._items == null)
				{
					return 0;
				}
				return this._items.Count;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x000BA391 File Offset: 0x000B8591
		private Type ItemType
		{
			get
			{
				return typeof(DataTableMapping);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataTableMapping" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to return.</param>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object at the specified index.</returns>
		// Token: 0x17000737 RID: 1847
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataTableMapping this[int index]
		{
			get
			{
				this.RangeCheck(index);
				return this._items[index];
			}
			set
			{
				this.RangeCheck(index);
				this.Replace(index, value);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</summary>
		/// <param name="sourceTable">The case-sensitive name of the source table.</param>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</returns>
		// Token: 0x17000738 RID: 1848
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataTableMapping this[string sourceTable]
		{
			get
			{
				int index = this.RangeCheck(sourceTable);
				return this._items[index];
			}
			set
			{
				int index = this.RangeCheck(sourceTable);
				this.Replace(index, value);
			}
		}

		/// <summary>Adds an <see cref="T:System.Object" /> that is a table mapping to the collection.</summary>
		/// <param name="value">A <see langword="DataTableMapping" /> object to add to the collection.</param>
		/// <returns>The index of the <see langword="DataTableMapping" /> object added to the collection.</returns>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataTableMapping" /> object.</exception>
		// Token: 0x06002AE1 RID: 10977 RVA: 0x000BA405 File Offset: 0x000B8605
		public int Add(object value)
		{
			this.ValidateType(value);
			this.Add((DataTableMapping)value);
			return this.Count - 1;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000BA423 File Offset: 0x000B8623
		private DataTableMapping Add(DataTableMapping value)
		{
			this.AddWithoutEvents(value);
			return value;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.Common.DataTableMapping" /> array to the end of the collection.</summary>
		/// <param name="values">The array of <see cref="T:System.Data.Common.DataTableMapping" /> objects to add to the collection.</param>
		// Token: 0x06002AE3 RID: 10979 RVA: 0x000BA42D File Offset: 0x000B862D
		public void AddRange(DataTableMapping[] values)
		{
			this.AddEnumerableRange(values, false);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Array" /> to the end of the collection.</summary>
		/// <param name="values">An <see cref="T:System.Array" /> of values to add to the collection.</param>
		// Token: 0x06002AE4 RID: 10980 RVA: 0x000BA42D File Offset: 0x000B862D
		public void AddRange(Array values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000BA438 File Offset: 0x000B8638
		private void AddEnumerableRange(IEnumerable values, bool doClone)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			foreach (object value in values)
			{
				this.ValidateType(value);
			}
			if (doClone)
			{
				using (IEnumerator enumerator = values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ICloneable cloneable = (ICloneable)obj;
						this.AddWithoutEvents(cloneable.Clone() as DataTableMapping);
					}
					return;
				}
			}
			foreach (object obj2 in values)
			{
				DataTableMapping value2 = (DataTableMapping)obj2;
				this.AddWithoutEvents(value2);
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataTableMapping" /> object to the collection when given a source table name and a <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <param name="sourceTable">The case-sensitive name of the source table to map from.</param>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> table to map to.</param>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object that was added to the collection.</returns>
		// Token: 0x06002AE6 RID: 10982 RVA: 0x000BA52C File Offset: 0x000B872C
		public DataTableMapping Add(string sourceTable, string dataSetTable)
		{
			return this.Add(new DataTableMapping(sourceTable, dataSetTable));
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000BA53B File Offset: 0x000B873B
		private void AddWithoutEvents(DataTableMapping value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Add(value);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000BA558 File Offset: 0x000B8758
		private List<DataTableMapping> ArrayList()
		{
			List<DataTableMapping> result;
			if ((result = this._items) == null)
			{
				result = (this._items = new List<DataTableMapping>());
			}
			return result;
		}

		/// <summary>Removes all <see cref="T:System.Data.Common.DataTableMapping" /> objects from the collection.</summary>
		// Token: 0x06002AE9 RID: 10985 RVA: 0x000BA57D File Offset: 0x000B877D
		public void Clear()
		{
			if (0 < this.Count)
			{
				this.ClearWithoutEvents();
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000BA590 File Offset: 0x000B8790
		private void ClearWithoutEvents()
		{
			if (this._items != null)
			{
				foreach (DataTableMapping dataTableMapping in this._items)
				{
					dataTableMapping.Parent = null;
				}
				this._items.Clear();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name exists in the collection.</summary>
		/// <param name="value">The case-sensitive source table name containing the <see cref="T:System.Data.Common.DataTableMapping" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains a <see cref="T:System.Data.Common.DataTableMapping" /> object with this source table name; otherwise <see langword="false" />.</returns>
		// Token: 0x06002AEB RID: 10987 RVA: 0x000BA5F4 File Offset: 0x000B87F4
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether the given <see cref="T:System.Data.Common.DataTableMapping" /> object exists in the collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataTableMapping" />.</param>
		/// <returns>
		///   <see langword="true" /> if this collection contains the specified <see cref="T:System.Data.Common.DataTableMapping" />; otherwise <see langword="false" />.</returns>
		// Token: 0x06002AEC RID: 10988 RVA: 0x000BA603 File Offset: 0x000B8803
		public bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataTableMappingCollection" /> to the specified array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> to which to copy the <see cref="T:System.Data.Common.DataTableMappingCollection" /> elements.</param>
		/// <param name="index">The starting index of the array.</param>
		// Token: 0x06002AED RID: 10989 RVA: 0x000BA612 File Offset: 0x000B8812
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.ArrayList()).CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataTableMapping" /> to the specified array.</summary>
		/// <param name="array">A <see cref="T:System.Data.Common.DataTableMapping" /> to which to copy the <see cref="T:System.Data.Common.DataTableMappingCollection" /> elements.</param>
		/// <param name="index">The starting index of the array.</param>
		// Token: 0x06002AEE RID: 10990 RVA: 0x000BA621 File Offset: 0x000B8821
		public void CopyTo(DataTableMapping[] array, int index)
		{
			this.ArrayList().CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> table to find.</param>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</returns>
		// Token: 0x06002AEF RID: 10991 RVA: 0x000BA630 File Offset: 0x000B8830
		public DataTableMapping GetByDataSetTable(string dataSetTable)
		{
			int num = this.IndexOfDataSetTable(dataSetTable);
			if (0 > num)
			{
				throw ADP.TablesDataSetTable(dataSetTable);
			}
			return this._items[num];
		}

		/// <summary>Gets an enumerator that can iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06002AF0 RID: 10992 RVA: 0x000BA65C File Offset: 0x000B885C
		public IEnumerator GetEnumerator()
		{
			return this.ArrayList().GetEnumerator();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Common.DataTableMapping" /> object within the collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataTableMapping" /> object to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Common.DataTableMapping" /> object within the collection.</returns>
		// Token: 0x06002AF1 RID: 10993 RVA: 0x000BA670 File Offset: 0x000B8870
		public int IndexOf(object value)
		{
			if (value != null)
			{
				this.ValidateType(value);
				for (int i = 0; i < this.Count; i++)
				{
					if (this._items[i] == value)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</summary>
		/// <param name="sourceTable">The case-sensitive name of the source table.</param>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</returns>
		// Token: 0x06002AF2 RID: 10994 RVA: 0x000BA6AC File Offset: 0x000B88AC
		public int IndexOf(string sourceTable)
		{
			if (!string.IsNullOrEmpty(sourceTable))
			{
				for (int i = 0; i < this.Count; i++)
				{
					string sourceTable2 = this._items[i].SourceTable;
					if (sourceTable2 != null && ADP.SrcCompare(sourceTable, sourceTable2) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the <see langword="DataSet" /> table to find.</param>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the given <see cref="T:System.Data.DataSet" /> table name, or -1 if the <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist in the collection.</returns>
		// Token: 0x06002AF3 RID: 10995 RVA: 0x000BA6F4 File Offset: 0x000B88F4
		public int IndexOfDataSetTable(string dataSetTable)
		{
			if (!string.IsNullOrEmpty(dataSetTable))
			{
				for (int i = 0; i < this.Count; i++)
				{
					string dataSetTable2 = this._items[i].DataSetTable;
					if (dataSetTable2 != null && ADP.DstCompare(dataSetTable, dataSetTable2) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataTableMapping" /> object into the <see cref="T:System.Data.Common.DataTableMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to insert.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to insert.</param>
		// Token: 0x06002AF4 RID: 10996 RVA: 0x000BA73B File Offset: 0x000B893B
		public void Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (DataTableMapping)value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataTableMapping" /> object into the <see cref="T:System.Data.Common.DataTableMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to insert.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to insert.</param>
		// Token: 0x06002AF5 RID: 10997 RVA: 0x000BA751 File Offset: 0x000B8951
		public void Insert(int index, DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Insert(index, value);
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000BA77D File Offset: 0x000B897D
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.TablesIndexInt32(index, this);
			}
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000BA794 File Offset: 0x000B8994
		private int RangeCheck(string sourceTable)
		{
			int num = this.IndexOf(sourceTable);
			if (num < 0)
			{
				throw ADP.TablesSourceIndex(sourceTable);
			}
			return num;
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataTableMapping" /> object located at the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to remove.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">A <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist with the specified index.</exception>
		// Token: 0x06002AF8 RID: 11000 RVA: 0x000BA7A8 File Offset: 0x000B89A8
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name from the collection.</summary>
		/// <param name="sourceTable">The case-sensitive source table name to find.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">A <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist with the specified source table name.</exception>
		// Token: 0x06002AF9 RID: 11001 RVA: 0x000BA7B8 File Offset: 0x000B89B8
		public void RemoveAt(string sourceTable)
		{
			int index = this.RangeCheck(sourceTable);
			this.RemoveIndex(index);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000BA7D4 File Offset: 0x000B89D4
		private void RemoveIndex(int index)
		{
			this._items[index].Parent = null;
			this._items.RemoveAt(index);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataTableMapping" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to remove.</param>
		/// <exception cref="T:System.InvalidCastException">The object specified was not a <see cref="T:System.Data.Common.DataTableMapping" /> object.</exception>
		/// <exception cref="T:System.ArgumentException">The object specified is not in the collection.</exception>
		// Token: 0x06002AFB RID: 11003 RVA: 0x000BA7F4 File Offset: 0x000B89F4
		public void Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((DataTableMapping)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataTableMapping" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to remove.</param>
		// Token: 0x06002AFC RID: 11004 RVA: 0x000BA80C File Offset: 0x000B8A0C
		public void Remove(DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw ADP.CollectionRemoveInvalidObject(this.ItemType, this);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000BA847 File Offset: 0x000B8A47
		private void Replace(int index, DataTableMapping newValue)
		{
			this.Validate(index, newValue);
			this._items[index].Parent = null;
			newValue.Parent = this;
			this._items[index] = newValue;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000BA877 File Offset: 0x000B8A77
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw ADP.NotADataTableMapping(value);
			}
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000BA89C File Offset: 0x000B8A9C
		private void Validate(int index, DataTableMapping value)
		{
			if (value == null)
			{
				throw ADP.TablesAddNullAttempt("value");
			}
			if (value.Parent != null)
			{
				if (this != value.Parent)
				{
					throw ADP.TablesIsNotParent(this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.TablesIsParent(this);
				}
			}
			string text = value.SourceTable;
			if (string.IsNullOrEmpty(text))
			{
				index = 1;
				do
				{
					text = "SourceTable" + index.ToString(CultureInfo.InvariantCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				value.SourceTable = text;
				return;
			}
			this.ValidateSourceTable(index, text);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000BA928 File Offset: 0x000B8B28
		internal void ValidateSourceTable(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.TablesUniqueSourceTable(value);
			}
		}

		/// <summary>Gets a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source table name and <see cref="T:System.Data.DataSet" /> table name, using the given <see cref="T:System.Data.MissingMappingAction" />.</summary>
		/// <param name="tableMappings">The <see cref="T:System.Data.Common.DataTableMappingCollection" /> collection to search.</param>
		/// <param name="sourceTable">The case-sensitive name of the mapped source table.</param>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the mapped <see cref="T:System.Data.DataSet" /> table.</param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
		/// <returns>A <see cref="T:System.Data.Common.DataTableMapping" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to <see langword="Error" />, and no mapping was specified.</exception>
		// Token: 0x06002B01 RID: 11009 RVA: 0x000BA94C File Offset: 0x000B8B4C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataTableMapping GetTableMappingBySchemaAction(DataTableMappingCollection tableMappings, string sourceTable, string dataSetTable, MissingMappingAction mappingAction)
		{
			if (tableMappings != null)
			{
				int num = tableMappings.IndexOf(sourceTable);
				if (-1 != num)
				{
					return tableMappings._items[num];
				}
			}
			if (string.IsNullOrEmpty(sourceTable))
			{
				throw ADP.InvalidSourceTable("sourceTable");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return new DataTableMapping(sourceTable, dataSetTable);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingTableMapping(sourceTable);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		// Token: 0x04001B15 RID: 6933
		private List<DataTableMapping> _items;
	}
}
