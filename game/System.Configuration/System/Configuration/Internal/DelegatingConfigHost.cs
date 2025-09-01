using System;
using System.IO;
using System.Security;
using System.Xml;
using Unity;

namespace System.Configuration.Internal
{
	/// <summary>Delegates all members of the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> interface to another instance of a host.</summary>
	// Token: 0x0200007A RID: 122
	public class DelegatingConfigHost : IInternalConfigHost, IInternalConfigurationBuilderHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Internal.DelegatingConfigHost" /> class.</summary>
		// Token: 0x060003F8 RID: 1016 RVA: 0x00002050 File Offset: 0x00000250
		protected DelegatingConfigHost()
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object.</returns>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000B182 File Offset: 0x00009382
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000B18A File Offset: 0x0000938A
		protected IInternalConfigHost Host
		{
			get
			{
				return this.host;
			}
			set
			{
				this.host = value;
			}
		}

		/// <summary>Creates a new configuration context.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationSubPath">A string representing a location subpath.</param>
		/// <returns>A <see cref="T:System.Object" /> representing a new configuration context.</returns>
		// Token: 0x060003FB RID: 1019 RVA: 0x0000B193 File Offset: 0x00009393
		public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
		{
			return this.host.CreateConfigurationContext(configPath, locationSubPath);
		}

		/// <summary>Creates a deprecated configuration context.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>A <see cref="T:System.Object" /> representing a deprecated configuration context.</returns>
		// Token: 0x060003FC RID: 1020 RVA: 0x0000B1A2 File Offset: 0x000093A2
		public virtual object CreateDeprecatedConfigContext(string configPath)
		{
			return this.host.CreateDeprecatedConfigContext(configPath);
		}

