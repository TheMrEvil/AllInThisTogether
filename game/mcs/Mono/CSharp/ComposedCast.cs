using System;
using Mono.CSharp.Nullable;

namespace Mono.CSharp
{
	// Token: 0x02000207 RID: 519
	public class ComposedCast : TypeExpr
	{
		// Token: 0x06001AD7 RID: 6871 RVA: 0x000826A0 File Offset: 0x000808A0
		public ComposedCast(FullNamedExpression left, ComposedTypeSpecifier spec)
		{
			if (spec == null)
			{
				throw new ArgumentNullException("spec");
			}
			this.left = left;
			this.spec = spec;
			this.loc = left.Location;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000826D0 File Offset: 0x000808D0
		public override TypeSpec ResolveAsType(IMemberContext ec, bool allowUnboundTypeArguments)
		{
			this.type = this.left.ResolveAsType(ec, false);
			if (this.type == null)
			{
				return null;
			}
			this.eclass = ExprClass.Type;
			ComposedTypeSpecifier next = this.spec;
			if (next.IsNullable)
			{
				this.type = new NullableType(this.type, this.loc).ResolveAsType(ec, false);
				if (this.type == null)
				{
					return null;
				}
				next = next.Next;
			}
			else if (next.IsPointer)
			{
				if (!TypeManager.VerifyUnmanaged(ec.Module, this.type, this.loc))
				{
					return null;
				}
				if (!ec.IsUnsafe)
				{
					Expression.UnsafeError(ec.Module.Compiler.Report, this.loc);
				}
				do
				{
					this.type = PointerContainer.MakeType(ec.Module, this.type);
					next = next.Next;
				}
				while (next != null && next.IsPointer);
			}
			if (next != null && next.Dimension > 0)
			{
				if (this.type.IsSpecialRuntimeType)
				{
					ec.Module.Compiler.Report.Error(611, this.loc, "Array elements cannot be of type `{0}'", this.type.GetSignatureForError());
				}
				else if (this.type.IsStatic)
				{
					ec.Module.Compiler.Report.SymbolRelatedToPreviousError(this.type);
					ec.Module.Compiler.Report.Error(719, this.loc, "Array elements cannot be of static type `{0}'", this.type.GetSignatureForError());
				}
				else
				{
					this.MakeArray(ec.Module, next);
				}
			}
			return this.type;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0008286E File Offset: 0x00080A6E
		private void MakeArray(ModuleContainer module, ComposedTypeSpecifier spec)
		{
			if (spec.Next != null)
			{
				this.MakeArray(module, spec.Next);
			}
			this.type = ArrayContainer.MakeType(module, this.type, spec.Dimension);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0008289D File Offset: 0x00080A9D
		public override string GetSignatureForError()
		{
			return this.left.GetSignatureForError() + this.spec.GetSignatureForError();
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000828BA File Offset: 0x00080ABA
		public override object Accept(StructuralVisitor visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x04000A07 RID: 2567
		private FullNamedExpression left;

		// Token: 0x04000A08 RID: 2568
		private ComposedTypeSpecifier spec;
	}
}
