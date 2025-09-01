using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	// Token: 0x020000BD RID: 189
	internal interface IGenericNameProvider
	{
		// Token: 0x06000B14 RID: 2836
		int GetParameterCount();

		// Token: 0x06000B15 RID: 2837
		IList<int> GetNestedParameterCounts();

		// Token: 0x06000B16 RID: 2838
		string GetParameterName(int paramIndex);

		// Token: 0x06000B17 RID: 2839
		string GetNamespaces();

		// Token: 0x06000B18 RID: 2840
		string GetGenericTypeName();

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000B19 RID: 2841
		bool ParametersFromBuiltInNamespaces { get; }
	}
}
