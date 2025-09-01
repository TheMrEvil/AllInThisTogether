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
	// Token: 0x02000048 RID: 72
	public class MutableServiceHub : IMutableServiceHub, IServiceHub
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000BD05 File Offset: 0x00009F05
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000BD0D File Offset: 0x00009F0D
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000BD16 File Offset: 0x00009F16
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000BD1E File Offset: 0x00009F1E
		public IMetadataController MetadataController
		{
			[CompilerGenerated]
			get
			{
				return this.<MetadataController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MetadataController>k__BackingField = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000BD27 File Offset: 0x00009F27
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000BD2F File Offset: 0x00009F2F
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000BD38 File Offset: 0x00009F38
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000BD40 File Offset: 0x00009F40
		public IWebClient WebClient
		{
			[CompilerGenerated]
			get
			{
				return this.<WebClient>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WebClient>k__BackingField = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000BD49 File Offset: 0x00009F49
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000BD51 File Offset: 0x00009F51
		public ICacheController CacheController
		{
			[CompilerGenerated]
			get
			{
				return this.<CacheController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CacheController>k__BackingField = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000BD5A File Offset: 0x00009F5A
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000BD62 File Offset: 0x00009F62
		public IParseObjectClassController ClassController
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ClassController>k__BackingField = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000BD6B File Offset: 0x00009F6B
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000BD73 File Offset: 0x00009F73
		public IParseDataDecoder Decoder
		{
			[CompilerGenerated]
			get
			{
				return this.<Decoder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Decoder>k__BackingField = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000BD7C File Offset: 0x00009F7C
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000BD84 File Offset: 0x00009F84
		public IParseInstallationController InstallationController
		{
			[CompilerGenerated]
			get
			{
				return this.<InstallationController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InstallationController>k__BackingField = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000BD8D File Offset: 0x00009F8D
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000BD95 File Offset: 0x00009F95
		public IParseCommandRunner CommandRunner
		{
			[CompilerGenerated]
			get
			{
				return this.<CommandRunner>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CommandRunner>k__BackingField = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000BD9E File Offset: 0x00009F9E
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000BDA6 File Offset: 0x00009FA6
		public IParseCloudCodeController CloudCodeController
		{
			[CompilerGenerated]
			get
			{
				return this.<CloudCodeController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CloudCodeController>k__BackingField = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000BDAF File Offset: 0x00009FAF
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000BDB7 File Offset: 0x00009FB7
		public IParseConfigurationController ConfigurationController
		{
			[CompilerGenerated]
			get
			{
				return this.<ConfigurationController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ConfigurationController>k__BackingField = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		public IParseFileController FileController
		{
			[CompilerGenerated]
			get
			{
				return this.<FileController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<FileController>k__BackingField = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		public IParseObjectController ObjectController
		{
			[CompilerGenerated]
			get
			{
				return this.<ObjectController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ObjectController>k__BackingField = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000BDE2 File Offset: 0x00009FE2
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000BDEA File Offset: 0x00009FEA
		public IParseQueryController QueryController
		{
			[CompilerGenerated]
			get
			{
				return this.<QueryController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<QueryController>k__BackingField = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000BDF3 File Offset: 0x00009FF3
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000BDFB File Offset: 0x00009FFB
		public IParseSessionController SessionController
		{
			[CompilerGenerated]
			get
			{
				return this.<SessionController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<SessionController>k__BackingField = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000BE04 File Offset: 0x0000A004
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000BE0C File Offset: 0x0000A00C
		public IParseUserController UserController
		{
			[CompilerGenerated]
			get
			{
				return this.<UserController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UserController>k__BackingField = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000BE15 File Offset: 0x0000A015
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000BE1D File Offset: 0x0000A01D
		public IParseCurrentUserController CurrentUserController
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentUserController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentUserController>k__BackingField = value;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000BE26 File Offset: 0x0000A026
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000BE2E File Offset: 0x0000A02E
		public IParseAnalyticsController AnalyticsController
		{
			[CompilerGenerated]
			get
			{
				return this.<AnalyticsController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AnalyticsController>k__BackingField = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000BE37 File Offset: 0x0000A037
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000BE3F File Offset: 0x0000A03F
		public IParseInstallationCoder InstallationCoder
		{
			[CompilerGenerated]
			get
			{
				return this.<InstallationCoder>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InstallationCoder>k__BackingField = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000BE48 File Offset: 0x0000A048
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000BE50 File Offset: 0x0000A050
		public IParsePushChannelsController PushChannelsController
		{
			[CompilerGenerated]
			get
			{
				return this.<PushChannelsController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PushChannelsController>k__BackingField = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000BE59 File Offset: 0x0000A059
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000BE61 File Offset: 0x0000A061
		public IParsePushController PushController
		{
			[CompilerGenerated]
			get
			{
				return this.<PushController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PushController>k__BackingField = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000BE6A File Offset: 0x0000A06A
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000BE72 File Offset: 0x0000A072
		public IParseCurrentInstallationController CurrentInstallationController
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentInstallationController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<CurrentInstallationController>k__BackingField = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000BE7B File Offset: 0x0000A07B
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000BE83 File Offset: 0x0000A083
		public IParseInstallationDataFinalizer InstallationDataFinalizer
		{
			[CompilerGenerated]
			get
			{
				return this.<InstallationDataFinalizer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<InstallationDataFinalizer>k__BackingField = value;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000BE8C File Offset: 0x0000A08C
		public MutableServiceHub SetDefaults(IServerConnectionData connectionData = null)
		{
			if (this.ServerConnectionData == null)
			{
				this.ServerConnectionData = connectionData;
			}
			if (this.MetadataController == null)
			{
				this.MetadataController = new MetadataController
				{
					EnvironmentData = EnvironmentData.Inferred,
					HostManifestData = HostManifestData.Inferred
				};
			}
			if (this.Cloner == null)
			{
				this.Cloner = new ConcurrentUserServiceHubCloner();
			}
			if (this.WebClient == null)
			{
				this.WebClient = new UniversalWebClient();
			}
			if (this.CacheController == null)
			{
				this.CacheController = new CacheController();
			}
			if (this.ClassController == null)
			{
				this.ClassController = new ParseObjectClassController();
			}
			if (this.Decoder == null)
			{
				this.Decoder = new ParseDataDecoder(this.ClassController);
			}
			if (this.InstallationController == null)
			{
				this.InstallationController = new ParseInstallationController(this.CacheController);
			}
			if (this.CommandRunner == null)
			{
				this.CommandRunner = new ParseCommandRunner(this.WebClient, this.InstallationController, this.MetadataController, this.ServerConnectionData, new Lazy<IParseUserController>(() => this.UserController));
			}
			if (this.CloudCodeController == null)
			{
				this.CloudCodeController = new ParseCloudCodeController(this.CommandRunner, this.Decoder);
			}
			if (this.ConfigurationController == null)
			{
				this.ConfigurationController = new ParseConfigurationController(this.CommandRunner, this.CacheController, this.Decoder);
			}
			if (this.FileController == null)
			{
				this.FileController = new ParseFileController(this.CommandRunner);
			}
			if (this.ObjectController == null)
			{
				this.ObjectController = new ParseObjectController(this.CommandRunner, this.Decoder, this.ServerConnectionData);
			}
			if (this.QueryController == null)
			{
				this.QueryController = new ParseQueryController(this.CommandRunner, this.Decoder);
			}
			if (this.SessionController == null)
			{
				this.SessionController = new ParseSessionController(this.CommandRunner, this.Decoder);
			}
			if (this.UserController == null)
			{
				this.UserController = new ParseUserController(this.CommandRunner, this.Decoder);
			}
			if (this.CurrentUserController == null)
			{
				this.CurrentUserController = new ParseCurrentUserController(this.CacheController, this.ClassController, this.Decoder);
			}
			if (this.AnalyticsController == null)
			{
				this.AnalyticsController = new ParseAnalyticsController(this.CommandRunner);
			}
			if (this.InstallationCoder == null)
			{
				this.InstallationCoder = new ParseInstallationCoder(this.Decoder, this.ClassController);
			}
			if (this.PushController == null)
			{
				this.PushController = new ParsePushController(this.CommandRunner, this.CurrentUserController);
			}
			if (this.CurrentInstallationController == null)
			{
				this.CurrentInstallationController = new ParseCurrentInstallationController(this.InstallationController, this.CacheController, this.InstallationCoder, this.ClassController);
			}
			if (this.PushChannelsController == null)
			{
				this.PushChannelsController = new ParsePushChannelsController(this.CurrentInstallationController);
			}
			if (this.InstallationDataFinalizer == null)
			{
				this.InstallationDataFinalizer = new ParseInstallationDataFinalizer();
			}
			return this;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000C17F File Offset: 0x0000A37F
		public MutableServiceHub()
		{
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000C187 File Offset: 0x0000A387
		[CompilerGenerated]
		private IParseUserController <SetDefaults>b__92_0()
		{
			return this.UserController;
		}

		// Token: 0x040000A3 RID: 163
		[CompilerGenerated]
		private IServerConnectionData <ServerConnectionData>k__BackingField;

		// Token: 0x040000A4 RID: 164
		[CompilerGenerated]
		private IMetadataController <MetadataController>k__BackingField;

		// Token: 0x040000A5 RID: 165
		[CompilerGenerated]
		private IServiceHubCloner <Cloner>k__BackingField;

		// Token: 0x040000A6 RID: 166
		[CompilerGenerated]
		private IWebClient <WebClient>k__BackingField;

		// Token: 0x040000A7 RID: 167
		[CompilerGenerated]
		private ICacheController <CacheController>k__BackingField;

		// Token: 0x040000A8 RID: 168
		[CompilerGenerated]
		private IParseObjectClassController <ClassController>k__BackingField;

		// Token: 0x040000A9 RID: 169
		[CompilerGenerated]
		private IParseDataDecoder <Decoder>k__BackingField;

		// Token: 0x040000AA RID: 170
		[CompilerGenerated]
		private IParseInstallationController <InstallationController>k__BackingField;

		// Token: 0x040000AB RID: 171
		[CompilerGenerated]
		private IParseCommandRunner <CommandRunner>k__BackingField;

		// Token: 0x040000AC RID: 172
		[CompilerGenerated]
		private IParseCloudCodeController <CloudCodeController>k__BackingField;

		// Token: 0x040000AD RID: 173
		[CompilerGenerated]
		private IParseConfigurationController <ConfigurationController>k__BackingField;

		// Token: 0x040000AE RID: 174
		[CompilerGenerated]
		private IParseFileController <FileController>k__BackingField;

		// Token: 0x040000AF RID: 175
		[CompilerGenerated]
		private IParseObjectController <ObjectController>k__BackingField;

		// Token: 0x040000B0 RID: 176
		[CompilerGenerated]
		private IParseQueryController <QueryController>k__BackingField;

		// Token: 0x040000B1 RID: 177
		[CompilerGenerated]
		private IParseSessionController <SessionController>k__BackingField;

		// Token: 0x040000B2 RID: 178
		[CompilerGenerated]
		private IParseUserController <UserController>k__BackingField;

		// Token: 0x040000B3 RID: 179
		[CompilerGenerated]
		private IParseCurrentUserController <CurrentUserController>k__BackingField;

		// Token: 0x040000B4 RID: 180
		[CompilerGenerated]
		private IParseAnalyticsController <AnalyticsController>k__BackingField;

		// Token: 0x040000B5 RID: 181
		[CompilerGenerated]
		private IParseInstallationCoder <InstallationCoder>k__BackingField;

		// Token: 0x040000B6 RID: 182
		[CompilerGenerated]
		private IParsePushChannelsController <PushChannelsController>k__BackingField;

		// Token: 0x040000B7 RID: 183
		[CompilerGenerated]
		private IParsePushController <PushController>k__BackingField;

		// Token: 0x040000B8 RID: 184
		[CompilerGenerated]
		private IParseCurrentInstallationController <CurrentInstallationController>k__BackingField;

		// Token: 0x040000B9 RID: 185
		[CompilerGenerated]
		private IParseInstallationDataFinalizer <InstallationDataFinalizer>k__BackingField;
	}
}
