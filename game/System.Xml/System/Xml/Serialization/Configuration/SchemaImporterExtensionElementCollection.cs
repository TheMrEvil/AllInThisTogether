using System;
using System.Configuration;

namespace System.Xml.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure the operation of the <see cref="T:System.Xml.Serialization.XmlSchemaImporter" />. This class cannot be inherited.</summary>
	// Token: 0x0200031B RID: 795
	[ConfigurationCollection(typeof(SchemaImporterExtensionElement))]
	public sealed class SchemaImporterExtensionElementCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElementCollection" /> class.</summary>
		// Token: 0x060020CD RID: 8397 RVA: 0x000D1A85 File Offset: 0x000CFC85
		public SchemaImporterExtensionElementCollection()
		{
		}

		/// <summary>Gets or sets the object that represents the XML element at the specified index.</summary>
		/// <param name="index">The zero-based index of the XML element to get or set.</param>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> at the specified index.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> is less than zero.-or- 
		///         <paramref name="index" /> is equal to or greater than <see langword="Count" />.</exception>
		// Token: 0x1700064A RID: 1610
		public SchemaImporterExtensionElement this[int index]
		{
			get
			{
				return (SchemaImporterExtensionElement)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets or sets the item with the specified name.</summary>
		/// <param name="name">The name of the item to get or set.</param>
		/// <returns>The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> with the specified name.</returns>
		// Token: 0x1700064B RID: 1611
		public SchemaImporterExtensionElement this[string name]
		{
			get
			{
				return (SchemaImporterExtensionElement)base.BaseGet(name);
			}
			set
			{
				if (base.BaseGet(name) != null)
				{
					base.BaseRemove(name);
				}
				this.BaseAdd(value);
			}
		}

		/// <summary>Adds an item to the end of the collection.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to add to the collection.</param>
		// Token: 0x060020D2 RID: 8402 RVA: 0x000D1ADC File Offset: 0x000CFCDC
		public void Add(SchemaImporterExtensionElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all items from the collection.</summary>
		// Token: 0x060020D3 RID: 8403 RVA: 0x000D1AE5 File Offset: 0x000CFCE5
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000D1AED File Offset: 0x000CFCED
		protected override ConfigurationElement CreateNewElement()
		{
			return new SchemaImporterExtensionElement();
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000D1AF4 File Offset: 0x000CFCF4
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SchemaImporterExtensionElement)element).Key;
		}

		/// <summary>Returns the zero-based index of the first element in the collection with the specified value.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to find.</param>
		/// <returns>The index of the found element.</returns>
		// Token: 0x060020D6 RID: 8406 RVA: 0x000D1B01 File Offset: 0x000CFD01
		public int IndexOf(SchemaImporterExtensionElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the first occurrence of a specific item from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Xml.Serialization.Configuration.SchemaImporterExtensionElement" /> to remove.</param>
		// Token: 0x060020D7 RID: 8407 RVA: 0x000D1B0A File Offset: 0x000CFD0A
		public void Remove(SchemaImporterExtensionElement element)
		{
			base.BaseRemove(element.Key);
		}

		/// <summary>Removes the item with the specified name from the collection.</summary>
		/// <param name="name">The name of the item to remove.</param>
		// Token: 0x060020D8 RID: 8408 RVA: 0x000D1B18 File Offset: 0x000CFD18
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the item at the specified index from the collection.</summary>
		/// <param name="index">The index of the object to remove.</param>
		// Token: 0x060020D9 RID: 8409 RVA: 0x000D1B21 File Offset: 0x000CFD21
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
