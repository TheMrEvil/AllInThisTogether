using System;
using System.Collections;
using Unity;

namespace System.Configuration
{
	/// <summary>Contains a collection of locked configuration objects. This class cannot be inherited.</summary>
	// Token: 0x02000025 RID: 37
	public sealed class ConfigurationLockCollection : ICollection, IEnumerable
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00005AB4 File Offset: 0x00003CB4
		internal ConfigurationLockCollection(ConfigurationElement element, ConfigurationLockType lockType)
		{
			this.names = new ArrayList();
			this.element = element;
			this.lockType = lockType;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005AD8 File Offset: 0x00003CD8
		private void CheckName(string name)
		{
			bool flag = (this.lockType & ConfigurationLockType.Attribute) == ConfigurationLockType.Attribute;
			if (this.valid_name_hash == null)
			{
				this.valid_name_hash = new Hashtable();
				foreach (object obj in this.element.Properties)
				{
					ConfigurationProperty configurationProperty = (ConfigurationProperty)obj;
					if (flag != configurationProperty.IsElement)
					{
						this.valid_name_hash.Add(configurationProperty.Name, true);
					}
				}
				if (!flag)
				{
					ConfigurationElementCollection defaultCollection = this.element.GetDefaultCollection();
					this.valid_name_hash.Add(defaultCollection.AddElementName, true);
					this.valid_name_hash.Add(defaultCollection.ClearElementName, true);
					this.valid_name_hash.Add(defaultCollection.RemoveElementName, true);
				}
				string[] array = new string[this.valid_name_hash.Keys.Count];
				this.valid_name_hash.Keys.CopyTo(array, 0);
				this.valid_names = string.Join(",", array);
			}
			if (this.valid_name_hash[name] == null)
			{
				throw new ConfigurationErrorsException(string.Format("The {2} '{0}' is not valid in the locked list for this section.  The following {3} can be locked: '{1}'", new object[]
				{
					name,
					this.valid_names,
					flag ? "attribute" : "element",
					flag ? "attributes" : "elements"
				}));
			}
		}

		/// <summary>Locks a configuration object by adding it to the collection.</summary>
		/// <param name="name">The name of the configuration object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when the <paramref name="name" /> does not match an existing configuration object within the collection.</exception>
		// Token: 0x06000139 RID: 313 RVA: 0x00005C5C File Offset: 0x00003E5C
		public void Add(string name)
		{
			this.CheckName(name);
			if (!this.names.Contains(name))
			{
				this.names.Add(name);
				this.is_modified = true;
			}
		}

		/// <summary>Clears all configuration objects from the collection.</summary>
		// Token: 0x0600013A RID: 314 RVA: 0x00005C87 File Offset: 0x00003E87
		public void Clear()
		{
			this.names.Clear();
			this.is_modified = true;
		}

		/// <summary>Verifies whether a specific configuration object is locked.</summary>
		/// <param name="name">The name of the configuration object to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> contains the specified configuration object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600013B RID: 315 RVA: 0x00005C9B File Offset: 0x00003E9B
		public bool Contains(string name)
		{
			return this.names.Contains(name);
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.ConfigurationLockCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x0600013C RID: 316 RVA: 0x00005CA9 File Offset: 0x00003EA9
		public void CopyTo(string[] array, int index)
		{
			this.names.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		// Token: 0x0600013D RID: 317 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public IEnumerator GetEnumerator()
		{
			return this.names.GetEnumerator();
		}

		/// <summary>Verifies whether a specific configuration object is read-only.</summary>
		/// <param name="name">The name of the configuration object to verify.</param>
		/// <returns>
		///   <see langword="true" /> if the specified configuration object in the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection is read-only; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The specified configuration object is not in the collection.</exception>
		// Token: 0x0600013E RID: 318 RVA: 0x00005CC8 File Offset: 0x00003EC8
		[MonoInternalNote("we can't possibly *always* return false here...")]
		public bool IsReadOnly(string name)
		{
			for (int i = 0; i < this.names.Count; i++)
			{
				if ((string)this.names[i] == name)
				{
					return false;
				}
			}
			throw new ConfigurationErrorsException(string.Format("The entry '{0}' is not in the collection.", name));
		}

		/// <summary>Removes a configuration object from the collection.</summary>
		/// <param name="name">The name of the configuration object.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when the <paramref name="name" /> does not match an existing configuration object within the collection.</exception>
		// Token: 0x0600013F RID: 319 RVA: 0x00005D16 File Offset: 0x00003F16
		public void Remove(string name)
		{
			this.names.Remove(name);
			this.is_modified = true;
		}

		/// <summary>Locks a set of configuration objects based on the supplied list.</summary>
		/// <param name="attributeList">A comma-delimited string.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Occurs when an item in the <paramref name="attributeList" /> parameter is not a valid lockable configuration attribute.</exception>
		// Token: 0x06000140 RID: 320 RVA: 0x00005D2C File Offset: 0x00003F2C
		public void SetFromList(string attributeList)
		{
			this.Clear();
			char[] separator = new char[]
			{
				','
			};
			foreach (string text in attributeList.Split(separator))
			{
				this.Add(text.Trim());
			}
		}

		/// <summary>Copies the entire <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000141 RID: 321 RVA: 0x00005CA9 File Offset: 0x00003EA9
		void ICollection.CopyTo(Array array, int index)
		{
			this.names.CopyTo(array, index);
		}

		/// <summary>Gets a list of configuration objects contained in the collection.</summary>
		/// <returns>A comma-delimited string that lists the lock configuration objects in the collection.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005D74 File Offset: 0x00003F74
		public string AttributeList
		{
			get
			{
				string[] array = new string[this.names.Count];
				this.names.CopyTo(array, 0);
				return string.Join(",", array);
			}
		}

		/// <summary>Gets the number of locked configuration objects contained in the collection.</summary>
		/// <returns>The number of locked configuration objects contained in the collection.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005DAA File Offset: 0x00003FAA
		public int Count
		{
			get
			{
				return this.names.Count;
			}
		}

		/// <summary>Gets a value specifying whether the collection of locked objects has parent elements.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection has parent elements; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000023BB File Offset: 0x000005BB
		[MonoTODO]
		public bool HasParentElements
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value specifying whether the collection has been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection has been modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005DB7 File Offset: 0x00003FB7
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005DBF File Offset: 0x00003FBF
		[MonoTODO]
		public bool IsModified
		{
			get
			{
				return this.is_modified;
			}
			internal set
			{
				this.is_modified = value;
			}
		}

		/// <summary>Gets a value specifying whether the collection is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000023BB File Offset: 0x000005BB
		[MonoTODO]
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object used to synchronize access to this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</summary>
		/// <returns>An object used to synchronize access to this <see cref="T:System.Configuration.ConfigurationLockCollection" /> collection.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00002058 File Offset: 0x00000258
		[MonoTODO]
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00003518 File Offset: 0x00001718
		internal ConfigurationLockCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000094 RID: 148
		private ArrayList names;

		// Token: 0x04000095 RID: 149
		private ConfigurationElement element;

		// Token: 0x04000096 RID: 150
		private ConfigurationLockType lockType;

		// Token: 0x04000097 RID: 151
		private bool is_modified;

		// Token: 0x04000098 RID: 152
		private Hashtable valid_name_hash;

		// Token: 0x04000099 RID: 153
		private string valid_names;
	}
}
