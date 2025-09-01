using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Configuration
{
	/// <summary>Contains metadata about an individual section within the configuration hierarchy. This class cannot be inherited.</summary>
	// Token: 0x02000069 RID: 105
	public sealed class SectionInformation
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000A5FC File Offset: 0x000087FC
		[MonoTODO("default value for require_permission")]
		internal SectionInformation()
		{
			this.allow_definition = ConfigurationAllowDefinition.Everywhere;
			this.allow_location = true;
			this.allow_override = true;
			this.inherit_on_child_apps = true;
			this.restart_on_external_changes = true;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000A654 File Offset: 0x00008854
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000A65C File Offset: 0x0000885C
		internal string ConfigFilePath
		{
			[CompilerGenerated]
			get
			{
				return this.<ConfigFilePath>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConfigFilePath>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets a value that indicates where in the configuration file hierarchy the associated configuration section can be defined.</summary>
		/// <returns>A value that indicates where in the configuration file hierarchy the associated <see cref="T:System.Configuration.ConfigurationSection" /> object can be declared.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000A665 File Offset: 0x00008865
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000A66D File Offset: 0x0000886D
		public ConfigurationAllowDefinition AllowDefinition
		{
			get
			{
				return this.allow_definition;
			}
			set
			{
				this.allow_definition = value;
			}
		}

		/// <summary>Gets or sets a value that indicates where in the configuration file hierarchy the associated configuration section can be declared.</summary>
		/// <returns>A value that indicates where in the configuration file hierarchy the associated <see cref="T:System.Configuration.ConfigurationSection" /> object can be declared for .exe files.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000A676 File Offset: 0x00008876
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000A67E File Offset: 0x0000887E
		public ConfigurationAllowExeDefinition AllowExeDefinition
		{
			get
			{
				return this.allow_exe_definition;
			}
			set
			{
				this.allow_exe_definition = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the configuration section allows the <see langword="location" /> attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="location" /> attribute is allowed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000A687 File Offset: 0x00008887
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000A68F File Offset: 0x0000888F
		public bool AllowLocation
		{
			get
			{
				return this.allow_location;
			}
			set
			{
				this.allow_location = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the associated configuration section can be overridden by lower-level configuration files.</summary>
		/// <returns>
		///   <see langword="true" /> if the section can be overridden; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000A698 File Offset: 0x00008898
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public bool AllowOverride
		{
			get
			{
				return this.allow_override;
			}
			set
			{
				this.allow_override = value;
			}
		}

		/// <summary>Gets or sets the name of the include file in which the associated configuration section is defined, if such a file exists.</summary>
		/// <returns>The name of the include file in which the associated <see cref="T:System.Configuration.ConfigurationSection" /> is defined, if such a file exists; otherwise, an empty string ("").</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000A6A9 File Offset: 0x000088A9
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000A6B1 File Offset: 0x000088B1
		public string ConfigSource
		{
			get
			{
				return this.config_source;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.config_source = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the associated configuration section will be saved even if it has not been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationSection" /> object will be saved even if it has not been modified; otherwise, <see langword="false" />. The default is <see langword="false" />.  
		///
		///  If the configuration file is saved (even if there are no modifications), ASP.NET restarts the application.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000A6C4 File Offset: 0x000088C4
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000A6CC File Offset: 0x000088CC
		public bool ForceSave
		{
			get
			{
				return this.force_update;
			}
			set
			{
				this.force_update = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the settings that are specified in the associated configuration section are inherited by applications that reside in a subdirectory of the relevant application.</summary>
		/// <returns>
		///   <see langword="true" /> if the settings specified in this <see cref="T:System.Configuration.ConfigurationSection" /> object are inherited by child applications; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000A6D5 File Offset: 0x000088D5
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000A6DD File Offset: 0x000088DD
		public bool InheritInChildApplications
		{
			get
			{
				return this.inherit_on_child_apps;
			}
			set
			{
				this.inherit_on_child_apps = value;
			}
		}

		/// <summary>Gets a value that indicates whether the configuration section must be declared in the configuration file.</summary>
		/// <returns>
		///   <see langword="true" /> if the associated <see cref="T:System.Configuration.ConfigurationSection" /> object must be declared in the configuration file; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000371B File Offset: 0x0000191B
		[MonoTODO]
		public bool IsDeclarationRequired
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section is declared in the configuration file.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationSection" /> is declared in the configuration file; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000023BB File Offset: 0x000005BB
		[MonoTODO]
		public bool IsDeclared
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section is locked.</summary>
		/// <returns>
		///   <see langword="true" /> if the section is locked; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000023BB File Offset: 0x000005BB
		[MonoTODO]
		public bool IsLocked
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section is protected.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Configuration.ConfigurationSection" /> is protected; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000A6E6 File Offset: 0x000088E6
		public bool IsProtected
		{
			get
			{
				return this.protection_provider != null;
			}
		}

		/// <summary>Gets the name of the associated configuration section.</summary>
		/// <returns>The complete name of the configuration section.</returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000A6F1 File Offset: 0x000088F1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the protected configuration provider for the associated configuration section.</summary>
		/// <returns>The protected configuration provider for this <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000A6F9 File Offset: 0x000088F9
		public ProtectedConfigurationProvider ProtectionProvider
		{
			get
			{
				return this.protection_provider;
			}
		}

		/// <summary>Gets a value that indicates whether the associated configuration section requires access permissions.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="requirePermission" /> attribute is set to <see langword="true" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000A701 File Offset: 0x00008901
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000A709 File Offset: 0x00008909
		[MonoTODO]
		public bool RequirePermission
		{
			get
			{
				return this.require_permission;
			}
			set
			{
				this.require_permission = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether a change in an external configuration include file requires an application restart.</summary>
		/// <returns>
		///   <see langword="true" /> if a change in an external configuration include file requires an application restart; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000A712 File Offset: 0x00008912
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000A71A File Offset: 0x0000891A
		[MonoTODO]
		public bool RestartOnExternalChanges
		{
			get
			{
				return this.restart_on_external_changes;
			}
			set
			{
				this.restart_on_external_changes = value;
			}
		}

		/// <summary>Gets the name of the associated configuration section.</summary>
		/// <returns>The name of the associated <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000A6F1 File Offset: 0x000088F1
		[MonoTODO]
		public string SectionName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the section class name.</summary>
		/// <returns>The name of the class that is associated with this <see cref="T:System.Configuration.ConfigurationSection" /> section.</returns>
		/// <exception cref="T:System.ArgumentException">The selected value is <see langword="null" /> or an empty string ("").</exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value conflicts with a value that is already defined.</exception>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000A723 File Offset: 0x00008923
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000A72B File Offset: 0x0000892B
		public string Type
		{
			get
			{
				return this.type_name;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("Value cannot be null or empty.");
				}
				this.type_name = value;
			}
		}

		/// <summary>Gets the configuration section that contains the configuration section associated with this object.</summary>
		/// <returns>The configuration section that contains the <see cref="T:System.Configuration.ConfigurationSection" /> that is associated with this <see cref="T:System.Configuration.SectionInformation" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method is invoked from a parent section.</exception>
		// Token: 0x0600039C RID: 924 RVA: 0x0000A74A File Offset: 0x0000894A
		public ConfigurationSection GetParentSection()
		{
			return this.parent;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000A752 File Offset: 0x00008952
		internal void SetParentSection(ConfigurationSection parent)
		{
			this.parent = parent;
		}

		/// <summary>Returns an XML node object that represents the associated configuration-section object.</summary>
		/// <returns>The XML representation for this configuration section.</returns>
		/// <exception cref="T:System.InvalidOperationException">This configuration object is locked and cannot be edited.</exception>
		// Token: 0x0600039E RID: 926 RVA: 0x0000A75B File Offset: 0x0000895B
		public string GetRawXml()
		{
			return this.raw_xml;
		}

		/// <summary>Marks a configuration section for protection.</summary>
		/// <param name="protectionProvider">The name of the protection provider to use.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Configuration.SectionInformation.AllowLocation" /> property is set to <see langword="false" />.  
		/// -or-
		///  The target section is already a protected data section.</exception>
		// Token: 0x0600039F RID: 927 RVA: 0x0000A763 File Offset: 0x00008963
		public void ProtectSection(string protectionProvider)
		{
			this.protection_provider = ProtectedConfiguration.GetProvider(protectionProvider, true);
		}

		/// <summary>Forces the associated configuration section to appear in the configuration file, or removes an existing section from the configuration file.</summary>
		/// <param name="force">
		///   <see langword="true" /> if the associated section should be written in the configuration file; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="force" /> is <see langword="true" /> and the associated section cannot be exported to the child configuration file, or it is undeclared.</exception>
		// Token: 0x060003A0 RID: 928 RVA: 0x000023B9 File Offset: 0x000005B9
		[MonoTODO]
		public void ForceDeclaration(bool force)
		{
		}

		/// <summary>Forces the associated configuration section to appear in the configuration file.</summary>
		// Token: 0x060003A1 RID: 929 RVA: 0x0000A772 File Offset: 0x00008972
		public void ForceDeclaration()
		{
			this.ForceDeclaration(true);
		}

		/// <summary>Causes the associated configuration section to inherit all its values from the parent section.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method cannot be called outside editing mode.</exception>
		// Token: 0x060003A2 RID: 930 RVA: 0x0000371B File Offset: 0x0000191B
		[MonoTODO]
		public void RevertToParent()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the protected configuration encryption from the associated configuration section.</summary>
		// Token: 0x060003A3 RID: 931 RVA: 0x0000A77B File Offset: 0x0000897B
		public void UnprotectSection()
		{
			this.protection_provider = null;
		}

		/// <summary>Sets the object to an XML representation of the associated configuration section within the configuration file.</summary>
		/// <param name="rawXml">The XML to use.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rawXml" /> is <see langword="null" />.</exception>
		// Token: 0x060003A4 RID: 932 RVA: 0x0000A784 File Offset: 0x00008984
		public void SetRawXml(string rawXml)
		{
			this.raw_xml = rawXml;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000A78D File Offset: 0x0000898D
		[MonoTODO]
		internal void SetName(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ConfigurationBuilder" /> object for this configuration section.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationBuilder" /> object for this configuration section.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00003527 File Offset: 0x00001727
		public ConfigurationBuilder ConfigurationBuilder
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.OverrideMode" /> enumeration value that specifies whether the associated configuration section can be overridden by child configuration files.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.OverrideMode" /> enumeration values.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">An attempt was made to change both the <see cref="P:System.Configuration.SectionInformation.AllowOverride" /> and <see cref="P:System.Configuration.SectionInformation.OverrideMode" /> properties, which is not supported for compatibility reasons.</exception>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000A798 File Offset: 0x00008998
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00003518 File Offset: 0x00001718
		public OverrideMode OverrideMode
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return OverrideMode.Inherit;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies the default override behavior of a configuration section by child configuration files.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.OverrideMode" /> enumeration values.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The override behavior is specified in a parent configuration section.</exception>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000A7B4 File Offset: 0x000089B4
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00003518 File Offset: 0x00001718
		public OverrideMode OverrideModeDefault
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return OverrideMode.Inherit;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the override behavior of a configuration section that is in turn based on whether child configuration files can lock the configuration section.</summary>
		/// <returns>One of the <see cref="T:System.Configuration.OverrideMode" /> enumeration values.</returns>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000A7D0 File Offset: 0x000089D0
		public OverrideMode OverrideModeEffective
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return OverrideMode.Inherit;
			}
		}

		// Token: 0x0400013D RID: 317
		private ConfigurationSection parent;

		// Token: 0x0400013E RID: 318
		private ConfigurationAllowDefinition allow_definition = ConfigurationAllowDefinition.Everywhere;

		// Token: 0x0400013F RID: 319
		private ConfigurationAllowExeDefinition allow_exe_definition = ConfigurationAllowExeDefinition.MachineToApplication;

		// Token: 0x04000140 RID: 320
		private bool allow_location;

		// Token: 0x04000141 RID: 321
		private bool allow_override;

		// Token: 0x04000142 RID: 322
		private bool inherit_on_child_apps;

		// Token: 0x04000143 RID: 323
		private bool restart_on_external_changes;

		// Token: 0x04000144 RID: 324
		private bool require_permission;

		// Token: 0x04000145 RID: 325
		private string config_source = string.Empty;

		// Token: 0x04000146 RID: 326
		private bool force_update;

		// Token: 0x04000147 RID: 327
		private string name;

		// Token: 0x04000148 RID: 328
		private string type_name;

		// Token: 0x04000149 RID: 329
		private string raw_xml;

		// Token: 0x0400014A RID: 330
		private ProtectedConfigurationProvider protection_provider;

		// Token: 0x0400014B RID: 331
		[CompilerGenerated]
		private string <ConfigFilePath>k__BackingField;
	}
}
