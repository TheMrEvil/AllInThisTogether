using System;

namespace Mono.CSharp
{
	// Token: 0x02000249 RID: 585
	public interface IGenericMethodDefinition : IMethodDefinition, IMemberDefinition
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001D0C RID: 7436
		TypeParameterSpec[] TypeParameters { get; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001D0D RID: 7437
		int TypeParametersCount { get; }
	}
}
