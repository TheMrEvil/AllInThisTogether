using System;
using System.Reflection;
using System.Reflection.Emit;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x02000252 RID: 594
	public interface IMethodData : IMemberContext, IModuleContext
	{
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001D75 RID: 7541
		CallingConventions CallingConventions { get; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001D76 RID: 7542
		Location Location { get; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001D77 RID: 7543
		MemberName MethodName { get; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001D78 RID: 7544
		TypeSpec ReturnType { get; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001D79 RID: 7545
		ParametersCompiled ParameterInfo { get; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001D7A RID: 7546
		MethodSpec Spec { get; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001D7B RID: 7547
		bool IsAccessor { get; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001D7C RID: 7548
		Attributes OptAttributes { get; }

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001D7D RID: 7549
		// (set) Token: 0x06001D7E RID: 7550
		ToplevelBlock Block { get; set; }

		// Token: 0x06001D7F RID: 7551
		EmitContext CreateEmitContext(ILGenerator ig, SourceMethodBuilder sourceMethod);
	}
}
