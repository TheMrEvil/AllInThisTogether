using System;
using System.Reflection.Emit;
using Mono.CompilerServices.SymbolWriter;

namespace Mono.CSharp
{
	// Token: 0x02000232 RID: 562
	public class StateMachineMethod : Method
	{
		// Token: 0x06001C5D RID: 7261 RVA: 0x00089C0F File Offset: 0x00087E0F
		public StateMachineMethod(StateMachine host, StateMachineInitializer expr, FullNamedExpression returnType, Modifiers mod, MemberName name, Block.Flags blockFlags) : base(host, returnType, mod | Modifiers.COMPILER_GENERATED, name, ParametersCompiled.EmptyReadOnlyParameters, null)
		{
			this.expr = expr;
			base.Block = new ToplevelBlock(host.Compiler, ParametersCompiled.EmptyReadOnlyParameters, Location.Null, blockFlags);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00089C50 File Offset: 0x00087E50
		public override EmitContext CreateEmitContext(ILGenerator ig, SourceMethodBuilder sourceMethod)
		{
			EmitContext emitContext = new EmitContext(this, ig, base.MemberType, sourceMethod);
			emitContext.CurrentAnonymousMethod = this.expr;
			if (this.expr is AsyncInitializer)
			{
				emitContext.With(BuilderContext.Options.AsyncBody, true);
			}
			return emitContext;
		}

		// Token: 0x04000A7B RID: 2683
		private readonly StateMachineInitializer expr;
	}
}
