using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using Mono.Xml;

namespace System.Runtime.Remoting
{
	/// <summary>Provides various static methods for configuring the remoting infrastructure.</summary>
	// Token: 0x02000565 RID: 1381
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		/// <summary>Gets the ID of the currently executing application.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the ID of the currently executing application.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000C2AE4 File Offset: 0x000C0CE4
		public static string ApplicationId
		{
			get
			{
				RemotingConfiguration.applicationID = RemotingConfiguration.ApplicationName;
				return RemotingConfiguration.applicationID;
			}
		}

		/// <summary>Gets or sets the name of a remoting application.</summary>
		/// <returns>The name of a remoting application.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value.</exception>
		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000C2AF5 File Offset: 0x000C0CF5
		// (set) Token: 0x06003624 RID: 13860 RVA: 0x000C2AFC File Offset: 0x000C0CFC
		public static string ApplicationName
		{
			get
			{
				return RemotingConfiguration.applicationName;
			}
			set
			{
				RemotingConfiguration.applicationName = value;
			}
		}

		/// <summary>Gets or sets value that indicates how custom errors are handled.</summary>
		/// <returns>A member of the <see cref="T:System.Runtime.Remoting.CustomErrorsModes" /> enumeration that indicates how custom errors are handled.</returns>
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000C2B04 File Offset: 0x000C0D04
		// (set) Token: 0x06003626 RID: 13862 RVA: 0x000C2B0B File Offset: 0x000C0D0B
		public static CustomErrorsModes CustomErrorsMode
		{
			get
			{
				return RemotingConfiguration._errorMode;
			}
			set
			{
				RemotingConfiguration._errorMode = value;
			}
		}

		/// <summary>Gets the ID of the currently executing process.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the ID of the currently executing process.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission.</exception>
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000C2B13 File Offset: 0x000C0D13
		public static string ProcessId
		{
			get
			{
				if (RemotingConfiguration.processGuid == null)
				{
					RemotingConfiguration.processGuid = AppDomain.GetProcessGuid();
				}
				return RemotingConfiguration.processGuid;
			}
		}

