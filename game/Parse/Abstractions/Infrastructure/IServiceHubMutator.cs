using System;

namespace Parse.Abstractions.Infrastructure
{
	// Token: 0x0200009B RID: 155
	public interface IServiceHubMutator
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005B4 RID: 1460
		bool Valid { get; }

		// Token: 0x060005B5 RID: 1461
		void Mutate(ref IMutableServiceHub target, in IServiceHub composedHub);
	}
}
