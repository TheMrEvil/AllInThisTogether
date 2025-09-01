using System;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x02000099 RID: 153
	public interface IServiceHubCloner
	{
		// Token: 0x060005B2 RID: 1458
		IServiceHub BuildHub(in IServiceHub reference, IServiceHubComposer composer, params IServiceHubMutator[] requestedMutators);
	}
}
