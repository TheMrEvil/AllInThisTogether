using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides an abstraction of a property on a class.</summary>
	// Token: 0x020003DF RID: 991
	public abstract class PropertyDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the specified name and attributes.</summary>
		/// <param name="name">The name of the property.</param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the property attributes.</param>
		// Token: 0x0600204C RID: 8268 RVA: 0x0006C2CB File Offset: 0x0006A4CB
		protected PropertyDescriptor(string name, Attribute[] attrs) : base(name, attrs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name and attributes in the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the property and its attributes.</param>
		// Token: 0x0600204D RID: 8269 RVA: 0x0006C2D5 File Offset: 0x0006A4D5
		protected PropertyDescriptor(MemberDescriptor descr) : base(descr)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> containing the name of the member and its attributes.</param>
		/// <param name="attrs">An <see cref="T:System.Attribute" /> array containing the attributes you want to associate with the property.</param>
		// Token: 0x0600204E RID: 8270 RVA: 0x0006C2DE File Offset: 0x0006A4DE
		protected PropertyDescriptor(MemberDescriptor descr, Attribute[] attrs) : base(descr, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of the component this property is bound to.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.</returns>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600204F RID: 8271
		public abstract Type ComponentType { get; }

		/// <summary>Gets the type converter for this property.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is used to convert the <see cref="T:System.Type" /> of this property.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x0006FD18 File Offset: 0x0006DF18
		public virtual TypeConverter Converter
		{
			get
			{
				AttributeCollection attributes = this.Attributes;
				if (this._converter == null)
				{
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)attributes[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute.ConverterTypeName != null && typeConverterAttribute.ConverterTypeName.Length > 0)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							this._converter = (TypeConverter)this.CreateInstance(typeFromName);
						}
					}
					if (this._converter == null)
					{
						this._converter = TypeDescriptor.GetConverter(this.PropertyType);
					}
				}
				return this._converter;
			}
		}

		/// <summary>Gets a value indicating whether this property should be localized, as specified in the <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the member is marked with the <see cref="T:System.ComponentModel.LocalizableAttribute" /> set to <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002051 RID: 8273 RVA: 0x0006FDB9 File Offset: 0x0006DFB9
		public virtual bool IsLocalizable
		{
			get
			{
				return LocalizableAttribute.Yes.Equals(this.Attributes[typeof(LocalizableAttribute)]);
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether this property is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the property is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002052 RID: 8274
		public abstract bool IsReadOnly { get; }

		/// <summary>Gets a value indicating whether this property should be serialized, as specified in the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> enumeration values that specifies whether this property should be serialized.</returns>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x0006FDDA File Offset: 0x0006DFDA
		public DesignerSerializationVisibility SerializationVisibility
		{
			get
			{
				return ((DesignerSerializationVisibilityAttribute)this.Attributes[typeof(DesignerSerializationVisibilityAttribute)]).Visibility;
			}
		}

		/// <summary>When overridden in a derived class, gets the type of the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06002054 RID: 8276
		public abstract Type PropertyType { get; }

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to add the handler for.</param>
		/// <param name="handler">The delegate to add as a listener.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is <see langword="null" />.</exception>
		// Token: 0x06002055 RID: 8277 RVA: 0x0006FDFC File Offset: 0x0006DFFC
		public virtual void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this._valueChangedHandlers == null)
			{
				this._valueChangedHandlers = new Hashtable();
			}
			EventHandler a = (EventHandler)this._valueChangedHandlers[component];
			this._valueChangedHandlers[component] = Delegate.Combine(a, handler);
		}

		/// <summary>When overridden in a derived class, returns whether resetting an object changes its value.</summary>
		/// <param name="component">The component to test for reset capability.</param>
		/// <returns>
		///   <see langword="true" /> if resetting the component changes its value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002056 RID: 8278
		public abstract bool CanResetValue(object component);

		/// <summary>Compares this to another object to see if they are equivalent.</summary>
		/// <param name="obj">The object to compare to this <see cref="T:System.ComponentModel.PropertyDescriptor" />.</param>
		/// <returns>
		///   <see langword="true" /> if the values are equivalent; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002057 RID: 8279 RVA: 0x0006FE60 File Offset: 0x0006E060
		public override bool Equals(object obj)
		{
			try
			{
				if (obj == this)
				{
					return true;
				}
				if (obj == null)
				{
					return false;
				}
				PropertyDescriptor propertyDescriptor = obj as PropertyDescriptor;
				if (propertyDescriptor != null && propertyDescriptor.NameHashCode == this.NameHashCode && propertyDescriptor.PropertyType == this.PropertyType && propertyDescriptor.Name.Equals(this.Name))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		/// <summary>Creates an instance of the specified type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create.</param>
		/// <returns>A new instance of the type.</returns>
		// Token: 0x06002058 RID: 8280 RVA: 0x0006FED8 File Offset: 0x0006E0D8
		protected object CreateInstance(Type type)
		{
			Type[] array = new Type[]
			{
				typeof(Type)
			};
			if (type.GetConstructor(array) != null)
			{
				return TypeDescriptor.CreateInstance(null, type, array, new object[]
				{
					this.PropertyType
				});
			}
			return TypeDescriptor.CreateInstance(null, type, null, null);
		}

		/// <summary>Adds the attributes of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the specified list of attributes in the parent class.</summary>
		/// <param name="attributeList">An <see cref="T:System.Collections.IList" /> that lists the attributes in the parent class. Initially, this is empty.</param>
		// Token: 0x06002059 RID: 8281 RVA: 0x0006FF29 File Offset: 0x0006E129
		protected override void FillAttributes(IList attributeList)
		{
			this._converter = null;
			this._editors = null;
			this._editorTypes = null;
			this._editorCount = 0;
			base.FillAttributes(attributeList);
		}

		/// <summary>Returns the default <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />.</summary>
		/// <returns>A collection of property descriptor.</returns>
		// Token: 0x0600205A RID: 8282 RVA: 0x0006FF4E File Offset: 0x0006E14E
		public PropertyDescriptorCollection GetChildProperties()
		{
			return this.GetChildProperties(null, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> using a specified array of attributes as a filter.</summary>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes.</returns>
		// Token: 0x0600205B RID: 8283 RVA: 0x0006FF58 File Offset: 0x0006E158
		public PropertyDescriptorCollection GetChildProperties(Attribute[] filter)
		{
			return this.GetChildProperties(null, filter);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object.</summary>
		/// <param name="instance">A component to get the properties for.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for the specified component.</returns>
		// Token: 0x0600205C RID: 8284 RVA: 0x0006FF62 File Offset: 0x0006E162
		public PropertyDescriptorCollection GetChildProperties(object instance)
		{
			return this.GetChildProperties(instance, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object using a specified array of attributes as a filter.</summary>
		/// <param name="instance">A component to get the properties for.</param>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for the specified component.</returns>
		// Token: 0x0600205D RID: 8285 RVA: 0x0006FF6C File Offset: 0x0006E16C
		public virtual PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
		{
			if (instance == null)
			{
				return TypeDescriptor.GetProperties(this.PropertyType, filter);
			}
			return TypeDescriptor.GetProperties(instance, filter);
		}

		/// <summary>Gets an editor of the specified type.</summary>
		/// <param name="editorBaseType">The base type of editor, which is used to differentiate between multiple editors that a property supports.</param>
		/// <returns>An instance of the requested editor type, or <see langword="null" /> if an editor cannot be found.</returns>
		// Token: 0x0600205E RID: 8286 RVA: 0x0006FF88 File Offset: 0x0006E188
		public virtual object GetEditor(Type editorBaseType)
		{
			object obj = null;
			AttributeCollection attributes = this.Attributes;
			if (this._editorTypes != null)
			{
				for (int i = 0; i < this._editorCount; i++)
				{
					if (this._editorTypes[i] == editorBaseType)
					{
						return this._editors[i];
					}
				}
			}
			if (obj == null)
			{
				for (int j = 0; j < attributes.Count; j++)
				{
					EditorAttribute editorAttribute = attributes[j] as EditorAttribute;
					if (editorAttribute != null)
					{
						Type typeFromName = this.GetTypeFromName(editorAttribute.EditorBaseTypeName);
						if (editorBaseType == typeFromName)
						{
							Type typeFromName2 = this.GetTypeFromName(editorAttribute.EditorTypeName);
							if (typeFromName2 != null)
							{
								obj = this.CreateInstance(typeFromName2);
								break;
							}
						}
					}
				}
				if (obj == null)
				{
					obj = TypeDescriptor.GetEditor(this.PropertyType, editorBaseType);
				}
				if (this._editorTypes == null)
				{
					this._editorTypes = new Type[5];
					this._editors = new object[5];
				}
				if (this._editorCount >= this._editorTypes.Length)
				{
					Type[] array = new Type[this._editorTypes.Length * 2];
					object[] array2 = new object[this._editors.Length * 2];
					Array.Copy(this._editorTypes, array, this._editorTypes.Length);
					Array.Copy(this._editors, array2, this._editors.Length);
					this._editorTypes = array;
					this._editors = array2;
				}
				this._editorTypes[this._editorCount] = editorBaseType;
				object[] editors = this._editors;
				int editorCount = this._editorCount;
				this._editorCount = editorCount + 1;
				editors[editorCount] = obj;
			}
			return obj;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for this object.</returns>
		// Token: 0x0600205F RID: 8287 RVA: 0x000700FD File Offset: 0x0006E2FD
		public override int GetHashCode()
		{
			return this.NameHashCode ^ this.PropertyType.GetHashCode();
		}

		/// <summary>This method returns the object that should be used during invocation of members.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the invocation target.</param>
		/// <param name="instance">The potential invocation target.</param>
		/// <returns>The <see cref="T:System.Object" /> that should be used during invocation of members.</returns>
		// Token: 0x06002060 RID: 8288 RVA: 0x00070114 File Offset: 0x0006E314
		protected override object GetInvocationTarget(Type type, object instance)
		{
			object obj = base.GetInvocationTarget(type, instance);
			ICustomTypeDescriptor customTypeDescriptor = obj as ICustomTypeDescriptor;
			if (customTypeDescriptor != null)
			{
				obj = customTypeDescriptor.GetPropertyOwner(this);
			}
			return obj;
		}

		/// <summary>Returns a type using its name.</summary>
		/// <param name="typeName">The assembly-qualified name of the type to retrieve.</param>
		/// <returns>A <see cref="T:System.Type" /> that matches the given type name, or <see langword="null" /> if a match cannot be found.</returns>
		// Token: 0x06002061 RID: 8289 RVA: 0x00070140 File Offset: 0x0006E340
		protected Type GetTypeFromName(string typeName)
		{
			if (typeName == null || typeName.Length == 0)
			{
				return null;
			}
			Type type = Type.GetType(typeName);
			Type type2 = null;
			if (this.ComponentType != null && (type == null || this.ComponentType.Assembly.FullName.Equals(type.Assembly.FullName)))
			{
				int num = typeName.IndexOf(',');
				if (num != -1)
				{
					typeName = typeName.Substring(0, num);
				}
				type2 = this.ComponentType.Assembly.GetType(typeName);
			}
			return type2 ?? type;
		}

		/// <summary>When overridden in a derived class, gets the current value of the property on a component.</summary>
		/// <param name="component">The component with the property for which to retrieve the value.</param>
		/// <returns>The value of a property for a given component.</returns>
		// Token: 0x06002062 RID: 8290
		public abstract object GetValue(object component);

		/// <summary>Raises the <c>ValueChanged</c> event that you implemented.</summary>
		/// <param name="component">The object that raises the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002063 RID: 8291 RVA: 0x000701CB File Offset: 0x0006E3CB
		protected virtual void OnValueChanged(object component, EventArgs e)
		{
			if (component != null)
			{
				Hashtable valueChangedHandlers = this._valueChangedHandlers;
				EventHandler eventHandler = (EventHandler)((valueChangedHandlers != null) ? valueChangedHandlers[component] : null);
				if (eventHandler == null)
				{
					return;
				}
				eventHandler(component, e);
			}
		}

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to remove the handler for.</param>
		/// <param name="handler">The delegate to remove as a listener.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is <see langword="null" />.</exception>
		// Token: 0x06002064 RID: 8292 RVA: 0x000701F4 File Offset: 0x0006E3F4
		public virtual void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this._valueChangedHandlers != null)
			{
				EventHandler eventHandler = (EventHandler)this._valueChangedHandlers[component];
				eventHandler = (EventHandler)Delegate.Remove(eventHandler, handler);
				if (eventHandler != null)
				{
					this._valueChangedHandlers[component] = eventHandler;
					return;
				}
				this._valueChangedHandlers.Remove(component);
			}
		}

		/// <summary>Retrieves the current set of <c>ValueChanged</c> event handlers for a specific component</summary>
		/// <param name="component">The component for which to retrieve event handlers.</param>
		/// <returns>A combined multicast event handler, or <see langword="null" /> if no event handlers are currently assigned to <paramref name="component" />.</returns>
		// Token: 0x06002065 RID: 8293 RVA: 0x00070261 File Offset: 0x0006E461
		protected internal EventHandler GetValueChangedHandler(object component)
		{
			if (component != null && this._valueChangedHandlers != null)
			{
				return (EventHandler)this._valueChangedHandlers[component];
			}
			return null;
		}

		/// <summary>When overridden in a derived class, resets the value for this property of the component to the default value.</summary>
		/// <param name="component">The component with the property value that is to be reset to the default value.</param>
		// Token: 0x06002066 RID: 8294
		public abstract void ResetValue(object component);

		/// <summary>When overridden in a derived class, sets the value of the component to a different value.</summary>
		/// <param name="component">The component with the property value that is to be set.</param>
		/// <param name="value">The new value.</param>
		// Token: 0x06002067 RID: 8295
		public abstract void SetValue(object component, object value);

		/// <summary>When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.</summary>
		/// <param name="component">The component with the property to be examined for persistence.</param>
		/// <returns>
		///   <see langword="true" /> if the property should be persisted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002068 RID: 8296
		public abstract bool ShouldSerializeValue(object component);

		/// <summary>Gets a value indicating whether value change notifications for this property may originate from outside the property descriptor.</summary>
		/// <returns>
		///   <see langword="true" /> if value change notifications may originate from outside the property descriptor; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06002069 RID: 8297 RVA: 0x00003062 File Offset: 0x00001262
		public virtual bool SupportsChangeEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000FBF RID: 4031
		private TypeConverter _converter;

		// Token: 0x04000FC0 RID: 4032
		private Hashtable _valueChangedHandlers;

		// Token: 0x04000FC1 RID: 4033
		private object[] _editors;

		// Token: 0x04000FC2 RID: 4034
		private Type[] _editorTypes;

		// Token: 0x04000FC3 RID: 4035
		private int _editorCount;
	}
}
