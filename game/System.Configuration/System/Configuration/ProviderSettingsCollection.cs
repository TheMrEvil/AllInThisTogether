using System;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	// Token: 0x02000062 RID: 98
	[ConfigurationCollection(typeof(ProviderSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ProviderSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Configuration.ProviderSettings" /> object to the collection.</summary>
		/// <param name="provider">The <see cref="T:System.Configuration.ProviderSettings" /> object to add.</param>
		// Token: 0x0600032D RID: 813 RVA: 0x000075C8 File Offset: 0x000057C8
		public void Add(ProviderSettings provider)
		{
			this.BaseAdd(provider);
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x0600032E RID: 814 RVA: 0x000075D1 File Offset: 0x000057D1
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008ED4 File Offset: 0x000070D4
		protected override ConfigurationElement CreateNewElement()
		{
			return new ProviderSettings();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008EDB File Offset: 0x000070DB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ProviderSettings)element).Name;
		}

		/// <summary>Removes an element from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object to remove.</param>
		// Token: 0x06000331 RID: 817 RVA: 0x000075F0 File Offset: 0x000057F0
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Gets or sets a value at the specified index in the <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ProviderSettings" /> to return.</param>
		/// <returns>The specified <see cref="T:System.Configuration.ProviderSettings" />.</returns>
		// Token: 0x170000EF RID: 239
		public ProviderSettings this[int index]
		{
			get
			{
				return (ProviderSettings)base.BaseGet(index);
			}
			set
			{
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets an item from the collection.</summary>
		/// <param name="key">A string reference to the <see cref="T:System.Configuration.ProviderSettings" /> object within the collection.</param>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettings" /> object contained in the collection.</returns>
		// Token: 0x170000F0 RID: 240
		public ProviderSettings this[string key]
		{
			get
			{
				return (ProviderSettings)base.BaseGet(key);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00008F0E File Offset: 0x0000710E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProviderSettingsCollection.props;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProviderSettingsCollection" /> class.</summary>
		// Token: 0x06000336 RID: 822 RVA: 0x00007500 File Offset: 0x00005700
		public ProviderSettingsCollection()
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00008F15 File Offset: 0x00007115
		// Note: this type is marked as 'beforefieldinit'.
		static ProviderSettingsCollection()
		{
		}

		// Token: 0x0400012B RID: 299
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
