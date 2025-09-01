using System;

namespace Mono.CSharp
{
	// Token: 0x020002E1 RID: 737
	public interface ITypeDefinition : IMemberDefinition
	{
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06002318 RID: 8984
		IAssemblyDefinition DeclaringAssembly { get; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06002319 RID: 8985
		string Namespace { get; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x0600231A RID: 8986
		bool IsPartial { get; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x0600231B RID: 8987
		bool IsComImport { get; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x0600231C RID: 8988
		bool IsTypeForwarder { get; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x0600231D RID: 8989
		bool IsCyclicTypeForwarder { get; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600231E RID: 8990
		int TypeParametersCount { get; }

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600231F RID: 8991
		TypeParameterSpec[] TypeParameters { get; }

		// Token: 0x06002320 RID: 8992
		TypeSpec GetAttributeCoClass();

		// Token: 0x06002321 RID: 8993
		string GetAttributeDefaultMember();

		// Token: 0x06002322 RID: 8994
		AttributeUsageAttribute GetAttributeUsage(PredefinedAttribute pa);

		// Token: 0x06002323 RID: 8995
		bool IsInternalAsPublic(IAssemblyDefinition assembly);

		// Token: 0x06002324 RID: 8996
		void LoadMembers(TypeSpec declaringType, bool onlyTypes, ref MemberCache cache);
	}
}
