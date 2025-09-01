using System;

namespace System
{
	// Token: 0x02000256 RID: 598
	internal interface TypeName : IEquatable<TypeName>
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001BAB RID: 7083
		string DisplayName { get; }

		// Token: 0x06001BAC RID: 7084
		TypeName NestedName(TypeIdentifier innerName);
	}
}
