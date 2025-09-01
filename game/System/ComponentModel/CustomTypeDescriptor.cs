using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple default implementation of the <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> interface.</summary>
	// Token: 0x02000393 RID: 915
	public abstract class CustomTypeDescriptor : ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class.</summary>
		// Token: 0x06001DFF RID: 7679 RVA: 0x0000219B File Offset: 0x0000039B
		protected CustomTypeDescriptor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class using a parent custom type descriptor.</summary>
		/// <param name="parent">The parent custom type descriptor.</param>
		// Token: 0x06001E00 RID: 7680 RVA: 0x0006B3B7 File Offset: 0x000695B7
		protected CustomTypeDescriptor(ICustomTypeDescriptor parent)
		{
			this._parent = parent;
		}

		/// <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
		// Token: 0x06001E01 RID: 7681 RVA: 0x0006B3C6 File Offset: 0x000695C6
		public virtual AttributeCollection GetAttributes()
		{
			if (this._parent != null)
			{
				return this._parent.GetAttributes();
			}
			return AttributeCollection.Empty;
		}

		/// <summary>Returns the fully qualified name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the fully qualified class name of the type this type descriptor is describing. The default is <see langword="null" />.</returns>
		// Token: 0x06001E02 RID: 7682 RVA: 0x0006B3E1 File Offset: 0x000695E1
		public virtual string GetClassName()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetClassName();
		}

		/// <summary>Returns the name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the component instance this type descriptor is describing. The default is <see langword="null" />.</returns>
		// Token: 0x06001E03 RID: 7683 RVA: 0x0006B3F4 File Offset: 0x000695F4
		public virtual string GetComponentName()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetComponentName();
		}

		/// <summary>Returns a type converter for the type represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. The default is a newly created <see cref="T:System.ComponentModel.TypeConverter" />.</returns>
		// Token: 0x06001E04 RID: 7684 RVA: 0x0006B407 File Offset: 0x00069607
		public virtual TypeConverter GetConverter()
		{
			if (this._parent != null)
			{
				return this._parent.GetConverter();
			}
			return new TypeConverter();
		}

		/// <summary>Returns the event descriptor for the default event of the object represented by this type descriptor.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> for the default event on the object represented by this type descriptor. The default is <see langword="null" />.</returns>
		// Token: 0x06001E05 RID: 7685 RVA: 0x0006B422 File Offset: 0x00069622
		public virtual EventDescriptor GetDefaultEvent()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetDefaultEvent();
		}

		/// <summary>Returns the property descriptor for the default property of the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the default property on the object represented by this type descriptor. The default is <see langword="null" />.</returns>
		// Token: 0x06001E06 RID: 7686 RVA: 0x0006B435 File Offset: 0x00069635
		public virtual PropertyDescriptor GetDefaultProperty()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetDefaultProperty();
		}

		/// <summary>Returns an editor of the specified type that is to be associated with the class represented by this type descriptor.</summary>
		/// <param name="editorBaseType">The base type of the editor to retrieve.</param>
		/// <returns>An editor of the given type that is to be associated with the class represented by this type descriptor. The default is <see langword="null" />.</returns>
		// Token: 0x06001E07 RID: 7687 RVA: 0x0006B448 File Offset: 0x00069648
		public virtual object GetEditor(Type editorBaseType)
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetEditor(editorBaseType);
		}

		/// <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		// Token: 0x06001E08 RID: 7688 RVA: 0x0006B45C File Offset: 0x0006965C
		public virtual EventDescriptorCollection GetEvents()
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents();
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <param name="attributes">An array of attributes to use as a filter. This can be <see langword="null" />.</param>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		// Token: 0x06001E09 RID: 7689 RVA: 0x0006B477 File Offset: 0x00069677
		public virtual EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents(attributes);
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		// Token: 0x06001E0A RID: 7690 RVA: 0x0006B493 File Offset: 0x00069693
		public virtual PropertyDescriptorCollection GetProperties()
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties();
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <param name="attributes">An array of attributes to use as a filter. This can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		// Token: 0x06001E0B RID: 7691 RVA: 0x0006B4AE File Offset: 0x000696AE
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties(attributes);
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <param name="pd">The property descriptor for which to retrieve the owning object.</param>
		/// <returns>An <see cref="T:System.Object" /> that owns the given property specified by the type descriptor. The default is <see langword="null" />.</returns>
		// Token: 0x06001E0C RID: 7692 RVA: 0x0006B4CA File Offset: 0x000696CA
		public virtual object GetPropertyOwner(PropertyDescriptor pd)
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetPropertyOwner(pd);
		}

		// Token: 0x04000F05 RID: 3845
		private readonly ICustomTypeDescriptor _parent;
	}
}
