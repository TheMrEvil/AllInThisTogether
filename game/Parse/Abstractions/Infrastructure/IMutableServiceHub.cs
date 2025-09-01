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
	// Token: 0x02000095 RID: 149
	public interface IMutableServiceHub : IServiceHub
	{
		// Token: 0x170001B7 RID: 439
		// (set) Token: 0x06000579 RID: 1401
		IServerConnectionData ServerConnectionData { set; }

		// Token: 0x170001B8 RID: 440
		// (set) Token: 0x0600057A RID: 1402
		IMetadataController MetadataController { set; }

		// Token: 0x170001B9 RID: 441
		// (set) Token: 0x0600057B RID: 1403
		IServiceHubCloner Cloner { set; }

		// Token: 0x170001BA RID: 442
		// (set) Token: 0x0600057C RID: 1404
		IWebClient WebClient { set; }

		// Token: 0x170001BB RID: 443
		// (set) Token: 0x0600057D RID: 1405
		ICacheController CacheController { set; }

		// Token: 0x170001BC RID: 444
		// (set) Token: 0x0600057E RID: 1406
		IParseObjectClassController ClassController { set; }

		// Token: 0x170001BD RID: 445
		// (set) Token: 0x0600057F RID: 1407
		IParseDataDecoder Decoder { set; }

		// Token: 0x170001BE RID: 446
		// (set) Token: 0x06000580 RID: 1408
		IParseInstallationController InstallationController { set; }

		// Token: 0x170001BF RID: 447
		// (set) Token: 0x06000581 RID: 1409
		IParseCommandRunner CommandRunner { set; }

		// Token: 0x170001C0 RID: 448
		// (set) Token: 0x06000582 RID: 1410
		IParseCloudCodeController CloudCodeController { set; }

		// Token: 0x170001C1 RID: 449
		// (set) Token: 0x06000583 RID: 1411
		IParseConfigurationController ConfigurationController { set; }

		// Token: 0x170001C2 RID: 450
		// (set) Token: 0x06000584 RID: 1412
		IParseFileController FileController { set; }

		// Token: 0x170001C3 RID: 451
		// (set) Token: 0x06000585 RID: 1413
		IParseObjectController ObjectController { set; }

		// Token: 0x170001C4 RID: 452
		// (set) Token: 0x06000586 RID: 1414
		IParseQueryController QueryController { set; }

		// Token: 0x170001C5 RID: 453
		// (set) Token: 0x06000587 RID: 1415
		IParseSessionController SessionController { set; }

		// Token: 0x170001C6 RID: 454
		// (set) Token: 0x06000588 RID: 1416
		IParseUserController UserController { set; }

		// Token: 0x170001C7 RID: 455
		// (set) Token: 0x06000589 RID: 1417
		IParseCurrentUserController CurrentUserController { set; }

		// Token: 0x170001C8 RID: 456
		// (set) Token: 0x0600058A RID: 1418
		IParseAnalyticsController AnalyticsController { set; }

		// Token: 0x170001C9 RID: 457
		// (set) Token: 0x0600058B RID: 1419
		IParseInstallationCoder InstallationCoder { set; }

		// Token: 0x170001CA RID: 458
		// (set) Token: 0x0600058C RID: 1420
		IParsePushChannelsController PushChannelsController { set; }

		// Token: 0x170001CB RID: 459
		// (set) Token: 0x0600058D RID: 1421
		IParsePushController PushController { set; }

		// Token: 0x170001CC RID: 460
		// (set) Token: 0x0600058E RID: 1422
		IParseCurrentInstallationController CurrentInstallationController { set; }

		// Token: 0x170001CD RID: 461
		// (set) Token: 0x0600058F RID: 1423
		IParseInstallationDataFinalizer InstallationDataFinalizer { set; }
	}
}
