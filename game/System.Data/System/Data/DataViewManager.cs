using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data
{
	/// <summary>Contains a default <see cref="T:System.Data.DataViewSettingCollection" /> for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x020000D9 RID: 217
	public class DataViewManager : MarshalByValueComponent, IBindingList, IList, ICollection, IEnumerable, ITypedList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataViewManager" /> class.</summary>
		// Token: 0x06000D71 RID: 3441 RVA: 0x00037368 File Offset: 0x00035568
		public DataViewManager() : this(null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataViewManager" /> class for the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The name of the <see cref="T:System.Data.DataSet" /> to use.</param>
		// Token: 0x06000D72 RID: 3442 RVA: 0x00037372 File Offset: 0x00035572
		public DataViewManager(DataSet dataSet) : this(dataSet, false)
		{
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003737C File Offset: 0x0003557C
		internal DataViewManager(DataSet dataSet, bool locked)
		{
			GC.SuppressFinalize(this);
			this._dataSet = dataSet;
			if (this._dataSet != null)
			{
				this._dataSet.Tables.CollectionChanged += this.TableCollectionChanged;
				this._dataSet.Relations.CollectionChanged += this.RelationCollectionChanged;
			}
			this._locked = locked;
			this._item = new DataViewManagerListItemTypeDescriptor(this);
			this._dataViewSettingsCollection = new DataViewSettingCollection(this);
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataSet" /> to use with the <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to use.</returns>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x000373FD File Offset: 0x000355FD
		// (set) Token: 0x06000D75 RID: 3445 RVA: 0x00037408 File Offset: 0x00035608
		[DefaultValue(null)]
		public DataSet DataSet
		{
			get
			{
				return this._dataSet;
			}
			set
			{
				if (value == null)
				{
					throw ExceptionBuilder.SetFailed("DataSet to null");
				}
				if (this._locked)
				{
					throw ExceptionBuilder.SetDataSetFailed();
				}
				if (this._dataSet != null)
				{
					if (this._nViews > 0)
					{
						throw ExceptionBuilder.CanNotSetDataSet();
					}
					this._dataSet.Tables.CollectionChanged -= this.TableCollectionChanged;
					this._dataSet.Relations.CollectionChanged -= this.RelationCollectionChanged;
				}
				this._dataSet = value;
				this._dataSet.Tables.CollectionChanged += this.TableCollectionChanged;
				this._dataSet.Relations.CollectionChanged += this.RelationCollectionChanged;
				this._dataViewSettingsCollection = new DataViewSettingCollection(this);
				this._item.Reset();
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSettingCollection" /> for each <see cref="T:System.Data.DataTable" /> in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewSettingCollection" /> for each <see langword="DataTable" />.</returns>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000374DA File Offset: 0x000356DA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataViewSettingCollection DataViewSettings
		{
			get
			{
				return this._dataViewSettingsCollection;
			}
		}

		/// <summary>Gets or sets a value that is used for code persistence.</summary>
		/// <returns>A value that is used for code persistence.</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x000374E4 File Offset: 0x000356E4
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x000375C0 File Offset: 0x000357C0
		public string DataViewSettingCollectionString
		{
			get
			{
				if (this._dataSet == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<DataViewSettingCollectionString>");
				foreach (object obj in this._dataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					DataViewSetting dataViewSetting = this._dataViewSettingsCollection[dataTable];
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<{0} Sort=\"{1}\" RowFilter=\"{2}\" RowStateFilter=\"{3}\"/>", new object[]
					{
						dataTable.EncodedTableName,
						dataViewSetting.Sort,
						dataViewSetting.RowFilter,
						dataViewSetting.RowStateFilter
					});
				}
				stringBuilder.Append("</DataViewSettingCollectionString>");
				return stringBuilder.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					return;
				}
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(value));
				xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
				xmlTextReader.Read();
				if (xmlTextReader.Name != "DataViewSettingCollectionString")
				{
					throw ExceptionBuilder.SetFailed("DataViewSettingCollectionString");
				}
				while (xmlTextReader.Read())
				{
					if (xmlTextReader.NodeType == XmlNodeType.Element)
					{
						string tableName = XmlConvert.DecodeName(xmlTextReader.LocalName);
						if (xmlTextReader.MoveToAttribute("Sort"))
						{
							this._dataViewSettingsCollection[tableName].Sort = xmlTextReader.Value;
						}
						if (xmlTextReader.MoveToAttribute("RowFilter"))
						{
							this._dataViewSettingsCollection[tableName].RowFilter = xmlTextReader.Value;
						}
						if (xmlTextReader.MoveToAttribute("RowStateFilter"))
						{
							this._dataViewSettingsCollection[tableName].RowStateFilter = (DataViewRowState)Enum.Parse(typeof(DataViewRowState), xmlTextReader.Value);
						}
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</returns>
		// Token: 0x06000D79 RID: 3449 RVA: 0x000376B4 File Offset: 0x000358B4
		IEnumerator IEnumerable.GetEnumerator()
		{
			DataViewManagerListItemTypeDescriptor[] array = new DataViewManagerListItemTypeDescriptor[1];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</returns>
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x00006D61 File Offset: 0x00004F61
		int ICollection.Count
		{
			get
			{
				return 1;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00005696 File Offset: 0x00003896
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x00006D64 File Offset: 0x00004F64
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000D7F RID: 3455 RVA: 0x000376D6 File Offset: 0x000358D6
		void ICollection.CopyTo(Array array, int index)
		{
			array.SetValue(new DataViewManagerListItemTypeDescriptor(this), index);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x1700025A RID: 602
		object IList.this[int index]
		{
			get
			{
				return this._item;
			}
			set
			{
				throw ExceptionBuilder.CannotModifyCollection();
			}
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		// Token: 0x06000D82 RID: 3458 RVA: 0x000376ED File Offset: 0x000358ED
		int IList.Add(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x06000D83 RID: 3459 RVA: 0x000376ED File Offset: 0x000358ED
		void IList.Clear()
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000D84 RID: 3460 RVA: 0x000376F4 File Offset: 0x000358F4
		bool IList.Contains(object value)
		{
			return value == this._item;
		}

		/// <summary>Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		// Token: 0x06000D85 RID: 3461 RVA: 0x000376FF File Offset: 0x000358FF
		int IList.IndexOf(object value)
		{
			if (value != this._item)
			{
				return -1;
			}
			return 1;
		}

		/// <summary>Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000D86 RID: 3462 RVA: 0x000376ED File Offset: 0x000358ED
		void IList.Insert(int index, object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
		// Token: 0x06000D87 RID: 3463 RVA: 0x000376ED File Offset: 0x000358ED
		void IList.Remove(object value)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>Removes the <see cref="T:System.Collections.IList" /> item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		// Token: 0x06000D88 RID: 3464 RVA: 0x000376ED File Offset: 0x000358ED
		void IList.RemoveAt(int index)
		{
			throw ExceptionBuilder.CannotModifyCollection();
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IBindingList.AllowNew
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</returns>
		// Token: 0x06000D8A RID: 3466 RVA: 0x0003770D File Offset: 0x0003590D
		object IBindingList.AddNew()
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</returns>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IBindingList.AllowEdit
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</returns>
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IBindingList.AllowRemove
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</returns>
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</returns>
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</returns>
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00006D64 File Offset: 0x00004F64
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</returns>
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0003770D File Offset: 0x0003590D
		bool IBindingList.IsSorted
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</returns>
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0003770D File Offset: 0x0003590D
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</returns>
		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0003770D File Offset: 0x0003590D
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw DataViewManager.s_notSupported;
			}
		}

		/// <summary>Occurs after a row is added to or deleted from a <see cref="T:System.Data.DataView" />.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000D93 RID: 3475 RVA: 0x00037714 File Offset: 0x00035914
		// (remove) Token: 0x06000D94 RID: 3476 RVA: 0x0003774C File Offset: 0x0003594C
		public event ListChangedEventHandler ListChanged
		{
			[CompilerGenerated]
			add
			{
				ListChangedEventHandler listChangedEventHandler = this.ListChanged;
				ListChangedEventHandler listChangedEventHandler2;
				do
				{
					listChangedEventHandler2 = listChangedEventHandler;
					ListChangedEventHandler value2 = (ListChangedEventHandler)Delegate.Combine(listChangedEventHandler2, value);
					listChangedEventHandler = Interlocked.CompareExchange<ListChangedEventHandler>(ref this.ListChanged, value2, listChangedEventHandler2);
				}
				while (listChangedEventHandler != listChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ListChangedEventHandler listChangedEventHandler = this.ListChanged;
				ListChangedEventHandler listChangedEventHandler2;
				do
				{
					listChangedEventHandler2 = listChangedEventHandler;
					ListChangedEventHandler value2 = (ListChangedEventHandler)Delegate.Remove(listChangedEventHandler2, value);
					listChangedEventHandler = Interlocked.CompareExchange<ListChangedEventHandler>(ref this.ListChanged, value2, listChangedEventHandler2);
				}
				while (listChangedEventHandler != listChangedEventHandler2);
			}
		}

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching.</param>
		// Token: 0x06000D95 RID: 3477 RVA: 0x00007EED File Offset: 0x000060ED
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		// Token: 0x06000D96 RID: 3478 RVA: 0x0003770D File Offset: 0x0003590D
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the property parameter to search for.</param>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		// Token: 0x06000D97 RID: 3479 RVA: 0x0003770D File Offset: 0x0003590D
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x06000D98 RID: 3480 RVA: 0x00007EED File Offset: 0x000060ED
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		/// <summary>Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		// Token: 0x06000D99 RID: 3481 RVA: 0x0003770D File Offset: 0x0003590D
		void IBindingList.RemoveSort()
		{
			throw DataViewManager.s_notSupported;
		}

		/// <summary>Returns the name of the list.</summary>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects, for which the list name is returned. This can be <see langword="null" />.</param>
		/// <returns>The name of the list.</returns>
		// Token: 0x06000D9A RID: 3482 RVA: 0x00037784 File Offset: 0x00035984
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			DataSet dataSet = this.DataSet;
			if (dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return dataSet.DataSetName;
			}
			DataTable dataTable = dataSet.FindTable(null, listAccessors, 0);
			if (dataTable != null)
			{
				return dataTable.TableName;
			}
			return string.Empty;
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</summary>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the collection as bindable. This can be <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</returns>
		// Token: 0x06000D9B RID: 3483 RVA: 0x000377C8 File Offset: 0x000359C8
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			DataSet dataSet = this.DataSet;
			if (dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				return ((ICustomTypeDescriptor)new DataViewManagerListItemTypeDescriptor(this)).GetProperties();
			}
			DataTable dataTable = dataSet.FindTable(null, listAccessors, 0);
			if (dataTable != null)
			{
				return dataTable.GetPropertyDescriptorCollection(null);
			}
			return new PropertyDescriptorCollection(null);
		}

		/// <summary>Creates a <see cref="T:System.Data.DataView" /> for the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="table">The name of the <see cref="T:System.Data.DataTable" /> to use in the <see cref="T:System.Data.DataView" />.</param>
		/// <returns>A <see cref="T:System.Data.DataView" /> object.</returns>
		// Token: 0x06000D9C RID: 3484 RVA: 0x00037813 File Offset: 0x00035A13
		public DataView CreateDataView(DataTable table)
		{
			if (this._dataSet == null)
			{
				throw ExceptionBuilder.CanNotUseDataViewManager();
			}
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this);
			return dataView;
		}

		/// <summary>Raises the <see cref="E:System.Data.DataViewManager.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D9D RID: 3485 RVA: 0x00037830 File Offset: 0x00035A30
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			try
			{
				ListChangedEventHandler listChanged = this.ListChanged;
				if (listChanged != null)
				{
					listChanged(this, e);
				}
			}
			catch (Exception e2) when (ADP.IsCatchableExceptionType(e2))
			{
				ExceptionBuilder.TraceExceptionWithoutRethrow(e2);
			}
		}

		/// <summary>Raises a <see cref="E:System.Data.DataTableCollection.CollectionChanged" /> event when a <see cref="T:System.Data.DataTable" /> is added to or removed from the <see cref="T:System.Data.DataTableCollection" />.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D9E RID: 3486 RVA: 0x00037884 File Offset: 0x00035A84
		protected virtual void TableCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			PropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataTablePropertyDescriptor((DataTable)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataTablePropertyDescriptor((DataTable)e.Element)) : null)));
		}

		/// <summary>Raises a <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event when a <see cref="T:System.Data.DataRelation" /> is added to or removed from the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06000D9F RID: 3487 RVA: 0x000378F0 File Offset: 0x00035AF0
		protected virtual void RelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			DataRelationPropertyDescriptor propDesc = null;
			this.OnListChanged((e.Action == CollectionChangeAction.Add) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : ((e.Action == CollectionChangeAction.Refresh) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, propDesc) : ((e.Action == CollectionChangeAction.Remove) ? new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, new DataRelationPropertyDescriptor((DataRelation)e.Element)) : null)));
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003795A File Offset: 0x00035B5A
		// Note: this type is marked as 'beforefieldinit'.
		static DataViewManager()
		{
		}

		// Token: 0x04000843 RID: 2115
		private DataViewSettingCollection _dataViewSettingsCollection;

		// Token: 0x04000844 RID: 2116
		private DataSet _dataSet;

		// Token: 0x04000845 RID: 2117
		private DataViewManagerListItemTypeDescriptor _item;

		// Token: 0x04000846 RID: 2118
		private bool _locked;

		// Token: 0x04000847 RID: 2119
		internal int _nViews;

		// Token: 0x04000848 RID: 2120
		private static NotSupportedException s_notSupported = new NotSupportedException();

		// Token: 0x04000849 RID: 2121
		[CompilerGenerated]
		private ListChangedEventHandler ListChanged;
	}
}
