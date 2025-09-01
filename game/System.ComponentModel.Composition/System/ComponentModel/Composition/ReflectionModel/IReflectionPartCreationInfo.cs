using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000066 RID: 102
	internal interface IReflectionPartCreationInfo : ICompositionElement
	{
		// Token: 0x06000282 RID: 642
		Type GetPartType();

		// Token: 0x06000283 RID: 643
		Lazy<Type> GetLazyPartType();

		// Token: 0x06000284 RID: 644
		ConstructorInfo GetConstructor();

		// Token: 0x06000285 RID: 645
		IDictionary<string, object> GetMetadata();

		// Token: 0x06000286 RID: 646
		IEnumerable<ExportDefinition> GetExports();

		// Token: 0x06000287 RID: 647
		IEnumerable<ImportDefinition> GetImports();

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000288 RID: 648
		bool IsDisposalRequired { get; }
	}
}
