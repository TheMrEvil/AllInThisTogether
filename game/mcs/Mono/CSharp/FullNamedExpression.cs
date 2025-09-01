using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x020001B7 RID: 439
	public abstract class FullNamedExpression : Expression
	{
		// Token: 0x06001705 RID: 5893 RVA: 0x0000AF70 File Offset: 0x00009170
		protected override void CloneTo(CloneContext clonectx, Expression target)
		{
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool ContainsEmitWithAwait()
		{
			return false;
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		public override Expression CreateExpressionTree(ResolveContext ec)
		{
			throw new NotSupportedException("ET");
		}

		// Token: 0x06001708 RID: 5896
		public abstract FullNamedExpression ResolveAsTypeOrNamespace(IMemberContext mc, bool allowUnboundTypeArguments);

		// Token: 0x06001709 RID: 5897 RVA: 0x0006DFB4 File Offset: 0x0006C1B4
		public override TypeSpec ResolveAsType(IMemberContext mc, bool allowUnboundTypeArguments = false)
		{
			FullNamedExpression fullNamedExpression = this.ResolveAsTypeOrNamespace(mc, allowUnboundTypeArguments);
			if (fullNamedExpression == null)
			{
				return null;
			}
			TypeExpr typeExpr = fullNamedExpression as TypeExpr;
			if (typeExpr == null)
			{
				Expression.Error_UnexpectedKind(mc, fullNamedExpression, "type", fullNamedExpression.ExprClassName, this.loc);
				return null;
			}
			typeExpr.loc = this.loc;
			this.type = typeExpr.Type;
			List<MissingTypeSpecReference> missingDependencies = this.type.GetMissingDependencies();
			if (missingDependencies != null)
			{
				ImportedTypeDefinition.Error_MissingDependency(mc, missingDependencies, this.loc);
			}
			if (this.type.Kind == MemberKind.Void)
			{
				mc.Module.Compiler.Report.Error(673, this.loc, "System.Void cannot be used from C#. Consider using `void'");
			}
			if (!(mc is TypeDefinition.BaseContext) && !(mc is UsingAliasNamespace.AliasContext))
			{
				ObsoleteAttribute attributeObsolete = this.type.GetAttributeObsolete();
				if (attributeObsolete != null && !mc.IsObsolete)
				{
					AttributeTester.Report_ObsoleteMessage(attributeObsolete, typeExpr.GetSignatureForError(), base.Location, mc.Module.Compiler.Report);
				}
			}
			return this.type;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0006E0AD File Offset: 0x0006C2AD
		public override void Emit(EmitContext ec)
		{
			throw new InternalErrorException("FullNamedExpression `{0}' found in resolved tree", new object[]
			{
				this.GetSignatureForError()
			});
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00068BDB File Offset: 0x00066DDB
		protected FullNamedExpression()
		{
		}
	}
}
