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
	// Token: 0x02000044 RID: 68
	public class LateInitializedMutableServiceHub : IMutableServiceHub, IServiceHub
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000B613 File Offset: 0x00009813
		private LateInitializer LateInitializer
		{
			[CompilerGenerated]
			get
			{
				return this.<LateInitializer>k__BackingField;
			}
		} = new LateInitializer();

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B61B File Offset: 0x0000981B
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000B623 File Offset: 0x00009823
		public IServiceHubCloner Cloner
		{
			[CompilerGenerated]
			get
			{
				return this.<Cloner>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Cloner>k__BackingField = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000B62C File Offset: 0x0000982C
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000B658 File Offset: 0x00009858
		public IMetadataController MetadataController
		{
			get
			{
				return this.LateInitializer.GetValue<IMetadataController>(() => new MetadataController
				{
					EnvironmentData = EnvironmentData.Inferred,
					HostManifestData = HostManifestData.Inferred
				});
			}
			set
			{
				this.LateInitializer.SetValue<IMetadataController>(value, true);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000B668 File Offset: 0x00009868
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000B694 File Offset: 0x00009894
		public IWebClient WebClient
		{
			get
			{
				return this.LateInitializer.GetValue<IWebClient>(() => new UniversalWebClient());
			}
			set
			{
				this.LateInitializer.SetValue<IWebClient>(value, true);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B6A4 File Offset: 0x000098A4
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public ICacheController CacheController
		{
			get
			{
				return this.LateInitializer.GetValue<ICacheController>(() => new CacheController());
			}
			set
			{
				this.LateInitializer.SetValue<ICacheController>(value, true);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B6E0 File Offset: 0x000098E0
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000B70C File Offset: 0x0000990C
		public IParseObjectClassController ClassController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseObjectClassController>(() => new ParseObjectClassController());
			}
			set
			{
				this.LateInitializer.SetValue<IParseObjectClassController>(value, true);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B71C File Offset: 0x0000991C
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000B735 File Offset: 0x00009935
		public IParseInstallationController InstallationController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseInstallationController>(() => new ParseInstallationController(this.CacheController));
			}
			set
			{
				this.LateInitializer.SetValue<IParseInstallationController>(value, true);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B745 File Offset: 0x00009945
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000B75E File Offset: 0x0000995E
		public IParseCommandRunner CommandRunner
		{
			get
			{
				return this.LateInitializer.GetValue<IParseCommandRunner>(() => new ParseCommandRunner(this.WebClient, this.InstallationController, this.MetadataController, this.ServerConnectionData, new Lazy<IParseUserController>(() => this.UserController)));
			}
			set
			{
				this.LateInitializer.SetValue<IParseCommandRunner>(value, true);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B76E File Offset: 0x0000996E
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000B787 File Offset: 0x00009987
		public IParseCloudCodeController CloudCodeController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseCloudCodeController>(() => new ParseCloudCodeController(this.CommandRunner, this.Decoder));
			}
			set
			{
				this.LateInitializer.SetValue<IParseCloudCodeController>(value, true);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000B797 File Offset: 0x00009997
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000B7B0 File Offset: 0x000099B0
		public IParseConfigurationController ConfigurationController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseConfigurationController>(() => new ParseConfigurationController(this.CommandRunner, this.CacheController, this.Decoder));
			}
			set
			{
				this.LateInitializer.SetValue<IParseConfigurationController>(value, true);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000B7C0 File Offset: 0x000099C0
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000B7D9 File Offset: 0x000099D9
		public IParseFileController FileController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseFileController>(() => new ParseFileController(this.CommandRunner));
			}
			set
			{
				this.LateInitializer.SetValue<IParseFileController>(value, true);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000B7E9 File Offset: 0x000099E9
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000B802 File Offset: 0x00009A02
		public IParseObjectController ObjectController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseObjectController>(() => new ParseObjectController(this.CommandRunner, this.Decoder, this.ServerConnectionData));
			}
			set
			{
				this.LateInitializer.SetValue<IParseObjectController>(value, true);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000B812 File Offset: 0x00009A12
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000B82B File Offset: 0x00009A2B
		public IParseQueryController QueryController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseQueryController>(() => new ParseQueryController(this.CommandRunner, this.Decoder));
			}
			set
			{
				this.LateInitializer.SetValue<IParseQueryController>(value, true);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000B83B File Offset: 0x00009A3B
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000B854 File Offset: 0x00009A54
		public IParseSessionController SessionController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseSessionController>(() => new ParseSessionController(this.CommandRunner, this.Decoder));
			}
			set
			{
				this.LateInitializer.SetValue<IParseSessionController>(value, true);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000B864 File Offset: 0x00009A64
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000B87D File Offset: 0x00009A7D
		public IParseUserController UserController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseUserController>(() => new ParseUserController(this.CommandRunner, this.Decoder));
			}
			set
			{
				this.LateInitializer.SetValue<IParseUserController>(value, true);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000B88D File Offset: 0x00009A8D
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000B8A6 File Offset: 0x00009AA6
		public IParseCurrentUserController CurrentUserController
		{
			get
			{
				return this.LateInitializer.GetValue<ParseCurrentUserController>(() => new ParseCurrentUserController(this.CacheController, this.ClassController, this.Decoder));
			}
			set
			{
				this.LateInitializer.SetValue<IParseCurrentUserController>(value, true);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000B8B6 File Offset: 0x00009AB6
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000B8CF File Offset: 0x00009ACF
		public IParseAnalyticsController AnalyticsController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseAnalyticsController>(() => new ParseAnalyticsController(this.CommandRunner));
			}
			set
			{
				this.LateInitializer.SetValue<IParseAnalyticsController>(value, true);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000B8DF File Offset: 0x00009ADF
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		public IParseInstallationCoder InstallationCoder
		{
			get
			{
				return this.LateInitializer.GetValue<IParseInstallationCoder>(() => new ParseInstallationCoder(this.Decoder, this.ClassController));
			}
			set
			{
				this.LateInitializer.SetValue<IParseInstallationCoder>(value, true);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000B908 File Offset: 0x00009B08
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0000B921 File Offset: 0x00009B21
		public IParsePushChannelsController PushChannelsController
		{
			get
			{
				return this.LateInitializer.GetValue<IParsePushChannelsController>(() => new ParsePushChannelsController(this.CurrentInstallationController));
			}
			set
			{
				this.LateInitializer.SetValue<IParsePushChannelsController>(value, true);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000B931 File Offset: 0x00009B31
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000B94A File Offset: 0x00009B4A
		public IParsePushController PushController
		{
			get
			{
				return this.LateInitializer.GetValue<IParsePushController>(() => new ParsePushController(this.CommandRunner, this.CurrentUserController));
			}
			set
			{
				this.LateInitializer.SetValue<IParsePushController>(value, true);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000B95A File Offset: 0x00009B5A
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000B973 File Offset: 0x00009B73
		public IParseCurrentInstallationController CurrentInstallationController
		{
			get
			{
				return this.LateInitializer.GetValue<IParseCurrentInstallationController>(() => new ParseCurrentInstallationController(this.InstallationController, this.CacheController, this.InstallationCoder, this.ClassController));
			}
			set
			{
				this.LateInitializer.SetValue<IParseCurrentInstallationController>(value, true);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000B983 File Offset: 0x00009B83
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000B99C File Offset: 0x00009B9C
		public IParseDataDecoder Decoder
		{
			get
			{
				return this.LateInitializer.GetValue<IParseDataDecoder>(() => new ParseDataDecoder(this.ClassController));
			}
			set
			{
				this.LateInitializer.SetValue<IParseDataDecoder>(value, true);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000B9AC File Offset: 0x00009BAC
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		public IParseInstallationDataFinalizer InstallationDataFinalizer
		{
			get
			{
				return this.LateInitializer.GetValue<IParseInstallationDataFinalizer>(() => new ParseInstallationDataFinalizer());
			}
			set
			{
				this.LateInitializer.SetValue<IParseInstallationDataFinalizer>(value, true);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000B9F0 File Offset: 0x00009BF0
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

		// Token: 0x0600034A RID: 842 RVA: 0x0000B9F9 File Offset: 0x00009BF9
		public LateInitializedMutableServiceHub()
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BA0C File Offset: 0x00009C0C
		[CompilerGenerated]
		private IParseInstallationController <get_InstallationController>b__20_0()
		{
			return new ParseInstallationController(this.CacheController);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BA19 File Offset: 0x00009C19
		[CompilerGenerated]
		private IParseCommandRunner <get_CommandRunner>b__23_0()
		{
			return new ParseCommandRunner(this.WebClient, this.InstallationController, this.MetadataController, this.ServerConnectionData, new Lazy<IParseUserController>(() => this.UserController));
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BA49 File Offset: 0x00009C49
		[CompilerGenerated]
		private IParseUserController <get_CommandRunner>b__23_1()
		{
			return this.UserController;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BA51 File Offset: 0x00009C51
		[CompilerGenerated]
		private IParseCloudCodeController <get_CloudCodeController>b__26_0()
		{
			return new ParseCloudCodeController(this.CommandRunner, this.Decoder);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BA64 File Offset: 0x00009C64
		[CompilerGenerated]
		private IParseConfigurationController <get_ConfigurationController>b__29_0()
		{
			return new ParseConfigurationController(this.CommandRunner, this.CacheController, this.Decoder);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000BA7D File Offset: 0x00009C7D
		[CompilerGenerated]
		private IParseFileController <get_FileController>b__32_0()
		{
			return new ParseFileController(this.CommandRunner);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000BA8A File Offset: 0x00009C8A
		[CompilerGenerated]
		private IParseObjectController <get_ObjectController>b__35_0()
		{
			return new ParseObjectController(this.CommandRunner, this.Decoder, this.ServerConnectionData);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000BAA3 File Offset: 0x00009CA3
		[CompilerGenerated]
		private IParseQueryController <get_QueryController>b__38_0()
		{
			return new ParseQueryController(this.CommandRunner, this.Decoder);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000BAB6 File Offset: 0x00009CB6
		[CompilerGenerated]
		private IParseSessionController <get_SessionController>b__41_0()
		{
			return new ParseSessionController(this.CommandRunner, this.Decoder);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000BAC9 File Offset: 0x00009CC9
		[CompilerGenerated]
		private IParseUserController <get_UserController>b__44_0()
		{
			return new ParseUserController(this.CommandRunner, this.Decoder);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BADC File Offset: 0x00009CDC
		[CompilerGenerated]
		private ParseCurrentUserController <get_CurrentUserController>b__47_0()
		{
			return new ParseCurrentUserController(this.CacheController, this.ClassController, this.Decoder);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000BAF5 File Offset: 0x00009CF5
		[CompilerGenerated]
		private IParseAnalyticsController <get_AnalyticsController>b__50_0()
		{
			return new ParseAnalyticsController(this.CommandRunner);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000BB02 File Offset: 0x00009D02
		[CompilerGenerated]
		private IParseInstallationCoder <get_InstallationCoder>b__53_0()
		{
			return new ParseInstallationCoder(this.Decoder, this.ClassController);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000BB15 File Offset: 0x00009D15
		[CompilerGenerated]
		private IParsePushChannelsController <get_PushChannelsController>b__56_0()
		{
			return new ParsePushChannelsController(this.CurrentInstallationController);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000BB22 File Offset: 0x00009D22
		[CompilerGenerated]
		private IParsePushController <get_PushController>b__59_0()
		{
			return new ParsePushController(this.CommandRunner, this.CurrentUserController);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000BB35 File Offset: 0x00009D35
		[CompilerGenerated]
		private IParseCurrentInstallationController <get_CurrentInstallationController>b__62_0()
		{
			return new ParseCurrentInstallationController(this.InstallationController, this.CacheController, this.InstallationCoder, this.ClassController);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000BB54 File Offset: 0x00009D54
		[CompilerGenerated]
		private IParseDataDecoder <get_Decoder>b__65_0()
		{
			return new ParseDataDecoder(this.ClassController);
		}

		// Token: 0x0400009C RID: 156
		[CompilerGenerated]
		private readonly LateInitializer <LateInitializer>k__BackingField;

		// Token: 0x0400009D RID: 157
		[CompilerGenerated]
		private IServiceHubCloner <Cloner>k__BackingField;

		// Token: 0x0400009E RID: 158
		[CompilerGenerated]
		private IServerConnectionData <ServerConnectionData>k__BackingField;

		// Token: 0x02000115 RID: 277
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600073A RID: 1850 RVA: 0x000161DE File Offset: 0x000143DE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600073B RID: 1851 RVA: 0x000161EA File Offset: 0x000143EA
			public <>c()
			{
			}

			// Token: 0x0600073C RID: 1852 RVA: 0x000161F2 File Offset: 0x000143F2
			internal IMetadataController <get_MetadataController>b__8_0()
			{
				return new MetadataController
				{
					EnvironmentData = EnvironmentData.Inferred,
					HostManifestData = HostManifestData.Inferred
				};
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x0001620F File Offset: 0x0001440F
			internal IWebClient <get_WebClient>b__11_0()
			{
				return new UniversalWebClient();
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x00016216 File Offset: 0x00014416
			internal ICacheController <get_CacheController>b__14_0()
			{
				return new CacheController();
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x0001621D File Offset: 0x0001441D
			internal IParseObjectClassController <get_ClassController>b__17_0()
			{
				return new ParseObjectClassController();
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x00016224 File Offset: 0x00014424
			internal IParseInstallationDataFinalizer <get_InstallationDataFinalizer>b__68_0()
			{
				return new ParseInstallationDataFinalizer();
			}

			// Token: 0x04000250 RID: 592
			public static readonly LateInitializedMutableServiceHub.<>c <>9 = new LateInitializedMutableServiceHub.<>c();

			// Token: 0x04000251 RID: 593
			public static Func<IMetadataController> <>9__8_0;

			// Token: 0x04000252 RID: 594
			public static Func<IWebClient> <>9__11_0;

			// Token: 0x04000253 RID: 595
			public static Func<ICacheController> <>9__14_0;

			// Token: 0x04000254 RID: 596
			public static Func<IParseObjectClassController> <>9__17_0;

			// Token: 0x04000255 RID: 597
			public static Func<IParseInstallationDataFinalizer> <>9__68_0;
		}
	}
}
