using System;

namespace Mono.CSharp
{
	// Token: 0x02000156 RID: 342
	public interface IMemberContext : IModuleContext
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001108 RID: 4360
		TypeSpec CurrentType { get; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001109 RID: 4361
		TypeParameters CurrentTypeParameters { get; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600110A RID: 4362
		MemberCore CurrentMemberDefinition { get; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600110B RID: 4363
		bool IsObsolete { get; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x0600110C RID: 4364
		bool IsUnsafe { get; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x0600110D RID: 4365
		bool IsStatic { get; }

		// Token: 0x0600110E RID: 4366
		string GetSignatureForError();

		// Token: 0x0600110F RID: 4367
		ExtensionMethodCandidates LookupExtensionMethod(string name, int arity);

		// Token: 0x06001110 RID: 4368
		FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc);

		// Token: 0x06001111 RID: 4369
		FullNamedExpression LookupNamespaceAlias(string name);
	}
}