		/// <summary>Reads the configuration file and configures the remoting infrastructure.</summary>
		/// <param name="filename">The name of the remoting configuration file. Can be <see langword="null" />.</param>
		/// <param name="ensureSecurity">If set to <see langword="true" /> security is required. If set to <see langword="false" />, security is not required but still may be used.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003628 RID: 13864 RVA: 0x000C2B2C File Offset: 0x000C0D2C
		[MonoTODO("ensureSecurity support has not been implemented")]
		public static void Configure(string filename, bool ensureSecurity)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (!RemotingConfiguration.defaultConfigRead)
				{
					string bundledMachineConfig = Environment.GetBundledMachineConfig();
					if (bundledMachineConfig != null)
					{
						RemotingConfiguration.ReadConfigString(bundledMachineConfig);
					}
					if (File.Exists(Environment.GetMachineConfigPath()))
					{
						RemotingConfiguration.ReadConfigFile(Environment.GetMachineConfigPath());
					}
					RemotingConfiguration.defaultConfigRead = true;
				}
				if (filename != null)
				{
					RemotingConfiguration.ReadConfigFile(filename);
				}
			}
		}

		/// <summary>Reads the configuration file and configures the remoting infrastructure. <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String)" /> is obsolete. Please use <see cref="M:System.Runtime.Remoting.RemotingConfiguration.Configure(System.String,System.Boolean)" /> instead.</summary>
		/// <param name="filename">The name of the remoting configuration file. Can be <see langword="null" />.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003629 RID: 13865 RVA: 0x000C2BA0 File Offset: 0x000C0DA0
		[Obsolete("Use Configure(String,Boolean)")]
		public static void Configure(string filename)
		{
			RemotingConfiguration.Configure(filename, false);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000C2BAC File Offset: 0x000C0DAC
		private static void ReadConfigString(string filename)
		{
			try
			{
				SmallXmlParser smallXmlParser = new SmallXmlParser();
				using (TextReader textReader = new StringReader(filename))
				{
					ConfigHandler handler = new ConfigHandler(false);
					smallXmlParser.Parse(textReader, handler);
				}
			}
			catch (Exception ex)
			{
				throw new RemotingException("Configuration string could not be loaded: " + ex.Message, ex);
			}
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000C2C18 File Offset: 0x000C0E18
		private static void ReadConfigFile(string filename)
		{
			try
			{
				SmallXmlParser smallXmlParser = new SmallXmlParser();
				using (TextReader textReader = new StreamReader(filename))
				{
					ConfigHandler handler = new ConfigHandler(false);
					smallXmlParser.Parse(textReader, handler);
				}
			}
			catch (Exception ex)
			{
				throw new RemotingException("Configuration file '" + filename + "' could not be loaded: " + ex.Message, ex);
			}
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000C2C8C File Offset: 0x000C0E8C
		internal static void LoadDefaultDelayedChannels()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (!RemotingConfiguration.defaultDelayedConfigRead && !RemotingConfiguration.defaultConfigRead)
				{
					SmallXmlParser smallXmlParser = new SmallXmlParser();
					using (TextReader textReader = new StreamReader(Environment.GetMachineConfigPath()))
					{
						ConfigHandler handler = new ConfigHandler(true);
						smallXmlParser.Parse(textReader, handler);
					}
					RemotingConfiguration.defaultDelayedConfigRead = true;
				}
			}
		}

		/// <summary>Retrieves an array of object types registered on the client as types that will be activated remotely.</summary>
		/// <returns>An array of object types registered on the client as types that will be activated remotely.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600362D RID: 13869 RVA: 0x000C2D18 File Offset: 0x000C0F18
		public static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry[] result;
			lock (obj)
			{
				ActivatedClientTypeEntry[] array = new ActivatedClientTypeEntry[RemotingConfiguration.activatedClientEntries.Count];
				RemotingConfiguration.activatedClientEntries.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		/// <summary>Retrieves an array of object types registered on the service end that can be activated on request from a client.</summary>
		/// <returns>An array of object types registered on the service end that can be activated on request from a client.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600362E RID: 13870 RVA: 0x000C2D78 File Offset: 0x000C0F78
		public static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			ActivatedServiceTypeEntry[] result;
			lock (obj)
			{
				ActivatedServiceTypeEntry[] array = new ActivatedServiceTypeEntry[RemotingConfiguration.activatedServiceEntries.Count];
				RemotingConfiguration.activatedServiceEntries.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		/// <summary>Retrieves an array of object types registered on the client end as well-known types.</summary>
		/// <returns>An array of object types registered on the client end as well-known types.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600362F RID: 13871 RVA: 0x000C2DD8 File Offset: 0x000C0FD8
		public static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry[] result;
			lock (obj)
			{
				WellKnownClientTypeEntry[] array = new WellKnownClientTypeEntry[RemotingConfiguration.wellKnownClientEntries.Count];
				RemotingConfiguration.wellKnownClientEntries.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		/// <summary>Retrieves an array of object types registered on the service end as well-known types.</summary>
		/// <returns>An array of object types registered on the service end as well-known types.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003630 RID: 13872 RVA: 0x000C2E38 File Offset: 0x000C1038
		public static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			WellKnownServiceTypeEntry[] result;
			lock (obj)
			{
				WellKnownServiceTypeEntry[] array = new WellKnownServiceTypeEntry[RemotingConfiguration.wellKnownServiceEntries.Count];
				RemotingConfiguration.wellKnownServiceEntries.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		/// <summary>Returns a Boolean value that indicates whether the specified <see cref="T:System.Type" /> is allowed to be client activated.</summary>
		/// <param name="svrType">The object <see cref="T:System.Type" /> to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Type" /> is allowed to be client activated; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003631 RID: 13873 RVA: 0x000C2E98 File Offset: 0x000C1098
		public static bool IsActivationAllowed(Type svrType)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			bool result;
			lock (obj)
			{
				result = RemotingConfiguration.activatedServiceEntries.ContainsKey(svrType);
			}
			return result;
		}

		/// <summary>Checks whether the specified object <see cref="T:System.Type" /> is registered as a remotely activated client type.</summary>
		/// <param name="svrType">The object type to check.</param>
		/// <returns>The <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003632 RID: 13874 RVA: 0x000C2EE0 File Offset: 0x000C10E0
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry result;
			lock (obj)
			{
				result = (RemotingConfiguration.activatedClientEntries[svrType] as ActivatedClientTypeEntry);
			}
			return result;
		}

		/// <summary>Checks whether the object specified by its type name and assembly name is registered as a remotely activated client type.</summary>
		/// <param name="typeName">The type name of the object to check.</param>
		/// <param name="assemblyName">The assembly name of the object to check.</param>
		/// <returns>The <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003633 RID: 13875 RVA: 0x000C2F2C File Offset: 0x000C112C
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
		{
			return RemotingConfiguration.IsRemotelyActivatedClientType(Assembly.Load(assemblyName).GetType(typeName));
		}

		/// <summary>Checks whether the specified object <see cref="T:System.Type" /> is registered as a well-known client type.</summary>
		/// <param name="svrType">The object <see cref="T:System.Type" /> to check.</param>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003634 RID: 13876 RVA: 0x000C2F40 File Offset: 0x000C1140
		public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry result;
			lock (obj)
			{
				result = (RemotingConfiguration.wellKnownClientEntries[svrType] as WellKnownClientTypeEntry);
			}
			return result;
		}

		/// <summary>Checks whether the object specified by its type name and assembly name is registered as a well-known client type.</summary>
		/// <param name="typeName">The type name of the object to check.</param>
		/// <param name="assemblyName">The assembly name of the object to check.</param>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003635 RID: 13877 RVA: 0x000C2F8C File Offset: 0x000C118C
		public static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
		{
			return RemotingConfiguration.IsWellKnownClientType(Assembly.Load(assemblyName).GetType(typeName));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> on the client end as a type that can be activated on the server.</summary>
		/// <param name="entry">Configuration settings for the client-activated type.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003636 RID: 13878 RVA: 0x000C2FA0 File Offset: 0x000C11A0
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.activatedClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the client end as a type that can be activated on the server, using the given parameters to initialize a new instance of the <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> class.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />.</param>
		/// <param name="appUrl">URL of the application where this type is activated.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typeName" /> or <paramref name="URI" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003637 RID: 13879 RVA: 0x000C303C File Offset: 0x000C123C
		public static void RegisterActivatedClientType(Type type, string appUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(type, appUrl));
		}

		/// <summary>Registers an object type recorded in the provided <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> on the service end as one that can be activated on request from a client.</summary>
		/// <param name="entry">Configuration settings for the client-activated type.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003638 RID: 13880 RVA: 0x000C306C File Offset: 0x000C126C
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				RemotingConfiguration.activatedServiceEntries.Add(entry.ObjectType, entry);
			}
		}

		/// <summary>Registers a specified object type on the service end as a type that can be activated on request from a client.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of object to register.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x06003639 RID: 13881 RVA: 0x000C30B8 File Offset: 0x000C12B8
		public static void RegisterActivatedServiceType(Type type)
		{
			RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(type));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the client end as a well-known type that can be activated on the server, using the given parameters to initialize a new instance of the <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> class.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />.</param>
		/// <param name="objectUrl">URL of a well-known client object.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600363A RID: 13882 RVA: 0x000C30C5 File Offset: 0x000C12C5
		public static void RegisterWellKnownClientType(Type type, string objectUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(type, objectUrl));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> on the client end as a well-known type that can be activated on the server.</summary>
		/// <param name="entry">Configuration settings for the well-known type.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600363B RID: 13883 RVA: 0x000C30F8 File Offset: 0x000C12F8
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.wellKnownClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> on the service end as a well-known type, using the given parameters to initialize a new instance of <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" />.</summary>
		/// <param name="type">The object <see cref="T:System.Type" />.</param>
		/// <param name="objectUri">The object URI.</param>
		/// <param name="mode">The activation mode of the well-known object type being registered. (See <see cref="T:System.Runtime.Remoting.WellKnownObjectMode" />.)</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600363C RID: 13884 RVA: 0x000C3194 File Offset: 0x000C1394
		public static void RegisterWellKnownServiceType(Type type, string objectUri, WellKnownObjectMode mode)
		{
			RemotingConfiguration.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(type, objectUri, mode));
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> on the service end as a well-known type.</summary>
		/// <param name="entry">Configuration settings for the well-known type.</param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels.</exception>
		// Token: 0x0600363D RID: 13885 RVA: 0x000C31A4 File Offset: 0x000C13A4
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			Hashtable obj = RemotingConfiguration.channelTemplates;
			lock (obj)
			{
				RemotingConfiguration.wellKnownServiceEntries[entry.ObjectUri] = entry;
				RemotingServices.CreateWellKnownServerIdentity(entry.ObjectType, entry.ObjectUri, entry.Mode);
			}
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000C3208 File Offset: 0x000C1408
		internal static void RegisterChannelTemplate(ChannelData channel)
		{
			RemotingConfiguration.channelTemplates[channel.Id] = channel;
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000C321B File Offset: 0x000C141B
		internal static void RegisterClientProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.clientProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000C322E File Offset: 0x000C142E
		internal static void RegisterServerProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.serverProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000C3244 File Offset: 0x000C1444
		internal static void RegisterChannels(ArrayList channels, bool onlyDelayed)
		{
			foreach (object obj in channels)
			{
				ChannelData channelData = (ChannelData)obj;
				if ((!onlyDelayed || !(channelData.DelayLoadAsClientChannel != "true")) && (!RemotingConfiguration.defaultDelayedConfigRead || !(channelData.DelayLoadAsClientChannel == "true")))
				{
					if (channelData.Ref != null)
					{
						ChannelData channelData2 = (ChannelData)RemotingConfiguration.channelTemplates[channelData.Ref];
						if (channelData2 == null)
						{
							throw new RemotingException("Channel template '" + channelData.Ref + "' not found");
						}
						channelData.CopyFrom(channelData2);
					}
					foreach (object obj2 in channelData.ServerProviders)
					{
						ProviderData providerData = (ProviderData)obj2;
						if (providerData.Ref != null)
						{
							ProviderData providerData2 = (ProviderData)RemotingConfiguration.serverProviderTemplates[providerData.Ref];
							if (providerData2 == null)
							{
								throw new RemotingException("Provider template '" + providerData.Ref + "' not found");
							}
							providerData.CopyFrom(providerData2);
						}
					}
					foreach (object obj3 in channelData.ClientProviders)
					{
						ProviderData providerData3 = (ProviderData)obj3;
						if (providerData3.Ref != null)
						{
							ProviderData providerData4 = (ProviderData)RemotingConfiguration.clientProviderTemplates[providerData3.Ref];
							if (providerData4 == null)
							{
								throw new RemotingException("Provider template '" + providerData3.Ref + "' not found");
							}
							providerData3.CopyFrom(providerData4);
						}
					}
					ChannelServices.RegisterChannelConfig(channelData);
				}
			}
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000C345C File Offset: 0x000C165C
		internal static void RegisterTypes(ArrayList types)
		{
			foreach (object obj in types)
			{
				TypeEntry typeEntry = (TypeEntry)obj;
				if (typeEntry is ActivatedClientTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedClientType((ActivatedClientTypeEntry)typeEntry);
				}
				else if (typeEntry is ActivatedServiceTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedServiceType((ActivatedServiceTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownClientTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownClientType((WellKnownClientTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownServiceTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownServiceType((WellKnownServiceTypeEntry)typeEntry);
				}
			}
		}

		/// <summary>Indicates whether the server channels in this application domain return filtered or complete exception information to local or remote callers.</summary>
		/// <param name="isLocalRequest">
		///   <see langword="true" /> to specify local callers; <see langword="false" /> to specify remote callers.</param>
		/// <returns>
		///   <see langword="true" /> if only filtered exception information is returned to local or remote callers, as specified by the <paramref name="isLocalRequest" /> parameter; <see langword="false" /> if complete exception information is returned.</returns>
		// Token: 0x06003643 RID: 13891 RVA: 0x000C34FC File Offset: 0x000C16FC
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			return RemotingConfiguration._errorMode != CustomErrorsModes.Off && (RemotingConfiguration._errorMode == CustomErrorsModes.On || !isLocalRequest);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000C3518 File Offset: 0x000C1718
		internal static void SetCustomErrorsMode(string mode)
		{
			if (mode == null)
			{
				throw new RemotingException("mode attribute is required");
			}
			string text = mode.ToLower();
			if (text != "on" && text != "off" && text != "remoteonly")
			{
				throw new RemotingException("Invalid custom error mode: " + mode);
			}
			RemotingConfiguration._errorMode = (CustomErrorsModes)Enum.Parse(typeof(CustomErrorsModes), text, true);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000C3590 File Offset: 0x000C1790
		// Note: this type is marked as 'beforefieldinit'.
		static RemotingConfiguration()
		{
		}

		// Token: 0x0400252D RID: 9517
		private static string applicationID = null;

		// Token: 0x0400252E RID: 9518
		private static string applicationName = null;

		// Token: 0x0400252F RID: 9519
		private static string processGuid = null;

		// Token: 0x04002530 RID: 9520
		private static bool defaultConfigRead = false;

		// Token: 0x04002531 RID: 9521
		private static bool defaultDelayedConfigRead = false;

		// Token: 0x04002532 RID: 9522
		private static CustomErrorsModes _errorMode = CustomErrorsModes.RemoteOnly;

		// Token: 0x04002533 RID: 9523
		private static Hashtable wellKnownClientEntries = new Hashtable();

		// Token: 0x04002534 RID: 9524
		private static Hashtable activatedClientEntries = new Hashtable();

		// Token: 0x04002535 RID: 9525
		private static Hashtable wellKnownServiceEntries = new Hashtable();

		// Token: 0x04002536 RID: 9526
		private static Hashtable activatedServiceEntries = new Hashtable();

		// Token: 0x04002537 RID: 9527
		private static Hashtable channelTemplates = new Hashtable();

		// Token: 0x04002538 RID: 9528
		private static Hashtable clientProviderTemplates = new Hashtable();

		// Token: 0x04002539 RID: 9529
		private static Hashtable serverProviderTemplates = new Hashtable();
	}
}
