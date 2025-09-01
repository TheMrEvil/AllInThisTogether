using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects.</summary>
	// Token: 0x02000034 RID: 52
	[Serializable]
	public sealed class ConfigurationSectionGroupCollection : NameObjectCollectionBase
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000717F File Offset: 0x0000537F
		internal ConfigurationSectionGroupCollection(Configuration config, SectionGroupInfo group) : base(StringComparer.Ordinal)
		{
			this.config = config;
			this.group = group;
		}

		/// <summary>Gets the keys to all <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects contained in this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> object that contains the names of all section groups in this collection.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000719A File Offset: 0x0000539A
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return this.group.Groups.Keys;
			}
		}

		/// <summary>Gets the number of section groups in the collection.</summary>
		/// <returns>An integer that represents the number of section groups in the collection.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000071AC File Offset: 0x000053AC
		public override int Count
		{
			get
			{
				return this.group.Groups.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose name is specified from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object with the specified name.  
		///  In C#, this property is the indexer for the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> class.</returns>
		// Token: 0x17000090 RID: 144
		public ConfigurationSectionGroup this[string name]
		{
			get
			{
				ConfigurationSectionGroup configurationSectionGroup = base.BaseGet(name) as ConfigurationSectionGroup;
				if (configurationSectionGroup == null)
				{
					SectionGroupInfo sectionGroupInfo = this.group.Groups[name] as SectionGroupInfo;
					if (sectionGroupInfo == null)
					{
						return null;
					}
					configurationSectionGroup = this.config.GetSectionGroupInstance(sectionGroupInfo);
					base.BaseSet(name, configurationSectionGroup);
				}
				return configurationSectionGroup;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose index is specified from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.  
		///  In C#, this property is the indexer for the <see cref="T:System.Configuration.ConfigurationSectionCollection" /> class.</returns>
		// Token: 0x17000091 RID: 145
		public ConfigurationSectionGroup this[int index]
		{
			get
			{
				return this[this.GetKey(index)];
			}
		}

		/// <summary>Adds a <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be added.</param>
		/// <param name="sectionGroup">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be added.</param>
		// Token: 0x060001E1 RID: 481 RVA: 0x0000721E File Offset: 0x0000541E
		public void Add(string name, ConfigurationSectionGroup sectionGroup)
		{
			this.config.CreateSectionGroup(this.group, name, sectionGroup);
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x060001E2 RID: 482 RVA: 0x00007234 File Offset: 0x00005434
		public void Clear()
		{
			if (this.group.Groups != null)
			{
				foreach (object obj in this.group.Groups)
				{
					ConfigInfo configInfo = (ConfigInfo)obj;
					this.config.RemoveConfigInfo(configInfo);
				}
			}
		}

		/// <summary>Copies this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object to an array.</summary>
		/// <param name="array">The array to copy the object to.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="array" /> is less than the value of <see cref="P:System.Configuration.ConfigurationSectionGroupCollection.Count" /> plus <paramref name="index" />.</exception>
		// Token: 0x060001E3 RID: 483 RVA: 0x000072A4 File Offset: 0x000054A4
		public void CopyTo(ConfigurationSectionGroup[] array, int index)
		{
			for (int i = 0; i < this.group.Groups.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object contained in the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.</returns>
		// Token: 0x060001E4 RID: 484 RVA: 0x000072D8 File Offset: 0x000054D8
		public ConfigurationSectionGroup Get(int index)
		{
			return this[index];
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object to be returned.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object with the specified name.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null or an empty string ("").</exception>
		// Token: 0x060001E5 RID: 485 RVA: 0x000072E1 File Offset: 0x000054E1
		public ConfigurationSectionGroup Get(string name)
		{
			return this[name];
		}

		/// <summary>Gets an enumerator that can iterate through the <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</returns>
		// Token: 0x060001E6 RID: 486 RVA: 0x000072EA File Offset: 0x000054EA
		public override IEnumerator GetEnumerator()
		{
			return this.group.Groups.AllKeys.GetEnumerator();
		}

		/// <summary>Gets the key of the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object contained in this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="index">The index of the section group whose key is to be returned.</param>
		/// <returns>The key of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object at the specified index.</returns>
		// Token: 0x060001E7 RID: 487 RVA: 0x00007301 File Offset: 0x00005501
		public string GetKey(int index)
		{
			return this.group.Groups.GetKey(index);
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose name is specified from this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="name">The name of the section group to be removed.</param>
		// Token: 0x060001E8 RID: 488 RVA: 0x00007314 File Offset: 0x00005514
		public void Remove(string name)
		{
			SectionGroupInfo sectionGroupInfo = this.group.Groups[name] as SectionGroupInfo;
			if (sectionGroupInfo != null)
			{
				this.config.RemoveConfigInfo(sectionGroupInfo);
			}
		}

		/// <summary>Removes the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object whose index is specified from this <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object.</summary>
		/// <param name="index">The index of the section group to be removed.</param>
		// Token: 0x060001E9 RID: 489 RVA: 0x00007348 File Offset: 0x00005548
		public void RemoveAt(int index)
		{
			SectionGroupInfo sectionGroupInfo = this.group.Groups[index] as SectionGroupInfo;
			this.config.RemoveConfigInfo(sectionGroupInfo);
		}

		/// <summary>Used by the system during serialization.</summary>
		/// <param name="info">The applicable <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The applicable <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x060001EA RID: 490 RVA: 0x0000371B File Offset: 0x0000191B
		[MonoTODO]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00003518 File Offset: 0x00001718
		internal ConfigurationSectionGroupCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000CF RID: 207
		private SectionGroupInfo group;

		// Token: 0x040000D0 RID: 208
		private Configuration config;
	}
}
