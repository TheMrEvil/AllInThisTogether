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

namespace Parse.Infrastructure
{
	// Token: 0x02000049 RID: 73
	public class OrchestrationServiceHub : IServiceHub
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000C18F File Offset: 0x0000A38F
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000C197 File Offset: 0x0000A397
		public IServiceHub Default
		{
			[CompilerGenerated]
			get
			{
				return this.<Default>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Default>k__BackingField = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600039E RID: 926 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		public IServiceHub Custom
		{
			[CompilerGenerated]
			get
			{
				return this.<Custom>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Custom>k__BackingField = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000C1B1 File Offset: 0x0000A3B1
		public IServiceHubCloner Cloner
		{
			get
			{
				return this.Custom.Cloner ?? this.Default.Cloner;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public IMetadataController MetadataController
		{
			get
			{
				return this.Custom.MetadataController ?? this.Default.MetadataController;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000C1E9 File Offset: 0x0000A3E9
		public IWebClient WebClient
		{
			get
			{
				return this.Custom.WebClient ?? this.Default.WebClient;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000C205 File Offset: 0x0000A405
		public ICacheController CacheController
		{
			get
			{
				return this.Custom.CacheController ?? this.Default.CacheController;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000C221 File Offset: 0x0000A421
		public IParseObjectClassController ClassController
		{
			get
			{
				return this.Custom.ClassController ?? this.Default.ClassController;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000C23D File Offset: 0x0000A43D
		public IParseInstallationController InstallationController
		{
			get
			{
				return this.Custom.InstallationController ?? this.Default.InstallationController;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000C259 File Offset: 0x0000A459
		public IParseCommandRunner CommandRunner
		{
			get
			{
				return this.Custom.CommandRunner ?? this.Default.CommandRunner;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000C275 File Offset: 0x0000A475
		public IParseCloudCodeController CloudCodeController
		{
			get
			{
				return this.Custom.CloudCodeController ?? this.Default.CloudCodeController;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000C291 File Offset: 0x0000A491
		public IParseConfigurationController ConfigurationController
		{
			get
			{
				return this.Custom.ConfigurationController ?? this.Default.ConfigurationController;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		public IParseFileController FileController
		{
			get
			{
				return this.Custom.FileController ?? this.Default.FileController;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000C2C9 File Offset: 0x0000A4C9
		public IParseObjectController ObjectController
		{
			get
			{
				return this.Custom.ObjectController ?? this.Default.ObjectController;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000C2E5 File Offset: 0x0000A4E5
		public IParseQueryController QueryController
		{
			get
			{
				return this.Custom.QueryController ?? this.Default.QueryController;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000C301 File Offset: 0x0000A501
		public IParseSessionController SessionController
		{
			get
			{
				return this.Custom.SessionController ?? this.Default.SessionController;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000C31D File Offset: 0x0000A51D
		public IParseUserController UserController
		{
			get
			{
				return this.Custom.UserController ?? this.Default.UserController;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000C339 File Offset: 0x0000A539
		public IParseCurrentUserController CurrentUserController
		{
			get
			{
				return this.Custom.CurrentUserController ?? this.Default.CurrentUserController;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000C355 File Offset: 0x0000A555
		public IParseAnalyticsController AnalyticsController
		{
			get
			{
				return this.Custom.AnalyticsController ?? this.Default.AnalyticsController;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000C371 File Offset: 0x0000A571
		public IParseInstallationCoder InstallationCoder
		{
			get
			{
				return this.Custom.InstallationCoder ?? this.Default.InstallationCoder;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000C38D File Offset: 0x0000A58D
		public IParsePushChannelsController PushChannelsController
		{
			get
			{
				return this.Custom.PushChannelsController ?? this.Default.PushChannelsController;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000C3A9 File Offset: 0x0000A5A9
		public IParsePushController PushController
		{
			get
			{
				return this.Custom.PushController ?? this.Default.PushController;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000C3C5 File Offset: 0x0000A5C5
		public IParseCurrentInstallationController CurrentInstallationController
		{
			get
			{
				return this.Custom.CurrentInstallationController ?? this.Default.CurrentInstallationController;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000C3E1 File Offset: 0x0000A5E1
		public IServerConnectionData ServerConnectionData
		{
			get
			{
				return this.Custom.ServerConnectionData ?? this.Default.ServerConnectionData;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000C3FD File Offset: 0x0000A5FD
		public IParseDataDecoder Decoder
		{
			get
			{
				return this.Custom.Decoder ?? this.Default.Decoder;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000C419 File Offset: 0x0000A619
		public IParseInstallationDataFinalizer InstallationDataFinalizer
		{
			get
			{
				return this.Custom.InstallationDataFinalizer ?? this.Default.InstallationDataFinalizer;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000C435 File Offset: 0x0000A635
		public OrchestrationServiceHub()
		{
		}

		// Token: 0x040000BA RID: 186
		[CompilerGenerated]
		private IServiceHub <Default>k__BackingField;

		// Token: 0x040000BB RID: 187
		[CompilerGenerated]
		private IServiceHub <Custom>k__BackingField;
	}
}
