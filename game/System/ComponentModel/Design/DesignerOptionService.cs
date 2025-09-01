using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using Unity;

namespace System.ComponentModel.Design
{
	/// <summary>Provides a base class for getting and setting option values for a designer.</summary>
	// Token: 0x0200047B RID: 1147
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class DesignerOptionService : IDesignerOptionService
	{
		/// <summary>Gets the options collection for this service.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> populated with available designer options.</returns>
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x00082365 File Offset: 0x00080565
		public DesignerOptionService.DesignerOptionCollection Options
		{
			get
			{
				if (this._options == null)
				{
					this._options = new DesignerOptionService.DesignerOptionCollection(this, null, string.Empty, null);
				}
				return this._options;
			}
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name and adds it to the given parent.</summary>
		/// <param name="parent">The parent designer option collection. All collections have a parent except the root object collection.</param>
		/// <param name="name">The name of this collection.</param>
		/// <param name="value">The object providing properties for this collection. Can be <see langword="null" /> if the collection should not provide any properties.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> with the given name.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="parent" /> or <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string.</exception>
		// Token: 0x060024BB RID: 9403 RVA: 0x00082388 File Offset: 0x00080588
		protected DesignerOptionService.DesignerOptionCollection CreateOptionCollection(DesignerOptionService.DesignerOptionCollection parent, string name, object value)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.GetString("'{1}' is not a valid value for '{0}'.", new object[]
				{
					name.Length.ToString(CultureInfo.CurrentCulture),
					0.ToString(CultureInfo.CurrentCulture)
				}), "name.Length");
			}
			return new DesignerOptionService.DesignerOptionCollection(this, parent, name, value);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x00082404 File Offset: 0x00080604
		private PropertyDescriptor GetOptionProperty(string pageName, string valueName)
		{
			if (pageName == null)
			{
				throw new ArgumentNullException("pageName");
			}
			if (valueName == null)
			{
				throw new ArgumentNullException("valueName");
			}
			string[] array = pageName.Split(new char[]
			{
				'\\'
			});
			DesignerOptionService.DesignerOptionCollection designerOptionCollection = this.Options;
			foreach (string name in array)
			{
				designerOptionCollection = designerOptionCollection[name];
				if (designerOptionCollection == null)
				{
					return null;
				}
			}
			return designerOptionCollection.Properties[valueName];
		}

		/// <summary>Populates a <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
		/// <param name="options">The collection to populate.</param>
		// Token: 0x060024BD RID: 9405 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void PopulateOptionCollection(DesignerOptionService.DesignerOptionCollection options)
		{
		}

		/// <summary>Shows the options dialog box for the given object.</summary>
		/// <param name="options">The options collection containing the object to be invoked.</param>
		/// <param name="optionObject">The actual options object.</param>
		/// <returns>
		///   <see langword="true" /> if the dialog box is shown; otherwise, <see langword="false" />.</returns>
		// Token: 0x060024BE RID: 9406 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool ShowDialog(DesignerOptionService.DesignerOptionCollection options, object optionObject)
		{
			return false;
		}

