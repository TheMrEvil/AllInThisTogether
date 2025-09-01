using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.KeyValueConfigurationElement" /> objects.</summary>
	// Token: 0x0200004F RID: 79
	[ConfigurationCollection(typeof(KeyValueConfigurationElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public class KeyValueConfigurationCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the collection based on the supplied parameters.</summary>
		/// <param name="keyValue">A <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		// Token: 0x060002AA RID: 682 RVA: 0x00008349 File Offset: 0x00006549
		public void Add(KeyValueConfigurationElement keyValue)
		{
			keyValue.Init();
			this.BaseAdd(keyValue);
		}

		/// <summary>Adds a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the collection based on the supplied parameters.</summary>
		/// <param name="key">A string specifying the key.</param>
		/// <param name="value">A string specifying the value.</param>
		// Token: 0x060002AB RID: 683 RVA: 0x00008358 File Offset: 0x00006558
		public void Add(string key, string value)
		{
			this.Add(new KeyValueConfigurationElement(key, value));
		}

		/// <summary>Clears the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection.</summary>
		// Token: 0x060002AC RID: 684 RVA: 0x000075D1 File Offset: 0x000057D1
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object from the collection.</summary>
		/// <param name="key">A string specifying the <paramref name="key" />.</param>
		// Token: 0x060002AD RID: 685 RVA: 0x000075F0 File Offset: 0x000057F0
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		/// <summary>Gets the keys to all items contained in the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection.</summary>
		/// <returns>A string array.</returns>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00008368 File Offset: 0x00006568
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				int num = 0;
				foreach (object obj in this)
				{
					KeyValueConfigurationElement keyValueConfigurationElement = (KeyValueConfigurationElement)obj;
					array[num++] = keyValueConfigurationElement.Key;
				}
				return array;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object based on the supplied parameter.</summary>
		/// <param name="key">The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> contained in the collection.</param>
		/// <returns>A configuration element, or <see langword="null" /> if the key does not exist in the collection.</returns>
		// Token: 0x170000BF RID: 191
		public KeyValueConfigurationElement this[string key]
		{
			get
			{
				return (KeyValueConfigurationElement)base.BaseGet(key);
			}
		}

		/// <summary>When overridden in a derived class, the <see cref="M:System.Configuration.KeyValueConfigurationCollection.CreateNewElement" /> method creates a new <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>A newly created <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		// Token: 0x060002B0 RID: 688 RVA: 0x000083E2 File Offset: 0x000065E2
		protected override ConfigurationElement CreateNewElement()
		{
			return new KeyValueConfigurationElement();
		}

		/// <summary>Gets the element key for a specified configuration element when overridden in a derived class.</summary>
		/// <param name="element">The <see cref="T:System.Configuration.KeyValueConfigurationElement" /> to which the key should be returned.</param>
		/// <returns>An object that acts as the key for the specified <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		// Token: 0x060002B1 RID: 689 RVA: 0x000083E9 File Offset: 0x000065E9
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((KeyValueConfigurationElement)element).Key;
		}

		/// <summary>Gets a collection of configuration properties.</summary>
		/// <returns>A collection of configuration properties.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000083F6 File Offset: 0x000065F6
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection();
				}
				return this.properties;
			}
		}

		/// <summary>Gets a value indicating whether an attempt to add a duplicate <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> collection will cause an exception to be thrown.</summary>
		/// <returns>
		///   <see langword="true" /> if an attempt to add a duplicate <see cref="T:System.Configuration.KeyValueConfigurationElement" /> to the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> will cause an exception to be thrown; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000023BB File Offset: 0x000005BB
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> class.</summary>
		// Token: 0x060002B4 RID: 692 RVA: 0x00007500 File Offset: 0x00005700
		public KeyValueConfigurationCollection()
		{
		}

		// Token: 0x040000FF RID: 255
		private ConfigurationPropertyCollection properties;
	}
}
