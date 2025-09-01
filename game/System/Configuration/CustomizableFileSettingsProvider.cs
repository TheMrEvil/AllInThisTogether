using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020001AC RID: 428
	internal class CustomizableFileSettingsProvider : SettingsProvider, IApplicationSettingsProvider
	{
		// Token: 0x06000B37 RID: 2871 RVA: 0x00030069 File Offset: 0x0002E269
		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(name, config);
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x00030073 File Offset: 0x0002E273
		internal static string UserRoamingFullPath
		{
			get
			{
				return Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, CustomizableFileSettingsProvider.userRoamingName);
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x00030084 File Offset: 0x0002E284
		internal static string UserLocalFullPath
		{
			get
			{
				return Path.Combine(CustomizableFileSettingsProvider.userLocalPath, CustomizableFileSettingsProvider.userLocalName);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00030095 File Offset: 0x0002E295
		public static string PrevUserRoamingFullPath
		{
			get
			{
				return Path.Combine(CustomizableFileSettingsProvider.userRoamingPathPrevVersion, CustomizableFileSettingsProvider.userRoamingName);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x000300A6 File Offset: 0x0002E2A6
		public static string PrevUserLocalFullPath
		{
			get
			{
				return Path.Combine(CustomizableFileSettingsProvider.userLocalPathPrevVersion, CustomizableFileSettingsProvider.userLocalName);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x000300B7 File Offset: 0x0002E2B7
		public static string UserRoamingPath
		{
			get
			{
				return CustomizableFileSettingsProvider.userRoamingPath;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x000300BE File Offset: 0x0002E2BE
		public static string UserLocalPath
		{
			get
			{
				return CustomizableFileSettingsProvider.userLocalPath;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000300C5 File Offset: 0x0002E2C5
		public static string UserRoamingName
		{
			get
			{
				return CustomizableFileSettingsProvider.userRoamingName;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x000300CC File Offset: 0x0002E2CC
		public static string UserLocalName
		{
			get
			{
				return CustomizableFileSettingsProvider.userLocalName;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000300D3 File Offset: 0x0002E2D3
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x000300DC File Offset: 0x0002E2DC
		public static UserConfigLocationOption UserConfigSelector
		{
			get
			{
				return CustomizableFileSettingsProvider.userConfig;
			}
			set
			{
				CustomizableFileSettingsProvider.userConfig = value;
				if ((CustomizableFileSettingsProvider.userConfig & UserConfigLocationOption.Other) != (UserConfigLocationOption)0U)
				{
					CustomizableFileSettingsProvider.isVersionMajor = false;
					CustomizableFileSettingsProvider.isVersionMinor = false;
					CustomizableFileSettingsProvider.isVersionBuild = false;
					CustomizableFileSettingsProvider.isVersionRevision = false;
					CustomizableFileSettingsProvider.isCompany = false;
					return;
				}
				CustomizableFileSettingsProvider.isVersionRevision = ((CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)8U) > (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isVersionBuild = (CustomizableFileSettingsProvider.isVersionRevision | (CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)4U) > (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isVersionMinor = (CustomizableFileSettingsProvider.isVersionBuild | (CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)2U) > (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isVersionMajor = (CustomizableFileSettingsProvider.IsVersionMinor | (CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)1U) > (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isCompany = ((CustomizableFileSettingsProvider.userConfig & (UserConfigLocationOption)16U) > (UserConfigLocationOption)0U);
				CustomizableFileSettingsProvider.isProduct = ((CustomizableFileSettingsProvider.userConfig & UserConfigLocationOption.Product) > (UserConfigLocationOption)0U);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x00030189 File Offset: 0x0002E389
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x00030190 File Offset: 0x0002E390
		public static bool IsVersionMajor
		{
			get
			{
				return CustomizableFileSettingsProvider.isVersionMajor;
			}
			set
			{
				CustomizableFileSettingsProvider.isVersionMajor = value;
				CustomizableFileSettingsProvider.isVersionMinor = false;
				CustomizableFileSettingsProvider.isVersionBuild = false;
				CustomizableFileSettingsProvider.isVersionRevision = false;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000301AA File Offset: 0x0002E3AA
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x000301B1 File Offset: 0x0002E3B1
		public static bool IsVersionMinor
		{
			get
			{
				return CustomizableFileSettingsProvider.isVersionMinor;
			}
			set
			{
				CustomizableFileSettingsProvider.isVersionMinor = value;
				if (CustomizableFileSettingsProvider.isVersionMinor)
				{
					CustomizableFileSettingsProvider.isVersionMajor = true;
				}
				CustomizableFileSettingsProvider.isVersionBuild = false;
				CustomizableFileSettingsProvider.isVersionRevision = false;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x000301D2 File Offset: 0x0002E3D2
		// (set) Token: 0x06000B47 RID: 2887 RVA: 0x000301D9 File Offset: 0x0002E3D9
		public static bool IsVersionBuild
		{
			get
			{
				return CustomizableFileSettingsProvider.isVersionBuild;
			}
			set
			{
				CustomizableFileSettingsProvider.isVersionBuild = value;
				if (CustomizableFileSettingsProvider.isVersionBuild)
				{
					CustomizableFileSettingsProvider.isVersionMajor = true;
					CustomizableFileSettingsProvider.isVersionMinor = true;
				}
				CustomizableFileSettingsProvider.isVersionRevision = false;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x000301FA File Offset: 0x0002E3FA
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00030201 File Offset: 0x0002E401
		public static bool IsVersionRevision
		{
			get
			{
				return CustomizableFileSettingsProvider.isVersionRevision;
			}
			set
			{
				CustomizableFileSettingsProvider.isVersionRevision = value;
				if (CustomizableFileSettingsProvider.isVersionRevision)
				{
					CustomizableFileSettingsProvider.isVersionMajor = true;
					CustomizableFileSettingsProvider.isVersionMinor = true;
					CustomizableFileSettingsProvider.isVersionBuild = true;
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00030222 File Offset: 0x0002E422
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00030229 File Offset: 0x0002E429
		public static bool IsCompany
		{
			get
			{
				return CustomizableFileSettingsProvider.isCompany;
			}
			set
			{
				CustomizableFileSettingsProvider.isCompany = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00030231 File Offset: 0x0002E431
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00030238 File Offset: 0x0002E438
		public static bool IsEvidence
		{
			get
			{
				return CustomizableFileSettingsProvider.isEvidence;
			}
			set
			{
				CustomizableFileSettingsProvider.isEvidence = value;
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00030240 File Offset: 0x0002E440
		private static string GetCompanyName()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			AssemblyCompanyAttribute[] array = (AssemblyCompanyAttribute[])assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
			if (array != null && array.Length != 0)
			{
				return array[0].Company;
			}
			MethodInfo entryPoint = assembly.EntryPoint;
			Type type = (entryPoint != null) ? entryPoint.DeclaringType : null;
			if (!(type != null) || string.IsNullOrEmpty(type.Namespace))
			{
				return "Program";
			}
			int num = type.Namespace.IndexOf('.');
			if (num >= 0)
			{
				return type.Namespace.Substring(0, num);
			}
			return type.Namespace;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000302E8 File Offset: 0x0002E4E8
		private static string GetProductName()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
			return string.Format("{0}_{1}_{2}", AppDomain.CurrentDomain.FriendlyName, (publicKeyToken != null && publicKeyToken.Length != 0) ? "StrongName" : "Url", CustomizableFileSettingsProvider.GetEvidenceHash());
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00030344 File Offset: 0x0002E544
		private static string GetEvidenceHash()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
			byte[] array = SHA1.Create().ComputeHash((publicKeyToken != null && publicKeyToken.Length != 0) ? publicKeyToken : Encoding.UTF8.GetBytes(assembly.EscapedCodeBase));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000303D0 File Offset: 0x0002E5D0
		private static string GetProductVersion()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			if (assembly == null)
			{
				assembly = Assembly.GetCallingAssembly();
			}
			if (assembly == null)
			{
				return string.Empty;
			}
			return assembly.GetName().Version.ToString();
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00030414 File Offset: 0x0002E614
		private static void CreateUserConfigPath()
		{
			if (CustomizableFileSettingsProvider.userDefine)
			{
				return;
			}
			if (CustomizableFileSettingsProvider.ProductName == "")
			{
				CustomizableFileSettingsProvider.ProductName = CustomizableFileSettingsProvider.GetProductName();
			}
			if (CustomizableFileSettingsProvider.CompanyName == "")
			{
				CustomizableFileSettingsProvider.CompanyName = CustomizableFileSettingsProvider.GetCompanyName();
			}
			if (CustomizableFileSettingsProvider.ForceVersion == "")
			{
				CustomizableFileSettingsProvider.ProductVersion = CustomizableFileSettingsProvider.GetProductVersion().Split('.', StringSplitOptions.None);
			}
			if (CustomizableFileSettingsProvider.userRoamingBasePath == "")
			{
				CustomizableFileSettingsProvider.userRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			}
			else
			{
				CustomizableFileSettingsProvider.userRoamingPath = CustomizableFileSettingsProvider.userRoamingBasePath;
			}
			if (CustomizableFileSettingsProvider.userLocalBasePath == "")
			{
				CustomizableFileSettingsProvider.userLocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			}
			else
			{
				CustomizableFileSettingsProvider.userLocalPath = CustomizableFileSettingsProvider.userLocalBasePath;
			}
			if (CustomizableFileSettingsProvider.isCompany)
			{
				CustomizableFileSettingsProvider.userRoamingPath = Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, CustomizableFileSettingsProvider.CompanyName);
				CustomizableFileSettingsProvider.userLocalPath = Path.Combine(CustomizableFileSettingsProvider.userLocalPath, CustomizableFileSettingsProvider.CompanyName);
			}
			if (CustomizableFileSettingsProvider.isProduct)
			{
				if (CustomizableFileSettingsProvider.isEvidence)
				{
					Assembly assembly = Assembly.GetEntryAssembly();
					if (assembly == null)
					{
						assembly = Assembly.GetCallingAssembly();
					}
					byte[] publicKeyToken = assembly.GetName().GetPublicKeyToken();
					CustomizableFileSettingsProvider.ProductName = string.Format("{0}_{1}_{2}", CustomizableFileSettingsProvider.ProductName, (publicKeyToken != null) ? "StrongName" : "Url", CustomizableFileSettingsProvider.GetEvidenceHash());
				}
				CustomizableFileSettingsProvider.userRoamingPath = Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, CustomizableFileSettingsProvider.ProductName);
				CustomizableFileSettingsProvider.userLocalPath = Path.Combine(CustomizableFileSettingsProvider.userLocalPath, CustomizableFileSettingsProvider.ProductName);
			}
			string text;
			if (CustomizableFileSettingsProvider.ForceVersion == "")
			{
				if (CustomizableFileSettingsProvider.isVersionRevision)
				{
					text = string.Format("{0}.{1}.{2}.{3}", new object[]
					{
						CustomizableFileSettingsProvider.ProductVersion[0],
						CustomizableFileSettingsProvider.ProductVersion[1],
						CustomizableFileSettingsProvider.ProductVersion[2],
						CustomizableFileSettingsProvider.ProductVersion[3]
					});
				}
				else if (CustomizableFileSettingsProvider.isVersionBuild)
				{
					text = string.Format("{0}.{1}.{2}", CustomizableFileSettingsProvider.ProductVersion[0], CustomizableFileSettingsProvider.ProductVersion[1], CustomizableFileSettingsProvider.ProductVersion[2]);
				}
				else if (CustomizableFileSettingsProvider.isVersionMinor)
				{
					text = string.Format("{0}.{1}", CustomizableFileSettingsProvider.ProductVersion[0], CustomizableFileSettingsProvider.ProductVersion[1]);
				}
				else if (CustomizableFileSettingsProvider.isVersionMajor)
				{
					text = CustomizableFileSettingsProvider.ProductVersion[0];
				}
				else
				{
					text = "";
				}
			}
			else
			{
				text = CustomizableFileSettingsProvider.ForceVersion;
			}
			string text2 = CustomizableFileSettingsProvider.PrevVersionPath(CustomizableFileSettingsProvider.userRoamingPath, text);
			string text3 = CustomizableFileSettingsProvider.PrevVersionPath(CustomizableFileSettingsProvider.userLocalPath, text);
			CustomizableFileSettingsProvider.userRoamingPath = Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, text);
			CustomizableFileSettingsProvider.userLocalPath = Path.Combine(CustomizableFileSettingsProvider.userLocalPath, text);
			if (text2 != "")
			{
				CustomizableFileSettingsProvider.userRoamingPathPrevVersion = Path.Combine(CustomizableFileSettingsProvider.userRoamingPath, text2);
			}
			if (text3 != "")
			{
				CustomizableFileSettingsProvider.userLocalPathPrevVersion = Path.Combine(CustomizableFileSettingsProvider.userLocalPath, text3);
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000306B8 File Offset: 0x0002E8B8
		private static string PrevVersionPath(string dirName, string currentVersion)
		{
			string text = "";
			if (!Directory.Exists(dirName))
			{
				return text;
			}
			foreach (DirectoryInfo directoryInfo in new DirectoryInfo(dirName).GetDirectories())
			{
				if (string.Compare(currentVersion, directoryInfo.Name, StringComparison.Ordinal) > 0 && string.Compare(text, directoryInfo.Name, StringComparison.Ordinal) < 0)
				{
					text = directoryInfo.Name;
				}
			}
			return text;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0003071B File Offset: 0x0002E91B
		public static bool SetUserRoamingPath(string configPath)
		{
			if (CustomizableFileSettingsProvider.CheckPath(configPath))
			{
				CustomizableFileSettingsProvider.userRoamingBasePath = configPath;
				return true;
			}
			return false;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0003072E File Offset: 0x0002E92E
		public static bool SetUserLocalPath(string configPath)
		{
			if (CustomizableFileSettingsProvider.CheckPath(configPath))
			{
				CustomizableFileSettingsProvider.userLocalBasePath = configPath;
				return true;
			}
			return false;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00030741 File Offset: 0x0002E941
		private static bool CheckFileName(string configFile)
		{
			return configFile.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00030751 File Offset: 0x0002E951
		public static bool SetUserRoamingFileName(string configFile)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(configFile))
			{
				CustomizableFileSettingsProvider.userRoamingName = configFile;
				return true;
			}
			return false;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00030764 File Offset: 0x0002E964
		public static bool SetUserLocalFileName(string configFile)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(configFile))
			{
				CustomizableFileSettingsProvider.userLocalName = configFile;
				return true;
			}
			return false;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00030777 File Offset: 0x0002E977
		public static bool SetCompanyName(string companyName)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(companyName))
			{
				CustomizableFileSettingsProvider.CompanyName = companyName;
				return true;
			}
			return false;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0003078A File Offset: 0x0002E98A
		public static bool SetProductName(string productName)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(productName))
			{
				CustomizableFileSettingsProvider.ProductName = productName;
				return true;
			}
			return false;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003079D File Offset: 0x0002E99D
		public static bool SetVersion(int major)
		{
			CustomizableFileSettingsProvider.ForceVersion = string.Format("{0}", major);
			return true;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000307B5 File Offset: 0x0002E9B5
		public static bool SetVersion(int major, int minor)
		{
			CustomizableFileSettingsProvider.ForceVersion = string.Format("{0}.{1}", major, minor);
			return true;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000307D3 File Offset: 0x0002E9D3
		public static bool SetVersion(int major, int minor, int build)
		{
			CustomizableFileSettingsProvider.ForceVersion = string.Format("{0}.{1}.{2}", major, minor, build);
			return true;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000307F7 File Offset: 0x0002E9F7
		public static bool SetVersion(int major, int minor, int build, int revision)
		{
			CustomizableFileSettingsProvider.ForceVersion = string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				major,
				minor,
				build,
				revision
			});
			return true;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00030833 File Offset: 0x0002EA33
		public static bool SetVersion(string forceVersion)
		{
			if (CustomizableFileSettingsProvider.CheckFileName(forceVersion))
			{
				CustomizableFileSettingsProvider.ForceVersion = forceVersion;
				return true;
			}
			return false;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00030848 File Offset: 0x0002EA48
		private static bool CheckPath(string configPath)
		{
			char[] invalidPathChars = Path.GetInvalidPathChars();
			if (configPath.IndexOfAny(invalidPathChars) >= 0)
			{
				return false;
			}
			string path = configPath;
			string fileName;
			while ((fileName = Path.GetFileName(path)) != "")
			{
				if (!CustomizableFileSettingsProvider.CheckFileName(fileName))
				{
					return false;
				}
				path = Path.GetDirectoryName(path);
			}
			return true;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x00030891 File Offset: 0x0002EA91
		public override string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00030899 File Offset: 0x0002EA99
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x000308A1 File Offset: 0x0002EAA1
		public override string ApplicationName
		{
			get
			{
				return this.app_name;
			}
			set
			{
				this.app_name = value;
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x000308AC File Offset: 0x0002EAAC
		private string StripXmlHeader(string serializedValue)
		{
			if (serializedValue == null)
			{
				return string.Empty;
			}
			XmlElement xmlElement = new XmlDocument().CreateElement("value");
			xmlElement.InnerXml = serializedValue;
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.XmlDeclaration)
				{
					xmlElement.RemoveChild(xmlNode);
					break;
				}
			}
			return xmlElement.InnerXml;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00030938 File Offset: 0x0002EB38
		private void SaveProperties(ExeConfigurationFileMap exeMap, SettingsPropertyValueCollection collection, ConfigurationUserLevel level, SettingsContext context, bool checkUserLevel)
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeMap, level);
			UserSettingsGroup userSettingsGroup = configuration.GetSectionGroup("userSettings") as UserSettingsGroup;
			bool flag = level == ConfigurationUserLevel.PerUserRoaming;
			if (userSettingsGroup == null)
			{
				userSettingsGroup = new UserSettingsGroup();
				configuration.SectionGroups.Add("userSettings", userSettingsGroup);
			}
			ApplicationSettingsBase currentSettings = context.CurrentSettings;
			string name = this.NormalizeInvalidXmlChars(((currentSettings != null) ? currentSettings.GetType() : typeof(ApplicationSettingsBase)).FullName);
			ClientSettingsSection clientSettingsSection = userSettingsGroup.Sections.Get(name) as ClientSettingsSection;
			if (clientSettingsSection == null)
			{
				clientSettingsSection = new ClientSettingsSection();
				userSettingsGroup.Sections.Add(name, clientSettingsSection);
			}
			bool flag2 = false;
			if (clientSettingsSection == null)
			{
				return;
			}
			foreach (object obj in collection)
			{
				SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
				if ((!checkUserLevel || settingsPropertyValue.Property.Attributes.Contains(typeof(SettingsManageabilityAttribute)) == flag) && !settingsPropertyValue.Property.Attributes.Contains(typeof(ApplicationScopedSettingAttribute)))
				{
					flag2 = true;
					SettingElement settingElement = clientSettingsSection.Settings.Get(settingsPropertyValue.Name);
					if (settingElement == null)
					{
						settingElement = new SettingElement(settingsPropertyValue.Name, settingsPropertyValue.Property.SerializeAs);
						clientSettingsSection.Settings.Add(settingElement);
					}
					if (settingElement.Value.ValueXml == null)
					{
						settingElement.Value.ValueXml = new XmlDocument().CreateElement("value");
					}
					switch (settingsPropertyValue.Property.SerializeAs)
					{
					case SettingsSerializeAs.String:
						settingElement.Value.ValueXml.InnerText = (settingsPropertyValue.SerializedValue as string);
						break;
					case SettingsSerializeAs.Xml:
						settingElement.Value.ValueXml.InnerXml = this.StripXmlHeader(settingsPropertyValue.SerializedValue as string);
						break;
					case SettingsSerializeAs.Binary:
						settingElement.Value.ValueXml.InnerText = ((settingsPropertyValue.SerializedValue != null) ? Convert.ToBase64String(settingsPropertyValue.SerializedValue as byte[]) : string.Empty);
						break;
					default:
						throw new NotImplementedException();
					}
				}
			}
			if (flag2)
			{
				configuration.Save(ConfigurationSaveMode.Minimal, true);
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00030B9C File Offset: 0x0002ED9C
		private string NormalizeInvalidXmlChars(string str)
		{
			char[] anyOf = new char[]
			{
				'+'
			};
			if (str == null || str.IndexOfAny(anyOf) == -1)
			{
				return str;
			}
			str = str.Replace("+", "_x002B_");
			return str;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00030BD8 File Offset: 0x0002EDD8
		private void LoadPropertyValue(SettingsPropertyCollection collection, SettingElement element, bool allowOverwrite)
		{
			SettingsProperty settingsProperty = collection[element.Name];
			if (settingsProperty == null)
			{
				settingsProperty = new SettingsProperty(element.Name);
				collection.Add(settingsProperty);
			}
			SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
			settingsPropertyValue.IsDirty = false;
			if (element.Value.ValueXml != null)
			{
				switch (settingsPropertyValue.Property.SerializeAs)
				{
				case SettingsSerializeAs.String:
					settingsPropertyValue.SerializedValue = element.Value.ValueXml.InnerText.Trim();
					break;
				case SettingsSerializeAs.Xml:
					settingsPropertyValue.SerializedValue = element.Value.ValueXml.InnerXml;
					break;
				case SettingsSerializeAs.Binary:
					settingsPropertyValue.SerializedValue = Convert.FromBase64String(element.Value.ValueXml.InnerText);
					break;
				}
			}
			else
			{
				settingsPropertyValue.SerializedValue = settingsProperty.DefaultValue;
			}
			try
			{
				if (allowOverwrite)
				{
					this.values.Remove(element.Name);
				}
				this.values.Add(settingsPropertyValue);
			}
			catch (ArgumentException inner)
			{
				throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, "Failed to load value for '{0}'.", element.Name), inner);
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00030CF4 File Offset: 0x0002EEF4
		private void LoadProperties(ExeConfigurationFileMap exeMap, SettingsPropertyCollection collection, ConfigurationUserLevel level, string sectionGroupName, bool allowOverwrite, string groupName)
		{
			ConfigurationSectionGroup sectionGroup = ConfigurationManager.OpenMappedExeConfiguration(exeMap, level).GetSectionGroup(sectionGroupName);
			if (sectionGroup != null)
			{
				foreach (object obj in sectionGroup.Sections)
				{
					ConfigurationSection configurationSection = (ConfigurationSection)obj;
					if (!(configurationSection.SectionInformation.Name != groupName))
					{
						ClientSettingsSection clientSettingsSection = configurationSection as ClientSettingsSection;
						if (clientSettingsSection != null)
						{
							using (IEnumerator enumerator2 = clientSettingsSection.Settings.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									SettingElement element = (SettingElement)obj2;
									this.LoadPropertyValue(collection, element, allowOverwrite);
								}
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00030DD0 File Offset: 0x0002EFD0
		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
		{
			this.CreateExeMap();
			if (CustomizableFileSettingsProvider.UserLocalFullPath == CustomizableFileSettingsProvider.UserRoamingFullPath)
			{
				this.SaveProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoaming, context, false);
				return;
			}
			this.SaveProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoaming, context, true);
			this.SaveProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoamingAndLocal, context, true);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00030E28 File Offset: 0x0002F028
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
		{
			this.CreateExeMap();
			this.values = new SettingsPropertyValueCollection();
			string text = context["GroupName"] as string;
			text = this.NormalizeInvalidXmlChars(text);
			this.LoadProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.None, "applicationSettings", false, text);
			this.LoadProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.None, "userSettings", false, text);
			this.LoadProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoaming, "userSettings", true, text);
			this.LoadProperties(this.exeMapCurrent, collection, ConfigurationUserLevel.PerUserRoamingAndLocal, "userSettings", true, text);
			foreach (object obj in collection)
			{
				SettingsProperty settingsProperty = (SettingsProperty)obj;
				if (this.values[settingsProperty.Name] == null)
				{
					this.values.Add(new SettingsPropertyValue(settingsProperty));
				}
			}
			return this.values;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00030F20 File Offset: 0x0002F120
		private void CreateExeMap()
		{
			if (this.exeMapCurrent == null)
			{
				CustomizableFileSettingsProvider.CreateUserConfigPath();
				this.exeMapCurrent = new ExeConfigurationFileMap();
				Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
				this.exeMapCurrent.ExeConfigFilename = assembly.Location + ".config";
				this.exeMapCurrent.LocalUserConfigFilename = CustomizableFileSettingsProvider.UserLocalFullPath;
				this.exeMapCurrent.RoamingUserConfigFilename = CustomizableFileSettingsProvider.UserRoamingFullPath;
				if (CustomizableFileSettingsProvider.webConfigurationFileMapType != null && typeof(ConfigurationFileMap).IsAssignableFrom(CustomizableFileSettingsProvider.webConfigurationFileMapType))
				{
					try
					{
						ConfigurationFileMap configurationFileMap = Activator.CreateInstance(CustomizableFileSettingsProvider.webConfigurationFileMapType) as ConfigurationFileMap;
						if (configurationFileMap != null)
						{
							string machineConfigFilename = configurationFileMap.MachineConfigFilename;
							if (!string.IsNullOrEmpty(machineConfigFilename))
							{
								this.exeMapCurrent.ExeConfigFilename = machineConfigFilename;
							}
						}
					}
					catch
					{
					}
				}
				if (CustomizableFileSettingsProvider.PrevUserLocalFullPath != "" && CustomizableFileSettingsProvider.PrevUserRoamingFullPath != "")
				{
					this.exeMapPrev = new ExeConfigurationFileMap();
					this.exeMapPrev.ExeConfigFilename = assembly.Location + ".config";
					this.exeMapPrev.LocalUserConfigFilename = CustomizableFileSettingsProvider.PrevUserLocalFullPath;
					this.exeMapPrev.RoamingUserConfigFilename = CustomizableFileSettingsProvider.PrevUserRoamingFullPath;
				}
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00002F6A File Offset: 0x0000116A
		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
		{
			return null;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00031060 File Offset: 0x0002F260
		public void Reset(SettingsContext context)
		{
			if (this.values == null)
			{
				SettingsPropertyCollection collection = new SettingsPropertyCollection();
				this.GetPropertyValues(context, collection);
			}
			if (this.values != null)
			{
				foreach (object obj in this.values)
				{
					SettingsPropertyValue settingsPropertyValue = (SettingsPropertyValue)obj;
					this.values[settingsPropertyValue.Name].PropertyValue = settingsPropertyValue.Reset();
				}
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00003917 File Offset: 0x00001B17
		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
		{
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000310F0 File Offset: 0x0002F2F0
		public static void setCreate()
		{
			CustomizableFileSettingsProvider.CreateUserConfigPath();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000310F7 File Offset: 0x0002F2F7
		public CustomizableFileSettingsProvider()
		{
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0003110C File Offset: 0x0002F30C
		// Note: this type is marked as 'beforefieldinit'.
		static CustomizableFileSettingsProvider()
		{
		}

		// Token: 0x04000765 RID: 1893
		private static Type webConfigurationFileMapType;

		// Token: 0x04000766 RID: 1894
		private static string userRoamingPath = "";

		// Token: 0x04000767 RID: 1895
		private static string userLocalPath = "";

		// Token: 0x04000768 RID: 1896
		private static string userRoamingPathPrevVersion = "";

		// Token: 0x04000769 RID: 1897
		private static string userLocalPathPrevVersion = "";

		// Token: 0x0400076A RID: 1898
		private static string userRoamingName = "user.config";

		// Token: 0x0400076B RID: 1899
		private static string userLocalName = "user.config";

		// Token: 0x0400076C RID: 1900
		private static string userRoamingBasePath = "";

		// Token: 0x0400076D RID: 1901
		private static string userLocalBasePath = "";

		// Token: 0x0400076E RID: 1902
		private static string CompanyName = "";

		// Token: 0x0400076F RID: 1903
		private static string ProductName = "";

		// Token: 0x04000770 RID: 1904
		private static string ForceVersion = "";

		// Token: 0x04000771 RID: 1905
		private static string[] ProductVersion;

		// Token: 0x04000772 RID: 1906
		private static bool isVersionMajor = false;

		// Token: 0x04000773 RID: 1907
		private static bool isVersionMinor = false;

		// Token: 0x04000774 RID: 1908
		private static bool isVersionBuild = false;

		// Token: 0x04000775 RID: 1909
		private static bool isVersionRevision = false;

		// Token: 0x04000776 RID: 1910
		private static bool isCompany = true;

		// Token: 0x04000777 RID: 1911
		private static bool isProduct = true;

		// Token: 0x04000778 RID: 1912
		private static bool isEvidence = false;

		// Token: 0x04000779 RID: 1913
		private static bool userDefine = false;

		// Token: 0x0400077A RID: 1914
		private static UserConfigLocationOption userConfig = UserConfigLocationOption.Company_Product;

		// Token: 0x0400077B RID: 1915
		private string app_name = string.Empty;

		// Token: 0x0400077C RID: 1916
		private ExeConfigurationFileMap exeMapCurrent;

		// Token: 0x0400077D RID: 1917
		private ExeConfigurationFileMap exeMapPrev;

		// Token: 0x0400077E RID: 1918
		private SettingsPropertyValueCollection values;
	}
}