		/// <summary>Decrypts an encrypted configuration section.</summary>
		/// <param name="encryptedXml">An encrypted section of a configuration file.</param>
		/// <param name="protectionProvider">A <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">A <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		/// <returns>A string representing a decrypted configuration section.</returns>
		// Token: 0x060003FD RID: 1021 RVA: 0x0000B1B0 File Offset: 0x000093B0
		public virtual string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return this.host.DecryptSection(encryptedXml, protectionProvider, protectedConfigSection);
		}

		/// <summary>Deletes the <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		// Token: 0x060003FE RID: 1022 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public virtual void DeleteStream(string streamName)
		{
			this.host.DeleteStream(streamName);
		}

		/// <summary>Encrypts a section of a configuration object.</summary>
		/// <param name="clearTextXml">A section of the configuration that is not encrypted.</param>
		/// <param name="protectionProvider">A <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">A <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		/// <returns>A string representing an encrypted section of the configuration object.</returns>
		// Token: 0x060003FF RID: 1023 RVA: 0x0000B1CE File Offset: 0x000093CE
		public virtual string EncryptSection(string clearTextXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return this.host.EncryptSection(clearTextXml, protectionProvider, protectedConfigSection);
		}

		/// <summary>Returns a configuration path based on a location subpath.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationSubPath">A string representing a location subpath.</param>
		/// <returns>A string representing a configuration path.</returns>
		// Token: 0x06000400 RID: 1024 RVA: 0x0000B1DE File Offset: 0x000093DE
		public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			return this.host.GetConfigPathFromLocationSubPath(configPath, locationSubPath);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> representing the type of the configuration.</summary>
		/// <param name="typeName">A string representing the configuration type.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> if an exception should be thrown if an error is encountered; <see langword="false" /> if an exception should not be thrown if an error is encountered.</param>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the configuration.</returns>
		// Token: 0x06000401 RID: 1025 RVA: 0x0000B1ED File Offset: 0x000093ED
		public virtual Type GetConfigType(string typeName, bool throwOnError)
		{
			return this.host.GetConfigType(typeName, throwOnError);
		}

		/// <summary>Returns a string representing the type name of the configuration object.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> object.</param>
		/// <returns>A string representing the type name of the configuration object.</returns>
		// Token: 0x06000402 RID: 1026 RVA: 0x0000B1FC File Offset: 0x000093FC
		public virtual string GetConfigTypeName(Type t)
		{
			return this.host.GetConfigTypeName(t);
		}

		/// <summary>Sets the specified permission set if available within the host object.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <param name="permissionSet">A <see cref="T:System.Security.PermissionSet" /> object.</param>
		/// <param name="isHostReady">
		///   <see langword="true" /> if the host has finished initialization; otherwise, <see langword="false" />.</param>
		// Token: 0x06000403 RID: 1027 RVA: 0x0000B20A File Offset: 0x0000940A
		public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			this.host.GetRestrictedPermissions(configRecord, out permissionSet, out isHostReady);
		}

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</returns>
		// Token: 0x06000404 RID: 1028 RVA: 0x0000B21A File Offset: 0x0000941A
		public virtual string GetStreamName(string configPath)
		{
			return this.host.GetStreamName(configPath);
		}

		/// <summary>Returns the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration source.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="configSource">A string representing the configuration source.</param>
		/// <returns>A string representing the name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration source.</returns>
		// Token: 0x06000405 RID: 1029 RVA: 0x0000B228 File Offset: 0x00009428
		public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
		{
			return this.host.GetStreamNameForConfigSource(streamName, configSource);
		}

		/// <summary>Returns a <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" /> object representing the version of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <returns>A <see cref="P:System.Diagnostics.FileVersionInfo.FileVersion" /> object representing the version of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</returns>
		// Token: 0x06000406 RID: 1030 RVA: 0x0000B237 File Offset: 0x00009437
		public virtual object GetStreamVersion(string streamName)
		{
			return this.host.GetStreamVersion(streamName);
		}

		/// <summary>Instructs the host to impersonate and returns an <see cref="T:System.IDisposable" /> object required internally by the .NET Framework.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> value.</returns>
		// Token: 0x06000407 RID: 1031 RVA: 0x0000B245 File Offset: 0x00009445
		public virtual IDisposable Impersonate()
		{
			return this.host.Impersonate();
		}

		/// <summary>Initializes the configuration host.</summary>
		/// <param name="configRoot">An <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</param>
		/// <param name="hostInitParams">A parameter object containing the values used for initializing the configuration host.</param>
		// Token: 0x06000408 RID: 1032 RVA: 0x0000B252 File Offset: 0x00009452
		public virtual void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
		{
			this.host.Init(configRoot, hostInitParams);
		}

		/// <summary>Initializes the host for configuration.</summary>
		/// <param name="locationSubPath">A string representing a location subpath (passed by reference).</param>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="locationConfigPath">The location configuration path.</param>
		/// <param name="configRoot">The configuration root element.</param>
		/// <param name="hostInitConfigurationParams">A parameter object representing the parameters used to initialize the host.</param>
		// Token: 0x06000409 RID: 1033 RVA: 0x0000B261 File Offset: 0x00009461
		public virtual void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams)
		{
			this.host.InitForConfiguration(ref locationSubPath, out configPath, out locationConfigPath, configRoot, hostInitConfigurationParams);
		}

		/// <summary>Returns a value indicating whether the configuration is above the application configuration in the configuration hierarchy.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration is above the application configuration in the configuration hierarchy; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600040A RID: 1034 RVA: 0x0000B275 File Offset: 0x00009475
		public virtual bool IsAboveApplication(string configPath)
		{
			return this.host.IsAboveApplication(configPath);
		}

		/// <summary>Returns a value indicating whether a configuration record is required for the host configuration initialization.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a configuration record is required for the host configuration initialization; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600040B RID: 1035 RVA: 0x0000B283 File Offset: 0x00009483
		public virtual bool IsConfigRecordRequired(string configPath)
		{
			return this.host.IsConfigRecordRequired(configPath);
		}

		/// <summary>Restricts or allows definitions in the host configuration.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="allowDefinition">The <see cref="T:System.Configuration.ConfigurationAllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">The <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the grant or restriction of definitions in the host configuration was successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600040C RID: 1036 RVA: 0x0000B291 File Offset: 0x00009491
		public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return this.host.IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition);
		}

		/// <summary>Returns a value indicating whether the initialization of a configuration object is considered delayed.</summary>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the initialization of a configuration object is considered delayed; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600040D RID: 1037 RVA: 0x0000B2A1 File Offset: 0x000094A1
		public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
		{
			return this.host.IsInitDelayed(configRecord);
		}

		/// <summary>Returns a value indicating whether the file path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600040E RID: 1038 RVA: 0x0000B2AF File Offset: 0x000094AF
		public virtual bool IsFile(string streamName)
		{
			return this.host.IsFile(streamName);
		}

		/// <summary>Returns a value indicating whether a configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands.</summary>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600040F RID: 1039 RVA: 0x0000B2BD File Offset: 0x000094BD
		public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			return this.host.IsFullTrustSectionWithoutAptcaAllowed(configRecord);
		}

		/// <summary>Returns a value indicating whether the configuration object supports a location tag.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration object supports a location tag; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000410 RID: 1040 RVA: 0x0000B2CB File Offset: 0x000094CB
		public virtual bool IsLocationApplicable(string configPath)
		{
			return this.host.IsLocationApplicable(configPath);
		}

		/// <summary>Gets a value indicating whether the configuration is remote.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration is remote; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000B2D9 File Offset: 0x000094D9
		public virtual bool IsRemote
		{
			get
			{
				return this.host.IsRemote;
			}
		}

		/// <summary>Returns a value indicating whether a configuration path is to a configuration node whose contents should be treated as a root.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration path is to a configuration node whose contents should be treated as a root; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000412 RID: 1042 RVA: 0x0000B2E6 File Offset: 0x000094E6
		public virtual bool IsSecondaryRoot(string configPath)
		{
			return this.host.IsSecondaryRoot(configPath);
		}

		/// <summary>Returns a value indicating whether the configuration path is trusted.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration path is trusted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000413 RID: 1043 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public virtual bool IsTrustedConfigPath(string configPath)
		{
			return this.host.IsTrustedConfigPath(configPath);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object to read a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <returns>The object specified by <paramref name="streamName" />.</returns>
		// Token: 0x06000414 RID: 1044 RVA: 0x0000B302 File Offset: 0x00009502
		public virtual Stream OpenStreamForRead(string streamName)
		{
			return this.host.OpenStreamForRead(streamName);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object to read a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		/// <returns>The object specified by <paramref name="streamName" />.</returns>
		// Token: 0x06000415 RID: 1045 RVA: 0x0000B310 File Offset: 0x00009510
		public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
		{
			return this.host.OpenStreamForRead(streamName, assertPermissions);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file or for writing to a temporary file used to build a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> object from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object (passed by reference).</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		// Token: 0x06000416 RID: 1046 RVA: 0x0000B31F File Offset: 0x0000951F
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			return this.host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext);
		}

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> object from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file (passed by reference).</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		/// <returns>The object specified by the <paramref name="streamName" /> parameter.</returns>
		// Token: 0x06000417 RID: 1047 RVA: 0x0000B32F File Offset: 0x0000952F
		public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			return this.host.OpenStreamForWrite(streamName, templateStreamName, ref writeContext, assertPermissions);
		}

		/// <summary>Returns a value indicating whether the entire configuration file could be read by a designated <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the entire configuration file could be read by the <see cref="T:System.IO.Stream" /> object designated by <paramref name="streamName" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000418 RID: 1048 RVA: 0x0000B341 File Offset: 0x00009541
		public virtual bool PrefetchAll(string configPath, string streamName)
		{
			return this.host.PrefetchAll(configPath, streamName);
		}

		/// <summary>Instructs the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to read a designated section of its associated configuration file.</summary>
		/// <param name="sectionGroupName">A string representing the name of a section group in the configuration file.</param>
		/// <param name="sectionName">A string representing the name of a section in the configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if a section of the configuration file designated by the <paramref name="sectionGroupName" /> and <paramref name="sectionName" /> parameters can be read by a <see cref="T:System.IO.Stream" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000419 RID: 1049 RVA: 0x0000B350 File Offset: 0x00009550
		public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			return this.host.PrefetchSection(sectionGroupName, sectionName);
		}

		/// <summary>Indicates that a new configuration record requires a complete initialization.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		// Token: 0x0600041A RID: 1050 RVA: 0x0000B35F File Offset: 0x0000955F
		public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
		{
			this.host.RequireCompleteInit(configRecord);
		}

		/// <summary>Instructs the host to monitor an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object to receive the returned data representing the changes in the configuration file.</param>
		/// <returns>An <see cref="T:System.Object" /> instance containing changed configuration settings.</returns>
		// Token: 0x0600041B RID: 1051 RVA: 0x0000B36D File Offset: 0x0000956D
		public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			return this.host.StartMonitoringStreamForChanges(streamName, callback);
		}

		/// <summary>Instructs the host object to stop monitoring an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object.</param>
		// Token: 0x0600041C RID: 1052 RVA: 0x0000B37C File Offset: 0x0000957C
		public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			this.host.StopMonitoringStreamForChanges(streamName, callback);
		}

		/// <summary>Verifies that a configuration definition is allowed for a configuration record.</summary>
		/// <param name="configPath">A string representing the path to a configuration file.</param>
		/// <param name="allowDefinition">An <see cref="P:System.Configuration.SectionInformation.AllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">A <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object</param>
		/// <param name="errorInfo">An <see cref="T:System.Configuration.Internal.IConfigErrorInfo" /> object.</param>
		// Token: 0x0600041D RID: 1053 RVA: 0x0000B38B File Offset: 0x0000958B
		public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
			this.host.VerifyDefinitionAllowed(configPath, allowDefinition, allowExeDefinition, errorInfo);
		}

		/// <summary>Indicates that all writing to the configuration file has completed.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="success">
		///   <see langword="true" /> if writing to the configuration file completed successfully; otherwise, <see langword="false" />.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		// Token: 0x0600041E RID: 1054 RVA: 0x0000B39D File Offset: 0x0000959D
		public virtual void WriteCompleted(string streamName, bool success, object writeContext)
		{
			this.host.WriteCompleted(streamName, success, writeContext);
		}

		/// <summary>Indicates that all writing to the configuration file has completed and specifies whether permissions should be asserted.</summary>
		/// <param name="streamName">The name of a <see cref="T:System.IO.Stream" /> object performing I/O tasks on a configuration file.</param>
		/// <param name="success">
		///   <see langword="true" /> to indicate that writing was completed successfully; otherwise, <see langword="false" />.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		// Token: 0x0600041F RID: 1055 RVA: 0x0000B3AD File Offset: 0x000095AD
		public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
			this.host.WriteCompleted(streamName, success, writeContext, assertPermissions);
		}

		/// <summary>Gets a value indicating whether the host configuration supports change notifications.</summary>
		/// <returns>
		///   <see langword="true" /> if the host supports change notifications; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000B3BF File Offset: 0x000095BF
		public virtual bool SupportsChangeNotifications
		{
			get
			{
				return this.host.SupportsChangeNotifications;
			}
		}

		/// <summary>Gets a value indicating whether the host configuration supports location tags.</summary>
		/// <returns>
		///   <see langword="true" /> if the host supports location tags; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000B3CC File Offset: 0x000095CC
		public virtual bool SupportsLocation
		{
			get
			{
				return this.host.SupportsLocation;
			}
		}

		/// <summary>Gets a value indicating whether the host configuration has path support.</summary>
		/// <returns>
		///   <see langword="true" /> if the host configuration has path support; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000B3D9 File Offset: 0x000095D9
		public virtual bool SupportsPath
		{
			get
			{
				return this.host.SupportsPath;
			}
		}

		/// <summary>Gets a value indicating whether the host configuration supports refresh.</summary>
		/// <returns>
		///   <see langword="true" /> if the host configuration supports refresh; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000B3E6 File Offset: 0x000095E6
		public virtual bool SupportsRefresh
		{
			get
			{
				return this.host.SupportsRefresh;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.Internal.IInternalConfigurationBuilderHost" /> object if the delegated host provides the functionality required by that interface.</summary>
		/// <returns>An <see cref="T:System.Configuration.Internal.IInternalConfigurationBuilderHost" /> object.</returns>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00003527 File Offset: 0x00001727
		protected IInternalConfigurationBuilderHost ConfigBuilderHost
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Processes a <see cref="T:System.Configuration.ConfigurationSection" /> object using the provided <see cref="T:System.Configuration.ConfigurationBuilder" />.</summary>
		/// <param name="configSection">The <see cref="T:System.Configuration.ConfigurationSection" /> to process.</param>
		/// <param name="builder">
		///   <see cref="T:System.Configuration.ConfigurationBuilder" /> to use to process the <paramref name="configSection" />.</param>
		/// <returns>The processed <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		// Token: 0x06000425 RID: 1061 RVA: 0x00003527 File Offset: 0x00001727
		public virtual ConfigurationSection ProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder builder)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Processes the markup of a configuration section using the provided <see cref="T:System.Configuration.ConfigurationBuilder" />.</summary>
		/// <param name="rawXml">The <see cref="T:System.Xml.XmlNode" /> to process.</param>
		/// <param name="builder">
		///   <see cref="T:System.Configuration.ConfigurationBuilder" /> to use to process the <paramref name="rawXml" />.</param>
		/// <returns>The processed <see cref="T:System.Xml.XmlNode" />.</returns>
		// Token: 0x06000426 RID: 1062 RVA: 0x00003527 File Offset: 0x00001727
		public virtual XmlNode ProcessRawXml(XmlNode rawXml, ConfigurationBuilder builder)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04000166 RID: 358
		private IInternalConfigHost host;
	}
}
