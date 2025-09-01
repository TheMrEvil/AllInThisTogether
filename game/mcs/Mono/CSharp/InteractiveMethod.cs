using System;

namespace Mono.CSharp
{
	// Token: 0x02000173 RID: 371
	internal class InteractiveMethod : Method
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x0004A413 File Offset: 0x00048613
		public InteractiveMethod(TypeDefinition parent, FullNamedExpression returnType, Modifiers mod, ParametersCompiled parameters) : base(parent, returnType, mod, new MemberName("Host"), parameters, null)
		{
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x0004A42C File Offset: 0x0004862C
		public void ChangeToAsync()
		{
			base.ModFlags |= Modifiers.ASYNC;
			base.ModFlags &= ~Modifiers.UNSAFE;
			this.type_expr = new TypeExpression(this.Module.PredefinedTypes.Task.TypeSpec, base.Location);
			this.parameters = ParametersCompiled.EmptyReadOnlyParameters;
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x0004A48E File Offset: 0x0004868E
		public override string GetSignatureForError()
		{
			return "InteractiveHost";
		}
	}
}
