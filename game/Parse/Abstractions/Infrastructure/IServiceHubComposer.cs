using System;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x0200009A RID: 154
	public interface IServiceHubComposer
	{
		// Token: 0x060005B3 RID: 1459
		IServiceHub BuildHub(IMutableServiceHub serviceHub = null, IServiceHub extension = null, params IServiceHubMutator[] configurators);
	}
}
