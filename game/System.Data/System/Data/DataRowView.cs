using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity;

namespace System.Data
{
	/// <summary>Represents a customized view of a <see cref="T:System.Data.DataRow" />.</summary>
	// Token: 0x020000C8 RID: 200
	public class DataRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo, INotifyPropertyChanged
	{
		// Token: 0x06000C26 RID: 3110 RVA: 0x0003236B File Offset: 0x0003056B
		internal DataRowView(DataView dataView, DataRow row)
		{
			this._dataView = dataView;
			this._row = row;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.DataRowView" /> is identical to the specified object.</summary>
		/// <param name="other">An <see cref="T:System.Object" /> to be compared.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="object" /> is a <see cref="T:System.Data.DataRowView" /> and it returns the same row as the current <see cref="T:System.Data.DataRowView" />; otherwise <see langword="false" />.</returns>
		// Token: 0x06000C27 RID: 3111 RVA: 0x00032381 File Offset: 0x00030581
		public override bool Equals(object other)
		{
			return this == other;
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code 1, which represents Boolean <see langword="true" /> if the value of this instance is nonzero; otherwise the integer zero, which represents Boolean <see langword="false" />.</returns>
		// Token: 0x06000C28 RID: 3112 RVA: 0x00032387 File Offset: 0x00030587
		public override int GetHashCode()
		{
			return this.Row.GetHashCode();
		}

		/// <summary>Gets the <see cref="T:System.Data.DataView" /> to which this row belongs.</summary>
		/// <returns>The <see langword="DataView" /> to which this row belongs.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x00032394 File Offset: 0x00030594
		public DataView DataView
		{
			get
			{
				return this._dataView;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0003239C File Offset: 0x0003059C
		internal int ObjectID
		{
			get
			{
				return this._row._objectID;
			}
		}

		/// <summary>Gets or sets a value in a specified column.</summary>
		/// <param name="ndx">The specified column.</param>
		/// <returns>The value of the column.</returns>
		// Token: 0x1700020E RID: 526
		public object this[int ndx]
		{
			get
			{
				return this.Row[ndx, this.RowVersionDefault];
			}
			set
			{
				if (!this._dataView.AllowEdit && !this.IsNew)
				{
					throw ExceptionBuilder.CanNotEdit();
				}
				this.SetColumnValue(this._dataView.Table.Columns[ndx], value);
			}
		}

		/// <summary>Gets or sets a value in a specified column.</summary>
		/// <param name="property">String that contains the specified column.</param>
		/// <returns>The value of the column.</returns>
		// Token: 0x1700020F RID: 527
		public object this[string property]
		{
			get
			{
				DataColumn dataColumn = this._dataView.Table.Columns[property];
				if (dataColumn != null)
				{
					return this.Row[dataColumn, this.RowVersionDefault];
				}
				if (this._dataView.Table.DataSet != null && this._dataView.Table.DataSet.Relations.Contains(property))
				{
					return this.CreateChildView(property);
				}
				throw ExceptionBuilder.PropertyNotFound(property, this._dataView.Table.TableName);
			}
			set
			{
				DataColumn dataColumn = this._dataView.Table.Columns[property];
				if (dataColumn == null)
				{
					throw ExceptionBuilder.SetFailed(property);
				}
				if (!this._dataView.AllowEdit && !this.IsNew)
				{
					throw ExceptionBuilder.CanNotEdit();
				}
				this.SetColumnValue(dataColumn, value);
			}
		}

		/// <summary>Gets the error message for the property with the given name.</summary>
		/// <param name="colName">The name of the property whose error message to get.</param>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		// Token: 0x17000210 RID: 528
		string IDataErrorInfo.this[string colName]
		{
			get
			{
				return this.Row.GetColumnError(colName);
			}
		}

		/// <summary>Gets a message that describes any validation errors for the object.</summary>
		/// <returns>The validation error on the object.</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x000324DF File Offset: 0x000306DF
		string IDataErrorInfo.Error
		{
			get
			{
				return this.Row.RowError;
			}
		}

		/// <summary>Gets the current version description of the <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. Possible values for the <see cref="P:System.Data.DataRowView.RowVersion" /> property are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x000324EC File Offset: 0x000306EC
		public DataRowVersion RowVersion
		{
			get
			{
				return this.RowVersionDefault & (DataRowVersion)(-1025);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x000324FA File Offset: 0x000306FA
		private DataRowVersion RowVersionDefault
		{
			get
			{
				return this.Row.GetDefaultRowVersion(this._dataView.RowStateFilter);
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00032512 File Offset: 0x00030712
		internal int GetRecord()
		{
			return this.Row.GetRecordFromVersion(this.RowVersionDefault);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00032525 File Offset: 0x00030725
		internal bool HasRecord()
		{
			return this.Row.HasVersion(this.RowVersionDefault);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00032538 File Offset: 0x00030738
		internal object GetColumnValue(DataColumn column)
		{
			return this.Row[column, this.RowVersionDefault];
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0003254C File Offset: 0x0003074C
		internal void SetColumnValue(DataColumn column, object value)
		{
			if (this._delayBeginEdit)
			{
				this._delayBeginEdit = false;
				this.Row.BeginEdit();
			}
			if (DataRowVersion.Original == this.RowVersionDefault)
			{
				throw ExceptionBuilder.SetFailed(column.ColumnName);
			}
			this.Row[column] = value;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> and parent.</summary>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object.</param>
		/// <param name="followParent">The parent object.</param>
		/// <returns>A <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		// Token: 0x06000C37 RID: 3127 RVA: 0x0003259C File Offset: 0x0003079C
		public DataView CreateChildView(DataRelation relation, bool followParent)
		{
			if (relation == null || relation.ParentKey.Table != this.DataView.Table)
			{
				throw ExceptionBuilder.CreateChildView();
			}
			RelatedView relatedView;
			if (!followParent)
			{
				int record = this.GetRecord();
				object[] keyValues = relation.ParentKey.GetKeyValues(record);
				relatedView = new RelatedView(relation.ChildColumnsReference, keyValues);
			}
			else
			{
				relatedView = new RelatedView(this, relation.ParentKey, relation.ChildColumnsReference);
			}
			relatedView.SetIndex("", DataViewRowState.CurrentRows, null);
			relatedView.SetDataViewManager(this.DataView.DataViewManager);
			return relatedView;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" />.</summary>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object.</param>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		// Token: 0x06000C38 RID: 3128 RVA: 0x00032629 File Offset: 0x00030829
		public DataView CreateChildView(DataRelation relation)
		{
			return this.CreateChildView(relation, false);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> name and parent.</summary>
		/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name.</param>
		/// <param name="followParent">The parent</param>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		// Token: 0x06000C39 RID: 3129 RVA: 0x00032633 File Offset: 0x00030833
		public DataView CreateChildView(string relationName, bool followParent)
		{
			return this.CreateChildView(this.DataView.Table.ChildRelations[relationName], followParent);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" /> name.</summary>
		/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name.</param>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		// Token: 0x06000C3A RID: 3130 RVA: 0x00032652 File Offset: 0x00030852
		public DataView CreateChildView(string relationName)
		{
			return this.CreateChildView(relationName, false);
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> being viewed.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> being viewed by the <see cref="T:System.Data.DataRowView" />.</returns>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0003265C File Offset: 0x0003085C
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		/// <summary>Begins an edit procedure.</summary>
		// Token: 0x06000C3C RID: 3132 RVA: 0x00032664 File Offset: 0x00030864
		public void BeginEdit()
		{
			this._delayBeginEdit = true;
		}

		/// <summary>Cancels an edit procedure.</summary>
		// Token: 0x06000C3D RID: 3133 RVA: 0x00032670 File Offset: 0x00030870
		public void CancelEdit()
		{
			DataRow row = this.Row;
			if (this.IsNew)
			{
				this._dataView.FinishAddNew(false);
			}
			else
			{
				row.CancelEdit();
			}
			this._delayBeginEdit = false;
		}

		/// <summary>Commits changes to the underlying <see cref="T:System.Data.DataRow" /> and ends the editing session that was begun with <see cref="M:System.Data.DataRowView.BeginEdit" />.  Use <see cref="M:System.Data.DataRowView.CancelEdit" /> to discard the changes made to the <see cref="T:System.Data.DataRow" />.</summary>
		// Token: 0x06000C3E RID: 3134 RVA: 0x000326A7 File Offset: 0x000308A7
		public void EndEdit()
		{
			if (this.IsNew)
			{
				this._dataView.FinishAddNew(true);
			}
			else
			{
				this.Row.EndEdit();
			}
			this._delayBeginEdit = false;
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.DataRowView" /> is new.</summary>
		/// <returns>
		///   <see langword="true" /> if the row is new; otherwise <see langword="false" />.</returns>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x000326D1 File Offset: 0x000308D1
		public bool IsNew
		{
			get
			{
				return this._row == this._dataView._addNewRow;
			}
		}

		/// <summary>Indicates whether the row is in edit mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the row is in edit mode; otherwise <see langword="false" />.</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000326E6 File Offset: 0x000308E6
		public bool IsEdit
		{
			get
			{
				return this.Row.HasVersion(DataRowVersion.Proposed) || this._delayBeginEdit;
			}
		}

		/// <summary>Deletes a row.</summary>
		// Token: 0x06000C41 RID: 3137 RVA: 0x00032702 File Offset: 0x00030902
		public void Delete()
		{
			this._dataView.Delete(this.Row);
		}

		/// <summary>Event that is raised when a <see cref="T:System.Data.DataRowView" /> property is changed.</summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000C42 RID: 3138 RVA: 0x00032718 File Offset: 0x00030918
		// (remove) Token: 0x06000C43 RID: 3139 RVA: 0x00032750 File Offset: 0x00030950
		public event PropertyChangedEventHandler PropertyChanged
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanged;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00032785 File Offset: 0x00030985
		internal void RaisePropertyChangedEvent(string propName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An AttributeCollection containing the attributes for this object.</returns>
		// Token: 0x06000C45 RID: 3141 RVA: 0x0003279E File Offset: 0x0003099E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of this instance of a component.</returns>
		// Token: 0x06000C46 RID: 3142 RVA: 0x00003E32 File Offset: 0x00002032
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of this instance of a component.</returns>
		// Token: 0x06000C47 RID: 3143 RVA: 0x00003E32 File Offset: 0x00002032
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>The type converter for this instance of a component.</returns>
		// Token: 0x06000C48 RID: 3144 RVA: 0x00003E32 File Offset: 0x00002032
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>The default event for this instance of a component.</returns>
		// Token: 0x06000C49 RID: 3145 RVA: 0x00003E32 File Offset: 0x00002032
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>The default property for this instance of a component.</returns>
		// Token: 0x06000C4A RID: 3146 RVA: 0x00003E32 File Offset: 0x00002032
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or <see langword="null" /> if the editor cannot be found.</returns>
		// Token: 0x06000C4B RID: 3147 RVA: 0x00003E32 File Offset: 0x00002032
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>The events for this instance of a component.</returns>
		// Token: 0x06000C4C RID: 3148 RVA: 0x000327A6 File Offset: 0x000309A6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the events for this instance of a component with specified attributes.</summary>
		/// <param name="attributes">The attributes</param>
		/// <returns>The events for this instance of a component.</returns>
		// Token: 0x06000C4D RID: 3149 RVA: 0x000327A6 File Offset: 0x000309A6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>The properties for this instance of a component.</returns>
		// Token: 0x06000C4E RID: 3150 RVA: 0x000327AE File Offset: 0x000309AE
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		/// <summary>Returns the properties for this instance of a component with specified attributes.</summary>
		/// <param name="attributes">The attributes.</param>
		/// <returns>The properties for this instance of a component.</returns>
		// Token: 0x06000C4F RID: 3151 RVA: 0x000327B7 File Offset: 0x000309B7
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._dataView.Table == null)
			{
				return DataRowView.s_zeroPropertyDescriptorCollection;
			}
			return this._dataView.Table.GetPropertyDescriptorCollection(attributes);
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		// Token: 0x06000C50 RID: 3152 RVA: 0x00005696 File Offset: 0x00003896
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000327DD File Offset: 0x000309DD
		// Note: this type is marked as 'beforefieldinit'.
		static DataRowView()
		{
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000108A6 File Offset: 0x0000EAA6
		internal DataRowView()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007F3 RID: 2035
		private readonly DataView _dataView;

		// Token: 0x040007F4 RID: 2036
		private readonly DataRow _row;

		// Token: 0x040007F5 RID: 2037
		private bool _delayBeginEdit;

		// Token: 0x040007F6 RID: 2038
		private static readonly PropertyDescriptorCollection s_zeroPropertyDescriptorCollection = new PropertyDescriptorCollection(null);

		// Token: 0x040007F7 RID: 2039
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanged;
	}
}
