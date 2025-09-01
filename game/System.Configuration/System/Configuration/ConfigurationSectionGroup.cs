using System;
using System.Runtime.Versioning;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a group of related sections within a configuration file.</summary>
	// Token: 0x02000033 RID: 51
	public class ConfigurationSectionGroup
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> class.</summary>
		// Token: 0x060001CD RID: 461 RVA: 0x00002050 File Offset: 0x00000250
		public ConfigurationSectionGroup()
		{
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00007064 File Offset: 0x00005264
		private Configuration Config
		{
			get
			{
				if (this.config == null)
				{
					throw new InvalidOperationException("ConfigurationSectionGroup cannot be edited until it is added to a Configuration instance as its descendant");
				}
				return this.config;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007080 File Offset: 0x00005280
		internal void Initialize(Configuration config, SectionGroupInfo group)
		{
			if (this.initialized)
			{
				string str = "INTERNAL ERROR: this configuration section is being initialized twice: ";
				Type type = base.GetType();
				throw new SystemException(str + ((type != null) ? type.ToString() : null));
			}
			this.initialized = true;
			this.config = config;
			this.group = group;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000070CC File Offset: 0x000052CC
		internal void SetName(string name)
		{
			this.name = name;
		}

		/// <summary>Forces the declaration for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <param name="force">
		///   <see langword="true" /> if the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object must be written to the file; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is the root section group.  
		/// -or-
		///  The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object has a location.</exception>
		// Token: 0x060001D1 RID: 465 RVA: 0x000070D5 File Offset: 0x000052D5
		[MonoTODO]
		public void ForceDeclaration(bool force)
		{
			this.require_declaration = force;
		}

		/// <summary>Forces the declaration for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		// Token: 0x060001D2 RID: 466 RVA: 0x000070DE File Offset: 0x000052DE
		public void ForceDeclaration()
		{
			this.ForceDeclaration(true);
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is declared.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> is declared; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000023BB File Offset: 0x000005BB
		[MonoTODO]
		public bool IsDeclared
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object declaration is required.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> declaration is required; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000070E7 File Offset: 0x000052E7
		[MonoTODO]
		public bool IsDeclarationRequired
		{
			get
			{
				return this.require_declaration;
			}
		}

		/// <summary>Gets the name property of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>The name property of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000070EF File Offset: 0x000052EF
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the section group name associated with this <see cref="T:System.Configuration.ConfigurationSectionGroup" />.</summary>
		/// <returns>The section group name of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000070F7 File Offset: 0x000052F7
		[MonoInternalNote("Check if this is correct")]
		public string SectionGroupName
		{
			get
			{
				return this.group.XPath;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects that are children of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> objects that are children of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00007104 File Offset: 0x00005304
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new ConfigurationSectionGroupCollection(this.Config, this.group);
				}
				return this.groups;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object that contains all of <see cref="T:System.Configuration.ConfigurationSection" /> objects within this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionCollection" /> object that contains all the <see cref="T:System.Configuration.ConfigurationSection" /> objects within this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000712B File Offset: 0x0000532B
		public ConfigurationSectionCollection Sections
		{
			get
			{
				if (this.sections == null)
				{
					this.sections = new ConfigurationSectionCollection(this.Config, this.group);
				}
				return this.sections;
			}
		}

		/// <summary>Gets or sets the type for this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>The type of this <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object is the root section group.  
		/// -or-
		///  The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object has a location.</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The section or group is already defined at another level.</exception>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00007152 File Offset: 0x00005352
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000715A File Offset: 0x0000535A
		public string Type
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Configuration.ConfigurationSectionGroup" /> instance should be serialized when the configuration object hierarchy is serialized for the specified target version of the .NET Framework.</summary>
		/// <param name="targetFramework">The target version of the .NET Framework.</param>
		/// <returns>
		///   <see langword="true" /> if the current section group should be serialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x060001DB RID: 475 RVA: 0x00007164 File Offset: 0x00005364
		protected internal virtual bool ShouldSerializeSectionGroupInTargetVersion(FrameworkName targetFramework)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x040000C7 RID: 199
		private bool require_declaration;

		// Token: 0x040000C8 RID: 200
		private string name;

		// Token: 0x040000C9 RID: 201
		private string type_name;

		// Token: 0x040000CA RID: 202
		private ConfigurationSectionCollection sections;

		// Token: 0x040000CB RID: 203
		private ConfigurationSectionGroupCollection groups;

		// Token: 0x040000CC RID: 204
		private Configuration config;

		// Token: 0x040000CD RID: 205
		private SectionGroupInfo group;

		// Token: 0x040000CE RID: 206
		private bool initialized;
	}
}