		/// <summary>Gets the value of an option defined in this package.</summary>
		/// <param name="pageName">The page to which the option is bound.</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <returns>The value of the option named <paramref name="valueName" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is <see langword="null" />.</exception>
		// Token: 0x060024BF RID: 9407 RVA: 0x00082474 File Offset: 0x00080674
		object IDesignerOptionService.GetOptionValue(string pageName, string valueName)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				return optionProperty.GetValue(null);
			}
			return null;
		}

		/// <summary>Sets the value of an option defined in this package.</summary>
		/// <param name="pageName">The page to which the option is bound</param>
		/// <param name="valueName">The name of the option value.</param>
		/// <param name="value">The value of the option.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pageName" /> or <paramref name="valueName" /> is <see langword="null" />.</exception>
		// Token: 0x060024C0 RID: 9408 RVA: 0x00082498 File Offset: 0x00080698
		void IDesignerOptionService.SetOptionValue(string pageName, string valueName, object value)
		{
			PropertyDescriptor optionProperty = this.GetOptionProperty(pageName, valueName);
			if (optionProperty != null)
			{
				optionProperty.SetValue(null, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerOptionService" /> class.</summary>
		// Token: 0x060024C1 RID: 9409 RVA: 0x0000219B File Offset: 0x0000039B
		protected DesignerOptionService()
		{
		}

		// Token: 0x04001488 RID: 5256
		private DesignerOptionService.DesignerOptionCollection _options;

		/// <summary>Contains a collection of designer options. This class cannot be inherited.</summary>
		// Token: 0x0200047C RID: 1148
		[TypeConverter(typeof(DesignerOptionService.DesignerOptionConverter))]
		[Editor("", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public sealed class DesignerOptionCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060024C2 RID: 9410 RVA: 0x000824BC File Offset: 0x000806BC
			internal DesignerOptionCollection(DesignerOptionService service, DesignerOptionService.DesignerOptionCollection parent, string name, object value)
			{
				this._service = service;
				this._parent = parent;
				this._name = name;
				this._value = value;
				if (this._parent != null)
				{
					if (this._parent._children == null)
					{
						this._parent._children = new ArrayList(1);
					}
					this._parent._children.Add(this);
				}
			}

			/// <summary>Gets the number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</summary>
			/// <returns>The number of child option collections this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" /> contains.</returns>
			// Token: 0x17000766 RID: 1894
			// (get) Token: 0x060024C3 RID: 9411 RVA: 0x00082524 File Offset: 0x00080724
			public int Count
			{
				get
				{
					this.EnsurePopulated();
					return this._children.Count;
				}
			}

			/// <summary>Gets the name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>The name of this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</returns>
			// Token: 0x17000767 RID: 1895
			// (get) Token: 0x060024C4 RID: 9412 RVA: 0x00082537 File Offset: 0x00080737
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			/// <summary>Gets the parent collection object.</summary>
			/// <returns>The parent collection object, or <see langword="null" /> if there is no parent.</returns>
			// Token: 0x17000768 RID: 1896
			// (get) Token: 0x060024C5 RID: 9413 RVA: 0x0008253F File Offset: 0x0008073F
			public DesignerOptionService.DesignerOptionCollection Parent
			{
				get
				{
					return this._parent;
				}
			}

			/// <summary>Gets the collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</summary>
			/// <returns>The collection of properties offered by this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />, along with all of its children.</returns>
			// Token: 0x17000769 RID: 1897
			// (get) Token: 0x060024C6 RID: 9414 RVA: 0x00082548 File Offset: 0x00080748
			public PropertyDescriptorCollection Properties
			{
				get
				{
					if (this._properties == null)
					{
						ArrayList arrayList;
						if (this._value != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this._value);
							arrayList = new ArrayList(properties.Count);
							using (IEnumerator enumerator = properties.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									PropertyDescriptor property = (PropertyDescriptor)obj;
									arrayList.Add(new DesignerOptionService.DesignerOptionCollection.WrappedPropertyDescriptor(property, this._value));
								}
								goto IL_76;
							}
						}
						arrayList = new ArrayList(1);
						IL_76:
						this.EnsurePopulated();
						foreach (object obj2 in this._children)
						{
							DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj2;
							arrayList.AddRange(designerOptionCollection.Properties);
						}
						PropertyDescriptor[] properties2 = (PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor));
						this._properties = new PropertyDescriptorCollection(properties2, true);
					}
					return this._properties;
				}
			}

			/// <summary>Gets the child collection at the given index.</summary>
			/// <param name="index">The zero-based index of the child collection to get.</param>
			/// <returns>The child collection at the specified index.</returns>
			// Token: 0x1700076A RID: 1898
			public DesignerOptionService.DesignerOptionCollection this[int index]
			{
				get
				{
					this.EnsurePopulated();
					if (index < 0 || index >= this._children.Count)
					{
						throw new IndexOutOfRangeException("index");
					}
					return (DesignerOptionService.DesignerOptionCollection)this._children[index];
				}
			}

			/// <summary>Gets the child collection at the given name.</summary>
			/// <param name="name">The name of the child collection.</param>
			/// <returns>The child collection with the name specified by the <paramref name="name" /> parameter, or <see langword="null" /> if the name is not found.</returns>
			// Token: 0x1700076B RID: 1899
			public DesignerOptionService.DesignerOptionCollection this[string name]
			{
				get
				{
					this.EnsurePopulated();
					foreach (object obj in this._children)
					{
						DesignerOptionService.DesignerOptionCollection designerOptionCollection = (DesignerOptionService.DesignerOptionCollection)obj;
						if (string.Compare(designerOptionCollection.Name, name, true, CultureInfo.InvariantCulture) == 0)
						{
							return designerOptionCollection;
						}
					}
					return null;
				}
			}

			/// <summary>Copies the entire collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The <paramref name="array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x060024C9 RID: 9417 RVA: 0x00082708 File Offset: 0x00080908
			public void CopyTo(Array array, int index)
			{
				this.EnsurePopulated();
				this._children.CopyTo(array, index);
			}

			// Token: 0x060024CA RID: 9418 RVA: 0x0008271D File Offset: 0x0008091D
			private void EnsurePopulated()
			{
				if (this._children == null)
				{
					this._service.PopulateOptionCollection(this);
					if (this._children == null)
					{
						this._children = new ArrayList(1);
					}
				}
			}

			/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate this collection.</returns>
			// Token: 0x060024CB RID: 9419 RVA: 0x00082747 File Offset: 0x00080947
			public IEnumerator GetEnumerator()
			{
				this.EnsurePopulated();
				return this._children.GetEnumerator();
			}

			/// <summary>Returns the index of the first occurrence of a given value in a range of this collection.</summary>
			/// <param name="value">The object to locate in the collection.</param>
			/// <returns>The index of the first occurrence of value within the entire collection, if found; otherwise, the lower bound of the collection minus 1.</returns>
			// Token: 0x060024CC RID: 9420 RVA: 0x0008275A File Offset: 0x0008095A
			public int IndexOf(DesignerOptionService.DesignerOptionCollection value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			// Token: 0x060024CD RID: 9421 RVA: 0x00082770 File Offset: 0x00080970
			private static object RecurseFindValue(DesignerOptionService.DesignerOptionCollection options)
			{
				if (options._value != null)
				{
					return options._value;
				}
				foreach (object obj in options)
				{
					object obj2 = DesignerOptionService.DesignerOptionCollection.RecurseFindValue((DesignerOptionService.DesignerOptionCollection)obj);
					if (obj2 != null)
					{
						return obj2;
					}
				}
				return null;
			}

			/// <summary>Displays a dialog box user interface (UI) with which the user can configure the options in this <see cref="T:System.ComponentModel.Design.DesignerOptionService.DesignerOptionCollection" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the dialog box can be displayed; otherwise, <see langword="false" />.</returns>
			// Token: 0x060024CE RID: 9422 RVA: 0x000827DC File Offset: 0x000809DC
			public bool ShowDialog()
			{
				object obj = DesignerOptionService.DesignerOptionCollection.RecurseFindValue(this);
				return obj != null && this._service.ShowDialog(this, obj);
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized and, therefore, thread safe.</summary>
			/// <returns>
			///   <see langword="true" /> if the access to the collection is synchronized; otherwise, <see langword="false" />.</returns>
			// Token: 0x1700076C RID: 1900
			// (get) Token: 0x060024CF RID: 9423 RVA: 0x00003062 File Offset: 0x00001262
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x1700076D RID: 1901
			// (get) Token: 0x060024D0 RID: 9424 RVA: 0x000075E1 File Offset: 0x000057E1
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection has a fixed size; otherwise, <see langword="false" />.</returns>
			// Token: 0x1700076E RID: 1902
			// (get) Token: 0x060024D1 RID: 9425 RVA: 0x0000390E File Offset: 0x00001B0E
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
			// Token: 0x1700076F RID: 1903
			// (get) Token: 0x060024D2 RID: 9426 RVA: 0x0000390E File Offset: 0x00001B0E
			bool IList.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			/// <summary>Gets or sets the element at the specified index.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>The element at the specified index.</returns>
			// Token: 0x17000770 RID: 1904
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			/// <returns>The position into which the new element was inserted.</returns>
			// Token: 0x060024D5 RID: 9429 RVA: 0x000044FA File Offset: 0x000026FA
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x060024D6 RID: 9430 RVA: 0x000044FA File Offset: 0x000026FA
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			/// <summary>Determines whether the collection contains a specific value.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x060024D7 RID: 9431 RVA: 0x0008280B File Offset: 0x00080A0B
			bool IList.Contains(object value)
			{
				this.EnsurePopulated();
				return this._children.Contains(value);
			}

			/// <summary>Determines the index of a specific item in the collection.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
			/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
			// Token: 0x060024D8 RID: 9432 RVA: 0x0008275A File Offset: 0x0008095A
			int IList.IndexOf(object value)
			{
				this.EnsurePopulated();
				return this._children.IndexOf(value);
			}

			/// <summary>Inserts an item into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
			// Token: 0x060024D9 RID: 9433 RVA: 0x000044FA File Offset: 0x000026FA
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
			// Token: 0x060024DA RID: 9434 RVA: 0x000044FA File Offset: 0x000026FA
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes the collection item at the specified index.</summary>
			/// <param name="index">The zero-based index of the item to remove.</param>
			// Token: 0x060024DB RID: 9435 RVA: 0x000044FA File Offset: 0x000026FA
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060024DC RID: 9436 RVA: 0x00013BCA File Offset: 0x00011DCA
			internal DesignerOptionCollection()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04001489 RID: 5257
			private DesignerOptionService _service;

			// Token: 0x0400148A RID: 5258
			private DesignerOptionService.DesignerOptionCollection _parent;

			// Token: 0x0400148B RID: 5259
			private string _name;

			// Token: 0x0400148C RID: 5260
			private object _value;

			// Token: 0x0400148D RID: 5261
			private ArrayList _children;

			// Token: 0x0400148E RID: 5262
			private PropertyDescriptorCollection _properties;

			// Token: 0x0200047D RID: 1149
			private sealed class WrappedPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x060024DD RID: 9437 RVA: 0x0008281F File Offset: 0x00080A1F
				internal WrappedPropertyDescriptor(PropertyDescriptor property, object target) : base(property.Name, null)
				{
					this.property = property;
					this.target = target;
				}

				// Token: 0x17000771 RID: 1905
				// (get) Token: 0x060024DE RID: 9438 RVA: 0x0008283C File Offset: 0x00080A3C
				public override AttributeCollection Attributes
				{
					get
					{
						return this.property.Attributes;
					}
				}

				// Token: 0x17000772 RID: 1906
				// (get) Token: 0x060024DF RID: 9439 RVA: 0x00082849 File Offset: 0x00080A49
				public override Type ComponentType
				{
					get
					{
						return this.property.ComponentType;
					}
				}

				// Token: 0x17000773 RID: 1907
				// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00082856 File Offset: 0x00080A56
				public override bool IsReadOnly
				{
					get
					{
						return this.property.IsReadOnly;
					}
				}

				// Token: 0x17000774 RID: 1908
				// (get) Token: 0x060024E1 RID: 9441 RVA: 0x00082863 File Offset: 0x00080A63
				public override Type PropertyType
				{
					get
					{
						return this.property.PropertyType;
					}
				}

				// Token: 0x060024E2 RID: 9442 RVA: 0x00082870 File Offset: 0x00080A70
				public override bool CanResetValue(object component)
				{
					return this.property.CanResetValue(this.target);
				}

				// Token: 0x060024E3 RID: 9443 RVA: 0x00082883 File Offset: 0x00080A83
				public override object GetValue(object component)
				{
					return this.property.GetValue(this.target);
				}

				// Token: 0x060024E4 RID: 9444 RVA: 0x00082896 File Offset: 0x00080A96
				public override void ResetValue(object component)
				{
					this.property.ResetValue(this.target);
				}

				// Token: 0x060024E5 RID: 9445 RVA: 0x000828A9 File Offset: 0x00080AA9
				public override void SetValue(object component, object value)
				{
					this.property.SetValue(this.target, value);
				}

				// Token: 0x060024E6 RID: 9446 RVA: 0x000828BD File Offset: 0x00080ABD
				public override bool ShouldSerializeValue(object component)
				{
					return this.property.ShouldSerializeValue(this.target);
				}

				// Token: 0x0400148F RID: 5263
				private object target;

				// Token: 0x04001490 RID: 5264
				private PropertyDescriptor property;
			}
		}

		// Token: 0x0200047E RID: 1150
		internal sealed class DesignerOptionConverter : TypeConverter
		{
			// Token: 0x060024E7 RID: 9447 RVA: 0x0000390E File Offset: 0x00001B0E
			public override bool GetPropertiesSupported(ITypeDescriptorContext cxt)
			{
				return true;
			}

			// Token: 0x060024E8 RID: 9448 RVA: 0x000828D0 File Offset: 0x00080AD0
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext cxt, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
				DesignerOptionService.DesignerOptionCollection designerOptionCollection = value as DesignerOptionService.DesignerOptionCollection;
				if (designerOptionCollection == null)
				{
					return propertyDescriptorCollection;
				}
				foreach (object obj in designerOptionCollection)
				{
					DesignerOptionService.DesignerOptionCollection option = (DesignerOptionService.DesignerOptionCollection)obj;
					propertyDescriptorCollection.Add(new DesignerOptionService.DesignerOptionConverter.OptionPropertyDescriptor(option));
				}
				foreach (object obj2 in designerOptionCollection.Properties)
				{
					PropertyDescriptor value2 = (PropertyDescriptor)obj2;
					propertyDescriptorCollection.Add(value2);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x060024E9 RID: 9449 RVA: 0x00082990 File Offset: 0x00080B90
			public override object ConvertTo(ITypeDescriptorContext cxt, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					return SR.GetString("(Collection)");
				}
				return base.ConvertTo(cxt, culture, value, destinationType);
			}

			// Token: 0x060024EA RID: 9450 RVA: 0x00018550 File Offset: 0x00016750
			public DesignerOptionConverter()
			{
			}

			// Token: 0x0200047F RID: 1151
			private class OptionPropertyDescriptor : PropertyDescriptor
			{
				// Token: 0x060024EB RID: 9451 RVA: 0x000829BB File Offset: 0x00080BBB
				internal OptionPropertyDescriptor(DesignerOptionService.DesignerOptionCollection option) : base(option.Name, null)
				{
					this._option = option;
				}

				// Token: 0x17000775 RID: 1909
				// (get) Token: 0x060024EC RID: 9452 RVA: 0x000829D1 File Offset: 0x00080BD1
				public override Type ComponentType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x17000776 RID: 1910
				// (get) Token: 0x060024ED RID: 9453 RVA: 0x0000390E File Offset: 0x00001B0E
				public override bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17000777 RID: 1911
				// (get) Token: 0x060024EE RID: 9454 RVA: 0x000829D1 File Offset: 0x00080BD1
				public override Type PropertyType
				{
					get
					{
						return this._option.GetType();
					}
				}

				// Token: 0x060024EF RID: 9455 RVA: 0x00003062 File Offset: 0x00001262
				public override bool CanResetValue(object component)
				{
					return false;
				}

				// Token: 0x060024F0 RID: 9456 RVA: 0x000829DE File Offset: 0x00080BDE
				public override object GetValue(object component)
				{
					return this._option;
				}

				// Token: 0x060024F1 RID: 9457 RVA: 0x00003917 File Offset: 0x00001B17
				public override void ResetValue(object component)
				{
				}

				// Token: 0x060024F2 RID: 9458 RVA: 0x00003917 File Offset: 0x00001B17
				public override void SetValue(object component, object value)
				{
				}

				// Token: 0x060024F3 RID: 9459 RVA: 0x00003062 File Offset: 0x00001262
				public override bool ShouldSerializeValue(object component)
				{
					return false;
				}

				// Token: 0x04001491 RID: 5265
				private DesignerOptionService.DesignerOptionCollection _option;
			}
		}
	}
}
