using System;

namespace Mono.CSharp
{
	// Token: 0x020002DD RID: 733
	internal class PrimaryConstructorField : Field
	{
		// Token: 0x060022C6 RID: 8902 RVA: 0x000AB69C File Offset: 0x000A989C
		public PrimaryConstructorField(TypeDefinition parent, Parameter parameter) : base(parent, new PrimaryConstructorField.TypeExpressionFromParameter(parameter), Modifiers.PRIVATE, new MemberName(parameter.Name, parameter.Location), null)
		{
			this.caching_flags |= (MemberCore.Flags.IsUsed | MemberCore.Flags.IsAssigned);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0003F215 File Offset: 0x0003D415
		public override string GetSignatureForError()
		{
			return base.MemberName.Name;
		}

		// Token: 0x02000404 RID: 1028
		private sealed class TypeExpressionFromParameter : TypeExpr
		{
			// Token: 0x06002839 RID: 10297 RVA: 0x000BECBA File Offset: 0x000BCEBA
			public TypeExpressionFromParameter(Parameter parameter)
			{
				this.parameter = parameter;
				this.eclass = ExprClass.Type;
				this.loc = parameter.Location;
			}

			// Token: 0x0600283A RID: 10298 RVA: 0x000BECDC File Offset: 0x000BCEDC
			public override TypeSpec ResolveAsType(IMemberContext mc, bool allowUnboundTypeArguments)
			{
				return this.parameter.Type;
			}

			// Token: 0x0400116A RID: 4458
			private Parameter parameter;
		}
	}
}
