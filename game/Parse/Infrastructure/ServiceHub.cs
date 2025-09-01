using System;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Infrastructure.Data;
using Parse.Abstractions.Infrastructure.Execution;
using Parse.Abstractions.Platform.Analytics;
using Parse.Abstractions.Platform.Cloud;
using Parse.Abstractions.Platform.Configuration;
using Parse.Abstractions.Platform.Files;
using Parse.Abstractions.Platform.Installations;
using Parse.Abstractions.Platform.Objects;
using Parse.Abstractions.Platform.Push;
using Parse.Abstractions.Platform.Queries;
using Parse.Abstractions.Platform.Sessions;
using Parse.Abstractions.Platform.Users;
using Parse.Infrastructure.Data;
using Parse.Infrastructure.Execution;
using Parse.Infrastructure.Utilities;
using Parse.Platform.Analytics;
using Parse.Platform.Cloud;
using Parse.Platform.Configuration;
using Parse.Platform.Files;
using Parse.Platform.Installations;
using Parse.Platform.Objects;
using Parse.Platform.Push;
using Parse.Platform.Queries;
using Parse.Platform.Sessions;
using Parse.Platform.Users;

namespace Parse.Infrastructure
{
	// Token: 0x0200004D RID: 77
	public class ServiceHub : IServiceHub
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000C561 File Offset: 0x0000A761
		private LateInitializer LateInitializer
		{
			[CompilerGenerated]
			get
			{
				return this.<LateInitializer>k__BackingField;
			}
		} = new LateInitializer();

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000C569 File Offset: 0x0000A769
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000C571 File Offset: 0x0000A771
		public IServerConnectionData ServerConnectionData
		{
			[CompilerGenerated]
			get
			{
				return this.<ServerConnectionData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ServerConnectionData>k__BackingField = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000C57A File Offset: 0x0000A77A
		public IMetadataController MetadataController
		{
			get
			{
				return this.LateInitializer.GetValue<MetadataController>(() => new MetadataController
				{
					HostManifestData = HostManifestData.Inferred,
					EnvironmentData = EnvironmentData.Inferred
				});
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000C5A6 File Offset: 0x0000A7A6
		public IServiceHubCloner Cloner
		{
			get
			{
				return this.LateInitializer.GetValue<IServiceHubCloner>(() => new
				{

				} as IServiceHubCloner);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000C5D2 File Offset: 0x0000A7D2
		public IWebClient WebClient
		{
			get
			{
				return this.LateInitializer.GetValue<UniversalWebClient>(() => new UniversalWebClient());
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000C5FE File Offset: 0x0000A7FE
		public ICacheController CacheController
		{
			get
			{
				return this.LateInitializer.GetValue<CacheController>(() => new CacheController());
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000C62A File Offset: 0x0000A82A
		public IParseObjectClassController ClassController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseObjectClassController>(() => new ParseObjectClassController());
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000C656 File Offset: 0x0000A856
		public IParseDataDecoder Decoder
		{
			get
			{
				return this.LateInitializer.GetValue<ParseDataDecoder>(() => new ParseDataDecoder(this.ClassController));
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000C66F File Offset: 0x0000A86F
		public IParseInstallationController InstallationController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseInstallationController>(() => new ParseInstallationController(this.CacheController));
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000C688 File Offset: 0x0000A888
		public IParseCommandRunner CommandRunner
		{
			get
			{
				return this.LateInitializer.GetValue<ParseCommandRunner>(() => new ParseCommandRunner(this.WebClient, this.InstallationController, this.MetadataController, this.ServerConnectionData, new Lazy<IParseUserController>(() => this.UserController)));
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000C6A1 File Offset: 0x0000A8A1
		public IParseCloudCodeController CloudCodeController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseCloudCodeController>(() => new ParseCloudCodeController(this.CommandRunner, this.Decoder));
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000C6BA File Offset: 0x0000A8BA
		public IParseConfigurationController ConfigurationController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseConfigurationController>(() => new ParseConfigurationController(this.CommandRunner, this.CacheController, this.Decoder));
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
		public IParseFileController FileController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseFileController>(() => new ParseFileController(this.CommandRunner));
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public IParseObjectController ObjectController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseObjectController>(() => new ParseObjectController(this.CommandRunner, this.Decoder, this.ServerConnectionData));
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000C705 File Offset: 0x0000A905
		public IParseQueryController QueryController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseQueryController>(() => new ParseQueryController(this.CommandRunner, this.Decoder));
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000C71E File Offset: 0x0000A91E
		public IParseSessionController SessionController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseSessionController>(() => new ParseSessionController(this.CommandRunner, this.Decoder));
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000C737 File Offset: 0x0000A937
		public IParseUserController UserController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseUserController>(() => new ParseUserController(this.CommandRunner, this.Decoder));
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000C750 File Offset: 0x0000A950
		public IParseCurrentUserController CurrentUserController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseCurrentUserController>(() => new ParseCurrentUserController(this.CacheController, this.ClassController, this.Decoder));
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000C769 File Offset: 0x0000A969
		public IParseAnalyticsController AnalyticsController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseAnalyticsController>(() => new ParseAnalyticsController(this.CommandRunner));
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000C782 File Offset: 0x0000A982
		public IParseInstallationCoder InstallationCoder
		{
			get
			{
				return this.LateInitializer.GetValue<ParseInstallationCoder>(() => new ParseInstallationCoder(this.Decoder, this.ClassController));
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000C79B File Offset: 0x0000A99B
		public IParsePushChannelsController PushChannelsController
		{
			get
			{
				return this.LateInitializer.GetValue<ParsePushChannelsController>(() => new ParsePushChannelsController(this.CurrentInstallationController));
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public IParsePushController PushController
		{
			get
			{
				return this.LateInitializer.GetValue<ParsePushController>(() => new ParsePushController(this.CommandRunner, this.CurrentUserController));
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000C7CD File Offset: 0x0000A9CD
		public IParseCurrentInstallationController CurrentInstallationController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseCurrentInstallationController>(() => new ParseCurrentInstallationController(this.InstallationController, this.CacheController, this.InstallationCoder, this.ClassController));
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000C7E6 File Offset: 0x0000A9E6
		public IParseInstallationDataFinalizer InstallationDataFinalizer
		{
			get
			{
				return this.LateInitializer.GetValue<ParseInstallationDataFinalizer>(() => new ParseInstallationDataFinalizer());
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000C812 File Offset: 0x0000AA12
		public bool Reset()
		{
			return this.LateInitializer.Used && this.LateInitializer.Reset();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000C82E File Offset: 0x0000AA2E
		public ServiceHub()
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000C841 File Offset: 0x0000AA41
		[CompilerGenerated]
		private ParseDataDecoder <get_Decoder>b__18_0()
		{
			return new ParseDataDecoder(this.ClassController);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000C84E File Offset: 0x0000AA4E
		[CompilerGenerated]
		private ParseInstallationController <get_InstallationController>b__20_0()
		{
			return new ParseInstallationController(this.CacheController);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000C85B File Offset: 0x0000AA5B
		[CompilerGenerated]
		private ParseCommandRunner <get_CommandRunner>b__22_0()
		{
			return new ParseCommandRunner(this.WebClient, this.InstallationController, this.MetadataController, this.ServerConnectionData, new Lazy<IParseUserController>(() => this.UserController));
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000C88B File Offset: 0x0000AA8B
		[CompilerGenerated]
		private IParseUserController <get_CommandRunner>b__22_1()
		{
			return this.UserController;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000C893 File Offset: 0x0000AA93
		[CompilerGenerated]
		private ParseCloudCodeController <get_CloudCodeController>b__24_0()
		{
			return new ParseCloudCodeController(this.CommandRunner, this.Decoder);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000C8A6 File Offset: 0x0000AAA6
		[CompilerGenerated]
		private ParseConfigurationController <get_ConfigurationController>b__26_0()
		{
			return new ParseConfigurationController(this.CommandRunner, this.CacheController, this.Decoder);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000C8BF File Offset: 0x0000AABF
		[CompilerGenerated]
		private ParseFileController <get_FileController>b__28_0()
		{
			return new ParseFileController(this.CommandRunner);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000C8CC File Offset: 0x0000AACC
		[CompilerGenerated]
		private ParseObjectController <get_ObjectController>b__30_0()
		{
			return new ParseObjectController(this.CommandRunner, this.Decoder, this.ServerConnectionData);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000C8E5 File Offset: 0x0000AAE5
		[CompilerGenerated]
		private ParseQueryController <get_QueryController>b__32_0()
		{
			return new ParseQueryController(this.CommandRunner, this.Decoder);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		[CompilerGenerated]
		private ParseSessionController <get_SessionController>b__34_0()
		{
			return new ParseSessionController(this.CommandRunner, this.Decoder);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000C90B File Offset: 0x0000AB0B
		[CompilerGenerated]
		private ParseUserController <get_UserController>b__36_0()
		{
			return new ParseUserController(this.CommandRunner, this.Decoder);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000C91E File Offset: 0x0000AB1E
		[CompilerGenerated]
		private ParseCurrentUserController <get_CurrentUserController>b__38_0()
		{
			return new ParseCurrentUserController(this.CacheController, this.ClassController, this.Decoder);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000C937 File Offset: 0x0000AB37
		[CompilerGenerated]
		private ParseAnalyticsController <get_AnalyticsController>b__40_0()
		{
			return new ParseAnalyticsController(this.CommandRunner);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000C944 File Offset: 0x0000AB44
		[CompilerGenerated]
		private ParseInstallationCoder <get_InstallationCoder>b__42_0()
		{
			return new ParseInstallationCoder(this.Decoder, this.ClassController);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000C957 File Offset: 0x0000AB57
		[CompilerGenerated]
		private ParsePushChannelsController <get_PushChannelsController>b__44_0()
		{
			return new ParsePushChannelsController(this.CurrentInstallationController);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000C964 File Offset: 0x0000AB64
		[CompilerGenerated]
		private ParsePushController <get_PushController>b__46_0()
		{
			return new ParsePushController(this.CommandRunner, this.CurrentUserController);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C977 File Offset: 0x0000AB77
		[CompilerGenerated]
		private ParseCurrentInstallationController <get_CurrentInstallationController>b__48_0()
		{
			return new ParseCurrentInstallationController(this.InstallationController, this.CacheController, this.InstallationCoder, this.ClassController);
		}

		// Token: 0x040000C4 RID: 196
		[CompilerGenerated]
		private readonly LateInitializer <LateInitializer>k__BackingField;

		// Token: 0x040000C5 RID: 197
		[CompilerGenerated]
		private IServerConnectionData <ServerConnectionData>k__BackingField;

		// Token: 0x02000117 RID: 279
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000741 RID: 1857 RVA: 0x0001622B File Offset: 0x0001442B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x00016237 File Offset: 0x00014437
			public <>c()
			{
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x0001623F File Offset: 0x0001443F
			internal MetadataController <get_MetadataController>b__8_0()
			{
				return new MetadataController
				{
					HostManifestData = HostManifestData.Inferred,
					EnvironmentData = EnvironmentData.Inferred
				};
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x0001625C File Offset: 0x0001445C
			internal IServiceHubCloner <get_Cloner>b__10_0()
			{
				return new
				{

				} as IServiceHubCloner;
			}

			// Token: 0x06000745 RID: 1861 RVA: 0x00016268 File Offset: 0x00014468
			internal UniversalWebClient <get_WebClient>b__12_0()
			{
				return new UniversalWebClient();
			}

			// Token: 0x06000746 RID: 1862 RVA: 0x0001626F File Offset: 0x0001446F
			internal CacheController <get_CacheController>b__14_0()
			{
				return new CacheController();
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x00016276 File Offset: 0x00014476
			internal ParseObjectClassController <get_ClassController>b__16_0()
			{
				return new ParseObjectClassController();
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x0001627D File Offset: 0x0001447D
			internal ParseInstallationDataFinalizer <get_InstallationDataFinalizer>b__50_0()
			{
				return new ParseInstallationDataFinalizer();
			}

			// Token: 0x04000283 RID: 643
			public static readonly ServiceHub.<>c <>9 = new ServiceHub.<>c();

			// Token: 0x04000284 RID: 644
			public static Func<MetadataController> <>9__8_0;

			// Token: 0x04000285 RID: 645
			public static Func<IServiceHubCloner> <>9__10_0;

			// Token: 0x04000286 RID: 646
			public static Func<UniversalWebClient> <>9__12_0;

			// Token: 0x04000287 RID: 647
			public static Func<CacheController> <>9__14_0;

			// Token: 0x04000288 RID: 648
			public static Func<ParseObjectClassController> <>9__16_0;

			// Token: 0x04000289 RID: 649
			public static Func<ParseInstallationDataFinalizer> <>9__50_0;
		}
	}
}
