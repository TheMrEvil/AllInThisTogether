using System;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.NameValueConfigurationElement" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000054 RID: 84
	[ConfigurationCollection(typeof(NameValueConfigurationElement), AddItemName = "add", RemoveItemName = "remove", ClearItemsName = "clear", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class NameValueConfigurationCollection : ConfigurationElementCollection
	{
		// Token: 0x060002CD RID: 717 RVA: 0x00008684 File Offset: 0x00006884
		static NameValueConfigurationCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.NameValueConfigurationCollection" /> class.</summary>
		// Token: 0x060002CE RID: 718 RVA: 0x00007500 File Offset: 0x00005700
		public NameValueConfigurationCollection()
		{
		}

		/// <summary>Gets the keys to all items contained in the <see cref="T:System.Configuration.NameValueConfigurationCollection" />.</summary>
		/// <returns>A string array.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00008690 File Offset: 0x00006890
		public string[] AllKeys
		{
			get
			{
				return (string[])base.BaseGetAllKeys();
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object based on the supplied parameter.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> contained in the collection.</param>
		/// <returns>A <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</returns>
		// Token: 0x170000CA RID: 202
		public NameValueConfigurationElement this[string name]
		{
			get
			{
				return (NameValueConfigurationElement)base.BaseGet(name);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000086AB File Offset: 0x000068AB
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NameValueConfigurationCollection.properties;
			}
		}

		/// <summary>Adds a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object to the collection.</summary>
		/// <param name="nameValue">A  <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		// Token: 0x060002D3 RID: 723 RVA: 0x000086B2 File Offset: 0x000068B2
		public void Add(NameValueConfigurationElement nameValue)
		{
			base.BaseAdd(nameValue, false);
		}

		/// <summary>Clears the <see cref="T:System.Configuration.NameValueConfigurationCollection" />.</summary>
		// Token: 0x060002D4 RID: 724 RVA: 0x000075D1 File Offset: 0x000057D1
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000086BC File Offset: 0x000068BC
		protected override ConfigurationElement CreateNewElement()
		{
			return new NameValueConfigurationElement("", "");
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000086CD File Offset: 0x000068CD
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NameValueConfigurationElement)element).Name;
		}

		/// <summary>Removes a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object from the collection based on the provided parameter.</summary>
		/// <param name="nameValue">A <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		// Token: 0x060002D7 RID: 727 RVA: 0x0000371B File Offset: 0x0000191B
		public void Remove(NameValueConfigurationElement nameValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes a <see cref="T:System.Configuration.NameValueConfigurationElement" /> object from the collection based on the provided parameter.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		// Token: 0x060002D8 RID: 728 RVA: 0x000075F0 File Offset: 0x000057F0
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		// Token: 0x0400010B RID: 267
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
