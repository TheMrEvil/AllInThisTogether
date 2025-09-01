using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x02000190 RID: 400
	public interface IMethodDefinition : IMemberDefinition
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060015B4 RID: 5556
		MethodBase Metadata { get; }
	}
}
