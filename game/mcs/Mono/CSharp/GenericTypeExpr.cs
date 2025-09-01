using System;

namespace Mono.CSharp
{
	// Token: 0x02000228 RID: 552
	internal class GenericTypeExpr : TypeExpr
	{
		// Token: 0x06001C10 RID: 7184 RVA: 0x00087C30 File Offset: 0x00085E30
		public GenericTypeExpr(TypeSpec open_type, TypeArguments args, Location l)
		{
			this.open_type = open_type;
			this.loc = l;
			this.args = args;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0003194B File Offset: 0x0002FB4B
		public override string GetSignatureForError()
		{
			return this.type.GetSignatureForError();
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00087C50 File Offset: 0x00085E50
		public override TypeSpec ResolveAsType(IMemberContext mc, bool allowUnboundTypeArguments = false)
		{
			if (this.eclass != ExprClass.Unresolved)
			{
				return this.type;
			}
			if (!this.args.Resolve(mc, allowUnboundTypeArguments))
			{
				return null;
			}
			TypeSpec[] arguments = this.args.Arguments;
			if (arguments == null)
			{
				return null;
			}
			InflatedTypeSpec inflatedTypeSpec = this.open_type.MakeGenericType(mc, arguments);
			this.type = inflatedTypeSpec;
			this.eclass = ExprClass.Type;
			if (!inflatedTypeSpec.HasConstraintsChecked && mc.Module.HasTypesFullyDefined)
			{
				TypeParameterSpec[] constraints = inflatedTypeSpec.Constraints;
				if (constraints != null)
				{
					ConstraintChecker constraintChecker = new ConstraintChecker(mc);
					if (constraintChecker.CheckAll(this.open_type, arguments, constraints, this.loc))
					{
						inflatedTypeSpec.HasConstraintsChecked = true;
					}
				}
			}
			return this.type;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00087CF8 File Offset: 0x00085EF8
		public override bool Equals(object obj)
		{
			GenericTypeExpr genericTypeExpr = obj as GenericTypeExpr;
			return genericTypeExpr != null && this.type != null && genericTypeExpr.type != null && this.type == genericTypeExpr.type;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00087D31 File Offset: 0x00085F31
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000A60 RID: 2656
		private TypeArguments args;

		// Token: 0x04000A61 RID: 2657
		private TypeSpec open_type;
	}
}
