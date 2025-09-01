using System;
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
	// Token: 0x02000098 RID: 152
	public interface IServiceHub
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600059B RID: 1435
		IServerConnectionData ServerConnectionData { get; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600059C RID: 1436
		IMetadataController MetadataController { get; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600059D RID: 1437
		IServiceHubCloner Cloner { get; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600059E RID: 1438
		IWebClient WebClient { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600059F RID: 1439
		ICacheController CacheController { get; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005A0 RID: 1440
		IParseObjectClassController ClassController { get; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005A1 RID: 1441
		IParseDataDecoder Decoder { get; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005A2 RID: 1442
		IParseInstallationController InstallationController { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060005A3 RID: 1443
		IParseCommandRunner CommandRunner { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060005A4 RID: 1444
		IParseCloudCodeController CloudCodeController { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060005A5 RID: 1445
		IParseConfigurationController ConfigurationController { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005A6 RID: 1446
		IParseFileController FileController { get; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005A7 RID: 1447
		IParseObjectController ObjectController { get; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060005A8 RID: 1448
		IParseQueryController QueryController { get; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060005A9 RID: 1449
		IParseSessionController SessionController { get; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060005AA RID: 1450
		IParseUserController UserController { get; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060005AB RID: 1451
		IParseCurrentUserController CurrentUserController { get; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060005AC RID: 1452
		IParseAnalyticsController AnalyticsController { get; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060005AD RID: 1453
		IParseInstallationCoder InstallationCoder { get; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060005AE RID: 1454
		IParsePushChannelsController PushChannelsController { get; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060005AF RID: 1455
		IParsePushController PushController { get; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005B0 RID: 1456
		IParseCurrentInstallationController CurrentInstallationController { get; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005B1 RID: 1457
		IParseInstallationDataFinalizer InstallationDataFinalizer { get; }
	}
}
