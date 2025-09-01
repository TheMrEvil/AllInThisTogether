using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of attributes.</summary>
	// Token: 0x0200037D RID: 893
	public class AttributeCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeCollection" /> class.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributes" /> is <see langword="null" />.</exception>
		// Token: 0x06001D5C RID: 7516 RVA: 0x00068A38 File Offset: 0x00066C38
		public AttributeCollection(params Attribute[] attributes)
		{
			this._attributes = (attributes ?? Array.Empty<Attribute>());
			for (int i = 0; i < this._attributes.Length; i++)
			{
				if (this._attributes[i] == null)
				{
					throw new ArgumentNullException("attributes");
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeCollection" /> class.</summary>
		// Token: 0x06001D5D RID: 7517 RVA: 0x0000219B File Offset: 0x0000039B
		protected AttributeCollection()
		{
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.AttributeCollection" /> from an existing <see cref="T:System.ComponentModel.AttributeCollection" />.</summary>
		/// <param name="existing">An <see cref="T:System.ComponentModel.AttributeCollection" /> from which to create the copy.</param>
		/// <param name="newAttributes">An array of type <see cref="T:System.Attribute" /> that provides the attributes for this collection. Can be <see langword="null" />.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.AttributeCollection" /> that is a copy of <paramref name="existing" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="existing" /> is <see langword="null" />.</exception>
		// Token: 0x06001D5E RID: 7518 RVA: 0x00068A84 File Offset: 0x00066C84
		public static AttributeCollection FromExisting(AttributeCollection existing, params Attribute[] newAttributes)
		{
			if (existing == null)
			{
				throw new ArgumentNullException("existing");
			}
			if (newAttributes == null)
			{
				newAttributes = Array.Empty<Attribute>();
			}
			Attribute[] array = new Attribute[existing.Count + newAttributes.Length];
			int count = existing.Count;
			existing.CopyTo(array, 0);
			for (int i = 0; i < newAttributes.Length; i++)
			{
				if (newAttributes[i] == null)
				{
					throw new ArgumentNullException("newAttributes");
				}
				bool flag = false;
				for (int j = 0; j < existing.Count; j++)
				{
					if (array[j].TypeId.Equals(newAttributes[i].TypeId))
					{
						flag = true;
						array[j] = newAttributes[i];
						break;
					}
				}
				if (!flag)
				{
					array[count++] = newAttributes[i];
				}
			}
			Attribute[] array2;
			if (count < array.Length)
			{
				array2 = new Attribute[count];
				Array.Copy(array, 0, array2, 0, count);
			}
			else
			{
				array2 = array;
			}
			return new AttributeCollection(array2);
		}

		/// <summary>Gets the attribute collection.</summary>
		/// <returns>The attribute collection.</returns>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00068B51 File Offset: 0x00066D51
		protected virtual Attribute[] Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		/// <summary>Gets the number of attributes.</summary>
		/// <returns>The number of attributes.</returns>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x00068B59 File Offset: 0x00066D59
		public int Count
		{
			get
			{
				return this.Attributes.Length;
			}
		}

		/// <summary>Gets the attribute with the specified index number.</summary>
		/// <param name="index">The zero-based index of <see cref="T:System.ComponentModel.AttributeCollection" />.</param>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified index number.</returns>
		// Token: 0x170005EA RID: 1514
		public virtual Attribute this[int index]
		{
			get
			{
				return this.Attributes[index];
			}
		}

		/// <summary>Gets the attribute with the specified type.</summary>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the <see cref="T:System.Attribute" /> to get from the collection.</param>
		/// <returns>The <see cref="T:System.Attribute" /> with the specified type or, if the attribute does not exist, the default value for the attribute type.</returns>
		// Token: 0x170005EB RID: 1515
		public virtual Attribute this[Type attributeType]
		{
			get
			{
				object obj = AttributeCollection.s_internalSyncObject;
				Attribute defaultAttribute;
				lock (obj)
				{
					if (this._foundAttributeTypes == null)
					{
						this._foundAttributeTypes = new AttributeCollection.AttributeEntry[5];
					}
					int i = 0;
					while (i < 5)
					{
						if (this._foundAttributeTypes[i].type == attributeType)
						{
							int index = this._foundAttributeTypes[i].index;
							if (index != -1)
							{
								return this.Attributes[index];
							}
							return this.GetDefaultAttribute(attributeType);
						}
						else
						{
							if (this._foundAttributeTypes[i].type == null)
							{
								break;
							}
							i++;
						}
					}
					int index2 = this._index;
					this._index = index2 + 1;
					i = index2;
					if (this._index >= 5)
					{
						this._index = 0;
					}
					this._foundAttributeTypes[i].type = attributeType;
					int num = this.Attributes.Length;
					for (int j = 0; j < num; j++)
					{
						Attribute attribute = this.Attributes[j];
						if (attribute.GetType() == attributeType)
						{
							this._foundAttributeTypes[i].index = j;
							return attribute;
						}
					}
					for (int k = 0; k < num; k++)
					{
						Attribute attribute2 = this.Attributes[k];
						if (attributeType.IsInstanceOfType(attribute2))
						{
							this._foundAttributeTypes[i].index = k;
							return attribute2;
						}
					}
					this._foundAttributeTypes[i].index = -1;
					defaultAttribute = this.GetDefaultAttribute(attributeType);
				}
				return defaultAttribute;
			}
		}

		/// <summary>Determines whether this collection of attributes has the specified attribute.</summary>
		/// <param name="attribute">An <see cref="T:System.Attribute" /> to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the attribute or is the default attribute for the type of attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D63 RID: 7523 RVA: 0x00068D1C File Offset: 0x00066F1C
		public bool Contains(Attribute attribute)
		{
			Attribute attribute2 = this[attribute.GetType()];
			return attribute2 != null && attribute2.Equals(attribute);
		}

		/// <summary>Determines whether this attribute collection contains all the specified attributes in the attribute array.</summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains all the attributes; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D64 RID: 7524 RVA: 0x00068D44 File Offset: 0x00066F44
		public bool Contains(Attribute[] attributes)
		{
			if (attributes == null)
			{
				return true;
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				if (!this.Contains(attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns the default <see cref="T:System.Attribute" /> of a given <see cref="T:System.Type" />.</summary>
		/// <param name="attributeType">The <see cref="T:System.Type" /> of the attribute to retrieve.</param>
		/// <returns>The default <see cref="T:System.Attribute" /> of a given <paramref name="attributeType" />.</returns>
		// Token: 0x06001D65 RID: 7525 RVA: 0x00068D74 File Offset: 0x00066F74
		protected Attribute GetDefaultAttribute(Type attributeType)
		{
			object obj = AttributeCollection.s_internalSyncObject;
			Attribute result;
			lock (obj)
			{
				if (AttributeCollection.s_defaultAttributes == null)
				{
					AttributeCollection.s_defaultAttributes = new Hashtable();
				}
				if (AttributeCollection.s_defaultAttributes.ContainsKey(attributeType))
				{
					result = (Attribute)AttributeCollection.s_defaultAttributes[attributeType];
				}
				else
				{
					Attribute attribute = null;
					Type reflectionType = TypeDescriptor.GetReflectionType(attributeType);
					FieldInfo field = reflectionType.GetField("Default", BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
					if (field != null && field.IsStatic)
					{
						attribute = (Attribute)field.GetValue(null);
					}
					else
					{
						ConstructorInfo constructor = reflectionType.UnderlyingSystemType.GetConstructor(Array.Empty<Type>());
						if (constructor != null)
						{
							attribute = (Attribute)constructor.Invoke(Array.Empty<object>());
							if (!attribute.IsDefaultAttribute())
							{
								attribute = null;
							}
						}
					}
					AttributeCollection.s_defaultAttributes[attributeType] = attribute;
					result = attribute;
				}
			}
			return result;
		}

		/// <summary>Gets an enumerator for this collection.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06001D66 RID: 7526 RVA: 0x00068E6C File Offset: 0x0006706C
		public IEnumerator GetEnumerator()
		{
			return this.Attributes.GetEnumerator();
		}

		/// <summary>Determines whether a specified attribute is the same as an attribute in the collection.</summary>
		/// <param name="attribute">An instance of <see cref="T:System.Attribute" /> to compare with the attributes in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if the attribute is contained within the collection and has the same value as the attribute in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D67 RID: 7527 RVA: 0x00068E7C File Offset: 0x0006707C
		public bool Matches(Attribute attribute)
		{
			for (int i = 0; i < this.Attributes.Length; i++)
			{
				if (this.Attributes[i].Match(attribute))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the attributes in the specified array are the same as the attributes in the collection.</summary>
		/// <param name="attributes">An array of <see cref="T:System.CodeDom.MemberAttributes" /> to compare with the attributes in this collection.</param>
		/// <returns>
		///   <see langword="true" /> if all the attributes in the array are contained in the collection and have the same values as the attributes in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001D68 RID: 7528 RVA: 0x00068EB0 File Offset: 0x000670B0
		public bool Matches(Attribute[] attributes)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (!this.Matches(attributes[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the collection is synchronized (thread-safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x00002F6A File Offset: 0x0000116A
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x00068ED9 File Offset: 0x000670D9
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06001D6C RID: 7532 RVA: 0x00068EE1 File Offset: 0x000670E1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Copies the collection to an array, starting at the specified index.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to copy the collection to.</param>
		/// <param name="index">The index to start from.</param>
		// Token: 0x06001D6D RID: 7533 RVA: 0x00068EE9 File Offset: 0x000670E9
		public void CopyTo(Array array, int index)
		{
			Array.Copy(this.Attributes, 0, array, index, this.Attributes.Length);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00068F01 File Offset: 0x00067101
		// Note: this type is marked as 'beforefieldinit'.
		static AttributeCollection()
		{
		}

		/// <summary>Specifies an empty collection that you can use, rather than creating a new one. This field is read-only.</summary>
		// Token: 0x04000ED1 RID: 3793
		public static readonly AttributeCollection Empty = new AttributeCollection(null);

		// Token: 0x04000ED2 RID: 3794
		private static Hashtable s_defaultAttributes;

		// Token: 0x04000ED3 RID: 3795
		private readonly Attribute[] _attributes;

		// Token: 0x04000ED4 RID: 3796
		private static readonly object s_internalSyncObject = new object();

		// Token: 0x04000ED5 RID: 3797
		private const int FOUND_TYPES_LIMIT = 5;

		// Token: 0x04000ED6 RID: 3798
		private AttributeCollection.AttributeEntry[] _foundAttributeTypes;

		// Token: 0x04000ED7 RID: 3799
		private int _index;

		// Token: 0x0200037E RID: 894
		private struct AttributeEntry
		{
			// Token: 0x04000ED8 RID: 3800
			public Type type;

			// Token: 0x04000ED9 RID: 3801
			public int index;
		}
	}
}
