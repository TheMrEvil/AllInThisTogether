using System;

namespace Mono.CSharp
{
	// Token: 0x020001C7 RID: 455
	internal class VarExpr : SimpleName
	{
		// Token: 0x06001804 RID: 6148 RVA: 0x00073C24 File Offset: 0x00071E24
		public VarExpr(Location loc) : base("var", loc)
		{
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00073C34 File Offset: 0x00071E34
		public bool InferType(ResolveContext ec, Expression right_side)
		{
			if (this.type != null)
			{
				throw new InternalErrorException("An implicitly typed local variable could not be redefined");
			}
			this.type = right_side.Type;
			if (this.type == InternalType.NullLiteral || this.type.Kind == MemberKind.Void || this.type == InternalType.AnonymousMethod || this.type == InternalType.MethodGroup)
			{
				ec.Report.Error(815, this.loc, "An implicitly typed local variable declaration cannot be initialized with `{0}'", this.type.GetSignatureForError());
				this.type = InternalType.ErrorType;
				return false;
			}
			this.eclass = ExprClass.Variable;
			return true;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00073CD4 File Offset: 0x00071ED4
		protected override void Error_TypeOrNamespaceNotFound(IMemberContext ec)
		{
			if (ec.Module.Compiler.Settings.Version < LanguageVersion.V_3)
			{
				base.Error_TypeOrNamespaceNotFound(ec);
				return;
			}
			ec.Module.Compiler.Report.Error(825, this.loc, "The contextual keyword `var' may only appear within a local variable declaration");
		}
	}
}
