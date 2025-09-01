using System;
using System.Collections;
using System.Configuration.Internal;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Permissions;
using System.Threading;
using System.Xml;
using Unity;

namespace System.Configuration
{
	/// <summary>Represents a configuration file that is applicable to a particular computer, application, or resource. This class cannot be inherited.</summary>
	// Token: 0x02000014 RID: 20
	public sealed class Configuration
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600004F RID: 79 RVA: 0x0000270C File Offset: 0x0000090C
		// (remove) Token: 0x06000050 RID: 80 RVA: 0x00002740 File Offset: 0x00000940
		internal static event ConfigurationSaveEventHandler SaveStart
		{
			[CompilerGenerated]
			add
			{
				ConfigurationSaveEventHandler configurationSaveEventHandler = Configuration.SaveStart;
				ConfigurationSaveEventHandler configurationSaveEventHandler2;
				do
				{
					configurationSaveEventHandler2 = configurationSaveEventHandler;
					ConfigurationSaveEventHandler value2 = (ConfigurationSaveEventHandler)Delegate.Combine(configurationSaveEventHandler2, value);
					configurationSaveEventHandler = Interlocked.CompareExchange<ConfigurationSaveEventHandler>(ref Configuration.SaveStart, value2, configurationSaveEventHandler2);
				}
				while (configurationSaveEventHandler != configurationSaveEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ConfigurationSaveEventHandler configurationSaveEventHandler = Configuration.SaveStart;
				ConfigurationSaveEventHandler configurationSaveEventHandler2;
				do
				{
					configurationSaveEventHandler2 = configurationSaveEventHandler;
					ConfigurationSaveEventHandler value2 = (ConfigurationSaveEventHandler)Delegate.Remove(configurationSaveEventHandler2, value);
					configurationSaveEventHandler = Interlocked.CompareExchange<ConfigurationSaveEventHandler>(ref Configuration.SaveStart, value2, configurationSaveEventHandler2);
				}
				while (configurationSaveEventHandler != configurationSaveEventHandler2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000051 RID: 81 RVA: 0x00002774 File Offset: 0x00000974
		// (remove) Token: 0x06000052 RID: 82 RVA: 0x000027A8 File Offset: 0x000009A8
		internal static event ConfigurationSaveEventHandler SaveEnd
		{
			[CompilerGenerated]
			add
			{
				ConfigurationSaveEventHandler configurationSaveEventHandler = Configuration.SaveEnd;
				ConfigurationSaveEventHandler configurationSaveEventHandler2;
				do
				{
					configurationSaveEventHandler2 = configurationSaveEventHandler;
					ConfigurationSaveEventHandler value2 = (ConfigurationSaveEventHandler)Delegate.Combine(configurationSaveEventHandler2, value);
					configurationSaveEventHandler = Interlocked.CompareExchange<ConfigurationSaveEventHandler>(ref Configuration.SaveEnd, value2, configurationSaveEventHandler2);
				}
				while (configurationSaveEventHandler != configurationSaveEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				ConfigurationSaveEventHandler configurationSaveEventHandler = Configuration.SaveEnd;
				ConfigurationSaveEventHandler configurationSaveEventHandler2;
				do
				{
					configurationSaveEventHandler2 = configurationSaveEventHandler;
					ConfigurationSaveEventHandler value2 = (ConfigurationSaveEventHandler)Delegate.Remove(configurationSaveEventHandler2, value);
					configurationSaveEventHandler = Interlocked.CompareExchange<ConfigurationSaveEventHandler>(ref Configuration.SaveEnd, value2, configurationSaveEventHandler2);
				}
				while (configurationSaveEventHandler != configurationSaveEventHandler2);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000027DC File Offset: 0x000009DC
		internal Configuration(Configuration parent, string locationSubPath)
		{
			this.elementData = new Hashtable();
			base..ctor();
			this.parent = parent;
			this.system = parent.system;
			this.rootGroup = parent.rootGroup;
			this.locationSubPath = locationSubPath;
			this.configPath = parent.ConfigPath;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000282C File Offset: 0x00000A2C
		internal Configuration(InternalConfigurationSystem system, string locationSubPath)
		{
			this.elementData = new Hashtable();
			base..ctor();
			this.hasFile = true;
			this.system = system;
			system.InitForConfiguration(ref locationSubPath, out this.configPath, out this.locationConfigPath);
			Configuration configuration = null;
			if (locationSubPath != null)
			{
				configuration = new Configuration(system, locationSubPath);
				if (this.locationConfigPath != null)
				{
					configuration = configuration.FindLocationConfiguration(this.locationConfigPath, configuration);
				}
			}
			this.Init(system, this.configPath, configuration);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000028A0 File Offset: 0x00000AA0
		internal Configuration FindLocationConfiguration(string relativePath, Configuration defaultConfiguration)
		{
			Configuration configuration = defaultConfiguration;
			if (!string.IsNullOrEmpty(this.LocationConfigPath))
			{
				Configuration parentWithFile = this.GetParentWithFile();
				if (parentWithFile != null)
				{
					string configPathFromLocationSubPath = this.system.Host.GetConfigPathFromLocationSubPath(this.configPath, relativePath);
					configuration = parentWithFile.FindLocationConfiguration(configPathFromLocationSubPath, defaultConfiguration);
				}
			}
			string text = this.configPath.Substring(1) + "/";
			if (relativePath.StartsWith(text, StringComparison.Ordinal))
			{
				relativePath = relativePath.Substring(text.Length);
			}
			ConfigurationLocation configurationLocation = this.Locations.FindBest(relativePath);
			if (configurationLocation == null)
			{
				return configuration;
			}
			configurationLocation.SetParentConfiguration(configuration);
			return configurationLocation.OpenConfiguration();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002938 File Offset: 0x00000B38
		internal void Init(IConfigSystem system, string configPath, Configuration parent)
		{
			this.system = system;
			this.configPath = configPath;
			this.streamName = system.Host.GetStreamName(configPath);
			this.parent = parent;
			if (parent != null)
			{
				this.rootGroup = parent.rootGroup;
			}
			else
			{
				this.rootGroup = new SectionGroupInfo();
				this.rootGroup.StreamName = this.streamName;
			}
			try
			{
				if (this.streamName != null)
				{
					this.Load();
				}
			}
			catch (XmlException ex)
			{
				throw new ConfigurationErrorsException(ex.Message, ex, this.streamName, 0);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000029D0 File Offset: 0x00000BD0
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000029D8 File Offset: 0x00000BD8
		internal Configuration Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal Configuration GetParentWithFile()
		{
			Configuration configuration = this.Parent;
			while (configuration != null && !configuration.HasFile)
			{
				configuration = configuration.Parent;
			}
			return configuration;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002A0D File Offset: 0x00000C0D
		internal string FileName
		{
			get
			{
				return this.streamName;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002A15 File Offset: 0x00000C15
		internal IInternalConfigHost ConfigHost
		{
			get
			{
				return this.system.Host;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002A22 File Offset: 0x00000C22
		internal string LocationConfigPath
		{
			get
			{
				return this.locationConfigPath;
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002A2C File Offset: 0x00000C2C
		internal string GetLocationSubPath()
		{
			Configuration configuration = this.parent;
			string text = null;
			while (configuration != null)
			{
				text = configuration.locationSubPath;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				configuration = configuration.parent;
			}
			return text;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002A60 File Offset: 0x00000C60
		internal string ConfigPath
		{
			get
			{
				return this.configPath;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.AppSettingsSection" /> object configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.AppSettingsSection" /> object representing the <see langword="appSettings" /> configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002A68 File Offset: 0x00000C68
		public AppSettingsSection AppSettings
		{
			get
			{
				return (AppSettingsSection)this.GetSection("appSettings");
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConnectionStringsSection" /> configuration-section object that applies to this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringsSection" /> configuration-section object representing the <see langword="connectionStrings" /> configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002A7A File Offset: 0x00000C7A
		public ConnectionStringsSection ConnectionStrings
		{
			get
			{
				return (ConnectionStringsSection)this.GetSection("connectionStrings");
			}
		}

		/// <summary>Gets the physical path to the configuration file represented by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The physical path to the configuration file represented by this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002A8C File Offset: 0x00000C8C
		public string FilePath
		{
			get
			{
				if (this.streamName == null && this.parent != null)
				{
					return this.parent.FilePath;
				}
				return this.streamName;
			}
		}

		/// <summary>Gets a value that indicates whether a file exists for the resource represented by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if there is a configuration file; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public bool HasFile
		{
			get
			{
				return this.hasFile;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public ContextInformation EvaluationContext
		{
			get
			{
				if (this.evaluationContext == null)
				{
					object ctx = this.system.Host.CreateConfigurationContext(this.configPath, this.GetLocationSubPath());
					this.evaluationContext = new ContextInformation(this, ctx);
				}
				return this.evaluationContext;
			}
		}

		/// <summary>Gets the locations defined within this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationLocationCollection" /> containing the locations defined within this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002AFD File Offset: 0x00000CFD
		public ConfigurationLocationCollection Locations
		{
			get
			{
				if (this.locations == null)
				{
					this.locations = new ConfigurationLocationCollection();
				}
				return this.locations;
			}
		}

		/// <summary>Gets or sets a value indicating whether the configuration file has an XML namespace.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration file has an XML namespace; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002B18 File Offset: 0x00000D18
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002B23 File Offset: 0x00000D23
		public bool NamespaceDeclared
		{
			get
			{
				return this.rootNamespace != null;
			}
			set
			{
				this.rootNamespace = (value ? "http://schemas.microsoft.com/.NetConfiguration/v2.0" : null);
			}
		}

		/// <summary>Gets the root <see cref="T:System.Configuration.ConfigurationSectionGroup" /> for this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The root section group for this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002B36 File Offset: 0x00000D36
		public ConfigurationSectionGroup RootSectionGroup
		{
			get
			{
				if (this.rootSectionGroup == null)
				{
					this.rootSectionGroup = new ConfigurationSectionGroup();
					this.rootSectionGroup.Initialize(this, this.rootGroup);
				}
				return this.rootSectionGroup;
			}
		}

		/// <summary>Gets a collection of the section groups defined by this configuration.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> collection representing the collection of section groups for this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002B63 File Offset: 0x00000D63
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				return this.RootSectionGroup.SectionGroups;
			}
		}

		/// <summary>Gets a collection of the sections defined by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A collection of the sections defined by this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002B70 File Offset: 0x00000D70
		public ConfigurationSectionCollection Sections
		{
			get
			{
				return this.RootSectionGroup.Sections;
			}
		}

		/// <summary>Returns the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <param name="sectionName">The path to the section to be returned.</param>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		// Token: 0x0600006A RID: 106 RVA: 0x00002B80 File Offset: 0x00000D80
		public ConfigurationSection GetSection(string sectionName)
		{
			string[] array = sectionName.Split('/', StringSplitOptions.None);
			if (array.Length == 1)
			{
				return this.Sections[array[0]];
			}
			ConfigurationSectionGroup configurationSectionGroup = this.SectionGroups[array[0]];
			int num = 1;
			while (configurationSectionGroup != null && num < array.Length - 1)
			{
				configurationSectionGroup = configurationSectionGroup.SectionGroups[array[num]];
				num++;
			}
			if (configurationSectionGroup != null)
			{
				return configurationSectionGroup.Sections[array[array.Length - 1]];
			}
			return null;
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <param name="sectionGroupName">The path name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> to return.</param>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> specified.</returns>
		// Token: 0x0600006B RID: 107 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public ConfigurationSectionGroup GetSectionGroup(string sectionGroupName)
		{
			string[] array = sectionGroupName.Split('/', StringSplitOptions.None);
			ConfigurationSectionGroup configurationSectionGroup = this.SectionGroups[array[0]];
			int num = 1;
			while (configurationSectionGroup != null && num < array.Length)
			{
				configurationSectionGroup = configurationSectionGroup.SectionGroups[array[num]];
				num++;
			}
			return configurationSectionGroup;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C3C File Offset: 0x00000E3C
		internal ConfigurationSection GetSectionInstance(SectionInfo config, bool createDefaultInstance)
		{
			object obj = this.elementData[config];
			ConfigurationSection configurationSection = obj as ConfigurationSection;
			if (configurationSection != null || !createDefaultInstance)
			{
				return configurationSection;
			}
			object obj2 = config.CreateInstance();
			configurationSection = (obj2 as ConfigurationSection);
			if (configurationSection == null)
			{
				configurationSection = new DefaultSection
				{
					SectionHandler = (obj2 as IConfigurationSectionHandler)
				};
			}
			configurationSection.Configuration = this;
			ConfigurationSection configurationSection2 = null;
			if (this.parent != null)
			{
				configurationSection2 = this.parent.GetSectionInstance(config, true);
				configurationSection.SectionInformation.SetParentSection(configurationSection2);
			}
			configurationSection.SectionInformation.ConfigFilePath = this.FilePath;
			configurationSection.ConfigContext = this.system.Host.CreateDeprecatedConfigContext(this.configPath);
			string text = obj as string;
			configurationSection.RawXml = text;
			configurationSection.Reset(configurationSection2);
			if (text != null)
			{
				XmlTextReader xmlTextReader = new ConfigXmlTextReader(new StringReader(text), this.FilePath);
				configurationSection.DeserializeSection(xmlTextReader);
				xmlTextReader.Close();
				if (!string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource) && !string.IsNullOrEmpty(this.FilePath))
				{
					configurationSection.DeserializeConfigSource(Path.GetDirectoryName(this.FilePath));
				}
			}
			this.elementData[config] = configurationSection;
			return configurationSection;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002D5C File Offset: 0x00000F5C
		internal ConfigurationSectionGroup GetSectionGroupInstance(SectionGroupInfo group)
		{
			ConfigurationSectionGroup configurationSectionGroup = group.CreateInstance() as ConfigurationSectionGroup;
			if (configurationSectionGroup != null)
			{
				configurationSectionGroup.Initialize(this, group);
			}
			return configurationSectionGroup;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002D81 File Offset: 0x00000F81
		internal void SetConfigurationSection(SectionInfo config, ConfigurationSection sec)
		{
			this.elementData[config] = sec;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002D81 File Offset: 0x00000F81
		internal void SetSectionXml(SectionInfo config, string data)
		{
			this.elementData[config] = data;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002D90 File Offset: 0x00000F90
		internal string GetSectionXml(SectionInfo config)
		{
			return this.elementData[config] as string;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002DA4 File Offset: 0x00000FA4
		internal void CreateSection(SectionGroupInfo group, string name, ConfigurationSection sec)
		{
			if (group.HasChild(name))
			{
				throw new ConfigurationErrorsException("Cannot add a ConfigurationSection. A section or section group already exists with the name '" + name + "'");
			}
			if (!this.HasFile && !sec.SectionInformation.AllowLocation)
			{
				throw new ConfigurationErrorsException("The configuration section <" + name + "> cannot be defined inside a <location> element.");
			}
			if (!this.system.Host.IsDefinitionAllowed(this.configPath, sec.SectionInformation.AllowDefinition, sec.SectionInformation.AllowExeDefinition))
			{
				object obj = (sec.SectionInformation.AllowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication) ? sec.SectionInformation.AllowExeDefinition : sec.SectionInformation.AllowDefinition;
				throw new ConfigurationErrorsException(string.Concat(new string[]
				{
					"The section <",
					name,
					"> can't be defined in this configuration file (the allowed definition context is '",
					(obj != null) ? obj.ToString() : null,
					"')."
				}));
			}
			if (sec.SectionInformation.Type == null)
			{
				sec.SectionInformation.Type = this.system.Host.GetConfigTypeName(sec.GetType());
			}
			SectionInfo sectionInfo = new SectionInfo(name, sec.SectionInformation);
			sectionInfo.StreamName = this.streamName;
			sectionInfo.ConfigHost = this.system.Host;
			group.AddChild(sectionInfo);
			this.elementData[sectionInfo] = sec;
			sec.Configuration = this;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F0C File Offset: 0x0000110C
		internal void CreateSectionGroup(SectionGroupInfo parentGroup, string name, ConfigurationSectionGroup sec)
		{
			if (parentGroup.HasChild(name))
			{
				throw new ConfigurationErrorsException("Cannot add a ConfigurationSectionGroup. A section or section group already exists with the name '" + name + "'");
			}
			if (sec.Type == null)
			{
				sec.Type = this.system.Host.GetConfigTypeName(sec.GetType());
			}
			sec.SetName(name);
			SectionGroupInfo sectionGroupInfo = new SectionGroupInfo(name, sec.Type);
			sectionGroupInfo.StreamName = this.streamName;
			sectionGroupInfo.ConfigHost = this.system.Host;
			parentGroup.AddChild(sectionGroupInfo);
			this.elementData[sectionGroupInfo] = sec;
			sec.Initialize(this, sectionGroupInfo);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002FA9 File Offset: 0x000011A9
		internal void RemoveConfigInfo(ConfigInfo config)
		{
			this.elementData.Remove(config);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		// Token: 0x06000074 RID: 116 RVA: 0x00002FB7 File Offset: 0x000011B7
		public void Save()
		{
			this.Save(ConfigurationSaveMode.Modified, false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		// Token: 0x06000075 RID: 117 RVA: 0x00002FC1 File Offset: 0x000011C1
		public void Save(ConfigurationSaveMode saveMode)
		{
			this.Save(saveMode, false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <param name="forceSaveAll">
		///   <see langword="true" /> to save even if the configuration was not modified; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		// Token: 0x06000076 RID: 118 RVA: 0x00002FCC File Offset: 0x000011CC
		public void Save(ConfigurationSaveMode saveMode, bool forceSaveAll)
		{
			if (!forceSaveAll && saveMode != ConfigurationSaveMode.Full && !this.HasValues(saveMode))
			{
				this.ResetModified();
				return;
			}
			ConfigurationSaveEventHandler saveStart = Configuration.SaveStart;
			ConfigurationSaveEventHandler saveEnd = Configuration.SaveEnd;
			object obj = null;
			Exception ex = null;
			Stream stream = this.system.Host.OpenStreamForWrite(this.streamName, null, ref obj);
			try
			{
				if (saveStart != null)
				{
					saveStart(this, new ConfigurationSaveEventArgs(this.streamName, true, null, obj));
				}
				this.Save(stream, saveMode, forceSaveAll);
				this.system.Host.WriteCompleted(this.streamName, true, obj);
			}
			catch (Exception ex)
			{
				this.system.Host.WriteCompleted(this.streamName, false, obj);
				throw;
			}
			finally
			{
				stream.Close();
				if (saveEnd != null)
				{
					saveEnd(this, new ConfigurationSaveEventArgs(this.streamName, false, ex, obj));
				}
			}
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		// Token: 0x06000077 RID: 119 RVA: 0x000030B0 File Offset: 0x000012B0
		public void SaveAs(string filename)
		{
			this.SaveAs(filename, ConfigurationSaveMode.Modified, false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.  
		/// -or-
		///  The configuration file has changed.</exception>
		// Token: 0x06000078 RID: 120 RVA: 0x000030BB File Offset: 0x000012BB
		public void SaveAs(string filename, ConfigurationSaveMode saveMode)
		{
			this.SaveAs(filename, saveMode, false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <param name="forceSaveAll">
		///   <see langword="true" /> to save even if the configuration was not modified; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filename" /> is null or an empty string ("").</exception>
		// Token: 0x06000079 RID: 121 RVA: 0x000030C8 File Offset: 0x000012C8
		[MonoInternalNote("Detect if file has changed")]
		public void SaveAs(string filename, ConfigurationSaveMode saveMode, bool forceSaveAll)
		{
			if (!forceSaveAll && saveMode != ConfigurationSaveMode.Full && !this.HasValues(saveMode))
			{
				this.ResetModified();
				return;
			}
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(filename));
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			this.Save(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write), saveMode, forceSaveAll);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003118 File Offset: 0x00001318
		private void Save(Stream stream, ConfigurationSaveMode mode, bool forceUpdateAll)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(new StreamWriter(stream));
			xmlTextWriter.Formatting = Formatting.Indented;
			try
			{
				xmlTextWriter.WriteStartDocument();
				if (this.rootNamespace != null)
				{
					xmlTextWriter.WriteStartElement("configuration", this.rootNamespace);
				}
				else
				{
					xmlTextWriter.WriteStartElement("configuration");
				}
				if (this.rootGroup.HasConfigContent(this))
				{
					this.rootGroup.WriteConfig(this, xmlTextWriter, mode);
				}
				foreach (object obj in this.Locations)
				{
					ConfigurationLocation configurationLocation = (ConfigurationLocation)obj;
					if (configurationLocation.OpenedConfiguration == null)
					{
						xmlTextWriter.WriteRaw("\n");
						xmlTextWriter.WriteRaw(configurationLocation.XmlContent);
					}
					else
					{
						xmlTextWriter.WriteStartElement("location");
						xmlTextWriter.WriteAttributeString("path", configurationLocation.Path);
						if (!configurationLocation.AllowOverride)
						{
							xmlTextWriter.WriteAttributeString("allowOverride", "false");
						}
						configurationLocation.OpenedConfiguration.SaveData(xmlTextWriter, mode, forceUpdateAll);
						xmlTextWriter.WriteEndElement();
					}
				}
				this.SaveData(xmlTextWriter, mode, forceUpdateAll);
				xmlTextWriter.WriteEndElement();
				this.ResetModified();
			}
			finally
			{
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003260 File Offset: 0x00001460
		private void SaveData(XmlTextWriter tw, ConfigurationSaveMode mode, bool forceUpdateAll)
		{
			this.rootGroup.WriteRootData(tw, this, mode);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003270 File Offset: 0x00001470
		private bool HasValues(ConfigurationSaveMode mode)
		{
			foreach (object obj in this.Locations)
			{
				ConfigurationLocation configurationLocation = (ConfigurationLocation)obj;
				if (configurationLocation.OpenedConfiguration != null && configurationLocation.OpenedConfiguration.HasValues(mode))
				{
					return true;
				}
			}
			return this.rootGroup.HasValues(this, mode);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032EC File Offset: 0x000014EC
		private void ResetModified()
		{
			foreach (object obj in this.Locations)
			{
				ConfigurationLocation configurationLocation = (ConfigurationLocation)obj;
				if (configurationLocation.OpenedConfiguration != null)
				{
					configurationLocation.OpenedConfiguration.ResetModified();
				}
			}
			this.rootGroup.ResetModified(this);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003360 File Offset: 0x00001560
		private bool Load()
		{
			if (string.IsNullOrEmpty(this.streamName))
			{
				return true;
			}
			Stream stream = null;
			try
			{
				stream = this.system.Host.OpenStreamForRead(this.streamName);
				if (stream == null)
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
			using (XmlTextReader xmlTextReader = new ConfigXmlTextReader(stream, this.streamName))
			{
				this.ReadConfigFile(xmlTextReader, this.streamName);
			}
			this.ResetModified();
			return true;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000033F0 File Offset: 0x000015F0
		private void ReadConfigFile(XmlReader reader, string fileName)
		{
			reader.MoveToContent();
			if (reader.NodeType != XmlNodeType.Element || reader.Name != "configuration")
			{
				this.ThrowException("Configuration file does not have a valid root element", reader);
			}
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "xmlns")
					{
						this.rootNamespace = reader.Value;
					}
					else
					{
						this.ThrowException(string.Format("Unrecognized attribute '{0}' in root element", reader.LocalName), reader);
					}
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			reader.MoveToContent();
			if (reader.LocalName == "configSections")
			{
				if (reader.HasAttributes)
				{
					this.ThrowException("Unrecognized attribute in <configSections>.", reader);
				}
				this.rootGroup.ReadConfig(this, fileName, reader);
			}
			this.rootGroup.ReadRootData(reader, this, true);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034D9 File Offset: 0x000016D9
		internal void ReadData(XmlReader reader, bool allowOverride)
		{
			this.rootGroup.ReadData(this, reader, allowOverride);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034EC File Offset: 0x000016EC
		private void ThrowException(string text, XmlReader reader)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			throw new ConfigurationErrorsException(text, this.streamName, (xmlLineInfo != null) ? xmlLineInfo.LineNumber : 0);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003518 File Offset: 0x00001718
		internal Configuration()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Specifies a function delegate that is used to transform assembly strings in configuration files.</summary>
		/// <returns>A delegate that transforms type strings. The default value is <see langword="null" />.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000351F File Offset: 0x0000171F
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003518 File Offset: 0x00001718
		public Func<string, string> AssemblyStringTransformer
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Specifies the targeted version of the .NET Framework when a version earlier than the current one is targeted.</summary>
		/// <returns>The name of the targeted version of the .NET Framework. The default value is <see langword="null" />, which indicates that the current version is targeted.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003527 File Offset: 0x00001727
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003518 File Offset: 0x00001718
		public FrameworkName TargetFramework
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Specifies a function delegate that is used to transform type strings in configuration files.</summary>
		/// <returns>A delegate that transforms type strings. The default value is <see langword="null" />.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000351F File Offset: 0x0000171F
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003518 File Offset: 0x00001718
		public Func<string, string> TypeStringTransformer
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			[ConfigurationPermission(SecurityAction.Demand, Unrestricted = true)]
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0400003F RID: 63
		private Configuration parent;

		// Token: 0x04000040 RID: 64
		private Hashtable elementData;

		// Token: 0x04000041 RID: 65
		private string streamName;

		// Token: 0x04000042 RID: 66
		private ConfigurationSectionGroup rootSectionGroup;

		// Token: 0x04000043 RID: 67
		private ConfigurationLocationCollection locations;

		// Token: 0x04000044 RID: 68
		private SectionGroupInfo rootGroup;

		// Token: 0x04000045 RID: 69
		private IConfigSystem system;

		// Token: 0x04000046 RID: 70
		private bool hasFile;

		// Token: 0x04000047 RID: 71
		private string rootNamespace;

		// Token: 0x04000048 RID: 72
		private string configPath;

		// Token: 0x04000049 RID: 73
		private string locationConfigPath;

		// Token: 0x0400004A RID: 74
		private string locationSubPath;

		// Token: 0x0400004B RID: 75
		[CompilerGenerated]
		private static ConfigurationSaveEventHandler SaveStart;

		// Token: 0x0400004C RID: 76
		[CompilerGenerated]
		private static ConfigurationSaveEventHandler SaveEnd;

		// Token: 0x0400004D RID: 77
		private ContextInformation evaluationContext;
	}
}
