using System;
using System.Reflection;

namespace Mono.CSharp
{
	// Token: 0x020001C8 RID: 456
	public class EnumMember : Const
	{
		// Token: 0x06001807 RID: 6151 RVA: 0x00073D26 File Offset: 0x00071F26
		public EnumMember(Enum parent, MemberName name, Attributes attrs) : base(parent, new EnumMember.EnumTypeExpr(), Modifiers.PUBLIC, name, attrs)
		{
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00073D38 File Offset: 0x00071F38
		private static bool IsValidEnumType(TypeSpec t)
		{
			switch (t.BuiltinType)
			{
			case BuiltinTypeSpec.Type.Byte:
			case BuiltinTypeSpec.Type.SByte:
			case BuiltinTypeSpec.Type.Char:
			case BuiltinTypeSpec.Type.Short:
			case BuiltinTypeSpec.Type.UShort:
			case BuiltinTypeSpec.Type.Int:
			case BuiltinTypeSpec.Type.UInt:
			case BuiltinTypeSpec.Type.Long:
			case BuiltinTypeSpec.Type.ULong:
				return true;
			default:
				return t.IsEnum;
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00073D84 File Offset: 0x00071F84
		public override Constant ConvertInitializer(ResolveContext rc, Constant expr)
		{
			if (expr is EnumConstant)
			{
				expr = ((EnumConstant)expr).Child;
			}
			Enum @enum = (Enum)this.Parent;
			TypeSpec underlyingType = @enum.UnderlyingType;
			if (expr != null)
			{
				expr = expr.ImplicitConversionRequired(rc, underlyingType);
				if (expr != null && !EnumMember.IsValidEnumType(expr.Type))
				{
					@enum.Error_UnderlyingType(base.Location);
					expr = null;
				}
			}
			if (expr == null)
			{
				expr = New.Constantify(underlyingType, base.Location);
			}
			return new EnumConstant(expr, base.MemberType);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00073E04 File Offset: 0x00072004
		public override bool Define()
		{
			if (!this.ResolveMemberType())
			{
				return false;
			}
			this.FieldBuilder = this.Parent.TypeBuilder.DefineField(base.Name, base.MemberType.GetMetaInfo(), FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			this.spec = new ConstSpec(this.Parent.Definition, this, base.MemberType, this.FieldBuilder, base.ModFlags, this.initializer);
			this.Parent.MemberCache.AddMember(this.spec);
			return true;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00073E8A File Offset: 0x0007208A
		public override void Accept(StructuralVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x020003AE RID: 942
		private class EnumTypeExpr : TypeExpr
		{
			// Token: 0x06002708 RID: 9992 RVA: 0x000BAD9A File Offset: 0x000B8F9A
			public override TypeSpec ResolveAsType(IMemberContext ec, bool allowUnboundTypeArguments)
			{
				this.type = ec.CurrentType;
				this.eclass = ExprClass.Type;
				return this.type;
			}

			// Token: 0x06002709 RID: 9993 RVA: 0x000BADB5 File Offset: 0x000B8FB5
			public EnumTypeExpr()
			{
			}
		}
	}
}
