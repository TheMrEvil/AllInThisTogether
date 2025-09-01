using System;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.ConnectionStringSettings" /> objects.</summary>
	// Token: 0x02000039 RID: 57
	[ConfigurationCollection(typeof(ConnectionStringSettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ConnectionStringSettingsCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> class.</summary>
		// Token: 0x060001FF RID: 511 RVA: 0x00007500 File Offset: 0x00005700
		public ConnectionStringSettingsCollection()
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.ConnectionStringSettings" /> object with the specified name in the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		/// <returns>The <see cref="T:System.Configuration.ConnectionStringSettings" /> object with the specified name; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000098 RID: 152
		public ConnectionStringSettings this[string name]
		{
			get
			{
				foreach (object obj in this)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)obj;
					if (configurationElement is ConnectionStringSettings && string.Compare(((ConnectionStringSettings)configurationElement).Name, name, true, CultureInfo.InvariantCulture) == 0)
					{
						return configurationElement as ConnectionStringSettings;
					}
				}
				return null;
			}
		}

		/// <summary>Gets or sets the connection string at the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		/// <returns>The <see cref="T:System.Configuration.ConnectionStringSettings" /> object at the specified index.</returns>
		// Token: 0x17000099 RID: 153
		public ConnectionStringSettings this[int index]
		{
			get
			{
				return (ConnectionStringSettings)base.BaseGet(index);
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000075AC File Offset: 0x000057AC
		[MonoTODO]
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000075B4 File Offset: 0x000057B4
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionStringSettings();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000075BB File Offset: 0x000057BB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ConnectionStringSettings)element).Name;
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ConnectionStringSettings" /> object to the collection.</summary>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object to add to the collection.</param>
		// Token: 0x06000206 RID: 518 RVA: 0x000075C8 File Offset: 0x000057C8
		public void Add(ConnectionStringSettings settings)
		{
			this.BaseAdd(settings);
		}

		/// <summary>Removes all the <see cref="T:System.Configuration.ConnectionStringSettings" /> objects from the collection.</summary>
		// Token: 0x06000207 RID: 519 RVA: 0x000075D1 File Offset: 0x000057D1
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns the collection index of the passed <see cref="T:System.Configuration.ConnectionStringSettings" /> object.</summary>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		/// <returns>The collection index of the specified <see cref="T:System.Configuration.ConnectionStringSettingsCollection" /> object.</returns>
		// Token: 0x06000208 RID: 520 RVA: 0x000075D9 File Offset: 0x000057D9
		public int IndexOf(ConnectionStringSettings settings)
		{
			return base.BaseIndexOf(settings);
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConnectionStringSettings" /> object from the collection.</summary>
		/// <param name="settings">A <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x06000209 RID: 521 RVA: 0x000075E2 File Offset: 0x000057E2
		public void Remove(ConnectionStringSettings settings)
		{
			base.BaseRemove(settings.Name);
		}

		/// <summary>Removes the specified <see cref="T:System.Configuration.ConnectionStringSettings" /> object from the collection.</summary>
		/// <param name="name">The name of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x0600020A RID: 522 RVA: 0x000075F0 File Offset: 0x000057F0
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConnectionStringSettings" /> object at the specified index in the collection.</summary>
		/// <param name="index">The index of a <see cref="T:System.Configuration.ConnectionStringSettings" /> object in the collection.</param>
		// Token: 0x0600020B RID: 523 RVA: 0x000075F9 File Offset: 0x000057F9
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007604 File Offset: 0x00005804
		protected override void BaseAdd(int index, ConfigurationElement element)
		{
			if (!(element is ConnectionStringSettings))
			{
				base.BaseAdd(element);
			}
			if (this.IndexOf((ConnectionStringSettings)element) >= 0)
			{
				throw new ConfigurationErrorsException(string.Format("The element {0} already exist!", ((ConnectionStringSettings)element).Name));
			}
			this[index] = (ConnectionStringSettings)element;
		}
	}
}
