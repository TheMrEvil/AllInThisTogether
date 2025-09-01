using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Configuration
{
	/// <summary>Represents a collection of configuration-element properties.</summary>
	// Token: 0x0200002B RID: 43
	public class ConfigurationPropertyCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> class.</summary>
		// Token: 0x06000188 RID: 392 RVA: 0x00006617 File Offset: 0x00004817
		public ConfigurationPropertyCollection()
		{
			this.collection = new List<ConfigurationProperty>();
		}

		/// <summary>Gets the number of properties in the collection.</summary>
		/// <returns>The number of properties in the collection.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000662A File Offset: 0x0000482A
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		/// <summary>Gets the collection item with the specified name.</summary>
		/// <param name="name">The <see cref="T:System.Configuration.ConfigurationProperty" /> to return.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationProperty" /> with the specified <paramref name="name" />.</returns>
		// Token: 0x17000074 RID: 116
		public ConfigurationProperty this[string name]
		{
			get
			{
				foreach (ConfigurationProperty configurationProperty in this.collection)
				{
					if (configurationProperty.Name == name)
					{
						return configurationProperty;
					}
				}
				return null;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000023BB File Offset: 0x000005BB
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000669C File Offset: 0x0000489C
		public object SyncRoot
		{
			get
			{
				return this.collection;
			}
		}

		/// <summary>Adds a configuration property to the collection.</summary>
		/// <param name="property">The <see cref="T:System.Configuration.ConfigurationProperty" /> to add.</param>
		// Token: 0x0600018D RID: 397 RVA: 0x000066A4 File Offset: 0x000048A4
		public void Add(ConfigurationProperty property)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			this.collection.Add(property);
		}

		/// <summary>Specifies whether the configuration property is contained in this collection.</summary>
		/// <param name="name">An identifier for the <see cref="T:System.Configuration.ConfigurationProperty" /> to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Configuration.ConfigurationProperty" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600018E RID: 398 RVA: 0x000066C0 File Offset: 0x000048C0
		public bool Contains(string name)
		{
			ConfigurationProperty configurationProperty = this[name];
			return configurationProperty != null && this.collection.Contains(configurationProperty);
		}

		/// <summary>Copies this ConfigurationPropertyCollection to an array.</summary>
		/// <param name="array">Array to which to copy.</param>
		/// <param name="index">Index at which to begin copying.</param>
		// Token: 0x0600018F RID: 399 RVA: 0x000066E6 File Offset: 0x000048E6
		public void CopyTo(ConfigurationProperty[] array, int index)
		{
			this.collection.CopyTo(array, index);
		}

		/// <summary>Copies this collection to an array.</summary>
		/// <param name="array">The array to which to copy.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		// Token: 0x06000190 RID: 400 RVA: 0x000066F5 File Offset: 0x000048F5
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this.collection).CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection</returns>
		// Token: 0x06000191 RID: 401 RVA: 0x00006704 File Offset: 0x00004904
		public IEnumerator GetEnumerator()
		{
			return this.collection.GetEnumerator();
		}

		/// <summary>Removes a configuration property from the collection.</summary>
		/// <param name="name">The <see cref="T:System.Configuration.ConfigurationProperty" /> to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Configuration.ConfigurationProperty" /> was removed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000192 RID: 402 RVA: 0x00006716 File Offset: 0x00004916
		public bool Remove(string name)
		{
			return this.collection.Remove(this[name]);
		}

		/// <summary>Removes all configuration property objects from the collection.</summary>
		// Token: 0x06000193 RID: 403 RVA: 0x0000672A File Offset: 0x0000492A
		public void Clear()
		{
			this.collection.Clear();
		}

		// Token: 0x040000AA RID: 170
		private List<ConfigurationProperty> collection;
	}
}
