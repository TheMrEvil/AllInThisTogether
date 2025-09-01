using System;

namespace System
{
	// Token: 0x02000257 RID: 599
	internal interface TypeIdentifier : TypeName, IEquatable<TypeName>
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001BAD RID: 7085
		string InternalName { get; }
	}
}
