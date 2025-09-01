using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Parse.Abstractions.Infrastructure;
using Parse.Infrastructure;
using Parse.Infrastructure.Utilities;

namespace Parse
{
	// Token: 0x0200000F RID: 15
	public class ParseClient : CustomServiceHub, IServiceHubComposer
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004C3A File Offset: 0x00002E3A
		internal static string[] DateFormatStrings
		{
			[CompilerGenerated]
			get
			{
				return ParseClient.<DateFormatStrings>k__BackingField;
			}
		} = new string[]
		{
			"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'",
			"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ff'Z'",
			"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'f'Z'"
		};

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004C41 File Offset: 0x00002E41
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004C48 File Offset: 0x00002E48
		public static bool IL2CPPCompiled
		{
			[CompilerGenerated]
			get
			{
				return ParseClient.<IL2CPPCompiled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				ParseClient.<IL2CPPCompiled>k__BackingField = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004C50 File Offset: 0x00002E50
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004C57 File Offset: 0x00002E57
		public static ParseClient Instance
		{
			[CompilerGenerated]
			get
			{
				return ParseClient.<Instance>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				ParseClient.<Instance>k__BackingField = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004C60 File Offset: 0x00002E60
		internal static string Version
		{
			get
			{
				Type typeFromHandle = typeof(ParseClient);
				string text;
				if (typeFromHandle == null)
				{
					text = null;
				}
				else
				{
					Assembly assembly = typeFromHandle.Assembly;
					if (assembly == null)
					{
						text = null;
					}
					else
					{
						AssemblyInformationalVersionAttribute customAttribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
						text = ((customAttribute != null) ? customAttribute.InformationalVersion : null);
					}
				}
				string result;
				if ((result = text) == null)
				{
					Type typeFromHandle2 = typeof(ParseClient);
					if (typeFromHandle2 == null)
					{
						return null;
					}
					Assembly assembly2 = typeFromHandle2.Assembly;
					if (assembly2 == null)
					{
						return null;
					}
					AssemblyName name = assembly2.GetName();
					if (name == null)
					{
						return null;
					}
					Version version = name.Version;
					if (version == null)
					{
						return null;
					}
					result = version.ToString();
				}
				return result;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004CD5 File Offset: 0x00002ED5
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004CDD File Offset: 0x00002EDD
		public override IServiceHub Services
		{
			[CompilerGenerated]
			get
			{
				return this.<Services>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Services>k__BackingField = value;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004CE8 File Offset: 0x00002EE8
		public ParseClient(string applicationID, string serverURI, string key, IServiceHub serviceHub = null, params IServiceHubMutator[] configurators) : this(new ServerConnectionData
		{
			ApplicationID = applicationID,
			ServerURI = serverURI,
			Key = key
		}, serviceHub, configurators)
		{
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004D28 File Offset: 0x00002F28
		public ParseClient(IServerConnectionData configuration, IServiceHub serviceHub = null, params IServiceHubMutator[] configurators)
		{
			ParseClient.<>c__DisplayClass18_0 CS$<>8__locals1;
			CS$<>8__locals1.configuration = configuration;
			base..ctor();
			IServiceHub services;
			if (serviceHub == null)
			{
				IServiceHub serviceHub2 = new ServiceHub
				{
					ServerConnectionData = ParseClient.<.ctor>g__GenerateServerConnectionData|18_0(ref CS$<>8__locals1)
				};
				services = serviceHub2;
			}
			else
			{
				IServiceHub serviceHub2 = new OrchestrationServiceHub
				{
					Custom = serviceHub,
					Default = new ServiceHub
					{
						ServerConnectionData = ParseClient.<.ctor>g__GenerateServerConnectionData|18_0(ref CS$<>8__locals1)
					}
				};
				services = serviceHub2;
			}
			this.Services = services;
			if (configurators != null)
			{
				int length = configurators.Length;
				if (length > 0)
				{
					IMutableServiceHub mutableServiceHub = serviceHub as IMutableServiceHub;
					IServiceHub serviceHub2;
					if (mutableServiceHub == null)
					{
						if (serviceHub == null)
						{
							throw new InvalidOperationException();
						}
						serviceHub2 = this.BuildHub(null, this.Services, configurators);
					}
					else
					{
						serviceHub2 = this.BuildHub(new ValueTuple<IMutableServiceHub, IServerConnectionData>(mutableServiceHub, mutableServiceHub.ServerConnectionData = (serviceHub.ServerConnectionData ?? this.Services.ServerConnectionData)).Item1, this.Services, configurators);
					}
					this.Services = serviceHub2;
				}
			}
			this.Services.ClassController.AddIntrinsic();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004E10 File Offset: 0x00003010
		public ParseClient()
		{
			ParseClient instance = ParseClient.Instance;
			if (instance == null)
			{
				throw new InvalidOperationException("A ParseClient instance with an initializer service must first be publicized in order for the default constructor to be used.");
			}
			IServiceHubCloner cloner = instance.Services.Cloner;
			IServiceHub services = ParseClient.Instance.Services;
			this.Services = cloner.BuildHub(services, this, Array.Empty<IServiceHubMutator>());
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004E60 File Offset: 0x00003060
		public void Publicize()
		{
			object mutex = ParseClient.Mutex;
			lock (mutex)
			{
				ParseClient.Instance = this;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004EA0 File Offset: 0x000030A0
		private static object Mutex
		{
			[CompilerGenerated]
			get
			{
				return ParseClient.<Mutex>k__BackingField;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004EA8 File Offset: 0x000030A8
		internal static string BuildQueryString(IDictionary<string, object> parameters)
		{
			return string.Join("&", (from <>h__TransparentIdentifier0 in parameters.Select(delegate(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return new
				{
					pair = pair,
					valueString = (keyValuePair.Value as string)
				};
			})
			select Uri.EscapeDataString(<>h__TransparentIdentifier0.pair.Key) + "=" + Uri.EscapeDataString(string.IsNullOrEmpty(<>h__TransparentIdentifier0.valueString) ? JsonUtilities.Encode(<>h__TransparentIdentifier0.pair.Value) : <>h__TransparentIdentifier0.valueString)).ToArray<string>());
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004F10 File Offset: 0x00003110
		internal static IDictionary<string, string> DecodeQueryString(string queryString)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string[] array = queryString.Split(new char[]
			{
				'&'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				}, 2);
				dictionary[array2[0]] = ((array2.Length == 2) ? Uri.UnescapeDataString(array2[1].Replace("+", " ")) : null);
			}
			return dictionary;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004F82 File Offset: 0x00003182
		internal static IDictionary<string, object> DeserializeJsonString(string jsonData)
		{
			return JsonUtilities.Parse(jsonData) as IDictionary<string, object>;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004F8F File Offset: 0x0000318F
		internal static string SerializeJsonString(IDictionary<string, object> jsonData)
		{
			return JsonUtilities.Encode(jsonData);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004F98 File Offset: 0x00003198
		public IServiceHub BuildHub(IMutableServiceHub target = null, IServiceHub extension = null, params IServiceHubMutator[] configurators)
		{
			OrchestrationServiceHub orchestrationServiceHub = new OrchestrationServiceHub();
			IMutableServiceHub custom;
			if ((custom = target) == null)
			{
				custom = (target = new MutableServiceHub());
			}
			orchestrationServiceHub.Custom = custom;
			orchestrationServiceHub.Default = (extension ?? new ServiceHub());
			OrchestrationServiceHub orchestrationServiceHub2 = orchestrationServiceHub;
			foreach (IServiceHubMutator serviceHubMutator in from configurator in configurators
			where configurator.Valid
			select configurator)
			{
				IServiceHub serviceHub = orchestrationServiceHub2;
				serviceHubMutator.Mutate(ref target, serviceHub);
				orchestrationServiceHub2.Custom = target;
			}
			return orchestrationServiceHub2;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005038 File Offset: 0x00003238
		// Note: this type is marked as 'beforefieldinit'.
		static ParseClient()
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			bool il2CPPCompiled;
			if (currentDomain == null)
			{
				il2CPPCompiled = false;
			}
			else
			{
				string friendlyName = currentDomain.FriendlyName;
				bool? flag = (friendlyName != null) ? new bool?(friendlyName.Equals("IL2CPP Root Domain")) : null;
				bool flag2 = true;
				il2CPPCompiled = (flag.GetValueOrDefault() == flag2 & flag != null);
			}
			ParseClient.IL2CPPCompiled = il2CPPCompiled;
			ParseClient.Mutex = new object();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000050BC File Offset: 0x000032BC
		[CompilerGenerated]
		internal static IServerConnectionData <.ctor>g__GenerateServerConnectionData|18_0(ref ParseClient.<>c__DisplayClass18_0 A_0)
		{
			if (A_0.configuration != null)
			{
				if (A_0.configuration is ServerConnectionData)
				{
					ServerConnectionData serverConnectionData = (ServerConnectionData)A_0.configuration;
					if (serverConnectionData.Test)
					{
						if (serverConnectionData.ServerURI == null)
						{
							ServerConnectionData serverConnectionData2 = serverConnectionData;
							return new ServerConnectionData
							{
								ApplicationID = serverConnectionData2.ApplicationID,
								Headers = serverConnectionData2.Headers,
								MasterKey = serverConnectionData2.MasterKey,
								Test = serverConnectionData2.Test,
								Key = serverConnectionData2.Key,
								ServerURI = "https://api.parse.com/1/"
							};
						}
						return serverConnectionData;
					}
				}
				string serverURI = A_0.configuration.ServerURI;
				if (serverURI != null)
				{
					if (serverURI == "https://api.parse.com/1/")
					{
						throw new InvalidOperationException("Since the official parse server has shut down, you must specify a URI that points to a hosted instance.");
					}
					if (A_0.configuration.ApplicationID != null)
					{
						if (A_0.configuration.Key != null)
						{
							return A_0.configuration;
						}
					}
				}
				throw new InvalidOperationException("The IServerConnectionData implementation instance provided to the ParseClient constructor must be populated with the information needed to connect to a Parse server instance.");
			}
			throw new ArgumentNullException("configuration");
		}

		// Token: 0x0400001D RID: 29
		[CompilerGenerated]
		private static readonly string[] <DateFormatStrings>k__BackingField;

		// Token: 0x0400001E RID: 30
		[CompilerGenerated]
		private static bool <IL2CPPCompiled>k__BackingField;

		// Token: 0x0400001F RID: 31
		[CompilerGenerated]
		private static ParseClient <Instance>k__BackingField;

		// Token: 0x04000020 RID: 32
		[CompilerGenerated]
		private IServiceHub <Services>k__BackingField;

		// Token: 0x04000021 RID: 33
		[CompilerGenerated]
		private static readonly object <Mutex>k__BackingField;

		// Token: 0x020000AF RID: 175
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass18_0
		{
			// Token: 0x04000137 RID: 311
			public IServerConnectionData configuration;
		}

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005EF RID: 1519 RVA: 0x000130B3 File Offset: 0x000112B3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x000130BF File Offset: 0x000112BF
			public <>c()
			{
			}

			// Token: 0x060005F1 RID: 1521 RVA: 0x000130C8 File Offset: 0x000112C8
			internal <>f__AnonymousType1<KeyValuePair<string, object>, string> <BuildQueryString>b__24_0(KeyValuePair<string, object> pair)
			{
				KeyValuePair<string, object> keyValuePair = pair;
				return new
				{
					pair = pair,
					valueString = (keyValuePair.Value as string)
				};
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x000130EC File Offset: 0x000112EC
			internal string <BuildQueryString>b__24_1(<>f__AnonymousType1<KeyValuePair<string, object>, string> <>h__TransparentIdentifier0)
			{
				return Uri.EscapeDataString(<>h__TransparentIdentifier0.pair.Key) + "=" + Uri.EscapeDataString(string.IsNullOrEmpty(<>h__TransparentIdentifier0.valueString) ? JsonUtilities.Encode(<>h__TransparentIdentifier0.pair.Value) : <>h__TransparentIdentifier0.valueString);
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x00013143 File Offset: 0x00011343
			internal bool <BuildHub>b__28_0(IServiceHubMutator configurator)
			{
				return configurator.Valid;
			}

			// Token: 0x04000138 RID: 312
			public static readonly ParseClient.<>c <>9 = new ParseClient.<>c();

			// Token: 0x04000139 RID: 313
			public static Func<KeyValuePair<string, object>, <>f__AnonymousType1<KeyValuePair<string, object>, string>> <>9__24_0;

			// Token: 0x0400013A RID: 314
			public static Func<<>f__AnonymousType1<KeyValuePair<string, object>, string>, string> <>9__24_1;

			// Token: 0x0400013B RID: 315
			public static Func<IServiceHubMutator, bool> <>9__28_0;
		}
	}
}
