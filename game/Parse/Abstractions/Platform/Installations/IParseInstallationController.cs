using System;
using System.Threading.Tasks;

namespace Parse.Abstractions.Platform.Installations
{
	// Token: 0x02000083 RID: 131
	public interface IParseInstallationController
	{
		// Token: 0x06000532 RID: 1330
		Task SetAsync(Guid? installationId);

		// Token: 0x06000533 RID: 1331
		Task<Guid?> GetAsync();

		// Token: 0x06000534 RID: 1332
		Task ClearAsync();
	}
}
