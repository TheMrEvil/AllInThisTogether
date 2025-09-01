using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common
{
	/// <summary>Contains a collection of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</summary>
	// Token: 0x02000380 RID: 896
	public sealed class DataColumnMappingCollection : MarshalByRefObject, IColumnMappingCollection, IList, ICollection, IEnumerable
	{
		/// <summary>Creates an empty <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</summary>
		// Token: 0x06002A35 RID: 10805 RVA: 0x00003DB9 File Offset: 0x00001FB9
		public DataColumnMappingCollection()
		{
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x00006D64 File Offset: 0x00004F64
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002A37 RID: 10807 RVA: 0x00005696 File Offset: 0x00003896
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002A38 RID: 10808 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002A39 RID: 10809 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x1700071F RID: 1823
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataColumnMapping)value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.IColumnMapping" /> object with the specified <see langword="SourceColumn" /> name.</summary>
		/// <param name="index">Index of the element.</param>
		/// <returns>The <see langword="IColumnMapping" /> object with the specified <see langword="SourceColumn" /> name.</returns>
		// Token: 0x17000720 RID: 1824
		object IColumnMappingCollection.this[string index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.ValidateType(value);
				this[index] = (DataColumnMapping)value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> by using the source column and <see cref="T:System.Data.DataSet" /> column names.</summary>
		/// <param name="sourceColumnName">The case-sensitive name of the source column.</param>
		/// <param name="dataSetColumnName">The name of the <see cref="T:System.Data.DataSet" /> column.</param>
		/// <returns>The ColumnMapping object that was added to the collection.</returns>
		// Token: 0x06002A3E RID: 10814 RVA: 0x000B8C66 File Offset: 0x000B6E66
		IColumnMapping IColumnMappingCollection.Add(string sourceColumnName, string dataSetColumnName)
		{
			return this.Add(sourceColumnName, dataSetColumnName);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMapping" /> object that has the specified <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <param name="dataSetColumnName">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to find.</param>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object that  has the specified <see cref="T:System.Data.DataSet" /> column name.</returns>
		// Token: 0x06002A3F RID: 10815 RVA: 0x000B8C70 File Offset: 0x000B6E70
		IColumnMapping IColumnMappingCollection.GetByDataSetColumn(string dataSetColumnName)
		{
			return this.GetByDataSetColumn(dataSetColumnName);
		}

		/// <summary>Gets the number of <see cref="T:System.Data.Common.DataColumnMapping" /> objects in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000B8C79 File Offset: 0x000B6E79
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

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002A41 RID: 10817 RVA: 0x000B8C90 File Offset: 0x000B6E90
		private Type ItemType
		{
			get
			{
				return typeof(DataColumnMapping);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataColumnMapping" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to find.</param>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object at the specified index.</returns>
		// Token: 0x17000723 RID: 1827
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataColumnMapping this[int index]
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

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</summary>
		/// <param name="sourceColumn">The case-sensitive name of the source column.</param>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</returns>
		// Token: 0x17000724 RID: 1828
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataColumnMapping this[string sourceColumn]
		{
			get
			{
				int index = this.RangeCheck(sourceColumn);
				return this._items[index];
			}
			set
			{
				int index = this.RangeCheck(sourceColumn);
				this.Replace(index, value);
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the collection.</summary>
		/// <param name="value">A <see langword="DataColumnMapping" /> object to add to the collection.</param>
		/// <returns>The index of the <see langword="DataColumnMapping" /> object that was added to the collection.</returns>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</exception>
		// Token: 0x06002A46 RID: 10822 RVA: 0x000B8D05 File Offset: 0x000B6F05
		public int Add(object value)
		{
			this.ValidateType(value);
			this.Add((DataColumnMapping)value);
			return this.Count - 1;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000B8D23 File Offset: 0x000B6F23
		private DataColumnMapping Add(DataColumnMapping value)
		{
			this.AddWithoutEvents(value);
			return value;
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the collection when given a source column name and a <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <param name="sourceColumn">The case-sensitive name of the source column to map to.</param>
		/// <param name="dataSetColumn">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to map to.</param>
		/// <returns>The <see langword="DataColumnMapping" /> object that was added to the collection.</returns>
		// Token: 0x06002A48 RID: 10824 RVA: 0x000B8D2D File Offset: 0x000B6F2D
		public DataColumnMapping Add(string sourceColumn, string dataSetColumn)
		{
			return this.Add(new DataColumnMapping(sourceColumn, dataSetColumn));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> array to the end of the collection.</summary>
		/// <param name="values">The array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects to add to the collection.</param>
		// Token: 0x06002A49 RID: 10825 RVA: 0x000B8D3C File Offset: 0x000B6F3C
		public void AddRange(DataColumnMapping[] values)
		{
			this.AddEnumerableRange(values, false);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Array" /> to the end of the collection.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> to add to the collection.</param>
		// Token: 0x06002A4A RID: 10826 RVA: 0x000B8D3C File Offset: 0x000B6F3C
		public void AddRange(Array values)
		{
			this.AddEnumerableRange(values, false);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000B8D48 File Offset: 0x000B6F48
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
						this.AddWithoutEvents(cloneable.Clone() as DataColumnMapping);
					}
					return;
				}
			}
			foreach (object obj2 in values)
			{
				DataColumnMapping value2 = (DataColumnMapping)obj2;
				this.AddWithoutEvents(value2);
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000B8E3C File Offset: 0x000B703C
		private void AddWithoutEvents(DataColumnMapping value)
		{
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Add(value);
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000B8E59 File Offset: 0x000B7059
		private List<DataColumnMapping> ArrayList()
		{
			if (this._items == null)
			{
				this._items = new List<DataColumnMapping>();
			}
			return this._items;
		}

		/// <summary>Removes all <see cref="T:System.Data.Common.DataColumnMapping" /> objects from the collection.</summary>
		// Token: 0x06002A4E RID: 10830 RVA: 0x000B8E74 File Offset: 0x000B7074
		public void Clear()
		{
			if (0 < this.Count)
			{
				this.ClearWithoutEvents();
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000B8E88 File Offset: 0x000B7088
		private void ClearWithoutEvents()
		{
			if (this._items != null)
			{
				foreach (DataColumnMapping dataColumnMapping in this._items)
				{
					dataColumnMapping.Parent = null;
				}
				this._items.Clear();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the given source column name exists in the collection.</summary>
		/// <param name="value">The case-sensitive source column name of the <see cref="T:System.Data.Common.DataColumnMapping" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if collection contains a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002A50 RID: 10832 RVA: 0x000B8EEC File Offset: 0x000B70EC
		public bool Contains(string value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the given <see cref="T:System.Object" /> exists in the collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" />.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the specified <see cref="T:System.Data.Common.DataColumnMapping" /> object; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</exception>
		// Token: 0x06002A51 RID: 10833 RVA: 0x000B8EFB File Offset: 0x000B70FB
		public bool Contains(object value)
		{
			return -1 != this.IndexOf(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> to the specified array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> to which to copy <see cref="T:System.Data.Common.DataColumnMappingCollection" /> elements.</param>
		/// <param name="index">The starting index of the array.</param>
		// Token: 0x06002A52 RID: 10834 RVA: 0x000B8F0A File Offset: 0x000B710A
		public void CopyTo(Array array, int index)
		{
			((ICollection)this.ArrayList()).CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> to the specified <see cref="T:System.Data.Common.DataColumnMapping" /> array.</summary>
		/// <param name="array">A <see cref="T:System.Data.Common.DataColumnMapping" /> array to which to copy the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> elements.</param>
		/// <param name="index">The zero-based index in the <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06002A53 RID: 10835 RVA: 0x000B8F19 File Offset: 0x000B7119
		public void CopyTo(DataColumnMapping[] array, int index)
		{
			this.ArrayList().CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <param name="value">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to find.</param>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> column name.</returns>
		// Token: 0x06002A54 RID: 10836 RVA: 0x000B8F28 File Offset: 0x000B7128
		public DataColumnMapping GetByDataSetColumn(string value)
		{
			int num = this.IndexOfDataSetColumn(value);
			if (0 > num)
			{
				throw ADP.ColumnsDataSetColumn(value);
			}
			return this._items[num];
		}

		/// <summary>Gets an enumerator that can iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06002A55 RID: 10837 RVA: 0x000B8F54 File Offset: 0x000B7154
		public IEnumerator GetEnumerator()
		{
			return this.ArrayList().GetEnumerator();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> within the collection.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" /> to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> within the collection.</returns>
		// Token: 0x06002A56 RID: 10838 RVA: 0x000B8F68 File Offset: 0x000B7168
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

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataColumnMapping" /> with the specified source column name.</summary>
		/// <param name="sourceColumn">The case-sensitive name of the source column.</param>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataColumnMapping" /> with the specified case-sensitive source column name.</returns>
		// Token: 0x06002A57 RID: 10839 RVA: 0x000B8FA4 File Offset: 0x000B71A4
		public int IndexOf(string sourceColumn)
		{
			if (!string.IsNullOrEmpty(sourceColumn))
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (ADP.SrcCompare(sourceColumn, this._items[i].SourceColumn) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> with the given <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <param name="dataSetColumn">The name, which is not case-sensitive, of the data set column to find.</param>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> with the given <see langword="DataSet" /> column name, or -1 if the <see langword="DataColumnMapping" /> object does not exist in the collection.</returns>
		// Token: 0x06002A58 RID: 10840 RVA: 0x000B8FE8 File Offset: 0x000B71E8
		public int IndexOfDataSetColumn(string dataSetColumn)
		{
			if (!string.IsNullOrEmpty(dataSetColumn))
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					if (ADP.DstCompare(dataSetColumn, this._items[i].DataSetColumn) == 0)
					{
						return i;
					}
				}
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataColumnMapping" /> object into the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to insert.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> object.</param>
		// Token: 0x06002A59 RID: 10841 RVA: 0x000B902C File Offset: 0x000B722C
		public void Insert(int index, object value)
		{
			this.ValidateType(value);
			this.Insert(index, (DataColumnMapping)value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataColumnMapping" /> object into the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to insert.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> object.</param>
		// Token: 0x06002A5A RID: 10842 RVA: 0x000B9042 File Offset: 0x000B7242
		public void Insert(int index, DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			this.Validate(-1, value);
			value.Parent = this;
			this.ArrayList().Insert(index, value);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000B906E File Offset: 0x000B726E
		private void RangeCheck(int index)
		{
			if (index < 0 || this.Count <= index)
			{
				throw ADP.ColumnsIndexInt32(index, this);
			}
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000B9085 File Offset: 0x000B7285
		private int RangeCheck(string sourceColumn)
		{
			int num = this.IndexOf(sourceColumn);
			if (num < 0)
			{
				throw ADP.ColumnsIndexSource(sourceColumn);
			}
			return num;
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to remove.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified index.</exception>
		// Token: 0x06002A5D RID: 10845 RVA: 0x000B9099 File Offset: 0x000B7299
		public void RemoveAt(int index)
		{
			this.RangeCheck(index);
			this.RemoveIndex(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name from the collection.</summary>
		/// <param name="sourceColumn">The case-sensitive source column name.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</exception>
		// Token: 0x06002A5E RID: 10846 RVA: 0x000B90AC File Offset: 0x000B72AC
		public void RemoveAt(string sourceColumn)
		{
			int index = this.RangeCheck(sourceColumn);
			this.RemoveIndex(index);
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000B90C8 File Offset: 0x000B72C8
		private void RemoveIndex(int index)
		{
			this._items[index].Parent = null;
			this._items.RemoveAt(index);
		}

		/// <summary>Removes the <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" /> to remove.</param>
		/// <exception cref="T:System.InvalidCastException">The object specified was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</exception>
		/// <exception cref="T:System.ArgumentException">The object specified is not in the collection.</exception>
		// Token: 0x06002A60 RID: 10848 RVA: 0x000B90E8 File Offset: 0x000B72E8
		public void Remove(object value)
		{
			this.ValidateType(value);
			this.Remove((DataColumnMapping)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataColumnMapping" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> to remove.</param>
		// Token: 0x06002A61 RID: 10849 RVA: 0x000B9100 File Offset: 0x000B7300
		public void Remove(DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			int num = this.IndexOf(value);
			if (-1 != num)
			{
				this.RemoveIndex(num);
				return;
			}
			throw ADP.CollectionRemoveInvalidObject(this.ItemType, this);
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000B913B File Offset: 0x000B733B
		private void Replace(int index, DataColumnMapping newValue)
		{
			this.Validate(index, newValue);
			this._items[index].Parent = null;
			newValue.Parent = this;
			this._items[index] = newValue;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000B916B File Offset: 0x000B736B
		private void ValidateType(object value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw ADP.NotADataColumnMapping(value);
			}
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000B9190 File Offset: 0x000B7390
		private void Validate(int index, DataColumnMapping value)
		{
			if (value == null)
			{
				throw ADP.ColumnsAddNullAttempt("value");
			}
			if (value.Parent != null)
			{
				if (this != value.Parent)
				{
					throw ADP.ColumnsIsNotParent(this);
				}
				if (index != this.IndexOf(value))
				{
					throw ADP.ColumnsIsParent(this);
				}
			}
			string text = value.SourceColumn;
			if (string.IsNullOrEmpty(text))
			{
				index = 1;
				do
				{
					text = "SourceColumn" + index.ToString(CultureInfo.InvariantCulture);
					index++;
				}
				while (-1 != this.IndexOf(text));
				value.SourceColumn = text;
				return;
			}
			this.ValidateSourceColumn(index, text);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000B921C File Offset: 0x000B741C
		internal void ValidateSourceColumn(int index, string value)
		{
			int num = this.IndexOf(value);
			if (-1 != num && index != num)
			{
				throw ADP.ColumnsUniqueSourceColumn(value);
			}
		}

		/// <summary>A static method that returns a <see cref="T:System.Data.DataColumn" /> object without instantiating a <see cref="T:System.Data.Common.DataColumnMappingCollection" /> object.</summary>
		/// <param name="columnMappings">The <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</param>
		/// <param name="sourceColumn">The case-sensitive column name from a data source.</param>
		/// <param name="dataType">The data type for the column being mapped.</param>
		/// <param name="dataTable">An instance of <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
		/// <param name="schemaAction">Determines the action to take when the existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		// Token: 0x06002A66 RID: 10854 RVA: 0x000B9240 File Offset: 0x000B7440
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumn(DataColumnMappingCollection columnMappings, string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			if (columnMappings != null)
			{
				int num = columnMappings.IndexOf(sourceColumn);
				if (-1 != num)
				{
					return columnMappings._items[num].GetDataColumnBySchemaAction(dataTable, dataType, schemaAction);
				}
			}
			if (string.IsNullOrEmpty(sourceColumn))
			{
				throw ADP.InvalidSourceColumn("sourceColumn");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return DataColumnMapping.GetDataColumnBySchemaAction(sourceColumn, sourceColumn, dataTable, dataType, schemaAction);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingColumnMapping(sourceColumn);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		/// <summary>Gets a <see cref="T:System.Data.Common.DataColumnMapping" /> for the specified <see cref="T:System.Data.Common.DataColumnMappingCollection" />, source column name, and <see cref="T:System.Data.MissingMappingAction" />.</summary>
		/// <param name="columnMappings">The <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</param>
		/// <param name="sourceColumn">The case-sensitive source column name to find.</param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
		/// <returns>A <see cref="T:System.Data.Common.DataColumnMapping" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to <see langword="Error" />, and no mapping was specified.</exception>
		// Token: 0x06002A67 RID: 10855 RVA: 0x000B92B8 File Offset: 0x000B74B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumnMapping GetColumnMappingBySchemaAction(DataColumnMappingCollection columnMappings, string sourceColumn, MissingMappingAction mappingAction)
		{
			if (columnMappings != null)
			{
				int num = columnMappings.IndexOf(sourceColumn);
				if (-1 != num)
				{
					return columnMappings._items[num];
				}
			}
			if (string.IsNullOrEmpty(sourceColumn))
			{
				throw ADP.InvalidSourceColumn("sourceColumn");
			}
			switch (mappingAction)
			{
			case MissingMappingAction.Passthrough:
				return new DataColumnMapping(sourceColumn, sourceColumn);
			case MissingMappingAction.Ignore:
				return null;
			case MissingMappingAction.Error:
				throw ADP.MissingColumnMapping(sourceColumn);
			default:
				throw ADP.InvalidMissingMappingAction(mappingAction);
			}
		}

		// Token: 0x04001AD1 RID: 6865
		private List<DataColumnMapping> _items;
	}
}
