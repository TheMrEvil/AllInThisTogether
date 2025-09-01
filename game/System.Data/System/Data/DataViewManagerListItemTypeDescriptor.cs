using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000DA RID: 218
	internal sealed class DataViewManagerListItemTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00037966 File Offset: 0x00035B66
		internal DataViewManagerListItemTypeDescriptor(DataViewManager dataViewManager)
		{
			this._dataViewManager = dataViewManager;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00037975 File Offset: 0x00035B75
		internal void Reset()
		{
			this._propsCollection = null;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003797E File Offset: 0x00035B7E
		internal DataView GetDataView(DataTable table)
		{
			DataView dataView = new DataView(table);
			dataView.SetDataViewManager(this._dataViewManager);
			return dataView;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003279E File Offset: 0x0003099E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x00003E32 File Offset: 0x00002032
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00003E32 File Offset: 0x00002032
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00003E32 File Offset: 0x00002032
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00003E32 File Offset: 0x00002032
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00003E32 File Offset: 0x00002032
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00003E32 File Offset: 0x00002032
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000327A6 File Offset: 0x000309A6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000327A6 File Offset: 0x000309A6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x000327AE File Offset: 0x000309AE
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00037994 File Offset: 0x00035B94
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propsCollection == null)
			{
				PropertyDescriptor[] array = null;
				DataSet dataSet = this._dataViewManager.DataSet;
				if (dataSet != null)
				{
					int count = dataSet.Tables.Count;
					array = new PropertyDescriptor[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = new DataTablePropertyDescriptor(dataSet.Tables[i]);
					}
				}
				this._propsCollection = new PropertyDescriptorCollection(array);
			}
			return this._propsCollection;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00005696 File Offset: 0x00003896
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x0400084A RID: 2122
		private DataViewManager _dataViewManager;

		// Token: 0x0400084B RID: 2123
		private PropertyDescriptorCollection _propsCollection;
	}
}
