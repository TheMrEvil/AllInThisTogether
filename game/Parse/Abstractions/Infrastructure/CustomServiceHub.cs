using System;
using System.Runtime.CompilerServices;
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

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x0200008B RID: 139
	public abstract class CustomServiceHub : ICustomServiceHub, IServiceHub
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00012B3D File Offset: 0x00010D3D
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00012B45 File Offset: 0x00010D45
		public virtual IServiceHub Services
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

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00012B4E File Offset: 0x00010D4E
		public virtual IServiceHubCloner Cloner
		{
			get
			{
				return this.Services.Cloner;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00012B5B File Offset: 0x00010D5B
		public virtual IMetadataController MetadataController
		{
			get
			{
				return this.Services.MetadataController;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00012B68 File Offset: 0x00010D68
		public virtual IWebClient WebClient
		{
			get
			{
				return this.Services.WebClient;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00012B75 File Offset: 0x00010D75
		public virtual ICacheController CacheController
		{
			get
			{
				return this.Services.CacheController;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00012B82 File Offset: 0x00010D82
		public virtual IParseObjectClassController ClassController
		{
			get
			{
				return this.Services.ClassController;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00012B8F File Offset: 0x00010D8F
		public virtual IParseInstallationController InstallationController
		{
			get
			{
				return this.Services.InstallationController;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00012B9C File Offset: 0x00010D9C
		public virtual IParseCommandRunner CommandRunner
		{
			get
			{
				return this.Services.CommandRunner;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00012BA9 File Offset: 0x00010DA9
		public virtual IParseCloudCodeController CloudCodeController
		{
			get
			{
				return this.Services.CloudCodeController;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00012BB6 File Offset: 0x00010DB6
		public virtual IParseConfigurationController ConfigurationController
		{
			get
			{
				return this.Services.ConfigurationController;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00012BC3 File Offset: 0x00010DC3
		public virtual IParseFileController FileController
		{
			get
			{
				return this.Services.FileController;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00012BD0 File Offset: 0x00010DD0
		public virtual IParseObjectController ObjectController
		{
			get
			{
				return this.Services.ObjectController;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00012BDD File Offset: 0x00010DDD
		public virtual IParseQueryController QueryController
		{
			get
			{
				return this.Services.QueryController;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00012BEA File Offset: 0x00010DEA
		public virtual IParseSessionController SessionController
		{
			get
			{
				return this.Services.SessionController;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00012BF7 File Offset: 0x00010DF7
		public virtual IParseUserController UserController
		{
			get
			{
				return this.Services.UserController;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00012C04 File Offset: 0x00010E04
		public virtual IParseCurrentUserController CurrentUserController
		{
			get
			{
				return this.Services.CurrentUserController;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00012C11 File Offset: 0x00010E11
		public virtual IParseAnalyticsController AnalyticsController
		{
			get
			{
				return this.Services.AnalyticsController;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00012C1E File Offset: 0x00010E1E
		public virtual IParseInstallationCoder InstallationCoder
		{
			get
			{
				return this.Services.InstallationCoder;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00012C2B File Offset: 0x00010E2B
		public virtual IParsePushChannelsController PushChannelsController
		{
			get
			{
				return this.Services.PushChannelsController;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00012C38 File Offset: 0x00010E38
		public virtual IParsePushController PushController
		{
			get
			{
				return this.Services.PushController;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00012C45 File Offset: 0x00010E45
		public virtual IParseCurrentInstallationController CurrentInstallationController
		{
			get
			{
				return this.Services.CurrentInstallationController;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00012C52 File Offset: 0x00010E52
		public virtual IServerConnectionData ServerConnectionData
		{
			get
			{
				return this.Services.ServerConnectionData;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00012C5F File Offset: 0x00010E5F
		public virtual IParseDataDecoder Decoder
		{
			get
			{
				return this.Services.Decoder;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00012C6C File Offset: 0x00010E6C
		public virtual IParseInstallationDataFinalizer InstallationDataFinalizer
		{
			get
			{
				return this.Services.InstallationDataFinalizer;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00012C79 File Offset: 0x00010E79
		protected CustomServiceHub()
		{
		}

		// Token: 0x04000108 RID: 264
		[CompilerGenerated]
		private IServiceHub <Services>k__BackingField;
	}
}
