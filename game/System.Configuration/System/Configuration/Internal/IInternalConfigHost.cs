using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Configuration.Internal
{
	/// <summary>Defines interfaces used by internal .NET structures to initialize application configuration properties.</summary>
	// Token: 0x02000081 RID: 129
	[ComVisible(false)]
	public interface IInternalConfigHost
	{
		/// <summary>Creates and returns a context object for a <see cref="T:System.Configuration.ConfigurationElement" /> of an application configuration.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationSubPath">A string representing a subpath location of the configuration element.</param>
		/// <returns>A context object for a <see cref="T:System.Configuration.ConfigurationElement" /> object of an application configuration.</returns>
		// Token: 0x06000440 RID: 1088
		object CreateConfigurationContext(string configPath, string locationSubPath);

		/// <summary>Creates and returns a deprecated context object of the application configuration.</summary>
		/// <param name="configPath">A string representing a path to an application configuration file.</param>
		/// <returns>A deprecated context object of the application configuration.</returns>
		// Token: 0x06000441 RID: 1089
		object CreateDeprecatedConfigContext(string configPath);

		/// <summary>Decrypts an encrypted configuration section and returns it as a string.</summary>
		/// <param name="encryptedXml">An encrypted XML string representing a configuration section.</param>
		/// <param name="protectionProvider">The <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">The <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		/// <returns>A decrypted configuration section as a string.</returns>
		// Token: 0x06000442 RID: 1090
		string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection);

		/// <summary>Deletes the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the application configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		// Token: 0x06000443 RID: 1091
		void DeleteStream(string streamName);

		/// <summary>Encrypts a configuration section and returns it as a string.</summary>
		/// <param name="clearTextXml">An XML string representing a configuration section to encrypt.</param>
		/// <param name="protectionProvider">The <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object.</param>
		/// <param name="protectedConfigSection">The <see cref="T:System.Configuration.ProtectedConfigurationSection" /> object.</param>
		/// <returns>An encrypted configuration section represented as a string.</returns>
		// Token: 0x06000444 RID: 1092
		string EncryptSection(string clearTextXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection);

		/// <summary>Returns the complete path to an application configuration file based on the location subpath.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationSubPath">The subpath location of the configuration file.</param>
		/// <returns>A string representing the complete path to an application configuration file.</returns>
		// Token: 0x06000445 RID: 1093
		string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath);

		/// <summary>Returns a <see cref="T:System.Type" /> object representing the type of the configuration object.</summary>
		/// <param name="typeName">The type name</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if an error occurs; otherwise, <see langword="false" /></param>
		/// <returns>A <see cref="T:System.Type" /> object representing the type of the configuration object.</returns>
		// Token: 0x06000446 RID: 1094
		Type GetConfigType(string typeName, bool throwOnError);

		/// <summary>Returns a string representing a type name from the <see cref="T:System.Type" /> object representing the type of the configuration.</summary>
		/// <param name="t">A <see cref="T:System.Type" /> object.</param>
		/// <returns>A string representing the type name from a <see cref="T:System.Type" /> object representing the type of the configuration.</returns>
		// Token: 0x06000447 RID: 1095
		string GetConfigTypeName(Type t);

		/// <summary>Associates the configuration with a <see cref="T:System.Security.PermissionSet" /> object.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <param name="permissionSet">The <see cref="T:System.Security.PermissionSet" /> object to associate with the configuration.</param>
		/// <param name="isHostReady">
		///   <see langword="true" /> to indicate the configuration host is has completed building associated permissions; otherwise, <see langword="false" />.</param>
		// Token: 0x06000448 RID: 1096
		void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady);

		/// <summary>Returns a string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>A string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> I/O tasks on the configuration file.</returns>
		// Token: 0x06000449 RID: 1097
		string GetStreamName(string configPath);

		/// <summary>Returns a string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on a remote configuration file.</summary>
		/// <param name="streamName">A string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="configSource">A string representing a path to a remote configuration file.</param>
		/// <returns>A string representing the configuration file name associated with the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</returns>
		// Token: 0x0600044A RID: 1098
		string GetStreamNameForConfigSource(string streamName, string configSource);

		/// <summary>Returns the version of the <see cref="T:System.IO.Stream" /> object associated with configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <returns>The version of the <see cref="T:System.IO.Stream" /> object associated with configuration file.</returns>
		// Token: 0x0600044B RID: 1099
		object GetStreamVersion(string streamName);

		/// <summary>Instructs the host to impersonate and returns an <see cref="T:System.IDisposable" /> object required by the internal .NET structure.</summary>
		/// <returns>An <see cref="T:System.IDisposable" /> value.</returns>
		// Token: 0x0600044C RID: 1100
		IDisposable Impersonate();

		/// <summary>Initializes a configuration host.</summary>
		/// <param name="configRoot">The configuration root object.</param>
		/// <param name="hostInitParams">The parameter object containing the values used for initializing the configuration host.</param>
		// Token: 0x0600044D RID: 1101
		void Init(IInternalConfigRoot configRoot, params object[] hostInitParams);

		/// <summary>Initializes a configuration object.</summary>
		/// <param name="locationSubPath">The subpath location of the configuration file.</param>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="locationConfigPath">A string representing the location of a configuration path.</param>
		/// <param name="configRoot">The <see cref="T:System.Configuration.Internal.IInternalConfigRoot" /> object.</param>
		/// <param name="hostInitConfigurationParams">The parameter object containing the values used for initializing the configuration host.</param>
		// Token: 0x0600044E RID: 1102
		void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams);

		/// <summary>Returns a value indicating whether the configuration file is located at a higher level in the configuration hierarchy than the application configuration.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> the configuration file is located at a higher level in the configuration hierarchy than the application configuration; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600044F RID: 1103
		bool IsAboveApplication(string configPath);

		/// <summary>Returns a value indicating whether a child record is required for a child configuration path.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if child record is required for a child configuration path; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000450 RID: 1104
		bool IsConfigRecordRequired(string configPath);

		/// <summary>Determines if a different <see cref="T:System.Type" /> definition is allowable for an application configuration object.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="allowDefinition">A <see cref="T:System.Configuration.ConfigurationAllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">A <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if a different <see cref="T:System.Type" /> definition is allowable for an application configuration object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000451 RID: 1105
		bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition);

		/// <summary>Returns a value indicating whether the file path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the path used by a <see cref="T:System.IO.Stream" /> object to read a configuration file is a valid path; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000452 RID: 1106
		bool IsFile(string streamName);

		/// <summary>Returns a value indicating whether a configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands.</summary>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration section requires a fully trusted code access security level and does not allow the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> attribute to disable implicit link demands; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000453 RID: 1107
		bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord);

		/// <summary>Returns a value indicating whether the initialization of a configuration object is considered delayed.</summary>
		/// <param name="configRecord">The <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the initialization of a configuration object is considered delayed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000454 RID: 1108
		bool IsInitDelayed(IInternalConfigRecord configRecord);

		/// <summary>Returns a value indicating whether the configuration object supports a location tag.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration object supports a location tag; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000455 RID: 1109
		bool IsLocationApplicable(string configPath);

		/// <summary>Returns a value indicating whether the configuration is remote.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration is remote; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000456 RID: 1110
		bool IsRemote { get; }

		/// <summary>Returns a value indicating whether a configuration path is to a configuration node whose contents should be treated as a root.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration path is to a configuration node whose contents should be treated as a root; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000457 RID: 1111
		bool IsSecondaryRoot(string configPath);

		/// <summary>Returns a value indicating whether the configuration path is trusted.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the configuration path is trusted; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000458 RID: 1112
		bool IsTrustedConfigPath(string configPath);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> to read a configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		// Token: 0x06000459 RID: 1113
		Stream OpenStreamForRead(string streamName);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object to read a configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		/// <returns>The object specified by <paramref name="streamName" />.</returns>
		// Token: 0x0600045A RID: 1114
		Stream OpenStreamForRead(string streamName, bool assertPermissions);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file or for writing to a temporary file used to build a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> object from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object.</param>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		// Token: 0x0600045B RID: 1115
		Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext);

		/// <summary>Opens a <see cref="T:System.IO.Stream" /> object for writing to a configuration file. Allows a <see cref="T:System.IO.Stream" /> object to be designated as a template for copying file attributes.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="templateStreamName">The name of a <see cref="T:System.IO.Stream" /> from which file attributes are to be copied as a template.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		/// <returns>The object specified by <paramref name="streamName" />.</returns>
		// Token: 0x0600045C RID: 1116
		Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions);

		/// <summary>Returns a value that indicates whether the entire configuration file could be read by a designated <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <returns>
		///   <see langword="true" /> if the entire configuration file could be read by the <see cref="T:System.IO.Stream" /> object designated by <paramref name="streamName" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600045D RID: 1117
		bool PrefetchAll(string configPath, string streamName);

		/// <summary>Instructs the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to read a designated section of its associated configuration file.</summary>
		/// <param name="sectionGroupName">A string representing the identifying name of a configuration file section group.</param>
		/// <param name="sectionName">A string representing the identifying name of a configuration file section.</param>
		/// <returns>
		///   <see langword="true" /> if a section of the configuration file designated by <paramref name="sectionGroupName" /> and <paramref name="sectionName" /> could be read by a <see cref="T:System.IO.Stream" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600045E RID: 1118
		bool PrefetchSection(string sectionGroupName, string sectionName);

		/// <summary>Indicates a new configuration record requires a complete initialization.</summary>
		/// <param name="configRecord">An <see cref="T:System.Configuration.Internal.IInternalConfigRecord" /> object.</param>
		// Token: 0x0600045F RID: 1119
		void RequireCompleteInit(IInternalConfigRecord configRecord);

		/// <summary>Instructs the <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to monitor an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object to receive the returned data representing the changes in the configuration file.</param>
		/// <returns>An <see cref="T:System.Object" /> containing changed configuration settings.</returns>
		// Token: 0x06000460 RID: 1120
		object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback);

		/// <summary>Instructs the  <see cref="T:System.Configuration.Internal.IInternalConfigHost" /> object to stop monitoring an associated <see cref="T:System.IO.Stream" /> object for changes in a configuration file.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="callback">A <see cref="T:System.Configuration.Internal.StreamChangeCallback" /> object.</param>
		// Token: 0x06000461 RID: 1121
		void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback);

		/// <summary>Verifies that a configuration definition is allowed for a configuration record.</summary>
		/// <param name="configPath">A string representing the path of the application configuration file.</param>
		/// <param name="allowDefinition">A <see cref="P:System.Configuration.SectionInformation.AllowDefinition" /> object.</param>
		/// <param name="allowExeDefinition">A <see cref="T:System.Configuration.ConfigurationAllowExeDefinition" /> object</param>
		/// <param name="errorInfo">An <see cref="T:System.Configuration.Internal.IConfigErrorInfo" /> object.</param>
		// Token: 0x06000462 RID: 1122
		void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo);

		/// <summary>Indicates that all writing to the configuration file has completed.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="success">
		///   <see langword="true" /> if the write to the configuration file was completed successfully; otherwise, <see langword="false" />.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		// Token: 0x06000463 RID: 1123
		void WriteCompleted(string streamName, bool success, object writeContext);

		/// <summary>Indicates that all writing to the configuration file has completed and specifies whether permissions should be asserted.</summary>
		/// <param name="streamName">A string representing the name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="success">
		///   <see langword="true" /> to indicate the write was completed successfully; otherwise, <see langword="false" />.</param>
		/// <param name="writeContext">The write context of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
		/// <param name="assertPermissions">
		///   <see langword="true" /> to assert permissions; otherwise, <see langword="false" />.</param>
		// Token: 0x06000464 RID: 1124
		void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions);

		/// <summary>Returns a value indicating whether the host configuration supports change notification.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration supports change notification; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000465 RID: 1125
		bool SupportsChangeNotifications { get; }

		/// <summary>Returns a value indicating whether the host configuration supports location tags.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration supports location tags; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000466 RID: 1126
		bool SupportsLocation { get; }

		/// <summary>Returns a value indicating whether the host configuration supports path tags.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration supports path tags; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000467 RID: 1127
		bool SupportsPath { get; }

		/// <summary>Returns a value indicating whether the host configuration supports configuration refresh.</summary>
		/// <returns>
		///   <see langword="true" /> if the configuration supports configuration refresh; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000468 RID: 1128
		bool SupportsRefresh { get; }
	}
}
